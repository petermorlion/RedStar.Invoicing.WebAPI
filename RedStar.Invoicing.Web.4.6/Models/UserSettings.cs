using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedStar.Invoicing.Web._4._6.Models
{
    public class UserSettings
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string LogoUrl { get; set; }
        public string InvoiceTemplate { get; set; }
    }
}
