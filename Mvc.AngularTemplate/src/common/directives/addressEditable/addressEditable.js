angular.module('directives.addressEditable', [])

// A directive to allow editing of any html element linked to an angular modal
.directive('addressEditable', function () {
  return {
    restrict: 'E',
    require: 'ngModel',
    // The model referenced needs to have the following attributes: address1, address2, city, state, zip5, zip4
    // The component needs to have an attribute for address-title to display the title.
    // The component needs to have an attribute for onremove-address referencing a function to handle removing of the address
    replace: true,
    scope: {
      title: '@addressTitle',
      startOpen: '@',
      address: '=ngModel',
      onremoveAddress: '&'
    },
    templateUrl: 'directives/addressEditable/addressEditable.tpl.html',
    link: function (scope, element, attrs) {
      // Title Element
      var title = angular.element(element.children()[0]),
        opened = true;

      // Bind the title div click and function below    
      title.bind('click', toggle);

      // Toggle method to open and close the form
      function toggle() {
        opened = !opened;
        element.removeClass(opened ? 'closed' : 'opened');
        element.addClass(opened ? 'opened' : 'closed');
      }

      // Initialize the toggle to close the items.
      toggle();

      scope.$watch(function () { return scope.startOpen; }, function (scopeValue) {
        if (scopeValue == 'true') {
          toggle();
        }
      });
    }
  };
});

