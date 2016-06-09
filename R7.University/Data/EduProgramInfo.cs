﻿//
//  EduProgramInfo.cs
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
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.ComponentModel.DataAnnotations;
using R7.DotNetNuke.Extensions.Utilities;
using R7.University.Models;
using R7.University.ModelExtensions;

namespace R7.University.Data
{
    [TableName ("University_EduPrograms")]
    [PrimaryKey ("EduProgramID", AutoIncrement = true)]
    public class EduProgramInfo: IEduProgram
    {
        public EduProgramInfo ()
        {
            Documents = new List<IDocument> ();
        }

        #region IEduProgram implementation

        public int EduProgramID { get; set; }

        public int EduLevelID { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public string Generation { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int LastModifiedByUserID { get; set; }

        public DateTime LastModifiedOnDate { get; set; }

        public int CreatedByUserID { get; set; }

        public DateTime CreatedOnDate { get; set; }

        [IgnoreColumn]
        public IEduLevel EduLevel { get; set; }

        [IgnoreColumn]
        public IList<IDocument> Documents { get; set; }

        #endregion

        // TODO: Move to viewmodel
        [IgnoreColumn]
        public string EduProgramString
        {
            get { return TextUtils.FormatList (" ", Code, Title); }
        }

        [IgnoreColumn]
        public bool IsPublished
        {
            get {
                var now = DateTime.Now;
                return (StartDate == null || now >= StartDate) && (EndDate == null || now < EndDate);
            }
        }

        [IgnoreColumn]
        public IList<IDocument> EduStandardDocuments
        {
            get {
                return Documents.Where (d => d.DocumentType.GetSystemDocumentType () == SystemDocumentType.EduStandard).ToList ();
            }
        }
    }
}
