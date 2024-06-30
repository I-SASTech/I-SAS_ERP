<template>
    <div id="matchingTransactionTable">
        <modal :show="show" :modalLarge="true">
            <div class="modal-header">

                <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('AssignPermissionsToRoleForAdd.Permissions') }}</h5>
                <small>{{ $t('AssignPermissionsToRoleForAdd.For') }} {{roleDetails.name}}</small>
            </div>
            <div class="modal-body">
                <div class="card-footer col-md-3" v-if="loading">
                    <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
                </div>
                <div class="row">
                    <div class="col-md-6 mt-2">
                        <moduleNamesDropdownForAdd v-model="v$.roleDetails.moduleId.$model" @update:modelValue="GetCategories(roleDetails.moduleId)"></moduleNamesDropdownForAdd>
                    </div>
                    <div class="col-md-6 mt-2">
                        <moduleCategoryDropdown v-model="roleDetails.moduleName" :id="moduleId" @update:modelValue="GetRights(roleDetails.moduleName)" v-bind:key="render"></moduleCategoryDropdown>
                    </div>
                </div>
                <div>
                    <div class="col-md-5 mt-2">
                        <!--<VueRadioButton v-model="toggleValue"
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
                        </VueRadioButton>-->
                    </div>
                </div>
                <div v-if="isShown == true" v-bind:key="DescriptionCrudRander">
                    <table class="table table-striped table-hover">
                        <thead>
                            <tr>
                                <th>
                                    <label class="form-checkbox">
                                        <input type="checkbox" v-model="selectAll" @click="select">
                                        <i class="form-icon"></i>
                                    </label>
                                </th>
                                <th>{{ $t('AssignPermissionsToRoleForAdd.Description') }}</th>
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
                <button type="button" class="btn btn-danger " v-on:click="close()">{{ $t('AssignPermissionsToRoleForAdd.Close') }}</button>

                <button title="Add New Item" class="btn btn-outline-primary "
                        v-on:click="SavePermissionData()">
                    <i class="fa fa-plus"> </i> {{ $t('AssignPermissionsToRoleForAdd.AddNew') }}
                </button>

            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </modal>
    </div>

</template>
<script>
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    /*import VueRadioButton from "vue-radio-button";*/
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
        components: {
            Loading
            /*VueRadioButton*/
        },
        props: ['roledetailsforpermissions', 'show'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                toggleValue: '',
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
                AllUserRolesList: [],
                moduleId: 0,
                moduleName: '',
                render: 0,
                isShown: false,
                allowAll: '',
                selectAll: false,
                isActive: false,
                DescriptionCrudRander: 0,
                nobleRolePermission: [],
                loading: false,
                roleDetails: {},
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
            isAllow: function (val) {
                this.isShown = false;
                this.allowAll = val.id;
            },
            close: function () {
                this.$emit('close');
            },
            GetCategories: function (id) {

                this.moduleId = id;
                this.render++;
            },
            GetRights: function (name) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (name == null) {
                    name = '';
                }
                root.$https.get('/Company/GetRightsByModuleName?moduleName=' + name + '&isRights=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data.length > 0) {
                        root.userRolesList = response.data;
                        root.isShown = true;
                        root.DescriptionCrudRander++;
                    }
                    else {
                        root.isShown = false;
                    }
                });
            },
            select: function () {
                if (!this.selectAll) {
                    for (let i in this.userRolesList) {
                        this.userRolesList[i].isActive = true;
                    }
                }
                if (this.selectAll) {
                    for (let i in this.userRolesList) {
                        this.userRolesList[i].isActive = false;
                    }
                }
            },
            SavePermissionData: function () {
                this.loading = true;
                var root = this;
                //eslint-disable-line;

                var nobleRolePermission = [];
                if (this.allowAll == true) {
                    nobleRolePermission.push({
                        roleId: this.roleDetails.roleId,
                        permissionId: '00000000-0000-0000-0000-000000000000',
                        isActive: true,
                        allowAll: this.allowAll,
                        companyId: this.roleDetails.companyId,
                        isNobel: true
                    });
                }
                 if (this.allowAll == false) {
                    nobleRolePermission.push({
                        roleId: this.roleDetails.roleId,
                        permissionId: '00000000-0000-0000-0000-000000000000',
                        isActive: false,
                        allowAll: this.allowAll,
                        isNobel: true,
                        companyId: this.roleDetails.companyId,
                    });
                }
                if (this.allowAll == '') {
                    this.userRolesList.forEach(function (x) {
                        if (x.isActive == true && x.permissionName!=null) {


                            nobleRolePermission.push({
                                roleId: root.roleDetails.roleId,
                                nobleModuleId: root.roleDetails.moduleId,
                                category: root.roleDetails.moduleName,
                                Description: x.permissionName,
                                isActive: x.isActive,
                                isNobel: true,

                                companyId: root.roleDetails.companyId
                            });
                        }
                    })
                    
                }
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                root.loading = true;
                root.$https.post('/Company/SaveNobleRolePermissions', nobleRolePermission, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.message.isAddUpdate == "Data Updated Successfully") {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                            icon: 'success',
                            text: "Your Rights has been Saved!",
                            type: 'success',
                            showConfirmButton: true,
                            timer: 1000,
                            timerProgressBar: true
                        });
                        root.loading = false;
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
                        root.loading = false;
                    }
                }).catch(error => {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: error,
                        type: 'error',
                        showConfirmButton: false,
                        timer: 1000,
                        timerProgressBar: true
                    });
                    root.loading = false;
                });
            },
            GetDataByCompanyId: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/NobleRolesDetail?companyId=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.roleDetails.roleId = response.data.id;
                        root.roleDetails.name = response.data.name;
                     
                    }
                    else {
                        root.isShown = false;
                    }
                });
            }
        },
        created: function () {
            this.roleDetails = this.roledetailsforpermissions;
        },
        mounted: function () {
            if (this.roleDetails.companyId != '' || this.roleDetails.companyId != '00000000-0000-0000-0000-000000000000') {
                this.GetDataByCompanyId(this.roleDetails.companyId);
            }
        }
    }
</script>