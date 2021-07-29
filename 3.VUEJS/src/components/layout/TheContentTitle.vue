<template>
  <div class="content-title">
    <b>Danh sách nhân viên</b>
    <span>
      <Button
        buttonId="btnDelete"
        buttonClass="button button-icon"
        iClass="fa-trash-alt"
        buttonText="Xóa nhân viên"
      />
      <Button
        buttonId="BtnAdd"
        buttonClass="button button-icon"
        iClass="fa-user-plus"
        buttonText="Thêm nhân viên"
      />
    </span>
  </div>
</template>

<script>
import Button from "../base/BaseButton.vue";

export default {
  name: "TheContentTitle",
  components: {
    Button,
  },
};
</script>

<style scoped>
.content .content-body .content-title {
  width: 100%;
  height: 40px;
  float: left;
  display: flex;
  justify-content: space-between;
  margin-bottom: 10px;
}

.content .content-body .content-title > b {
  line-height: 40px;
  font-size: 20px;
}

.content .content-body .content-title > span {
  width: auto;
  display: flex;
  justify-content: flex-end;
}

.content .content-body .content-title > span #btnDelete {
  margin-right: 16px;
  visibility: visible;
}
</style>