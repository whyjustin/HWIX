ko.extenders.datetime = function (target, format) {
    var result = ko.dependentObservable({
        read: function() {
            var date = getDateFromNETDate(target());
            return formatDate(date, format);
        },
        write: target
    });
    result.raw = target;
    return result;
};

ko.extenders.format = function(target, format) {
    var result = ko.dependentObservable({
        read: function() {
            if (format === "$") {
                return "$" + target().toLocaleString();
            } else {
                return target();
            }
        },
        write: target
    });
    result.raw = target;
    return result;
};

ko.subscribable.fn.percent = function (precision) {
    var target = this;

    var result = ko.computed({
        read: function () {
            return target();
        },
        write: target
    });

    result.formatted = ko.computed({
        read: function () {
            return '%' + (target() * 100).toFixed(precision);
        },
        write: target
    });

    return result;
};

ko.subscribable.fn.stripHref = function() {
    var target = this;

    var result = ko.computed({
        read: function() {
            return target();
        },
        write: target
    });

    result.formatted = ko.computed({
        read: function() {
            return target().replace(/ /g, '').replace(/\(/g, '').replace(/\)/g, '').replace(/\//g, '');
        },
        write: target
    });

    return result;
};

ko.subscribable.fn.shortenUrl = function () {
    var target = this;
    
    var result = ko.computed({
        read: function () {
            return target();
        },
        write: target
    });

    result.formatted = ko.computed({
        read: function () {
            var url = target();
            var form = url.replace('http://', '').replace('www.', '');
            if (form.length > 10) {
                return form.substring(0, 10) + '...';
            } else {
                return form;
            }
        },
        write: target
    });

    return result;
}