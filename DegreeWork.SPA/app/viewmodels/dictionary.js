define(['knockout',
    'lodash',
    'common/requestContextBuilder',
    'services/dictionaryService',
    'viewmodels/dictionary-card',
    'bindings/editableNode'],
function(ko, _, RequestContext, Service, DictionaryCard) {
    var Dictionary = function() {
        var me = this;

        me._service = new Service();
        me.requestContext = new RequestContext(me);
        me.records = ko.observableArray();
        me.insertRecordModel = new DictionaryCard();

        me.dictionaryService = {
            add: function(record) {
                me._service.add(record).done(function(resultRecord) {
                    var addedCard = new DictionaryCard(resultRecord);
                    me.records.push(addedCard);
                });
            },
            update: function(record) {
                me._service.update(record);
            }
        };
    };

    Dictionary.prototype.activate = function() {
        return this.refresh();
    }

    Dictionary.prototype.refresh = function(data) {
        var me = this;

        return me._service.getRecords(data).then(function(records) {
            var dictionaryCards = _.map(records, function(val) {
                return new DictionaryCard(val);
            });
            me.records(dictionaryCards);
        });
    }

    return Dictionary;
});