define(['knockout', 'common/constants'], function(ko, constants) {
    var ImageRevealer = function() {
        this.isShown = ko.observable(false);
        this.defaultSrc = constants.emptyWord.wordImage;
        this.data = ko.observable();

        this.update = function(value) {
            this.isShown(false);
            this.data(value);
        };

        this.reveal = function() {
            this.isShown(true);
        };
    }

    return ImageRevealer;
});