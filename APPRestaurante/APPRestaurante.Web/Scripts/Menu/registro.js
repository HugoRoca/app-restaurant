var menuRegistroModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;

    me.Elementos = (function () {
        function getFecha() { return $('input[name=Fecha]'); }
        function getTitulo() { return $('input[name=Titulo]'); }
        function getDescripcion() { return $('input[name=Descripcion]'); }
        function getTipo() { return $('input[name=Tipo]'); }
        function getPrecio() { return $('input[name=Precio]'); }
        return {
            getFecha: getFecha,
            getTitulo: getTitulo,
            getDescripcion: getDescripcion,
            getTipo: getTipo,
            getPrecio: getPrecio
        }
    })();

    me.Servicios = (function () {
        function registrar(formData) {
            $.ajax({
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
            formData.append("Fecha", me.Elementos.getFecha().val());
            formData.append("Titulo", me.Elementos.getTitulo().val());
            formData.append("Descripcion", me.Elementos.getDescripcion().val());
            formData.append("Tipo", me.Elementos.getTipo().val());
            formData.append("Precio", me.Elementos.getPrecio().val());

            var successRegistro = function (r) {
                if (!r.Success) {
                    alert('Debe de completar todos los campos');
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
            body.on('click', 'button[name=btnGuardar]', me.Eventos.insertarDatos);
            
            me.Elementos.getFecha().prop("disabled", true);

            var fecha = new Date();
            me.Elementos.getFecha().val(FuncionesGenerales.ConvertirFechaDDMMYYYY(fecha));
        }
        return {
            inicializarEventos: inicializarEventos
        }
    })();

    me.InicializarEventos = function () {
        me.Funciones.inicializarEventos();
    }

    return me;


})(menuRegistroData, jQuery);

window.menuRegistroModule = menuRegistroModule;

$(document).ready(function () {
    menuRegistroModule.InicializarEventos();
});