﻿//
//  SettingsEduProgramDirectory.ascx.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2015-2016 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Linq;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Web.UI.WebControls;
using R7.DotNetNuke.Extensions.Modules;
using R7.University.Data;
using R7.University.EduProgramDirectory.Components;
using R7.University.ViewModels;

namespace R7.University.EduProgramDirectory
{
    public partial class SettingsEduProgramDirectory : ModuleSettingsBase<EduProgramDirectorySettings>
    {
        protected override void OnInit (EventArgs e)
        {
            base.OnInit (e);

            // fill edulevels list
            var eduLevels = UniversityRepository.Instance.DataProvider.GetObjects<EduLevelInfo> ()
                .OrderBy (el => el.SortIndex);
           
            foreach (var eduLevel in eduLevels) {
                listEduLevels.Items.Add (new DnnListBoxItem {
                    Text = FormatHelper.FormatShortTitle (eduLevel.ShortTitle, eduLevel.Title),
                    Value = eduLevel.EduLevelID.ToString ()
                });
            }

            // fill columns list
            foreach (var column in Enum.GetNames (typeof (EduProgramDirectoryColumn))) {
                listColumns.Items.Add (new DnnListBoxItem {
                    Text = LocalizeString ("EduProgram" + column + ".Column"),
                    Value = column
                });
            }
        }

        /// <summary>
        /// Handles the loading of the module setting for this control
        /// </summary>
        public override void LoadSettings ()
        {
            try {
                if (!IsPostBack) {
                    // check edulevels list items
                    foreach (var eduLevelIdString in Settings.EduLevels) {
                        var item = listEduLevels.FindItemByValue (eduLevelIdString);
                        if (item != null) {
                            item.Checked = true;
                        }
                    }

                    // check columns list items
                    foreach (var columnString in Settings.Columns) {
                        var item = listColumns.FindItemByValue (columnString);
                        if (item != null) {
                            item.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        /// <summary>
        /// handles updating the module settings for this control
        /// </summary>
        public override void UpdateSettings ()
        {
            try {
                Settings.EduLevels = listEduLevels.CheckedItems.Select (i => i.Value).ToList ();
                Settings.Columns = listColumns.CheckedItems.Select (i => i.Value).ToList ();

                ModuleController.SynchronizeModule (ModuleId);
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }
    }
}

