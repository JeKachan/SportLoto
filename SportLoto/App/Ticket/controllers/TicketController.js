angular.module('ticketApp').controller("TicketController",
    ['$scope', '$http', "$timeout", 'ticketFactory',
    function ($scope, $http, $timeout,ticketFactory) {
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
        };

        $scope.resetTicketSelect = function () {
            var ticket = $scope.ticket;
            //reset all selected cells in sections
            _.forEach(ticket, function (section) {
                _.forEach(section, function (cell) {
                    cell.isSelect = false;
                });
            });
        };

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
                //ticket format [[1, 34, 2, 4, 43, 23], ...]
                var ticket = _.map($scope.ticket, function (section) {
                    var selectedCells = _.filter(section, function (cell) {
                        return cell.isSelect;
                    });

                    return _.map(selectedCells, function (cell) { 
                        return cell.value;
                    });
                });


                $http({
                    method: 'POST',
                    url: '/Ticket/CreateTicket',
                    data: {
                        ticketJson: angular.toJson(ticket)
                    }
                }).then(function(response){
                    if (response.data.Succesed) {
                        $scope.showSuccesMessage = true;
                        $scope.resetTicketSelect();
                        $timeout(function() {
                            $scope.showSuccesMessage = false;
                        }, 5000);
                    }
                    else {
                        $scope.showErrorMessage = true;
                        $scope.errorMessage = response.date != null && response.data.Errors.length > 0 ?
                            response.data.Errors.join(". ") : "Error was occurred when added the ticket."
                        $timeout(function() {
                            $scope.showErrorMessage = false;
                            $scope.errorMessage = "";
                        }, 5000);
                    }
                    console.log("success", response);
                }).then(function(response) {
                    console.log("error", response);
                });
            }
        };
    }
]);