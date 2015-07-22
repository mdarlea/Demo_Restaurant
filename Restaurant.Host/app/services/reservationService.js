﻿(function () {
    'use strict';

    angular.module('appRestaurant').factory('$reservationService',
        ['$resource', 'swAppSettings',
        function ($resource, swAppSettings) {
            return $resource(
                swAppSettings.apiServiceBaseUri + 'api/reservation/:id',
                {
                     id: '@id'
                },
                {
                    'addNewReservation': {
                        method: 'POST',
                        isArray: false
                    },
                    'getAllReservations' : {
                        method: 'GET',
                        isArray: true
                    },
                    'getReservation' : {
                        method: 'GET',
                        isArray: false
                    }
                });
        }]);
})();