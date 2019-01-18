(function () {

    // Creating the controller
    var controllerId = 'dashboard';
    app.controller(controllerId, ['userService', '$routeParams', dashboard]);

    function dashboard(userService, $routeParams) {

        var vm = this;
        vm.title = 'Dashboard - ';
        vm.isSubmitting = false;
        vm.isSearching = false;
        vm.newForm = {}

        vm.createNewForm = createNewForm;
        vm.searchForm = searchForm;
        vm.deleteHistory = deleteHistory;

        function init() {
            //TODO :: Add some logic to get user id
            vm.userId = 100;

            // Editting a form details
            if ($routeParams.formId) {
                getFromDetails($routeParams.formId);
            }

        }

        function createNewForm() {
            vm.newForm.UserId = vm.userId;
            vm.isSubmitting = true;
            userService.createNewForm(vm.newForm).then(function (res) {
                vm.isSubmitting = false;
                vm.newForm = {};
                alert('Added successfully!');
            }, function (err) {
                console.log(err)
            })
        }

        function getFromDetails(formId) {
            userService.getFromDetails(formId).then(function (data) {
                vm.isSearching = false;
                if (data) {
                    vm.newForm = data;
                    vm.userId = data.UserId;
                }
                else {
                    vm.newForm = {};
                    alert('No active form found');
                }
            }, function (err) {
                vm.isSearching = false;
                vm.newForm = {};
                alert('No active form found');
            });
        }

        function searchForm() {

            if (!vm.formId) {
                alert('Enter form Id');
                return;
            }

            vm.isSearching = true;
            getFromDetails(vm.formId);
        }

        function deleteHistory(userId) {
            if (!confirm('Are you sure you want to delete this history?')) return;

            userService.deleteForm(userId).then(function (data) {
                alert('Form deleted');
                vm.newForm = {};
            });
        }

        init();


    };
})();


