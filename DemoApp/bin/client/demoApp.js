angular.module('demoApp', [
    'LocalStorageModule'
]).config([
    '$httpProvider',
    function ($httpProvider) {
        $httpProvider.interceptors.push('authenticationInterceptor');
    }
]).run([
    'authentication',
    function (authentication) {
        // This logs the user in when the application is loading if thier token is inside
        // the browser's localstorage
        authentication.fillAuthData();
    }
]);