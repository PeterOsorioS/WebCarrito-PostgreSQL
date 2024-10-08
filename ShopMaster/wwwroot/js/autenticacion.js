$(document).ready(function () {
    $('#loginForm').on('submit', function (e) {
        e.preventDefault(); // Previene el envío por defecto del formulario

        let correo = $('input[name="Correo"]').val();
        let clave = $('input[name="Clave"]').val();

        // Resetear mensajes de error
        $('#mensajeError').hide();

        // Validación básica del correo y contraseña
        if (!correo) {
            $('#mensajeError').text('El correo es obligatorio.').show();
            return;
        }
        if (!clave) {
            $('#mensajeError').text('La contraseña es obligatoria.').show();
            return;
        }

        // Serializar los datos del formulario
        let formData = $(this).serialize();

        // Enviar solicitud AJAX al servidor
        $.ajax({
            type: 'POST',
            url: '/Acceso/Auth/Login', // URL de la acción de login
            data: formData,
            success: function (response) {
                if (response.success) {
                    // Redirigir a la URL correspondiente
                    window.location.href = response.redirectUrl;
                } else {
                    // Mostrar mensaje de error desde el servidor
                    $('#mensajeError').text(response.message).show();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                // Mostrar un mensaje de error si ocurre un problema inesperado
                $('#mensajeError').text('Ocurrió un error inesperado.').show();
            }
        });
    });
});
