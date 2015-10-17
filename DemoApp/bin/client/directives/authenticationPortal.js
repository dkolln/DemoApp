angular.module('demoApp').directive('authenticationPortal', [
    'toaster',
    'authentication',
    function (toaster, authentication) {
        return {
            restrict: 'E',
            templateUrl: 'directives/authenticationPortal.html',
            link: function (scope, element, attrs) {

                // Expose the authentication.isAuthenticated method on the scope.
                scope.isAuthenticated = authentication.isAuthenticated;

                // Logs the user into the application.
                scope.login = function (userName, password) {
                    // Dismiss the login error (if we have one).
                    scope.dismissLoginError();

                    return authentication.login(userName, password).then(function (result) {
                        // TODO: Something on success?
                        toaster.success('User ' + userName + ' Authenticated');

                    }, function (result) {
                        scope.loginError = result.error_description || result;
                    });
                };

                // Dismisses the login error.
                scope.dismissLoginError = function () {
                    scope.loginError = undefined;
                };
            }
        };
    }
]);