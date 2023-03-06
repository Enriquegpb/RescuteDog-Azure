using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RecuteDog.Data;
using RecuteDog.Models;
using System.Collections.Generic;

namespace RecuteDog.Repositories
{
    #region PROCEDURES
    //--ESTA VISTA ME VA A SERVIR PARA OBTENEER UN INFORME DE TODAS LAS MASCOTAS ADOPTADAS

    //CREATE PROCEDURE SP_NUEVA_ADOPCION(@IDMASCOTA INT, @IDUSER INT)
    //AS

    //    INSERT INTO ADOPCIONES VALUES(@IDMASCOTA, @IDUSER, GETDATE())
    //GO

    //    CREATE PROCEDURE SP_DEVOLVER_ANIMAL_AL_REFUGIGO(@IDMASCOTA INT)
    //AS
    // DELETE FROM ADOPCIONES WHERE IDMASCOTA = @IDMASCOTA
    //GO
    #endregion
    public class RepositoryAdopciones: IRepoAdopciones
    {
        private MascotaContext context;

        public RepositoryAdopciones(MascotaContext context)
        {
            this.context = context;
        }

        public void DevolverAnimalAlRefugio(int idmascota)
        {
            string sql = "SP_DEVOLVER_ANIMAL_AL_REFUGIGO @IDMASCOTA";
            SqlParameter pamidmascota = new SqlParameter("@IDMASCOTA", idmascota);
            this.context.Database.ExecuteSqlRaw(sql, pamidmascota);
        }

        public void NuevaAdopcion(int idmascota, int iduser)
        {
            string sql = "SP_NUEVA_ADOPCION @IDMASCOTA, @IDUSER";
            SqlParameter pamidmascota = new SqlParameter("@IDMASCOTA", idmascota);
            SqlParameter pamiduser = new SqlParameter("@IDUSER", iduser);
            this.context.Database.ExecuteSqlRaw(sql, pamidmascota, pamiduser);
        }
    }
}
