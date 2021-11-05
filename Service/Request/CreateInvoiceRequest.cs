using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Request
{
   public class CreateInvoiceRequest
    {
        [Required]
        public ICollection<InvoiceProductsRequest> InvoiceProductsRequests { get; set; }
        public bool IsAffiliateStore { get; set; }  
        public bool IsEmployeeStore { get; set; }
        public bool IsECustomerOverTwoYears { get; set; }

        [Required]
        public string SellersName { get; set; }
        [Required]
        public string CompanyAddress { get; set; }

        [Required]
        public int CustomerId { get; set; }


        [Required]
        public DateTime IssueDate { get; set; }
        [Required]
        public DateTime PaymentDueDate { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public int ReferenceNumber { get; set; }

    }

    public class InvoiceProductsRequest 
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
