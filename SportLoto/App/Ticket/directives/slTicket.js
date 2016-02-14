angular.module('ticketApp').directive('slTicket',
[
    'ticketFactory',
    function (ticketFactory) {
        var MAX_CELL_IN_SECTION = ticketFactory.MAX_CELL_IN_SECTION;

        return {
            templateUrl: '/App/Ticket/directives/slTicket.html',
            restrict: 'E',
            scope: {
                ticket: '=ticket'
            },
            link: function (scope, elem, attrs) {
                scope.toogleSelect = function (cell, section) {
                    var selectedCells = _.filter(section, function (val) { return val.isSelect});
                    if (selectedCells.length < MAX_CELL_IN_SECTION) {
                        cell.isSelect = cell.isSelect ? false : true;
                    }
                    else if (cell.isSelect) {
                        cell.isSelect = false;
                    }
                }
            }
        }
    }
]);
