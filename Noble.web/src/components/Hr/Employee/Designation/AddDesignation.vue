<template >
    <modal :show="show" v-if=" isValid('Can Save Brand') || isValid('Can Edit Brand') ">

        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header" v-if="type=='Edit'">

                            <h5 class="modal-title DayHeading" id="myModalLabel"> {{ $t('Designation.UpdateDesignation') }}</h5>

                        </div>
                        <div class="modal-header" v-else>

                            <h5 class="modal-title DayHeading" id="myModalLabel"> {{ $t('Designation.CreateDesignation') }}</h5>

                        </div>
                        <div class="">
                            <div class="card-body" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <div class="row ">
                                    <div :key="render" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.designation.code.$error}">
                                        <label class="text  font-weight-bolder"> {{ $t('Designation.Code') }}:<span class="text-danger"> *</span></label>
                                        <input disabled class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.designation.code.$model" type="text" />
                                        <span v-if="v$.designation.code.$error" class="error">
                                            <span v-if="!v$.designation.code.maxLength">{{ $t('CodeLength') }}</span>
                                        </span>
                                    </div>
                                    <div v-if="english=='true'" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.designation.name.$error}">
                                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('Designation.Name'))  }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.designation.name.$model" type="text" />
                                        <span v-if="v$.designation.name.$error" class="error">
                                            <span v-if="!v$.designation.name.required"> {{ $t('NameRequired') }}</span>
                                            <span v-if="!v$.designation.name.maxLength">{{ $t('NameLength') }}</span>
                                        </span>
                                    </div>
                                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.designation.nameArabic.$error}">
                                        <label class="text  font-weight-bolder">{{$arabicLanguage($t('Designation.Name'))  }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control "  v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"  v-model="v$.designation.nameArabic.$model" type="text" />
                                        <span v-if="v$.designation.nameArabic.$error" class="error">
                                            <span v-if="!v$.designation.nameArabic.required"> {{ $t('NameRequired') }}</span>
                                            <span v-if="!v$.designation.nameArabic.maxLength">{{ $t('NameLength') }}</span>
                                        </span>
                                    </div>

                                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.designation.description.$error}">
                                        <label class="text  font-weight-bolder"> {{ $t('Designation.Description') }}: </label>
                                        <textarea class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.designation.description.$model" type="text" />
                                        <span v-if="v$.designation.description.$error" class="error">{{ $t('descriptionLength') }}</span>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label style="margin: 7px;">{{ $t('Designation.Active') }}</label> <br />
                                        <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': designation.isActive, 'bootstrap-switch-off': !designation.isActive}" v-on:click="designation.isActive = !designation.isActive" style="width: 72px;">
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
                        <div v-if="!loading">
                            <div class="modal-footer justify-content-right" v-if="type=='Edit' && isValid('Can Edit Brand')">
                                <button type="button" class="btn btn-primary  " v-on:click="SaveDesignation" v-bind:disabled="v$.designation.$invalid"> {{ $t('btnUpdate') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('btnClear') }}</button>
                            </div>
                            <div class="modal-footer justify-content-right" v-if="type!='Edit' && isValid('Can Save Brand')">
                                <button type="button" class="btn btn-primary  " v-on:click="SaveDesignation" v-bind:disabled="v$.designation.$invalid"> {{ $t('btnSave') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('btnClear') }}</button>
                            </div>
                        </div>
                        <div v-else>
                            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                        </div>                        
                    </div>
                </div>
            </div>
        </div>
    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import { maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
      import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
        mixins: [clickMixin],
        props: ['show', 'newDesignation', 'type'],
        components: {
           Loading 
        },
          setup() {
            return { v$: useVuelidate() }
        },

        data: function () {
            return {
                designation: this.newDesignation,
                arabic: '',
                english: '',
                render: 0,
                loading: false,
            }
        },
        validations() {
           return{
             designation: {
                name: {                    
                    maxLength: maxLength(50)
                },
                code: {
                    
                    maxLength: maxLength(30)
                },
                nameArabic: {
                    required: requiredIf(function () {
                            return !this.designation.name;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.name == '' || x.name == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(50)
                },
                description: {
                    maxLength: maxLength(200)
                }
            }
           }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
            GetAutoCodeGenerator: function () {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/EmployeeRegistration/DesignationCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data != null) {
                        root.designation.code = response.data;
                        root.render++;
                    }
                });
            },
            SaveDesignation: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/EmployeeRegistration/SaveDesignation', this.designation, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {
                           
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : 'تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                          
                            root.close();
                        }
                        else {
                           
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                           
                        }
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your Designation Name  Already Exist!",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            }
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.designation.id == '00000000-0000-0000-0000-000000000000' || this.designation.id == undefined || this.designation.id == '')
                this.GetAutoCodeGenerator();

        }
    }
</script>
