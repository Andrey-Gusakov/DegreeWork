﻿define(['common/constants', 'utils/common-helpers', 'viewmodels/trainings/options-training'], function(constants, utils, OptionsTraining) {
    var WordTranslation = function(revealersController, nextUnlocker, config) {
        OptionsTraining.call(this, nextUnlocker, config);
        this.revealersController = revealersController;
    };

    utils.object.inherit(WordTranslation, OptionsTraining);

    WordTranslation.prototype.updateScreen = function(newWord) {
        this.revealersController.reveal(constants.wordAttributes.PRONUNCIATION);
        this.constructor.baseClass.updateScreen.call(this, newWord);
    }

    return WordTranslation;
});