<template>
  <div
    v-bind:id="dropdownId"
    v-bind:class="['dropdown', dropdownClass]"
    style="border: none"
    v-bind:value="dropdownValue"
  >
    <button class="dropdown-main">
      <p>{{ dropdownText }}</p>
      <div class="icon"><i class="fas fa-chevron-down"></i></div>
    </button>
    <div v-if="dropdownTitle == 'Positions'" class="dropdown-data">
      <DropdownItem
        v-for="itemDropdown in itemDropdowns"
        :key="itemDropdown.PositionId"
        :class="{ 'item-selected': itemDropdown.PositionId == dropdownValue }"
        dropdownItemClass=""
        :dropdownItemValueId="itemDropdown.PositionId"
        :dropdownItemValueName="itemDropdown.PositionName"
        @itemClick="itemOnClick"
      />
    </div>
    <div v-else-if="dropdownTitle == 'Positions1'" class="dropdown-data">
      <DropdownItem
        :class="{ 'item-selected': '' == dropdownValue }"
        dropdownItemClass=""
        dropdownItemValueId=""
        dropdownItemValueName="Tất cả vị trí"
        @itemClick="itemOnClick"
      />
      <DropdownItem
        v-for="itemDropdown in itemDropdowns"
        :key="itemDropdown.PositionId"
        :class="{ 'item-selected': itemDropdown.PositionId == dropdownValue }"
        dropdownItemClass=""
        :dropdownItemValueId="itemDropdown.PositionId"
        :dropdownItemValueName="itemDropdown.PositionName"
        @itemClick="itemOnClick"
      />
    </div>
    <div v-else-if="dropdownTitle == 'Departments'" class="dropdown-data">
      <DropdownItem
        v-for="itemDropdown in itemDropdowns"
        :key="itemDropdown.DepartmentId"
        :class="{ 'item-selected': itemDropdown.DepartmentId == dropdownValue }"
        dropdownItemClass=""
        :dropdownItemValueId="itemDropdown.DepartmentId"
        :dropdownItemValueName="itemDropdown.DepartmentName"
        @itemClick="itemOnClick"
      />
    </div>
    <div v-else-if="dropdownTitle == 'Departments1'" class="dropdown-data">
      <DropdownItem
        :class="{ 'item-selected': '' == dropdownValue }"
        dropdownItemClass=""
        dropdownItemValueId=""
        dropdownItemValueName="Tất cả phòng ban"
        @itemClick="itemOnClick"
      />
      <DropdownItem
        v-for="itemDropdown in itemDropdowns"
        :key="itemDropdown.DepartmentId"
        :class="{ 'item-selected': itemDropdown.DepartmentId == dropdownValue }"
        dropdownItemClass=""
        :dropdownItemValueId="itemDropdown.DepartmentId"
        :dropdownItemValueName="itemDropdown.DepartmentName"
        @itemClick="itemOnClick"
      />
    </div>
    <div v-else class="dropdown-data">
      <DropdownItem
        v-for="itemDropdown in itemDropdowns"
        :key="itemDropdown.itemId"
        :class="{ 'item-selected': itemDropdown.itemId == dropdownValue }"
        dropdownItemClass=""
        :dropdownItemValueId="itemDropdown.itemId"
        :dropdownItemValueName="itemDropdown.itemName"
        @itemClick="itemOnClick"
      />
    </div>
  </div>
</template>

<script>
import axios from "axios";
import DropdownItem from "./BaseDropdownItem.vue";
export default {
  name: "BaseDropdown",
  components: {
    DropdownItem,
  },
  props: {
    dropdownId: String,
    dropdownClass: String,
    dropdownValue:{
      type: String,
      default: ''
    },
    dropdownText:{
      type: String,
      default: 'Tất cả'
    },
      dropdownTitle: String,
  },
  created() {
    this.loadDropdown();
  },
  methods: {
    loadDropdown() {
      var vm = this;
      if (this.dropdownTitle == "Positions" || this.dropdownTitle == "Positions1") {
        if(this.dropdownTitle == "Positions1"){
          this.dropdownText="Tất cả vị trí";
        }
        axios
          .get("http://cukcuk.manhnv.net/v1/Positions")
          .then((res) => {
            vm.itemDropdowns = [];
            vm.itemDropdowns = res.data;
          })
          .catch((res) => {
            console.log(res);
          });
      } else if (this.dropdownTitle == "Departments" || this.dropdownTitle == "Departments1") {
        if(this.dropdownTitle == "Departments1"){
          this.dropdownText="Tất cả phòng ban";
        }
        axios
          .get("http://cukcuk.manhnv.net/api/Department")
          .then((res) => {
            vm.itemDropdowns = [];
            vm.itemDropdowns = res.data;
          })
          .catch((res) => {
            console.log(res);
          });
      }else if(this.dropdownTitle == "Gender"){
        vm.itemDropdowns = [
          { itemId : '0', itemName : "Nữ" },
          { itemId : '1', itemName : "Nam" },
          { itemId : '2', itemName : "Không xác định" }
        ];
      }else if(this.dropdownTitle == "WorkStatus"){
        vm.itemDropdowns = [
          { itemId : '0', itemName : "Đã nghỉ việc" },
          { itemId : '1', itemName : "Đang làm việc" },
          { itemId : '2', itemName : "Đang thử việc" }
        ];
      }
    },
    itemOnClick(item){
      this.$emit('dropdownOnSelect', item);
    }
  },
  data() {
    return {
      itemDropdowns: [],
    };
  },
  watch:{
    dropdownValue(){
      this.$emit('input',this.dropdownValue);
    }
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
  z-index: 999;
}

.dropdown .dropdown-main:focus ~ .dropdown-data {
  /* height: auto;
    max-height: 210px; */
  height: 140px;
  transition: 1s;
  background-color: white;
  z-index: 999;
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

.dropdown-restaurant {
  width: 190px;
}

.dropdown-restaurant .dropdown-main {
  border: none;
}

.dropdown-restaurant .dropdown-main p {
  font-weight: bold;
  font-size: 15px;
}
</style>