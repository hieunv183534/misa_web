<template>
  <div class="content-header">
    <div class="header-left">
      <Dropdown
      dropdownId=""
      dropdownClass="dropdown-restaurant"
      dropdownValue="Đông"
      dropdownText="Nhà hàng biển đông"
      />
    </div>
    <div class="header-right">
      <div class="avatar"></div>
      <p>Nguyễn Văn Mạnh</p>
      <div class="option-icon"></div>
    </div>
  </div>
</template>

<script>
import Dropdown from "../base/BaseDropdown.vue"
export default {
    name:'TheContentHeader',
    components:{
      Dropdown
    }
};
</script>

<style scoped>
.content .content-header {
  width: 100%;
  height: 48px;
  float: left;
  border-bottom: solid 1px #e4e4e4;
  display: flex;
  justify-content: space-between;
}

.content .content-header .header-left {
  height: 100%;
  width: 300px;
  display: flex;
  align-items: center;
}

.content .content-header .header-right {
  height: 100%;
  width: 300px;
  display: flex;
  align-items: center;
  justify-content: flex-end;
}

.content .content-header .header-right .avatar {
  width: 33px;
  height: 33px;
  background-image: url("../../assets/img/avatar-default.png");
  background-repeat: no-repeat;
  background-size: contain;
  margin-right: 16px;
}

.content .content-header .header-right p {
  display: block;
  font-size: 13px;
  line-height: 48px;
  margin-right: 16px;
}

.content .content-header .header-right .option-icon {
  width: 20px;
  height: 20px;
  background-image: url("../../assets/icon/option.png");
  background-repeat: no-repeat;
  background-size: contain;
  margin-right: 16px;
}
</style>