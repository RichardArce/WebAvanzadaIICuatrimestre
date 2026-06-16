(() => {

    const Carro = {
        tabla: null,
        init() {
            this.inicializarTabla();
            this.registrarEventos()
        },
        inicializarTabla() {
            this.tabla = $('#tblCarro').DataTable({
                ajax: {
                    url: 'Carro/GetCarros',
                    type: 'GET',
                    dataSrc: 'dato'
                },
                columns: [
                    { data: 'id' },
                    { data: 'placa' },
                    { data: 'marca' },
                    { data: 'valorFiscal' },
                    {
                        data: 'chocado',
                        render: (data, type, row) => {
                            // Mostrar texto y color según el valor: 1 = Chocado (rojo), 0 = No chocado (verde)
                            const val = typeof data === 'string' ? parseInt(data, 10) : data;
                            if (type === 'display' || type === 'filter') {
                                if (val === 1) {
                                    return `<span class="badge bg-danger">Chocado</span>`;
                                }
                                return `<span class="badge bg-success">No chocado</span>`;
                            }
                            return data;
                        }
                    },
                    { data: 'duenno.nombre', defaultContent: '' },
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

            $('#tblCarro').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                Carro.cargarCarro(id);
            });
            $('#tblCarro').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                Carro.eliminarCarro(id);
            });

            $('#btnGuardarCarro').on('click', function () {
                Carro.guardarCarro();
            });

            // Editar
            $('#btnEditarCarro').on('click', function () {
                Carro.editarCarro();
            });

        },
        guardarCarro() {
            let form = $('#formCrearCarro');

            if (!form.valid()) { //VALIDAR FORMULARIO
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {

                    if (respuesta.esCorrecto) {

                        $('#modalCrearCarro').modal('hide');
                        form[0].reset();
                        Carro.tabla.ajax.reload();

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

        eliminarCarro(id) {

            Swal.fire({
                title: "Estas seguro?",
                text: "No podras revertir esta operacion!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Si, eliminar",
            }).then((result) => {

                if (result.isConfirmed) {

                    $.ajax({
                        url: `/Carro/DeleteCarro?id=${id}`,
                        type: 'DELETE',
                        success: function (respuesta) {
                            if (respuesta.esCorrecto) {
                                Carro.tabla.ajax.reload();
                                Swal.fire({
                                    title: 'Correcto',
                                    text: respuesta.mensaje || 'Carro eliminado correctamente',
                                    icon: 'success'
                                });
                            } else {
                                Swal.fire({
                                    title: 'Incorrecto',
                                    text: respuesta.mensaje || 'No se pudo eliminar el carro',
                                    icon: 'error'
                                });
                            }
                        },
                        error: function () {
                            Swal.fire({
                                title: 'Error',
                                text: 'Ocurrió un error al intentar eliminar el carro',
                                icon: 'error'
                            });
                        }
                    });

                }


            });


        },
        cargarCarro(id) {

            $.get(`/Carro/GetCarroById?id=${id}`, function (resultado) {
                if (resultado.esCorrecto) {
                    let data = resultado.dato;

                    $('#Id').val(data.id);
                    $('#Placa').val(data.placa);
                    $('#Marca').val(data.marca);
                    $('#ValorFiscal').val(data.valorFiscal);
                    $('#Chocado').val(data.chocado);
                    $('#Fkduenno').val(data.fkduenno);

                    // Show edit modal specifically
                    $('#modalEditarCarro').modal('show');
                }
            });
        },

        editarCarro() {
            let form = $('#formEditarCarro');

            if (!form.valid()) { //VALIDAR FORMULARIO
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {

                    if (respuesta.esCorrecto) {

                        $('#modalEditarCarro').modal('hide');
                        form[0].reset();
                        Carro.tabla.ajax.reload();

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

    };
    $(document).ready(() => Carro.init());

})();
