using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Data;

namespace CRUD.Net.Infra.Data.ADODataAccess
{
    public class DataAccess
    {
        #region Objetos Estáticos
        public static SqlConnection sqlconnection = new SqlConnection();
        public static SqlCommand comando = new SqlCommand();
        public static SqlParameter parametro = new SqlParameter();
        #endregion

        #region Obter SqlConnection
        public static SqlConnection connection()
        {
            try
            {
                string dadosConexao = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"].ConnectionString;
                sqlconnection = new SqlConnection(dadosConexao);
                if (sqlconnection.State == ConnectionState.Closed)
                {
                    sqlconnection.Open();
                }
                return sqlconnection;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Abre Conexão
        public void Open()
        {
            sqlconnection.Open();
        }
        #endregion

        #region Fecha Conexão
        public void Close()
        {
            sqlconnection.Close();
        }
        #endregion

        #region Adiciona Parâmetros
        public void AdicionarParametro(string nome,
        SqlDbType tipo, int tamanho, object valor)
        {
            parametro = new SqlParameter();
            parametro.ParameterName = nome;
            parametro.SqlDbType = tipo;
            parametro.Size = tamanho;
            parametro.Value = valor;
            comando.Parameters.Add(parametro);
        }
        #endregion

        #region Adiciona Parâmetros
        public void AdicionarParametro(string nome, SqlDbType tipo, object valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nome;
            parametro.SqlDbType = tipo;
            parametro.Value = valor;
            comando.Parameters.Add(parametro);
        }
        #endregion

        #region Remove os parâmetros
        public void RemoverParametro(string pNome)
        {
            if (comando.Parameters.Contains(pNome))
                comando.Parameters.Remove(pNome);
        }
        #endregion

        #region Limpar Parâmetros
        public void LimparParametros()
        {
            comando.Parameters.Clear();
        }
        #endregion

        #region Executar Consulta SQL
        public DataTable ExecutaConsulta(string sql)
        {
            try
            {
                comando.Connection = connection();
                comando.CommandText = sql;
                comando.ExecuteScalar();
                IDataReader dtreader = comando.ExecuteReader();
                DataTable dtresult = new DataTable();
                dtresult.Load(dtreader);
                sqlconnection.Close();
                return dtresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
