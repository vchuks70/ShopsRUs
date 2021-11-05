using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Service.Request;
using Service.Services;
using ShopRUsTest.Infastructure;
using Xunit;

namespace ShopRUsTest
{
    public class GeneralUnitTest
    {
       private ApplicationDbContext context = DbInitializerTest.DbContextHelperTest();

        [Fact]
        public async Task CreateCustomer()
        {

            var customerService = new CustomerService(context);



            CreateCustomerRequest newCustomer = new CreateCustomerRequest
            {
                Name = "Andre Wiggans",
                Age = 76,
                Gender = "Male",
                Address = "test4",
                Email = "test4@gmail.com"




            };

            var result = await customerService.Createcustomer(newCustomer);

            Assert.True(result.Status);



        }

        [Fact]
        public async Task GetCustomerById()
        {




            var customerService = new CustomerService(context);






            var result = await customerService.GetCustomerById(1);

            Assert.True(result.Status);



        }


        [Fact]
        public async Task GetCustomerByName()
        {


            // Arrange

            var customerService = new CustomerService(context);


            // Act

            var name = "David John";

            var result = await customerService.GetCustomerByName(name);

            // Assert

            Assert.True(result.Status);



        }

        [Fact]
        public async Task GetCustomerAll()
        {


            //var newCustomer = new Customer
            //{
            //    Name = "David John",
            //    Age = 56,
            //    Gender = "Male",
            //    Address = "test",
            //    Email = "test@gmail.com",
            //    DateCreated = DateTime.Now





            //};
            //context.Customers.Add(newCustomer);
            //context.SaveChanges();

            var customerService = new CustomerService(context);

            //  public string Name { get; set; }

            //public int Age { get; set; }
            //public string Gender { get; set; }
            //public string Phonenumber { get; set; }
            //public string Email { get; set; }
            //public string Address { get; set; }
            //public DateTime DateCreated { get; set; }




            var result = await customerService.GetAllCustomers();

            Assert.True(result.Status);



        }


        [Fact]
        public async Task CreateDiscount()
        {

            var discountsService = new DiscountsService(context);



            DiscountsRequest newDiscount = new DiscountsRequest
            {
                DiscountType = "cash",
                PercentageProperty = 10




            };

            var result = await discountsService.CreateDiscount(newDiscount);

            Assert.True(result.Status);



        }




        [Fact]
        public async Task GetDiscountByType()
        {




            var discountsService = new DiscountsService(context);




            var name = "credit card";

            var result = await discountsService.GetDiscountByType(name);

            Assert.True(result.Status);



        }

        [Fact]
        public async Task GetDiscountAll()
        {




            var discountService = new DiscountsService(context);






            var result = await discountService.GetAllDiscount();

            Assert.True(result.Status);



        }

        [Fact]
        public async Task InvoiceAmount()
        {

          
       


        var invoiceService = new InvoiceService(context);


            var request = new CreateInvoiceRequest()
            {
                InvoiceProductsRequests = new List<InvoiceProductsRequest>(){
                 new InvoiceProductsRequest()
        {
            ProductId = 1,
            Count = 2
        }
            },
                ReferenceNumber = 1223,
                DeliveryDate = DateTime.Now,
                PaymentDueDate = DateTime.Now,
                IssueDate = DateTime.Now,
                CustomerId = 1,
                CompanyAddress = "test",
                IsAffiliateStore = true,
                IsECustomerOverTwoYears = false,
                IsEmployeeStore = false,
                SellersName = "test"



            };



            var result = await invoiceService.GetTotalInvoiceAmount(request);

            Assert.True(result.Status);



        }

        [Fact]
        public void Cleanup()
        {
            context.Database.EnsureDeleted(); // Remove from memory
            context.Dispose();
        }
    }
}
