using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sheboygan.Models
{
    public class DwfPlotViewModel : MapGuideViewModel
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Scale { get; set; }
    }
}