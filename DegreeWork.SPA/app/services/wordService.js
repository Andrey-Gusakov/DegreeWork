define(['services/requestService'], function(RequestService) {
    function WordService() {
        var requestService = new RequestService(this);

        this.searchWord = function(word) {
            return requestService.post(word, 'search');
        }
    }

    return new WordService();
});