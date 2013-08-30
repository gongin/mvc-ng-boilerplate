angular.module( 'app', [
  'templates-app',
  'templates-common',
  'home',
  'about',
  'ui.state',
  'ui.route'
])

.config( function myAppConfig ( $stateProvider, $urlRouterProvider ) {
  $urlRouterProvider.otherwise( '/home' );
})

.run( function run ( titleService ) {
  titleService.setSuffix( ' | Mvc-Ng-Boilerplate' );
})

.controller( 'AppCtrl', function AppCtrl ( $scope, $location ) {
})

;

