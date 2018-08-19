var empleadoRegistroModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;

    me.Elementos = (function () {
        function getNombre() { return $('input[name=Nombres]'); }
        function getApellido() { return $('input[name=Apellidos]'); }
        function getDireccion() { return $('textarea[name=Direccion]'); }
        function getCelular() { return $('input[name=Celular]'); }
        function getTipoDocumento() { return $('select[name=TipoDocumento]'); }
        function getDocumento() { return $('input[name=Documento]'); }
        function getId() { return $('input[name=Id]');}
        return {
            getNombre: getNombre,
            getApellido: getApellido,
            getDireccion: getDireccion,
            getCelular: getCelular,
            getTipoDocumento: getTipoDocumento,
            getDocumento: getDocumento,
            getId: getId
        }
    })();

    me.Servicios = (function () {
        function registrar(formData) {
            return $.ajax({
                url: urls.urlRegistro,
                type: 'POST',
                data: formData,
                dataType: 'json',
                contentType: false,
                processData: false
            });
        }

        return {
            registrar: registrar
        }
    })();

    me.Eventos = (function () {
        function insertarDatos() {
            var formData = new FormData();
            var file = document.getElementById("Foto").files[0];
            formData.append("Foto", file);
            formData.append("Nombre", me.Elementos.getNombre().val());
            formData.append("Apellido", me.Elementos.getApellido().val());
            formData.append("Direccion", me.Elementos.getDireccion().val());
            formData.append("Celular", me.Elementos.getCelular().val());
            formData.append("TipoDocumento", me.Elementos.getTipoDocumento().val());
            formData.append("Documento", me.Elementos.getDocumento().val());
            formData.append("Id", me.Elementos.getId().val());

            var successRegistro = function (r) {
                if (!r.Success) {
                    FuncionesGenerales.AbrirMensaje(r.Message);
                } else {
                    window.location.href = urls.urlPaginaLista;
                }
            }

            me.Servicios.registrar(formData).then(successRegistro, function (e) {
                console.log(e);
            });
        }

        return {
            insertarDatos: insertarDatos
        }
    })();

    me.Funciones = (function () {
        function inicializarEventos() {
            var body = $('body');
            body.on('click', 'button[name=Guardar]', me.Eventos.insertarDatos);
        }

        return {
            inicializarEventos: inicializarEventos
        }
    })();

    me.InicializarEventos = function () {
        me.Funciones.inicializarEventos();
    }

    return me;

})(empleadoRegistroData, jQuery);

window.empleadoRegistroModule = empleadoRegistroModule;

$(document).ready(function () {
    empleadoRegistroModule.InicializarEventos();
});