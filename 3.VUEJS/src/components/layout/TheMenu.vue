<template>
    <div class="menu" :class="{'collapse-menu': isCollapseMenu}">
        <div class="menu-header">
            <button class="toggle" @click="$emit('btnToggleOnClick')"></button>
            <div v-if="!isCollapse" class="logo"></div>
        </div>
        <div class="menu-body">
            <MenuItem
            menuItemClass="dashboard-icon"
            menuItemText="Tổng quan"
            :isShowText="!isCollapse"
            />
            <MenuItem
            menuItemClass="document-online-icon"
            menuItemText="Hồ sơ online"
            :isShowText="!isCollapse"
            />
            <MenuItem
            menuItemClass="report-icon"
            menuItemText="Theo dõi chi"
            :isShowText="!isCollapse"
            />
            <MenuItem
            menuItemClass="dashboard-icon"
            menuItemText="Danh mục khách hàng"
            :isShowText="!isCollapse"
            />
            <MenuItem
            menuItemClass="dic-employee-icon"
            menuItemText="Danh mục nhân viên"
            :isShowText="!isCollapse"
            />
            <MenuItem
            menuItemClass="setting-icon"
            menuItemText="Thiết lập"
            :isShowText="!isCollapse"
            />
        </div>
    </div>
</template>

<script>
import MenuItem from "./TheMenuItem.vue"

export default {
    name: 'TheMenu',
    components:{
        MenuItem
    },
    props:{
        isCollapseMenu: Boolean,
    },
    data(){
        return{
            isCollapse: false
        }
    },
    watch:{
        isCollapseMenu(){
            if(this.isCollapseMenu == true){
                this.isCollapse = this.isCollapseMenu;
            }else{
                setTimeout(()=>{
                    this.isCollapse = this.isCollapseMenu;
                },500);
            }
        }
    }
}
</script>

<style scoped>
.menu {
    position: relative;
    width: 225px;
    height: 100vh;
    border-right: solid 1px #e4e4e4;
    float: left;
    transition: 0.5s;
    overflow: hidden;
}

.collapse-menu{
    width: 52px;
}

    .menu .menu-header {
        width: 100%;
        height: 48px;
        display: flex;
        align-items: center;
        transition: 0.5s;
    }

        .menu .menu-header .toggle {
            background-color: #fff;
            height: 24px;
            width: 24px;
            margin: 12px 0px;
            margin-right: 10px;
            margin-left: 16px;
            background-image: url('../../assets/icon/toggle.png');
            background-repeat: no-repeat;
            background-size: contain;
            outline: none;
            border: none;
        }

        .menu .menu-header .logo {
            background-color: #fff;
            height: 24px;
            width: 100px;
            background-image: url('../../assets/img/cukcuk-logo.png');
            background-repeat: no-repeat;
            background-size: contain;
            transition: 0.5s;
        }

    .menu .menu-body {
        width: 100%;
        transition: 0.5s;
    }

        
    /*.menu .menu-out {
        position: absolute;
        width: 24px;
        height: 24px;
        background-color: aqua;
        transform: rotate(45deg);
        right: -13px;
        top: 60px;
    }

    .menu .menu-in {
        position: absolute;
        width: 24px;
        height: 24px;
        background-color: greenyellow;
        transform: rotate(45deg);
        left: -13px;
        top: 60px;
    }*/

</style>