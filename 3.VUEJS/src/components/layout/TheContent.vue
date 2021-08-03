<template>
  <div class="content" :class="{'collapse-content' : isCollapseMenu}">
      <ContentHeader/>
      <EmployeeList
      @btnAddOnClick = "$emit('showAddForm')"
      @chooseAnEmployee = "chooseAnEmployee1"
      @btnDeleteOnClick ="$emit('showDeletePopup')"
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
    },
    props:{
        isCollapseMenu: Boolean
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

.collapse-content{
    width: calc(100% - 54px);
}
</style>