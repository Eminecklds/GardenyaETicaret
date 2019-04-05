app.service('myService', function ($http) {
    //get all employes
    this.getUrunlers = function () { //this $scope aynı şeyi temsil ediyor.
        return $http.get("/Home/GetAll");

    }
});