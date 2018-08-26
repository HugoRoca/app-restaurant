var menuModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;

    me.Elementos = (function () {
        function getFechaDesde() { return $('.Desde'); }
        function getFechaHasta() { return $('.Hasta'); }
        function getTotalFilas() { return $('select[name=totalRegistros]'); }
        return {
            getFechaDesde: getFechaDesde,
            getFechaHasta: getFechaHasta,
            getTotalFilas: getTotalFilas
        }
    })();

    me.Servicios = (function () {
        function ContarLista(_rows) {
            var _desde = FuncionesGenerales.ConvertirFechaYYYYMMDD(me.Elementos.getFechaDesde().val());
            var _hasta = FuncionesGenerales.ConvertirFechaYYYYMMDD(me.Elementos.getFechaHasta().val());
            return $.ajax({
                url: urls.urlContador,
                method: 'POST',
                data: {
                    rows: _rows,
                    desde: _desde,
                    hasta: _hasta
                }
            });
        }

        function Lista(_pagina) {
            var _desde = me.Elementos.getFechaDesde().val();
            var _hasta = me.Elementos.getFechaHasta().val();

            return $.ajax({
                url: urls.urlBuscar,
                method: 'POST',
                data: {
                    desde: _desde,
                    hasta: _hasta,
                    pagina: _pagina,
                    fila: me.Elementos.getTotalFilas().val()
                }
            });
        }

        function Eliminar(_id) {
            return $.ajax({
                url: urls.urlEliminar,
                method: 'POST',
                data: {
                    id: _id
                }
            });
        }

        return {
            ContarLista: ContarLista,
            Lista: Lista,
            Eliminar: Eliminar
        }
    })();

    me.Eventos = (function () {
        function LlamarNuevoRegistro() {
            window.location.href = urls.llamaNuevoRegistro;
        }

        function LlenarTabla(_pagina) {
            FuncionesGenerales.AbrirCargando();

            var successLista = function (r) {
                var tabla = '';
                for (var i = 0; i < r.length; i++) {
                    tabla += '<tr>';
                    tabla += '<td>' +
                        '<a class="btn btn-success btn-xs" href="' + urls.llamaNuevoRegistro + '/' + r[i].idDetalle + '"> <i class="fa fa-edit"></i></a>' +
                        '<a class="btn btn-danger btn-xs" href="javascript:;" onclick="menuModule.Eliminar(' + r[i].idDetalle + ');"><i class="fa fa-trash"></i></a>' +
                        '</td>';
                    tabla += '<td>' + r[i].fecha + '</td>';
                    tabla += '<td>' + r[i].titulo + '</td>';
                    tabla += '<td>' + r[i].descripcion + '</td>';
                    tabla += '<td>' + r[i].tipo + '</td>';
                    tabla += '<td class="text-right"> s/. ' + r[i].precio + '</td>';
                    tabla += '<td class="text-center"><img src="../../Uploads/Menu/' + r[i].foto + '" width="100"></td>';
                    tabla += '</tr > ';
                }

                if (tabla != '') {
                    $('#ListaMenuTbody').html(tabla);
                }
                FuncionesGenerales.CerrarCargando();
            }

            me.Servicios.Lista(_pagina).then(successLista, function (e) {
                console.log(e);
                FuncionesGenerales.CerrarCargando();
            });
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
                LlenarTabla(num);
            });
        }

        function PaginacionFuncionalidad() {
            FuncionesGenerales.AbrirCargando();

            var success = function (r) {
                PaginacionDisenio(r.Page.TotalPages);
                FuncionesGenerales.CerrarCargando();
            };

            me.Servicios.ContarLista(me.Elementos.getTotalFilas().val()).then(success, function (e) {
                console.log(e);
                FuncionesGenerales.CerrarCargando();
            });
        }

        function Buscar() {
            if (me.Elementos.getFechaDesde().val() == "" || me.Elementos.getFechaHasta().val() == "") {
                FuncionesGenerales.CerrarCargando();
                FuncionesGenerales.AbrirMensaje('Debe de completar las fechas.');
                return false;
            }
            PaginacionFuncionalidad();
            LlenarTabla(1);
        }

        function EliminaRegistro(_id) {
            var successEliminar = function (r) {
                FuncionesGenerales.CerrarCargando();
                Buscar();
            }

            bootbox.confirm("¿Está seguro de eliminar el registro?", function (result) {
                if (result) {
                    FuncionesGenerales.AbrirCargando();
                    me.Servicios.Eliminar(_id).then(successEliminar, function (e) {
                        FuncionesGenerales.CerrarCargando();
                        console.log(e);
                    });
                }
            });
        }

        return {
            LlamarNuevoRegistro: LlamarNuevoRegistro,
            PaginacionFuncionalidad: PaginacionFuncionalidad,
            LlenarTabla: LlenarTabla,
            Buscar: Buscar,
            EliminaRegistro: EliminaRegistro
        }
    })();

    me.Funciones = (function () {
        function inicializarEventos() {
            var body = $('body');
            body.on('click', 'button[name=NuevoRegistro]', me.Eventos.LlamarNuevoRegistro);
            body.on('click', 'button[name=Buscar]', me.Eventos.Buscar);
            body.on('change', 'select[name=totalRegistros]', me.Eventos.Buscar);
            FuncionesGenerales.LlamarCalendario(me.Elementos.getFechaDesde());
            FuncionesGenerales.LlamarCalendario(me.Elementos.getFechaHasta());

            var fecha = new Date();
            me.Elementos.getFechaDesde().val(FuncionesGenerales.ConvertirFechaDDMMYYYY(fecha));
            me.Elementos.getFechaHasta().val(FuncionesGenerales.ConvertirFechaDDMMYYYY(fecha));

            me.Eventos.Buscar();
        }

        return {
            inicializarEventos: inicializarEventos
        }
    })();

    me.Inicializar = function () {
        me.Funciones.inicializarEventos();
    }

    me.Eliminar = function (_id) {
        me.Eventos.EliminaRegistro(_id);
    }

    return me;

})(menuData, jQuery);

window.menuModule = menuModule;

$(document).ready(function () {
    menuModule.Inicializar();
});