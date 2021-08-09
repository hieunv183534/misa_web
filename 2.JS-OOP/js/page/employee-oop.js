myEmployee = '';

$(document).ready(function () {
    myEmployee = new EmployeeJS();
})




class EmployeeJS {
    checkedEmployees = [];
    flagAdd = 1;
    myEmployeeId = '';

    constructor() {
        this.loadData();
        this.loadDropdown();
        this.initEvent();
    }

    /**
    * hàm load dữ liệu
    * Author: hieunv (21/07/2021)
    * */
    loadData() {
        var seft = this;
        $('.grid table tbody').empty();
        try {
            this.dataUrl = "http://cukcuk.manhnv.net/v1/Employees";

            //Lấy thông tin tương ứng với các cột để map vào
            $.ajax({
                url: this.dataUrl,
                method: 'GET'
            }).done(function (res) {
                seft.bindingData(res);
            }).fail(function (res) {
            });
        } catch (e) {
            console.log(e);
        }
    }


    /**
    * Binding data to table
    * @param {any} objs
    */
    bindingData(objs) {
        // lấy thông tin các cột dữ liệu
        var cols = $('.grid table thead th');
        $.each(objs, function (index, obj) {
            var tr = $(`<tr></tr>`);
            $.each(cols, function (index, th) {
                var td = $(`<td></td>`);
                var fieldName = $(th).attr('fieldName');
                if (fieldName == 'WorkStatus') {
                    var value = (obj[fieldName] == 1) ? "Đang làm việc" : "Đã nghỉ việc";
                } else if (fieldName == 'check') {
                    var value = $(`<input type="checkbox" style="width:46px; height:24px;"/>`);
                } else {
                    var value = obj[fieldName];
                }
                var formatType = $(th).attr('formatType');
                switch (formatType) {
                    case 'ddmmyyyy':
                        value = CommonJS.formatDate(value);
                        break;
                    case 'money':
                        value = CommonJS.formatSalary(value);
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
        });
    }


    getRandomInt(a, b) {
        return Math.random() * (b - a) + a;
    }


    /**
     * Khởi tạo các sự kiện cho trang employee
     * Author hieunv 26/07/2021
     * */
    initEvent() {
        var seft = this;
        //1.Sự kiện khi click thêm mới
        $('#btnAdd').click(


            () => {
                var number = 200;
            var Ho = ['Lê', 'Nguyễn', 'Trần', 'Lê Hữu', 'Lý', 'Lương', 'Phạm', 'Đỗ', 'Võ', 'Lưu', 'Đậu', 'Trương', 'Hồ', 'Văn', 'Mạc'];
            var TenLot = ['Ngọc', 'Đức', 'Văn', 'Việt', 'Tiến', 'Thị', 'Khánh', 'Thu', 'Thủy'];
            var Ten = ['Hưng', 'Hùng', 'Dũng', 'Lan', 'Thu', 'Huyền', 'Trâm', 'Huy', 'Đức', 'Long', 'Tú', 'Tùng', 'Đức', 'Ngọc', 'Loan', 'Hường', 'Tú', 'Lan', 'Mạnh'];

                var dep = [
                    {
                        "DepartmentId": "142cb08f-7c31-21fa-8e90-67245e8b283e",
                        "DepartmentCode": "PB99",
                        "DepartmentName": "Phòng Marketting",
                        "Description": "Praesentium excepturi architecto ipsum possimus. Dolore molestiae omnis nihil. Aliquid perspiciatis qui. Ea sed nam; accusantium ipsam ut. Soluta.",
                        "CreatedDate": "1970-01-01T00:07:13",
                        "CreatedBy": "Letha Bolt",
                        "ModifiedDate": null,
                        "ModifiedBy": null
                    },
                    {
                        "DepartmentId": "17120d02-6ab5-3e43-18cb-66948daf6128",
                        "DepartmentCode": "PB89",
                        "DepartmentName": "Phòng đào tạo",
                        "Description": "Neque amet ut. Natus quis ratione. Itaque tempore ut. Enim impedit magnam. Quo consectetur temporibus! Excepturi debitis perspiciatis. Quis et expedita!",
                        "CreatedDate": "1972-09-20T22:18:35",
                        "CreatedBy": "Miyoko Mckinney",
                        "ModifiedDate": null,
                        "ModifiedBy": null
                    },
                    {
                        "DepartmentId": "469b3ece-744a-45d5-957d-e8c757976496",
                        "DepartmentCode": "PB86",
                        "DepartmentName": "Phòng Nhân sự",
                        "Description": "Quam odit provident dolor. Natus error velit consequatur hic vero ut! Est nemo molestiae adipisci qui quia ipsam.",
                        "CreatedDate": "1991-02-12T05:09:15",
                        "CreatedBy": "Vanita Kelleher",
                        "ModifiedDate": null,
                        "ModifiedBy": null
                    },
                    {
                        "DepartmentId": "4e272fc4-7875-78d6-7d32-6a1673ffca7c",
                        "DepartmentCode": "PB92",
                        "DepartmentName": "Phòng Công nghệ",
                        "Description": "Qui tempora nisi similique laboriosam illum nesciunt. Unde similique omnis voluptatem sit nisi ipsum. Illum accusantium sit quia quidem; in et fuga.",
                        "CreatedDate": "1986-10-11T05:25:20",
                        "CreatedBy": "Marcos Abraham",
                        "ModifiedDate": null,
                        "ModifiedBy": null
                    }
                ];
                var pos = [
                    {
                        "PositionId": "30d41e52-5e66-72bc-6c1c-b47866e0b131",
                        "PositionCode": "P08",
                        "PositionName": "Giám đốc",
                        "Description": "Ut aut consectetur officia error dolor sequi.",
                        "ParentId": null,
                        "CreatedDate": "1970-01-01T00:00:55",
                        "CreatedBy": "Antoine Banks",
                        "ModifiedDate": null,
                        "ModifiedBy": null
                    },
                    {
                        "PositionId": "548dce5f-5f29-4617-725d-e2ec561b0f41",
                        "PositionCode": "P94",
                        "PositionName": "Nhân viên",
                        "Description": "Beatae modi aliquam aperiam atque soluta. Qui blanditiis amet. Dolorem expedita ut. Perspiciatis eos qui. Eos omnis eum; non aut unde.",
                        "ParentId": null,
                        "CreatedDate": "1970-09-09T01:45:48",
                        "CreatedBy": "Chaya Mccaffrey",
                        "ModifiedDate": null,
                        "ModifiedBy": null
                    },
                    {
                        "PositionId": "589edf01-198a-4ff5-958e-fb52fd75a1d4",
                        "PositionCode": "P07",
                        "PositionName": "Phó phòng",
                        "Description": "Maiores totam facere. Numquam error earum incidunt et laudantium accusamus. Dolor sint perspiciatis. Voluptas repellendus soluta; labore nisi dicta. Quia.",
                        "ParentId": null,
                        "CreatedDate": "1987-02-24T22:38:53",
                        "CreatedBy": "Aundrea Cahill",
                        "ModifiedDate": null,
                        "ModifiedBy": null
                    },
                    {
                        "PositionId": "5bd71cda-209f-2ade-54d1-35c781481818",
                        "PositionCode": "P92",
                        "PositionName": "Trưởng phòng",
                        "Description": "Dolorum animi hic recusandae non laudantium voluptatem. Modi dicta magni et autem magni eum.",
                        "ParentId": null,
                        "CreatedDate": "2006-08-16T13:18:07",
                        "CreatedBy": "Earle Caudill",
                        "ModifiedDate": null,
                        "ModifiedBy": null
                    }
                ];
            var settings = {
                "url": "http://cukcuk.manhnv.net/v1/Employees",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify({
                    "createdDate": "2021-08-07T14:30:23.591Z",
                    "createdBy": "Hieunv",
                    "modifiedBy": "Hieunv",
                    "employeeId": "0760cdff-e8a0-11eb-94eb-42010a8c0002",
                    "employeeCode": `NV-${number}`,
                    "fullName": `${Ho[this.getRandomInt(0, Ho.length)]} ${TenLot[this.getRandomInt(0, TenLot.length)]} ${Ten[this.getRandomInt(0, Ten.length)]}`,
                    "gender": this.getRandomInt(1, 4),
                    "dateOfBirth": "2000-01-08T12:00:23.591Z",
                    "phoneNumber": `0${Math.round(Math.random() * 1000000000)}`,
                    "email": `nhanvien.MF${number}@misa.cukcuk.vn`,
                    "address": "TH",
                    "identityNumber": `0382${Math.round(Math.random() * 100000000)}`,
                    "identityDate": "2021-07-19T14:30:23.591Z",
                    "identityPlace": "AMERICA",
                    "joinDate": "2021-07-19T14:30:23.591Z",
                    "workStatus": this.getRandomInt(1, 4),
                    "personalTaxCode": `${Math.round(Math.random() * 10000000)}`,
                    "salary": Math.floor(Math.random() * 1000000000),
                    "positionId": pos[this.getRandomInt(0, pos.length) % pos.length].PositionId,
                    "departmentId": dep[this.getRandomInt(0, dep.length) % dep.length].DepartmentId,
                }),
            };

            $.ajax(settings).done(function (response) {
                console.log(number);
            });
        }


            /*function () {
            seft.btnAddOnClick(seft, this);
        }*/);

        //2.Sự kiện khi click exit trên form thông tin
        $('#btnExit').click(function () {
            seft.btnExitOnClick(seft, this);
        });

        //3.Sự kiện khi click save trên form thông tin
        $('#btnSave').click(function () {
            seft.btnSaveOnClick(seft, this);
        });

        //4.Sự kiện khi click hủy trên form thông tin
        $('#btnCancel').click(function () {
            seft.btnCancelOnClick(seft, this);
        });

        //5.Sự kiện khi click refresh trên form thông tin
        $('#btnRefresh').click(function () {
            seft.btnRefreshOnClick(seft, this);
        });


        //6.Sự kiện khi dbclick 1 hàng trên bảng
        $('.grid table').on('dblclick', 'tbody tr', function () {
            seft.tableRowOnDbClick(seft, this);
        });

        //14.Click vào checkbox trên các hàng
        $('.grid table tbody').on('click', 'tr td input', function () {
            seft.checkcheck(seft, this);
        });

        //7.Sự kiện khi click vào toggle
        $('#btnToggle').click(function () {
            seft.btnToggleOnClick(seft, this);
        });

        //8.Validate dữ liệu
        // Các trường bắt buộc nhập:Mã nhân viên, họ và tên, cmnd, số điện thoại, email
        $('input[required]').blur(function () {
            seft.requiredNote(seft, this);
        });

        //9.Email đúng định dạng
        $('input#inputEmail').blur(function () {
            CommonJS.checkEmail(seft, this);
        });

        //10. Lương đúng định dạng
        $('input#inputSalary').blur(function () {
            CommonJS.checkSalary(seft, this);
        });

        //11. Xóa các nhân viên được chọn
        $('#btnDelete').click(function () {
            seft.btnDeleteOnClick(seft, this);
        });

        //12.Format Salary when inputSalary keydown
        $('input#inputSalary').keyup(function () {
            seft.inputSalaryOnKeyup(seft, this);
        });

        //13.Click vào nút exit trên popup
        $('.popup button.btn-exit').click(() => {
            $('.popup').css('display', 'none');
        })


    }

    /**
     * Đổ dữ liệu vào các dropdown
     * Author hieunv 26/07/2021
     * */
    loadDropdown() {
        //1.Dropdown phòng ban form
        this.loadDropdownData("Department", 0)
        //2.Dropdown vị trí form
        this.loadDropdownData("Position", 0);

        //3.Dropdown phòng ban filter
        this.loadDropdownData("Department", 1);
        //4.Dropdown vị trí filter
        this.loadDropdownData("Position", 1);
    }

    /**
    * Load dữ liệu lên dropdown có tên trường tương ứng với fieldName
    * Auto select item đầu tiên trong sanh sách item
    * @param {any} fieldName
     */
    loadDropdownData(fieldName, isFilter) {
        var myUrl = '';
        if (fieldName == "Department") myUrl = "http://cukcuk.manhnv.net/api/Department";
        else if (fieldName == "Position") myUrl = "http://cukcuk.manhnv.net/v1/Positions";
        if (isFilter == 1) fieldName += '1';
        $.ajax({
            url: myUrl,
            method: 'GET'
        }).done(res => {
            $(`#input${fieldName}Name .dropdown-data`).empty();
            $(`#input${fieldName}Name .dropdown-main p`).empty();
            if (isFilter == 1) {
                var name = (fieldName == "Position1") ? "Tất cả vị trí" : "Tất cả phòng ban";
                $(`#input${fieldName}Name`).attr("value", '');
                let dropdownItemHTML = $(`<div valueid="" valuename="${name}" class="dropdown-item item-selected">
                                            <i class="fas fa-check"></i>
                                            <p>${name}</p>
                                        </div>`);
                $(`#input${fieldName}Name .dropdown-data`).append(dropdownItemHTML);
                $(`#input${fieldName}Name .dropdown-main p`).append(name);
                $(`#input${fieldName}Name`).attr("value", '');
            } else {
                $(`#input${fieldName}Name .dropdown-main p`).append(res[0][`${fieldName}Name`]);
                $(`#input${fieldName}Name`).attr("value", `${res[0][`${fieldName}Id`]}`);
            }
            $.each(res, function (idex, item) {
                var id = '';
                var name = '';
                if (isFilter == 1) {
                    name = (fieldName == "Position1") ? item[`PositionName`] : item[`DepartmentName`];
                    id = (fieldName == "Position1") ? item[`PositionId`] : item[`DepartmentId`];
                } else {
                    name = item[`${fieldName}Name`];
                    id = item[`${fieldName}Id`];
                }
                let dropdownItemHTML = $(`<div valueid="${id}" valuename="${name}" class="dropdown-item">
                                            <i class="fas fa-check"></i>
                                            <p>${name}</p>
                                        </div>`);
                $(`#input${fieldName}Name .dropdown-data`).append(dropdownItemHTML);
                if (id == $(`#input${fieldName}Name`).attr('value')) {
                    dropdownItemHTML.addClass('item-selected');
                }
            });
        })
    }

    /**
     * Xử lí sự kiện khi click vào button add
     * @param {any} e
     * Author hieunv 26/07/2021
     */
    btnAddOnClick(seft, thisElement) {
        console.log(seft);
        seft.flagAdd = 1;
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


    /**
     * Xử lí sư kiện khi click button delete
     * @param {any} seft
     * @param {any} thisElement
     * Author hieunv 27/07/2021
     */
    btnDeleteOnClick(seft, thisElement) {
        var employees = seft.checkedEmployees;
        var delSuccess = 0;
        $('.popup').css('display', 'block');
        PopupJS.showPopup('danger', 'Bạn có chắc chắn muốn xóa các bản ghi được chọn?', "Xóa các bản ghi!");
        $('.popup button.btn-z').click(() => {
            $.each(employees, function (index, item) {
                $.ajax({
                    url: "http://cukcuk.manhnv.net/v1/Employees/" + item,
                    method: 'DELETE'
                }).done(res => {
                    delSuccess++;
                    seft.checkedEmployees = seft.checkedEmployees.filter(e => e !== item);
                    seft.loadData();
                    ToolTipJS.showMes('success', "Đã xóa thành công " + delSuccess + "/" + (employees.length) + " bản ghi!")
                    $('#btnDelete').css('visibility', 'hidden');
                    $('.popup').css('display', 'none');
                }).fail(res => {
                    $('.popup').css('display', 'none');
                })
            })
        })
        $('.popup button.btn-y').click(() => {
            $('.popup').css('display', 'none');
        })
    }


    /**
     * Xử lí sự kiện khi click vào button exit trên form
     * @param {any} e
     */
    btnExitOnClick(seft, thisElement) {
        $('.dialog').css("visibility", "hidden");
    }

    /**
     * Xử lí sự kiện khi click button cancel
     * @param {any} seft
     * @param {any} thisElement
     * Author hieunv 26/07/2021
     */
    btnCancelOnClick(seft, thisElement) {
        $('.dialog').css("visibility", "hidden");
    }

    /**
     * Xử lí sự kiện khi click button refresh
     * @param {any} seft
     * @param {any} thisElement
     */
    btnRefreshOnClick(seft, thisElement) {
        seft.checkedEmployees = [];
        seft.loadData();
    }

    /**
     * Xử lí sự kiện khi click vào button save
     */
    btnSaveOnClick(seft, thisElement) {
        var myUrl = "http://cukcuk.manhnv.net/v1/Employees";
        var method = 'POST';
        if (seft.flagAdd != 1) {
            myUrl = "http://cukcuk.manhnv.net/v1/Employees/" + seft.myEmployeeId;
            method = 'PUT';
        }
        var employeeCode = $('#inputEmployeeCode').val();
        var fullName = $('#inputFullName').val();
        var identityNumber = $('#inputIdentityNumber').val();
        var email = $('#inputEmail').val();
        var phoneNumber = $('#inputPhoneNumber').val();
        if (!(employeeCode == '' || fullName == '' || identityNumber == '' || email == '' || phoneNumber == '')) {
            var mes = 'Bạn có chắc chắn muốn câp nhật bản ghi này?';
            var title = 'Chỉnh sửa bản ghi!';
            if (seft.flagAdd == 1) {
                mes = 'Bạn có chắc chắn muốn thêm bản ghi này?';
                title = 'Thêm mới bản ghi!';
            }
            PopupJS.showPopup('warning', mes, title);
            $('.popup').css('display', 'block');
            $('.popup button.btn-x').click(() => {
                var employee = {};
                employee.EmployeeCode = $('#inputEmployeeCode').val();
                employee.FullName = $('#inputFullName').val();
                employee.DateOfBirth = $('#inputDateOfBirth').val();
                employee.Gender = $('#inputGenderName').attr('value');
                employee.IdentityNumber = $('#inputIdentityNumber').val();
                employee.IdentityDate = $('#inputIdentityDate').val();
                employee.IdentityPlace = $('#inputIdentityPlace').val();
                employee.Email = $('#inputEmail').val();
                employee.PhoneNumber = $('#inputPhoneNumber').val();
                employee.PositionId = $('#inputPositionName').attr('value');
                employee.DepartmentId = $('#inputDepartmentName').attr('value');
                employee.PersonalTaxCode = $('#inputPersonalTaxCode').val();
                employee.Salary = $('#inputSalary').val().replaceAll('.', '');
                employee.JoinDate = $('#inputJoinDate').val();
                employee.WorkStatus = $('#inputWorkStatus').attr('value');
                // gọi ajax post dữ liệu
                $.ajax({
                    url: myUrl,
                    method: method,
                    data: JSON.stringify(employee),
                    dataType: 'json',
                    contentType: 'application/json'
                }).done(res => {
                    $('.dialog').css("visibility", "hidden");
                    $('.popup').css('display', 'none');
                    seft.loadData();
                    if (seft.flagAdd == 1) {
                        ToolTipJS.showMes('success', 'Thêm mới bản ghi thành công!');
                    } else {
                        ToolTipJS.showMes('success', 'Cập nhật bản ghi thành công!');
                    }
                    $('.popup').css('display', 'none');
                }).fail(res => {
                    ToolTipJS.showMes('danger', 'Đã có lỗi xảy ra!');
                });
            })
        } else {
            ToolTipJS.showMes('danger', "Bạn chưa nhập đủ các trường bắt buộc!");
        }
    }

    /**
     * Hiển thị form và đổ dữ liệu vào form thông tin người được chọn
     * @param {any} seft
     * @param {any} thisElement
     * Author hieunv 26/07/2021
     */
    tableRowOnDbClick(seft, thisElement) {
        seft.flagAdd = 0;
        $('.dialog input').removeClass('border-red');
        try {
            // Gọi api lấy dữ liệu
            const employeeId = $(thisElement).attr('idobj');
            seft.myEmployeeId = employeeId;
            var myUrl = "http://cukcuk.manhnv.net/v1/Employees/" + employeeId;
            $.ajax({
                url: myUrl,
                method: 'GET'
            }).done(function (res) {
                var employee = res;
                $('#inputEmployeeCode').val(res['EmployeeCode']);
                $('#inputFullName').val(res['FullName']);
                $('#inputDateOfBirth').val(CommonJS.formatDateToValue(res['DateOfBirth']));
                $('#inputGenderName').attr('value');
                //match drropdown GenderName
                var value1 = res['Gender'];
                seft.matchItemDropdown('GenderName', value1);
                $('#inputIdentityNumber').val(res['IdentityNumber']);
                $('#inputIdentityDate').val(CommonJS.formatDateToValue(res['IdentityDate']));
                $('#inputIdentityPlace').val(res['IdentityPlace']);
                $('#inputEmail').val(res['Email']);
                $('#inputPhoneNumber').val(res['PhoneNumber']);
                //match dropdown PostionName
                var value2 = res['PositionId'];
                seft.matchItemDropdown('PositionName', value2);
                //match dropdown DepartmentName
                var value3 = res['DepartmentId'];
                seft.matchItemDropdown('DepartmentName', value3);
                $('#inputPersonalTaxCode').val(res['PersonalTaxCode']);
                var valueSalary = res['Salary'];
                $('#inputSalary').val(CommonJS.formatSalary(valueSalary));
                $('#inputJoinDate').val(CommonJS.formatDateToValue(res['JoinDate']));
                //match dropdown WorkStatus
                var value4 = res['WorkStatus'];
                seft.matchItemDropdown('WorkStatus', value4);
            }).fail(function (res) {

            })

            $('.dialog').css("visibility", "visible");
            $('.autofocus').focus();
        } catch (e) {

        }
    }

    /**
     * load dữ liệu của người được chọn lên các dropdown
     * @param {any} fieldName
     * @param {any} value
     * Author hieunv 26/07/2021
     */
    matchItemDropdown(fieldName, value) {
        var myDropdownItems = $(`#input${fieldName} .dropdown-data .dropdown-item`);
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


    /**
     * Cảnh báo khi bỏ trống trường bắt buộc
     * @param {any} seft
     * @param {any} thisElement
     * Author hieunv 26/07/2021
     */
    requiredNote(seft, thisElement) {
        let value = $(thisElement).val();
        if (value == '') {
            // chuyển border thành màu đỏ cảnh báo và khi hover hiện thông tin cảnh báo
            $(thisElement).addClass('border-red');
            $(thisElement).attr('title', 'Thông tin này bắt buộc nhập!');
            ToolTipJS.showMes('danger', 'Thông tin này bắt buộc nhập!')
        } else {
            $(thisElement).removeClass('border-red');
            $(thisElement).removeAttr('title');
        }
    }

    /**
     * xử lí sự kiện khi click vào checkbox trên các hàng
     * @param {any} seft
     * @param {any} thischeck
     */
    checkcheck(seft, thisElement) {
        var thischeck = $(thisElement);
        if (thischeck.is(":checked")) {
            thischeck.parents('tr').css('background-color', "#E3F3EE");
            seft.checkedEmployees.push($(thisElement).parents('tr').attr('idobj'));
            $('#btnDelete').css('visibility', 'visible');
        } else {

            if (thischeck.parents('tr').attr('bgindex') % 2 == 0) {
                thischeck.parents('tr').css('background-color', '#FFF3EB');
            } else {
                thischeck.parents('tr').css('background-color', "#ffffff");
            }

            seft.checkedEmployees = seft.checkedEmployees.filter(e => e !== thischeck.parents('tr').attr('idobj'));
            if (seft.checkedEmployees == 0) {
                $('#btnDelete').css('visibility', 'hidden');
            }
        }
    }


    /**
     * format tiềnlương khi input
     * @param {any} seft
     * @param {any} thisElement
     * Author hieunv 27/07/2021
     */
    inputSalaryOnKeyup(seft, thisElement) {
        var valueSalary = $(thisElement).val().replaceAll('.', '');
        $(thisElement).val(CommonJS.formatSalary(valueSalary));
    }

    /**
     * Xử lí menu collapse khi click button toggle
     * @param {any} seft
     * @param {any} thisElement
     * Author hieunv 27/07/2021
     */
    btnToggleOnClick(seft, thisElement) {
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
}