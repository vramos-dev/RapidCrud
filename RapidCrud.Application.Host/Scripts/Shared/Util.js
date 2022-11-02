function Util() {
    var myself = this;
    myself.VERSION = "1.0.0";

    myself.IsNullOrUndefined = function (p) {
        return (p == null || typeof (p) == "undefined");
    };

    myself.IsNullOrEmpty = function (p) {
        return (this.IsNullOrUndefined(p) || p == "");
    };

    myself.IsNullOrWhiteSpace = function (p) {
        return (this.IsNullOrUndefined(p) || p.trim() == "");
    };

    myself.GetUrlDomain = function () {
        var url = "";
        var hdnUrlDomain = document.getElementById("form-hdn-url-domain");
        if (this.IsNullOrUndefined(hdnUrlDomain) || this.IsNullOrWhiteSpace(hdnUrlDomain.value))
            console.error("Domain URL doesn't configured.");
        else
            url = hdnUrlDomain.value;
        return url;
    };

    myself.ShowMessages = function (messages) {
        var toast = {
            success: {
                icon: "success",
                heading: "Success",
                text: []
            },
            info: {
                icon: "info",
                heading: "Information",
                text: []
            },
            warning: {
                icon: "warning",
                heading: "Warning",
                text: []
            },
            error: {
                icon: "error",
                heading: "Error",
                text: []
            }
        };

        if (!myself.IsNullOrUndefined(messages) && messages.length > 0) {
            for (var i = 0; i < messages.length; i++) {
                if (messages[i].Type == "S") {
                    toast.success.text.push(messages[i].Message);
                }
                else if (messages[i].Type == "I") {
                    toast.info.text.push(messages[i].Message);
                }
                else if (messages[i].Type == "W") {
                    toast.warning.text.push(messages[i].Message);
                }
                else if (messages[i].Type == "E") {
                    toast.error.text.push(messages[i].Message);
                }
                else if (messages[i].Type == "X") {
                    console.info("System Error => " + messages[i].Message);
                }
                else if (messages[i].Type == "L") {
                    console.info("System Log => " + messages[i].Message);
                }
                else if (messages[i].Type == "D") {
                    console.info("System Debug => " + messages[i].Message);
                }
            }
        }

        if (toast.success.text.length > 0) {
            $.toast(toast.success);
        }

        if (toast.error.text.length > 0) {
            $.toast(toast.error);
        }

        if (toast.warning.text.length > 0) {
            $.toast(toast.warning);
        }

        if (toast.info.text.length > 0) {
            $.toast(toast.info);
        }
    }

    myself.ShowErrorMessage = function (textStatus, msgError) {
        var messages = [];
        messages.push({
            Type: "E",
            Message: "An error occurred."
        });
        messages.push({
            Type: "X",
            Message: textStatus + " : " + msgError
        });
        myself.ShowMessages(messages);
    }

    myself.ShowInfoMessage = function (message) {
        var messages = [];
        messages.push({
            Type: "I",
            Message: message
        });
        myself.ShowMessages(messages);
    }

    myself.ShowWarningMessage = function (message) {
        var messages = [];
        messages.push({
            Type: "W",
            Message: message
        });
        myself.ShowMessages(messages);
    }

    myself.ValidatorEvent = function (group) {
        $('[data-group=' + group + ']').blur(function (evt) {
            myself.Validator(evt.currentTarget);
        });
    }

    myself.ValidatorGroup = function (group) {
        var eval = true;
        var ctrls = $('[data-group=' + group + ']');
        for (var i = 0; i < ctrls.length; i++) {
            var aux = myself.Validator(ctrls[i]);
            eval = eval && aux;
        }
        return eval;
    }

    myself.Validator = function (ctrl) {
        var eval = true;
        var $ctrl = $(ctrl);
        var validators = ($ctrl.data("validator") || "").split(";");
        for (var i = 0; i < validators.length; i++) {
            var aux = true;
            if (validators[i] == "required") {
                aux = myself.EvalRequired($ctrl);                                    
            }
            else if (validators[i] == "format-code") {
                aux = myself.EvalFormatCode($ctrl);
            }
            else if (validators[i] == "format-index") {
                aux = myself.EvalFormatIndex($ctrl);
            }
            
            if (aux) {
                $ctrl.parent(".form-group").removeClass("has-error")
            }
            else {
                $ctrl.parent(".form-group").addClass("has-error")
            }

            eval = eval && aux;
        }
        
        return eval;
    }

    myself.EvalRequired = function ($ctrl) {
        var eval = true;
        eval = eval && !myself.IsNullOrWhiteSpace($ctrl.val());
        if ($ctrl.is("select")) {
            eval = eval && ($ctrl.val() != "-1");
        }
        return eval;
    }

    myself.EvalFormatCode = function ($ctrl) {
        var eval = true;
        eval = eval && /^[a-zA-Z0-9_]{3,50}$/.test($ctrl.val());
        return eval;
    }

    myself.EvalFormatIndex = function ($ctrl) {
        var eval = true;
        eval = eval && /^[0-9]{1,4}$/.test($ctrl.val());
        return eval;
    }

    myself.LoadingBar = {
        setting: {
            loadingBarContainer: $('<div id="rapid-loading-bar"><div class="bar"><div class="peg"></div></div></div>'),
            incTimeout: null,
            completeTimeout: null,
            started: false,
            status: 0,
            startSize: 0.02
        },
        create: function () {
            $("body").append(myself.LoadingBar.setting.loadingBarContainer);
        },
        move: {
            _start: function () {
                clearTimeout(myself.LoadingBar.setting.completeTimeout);
                if (myself.LoadingBar.setting.started) {
                    return;
                }
                myself.LoadingBar.setting.started = true;
                myself.LoadingBar.move._set(myself.LoadingBar.setting.startSize);
            },
            _set: function (n) {
                if (!myself.LoadingBar.setting.started) {
                    return;
                }
                var pct = (n * 100) + '%';
                myself.LoadingBar.setting.loadingBarContainer.find('div').eq(0).css('width', pct);
                myself.LoadingBar.setting.status = n;
                clearTimeout(myself.LoadingBar.setting.incTimeout);
                myself.LoadingBar.setting.incTimeout = setTimeout(function () {
                    myself.LoadingBar.move._inc();
                }, 250);
            },
            _inc: function () {
                if (myself.LoadingBar.move._status() >= 1) {
                    myself.LoadingBar.setting.loadingBarContainer.find('div.peg').eq(0).removeClass('move');
                    return;
                }
                var rnd = 0;
                var stat = myself.LoadingBar.move._status();
                if (stat >= 0 && stat < 0.25) {
                    // Start out between 3 - 6% increments
                    rnd = (Math.random() * (5 - 3 + 1) + 3) / 100;
                } else if (stat >= 0.25 && stat < 0.65) {
                    // increment between 0 - 3%
                    rnd = (Math.random() * 3) / 100;
                } else if (stat >= 0.65 && stat < 0.9) {
                    // increment between 0 - 2%
                    rnd = (Math.random() * 2) / 100;
                } else if (stat >= 0.9 && stat < 0.99) {
                    // finally, increment it .5 %
                    rnd = 0.005;                    
                } else {
                    // after 99%, don't increment:
                    rnd = 0;
                    myself.LoadingBar.setting.loadingBarContainer.find('div.peg').eq(0).addClass('move');
                }

                var pct = myself.LoadingBar.move._status() + rnd;
                myself.LoadingBar.move._set(pct);
            },
            _status: function () {
                return myself.LoadingBar.setting.status;
            },
            _complete: function () {
                myself.LoadingBar.move._set(1);
                myself.LoadingBar.setting.completeTimeout = setTimeout(function () {
                    myself.LoadingBar.setting.status = 0;
                    myself.LoadingBar.setting.started = false;
                    myself.LoadingBar.remove();
                }, 500);
            }
        },
        remove: function () {
            myself.LoadingBar.setting.loadingBarContainer.remove();
        }
    };

    myself.SyncroScroll = function ($table) {
        var container = $table.parents(".fixed-table-container");
        var body = container.children(".fixed-table-body");
        if (body.length > 0) {
            var div = '<div id="rapid-scroll-top-table-' + $table.attr("id") + '" class="rapid-scroll-top-table"><div style="width:'
                + body[0].scrollWidth + 'px"></div></div>';
            container.parent().append(div);

            $("#rapid-scroll-top-table-" + $table.attr("id")).scroll(function () {
                body.scrollLeft($(this).scrollLeft());
            });

            body.css("overflow-x", "hidden");
        }
    }

    myself.ResizeTableInModal = function ($table, $modal) {
        var container = $table.parents(".fixed-table-container");
        var body = container.children(".fixed-table-body");
        
        var heightWindow = $(window).height();
        var heightHeader = $modal.find(".modal-content .modal-header")[0].offsetHeight || 0;
        var heightFooter = $modal.find(".modal-content .modal-footer")[0].offsetHeight || 0;

        var heightTable = heightWindow - heightHeader - heightFooter - 150;
        var heightCurrent = body[0].scrollHeight + 20;

        if (heightCurrent > heightTable)
            container.css("height", heightTable + "px");
        else {
            if (heightCurrent == 20)
                container.css("height", "150px"); //por default
            else
                container.css("height", heightCurrent + "px");
        }
    }

    myself.ExportTable = function (tableId, type, fileName, sheetName) {
        /* First, call the `TableExport` constructor and save the return instance to a variable */
        var table = TableExport(document.getElementById(tableId));
        var configData = table.getExportData()[tableId][type];
        table.export2file(configData.data, configData.mimeType, fileName || configData.filename, configData.fileExtension, configData.merges, configData.RTL, sheetName || configData.sheetname)
        table.remove(); //eliminando la tabla creada
    }
};

(function () {
    window.Util = new Util();
})();