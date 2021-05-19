using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDataManager.Library.DBUtility
{
    public class SqlDataAccess : IDisposable
    {
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _isClosed = false;

        /// <summary>
        /// Open connect and start transaction methode
        /// </summary>
        /// <param name="connectionStringName"></param>
        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            _isClosed = false;

        }
        /// <summary>
        /// Close connect and stop transaction methode on success
        /// </summary>
        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();
            _isClosed = true;
        }

        /// <summary>
        /// Close connect and stop transaction methode on failed
        /// </summary>
        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();
            _isClosed = true;
        }

        public void Dispose()
        {
            if (!_isClosed)
            {
                try
                {
                    CommitTransaction();
                }
                catch (Exception)
                {
                    //todo - log this issue;
                    throw;
                };
            }
            _transaction = null;
            _connection = null;
            
        }

        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> rows = _connection.Query<T>(storedProcedure, parameters, 
                commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();
            return rows;
        }

        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
            _connection.Execute(storedProcedure, parameters, 
                commandType: CommandType.StoredProcedure, transaction: _transaction);
        }


        //load using the transaction
        //save using the transaction
        //Close connect and stop transaction methode
        //commit
    }
}
