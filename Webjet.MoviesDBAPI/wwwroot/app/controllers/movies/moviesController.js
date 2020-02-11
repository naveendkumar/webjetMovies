app.controller('MoviesCtrl', function ($q, $scope, $state, $stateParams, movieService) {
    $scope.movies = {};
    $scope.loading = true;

    function findCheapMovie() {
        //loop movies by title
        debugger // eslint-disable-line
        angular.forEach($scope.movies,
            function (movieData, key) {
                var cheapMovie = null;
                //loop movies in the same title
                angular.forEach(movieData,
                    function (movie, key) {
                        if (!(movie && movie.price > 0))
                            return;

                        if (!cheapMovie ) {
                            cheapMovie = movie;
                            return;
                        }

                        if ((cheapMovie.price > movie.price))
                            cheapMovie = movie;
                    });

                if(cheapMovie)
                    cheapMovie.isCheapMovie = true;
            });
    }

    function init() {
       
        $scope.userName = $stateParams.userName;
        movieService.movieList()
            .then(function (response) {
                $scope.loading = false;
                $scope.movies = response.data.sources;
                findCheapMovie();
            }, function (error) {
            });
    }

    init();

    $scope.loadMovieInfo = function (id, source) {
        $state.go("movie", { movieId: id, movieSource: source});
    };
});