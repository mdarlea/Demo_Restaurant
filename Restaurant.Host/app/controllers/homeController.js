﻿(function () {
    'use strict';
    angular.module('app').controller('homeController',
        ['$scope', '$location', '$authService', function ($scope, $location, $authService) {
            $scope.isAuth = $authService.authentication.isAuth;

            if (!$authService.authentication.isAuth) {
                $location.path('/login');
            }
        }]);
})();