angular.module( 'app', [
  'templates-app',
  'templates-common',
  'home',
  'about',
  'search',
  'ui.state',
  'ui.route'
])

  // Having the inputs prefixed before the method protects them from minification, otherwise the injection could fail.
  // Only show the default and routing for this page.  Each "module" defines it's own config and they are put together.  Allows for easy transfer of parts.
  // Omitting function name out of being lazy
.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', function ($stateProvider, $urlRouterProvider, $locationProvider) {

  // This enables html 5 mode, but must also be configured for url re-write in the web.config.
  // The hash prefix is in the event a browser doesn't support html5mode, it will goto #! syntax
  $locationProvider.html5Mode(true).hashPrefix('!');

  // Default for any route unknown.
  $urlRouterProvider.otherwise('/home');

  // Example of page navigation based on url parameters: If the incoming location has a query property, redirect to the proper search page
  $urlRouterProvider.rule(function ($injector, $location) {
    if ($location.search().query !== undefined) {
      return '/search';
    }
  });
}])

  // Example of a service that maintains the title bar for each page.  Code is in Common/services and written in CoffeeScript!
.run( ['titleService', function ( titleService ) {
  titleService.setSuffix( ' | Mvc-Ng-Boilerplate' );
}])

  // Controller syntax, even though it isn't doing anything.  Notice the minification proofing again
.controller( 'AppCtrl',['$scope', '$location', function ( $scope, $location ) {
}])

;

