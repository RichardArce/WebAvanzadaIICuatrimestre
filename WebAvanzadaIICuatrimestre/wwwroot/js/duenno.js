(() => {

    const Duenno = {
        tabla: null,
        init() {
            this.inicializarTabla();
            this.registrarEventos()
        },
        inicializarTabla() {
            this.tabla = $('#tblDuenno').DataTable({
                ajax: {
                    url: 'Duenno/ObtenerDuennos',
                    type: 'GET',
                    dataSrc:'data'
                },
                columns: [
                    { data: 'id' },
                    { data: 'nombre' },
                    { data: 'edad' },
                    { data: 'apellido1' },
                    { data: 'apellido2' },
                    {
                        data: null,
                        title: 'Acciones',
                        orderable: false,
                        render: (data, type, row) => {
                            return `
                                   <button class="btn btn-sm btn-primary editar" data-id="${row.id}">Editar</button>
                                   <button class="btn btn-sm btn-danger eliminar" data-id="${row.id}">Eliminar</button>
                                    `
                        }
                    }
                ],

                language: {
                    url: 'https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
                }

            });
        },
        registrarEventos() {

            $('#btnGuardarDuenno').on('click', function() {
                Duenno.guardarDuenno();
            });

        },
        guardarDuenno() {
            let form = $('#formCrearDuenno');

            if (!form.valid()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {

                    if (respuesta.esCorrecto) {

                        Swal.fire({
                            title: 'Correcto',
                            texte: respuesta.mensaje,
                            icon: 'success'
                        });
                    }
                    else {
                        //Podria estar un mensaje de error
                    }

                }


            })


        }
    };



    $(document).ready(() => Duenno.init());

})(); //Encapsulamos el código para evitar conflictos con otras partes del proyecto
