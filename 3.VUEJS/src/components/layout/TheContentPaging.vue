<template>
  <div class="content-paging">
    <div class="paging-left">
      <p>Hiển thị <b>1-10/1000</b> nhân viên</p>
    </div>
    <div class="paging-center">
      <Button
        buttonId=""
        buttonClass="btn-inline"
        iClass="fa-angle-double-left"
        buttonText=""
      />
      <Button
        buttonId=""
        buttonClass="btn-inline"
        iClass="fa-angle-left"
        buttonText=""
      />
      <Button
        buttonId=""
        buttonClass="circle"
        iClass=""
        buttonText="1"
      />
       <Button
        buttonId=""
        buttonClass="circle"
        iClass=""
        buttonText="2"
      />
       <Button
        buttonId=""
        buttonClass="circle"
        iClass=""
        buttonText="3"
      />
       <Button
        buttonId=""
        buttonClass="circle"
        iClass=""
        buttonText="4"
      />
      <Button
        buttonId=""
        buttonClass="btn-inline"
        iClass="fa-angle-right"
        buttonText=""
      />
      <Button
        buttonId=""
        buttonClass="btn-inline"
        iClass="fa-angle-double-right"
        buttonText=""
      />
    </div>
    <div class="paging-right">
      <p><b>10</b> nhân viên/trang</p>
      <div class="up-and-down">
        <button><i class="fas fa-chevron-up"></i></button>
        <button><i class="fas fa-chevron-down"></i></button>
      </div>
    </div>
  </div>
</template>

<script>
import Button from "../base/BaseButton.vue"
export default {
  name: "TheContentPaging",
  components:{
    Button
  }
};
</script>

<style scoped>
.content .content-body .content-paging {
  width: 100%;
  height: 56px;
  float: left;
  position: relative;
}

.content .content-body .content-paging .paging-left {
  height: 100%;
  width: 300px;
  position: absolute;
  left: 0;
}

.content .content-body .content-paging .paging-left > p {
  line-height: 56px;
  margin: 0;
  text-align: left;
}

.content .content-body .content-paging .paging-center {
  width: 400px;
  height: 100%;
  position: absolute;
  left: 50%;
  margin-left: -200px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.content .content-body .content-paging .paging-right {
  height: 40px;
  width: 150px;
  border-radius:4px ;
  position: absolute;
  right: 0;
  top: 8px;
  background-color: #fff;
  display: flex;
  justify-content: center;
  align-items: center;
}

.content .content-body .content-paging .paging-right > p {
  display: block;
  line-height: 40px;
  float: left;
  width: 125px;
  text-align: center;
}

.up-and-down{
  width: 25px;
  height: 60%;
  float: left;
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding-bottom: 2px;
  box-sizing: border-box;
}

.up-and-down button{
  width: 100%;
  height: 50%;
  float: left;
  border: none;
  outline: none;
  background-color: #fff;
}
</style>