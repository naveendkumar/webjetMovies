var app = angular.module('app', ['ui.bootstrap', 'ui.router', 'ngRoute'])
    .config(function ($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('movies',
            {
                url: '/movies',
                templateUrl: 'app/views/movies/movies.html',
                controller: 'MoviesCtrl'
            })
            .state('movie',
            {
                url: '/movie',
                params: {
                    movieId: null,
                    movieSource: null
                },
                templateUrl: 'app/views/movies/movie.html',
                controller: 'MovieCtrl'
            })

        $urlRouterProvider.otherwise('/movies');
    });

angular.
    module('exceptionOverwrite', []).
    factory('$exceptionHandler', ['$log', 'logErrorsToBackend', function ($log, logErrorsToBackend) {
        return function myExceptionHandler(exception, cause) {
            logErrorsToBackend(exception, cause);
            $log.warn(exception, cause);
        };
    }]);





