using ComputerVision.Api;
using ComputerVision.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ComputerVision
{
    public class FolderWatcher
    {
        // Replace <Subscription Key> with your valid subscription key.
        const string subscriptionKey = "09fe4860fcb541e896f8fffb68e0597d";

        // You must use the same Azure region in your REST API method as you used to
        // get your subscription keys. For example, if you got your subscription keys
        // from the West US region, replace "westcentralus" in the URL
        // below with "westus".
        //
        // Free trial subscription keys are generated in the "westcentralus" region.
        // If you use a free trial subscription key, you shouldn't need to change
        // this region.
        const string uriBase =
            "https://manuhackathon.cognitiveservices.azure.com/vision/v2.0/read/core/asyncBatchAnalyze";

        string _sourcePath = string.Empty;
        string _fileName = string.Empty;

        OcrClient _orcClient;

        public FolderWatcher(string sourcePath)
        {            
            this._sourcePath = sourcePath;
            this._orcClient = new OcrClient();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Run()
        {
            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = _sourcePath;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                // Only watch text files.
                watcher.Filter = "*.*";

                // Add event handlers.
                //watcher.Changed += OnChanged;
                watcher.Created += OnCreated;
                //watcher.Deleted += OnChanged;
                //watcher.Renamed += OnRenamed;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;
            }
        }

        void ArchiveFile(string fileName)
        {            
            string sourceFile = Path.Combine(_sourcePath, fileName);
            string destFile = Path.Combine(_sourcePath, "Archive", fileName);
            File.Move(sourceFile, destFile);
        }

        // Define the event handlers.
        void OnCreated(object source, FileSystemEventArgs e)
        {
            _fileName = e.Name;
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
            ReadText(e.FullPath).Wait();
        }

        async Task ReadText(string imageFilePath)
        {
            try
            {
               
                HttpClient client = new HttpClient();

                // Request headers.
                client.DefaultRequestHeaders.Add(
                    "Ocp-Apim-Subscription-Key", subscriptionKey);

                // Assemble the URI for the REST API method.
                string uri = uriBase;

                HttpResponseMessage response;

                // Two REST API methods are required to extract text.
                // One method to submit the image for processing, the other method
                // to retrieve the text found in the image.

                // operationLocation stores the URI of the second REST API method,
                // returned by the first REST API method.
                string operationLocation;

                // Reads the contents of the specified local image
                // into a byte array.
                byte[] byteData = GetImageAsByteArray(imageFilePath);

                // Adds the byte array as an octet stream to the request body.
                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    // This example uses the "application/octet-stream" content type.
                    // The other content types you can use are "application/json"
                    // and "multipart/form-data".
                    content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");

                    // The first REST API method, Batch Read, starts
                    // the async process to analyze the written text in the image.
                    response = await client.PostAsync(uri, content);
                }

                // The response header for the Batch Read method contains the URI
                // of the second method, Read Operation Result, which
                // returns the results of the process in the response body.
                // The Batch Read operation does not return anything in the response body.
                if (response.IsSuccessStatusCode)
                    operationLocation =
                        response.Headers.GetValues("Operation-Location").FirstOrDefault();
                else
                {
                    // Display the JSON error data.
                    string errorString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("\n\nResponse:\n{0}\n",
                        JToken.Parse(errorString).ToString());
                    return;
                }

                // If the first REST API method completes successfully, the second 
                // REST API method retrieves the text written in the image.
                //
                // Note: The response may not be immediately available. Text
                // recognition is an asynchronous operation that can take a variable
                // amount of time depending on the length of the text.
                // You may need to wait or retry this operation.
                //
                // This example checks once per second for ten seconds.
                string contentString;
                int i = 0;
                do
                {
                    System.Threading.Thread.Sleep(1000);
                    response = await client.GetAsync(operationLocation);
                    contentString = await response.Content.ReadAsStringAsync();
                    ++i;
                }
                while (i < 10 && contentString.IndexOf("\"status\":\"Succeeded\"") == -1);

                if (i == 10 && contentString.IndexOf("\"status\":\"Succeeded\"") == -1)
                {
                    Console.WriteLine("\nTimeout error.\n");
                    return;
                }

                // Display the JSON response.
                Console.WriteLine("\nResponse:\n\n{0}\n",
                    JToken.Parse(contentString).ToString());
                Console.WriteLine("\nResponse:\n\n{0}\n",
                                   JToken.Parse(contentString).ToString());
                JObject o1 = (JObject)JToken.Parse(contentString);

                List<String> OCRData =
                    (from p in o1["recognitionResults"][0]["lines"]
                    select (string)p["text"]).ToList();

                var jsonstring = Path.Combine(Directory.GetParent(Directory.GetParent("Mappings.json").FullName).FullName.Replace("\\bin", ""), "Mappings.json");
                
                Mappings yourObject = new JavaScriptSerializer().Deserialize<Mappings>(jsonstring);
                ModelMapping(yourObject, OCRData);
                var model = ToCustomerDetails(OCRData);
                await _orcClient.PostPolicyInfoAsync(model);

                //Archieve the file
                ArchiveFile(_fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        byte[] GetImageAsByteArray(string imageFilePath)
        {
            // Open a read-only file stream for the specified file.
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                // Read the file's contents into a byte array.
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }

        PolicyInfo ModelMapping(Mappings mappings, List<string> OCRData)
        {
            int IndexInitial;
            int IndexFinal;            

            List<MappingElement> map = mappings.MappingElement;
            PolicyInfo polInfo = new PolicyInfo();

            foreach(MappingElement m in map)
            {
                String Fieldname = m.Field;
                int AdjustIndex = m.AdjustIndex;               
                IndexInitial = OCRData.FindIndex(x => x.Equals(m.InitialPosition));
                IndexFinal = OCRData.FindIndex(x => x.Equals(m.FinalPosition));
                StringBuilder Modelvalue = new StringBuilder();
                for(int i=IndexInitial+1; i<IndexFinal; i++)
                {
                    Modelvalue.Append(OCRData[i]);
                }
                Type myType = typeof(PolicyInfo);
                PropertyInfo myPropInfo = myType.GetProperty(Fieldname);
                myPropInfo.SetValue(polInfo, Modelvalue.ToString());
            }

            return polInfo;
        }

        PolicyInfo ToCustomerDetails(List<string> postTitles)
        {
            int nameIndex = postTitles.FindIndex(x => x.Contains("Full Name"));
            int idIndex = postTitles.FindIndex(x => x.Contains("NRIC / Passport / FIN"));
            int nationIndex = postTitles.FindIndex(x => x.Contains("Nationality"));
            int dobStart = postTitles.FindIndex(x => x.Contains("Date of Birth"));
            int dobEnd = postTitles.FindIndex(x => x.Contains("DD - MM - YYYY"));
            int genderStart = postTitles.FindIndex(x => x.Contains("Gender"));
            int genderEnd = postTitles.FindIndex(x => x.Contains("M - Male / F - Female"));
            int maritalStart = postTitles.FindIndex(x => x.Contains("Marital Status"));
            int maritalEnd = postTitles.FindIndex(x => x.Contains("S - Single / M - Married"));
            int addressIndex = postTitles.FindIndex(x => x.Contains("Residential Address"));
            int contactIndex = postTitles.FindIndex(x => x.Contains("Contact Details"));
            int mobileNoIndex = postTitles.FindIndex(x => x.Contains("Mobile number"));
            int homeNoIndex = postTitles.FindIndex(x => x.Contains("Home number"));
            int sectionBIndex = postTitles.FindIndex(x => x.Contains("Section B - Plan Details"));


            var names = postTitles.Skip(nameIndex + 1).Take(idIndex - (nameIndex + 1));
            var fullName = String.Join(" ", names);
            
            var ids = postTitles.Skip(idIndex + 1).Take(nationIndex - (idIndex + 1));
            var id = String.Join(" ", ids);

            var nationalities = postTitles.Skip(nationIndex + 1).Take(dobStart - (nationIndex + 1));
            var nationality = String.Join(" ", nationalities);

            var dob = postTitles.Skip(dobStart + 1).Take(dobEnd - (dobStart + 1)).FirstOrDefault().Replace(".", "-");

            var genders = postTitles.Skip(genderStart + 1).Take(genderEnd - (genderStart + 1));
            var gender = String.Join(" ", genders);
            
            var maritalStatuses = postTitles.Skip(maritalStart + 1).Take(maritalEnd - (maritalStart + 1));
            var maritalStatus = String.Join(" ", maritalStatuses);

            var addresses = postTitles.Skip(addressIndex + 1).Take(contactIndex - (addressIndex + 1));
            var address = String.Join(" ", addresses);

            var mobileNos = postTitles.Skip(mobileNoIndex + 1).Take(homeNoIndex - (mobileNoIndex + 1));
            var mobileNo = String.Join(" ", mobileNos);

            var homeNos = postTitles.Skip(homeNoIndex + 1).Take(sectionBIndex - (homeNoIndex + 1));
            var homeNo = String.Join(" ", homeNos);

            return new PolicyInfo
            {
                FullName = fullName,
                IdNumber = id,
                Nationality = nationality,
                DateOfBirth = dob,
                Gender = gender,
                MaritalStatus = maritalStatus,
                Address = address,
                Mobile = mobileNo,
                HomeNumber = homeNo
            };
        }
    } 
}
