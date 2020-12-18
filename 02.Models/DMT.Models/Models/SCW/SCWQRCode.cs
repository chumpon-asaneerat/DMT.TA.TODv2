#region Using

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace DMT.Models
{
    public class SCWQRCode
    {
        public DateTime? trxDateTime { get; set; }
        public decimal? amount { get; set; }
        public string approvCode { get; set; }
        public string refNo { get; set; }
        public string staffId { get; set; }
        public string staffNameTh { get; set; }
        public string staffNameEn { get; set; }
        public int laneId { get; set; }

        [JsonIgnore]
        public string trxDateTimeString
        {
            get
            {
                if (!trxDateTime.HasValue) return string.Empty;
                return (trxDateTime.Value == DateTime.MinValue) ? string.Empty : trxDateTime.Value.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
            }
            set { }
        }

        public override int GetHashCode()
        {
            decimal amt = (amount.HasValue) ? amount.Value : decimal.Zero;
            DateTime dt = (trxDateTime.HasValue) ?
                trxDateTime.Value : DateTime.MinValue;
            string dtStr = dt.ToDateTimeString();
            string value = string.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6}_{7}",
                this.staffId, this.staffNameEn, this.staffNameTh,
                this.laneId,
                this.refNo, this.approvCode, amt,
                dtStr);
            return value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            return obj.GetHashCode() == this.GetHashCode();
        }
    }

    public class SCWQRCodeResult
    {
        public List<SCWQRCode> list { get; set; }
        public SCWStatus status { get; set; }
    }
}
