<template>
    <div class="grid">
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
            <th fieldName="DepartmentName" style="text-align: left">
              Phòng ban
            </th>
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
          <tr v-for="employee in employees" :key="employee.EmployeeId">
              <td>#</td>
              <td>{{employee.EmployeeCode}}</td>
              <td>{{employee.FullName}}</td>
              <td>{{employee.DateOfBirth}}</td>
              <td>{{employee.GenderName}}</td>
              <td>{{employee.PhoneNumber}}</td>
              <td>{{employee.Email}}</td>
              <td>{{employee.DepartmentName}}</td>
              <td>{{employee.PositionName}}</td>
              <td>{{employee.Salary}}</td>
              <td>{{employee.WorkStatus}}</td>
          </tr>
        </tbody>
      </table>
    </div>
</template>

<script>
import axios from 'axios'

export default {
    name: 'TheGrid',
    created(){
        var vm = this;
        axios.get("http://cukcuk.manhnv.net/v1/Employees").then(res=>{
            console.log(res.data);
            vm.employees= res.data;
        }).catch(res=>{
            console.log(res);
        })
    },
    data(){
        return{
            employees : []
        }
    }
}
</script>

<style scoped>
.content .content-body .grid {
  width: 100%;
  height: calc(100% - 156px);
  float: left;
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
        overflow-x: scroll;
        display: block;
    }

thead {
    border-bottom: solid 1px #E4E4E4;
}

thead, tbody {
    display: block;
}

tbody {
    overflow-y: scroll;
    overflow-x: hidden;
    height: calc( 100% - 55px );
}

td, th {
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

td{
    height:48px;
}


tbody tr {
    border-bottom: 1px solid #E4E4E4;
}


    tbody tr:nth-child(2n+1) {
        background-color: #FFF3EB;
    }

    tbody tr:hover {
        background-color: #E9EBEE !important;
    }

    tbody tr td input:checked{
        background-color:red;
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
    text-align:center;
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
    min-width: 200px;
    max-width: 200px;
}

/*chức vụ*/
td:nth-child(9),
th:nth-child(9) {
    min-width: 150px;
    max-width: 150px;
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