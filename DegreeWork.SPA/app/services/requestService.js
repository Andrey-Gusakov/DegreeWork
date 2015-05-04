define(['durandal/system', 'jquery', 'lodash', 'common/apis'], function(system, $, _, apis) {
    var KEY_PATTERN = /\/(.*?)Service/;
    var DEFAULT_BASEPATH = '/api/';

    var RequestService = function(token) {
        this._token = token;
    }

    function getKey(token) {
        if(getKey.key) {
            return getKey.key;
        }

        if(token) {
            if(_.isString(token)) {
                getKey.key = token;
            }
            else {
                var moduleId = system.getModuleId(token);
                if(moduleId) {
                    var arr = moduleId.match(KEY_PATTERN);
                    getKey.key = arr[arr.length - 1];
                }
            }
        }

        if(!getKey.key) {
            throw new Error('Can\'t resolve');
        }

        return getKey.key;
    }

    RequestService.prototype.get = function(data, keys) {
        var requestOptions = this._getDefaultOptions("GET", { keys: keys }, data);
        return performRequest(requestOptions);
    };

    RequestService.prototype.post = function(data, keys) {
        var requestOptions = this._getDefaultOptions("POST", { keys: keys }, data);
        return performRequest(requestOptions);
    };

    RequestService.prototype.put = function(data, options) {
        var requestOptions = this._getDefaultOptions("PUT", options, data);
        return performRequest(requestOptions);
    };

    RequestService.prototype.delete = function(options) {
        var requestOptions = this._getDefaultOptions("DELETE", options);
        return performRequest(requestOptions);
    }

    RequestService.prototype._getDefaultOptions = function(method, options, data) {
        return {
            method: method,
            token: this._token,
            data: data,
            additionalKeys: options.keys,
            tweakUrlCallback: !_.isUndefined(options.id) && function(url) {
                return url + '/' + options.id
            }
        };
    }

    function performRequest(options) {
        var mainKey = getKey(options.token);
        var url = resolveUrl(mainKey, options.method, options.additionalKeys);
        if(_.isFunction(options.tweakUrlCallback)) {
            url = options.tweakUrlCallback(url);
        }
        var data = options.data;
        if(_.isString(data)) {
            data = '=' + data;
        }
        var promise = $.ajax({
            url: url,
            method: options.method,
            data: data,
        });

        return promise;
    }

    function resolveUrl(key, method, additionalKeys) {
        if(!key && !addionalKeys) {
            throw new Error('cant resolve url');
        }

        var url;
        if(!additionalKeys) {
            url = DEFAULT_BASEPATH + key;
        }
        else {
            var keyArr = additionalKeys.split('.');
            if(!key) {
                key = keyArr.unshift();
            }

            if(_.has(apis, key)) {
                url = apis[key][method.toLowerCase()];
            }
            if(_.isObject(url)) {
                if(keyArr.length == 0) {
                    url = url.default;
                }
                else {
                    while(keyArr.length > 0) {
                        var segment = keyArr.shift();
                        url = url[segment];
                    }
                }
            }
        }

        return url;
    }

    return RequestService;
});