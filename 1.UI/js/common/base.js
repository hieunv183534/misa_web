
class BaseJS {
    constructor() {
        this.loadData();
    }

    loadData() {
        // lấy thông tin các cột dữ liệu
        var cols = $('.grid table.tb-head thead th');

        //Lấy thông tin tương ứng với các cột để map vào

        $.ajax({
            url: "http://cukcuk.manhnv.net/v1/Employees",
            method: 'GET'
        }).done(function (res) {
            $.each(res, function (index, obj) {
                var tr = $(`<tr></tr>`);
                $.each(cols, function (index, th) {
                    var td = $(`<td></td>`);
                    var fieldName = $(th).attr('fieldName');
                    var value = obj[fieldName];
                    $(td).append(value);
                    $(tr).append(td);
                })
                debugger;
                $('.grid table.tb-body tbody').append(tr);
            })
        }).fail(function (res) {

        });
        
    }
}