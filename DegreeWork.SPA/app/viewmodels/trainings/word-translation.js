define(['lodash', 'knockout', 'common/constants', 'services/trainingService'], function(_, ko, constants, service) {
    var WordTranslation = function(nextUnlocker, config) {
        this.nextUnlocker = nextUnlocker;
        this.config = config;
        this.representation = ko.observable('');
        this.answers = ko.observableArray();
    };

    WordTranslation.prototype.activate = function() {
        var me = this;
        return service
            .getWords(me.config.trainingId, [constants.wordAttributes.TRANSLATION], me.config.toTake, me.config.toSkip)
            .then(function(casualData) {
                me.samples = _.map(casualData, 'translation');
                me._updateWorkingArea();
            });
    };

    WordTranslation.prototype._updateWorkingArea = function() {
        if(!this.samples) {
            return;
        };

        var currentSamples = _.sample(this.samples, 4);
        var randomIndex = new Date().getSeconds() % 4;
        currentSamples.splice(randomIndex, 0, this.currentWord.translation);
        this.answers(currentSamples);
    };

    WordTranslation.prototype.updateScreen = function(newWord) {
        this.currentWord = newWord;
        this.representation(newWord.representation);
        this._updateWorkingArea();
    };

    WordTranslation.prototype.showAnswer = function() {

    };

    WordTranslation.prototype.checkAnswer = function(data, event) {
    };

    return WordTranslation;
});