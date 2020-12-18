#region Using

using System.Collections.Generic;
using NLib.Reflection;

#endregion

namespace DMT.Models
{
    public class SCWCoupon
    {
        [PropertyMapName("couponId")]
        public int couponId { get; set; }

        [PropertyMapName("couponValue")]
        public decimal couponValue { get; set; }

        [PropertyMapName("abbreviation")]
        public string abbreviation { get; set; }

        [PropertyMapName("description")]
        public string description { get; set; }
    }

    public class SCWCouponList
    {
        public List<SCWCoupon> list { get; set; }
        public SCWStatus status { get; set; }
    }
}
