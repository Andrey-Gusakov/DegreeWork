define(['lodash'], function(_) {
    var WordTranslation = function() {
        this.representation = ko.observable('');
    };

    WordTranslation.prototype.updateScreen = function(newWord) {
        this.currentWord = newWord;
        this.representation(nextWord.representation);
    };

    WordTranslation.prototype.showAnswer = function() {

    };

    WordTranslation.prototype.checkAnswer = function(data, event) {

    }

    return WordTranslation;
});