var ifatApp = angular.module('ifatApp', [
  'ngRoute',
  'ifatAppCtrls'
]);

ifatApp.config(function($locationProvider, $routeProvider) {
    $routeProvider.
    when("/home", {
        templateUrl: "partials/questions.html",
        controller: "questionCtrl",
        activetab: "home"
    }).
    when("/filelocation", {
        templateUrl: "partials/setqfile.html",
        controller: "setFileCtrl",
        activetab: "filelocation"
    }).
    when("/finish", {
        templateUrl: "partials/finish.html",
        controller: "finishCtrl"
    }).
    otherwise({
        redirectTo: "/home"
    });
});