// Todo: use array with parameter names
angular.module("fileBrowser", ["ngRoute"])
    .constant("routes", {
        browse: "/api/browse",
        download: "/api/download",
        statistics: "api/statistics"
    });

// 'Route out of sync' fix
// see: https://github.com/angular/angular.js/issues/2100
angular.module("fileBrowser")
    .run(function($rootScope, $window, $location) {
        $rootScope.$on("$routeChangeError", function(event, current, previous) {
            if (previous) $window.history.back();
            else $location.path("/").replace();
        });
    });
