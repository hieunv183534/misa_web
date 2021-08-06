<template>
    <div class="dialog-header">
        <p>{{ dialogTitle }}</p>
        <Button buttonId="btnExit"
                buttonText=""
                buttonClass="btn-exit"
                iClass="fa-times"
                @btn-click="$emit('btnExitOnClick')" />
    </div>
</template>

<script>
    import Button from "./BaseButton.vue";
    export default {
        name: "BaseDialogHeader",
        components: {
            Button
        },
        props: {
            //để truyền vào title hiển thị trên form header
            dialogTitle: String
        }
    };
</script>

<style scoped>
    .employee-profile-dialog .dialog-header {
        height: 72px;
        width: 100%;
        float: left;
        position: relative;
    }

        .employee-profile-dialog .dialog-header p {
            font-size: 24px;
            font-weight: bolder;
            display: block;
            line-height: 72px;
            padding-left: 24px;
            margin: 0;
        }

        .employee-profile-dialog .dialog-header button.btn-exit {
            position: absolute;
            top: 0;
            right: 0;
            width: 30px;
            height: 30px;
            outline: none;
            border: navajowhite;
            background-color: #ffffff;
            display: flex;
            align-items: center;
            justify-content: center;
            border-bottom-left-radius: 4px;
            border-top-right-radius: 4px;
        }

            .employee-profile-dialog .dialog-header button.btn-exit:hover {
                background-color: #E5E5E5;
            }

        .employee-profile-dialog .dialog-header button i {
            font-size: 21px;
        }
</style>