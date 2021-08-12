<template>
  <div class="content" v-bind:class="{'collapse-content' : isCollapseMenu}">
      <ContentHeader/>
      <EmployeeList
      @btnAddOnClick = "$emit('showAddForm')"
      @chooseAnEmployee = "chooseAnEmployee1"
      @btnDeleteOnClick ="$emit('showDeletePopup')"
      @btnCopyOnClick ="$emit('showCopyPopup')"
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
        methods: {
        // nhận sự kiện dbclick tr từ grid và truyền tiếp lên cho parent(App)
        chooseAnEmployee1(employeeId){
            this.$emit('showEditForm',employeeId)
        }
    },
        props: {
        // để truyền vào giá tri true/false tương ứng với thu nhỏ hay phóng to menu=> page này cũng phải thay đổi kích thước theo 
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