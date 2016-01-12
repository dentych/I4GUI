var ifatAppCtrls = angular.module("ifatAppCtrls", []);

ifatAppCtrls.controller("questionCtrl", function ($scope, $route, $http, $location) {
    var filename = localStorage.getItem("IFATFilename");
    if (filename == null) {
        filename = "questions.json";
    }

    $scope.questions = {};

    var questions = {};

    $http.get("questions/" + filename).success(function (response) {
        questions = response;

        for (var i = 0; i < questions.length; i++) {
            questions[i].points = 0;
            questions[i].mistakes = 0;
            questions[i].answers = [];
        }

        $scope.questions = questions;
    });

    $scope.clickedButtons = [];

    $scope.answer = function (answer, index) {
        var question = $scope.questions[index];
        question.answers.push(answer);

        if (question.Correct == answer) {
            question.answered = true;
        }
        else {
            question.mistakes++;
            switch (question.mistakes) {
                case 1:
                    question.points = 2;
                    break;
                case 2:
                    question.points = 4;
                    break;
                case 3:
                    question.points = 8;
                    break;
            }
        }
    };

    $scope.isDisabled = function () {
        for (var i = 0; i < $scope.questions.length; i++) {
            if ($scope.questions[i].answered != true) {
                return true;
            }
        }

        return false;
    }

    $scope.totalScore = function() {
        var sum = 0;
        for (var i = 0; i < $scope.questions.length; i++) {
            sum += $scope.questions[i].points;
        }
        
        return sum;
    }
    
    $scope.complete = function () {
        localStorage.setItem("IFATResult", $scope.totalScore());
        localStorage.setItem("IFATQuestionCount", $scope.questions.length);
        $location.path("/finish");
        // alert($scope.totalScore());
    }

    $scope.getButtonClass = function (question, letter) {
        var result = {};
        result.answerbutton = question.answers.indexOf(letter) == -1;
        result.answeredbutton = question.answers.indexOf(letter) != -1;
        result.correctbutton = question.answers.indexOf(letter) != -1 && question.Correct == letter;

        return result;
    }
});

ifatAppCtrls.controller("setFileCtrl", function ($scope, $location, $route) {
    $scope.$emit("routeEvent", $route.current.activetab);

    if (typeof (Storage) === "undefined") {
        alert("LocationStorage fungerer ikke.. Default fil (questions.json) vil blive brugt.");
        return;
    }

    $scope.filename = localStorage.getItem("IFATFilename");

    $scope.saveFilename = function () {
        if ($scope.filename == "") {
            localStorage.removeItem("IFATFilename");
        }
        else {
            localStorage.setItem("IFATFilename", $scope.filename);
        }
        $location.path("/");
    }
});

ifatAppCtrls.controller("finishCtrl", function ($scope) {
    $scope.finalScore = localStorage.getItem("IFATResult");
    count = localStorage.getItem("IFATQuestionCount");
    $scope.possiblePoints = count * 8;
});

ifatAppCtrls.controller("navCtrl", function ($scope, $route) {
    $scope.$on("routeEvent", function (event, args) {
        alert("Yup!" + args);
    });
});