using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Infrastructure.Abstractions.Orm
{
    public class DbConfig : DbConfiguration
    {
        public DbConfig()
        {
            //if (!AppSettings.IsDebugMode) {
            //    var name = SqlProviderServices.ProviderInvariantName;
            //    this.SetProviderFactory(name, System.Data.SqlClient.SqlClientFactory.Instance);
            //    this.SetProviderServices(name, SqlProviderServices.Instance);
            //    this.SetDefaultConnectionFactory(new SqlConnectionFactory());
            //}
            var name = "Npgsql";
            this.SetProviderFactory(name, NpgsqlFactory.Instance);
            this.SetProviderServices(name, NpgsqlServices.Instance);
            this.SetDefaultConnectionFactory(new NpgsqlConnectionFactory());            
        }
    }
}
