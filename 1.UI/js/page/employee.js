flagAdd = 1;
myEmployeeId = '';


$(document).ready(function () {
    new EmployeeJS();
})


/**
 * Class quản lí các sự kiện cho trang employee
 * */
class EmployeeJS extends BaseJS {
    static checkedEmployees = [];
    constructor() {
        super();
        this.initEvent();
        this.loadDropdown();
    }

    setDataUrl() {
        this.dataUrl = "http://cukcuk.manhnv.net/v1/Employees";
    }

    loadDropdown() {
        //bind dữ liệu lên các dropdown
        //1.Dropdown phòng ban
        loadDropdownData("Department");
        //2.Dropdown vị trí
        loadDropdownData("Position");
    }

    initEvent() {
        //1.Sự kiện khi click thêm mới
        $('#btnAdd').click(btnAddOnClick);

        //2.Sự kiện khi click exit trên form thông tin
        $('#btnExit').click(btnExitOnClick);

        //3.Sự kiện khi click save trên form thông tin
        $('#btnSave').click(btnSaveOnClick);

        //4.Sự kiện khi click hủy trên form thông tin
        $('#btnCancel').click(btnCancelOnClick);

        //5.Sự kiện khi click refresh trên form thông tin
        $('#btnRefresh').click(btnRefreshOnClick);

        //6.Sự kiện khi dbclick 1 hàng trên bảng
        $('.grid table').on('dblclick', 'tbody tr', tableRowOnDbClick);

        //7.Sự kiện khi click vào toggle
        $('#btnToggle').click(btnToggleOnClick);

        //8.Validate dữ liệu
        // Các trường bắt buộc nhập:Mã nhân viên, họ và tên, cmnd, số điện thoại, email
        $('input[required]').blur(requiredNote);

        //9.Email đúng định dạng
        $('input#inputEmail').blur(checkEmail);

        //10. Lương đúng định dạng
        $('input#inputSalary').blur(checkSalary);

        //11. Xóa các nhân viên được chọn
        $('#btnDelete').click(btnDeleteOnClick);

        //12.Format Salary when inputSalary keydown
        $('input#inputSalary').keyup(inputSalaryOnKeyup)
    }
}


// functions event here


function inputSalaryOnKeyup() {
    var valueSalary = $(this).val().replaceAll('.', '');
    $(this).val(formatSalary(valueSalary));
}

function btnAddOnClick(e) {
    flagAdd = 1;
    $('.dialog').css("visibility", "visible");
    $('.dialog input').val(null);
    $('.dialog input').removeClass('border-red');
    $('.autofocus').focus();
    //lấy mã nhân viên mới binding vào form
    $.ajax({
        url: "http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode",
        method: 'GET',

    }).done(res => {
        $('#inputEmployeeCode').val(res);
    }).fail(res => {

    });
}

function btnExitOnClick(e) {
    $('.dialog').css("visibility", "hidden");
}

// Thực hiện lưu dũe liệu
function btnSaveOnClick(e) {
    var myUrl = "http://cukcuk.manhnv.net/v1/Employees";
    var method = 'POST';
    //console.log(flagAdd);
    //console.log(myEmployeeId);
    if (flagAdd != 1) {
        myUrl = "http://cukcuk.manhnv.net/v1/Employees/" + myEmployeeId;
        method = 'PUT';
    }

    var email = $('#inputEmail').val();
    var salary = $('#inputSalary').val();
    if (validateEmail(email) && $.isNumeric(salary)) {
        var employee = {};
        employee.EmployeeCode = $('#inputEmployeeCode').val();
        employee.FullName = $('#inputFullName').val();
        employee.DateOfBirth = $('#inputDateOfBirth').val();
        employee.Gender = ($('#inputGenderName').attr('value') == "Nam") ? 1 : 0;
        employee.IdentityNumber = $('#inputIdentityNumber').val();
        employee.IdentityDate = $('#inputIdentityDate').val();
        employee.IdentityPlace = $('#inputIdentityPlace').val();
        employee.Email = $('#inputEmail').val();
        employee.PhoneNumber = $('#inputPhoneNumber').val();
        employee.PositionId = $('#inputPositionName').attr('value');
        employee.DepartmentId = $('#inputDepartmentName').attr('value');
        employee.PersonalTaxCode = $('#inputPersonalTaxCode').val();
        employee.Salary = $('#inputSalary').val();
        employee.JoinDate = $('#inputJoinDate').val();
        employee.WorkStatus = ($('#inputWorkStatus').attr('value') == "Đang làm việc") ? 1 : 0;


        // gọi ajax post dữ liệu
        $.ajax({
            url: myUrl,
            method: method,
            data: JSON.stringify(employee),
            dataType: 'json',
            contentType: 'application/json'
        }).done(res => {
            if (flagAdd == 1) {
                alert('Thêm mới thành công!');
            } else {
                alert('Cập nhật thông tin thành công!');
            }
        }).fail(res => {
            alert("Đã có lỗi xảy ra!");
        });

        // Ẩn dialog đi và load lại dữ liệu
        setTimeout(loadData, 2000);
        $('.dialog').css("visibility", "hidden");
    } else {
        alert("Vui lòng kiểm tra lại email và lương!");
    }

}


/**
 * Xóa những bản ghi được chọn khi click vào btnDelete
 * */
function btnDeleteOnClick() {
    var employees = EmployeeJS.checkedEmployees;
    var resSuccess = 0;
    if (employees.length == 0) {
        alert("Bạn chưa chọn bản ghi nào cả!");
    } else {
        $.each(employees, function (index, item) {
            $.ajax({
                url: "http://cukcuk.manhnv.net/v1/Employees/" + item,
                method: 'DELETE'
            }).done(res => {
                resSuccess++;
                EmployeeJS.checkedEmployees = EmployeeJS.checkedEmployees.filter(e => e !== item);
            }).fail(res => {
                console.log(res);
            })
        })
        setTimeout(() => {
            loadData();
            alert("Đã xóa thành công" + resSuccess + "/" + (employees.length) + "bản ghi được chọn!");
            $('#btnDelete').css('visibility', 'hidden');
        }, 1000)
    }
}

function btnCancelOnClick(e) {
    $('.dialog').css("visibility", "hidden");
}

function btnRefreshOnClick(e) {
    loadData();
}

/**
 * biding thông tin nhân viên được chọn lên form
 * @param {any} e
 */
function tableRowOnDbClick(e) {
    flagAdd = 0;
    try {
        // Gọi api lấy dữ liệu
        const employeeId = $(this).attr('idobj');
        myEmployeeId = employeeId;
        var myUrl = "http://cukcuk.manhnv.net/v1/Employees/" + employeeId;
        $.ajax({
            url: myUrl,
            method: 'GET'
        }).done(function (res) {
            var employee = res;
            $('#inputEmployeeCode').val(res['EmployeeCode']);
            $('#inputFullName').val(res['FullName']);
            $('#inputDateOfBirth').val(formatDateToValue(res['DateOfBirth']));
            $('#inputGenderName').attr('value');
            //match drropdown GenderName
            var value = res['GenderName'];
            matchItemDropdown('GenderName', value);
            $('#inputIdentityNumber').val(res['IdentityNumber']);
            $('#inputIdentityDate').val(formatDateToValue(res['IdentityDate']));
            $('#inputIdentityPlace').val(res['IdentityPlace']);
            $('#inputEmail').val(res['Email']);
            $('#inputPhoneNumber').val(res['PhoneNumber']);
            //match dropdown PostionName
            var value = res['PositionId'];
            matchItemDropdown('PositionName', value);
            //match dropdown DepartmentName
            var value = res['DepartmentId'];
            matchItemDropdown('DepartmentName', value);
            $('#inputPersonalTaxCode').val(res['PersonalTaxCode']);
            $('#inputSalary').val(res['Salary']);
            $('#inputJoinDate').val(formatDateToValue(res['JoinDate']));
            //match dropdown WorkStatus
            var value = res['WorkStatus'];
            matchItemDropdown('WorkStatus', value);
        }).fail(function (res) {

        })

        $('.dialog').css("visibility", "visible");
        $('.autofocus').focus();
    } catch (e) {

    }
}

/**
 * to collase menu when click toggle
 * @param {any} e
 */
function btnToggleOnClick(e) {
    /*e.preventDefault();*/
    if ($('.menu').width() > 50) {
        $('.menu').width(48);
        $('.menu p').hide();
        $('.menu .menu-header .logo').hide();
        $(".content").animate(
            {
                left: "52px",
                width: "100%",
            },
            0,
            () => {
                $(".content").css("width", "calc(100% - 52px)")
            }
        )
    } else {
        $('.menu').width(225);
        setTimeout(function () {
            $('.menu p').show();
            $('.menu .menu-header .logo').show();
        }, 500);
        $(".content").animate(
            {
                left: "227px",
            },
            0,
            () => {
                $(".content").css("width", "calc(100% - 227px)")
            }
        )
    }
}

/**
 * Cảnh báo người dùng khi bỏ trống ô bắt buộc
 * */
function requiredNote() {
    let value = $(this).val();
    if (value == '') {
        // chuyển border thành màu đỏ cảnh báo và khi hover hiện thông tin cảnh báo
        $(this).addClass('border-red');
        $(this).attr('title', 'Thông tin này bắt buộc nhập!');
    } else {
        $(this).removeClass('border-red');
        $(this).removeAttr('title');
    }
}

/**
 * warning if invalid email
 * Author hieunv (21/07/2021)
 * */
function checkEmail() {
    var email = $(this).val()
    if (!validateEmail(email)) {
        // chuyển border thành màu đỏ cảnh báo và khi hover hiện thông tin cảnh báo
        $(this).addClass('border-red');
        $(this).attr('title', 'Email không đúng đinh dạng!');
    } else {
        $(this).removeClass('border-red');
        $(this).removeAttr('title');
    }
}

function checkSalary() {
    var salary = $(this).val()
    if (!$.isNumeric( salary.replaceAll('.',''))) {
        // chuyển border thành màu đỏ cảnh báo và khi hover hiện thông tin cảnh báo
        $(this).addClass('border-red');
        $(this).attr('title', 'Salary không đúng đinh dạng!');
    } else {
        $(this).removeClass('border-red');
        $(this).removeAttr('title');
    }
}

/**
 * hàm load dữ liệu
 * Author: hieunv (21/07/2021)
 * */
function loadData() {
    $('.grid table tbody').empty();
    try {
        this.dataUrl = "http://cukcuk.manhnv.net/v1/Employees";
        // lấy thông tin các cột dữ liệu
        var cols = $('.grid table thead th');

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
                    } else if (fieldName == 'check') {
                        var value = $(`<input onclick="checkcheck($(this))" type="checkbox" style="width:46px; height:24px;"/>`);
                    } else {
                        var value = obj[fieldName];
                    }
                    var formatType = $(th).attr('formatType');
                    switch (formatType) {
                        case 'ddmmyyyy':
                            value = formatDate(value);
                            break;
                        case 'money':
                            value = formatSalary(value);
                            break;
                        default:
                            break;
                    }

                    $(td).append(value);
                    $(tr).append(td);
                })
                tr.attr('bgindex', index);
                tr.attr('idobj', obj.EmployeeId);

                $('.grid table tbody').append(tr);
            })
        }).fail(function (res) {

        });
    } catch (e) {

    }
}

/**
 * Load dữ liệu lên dropdown có tên trường tương ứng với fieldName
 * Auto select item đầu tiên trong sanh sách item
 * @param {any} fieldName
 */
function loadDropdownData(fieldName) {
    var myUrl = '';
    if (fieldName == "Department") myUrl = "http://cukcuk.manhnv.net/api/Department";
    else if (fieldName == "Position") myUrl = "http://cukcuk.manhnv.net/v1/Positions";
    $.ajax({
        url: myUrl,
        method: 'GET'
    }).done(res => {
        $(`#input${fieldName}Name .dropdown-data`).empty();
        $(`#input${fieldName}Name .dropdown-main p`).empty();
        $(`#input${fieldName}Name .dropdown-main p`).append(res[0][`${fieldName}Name`]);
        $(`#input${fieldName}Name`).attr("value", `${res[0][`${fieldName}Id`]}`);
        $.each(res, function (idex, item) {
            const name = item[`${fieldName}Name`];
            const id = item[`${fieldName}Id`];
            let dropdownItemHTML = $(`<div valueid="${id}" valuename="${name}" class="dropdown-item">
                                            <i class="fas fa-check"></i>
                                            <p>${name}</p>
                                        </div>`);
            $(`#input${fieldName}Name .dropdown-data`).append(dropdownItemHTML);
            if (id == $(`#input${fieldName}Name`).attr('value')) {
                dropdownItemHTML.addClass('item-selected');
            }
        })
    })
}

function matchItemDropdown(fieldName, value) {
    myDropdownItems = $(`#input${fieldName} .dropdown-data .dropdown-item`);
    $(`#input${fieldName}`).attr('value', value);
    $.each(myDropdownItems, function (index, item) {
        $(item).removeClass('item-selected');
        if (value == $(item).attr('valueid')) {
            $(item).addClass('item-selected');
            $(`#input${fieldName} .dropdown-main p`).empty();
            $(`#input${fieldName} .dropdown-main p`).append($(item).attr('valuename'));
        }
    });
}