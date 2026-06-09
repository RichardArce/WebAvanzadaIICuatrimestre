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
                    url: 'Duenno/GetDuennos',
                    type: 'GET',
                    dataSrc: 'dato'
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
                                   <button class="btn btn-sm btn-primary btn-editar" data-id="${row.id}">Editar</button>
                                   <button class="btn btn-sm btn-danger btn-eliminar" data-id="${row.id}">Eliminar</button>
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

            $('#tblDuenno').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                Duenno.cargarDuenno(id);
            });
            $('#tblDuenno').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                Duenno.eliminarDuenno(id);
            });

            $('#btnGuardarDuenno').on('click', function () {
                Duenno.guardarDuenno();
            });

            $('#btnEditarDuenno').on('click', function () {
                Duenno.editarDuenno();
            });

        },
        guardarDuenno() {
            let form = $('#formCrearDuenno');

            if (!form.valid()) { //VALIDAR FORMULARIO
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {

                    if (respuesta.esCorrecto) {

                        $('#modalCrearDuenno').modal('hide');
                        form[0].reset();
                        Duenno.tabla.ajax.reload();

                        Swal.fire({
                            title: 'Correcto',
                            text: respuesta.mensaje,
                            icon: 'success'
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Incorrecto',
                            text: respuesta.mensaje,
                            icon: 'error'
                        });
                    }

                }


            })
        },


        editarDuenno() {
            let form = $('#formEditarDuenno');

            if (!form.valid()) { //VALIDAR FORMULARIO
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {

                    if (respuesta.esCorrecto) {

                        $('#modalEditarDuenno').modal('hide');
                        form[0].reset();
                        Duenno.tabla.ajax.reload();

                        Swal.fire({
                            title: 'Correcto',
                            text: respuesta.mensaje,
                            icon: 'success'
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Incorrecto',
                            text: respuesta.mensaje,
                            icon: 'error'
                        });
                    }

                }


            })
        },

        eliminarDuenno(id) {

            Swal.fire({
                title: "Estas seguro?",
                text: "No podras revertir esta operacion!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Si, eliminar",
            }).then((result) => {

                if (result.isConfirmed) {

                    $.ajax({
                        url: `/Duenno/DeleteDuenno?id=${id}`,
                        type: 'DELETE',
                        success: function (respuesta) {
                            if (respuesta.esCorrecto) {
                                Duenno.tabla.ajax.reload();
                                Swal.fire({
                                    title: 'Correcto',
                                    text: respuesta.mensaje || 'Dueño eliminado correctamente',
                                    icon: 'success'
                                });
                            } else {
                                Swal.fire({
                                    title: 'Incorrecto',
                                    text: respuesta.mensaje || 'No se pudo eliminar el dueño',
                                    icon: 'error'
                                });
                            }
                        },
                        error: function () {
                            Swal.fire({
                                title: 'Error',
                                text: 'Ocurrió un error al intentar eliminar el dueño',
                                icon: 'error'
                            });
                        }
                    });

                }


            });


        },
        cargarDuenno(id) {

            $.get(`/Duenno/GetDuennoById?id=${id}`, function (resultado) {
                //Espacios, para dividir el proceso
                if (resultado.esCorrecto) {
                    let data = resultado.dato;                 //1. Cargar los datos

                    $('#Id').val(data.id);                     //2. Pintar los datos en el formulario
                    $('#Nombre').val(data.nombre);
                    $('#Apellido1').val(data.apellido1);
                    $('#Apellido2').val(data.apellido2);
                    $('#Edad').val(data.edad);
                                                               //3. Mostrar el modal

                    $('#modalEditarDuenno').modal('show');
                }
            });
        },
        


    };
    $(document).ready(() => Duenno.init());

})(); //Encapsulamos el código para evitar conflictos con otras partes del proyecto
