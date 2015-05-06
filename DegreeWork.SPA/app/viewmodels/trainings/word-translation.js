define(['lodash', 'knockout', 'common/constants', 'services/trainingService'], function(_, ko, constants, service) {
    var WordTranslation = function(nextUnlocker, config) {
        this.nextUnlocker = nextUnlocker;
        this.config = config;
        this.representation = ko.observable('');
    };

    WordTranslation.prototype.activate = function() {
        var me = this;
        return service
            .getWords(me.config.trainingId, [constants.wordAttributes.REPRESENTATION], me.config.toTake, me.config.toSkip)
            .then(function(casualData) {
                console.log(casualData);
            });
    }

    WordTranslation.prototype.updateScreen = function(newWord) {
        this.currentWord = newWord;
        this.representation(newWord.representation);
    };

    WordTranslation.prototype.showAnswer = function() {

    };

    WordTranslation.prototype.checkAnswer = function(data, event) {

    }

    return WordTranslation;
});