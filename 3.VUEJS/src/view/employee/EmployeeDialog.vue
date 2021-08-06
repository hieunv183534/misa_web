<template>
    <div class="dialog" v-bind:class="{ 'dialog-show': isShow }">
        <div class="dialog-modal"></div>
        <div class="employee-profile-dialog">
            <DialogHeader dialogTitle="THÔNG TIN NHÂN VIÊN"
                          @btnExitOnClick="hideThisForm" />
            <div class="dialog-content">
                <div class="dialog-content-left">
                    <div class="avt-default"></div>
                    <p>Ảnh avatar ở đây</p>
                </div>
                <div class="dialog-content-right">
                    <div class="thongtinchung">
                        <DialogTitleInfo dialogTitleInfo="A. THÔNG TIN CHUNG" />
                        <div class="my-row">
                            <div class="small-info">
                                <p>Mã nhân viên (<font color="red">*</font>)</p>
                                <Input inputType="text"
                                       inputPlacehoder=""
                                       :inputValue="employeeCode"
                                       inputId="inputEmployeeCode"
                                       v-model="employeeCode"
                                       @input-blur="validateEmployeeCode"
                                       :isBorderRed="employeeCodeValidate" />
                            </div>
                            <div class="small-info">
                                <p>Họ và tên(<font color="red">*</font>)</p>
                                <Input inputType="text"
                                       inputPlacehoder=""
                                       :inputValue="fullName"
                                       inputId="inputFullName"
                                       v-model="fullName"
                                       @input-blur="validateFullName"
                                       :isBorderRed="fullNameValidate" />
                            </div>
                        </div>
                        <div class="my-row">
                            <div class="small-info">
                                <p>Ngày sinh</p>
                                <Input inputType="date"
                                       inputPlacehoder=""
                                       :inputValue="dateOfBirth"
                                       inputId="inputFullName"
                                       v-model="dateOfBirth" />
                            </div>
                            <div class="small-info">
                                <p>Giới tính</p>
                                <Dropdown dropdownClass="dropdown"
                                          dropdownId="inputGenderName"
                                          :dropdownText="dropdownGenderObj.name"
                                          :dropdownValue="dropdownGenderObj.id"
                                          dropdownTitle="Gender"
                                          @dropdownOnSelect="dropdownGenderOnSelect"
                                          v-model="gender"
                                          @returnListItems="getListItems" />
                            </div>
                        </div>
                        <div class="my-row">
                            <div class="small-info">
                                <p>Số CMND/Căn cước(<font color="red">*</font>)</p>
                                <Input inputType="text"
                                       inputPlacehoder=""
                                       :inputValue="identityNumber"
                                       inputId="inputIdentityNumber"
                                       v-model="identityNumber"
                                       @input-blur="validateIdentityNumber"
                                       :isBorderRed="identityNumberValidate" />
                            </div>
                            <div class="small-info">
                                <p>Ngày cấp</p>
                                <Input inputType="date"
                                       inputPlacehoder=""
                                       :inputValue="identityDate"
                                       inputId="inputIdentityDate"
                                       v-model="identityDate" />
                            </div>
                        </div>
                        <div class="my-row">
                            <div class="small-info">
                                <p>Nơi cấp</p>
                                <Input inputType="text"
                                       inputPlacehoder=""
                                       :inputValue="identityPlace"
                                       inputId="inputIdentityPlace"
                                       v-model="identityPlace" />
                            </div>
                        </div>
                        <div class="my-row">
                            <div class="small-info">
                                <p>Email(<font color="red">*</font>)</p>
                                <Input inputType="text"
                                       inputPlacehoder=""
                                       :inputValue="email"
                                       inputId="inputEmail"
                                       v-model="email"
                                       @input-blur="validateEmail"
                                       :isBorderRed="emailValidate" />
                            </div>
                            <div class="small-info">
                                <p>Số điện thoại(<font color="red">*</font>)</p>
                                <Input inputType="text"
                                       inputPlacehoder=""
                                       :inputValue="phoneNumber"
                                       inputId="inputPhoneNumber"
                                       v-model="phoneNumber"
                                       @input-blur="validatePhoneNumber"
                                       :isBorderRed="phoneNumberValidate" />
                            </div>
                        </div>
                    </div>
                    <div class="thongtincongviec">
                        <DialogTitleInfo dialogTitleInfo="A. THÔNG TIN CÔNG VIỆC" />
                        <div class="my-row">
                            <div class="small-info">
                                <p>Vị trí</p>
                                <Dropdown dropdownId="inputPosition1Name"
                                          dropdownClass=""
                                          :dropdownValue="dropdownPositionObj.id"
                                          :dropdownText="dropdownPositionObj.name"
                                          dropdownTitle="Positions"
                                          @dropdownOnSelect="dropdownPositionOnSelect"
                                          v-model="positionId"
                                          @returnListItems="getListItems" />
                            </div>
                            <div class="small-info">
                                <p>Phòng ban</p>
                                <Dropdown dropdownId="inputPosition1Name"
                                          dropdownClass=""
                                          :dropdownValue="dropdownDepartmentObj.id"
                                          :dropdownText="dropdownDepartmentObj.name"
                                          dropdownTitle="Departments"
                                          @dropdownOnSelect="dropdownDepartmentOnSelect"
                                          v-model="departmentId"
                                          @returnListItems="getListItems" />
                            </div>
                        </div>
                        <div class="my-row">
                            <div class="small-info">
                                <p>Mã số thuế cá nhân</p>
                                <Input inputType="text"
                                       inputPlacehoder=""
                                       :inputValue="personalTaxCode"
                                       inputId="inputPersonalTaxCode"
                                       v-model="personalTaxCode" />
                            </div>
                            <div class="small-info">
                                <p>Mức lương cơ bản</p>
                                <Input inputClass="text-align-right"
                                       inputType="text"
                                       inputPlacehoder=""
                                       :inputValue="salary"
                                       inputId="inputSalary"
                                       v-model="salary" />
                            </div>
                        </div>
                        <div class="my-row">
                            <div class="small-info">
                                <p>Ngày gia nhập công ty</p>
                                <Input inputType="date"
                                       inputPlacehoder=""
                                       :inputValue="joinDate"
                                       inputId="inputJoinDate"
                                       v-model="joinDate" />
                            </div>
                            <div class="small-info">
                                <p>Tình trạng công việc</p>
                                <Dropdown dropdownClass="dropdown last-dropdown"
                                          dropdownId="inputWorkStatus"
                                          :dropdownText="dropdownWorkStatusObj.name"
                                          :dropdownValue="dropdownWorkStatusObj.id"
                                          dropdownTitle="WorkStatus"
                                          @dropdownOnSelect="dropdownWorkStatusOnSelect"
                                          v-model="workStatus"
                                          @returnListItems="getListItems" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <DialogFooter @btnCancelOnClick="hideThisForm"
                          @btnSaveOnClick="saveEmployee" />
        </div>
    </div>
</template>

<script>
    import axios from "axios";
    import DialogHeader from "../../components/base/BaseDialogHeader.vue";
    import DialogFooter from "../../components/base/BaseDialogFooter.vue";
    import DialogTitleInfo from "../../components/base/BaseDialogTitleInfo.vue";
    import Input from "../../components/base/BaseInput.vue";
    import Dropdown from "../../components/base/BaseDropdown.vue";
    import { eventBus } from "../../main.js";
    import { eventBus1 } from "../../main.js";

    export default {
        name: "EmployeeDialog",
        components: {
            DialogHeader,
            DialogFooter,
            DialogTitleInfo,
            Input,
            Dropdown,
        },
        props: {
            // để nhận giá trị true/false từ parent => bind cho class để ẩn/hiện dialog
            isShow: {
                type: Boolean,
                default: false,
            },
            // để nhận giá trị từ parent => để phân biệt đang thêm hay sửa
            mode: {
                type: Number,
                default: 1,
            },
            // để nhận giá trị EmployeeId khi mà form đang trong trạng thái sửa
            myEmployeeId: {
                type: String,
                default: "",
            },
            // để nhận giá trị từ parent cho biết form đang được mở lại => kết hợp với mode để thực hiện những tính năng tương ứng
            isReOpen: {
                type: Boolean,
                default: false,
            },
        },
        data() {
            return {
                // obj để lưu data lấy từ input để save
                employee: {},

                //các data lưu trữ các trường của employee
                employeeCode: "",
                fullName: "",
                gender: "",
                dateOfBirth: "",
                phoneNumber: "",
                email: "",
                identityNumber: "",
                identityDate: "",
                identityPlace: "",
                joinDate: "",
                departmentId: "",
                positionId: "",
                personalTaxCode: "",
                salary: "",
                workStatus: "",

                // để nhận giá trị id và name của item dropdown được select nhận từ dropdown 
                //và truyền lại giá trị tương ứng value, text cho dropdown
                dropdownGenderObj: {},
                dropdownPositionObj: {},
                dropdownDepartmentObj: {},
                dropdownWorkStatusObj: {},

                // để lưu giá trị có đang trong tình trang ô input tương ứng bị cảnh báo validate hay không
                employeeCodeValidate: false,
                fullNameValidate: false,
                identityNumberValidate: false,
                emailValidate: false,
                phoneNumberValidate: false,

                // nhận list id,name của các item từ dropdown truyền lên 
                //=> để lấy text tương ứng match vào dropdown khi sửa vì đối tượng lấy từ api chỉ có id không có name
                dropdownGenderItems: [],
                dropdownPositionItems: [],
                dropdownDepartmentItems: [],
                dropdownWorkStatusItems: []
            };
        },
        created() {
            // lắng nghe sự kiện từ eventBus1 (sự kiện khi mà click vào yes trên popup khi confirm sửa hoặc xóa)
            eventBus1.$on("addOrUpdateData", () => {
                if (this.mode == 1) {
                    this.addData();
                } else {
                    this.updateData();
                }
            });
        },
        methods: {
            // emit sự kiện ẩn dialog lên cho parent xử lí
            hideThisForm() {
                this.$emit("hideDialog");
            },
            /**
             * sau khi confirm thì thực hiện thêm mới employee
             * Author hieunv 03/08/2021
             * */
            addData() {
                var vm = this;
                axios
                    .post("http://cukcuk.manhnv.net/v1/Employees", vm.employee)
                    .then(() => {
                        eventBus.$emit("showTooltipAddSuccess");
                        eventBus.$emit("loadData");
                    })
                    .catch((res) => {
                        console.log(res);
                    });
            },
            /**
             * sau khi confirm thì thực hiện sửa employee
             * Author hieunv 03/08/2021
             * */
            updateData() {
                var vm = this;
                axios
                    .put(
                        "http://cukcuk.manhnv.net/v1/Employees/" + vm.myEmployeeId,
                        vm.employee
                    )
                    .then(() => {
                        eventBus.$emit("showTooltipUpdateSuccess");
                        eventBus.$emit("loadData");
                    })
                    .catch((res) => {
                        console.log(res);
                    });
            },
            /**
             * Xử lí sự kiện khi click vào btn Save => lấy value từ các input, dropdown => validate requie 
             * => dựa vào this.mode để thực hiện show popup confirm tương ứng
             * Author hieunv 01/08/2021
             * */
            saveEmployee() {
                this.employee.EmployeeCode = this.employeeCode;
                this.employee.FullName = this.fullName;
                this.employee.DateOfBirth = this.dateOfBirth;
                this.employee.PhoneNumber = this.phoneNumber;
                this.employee.Email = this.email;
                this.employee.IdentityNumber = this.identityNumber;
                this.employee.IdentityDate = this.identityDate;
                this.employee.IdentityPlace = this.identityPlace;
                this.employee.JoinDate = this.joinDate;
                this.employee.PersonalTaxCode = this.personalTaxCode;
                this.employee.Salary = this.salary;
                this.employee.PositionId = this.positionId;
                this.employee.DepartmentId = this.departmentId;
                this.employee.Gender = parseInt(this.gender);
                this.employee.WorkStatus = parseInt(this.workStatus);

                if (
                    this.employee.EmployeeCode !== "" &&
                    this.employee.FullName !== "" &&
                    this.employee.IdentityNumber !== "" &&
                    this.employee.Email !== "" &&
                    this.employee.PhoneNumber !== ""
                ) {
                    if (this.mode == 1) {
                        eventBus1.$emit("showPopupConfirmAdd");
                    } else {
                        eventBus1.$emit("showPopupConfirmUpdate");
                    }
                } else {
                    eventBus1.$emit("showTooltipInputRequiedAll");
                }
            },
            /**
             * format date về dạng yyyy-mm-dd
             * @param _date
             * Author hieunv 01/08/2021
             */
            formatDateToValue(_date) {
                if (_date != null) {
                    var date = new Date(_date);
                    var day = date.getDate();
                    day = day < 10 ? "0" + day : day;
                    var month = date.getMonth() + 1;
                    month = month < 10 ? "0" + month : month;
                    var year = date.getFullYear();
                    return year + "-" + month + "-" + day;
                } else {
                    return "";
                }
            },

            // gán obj chứa id và name của item được select khi nhận được sự kiện select item từ các drop down
            dropdownGenderOnSelect(obj) {
                this.dropdownGenderObj = obj;
                this.gender = obj.id;
            },
            dropdownPositionOnSelect(obj) {
                this.dropdownPositionObj = obj;
                this.positionId = obj.id;
            },
            dropdownDepartmentOnSelect(obj) {
                this.dropdownDepartmentObj = obj;
                this.departmentId = obj.id;
            },
            dropdownWorkStatusOnSelect(obj) {
                this.dropdownWorkStatusObj = obj;
                this.workStatus = obj.id;
            },

            // validate các trường requid
            validateEmployeeCode() {
                if (this.employeeCode == "") {
                    eventBus1.$emit("showTooltipInputRequied");
                    this.employeeCodeValidate = true;
                } else {
                    this.employeeCodeValidate = false;
                }
            },
            validateFullName() {
                if (this.fullName == "") {
                    eventBus1.$emit("showTooltipInputRequied");
                    this.fullNameValidate = true;
                } else {
                    this.fullNameValidate = false;
                }
            },
            validateIdentityNumber() {
                if (this.identityNumber == "") {
                    eventBus1.$emit("showTooltipInputRequied");
                    this.identityNumberValidate = true;
                } else {
                    this.identityNumberValidate = false;
                }
            },
            validateEmail() {
                if (this.email == "") {
                    eventBus1.$emit("showTooltipInputRequied");
                    this.emailValidate = true;
                } else {
                    this.emailValidate = false;
                }
            },
            validatePhoneNumber() {
                if (this.phoneNumber == "") {
                    eventBus1.$emit("showTooltipInputRequied");
                    this.phoneNumberValidate = true;
                } else {
                    this.phoneNumberValidate = false;
                }
            },

            // lấy list item chứa id và name tương ứng của các option dropdown khi nhận được sự kiện các dropdown đã load xong dữ liệu
            // vì lúc get employee theo id chỉ nhận được id, không nhận được name của các option dropdown nên phải lấy name tương ứng
            // truyền lên từ dropdown để match vào dropdown
            getListItems(listItem, dropdownTitle) {
                if (dropdownTitle == 'Gender') {
                    this.dropdownGenderItems = listItem;
                }
                if (dropdownTitle == 'Positions') {
                    this.dropdownPositionItems = listItem;
                }
                if (dropdownTitle == 'Departments') {
                    this.dropdownDepartmentItems = listItem;
                }
                if (dropdownTitle == 'WorkStatus') {
                    this.dropdownWorkStatusItems = listItem;
                }
            }
        },
        watch: {
            // khi prop isReOpen thay đổi => biết được form được mở lại , kết hợp với mode để thực hiện tính năng tương ứng
            isReOpen() {
                //loại bỏ border red của lần validate trước
                this.employeeCodeValidate = false;
                this.fullNameValidate = false;
                this.identityNumberValidate = false;
                this.emailValidate = false;
                this.phoneNumberValidate = false;

                if (this.mode == 1) {
                    //call api lấy dữ liệu newEmployeeCode
                    axios
                        .get("http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode")
                        .then((res) => {
                            this.employeeCode = res.data;
                        })
                        .catch(() => { });

                    this.fullName = "";
                    this.dateOfBirth = "";
                    this.phoneNumber = "";
                    this.email = "";
                    this.identityNumber = "";
                    this.identityDate = "";
                    this.identityPlace = "";
                    this.joinDate = "";
                    this.personalTaxCode = "";
                    this.salary = "";
                    this.dropdownPositionObj = { id: "", name: "" };
                    this.dropdownDepartmentObj = { id: "", name: "" };
                    this.dropdownGenderObj = { id: "", name: "" };
                    this.dropdownWorkStatusObj = { id: "", name: "" };
                } else {
                    // lấy lên nhân viên và bind vào form
                    axios
                        .get("http://cukcuk.manhnv.net/v1/Employees/" + this.myEmployeeId)
                        .then((res) => {
                            this.fullName = res.data.FullName;
                            this.employeeCode = res.data.EmployeeCode;
                            this.dateOfBirth = this.formatDateToValue(res.data.DateOfBirth);
                            this.phoneNumber = res.data.PhoneNumber;
                            this.email = res.data.Email;
                            this.identityNumber = res.data.identityNumber;
                            this.identityDate = this.formatDateToValue(res.data.IdentityDate);
                            this.identityPlace = res.data.IdentityPlace;
                            this.joinDate = this.formatDateToValue(res.data.JoinDate);
                            this.personalTaxCode = res.data.PersonalTaxCode;
                            this.salary = res.data.Salary + "";
                            // match dropdown gender với id item gender
                            this.dropdownGenderObj.id = res.data.Gender + '';
                            this.dropdownGenderItems.forEach((item) => {
                                if (item.itemId == this.dropdownGenderObj.id) {
                                    this.dropdownGenderObj.name = item.itemName;
                                }
                            })
                            // match dropdown positions với id item positions
                            this.dropdownPositionObj.id = res.data.PositionId;
                            this.dropdownPositionItems.forEach((item) => {
                                if (item.PositionId == this.dropdownPositionObj.id) {
                                    this.dropdownPositionObj.name = item.PositionName;
                                }
                            })
                            // match dropdown departments với id item departments
                            this.dropdownDepartmentObj.id = res.data.DepartmentId;
                            this.dropdownDepartmentItems.forEach((item) => {
                                if (item.DepartmentId == this.dropdownDepartmentObj.id) {
                                    this.dropdownDepartmentObj.name = item.DepartmentName;
                                }
                            })
                            // match dropdown workStatus với id item workStatus
                            this.dropdownWorkStatusObj.id = res.data.WorkStatus + '';
                            this.dropdownWorkStatusItems.forEach((item) => {
                                if (item.itemId == this.dropdownWorkStatusObj.id) {
                                    this.dropdownWorkStatusObj.name = item.itemName;
                                }
                            })
                        })
                        .catch((res) => {
                            console.log(res);
                        });
                }
            },
        },
    };
</script>

<style scoped>
    .dialog {
        margin: 0;
        padding: 0;
        width: 100%;
        height: 100vh;
        visibility: hidden;
        position: relative;
    }

    .dialog-show {
        visibility: visible;
    }

    .dialog-modal {
        position: fixed;
        top: 0;
        bottom: 0;
        left: 0;
        right: 0;
        background-color: black;
        opacity: 0.5;
    }

    .employee-profile-dialog {
        opacity: 1;
        position: fixed;
        top: 24px;
        left: 50%;
        margin-left: -410px;
        width: 820px;
        height: calc(100vh - 48px);
        border-radius: 5px;
        background-color: #ffffff;
        overflow: hidden;
    }

        .employee-profile-dialog .dialog-content {
            width: 100%;
            height: calc(100% - 135px);
            float: left;
        }

            .employee-profile-dialog .dialog-content .dialog-content-left {
                width: 200px;
                height: 100%;
                float: left;
                display: flex;
                flex-direction: column;
            }

                .employee-profile-dialog .dialog-content .dialog-content-left .avt-default {
                    width: 167px;
                    height: 167px;
                    background-image: url("../../assets/img/default-avatar.jpg");
                    background-repeat: no-repeat;
                    background-size: contain;
                    border-radius: 50%;
                    border: 1px solid #bdbec0;
                    margin: 10px auto;
                    display: block;
                }

                .employee-profile-dialog .dialog-content .dialog-content-left p {
                    display: block;
                    margin: 0 auto;
                }

            .employee-profile-dialog .dialog-content .dialog-content-right {
                width: calc(100% - 200px);
                height: 100%;
                float: left;
                overflow-y: scroll;
                /*-webkit-overflow-scrolling: touch;*/
            }

                .employee-profile-dialog .dialog-content .dialog-content-right .thongtinchung {
                    width: 100%;
                    height: 470px;
                    float: left;
                }

                .employee-profile-dialog
                .dialog-content
                .dialog-content-right
                .thongtincongviec {
                    width: 100%;
                    height: calc(100% - 450px);
                    float: left;
                }

                .employee-profile-dialog .dialog-content .dialog-content-right .my-row {
                    width: calc(100% - 24px);
                    height: 57px;
                    display: flex;
                    justify-content: space-between;
                    margin-bottom: 24px;
                }

                .employee-profile-dialog .dialog-content .dialog-content-right .small-info {
                    width: 290px;
                    height: 100%;
                    background-color: #ffffff;
                    float: left;
                }

                    .employee-profile-dialog .dialog-content .dialog-content-right .small-info p {
                        color: black;
                        display: block;
                        margin: 0;
                        margin-bottom: 4px;
                    }

                    .employee-profile-dialog
                    .dialog-content
                    .dialog-content-right
                    .small-info
                    input {
                        width: 290px;
                    }

                    /*.employee-profile-dialog .dialog-content .dialog-content-right .small-info .combobox {
                    width: 290px;
                }*/

                    .employee-profile-dialog
                    .dialog-content
                    .dialog-content-right
                    .small-info
                    .dropdown {
                        width: 290px;
                    }

                        .employee-profile-dialog
                        .dialog-content
                        .dialog-content-right
                        .small-info
                        .dropdown
                        p {
                            margin: 0;
                            line-height: 40px;
                            font-size: 13px;
                            margin-left: 10px;
                            font-family: "GoogleSans-Regular";
                        }
</style>