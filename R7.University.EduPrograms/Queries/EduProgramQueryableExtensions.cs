﻿//
//  EduProgramQueryableExtensions.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017 Roman M. Yagodin
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

using System.Linq;
using R7.University.Models;

namespace R7.University.EduPrograms.Queries
{
    public static class EduProgramQueryableExtensions
    {
        public static IQueryable<EduProgramInfo> DefaultOrder (this IQueryable<EduProgramInfo> source)
        {
            return source.OrderBy (ep => ep.EduLevel.SortIndex)
                         .ThenBy (ep => ep.Code)
                         .ThenBy (ep => ep.Title);
        }
    }
}
