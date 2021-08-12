<template>
    <input v-bind:class="[inputClass,{'border-red': isBorderRed}]"
           v-bind:type="inputType"
           v-bind:placeholder="inputPlacehoder"
           v-bind:value="inputValue"
           v-bind:id="inputId"
           @input="[$emit('inputOnInput'),$emit('input', $event.target.value)]"
           @blur="$emit('input-blur')"
           ref="autofocus"
            />
</template>

<script>
    export default {
        name: "BaseInput",
        props: {
            // để truyền vào class cho input
            inputClass: String,
            // để truyền vào type cho input (text/date/...)
            inputType: String,
            // để truyền vào placehoder cho input
            inputPlacehoder: String,
            // để truyền vào value cho input
            inputValue: String,
            // để truyền vào id cho input
            inputId: String,
            // để truyền vào requied cho input(có bắt buộc nhập hay không)
            inputRequied: {
                type: Boolean,
                default: false
            },
            isBorderRed: Boolean,
            isFocus:{
                type: Boolean,
                default: false
            }
        },
        watch:{
            isFocus(){
                this.$refs.autofocus.focus();
            }
        }
    };
</script>

<style scoped>
        input {
            width: 325px;
            height: 40px;
            padding: 10px 16px;
            border-radius: 4px;
            outline: none;
            border: solid 1px #bbbbbb;
            box-sizing: border-box;
            font-family: "GoogleSans-Regular";
            font-size: 13px;
        }

            input:focus {
                border-color: #019160;
            }

            input.border-red {
                border-color: #FF4747;
            }

            input.input-search {
                font-family: GoogleSans-Regular;
            }

        select.chondi {
            width: 325px;
            height: 40px;
            padding: 10px 16px;
            border-radius: 4px;
            outline: none;
            border: solid 1px #bbbbbb;
            box-sizing: border-box;
        }

        input.input-icon {
            background-image: url("../../assets/icon/search.png");
            background-repeat: no-repeat;
            background-position: 16px center;
            background-size: 16px;
            padding-left: 40px;
        }

        input#inputSalary {
            background-image: url("../../assets/img/vnd.jpg");
            background-repeat: no-repeat;
            background-position-x: 243px;
            background-position-y: 10px;
            background-size: 38px;
            padding-right: 50px;
        }

        .text-align-right{
            text-align: right;
        }
</style>