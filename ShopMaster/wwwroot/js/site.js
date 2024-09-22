(function ($) {
    "use strict";


    $("#sidebarToggle, #sidebarToggleTop").on('click', function (e) {
        $("body").toggleClass("sidebar-toggled");
        $(".sidebar").toggleClass("toggled");
        if ($(".sidebar").hasClass("toggled")) {
            $('.sidebar .collapse').collapse('hide');
        }
    });

    $(window).resize(function () {
        var windowWidth = $(window).width();

        if (windowWidth < 768) {
            $('.sidebar .collapse').collapse('hide');
        }

        if (windowWidth < 480 && !$(".sidebar").hasClass("toggled")) {
            $("body").addClass("sidebar-toggled");
            $(".sidebar").addClass("toggled");
            $('.sidebar .collapse').collapse('hide');
        }
    });


    $('body.fixed-nav .sidebar').on('mousewheel DOMMouseScroll wheel', function (e) {
        if ($(window).width() > 768) {
            var e0 = e.originalEvent,
                delta = e0.wheelDelta || -e0.detail;
            this.scrollTop += (delta < 0 ? 1 : -1) * 30;
            e.preventDefault();
        }
    });

  
    $(document).on('scroll', function () {
        var scrollDistance = $(this).scrollTop();
        if (scrollDistance > 100) {
            $('.scroll-to-top').fadeIn();
        } else {
            $('.scroll-to-top').fadeOut();
        }
    });

  
    $(document).on('click', 'a.scroll-to-top', function (e) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: ($($anchor.attr('href')).offset().top)
        }, 1000, 'easeInOutExpo');
        e.preventDefault();
    });


    $('.sidebar .collapse').on('hide.bs.collapse', function () {
        $(this).prev().find('.fas').removeClass('fa-angle-down').addClass('fa-angle-right');
    });

    $('.sidebar .collapse').on('show.bs.collapse', function () {
        $(this).prev().find('.fas').removeClass('fa-angle-right').addClass('fa-angle-down');
    });

})(jQuery);


$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    var dataTable = $("#tbl").DataTable({
        responsive: true,
        ordering: 'desc',
        "language": {
            "url": "//cdn.datatables.net/plug-ins/2.1.7/i18n/es-MX.json"
        },
        "width": "100%"
    });
}
$(document).on('click', '.delete-user', function (e) {
    e.preventDefault();
    var deleteUrl = $(this).data('url');
    var row = $(this).closest('tr');
    Swal.fire({
        title: "¿Estás seguro de borrar?",
        text: "Este contenido no se puede recuperar.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sí, borrar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: deleteUrl,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        row.remove();
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function (xhr, status, error) {
                    toastr.error("Se produjo un error al intentar eliminar");
                }
            });
        }
    });
});

$(document).ready(function () {

    window.Perfil_Usuario = function (id) {
        $.ajax({
            url: '/Resumen/Usuarios/Perfil/' + id,
            type: 'GET',
            success: function (data) {
                $('#ContenedorModalPerfilUsuario').html(data);
                $('#modal_perfil_usuario').modal('show');
            },
            error: function (xhr, status, error) {
                toastr.error("Error al cargar el perfil");
            }
        });
    };

});