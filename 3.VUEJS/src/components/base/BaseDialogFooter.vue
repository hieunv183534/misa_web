<template>
  <div class="dialog-footer">
    <Button
    buttonId="btnCancel"
    buttonClass="btn-cancel"
    buttonText="Hủy"
    @btn-click="$emit('btnCancelOnClick')"
    />
    <Button
    buttonId="btnSave"
    buttonClass="button button-icon btn-save"
    buttonText="Lưu"
    iClass="fa-save"
    @btn-click="$emit('btnSaveOnClick')"
    />
  </div>
</template>

<script>
import Button from "./BaseButton.vue"

export default {
  name: "BaseDialogFooter",
  components:{
      Button
  }
};
</script>

<style scoped>
.employee-profile-dialog .dialog-footer {
  height: 60px;
  width: 100%;
  float: left;
  background-color: #e9ebee;
  display: flex;
  justify-content: flex-end;
  align-items: center;
}

.employee-profile-dialog .dialog-footer button {
  width: 100px;
  height: 40px;
  margin-right: 25px;
  justify-content: center;
}

.employee-profile-dialog .dialog-footer .btn-cancel {
  border: none;
  background-color: #e9ebee;
  border-radius:4px ;
}

.employee-profile-dialog .dialog-footer .btn-cancel:hover{
  background-color: #e5e5e5;
}

.employee-profile-dialog .dialog-footer .btn-cancel i{
    display: none !important;
}
</style>