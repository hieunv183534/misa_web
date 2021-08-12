<template>
  <div class="loader-wrapper">
    <div class="loader">
      <div class="loader-modal"></div>
      <div class="loader-img"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseLoader",
};
</script>

<style scoped>
.loader-wrapper{
    position: fixed;
    top: 0;
    left: 0;
    bottom: 0;
    right: 0;
}

.loader-wrapper .loader{
    width: 100%;
    height: 100%;
    position: relative;
}

.loader-wrapper .loader .loader-modal{
    width: 100%;
    height: 100%;
    background-color: #000000;
    opacity: 0.7;
}

.loader-wrapper .loader .loader-img{
    background-image: url("../../assets/img/loader.svg");
    width: 100px;
    height: 100px;
    position: absolute;
    top: 50%;
    left: 50%;
    margin-top:-50px ;
    margin-left:-50px ;
}
</style>