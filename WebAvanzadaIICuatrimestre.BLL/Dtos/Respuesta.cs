using System;
using System.Collections.Generic;
using System.Text;

namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class Respuesta<T>
    {
        public bool esCorrecto { get; set; }

        public string mensaje { get; set; } = string.Empty;

        public T Dato { get; set; }

        public int codigo { get; set; }


        public Respuesta()
        {
            esCorrecto = true;
            mensaje = "Operación realizada correctamente";
            codigo = 200;
        }

    }
}
