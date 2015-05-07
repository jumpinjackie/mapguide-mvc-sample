using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sheboygan.Models
{
    public class MapGuideViewerInputModel
    {
        public string Session { get; set; }

        public string MapName { get; set; }
    }

    public class MapGuideViewModel
    {
        public string Session { get; set; }

        public string MapName { get; set; }
    }
}