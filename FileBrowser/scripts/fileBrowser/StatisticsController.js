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