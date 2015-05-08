define(['common/constants', 'utils/common-helpers', 'viewmodels/trainings/options-training'], function(constants, utils, OptionsTraining) {
    var TranslationWord = function(revealersController, nextUnlocker, config) {
        OptionsTraining.call(this, nextUnlocker, config);
        this.revealersController = revealersController;
    };

    utils.object.inherit(TranslationWord, OptionsTraining);

    TranslationWord.prototype.setNext = function(newWord) {
        this.revealersController.reveal(constants.wordAttributes.PRONUNCIATION);
        this.constructor.baseClass.setNext.call(this, newWord);
    }

    return TranslationWord;
});