<template>
  <div id="app">
    <Menu/>
    <Content/>
    <ToolTip
    tooltipText="Vui lòng nhập đủ các trường!"
    tooltipIClass="fa-exclamation-triangle"
    />
    <Popup
    popupTitle="Xóa nhân viên"
    popupContent="Bạn có muốn xóa nhân viên này không?"
    />
  </div>
</template>

<script>
// import Button from "./components/base/BaseButton.vue";
import Menu from "./components/layout/TheMenu.vue"
import Content from "./components/layout/TheContent.vue"
import ToolTip from "./components/base/BaseToolTip.vue"
import Popup from "./components/base/BasePopup.vue"

export default {
  name: "App",
  components: {
    Menu,Content,ToolTip,Popup
  },
};
</script>

<style>
    @import './assets/font/fontawesome/css/all.css';

    @font-face {
        font-family: 'GoogleSans-Bold';
        src: url('assets/font/GoogleSans-Bold.otf') format('opentype');
    }
    @font-face {
        font-family: 'GoogleSans-Italic';
        src: url('assets/font/GoogleSans-Italic.otf') format('opentype');
    }
    @font-face {
        font-family: 'GoogleSans-Regular';
        src: url('assets/font/GoogleSans-Regular.otf') format('opentype');
    }
    @font-face {
        font-family: 'GoogleSans-Thin';
        src: url('assets/font/GoogleSans-Thin.otf') format('opentype');
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

/*.dropdown * :-webkit-scrollbar {
    width: 1px;
    background-color: #ffffff;
    border-radius: 1px;
}

.dropdown *:-webkit-scrollbar-thumb {
    background-color: grey;
    border-radius: 1px;
}
*/


body{
    margin: 0;
    padding: 0;
    font-family: GoogleSans-Regular;
    font-size:13px;
}

button{
    cursor:pointer;
}

</style>
