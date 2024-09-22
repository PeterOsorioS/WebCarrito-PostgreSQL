$(document).ready(function () {
    function getAntiForgeryToken() {
        return $('input[name="__RequestVerificationToken"]').val();
    }

    function getFormData() {
        return {
            IdUsuario: $('#IdUsuario').val(),
            Nombres: $('#nombres').val(),
            Apellidos: $('#apellidos').val(),
            Correo: $('#correo').val(),
            Clave: $('#clave').val(),
            ConfirmarClave: $('#clave').val(),
            IdRol: $('#IdRol').val(),
            Activo: $('#activo').is(':checked')
        };
    }

    window.EditUsuario = function (id) {
        $.ajax({
            url: '/Resumen/Usuarios/EditUsuario/' + id,
            type: 'GET',
            success: function (data) {
                $('#ContenedorModal').html(data);
                $('#modal_detalle_usuario').modal('show');
                $('#mensajeError').hide();

                var antiForgeryToken = getAntiForgeryToken();

                $('#editUserForm').on('keydown', function (event) {
                    if (event.key === "Enter") {
                        event.preventDefault();
                        return false;
                    }
                });

                $('#saveChanges').off('click').on('click', function () {
                    var formData = getFormData();
                    var formDataObject = new FormData();

                    $.each(formData, function (key, value) {
                        formDataObject.append('usuario.' + key, value);
                    });

                    $(".modal-body").LoadingOverlay("show", {
                        text: "Cargando...",
                        size: 22,
                        textResizeFactor: 0.3
                    });

                    $.ajax({
                        url: '/Resumen/Usuarios/EditUsuario/',
                        type: 'PUT',
                        data: formDataObject,
                        processData: false,
                        contentType: false,
                        headers: {
                            'RequestVerificationToken': antiForgeryToken
                        },
                        success: function (response) {
                            if (response.success) {
                                setTimeout(function () {
                                    $(".modal-body").LoadingOverlay("hide");
                                    $('#modal_detalle_usuario').modal('hide');
                                }, 1300);
                                toastr.success(response.message);

                                updateUserInList(formData);
                            } else {
                                var errorMessages = [];
                                if (response.errors) {
                                    // Suponiendo que response.errors es un array de mensajes de error
                                    response.errors.forEach(function (message) {
                                        if (message === "The value '' is invalid.") {
                                            message = "Seleccione un rol válido.";
                                        }
                                        errorMessages.push(`<strong>Error:</strong> ${message}`);
                                    });
                                }
                                $('#mensajeError').html(errorMessages.join('<br>')).show();
                                toastr.error("Por favor, revisa los errores y vuelve a intentarlo.");
                                $(".modal-body").LoadingOverlay("hide");
                            }
                        },
                        error: function (xhr) {
                            setTimeout(function () {
                                var errorMessage = xhr.responseJSON ? xhr.responseJSON.message : 'Ocurrió un error.';
                                toastr.error(errorMessage);
                                $(".modal-body").LoadingOverlay("hide");
                            }, 1300);
                        },
                    });
                });
            }
        });
    };

    function updateUserInList(user) {
        var userRow = $('#user_' + user.IdUsuario);
        if (userRow.length) {
            userRow.find('.user-nombres').text(user.Nombres);
            userRow.find('.user-apellidos').text(user.Apellidos);
            userRow.find('.user-correo').text(user.Correo);
            userRow.find('.user-activo').html(user.Activo ? '<i class="fa-solid fa-circle color-green"></i>' : '<i class="fa-solid fa-circle color-red"></i>');
        }
    }
});
$(document).ready(function () {
    function getAntiForgeryToken() {
        return $('input[name="__RequestVerificationToken"]').val();
    }

    window.Create = function () {
        $.ajax({
            url: '/Resumen/Usuarios/Create/',
            type: 'GET',
            success: function (data) {
                $('#ContenedorModalCreate').html(data);
                $('#modal_create_usuario').modal('show');
                $('#mensajeError').hide();

                var antiForgeryToken = getAntiForgeryToken();

                $('#createUserForm').on('keydown', function (event) {
                    if (event.key === "Enter") {
                        event.preventDefault();
                        return false;
                    }
                });

                $('#saveChangesCreate').off('click').on('click', function () {

                    var formData = new FormData();
                    formData.append("usuario.Nombres", $('#nombresN').val());
                    formData.append("usuario.Apellidos", $('#apellidosN').val());
                    formData.append("usuario.Correo", $('#correoN').val());
                    formData.append("usuario.Clave", $('#claveN').val());
                    formData.append("usuario.ConfirmarClave", $('#claveN').val());
                    formData.append("usuario.IdRol", $('#IdRol').val());
                    formData.append("usuario.Activo", $('#activoN').is(':checked'));

                    $(".modal-body").LoadingOverlay("show", {
                        text: "Cargando...",
                        size: 22,
                        textResizeFactor: 0.3
                    });

                    $.ajax({
                        url: '/Resumen/Usuarios/Create',
                        type: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,                
                        headers: {
                            'RequestVerificationToken': antiForgeryToken
                        },
                        success: function (response) {
                            $(".modal-body").LoadingOverlay("hide");
                            if (response.success) {
                                toastr.success(response.message);
                                setTimeout(function () {
                                    $('#modal_create_usuario').modal('hide');
                                    $('body').fadeOut(800, function () {
                                        location.reload();
                                    });
                                }, 1500);
                            } else {
                                var errorMessages = [];
                                if (response.errors) {
                                    // Suponiendo que response.errors es un array de mensajes de error
                                    response.errors.forEach(function (message) {
                                        if (message === "The value '' is invalid.") {
                                            message = "Seleccione un rol válido.";
                                        }
                                        errorMessages.push(`<strong>Error:</strong> ${message}`);
                                    });
                                }
                                $('#mensajeError').html(errorMessages.join('<br>')).show();
                                toastr.error("Por favor, revisa los errores y vuelve a intentarlo.");
                            }
                        },
                        error: function (xhr) {
                            $(".modal-body").LoadingOverlay("hide");
                            toastr.error("Se produjo un error al procesar la solicitud.");
                        },
                    });
                });
            },
            error: function () {
                toastr.error('Error al cargar el modal.');
            }
        });
    };
});