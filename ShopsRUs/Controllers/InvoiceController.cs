﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Service.Request;
using Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        public IInvoice InvoiceService { get; set; }

        public InvoiceController(IInvoice invoiceService)
        {
            InvoiceService = invoiceService;
        }

        [HttpPost]
        public async Task<ActionResult<GlobalResponse<InvoiceTotalAmountResponse>>> GetTotalInvoiceAmount( [FromBody] CreateInvoiceRequest model)
        {
            var result = await InvoiceService.GetTotalInvoiceAmount(model);
            return result.Status ? Ok(result) : BadRequest(result);
        }

    }
}
