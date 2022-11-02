function Agenda() {

    var myself = this;
    myself.IsEditable = false;
    myself.CurrentAgenda = {};
    myself.$table = $("#rapid-table-agenda");

    myself.Init = function () {
        myself.$table.bootstrapTable();
        myself.SetEvent();
        myself.LoadTable();
        Util.ValidatorEvent("agenda");
    };

    myself.SetEvent = function () {
        
        $("#rapid-btn-add-agenda").on("click", function () {
            myself.ResetForm();
            $("#rapid-div-list-agenda").hide();
            $("#rapid-div-addedit-agenda").show();
        });

        myself.$table.on("click", "span[id^='rapid-span-edit-agenda-']", function (evt) {
            myself.IsEditable = true;
            myself.CurrentAgenda = myself.$table.bootstrapTable("getRowByUniqueId", $(evt.target).data("id"));
            $("#rapid-div-list-agenda").hide();
            $("#rapid-div-addedit-agenda").show();
            myself.SetData();
        });

        myself.$table.on("click", "span[id^='rapid-span-delete-agenda-']", function (evt) {
            if (!$(evt.currentTarget).hasClass("disabled")) {
                $("#rapid-span-text-confirm").text("Are you sure to delete the client " + $(evt.target).data("id") + " ?");
                $("#rapid-btn-delete-confirm").unbind('click').click(function () {
                    myself.DeleteData($(evt.target).data("id"), evt);
                    $("#rapid-model-delete-confirm").modal('hide');
                });
                $("#rapid-model-delete-confirm").modal('show');
            }
        });

        $("#rapid-btn-save-agenda").on("click", function () {
            if (Util.ValidatorGroup("agenda")) {
                myself.SaveData();
            }
            else {
                Util.ShowInfoMessage("There are incomplete required fields.");
            }
        });

        $("#rapid-btn-cancel-agenda").on("click", function () {
            $("#rapid-div-list-agenda").show();
            $("#rapid-div-addedit-agenda").hide();
            myself.ResetForm();
        });
    }

    myself.LoadTable = function () {
        var url = Util.GetUrlDomain() + "/api/cliente";

        $.get(url)
            .done(function (resp) {
                myself.$table.bootstrapTable('load', resp.Data || {});
                if (!resp.Success) {
                    Util.ShowMessages(resp.Messages);
                }
            }).fail(function (xhr, textStatus, msgError) {
                Util.ShowErrorMessage(textStatus, msgError);
            });
    };

    myself.fnToolsFormatter = function (value, row, index, field) {
        var html = '<span id="rapid-span-edit-agenda-' + field + '-' + index + '" data-id="' + row.Id + '"';
        html += ' class="glyphicon glyphicon-pencil text-primary cursor-pointer" aria-hidden="true"></span>';
        html += '&nbsp;&nbsp;&nbsp;';
        html += '<span id="rapid-span-delete-agenda-' + field + '-' + index + '" data-id="' + row.Id + '"';
        html += ' class="glyphicon glyphicon-trash text-danger cursor-pointer" aria-hidden="true"></span> '
        return html;
    }

    myself.SetData = function () {
        if (myself.IsEditable) {
            $("#rapid-agenda-dni").val(myself.CurrentAgenda.Dni);
            $("#rapid-agenda-dni").attr("disabled", true);
            $("#rapid-agenda-nombre").val(myself.CurrentAgenda.Nombre);
            $("#rapid-agenda-apellido").val(myself.CurrentAgenda.Apellido);
            $("#rapid-agenda-telefono").val(myself.CurrentAgenda.Telefono);
            $("#rapid-agenda-email").val(myself.CurrentAgenda.Email);
        }
        else {
            $("#rapid-agenda-dni").val("");
            $("#rapid-agenda-dni").attr("disabled", false);
            $("#rapid-agenda-nombre").val("");
            $("#rapid-agenda-apellido").val("")
            $("#rapid-agenda-telefono").val("")
            $("#rapid-agenda-email").val("");
        }
    }

    myself.GetData = function () {
        var data = {
            Dni: $("#rapid-agenda-dni").val(),
            Nombre: $("#rapid-agenda-nombre").val(),
            Apellido: $("#rapid-agenda-apellido").val(),
            Telefono: $("#rapid-agenda-telefono").val(),
            Email: $("#rapid-agenda-email").val()
        };

        return data;
    }

    myself.SaveData = function () {
        var data = myself.GetData();
        var url = Util.GetUrlDomain() + "/api/cliente";
        var method = "POST";
        if (myself.IsEditable) {
            url += "/" + myself.CurrentAgenda.Id;
            method = "PUT";
        }

        $("#rapid-btn-save-agenda").prop('disabled', true);
        var request = $.ajax({
            url: url,
            method: method,
            data: JSON.stringify(data),
            contentType: 'application/json',
        });

        request.done(function (resp) {
            Util.ShowMessages(resp.Messages);
            if (resp.Success) {
                myself.LoadTable();
                myself.IsEditable = true;
                myself.CurrentAgenda = data;
                myself.SetData();
                $("#rapid-div-list-agenda").show();
                $("#rapid-div-addedit-agenda").hide();
            }
        });

        request.fail(function (xhr, textStatus, msgError) {
            Util.ShowErrorMessage(textStatus, msgError);
        });

        request.always(function () {
            $("#rapid-btn-save-agenda").prop('disabled', false);
        });
    }

    myself.ResetForm = function () {
        myself.IsEditable = false;
        myself.CurrentAgenda = {};
        myself.SetData();
    }

    myself.DeleteData = function (id, evt) {

        var url = Util.GetUrlDomain() + "/api/cliente/" + id;

        $(evt.currentTarget).addClass('disabled');

        var request = $.ajax({
            url: url,
            method: 'DELETE'
        });

        request.done(function (resp) {
            Util.ShowMessages(resp.Messages);
            if (resp.Success) {
                myself.LoadTable();
            }
        });

        request.fail(function (xhr, textStatus, msgError) {
            Util.ShowErrorMessage(textStatus, msgError);
        });

        request.always(function () {
            $(evt.currentTarget).removeClass('disabled');
        });
    }
};


(function () {
    window.Agenda = new Agenda();
})();