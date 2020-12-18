#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using NLib;
using NLib.Design;
using NLib.Reflection;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
// required for JsonIgnore attribute.
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Reflection;

#endregion

namespace DMT.Models
{
	#region TSB

	/// <summary>
	/// The TSB Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("TSB")]
	public class TSB : NTable<TSB>
	{
		#region Intenral Variables

		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;
		private string _NetworkId = string.Empty;

		private bool _Active = false;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public TSB() : base() { }

		#endregion

		#region Public Proprties

		#region Common

		/// <summary>
		/// Gets or sets TSBId.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets TSBId.")]
		[PrimaryKey, MaxLength(10)]
		[PropertyMapName("TSBId")]
		public string TSBId
		{
			get
			{
				return _TSBId;
			}
			set
			{
				if (_TSBId != value)
				{
					_TSBId = value;
					this.RaiseChanged("TSBId");
				}
			}
		}
		/// <summary>
		/// Gets or sets NetworkId.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets NetworkId.")]
		[MaxLength(10)]
		[PropertyMapName("NetworkId")]
		public string NetworkId
		{
			get
			{
				return _NetworkId;
			}
			set
			{
				if (_NetworkId != value)
				{
					_NetworkId = value;
					this.RaiseChanged("NetworkId");
				}
			}
		}
		/// <summary>
		/// Gets or sets TSBNameEN.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets TSBNameEN.")]
		[MaxLength(100)]
		[PropertyMapName("TSBNameEN")]
		public string TSBNameEN
		{
			get
			{
				return _TSBNameEN;
			}
			set
			{
				if (_TSBNameEN != value)
				{
					_TSBNameEN = value;
					this.RaiseChanged("TSBNameEN");
				}
			}
		}
		/// <summary>
		/// Gets or sets TSBNameTH.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets TSBNameTH.")]
		[MaxLength(100)]
		[PropertyMapName("TSBNameTH")]
		public string TSBNameTH
		{
			get
			{
				return _TSBNameTH;
			}
			set
			{
				if (_TSBNameTH != value)
				{
					_TSBNameTH = value;
					this.RaiseChanged("TSBNameTH");
				}
			}
		}
		/// <summary>
		/// Gets or sets is active TSB.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets is active TSB.")]
		[ReadOnly(true)]
		[PropertyMapName("Active")]
		public bool Active
		{
			get
			{
				return _Active;
			}
			set
			{
				if (_Active != value)
				{
					_Active = value;
					this.RaiseChanged("Active");
				}
			}
		}

		#endregion

		#endregion

		#region Static Methods

		#region Get All TSBs

		/// <summary>
		/// Gets TSBs.
		/// </summary>
		/// <param name="db">The database connection.</param>
		/// <returns>Returns List of TSB.</returns>
		public static NDbResult<List<TSB>> GetTSBs(SQLiteConnection db)
		{
			var result = new NDbResult<List<TSB>>();
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * FROM TSB ";
					result.Success();
					var data = NQuery.Query<TSB>(cmd);
					result.Success(data);
				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}
		/// <summary>
		/// Gets TSBs.
		/// </summary>
		/// <returns>Returns List of TSB.</returns>
		public static NDbResult<List<TSB>> GetTSBs()
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return GetTSBs(db);
			}
		}

		#endregion

		#region Get TSB By TSBId

		/// <summary>
		/// Gets TSB By TSB Id.
		/// </summary>
		/// <param name="db">The database connection.</param>
		/// <param name="tsbId">The TSB Id.</param>
		/// <returns>Returns TSB instance.</returns>
		public static NDbResult<TSB> GetTSB(SQLiteConnection db, string tsbId)
		{
			var result = new NDbResult<TSB>();
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * FROM TSB ";
					cmd += " WHERE TSBId = ? ";
					var data = NQuery.Query<TSB>(cmd, tsbId).FirstOrDefault();
					result.Success(data);
				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}
		/// <summary>
		/// Gets TSB By TSB Id.
		/// </summary>
		/// <param name="tsbId">The TSB Id.</param>
		/// <returns>Returns TSB instance.</returns>
		public static NDbResult<TSB> GetTSB(string tsbId)
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return GetTSB(db, tsbId);
			}
		}

		#endregion

		#region Get Current (Active) TSB

		/// <summary>
		/// Gets Active TSB.
		/// </summary>
		/// <returns>Returns Active TSB instance.</returns>
		public static NDbResult<TSB> GetCurrent()
		{
			var result = new NDbResult<TSB>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					// inactive all TSBs
					string cmd = string.Empty;
					cmd += "SELECT * FROM TSB ";
					cmd += " WHERE Active = 1 ";
					var results = NQuery.Query<TSB>(cmd);
					var data = (null != results) ? results.FirstOrDefault() : null;
					result.Success(data);
				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}

		#endregion

		#region Set Active TSB

		/// <summary>
		/// Set Active by TSB Id.
		/// </summary>
		/// <param name="tsbId">The TSB Id.</param>
		/// <returns>Returns Set Active status.</returns>
		public static NDbResult SetActive(string tsbId)
		{
			var result = new NDbResult();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					// inactive all TSBs
					string cmd = string.Empty;
					cmd += "UPDATE TSB ";
					cmd += "   SET Active = 0";
					NQuery.Execute(cmd);
					// Set active TSB
					cmd = string.Empty;
					cmd += "UPDATE TSB ";
					cmd += "   SET Active = 1 ";
					cmd += " WHERE TSBId = ? ";
					NQuery.Execute(cmd, tsbId);
					result.Success();
				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}

		#endregion

		#region Save TSB

		/// <summary>
		/// Save TSB.
		/// </summary>
		/// <param name="value">The TSB instance.</param>
		/// <returns>Returns TSB instance.</returns>
		public static NDbResult<TSB> SaveTSB(TSB value)
		{
			var result = new NDbResult<TSB>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					result = Save(value);

				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}

		#endregion

		#endregion
	}

	#endregion
}
