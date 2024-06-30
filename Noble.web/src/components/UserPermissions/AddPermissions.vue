<template>
    <modal :show="show">
        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header" v-if="type=='Edit'">
                            <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('AddPermissions.UpdateRoles') }}</h5>
                        </div>
                        <div class="modal-header" v-else>
                            <h5 class="modal-title DayHeading" id="myModalLabel"> {{ $t('AddPermissions.AddRoles') }}</h5>
                        </div>
                        <div>
                            <div class="card-body ">
                                <div class="row ">
                                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.newRole.name.$error}">
                                        <label class="text  font-weight-bolder"> {{ $t('AddPermissions.Name') }}: *</label>
                                        <input class="form-control" v-model="v$.newRole.name.$model" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" type="text" />
                                        <span v-if="v$.newRole.name.$error" class="error">
                                            <span v-if="!v$.newRole.name.required">{{ $t('AddPermissions.NameRequired') }}</span>
                                            <span v-if="!v$.newRole.name.maxLength">{{ $t('AddPermissions.NameLength') }}</span>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label style="margin: 7px;">{{ $t('AddPermissions.Active') }}</label> <br />
                                        <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': newRole.isActive, 'bootstrap-switch-off': !newRole.isActive}" v-on:click="newRole.isActive = !newRole.isActive" style="width: 72px;">
                                            <div class="bootstrap-switch-container" style="width: 122px; margin-left: 0px;">
                                                <span class="bootstrap-switch-handle-on bootstrap-switch-success" style="width: 50px;">
                                                    <i class="nc-icon nc-check-2"></i>
                                                </span>
                                                <span class="bootstrap-switch-label" style="width: 30px;">&nbsp;</span>
                                                <span class="bootstrap-switch-handle-off bootstrap-switch-success" style="width: 50px;">
                                                    <i class="nc-icon nc-simple-remove"></i>
                                                </span>
                                                <input class="bootstrap-switch" type="checkbox" data-toggle="switch" checked="" data-on-label="<i class='nc-icon nc-check-2'></i>" data-off-label="<i class='nc-icon nc-simple-remove'></i>" data-on-color="success" data-off-color="success">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-right" v-if="type=='Edit'">
                            <button type="button" class="btn btn-primary  " v-on:click="SaveRoles" v-bind:disabled="v$.newRole.$invalid"> {{ $t('AddPermissions.btnUpdate') }}</button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('AddPermissions.btnClear') }}</button>
                        </div>
                        <div class="modal-footer justify-content-right" v-else>
                            <button type="button" class="btn btn-primary  " v-on:click="SaveRoles" v-bind:disabled="v$.newRole.$invalid"> {{ $t('AddPermissions.btnSave') }}</button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('AddPermissions.btnClear') }}</button>
                        </div>
                    </div>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </modal>
</template>
<script>



    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'

    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        props: ['show', 'roles', 'type'],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                loading: false,
                render: 0,
                newRole: this.roles,
            }
        },
        validations() {
            return {
                newRole: {
                    name: {
                        required,
                        maxLength: maxLength(50)
                    }
                }
            }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
            SaveRoles: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.newRole.normalizedName = this.newRole.name;
                this.$https.post('/Company/SaveNobleRoles', this.newRole, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {
                            root.$store.isRolesList.push({
                                id: response.data.roles.id,
                                name: response.data.roles.name,
                                normalizedName: response.data.roles.normalizedName
                            })
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                icon: 'success',
                                text: "Your Role " + response.data.roles.name + " has been created!",
                                type: 'success',
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true
                            });
                            root.close();
                        }
                        else {
                            var data = root.$store.isRolesList.find(function (x) {
                                return x.id == response.data.roles.id;
                            });
                            data.id = response.data.roles.id;
                            data.name = response.data.roles.name;
                            data.description = response.data.roles.description;
                            data.code = response.data.roles.code;
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: "Your Role " + response.data.roles.name + " has been updated!",
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
                            text: "Your Role Name Already Exist!",
                            type: 'error',
                            showConfirmButton: false,
                            timer: 1000,
                            timerProgressBar: true
                        });
                        root.loading = false
                    }
                });
            }
        },
        mounted: function () {
            if (this.newRole.id == '00000000-0000-0000-0000-000000000000' || this.newRole.id == undefined || this.newRole.id == '') {
                this.newRole.name = '';
                this.newRole.normalizedName = '';
                this.newRole.id = '00000000-0000-0000-0000-000000000000';
            }
        }
    }
</script>
