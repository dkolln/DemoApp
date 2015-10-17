angular.module('demoApp').factory('toaster', [
    function () {
        $.toaster({
            settings: {
                toaster: {
                    css: {
                        top: null,
                        bottom: '10px',
                        left: '30px',
                    }
                },
                timeout: 3500
            }
        });

        var service = {};

        [
            'success',
            'info',
            'warning',
            'danger'
        ].forEach(function (priority) {
            service[priority] = function (message, title) {
                $.toaster({ message: message, title: title || '', priority: priority });
            };
        });

        return service;
    }
]);