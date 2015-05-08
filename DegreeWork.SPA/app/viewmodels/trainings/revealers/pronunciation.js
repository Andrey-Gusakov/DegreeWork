define(function() {
    var PronunciationRevealer = function() {
        this._audioElement = null;

        this.update = function(value) {
            this._audioElement = new Audio(value);
        };

        this.reveal = function() {
            if(this._audioElement) {
                this._audioElement.play();
            }
        };
    }

    return PronunciationRevealer;
});