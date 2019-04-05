app.controller('myCtrl', function ($scope, myservice) {
    //GetAll burada karşılamam lazım
    //$scope.divEmployee = false;
   
    function GetUrun(AltKategoriID) {
        console.log(AltKategoriID);
        var getData = myservice.getByID(AltKategoriID);
        //var getData = myservice.getEmployees();
        getData.then(function (urun) {
            $scope.urun = urun.data;

        }, function () {
            alert("veriler getirilemedi");
        });
    }
});