define(['lodash', 'durandal/system', 'common/constants',
    'viewmodels/trainings/revealers/image',
    /*'viewmodels/trainings/revealers/pronunciation'*/],
function(_, system, constants) {

    var container = _.reduce(Array.prototype.slice.call(arguments, 2), function(result, val) {
        var moduleId = system.getModuleId(val);
        var key = _.last(moduleId.split('/'));
        result[key] = val;

        return result;
    }, {});

    function getAvailableWorkers(keys) {
        var availableKeys = _.intersection(keys, _.keys(container));
        var result = _.map(availableKeys, function(key) {
            return {
                key: key,
                value: constants.wordAttributes[key.toUpperCase()]
            };
        });

        return result;
    }

    var RevealersManager = function(keys) {
        var me = this;

        var pairs = getAvailableWorkers(keys);
        this.revealers = _.map(pairs, function(pair) {
            var Ctor = container[pair.key];
            return new Ctor();
        });

        this.update = function(data) {
            _.forEach(me.revealers, function(revealer, idx) {
                var pair = pairs[idx];
                var localData = data[pair.key];
                revealer.update(localData);
            })
        };

        this.controller = function() {
            return {
                reveal: function(val) {
                    var revealer = _.find(me.revealers, function(r, idx) {
                        return pairs[idx].value === val;
                    });
                    if(revealer) {
                        revealer.reveal();
                    }
                },
                //revealAll: function() {}
            };
        }
    };

    return RevealersManager;
});