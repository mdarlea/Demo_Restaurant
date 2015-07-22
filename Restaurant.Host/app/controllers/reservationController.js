﻿(function () {
    'use strict';

    /**
    * 
    * @ngdoc object
    * @name appRestaurant.reservationController
    * @requires $scope
    * @requires $location
    * @requires $authService

    * @description Reservation controller
    */

    angular.module('appRestaurant').controller('reservationController',
        ['$scope', '$location', '$authService', '$reservationService','$utilities',
        function ($scope, $location, $authService, $reservationService, $utilities) {

            //redirect to home page if not authorized
            if (!$authService.authentication.isAuth) {
                $location.path('/login');
            };

            $scope.data = {
                reservationTime: $utilities.roundTime(new Date(new Date().getTime()),30)
            };

            $scope.numberExpr = /^[1-9][0-9]*$/;

            // function to submit the reservation after all validation has occurred            
            $scope.submitReservation = function (isValid) {

                // check to make sure the form is completely valid
                if (!isValid) {
                    $scope.message = "Invalid form data";
                    return false;
                }

                $scope.message = null;
                $scope.loading = true;
                $reservationService.addNewReservation($scope.data).$promise
                    .then(function (response) {
                        
                    }, function (err) {
                        $scope.message = err.data && err.data.message;
                    })
                    .finally(function (response) {
                        $scope.loading = false;
                    });
            };
        }]);
})();