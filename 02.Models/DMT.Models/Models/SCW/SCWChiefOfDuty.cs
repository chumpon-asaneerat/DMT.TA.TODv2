#region Using

using System;

#endregion

namespace DMT.Models
{
    public class SCWChiefOfDuty
    {
        public int? networkId { get; set; }
        public int? plazaId { get; set; }
        public string staffId { get; set; }
        // ประเภทพนักงาน 1=chief, 2=sup
        public int? staffTypeId { get; set; } // always 1
        public DateTime? beginDateTime { get; set; }
    }

    public class SCWChiefOfDutyResult
    {
        public SCWStatus status { get; set; }
    }
}
