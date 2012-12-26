function getDateFromNETDate(jsonDate) {
    var epochMilliseconds = jsonDate.replace(/^\/Date\(([0-9]+)([+-][0-9]{4})?\)\/$/, '$1');
    if (epochMilliseconds != jsonDate) {
        return new Date(parseInt(epochMilliseconds));
    }
}

function subtractYear(date, year) {
    return new Date(date.setFullYear(date.getFullYear() - year));
};

function templateFromFormat(format) {
    if (format === "$") {
        return '$#= dataItem.toLocaleString() #';
    } else {
        return '#= value #';
    }
}

function labelFromFormat(format) {
    if (format === "$") {
        return "C";
    } else {
        return "{0}";
    }
}