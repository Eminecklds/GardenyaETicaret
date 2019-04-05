app.service('myservice', function ($http) {
    //Get All Employees
    //$scope ile this aynı şeyi ifade eder
    this.getUrunler = function () {
        return $http.get("/Home/UrunlerListesi");

    }

    this.getByID = function (AltKategoriID) {
        var response = $http({
            method: "Post",
            url: "/Home/getByID",
            params: {
                id: JSON.stringify(AltKategoriID)
            }

        });
        return response;
    }
});