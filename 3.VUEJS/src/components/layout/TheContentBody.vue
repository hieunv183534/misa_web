<template>
  <div class="content-body">
    <ContentTitle/>
    <ContentToolbar/>
    <Grid/>
    <ContentPaging/>
  </div>
</template>

<script>
import ContentTitle from "./TheContentTitle.vue"
import ContentToolbar from "./TheContentToolbar.vue"
import Grid from "./TheGrid.vue"
import ContentPaging from "./TheContentPaging.vue"

export default {
    name:'TheContentBody',
    components:{
      ContentTitle, ContentToolbar, Grid, ContentPaging,
    }
};
</script>

<style scoped>
.content .content-body {
  width: 100%;
  height: calc(100% - 49px);
  float: left;
  padding: 24px;
  box-sizing: border-box;
}
</style>