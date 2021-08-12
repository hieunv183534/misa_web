<template>
  <div class="grid" :class="{ 'table-collapse': isCollase }">
    <table border="0">
      <thead>
        <tr>
          <th fieldName="check" class="checkbox" style="text-align: right"></th>
          <th style="text-align: left">#</th>
          <th fieldName="EmployeeCode" style="text-align: left">
            Mã nhân viên
          </th>
          <th fieldName="FullName" style="text-align: left">Họ và tên</th>
          <th
            fieldName="DateOfBirth"
            formatType="ddmmyyyy"
            style="text-align: center"
          >
            Ngày sinh
          </th>
          <th fieldName="GenderName" style="text-align: left">Giới tính</th>
          <th fieldName="PhoneNumber" style="text-align: left">Điện thoại</th>
          <th fieldName="Email" style="text-align: left">Email</th>
          <th fieldName="DepartmentName" style="text-align: left">Phòng ban</th>
          <th fieldName="PositionName" style="text-align: left">Chức vụ</th>
          <th fieldName="Salary" formatType="money" style="text-align: right">
            Mức lương cơ bản
          </th>
          <th fieldName="WorkStatus" style="text-align: left">
            Tình trạng công việc
          </th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(employee, index) in employees"
          :key="employee.EmployeeId"
          @dblclick="$emit('tableRowOnDbClick', employee.EmployeeId)"
          :class="{
            'active-row': checkedEmployees.includes(employee.EmployeeId),
          }"
        >
          <td>
            <CheckBox
              @checkboxOnClick="checkcheck(employee.EmployeeId)"
              :chechOrNo="checkedEmployees.includes(employee.EmployeeId)"
            />
          </td>
          <td>{{ index++ }}</td>
          <td>{{ employee.EmployeeCode }}</td>
          <td>{{ employee.FullName }}</td>
          <td>{{ formatDate(employee.DateOfBirth) }}</td>
          <td>{{ employee.GenderName }}</td>
          <td>{{ employee.PhoneNumber }}</td>
          <td>{{ employee.Email }}</td>
          <td>{{ employee.DepartmentName }}</td>
          <td>{{ employee.PositionName }}</td>
          <td>{{ formatSalary(employee.Salary) }}</td>
          <td>{{ employee.WorkStatus }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import CheckBox from "../base/BaseCheckBox.vue";
import axios from "axios";
import { eventBus } from "../../main.js";
import { eventBus1 } from "../../main.js";

export default {
  name: "TheGrid",
  components: {
    CheckBox,
  },
  data() {
    return {
      // Chứa danh sách các nhân viên lấy từ api get full employee
      employees: [],
      // lưu trữ id của các employee được chọn để xóa
      checkedEmployees: [],
      // page hiện tại có phải đang collapse menu không
      isCollase: false,
    };
  },
  created() {
    //load dữ liệu và bind lên table
    this.loadData();

    // lắng nghe sự kiện load dữ liệu từ eventBus
    eventBus.$on("loadData", () => {
      this.loadData();
    });

    // lắng nghe sự kiện xóa dữ liệu từ eventBus1
    eventBus1.$on("deleteData", () => {
      this.deleteData();
    });

    // lắng nghe sự kiện nhân bản dữ liệu từ eventBus1
    eventBus1.$on("CopyData", () => {
      this.copyData();
    });

    // lắng nghe sự kiện collapse menu từ eventBus1
    eventBus1.$on("collapseMenu", (_isCollapse) => {
      this.isCollase = _isCollapse;
      console.log(this.isCollase);
    });

    // lắng nghe sự kiện lọc theo vi trí và phòng ban
    eventBus1.$on(
      "filterByPositionAndDepartment",
      (_positionId, _departmentId) => {
        console.log(_positionId + "///" + _departmentId);
        if (_positionId == "" && _departmentId == "") {
          this.loadData();
        } else {
          eventBus1.$emit('showLoader');
          var url = "";
          if (_positionId !== "" && _departmentId !== "") {
            url =
              "http://cukcuk.manhnv.net/v1/Employees/employeeFilter?pageSize=100&pageNumber=1&employeeFilter=nv&departmentId=" +
              _departmentId +
              "&positionId=" +
              _positionId;
          } else if (_positionId !== "" && _departmentId == "") {
            url =
              "http://cukcuk.manhnv.net/v1/Employees/employeeFilter?pageSize=100&pageNumber=1&employeeFilter=nv&positionId=" +
              _positionId;
          } else {
            url =
              "http://cukcuk.manhnv.net/v1/Employees/employeeFilter?pageSize=100&pageNumber=1&employeeFilter=nv&departmentId=" +
              _departmentId;
          }
          var vm = this;
          axios
            .get(url)
            .then((res) => {
              console.log(res);
              vm.employees = res.data.Data;
              eventBus1.$emit('hideLoader');
            })
            .catch((res) => {
              console.log(res);
            });
        }
      }
    );
  },
  methods: {
    /**
     * Format date về dạng dd/mm/yyyy để hiển thị
     * @param _date
     * Author hieunv (30/07/2021)
     */
    formatDate(_date) {
      if (_date != null) {
        var date = new Date(_date);
        var day = date.getDate();
        day = day < 10 ? "0" + day : day;
        var month = date.getMonth() + 1;
        month = month < 10 ? "0" + month : month;
        var year = date.getFullYear();
        return day + "/" + month + "/" + year;
      } else {
        return "";
      }
    },
    /**
     * Format lương
     * @param _salary
     * Author hieunv (30/07/2021)
     */
    formatSalary(_salary) {
      if (_salary != null) {
        /*var salary = _salary.toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1.");*/
        return _salary.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
        /*return salary;*/
      } else {
        return "";
      }
    },
    /**
     * Xử lí sự kiện khi click và checkbox trên tr
     * @param id
     * Author hieunv (01/08/2021)
     */
    checkcheck(id) {
      if (this.checkedEmployees.includes(id)) {
        this.checkedEmployees = this.checkedEmployees.filter((e) => e !== id);
        if (this.checkedEmployees.length == 0) {
          this.$emit("hideDeleteBtn");
          this.$emit("hideCopyBtn");
        } else if (this.checkedEmployees.length == 1) {
          this.$emit("showCopyBtn");
        } else {
          this.$emit("hideCopyBtn");
        }
      } else {
        this.checkedEmployees.push(id);
        if (this.checkedEmployees.length == 1) {
          this.$emit("showDeleteBtn");
          this.$emit("showCopyBtn");
        } else {
          this.$emit("hideCopyBtn");
        }
      }
    },
    /**
     * dùng axios lấy lên dữ liệu danh sách nhân viên
     * Author hieunv (29/07/2021)
     * */
    loadData() {
      eventBus1.$emit('showLoader');
      var vm = this;
      axios
        .get("http://cukcuk.manhnv.net/v1/Employees")
        .then((res) => {
          vm.employees = res.data;
          eventBus1.$emit('hideLoader');
        })
        .catch((res) => {
          console.log(res);
        });
    },
    /**
     * Gọi api xóa tất cả nhân viên có trong danh sách được chọn
     * Author hieunv (02/08/2021)
     * */
    deleteData() {
      var vm = this;
      vm.checkedEmployees.forEach(function (item) {
        axios
          .delete("http://cukcuk.manhnv.net/v1/Employees/" + item)
          .then(() => {
            vm.checkedEmployees = vm.checkedEmployees.filter((e) => e !== item);
            if (vm.checkedEmployees.length == 0) {
              vm.loadData();
              eventBus.$emit("showTooltipDeleteSuccess");
              vm.$emit("hideDeleteBtn");
            }
          });
      });
    },
    /**
     * xử lsi sự kiện nhân bản nhân viên được chọn(hiện tại chỉ cho nhân bản 1 nhân viên 1 lần)
     * Author hieunv 10/08/2021
     */
    async copyData() {
      var vm = this;
      var em = {};
      em.EmployeeId = null;
      //lấy lên dữ liệu nhân viên theo id
      var emId = this.checkedEmployees[0];
      await axios
        .get("http://cukcuk.manhnv.net/v1/Employees/" + emId)
        .then((res) => {
          em = res.data;
        });

      // lấy mã nhân viên mới
      await axios
        .get("http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode")
        .then((res) => {
          em.EmployeeCode = res.data;
        });
      // post nhân bản lên server
      axios
        .post("http://cukcuk.manhnv.net/v1/Employees", em)
        .then(() => {
          this.$emit("hideDeleteBtn");
          this.$emit("hideCopyBtn");
          eventBus.$emit("showTooltipCopySuccess");
          vm.loadData();
          this.checkedEmployees = [];
        })
        .catch((res) => {
          console.log(res.data);
        });
    },
  },
  props: {
    // dùng để page list truyền vào nếu muốn load lại dữ liệu
    isLoadAgain: {
      type: Boolean,
      default: false,
    },
  },
  watch: {
    // nếu isLoadAgain thay đổi thì thực hiện gọi lại hàm loadData để load lại dữ liệu
    isLoadAgain() {
      this.loadData();
    },
  },
};
</script>

<style scoped>
.content .content-body .grid {
  width: calc(100vw - 275px);
  height: calc(100% - 132px);
  float: left;
  overflow: scroll;
  transition: 0.5s;
  background-color: #fff;
}

.table-collapse {
  width: calc(100vw - 102px) !important;
  transition: 0.5s;
}

::-webkit-scrollbar {
  height: 8px;
  width: 8px;
  background: lightgray;
}

::-webkit-scrollbar-thumb:horizontal {
  background: gray;
  border-radius: 4px;
}

/*.grid {
        border: 3px black solid;
    }*/
table {
  border-collapse: collapse;
  width: 100%;
  height: 100%;
  display: table;
}

thead {
  border-bottom: solid 1px #e4e4e4;
  position: sticky;
  top: 0;
  background-color: #fff;
}

thead,
tbody {
  display: block;
}

tbody {
  /* overflow-y: scroll;
      overflow-x: hidden; */
  height: calc(100% - 55px);
  width: 100%;
}

th {
  position: sticky;
  top: 0;
  background-color: #fff;
}

td,
th {
  height: 48px;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
  padding-left: 10px;
  padding-right: 10px;
}

th {
  height: 50px;
}

td {
  height: 48px;
}

tbody tr {
  border-bottom: 1px solid #e4e4e4;
}

tbody tr:hover {
  background-color: #e9ebee !important;
}

tbody tr td input:checked {
  background-color: red;
}

/*check*/
td:nth-child(1),
th:nth-child(1) {
  min-width: 46px;
  max-width: 46px;
  padding-left: 16px;
  padding-right: 10px;
  box-sizing: border-box;
}

/*STT */
td:nth-child(2),
th:nth-child(2) {
  min-width: 30px;
  max-width: 30px;
  padding-left: 0;
}

/*mã nhân viên*/
td:nth-child(3),
th:nth-child(3) {
  min-width: 120px;
  max-width: 120px;
}

/*họ tên*/
td:nth-child(4),
th:nth-child(4) {
  min-width: 160px;
  max-width: 160px;
}

/*ngày sinh*/
td:nth-child(5),
th:nth-child(5) {
  min-width: 120px;
  max-width: 120px;
  text-align: center;
}

/*giới tính*/
td:nth-child(6),
th:nth-child(6) {
  min-width: 100px;
  max-width: 100px;
}

/*điện thoại*/
td:nth-child(7),
th:nth-child(7) {
  min-width: 160px;
  max-width: 160px;
}

/*email*/
td:nth-child(8),
th:nth-child(8) {
  min-width: 250px;
  max-width: 250px;
}

/*phòng ban*/
td:nth-child(9),
th:nth-child(9) {
  min-width: 150px;
  max-width: 150px;
}

/*chức vụ*/
td:nth-child(10),
th:nth-child(10) {
  min-width: 100px;
  max-width: 100px;
}

/*mức lương cơ bản*/
td:nth-child(11),
th:nth-child(11) {
  min-width: 150px;
  max-width: 150px;
  text-align: right;
}

/*tình trạng công việc*/
td:nth-child(12),
th:nth-child(12) {
  min-width: 170px;
  max-width: 170px;
}

.active-row {
  background-color: #e3f3ee;
}

.active-row:hover {
  background-color: #e3f3ee !important;
}
</style>