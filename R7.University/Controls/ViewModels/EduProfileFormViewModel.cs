//
//  EduProfileFormViewModel.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2015-2017 Roman M. Yagodin
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
using System.Xml.Serialization;
using R7.Dnn.Extensions.ViewModels;
using R7.University.Components;
using R7.University.Controls.ViewModels;
using R7.University.ViewModels;
using R7.University.Models;

namespace R7.University.Controls
{
    [Serializable]
    public class EduProgramProfileFormViewModel: IEduProgramProfileFormWritable, IEditControlViewModel<EduProgramProfileFormInfo>
    {
        #region IEditControlViewModel implementation

        public int ViewItemID { get; set; }

        [XmlIgnore]
        public ViewModelContext Context { get; set; }

        ModelEditState _editState;
        ModelEditState _prevEditState;
        public ModelEditState EditState {
            get { return _editState; }
            set { _prevEditState = _editState; _editState = value; }
        }

        public void RestoreEditState ()
        {
            _editState = _prevEditState;
        }

        public IEditControlViewModel<EduProgramProfileFormInfo> Create (
            EduProgramProfileFormInfo model, ViewModelContext context)
        {
            var viewModel = new EduProgramProfileFormViewModel ();
            CopyCstor.Copy<IEduProgramProfileFormWritable> (model, viewModel);
            viewModel.EduFormViewModel = new EduFormViewModel (model.EduForm, context);
            viewModel.Context = context;

            return viewModel;
        }

        public EduProgramProfileFormInfo CreateModel ()
        {
            var model = new EduProgramProfileFormInfo ();
            CopyCstor.Copy<IEduProgramProfileFormWritable> (this, model);

            return model;
        }

        public void SetTargetItemId (int targetItemId, string targetItemKey)
        {
            EduProgramProfileID = targetItemId;
        }

        [XmlIgnore]
        public string CssClass {
            get {
                var cssClass = string.Empty;
                if (EditState == ModelEditState.Deleted) {
                    cssClass += " u8y-deleted";
                } else if (EditState == ModelEditState.Added) {
                    cssClass += " u8y-added";
                } else if (EditState == ModelEditState.Modified) {
                    cssClass += " u8y-updated";
                }

                return cssClass.TrimStart ();
            }
        }

        #endregion

        public EduProgramProfileFormViewModel ()
        {
            ViewItemID = ViewNumerator.GetNextItemID ();
        }

        #region IEduProgramProfileFormWritable implementation

        public long EduProgramProfileFormID { get; set; }

        public int EduProgramProfileID { get; set; }

        public int EduFormID { get; set; }

        public int TimeToLearn { get; set; }

        public int TimeToLearnHours { get; set; }

        public bool IsAdmissive { get; set; }

        [XmlIgnore]
        [Obsolete ("Use EduFormViewModel property instead", true)] 
        public EduFormInfo EduForm { get; set; }

        public EduFormViewModel EduFormViewModel { get; set; }

        #endregion

        [XmlIgnore]
        public string EduFormTitleLocalized
        {
            get {
                EduFormViewModel.Context = Context;
                return EduFormViewModel.TitleLocalized;
            }
        }

        [XmlIgnore]
        public string TimeToLearnYears_String => (TimeToLearn / 12 > 0) ? (TimeToLearn / 12).ToString () : string.Empty;

        [XmlIgnore]
        public string TimeToLearnMonths_String => (TimeToLearn % 12 > 0) ? (TimeToLearn % 12).ToString () : string.Empty;

        [XmlIgnore]
        public string TimeToLearnHours_String => (TimeToLearnHours > 0) ? TimeToLearnHours.ToString () : string.Empty;

    }
}
