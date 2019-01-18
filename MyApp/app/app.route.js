﻿ (function () {
	
	// Collect the routes
	app.constant('routes', [
		{
			url: '/',
			config: {
			    templateUrl: '/app/user/dashboard.html'
			}
		},
		{
			url: '/history/:userId',
			config: {
			    templateUrl: '/app/user/history.html'
			}
		},
        {
            url: '/edit/:formId',
            config: {
                templateUrl: '/app/user/dashboard.html'
            }
        }
		
	]);
	
	
	// Configure the routes and route resolvers
	app.config(['$routeProvider', 'routes', '$locationProvider', routeConfigurator]);
	function routeConfigurator($routeProvider, routes, $locationProvider) {
		routes.forEach(function (r) {
			$routeProvider.when(r.url, r.config);
		});
		$routeProvider.otherwise({ redirectTo: '/' });
	};


})();

