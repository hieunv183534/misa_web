<template>
    <div class="title-info">
              <p>{{dialogTitleInfo}}</p>
              <div class="layout"></div>
            </div>
</template>

<script>
export default {
    name: "BaseDialogTitleInfo",
    props:{
        dialogTitleInfo: String
    }
}
</script>

<style scoped>
.employee-profile-dialog .dialog-content .dialog-content-right .title-info {
  width: 100%;
  height: 30px;
  position: relative;
  margin-bottom: 24px;
}

.employee-profile-dialog .dialog-content .dialog-content-right .title-info p {
  font-weight: bold;
  font-size: 14px;
}

.employee-profile-dialog
  .dialog-content
  .dialog-content-right
  .title-info
  .layout {
  width: 77px;
  height: 5px;
  background-color: #019160;
  position: absolute;
  left: 0;
  bottom: 0;
}
</style>