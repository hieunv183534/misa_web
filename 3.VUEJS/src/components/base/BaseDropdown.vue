<template>
  <div v-bind:id="dropdownId" v-bind:class="['dropdown',dropdownClass]" style="border: none" v-bind:value="dropdownValue">
    <button class="dropdown-main">
      <p>{{dropdownText}}</p>
      <div class="icon"><i class="fas fa-chevron-down"></i></div>
    </button>
    <div class="dropdown-data">
        <DropdownItem
        dropdownItemClass="item-selected"
        dropdownItemValueId="Đông"
        dropdownItemValueName="Nhà hàng biển Đông"
        />
        <DropdownItem
        dropdownItemClass=""
        dropdownItemValueId="Tây"
        dropdownItemValueName="Nhà hàng biển Tây"
        />
        <DropdownItem
        dropdownItemClass=""
        dropdownItemValueId="Nam"
        dropdownItemValueName="Nhà hàng biển Nam"
        />
        <DropdownItem
        dropdownItemClass=""
        dropdownItemValueId="Bắc"
        dropdownItemValueName="Nhà hàng biển Bắc"
        />
    </div>
  </div>
</template>

<script>
import DropdownItem from "./BaseDropdownItem.vue"
export default {
    name:'BaseDropdown',
    components:{
        DropdownItem
    },
    props:{
        dropdownId : String,
        dropdownClass: String,
        dropdownValue: String,
        dropdownText: String
    }
};
</script>

<style scoped>
.dropdown {
  width: 200px;
  height: 40px;
  position: relative;
}

.dropdown p {
  margin: 0;
  line-height: 40px;
  font-size: 13px;
  margin-left: 10px;
  font-family: "GoogleSans-Regular";
}

.dropdown .dropdown-main {
  height: 40px;
  width: 100%;
  background-color: #ffffff;
  border: 1px solid #bbbbbb;
  border-radius: 4px;
  outline: none;
  box-sizing: border-box;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.dropdown .dropdown-main .icon {
  width: 20px;
  height: 20px;
  background-color: #ffffff;
  margin-right: 4px;
  margin-top: 7px;
}

.dropdown .dropdown-main .icon i {
  transition: 1s;
}

.dropdown .dropdown-main:focus .icon i {
  transform: rotate(180deg);
  transition: 1s;
}

.dropdown .dropdown-main:focus {
  border-color: #019160;
}

.dropdown .dropdown-data {
  position: absolute;
  border-radius: 4px;
  top: 40px;
  left: 0;
  width: 100%;
  height: 0;
  background-color: white;
  /* display: none; */
  transition: 1s;
  overflow-y: scroll;
  /*overflow-y: overlay;*/

}

.dropdown .dropdown-main:focus ~ .dropdown-data {
  /* height: auto;
    max-height: 210px; */
  height: 140px;
  transition: 1s;
  background-color: white;
  z-index: 99;
  box-shadow: 1px 2px 4px 1px #454545;
}


.dropdown.last-dropdown .dropdown-data {
  top: 0px;
}

.dropdown.last-dropdown .dropdown-main:focus ~ .dropdown-data {
  /* height: auto;
    max-height: 210px; */
  height: 140px;
  transition: 1s;
  background-color: white;
  z-index: 99;
  box-shadow: 1px -2px 4px 1px #454545;
  top: -140px;
}

/*dropdown restaurant*/

.content .content-header .header-left .dropdown {
  width: 190px;
}

.content .content-header .header-left .dropdown .dropdown-main {
  border: none;
}

.content .content-header .header-left .dropdown .dropdown-main p {
  font-weight: bold;
  font-size: 15px;
}

/* dropdown restaurant */

 .dropdown-restaurant{
    width:190px;
}

.dropdown-restaurant .dropdown-main {
    border: none;
}

.dropdown-restaurant .dropdown-main p{
    font-weight:bold;
    font-size:15px;
}
</style>