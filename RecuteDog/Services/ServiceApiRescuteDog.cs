using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using NugetRescuteDog.Models;

namespace RecuteDog.Services
{
    public class ServiceApiRescuteDog
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApiRescuteBlog;
        public ServiceApiRescuteDog(IConfiguration configuration)
        {
            this.UrlApiRescuteBlog =
                configuration.GetValue<string>("ApiUrls:ApiRescuteDog");
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");

        }

        public async Task<User> GetPerfilUsuarioAsync(string token)
        {
            string request = "/api/auth/getperfilusuario";
            User usuario =
                await this.CallApiAsync<User>(request, token);
            return usuario;
        }

        public async Task NewUsuarioAsync(string username, string password, string email, string phone, string imagen, string cumple)
        {

            using (HttpClient client = new HttpClient())
            {
                string request = "/api/auth";
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);


                User user = new User
                {
                    Id = 0,
                    Username = username,
                    Imagen = imagen,
                    Birdthday = cumple,
                    Contrasena = password,
                    Email = email,
                    Phone = phone,
                };
                string jsonRefugio =
                    JsonConvert.SerializeObject(user);
                StringContent content =
                    new StringContent(jsonRefugio, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        
        
        }public async Task UpdateUsuarioAsync(string username, string telefono, string email, string imagen, int iduser, string token)
        {

            using (HttpClient client = new HttpClient())
            {
                string request = "/api/auth/updateperfilusuario/"+ username+ "/"+ telefono + "/"+email+"/"+imagen+"/"+iduser;
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                
               
                HttpResponseMessage response =
                    await client.PutAsync(request, null);
            }
        }

        public async Task BajaUsuarioAsync(int idusuario, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/auth/" + idusuario;
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
             ("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }

        //LO PRIMERO DE TODO ES RECUPERAR NUESTRO TOKEN LA APP MVC
        public async Task<string> GetTokenAsync(string email, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/auth/login";
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                LoginModel model = new LoginModel
                {
                    Email = email,
                    Password = password
                };
                string jsonModel = JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(jsonModel, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    string data =
                        await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(data);
                    string token =
                        jsonObject.GetValue("response").ToString();
                    return token;

                }
                else
                {
                    return null;
                }

            }
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                string peticion = this.UrlApiRescuteBlog + request;
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<T> CallApiAsync<T>(string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        /**
         * Servicios REFUGIOS
         */
        public async Task<List<Refugio>> GetRefugiosAsync()
        {
            string request = "/api/refugios";
            List<Refugio> refugios =
                await this.CallApiAsync<List<Refugio>>(request);
            return refugios;
        } 
        public async Task<Refugio> FindRefugioAsync(int idrefugio)
        {
            string request = "/api/refugios/"+idrefugio;
           Refugio refugio =
                await this.CallApiAsync<Refugio>(request);
            return refugio;
        }
        public async Task NewRefugioAsync(Refugio refugio, string token)
        {

            using (HttpClient client = new HttpClient())
            {
                string request = "/api/refugios";
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
              ("Authorization", "bearer " + token);


                Refugio refugioalta = new Refugio
                {
                    IdRefugio = refugio.IdRefugio,
                    Descripcion = refugio.Descripcion,
                    Imagen = refugio.Imagen,
                    Localidad = refugio.Localidad,
                    Nombre = refugio.Nombre,
                    Ubicacion = refugio.Ubicacion,
                    Valoracion = refugio.Valoracion,
                };
                string jsonRefugio =
                    JsonConvert.SerializeObject(refugioalta);
                StringContent content =
                    new StringContent(jsonRefugio, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        } 
        public async Task UpdateRefugioAsync(Refugio refugio, string token)
        {

            using (HttpClient client = new HttpClient())
            {
                string request = "/api/refugios";
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
              ("Authorization", "bearer " + token);


                Refugio refugioalta = new Refugio
                {
                    IdRefugio = refugio.IdRefugio,
                    Descripcion = refugio.Descripcion,
                    Imagen = refugio.Imagen,
                    Localidad = refugio.Localidad,
                    Nombre = refugio.Nombre,
                    Ubicacion = refugio.Ubicacion,
                    Valoracion = refugio.Valoracion,
                };
                string jsonRefugio =
                    JsonConvert.SerializeObject(refugioalta);
                StringContent content =
                    new StringContent(jsonRefugio, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }

        public async Task DeleteRefugiosAsync(int idvoluntario, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/refugios/" + idvoluntario;
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add
              ("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }


        /**
         * Servicios MASCOTAS
         */
        public async Task<List<Mascota>> GetMascotasAsync(int idrefugio)
        {
            string request = "/api/mascotas/getmascotasrefugio/" + idrefugio;
            List<Mascota> mascotas =
                await this.CallApiAsync<List<Mascota>>(request);
            return mascotas;
        } 
        public async Task<List<Mascota>> GenerarInformeAdopcionesAsync(string token)
        {
            string request = "/api/mascotas/";
            List<Mascota> mascotas =
                await this.CallApiAsync<List<Mascota>>(request, token);
            return mascotas;
        }
        public async Task<Mascota> FindMascotaAsync(int idmascota)
        {
            string request = "/api/mascotas/" + idmascota;
            Mascota mascotas =
                await this.CallApiAsync<Mascota>(request);
            return mascotas;
        }

        public async Task FullBajaMascotasRufugio(int idrefugio, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/mascotas/" + idrefugio;
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add
                ("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }

        public async Task UpdateMascotaAsync(Mascota mascota, string token)
        {

            using (HttpClient client = new HttpClient())
            {
                string request = "/api/mascotas";
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
           ("Authorization", "bearer " + token);
                Mascota mascotacambiada = new Mascota
                {
                    Id = mascota.Id,
                    Adoptado = mascota.Adoptado,
                    Alto = mascota.Alto,
                    Ancho = mascota.Ancho,
                    Descripcion = mascota.Descripcion,
                    Edad = mascota.Edad,
                    Peligrosidad = mascota.Peligrosidad,
                    Imagen = mascota.Imagen,
                    Peso = mascota.Peso,
                    IdRefugio = mascota.IdRefugio,
                    Nombre = mascota.Nombre,
                    Raza = mascota.Raza,
                };
                string jsonVideoJuego =
                    JsonConvert.SerializeObject(mascotacambiada);
                StringContent content =
                    new StringContent(jsonVideoJuego, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }
        public async Task UpdateEstadoAdopcionAsync(int idmascota, bool estado, string token)
        {

            using (HttpClient client = new HttpClient())
            {
                string request = "/api/mascotas/updateestadoadopcion/"+idmascota+"/"+estado;
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
           ("Authorization", "bearer " + token);

                HttpResponseMessage response =
                    await client.PutAsync(request, null);
            }
        }



        public async Task NewMascotaAsync(Mascota mascota, string token)
        {

            using (HttpClient client = new HttpClient())
            {
                string request = "/api/mascotas";
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
              ("Authorization", "bearer " + token);


                Mascota mascotanueva = new Mascota
                {
                    Id = mascota.Id,
                    Adoptado = mascota.Adoptado,
                    Alto = mascota.Alto,
                    Ancho = mascota.Ancho,
                    Descripcion = mascota.Descripcion,
                    Edad = mascota.Edad,
                    Peligrosidad = mascota.Peligrosidad,
                    Imagen = mascota.Imagen,
                    Peso = mascota.Peso,
                    IdRefugio = mascota.IdRefugio,
                    Nombre = mascota.Nombre,
                    Raza = mascota.Raza,
                };
                string jsonMascota =
                    JsonConvert.SerializeObject(mascotanueva);
                StringContent content =
                    new StringContent(jsonMascota, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);

            }
        }
        /**
         * Servicios VOLUNTARIOS
         */
        public async Task<List<Voluntario>> GetVoluntariosAsync()
        {
            string request = "/api/voluntarios";
            List<Voluntario> voluntarios =
                await this.CallApiAsync<List<Voluntario>>(request);
            return voluntarios;
        } 
        public async Task<Voluntario> FindVoluntarioAsync(int idvoluntario)
        {
            string request = "/api/voluntarios/"+idvoluntario;
            Voluntario voluntario =
                await this.CallApiAsync<Voluntario>(request);
            return voluntario;
        }
        public async Task NewVoluntarioAsync(Voluntario voluntario, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/voluntarios";
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
              ("Authorization", "bearer " + token);


                Voluntario voluntarionuevo = new Voluntario
                {
                    IdVoluntario = voluntario.IdVoluntario,
                    Correo = voluntario.Correo,
                    Fecha_Nacimiento = voluntario.Fecha_Nacimiento,
                    IdRefugio = voluntario.IdRefugio,
                    Imagen = voluntario.Imagen,
                    Mensaje = voluntario.Mensaje,
                    Municipio = voluntario.Municipio,
                    Nombre = voluntario.Nombre,
                };
                string jsonVoluntario =
                    JsonConvert.SerializeObject(voluntarionuevo);
                StringContent content =
                    new StringContent(jsonVoluntario, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }
        public async Task UpdateVoluntarioAsync(Voluntario voluntario, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/voluntarios";
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
              ("Authorization", "bearer " + token);


                Voluntario voluntarionuevo = new Voluntario
                {
                    IdVoluntario = voluntario.IdVoluntario,
                    Correo = voluntario.Correo,
                    Fecha_Nacimiento = voluntario.Fecha_Nacimiento,
                    IdRefugio = voluntario.IdRefugio,
                    Imagen = voluntario.Imagen,
                    Mensaje = voluntario.Mensaje,
                    Municipio = voluntario.Municipio,
                    Nombre = voluntario.Nombre,
                };
                string jsonVoluntario =
                    JsonConvert.SerializeObject(voluntarionuevo);
                StringContent content =
                    new StringContent(jsonVoluntario, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }
        public async Task DeleteVoluntarioAsync(int idvoluntario, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/voluntarios/" + idvoluntario;
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }
        /**
         * 
         * Metodos PUBLICACIONES RESCUTEBLOG
         * 
         */
        public async Task<List<BlogModel>> GetPublicacionesAsync()
        {
            string request = "/api/blog";
            List<BlogModel> publicaciones =
                await this.CallApiAsync<List<BlogModel>>(request);
            return publicaciones;
        } 
        public async Task<BlogModel> FindPublicacionAsync(int idblog)
        {
            string request = "/api/blog/"+idblog;
            BlogModel publicacion =
                await this.CallApiAsync<BlogModel>(request);
            return publicacion;
        }

        public async Task NewBlogAsync(BlogModel post, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/blog";
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
              ("Authorization", "bearer " + token);


                BlogModel publicacion = new BlogModel
                {
                   IdPost = post.IdPost,
                   Imagen = post.Imagen,
                   Contenido = post.Contenido,
                   Fecha = DateTime.UtcNow.ToString("dd - MM - yyyy"),
                   IdUser = post.IdUser,
                   Titulo = post.Titulo

                };
                string jsonBlog =
                    JsonConvert.SerializeObject(publicacion);
                StringContent content =
                    new StringContent(jsonBlog, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }
        public async Task EditBlogAsync(BlogModel post, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/blog";
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
              ("Authorization", "bearer " + token);


                BlogModel publicacion = new BlogModel
                {
                   IdPost = post.IdPost,
                   Imagen = post.Imagen,
                   Contenido = post.Contenido,
                   Fecha = post.Fecha,
                   IdUser = post.IdUser,
                   Titulo = post.Titulo

                };
                string jsonBlog =
                    JsonConvert.SerializeObject(publicacion);
                StringContent content =
                    new StringContent(jsonBlog, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }

        public async Task DeletePublicacionAsync(int idblog, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/blog/" + idblog;
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add
            ("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }
        /**
         * Metodos ADOPCIONES
         */

        public async Task NewAdopcionAsync(int idmascota,int iduser, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/adopciones/nuevaadopcion/"+idmascota+"/"+iduser;
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
              ("Authorization", "bearer " + token);
              
                HttpResponseMessage response =
                    await client.PostAsync(request, null);
            }
        }

        public async Task DevolverAnimalAlRefugioAsync(int idadopcion, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/adopciones/" + idadopcion;
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
              ("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }



        /**
         * Servicios COMENTARIOS
         */

        public async Task<List<Comentario>> GetComentariosAsync()
        {
            string request = "/api/comentarios";
            List<Comentario> comentarios =
                await this.CallApiAsync<List<Comentario>>(request);
            return comentarios;
        } 
        public async Task<Comentario> FindComentarioAsync(int idcomentario)
        {
            string request = "/api/comentarios/"+idcomentario;
            Comentario comentarios =
                await this.CallApiAsync<Comentario>(request);
            return comentarios;
        }

        public async Task NewComentarioAsync(int idpost, string correo, string comentario, DateTime fechacomentario, int iduser, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/comentarios/newcomentario";
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
              ("Authorization", "bearer " + token);
                Comentario nuevocomentario = new Comentario
                {
                    IdUser = iduser,
                    Fecha = fechacomentario.ToString(),
                    ComentarioDesc = comentario,
                    Email = correo,
                    IdPost = idpost,

                };
                string jsonComentarios =
                    JsonConvert.SerializeObject(nuevocomentario);
                StringContent content =
                    new StringContent(jsonComentarios, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }
        public async Task EditComentarioAsync(Comentario comentario, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/comentarios";
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
              ("Authorization", "bearer " + token);


                Comentario nuevocomentario = new Comentario
                {
                    IdComentario = comentario.IdComentario,
                    IdUser = comentario.IdUser,
                    Fecha = comentario.Fecha,
                    ComentarioDesc = comentario.ComentarioDesc,
                    Email = comentario.Email,
                    IdPost = comentario.IdPost

                };
                string jsonComentarios =
                    JsonConvert.SerializeObject(nuevocomentario);
                StringContent content =
                    new StringContent(jsonComentarios, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }
        public async Task DeleteComentarioAsync(int idcomentario, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/comentarios/" + idcomentario;
                client.BaseAddress = new Uri(this.UrlApiRescuteBlog);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add
              ("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }



    }
}