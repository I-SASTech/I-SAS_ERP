<template>
    <modal :show="show" v-if="isValid('CanAddUserRole') || isValid('CanEditUserRole')">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'">{{ $t('AddRoles.UpdateRoles') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddRoles.AddRoles') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div v-if="english=='true'" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.roles.name.$error} && ($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddRoles.NameEn'))  }}:<span class="text-danger"> *</span></label>
                        <input class="form-control" v-model="v$.roles.name.$model" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" type="text" />
                        <span v-if="v$.roles.name.$error" class="error">
                            <span v-if="!v$.roles.name.required">{{ $t('AddRoles.NameRequired') }}</span>
                            <span v-if="!v$.roles.name.maxLength">{{ $t('AddRoles.NameLength') }}</span>
                        </span>
                    </div>
                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.roles.nameArabic.$error} && ($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                        <label class="text  font-weight-bolder"> {{$arabicLanguage($t('AddRoles.NameAr'))  }}: <span class="text-danger"> *</span></label>
                        <input class="form-control " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.roles.nameArabic.$model" type="text" />
                        <span v-if="v$.roles.nameArabic.$error" class="error">
                            <span v-if="!v$.roles.nameArabic.required"> {{ $t('AddRoles.NameRequired') }}</span>
                            <span v-if="!v$.roles.nameArabic.maxLength">{{ $t('AddRoles.NameLength') }}</span>
                        </span>
                    </div>




                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="roles.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddCategory.Active') }} </label>
                        </div>
                    </div>

                  


                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveRoles" v-bind:disabled="v$.roles.$invalid" v-if="type!='Edit' && isValid('CanAddUserRole')">{{ $t('AddCategory.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveRoles" v-bind:disabled="v$.roles.$invalid" v-if="type=='Edit' && isValid('CanEditUserRole')">{{ $t('AddCategory.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddCategory.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    

    import { requiredIf, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        props: ['show', 'newroles', 'type'],
        setup() {
            return { v$: useVuelidate() }
        },
        components: {
            Loading
        },
        data: function () {
            return {
                arabic: '',
                loading: false,
                english: '',
                render: 0,
                roles: {}
            }
        },
        validations() {
            return {
                roles: {
                    name: {
                        maxLength: maxLength(50)
                    },
                    nameArabic: {
                        required: requiredIf((x) => {
                            if (x.name == '' || x.name == null)
                                return true;
                            return false;
                        }),
                        maxLength: maxLength(50)
                    },
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
                this.roles.normalizedName = this.roles.name;
                this.$https.post('/Company/SaveNobleRoles', this.roles, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    root.loading = false;

                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {
                            root.$store.isRolesList.push({
                                id: response.data.roles.id,
                                name: response.data.roles.name,
                                nameArabic: response.data.roles.nameArabic,
                                normalizedName: response.data.roles.normalizedName
                            })
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                icon:'success',
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
                            data.nameArabic = response.data.roles.nameArabic;
                            data.normalizedName = response.data.roles.normalizedName;
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
            }
        },
        created: function () {
            this.roles= this.newroles;
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.roles.id == '00000000-0000-0000-0000-000000000000' || this.roles.id == undefined || this.roles.id == '')
            {
                this.roles.name = '';
                this.roles.normalizedName = '';
                this.roles.id = '00000000-0000-0000-0000-000000000000';
            }
        }
    }
</script>
