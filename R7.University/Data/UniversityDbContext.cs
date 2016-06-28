﻿//
//  UniversityDbContext.cs
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
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.ModelConfiguration.Conventions;
using DotNetNuke.Common.Utilities;

namespace R7.University.Data
{
    public class UniversityDbContext : DbContext
    {
        #region Singleton implementation

        static readonly Lazy<UniversityDbContext> instance = new Lazy<UniversityDbContext> (
            () => new UniversityDbContext ("name=SiteSqlServer")
        );

        public static UniversityDbContext Instance
        {
            get { return instance.Value; }
        }

        static UniversityDbContext ()
        {
            // do not use migrations (1)
            Database.SetInitializer<UniversityDbContext> (null);
        }

        protected UniversityDbContext (string nameOrConnectionString): base (nameOrConnectionString)
        {
            // do not use migrations (2)
            Configuration.AutoDetectChangesEnabled = false; 
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        #endregion

        protected override void OnModelCreating (DbModelBuilder modelBuilder)
        {
            // remove trailing '.' from schema name, by ex. "dbo." => "dbo"
            modelBuilder.HasDefaultSchema (Config.GetDataBaseOwner ().TrimEnd ('.'));

            // add mappings
            modelBuilder.Configurations.Add<PositionInfo> (new PositionMapping ());

            // add objectQualifier
            var plurService = new EnglishPluralizationService ();
            modelBuilder.Types ().Configure (entity => 
                entity.ToTable (Config.GetObjectQualifer () 
                    + "University_" + plurService.Pluralize (entity.ClrType.Name.Replace ("Info", string.Empty))));

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        #region IUniversityDbContext implementation

        public IDbSet<PositionInfo> Positions { get; set; }

        #endregion
    }
}

