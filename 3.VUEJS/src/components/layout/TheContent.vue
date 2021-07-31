<template>
  <div class="content">
      <ContentHeader/>
      <EmployeeList
      @btnAddOnClick = "$emit('showAddForm')"
      @chooseAnEmployee = "chooseAnEmployee1"
      />
  </div>
</template>

<script>
import ContentHeader from "./TheContentHeader.vue"
import EmployeeList from "../../view/employee/EmployeeList.vue"

export default {
    name: 'TheContent',
    components:{
        ContentHeader,EmployeeList
    },
    methods:{
        chooseAnEmployee1(employeeId){
            this.$emit('showEditForm',employeeId)
        }
    }
};
</script>

<style scoped>
.content {
    width: calc(100% - 227px);
    height: 100vh;
    float: left;
    transition: 0.5s;
}
</style>