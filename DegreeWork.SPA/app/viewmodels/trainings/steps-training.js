define(['lodash','knockout', 'common/constants'], function(_, ko, constants) {
    var defaultTemplate = "<div></div>";

    var StepsTraining = function(words, config, container) {
        var me = this;
        this.words = words;
        this.imagePath = constants.emptyWord.wordImage;
        this.isNextAllowed = ko.observable(false);
        this.index = -1;

        var TrainingLogic = container.getConstructor(config.trainingLogic);
        this.trainingLogic = new TrainingLogic({
            allowNext: function() {
                me.isNextAllowed(true);
            }
        }, config);

        this._setRepresentation();
    };

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
        if(++this.index < this.words.length) {
            var newWord = this.words[this.index];
            this.trainingLogic.updateScreen(newWord);
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