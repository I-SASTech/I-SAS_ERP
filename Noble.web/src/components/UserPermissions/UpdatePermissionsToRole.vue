<template>
    <div id="matchingTransactionTable">
        <modal :show="show" :modalLarge="true">
            <div class="modal-header">
                <h5 class="modal-title" id="myModalLabel">{{ $t('UpdatePermissionsToRole.Permissions') }}</h5>
                <small>{{ $t('UpdatePermissionsToRole.For') }} {{roleDetails.name}}</small>
            </div>
            <div class="modal-body">
                <div>
                </div>
                <div class="row">
                    <div class="col-md-7 mt-2">
                        <permissionCategoryDropdown v-model="selectedCategory"  v-bind:key="rander" v-bind:roleDetails="roleDetails"  @update:modelValue="GetCategoryPermissionQuery"></permissionCategoryDropdown>

                        <!--<moduleCategoryDropdown v-model="roleDetails.moduleName" :id="moduleId" @update:modelValue="GetRights(roleDetails.moduleName)" v-bind:key="render"></moduleCategoryDropdown>-->

                      <!--<moduleNamesDropdown v-model="v$.roleDetails.moduleId.$model" @update:modelValue="GetRights(roleDetails.moduleName)"></moduleNamesDropdown>-->
                    </div>
                    <!--<div class="col-md-5 mt-2">
                        <VueRadioButton v-model="toggleValue"
                                        v-on:click="isAllow(toggleValue)"
                                        :options="buttons"
                                        color="#f8a1155e"
                                        width="100"
                                        height="30">
                            <template #default="{ props }">
                                <div class="vue-radio-button">
                                    <img width="15" height="10" :src="props.icon" class="icon" />
                                    <div class="title">{{ props.title }}</div>
                                </div>
                            </template>
                        </VueRadioButton>
                    </div>-->
                </div>
                <div v-if="isShown == true">
                    <table class="table table-striped table-hover">
                        <thead>
                            <tr>
                                <th>
                                    <label class="form-checkbox">
                                        <input type="checkbox" v-model="selectAll" @click="select">
                                        <i class="form-icon"></i>
                                    </label>
                                </th>
                                <th>{{ $t('UpdatePermissionsToRole.Description') }}</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(user,index) in userRolesList" :key="index.permissionId">
                                <td v-if="user.isActive">
                                    <label class="form-checkbox">
                                        <input type="checkbox" checked v-model="user.isActive"> 
                                    </label>
                                </td>
                                <td v-if="!user.isActive">
                                    <label class="form-checkbox">
                                        <input type="checkbox" unchecked v-model="user.isActive"> 
                                    </label>
                                </td>
                                <td>{{user.permissionName}}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="modal-footer justify-content-center">
                <button type="button" class="btn btn-danger " v-on:click="close()">{{ $t('UpdatePermissionsToRole.Close') }}</button>
                <button title="Add New Item" class="btn btn-outline-primary "
                        v-on:click="SavePermissionData()">
                    <i class="fa fa-plus"> </i> {{ $t('UpdatePermissionsToRole.UpdateNew') }}
                </button>
            </div>
        </modal>
    </div>

</template>
<script>
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    //import VueRadioButton from "vue-radio-button";
    export default {
        props: ['roleDetails', 'show'],
        //components: { VueRadioButton },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                toggleValue:'',
                buttons: [
                    {
                        id: true,
                        icon: "",
                        title: "Allow All",
                    },
                    {
                        id: false,
                        icon: "",
                        title: "Remove All",
                    },
                ],
                userRolesList: [],
                isShown: false,
                allowAll: '',
                selectAll: false,
                rander: 0,
                isActive: false,
                selectedCategory: '',
            }
        },
        validations() {
            return {
                roleDetails: {
                    moduleId: {
                        required
                    }
                }
                }
        },
        methods: {
            
            isAllow: function (val){
                //eslint-disable-line;
                this.isShown = false;
                this.allowAll = val.id;
            },
            close: function () {
                this.$emit('close');
            },
            GetCategoryPermissionQuery: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
               
                root.$https.get('/Company/GetByCategoryPermissionQuery?categoryName=' + this.selectedCategory + '&isRights=true' + '&roleId=' + this.roleDetails.roleId + '&companyId=' + this.roleDetails.companyId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.length > 0) {
                        root.userRolesList = response.data;
                        root.isShown = true;

                    }
                    else {
                        root.isShown = false;
                    }
                });


            },
            GetRights: function (id){
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if(id == null){
                    id = '';
                }
                //eslint-dsiable-line
                root.$https.get('/Company/GetRightsByModuleName?moduleName=' + id + '&isRights=true' + '&roleId=' + this.roleDetails.roleId + '&companyId=' + this.roleDetails.companyId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.length > 0) {
                        root.userRolesList = response.data;
                        root.isShown = true;
                       
                    }
                    else {
                        root.isShown = false;
                    }
                });
            },
            select: function () {
                if (!this.selectAll) {
                    for (let i in this.userRolesList) {
                        // this.selected.push(this.userRolesList[i].permissionId, this.isActive);
                        this.userRolesList[i].isActive = true;
                    }
                }
                if (this.selectAll) {
                    for (let i in this.userRolesList) {
                        // this.selected.push(this.userRolesList[i].permissionId, this.isActive);
                        this.userRolesList[i].isActive = false;
                    }
                }
            },
            SavePermissionData: function () {
                var root = this;
                var nobleRolePermission = [];
                if (this.allowAll == '') {

                    this.userRolesList.forEach(function (x) {
                        nobleRolePermission.push({
                            roleId: root.roleDetails.roleId,
                            permissionId: x.permissionId,
                            isActive: x.isActive,
                            allowAll: x.allowAll,
                            isNobel: false,
                            isEdit: true,
                            companyId: root.roleDetails.companyId
                        });

                    })
                        
                }
                if(this.allowAll == true){
                    nobleRolePermission.push({
                        roleId: this.roleDetails.roleId,
                        permissionId: '00000000-0000-0000-0000-000000000000',
                        isActive: true,
                        allowAll: this.allowAll,
                        isNobel: false,
                        isEdit: true,
                        companyId: this.roleDetails.companyId
                    });
                }
                //if(this.allowAll == false){
                //    nobleRolePermission.push({
                //        roleId: this.roleDetails.roleId,
                //        permissionId: 1,
                //        isActive: false,
                //        allowAll: this.allowAll,
                //        isNobel: false,
                //        isEdit: true,
                //        companyId: this.roleDetails.companyId
                //    });
                //}
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                //eslint-disable-line
                root.$https.post('/Company/UpdateNobleRolePermissions', nobleRolePermission, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.message.isAddUpdate == "Data Updated Successfully") {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                            icon:'success',
                            text: "Your Rights has been Saved!",
                            type: 'success',
                            showConfirmButton: false,
                            timer: 1000,
                            timerProgressBar: true
                        });
                        root.close();
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your Role Against this User is Already Exist!",
                            type: 'error',
                            showConfirmButton: false,
                            timer: 1000,
                            timerProgressBar: true
                        });
                    }
                });
            },
            GetDataByCompanyId: function (id){
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/NobleRolesDetail?companyId=' + id , { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data != null) {
                        //eslint-disable-line
                        root.roleDetails.roleId = response.data.id;
                        root.roleDetails.name = response.data.name;
                        root.rander++;
                    }
                    else {
                        root.isShown = false;
                    }
                });
            }
        },
        mounted: function () {
            if(this.roleDetails.companyId != '' || this.roleDetails.companyId != '00000000-0000-0000-0000-000000000000'){
                this.GetDataByCompanyId(this.roleDetails.companyId);
               
            }
        }
    }
</script>