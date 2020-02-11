app.controller('MovieCtrl', function ($scope, $state, $stateParams, movieService) {
    var movieId = $stateParams.movieId;
    var movieSource = $stateParams.movieSource;
    $scope.loading = true;
    $scope.movie = {};

    $scope.goBack = function() {
        $state.go("movies", { userName: $stateParams.userName });
    };

    function init() {
        movieService.movieDetail(movieId, movieSource).then(function (response) {
            $scope.loading = false;
            var movieDetail = response.data;
            $scope.movie = movieDetail;

        }, function (error) {
            $scope.loading = false;
        });
    }

    init();
});