﻿<template>
    <div v-bind:class="['menu-item',menuItemType]">
        <div v-bind:class="['menu-icon', menuItemClass]"></div>
        <p v-if="isShowText">{{ menuItemText }}</p>
    </div>
</template>

<script>
    export default {
        name: "TheMenuItem",
        props: {
            // để truyền vào class cho menu item (để thay đổi hình ảnh trên item)
            menuItemClass: String,
            // để truyền vào text cho menu item
            menuItemText: String,
            // để truyền vào kiểu cho menu item
            menuItemType: String,
            // để thực hiện ẩn/hiện text(dùng khi collapse meunu)
            isShowText: Boolean
        },
    };
</script>

<style scoped>
    .menu .menu-body .menu-item {
        width: 100%;
        height: 44px;
        display: flex;
        cursor: pointer;
        text-decoration: none;
        transition: 0s;
    }

        .menu .menu-body .menu-item.menu-item-selected {
            background-color: #019160;
        }

        .menu .menu-body .menu-item .menu-icon {
            width: 20px;
            height: 20px;
            background-color: #fff;
            margin: auto 0;
            margin-left: 16px;
            margin-right: 10px;
            background-repeat: no-repeat;
            background-size: contain;
            transition: 0s;
        }

            .menu .menu-body .menu-item .menu-icon.dashboard-icon {
                background-image: url("../../assets/icon/dashboard.png");
            }

            .menu .menu-body .menu-item .menu-icon.document-online-icon {
                background-image: url("../../assets/icon/document-online.png");
            }

            .menu .menu-body .menu-item .menu-icon.dic-employee-icon {
                background-image: url("../../assets/icon/dic-employee.png");
            }

            .menu .menu-body .menu-item .menu-icon.report-icon {
                background-image: url("../../assets/icon/report.png");
            }

            .menu .menu-body .menu-item .menu-icon.setting-icon {
                background-image: url("../../assets/icon/setting.png");
            }

        .menu .menu-body .menu-item:hover {
            background-color: #e5e5e5;
            color: #000000;
            transition: 0s;
        }

            .menu .menu-body .menu-item:hover .menu-icon {
                background-color: #e5e5e5;
                transition: 0s;
            }

        .menu .menu-body .menu-item p {
            font-size: 13px;
            color: #515252;
            transition: 0.5s;
            overflow-x: hidden;
            white-space: nowrap;
        }

        .active{
            background-color: #019160 !important;
        }

        .active .menu-icon{
            background-color: #019160 !important;
        }

        .active p{
            color: #fff !important;
        }

</style>