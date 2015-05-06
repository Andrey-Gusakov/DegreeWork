define(['durandal/events',
    'plugins/router',
    'lodash',
    'knockout',
    'viewmodels/trainings/utils/words-container',
    'viewmodels/trainings/utils/trainings-container',
    'services/trainingService'],
function(Events, router, _, ko, WordsContainer, trainingsContainer, service) {
    var STATISTIC_VIEW = 'views/summary.html';

    var ctor = function() {
        this.trainingsArea = 'trainings';
        this.compositionData = { };
    };

    ctor.prototype.activate = function(trainingName) {
        var me = this;
        me.thisTrainingUrl = router.activeInstruction();

        var promise = service.getTraining(trainingName).then(function(trainingModel) {
            var config = JSON.parse(trainingModel.config);
            me._container = new WordsContainer(trainingModel.id, config.wordsInfo);
            
            return me._getTrainingComposition(trainingModel, config).then(function(trainingComposition) {
                me.compositionData.model = trainingComposition;
            });
        });

        return promise;
    }

    ctor.prototype._getTrainingComposition = function(trainingModel, config) {
        var me = this;
        var name = trainingModel.widgetName;
        return this._container.getWords().then(function(words) {
            var Training = trainingsContainer.getConstructor(name);
            config[name].trainingId = trainingModel.id;
            var training = new Training(words, config[name], trainingsContainer);
            training.compositionArea = me.trainingsArea;
            Events.includeIn(training);

            training.on('complete', function() {
                me._showStat();
            });

            if(_.isObject(config.compositionOptions)) {
                training = { model: training };
                _.defaults(training, compositionOptions);
            }

            return training;
        });
    }

    ctor.prototype._showStat = function() {
        result = this._container.getTrainingResult();
        result.commit();
        this.words = result.getTrainedWords();

        this.compositionData(STATISTIC_VIEW);
    }

    return ctor;
});