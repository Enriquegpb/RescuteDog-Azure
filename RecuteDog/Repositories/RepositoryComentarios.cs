using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RecuteDog.Data;
using RecuteDog.Models;
using System.Collections.Generic;

namespace RecuteDog.Repositories
{
    public class RepositoryComentarios : IRepoComentarios
    {
        #region PROCEDURES
        //        CREATE PROCEDURE SP_DELTE_COMENTARIO(@IDCOMENTARIO INT)
        //AS
        //    DELETE FROM COMENTARIOS WHERE IDCOMENTARIO = @IDCOMENTARIO
        //GO
        //        CREATE PROCEDURE SP_UPDATE_COMENTARIO(@IDCOMENTARIO INT, @EMAIL NVARCHAR(50), @COMENTARIO NVARCHAR(600))
        //AS
        //    UPDATE COMENTARIO SET EMAIL = @EMAIL, COMENTARIO = @COMENTARIO WHERE IDCOMENTARIO = @IDCOMENTARIO
        //GO
        //        CREATE PROCEDURE SP_NEW_COMENTARIO(@IDPOST INT, @EMAIL NVARCHAR(50), @COMENTARIO NVARCHAR(600))
        //AS
        //    DECLARE @IDCOMENTARIO INT

        //    SELECT @IDCOMENTARIO = MAX(IDCOMENTARIO) FROM COMENTARIOS

        //    INSERT INTO COMENTARIOS VALUES(@IDCOMENTARIO, @IDPOST, @EMAIL, @COMENTARIO)
        //GO
        #endregion
        private MascotaContext context;
        public RepositoryComentarios(MascotaContext context)
        {
            this.context = context;
        }
        public async Task DeleteComentario(int idcomentario)
        {
            string sql = "SP_DELTE_COMENTARIO @IDCOMENTARIO";
            SqlParameter pamidpost = new SqlParameter("@IDCOMENTARIO", idcomentario);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidpost);
        }

        public async Task EditComentario(Comentario comentario)
        {
            string sql = "SP_UPDATE_COMENTARIO @IDCOMENTARIO, @EMAIL, @COMENTARIO";
            SqlParameter pamidcomentario = new SqlParameter("@IDCOMENTARIO", comentario.IdComentario);
            SqlParameter pamemail = new SqlParameter("@EMAIL", comentario.IdComentario);
            SqlParameter pamcomentario = new SqlParameter("@COMENTARIO", comentario.ComentarioDesc);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidcomentario, pamemail, pamcomentario);
        }

        public Comentario FindComentario(int idcomentario)
        {
            return this.context.Comentarios.Where(x => x.IdComentario == idcomentario).FirstOrDefault();
        }

        public List<Comentario> GetComentarios()
        {
            return  this.context.Comentarios.OrderBy(x => x.Fecha).ToList();
        }

        public async Task NewComentario(Comentario comentario)
        {
            string sql = "SP_NEW_COMENTARIO @IDCOMENTARIO, @IDPOST, @EMAIL, @COMENTARIO";
            SqlParameter pamidcomentario = new SqlParameter("@IDCOMENTARIO", comentario.IdComentario);
            SqlParameter pamidpost = new SqlParameter("@IDPOST", comentario.IdPost);
            SqlParameter pamemail = new SqlParameter("@EMAIL", comentario.IdComentario);
            SqlParameter pamcomentario = new SqlParameter("@COMENTARIO", comentario.ComentarioDesc);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidcomentario, pamidpost, pamemail, pamcomentario);
        }
    }
}
