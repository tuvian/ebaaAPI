using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace WebAPIMySchool
{
    public class DAL
    {
        
        #region Declarations

        OdbcConnection _conn;
        OleDbConnection _oledbConn;
        //"DRIVER={MySQL ODBC 3.51 Driver}; SERVER=localhost; DATABASE=onlinecareer; UID=root; PASSWORD=123; OPTION=3";
        private string sLogFormat;
        private string sErrorTime;

        public OdbcCommand odbCommand;
        OdbcDataAdapter dataAdapter;

        #endregion Declarations

        public DAL()
        {
            odbCommand = new OdbcCommand();
            dataAdapter = new OdbcDataAdapter();
        }

        #region Common

        /// <summary>
        /// 
        /// </summary>
        private void OpenConnection()
        {
            string _connectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            //string _olebdConnectionString = ConfigurationSettings.AppSettings["OleDBConnectionString"].ToString();
            //_oledbConn = new OleDbConnection(_olebdConnectionString);
            //_oledbConn.Open();
            _conn = new OdbcConnection(_connectionString);
            _conn.Open();
        }

        public void ExecuteNonQuery(string sqlQuery)
        {
            try
            {
                OpenConnection();
                OdbcCommand _dbCommand = new OdbcCommand(sqlQuery, _conn);
                _dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }

        /// <summary>
        /// Execute Non query with return integer.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public int ExecuteQuery(string sqlQuery)
        {
            try
            {
                OpenConnection();
                OdbcCommand _dbCommand = new OdbcCommand(sqlQuery, _conn);
                return _dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }

        public DataTable ExecuteDataTable(string sqlQuery)
        {
            try
            {
                OpenConnection();
                DataTable _resultTable = new DataTable();
                OdbcCommand _dbCommand = new OdbcCommand(sqlQuery, _conn);
                OdbcDataAdapter _dataAdapter = new OdbcDataAdapter(_dbCommand);
                //_dbCommand.ExecuteNonQuery();
                _dataAdapter.SelectCommand = _dbCommand;
                _dataAdapter.Fill(_resultTable);
                return _resultTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }

        public object ExecuteScalar(string sqlQuery)
        {
            try
            {
                OpenConnection();
                OdbcCommand _dbCommand = new OdbcCommand(sqlQuery, _conn);
                return _dbCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }

        public DataSet ExecuteDataSet(string sqlQuery, CommandType mySQLCommandType)
        {
            try
            {
                OpenConnection();
                DataSet _resultDataSet = new DataSet();
                odbCommand.CommandText = sqlQuery;
                odbCommand.Connection = _conn;
                
                int test = 0;
                //dataAdapter.SelectCommand = OdbcCommand;  new OdbcDataAdapter(odbCommand);
                dataAdapter.SelectCommand = odbCommand;
                test = dataAdapter.Fill(_resultDataSet);
                return _resultDataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void ClearParameters()
        {
            try
            {
                this.odbCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OdbcParameter AddParameter()
        {
            try
            {
                OdbcParameter sqlParam = new OdbcParameter();
                this.odbCommand.Parameters.Add(sqlParam);
                return sqlParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OdbcParameter AddParameter(string parameterName, OdbcType sqlDatabaseType, int parameterLength, ParameterDirection parameterDirection)
        {
            try
            {
                OdbcParameter sqlParam = new OdbcParameter(parameterName, sqlDatabaseType, parameterLength);
                sqlParam.Direction = parameterDirection;
                return this.odbCommand.Parameters.Add(sqlParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OdbcParameter AddParameter(string parameterName, OdbcType sqlDatabaseType, int parameterLength, ParameterDirection parameterDirection, object parameterValue)
        {
            try
            {
                OdbcParameter sqlParam = (OdbcParameter)this.AddParameter(parameterName, sqlDatabaseType, parameterLength, parameterDirection);
                sqlParam.Value = parameterValue;
                return sqlParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}