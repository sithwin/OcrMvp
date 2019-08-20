using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OcrService.Data;
using OcrService.Models;

namespace OcrService.Controllers
{
    public class NewBusinessController : Controller
    {
        public readonly MongoContext context = new MongoContext();

        [HttpPost]
        public void Post([FromBody] PolicyInfo model)
        {
            context.PolicyInfo.InsertOne(model);
        }
    }
}