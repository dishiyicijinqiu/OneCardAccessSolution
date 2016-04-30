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
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSQLite();
            Console.ReadKey();
        }
        static void TestSQLite()
        {
            int userID = 1;
            string sqlCommand = @"SELECT * FROM [UserInfo] WHERE [UserInfoID] = $UserInfoID";

            var database = EnterpriseLibraryContainer.Current.GetInstance<Database>();
            Console.WriteLine("oh,yeah");
            Console.ReadKey();

            Database db = DatabaseFactory.CreateDatabase("DefaultConnectionString");
            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            db.AddInParameter(dbCommand, "$UserInfoID", DbType.Int32, userID);

            using (IDataReader rdr = db.ExecuteReader(dbCommand))
            {
                if (rdr.Read())
                {
                    Console.WriteLine(rdr.GetInt32(0).ToString());
                    Console.WriteLine(rdr.GetString(1));
                }
            }
        }
        static void TestEnvironment()
        {
            Console.WriteLine(string.Format("CommandLine:{0}", Environment.CommandLine));
            Console.WriteLine(string.Format("CurrentDirectory:{0}", Environment.CurrentDirectory));
            Console.WriteLine(string.Format("GetPathRoot:{0}", System.IO.Path.GetPathRoot(Environment.CurrentDirectory)));
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
