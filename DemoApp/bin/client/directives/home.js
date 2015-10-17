angular.module('demoApp').directive('home', [
    '$http',
    function ($http) {
        return {
            restrict: 'E',
            templateUrl: 'directives/home.html',
            link: function (scope) {
                scope.selectedTab = 'createADocument';
                scope.document = {};

                $http.get('api/user').then(function (response) {
                    scope.users = response.data;
                });

                function updateDocumentList() {
                    $http.get('api/document').then(function (response) {
                        // This is executed on a successful GET request.
                        scope.documents = response.data;

                        scope.documents.forEach(function (d) {
                            d.createdWhen = new Date(d.createdWhen);
                        });
                    }, function (response) {
                        // TODO: Something on unsuccessful GET request.
                    });
                }

                updateDocumentList();

                scope.addDocument = function () {
                    scope.dismissAddDocumentError();

                    $http.put('api/document', scope.document).then(function () {
                        // This is executed on a successful PUT request.
                        updateDocumentList();
                        // Switch to the 'View All Documents' tab.
                        scope.selectTab('viewAllDocuments');

                    }, function (response) {
                        // This is executed on an unsuccessful PUT request.
                        scope.addDocumentError = response.data;
                    });
                };

                scope.dismissAddDocumentError = function () {
                    scope.addDocumentError = undefined;
                };

                scope.selectTab = function (tabName) {
                    scope.selectedTab = tabName;
                };

                scope.getUserNameFromUserId = function (userId) {
                    var user = _.find(scope.users, { userId: userId });
                    if (user) {
                        return user.userName;
                    }
                };
            }
        };
    }
]);