app.factory('movieService', function ($http) {
    var serviceBase = '/api';

    var populateAuthTokenToHeader = function() {
        var token = localStorage.token;

        if (token) {
            $http.defaults.headers.common.Authorization = token;
        }
    };

    var movieList = function() {
        populateAuthTokenToHeader();

        return $http.get(`${serviceBase}/Movie/movies`).then(function(response) {
            return response.data;
        });
    };

    var movieDetail = function(id, source) {
        populateAuthTokenToHeader();

        return $http.get(`${serviceBase}/Movie/movies/${source}/${id}`).then(function(response) {
            return response.data;
        });
    };

    return {
        movieList: movieList,
        movieDetail: movieDetail
    };
});

