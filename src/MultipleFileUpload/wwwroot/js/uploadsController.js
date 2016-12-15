(function () {

    "use strict";

    angular.module("app-uploads")
        .controller("uploadsController", uploadsController);

    function uploadsController($http) {
        var vm = this;

        vm.message = "hello from danny";

        vm.retrieved = "";
        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/uploads")
            .then(function (response) {
                vm.retrieved = response.data;
            }, function (error) {
                vm.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.upload = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            var fileUpload = $("#files").get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length ; i++) {
                data.append(files[i].name, files[i]);
            }


            $http.post("/api/uploads", data, {
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity
                })
                .then(function (response) {

                }, function () {
                    vm.errorMessage = "Failed to save data";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };
    }

})();