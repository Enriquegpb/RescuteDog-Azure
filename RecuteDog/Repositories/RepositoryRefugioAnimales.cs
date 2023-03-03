using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RecuteDog.Data;
using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    #region PROCEDURESMASCOTAS

    //    CREATE PROCEDURE SP_SALIDA_MASCOTA_ADOPTADA(@ID INT)
    //AS
    //    DELETE FROM MASCOTAS WHERE ID = @ID
    //GO

    //    CREATE PROCEDURE SP_INGRESO_MASCOTA_RESCATADA(@NOMBRE NVARCHAR(50), @RAZA NVARCHAR(50), @EDAD INT, @ANCHO FLOAT, @ALTO FLOAT, @PESO FLOAT, @DESCRIPCION NVARCHAR(200), @PELIGROSIDAD BIT, @IMAGEN NVARCHAR(600))
    //AS

    //    DECLARE @ID INT

    //    SELECT @ID = MAX(ID) FROM MASCOTAS --AUTOINCREMENTO

    //    INSERT INTO MASCOTAS VALUES(@ID, @NOMBRE, @RAZA, @EDAD, @ANCHO, @ALTO, @PESO, @DESCRIPCION, @PELIGROSIDAD, @IMAGEN)
    //GO

    //CREATE PROCEDURE SP_DETALLES_MASCOTA(@ID INT)
    //AS

    //    SELECT* FROM MASCOTAS WHERE IDMASCOTA = @ID
    //GO
    #endregion
    public class RepositoryRefugioAnimales
    {
        private MascotaContext context;
        public RepositoryRefugioAnimales(MascotaContext context)
        {
            this.context = context;
        }
        public List<Mascota> GetMascotas()
        {
            var consulta = from datos in this.context.Mascotas
                           select datos;
            return consulta.ToList();
        }

        public Mascota DetailsMascota(int idmascota)
        {
            string sql = "SP_DETALLES_MASCOTA @ID";
            SqlParameter pamidanimal = new SqlParameter("@ID", idmascota);
            var consulta = this.context.Mascotas.FromSqlRaw(sql, pamidanimal);
            Mascota mascota = consulta.AsEnumerable().FirstOrDefault();
            return mascota;
        }

        public void DeleteAnimal(int id)
        {
            string sql = "SP_SALIDA_MASCOTA_ADOPTADA";
            SqlParameter pamidanimal = new SqlParameter("@ID", id);
            this.context.Database.ExecuteSqlRaw(sql, pamidanimal);
        }

        public void IngresoAnimal(Mascota mascota)
        {
            string sql = "SP_INGRESO_MASCOTA_RESCATADA";
            SqlParameter pamidanimal = new SqlParameter("@ID", mascota.Id);
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", mascota.Nombre);
            SqlParameter pamraza = new SqlParameter("@RAZA", mascota.Raza);
            SqlParameter pamedad = new SqlParameter("@EDAD", mascota.Edad);
            SqlParameter pamancho = new SqlParameter("@ANCHO", mascota.Ancho);
            SqlParameter pamalto = new SqlParameter("@ALTO", mascota.Alto);
            SqlParameter pampeso = new SqlParameter("@PESO", mascota.Peso);
            SqlParameter pamdescripcion = new SqlParameter("@DESCRIPCION", mascota.Descripcion);
            SqlParameter pampeligrosidad = new SqlParameter("@PELIGROSIDAD", mascota.Peligrosidad);
            SqlParameter pamimagen = new SqlParameter("@IMAGEN", mascota.Imagen);
            this.context.Database.ExecuteSqlRaw(sql, pamidanimal, pamnombre, pamraza, pamedad, pamancho, pamalto, pampeso, pamdescripcion, pampeligrosidad, pamimagen);
        }
    }
}
