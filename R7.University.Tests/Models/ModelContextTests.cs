//
//  DataRepositoryTests.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2016 Roman M. Yagodin
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
using System.Linq;
using Xunit;

namespace R7.University.Tests.Models
{
    public class ModelContextTests
    {
        [Fact]
        public void DataRepositoryTest ()
        {
            var modelContext = new TestModelContext ();
            modelContext.Query<TestEntity> ().ToList ();
            modelContext.Dispose ();

            // repository call after dispose should throw exception
            Assert.Throws (typeof (InvalidOperationException), () => modelContext.Query<TestEntity> ().ToList ());
        }

        [Fact]
        public void QueryOneTest ()
        {
            using (var modelContext = new TestModelContext ()) {
                modelContext.Add<TestEntity> (new TestEntity { Id = 1, Title = "Hello, world!" });
                modelContext.Add<TestEntity> (new TestEntity { Id = 2, Title = "Hello again!" });

                Assert.Equal (2, modelContext.QueryOne<TestEntity> (e => e.Id == 2).Single ().Id);
            }
        }
    }
}

