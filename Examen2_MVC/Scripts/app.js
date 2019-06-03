//################# DEFINCIONES GENERALES
//Area de de edicion con funciones de negrita subrayado - editor de texto

//Personalizar input de tiempo
$('.seleccion_tiempo').timepicker({
    showInputs: false
});

//Personalizr input de fecha
var date_input = $('.seleccion_fecha');
var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
var options = {
    format: 'yyyy/mm/dd',
    container: container,
    todayHighlight: true,
    autoclose: true,
    locale: 'es'
};

date_input.datepicker(options);
date_input.datepicker('setDate', '');

//Configuracion para las notificaciones
toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "rtl": false,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": 300,
    "hideDuration": 1000,
    "timeOut": 5000,
    "extendedTimeOut": 1000,
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

//################### FUNCIONES GENERALES

//Funcion para relizar una peticion ajax
var llamarAjax = function (nombre, accionPersonalizada) {
    var frmVlalidation = $(nombre).valid();

    if (frmVlalidation === true) {
        var ajaxConfig = {
            contentType: false,
            processData: false,
            data: new FormData($(nombre)[0]),
            type: $(nombre).attr('method'),
            url: $(nombre).attr('action'),
            success: accionPersonalizada
        }

        $.ajax(ajaxConfig);
    }
}

function mensajeEditar(nombre_modal) {
    //Ocultar modal
    $(nombre_modal).modal('toggle');

    //Mostrar mensaje de actualizacion exitosa
    toastr.success("Se edito correctamente.", "Aviso del Sistema", 'Aviso del Sistema');
}

function mensajeCrear(nombre_modal) {
    //Ocultar modal
    $(nombre_modal).modal('toggle');

    //Mostrar mensaje de creado exitoso
    toastr.success("Se creo correctamente.", "Aviso del Sistema", 'Aviso del Sistema');
}

function mensajeEliminar(nombre_modal) {
    //Ocultar modal
    $(nombre_modal).modal('toggle');

    //Mostrar mensaje de creado exitoso
    toastr.success("Se elimino correctamente.", "Aviso del Sistema", 'Aviso del Sistema');
}

function mensajeInfo(msj) {
    var mensaje = msj;
    if (mensaje !== "") {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "rtl": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": 300,
            "hideDuration": 1000000000000,
            "timeOut": 50000000000,
            "extendedTimeOut": 10000000000,
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.info(mensaje, "Aviso del Sistema", 'Aviso del Sistema', { timeOut: 500000 });
    }
}

function mensajeError() {
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        llamarAjax;
    });

}

//Funcion encargado de abrir un modal
function abrirModal(nombre_modal) {
    //funcion que abre el modal si este esta cerrado y si este este abirto lo cirra
    $(nombre_modal).modal('toggle');
    //Activar la validacion con data notation en el formulario del modal
    $.validator.unobtrusive.parse($("#form0"));
}
