
using Microsoft.EntityFrameworkCore;
using Sell.Core.Domain;
using Sell.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sell.Data
{
    public class SQLServerApplicationContext : DbContext, IApplcationDbContext
    {
        public SQLServerApplicationContext(DbContextOptions option)
        : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.SetCreateOn();
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(SQLServerApplicationContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }
        public List<T> RunSp<T>(string StoreName, List<DbParamter> ListParamert) where T : new()
        {
            this.Database.OpenConnection();
            DbCommand cmd = this.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = StoreName;
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (var item in ListParamert)
            {
                cmd.Parameters.Add(new SqlParameter { ParameterName = item.ParametrName, Value =  item.Value });
            }


            List<T> list = new List<T>();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader != null && reader.HasRows)
                {
                    var entity = typeof(T);
                    var propDict = new Dictionary<string, PropertyInfo>();
                    var props = entity.GetProperties
           (BindingFlags.Instance | BindingFlags.Public);
                    propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);
                    while (reader.Read())
                    {
                        T newobject = new T();

                        for (int index = 0; index < reader.FieldCount; index++)
                        {
                            if (propDict.ContainsKey(reader.GetName(index).ToUpper()))
                            {
                                var info = propDict[reader.GetName(index).ToUpper()];
                                if ((info != null) && info.CanWrite)
                                {
                                    var val = reader.GetValue(index);
                                    info.SetValue(newobject, (val == DBNull.Value) ? null : val, null);
                                }
                            }
                        }
                        list.Add(newobject);
                    }

                }
                this.Database.CloseConnection();
                return list;
            }

        }
    }
}
