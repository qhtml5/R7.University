//
//  UpdateDivisionCommand.cs
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

using System;
using R7.University.ModelExtensions;
using R7.University.Models;
using R7.University.Security;

namespace R7.University.Commands
{
    public class UpdateDivisionCommand: UpdateCommand<DivisionInfo>
    {
        public UpdateDivisionCommand (IModelContext modelContext, ISecurityContext securityContext)
            : base (modelContext, securityContext)
        {
        }

        public override void Update (DivisionInfo entity, DateTime dateTime)
        {
            if (SecurityContext.CanUpdate (entity)) {
                entity.LastModifiedByUserId = SecurityContext.UserId;
                entity.LastModifiedOnDate = dateTime;

                if (entity.DivisionTermID != null) {
                    entity.UpdateTerm (ModelContext);
                } else {
                    entity.DivisionTermID = entity.AddTerm (ModelContext);
                }

                ModelContext.Update (entity);
            }
        }
    }
}
