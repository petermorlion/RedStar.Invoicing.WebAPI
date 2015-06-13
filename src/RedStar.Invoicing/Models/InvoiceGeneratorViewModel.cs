using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedStar.Invoicing.Models
{
    public class InvoiceGeneratorViewModel
    {
        public HtmlString InvoiceTemplate { get; set; }
        public string LogoUrl { get; set; }
    }
}
