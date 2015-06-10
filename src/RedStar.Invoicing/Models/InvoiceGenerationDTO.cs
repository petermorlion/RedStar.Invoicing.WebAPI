using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedStar.Invoicing.Models
{
    public class InvoiceGenerationDTO
    {
        public string InvoiceTemplate { get; set; }
        public string LogoUrl { get; set; }
    }
}
