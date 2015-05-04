define(['durandal/app', 'knockout', 'lodash', 'services/wordService'],
function(app, ko, _, wordService) {
    var WordEditor = function(context, wordModel) {
        this.isNew = !context.id;
        this.context = context;

        this.translations = ko.observableArray();
        this.wordImages = ko.observableArray();

        this.isComplete = ko.computed(function() {
            return this.translations().length > 0;
        }, this);

        if(wordModel) {
            this._setModel(wordModel);
        }
        else {
            this.wordImages.push({
                image: this.context.imagePath(),
                selected: true
            });
        }

        this.word = ko.observable(!this.isNew ? wordModel.word : null);
    };

    WordEditor.prototype._setModel = function(wordModel) {
        var me = this;

        this.translations(_.map(wordModel.translations, function(val) {
            return {
                translation: val,
                isAdded: ko.observable(me.isNew || _.some(me.context.translations(), function(t) { return val === t; }))
            };
        }));

        var wordImages = _.map(wordModel.wordImages, function(val) {
            return {
                image: val,
                selected: ko.observable(me.isNew ? false : (me.context.imagePath() === val))
            };
        });

        if(this.isNew) {
            wordImages[0].selected(true);
        }

        this._previousSelection = _.find(wordImages, function(obj) {
            return obj.selected();
        });

        this.wordImages(wordImages);
    }

    WordEditor.prototype.activate = function(activationData) {
        this._service = activationData.service;
    };

    WordEditor.prototype.toggleTranslation = function(data, event) {
        var translation = ko.dataFor(event.target);
        translation.isAdded(!translation.isAdded());
    };

    WordEditor.prototype.selectImage = function(data, event) {
        var imageModel = ko.dataFor(event.target);
        if(this._previousSelection) {
            this._previousSelection.selected(false);
        }

        imageModel.selected(true);
        this._previousSelection = imageModel;
    }

    WordEditor.prototype.searchWordData = function() {
        var me = this;

        wordService.searchWord(this.word()).then(function(data) {
            me._setModel(data);
        });
    };

    WordEditor.prototype.saveChanges = function() {
        if(!this.isNew) {
            var record = this._getDictionaryRecord();
            this.context.imagePath(record.wordImage);
            this.context.translations(record.translations.length > 0 ? record.translations : context.translations()[0]);

            this._service.update(record);
        }

        return true;
    };

    WordEditor.prototype.addWord = function() {
        if(!this.isComplete()) {
            app.showMessage('You must fill all sections!');
        }

        var record = this._getDictionaryRecord();
        this._service.add(record);
        this._clear();
    };

    WordEditor.prototype._clear = function() {
        this.translations([]);
        this.wordImages([{
            image: this.context.imagePath(),
            selected: true
        }]);
        this.word(null);
    }

    WordEditor.prototype._getDictionaryRecord = function() {
        var result = {};
        result.id = this.context.id;
        result.word = this.word();
        result.translations = _(ko.toJS(this.translations)).filter('isAdded').map('translation').value();
        var imageModel = _.find(this.wordImages(), function(val) {
            return val.selected();
        });
        if(imageModel) {
            result.wordImage = imageModel.image;
        }

        return result;
    }

    return WordEditor;
});