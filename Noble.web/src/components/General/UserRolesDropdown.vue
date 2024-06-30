<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" :placeholder="$t('UserRolesDropdown.SelectRoles')" track-by="name" :clear-on-select="false" 
                  
                     :show-labels="false" label="name" :preselect-first="true">
            <template v-slot:noResult>
                <p  class="text-danger">{{ $t('UserRolesDropdown.NoRoleFound') }} </p>
                <span  class="btn btn-primary " v-on:click="AddRole('Add')" v-if="isValid('CanAddUserRole')">{{ $t('UserRolesDropdown.CreateRole') }}</span><br />

            </template>
        </multiselect>
        <modal :show="show" v-if="show">
            <div style="margin-bottom:0px" class="card" >
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="tab-content" id="nav-tabContent">
                            <div class="modal-header" v-if="type=='Edit'">
                                <h5 class="modal-title" id="myModalLabel">{{ $t('UserRolesDropdown.UpdateRoles') }}</h5>
                            </div>
                            <div class="modal-header" v-else>
                                <h5 class="modal-title" id="myModalLabel"> {{ $t('UserRolesDropdown.AddRoles') }}</h5>
                            </div>
                            <div class="text-left">
                                <div class="card-body">
                                    <div class="row ">
                                        <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.roles.name.$error}">
                                            <label class="text  font-weight-bolder"> {{ $t('UserRolesDropdown.Name') }}: *</label>
                                            <input class="form-control" v-model="v$.roles.name.$model" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" type="text" />
                                            <span v-if="v$.roles.name.$error" class="error">
                                                <span v-if="!v$.roles.name.required">{{ $t('UserRolesDropdown.NameRequired') }}</span>
                                                <span v-if="!v$.roles.name.maxLength">{{ $t('UserRolesDropdown.NameLength') }}</span>
                                            </span>
                                        </div>
                                        <div class="form-group col-md-12">
                                            <label style="margin: 7px;">{{ $t('UserRolesDropdown.Active') }}</label> <br />
                                            <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': roles.isActive, 'bootstrap-switch-off': !roles.isActive}" v-on:click="roles.isActive = !roles.isActive" style="width: 72px;">
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
                                <button type="button" class="btn btn-primary  " v-on:click="SaveRoles" v-bind:disabled="v$.roles.$invalid"> {{ $t('UserRolesDropdown.btnUpdate') }}</button>
                                <button type="button" class="btn btn-secondary  mr-3 " v-on:click="close()">{{ $t('UserRolesDropdown.btnClear') }}</button>
                            </div>
                            <div class="modal-footer justify-content-right" v-else>
                                <button type="button" class="btn btn-primary  " v-on:click="SaveRoles" v-bind:disabled="v$.roles.$invalid"> {{ $t('UserRolesDropdown.btnSave') }}</button>
                                <button type="button" class="btn btn-secondary  mr-3 " v-on:click="close()">{{ $t('UserRolesDropdown.btnClear') }}</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </modal>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        name: 'userRolesdropdown',
        props: ["modelValue"],
        components: {
            Multiselect
        },
        mixins: [clickMixin],
          setup() {
            return { v$: useVuelidate() }
        },

        data: function () {
            return {
                options: [],
                value: '',
                show: false,
                type: '',
                roles: {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    normalizedName: '',
                    isActive: true
                },
                newRoles:{},
                render: 0
            }
        },
        validations() {
           return{
             roles: {
                name: {
                    required,
                    maxLength: maxLength(50)
                }
            }
           }
        },
        methods: {
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/NobleRolesList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        response.data.nobleRoleModel.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                name: result.name
                            })
                       })
                    }
                });
            },
            AddRole: function (type) {
                this.v$.$reset();
                this.newRoles = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    description: '',
                    isActive: true
                }
                this.show = !this.show;
                this.type = type;
            },
            close: function () {
                this.show = false;
            },
            SaveRoles: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.roles.normalizedName = this.roles.name;
                this.$https.post('/Company/SaveNobleRoles', this.roles, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {
                            root.$store.isRolesList.push({
                                id: response.data.roles.id,
                                name: response.data.roles.name,
                                normalizedName: response.data.roles.normalizedName
                            });
                            root.options.push({
                                id: response.data.roles.id,
                                name: response.data.roles.name,
                                normalizedName: response.data.roles.normalizedName
                            });
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                icon:'success',
                                text: "Your Role " + response.data.roles.name + " has been created!",
                                type: 'success',
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true
                            });
                            root.roles = {
                                id: '00000000-0000-0000-0000-000000000000',
                                name: '',
                                normalizedName: '',
                                isActive: true
                            };
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
                    }
                });
            },
        },
        computed: {
            DisplayValue: {
                get: function () {
                    if (this.value != "" || this.value != undefined) {
                        return this.value;
                    }
                    return this.modelValue;
                },
                set: function (value) {
                    this.value = value;
                    this.$emit('update:modelValue', value.id);
                }
            }
        },
        mounted: function () {
            this.getData();
        },
    }
</script>