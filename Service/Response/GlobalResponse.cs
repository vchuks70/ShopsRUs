﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Response
{
  public  class GlobalResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data{ get; set; }
    }
}
