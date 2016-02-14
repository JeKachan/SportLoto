angular.module('ticketApp').factory('ticketFactory', function () {
    var service = {};

    service.MAX_CELL_IN_SECTION = 6;
    service.SECTION_COUNT = 6;

    service.generateTicket = function () {
        var generateTicketSection = function () {
            return _.map(_.range(46), function (value) {
                return { value: value, isSelect: false }
            });
        };

        return _.map(_.range(6), function (v) {
            return generateTicketSection()
        });
    };

    return service;
});