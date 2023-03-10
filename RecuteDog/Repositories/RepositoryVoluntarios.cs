using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RecuteDog.Data;
using RecuteDog.Models;
using System.Collections.Generic;

namespace RecuteDog.Repositories
{
    #region PROCEDURES VOLUNTARIOS
//    CREATE PROCEDURE SP_NEWVOLUNTARIO(@NOMBRE NVARCHAR(50), @MENSAJE NVARCHAR(50), @IMAGEN NVARCHAR(600), @CORREO NVARCHAR(50), @MUNICIPIO NVARCHAR(50), @FECHANAC DATE, @REFUGIO NVARCHAR(50))
//AS
//    DECLARE @IDREFUGIO INT

//    DECLARE @IDVOLUNTARIO INT
//    SELECT @IDVOLUNTARIO = MAX(IDVOLUNTARIO) + 1 FROM VOLUNTARIOS;
//    SELECT @IDREFUGIO = IDREFUGIO FROM REFUGIOS WHERE @REFUGIO = NOMBRE

//    INSERT INTO VOLUNTARIOS VALUES(@IDVOLUNTARIO, @NOMBRE, @MENSAJE, @IMAGEN, @CORREO, @MUNICIPIO, @FECHANAC, @IDREFUGIO)
//GO

    #endregion
    public class RepositoryVoluntarios : IRepoVoluntarios
    {
        private MascotaContext context;
        public RepositoryVoluntarios(MascotaContext context)
        {
            this.context = context;
        }

        public async Task BajaVoluntario(int idvoluntario)
        {
            throw new NotImplementedException();
        }

        public List<Voluntario> Getvoluntarios()
        {
            var consulta = from datos in this.context.Voluntarios
                           select datos;
            return consulta.ToList();
        }

        public async Task ModificarDatosRefugio(Voluntario voluntario)
        {
            throw new NotImplementedException();
        }

        public async Task NewVoluntario(Voluntario voluntario, string refugio)
        {
            string sql = "SP_NEWVOLUNTARIO @NOMBRE, @MENSAJE, @IMAGEN, @CORREO, @MUNICIPIO, @FECHANAC, @REFUGIO";
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", voluntario.Nombre);
            SqlParameter pammensaje = new SqlParameter("@MENSAJE", voluntario.Mensaje);
            SqlParameter pamimagen = new SqlParameter("@IMAGEN", voluntario.Imagen);
            SqlParameter pamcorreo = new SqlParameter("@CORREO", voluntario.Correo);
            SqlParameter pammunicipio = new SqlParameter("@MUNICIPIO", voluntario.Municipio);
            SqlParameter pamfechanac = new SqlParameter("@FECHANAC", voluntario.Fecha_Nacimiento);
            SqlParameter pamrefugio = new SqlParameter("@REFUGIO", refugio);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamnombre, pammensaje, pamimagen, pamcorreo, pammunicipio, pamfechanac, pamrefugio);

            /*
             * 1. Se debería recuperar el municipio de un tabla de municipios??
             * 2. Se debería recuperar la fecha de la base de datos de usuarios??
             */
        }

        
    }
}
