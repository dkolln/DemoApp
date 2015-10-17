angular.module('demoApp').directive('navigation', [
    'authentication',
    function (authentication) {
        return {
            restrict: 'E',
            templateUrl: 'directives/navigation.html',
            link: function (scope) {

                // Expose the authentication.logOut method on the scope.
                scope.logOut = authentication.logOut;
            }
        };
    }
]);