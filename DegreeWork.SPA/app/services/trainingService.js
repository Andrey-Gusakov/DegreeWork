define(['services/requestService'], function(RequestService) {
    var TrainingService = function() {
        var requestService = new RequestService(this);

        this.getTraining = function(name) {
            var promise = requestService.get({ trainingName: name });
            return promise;
        }

        this.getWords = function(trainingId, attributes, count) {
            var promise = requestService.post({ trainingId: trainingId, wordAttributes: attributes, count: count }, 'words');
            return promise;
        }
    }

    return new TrainingService();
});