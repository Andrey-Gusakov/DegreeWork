define(['services/requestService'], function(RequestService) {
    var DictionaryService = function() {
        var requestService = new RequestService(this);

        this.getRecords = function(data) {
            var promise = requestService.get(data);
            return promise;
        }

        this.add = function(record) {
            var promise = requestService.post(record);
            return promise;
        }

        this.update = function(record) {
            var promise = requestService.put(record, { id: record.id });
            return promise;
        }

        this.remove = function(id) {
            var promise = requestService.delete({ id: id });
            return promise;
        }
    }

    return DictionaryService;
});