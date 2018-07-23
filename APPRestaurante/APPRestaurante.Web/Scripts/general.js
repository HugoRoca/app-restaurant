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
    LlamarCalendario: function (val) {
        val.datepicker({
            autoclose: true,
            todayHighlight: true,
            format: "dd/mm/yy"
        });
    }
};
