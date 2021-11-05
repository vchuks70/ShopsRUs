using Data.Entity;
using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Service.Interface;
using Service.Request;
using Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class InvoiceService : IInvoice
    {
        public ApplicationDbContext DbContext { get; set; }

        public InvoiceService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<GlobalResponse<InvoiceTotalAmountResponse>> GetTotalInvoiceAmount(CreateInvoiceRequest model)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(x=> x.Id == model.CustomerId);
            if (customer is null)
            {
                return new GlobalResponse<InvoiceTotalAmountResponse>
                {
                    Message = "Invalid  customer Id",
                    Status = false,
                    Data = null
                };
            }

            List<InvoiceProducts> InvoiceProducts = new List<InvoiceProducts>();
            List<InvoiceTotalHelper> productsHelper = new List<InvoiceTotalHelper>();

            foreach (var item in model.InvoiceProductsRequests)
            {
                var product = await DbContext.Products.SingleOrDefaultAsync(x => x.Id == item.ProductId);

                if (product is null)
                {
                    return new GlobalResponse<InvoiceTotalAmountResponse>
                    {
                        Message = "Invalid product Id",
                        Status = false,
                        Data = null
                    };
                }
                productsHelper.Add(new InvoiceTotalHelper { Price = product.Price * item.   Count, IsGroceries = product.IsGrocery });
                InvoiceProducts.Add(new InvoiceProducts { 
                CreateOn = DateTime.Now,
                Products = product,
                Price = product.Price
                });

                }
            var discountEligibleProductPrice = productsHelper.Where(x => x.IsGroceries == false).Sum(x => x.Price);
            var discountNonEligibleProductPrice = productsHelper.Where(x => x.IsGroceries == true).Sum(x => x.Price);

            var invoice = new Data.Invoice
            {
                CompanyAddress = model.CompanyAddress,
                DeliveryDate = model.DeliveryDate,
                IssueDate = model.IssueDate,
                PaymentDueDate = model.PaymentDueDate,
                ReferenceNumber = model.ReferenceNumber,
                SellersName = model.SellersName,
                Customer = customer,
                InvoiceProducts = InvoiceProducts
            };
            await DbContext.Invoices.AddAsync(invoice);
            await DbContext.SaveChangesAsync();
            if (model.IsAffiliateStore)
            {
                
                var discountedPrice = discountEligibleProductPrice - (discountEligibleProductPrice * 10 /100);
               var totalPrice = discountNonEligibleProductPrice + discountedPrice;
                var discountPerHundereds = (totalPrice / 100) * 5;
                var finalResult = totalPrice - discountPerHundereds;
                return new GlobalResponse<InvoiceTotalAmountResponse>
                {
                    Message = "Successful",
                    Status = true,
                    Data = new InvoiceTotalAmountResponse { TotalAmount = finalResult }
                };
            }

            if (model.IsEmployeeStore)
            {
        
                var discountedPrice = discountEligibleProductPrice - (discountEligibleProductPrice * 30 / 100);
                var totalPrice = discountNonEligibleProductPrice + discountedPrice;
                var discountPerHundereds = (totalPrice / 100) * 5;
                var finalResult = totalPrice - discountPerHundereds;
                return new GlobalResponse<InvoiceTotalAmountResponse>
                {
                    Message = "Successful",
                    Status = true,
                    Data = new InvoiceTotalAmountResponse { TotalAmount = finalResult }
                };
            }

            if (model.IsECustomerOverTwoYears)
            {

                var discountedPrice = discountEligibleProductPrice - (discountEligibleProductPrice * 5 / 100);
                var totalPrice = discountNonEligibleProductPrice + discountedPrice;
                var discountPerHundereds = (totalPrice / 100) * 5;
                var finalResult = totalPrice - discountPerHundereds;
                return new GlobalResponse<InvoiceTotalAmountResponse>
                {
                    Message = "Successful",
                    Status = true,
                    Data = new InvoiceTotalAmountResponse { TotalAmount = finalResult }
                };
            }


            else
            {
            
                var totalPrice = discountNonEligibleProductPrice + discountEligibleProductPrice;
                var discountPerHundereds = (totalPrice / 100) * 5;
                var finalResult = totalPrice - discountPerHundereds;
                return new GlobalResponse<InvoiceTotalAmountResponse>
                {
                    Message = "Successful",
                    Status = true,
                    Data = new InvoiceTotalAmountResponse { TotalAmount = finalResult }
                };

            
            }

        }
    }
}
