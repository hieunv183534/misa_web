<template>
  <div id="app">
    <Menu 
    :isCollapseMenu="isCollapseMenu"
    @btnToggleOnClick="btnToggleOnClick" />
    <Content
      @showAddForm="showAddForm"
      @showEditForm="showEditForm1"
      @showDeletePopup="showDeletePopup"
      @showCopyPopup="showCopyPopup"
      :isCollapseMenu="isCollapseMenu"
    />
    <ToolTip
      :tooltipText="tooltipText"
      :tooltipType="tooltipType"
      :tooltipScaleClass="tooltipScaleClass"
      @closeToolTip="closeToolTip"
    />
    <Popup
      :class="{ 'popup-show': isShowPopup }"
      :popupTitle="popupTitle"
      :popupContent="popupContent"
      :popupType="popupType"
      @hidePopup1="hidePopup"
      @hideDialog="hideDialog"
      :mode="popupMode"
    />
    <EmployeeDialog
      :isShow="isShowAddDialog"
      :mode="forMode"
      :myEmployeeId="employeeId"
      :isReOpen="reOpen"
      @hideDialog="hideForm"
    />
    <Loader v-if="isLoad"/>
  </div>
</template>

<script>
import { eventBus } from "./main.js";
import { eventBus1 } from "./main.js";
import Menu from "./components/layout/TheMenu.vue";
import Content from "./components/layout/TheContent.vue";
import ToolTip from "./components/base/BaseToolTip.vue";
import Popup from "./components/base/BasePopup.vue";
import EmployeeDialog from "./view/employee/EmployeeDialog.vue";
import Loader from "./components/base/BaseLoader.vue"

export default {
  name: "App",
  components: {
    Menu,
    Content,
    ToolTip,
    Popup,
    EmployeeDialog,
    Loader,
  },
  created() {
    eventBus.$on("showTooltipDeleteSuccess", () => {
      this.tooltipType = "success";
      this.tooltipText = "Đã xóa thành công !";
      this.tooltipScaleClass = "scale1";
    });
    eventBus.$on("showTooltipAddSuccess", () => {
      this.tooltipType = "success";
      this.tooltipText = "Đã thêm mới thành công !";
      this.tooltipScaleClass = "scale1";
    });
    eventBus.$on("showTooltipCopySuccess", () => {
      this.tooltipType = "success";
      this.tooltipText = "Đã nhân bản thành công thành công !";
      this.tooltipScaleClass = "scale1";
    });
    eventBus.$on("showTooltipUpdateSuccess", () => {
      this.tooltipType = "success";
      this.tooltipText = "Đã cập nhật thành công !";
      this.tooltipScaleClass = "scale1";
    }); 
    eventBus1.$on("showTooltipInputRequied", () => {
      this.tooltipType = "danger";
      this.tooltipText = "Thông tin này bắt buộc nhập !";
      this.tooltipScaleClass = "scale1";
    }); 
    eventBus1.$on("showTooltipInvalidEmail", () => {
      this.tooltipType = "warning";
      this.tooltipText = "Email chưa đúng định dạng !";
      this.tooltipScaleClass = "scale1";
    });
    eventBus1.$on("showTooltipInvalidSalary", () => {
      this.tooltipType = "warning";
      this.tooltipText = "Lương chưa đúng định dạng !";
      this.tooltipScaleClass = "scale1";
    });
    eventBus1.$on("showPopupConfirmAdd", () => {
      this.isShowPopup = true;
      this.popupContent = "Bạn có muốn thêm nhân viên này vào danh sách không?";
      this.popupType = "warning";
      this.popupTitle = "Thêm mới bản ghi";
      this.popupMode="addOrEdit"
    });
    eventBus1.$on("showPopupConfirmUpdate", () => {
      this.isShowPopup = true;
      this.popupContent = "Bạn có muốn cập nhật thông tin nhân viên này không?";
      this.popupType = "warning";
      this.popupTitle = "Cập nhật bản ghi";
      this.popupMode="addOrEdit"
    });
    eventBus1.$on("showTooltipInputRequiedAll", () => {
      this.tooltipType = "danger";
      this.tooltipText = "Chưa nhập đủ các trường bắt buộc !";
      this.tooltipScaleClass = "scale1";
    });
    eventBus1.$on('showLoader',()=>{
      this.isLoad = true;
    })
    eventBus1.$on('hideLoader',()=>{
      this.isLoad = false;
    })
  },
  data() {
    return {
      isCollapseMenu: false,
      isShowAddDialog: false,
      isLoad: false,
      isShowDialog: false,
      forMode: 1,
      employeeId: "",
      reOpen: false,
      isShowPopup: false,
      popupContent: "",
      popupType: "",
      popupMode: "",
      popupTitle: "",
      tooltipText: "",
      tooltipType: "",
      tooltipScaleClass: "scale0",
    };
  },
  methods: {
    showAddForm() {
      this.isShowAddDialog = true;
      this.forMode = 1;
      this.employeeId = "";
      this.reOpen = !this.reOpen;
    },
    hideForm() {
      this.isShowAddDialog = false;
    },
    showEditForm1(id) {
      this.isShowAddDialog = true;
      this.forMode = 0;
      this.employeeId = id;
      this.reOpen = !this.reOpen;
    },
    showDeletePopup() {
      this.isShowPopup = true;
      this.popupContent =
        "Bạn có muốn xóa các bản ghi này không?";
      this.popupType = "danger";
      this.popupTitle = "Xóa các bản ghi";
    },
    showCopyPopup() {
      this.isShowPopup = true;
      this.popupContent =
        "Bạn có muốn nhân bản nhân viên này không?";
      this.popupType = "warning";
      this.popupTitle = "Nhân bản bản ghi";
      this.popupMode="copy";
    },
    hidePopup() {
      this.isShowPopup = false;
    },
    closeToolTip() {
      this.tooltipScaleClass = "scale0";
    },
    btnToggleOnClick() {
      this.isCollapseMenu = !this.isCollapseMenu;
      eventBus1.$emit('collapseMenu',this.isCollapseMenu);
    },
    hideDialog(){
      this.isShowAddDialog = false;
    }
  },
};
</script>

<style>
@import "./assets/font/fontawesome/css/all.css";

@font-face {
  font-family: "GoogleSans-Bold";
  src: url("assets/font/GoogleSans-Bold.otf") format("opentype");
}
@font-face {
  font-family: "GoogleSans-Italic";
  src: url("assets/font/GoogleSans-Italic.otf") format("opentype");
}
@font-face {
  font-family: "GoogleSans-Regular";
  src: url("assets/font/GoogleSans-Regular.otf") format("opentype");
}
@font-face {
  font-family: "GoogleSans-Thin";
  src: url("assets/font/GoogleSans-Thin.otf") format("opentype");
}

::-webkit-scrollbar {
  width: 2px;
  background-color: #ffffff;
  border-radius: 4px;
}

::-webkit-scrollbar-thumb {
  background-color: grey;
  border-radius: 4px;
}

.grid *:hover::-webkit-scrollbar {
  width: 8px;
  background-color: lightgray;
}

body {
  margin: 0;
  padding: 0;
  font-family: GoogleSans-Regular;
  font-size: 13px;
}

button {
  cursor: pointer;
}
</style>
