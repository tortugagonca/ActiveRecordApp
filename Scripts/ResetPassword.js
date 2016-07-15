(function (angular) {
    'use strict';
    var app = angular.module('ResetPassword', ['ngAnimate', 'ui.bootstrap']);
    //var mode = angular.module('heroApp', []);
    app.controller('ResetPasswordController', function ($scope) {
        var controller = this;

        $scope.nomeUsuario = 'Joaquim';
        $scope.ResultadoExistenciaUsuario = 'D E S C U B R A';
        var setaValorExistenciaUsuario = function (resultado) {
            $scope.ResultadoExistenciaUsuario = resultado;
        };
        $scope.VerificaUsuarioExiste = function (nomeUsuario) {
            setaValorExistenciaUsuario("Calmae carai, to vendo isso");
            $.ajax({
                url: 'Home/VerificaExistenciaUsuario',
                data: {
                    userName: nomeUsuario
                }, 
                success: function (result) { 
                    setaValorExistenciaUsuario(result);
                    $scope.$apply();
                },
                scope: this

            });

        };
    });
})(window.angular);