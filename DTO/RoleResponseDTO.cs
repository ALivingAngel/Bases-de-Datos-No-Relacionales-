using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApiData.DTO
{
    public class RolResponseDto
    {
        public HttpStatusCode StatusCode { get; set; } //Códido de estado de la respuesta
        public bool Result { get; set; } //True si la acción fue completa, False si fue incompleta
        public List<string> Message { get; set; } //Lista de mensajes descriptores del estado final de la acción
    }

}
