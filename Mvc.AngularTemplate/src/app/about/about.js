angular.module( 'about', [
  'ui.state',
  'placeholders',
  'ui.bootstrap',
  'titleService'
])

.config(['$stateProvider', function ( $stateProvider ) {
  $stateProvider.state( 'about', {
    url: '/about',
    views: {
      "main": {
        controller: 'AboutCtrl',
        templateUrl: 'about/about.tpl.html'
      }
    }
  });
}])

.controller( 'AboutCtrl', ['$scope', 'titleService', function ( $scope, titleService ) {
  titleService.setTitle( 'What is It?' );
  
  // This is simple demo for UI Boostrap.
  $scope.dropdownDemoItems = [
    "The first choice!",
    "And another choice for you.",
    "but wait! A third!"
  ];
}])

;
