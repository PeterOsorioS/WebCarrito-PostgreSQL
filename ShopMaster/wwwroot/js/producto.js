$(document).ready(function () {
    function getAntiForgeryToken() {
        return $('input[name="__RequestVerificationToken"]').val();
    }

    function getProductFormData() {
        let formData = new FormData();
        let files = $('#subidaArchivo').prop('files');

       
        formData.append("producto.IdProducto", $('#IdProducto').val());
        formData.append("producto.Nombre", $('#Nombre').val());
        formData.append("producto.Descripcion", tinymce.get('Descripcion').getContent());
        formData.append("producto.IdCategoria", $('#IdCategoria').val());
        formData.append("producto.IdMarca", $('#IdMarca').val());
        formData.append("producto.Precio", $('#Precio').val());
        formData.append("producto.Stock", $('#Stock').val());
        formData.append("producto.Activo", $('#Activo').is(':checked'));
        formData.append("producto.FechaRegistro", $('#FechaRegistro').val());
        formData.append("producto.NombreImagen", $('#NombreImagen').val());

        if (files.length > 0) {
            formData.append("archivos", files[0]);
        }

        return formData;
    }
    window.Edit_Producto = function (id) {
        $.ajax({
            url: '/Mantenimiento/Productos/EditProducto/' + id,
            type: 'GET',
            success: function (data) {
                $('#ContenedorModalEditProducto').html(data);
                $('#modal_edit_producto').modal('show');

                if (tinymce.get('Descripcion')) {
                    tinymce.get('Descripcion').remove();
                }

                tinymce.init({
                    selector: '#Descripcion',
                    height: 300,
                    menubar: false,
                    resize: false
                });

                $('#saveChanges').off('click').on('click', function () {
                    let formData = getProductFormData();

                    $(".modal-body").LoadingOverlay("show", {
                        text: "Cargando...",
                        size: 28,
                        textResizeFactor: 0.4
                    });

                    $.ajax({
                        url: '/Mantenimiento/Productos/EditProducto/',
                        type: 'PUT',
                        data: formData,
                        processData: false,
                        contentType: false, // Necesario para el envío de archivos
                        headers: {
                            'RequestVerificationToken': getAntiForgeryToken()
                        },
                        success: function (response) {
                            if (response.success) {
                                toastr.success(response.message);
                                setTimeout(function () {
                                    $(".modal-body").LoadingOverlay("hide");
                                    $('#modal_edit_producto').modal('hide');
                                    // Recargar la página después de la actualización exitosa
                                    location.reload();
                                }, 1300);
                            } else {
                                toastr.error(response.message || 'Se produjo un error.');
                            }
                        },
                        error: function (xhr) {
                            setTimeout(function () {
                                let errorMessage = xhr.responseJSON ? xhr.responseJSON.message : 'Ocurrió un error.';
                                toastr.error(errorMessage);
                            }, 1300);
                        }
                    });
                });
            }
        });
    };
});

$(document).ready(function () {
    // Definir la función Create globalmente
    window.Create = function () {
        $.ajax({
            url: '/Mantenimiento/Productos/Create/',
            type: 'GET',
            success: function (data) {
                $('#ContenedorModalCreateProducto').html(data);
                $('#modal_create_producto').modal('show');
                $('#mensajeError').hide();

                // Inicializa TinyMCE en el modal
                tinymce.init({
                    selector: '#editorDescripcion',
                    height: 300,
                    menubar: false,
                    resize: false
                });

                $('#createProductForm').on('keydown', function (event) {
                    if (event.key === "Enter") {
                        event.preventDefault();
                        return false;
                    }
                });

                $('#saveChangesCreate').off('click').on('click', function () {
                    let form = $('#createProductForm')[0];
                    let formData = new FormData(form);

                    // Obtén el contenido de TinyMCE
                    let descripcionContent = tinymce.get('editorDescripcion').getContent();

                    // Añadir el contenido de TinyMCE al formData
                    formData.append('producto.Descripcion', descripcionContent);

                    $(".modal-body").LoadingOverlay("show", {
                        text: "Cargando...",
                        size: 28,
                        textResizeFactor: 0.4
                    });

                    $.ajax({
                        url: '/Mantenimiento/Productos/Create',
                        type: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,
                        success: function (response) {
                            if (response.success) {
                                toastr.success(response.message);
                                setTimeout(function () {
                                    $('#modal_create_producto').modal('hide');
                                    $('body').fadeOut(800, function () {
                                        location.reload();
                                    });
                                }, 1500);
                            } else {
                                setTimeout(function () {
                                    $(".modal-body").LoadingOverlay("hide");
                                    $('#mensajeError').show();
                                    let errorMessages = [];

                                    if (response.errors) {
                                        for (let field in response.errors) {
                                            response.errors[field].forEach(function (message) {
                                                errorMessages.push(`<strong>${field}</strong>: ${message}`);
                                            });
                                        }
                                    }
                                    $('#mensajeError').html(errorMessages.join('<br>'));
                                    toastr.error("Todos los campos son obligatorios.");
                                }, 1300);
                            }
                        },
                        error: function (xhr) {
                            setTimeout(function () {
                                $(".modal-body").LoadingOverlay("hide");
                                toastr.error("Ocurrió un error al procesar la solicitud.");
                            }, 1300);
                        }
                    });
                });

                $('#modal_create_producto').on('hidden.bs.modal', function () {
                    tinymce.remove('#editorDescripcion');
                });
            },
            error: function () {
                toastr.error('Error al cargar el modal.');
            }
        });
    };
});