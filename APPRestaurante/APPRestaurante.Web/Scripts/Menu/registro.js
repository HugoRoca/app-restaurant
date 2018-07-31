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

    })();

    me.Eventos = (function () {

    })();

    me.Funciones = (function () {
        function inicializarEventos() {
            var body = $('body');
            //FuncionesGenerales.LlamarCalendario(me.Elementos.getFecha());
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