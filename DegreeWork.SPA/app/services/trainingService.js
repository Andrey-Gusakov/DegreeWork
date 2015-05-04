define(['services/requestService'], function(RequestService) {
    var TrainingService = function() {
        var requestService = new RequestService(this);

        this.getTraining = function(name) {
            var promise = requestService.get(name);
            return promise;
        }
    }

    return new TrainingService();
});