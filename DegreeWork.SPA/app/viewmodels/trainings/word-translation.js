define(['lodash', 'viewmodels/trainings/steps-training'], function(_, StepsTraining) {
    var WordTranslation = function(config) {
        StepsTraining.call(this, config);
    }

    _.create(StepsTraining, {
        constructor: WordTranslation
    });


    return WordTranslation;
});