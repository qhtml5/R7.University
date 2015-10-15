﻿using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.UI.Modules;
using DotNetNuke.Common.Utilities;
using DotNetNuke.R7;
using R7.University;

namespace R7.University.Employee
{
	/// <summary>
	/// Provides strong typed access to settings used by module
	/// </summary>
	public partial class EmployeeSettings : SettingsWrapper
	{
		public EmployeeSettings (IModuleControl module) : base (module)
		{
		}

		public EmployeeSettings (ModuleInfo module) : base (module)
		{
		}

		#region Properties for settings

		private int? employeeId;

		/// <summary>
		/// Gets or sets the EmployeeID setting value. 
		/// Use <see cref="EmployeePortalModuleBase.GetEmployee()"/> 
		/// and <see cref="EmployeePortalModuleBase.GetEmployeeId()"/>
		/// to get employee info in the view contols.
		/// </summary>
		/// <value>The employee Id.</value>
		public int EmployeeID
		{
			get
			{ 
				if (employeeId == null)
					employeeId = ReadSetting<int> ("Employee_EmployeeID", Null.NullInteger); 
				
				return employeeId.Value;
			}
			set
			{ 
				WriteModuleSetting<int> ("Employee_EmployeeID", value); 
				employeeId = value;
			}
		}

		public bool ShowCurrentUser
		{
			get { return ReadSetting<bool> ("Employee_ShowCurrentUser", false); }
			set { WriteModuleSetting<bool> ("Employee_ShowCurrentUser", value); }
		}

		public bool AutoTitle
		{
			get { return ReadSetting<bool> ("Employee_AutoTitle", true); }
			set { WriteModuleSetting<bool> ("Employee_AutoTitle", value); }
		}

		public int PhotoWidth
		{
			// REVIEW: Need a way to customize default settings like PhotoWidth
			get { return ReadSetting<int> ("Employee_PhotoWidth", 192); }
			set { WriteTabModuleSetting<int> ("Employee_PhotoWidth", value); }
		}

		private int? dataCacheTime;

		public int DataCacheTime
		{
			get
			{ 
				if (dataCacheTime == null)
					dataCacheTime = ReadSetting<int> ("Employee_DataCacheTime", 1200);
				
				return dataCacheTime.Value;
			}
			set
			{ 
				WriteTabModuleSetting<int> ("Employee_DataCacheTime", value); 
				dataCacheTime = value;
			}
		}

		#endregion
	}
}

