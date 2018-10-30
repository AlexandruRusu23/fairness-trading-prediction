using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SmartHack_UI.Models
{
    public class PieDataChartFormat
    {
        public int value { get; set; }
        public string color { get; set; }
        public string highlight { get; set; }
        public string label { get; set; }

    }
}
