<template>
    <modal :show="show" v-if=" (formName=='StuffingLocation' && isValid('CanAddStuffingLocation'))
                               ||(formName=='PortOfLoading' && isValid('CanAddPartOfLoading')) ||(formName=='PortOfDestination' && isValid('CanAddPartOfDestination')) 
                               ||(formName=='OrderType' && isValid('CanAddOrderType')) ||(formName=='Service' && isValid('CanAddService')) 
                               ||(formName=='Incoterms' && isValid('CanAddIncoterms')) ||(formName=='Commodity' && isValid('CanAddCommodity')) 
                               ||(formName=='Carrier' && isValid('CanAddCarrier')) ||(formName=='ExportEXW' && isValid('CanAddExportExw')) 
                               ||(formName=='ImportFOB' && isValid('CanAddImportFob')) ||(formName=='QuantityContainer' && isValid('CanAddQuantityContainer'))
           || (formName=='StuffingLocation' && isValid('CanEditStuffingLocation'))
                               ||(formName=='PortOfLoading' && isValid('CanEditPartOfLoading')) ||(formName=='PortOfDestination' && isValid('CanEditPartOfDestination')) 
                               ||(formName=='OrderType' && isValid('CanEditOrderType')) ||(formName=='Service' && isValid('CanEditService')) 
                               ||(formName=='Incoterms' && isValid('CanEditIncoterms')) ||(formName=='Commodity' && isValid('CanEditCommodity')) 
                               ||(formName=='Carrier' && isValid('CanEditCarrier')) ||(formName=='ExportEXW' && isValid('CanEditExportExw')) 
                               ||(formName=='ImportFOB' && isValid('CanEditImportFob')) ||(formName=='QuantityContainer' && isValid('CanEditQuantityContainer')) ">

        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header" v-if="type=='Edit'">

                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='StuffingLocation'">{{ $t('importExport.UpdateStuffingLocation') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='PortOfLoading'">{{ $t('importExport.UpdatePortOfLoading') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='PortOfDestination'">{{ $t('importExport.UpdatePortOfDestination') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='OrderType'">{{ $t('importExport.UpdateOrderType') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='Service'">{{ $t('importExport.UpdateService') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='Incoterms'">{{ $t('importExport.UpdateIncoterms') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='Commodity'">{{ $t('importExport.UpdateCommodity') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='Carrier'">{{ $t('importExport.UpdateCarrier') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='ExportEXW'">{{ $t('importExport.UpdateExportEXW') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='ImportFOB'">{{ $t('importExport.UpdateImportFOB') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='QuantityContainer'">{{ $t('importExport.UpdateQuantityContainer') }} </h5>

                        </div>
                        <div class="modal-header" v-else>

                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='StuffingLocation'">{{ $t('importExport.AddStuffingLocation') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='PortOfLoading'">{{ $t('importExport.AddPortOfLoading') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='PortOfDestination'">{{ $t('importExport.AddPortOfDestination') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='OrderType'">{{ $t('importExport.AddOrderType') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='Service'">{{ $t('importExport.AddService') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='Incoterms'">{{ $t('importExport.AddIncoterms') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='Commodity'">{{ $t('importExport.AddCommodity') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='Carrier'">{{ $t('importExport.AddCarrier') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='ExportEXW'">{{ $t('importExport.AddExportEXW') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='ImportFOB'">{{ $t('importExport.AddImportFOB') }} </h5>
                            <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='QuantityContainer'">{{ $t('importExport.AddQuantityContainer') }} </h5>

                        </div>
                        <div>
                            <div class="card-body ">
                                <div class="row ">
                                    
                                    <div v-if="english=='true'" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.importExport.name.$error} && ($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('importExport.ImportExportName'))  }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.importExport.name.$model" type="text" />
                                        <span v-if="v$.importExport.name.$error" class="error">
                                            <span v-if="!v$.importExport.name.required">{{ $t('importExport.NameRequired') }}</span>
                                            <span v-if="!v$.importExport.name.maxLength">{{ $t('importExport.NameLength') }}</span>
                                        </span>
                                    </div>
                                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.importExport.nameArabic.$error}">
                                        <label class="text  font-weight-bolder"> {{$arabicLanguage($t('importExport.ImportExportNameAr'))  }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control "  v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"  v-model="v$.importExport.nameArabic.$model" type="text" />
                                        <span v-if="v$.importExport.nameArabic.$error" class="error">
                                            <span v-if="!v$.importExport.nameArabic.required"> {{ $t('importExport.NameRequired') }}</span>
                                            <span v-if="!v$.importExport.nameArabic.maxLength">{{ $t('importExport.NameLength') }}</span>
                                        </span>
                                    </div>


                                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.importExport.description.$error} && ($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        <label class="text  font-weight-bolder"> {{ $t('importExport.Description') }}: </label>
                                        <textarea class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.importExport.description.$model" type="text" />
                                        <span v-if="v$.importExport.description.$error" class="error">{{ $t('importExport.descriptionLength') }}</span>
                                    </div>
                                    <div class="form-group col-md-12" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        <label style="margin: 7px;">{{ $t('importExport.Active') }}</label> <br />
                                        <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': importExport.isActive, 'bootstrap-switch-off': !importExport.isActive}" v-on:click="importExport.isActive = !importExport.isActive" style="width: 72px;">
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
                            <div class="modal-footer justify-content-right" v-if="type=='Edit' && ((formName=='StuffingLocation' && isValid('CanEditStuffingLocation'))
                               ||(formName=='PortOfLoading' && isValid('CanEditPartOfLoading')) ||(formName=='PortOfDestination' && isValid('CanEditPartOfDestination')) 
                               ||(formName=='OrderType' && isValid('CanEditOrderType')) ||(formName=='Service' && isValid('CanEditService')) 
                               ||(formName=='Incoterms' && isValid('CanEditIncoterms')) ||(formName=='Commodity' && isValid('CanEditCommodity')) 
                               ||(formName=='Carrier' && isValid('CanEditCarrier')) ||(formName=='ExportEXW' && isValid('CanEditExportExw')) 
                               ||(formName=='ImportFOB' && isValid('CanEditImportFob')) ||(formName=='QuantityContainer' && isValid('CanEditQuantityContainer')))">
                                <button type="button" class="btn btn-primary  " v-on:click="SaveImportExport" v-bind:disabled="v$.importExport.$invalid"> {{ $t('importExport.btnUpdate') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('importExport.btnClear') }}</button>
                            </div>
                            <div class="modal-footer justify-content-right" v-if="type!='Edit' && ((formName=='StuffingLocation' && isValid('CanAddStuffingLocation'))
                               ||(formName=='PortOfLoading' && isValid('CanAddPartOfLoading')) ||(formName=='PortOfDestination' && isValid('CanAddPartOfDestination')) 
                               ||(formName=='OrderType' && isValid('CanAddOrderType')) ||(formName=='Service' && isValid('CanAddService')) 
                               ||(formName=='Incoterms' && isValid('CanAddIncoterms')) ||(formName=='Commodity' && isValid('CanAddCommodity')) 
                               ||(formName=='Carrier' && isValid('CanAddCarrier')) ||(formName=='ExportEXW' && isValid('CanAddExportExw')) 
                               ||(formName=='ImportFOB' && isValid('CanAddImportFob')) ||(formName=='QuantityContainer' && isValid('CanAddQuantityContainer')))">
                                <button type="button" class="btn btn-primary  " v-on:click="SaveImportExport" v-bind:disabled="v$.importExport.$invalid"> {{ $t('importExport.btnSave') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('importExport.btnClear') }}</button>
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
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    import {  maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        props: ['show', 'newImportExport', 'type','formName'],
        mixins: [clickMixin],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                importExport: this.newImportExport,
                arabic: '',
                english: '',
                render: 0,
                loading: false,
            }
        },
        validations() {
            return {
                importExport: {
                    name: {
                        maxLength: maxLength(50)
                    },
                    nameArabic: {
                        // required: requiredIf((x) => {
                        //     if (x.name == '' || x.name == null)
                        //         return true;
                        //     return false;
                        // }),
                        required: requiredIf(function () {
                            return !this.importExport.name;
                        }),
                        maxLength: maxLength(50)
                    },
                    code: {
                        maxLength: maxLength(30)
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
                this.$https.get('/ImportExport/ImportExportCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.importExport.code = response.data;
                        root.render++;
                    }
                });
            },
            SaveImportExport: function () {
                
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/ImportExport/SaveImportExportTypeInformation', this.importExport, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {

                            if (root.type != "Edit") {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
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
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
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
                    .finally(() => root.loading = false)
            }
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            //if (this.importExport.id == '00000000-0000-0000-0000-000000000000' || this.importExport.id == undefined || this.importExport.id == '')
            //    this.GetAutoCodeGenerator();

        }
    }
</script>
