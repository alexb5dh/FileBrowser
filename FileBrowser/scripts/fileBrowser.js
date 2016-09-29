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

angular.module("fileBrowser")
    .filter("backslash", function() {
        return function (input) {
            if (input == null) return input;
            return String.prototype.replace.call(input, /\//g, "\\");
        };
    });
(function() {

    function BrowseController($scope, $routeParams, routes, content) {
        $scope.path = $routeParams.path || "";
        $scope.folders = content.folders;
        $scope.files = content.files;

        // Todo: refactor file downloading
        $scope.getDownloadUrl = function(file) {
            return routes.download + "?path=" + file.path;
        }
    };

    BrowseController.resolve = {
        content: function($q, $route, $http, $log, routes) {
            var deferred = $q.defer();
            var path = $route.current.params.path || "";

            $http.get(routes.browse, { params: { path: path } })
                .then(
                    function(response) { deferred.resolve(response.data); },
                    function(response) { deferred.reject(response); }
                );

            return deferred.promise;
        }
    };

    angular.module("fileBrowser")
        .controller("BrowseController", BrowseController)
        .config(function($routeProvider) {
            $routeProvider
                .when("/:path*?", {
                    templateUrl: "templates/browse.html",
                    controller: BrowseController,
                    resolve: BrowseController.resolve
                });
        });

})();
(function () {

function StatisticsController($scope, $http, $q, routes) {
    $scope.statistics = { $loading: true };
    var httpCanceler = $q.defer();
    $http.get(routes.statistics, {
        params: { path: $scope.path },
        timeout: httpCanceler.promise
    })
        .then(function (response) {
            $scope.statistics = response.data;
            $scope.statistics.$success = true;
        }, function (response) {
            $scope.statistics = {};
            $scope.statistics.$error = true;
        });

    // Request is computationally heavy - cancel if not needed
    $scope.$on("$destroy", function() { httpCanceler.resolve(); });
}

angular.module("fileBrowser")
    .controller("StatisticsController", StatisticsController);

})();