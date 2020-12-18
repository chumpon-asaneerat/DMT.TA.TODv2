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
	#region Client

	/// <summary>
	/// The Client Data Model Class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	[Table("Client")]
	public class Client : NTable<Client>
	{
		#region Intenral Variables

		private int _PkId = 0;
		private string _Host = string.Empty;
		private int _PortNumber = 0;
		private string _AppName = string.Empty;

		private bool _Active = false;
		private DateTime? _LastUpdate = new DateTime?();

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public Client() : base() { }

		#endregion

		#region Public Proprties

		#region Common

		/// <summary>
		/// Gets or sets Client PkId
		/// </summary>
		[Category("Lane")]
		[Description("Gets or sets Client PkId")]
		[ReadOnly(true)]
		[PrimaryKey, AutoIncrement]
		[PropertyMapName("PkId")]
		public int PkId
		{
			get
			{
				return _PkId;
			}
			set
			{
				if (_PkId != value)
				{
					_PkId = value;
					this.RaiseChanged("PkId");
				}
			}
		}
		/// <summary>
		/// Gets or sets Host.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Host.")]
		//[ReadOnly(true)]
		[MaxLength(50)]
		[PropertyMapName("Host")]
		public string Host
		{
			get
			{
				return _Host;
			}
			set
			{
				if (_Host != value)
				{
					_Host = value;
					this.RaiseChanged("Host");
				}
			}
		}
		/// <summary>
		/// Gets or sets Port Number.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Port Number.")]
		[PropertyMapName("Port")]
		public int PortNumber
		{
			get
			{
				return _PortNumber;
			}
			set
			{
				if (_PortNumber != value)
				{
					_PortNumber = value;
					this.RaiseChanged("Port");
				}
			}
		}
		/// <summary>
		/// Gets or sets App Name.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets App Name.")]
		//[ReadOnly(true)]
		[MaxLength(100)]
		[PropertyMapName("AppName")]
		public string AppName
		{
			get
			{
				return _AppName;
			}
			set
			{
				if (_AppName != value)
				{
					_AppName = value;
					this.RaiseChanged("AppName");
				}
			}
		}
		/// <summary>
		/// Gets or sets is active.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets is active.")]
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
		/// <summary>
		/// Gets or sets Last Updated date.
		/// </summary>
		[Category("DataCenter")]
		[Description("Gets or sets Last Updated date.")]
		[ReadOnly(true)]
		[PropertyMapName("LastUpdate")]
		[PropertyOrder(10002)]
		public DateTime? LastUpdate
		{
			get { return _LastUpdate; }
			set
			{
				if (_LastUpdate != value)
				{
					_LastUpdate = value;
					this.RaiseChanged("LastUpdate");
				}
			}
		}

		#endregion

		#endregion

		#region Static Methods

		#region Get Clients

		/// <summary>
		/// Gets Clients.
		/// </summary>
		/// <param name="db">The database connection.</param>
		/// <returns>Returns List of Client.</returns>
		public static NDbResult<List<Client>> GetClients(SQLiteConnection db)
		{
			var result = new NDbResult<List<Client>>();
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
					cmd += "SELECT * FROM Client ";
					var data = NQuery.Query<Client>(cmd);
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
		/// Gets Clients.
		/// </summary>
		/// <returns>Returns List of Client.</returns>
		public static NDbResult<List<Client>> GetClients()
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return GetClients(db);
			}
		}

		#endregion

		#region Get Client

		/// <summary>
		/// Gets Client.
		/// </summary>
		/// <param name="db">The database connection.</param>
		/// <param name="value">The Client Instance.</param>
		/// <returns>Returns Client instance.</returns>
		public static NDbResult<Client> GetClient(SQLiteConnection db, Client value)
		{
			var result = new NDbResult<Client>();
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			if (null == value)
			{
				result.ParameterIsNull();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM Client ";
					cmd += " WHERE Host = ?";
					cmd += "   AND PortNumber = ?";
					cmd += "   AND AppName = ?";
					var data = NQuery.Query<Client>(cmd, value.Host, value.PortNumber, value.AppName).FirstOrDefault();
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
		/// Gets Client.
		/// </summary>
		/// <param name="value">The Client Instance.</param>
		/// <returns>Returns Client instance.</returns>
		public static NDbResult<Client> GetClient(Client value)
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return GetClient(db, value);
			}
		}

		#endregion

		#region Register

		/// <summary>
		/// Register.
		/// </summary>
		/// <param name="value">The Client Instance.</param>
		/// <returns>Returns NDbResult instance.</returns>
		public static NDbResult Register(Client value) 
		{
			var result = new NDbResult();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			if (null == value)
			{
				result.ParameterIsNull();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					var match = GetClient(value);
					if (match.Ok)
					{
						if (match.data == null)
						{
							// not found. Insert or Update.
							result = Save(value);
						}
						else
						{
							// found. Need PK for update so get it from match data.
							value.PkId = match.data.PkId;
							result = Save(value);
						}
					}

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

		#region Unregister

		/// <summary>
		/// Unregister.
		/// </summary>
		/// <param name="value">The Client Instance.</param>
		/// <returns>Returns NDbResult instance.</returns>
		public static NDbResult Unregister(Client value)
		{
			var result = new NDbResult();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			if (null == value)
			{
				result.ParameterIsNull();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					try
					{
						string cmd = string.Empty;
						cmd += "DELETE FROM Client ";
						cmd += " WHERE Host = ?";
						cmd += "   AND PortNumber = ?";
						cmd += "   AND AppName = ?";
						NQuery.Execute(cmd, value.Host, value.PortNumber, value.AppName);
						result.Success();
					}
					catch (Exception ex)
					{
						med.Err(ex);
						result.Error(ex);
					}
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
