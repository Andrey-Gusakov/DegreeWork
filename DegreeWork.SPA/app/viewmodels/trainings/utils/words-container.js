define(['lodash', 'common/constants', 'services/trainingService'], function(_, constants, trainingService) {
    var attributes = constants.wordAttributes;

    function getTrainingWordCtor(checkProperty, wordsContext) {
        return function(word) {
            var me = this;
            _.assign(me, word);
            this.check = function(data) {
                var result = me[checkProperty] === data;
                if(!me._isAdded)
                {
                    wordsContext.push({ id: me.id, word: me.word, translations: me.translations, correctAnswer: result });
                    me.isAdded = true;
                }
                return result;
            }
        }
    }

    var ctor = function(trainingId, wordsInfo) {
        this._trainingId = trainingId;
        this._attributes = _.union(wordsInfo.attributes, [attributes.REPRESENTATION, attributes.TRANSLATIONS]);
        this._count = wordsInfo.toTake;
        this._checkByField = wordsInfo.checkByField;
    };

    ctor.prototype.getTrainingResult = function() {
        var me = this;

        return {
            commit: function() {
                var commitData = _.map(me._context, function(obj) {
                    return {
                        id: word.id,
                        isCorrect: word.correctAnswer
                    };
                });
                trainingService.updateStatistic(commitData);
            },
            getTrainedWords: function() {
                return me._context;
            }
        };
    };

    ctor.prototype.getWords = function(checkProperty) {
        this._context = [];
        var TrainingWord = getTrainingWordCtor(this._checkByField, this._context);
        var words = trainingService.getWords(this._trainingId, this._attributes, this._count);
        var collection = _.map(words, function(word) {
            return new TrainingWord(word);
        });

        collection = _.shuffle(collection);
        return collection;
    }
    
    return ctor;
});