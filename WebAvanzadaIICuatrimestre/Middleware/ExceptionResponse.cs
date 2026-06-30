using System.Net;

namespace WebAvanzadaIICuatrimestre.Middleware
{
    public record ExceptionResponse(HttpStatusCode StatusCode,string description); //.NET 8

    //RECORD ES UNA FOMRA MAS FACIL DE CREAR UNA CLASE INMUTABLE, ES DECIR, QUE NO SE PUEDE MODIFICAR DESPUES DE SER CREADA.

    //SE UTILIZAN PARA CREAR OBJETOS QUE REPRESENTAN DATOS, COMO DTOs (Data Transfer Objects) O MODELOS DE RESPUESTA DE API.
}
