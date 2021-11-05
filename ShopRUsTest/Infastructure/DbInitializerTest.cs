using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Service.Helper;
using Service.Request;

namespace ShopRUsTest.Infastructure
{
   public class DbInitializerTest
    {
        
        public static  ApplicationDbContext DbContextHelperTest()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ShopRUsTest1")
            .Options;

            var context = new ApplicationDbContext(options);
            var newCustomer = new List<Customer>()
            {
                new Customer
            {
                Name = "David John",
                Age = 56,
                Gender = "Male",
                Address = "test",
                Email = "test@gmail.com",
                DateCreated = DateTime.Now,
                





            },
                     new Customer
            {
                Name = "Smith Rose",
                Age = 57,
                Gender = "Male",
                Address = "test2",
                Email = "test2@gmail.com",
                DateCreated = DateTime.Now





            }
        };
            context.Customers.AddRange(newCustomer);
          
            var discount  = new Discounts
            {
                DiscountType = "credit card",
                PercentageProperty = 30,
                DateCreated = DateTime.Now




            };
            context.Discounts.Add(discount);
            var products = DbSeedingHelper.SeedProducts();
            context.Products.AddRange(products);
            context.SaveChanges();
            return context;
        }
    
    }
}
