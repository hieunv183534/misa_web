<template>
  <div class="content-toolbar">
    <div class="toolbar-left">
      <Input
        inputClass="input-search input-icon"
        inputType="text"
        inputPlacehoder="Tìm kiếm theo mã, tên hoặc số điện thoại"
      />
      <Dropdown
        dropdownId="inputPosition1Name"
        dropdownClass=""
        dropdownValue="Đông"
        dropdownText="Tất cả vị trí"
      />
      <Dropdown
        dropdownId="inputDepartment1Name"
        dropdownClass=""
        dropdownValue="Đông"
        dropdownText="Tất cả phòng ban"
      />
    </div>
    <div class="toolbar-right">
      <Button
        buttonId="btnRefesh"
        buttonClass="button button-refresh"
        iClass="fa-sync-alt"
        buttonText=""
      />
    </div>
  </div>
</template>

<script>
import Input from "../base/BaseInput.vue";
import Dropdown from "../base/BaseDropdown.vue"
import Button from "../base/BaseButton.vue"

export default {
  name: "TheContentToolbar",
  components: {
    Input,Dropdown, Button
  },
};
</script>

<style scoped>
.content .content-body .content-toolbar {
  width: 100%;
  height: 40px;
  float: left;
  display: flex;
  justify-content: space-between;
  margin-bottom: 10px;
}

.content .content-body .content-toolbar .toolbar-left {
  height: 100%;
  width: 850px;
  display: flex;
}

/*.content .content-body .content-toolbar .toolbar-left .combobox{
                    margin-left:16px;
                }*/

.content .content-body .content-toolbar .toolbar-left .dropdown {
  margin-left: 16px;
}

.content .content-body .content-toolbar .toolbar-right {
  height: 100%;
  width: 250px;
  display: flex;
  justify-content: flex-end;
}
</style>