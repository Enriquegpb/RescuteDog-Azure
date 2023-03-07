using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RecuteDog.Data;
using RecuteDog.Models;
using System.Collections.Generic;

namespace RecuteDog.Repositories
{
    #region PROCEDURES
    //CREATE PROCEDURE SP_NUEVO_REFUGIO(@NOMBRE NVARCHAR(50), @LOCALIDAD NVARCHAR(50), @UBICACION NVARCHAR(50), @IMAGEN NVARCHAR(50), @VALORACION INT, @DESCRIPCION NVARCHAR(50))
    //AS
    //    DECLARE @IDREFUGIO INT

    //    SELECT @IDREFUGIO = MAX(IDREFUGIO) + 1 FROM REFUGIOS

    //    INSERT INTO REFUGIOS VALUES(@IDREFUGIO, @NOMBRE, @LOCALIDAD, @UBICACION, @IMAGEN, @VALORACION, @DESCRIPCION)
    //GO

    //    CREATE PROCEDURE SP_ACTUALIZAR_DATOS_REFUGIO(@IDREFUGIO INT, @NOMBRE NVARCHAR(50), @LOCALIDAD NVARCHAR(50), @UBICACION NVARCHAR(50), @IMAGEN NVARCHAR(50), @VALORACION INT, @DESCRIPCION NVARCHAR(50))
    //AS
    //    UPDATE REFUGIOS SET NOMBRE = @NOMBRE, LOCALIDAD =  @LOCALIDAD, UBICACION = @UBICACION, @IMAGEN = IMAGEN, VALORACION = @VALORACION, DESCRIPCION = @DESCRIPCION WHERE IDREFUGIO = @IDREFUGIO
    //GO

    //    CREATE PROCEDURE SP_BAJA_REFUGIO(@IDREFUGIO INT)
    //AS
    // DELETE FROM REFUGIOS WHERE IDREFUGIO = @IDREFUGIO
    //GO

    // CREATE PROCEDURE SP_DETAILS_REFUGIOS(@IDREFUGIO INT)
    //AS

    //     SELECT* FROM REFUGIOS WHERE IDREFUGIO = @IDREFUGIO
    //GO

    #endregion
    public class RepositoryRefugios : IRepoRefugios
    {
        
        private MascotaContext context;
        public RepositoryRefugios(MascotaContext context)
        {
            this.context = context;
        }

        public Refugio DetailsRefugio(int idrefugio)
        {
            string sql = "SP_DETAILS_REFUGIOS @IDREFUGIO";
            SqlParameter pamidrefugio = new SqlParameter("@IDREFUGIO", idrefugio);
            var consulta = this.context.Refugios.FromSqlRaw(sql, pamidrefugio);
            Refugio refugio = consulta.AsEnumerable().FirstOrDefault();
            return refugio;
        }

        public void AgregarRefugio(Refugio refugio)
        {
            string sql = "SP_NUEVO_REFUGIO @NOMBRE, @LOCALIDAD, @UBICACION, @IMAGEN, @VALORACION, @DESCRIPCION";
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", refugio.Nombre);
            SqlParameter pamnloc = new SqlParameter("@LOCALIDAD", refugio.Localidad);
            SqlParameter pamubicacion = new SqlParameter("@UBICACION", refugio.Ubicacion);
            SqlParameter pamimagen = new SqlParameter("@IMAGEN", refugio.Imagen);
            SqlParameter pamvaloracion = new SqlParameter("@VALORACION", refugio.Valoracion);
            SqlParameter pamdescripcion = new SqlParameter("@DESCRIPCION", refugio.Descripcion);
            this.context.Database.ExecuteSqlRaw(sql, pamnombre, pamnloc, pamubicacion, pamimagen, pamvaloracion, pamdescripcion);

        }

        public void BajaRefugio(int idrefugio)
        {
            string sql = "SP_BAJA_REFUGIO @IDREFUGIO";
            SqlParameter pamidrefugio = new SqlParameter("@IDREFUGIO", idrefugio);
            this.context.Database.ExecuteSqlRaw(sql, pamidrefugio);
        }

        public List<Refugio> GetRefugios()
        {
            var consulta = from datos in this.context.Refugios
                           select datos;
            return consulta.ToList();
        }

        public void ModificarDatosRefugio(Refugio refugio)
        {
            string sql = "SP_ACTUALIZAR_DATOS_REFUGIO @IDREFUGIO, @NOMBRE, @LOCALIDAD, @UBICACION, @IMAGEN, @VALORACION, @DESCRIPCION";
            SqlParameter pamidrefugio = new SqlParameter("@IDREFUGIO", refugio.IdRefugio);
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", refugio.Nombre);
            SqlParameter pamnloc = new SqlParameter("@LOCALIDAD", refugio.Localidad);
            SqlParameter pamubicacion = new SqlParameter("@UBICACION", refugio.Ubicacion);
            SqlParameter pamimagen = new SqlParameter("@IMAGEN", refugio.Imagen);
            SqlParameter pamvaloracion = new SqlParameter("@VALORACION", refugio.Valoracion);
            SqlParameter pamdescripcion = new SqlParameter("@DESCRIPCION", refugio.Descripcion);
            this.context.Database.ExecuteSqlRaw(sql, pamidrefugio, pamnombre, pamnloc, pamubicacion, pamimagen, pamvaloracion, pamdescripcion);
        }
    }
}
