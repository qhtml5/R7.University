﻿//
//  EmployeeQuery.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2016 Roman M. Yagodin
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
using R7.University.Models;
using R7.University.Queries;

namespace R7.University.Employee.Queries
{
    public class EmployeeQuery: QueryBase
    {
        public EmployeeQuery (IModelContext modelContext): base (modelContext)
        {
        }

        public EmployeeInfo Execute (int employeeId)
        {
            return ModelContext.QueryOne<EmployeeInfo> (e => e.EmployeeID == employeeId)
                .Include (e => e.Positions)
                .Include (e => e.Positions.Select (p => p.Position))
                .Include (e => e.Positions.Select (p => p.Division))
                .Include (e => e.Achievements)
                .Include (e => e.Achievements.Select (ea => ea.Achievement))
                .Include (e => e.Disciplines)
                .Include (e => e.Disciplines.Select (ed => ed.EduProgramProfile))
                .Include (e => e.Disciplines.Select (ed => ed.EduProgramProfile.EduProgram))
                .SingleOrDefault ();
        }

        public EmployeeInfo ByUserId (int userId)
        {
            return ModelContext.QueryOne<EmployeeInfo> (e => e.UserID == userId)
                .Include (e => e.Positions)
                .Include (e => e.Positions.Select (p => p.Position))
                .Include (e => e.Positions.Select (p => p.Division))
                .Include (e => e.Achievements)
                .Include (e => e.Achievements.Select (ea => ea.Achievement))
                .Include (e => e.Disciplines)
                .Include (e => e.Disciplines.Select (ed => ed.EduProgramProfile))
                .Include (e => e.Disciplines.Select (ed => ed.EduProgramProfile.EduProgram))
                .SingleOrDefault ();
        }
    }
}
