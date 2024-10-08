$(document).ready(function () {
    function getAntiForgeryToken() {
        return $('input[name="__RequestVerificationToken"]').val();
    }

    window.Edit_Marca = function (id) {
        $.ajax({
            url: '/Mantenimiento/Marca/EditMarca/' + id,
            type: 'GET',
            success: function (data) {
                $('#ContenedorModalEditMarca').html(data);
                $('#modal_edit_marca').modal('show');

                let antiForgeryToken = getAntiForgeryToken();
                $('#editMarcaForm').on('keydown', function (event) {
                    if (event.key === "Enter") {
                        event.preventDefault();
                        return false;
                    }
                });

                $('#saveChanges').off('click').on('click', function () {
                    let formData = {
                        IdMarca: $('#IdMarca').val(),
                        Descripcion: $('#Descripcion').val(),
                        Activo: $('#Activo').is(':checked')
                    };

                    $(".modal-body").LoadingOverlay("show", {
                        text: "Cargando...",
                        size: 28,
                        textResizeFactor: 0.4
                    });

               
                    if (!formData.Descripcion.trim()) {
                        setTimeout(function () {
                            $(".modal-body").LoadingOverlay("hide");
                            toastr.error('Por favor, complete todos los campos requeridos.');
                            $('#mensajeError').show();
                            $('#mensajeError').text("Por favor, complete todos los campos requeridos.");
                        }, 1300);
                        return;
                    }

                    $.ajax({
                        url: '/Mantenimiento/Marca/EditMarca/',
                        type: 'PUT',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(formData),
                        headers: {
                            'RequestVerificationToken': antiForgeryToken 
                        },
                        success: function (response) {
                            $(".modal-body").LoadingOverlay("hide");
                            if (response.success) {
                                toastr.success(response.message);
                                setTimeout(function () {
                                    $('#modal_edit_marca').modal('hide');
                                }, 1300);
                                updateMarcaInList(formData);
                            } else {
               
                                let errors = Array.isArray(response.errors) ? response.errors : [response.errors];
                                toastr.error(errors.join(', '));
                            }
                        },
                        error: function (xhr) {
                            $(".modal-body").LoadingOverlay("hide");
                            let errorMessage = xhr.responseJSON ? xhr.responseJSON.message : 'Ocurrió un error.';
                            toastr.error(errorMessage);
                        }
                    });
                });
            },
            error: function () {
                toastr.error('Error al cargar el modal.');
            }
        });
    };

    function updateMarcaInList(marca) {
        let marcaRow = $('#marca_' + marca.IdMarca);
        if (marcaRow.length) {
            marcaRow.find('.marca-desc').text(marca.Descripcion);
            marcaRow.find('.marca-activo').html(marca.Activo ? '<i class="fa-solid fa-circle color-green"></i>' : '<i class="fa-solid fa-circle color-red"></i>');
        }
    }

});

$(document).ready(function () {
    window.Create = function () {
        $.ajax({
            url: '/Mantenimiento/Marca/Create/',
            type: 'GET',
            success: function (data) {
                $('#ContenedorModalCreateMarca').html(data);
                $('#modal_create_marca').modal('show');
                $('#mensajeError').hide();

                $('#createMarcaForm').on('keydown', function (event) {
                    if (event.key === "Enter") {
                        event.preventDefault();
                        return false;
                    }
                });

                $('#saveChangesCreate').off('click').on('click', function () {
                    let form = $('#createMarcaForm');
                    let formData = form.serialize();

                    $(".modal-body").LoadingOverlay("show", {
                        text: "Cargando...",
                        size: 28,
                        textResizeFactor: 0.4
                    });

                    $.ajax({
                        url: '/Mantenimiento/Marca/Create',
                        type: 'POST',
                        data: formData,
                        success: function (response) {
                            $(".modal-body").LoadingOverlay("hide");
                            if (response.success) {
                                toastr.success(response.message);
                                setTimeout(function () {
                                    $('#modal_create_marca').modal('hide');
                                    $('body').fadeOut(800, function () {
                                        location.reload();
                                    });
                                }, 1500);
                            } else {
                                $('#mensajeError').show();
                                $('#mensajeError').html("Todos los campos son obligatorios.");
                                toastr.error("Error al crear marca");
                            }
                        },
                        error: function (xhr) {
                            $(".modal-body").LoadingOverlay("hide");
                            $('#mensajeError').show();
                            $('#mensajeError').html("Todos los campos son obligatorios.");
                            toastr.error("Error al crear marca");
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