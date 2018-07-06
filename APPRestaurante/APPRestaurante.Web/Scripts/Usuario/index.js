var loginModule = (function (globalData, $) {

    'use strict';

    var me = {};

    var urls = globalData.urlProvider;

    var localData = {
        //numero: globalData.numero, etc
    }

    me.Elements = (function () {
        function getInputUsuario() { return $('.inputUsuario'); }
        function getInputClave() { return $('.inputClave'); }
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
            body.on('click', '.btn-primary', me.Eventos.ingresar);
            body.on('keydown', '.inputClave', me.Eventos.ingresarEnter)
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

            var successValidate = function (r) {
                if (!r.Success) {
                    me.Funciones.setError(r.Message);
                    return;
                }
                window.location.href = urls.redirectInicio;
                me.Funciones.setError('');
            }

            me.Services.validar(_usuario, _clave).then(successValidate, function (e) {
                me.Funciones.setError(e.Message);
            });
        }
        function ingresarEnter(e) {
            if (e.keyCode === 13 && !e.shiftKey) {
                e.preventDefault();
                ingresar();
            }
        }
        return {
            ingresar: ingresar,
            ingresarEnter: ingresarEnter
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

/*
var actualizarCelularModule = (function (globalData, $) {
    'use strict';

    var me = {};

    var urls = globalData.urlProvider;
    var localData = {
        CelularActual: globalData.celular,
        InicialNumero: globalData.iniciaNumero === -1 ? '' : globalData.iniciaNumero.toString(),
        CelularValido: false,
        CelularNuevo: '',
        Expired: true,
        IsoPais: IsoPais
    };

    me.Elements = (function () {
        function getInputCelular() {
            return $('#NuevoCelular');
        }

        function getIconValidacionSms() {
            return $('.icono_validacion_codigo_sms');
        }

        function getErrorText() {
            return $('.text-error');
        }

        function getInputsCodeSms() {
            return $('.campo_ingreso_codigo_sms');
        }

        function getCelularNuevoText() {
            return $('#NumeroMensajeEnviado');
        }

        return {
            getInputCelular: getInputCelular,
            getIconValidacionSms: getIconValidacionSms,
            getErrorText: getErrorText,
            getCelularNuevoText: getCelularNuevoText,
            getInputsCodeSms: getInputsCodeSms
        };
    })();
    me.Services = (function () {
        function enviarSmsCode(numero) {
            return $.ajax({
                url: urls.enviarSmsCodigo,
                method: 'POST',
                data: {
                    celular: numero
                }
            });
        };

        function confirmarSmsCode(code) {
            return $.ajax({
                url: urls.confirmarSmsCode,
                method: 'POST',
                data: {
                    smsCode: code
                }
            });
        };

        return {
            enviarSmsCode: enviarSmsCode,
            confirmarSmsCode: confirmarSmsCode
        };
    })();
    me.Funciones = (function () {
        var panels = [
            $('.form_actualizar_celular'),
            $('.revisa_tu_celular'),
            $('.celular_actualizado')
        ];
        var interval;
        var counterElement = $('#time_counter');
        var cantMsInterval = 1000;

        function inicializarEventos() {
            var body = $('body');
            body.on('click', '.btn_continuar', me.Eventos.Continuar);
            body.on('click', '.enlace_cambiar_correo', me.Eventos.BackEdiNumber);
            body.on('click', '.enlace_reenviar_instrucciones', me.Eventos.SendSmsCode);
            body.on('keyup', '.campo_ingreso_codigo_sms', me.Eventos.ChangeCodeSms);
            body.on('keydown', '.campo_ingreso_codigo_sms', me.Eventos.OnlyNumberCodeSms);
            body.on('keydown', '#NuevoCelular', me.Eventos.OnlyNumberCodeSms);
            body.on('cut copy paste', '#NuevoCelular', function (e) { e.preventDefault(); });
        };

        function getLengthPais(iso) {
            var paises = {
                'PE': 9,
                'MX': 10,
                'EC': 10,
                'CL': 9,
                'BO': 8,
                'PR': 10,
                'DO': 10,
                'CR': 8,
                'GT': 8,
                'PA': 8,
                'SV': 8
            };

            return paises[iso];
        }

        function validarPhonePais(iso, numero) {
            var length = getLengthPais(iso);
            if (!length) {
                return {
                    valid: false,
                    length: 0
                };
            }

            return {
                valid: numero.length === length,
                length: length
            };
        }

        function validarCelular(numero) {
            if (!numero) {
                return {
                    Success: false,
                    Message: 'El número no puede estar vacío.'
                };
            }

            if (localData.CelularActual === numero) {
                return {
                    Success: false,
                    Message: 'El número no puede ser el mismo.'
                };
            }

            var pattern = /^\d+$/;
            if (!pattern.test(numero)) {
                return {
                    Success: false,
                    Message: 'El número no cumple con el formato.'
                };
            }

            var result = validarPhonePais(localData.IsoPais, numero);
            if (!result.valid) {
                return {
                    Success: false,
                    Message: 'El número debe tener ' + result.length + ' digitos.'
                };
            }

            if (localData.InicialNumero) {
                if (numero.charAt(0) !== localData.InicialNumero) {
                    return {
                        Success: false,
                        Message: 'El número debe empezar con ' + localData.InicialNumero + '.'
                    }
                }
            }

            return { Success: true };
        }

        function setReadOnlySmsCodeInput(disabled) {
            me.Elements.getInputsCodeSms().each(function () {
                $(this).prop('readonly', disabled);
            });
        }

        function getSmsCode() {
            var code = '';
            me.Elements.getInputsCodeSms().each(function () {
                code += $(this).val();
            });

            return code;
        }

        function setSmsCode(value) {
            value = value || '';
            me.Elements.getInputsCodeSms().each(function (idx) {
                var char = value.charAt(idx);
                $(this).val(char);
            });
        }

        function markSmsCodeStatus(valid) {
            var icon = me.Elements.getIconValidacionSms();

            icon.show();
            if (!valid) {
                icon.removeClass('validacion_exitosa')
                    .addClass('validacion_erronea');

                return;
            }

            icon.removeClass('validacion_erronea')
                .addClass('validacion_exitosa');
        }

        function resetSmsCode() {
            me.Funciones.SetSmsCode('');
            me.Elements.getIconValidacionSms().hide();
        }

        function format2(value) {
            if (value < 10) return '0' + value;

            return value;
        }

        function stepCounter(segs) {
            clearInterval(interval);

            localData.Expired = false;
            var now = 0;
            interval = setInterval(function () {

                now += cantMsInterval;
                var distance = segs - now;

                var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                var seconds = Math.floor((distance % (1000 * 60)) / 1000);
                counterElement.text(format2(minutes) + ":" + format2(seconds));

                if (distance < 0) {
                    localData.Expired = true;
                    clearInterval(interval);
                    counterElement.text("00:00");
                    resetSmsCode();
                }
            }, cantMsInterval);
        }

        function counter() {
            counterElement.text('03:00');
            stepCounter(3 * 60000);
        }

        function showError(text) {
            me.Elements.getErrorText().text(text);
        }

        function verifySmsCode(code) {
            if (localData.Expired) {
                return;
            }

            AbrirLoad();
            var successConfirmarSmsCode = function (r) {
                CerrarLoad();
                if (!r.Success) {
                    me.Funciones.MarkSmsCodeStatus(false);

                    return;
                }

                setReadOnlySmsCodeInput(true);
                me.Funciones.MarkSmsCodeStatus(true);
                setTimeout(function () {
                    me.Funciones.NavigatePanel(2);
                },
                    1000);
            };

            me.Services.confirmarSmsCode(code)
                .then(successConfirmarSmsCode, function (er) {
                    CerrarLoad();
                    me.Funciones.HandleError(er);
                });
        }

        function navigatePanel(index) {
            panels.forEach(function (item, idx) {
                if (idx === index) {
                    item.show();
                } else {
                    item.hide();
                }
            });
        }

        function setIsoPais(iso) {
            localData.IsoPais = iso;

            var len = getLengthPais(iso);
            if (len) {
                me.Elements.getInputCelular().prop('maxLength', len);
            }
        }

        function handleError(er) {
            alert('Ocurrio un error inesperado.');
        }

        return {
            InicializarEventos: inicializarEventos,
            ValidarCelular: validarCelular,
            GetSmsCode: getSmsCode,
            MarkSmsCodeStatus: markSmsCodeStatus,
            InitCounter: counter,
            VerifySmsCode: verifySmsCode,
            ShowError: showError,
            NavigatePanel: navigatePanel,
            SetIsoPais: setIsoPais,
            HandleError: handleError,
            SetSmsCode: setSmsCode,
            ResetSmsCode: resetSmsCode
        };

    })();
    me.Eventos = (function () {
        function continuar() {
            var nuevoCelular = me.Elements.getInputCelular().val();

            var result = me.Funciones.ValidarCelular(nuevoCelular);
            if (!result.Success) {
                me.Funciones.ShowError(result.Message);
                return;
            }

            localData.CelularNuevo = nuevoCelular;
            me.Funciones.ResetSmsCode();
            AbrirLoad();

            var successEnviarSmsCode = function (r) {
                $('#celularNuevo').text(nuevoCelular);
                CerrarLoad();
                if (!r.Success) {
                    me.Funciones.ShowError(r.Message);
                    return;
                }
                me.Elements.getCelularNuevoText().text(nuevoCelular);
                me.Funciones.NavigatePanel(1);
                me.Funciones.ShowError('');
                me.Funciones.InitCounter();
            };

            me.Services.enviarSmsCode(nuevoCelular)
                .then(successEnviarSmsCode, function (er) {
                    CerrarLoad();
                    me.Funciones.HandleError(er);
                });
        }

        function backEdiNumber() {
            me.Funciones.NavigatePanel(0);
        }

        function sendSmsCode() {
            me.Funciones.ResetSmsCode();
            AbrirLoad();
            me.Services.enviarSmsCode(localData.CelularNuevo)
                .then(function (r) {
                    CerrarLoad();
                    if (!r.Success) {
                        me.Funciones.ShowError(r.Message);
                        me.Funciones.NavigatePanel(0);

                        return;
                    }

                    me.Funciones.InitCounter();
                }, function (er) {
                    CerrarLoad();
                    me.Funciones.HandleError(er);
                });
        }

        function changeCodeSms(e) {
            var input = $(this);
            if (input.val()) {
                input.parent().next().find('.campo_ingreso_codigo_sms').focus();
            }

            if (e.keyCode === 8) {
                input.parent().prev().find('.campo_ingreso_codigo_sms').focus();
            }

            var code = me.Funciones.GetSmsCode();
            if (code.length !== 6) {
                return;
            }

            me.Funciones.VerifySmsCode(code);
        }

        function onlyNumberCodeSms(e) {
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                return;
            }
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        }

        return {
            Continuar: continuar,
            BackEdiNumber: backEdiNumber,
            SendSmsCode: sendSmsCode,
            ChangeCodeSms: changeCodeSms,
            OnlyNumberCodeSms: onlyNumberCodeSms
        };
    })();
    me.Inicializar = function () {
        me.Funciones.InicializarEventos();
    };

    return me;
})(actualizaCelularData, jQuery);

window.actualizarCelularModule = actualizarCelularModule;

$(document).ready(function () {
    actualizarCelularModule.Funciones.SetIsoPais(IsoPais);
    actualizarCelularModule.Inicializar();
});*/