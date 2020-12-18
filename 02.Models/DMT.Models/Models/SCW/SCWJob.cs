#region Using

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace DMT.Models
{
    public class SCWJob
    {
        public int? networkId { get; set; }
        public int? plazaId { get; set; }
        public int? laneId { get; set; }
        public int? jobNo { get; set; }
        public string staffId { get; set; }
        public DateTime? bojDateTime { get; set; }
        public DateTime? eojDateTime { get; set; }

        //TODO: Required properties (non json serialization)
        // FullNameEN/TH
        // Selected (??? json serialize required?)
        // boj/eoj Date, Time string.
    }

    public class SCWJobList
    {
        public List<SCWJob> list { get; set; }
        public SCWStatus status { get; set; }
    }
}
