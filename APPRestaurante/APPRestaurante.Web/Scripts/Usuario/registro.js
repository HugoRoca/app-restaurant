var accesoRegistroModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;

    me.Elementos = (function () {
        function getId() { return $('input[name=id]');}
        function getUsuario() { return $('input[name=NombreUsuario]'); }
        function getEmpleado() { return $('select[name=Empleado]'); }
        function getRol() { return $('select[name=Rol]'); }
        function getPassword() { return $('input[name=Contrasenia]'); }
        function getPasswordRepit() { return $('input[name=RepetirContrasenia]'); }

        return {
            getId: getId,
            getUsuario: getUsuario,
            getEmpleado: getEmpleado,
            getRol: getRol,
            getPassword: getPassword,
            getPasswordRepit: getPasswordRepit
        }
    })();

    me.Servicios = (function () {
        function registrar(_data) {
            return $.ajax({
                url: urls.urlRegistro,
                type: 'POST',
                data: {
                    usuarioModel: _data
                }
            });
        }

        return {
            registrar: registrar
        }
    })();

    me.Eventos = (function () {
        function insertarDatos() {

            var successRegistro = function (r) {
                if (!r.Success) {
                    FuncionesGenerales.AbrirMensaje(r.Message);
                } else {
                    window.location.href = urls.urlPaginaLista;
                }
            };

            var pass1 = me.Elementos.getPassword().val();
            var pass2 = me.Elementos.getPasswordRepit().val();

            if (pass1 != pass2) {
                FuncionesGenerales.AbrirMensaje("Las contraseñas no coinciden, por favor corregir");
                return false;
            }

            var usuario = {
                idEmpleado: me.Elementos.getEmpleado().val(),
                clave: me.Elementos.getPassword().val(),
                idRol: me.Elementos.getRol().val(),
                usuario: me.Elementos.getUsuario().val()
            }

            me.Servicios.registrar(usuario).then(successRegistro, function (e) {
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

})(accesoRegistroData, jQuery);

window.accesoRegistroModule = accesoRegistroModule;

$(document).ready(function () {
    accesoRegistroModule.InicializarEventos();
});