angular.module('demoApp').factory('authenticationInterceptor', [
    '$q',
    '$location',
    'localStorageService',
    function ($q, $location, localStorageService) {
        var service = {};

        service.request = function (config) {

            config.headers = config.headers || {};

            var authenticationData = localStorageService.get('authenticationData');
            if (authenticationData) {
                config.headers.Authorization = 'Bearer ' + authenticationData.token;
            }

            return config;
        }

        service.responseError = function (rejection) {
            if (rejection.status === 401) {
                $location.path('/');
            }
            return $q.reject(rejection);
        }

        return service;
    }
]);