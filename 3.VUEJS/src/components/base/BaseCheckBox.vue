<template>
    <div class="checkbox" @click="clickCheckBox" v-bind:class="{'checkbox-check': isCheck , 'checkbox-no-check': !isCheck }">
        <i v-if="isCheck" class="fas fa-check"></i>
    </div>
</template>

<script>
export default {
    name: 'BaseCheckBox',
    data(){
        return{
            isCheck: false,
        }
    },
    props:{
        chechOrNo: Boolean
    },
    methods:{
        clickCheckBox(){
            this.$emit('checkboxOnClick');

            // thay đổi trang thái
            this.isCheck = !this.isCheck;
        }
    },
    watch:{
        chechOrNo(){
            this.isCheck = this.chechOrNo;
        }
    }
}
</script>

<style scoped>

.checkbox-check{
    width: 20px;
    height: 20px;
    background-color: #019160;
    border-radius: 4px;
    border: none;
    color: #fff;
    display: flex;
    align-items: center;
    justify-content: center;
    margin: auto;
    border: 1px solid #019160;
}

.checkbox-no-check{
    width: 20px;
    height: 20px;
    background-color: #fff;
    border-radius: 4px;
    border: 1px solid #bbbbbb;
    display: flex;
    align-items: center;
    justify-content: center;
    margin: auto;
}

</style>