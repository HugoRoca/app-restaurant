FuncionesGenerales = {
    AbrirCargando: function () {
        try {
            if (!$("#loadingScreen")) {
                $(document.body).append('<div id="loadingScreen"></div>');
            }
            else if ($("#loadingScreen").length == 0) {
                $(document.body).append('<div id="loadingScreen"></div>');
            }

            if ($(".loading").length == 0) {
                $("#loadingScreen").append("<div class='loading'></div>");
                $(".loading").append("<div class='loading-titulo'>C a r g a n d o</div>");
                $(".loading").append("<div class='loading-mensaje'>Espere, por favor...</div>");
            }

            $('#loadingScreen').show();
        }
        catch (err) {
            console.log(err);
        }
    },
    CerrarCargando: function () {
        $('#loadingScreen').hide();
    },
    AbrirMensaje: function (Mensaje) {
        try {
            if (!$("#MensajeScreen")) {
                $(document.body).append('<div id="MensajeScreen"></div>');
            }
            else if ($("#MensajeScreen").length == 0) {
                $(document.body).append('<div id="MensajeScreen"></div>');
            }

            if ($(".mensaje").length == 0) {
                $("#MensajeScreen").append("<div class='mensaje'></div>");
                $(".mensaje").append("<div class='mensaje-titulo'>Mensaje</div>");
                $(".mensaje").append("<div class='mensaje-descripcion'>" + Mensaje + "</div>");
                $(".mensaje").append("<div class='mensaje-footer'><button class='btn btn-primary'>OK</button></div>");
            }

            $('#MensajeScreen').show();
        } catch (err) {
            console.log(err);
        }
    },
    LlamarCalendario: function (val) {
        val.datepicker({
            autoclose: true,
            todayHighlight: true,
            format: "dd/mm/yyyy"
        });
    },
    ConvertirFechaDDMMYYYY: function (fechaTime) {
        if (fechaTime == "0001-01-01T00:00:00") {
            return null;
        } else {
            var date = new Date(fechaTime);
            var Year = date.getFullYear();
            var Month = (1 + date.getMonth()).toString();
            Month = Month.length > 1 ? Month : '0' + Month;
            var Day = date.getDate().toString();
            Day = Day.length > 1 ? Day : '0' + Day;

            return Day + "/" + Month + "/" + Year;
        }
    }
};
