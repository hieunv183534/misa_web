<template>
  <div class="content">
      <ContentHeader/>
      <ContentBody/>
  </div>
</template>

<script>
import ContentHeader from "./TheContentHeader.vue"
import ContentBody from "./TheContentBody.vue"

export default {
    name: 'TheContent',
    components:{
        ContentHeader,ContentBody
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