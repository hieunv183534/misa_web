

class BaseJS {
    constructor() {
        this.dataUrl = "";
        this.setDataUrl();
        this.loadData();
        this.initEvent();
    }

    setDataUrl() {

    }

    initEvent() {
        //Sự kiện khi click thêm mới
        $('#btnAdd').click(function (e) {
            $('.dialog').css("visibility", "visible");
            $('#autofocus').focus();
        })
        //Sự kiện khi click exit trên form thông tin
        $('#btnExit').click(function (e) {
            $('.dialog').css("visibility", "hidden");
        })
        //Sự kiện khi click save trên form thông tin
        $('#btnSave').click(function (e) {
            alert("Lưu dữ liệu");
        })
        //Sự kiện khi click hủy trên form thông tin
        $('#btnCancel').click(function (e) {
            $('.dialog').css("visibility", "hidden");
        })
        //Sự kiện khi click refresh trên form thông tin
        $('#btnRefresh').click(function (e) {
            alert("Tải lại dữ liệu");
        })
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
                        var value = obj[fieldName];
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
                    $('.grid table.tb-body tbody').append(tr);
                })
            }).fail(function (res) {

            });
        } catch (e) {

        }      
    }
}