﻿<template>
    <button v-bind:class="buttonClass" v-bind:id="buttonId" @click="$emit('btn-click')">
        <i v-if="iClass !== ''" v-bind:class="['fas' , iClass]"></i>
        <p>{{buttonText}}</p>
    </button>
</template>

<script>
    export default {
        name: "Button",
        props: {
            // để truyền vào id cho btn
            buttonId: String,
            //để truyền vào text cho btn
            buttonText: String,
            //để truyền vào class cho icon (fontawesome)
            iClass: {
                type: String,
                default: ''
            },
            // để truyền vào class cho btn
            buttonClass: String
        }
    };
</script>

<style scoped>
    button {
        cursor: pointer;
        outline: none;
    }

    .button {
        height: 40px;
        min-width: 100px;
        background-color: #019160;
        border-radius: 4px;
        font-size: 13px;
        color: #ffffff;
        display: flex;
        padding-left: 24px;
        padding-right: 24px;
        align-items: center;
        border: 1px solid #019160;
        outline: none;
    }

        .button:hover {
            background-color: #2fbe8e;
            border: 1px solid #2fbe8e;
        }
        /*
    button p {
        display: block;
        line-height: 40px;
    }

    button i {
        display: block;
        line-height: 40px;
    }*/

        .button.button-icon {
            padding-left: 16px;
            padding-right: 16px;
            min-width: 50px;
        }

            .button.button-icon i {
                margin-right: 8px;
            }

        .button.button-refresh {
            border: 1px solid #bbbbbb;
            background-color: #ffffff;
            width: 45px;
            min-width: 45px;
            color: #000000;
            padding: 0;
        }

            .button.button-refresh:hover {
                background-color: #e9ebee;
                border: 1px solid #e9ebee;
            }

            .button.button-refresh i {
                margin: auto 16px;
            }

    .content .content-body .content-paging .paging-center button {
        height: 40px;
    }

        .content .content-body .content-paging .paging-center button.circle {
            width: 40px;
            height: 40px;
            border-radius: 50%;
            border: 1px solid #bbbbbb;
            display: flex;
            align-items: center;
            justify-content: center;
            background-color: #E9EBEE;
        }

            .content .content-body .content-paging .paging-center button.circle:hover {
                background-color: #ffffff;
            }

    /*.content .content-body .content-paging .paging-center button*/
    .btn-inline {
        border: none;
        outline: none;
        background-color: #ffffff;
        display: flex;
        align-items: center;
    }

.content .content-body .content-paging .paging-center button.btn-inline{
    background-color: #E9EBEE;
    width: 40px;
    height: 40px;
    border-radius: 4px;
    justify-content: center;

}

.content .content-body .content-paging .paging-center button.btn-inline:hover{
    background-color: #fff;
}

    .content .content-body .content-paging .paging-center button.btn-inline i {
        font-size: 20px;
    }


    /* paging */

    .button-page-active {
  background-color: #019160 !important;
}

.button-page-active p{
    color: #fff;
}

.button-page-active:hover {
  background-color: #019160 !important;
}

</style>