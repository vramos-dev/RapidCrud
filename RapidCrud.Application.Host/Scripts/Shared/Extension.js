(function () {

    if (!String.prototype.trim) {
        // Make sure we trim BOM and NBSP
        var rtrim = /^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g;
        String.prototype.trim = function () {
            return this.replace(rtrim, '');
        };
    }

    if (!String.prototype.isNullOrEmtpy) {
        String.prototype.isNullOrEmtpy = function () {
            return (this == "undefined" || this == "");
        };
    }

    if (!String.prototype.isNullOrWhiteSpace) {
        String.prototype.isNullOrWhiteSpace = function () {
            return (this == "undefined" || this.trim() == "");
        };
    }

})();