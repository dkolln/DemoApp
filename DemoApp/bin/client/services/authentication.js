angular.module('demoApp').factory('authentication', [
    '$http',
    '$q',
    'localStorageService',
    function ($http, $q, localStorageService) {
        // local variables
        var isAuthenticated = false;
        var userName = "";

        var service = {};

        service.login = function (username, password) {
            var data = 'grant_type=password&username=' + username + '&password=' + password;

            var deferred = $q.defer();
            $http.post('/token', data, {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (response) {
                localStorageService.set('authenticationData', { token: response.access_token, userName: userName });

                isAuthenticated = true;
                userName = userName;

                deferred.resolve(response);

            }).error(function (err, status) {
                service.logOut();

                deferred.reject(err);
            });

            return deferred.promise;

        };

        service.logOut = function () {
            localStorageService.remove('authenticationData');
            isAuthenticated = false;
            userName = "";
        };

        service.fillAuthData = function () {
            var authenticationData = localStorageService.get('authenticationData');
            if (authenticationData) {
                isAuthenticated = true;
                userName = authenticationData.userName;
            }
        };
        
        // Returns whether or not the user is authenticated.
        service.isAuthenticated = function () {
            return isAuthenticated;
        };

        return service;
    }
]);