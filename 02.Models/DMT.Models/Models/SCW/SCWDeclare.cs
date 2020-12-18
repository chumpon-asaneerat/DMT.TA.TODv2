#region Using

using System;
using System.Collections.Generic;

#endregion

namespace DMT.Models
{
    public class SCWDeclareCash
    {
        public int currencyId { get; set; }
        public int currencyDenomId { get; set; }
        public decimal denomValue { get; set; }

        public int number { get; set; }
        public decimal total { get; set; }
    }

    public class SCWDeclareCoupon
    {
        public int couponId { get; set; }
        public decimal couponValue { get; set; }

        public int number { get; set; }
        public decimal total { get; set; }
    }

    public class SCWDeclareCouponBook
    {
        public int couponBookId { get; set; }
        public decimal couponBookValue { get; set; }
        public int number { get; set; }
        public decimal total { get; set; }
    }

    public class SCWDeclareFreePass
    {
        public int cardAllowId { get; set; }
        public int number { get; set; }
    }

    public class SCWDeclareQRCode
    {
        public string approvalCode { get; set; }
        public DateTime trxDate { get; set; }
        public decimal amount { get; set; }
    }

    public class SCWDeclareEMV
    {
        public string approvalCode { get; set; }
        public DateTime trxDate { get; set; }
        public decimal amount { get; set; }
    }

    public class SCWDeclareResult
    {
        public SCWStatus status { get; set; }
    }

    public class SCWDeclare
    {
        public int? networkId { get; set; }
        public int? plazaId { get; set; }
        public string staffId { get; set; }
        public string bagNumber { get; set; }
        public string safetyBeltNumber { get; set; }

        public int? shiftTypeId { get; set; }

        public DateTime? declareDateTime { get; set; }
        public DateTime? attendanceDateTime { get; set; }
        public DateTime? departureDateTime { get; set; }
        public DateTime? operationDate { get; set; }

        public string declareById { get; set; }
        public string declareByName { get; set; }

        public decimal? cashTotalAmount { get; set; }
        public decimal? couponTotalAmount { get; set; }
        public decimal? couponBookTotalAmount { get; set; }
        public decimal? cardAllowTotalAmount { get; set; }

        public decimal? qrcodeTotalAmount { get; set; }
        public decimal? emvTotalAmount { get; set; }
        public decimal? otherTotalAmount { get; set; }
        public string cashRemark { get; set; }
        public string otherRemark { get; set; }

        public string chiefId { get; set; }
        public string chiefName { get; set; }

        public List<SCWJob> jobList { get; set; }

        public List<SCWDeclareCash> cashList { get; set; }
        public List<SCWDeclareCoupon> couponList { get; set; }
        public List<SCWDeclareCouponBook> couponBookList { get; set; }
        public List<SCWDeclareFreePass> cardAllowList { get; set; }
        public List<SCWDeclareQRCode> qrcodeList { get; set; }
        public List<SCWDeclareEMV> emvList { get; set; }
    }
}
