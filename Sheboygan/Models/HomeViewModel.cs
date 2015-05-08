using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sheboygan.Models
{
    public class HomeViewModel
    {
        public bool HasSampleResources { get; set; }

        public string AjaxLayout { get; set; }

        public string FlexLayout { get; set; }
    }
}