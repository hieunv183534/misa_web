/**------------------------------------------------------------------
 * Hàm format ngày tháng
 * @param {any} _date
 */
function formatDate(_date) {
    if (_date != null) {
        var date = new Date(_date);
        var day = date.getDate();
        day = (day < 10) ? '0' + day : day;
        var month = date.getMonth() + 1;
        month = (month < 10) ? '0' + month : month;
        var year = date.getFullYear();
        return day + '/' + month + '/' + year;
    }
    else {
        return '';
    }
}


/**-------------------------------------------------------------------
 * Hàm format lương
 * @param {any} _salary
 */
function formatSalary(_salary) {
    if (_salary != null) {
        var salary = _salary.toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1.");
        return salary;
    } else {
        return '';
    }
}
