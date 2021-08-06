<template>
    <input type="checkbox" style="width: 46px; height: 24px" v-bind:value="isChecked" @click="$emit('click', $event.target.value)" />
</template>

<script>
    export default {
        name: "BaseInputCheckbox",
        props: {
            // để truyền vào check hay ko check (true/false)
            isChecked: Boolean,
            // để truyền vào id cho checkbox
            checkboxId: String,
        },
    };
</script>

<style scoped>
</style>