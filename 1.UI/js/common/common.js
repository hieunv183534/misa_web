/**------------------------------------------------------------------
 * Hàm format ngày tháng
 * dạng ngày/tháng/năm
 * @param {any} _date
 * Author: hieunv 
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
 * Author: hieunv
 */
function formatSalary(_salary) {
    if (_salary != null) {
        var salary = _salary.toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1.");
        return salary;
    } else {
        return '';
    }
}


/**
 * Hàm kiểm tra email có đúng định dạng không
 * @param {any} email
 */
function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}
