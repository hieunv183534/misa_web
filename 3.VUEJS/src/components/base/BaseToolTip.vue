<template>
    <div class="messenger-wrapper">
        <div v-if="tooltipType == 'warning'"
             :class="['toast-messenger warning', tooltipScaleClass]"
             scale="0"
             v-bind:type="tooltipType">
            <i class="fas fa-exclamation-circle"></i>
            <p>{{ tooltipText }}</p>
            <button><i class="fas fa-times" @click="closeToolTip"></i></button>
        </div>
        <div v-else-if="tooltipType == 'danger'"
             :class="['toast-messenger danger', tooltipScaleClass]"
             scale="0"
             v-bind:type="tooltipType">
            <i class="fas fa-exclamation-triangle"></i>
            <p>{{ tooltipText }}</p>
            <button><i class="fas fa-times" @click="closeToolTip"></i></button>
        </div>
        <div v-else-if="tooltipType == 'success'"
             :class="['toast-messenger success', tooltipScaleClass]"
             scale="0"
             v-bind:type="tooltipType">
            <i class="fas fa-check-circle"></i>
            <p>{{ tooltipText }}</p>
            <button><i class="fas fa-times" @click="closeToolTip"></i></button>
        </div>
        <div v-else
             :class="['toast-messenger primary', tooltipScaleClass]"
             scale="0"
             v-bind:type="tooltipType">
            <i class="fas fa-exclamation"></i>
            <p>{{ tooltipText }}</p>
            <button><i class="fas fa-times" @click="closeToolTip"></i></button>
        </div>
    </div>
</template>

<script>
    export default {
        name: "BaseToolTip",
        props: {
            // để truyền vào text cho ToolTip
            tooltipText: String,
            // để truyền vào kiểu của Tooltip (warning/danger/success/primary)
            tooltipType: String,
            // để truyền vào scale1= hiện / scale0= ẩn ToolTip . kết hợp với transition tạo hiệu ứng :)
            tooltipScaleClass: String,
        },
        watch: {
            // khi hiện ToolTip thì auto sau 3 giây sẽ ẩn đi
            tooltipScaleClass() {
                if (this.tooltipScaleClass == "scale1") {
                    setTimeout(() => {
                        this.closeToolTip();
                    }, 3000);
                }
            },
        },
        methods: {
            // ẩn ToolTip khi click vào btn X trên toolTip
            closeToolTip() {
                this.$emit("closeToolTip");
            },
        },
    };
</script>

<style scoped>
    .messenger-wrapper {
        position: fixed;
        top: 4px;
        left: 50%;
        margin-left: -165px;
        z-index: 50000;
    }

    .toast-messenger {
        width: 330px;
        height: 48px;
        display: flex;
        align-items: center;
        padding-left: 24px;
        position: relative;
        border-radius: 4px;
        box-sizing: border-box;
        transform: scale(0);
        transition: 0.5s;
        box-shadow: 0px 3px 6px 0px rgba(0, 0, 0, 0.16);
        z-index: 50000;
        background-color: #ffffff;
        opacity: 1;
    }

        .toast-messenger p {
            line-height: 48px;
            margin-left: 10px;
        }

        .toast-messenger i {
            font-size: 24px;
        }

        .toast-messenger button {
            position: absolute;
            right: 10px;
            border: none;
            outline: none;
            background-color: #fff;
            border-radius: 50px;
        }

            .toast-messenger button:hover {
                background-color: #f1f1f1;
            }

        .toast-messenger.danger i {
            color: #eb5757;
        }

        .toast-messenger.success i {
            color: #01b075;
        }

        .toast-messenger.warning i {
            color: #f1c04e;
        }

        .toast-messenger.primary i {
            color: #eb5757;
        }

    .scale1 {
        transform: scale(1);
    }

    .scale0 {
        transform: scale(0);
    }
</style>