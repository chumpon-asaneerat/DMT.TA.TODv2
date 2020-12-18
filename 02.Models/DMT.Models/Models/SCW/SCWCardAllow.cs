#region Using

using System.Collections.Generic;
using NLib.Reflection;

#endregion

namespace DMT.Models
{
	public class SCWCardAllow
	{
		[PropertyMapName("cardAllowId")]
		public int cardAllowId { get; set; }

		[PropertyMapName("abbreviation")]
		public string abbreviation { get; set; }

		[PropertyMapName("description")]
		public string description { get; set; }
	}

	public class SCWCardAllowList
	{
		public List<SCWCardAllow> list { get; set; }
		public SCWStatus status { get; set; }
	}
}
