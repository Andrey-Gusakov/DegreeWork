define(['lodash', 'knockout', 'common/constants', 'services/trainingService'], function(_, ko, constants, service) {
    var DEFAULT_CLASS = 'btn-default';
    var CORRECTANSWER_CLASS = 'btn-success active';
    var WRONGANSWER_CLASS = 'btn-danger active';
    var SHOWANSWER_CLASS = 'btn-info active';
    var COUNT = 4;

    var WordTranslation = function(nextUnlocker, revealersController, config) {
        this.nextUnlocker = nextUnlocker;
        this.config = config;
        this.revealersController = revealersController;
        this.representation = ko.observable('');
        this.answers = ko.observableArray();
    };

    WordTranslation.prototype.activate = function() {
        var me = this;
        return service
            .getWords(me.config.trainingId, [constants.wordAttributes.TRANSLATION], me.config.toTake)
            .then(function(casualData) {
                me.samples = _.map(casualData, 'translation')
                me._updateWorkingArea();
            });
    };

    WordTranslation.prototype._updateWorkingArea = function() {
        if(!this.samples) {
            return;
        };
        var mainSample = this.currentWord.translation;
        var currentSamples = _.sample(this.samples, COUNT + 1);
        this._idx = currentSamples.indexOf(mainSample);
        if(this._idx === -1) {
            currentSamples.pop();
            this._idx = new Date().getSeconds() % COUNT;
            currentSamples.splice(this._idx, 0, mainSample);
        }
        var answers = _.map(currentSamples, function(data) {
            return {
                sample: data,
                sampleClass: ko.observable(DEFAULT_CLASS)
            };
        });
        this.answers(answers);
    };

    WordTranslation.prototype.updateScreen = function(newWord) {
        this.currentWord = newWord;
        this.revealersController.reveal(constants.wordAttributes.PRONUNCIATION);
        this.representation(newWord.representation);
        this._updateWorkingArea();
    };

    WordTranslation.prototype.showAnswer = function(skipped) {
        var correctAnswer = this.answers()[this._idx];
        if(skipped !== true) {
            this.currentWord.skip();
        }
        
        correctAnswer.sampleClass(SHOWANSWER_CLASS);
        this.nextUnlocker.allowNext();
    };

    WordTranslation.prototype.checkAnswer = function(me, event) {
        if(this._isDirty()) {
            return;
        }
        var data = ko.dataFor(event.target);
        var isCorrect = me.currentWord.check(data.sample);
        
        if(isCorrect) {
            data.sampleClass(CORRECTANSWER_CLASS);
            this.nextUnlocker.allowNext();
        }
        else {
            data.sampleClass(WRONGANSWER_CLASS);
            me.showAnswer(true);
        }
    };

    WordTranslation.prototype._isDirty = function() {
        var result = _.any(this.answers(), function(obj) {
            return obj.sampleClass() !== DEFAULT_CLASS;
        });

        return result;
    }

    return WordTranslation;
});