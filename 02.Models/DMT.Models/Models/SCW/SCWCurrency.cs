#region Using

using System.Collections.Generic;
using NLib.Reflection;

#endregion

namespace DMT.Models
{
    public class SCWCurrency
    {
        [PropertyMapName("currencyId")]
        public int currencyId { get; set; }

        [PropertyMapName("currencyDenomId")]
        public int currencyDenomId { get; set; }
        
        [PropertyMapName("abbreviation")]
        public string abbreviation { get; set; }
        
        [PropertyMapName("description")]
        public string description { get; set; }
        
        [PropertyMapName("denomValue")]
        public decimal denomValue { get; set; }
        
        [PropertyMapName("denomTypeId")]
        public int denomTypeId { get; set; }
    }

    public class SCWCurrencyList
    {
        public List<SCWCurrency> list { get; set; }
        public SCWStatus status { get; set; }
    }
}
