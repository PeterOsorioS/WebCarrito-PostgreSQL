$(document).ready(function () {

 
    if (typeof AOS !== 'undefined') {
        AOS.init({
            duration: 1000,
            easing: 'ease-in-out',
        });
    } else {
        console.error("AOS no está definido. Asegúrate de que el script de AOS esté correctamente cargado.");
    }

 
    $("#formBuscar").submit(function (event) {
        event.preventDefault(); 

        var form = $(this);
        var url = form.attr('action');
        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            type: "POST",
            url: url,
            data: form.serialize(),
            headers: {
                'RequestVerificationToken': token
            },
            success: function (response) {
                $("#product").html(response);
            },
            error: function (xhr, status, error) {
                console.error("Error en la búsqueda: ", error);
            }
        });
    });


    function actualizarProductos() {
        $("#formBuscar").submit();
    }


    $("#btnAplicarFiltros").click(function (event) {
        event.preventDefault();
        actualizarProductos();
    });

    $(document).on('click', '.ProductoTienda',function (event) {
        event.stopPropagation(); 
        if (!$(event.target).hasClass('add-to-cart-btn')) {
            var productId = $(this).data('product-id');
            DetalleProducto(productId);
        }
        
    });
    window.DetalleProducto = function (id) {
        $.ajax({
            url: '/Tienda/Home/DetalleProducto/' + id,
            type: 'GET',
            success: function (data) {
                $('#ContenedorModalDetalleProducto').html(data);
                $('#modal_detalle_producto').modal('show');
            },
            error: function (xhr, status, error) {
                toastr.error("Error al cargar los detalles del producto.");
            }
        });
    };
});


$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    var dataTable = $("#tbl").DataTable({
        responsive: true,
        ordering: 'desc',
        "language": {
            "url": "//cdn.datatables.net/plug-ins/2.1.0/i18n/es-MX.json"
        },
        "width": "100%"
    });
}

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