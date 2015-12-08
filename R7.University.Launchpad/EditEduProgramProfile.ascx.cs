﻿using System;
using System.Web.UI.WebControls;
using System.Linq;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.UserControls;
using DotNetNuke.R7;
using R7.University;
using R7.University.ControlExtensions;

namespace R7.University.Launchpad
{
    public partial class EditEduProgramProfile: 
        EditModuleBase<LaunchpadController,LaunchpadSettings,EduProgramProfileInfo>
	{
        protected EditEduProgramProfile (): base ("eduprogramprofile_id")
        {}

        protected override void OnInitControls ()
        {
            InitControls (buttonUpdate, buttonDelete, linkCancel, auditControl);
        }

        /// <summary>
        /// Handles Init event for a control.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnInit (EventArgs e)
        {
            base.OnInit (e);

            // bind education programs
            comboEduProgram.DataSource = Controller.GetObjects<EduProgramInfo> ()
                .OrderBy (ep => ep.EduLevelID)
                .ThenBy (ep => ep.Code);
            
            comboEduProgram.DataBind ();
            comboEduProgram.SelectedIndex = 0;
        }

        protected override void OnLoadItem (EduProgramProfileInfo item)
        {
            textProfileCode.Text = item.ProfileCode;
            textProfileTitle.Text = item.ProfileTitle;
            datetimeStartDate.SelectedDate = item.StartDate;
            datetimeEndDate.SelectedDate = item.EndDate;
            comboEduProgram.SelectByValue (item.EduProgramID);

            auditControl.Bind (item);
        }

        protected override void OnUpdateItem (EduProgramProfileInfo item)
        {
            // fill the object
            item.ProfileCode = textProfileCode.Text.Trim ();
            item.ProfileTitle = textProfileTitle.Text.Trim ();
            item.StartDate = datetimeStartDate.SelectedDate;
            item.EndDate = datetimeEndDate.SelectedDate;
            item.EduProgramID = int.Parse (comboEduProgram.SelectedValue);

            if (ItemId == null)
            {
                item.CreatedOnDate = DateTime.Now;
                item.LastModifiedOnDate = item.CreatedOnDate;
                item.CreatedByUserID = UserInfo.UserID;
                item.LastModifiedByUserID = item.CreatedByUserID;
            }
            else
            {
                item.LastModifiedOnDate = DateTime.Now;
                item.LastModifiedByUserID = UserInfo.UserID;

                // HACK: Set missing CreatedOnDate value
                // REVIEW: Solve on SqlDataProvider level on upgrage to 2.0.0?
                if (item.CreatedOnDate == default (DateTime)) 
                {
                    item.CreatedOnDate = item.LastModifiedOnDate;
                    item.CreatedByUserID = item.LastModifiedByUserID;
                }
            }
        }
	}
}
