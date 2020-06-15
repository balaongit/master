var app = angular.module('ApApp', []);

	app.controller('ApCtrl', function ($scope) {

	    $scope.catId = "";

$scope.catLevel = "";

	

	$scope.success = function (result) {

	        var data = result.d;

	        $scope.calculationResult = data;

	    }

	

	    $scope.calculateCategory = function () {

	        //$scope.calculationResult = "";

	        //Populate the values from input as numbers

	        var catId = Number($scope.catId);

	       

	        //Check for valid values

	        if ($scope.catId != null && Number(catId) > 0) {

	            var url = "/GetCategoryInfo",

	                data = { categoryId: catId };

	            $.ajax({

	                type: "POST",

	                dataType: "json",

	                url: url,

	                data: JSON.stringify(data),

	                contentType: "application/json;charset=utf-8",

	                success: function (result) {

	                    var data = result.d;

	

	 $('#resultDiv').html(data);

	                },

	                failure: function (result) {

	                    var data = result.d;

	                    $('#resultDiv').html(data);

	                }

	            });





	        } else {

	            $('#resultDiv').html("Please enter valid values.");

	        }





	    }

$scope.calculateLevel = function () {

	     

	        //Populate the values from input as numbers

	        var catId = Number($scope.catId);

	        var catLevel = Number($scope.catLevel);

	        //Check for valid values

	        if ($scope.catLevel != null && Number(catLevel) > 0) {



                //url

	            var url = "/GetCategoryInfoByLevel",

	               data = { level: catLevel };

	            $.ajax({

	                type: "POST",

	                dataType: "json",

	                url: url,

	                data: JSON.stringify(data),

	                contentType: "application/json;charset=utf-8",

	                success: function (result) {

	                    var data = result.d;

	                    //$scope.calculationResult = data;

	                    if (data.length > 0) {

	                        $('#levelDiv').html(data);





	                    } else {

	                        $('#levelDiv').html("No Information found at this level.");

	                    }





	                },

	                failure: function (result) {

	                    var data = result.d;

	                    $('#levelDiv').html(data);

	                }

	            });





	        } else {

	            $('#levelDiv').html("Please enter valid values.");

	        }





	    }



	

	

	});