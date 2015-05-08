define(['lodash',
    'durandal/system',
    'viewmodels/trainings/steps-training',
    'viewmodels/trainings/word-translation',
    'viewmodels/trainings/translation-word'],
function(_, system) {

    var ctors = _.transform(Array.prototype.slice.call(arguments, 2), function(result, val) {
        var key = system.getModuleId(val);
        key = _.last(key.split('/'));
        result[key] = val;
    })

    return {
        getConstructor: function(key) {
            var Ctor = ctors[key];
            return Ctor;
        }
    }
});