$(document).ready(function () {
    let userId = $('#userData').data('idusuario');
    let token = $('input[name="__RequestVerificationToken"]').val();

   
 
    $(document).on('click', '.add-to-cart-btn', function (event) {
        event.stopPropagation();
        let productId = $(this).data('product-id');
        let button = $(this);


        $.ajax({
            url: '/Tienda/Home/AgregarProductosCarrito/' + productId,
            type: 'GET',
            success: function (data) {
                let carrito = JSON.parse(localStorage.getItem('carrito_' + userId)) || [];
                let productoExistente = carrito.find(p => p.IdProducto === data.idProducto);

                let stockDisponible = data.stock;
                let cantidadAComprar = productoExistente ? productoExistente.Cantidad + 1 : 1;

                if (cantidadAComprar > stockDisponible || cantidadAComprar > 10) {
                    button.prop('disabled', true);
                    return;
                }

                if (productoExistente) {
                    productoExistente.Cantidad = cantidadAComprar;
                } else {
                    carrito.push({
                        IdProducto: data.idProducto,
                        Nombre: data.nombre,
                        Marca: data.marca,
                        Precio: data.precio,
                        RutaImagen: data.rutaImagen,
                        Cantidad: cantidadAComprar,
                        Stock: data.stock
                    });
                }

                localStorage.setItem('carrito_' + userId, JSON.stringify(carrito));
                toastr.success("Producto agregado al carrito");
            },
            error: function (xhr, status, error) {
                toastr.error("Error al obtener los detalles del producto");
            }
        });
        actualizarCantidadCarrito();
    });

    function mostrarProductosEnCarrito() {
        let datos = localStorage.getItem('carrito_' + userId);
        let productos = [];

        try {
            productos = JSON.parse(datos) || [];
        } catch (error) {
            console.error('Error al analizar los datos:', error);
        }

        let productosContainer = $('#productos-container');
        if (productos.length === 0) {
            productosContainer.html('<p class="text-center">No hay productos en el carrito.</p>');
            return;
        }
        let total = 0;
        productosContainer.empty();
        productos.forEach(function (producto) {
       
            let formatoPrecio = producto.Precio.toLocaleString('es-AR', {
                style: 'currency',
                currency: 'ARS',
                minimumFractionDigits: 0,
                maximumFractionDigits: 0
            });

            let productoHtml = `
                <div class="card mb-3 card-producto">
                    <div class="card-body">
                        <div class="row g-3 align-items-center">
                            <div class="col-12 col-sm-2 d-flex justify-content-center">
                                <img src="${producto.RutaImagen}" class="rounded" alt="${producto.Nombre}" style="width:100px; height:100px;">
                            </div>
                            <div class="col-12 col-sm-4 text-sm-start text-center">
                                <span class="fw-bold d-block">${producto.Nombre}</span>
                            </div>
                            <div class="col-6 col-sm-2 text-center">
                                <span class="d-block"><strong>Precio:</strong> ${formatoPrecio}</span>
                            </div>
                            <div class="col-6 col-sm-2 d-flex justify-content-center">
                                <div class="input-group">
                                    <button class="btn btn-outline-secondary btn-minus" data-product-id="${producto.IdProducto}">
                                        <i class="fa-solid fa-minus"></i>
                                    </button>
                                    <input class="form-control input-cantidad text-center" value="${producto.Cantidad}" style="width:40px;" disabled />
                                    <button class="btn btn-outline-secondary btn-plus" data-product-id="${producto.IdProducto}" ${producto.Cantidad >= producto.Stock ? 'disabled' : ''}>
                                        <i class="fa-solid fa-plus"></i>
                                    </button>
                                </div>
                            </div>
                            <div class="col-12 col-sm-2 text-center">
                                <button class="btn btn-outline-danger btn-eliminar" data-product-id="${producto.IdProducto}">
                                    <i class="fa-solid fa-trash"></i> Eliminar
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            `;
            productosContainer.append(productoHtml);
            total += producto.Precio * producto.Cantidad;
        });
        let formatoTotal = total.toLocaleString('es-AR', {
            style: 'currency',
            currency: 'ARS',
            minimumFractionDigits: 0,
            maximumFractionDigits: 0
        });

  
        let totalHtml = `
            <div class="text-end">
                <h5><strong>Total a pagar:</strong> ${formatoTotal}</h5>
            </div>
        `;
        productosContainer.append(totalHtml);
        BtnControlCarrito();
    }

    function BtnControlCarrito() {
        $('.btn-plus').on('click', function () {
            let productId = $(this).data('product-id');
            modificarCantidad(productId, 1);
        });

        $('.btn-minus').on('click', function () {
            let productId = $(this).data('product-id');
            modificarCantidad(productId, -1);
        });

        $('.btn-eliminar').on('click', function () {
            let productId = $(this).data('product-id');
            eliminarProducto(productId);
        });
    }

    function modificarCantidad(productId, cambio) {
        let carrito = JSON.parse(localStorage.getItem('carrito_' + userId)) || [];
        let producto = carrito.find(p => p.IdProducto === productId);

        if (producto) {
            let nuevaCantidad = producto.Cantidad + cambio;
            let stockDisponible = producto.Stock;
            let btnPlus = $(`.btn-plus[data-product-id="${productId}"]`);
            let btnMinus = $(`.btn-minus[data-product-id="${productId}"]`);

            if (nuevaCantidad > stockDisponible || nuevaCantidad > 10) {
                btnPlus.prop('disabled', true);
                return;
            }

            if (nuevaCantidad <= 0) {
                eliminarProducto(productId);
                return;
            }

            producto.Cantidad = nuevaCantidad;
            localStorage.setItem('carrito_' + userId, JSON.stringify(carrito));
            mostrarProductosEnCarrito();
        }
    }

    function eliminarProducto(productId) {
        let carrito = JSON.parse(localStorage.getItem('carrito_' + userId)) || [];
        carrito = carrito.filter(p => p.IdProducto !== productId);

        localStorage.setItem('carrito_' + userId, JSON.stringify(carrito));
        mostrarProductosEnCarrito(); // Actualizar la vista del carrito
    }

    mostrarProductosEnCarrito();

    $('#departamento option:first').prop('disabled', true);

    $('#municipio option:first').prop('disabled', true);

    $('#finalizarCompraBtn').on('click', function () {
        let carrito = JSON.parse(localStorage.getItem('carrito_' + userId)) || [];

        if (carrito.length === 0) {
            toastr.error("No hay productos en el carrito.");
            return;
        }


        let departamentoV = $('#departamento option:first').text();
        let municipioV = $('#municipio option:first').text();

        let contacto = $('#contacto').val();
        let telefono = $('#telefono').val();
        let direccion = $('#direccion').val();
        let departamento = $('#departamento option:selected').val();
        let municipio = $('#municipio option:selected').text();
        let DireccionCompleta = (`${direccion}, ${municipio}, ${departamento}`);

        if (departamento == departamentoV || municipio == municipioV) {
            toastr.error("Por favor ingresar el departamento y municipio");
            return;
        }

        if (!contacto || !telefono || !direccion) {
            toastr.error("Por favor complete todos los campos de contacto.");
            return;
        }

        let venta = {
            IdUsuario: userId,
            Contacto: contacto,
            Telefono: telefono,
            Direccion: DireccionCompleta,
            FechaVenta: new Date().toISOString(),
            TotalProducto: carrito.length,
            MontoTotal: carrito.reduce((total, producto) => total + (producto.Precio * producto.Cantidad), 0),
            DetalleVenta: carrito.map(function (producto) {
                return {
                    IdProducto: producto.IdProducto,
                    Cantidad: producto.Cantidad,
                    Total: producto.Precio * producto.Cantidad
                };
            })
        };

        $.ajax({
            url: '/Tienda/Home/RegistrarVenta',
            type: 'POST',
            data: JSON.stringify(venta),
            contentType: 'application/json; charset=utf-8',
            headers: {
                'RequestVerificationToken': token
            },
            success: function (response) {
                if (response.success) {
                    toastr.success("Venta registrada con éxito.");
                    localStorage.removeItem('carrito_' + userId);
                    mostrarProductosEnCarrito();

                    setTimeout(function () {
                        window.location.href = "/Tienda/Home/ResumenCompra?id=" + response.idVenta;
                    }, 2000);
                } else {
                    toastr.error("Error al registrar la venta: " + response.message);
                }
            },
            error: function (xhr, status, error) {
                toastr.error("Error al registrar la venta.");
            }
        });
    }); 
  
});
