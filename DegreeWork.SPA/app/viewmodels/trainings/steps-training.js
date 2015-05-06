define(['lodash','knockout', 'common/constants'], function(_, ko, constants) {
    var defaultTemplate = "<div><p class='lead' data-bind='text: representation'></p></div>";

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
        if(!_.isFunction(this.trainingLogic.getRepresentation)) {
            this.representationTemplate = {
                model: {
                    representation: this.trainingLogic.representation,
                    getView: function() {
                        return defaultTemplate;
                    }
                },
                
            };
        }
        else {
            this.representationTemplate = this.trainingLogic.getRepresentation();
        }
    }

    StepsTraining.prototype.activate = function() {
        this.showNext();
        this.isNextAllowed(true);
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