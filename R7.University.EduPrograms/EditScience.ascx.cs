//
//  EditScience.ascx.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017-2018 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Affero General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Affero General Public License for more details.
//
//  You should have received a copy of the GNU Affero General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Text.RegularExpressions;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Framework;
using DotNetNuke.Security;
using R7.Dnn.Extensions.Utilities;
using R7.University.Commands;
using R7.University.Components;
using R7.University.ModelExtensions;
using R7.University.Models;
using R7.University.Modules;
using R7.University.ViewModels;

namespace R7.University.EduPrograms
{
    public partial class EditScience : UniversityEditPortalModuleBase<ScienceInfo>, IActionable
    {
        protected EditScience () : base ("science_id")
        {
        }

        protected int? GetEduProgramId () =>
        	TypeUtils.ParseToNullable<int> (Request.QueryString [Key])
        			 ?? TypeUtils.ParseToNullable<int> (Request.QueryString ["eduprogram_id"]);

        protected IEduProgram GetEduProgram ()
        {
        	var eduProgramId = GetEduProgramId ();
        	return eduProgramId != null ? ModelContext.Get<EduProgramInfo> (eduProgramId.Value) : null;
        }

        protected override void OnLoad (EventArgs e)
        {
        	base.OnLoad (e);

            var eduProgram = GetEduProgram ();
            if (eduProgram != null) {
                ((CDefault) Page).Title = ((CDefault) Page).Title.Append (eduProgram.FormatTitle (), " &gt; ");
            }
        }

        #region UniversityEditPortalModuleBase implementation

        protected override void InitControls ()
        {
            InitControls (buttonUpdate, buttonDelete, linkCancel);
        }

        protected override void LoadItem (ScienceInfo item)
        {
            textDirections.Text = item.Directions;
            textBase.Text = item.Base;
            textScientists.Text = item.Scientists.ToString ();
            textStudents.Text = item.Students.ToString ();
            textMonographs.Text = item.Monographs.ToString ();
            textArticles.Text = item.Articles.ToString ();
            textArticlesForeign.Text = item.ArticlesForeign.ToString ();
            textPatents.Text = item.Patents.ToString ();
            textPatentsForeign.Text = item.PatentsForeign.ToString ();
            textCertificates.Text = item.Certificates.ToString ();
            textCertificatesForeign.Text = item.CertificatesForeign.ToString ();
            textFinancingByScientist.Text = item.FinancingByScientist.ToDecimalString ();
        }

        protected override void BeforeUpdateItem (ScienceInfo item)
        {
            item.Directions = HttpUtility.HtmlEncode (StripScripts (HttpUtility.HtmlDecode (textDirections.Text)));
            item.Base = HttpUtility.HtmlEncode (StripScripts (HttpUtility.HtmlDecode (textBase.Text)));
            item.Scientists = TypeUtils.ParseToNullable<int> (textScientists.Text);
            item.Students = TypeUtils.ParseToNullable<int> (textStudents.Text);
            item.Monographs = TypeUtils.ParseToNullable<int> (textMonographs.Text);
            item.Articles = TypeUtils.ParseToNullable<int> (textArticles.Text);
            item.ArticlesForeign = TypeUtils.ParseToNullable<int> (textArticlesForeign.Text);
            item.Patents = TypeUtils.ParseToNullable<int> (textPatents.Text);
            item.PatentsForeign = TypeUtils.ParseToNullable<int> (textPatentsForeign.Text);
            item.Certificates = TypeUtils.ParseToNullable<int> (textCertificates.Text);
            item.CertificatesForeign = TypeUtils.ParseToNullable<int> (textCertificatesForeign.Text);
            item.FinancingByScientist = TypeUtils.ParseToNullable<decimal> (textFinancingByScientist.Text);
        }

        string StripScripts (string html)
        {
        	html = Regex.Replace (html, @"<script.*>.*</script>", string.Empty, RegexOptions.Singleline);
        	html = Regex.Replace (html, @"<script.*/>", string.Empty, RegexOptions.Singleline);

        	return html;
        }

        protected override int GetItemId (ScienceInfo item) => item.ScienceId;

        protected override ScienceInfo GetItemWithDependencies (int itemId)
        {
            return ModelContext.Get<ScienceInfo> (itemId);
        }

        protected override void UpdateItem (ScienceInfo item)
        {
            ModelContext.Update (item);
            ModelContext.SaveChanges (true);
        }

        protected override void AddItem (ScienceInfo item)
        {
            var scienceId = TypeUtils.ParseToNullable<int> (Request.QueryString ["eduprogram_id"]);
            if (scienceId != null) {
                item.ScienceId = scienceId.Value;
            }

            new AddCommand<ScienceInfo> (ModelContext, SecurityContext).Add (item);
            ModelContext.SaveChanges ();
        }

        protected override void DeleteItem (ScienceInfo item)
        {
            new DeleteCommand<ScienceInfo> (ModelContext, SecurityContext).Delete (item);
            ModelContext.SaveChanges ();
        }

        #endregion

        #region IActionable implementation

        public ModuleActionCollection ModuleActions {
            get {
                var actions = new ModuleActionCollection ();
                var eduProgramId = GetEduProgramId ();
                if (eduProgramId != null) {
                    actions.Add (new ModuleAction (GetNextActionID ()) {
                        Title = LocalizeString ("EditEduProgram.Action"),
                        CommandName = ModuleActionType.EditContent,
                        Icon = UniversityIcons.Edit,
                        Secure = SecurityAccessLevel.Edit,
                        Url = EditUrl ("eduprogram_id", eduProgramId.ToString (), "EditEduProgram"),
                        Visible = SecurityContext.IsAdmin
                    });
                }
                return actions;
            }
        }

        #endregion
    }
}
