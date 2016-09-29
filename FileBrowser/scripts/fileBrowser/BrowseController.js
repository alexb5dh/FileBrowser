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