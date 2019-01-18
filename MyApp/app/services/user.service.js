(function () {
    'use strict';

    var serviceId = 'userService';
    app.factory(serviceId, ['$q', '$http', userService]);

    function userService($q, $http) {

        var service = {
            getHistory: getHistory,
            createNewForm: createNewForm,
            getFromDetails: getFromDetails,
            deleteForm: deleteForm
        };

        return service;

        function makeAjaxCall(actionNameAndParams, httpMethod, data) {
            httpMethod = httpMethod || 'GET';
            data = data || {};
            var deferred = $q.defer();
            var url = "/Home/" + actionNameAndParams;

            $http({ method: httpMethod, url: url, data: data })
               .success(deferred.resolve)
               .error(deferred.reject);

            return deferred.promise;

        }

        function getHistory(userId) {
            return makeAjaxCall("getHistory/" + userId, "GET");
        }

        function getFromDetails(formId) {
            return makeAjaxCall("getFormById/" + formId, "GET");
        }

        function createNewForm(data) {
            return makeAjaxCall("createNewForm", "POST", data);
        }

        function deleteForm(formId) {
            return makeAjaxCall("deleteForm/" + formId, "DELETE");
        }

    }
})();