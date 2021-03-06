//
//  EduProgramDirectorySettings.cs
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
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Entities.Modules.Settings;

namespace R7.University.EduPrograms.Models
{
    /// <summary>
    /// Provides strong typed access to settings used by module
    /// </summary>
    [Serializable]
    public class EduProgramDirectorySettings
    {
        [ModuleSetting (Prefix = "EduProgramDirectory_")]
        public int? DivisionId { get; set; }

        [ModuleSetting (Prefix = "EduProgramDirectory_", ParameterName = "EduLevels")]
        public string EduLevelsInternal { get; set; } = string.Empty;

        public IList<int> EduLevels
        {
            get { return EduLevelsInternal.Split (new [] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select (el => int.Parse (el))
                    .ToList ();
            }
            set {
                EduLevelsInternal = string.Join (";", value);
            }
        }

        [TabModuleSetting (Prefix = "EduProgramDirectory_", ParameterName = "Columns")]
        public string ColumnsInternal { get; set; } = string.Empty;

        public IList<string> Columns
        {
            get {
                return ColumnsInternal
                    .Split (new [] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList ();
            }
            set {
                ColumnsInternal = string.Join (";", value);
            }
        }
    }
}
