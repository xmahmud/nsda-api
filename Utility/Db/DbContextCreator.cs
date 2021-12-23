using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Utility.Db {
    public class DbContextCreator {

        public static TContext Create<TContext>(string connectionString) where TContext : DbContext {
            var builder = new DbContextOptionsBuilder<TContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(TContext).GetTypeInfo().Assembly.GetName().Name))
                .ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));

            var context = Activator.CreateInstance(typeof(TContext), new object[] { builder.Options }) as TContext;
            return context;
        }

    }
}
