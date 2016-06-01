using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.TEntity;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FengSharp.OneCardAccess.Services
{
    public abstract class ServiceBase
    {
        protected static string[] ModifyTreeEntityUnChangedFileds = { "treeno", "treeparentno", "treepath", "treeson", "treetotal", "createid", "createdate", "deleted" };
        protected static string[] ModifyEntityUnChangedFileds = { "createid", "createdate", "deleted" };

        protected static string[] CreateTreeEntityUnCreateFileds = { "treeno", "treepath", "treeson", "treetotal", "createdate", "lastmodifydate", "deleted" };
        protected static string[] CreateEntityUnCreateFileds = { "createdate", "lastmodifydate", "deleted" };
        private Database database;
        public Database Database
        {
            get
            {
                if (database == null)
                    database = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                return database;
            }
        }
        public T CreateEntity<T>(T t, DbTransaction tran = null, IEnumerable<string> uncreatefileds = null, IEnumerable<string> createfileds = null, Database _Database = null)
        {
            if (_Database == null)
                _Database = this.Database;
            var cmd = DataBaseExtend.GetCreateCommand(t, _Database, uncreatefileds, createfileds);
            if (tran == null)
                _Database.ExecuteNonQuery(cmd);
            else
                _Database.ExecuteNonQuery(cmd, tran);
            var keyvalue = _Database.GetParameterValue(cmd, t.GetKeyFiledName());
            var p = t.GetType().GetProperty(t.GetKeyFiledName());
            p.SetValue(t, keyvalue, null);
            return t;
        }
        /// <summary>
        /// 保存对象工作流
        /// </summary>
        /// <returns></returns>
        public int SaveEntityFlow<T>(T entity, string cMode, DbTransaction tran, Database _Database = null)
        {
            var KeyProperty = DataBaseExtend.GetKeyProperty<T>();
            var keyDbType = DataBaseExtend.GetDbTypeByPropertyTypeName(KeyProperty.PropertyType.Name);
            string ProcudeName = "P_Glo_SaveEntityFlow";
            switch (keyDbType)
            {
                default:
                    throw new Exception("不支持的类型");
                case DbType.Int32:
                    break;
                case DbType.String:
                    ProcudeName = "P_Glo_SaveEntityFlow;2";
                    break;
            }
            if (_Database == null)
                _Database = this.Database;
            var cmd = _Database.GetStoredProcCommand(ProcudeName);
            _Database.AddInParameter(cmd, "cMode", DbType.String, cMode);
            _Database.AddInParameter(cmd, "EntityId", keyDbType, KeyProperty.GetValue(entity, null));
            _Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
            _Database.ExecuteNonQuery(cmd, tran);
            return (int)_Database.GetParameterValue(cmd, "ReturnValue");
        }
        public virtual void DeleteEntity<T>(T entity, ref int ReturnValue, DbTransaction transaction = null, string cMode = null)
        {
            var KeyProperty = DataBaseExtend.GetKeyProperty<T>();
            var keyDbType = DataBaseExtend.GetDbTypeByPropertyTypeName(KeyProperty.PropertyType.Name);
            string ProcudeName = "P_Glo_Delete";
            switch (keyDbType)
            {
                default:
                    throw new Exception("不支持的类型");
                case DbType.Int32:
                    break;
                case DbType.String:
                    ProcudeName = "P_Glo_Delete;2";
                    break;
            }
            DbCommand cmd = Database.GetStoredProcCommand(ProcudeName);
            if (cMode == null)
                cMode = typeof(T).Name;
            Database.AddInParameter(cmd, "cMode", DbType.String, cMode);
            Database.AddInParameter(cmd, "EntityId", keyDbType, KeyProperty.GetValue(entity, null));
            Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
            Database.ExecuteNonQuery(cmd, transaction);
            ReturnValue = (int)Database.GetParameterValue(cmd, "ReturnValue");
        }
        public virtual void DeleteRelationEntitys<Primary, Foreign>(Primary primary, DbTransaction transaction = null, string cMode = null)
        {
            var KeyProperty = DataBaseExtend.GetKeyProperty<Primary>();
            var keyDbType = DataBaseExtend.GetDbTypeByPropertyTypeName(KeyProperty.PropertyType.Name);
            string ProcudeName = "P_Glo_Delete";
            switch (keyDbType)
            {
                default:
                    throw new Exception("不支持的类型");
                case DbType.Int32:
                    break;
                case DbType.String:
                    ProcudeName = "P_Glo_Delete;2";
                    break;
            }
            DbCommand cmd = Database.GetStoredProcCommand(ProcudeName);
            if (cMode == null)
                cMode = typeof(Primary).Name + "_" + typeof(Foreign).Name;
            Database.AddInParameter(cmd, "cMode", DbType.String, cMode);
            Database.AddInParameter(cmd, "EntityId", keyDbType, primary.GetKeyFiledValue());
            Database.ExecuteNonQuery(cmd, transaction);
        }
        public bool ModifyEntity<T>(T t, DbTransaction tran = null, IEnumerable<string> uncreatefileds = null, IEnumerable<string> createfileds = null, Database _Database = null)
        {
            if (_Database == null)
                _Database = this.Database;
            var cmd = DataBaseExtend.GetModifyCommand(t, _Database, uncreatefileds, createfileds);
            int rowcount = 0;
            if (tran == null)
                rowcount = _Database.ExecuteNonQuery(cmd);
            else
                rowcount = _Database.ExecuteNonQuery(cmd, tran);
            return rowcount > 0;
        }
        public virtual T FindById<T>(T t, string cMode = null)
        {
            if (cMode == null)
                cMode = typeof(T).Name;
            var KeyProperty = DataBaseExtend.GetKeyProperty<T>();
            var keyDbType = DataBaseExtend.GetDbTypeByPropertyTypeName(KeyProperty.PropertyType.Name);
            string ProcudeName = "P_Glo_FindById";
            switch (keyDbType)
            {
                default:
                    throw new Exception("不支持的类型");
                case DbType.Int32:
                    break;
                case DbType.String:
                    ProcudeName = "P_Glo_FindById;2";
                    break;
            }

            DbCommand cmd = Database.GetStoredProcCommand(ProcudeName);
            Database.AddInParameter(cmd, "cMode", DbType.String, cMode);
            Database.AddInParameter(cmd, "EntityId", keyDbType, t.GetKeyFiledValue());
            Database.AddInParameter(cmd, "UserId", DbType.String, (Session.Current == null ? string.Empty : (string)Session.Current.SessionClientId));
            DataSet ds = Database.ExecuteDataSet(cmd);
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                return default(T);
            var dr = ds.Tables[0].Rows[0];
            var ps = typeof(T).GetProperties();
            foreach (var p in ps)
            {
                if (ds.Tables[0].Columns.Contains(p.Name))
                    p.SetValue(t, dr[p.Name], null);
            }
            return t;
        }

        public virtual int MoveTree(string cMode, string sourceId, string targetId, BusinessEntity.MoveTree movetree, DbTransaction tran, Database _Database = null)
        {

            string ProcudeName = "P_Glo_MoveTree";
            if (_Database == null)
                _Database = this.Database;
            var cmd = _Database.GetStoredProcCommand(ProcudeName);
            _Database.AddInParameter(cmd, "cMode", DbType.String, cMode);
            _Database.AddInParameter(cmd, "SourceEntityId", DbType.String, sourceId);
            _Database.AddInParameter(cmd, "TargetEntityId", DbType.String, targetId);
            _Database.AddInParameter(cmd, "OP", DbType.Int16, BusinessEntity.MoveTree.IntoNode);
            _Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
            _Database.ExecuteNonQuery(cmd, tran);
            return (int)_Database.GetParameterValue(cmd, "ReturnValue");
        }

        public virtual T FindByNo<T>(string EntityNo, string cMode = null)
        {
            if (cMode == null)
                cMode = typeof(T).Name;
            DbCommand cmd = Database.GetStoredProcCommand("P_Glo_FindByNo");
            Database.AddInParameter(cmd, "cMode", DbType.String, cMode);
            Database.AddInParameter(cmd, "EntityNo", DbType.String, EntityNo);
            Database.AddInParameter(cmd, "UserId", DbType.String, (Session.Current == null ? string.Empty : (string)Session.Current.SessionClientId));
            DataSet ds = Database.ExecuteDataSet(cmd);
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                return default(T);
            T t = Activator.CreateInstance<T>();
            var dr = ds.Tables[0].Rows[0];
            var colnames = ds.Tables[0].Columns.Cast<DataColumn>().Select(m => m.ColumnName.ToLower()).ToList();
            var ps = typeof(T).GetProperties();
            foreach (var p in ps)
            {
                if (colnames.Contains(p.Name.ToLower()))
                    p.SetValue(t, dr[p.Name], null);
            }
            return t;
        }
        public virtual List<T> GetEntitys<T>(string cMode = null)
        {
            if (cMode == null)
                cMode = typeof(T).Name;
            DbCommand cmd = Database.GetStoredProcCommand("P_Glo_GetEntitys");
            Database.AddInParameter(cmd, "cMode", DbType.String, cMode);
            Database.AddInParameter(cmd, "UserId", DbType.String, (Session.Current == null ? string.Empty : (string)Session.Current.SessionClientId));
            DataSet ds = Database.ExecuteDataSet(cmd);
            var results = new List<T>();
            if (ds == null || ds.Tables.Count <= 0)
                return results;
            var ps = typeof(T).GetProperties();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var dr = ds.Tables[0].Rows[i];
                T t = (T)Activator.CreateInstance(typeof(T));
                foreach (var p in ps)
                {
                    if (ds.Tables[0].Columns.Contains(p.Name))
                        p.SetValue(t, dr[p.Name], null);
                }
                results.Add(t);
            }
            return results;

        }

        public virtual List<T> GetTreeEntitys<T>(string treeparentno, string cMode = null)
        {
            if (cMode == null)
                cMode = typeof(T).Name;
            DbCommand cmd = Database.GetStoredProcCommand("P_Glo_GetTreeEntitys");
            Database.AddInParameter(cmd, "cMode", DbType.String, cMode);
            Database.AddInParameter(cmd, "TreeParentNo", DbType.String, treeparentno);
            //Database.AddInParameter(cmd, "UserId", DbType.String, (Session.Current == null ? string.Empty : (string)Session.Current.SessionClientId));
            DataSet ds = Database.ExecuteDataSet(cmd);
            var results = new List<T>();
            if (ds == null || ds.Tables.Count <= 0)
                return results;
            var ps = typeof(T).GetProperties();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var dr = ds.Tables[0].Rows[i];
                T t = (T)Activator.CreateInstance(typeof(T));
                foreach (var p in ps)
                {
                    if (ds.Tables[0].Columns.Contains(p.Name))
                        p.SetValue(t, dr[p.Name], null);
                }
                results.Add(t);
            }
            return results;
        }
        public virtual List<Foreign> GetForeignEntitys<Primary, Foreign>(Primary primary, DbTransaction transaction = null, string cMode = null)
        {
            var KeyProperty = DataBaseExtend.GetKeyProperty<Primary>();
            var keyDbType = DataBaseExtend.GetDbTypeByPropertyTypeName(KeyProperty.PropertyType.Name);
            string ProcudeName = "P_Glo_GetForeignEntitys";
            switch (keyDbType)
            {
                default:
                    throw new Exception("不支持的类型");
                case DbType.Int32:
                    break;
                case DbType.String:
                    ProcudeName = "P_Glo_GetForeignEntitys;2";
                    break;
            }
            DbCommand cmd = Database.GetStoredProcCommand(ProcudeName);
            if (cMode == null)
                cMode = typeof(Primary).Name + "_" + typeof(Foreign).Name;
            Database.AddInParameter(cmd, "cMode", DbType.String, cMode);
            Database.AddInParameter(cmd, "EntityId", keyDbType, primary.GetKeyFiledValue());
            Database.AddInParameter(cmd, "UserId", DbType.String, (Session.Current == null ? string.Empty : (string)Session.Current.SessionClientId));
            DataSet ds = Database.ExecuteDataSet(cmd);
            var results = new List<Foreign>();
            if (ds == null || ds.Tables.Count <= 0)
                return results;
            var ps = typeof(Foreign).GetProperties();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var dr = ds.Tables[0].Rows[i];
                Foreign t = (Foreign)Activator.CreateInstance(typeof(Foreign));
                foreach (var p in ps)
                {
                    if (ds.Tables[0].Columns.Contains(p.Name))
                        p.SetValue(t, dr[p.Name], null);
                }
                results.Add(t);
            }
            return results;
        }
        protected void UseTran(Action<DbTransaction> action)
        {
            using (DbConnection connection = this.Database.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    action(transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        protected T UseTran<T>(Func<DbTransaction, T> func)
        {
            using (DbConnection connection = this.Database.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    T result = func(transaction);
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
    public static class DataBaseExtend
    {
        public static void AddReturnParameter(this Database database, DbCommand cmd, string name, DbType dbType)
        {
            database.AddParameter(cmd, name, dbType, ParameterDirection.ReturnValue, null, DataRowVersion.Default, null);
        }
        public static string GetKeyFiledName<T>(this T t)
        {
            var Propertys = typeof(T).GetProperties();
            foreach (var property in Propertys)
            {
                if (property.GetCustomAttributes(typeof(DataBaseKeyAttribute), true).Length > 0)
                {
                    return property.Name;
                }
            }
            return null;
        }
        public static PropertyInfo GetKeyProperty<T>()
        {
            var Propertys = typeof(T).GetProperties();
            foreach (var property in Propertys)
            {
                if (property.GetCustomAttributes(typeof(DataBaseKeyAttribute), true).Length > 0)
                {
                    return property;
                }
            }
            return null;
        }
        public static DataBaseKeyType GetKeyFiledKeyType<T>(this T t)
        {
            var Propertys = typeof(T).GetProperties();
            foreach (var property in Propertys)
            {
                var databasekey = property.GetCustomAttributes(typeof(DataBaseKeyAttribute), true).FirstOrDefault() as DataBaseKeyAttribute;
                if (databasekey != null)
                {
                    return databasekey.KeyType;
                }
            }
            return DataBaseKeyType.UnKnown;
        }

        public static object GetKeyFiledValue<T>(this T t)
        {
            var Propertys = typeof(T).GetProperties();
            foreach (var property in Propertys)
            {
                var databasekey = property.GetCustomAttributes(typeof(DataBaseKeyAttribute), true).FirstOrDefault() as DataBaseKeyAttribute;
                if (databasekey != null)
                {
                    return property.GetValue(t, null);
                }
            }
            return null;
        }
        public static DbType GetDbTypeByPropertyTypeName(string PropertyTypeName)
        {
            switch (PropertyTypeName)
            {
                default:
                    throw new Exception("不支持的类型");
                case "AnsiString":
                    return DbType.AnsiString;
                case "Binary":
                    return DbType.Binary;
                case "Byte":
                    return DbType.Byte;
                case "Boolean":
                    return DbType.Boolean;
                case "Currency":
                    return DbType.Currency;
                case "Date":
                    return DbType.Date;
                case "DateTime":
                    return DbType.DateTime;
                case "Decimal":
                    return DbType.Decimal;
                case "Double":
                    return DbType.Double;
                case "Guid":
                    return DbType.Guid;
                case "Int16":
                    return DbType.Int16;
                case "Int32":
                    return DbType.Int32;
                case "Int64":
                    return DbType.Int64;
                case "Object":
                    return DbType.Object;
                case "SByte":
                    return DbType.SByte;
                case "Single":
                    return DbType.Single;
                case "String":
                    return DbType.String;
                case "Time":
                    return DbType.Time;
                case "UInt16":
                    return DbType.UInt16;
                case "UInt32":
                    return DbType.UInt32;
                case "UInt64":
                    return DbType.UInt64;
                case "VarNumeric":
                    return DbType.VarNumeric;
                case "AnsiStringFixedLength":
                    return DbType.AnsiStringFixedLength;
                case "StringFixedLength":
                    return DbType.StringFixedLength;
                case "Xml":
                    return DbType.Xml;
                case "DateTime2":
                    return DbType.DateTime2;
                case "DateTimeOffset":
                    return DbType.DateTimeOffset;
            }
        }
        public static DbCommand GetCreateCommand<T>(T entity, Database _Database, IEnumerable<string> uncreatefileds = null, IEnumerable<string> createfileds = null)
        {
            string keyfiled = entity.GetKeyFiledName();
            DataBaseKeyType databasekeytype = entity.GetKeyFiledKeyType();
            if (databasekeytype == DataBaseKeyType.UnKnown)
                throw new Exception("不支持的主键类型");
            var type = typeof(T);
            var Propertys = type.GetProperties();
            var allfileds = Propertys.Select(t => t.Name).ToList();
            var genefileds = new List<string>();
            foreach (var field in allfileds)
            {
                if (createfileds != null)
                {
                    if (createfileds.Contains(field.ToLower()))
                    {
                        genefileds.Add(field);
                    }
                }
                else if (uncreatefileds != null)
                {
                    if (!uncreatefileds.Contains(field.ToLower()))
                    {
                        genefileds.Add(field);
                    }
                }
                else
                {
                    genefileds.Add(field);
                }
            }
            if (genefileds.Contains(keyfiled))
            {
                genefileds.Remove(keyfiled);
            }
            if (genefileds.Count <= 0)
            {
                throw new Exception("字段不能为0");
            }
            var genePropertys = Propertys.Where(t => genefileds.Contains(t.Name)).ToList();
            StringBuilder sbsql = new StringBuilder();
            sbsql.AppendFormat("INSERT INTO [dbo].[{0}]", type.Name);
            sbsql.AppendFormat("           (");
            if (databasekeytype == DataBaseKeyType.Guid)
            {
                sbsql.AppendFormat("{0},", keyfiled);
            }
            for (int i = 0; i < genefileds.Count; i++)
            {
                var field = genefileds[i];
                if (i == (genefileds.Count - 1))
                    sbsql.AppendFormat("{0}", field);
                else
                    sbsql.AppendFormat("{0},", field);
            }
            sbsql.AppendFormat("		   )");
            sbsql.AppendFormat("     VALUES");
            sbsql.AppendFormat("           (");
            if (databasekeytype == DataBaseKeyType.Guid)
            {
                sbsql.AppendFormat("@{0},", keyfiled);
            }
            for (int i = 0; i < genefileds.Count; i++)
            {
                var field = genefileds[i];
                if (i == (genefileds.Count - 1))
                    sbsql.AppendFormat("@{0}", field);
                else
                    sbsql.AppendFormat("@{0},", field);
            }
            sbsql.AppendFormat("		   ) ");
            if (databasekeytype == DataBaseKeyType.Int)
            {
                sbsql.AppendFormat("select @{0}=@@IDENTITY ", keyfiled);
            }
            DbCommand cmd = _Database.GetSqlStringCommand(sbsql.ToString());
            foreach (var property in genePropertys)
            {
                _Database.AddInParameter(cmd, property.Name, DataBaseExtend.GetDbTypeByPropertyTypeName(property.PropertyType.Name), property.GetValue(entity, null));
            }
            var KeyProperty = DataBaseExtend.GetKeyProperty<T>();

            if (databasekeytype == DataBaseKeyType.Guid)
            {
                _Database.AddParameter(cmd, keyfiled, DataBaseExtend.GetDbTypeByPropertyTypeName(KeyProperty.PropertyType.Name),
                    ParameterDirection.InputOutput, keyfiled, DataRowVersion.Default, Guid.NewGuid().ToString());
            }
            else
            {
                _Database.AddOutParameter(cmd, keyfiled, DataBaseExtend.GetDbTypeByPropertyTypeName(KeyProperty.PropertyType.Name), 4);
            }
            return cmd;
        }

        internal static DbCommand GetModifyCommand<T>(T entity, Database _Database, IEnumerable<string> uncreatefileds = null, IEnumerable<string> createfileds = null)
        {
            string keyfiled = entity.GetKeyFiledName();
            DataBaseKeyType databasekeytype = entity.GetKeyFiledKeyType();
            if (databasekeytype == DataBaseKeyType.UnKnown)
                throw new Exception("不支持的主键类型");
            var type = typeof(T);
            var Propertys = type.GetProperties();
            var allfileds = Propertys.Select(t => t.Name).ToList();
            var genefileds = new List<string>();
            foreach (var field in allfileds)
            {
                if (createfileds != null)
                {
                    if (createfileds.Contains(field.ToLower()))
                    {
                        genefileds.Add(field);
                    }
                }
                else if (uncreatefileds != null)
                {
                    if (!uncreatefileds.Contains(field.ToLower()))
                    {
                        genefileds.Add(field);
                    }
                }
                else
                {
                    genefileds.Add(field);
                }
            }
            if (genefileds.Contains(keyfiled))
            {
                genefileds.Remove(keyfiled);
            }
            if (genefileds.Count <= 0)
            {
                throw new Exception("字段不能为0");
            }
            var genePropertys = Propertys.Where(t => genefileds.Contains(t.Name)).ToList();
            StringBuilder sbsql = new StringBuilder();
            sbsql.AppendFormat("UPDATE [dbo].[{0}] set ", type.Name);
            for (int i = 0; i < genefileds.Count; i++)
            {
                var field = genefileds[i];
                if (i == (genefileds.Count - 1))
                    sbsql.AppendFormat("{0}=@{0} ", field);
                else
                    sbsql.AppendFormat("{0}=@{0}, ", field);

            }
            sbsql.AppendFormat("where {0}=@{0}", keyfiled);
            DbCommand cmd = _Database.GetSqlStringCommand(sbsql.ToString());
            foreach (var property in genePropertys)
            {
                _Database.AddInParameter(cmd, property.Name, GetDbTypeByPropertyTypeName(property.PropertyType.Name), property.GetValue(entity, null));
            }
            var KeyProperty = DataBaseExtend.GetKeyProperty<T>();
            _Database.AddInParameter(cmd, keyfiled, GetDbTypeByPropertyTypeName(KeyProperty.PropertyType.Name), entity.GetKeyFiledValue());
            return cmd;
        }
    }
}
