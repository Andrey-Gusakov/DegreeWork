define(['knockout', 'lodash', 'services/dictionaryService', 'viewmodels/word-editor', 'common/constants'],
function(ko, _, service, Editor, constants) {
    var DictionaryCard = function(dictionaryRecord) {
        if(!dictionaryRecord) {
            dictionaryRecord = constants.emptyWord;
        }
        else {
            this._id = dictionaryRecord.id;
            this._wordModel = dictionaryRecord.wordModel;
        }

        this.wordContext = {
            id: dictionaryRecord.id,
            word: ko.observable(dictionaryRecord.word),
            translations: ko.observableArray(dictionaryRecord.translations),
            imagePath: ko.observable(dictionaryRecord.wordImage)
        };
    };

    DictionaryCard.prototype.activate = function() {
    };

    DictionaryCard.prototype.getEditor = function() {
        if(!this._editor) {
            this._editor = new Editor(this.wordContext, this._wordModel);
        }

        return this._editor;
    };

    DictionaryCard.prototype.removeWord = function() {
        service.remove(this._id);
    }

    return DictionaryCard;
});