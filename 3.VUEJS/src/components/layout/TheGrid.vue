<template>
  <div class="grid" :class="{ 'table-collapse' : isCollase}">
    <table>
      <thead>
        <tr>
          <th fieldName="check" class="checkbox">#</th>
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
          v-for="employee in employees"
          :key="employee.EmployeeId"
          @dblclick="$emit('tableRowOnDbClick', employee.EmployeeId)"
        >
          <td>
            <input
              type="checkbox"
              style="width: 46px; height: 20px; background-color:red;"
              @click="checkcheck(employee.EmployeeId)"
            />
          </td>
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
import axios from "axios";
import { eventBus } from "../../main.js";
import { eventBus1 } from "../../main.js";

export default {
  name: "TheGrid",
  data() {
    return {
      employees: [],
      checkedEmployees: [],
      isCollase: false
    };
  },
  created() {
    this.loadData();

    eventBus.$on("loadData", () => {
      this.loadData();
    });

    eventBus1.$on("deleteData", () => {
      this.deleteData();
    });

     eventBus1.$on("collapseMenu", (_isCollapse) => {
       this.isCollase = _isCollapse;
      console.log(this.isCollase)
    });
  },
  methods: {
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
    formatSalary(_salary) {
      if (_salary != null) {
        /*var salary = _salary.toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1.");*/
        return _salary.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
        /*return salary;*/
      } else {
        return "";
      }
    },
    checkcheck(id) {
      if (this.checkedEmployees.includes(id)) {
        this.checkedEmployees = this.checkedEmployees.filter((e) => e !== id);
        if(this.checkedEmployees.length ==0){
        this.$emit('hideDeleteBtn');
        }
      } else {
        this.checkedEmployees.push(id);
        this.$emit('showDeleteBtn');
      }
    },
    loadData() {
      var vm = this;
      axios
        .get("http://cukcuk.manhnv.net/v1/Employees")
        .then((res) => {
          vm.employees = res.data;
        })
        .catch((res) => {
          console.log(res);
        });
    },
    deleteData() {
      var vm = this;
      vm.checkedEmployees.forEach(function (item) {
        axios
          .delete("http://cukcuk.manhnv.net/v1/Employees/" + item)
          .then(() => {
            vm.checkedEmployees = vm.checkedEmployees.filter((e) => e !== item);
            if (vm.checkedEmployees.length == 0) {
              vm.loadData();
              eventBus.$emit('showTooltipDeleteSuccess');
              vm.$emit('hideDeleteBtn');
            }
          });
      });
    },
  },
  props: {
    isLoadAgain: {
      type: Boolean,
      default: false,
    },
  },
  watch: {
    isLoadAgain() {
      this.loadData();
    },
  },
};
</script>

<style scoped>
.content .content-body .grid {
  width: calc(100vw - 275px);
  height: calc(100% - 156px);
  float: left;
  overflow: scroll;
  transition: 0.5s;
}

.table-collapse{
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
  min-width: 50px;
  max-width: 50px;
}

/*mã nhân viên*/
td:nth-child(2),
th:nth-child(2) {
  min-width: 120px;
  max-width: 120px;
}

/*họ tên*/
td:nth-child(3),
th:nth-child(3) {
  min-width: 160px;
  max-width: 160px;
}

/*ngày sinh*/
td:nth-child(4),
th:nth-child(4) {
  min-width: 120px;
  max-width: 120px;
  text-align: center;
}

/*giới tính*/
td:nth-child(5),
th:nth-child(5) {
  min-width: 100px;
  max-width: 100px;
}

/*điện thoại*/
td:nth-child(6),
th:nth-child(6) {
  min-width: 160px;
  max-width: 160px;
}

/*email*/
td:nth-child(7),
th:nth-child(7) {
  min-width: 250px;
  max-width: 250px;
}

/*phòng ban*/
td:nth-child(8),
th:nth-child(8) {
  min-width: 150px;
  max-width: 150px;
}

/*chức vụ*/
td:nth-child(9),
th:nth-child(9) {
  min-width: 100px;
  max-width: 100px;
}

/*mức lương cơ bản*/
td:nth-child(10),
th:nth-child(10) {
  min-width: 150px;
  max-width: 150px;
  text-align: right;
}

/*tình trạng công việc*/
td:nth-child(11),
th:nth-child(11) {
  min-width: 170px;
  max-width: 170px;
}
</style>