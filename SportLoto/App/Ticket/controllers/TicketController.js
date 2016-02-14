angular.module('ticketApp').controller("TicketController",
    ['$scope', '$http' ,'ticketFactory',
    function ($scope, $http, ticketFactory) {
        $scope.ticket = ticketFactory.generateTicket();
        var MAX_CELL_IN_SECTION = ticketFactory.MAX_CELL_IN_SECTION;
        var SECTION_COUNT = ticketFactory.SECTION_COUNT;

        //generate random ticket select
        $scope.randomTicket = function () {
            var ticket = $scope.ticket;
            $scope.resetTicketSelect();

            //generate new selected cells in ech section
            _.forEach(ticket, function (section) {
                _.forEach(_.slice(_.shuffle(section), 0, MAX_CELL_IN_SECTION), function (cell) {
                    cell.isSelect = true;
                })
            });
        }

        $scope.resetTicketSelect = function () {
            var ticket = $scope.ticket;
            //reset all selected cells in sections
            _.forEach(ticket, function (section) {
                _.forEach(section, function (cell) {
                    cell.isSelect = false;
                });
            });
        }

        //check ticket valid
        $scope.ticketIsValid = function () {
            var fullSection = _.filter($scope.ticket, function(section) {
                return _.filter(section, function (cell) {
                    return cell.isSelect;
                }).length === MAX_CELL_IN_SECTION
            });

            return fullSection.length === SECTION_COUNT;
        };

        $scope.applyTicket = function () {
            if ($scope.ticketIsValid()) {
                var ticket = _.map($scope.ticket, function (section) {
                    var selectedCells = _.filter(section, function (cell) {
                        return cell.isSelect;
                    })

                    return _.map(selectedCells, function (cell) { 
                        return cell.value;
                    });
                });
                console.log(angular.toJson(ticket));
                $http({
                    method: 'POST',
                    url: '/Ticket/CreateTicket',
                    data: {
                        ticketJson: angular.toJson(ticket)
                    }
                });
            }
        };
    }
]);