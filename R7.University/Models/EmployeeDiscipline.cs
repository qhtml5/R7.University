//
//  EmployeeDiscipline.cs
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

namespace R7.University.Models
{
    public interface IEmployeeDiscipline
    {
        long EmployeeDisciplineID { get; }

        int EmployeeID { get; }

        int EduProgramProfileID { get; }

        string Disciplines { get; }

        EmployeeInfo Employee { get; }

        EduProgramProfileInfo EduProgramProfile { get; }
    }

    public interface IEmployeeDisciplineWritable: IEmployeeDiscipline
    {
        new long EmployeeDisciplineID { get; set; }

        new int EmployeeID { get; set; }

        new int EduProgramProfileID { get; set; }

        new string Disciplines { get; set; }

        new EmployeeInfo Employee { get; set; }

        new EduProgramProfileInfo EduProgramProfile { get; set; }
    }

    public class EmployeeDisciplineInfo: IEmployeeDisciplineWritable
    {
        public long EmployeeDisciplineID { get; set; }

        public int EmployeeID { get; set; }

        public int EduProgramProfileID { get; set; }

        public string Disciplines { get; set; }

        public virtual EmployeeInfo Employee { get; set; }

        public virtual EduProgramProfileInfo EduProgramProfile { get; set; }
    }
}

