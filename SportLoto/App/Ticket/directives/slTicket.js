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
                scope.toggleSelect = function (cell, section) {
                    var selectedCells = _.filter(section, function (val) { return val.isSelect});
                    if (selectedCells.length < MAX_CELL_IN_SECTION) {
                        cell.isSelect = cell.isSelect ? false : true;
                    }

                    else if (cell.isSelect) {
                        cell.isSelect = false;
                    }
                };

                scope.setRandomCells = function(section) {
                    scope.resetSection(section);
                    _.forEach(_.slice(_.shuffle(section), 0, MAX_CELL_IN_SECTION), function(cell) {
                        cell.isSelect = true;
                    })

                };

                scope.resetSection = function(section) {
                    _.forEach(section, function(cell) {
                        cell.isSelect = false;
                    })
                };
            }
        }
    }
]);
