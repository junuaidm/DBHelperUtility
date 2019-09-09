using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace ERPDAL.Common
{
    public class clsCommonfunctions : IDisposable
    {

        /////UtilityDAL objUtilityDAL = new UtilityDAL();

        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["YourConnString"].ConnectionString;
            }
        }
        SqlConnection con;
        private bool IsDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool Diposing)
        {
            if (!IsDisposed)
            {
                if (Diposing)
                {
                    if (con != null)
                    {
                        con.Close();
                        con.Dispose();
                        con = null;
                    }
                    //Clean Up managed resources
                }
                //Clean up unmanaged resources
            }
            IsDisposed = true;
        }
        ~clsCommonfunctions()
        {
            Dispose(false);
        }

        #region #funtion
        /// <summary>
        /// ExicuteNoneQuery with Query
        /// </summary>
        /// <param name="string _query"></param>
        /// <returns>
        /// No returns
        /// </returns>
        public bool ExicuteNoneQuery(string _query)
        {
            int result = 0;
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        cmd.CommandText = _query;
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return (result > 0);

        }
        /// <summary>
        /// ExicuteNoneQuery With Single SqlParameter
        /// </summary>
        /// <param name="string _query"></param>
        /// <param name="SqlParameter Parameter"></param>
        public bool ExicuteNoneQuery(string _query, SqlParameter Parameter)
        {
            int result = 0;
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        cmd.CommandText = _query;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(Parameter);
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return (result > 0);

        }
        /// <summary>
        /// ExicuteNoneQuery With List of Parameter
        /// </summary>
        /// <param name="string _query"></param>
        /// <param name="List<SqlParameter> lstpm"></param>
        public bool ExicuteNoneQuery(string _query, List<SqlParameter> lstpm)
        {
            int result = 0;

            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        cmd.CommandText = _query;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(lstpm.ToArray());
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return (result > 0);

        }
        public bool ExicuteNoneQuery(string _query, SqlParameter[] lstParameters)
        {
            int result = 0;

            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        cmd.CommandText = _query;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(lstParameters);
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return (result > 0);

        }

        /// <summary>
        /// ExicuteNoneQuery With List of Parameter Inside a Transaction
        /// </summary>
        /// <param name="string _query"></param>
        /// <param name="List<SqlParameter> lstpm"></param>
        public bool ExicuteNoneQuery(string _query, List<SqlParameter> lstpm, SqlConnection oCon, SqlTransaction Transaction)
        {
            int result = 0;
            using (SqlCommand cmd = new SqlCommand(_query, oCon, Transaction))
            {
                try
                {
                    if (oCon.State != ConnectionState.Open)
                        oCon.Open();
                    cmd.CommandText = _query;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(lstpm.ToArray());
                    result = cmd.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {

                }
            }
            return (result > 0);
        }
        /// <summary>
        /// Exicutes DataTable with Query
        /// </summary>
        /// <param name="string _query"></param>
        /// <returns>
        /// DataTable
        /// </returns>
        public DataTable ExicuteDataTable(string _query)
        {
            DataTable dt = new DataTable();
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            cmd.CommandText = _query;
                            da.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            throw ex;

                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// Exicutes DataSet with Query
        /// </summary>
        /// <param name="string _query"></param>
        /// <returns>
        /// DataSet
        /// </returns>
        public DataSet ExicuteDataSet(string _query)
        {
            DataSet ds = new DataSet();
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            cmd.CommandText = _query;
                            da.Fill(ds);
                        }
                        catch (Exception ex)
                        {
                            throw ex;

                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            return ds;

        }
        /// <summary>
        /// Exicutes Data reader with Query
        /// </summary>
        /// <param name="_query"></param>
        /// <returns></returns>
        public SqlDataReader ExicuteReader(string _query)
        {
            SqlDataReader rdr = null;
            con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = con.CreateCommand())
            {
                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.CommandText = _query;
                    rdr = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return rdr;
        }
        public SqlDataReader ExicuteReader(string _query, SqlParameter Parameter)
        {
            SqlDataReader rdr = null;
            con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = con.CreateCommand())
            {
                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parameter);
                    cmd.CommandText = _query;
                    rdr = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return rdr;
        }
        public SqlDataReader ExicuteReader(string _query, List<SqlParameter> lstpm)
        {
            SqlDataReader rdr = null;
            con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = con.CreateCommand())
            {
                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(lstpm.ToArray());
                    cmd.CommandText = _query;
                    rdr = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return rdr;
        }
        /// <summary>
        /// Exicute datat Reader with query and parameter
        /// </summary>
        /// <param name="_query"></param>
        /// <param name="lstpm"></param>
        /// <returns></returns>

        /// <summary>
        /// Exicutes DataSet with Qury And give Name for Dataset
        /// </summary>
        /// <param name="string _query"></param>
        /// <param name="string strdtsetname"></param>
        /// <returns>
        /// DataSet
        /// </returns>
        public DataSet ExicuteDataSet(string _query, string strdtsetname)
        {
            DataSet ds = new DataSet();
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            cmd.CommandText = _query;
                            da.Fill(ds, strdtsetname);
                        }
                        catch (Exception ex)
                        {
                            throw ex;

                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            return ds;

        }
        /// <summary>
        /// ExicuteScalar With Query
        /// </summary>
        /// <param name="string _query"></param>
        /// <returns>
        /// value
        /// </returns>
        public Object ExicuteScalar(string _query)
        {
            Object objreturnValue = null;
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    try
                    {
                        cmd.CommandText = _query;
                        objreturnValue = cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return objreturnValue;

        }
        /// <summary>
        /// Exicute Scalar With single parameter
        /// </summary>
        /// <param name="string _query"></param>
        /// <param name="SqlParameter strParameter"></param>
        /// <returns>
        /// value
        /// </returns>
        public Object ExicuteScalar(string _query, SqlParameter Parameter)
        {
            Object objreturnValue = null;
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    try
                    {
                        cmd.CommandText = _query;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(Parameter);
                        objreturnValue = cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return objreturnValue;

        }
        /// <summary>
        /// Exicute Scalar with List of SqlParameter
        /// </summary>
        /// <param name="string _query"></param>
        /// <param name="List<SqlParameter> lstpm"></param>
        /// <returns>
        /// Value
        /// </returns>
        public object ExicuteScalar(string _query, List<SqlParameter> lstpm)
        {
            Object objreturnValue = null;
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    try
                    {
                        cmd.CommandText = _query;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(lstpm.ToArray());
                        objreturnValue = cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return objreturnValue;

        }

        /// <summary>
        /// Exicutes DataTable with single SqlParameter
        /// </summary>
        /// <param name="string _query"></param>
        /// <param name="SqlParameter lstpm"></param>
        /// <returns>
        /// DataTable
        /// </returns>
        public DataTable ExicuteDataTable(string _query, SqlParameter SqlParameter)
        {
            DataTable dt = new DataTable();
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            cmd.CommandText = _query;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(SqlParameter);
                            da.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            throw ex;

                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// Exicutes DataTable with List of SqlParameter
        /// </summary>
        /// <param name="string _query"></param>
        /// <param name="List<SqlParameter> lstpm"></param>
        /// <returns></returns>
        public DataTable ExicuteDataTable(string _query, List<SqlParameter> lstpm)
        {

            DataTable dt = new DataTable();
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            cmd.CommandText = _query;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddRange(lstpm.ToArray());
                            da.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            throw ex;

                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            return dt;

        }
        public DataTable ExicuteDataTable(string _query, SqlParameter[] lstParameters)
        {

            DataTable dt = new DataTable();
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            cmd.CommandText = _query;

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddRange(lstParameters);
                            da.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            throw ex;

                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            return dt;

        }
        /// <summary>
        /// Exicutes DataTable With of List of SqlParameter and Give table name
        /// </summary>
        /// <param name="string _query"></param>
        /// <param name="List<SqlParameter> lstpm"></param>
        /// <param name="string strTableName"></param>
        /// <returns>
        /// DataTable
        /// </returns>
        public DataTable ExicuteDataTable(string _query, List<SqlParameter> lstpm, string strTableName)
        {
            DataSet ds = new DataSet();
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            cmd.CommandText = _query;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddRange(lstpm.ToArray());
                            da.Fill(ds, strTableName);
                        }
                        catch (Exception ex)
                        {
                            throw ex;

                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            return ds.Tables[0];

        }


        /// <summary>
        /// Exicute DataSet with List of SqlParameter
        /// </summary>
        /// <param name="string _query"></param>
        /// <param name="List<SqlParameter> lstpm"></param>
        /// <returns>
        /// DataSet
        /// </returns>
        public DataSet ExicuteDataSet(string _query, List<SqlParameter> lstpm)
        {
            DataSet ds = new DataSet();
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            cmd.CommandText = _query;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddRange(lstpm.ToArray());
                            da.Fill(ds);
                        }
                        catch (Exception ex)
                        {
                            throw ex;

                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            return ds;

        }


        /// <summary>
        /// Exicute DataSet with List of SqlParameter
        /// </summary>
        /// <param name="string _query"></param>
        /// <param name="List<SqlParameter> lstpm"></param>
        /// <param name="string strDataSetName"></param>
        /// <returns>
        /// DataSet
        /// </returns>
        public DataSet ExicuteDataSet(string _query, List<SqlParameter> lstpm, string strDataSetName)
        {
            DataSet ds = new DataSet();
            using (con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            cmd.CommandText = _query;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddRange(lstpm.ToArray());
                            da.Fill(ds, strDataSetName);
                        }
                        catch (Exception ex)
                        {
                            throw ex;

                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            return ds;

        }
        /// <summary>
        /// To Ckeck Any ID  Existst in Any Table
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strColumnName"></param>
        /// <param name="strValue"></param>
        /// <returns>retun boolean </returns>
        public bool IsExists(string strTableName, string strColumnName, string strValue)
        {
            string sqlQuery = "";
            sqlQuery = string.Format("SELECT 1 FROM {0} WHERE {1} = '{2}'", strTableName, strColumnName, strValue);
            using (con = new SqlConnection(ConnectionString))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlQuery;
                    var retVal = cmd.ExecuteScalar();
                    if (retVal.ToInt32() == 1)
                    {
                        return true;
                    }
                    else return false;
                }
            }
        }
        /// <summary>
        /// To Ckeck Any ID  Existst in Any Table
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strColumnName"></param>
        /// <param name="strValue"></param>
        /// <returns>retun boolean </returns>
        public bool IsExists(string strTableName, string strColumnName, string strValue, string[] args)
        {
            string sqlQuery = "";
            string strCondition = "";
            if (args != null)
            {
                strCondition = " AND ";
                for (int i = 0; i < args.Length; i++)
                {
                    strCondition += args[i].ToString();
                }
            }
            sqlQuery = string.Format("SELECT 1 FROM {0} WHERE {1} = '{2}' {3} ", strTableName, strColumnName, strValue, strCondition);
            using (con = new SqlConnection(ConnectionString))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlQuery;
                    var retVal = cmd.ExecuteScalar();
                    if (retVal.ToInt32() == 1)
                    {
                        return true;
                    }
                    else return false;
                }
            }
        }
        public bool IsExists(string strTableName, string strColumnName, int intValue, string[] args)
        {
            string sqlQuery = "";
            string strCondition = "";
            if (args != null)
            {
                strCondition = " AND ";
                for (int i = 0; i < args.Length; i++)
                {
                    strCondition += args[i].ToString();
                }
            }
            sqlQuery = string.Format("SELECT 1 FROM {0} WHERE {1} = {2} {3} ", strTableName, strColumnName, intValue, strCondition);
            using (con = new SqlConnection(ConnectionString))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlQuery;
                    var retVal = cmd.ExecuteScalar();
                    if (retVal.ToInt32() == 1)
                    {
                        return true;
                    }
                    else return false;
                }
            }
        }
        public bool IsExists(string strTableName, string strColumnName, int intIDValue)
        {
            string sqlQuery = "";
            sqlQuery = string.Format("SELECT 1 FROM {0} WHERE {1} = {2}", strTableName, strColumnName, intIDValue.ToString());
            using (con = new SqlConnection(ConnectionString))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sqlQuery;
                    var retVal = cmd.ExecuteScalar();
                    if (retVal.ToInt32() == 1)
                    {
                        return true;
                    }
                    else return false;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strColumnName"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public object FetchTableValue(string strTableName, string strColumnName, string strWeherColumnName, int intIDValue)
        {
            string sqlQuery = "";
            sqlQuery = string.Format("SELECT {0} FROM {1} WHERE {2} = {3}", strColumnName, strTableName, strWeherColumnName, intIDValue.ToString());
            return ExicuteScalar(sqlQuery);
        }
        public object FetchTableValue(string strTableName, string strColumnName, string strWeherColumnName, string strValue)
        {
            string sqlQuery = "";
            sqlQuery = string.Format("SELECT {0} FROM {1} WHERE {2} = '{3}'", strColumnName, strTableName, strWeherColumnName, strValue);
            return ExicuteScalar(sqlQuery);
        }
        #endregion



    }
}

