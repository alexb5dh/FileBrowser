angular.module("fileBrowser")
    .filter("backslash", function() {
        return function (input) {
            if (input == null) return input;
            return String.prototype.replace.call(input, /\//g, "\\");
        };
    });