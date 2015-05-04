define(['plugins/router', 'durandal/app'], function (router, app) {
    return {
        router: router,
        activate: function () {
            router.map([
                { route: '', title: 'Welcome', moduleId: 'viewmodels/welcome', nav: true },
                { route: 'dictionary', title: 'Your dictionary', moduleId: 'viewmodels/dictionary', nav: true },
                { route: 'trainings', title: 'Your trainings', moduleId: 'viewmodels/trainings', nav: true },
                { route: 'training/:trainingName', title: 'Training', moduleId: 'viewmodels/training-manager' },
                { route: 'stats', moduleId: 'viewmodels/flickr', nav: true }
            ]).buildNavigationModel();

            return router.activate();
        }
    };
});