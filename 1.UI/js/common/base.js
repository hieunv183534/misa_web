
class BaseJS {
    constructor() {
        this.dataUrl = "";
        this.setDataUrl();
        this.loadData();
    }

    setDataUrl() {

    }

    loadData() {
        try {
            // lấy thông tin các cột dữ liệu
            var cols = $('.grid table.tb-head thead th');

            //Lấy thông tin tương ứng với các cột để map vào
            $.ajax({
                url: this.dataUrl,
                method: 'GET'
            }).done(function (res) {
                $.each(res, function (index, obj) {
                    var tr = $(`<tr></tr>`);
                    $.each(cols, function (index, th) {
                        var td = $(`<td></td>`);
                        var fieldName = $(th).attr('fieldName');
                        if (fieldName == 'WorkStatus') {
                            var value = (obj[fieldName] == 1) ? "Đang làm việc" : "Đã nghỉ việc";
                        } else {
                            var value = obj[fieldName];
                        }
                        var formatType = $(th).attr('formatType');
                        switch (formatType) {
                            case 'ddmmyyyy':
                                value = formatDate(value);
                                td = $(`<td style="text-align:center;"></td>`);
                                break;
                            case 'money':
                                value = formatSalary(value);
                                td = $(`<td style="text-align:right;"></td>`);
                                break;
                            default:
                                break;
                        }

                        $(td).append(value);
                        $(tr).append(td);
                    })
                    tr.attr('idobj', obj.EmployeeId);
                    $('.grid table.tb-body tbody').append(tr);
                })
            }).fail(function (res) {

            });
        } catch (e) {

        }      
    }
}