define(['common/constants'], function(constants) {
    var defaultTemplate = _.template("<p class='lead' data-bind='text: representation'><p>");

    var StepsTraining = function(words, config, container) {
        var me = this;
        this.words = words;
        this.imagePath = constants.emptyWord.wordImage;
        this.isNextAllowed = ko.observable(false);
        this.index = -1;
        this.defaultRepresentationTemplate = {
            model: trainingLogic,
            view: defaultTemplate
        };

        var TrainingLogic = container.getConstructor(config.trainingLogic);
        this.trainingLogic = new TrainingLogic({
            allowNext: function() {
                me.isNextAllowed(true);
            }
        });
    };

    StepsTraining.prototype.activate = function() {
        this.showNext();
    };

    StepsTraining.prototype.showNext = function() {
        if(++this.index < this.words.length) {
            var newWord = me.words[index];
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