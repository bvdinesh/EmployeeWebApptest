using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using EmployeeWebApp.Models;
namespace EmployeeWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("People");
            TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Doe"));
            // Print the fields for each customer.
            List<CustomerEntity> lstEnt = new List<CustomerEntity>();

            foreach (CustomerEntity entity in table.ExecuteQuery(query))
            {
                CustomerEntity ent = new CustomerEntity(entity.PartitionKey, entity.RowKey);
                ent.Email = entity.Email;
                ent.PhoneNumber = entity.PhoneNumber;
                lstEnt.Add(ent);
            }
            return View(lstEnt);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}