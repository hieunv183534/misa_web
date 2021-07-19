$(document).ready(function () {
    //$(".employee-profile-dialog").hide();
    //$(".dialog-modal").hide();
    //$(".combobox .combobox-data").hide();

    //setTimeout(fakeData(), 100);

    new CustomerJS();
})

/**
 * Class quản lí các sự kiện cho trang employee
 * */
class CustomerJS extends BaseJS{
    constructor() {
        /*this.loadData();*/
        super();
    }

    /**
     * Load dữ liệu
     * */
    //loadData() {
    //    //lấy dữ liệu về

    //    $.ajax({
    //        url: "http://cukcuk.manhnv.net/v1/Employees",
    //        method: 'GET'
    //    }).done(function (res) {
    //        //binding dữ liệu lên trang web
    //        var data = res;
    //        $.each(data, function (index, item) {
    //            var dateOfBirth = item['DateOfBirth'];
    //            dateOfBirth = formatDate(dateOfBirth);
    //            var salary = item['Salary'];
    //            salary = formatSalary(salary);
    //            var tr = $(
    //                `<tr>
    //                <td>`+ item['EmployeeCode'] + `</td>
    //                <td>`+ item['FullName'] + `</td>
    //                <td>`+ dateOfBirth + `</td>
    //                <td>`+ item['GenderName'] + `</td>
    //                <td>`+ item['PhoneNumber'] + `</td>
    //                <td>`+ item['Email'] + `</td>
    //             </tr>`
    //            );
    //            $('.content .content-body .grid table.tb-body tbody').append(tr);
    //        })
    //    }).fail(function (res) {

    //    });
    //}

    /**
     * Thêm mới dữ liệu
     * */
    add() {

    }

    /**
     * Sửa dữ liệu
     * */
    edit() {

    }


    /**
     * Xóa dữ liệu
     * */
    delete() {

    }
}



//$(".content .content-body .grid table.tb-body tbody tr").dblclick(function () {
//    $(".employee-profile-dialog").show();
//    $(".dialog-modal").show();
//});

//$(".btn-cancel").click(function () {
//    $(".employee-profile-dialog").hide();
//    $(".dialog-modal").hide();
//});

//$(".btn-save").click(function () {
//    $(".employee-profile-dialog").hide();
//    $(".dialog-modal").hide();
//});

//$(".employee-profile-dialog .dialog-header button.btn-exit").click(function () {
//    $(".employee-profile-dialog").hide();
//    $(".dialog-modal").hide();
//});


//$(".combobox button").click(function () {
//    $(".combobox .combobox-data").show();
//})
//$(".combobox .combobox-item").click(function () {
//    $(".combobox .combobox-data").hide();
//})