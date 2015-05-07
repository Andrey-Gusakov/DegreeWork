define(['lodash', 'knockout', 'common/constants', 'viewmodels/trainings/revealers/manager'],
function(_, ko, constants, RevealersManager) {
    var NEXTWORD_TEXT = 'Continue';
    var FINISH_TEXT = 'Show my answers!';

    var StepsTraining = function(words, config, container) {
        var me = this;
        this.words = words;
        this.imagePath = constants.emptyWord.wordImage;
        this.isNextAllowed = ko.observable(false);
        this._index = -1;
        this.continueButtonText = ko.observable(NEXTWORD_TEXT);

        this.revealersManager = new RevealersManager(_.keys(words[0]));
        this.revealersController = this.revealersManager.controller();

        this._setTrainigLogic(config, container);
        this._setRepresentation();
    };

    StepsTraining.prototype._setTrainigLogic = function(config, container) {
        var me = this;
        var TrainingLogic = container.getConstructor(config.trainingLogic);
        this.trainingLogic = new TrainingLogic({
            allowNext: function() {
                me.revealersController.reveal(constants.wordAttributes.IMAGE);
                me.isNextAllowed(true);
            }
        }, this.revealersManager.controller(), config);
    }

    StepsTraining.prototype._setRepresentation = function() {
        this.representationTemplate = {};
        if(!_.isFunction(this.trainingLogic.getRepresentation)) {
            this.representationTemplate.representation = this.trainingLogic.representation;
            this.representationTemplate.hasOwnComposition = false;
        }
        else {
            this.representationTemplate.compositionData = this.trainingLogic.getRepresentation();
            this.representationTemplate.hasOwnComposition = true;
        }
    }

    StepsTraining.prototype.activate = function() {
        this.showNext();
    };

    StepsTraining.prototype.showNext = function() {
        if(++this._index < this.words.length) {
            var newWord = this.words[this._index];
            this.revealersManager.update(newWord);
            this.trainingLogic.updateScreen(newWord);
            if(this._index + 1 == this.words.length) {
                this.continueButtonText(FINISH_TEXT);
            }
            this.isNextAllowed(false);
        }
        else {
            this.trigger('complete');
        }
    };

    StepsTraining.prototype.skipWord = function() {
        this.trainingLogic.showAnswer();
    }

    return StepsTraining;
});