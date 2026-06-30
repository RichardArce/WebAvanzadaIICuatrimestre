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
                        data: 'telefonos',
                        render: (data) => {
                            if (!data || data.length === 0) {
                                return '<span class="text-muted">Sin teléfono</span>';
                            }
                            return data.map(t => t.numero).join(', ');
                        }
                    },
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

            $('#btnAgregarTelefonoCrear').on('click', function () {
                Duenno.agregarFilaTelefono('#telefonosCrear');
            });

            $('#btnAgregarTelefonoEditar').on('click', function () {
                Duenno.agregarFilaTelefono('#telefonosEditar');
            });

            $(document).on('click', '.btn-quitar-telefono', function () {
                const contenedor = $(this).closest('.telefono-row').parent();
                $(this).closest('.telefono-row').remove();
                Duenno.reindexarTelefonos(contenedor);
            });

            $('#modalCrearDuenno').on('hidden.bs.modal', function () {
                $('#telefonosCrear').empty();
            });

        },

        agregarFilaTelefono(selectorContenedor, numero = '') {
            const indice = $(selectorContenedor).children('.telefono-row').length;
            const fila = $(`
                <div class="input-group mb-2 telefono-row">
                    <span class="input-group-text"><i class="bi bi-telephone"></i></span>
                    <input type="text" name="Telefonos[${indice}].Numero" class="form-control" placeholder="Ej: 8888-8888" value="${numero}" />
                    <button type="button" class="btn btn-outline-danger btn-quitar-telefono"><i class="bi bi-dash-lg"></i></button>
                </div>
            `);
            $(selectorContenedor).append(fila);
        },

        reindexarTelefonos(selectorContenedor) {
            $(selectorContenedor).children('.telefono-row').each(function (indice) {
                $(this).find('input').attr('name', `Telefonos[${indice}].Numero`);
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

                },
                error: function (respuesta) { //Mensaje detallado en la vista para que el reporte de error sea más claro
                    Swal.fire({
                        title: 'Incorrecto',
                        text: respuesta.responseJSON.description,
                        icon: 'error'
                    });
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

                    $('#telefonosEditar').empty();           //3. Pintar los telefonos
                    (data.telefonos || []).forEach(t => Duenno.agregarFilaTelefono('#telefonosEditar', t.numero));

                                                               //4. Mostrar el modal

                    $('#modalEditarDuenno').modal('show');
                }
            });
        },
        


    };
    $(document).ready(() => Duenno.init());

})(); //Encapsulamos el código para evitar conflictos con otras partes del proyecto
