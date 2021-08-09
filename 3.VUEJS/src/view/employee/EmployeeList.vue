<template>
    <div class="content-body">
        <div class="content-title">
            <b>Danh sách nhân viên</b>
            <span>
                <Button v-if="isShowDeleteBtn"
                        buttonId="btnDelete"
                        buttonClass="button button-icon"
                        iClass="fa-trash-alt"
                        buttonText="Xóa nhân viên"
                        @btn-click="btnDeleteOnClick" />
                <Button buttonId="BtnAdd"
                        buttonClass="button button-icon"
                        iClass="fa-user-plus"
                        buttonText="Thêm nhân viên"
                        @btn-click="btnAddOnClick" />
            </span>
        </div>
        <div class="content-toolbar">
            <div class="toolbar-left">
                <Input inputClass="input-search input-icon"
                       inputType="text"
                       inputPlacehoder="Tìm kiếm theo mã, tên hoặc số điện thoại" />
                <Dropdown dropdownId="inputPosition1Name"
                          dropdownClass=""
                          :dropdownValue="dropdownPositionObj.id"
                          :dropdownText="dropdownPositionObj.name"
                          dropdownTitle="Positions1"
                          @dropdownOnSelect="dropdownPositionOnSelect" />
                <Dropdown dropdownId="inputDepartment1Name"
                          dropdownClass=""
                          :dropdownValue="dropdownDepartmentObj.id"
                          :dropdownText="dropdownDepartmentObj.name"
                          dropdownTitle="Departments1"
                          @dropdownOnSelect="dropdownDepartmentOnSelect" />
            </div>
            <div class="toolbar-right">
                <Button buttonId="btnRefesh"
                        buttonClass="button button-refresh"
                        iClass="fa-sync-alt"
                        buttonText=""
                        @btn-click="reloadData" />
            </div>
        </div>
        <Grid @tableRowOnDbClick="tableRowOnDbClick1"
              :isLoadAgain="loadAgain"
              @showDeleteBtn="showDeleteBtn"
              @hideDeleteBtn="hideDeleteBtn" />
        <ContentPaging />
    </div>
</template>

<script>
    import Button from "../../components/base/BaseButton.vue";
    import Input from "../../components/base/BaseInput.vue";
    import Dropdown from "../../components/base/BaseDropdown.vue";
    import Grid from "../../components/layout/TheGrid.vue";
    import ContentPaging from "../../components/layout/TheContentPaging.vue";

    export default {
        name: "TheEmployeeList",
        components: {
            Button,
            Input,
            Dropdown,
            Grid,
            ContentPaging,
        },
        data() {
            return {
                // để nhận và truyền giá trị cho grid (load lại dữ liệu hay không)
                loadAgain: false,
                // để lưu id và name từ item dropdown selected mà dropdown truyền lên => để gán cho value và text cho dropdown tương ứng
                dropdownPositionObj: { name: "Tất cả vị trí"},
                // để lưu id và name từ item dropdown selected mà dropdown truyền lên => để gán cho value và text cho dropdown tương ứng
                dropdownDepartmentObj: {name: "Tất cả phòng ban"},
                // nhân giá trị true/false truyền lên từ grid => v-if để ẩn hiện btn delete
                isShowDeleteBtn: false,
            };
        },
        methods: {
            btnAddOnClick() {
                this.$emit("btnAddOnClick");
            },
            tableRowOnDbClick1(employeeId) {
                this.$emit("chooseAnEmployee", employeeId);
            },
            reloadData() {
                this.loadAgain = !this.loadAgain;
            },
            btnDeleteOnClick() {
                this.$emit("btnDeleteOnClick");
            },
            dropdownPositionOnSelect(obj) {
                this.dropdownPositionObj = obj;
                // lọc dữ liệu theo vị trí nhân viên
                
            },
            dropdownDepartmentOnSelect(obj) {
                this.dropdownDepartmentObj = obj;
            },
            showDeleteBtn() {
                this.isShowDeleteBtn = true;
            },
            hideDeleteBtn() {
                this.isShowDeleteBtn = false;
            }
        },
    };
</script>

<style scoped>
    .content .content-body {
        width: 100%;
        height: calc(100% - 49px);
        float: left;
        padding: 24px;
        box-sizing: border-box;
        background-color: #E9EBEE;
    }

        .content .content-body .content-title {
            width: 100%;
            height: 40px;
            float: left;
            display: flex;
            justify-content: space-between;
            margin-bottom: 10px;
        }

            .content .content-body .content-title > b {
                line-height: 40px;
                font-size: 20px;
            }

            .content .content-body .content-title > span {
                width: auto;
                display: flex;
                justify-content: flex-end;
            }

                .content .content-body .content-title > span #btnDelete {
                    margin-right: 16px;
                    visibility: visible;
                }

        .content .content-body .content-toolbar {
            width: 100%;
            height: 40px;
            float: left;
            display: flex;
            justify-content: space-between;
            margin-bottom: 10px;
        }

            .content .content-body .content-toolbar .toolbar-left {
                height: 100%;
                width: 850px;
                display: flex;
            }

                /*.content .content-body .content-toolbar .toolbar-left .combobox{
                    margin-left:16px;
                }*/

                .content .content-body .content-toolbar .toolbar-left .dropdown {
                    margin-left: 16px;
                }

            .content .content-body .content-toolbar .toolbar-right {
                height: 100%;
                width: 250px;
                display: flex;
                justify-content: flex-end;
            }
</style>