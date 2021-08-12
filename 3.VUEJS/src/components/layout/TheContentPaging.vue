<template>
  <div class="content-paging">
    <div class="paging-left">
      <p>
        Hiển thị <b>{{ startRecord }}-{{ endRecord }}/{{ totalRecord }}</b> nhân
        viên
      </p>
    </div>
    <div class="paging-center">
      <Button
        buttonId=""
        buttonClass="btn-inline"
        iClass="fa-angle-double-left"
        buttonText=""
        @btn-click="btnDoubleBackOnClick"
      />
      <Button
        buttonId=""
        buttonClass="btn-inline"
        iClass="fa-angle-left"
        buttonText=""
        @btn-click="btnBackOnClick"
      />
      <Button
        v-for="currentShowPage in currentShowPages"
        :key="currentShowPage"
        buttonId=""
        buttonClass="circle"
        :buttonText="currentShowPage+''"
        :class="{ 'button-page-active': currentShowPage == currentPage }"
        @btn-click="$emit('changeCurrentPage', currentShowPage)"
      />
      <Button
        buttonId=""
        buttonClass="btn-inline"
        iClass="fa-angle-right"
        buttonText=""
        @btn-click="btnNextOnClick"
      />
      <Button
        buttonId=""
        buttonClass="btn-inline"
        iClass="fa-angle-double-right"
        buttonText=""
        @btn-click="btnDoubleNextOnClick"
      />
    </div>
    <div class="paging-right">
      <p>
        <b>{{ pagingSize }}</b> nhân viên/trang
      </p>
      <div class="up-and-down">
        <button @click="pagingSizePlus">
          <i class="fas fa-chevron-up"></i>
        </button>
        <button @click="pagingSizeMinus">
          <i class="fas fa-chevron-down"></i>
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import Button from "../base/BaseButton.vue";
export default {
  name: "TheContentPaging",
  components: {
    Button,
  },
  props: {
    totalRecord: Number,
    pagingSize: {
      default: 10,
      type: Number,
    },
    currentPage: Number,
  },
  data() {
    return {
      startRecord: 0,
      endRecord: 0,
      currentShowPages: [],
      totalPage: 0,
    };
  },
  methods: {
    pagingSizePlus() {
      this.$emit("changePagingSize", this.pagingSize + 10);
      this.$emit("changeCurrentPage", 1);
    },
    pagingSizeMinus() {
      if (this.pagingSize > 10) {
        this.$emit("changePagingSize", this.pagingSize - 10);
        this.$emit("changeCurrentPage", 1);
      }
    },
    reloadPagingBar() {
      // load giá trị start và end r
      this.startRecord = this.pagingSize * (this.currentPage - 1) + 1;
      this.endRecord = this.startRecord + this.pagingSize - 1;
      if (this.endRecord > this.totalRecord) {
        this.endRecord = this.totalRecord;
      }
      // tính toán mảng các choose paging từ các dữ liệu
      this.totalPage = Math.ceil(this.totalRecord / this.pagingSize);
      if (this.totalPage <= 7) {
        this.currentShowPages = [];
        for (let i = 1; i <= this.totalPage; i++) {
          this.currentShowPages.push(i);
        }
      } else {
        this.currentShowPages = [];
        if (this.totalPage - this.currentPage >= 3 && this.currentPage >= 4) {
          let start = this.currentPage - 3;
          for (let i = start; i <= start + 6; i++) {
            this.currentShowPages.push(i);
          }
        } else if (this.currentPage <= 3) {
          this.currentShowPages = [1, 2, 3, 4, 5, 6, 7];
        } else {
          let start = this.totalPage - 6;
          for (let i = start; i <= start + 6; i++) {
            this.currentShowPages.push(i);
          }
        }
      }
    },
    btnDoubleBackOnClick() {
      this.$emit("changeCurrentPage", 1);
    },
    btnBackOnClick() {
      if (this.currentPage > 1) {
        this.$emit("changeCurrentPage", this.currentPage - 1);
      }
    },
    btnNextOnClick() {
      if (this.currentPage < this.totalPage) {
        this.$emit("changeCurrentPage", this.currentPage + 1);
      }
    },
    btnDoubleNextOnClick() {
      this.$emit("changeCurrentPage", this.totalPage);
    },
  },
  watch: {
    totalRecord() {
      this.reloadPagingBar();
    },
    pagingSize() {
      this.reloadPagingBar();
    },
    currentPage() {
      this.reloadPagingBar();
    },
  },
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
  width: auto;
  height: 100%;
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.content .content-body .content-paging .paging-center button {
  margin-left: 5px;
  margin-right: 5px;
}

.content .content-body .content-paging .paging-right {
  height: 40px;
  width: 150px;
  border-radius: 4px;
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

.up-and-down {
  width: 25px;
  height: 60%;
  float: left;
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding-bottom: 2px;
  box-sizing: border-box;
}

.up-and-down button {
  width: 100%;
  height: 50%;
  float: left;
  border: none;
  outline: none;
  background-color: #fff;
}
</style>