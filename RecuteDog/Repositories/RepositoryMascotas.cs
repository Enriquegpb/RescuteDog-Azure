using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RecuteDog.Data;
using RecuteDog.Models;
using System.Diagnostics.Metrics;

namespace RecuteDog.Repositories
{
    #region PROCEDURESMASCOTAS

    //    alter PROCEDURE SP_OBTENER_MASCOTAS_REFUGIOS(@IDREFUGIO INT)
    //AS

    //    SELECT* FROM MASCOTAS
    //    WHERE IDREFUGIO = @IDREFUGIO
    //GO

    //EXEC SP_OBTENER_MASCOTAS_REFUGIOS 2

    //CREATE VIEW V_MASCOTAS_REFUGIOS
    //AS

    //    SELECT MASCOTAS.* FROM MASCOTAS

    //    INNER JOIN REFUGIOS
    //    ON REFUGIOS.IDREFUGIO = MASCOTAS.IDREFUGIO
    //GO
    //SELECT * FROM V_MASCOTAS_REFUGIOS

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

    /**
     * CREAMOS UNA VISTA PARA VER TODAS LAS MASCOTAS PARA DESPUES RECUPERARLOS EN INFORME MEDIANTE UN PROCEDURE
     */

    //    CREATE VIEW V_VER_MASCOTAS_ADOPTADAS
    //    AS
    //SELECT MASCOTAS.* FROM MASCOTAS
    //INNER JOIN ADOPCIONES
    //ON MASCOTAS.IDMASCOTA = ADOPCIONES.IDMASCOTA
    //INNER JOIN USERS
    //ON ADOPCIONES.IDUSER = USERS.IDUSER
    //GO

    //    CREATE PROCEDURE SP_GENERAR_INFORME_ADOPCIONES
    //AS
    //SELECT* FROM V_VER_MASCOTAS_ADOPTADAS
    //GO

    //    CREATE PROCEDURE SP_UPDATE_STATE_ADOPCION(@IDMASCOTA INT, @ESTADO BIT)
    //AS
    //    UPDATE MASCOTAS SET ADOPTADO = @ESTADO WHERE IDMASCOTA = @IDMASCOTA
    //GO
    #endregion
    public class RepositoryMascotas: IRepoMascotas
    {
        private MascotaContext context;
        
        public RepositoryMascotas(MascotaContext context)
        {
            this.context = context;
        }
        public List<Mascota> GetMascotas(int idrefugio)
        {
            string sql = "SP_OBTENER_MASCOTAS_REFUGIOS @IDREFUGIO";
            SqlParameter pamidrefugio = new SqlParameter("@IDREFUGIO", idrefugio);
            var consulta = this.context.Mascotas.FromSqlRaw(sql, pamidrefugio);
            List <Mascota> mascotas = consulta.ToList();
            return mascotas;
        }

        public Mascota DetailsMascota(int idmascota)
        {
            string sql = "SP_DETALLES_MASCOTA @ID";
            SqlParameter pamidanimal = new SqlParameter("@ID", idmascota);
            var consulta = this.context.Mascotas.FromSqlRaw(sql, pamidanimal);
            Mascota mascota = consulta.AsEnumerable().FirstOrDefault();
            return mascota;
        }

        public async Task UpdateEstadoAdopcion(int idmascota,bool estado)
        {
            string sql = "SP_UPDATE_STATE_ADOPCION @IDMASCOTA, @ESTADO";
            SqlParameter pamidmascota = new SqlParameter("@IDMASCOTA", idmascota);
            SqlParameter pamestado = new SqlParameter("@ESTADO", estado);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidmascota, pamestado);
        }


        public async Task IngresoAnimal(Mascota mascota)
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
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidanimal, pamnombre, pamraza, pamedad, pamancho, pamalto, pampeso, pamdescripcion, pampeligrosidad, pamimagen);
        }

        public List<Mascota> GenerarInformeAdopciones()
        {
            string sql = "SP_GENERAR_INFORME_ADOPCIONES";
            var consulta = this.context.Mascotas.FromSqlRaw(sql);
            List<Mascota> mascotasadoptadas = consulta.ToList();
            return mascotasadoptadas;

        }

        //public async Task <List<Mascota>> SaveInformeAsync(List<Mascota> adopciones)
        //{
        //    await this.context.BulkInsertAsync(adopciones);
        //    return adopciones;
        //}
    }
}
