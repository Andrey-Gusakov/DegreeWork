define(['lodash'], function(_) {
    var RequestContextBuilder = function(refresher) {
        this._context = {};
        this._refresher = refresher;
    };

    RequestContextBuilder.prototype.getRequestContext = function() {
        var result = this._context;
        this._context = {};
        return result;
    };

    RequestContextBuilder.prototype.replace = function(obj) {
        _.assign(this._context, obj)
        if(this.refresher) {
            this.refresher.refresh();
        }
    };

    RequestContextBuilder.prototype.add = function(obj) {
        _.defaults(this._context, obj);
        if(this.refresher) {
            this.refresher.refresh();
        }
    };

    return RequestContextBuilder;
});