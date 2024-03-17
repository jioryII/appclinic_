using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlTypes;

namespace CapaDatos
{
    public class DEspecialista
    {
        private int _EspecialistaCMP;
        public int EspecialistaCMP
        {
            get { return _EspecialistaCMP; }
            set { _EspecialistaCMP = value; }
        }

        private string _EspecialistaNombre;
        public string EspecialistaNombre
        {
            get { return _EspecialistaNombre; }
            set { _EspecialistaNombre = value; }
        }

        private string _EspecialistaApellido;
        public string EspecialistaApellido
        {
            get { return _EspecialistaApellido; }
            set { _EspecialistaApellido = value; }
        }

        private int _EspecialidadCodigo;
        public int EspecialidadCodigo
        {
            get { return _EspecialidadCodigo; }
            set { _EspecialidadCodigo = value; }
        }

        public DEspecialista() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="especialistaCMP"></param>
        /// <param name="especialistaNombre"></param>
        /// <param name="especialistaApellido"></param>
        /// <param name="especialidadCodigo"></param>
        public DEspecialista(int especialistaCMP, string especialistaNombre, string especialistaApellido, int especialidadCodigo)
        {
            this.EspecialistaCMP = especialistaCMP;
            this.EspecialistaNombre = especialistaNombre;
            this.EspecialistaApellido = especialistaApellido;
            this.EspecialidadCodigo = especialidadCodigo;
        }

        /// <summary>
        /// aa
        /// </summary>
        /// <param name="Especialista"></param>
        /// <returns></returns>
        public string Insertar(DEspecialista Especialista)
        {
            string rpta = "";

            using (SqlConnection SqlCon = new SqlConnection(Conexion.Cn))
            using (SqlCommand SqlCmd = new SqlCommand("usp_InsertarEspecialista", SqlCon))
            {
                try
                {
                    SqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlCmd.Parameters.Add("@EspecialistaCMP", SqlDbType.Int).Value = Especialista.EspecialistaCMP;
                    SqlCmd.Parameters.Add("@EspecialistaNombre", SqlDbType.VarChar, 30).Value = Especialista.EspecialistaNombre;
                    SqlCmd.Parameters.Add("@EspecialistaApellido", SqlDbType.VarChar, 50).Value = Especialista.EspecialistaApellido;
                    SqlCmd.Parameters.Add("@EspecialidadCodigo", SqlDbType.Int).Value = Especialista.EspecialidadCodigo;

                    SqlCon.Open();
                    rpta = SqlCmd.ExecuteNonQuery() == 1 ? "Ok" : "No se Ingreso el Registro";

                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
                finally
                {
                    if (SqlCon.State == ConnectionState.Open) { SqlCon.Close(); }
                }

                return rpta;
            }
        }

        public string Actualizar(DEspecialista Especialista)
        {
            string rpta = "";

            using (SqlConnection SqlCon = new SqlConnection(Conexion.Cn))
            using (SqlCommand SqlCmd = new SqlCommand("usp_ActualizarEspecialista", SqlCon))
            {
                try
                {
                    SqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlCmd.Parameters.Add("@EspecialistaCMP", SqlDbType.Int).Value = Especialista.EspecialistaCMP;
                    SqlCmd.Parameters.Add("@EspecialistaNombre", SqlDbType.VarChar, 30).Value = Especialista.EspecialistaNombre;
                    SqlCmd.Parameters.Add("@EspecialistaApellido", SqlDbType.VarChar, 50).Value = Especialista.EspecialistaApellido;
                    SqlCmd.Parameters.Add("@EspecialidadCodigo", SqlDbType.Int).Value = Especialista.EspecialidadCodigo;

                    SqlCon.Open();
                    rpta = SqlCmd.ExecuteNonQuery() == 1 ? "Ok" : "No se Actualizo el Registro";

                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
                finally
                {
                    if (SqlCon.State == ConnectionState.Open) { SqlCon.Close(); }
                }

                return rpta;
            }
        }

        public string Eliminar(DEspecialista Especialista)
        {
            string rpta = "";

            using (SqlConnection SqlCon = new SqlConnection(Conexion.Cn))
            using (SqlCommand SqlCmd = new SqlCommand("usp_EliminarEspecialista", SqlCon))
            {
                try
                {
                    SqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlCmd.Parameters.Add("@EspecialistaCMP", SqlDbType.Int).Value = Especialista.EspecialistaCMP;

                    SqlCon.Open();
                    rpta = SqlCmd.ExecuteNonQuery() == 1 ? "Ok" : "No se Elimino el Registro";

                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
                finally
                {
                    if (SqlCon.State == ConnectionState.Open) { SqlCon.Close(); }
                }

                return rpta;
            }
        }

        public DataTable ListarEspecialista()
        {
            DataTable dtResultado = new DataTable("especialista");

            using (SqlConnection sqlCon = new SqlConnection(Conexion.Cn))
            {
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("usp_ListarEspecialistas", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter sqlDat = new SqlDataAdapter(sqlCmd);
                    sqlDat.Fill(dtResultado);
                }
                catch (Exception ex)
                {
                    // Log the exception for further investigation
                    Console.WriteLine("Error al obtener lista de especialista: " + ex.Message);
                    dtResultado = null;
                }
            }

            return dtResultado;
        }

        public DataTable ListarEspecialistaCMP(DEspecialista Especialista)
        {
            DataTable dtResultado = new DataTable("especialista");

            using (SqlConnection sqlCon = new SqlConnection(Conexion.Cn))
            {
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("usp_ListarEspecialistaCMP", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter sqlParam = new SqlParameter("@EspecialistaCMP", SqlDbType.Int)
                    {
                        Value = Especialista.EspecialistaCMP
                    };
                    sqlCmd.Parameters.Add(sqlParam);

                    SqlDataAdapter sqlDat = new SqlDataAdapter(sqlCmd);
                    sqlDat.Fill(dtResultado);
                }
                catch (Exception ex)
                {
                    // Log the exception for further investigation
                    Console.WriteLine("Error al obtener especialista por CMP: " + ex.Message);
                    dtResultado = null;
                }
            }

            return dtResultado;
        }
    }
}
