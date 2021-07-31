<template>
  <input type="checkbox" style="width: 46px; height: 24px" v-bind:value="isChecked" @click="$emit('click', $event.target.value)" />
</template>

<script>
export default {
  name: "BaseInputCheckbox",
  props: {
    isChecked: Boolean,
    checkboxId: String,
  },
};
</script>

<style scoped>
</style>