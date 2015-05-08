define(function() {
    return {
        object: {
            inherit: function(ctor, baseCtor) {
                var F = function() { }
                F.prototype = baseCtor.prototype;

                ctor.prototype = new F();
                ctor.prototype.constructor = ctor;
                ctor.baseClass = baseCtor.prototype;
            }
        }
    };
});