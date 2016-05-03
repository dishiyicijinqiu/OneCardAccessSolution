using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Core
{
    internal class PCConfig
    {
        internal static IEnumerable<string> CreateAndModifyInfoColNames = new System.Collections.Generic.List<string>()
        {
            "CreateId","Creater","CreateDate","LastModifyId","LastModifyer","LastModifyDate"
        };
        static string DefaultTempDir = Path.Combine(System.IO.Path.GetPathRoot(Environment.CurrentDirectory), "TempData");
        internal static string TempDir
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(UserConfig.UserTempDir))
                {
                    if (!Directory.Exists(UserConfig.UserTempDir))
                    {
                        Directory.CreateDirectory(UserConfig.UserTempDir);
                    }
                    return UserConfig.UserTempDir;
                }
                if (!Directory.Exists(DefaultTempDir))
                {
                    Directory.CreateDirectory(DefaultTempDir);
                }
                return DefaultTempDir;
            }
        }
    }
    internal class UserConfig
    {
        const string querysql = @"SELECT * FROM [T_UserConfig] WHERE [ConfigKey] = $ConfigKey";
        const string updatesql = @"update [T_UserConfig] set [ConfigValue] = $ConfigValue WHERE [ConfigKey] = $ConfigKey";
        const string insertsql = @"insert into [T_UserConfig] ([ConfigKey],[ConfigValue]) values ($ConfigKey,$ConfigValue)";
        const string DefaultUserConfigDataBase = "UserConfigConnection";
        static string _UserConfigDataBase;
        static string UserConfigDataBase
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_UserConfigDataBase))
                {
                    var connsettings = System.Configuration.ConfigurationManager.AppSettings["DefaultUserConfigConnection"];
                    if (connsettings == null)
                        _UserConfigDataBase = DefaultUserConfigDataBase;
                    else
                        _UserConfigDataBase = connsettings;
                }
                return _UserConfigDataBase;
            }
        }
        static Database Database
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(UserConfigDataBase))
                    return DatabaseFactory.CreateDatabase(UserConfigDataBase);
                else
                    return DatabaseFactory.CreateDatabase();
            }
        }
        static volatile bool hasUserTempDirLoaded = false;
        static string _UserTempDir;
        public static string UserTempDir
        {
            get
            {
                if (hasUserTempDirLoaded) return _UserTempDir;
                _UserTempDir = GetUserConfig<string>("UserTempDir");
                hasUserTempDirLoaded = true;
                return _UserTempDir;
            }
            set
            {
                SetUserConfig("UserTempDir", value);
                hasUserTempDirLoaded = false;
            }
        }

        private static void SetUserConfig(string ConfigKey, object ConfigValue)
        {
            if (!InterSetUserConfig(updatesql, ConfigKey, ConfigValue))
                InterSetUserConfig(insertsql, ConfigKey, ConfigValue);
        }
        private static bool InterSetUserConfig(string opsql, string ConfigKey, object ConfigValue)
        {
            DbCommand dbCommand = Database.GetSqlStringCommand(opsql);
            Database.AddInParameter(dbCommand, "$ConfigKey", DbType.String, ConfigKey);
            switch (ConfigKey)
            {
                default:
                    throw new Exception(Client.PC.Properties.Resources.Error_UserConfigKey);
                case "UserTempDir":
                    Database.AddInParameter(dbCommand, "$ConfigValue", DbType.Binary, Encoding.UTF8.GetBytes(ConfigValue.ToString()));
                    break;
            }
            int rowcount = Database.ExecuteNonQuery(dbCommand);
            return rowcount != 0;
        }

        static T GetUserConfig<T>(string ConfigKey)
        {
            DbCommand dbCommand = Database.GetSqlStringCommand(querysql);
            Database.AddInParameter(dbCommand, "$ConfigKey", DbType.String, ConfigKey);
            using (IDataReader rdr = Database.ExecuteReader(dbCommand))
            {
                if (rdr.Read())
                {
                    var data = rdr.GetValue(1) as byte[];
                    switch (ConfigKey)
                    {
                        default:
                            throw new Exception(Client.PC.Properties.Resources.Error_UserConfigKey);
                        case "UserTempDir":
                            return (T)(Encoding.UTF8.GetString(data) as object);
                    }
                }
            }
            return default(T);
        }
    }
    [ConfigurationElementType(typeof(SQLiteDatabaseData))]
    public class SQLiteDatabase : Database
    {
        public SQLiteDatabase(string connectionString, DbProviderFactory dbProviderFactory, IDataInstrumentationProvider instrumentationProvider)
            : base(connectionString, dbProviderFactory, instrumentationProvider)
        {
        }
        protected override void DeriveParameters(DbCommand discoveryCommand)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }
    }

    public class SQLiteDatabaseData : DatabaseData
    {
        public SQLiteDatabaseData(ConnectionStringSettings connectionStringSettings, IConfigurationSource configurationSource)
            : base(connectionStringSettings, configurationSource)
        {
        }
        public string ProviderName
        {
            get { return ConnectionStringSettings.ProviderName; }
        }
        public override IEnumerable<TypeRegistration> GetRegistrations()
        {
            yield return new TypeRegistration<Database>(
                () => new SQLiteDatabase(ConnectionString,
                   SQLiteFactory.Instance,
                    Container.Resolved<IDataInstrumentationProvider>(Name)))
            {
                Name = Name,
                Lifetime = TypeRegistrationLifetime.Transient
            };
        }
    }
}
