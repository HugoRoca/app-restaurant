var menuModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;

    var registros = 5;

    me.Elementos = (function () {
        function getFechaDesde() { return $('.Desde'); }
        function getFechaHasta() { return $('.Hasta'); }
        return {
            getFechaDesde: getFechaDesde,
            getFechaHasta: getFechaHasta
        }
    })();

    me.Servicios = (function () {
        function contarLista(_rows) {
            return $.ajax({
                url: urls.urlContador,
                method: 'POST',
                data: {
                    rows: _rows
                }
            });
        }

        function Lista() {

        }

        return {
            contarLista: contarLista
        }
    })();

    me.Eventos = (function () {
        function LlamarNuevoRegistro() {
            window.location.href = urls.llamaNuevoRegistro;
        }

        function PaginacionDisenio(columnas) {
            $('.pagination').bootpag({
                total: columnas,
                page: 1,
                maxVisible: 5,
                leaps: true,
                firstLastUse: true,
                first: '←',
                last: '→',
                wrapClass: 'pagination',
                activeClass: 'active',
                disabledClass: 'disabled',
                nextClass: 'next',
                prevClass: 'prev',
                lastClass: 'last',
                firstClass: 'first'
            }).on('page', function (event, num) {
                //getCustomers(num);
                console.log('Cambio de fila');
            });
        }

        function PaginacionFuncionalidad() {
            FuncionesGenerales.AbrirCargando();

            var success = function (r) {
                PaginacionDisenio(r.Page.TotalPages);
                FuncionesGenerales.CerrarCargando();
            };

            me.Servicios.contarLista(registros).then(success, function (e) {
                console.log(e);
                FuncionesGenerales.CerrarCargando();
            });
        }
        return {
            LlamarNuevoRegistro: LlamarNuevoRegistro,
            PaginacionFuncionalidad: PaginacionFuncionalidad
        }
    })();

    me.Funciones = (function () {
        function inicializarEventos() {
            var body = $('body');
            body.on('click', 'button[name=NuevoRegistro]', me.Eventos.LlamarNuevoRegistro);
            FuncionesGenerales.LlamarCalendario(me.Elementos.getFechaDesde());
            FuncionesGenerales.LlamarCalendario(me.Elementos.getFechaHasta());

            var fecha = new Date();
            me.Elementos.getFechaDesde().val(FuncionesGenerales.ConvertirFechaDDMMYYYY(fecha));
            me.Elementos.getFechaHasta().val(FuncionesGenerales.ConvertirFechaDDMMYYYY(fecha));
            //Init
            me.Eventos.PaginacionFuncionalidad();
        }
        return {
            inicializarEventos: inicializarEventos
        }
    })();

    me.Inicializar = function () {
        me.Funciones.inicializarEventos();
    }

    return me;

})(menuData, jQuery);

window.menuModule = menuModule;

$(document).ready(function () {
    menuModule.Inicializar();
});