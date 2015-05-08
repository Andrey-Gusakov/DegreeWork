define(['lodash', 'knockout', 'durandal/system', 'durandal/viewLocator', 'common/constants', 'services/trainingService'],
function(_, ko, system, viewLocator, constants, service) {
    var DEFAULT_CLASS = 'btn-default';
    var CORRECTANSWER_CLASS = 'btn-success active';
    var WRONGANSWER_CLASS = 'btn-danger active';
    var SHOWANSWER_CLASS = 'btn-info active';
    var COUNT = 4;

    var OptionsTraining = function(nextUnlocker, config) {
        this.nextUnlocker = nextUnlocker;
        this.config = config;
        this._representationProp = config["options-training"].representationProperty;
        this._sampleProp = config["options-training"].sampleProperty;
        this.representation = ko.observable('');
        this.answers = ko.observableArray();
    };

    OptionsTraining.prototype.getView = function() {
        var moduleId = system.getModuleId(OptionsTraining);
        var result = viewLocator.convertModuleIdToViewId(moduleId);
        return result;
    }

    OptionsTraining.prototype.activate = function() {
        var me = this;
        return service
            .getWords(me.config.trainingId, [ constants.wordAttributes[me._sampleProp.toUpperCase()] ], me.config.toTake)
            .then(function(casualData) {
                me.samples = _.map(casualData, me._sampleProp)
                me._updateWorkingArea();
            });
    };

    OptionsTraining.prototype._updateWorkingArea = function() {
        if(!this.samples) {
            return;
        };
        var mainSample = this.currentWord[this._sampleProp];
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

    OptionsTraining.prototype.updateScreen = function(newWord) {
        this.currentWord = newWord;
        this.representation(newWord[this._representationProp]);
        this._updateWorkingArea();
    };

    OptionsTraining.prototype.showAnswer = function(skipped) {
        var correctAnswer = this.answers()[this._idx];
        if(skipped !== true) {
            this.currentWord.skip();
        }

        correctAnswer.sampleClass(SHOWANSWER_CLASS);
        this.setNext();
    };

    OptionsTraining.prototype.checkAnswer = function(me, event) {
        if(this._isDirty()) {
            return;
        }
        var data = ko.dataFor(event.target);
        var isCorrect = me.currentWord.check(data.sample);

        if(isCorrect) {
            data.sampleClass(CORRECTANSWER_CLASS);
            this.setNext();
        }
        else {
            data.sampleClass(WRONGANSWER_CLASS);
            me.showAnswer(true);
        }
    };

    OptionsTraining.prototype.setNext = function() {
        this.nextUnlocker.allowNext();
    }

    OptionsTraining.prototype._isDirty = function() {
        var result = _.any(this.answers(), function(obj) {
            return obj.sampleClass() !== DEFAULT_CLASS;
        });

        return result;
    }

    return OptionsTraining;
});