using AutoMapper;
using ComputerVision.Api;
using ComputerVision.Models;
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
       //Computer Vision - Cognitive Services Subscription Key
        const string subscriptionKey = "09fe4860fcb541e896f8fffb68e0597d";
       
        const string uriBase =
            "https://manuhackathon.cognitiveservices.azure.com/vision/v2.0/read/core/asyncBatchAnalyze";

        string _sourcePath = string.Empty;
        string _fileName = string.Empty;
        IMapper _mapper;

        OcrClient _orcClient;

        public FolderWatcher(string sourcePath)
        {            
            this._sourcePath = sourcePath;
            this._orcClient = new OcrClient();          


            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<string, decimal>().ConvertUsing(new DecimalTypeConverter());
                cfg.CreateMap<PolicyInfoDTO, PolicyInfo>();
            });
            configuration.AssertConfigurationIsValid();
            _mapper = configuration.CreateMapper();
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
                watcher.Filter = "*.jpg*";

                // Add event handlers.
                //watcher.Changed += OnChanged;
                watcher.Created += OnCreated;
                //watcher.Deleted += OnChanged;
                //watcher.Renamed += OnRenamed;

                // Begin watching.
                watcher.EnableRaisingEvents = true;
                
               
                // Wait for the user to quit the program.
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("SASTY - OCR Engine Initialized");
                Console.ResetColor();
                Console.WriteLine("------------------------------------");
                Console.WriteLine("TO EXIT... PRESS q");
                Console.WriteLine("------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("SASTY FILE WATCHER IS NOW LISTENING......");
                Console.ResetColor();
                Console.WriteLine("------------------------------------");
               
                while (Console.Read() != 'q' || Console.Read() != 'Q') ;
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
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
            Console.WriteLine("------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("FILE READING IN PROGRESS..........");
            Console.ResetColor();
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
                    Console.WriteLine("------------------------------------");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("FILE READING ERROR !!!!!!!!!");
                    Console.ResetColor();
                    Console.WriteLine("\n\nError Response:\n{0}\n",
                        JToken.Parse(errorString).ToString());
                    Console.WriteLine("------------------------------------");
                   
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
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("\nTimeout error.\n");
                    Console.WriteLine("------------------------------------");
                    return;
                }

                // Display the JSON response.               
                //Console.WriteLine("\nResponse:\n\n{0}\n",
                //    JToken.Parse(contentString).ToString());

                JObject o1 = (JObject)JToken.Parse(contentString);

                List<String> OCRData =
                    (from p in o1["recognitionResults"][0]["lines"]
                    select (string)p["text"]).ToList();
                Console.WriteLine("------------------------------------");
                Console.WriteLine("OCR Read Content... ");
                Console.ForegroundColor = ConsoleColor.White;
                foreach(String s in OCRData)
                {
                    Console.WriteLine(s);
                }               
                Console.ResetColor();
                Console.WriteLine("------------------------------------");
                var jsonPath = Path.Combine(Directory.GetParent(Directory.GetParent("Mappings.json").FullName).FullName.Replace("\\bin", ""), "Mappings.json");
                var jsonstring = System.IO.File.ReadAllText(jsonPath);

                Mappings yourObject = new JavaScriptSerializer().Deserialize<Mappings>(jsonstring);
                var customerDTO = ModelMapping(yourObject, OCRData);

                var model = _mapper.Map<PolicyInfoDTO, PolicyInfo>(customerDTO);
                Console.WriteLine("------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("File Reading Completed Successfully");
                Console.ResetColor();
                Console.WriteLine("------------------------------------");
                await _orcClient.PostPolicyInfoAsync(model);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Policy Data Pushed to Database");
                Console.ResetColor();
                Console.WriteLine("------------------------------------");
                //Archieve the file
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("File Archiving in Progress...");
                ArchiveFile(_fileName);
                Console.WriteLine("File Archiving Done");
                Console.ResetColor();
                Console.WriteLine("------------------------------------");


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

        PolicyInfoDTO ModelMapping(Mappings mappings, List<string> OCRData)
        {
            int IndexInitial;
            int IndexFinal;            

            List<MappingElement> map = mappings.MappingElement;
            PolicyInfoDTO polInfo = new PolicyInfoDTO();

            foreach(MappingElement m in map)
            {
                String Fieldname = m.Field;
                int AdjustIndex = m.AdjustIndex;               
                IndexInitial = OCRData.FindIndex(x => x.Contains(m.InitialPosition));
                if (m.FinalPosition != null)
                    IndexFinal = OCRData.FindIndex(x => x.Contains(m.FinalPosition));
                else
                    IndexFinal = -1;
                StringBuilder Modelvalue = new StringBuilder();
                if (AdjustIndex <=0 && IndexInitial >=0 && IndexFinal >=0)
                {
                    for (int i = IndexInitial + 1; i < IndexFinal; i++)
                    {
                        Modelvalue.Append(OCRData[i] + " ");
                    }
                }
                else if(AdjustIndex > 0)
                {
                    Modelvalue.Append(OCRData[IndexInitial + AdjustIndex]);
                }
                Type myType = typeof(PolicyInfoDTO);
                PropertyInfo myPropInfo = myType.GetProperty(Fieldname);
                myPropInfo.SetValue(polInfo, Modelvalue.ToString().Trim().Replace("Date : ", ""));
            }

            return polInfo;
        }        
    }

    public class DecimalTypeConverter : ITypeConverter<string, decimal>
    {
        public decimal Convert(string source, decimal destination, ResolutionContext context)
        {
            decimal amount = 0;
            Decimal.TryParse(source.Replace(" ", ""), out amount);
            return amount;
        }
    }
}
