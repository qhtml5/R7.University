﻿//
//  DocumentViewModel.cs
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
using System.Xml.Serialization;
using DotNetNuke.Common;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Services.Localization;
using R7.DotNetNuke.Extensions.ViewModels;
using R7.University.Components;
using R7.University.Data;
using R7.University.Models;
using R7.University.Utilities;
using R7.University.ViewModels;

namespace R7.University.Controls
{
    [Serializable]
    public class DocumentViewModel: IDocument, IEditControlViewModel<DocumentInfo>
    {
        #region IDocument implementation

        public int DocumentID { get; set; }

        public int DocumentTypeID { get; set; }

        [XmlIgnore]
        public IDocumentType DocumentType { get; set; }

        /// <summary>
        /// XML-serializeable boilerplate for <see cref="DocumentViewModel.DocumentType" /> property
        /// </summary>
        /// <value>The document type view model.</value>
        public DocumentTypeViewModel DocumentTypeViewModel
        {
            get { return (DocumentTypeViewModel) DocumentType; }
            set { DocumentType = value; }
        }

        public string ItemID { get; set; }

        public string Title { get; set; }

        public string Group { get; set; }

        public string Url { get; set; }

        public int SortIndex { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        #endregion

        [XmlIgnore]
        public string LocalizedType
        { 
            get {
                var localizedType = Localization.GetString ("SystemDocumentType_" + DocumentType.Type + ".Text", 
                    Context.LocalResourceFile);
                    
                return (!string.IsNullOrEmpty (localizedType)) ? localizedType : DocumentType.Type;
            }
        }

        [XmlIgnore]
        public string FormattedUrl
        {
            get { 
                if (!string.IsNullOrWhiteSpace (Url)) {
                    return string.Format ("<a href=\"{0}\" target=\"_blank\">{1}</a>",
                        UrlUtils.LinkClickIdnHack (Url, Context.Module.TabId, Context.Module.ModuleId),
                        Localization.GetString ("DocumentUrlLabel.Text", Context.LocalResourceFile)
                    );
                }

                return string.Empty;
            }
        }

        [XmlIgnore]
        public string FileName
        {
            get {
                if (Globals.GetURLType (Url) == TabType.File) {
                    var file = FileManager.Instance.GetFile (int.Parse (Url.ToUpperInvariant ().Replace ("FILEID=","")));
                    return (file != null) ? file.FileName : string.Empty;
                }

                return string.Empty;
            }
        }

        #region IEditControlViewModel implementation

        public int ViewItemID { get; set; }

        [XmlIgnore]
        public ViewModelContext Context { get; set; }

        public IEditControlViewModel<DocumentInfo> FromModel (DocumentInfo model, ViewModelContext viewContext)
        {
            var viewModel = new DocumentViewModel ();
            CopyCstor.Copy<IDocument> (model, viewModel);

            // FIXME: Context not updated for referenced viewmodels
            viewModel.DocumentType = new DocumentTypeViewModel (model.DocumentType, viewContext);
            viewModel.Context = viewContext;

            return viewModel;
        }

        public DocumentInfo ToModel ()
        {
            var model = new DocumentInfo ();
            CopyCstor.Copy<IDocument> (this, model);

            return model;
        }

        public void SetTargetItemId (int targetItemId, string targetItemKey)
        {
            ItemID = targetItemKey + targetItemId;
        }

        #endregion

        public DocumentViewModel ()
        {
            ViewItemID = ViewNumerator.GetNextItemID ();
        }

        /*
        public static void BindToView (IEnumerable<DocumentViewModel> documents, ViewModelContext context)
        {
            foreach (var document in documents)
            {
                document.Context = context;
            }
        }*/
    }
}

