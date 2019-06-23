using System;
using SQLitePCL;
using WristCast.Core.Data;
using WristCast.Core.Model;
using Xunit;

namespace WristCast.Core.Tests
{
    public class SqlLiteTests
    {
        [Fact]
        public void Test1()
        {
            raw.SetProvider(new SQLite3Provider_sqlite3());
            raw.FreezeProvider(true);
            var context = new WristCastContext("D:\\dbsqlite.db");
          
        }
    }
}
