define(['lodash', 'durandal/system', 'viewmodels/trainings/word-translation'], function(_, system) {

    var ctors = _.transform(Array.prototype.slice.call(arguments, 2), function(result, val) {
        var key = system.getModuleId(val);
        key = _.last(key.split('/'));
        result[key] = val;
    })

    return {
        getActivator: function(key) {
            var Ctor = ctors[key];
            return {
                activate: function(words) {
                    var result = null;
                    if(Ctor) {
                        result = new Ctor(words);
                    }

                    return result;
                }
            };
        }
    }
});