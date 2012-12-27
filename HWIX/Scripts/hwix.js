var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

function getDateFromNETDate(jsonDate) {
    var epochMilliseconds = jsonDate.replace(/^\/Date\(([0-9]+)([+-][0-9]{4})?\)\/$/, '$1');
    if (epochMilliseconds != jsonDate) {
        return new Date(parseInt(epochMilliseconds));
    }
}

function addYear(date, year) {
    return new Date(date.setFullYear(date.getFullYear() + year));
};

function addMonth(date, month) {
    return new Date(date.setMonth(date.getMonth() + month));
}

function formatDate(date, format) {
    return format.replace(/MMMM/g, monthNames[date.getMonth()]).replace(/yyyy/g, date.getFullYear()).replace(/dd/g, padLeft(date.getDate(), 2)).replace(/MM/g, padLeft(date.getMonth() + 1, 2));
}

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

function padLeft(number, length, str) {
    return Array(length - String(number).length + 1).join(str || '0') + number;
}