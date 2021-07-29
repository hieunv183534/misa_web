<template>
  <div
    v-bind:class="['dropdown-item',dropdownItemClass]"
    v-bind:valueid="dropdownItemValueId"
    v-bind:valuename="dropdownItemValueName"
  >
    <i class="fas fa-check"></i>
    <p>{{dropdownItemValueName}}</p>
  </div>
</template>

<script>
export default {
  name: "BaseDropdownItem",
  props:{
    dropdownItemClass: String,
    dropdownItemValueId: String,
    dropdownItemValueName: String
  }
};
</script>

<style >
.dropdown .dropdown-data .dropdown-item {
  width: 100%;
  height: 40px;
  background-color: #ffffff;
  display: flex;
  align-items: center;
  cursor: pointer;
}

.dropdown .dropdown-data .dropdown-item:hover {
  background-color: #e9ebee;
}

.dropdown .dropdown-data .dropdown-item i {
  margin-left: 10px;
  margin-right: 10px;
  font-size: 16px;
  opacity: 0;
}

.dropdown .dropdown-data .dropdown-item.item-selected {
  background-color: #019160;
}

.dropdown .dropdown-data .dropdown-item.item-selected i {
  opacity: 1;
  color: #ffffff;
}

.dropdown .dropdown-data .dropdown-item.item-selected p {
  color: #ffffff;
}

.dropdown .dropdown-data .dropdown-item:first-child {
  margin-top: 5px;
}

.dropdown .dropdown-data .dropdown-item:last-child {
  margin-bottom: 5px;
}
</style>