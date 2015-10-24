using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Configuration = NHibernate.Cfg.Configuration;

namespace EWG.Infrastructure.Dal.Impl
{
    public class DbInitializer : IDbInitializer
    {
        private string _configurationFilePath;

        public void Initialize(string configurationFilePath)
        {
            _configurationFilePath = configurationFilePath;

            DropDatabaseIfExist();
            CreateBaseByScript();
            InsertDictionaries();
        }

        private void InsertDictionaries()
        {
            ExecuteScript(DatabaseScripts.InsertDictionariesScript, ConnectionString);
        }

        private string _connString;

        public string ConnectionString
        {
            get { return _connString ?? (_connString = GetConnectionString()); }
            set { _connString = value; }
        }

        private string GetConnectionString()
        {
            var config = new Configuration();
            if (string.IsNullOrEmpty(_configurationFilePath))
            {
                config.Configure();
            }
            else
            {
                config.Configure(_configurationFilePath); 
            }
            return config.GetProperty("connection.connection_string");
        }

        public void CreateBaseByScript()
        {
            CreateDatabase();
            ExecuteScriptFile();
        }

        public void ExecuteScriptFile()
        {
            string query = DatabaseScripts.NewDbScript;
            ExecuteScript(query, ConnectionString);
        }

        private void ExecuteScript(string script, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    var server = new Microsoft.SqlServer.Management.Smo.Server(new ServerConnection(connection));
                    server.ConnectionContext.ExecuteNonQuery(script);
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Ошибка при создании структуры БД - {0}", e));
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public void CreateDatabase()
        {
            var connectoinStringBuilder = new SqlConnectionStringBuilder(ConnectionString);
            string dbName = connectoinStringBuilder.InitialCatalog;
            connectoinStringBuilder.InitialCatalog = string.Empty;

            using (var connection = new SqlConnection(connectoinStringBuilder.ToString()))
            {
                try
                {
                    var serverConnection = new ServerConnection(connection);
                    var server = new Microsoft.SqlServer.Management.Smo.Server(serverConnection);
                    var db = new Database(server, dbName);
                    db.Create();
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Ошибка при создании БД - {0}", e));
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public void DropDatabaseIfExist()
        {
            var connectoinStringBuilder = new SqlConnectionStringBuilder(ConnectionString);
            string dbName = connectoinStringBuilder.InitialCatalog;
            connectoinStringBuilder.InitialCatalog = string.Empty;

            using (var connection = new SqlConnection(connectoinStringBuilder.ToString()))
            {
                try
                {
                    var serverConnection = new ServerConnection(connection);
                    var server = new Microsoft.SqlServer.Management.Smo.Server(serverConnection);
                    if (server.Databases.Contains(dbName))
                    {
                        server.KillDatabase(dbName);
                    }
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }
    }
}