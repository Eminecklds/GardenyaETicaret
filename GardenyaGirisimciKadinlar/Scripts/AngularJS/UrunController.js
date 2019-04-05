app.controller('myController', function ($scope, myService) {
    //GetAll burada karsılamam lazım
    GetAllUrun();

    //get datanın icine getemployeesdeki bilgileri aldı getdataya attı. get employees benim
    //servicejs te tanımladıgım fonksiyon
    function GetAllUrun() {
        var getData = myService.getUrunlers();
        getData.then(function (response) {  //veriyi getiren kısım
            $scope.urun = response.data;
        }, function () {

            alert("veriler getirilemedi")
        });

    }
});