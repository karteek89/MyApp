(function () {

    // Creating the controller
    var controllerId = 'history';
    app.controller(controllerId, ['userService', '$routeParams', history]);

    function history(userService, $routeParams) {

        var vm = this;
        vm.title = 'History - ';
        vm.historyList = [];

        function init() {
            userService.getHistory($routeParams.userId).then(function (list) {
                vm.historyList = list;
            }, function (err) {
                console.log(err);
            });
        }       

        init();

    };
})();


