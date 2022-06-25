using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CV19.Model
{
    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<ProvinceInfo> ProvinceCounts { get; set; }
    }
}
