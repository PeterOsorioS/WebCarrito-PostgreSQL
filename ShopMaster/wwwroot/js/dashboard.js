$(document).ready(function () {
   
    let today = new Date().toISOString().split('T')[0];

  
    $('#txtfechainicio').val(today);
    $('#txtfechafin').val(today);

    $("#formBuscar").submit(function (event) {
        event.preventDefault();

        let form = $(this);
        let url = form.attr('action');

        $.ajax({
            type: "POST",
            url: url,
            data: form.serialize(),
            success: function (response) {
         
                $("#tbl tbody").html(response);
            },
            error: function (xhr, status, error) {
                console.error("Error en la búsqueda: ", error);
            }
        });
    });

    $("#btnExportar").click(function () {
      
        $('#hiddenFechaInicio').val($('#txtfechainicio').val());
        $('#hiddenFechaFin').val($('#txtfechafin').val());

      
        $("#formExportar").submit();
    });
});
