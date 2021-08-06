<template>
    <button v-bind:class="['dropdown-item', dropdownItemClass]"
            v-bind:valueid="dropdownItemValueId"
            v-bind:valuename="dropdownItemValueName"
            @click="
      $emit('itemClick', {
        id: dropdownItemValueId,
        name: dropdownItemValueName,
      })
    ">
        <i class="fas fa-check"></i>
        <p>{{ dropdownItemValueName }}</p>
    </button>
</template>

<script>
    export default {
        name: "BaseDropdownItem",
        props: {
            // để truyền vào class cho itemdropdown (selected hay không)
            dropdownItemClass: String,
            // để truyền vào id(match với value của dropdown) cho item
            dropdownItemValueId: String,
            // để truyền vào name(text) cho item
            dropdownItemValueName: String,
        },
    };
</script>

<style>
    .dropdown .dropdown-data .dropdown-item {
        width: 100%;
        height: 40px;
        background-color: #ffffff;
        display: flex;
        align-items: center;
        cursor: pointer;
        outline: none;
        border: none;
    }

    .dropdown-item p {
        font-family: GoogleSans-Regular;
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