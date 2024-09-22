$(document).ready(function () {
    function getAntiForgeryToken() {
        return $('input[name="__RequestVerificationToken"]').val();
    }

    function getProductFormData() {
        var formData = new FormData();
        var files = $('#subidaArchivo').prop('files');

        formData.append("ID", $('#ID').val());
        formData.append("Descripcion", $('#Descripcion').val());
        formData.append("Activo", $('#Activo').is(':checked'));


        if (files.length > 0) {
            formData.append("archivos", files[0]);
        }

        return formData;
    }

    window.Edit_Slider = function (id) {
        $.ajax({
            url: '/Complementos/Slider/EditSlider/' + id,
            type: 'GET',
            success: function (data) {
                $('#ContenedorModalEditSlider').html(data);
                $('#modal_edit_slider').modal('show');

              

                $('#saveChanges').off('click').on('click', function () {
                    var formData = getProductFormData();

                    $(".modal-body").LoadingOverlay("show", {
                        text: "Cargando...",
                        size: 28,
                        textResizeFactor: 0.4
                    });

                    $.ajax({
                        url: '/Complementos/Slider/EditSlider/',
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
                                    $('#modal_edit_producto').modal('hide');
                                    location.reload();
                                }, 1300);
                            } else {
                                toastr.error("Error al actualizar el slider");
                                setTimeout(function () {
                                    $(".modal-body").LoadingOverlay("hide");
                                    $('#mensajeError').show();
                                    $('#mensajeError').html("Todos los campos son obligatorios.");
                                }, 1300);
                            }
                        },
                        error: function (xhr) {
                            setTimeout(function () {
                                toastr.error("Error al actualizar el slider");
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
            url: '/Complementos/Slider/Create/',
            type: 'GET',
            success: function (data) {
                $('#ContenedorModalCreateSlider').html(data);
                $('#modal_create_slider').modal('show');
                $('#mensajeError').hide();


                $('#createSliderForm').on('keydown', function (event) {
                    if (event.key === "Enter") {
                        event.preventDefault();
                        return false;
                    }
                });

                $('#saveChangesCreate').off('click').on('click', function () {
                    var form = $('#createSliderForm')[0];
                    var formData = new FormData(form);




                    $(".modal-body").LoadingOverlay("show", {
                        text: "Cargando...",
                        size: 28,
                        textResizeFactor: 0.4
                    });

                    $.ajax({
                        url: '/Complementos/Slider/Create',
                        type: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,
                        success: function (response) {
                            if (response.success) {
                                toastr.success(response.message);
                                setTimeout(function () {
                                    $('#modal_create_slider').modal('hide');
                                    $('body').fadeOut(800, function () {
                                        location.reload();
                                    });
                                }, 1500);
                            } else {
                                setTimeout(function () {
                                    $(".modal-body").LoadingOverlay("hide");
                                    $('#mensajeError').show();
                                    $('#mensajeError').html("Todos los campos son obligatorios.");
                                    toastr.error("Ocurrió un  error al crear el slider");
                                }, 1300);
                            }
                        },
                        error: function (xhr) {
                            setTimeout(function () {
                                $(".modal-body").LoadingOverlay("hide");
                                toastr.error("Ocurrió un  error al crear el slider");
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