

$(document).ready(function () {
    function getAntiForgeryToken() {
        return $('input[name="__RequestVerificationToken"]').val();
    }

    function getCategoriaFormData() {
        var formData = new FormData();
        var files = $('#subidaArchivo').prop('files');

     
        formData.append("IdCategoria", $('#IdCategoria').val());
        formData.append("Descripcion", $('#Descripcion').val());
        formData.append("Activo", $('#Activo').is(':checked'));
        formData.append("FechaRegistro", $('#FechaRegistro').val());


        if (files.length > 0) {
            formData.append("archivos", files[0]); // Añadimos el archivo al formData
        } 
        return formData;
    }

    window.Edit_Categoria = function (id) {
        $.ajax({
            url: '/Mantenimiento/Categorias/EditCategoria/' + id,
            type: 'GET',
            success: function (data) {
                $('#ContenedorModalEditCategoria').html(data);
                $('#modal_edit_categoria').modal('show');

                $('#saveChanges').off('click').on('click', function () {

                    var formData = getCategoriaFormData();

                    $(".modal-body").LoadingOverlay("show", {
                        text: "Cargando...",
                        size: 28,
                        textResizeFactor: 0.4
                    });

                    $.ajax({
                        url: '/Mantenimiento/Categorias/EditCategoria/',
                        type: 'PUT',
                        data: formData,
                        processData: false,
                        contentType: false, 
                        headers: {
                            'RequestVerificationToken': getAntiForgeryToken()
                        },
                        success: function (response) {
                            if (response.success) {
                                toastr.success(response.message);
                                setTimeout(function () {
                                    $(".modal-body").LoadingOverlay("hide");
                                    $('#modal_edit_categoria').modal('hide');
                                    location.reload();
                                }, 1300);
                            } else {
                                toastr.error(response.message || 'Se produjo un error.');
                            }
                        },
                        error: function (xhr) {
                            setTimeout(function () {
                                var errorMessage = xhr.responseJSON ? xhr.responseJSON.message : 'Ocurrió un error.';
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
    window.Create = function () {
        $.ajax({
            url: '/Mantenimiento/Categorias/Create/',
            type: 'GET',
            success: function (data) {
                $('#ContenedorModalCreateCategoria').html(data);
                $('#modal_create_categoria').modal('show');
                $('#mensajeError').hide();

                $('#createCategoryForm').on('keydown', function (event) {
                    if (event.key === "Enter") {
                        event.preventDefault();
                        return false;
                    }
                });

                $('#saveChangesCreate').off('click').on('click', function () {
                    var form = $('#createCategoryForm')[0];
                    var formData = new FormData(form);





                    $(".modal-body").LoadingOverlay("show", {
                        text: "Cargando...",
                        size: 28,
                        textResizeFactor: 0.4
                    });

                    $.ajax({
                        url: '/Mantenimiento/Categorias/Create',
                        type: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,
                        success: function (response) {
                            if (response.success) {
                                toastr.success(response.message);
                                setTimeout(function () {
                                    $('#modal_create_categoria').modal('hide');
                                    $('body').fadeOut(800, function () {
                                        location.reload();
                                    });
                                }, 1500);
                            } else {
                                setTimeout(function () {
                                    $(".modal-body").LoadingOverlay("hide");
                                    $('#mensajeError').show();

                                    $('#mensajeError').html("Todos los campos son obligatorios.");
                                    toastr.error("Error al crear categoria");
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
            },
            error: function () {
                toastr.error('Error al cargar el modal.');
            }
        });
    };
});