var loginModule = (function (globalData, $) {

    'use strict';

    var me = {};

    var urls = globalData.urlProvider;

    var localData = {
        //numero: globalData.numero, etc
    }

    me.Elements = (function () {
        function getInputUsuario() { return $('#txtUsuario'); }
        function getInputClave() { return $('#txtClave'); }
        function setErrorSpan() { return $('#spanError'); }
        return {
            getInputUsuario: getInputUsuario,
            getInputClave: getInputClave,
            setErrorSpan: setErrorSpan
        }
    })();
    me.Services = (function () {
        function validar(_usuario, _clave) {
            return $.ajax({
                url: urls.validarUsuario,
                method: 'POST',
                data: {
                    usuario: _usuario,
                    clave: _clave
                }
            });
        }
        return {
            validar: validar
        }
    })();
    me.Funciones = (function () {
        function inicializarEventos() {
            var body = $('body');
            body.on('click', '#btnEntrar', me.Eventos.ingresar);
        }
        function setError(text) {
            me.Elements.setErrorSpan().text(text);
        }
        return {
            inicializarEventos: inicializarEventos,
            setError: setError
        }
    })();
    me.Eventos = (function () {
        function ingresar() {
            var _usuario = me.Elements.getInputUsuario().val();
            var _clave = me.Elements.getInputClave().val();

            if (_usuario == '' || _clave == '') {
                me.Funciones.setError('Debe de ingresar los datos correctamente.');
                return;
            }
            alert('bienvenido');
        }
        return {
            ingresar: ingresar
        }        
    })();
    me.Inicializar = function () {
        me.Funciones.inicializarEventos();
    }
    return me;

})(loginData, jQuery);

window.loginModule = loginModule;

$(document).ready(function () {
    loginModule.Inicializar();
});