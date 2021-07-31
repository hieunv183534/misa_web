<template>
  <div class="dialog" v-bind:class="{ 'dialog-show': isShow }">
    <div class="dialog-modal"></div>
    <div class="employee-profile-dialog">
      <DialogHeader
        dialogTitle="THÔNG TIN NHÂN VIÊN"
        @btnExitOnClick="$emit('hideDialog')"
      />
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
                <Input
                  inputClass="autofocus"
                  inputType="text"
                  inputPlacehoder=""
                  :inputValue="employeeCode"
                  inputId="inputEmployeeCode"
                  v-model="employeeCode"
                />
              </div>
              <div class="small-info">
                <p>Họ và tên(<font color="red">*</font>)</p>
                <Input
                  inputClass=""
                  inputType="text"
                  inputPlacehoder=""
                  :inputValue="fullName"
                  inputId="inputFullName"
                  v-model="fullName"
                />
              </div>
            </div>
            <div class="my-row">
              <div class="small-info">
                <p>Ngày sinh</p>
                <Input
                  inputClass=""
                  inputType="date"
                  inputPlacehoder=""
                  :inputValue="dateOfBirth"
                  inputId="inputFullName"
                  v-model="dateOfBirth"
                />
              </div>
              <div class="small-info">
                <p>Giới tính</p>
                <Dropdown
                  dropdownClass="dropdown"
                  dropdownId="inputGenderName"
                  dropdownText="Nam"
                  dropdownValue="1"
                />
              </div>
            </div>
            <div class="my-row">
              <div class="small-info">
                <p>Số CMND/Căn cước(<font color="red">*</font>)</p>
                <Input
                  inputClass=""
                  inputType="text"
                  inputPlacehoder=""
                  :inputValue="identityNumber"
                  inputId="inputIdentityNumber"
                  v-model="identityNumber"
                />
              </div>
              <div class="small-info">
                <p>Ngày cấp</p>
                <Input
                  inputClass=""
                  inputType="date"
                  inputPlacehoder=""
                  :inputValue="identityDate"
                  inputId="inputIdentityDate"
                  v-model="identityDate"
                />
              </div>
            </div>
            <div class="my-row">
              <div class="small-info">
                <p>Nơi cấp</p>
                <Input
                  inputClass=""
                  inputType="text"
                  inputPlacehoder=""
                  :inputValue="identityPlace"
                  inputId="inputIdentityPlace"
                  v-model="identityPlace"
                />
              </div>
            </div>
            <div class="my-row">
              <div class="small-info">
                <p>Email(<font color="red">*</font>)</p>
                <Input
                  inputClass=""
                  inputType="text"
                  inputPlacehoder=""
                  :inputValue="email"
                  inputId="inputEmail"
                  v-model="email"
                />
              </div>
              <div class="small-info">
                <p>Số điện thoại(<font color="red">*</font>)</p>
                <Input
                  inputClass=""
                  inputType="text"
                  inputPlacehoder=""
                  :inputValue="phoneNumber"
                  inputId="inputPhoneNumber"
                  v-model="phoneNumber"
                />
              </div>
            </div>
          </div>
          <div class="thongtincongviec">
            <DialogTitleInfo dialogTitleInfo="A. THÔNG TIN CÔNG VIỆC" />
            <div class="my-row">
              <div class="small-info">
                <p>Vị trí</p>
                <Dropdown
                  dropdownClass="dropdown"
                  dropdownId="inputPositionName"
                  dropdownText="Giám đốc"
                  dropdownValue=""
                />
              </div>
              <div class="small-info">
                <p>Phòng ban</p>
                <Dropdown
                  dropdownClass="dropdown"
                  dropdownId="inputDepartmentName"
                  dropdownText="Phòng đào tạo"
                  dropdownValue=""
                />
              </div>
            </div>
            <div class="my-row">
              <div class="small-info">
                <p>Mã số thuế cá nhân</p>
                <Input
                  inputClass=""
                  inputType="text"
                  inputPlacehoder=""
                  :inputValue="personalTaxCode"
                  inputId="inputPersonalTaxCode"
                  v-model="personalTaxCode"
                />
              </div>
              <div class="small-info">
                <p>Mức lương cơ bản</p>
                <Input
                  inputClass="text-align-right"
                  inputType="text"
                  inputPlacehoder=""
                  :inputValue="salary"
                  inputId="inputSalary"
                  v-model="salary"
                />
              </div>
            </div>
            <div class="my-row">
              <div class="small-info">
                <p>Ngày gia nhập công ty</p>
                <Input
                  inputClass=""
                  inputType="date"
                  inputPlacehoder=""
                  :inputValue="joinDate"
                  inputId="inputJoinDate"
                  v-model="joinDate"
                />
              </div>
              <div class="small-info">
                <p>Tình trạng công việc</p>
                <Dropdown
                  dropdownClass="dropdown last-dropdown"
                  dropdownId="inputWorkStatus"
                  dropdownText="Đang làm việc"
                  dropdownValue="1"
                />
              </div>
            </div>
          </div>
        </div>
      </div>

      <DialogFooter
        @btnCancelOnClick="$emit('hideDialog1')"
        @btnSaveOnClick="saveEmployee"
      />
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
    isShow: {
      type: Boolean,
      default: false,
    },
    mode: {
      type: Number,
      default: 1,
    },
    myEmployeeId: {
      type: String,
      default: "",
    },
    isReOpen: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
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
    };
  },
  methods: {
    saveEmployee() {
      var employee = {};
      employee.EmployeeCode = this.employeeCode;
      employee.FullName = this.fullName;
      employee.DateOfBirth = this.dateOfBirth;
      employee.PhoneNumber = this.phoneNumber;
      employee.Email = this.email;
      employee.IdentityNumber = this.identityNumber;
      employee.IdentityDate = this.identityDate;
      employee.IdentityPlace = this.identityPlace;
      employee.JoinDate = this.joinDate;
      employee.PersonalTaxCode = this.personalTaxCode;
      employee.Salary = this.salary;

      //   var vm = this;
      if (this.mode == 1) {
        axios
          .post("http://cukcuk.manhnv.net/v1/Employees", employee)
          .then(() => {
            alert("Thêm nhân viên thành công!");
            eventBus.$emit("loadData");
          })
          .catch((res) => {
            console.log(res);
          });
      } else {
        axios
          .put(
            "http://cukcuk.manhnv.net/v1/Employees/" + this.myEmployeeId,
            employee
          )
          .then(() => {
            alert("Sửa nhân viên thành công!");
            eventBus.$emit("loadData");
          })
          .catch((res) => {
            console.log(res);
          });
      }
    },
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
  },
  watch: {
    isReOpen() {
      if (this.mode == 1) {
        this.fullName = "";
        this.employeeCode = "";
        this.dateOfBirth = "";
        this.phoneNumber = "";
        this.email = "";
        this.identityNumber = "";
        this.identityDate = "";
        this.identityPlace = "";
        this.joinDate = "";
        this.personalTaxCode = "";
        this.salary = "";
      } else {
        // lấy lên nhân viên và bind vào form
        axios
          .get("http://cukcuk.manhnv.net/v1/Employees/" + this.myEmployeeId)
          .then((res) => {
            this.fullName = res.data.FullName;
            this.employeeCode = res.data.EmployeeCode;
            this.dateOfBirth =this.formatDateToValue(res.data.DateOfBirth);
            this.phoneNumber = res.data.PhoneNumber;
            this.email = res.data.Email;
            this.identityNumber = res.data.identityNumber;
            this.identityDate = this.formatDateToValue(res.data.IdentityDate);
            this.identityPlace = res.data.IdentityPlace;
            this.joinDate = this.formatDateToValue(res.data.JoinDate);
            this.personalTaxCode = res.data.PersonalTaxCode;
            this.salary = res.data.Salary.toString();
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

/*.employee-profile-dialog .dialog-content .dialog-content-right .small-info input.calender {
                        background-image: url('../../content/img/calender.jpg');
                        background-repeat: no-repeat;
                        background-size: 16px;
                        background-position: 246px center;
                    }

                    .employee-profile-dialog .dialog-content .dialog-content-right .small-info input.down1 {
                        background-image: url('../../content/img/down1.jpg');
                        background-repeat: no-repeat;
                        background-size: 16px;
                        background-position: 264px center;
                    }*/
</style>