<template>
    <div id="matchingTransactionTable" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <modal :show="show">
            <div class="modal-header">
                <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('AssignRoleToUsers.Users') }}</h5>
                <small>{{ $t('AssignRoleToUsers.For') }} {{roleDetails.name}}</small>
            </div>
            <div class="modal-body">
                <div>
                    <table class="table table-striped table-hover">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>{{ $t('AssignRoleToUsers.UserName') }}</th>
                                <!-- <th></th> -->
                            </tr>

                        </thead>
                        <tbody>
                            <tr v-for="(user,index) in userRolesList" :key="index.id">
                                <td>{{index +1}}</td>
                                <td>
                                    {{user.userName}}
                                </td>
                                 <td>
                                    <button class="btn btn-danger  btn-icon float-right btn-sm" @click="removeUser(user.id)">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </td> 
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="row">
                    <div class="col-md-7 mt-2">
                        <usersDropdown v-bind:key="rander" v-model="v$.roleDetails.userId.$model"></usersDropdown>
                    </div>
                    <div class="col-md-3 mr-2">
                        <button title="Add New Item" class="btn btn-outline-primary "
                                :disabled="v$.$invalid" v-on:click="SavePermissionData()">
                            <i class="fa fa-plus"> </i> {{ $t('AssignRoleToUsers.AddNew') }}
                        </button>
                    </div>
                </div>
            </div>
            <div class="modal-footer justify-content-center">
                <button type="button" class="btn btn-danger " v-on:click="close()">{{ $t('AssignRoleToUsers.Close') }}</button>
            </div>
        </modal>
    </div>

</template>
<script>
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        props: ['roleDetails', 'show'],
        mixins: [clickMixin],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {    
                userRolesList: [],
                rander:0,
            }
        },
        validations() {
            return {
                roleDetails: {
                    userId: {
                        required
                    }
                }
                }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
            GetPermissionData: function () {
                var root = this;
                var roleId = this.roleDetails.roleId;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/GetNobleUserRoleList?id=' + roleId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$store.GetUserRolesList(response.data);
                        root.userRolesList = response.data;
                    }
                });
            },
            SavePermissionData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.post('/Company/SaveNobleUserRole', this.roleDetails, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {
                            root.$store.isUserRolesList.push({
                                id: response.data.permission.id,
                                name: response.data.permission.userName
                            })
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                icon:'success',
                                text: "Your Role " + response.data.permission[0].roleName + " has been created!",
                                type: 'success',
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true
                            });
                            root.close();
                        }
                        else {
                            var data = root.$store.isUserRolesList.find(function (x) {
                                return x.id == response.data.permission.id;
                            });
                            data.id = response.data.permission.id;
                            data.userId = response.data.permission.userId;
                            data.roleId = response.data.permission.roleId;
                            root.$swal({
                               title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: "Your Role " + response.data.permission[0].roleName + " has been updated!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true
                            });
                            root.close();
                        }
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
            removeUser: function (id) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/RemoveUserRole?id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                       
                        root.GetPermissionData();
                        root.rander++;
                    }
                });
            }
        },
        mounted: function () {
            this.GetPermissionData();
        }
    }
</script>