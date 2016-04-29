﻿//
// EmployeeListController.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014-2016 Roman M. Yagodin
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Data;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search.Entities;
using R7.University;
using R7.University.Data;

namespace R7.University.EmployeeList
{
    public class EmployeeListController: ModuleSearchBase
    {
        #region ModuleSearchBase implementaion

        public override IList<SearchDocument> GetModifiedSearchDocuments (ModuleInfo modInfo, DateTime beginDate)
        {
            var searchDocs = new List<SearchDocument> ();
            var settings = new EmployeeListSettings (modInfo);
		
            var	employees = UniversityRepository.Instance.DataProvider.GetObjects<EmployeeInfo> (CommandType.StoredProcedure, 
                       (settings.IncludeSubdivisions) ? // which SP to use
				"University_GetRecursiveEmployeesByDivisionID" : "University_GetEmployeesByDivisionID", 
                       settings.DivisionID, settings.SortType, false
                   );

            foreach (var employee in employees) {
                if (employee.LastModifiedOnDate.ToUniversalTime () > beginDate.ToUniversalTime ()) {
                    var aboutEmployee = employee.SearchDocumentText;
                    var sd = new SearchDocument ()
                    {
                        PortalId = modInfo.PortalID,
                        AuthorUserId = employee.LastModifiedByUserID,
                        Title = employee.FullName,
                        // Description = HtmlUtils.Shorten (aboutEmployee, 255, "..."),
                        Body = aboutEmployee,
                        ModifiedTimeUtc = employee.LastModifiedOnDate.ToUniversalTime (),
                        UniqueKey = string.Format ("University_Employee_{0}", employee.EmployeeID),
                        Url = string.Format ("/Default.aspx?tabid={0}#{1}", modInfo.TabID, modInfo.ModuleID),
                        IsActive = employee.IsPublished
                    };

                    searchDocs.Add (sd);
                }
            }
            return searchDocs;
        }

        #endregion
    }
}

