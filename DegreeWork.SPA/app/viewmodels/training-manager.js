define(['durandal/events',
    'plugins/router',
    'lodash',
    'knockout',
    'viewmodels/trainings/utils/words-container',
    'viewmodels/trainings/utils/trainings-activator',
    'services/trainingService'],
function(Events, router, _, ko, WordsContainer, activatorGetter, service) {
    var STATISTIC_VIEW = 'views/summary.html';

    var ctor = function() {
        this.compositionData = ko.observable();
        this.trainingsArea = 'trainings';
    };

    ctor.prototype.activate = function(trainingName) {
        var me = this;
        this.thisTrainingUrl = router.activeInstruction();

        var trainingModel = service.getTraining(trainingName);
        var config = JSON.parse(trainingModel.config);
        this._container = new WordsContainer(trainingModel.id, config.wordsInfo);
        
        var trainingComposition = this._getTrainingComposition(trainingName, config);
        this.compositionData(trainingComposition);
    }

    ctor.prototype._getTrainingComposition = function(trainingName, config) {
        var me = this;

        var trainingActivator = activatorGetter.getActivator('viewmodels/' + trainingName);
        var words = this._container.getWords();
        var training = trainingActivator.activate(words);
        training.config = config;
        training.compositionArea = this.trainingsArea;
        Events.includeIn(training);

        training.on('complete', function() {
            me._showStat();
        });

        if(_.isObject(config.compositionOptions)) {
            training = { model: training };
            _.defaults(training, compositionOptions);
        }

        return training;
    }

    ctor.prototype._showStat = function() {
        result = this._container.getTrainingResult();
        result.commit();
        this.words = result.getTrainedWords();

        this.compositionData(STATISTIC_VIEW);
    }

    return ctor;
});