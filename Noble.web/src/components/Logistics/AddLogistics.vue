<template>
    <div class="row"  v-if="((isValid('CanAddTransporter') || isValid('CanEditTransporter')) && formName=='Transporter') ||  ((isValid('CanAddClearanceAgent') || isValid('CanEditClearanceAgent')) && formName=='ClearanceAgent') ||  ((isValid('CanAddShippingLiner') || isValid('CanEditShippingLiner')) && formName=='ShippingLinear')">
        <div class="col-lg-12">
            <div class="row">

                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="logistics.id != '00000000-0000-0000-0000-000000000000'  && formName=='Transporter'" class="page-title">{{ $t('AddLogistics.UpdateTransporter')}} -  {{ logistics.code }}</h4>
                                <h4 v-if="logistics.id != '00000000-0000-0000-0000-000000000000' && formName=='ClearanceAgent'" class="page-title">{{ $t('AddLogistics.UpdateClearanceAgent')}} -  {{ logistics.code }}</h4>
                                <h4 v-if="logistics.id != '00000000-0000-0000-0000-000000000000' && formName=='ShippingLinear'" class="page-title">{{ $t('AddLogistics.UpdateShippingLinear')}} -  {{ logistics.code }}</h4>
                                <h4 v-if="logistics.id == '00000000-0000-0000-0000-000000000000' && formName=='Transporter'" class="page-title">{{ $t('AddLogistics.AddTransporter/Cargo')}} -  {{ logistics.code }}</h4>
                                <h4 v-if="logistics.id == '00000000-0000-0000-0000-000000000000'  && formName=='ClearanceAgent'" class="page-title">{{ $t('AddLogistics.AddClearanceAgent')}} -  {{ logistics.code }}</h4>
                                <h4 v-if="logistics.id == '00000000-0000-0000-0000-000000000000'  && formName=='ShippingLinear'" class="page-title">{{ $t('AddLogistics.AddShippingLinear')}} -  {{ logistics.code }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Logistics.Home') }}</a></li>
                                    <li class="breadcrumb-item active" v-if=" formName=='Transporter'">{{ $t('Logistics.Transporter/Cargo') }}</li>
                                    <li class="breadcrumb-item active" v-if="formName=='ClearanceAgent'">{{ $t('Logistics.ClearanceAgent') }}</li>
                                    <li class="breadcrumb-item active" v-if="formName=='ShippingLinear'">{{ $t('Logistics.ShippingLinear') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">

                            <div class="row">

                                <div v-if="english=='true'" class="col-sm-4 form-group " v-bind:class="{'has-danger' : v$.logistics.englishName.$error}">
                                    <label class="text  font-weight-bolder " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">{{$englishLanguage($t('AddLogistics.Name'))  }} :<span class="text-danger"> *</span></label>
                                    <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.logistics.englishName.$model" type="text" />
                                    <span v-if="v$.logistics.englishName.$error" class="error text-danger">
                                        <span v-if="!v$.logistics.englishName.required">{{ $t('AddLogistics.NameRequired') }}</span>
                                        <span v-if="!v$.logistics.englishName.maxLength">{{ $t('AddLogistics.NameLength') }}</span>
                                    </span>
                                </div>

                                <div v-if="isOtherLang()" class="col-sm-4 form-group " v-bind:class="{'has-danger' : v$.logistics.arabicName.$error}">
                                    <label class="text  font-weight-bolder ">{{$arabicLanguage($t('AddLogistics.Name'))  }} :<span class="text-danger"> *</span></label>
                                    <input class="form-control    " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.logistics.arabicName.$model" type="text" />
                                    <span v-if="v$.logistics.arabicName.$error" class="error text-danger">
                                        <span v-if="!v$.logistics.arabicName.required">{{ $t('AddLogistics.Name') }}</span>
                                        <span v-if="!v$.logistics.arabicName.maxLength">{{ $t('AddLogistics.NameLength') }}</span>
                                    </span>
                                </div>


                                <div class="col-sm-4 form-group ">
                                    <label class="text  font-weight-bolder ">{{ $t('AddLogistics.ContactName')}} :</label>
                                    <input class="form-control  " v-model="logistics.contactName" type="text" />

                                </div>
                                <div class="col-sm-4 form-group ">
                                    <label class="text  font-weight-bolder ">{{ $t('AddLogistics.ContactNumber')}} :</label>
                                    <input class="form-control  " v-model="logistics.contactNo" type="text" />

                                </div>
                                <div class="col-sm-4 form-group ">
                                    <label class="text  font-weight-bolder ">{{ $t('AddLogistics.RegisterUser_EmailID')}} :</label>
                                    <div v-bind:class="{'has-danger' : v$.logistics.email.$error}">
                                        <input class="form-control" v-model="v$.logistics.email.$model" />
                                        <span v-if="v$.logistics.email.$error" class="error text-danger">
                                            <span v-if="!v$.logistics.email.email"> {{ $t('AddLogistics.RegisterUser_Error_Format_EmailID') }} </span>
                                        </span>
                                    </div>

                                </div>
                                <div class="col-sm-4 form-group ">
                                    <label class="text  font-weight-bolder ">{{ $t('AddLogistics.Website')}} :</label>
                                    <input class="form-control  " v-model="logistics.website" type="text" />

                                </div>

                                <div class="col-sm-4 form-group ">
                                    <label class="text  font-weight-bolder ">{{ $t('AddLogistics.DriverName')}}:</label>
                                    <input class="form-control  " v-model="logistics.xcoordinates" type="text" />

                                </div>
                                <div class="col-sm-4 form-group " v-bind:class="{'has-danger' : v$.logistics.licenseNo.$error}">
                                    <label class="text  font-weight-bolder ">{{ $t('AddLogistics.DriverNumber')}} :</label>
                                    <input class="form-control" v-model="v$.logistics.licenseNo.$model" type="text" />
                                    <span v-if="v$.logistics.licenseNo.$error" class="error text-danger">
                                        <span v-if="!v$.logistics.licenseNo.maxLength">{{ $t('AddLogistics.NameLength') }}</span>
                                    </span>
                                </div>
                                <div class="col-sm-4 form-group ">
                                    <label class="text  font-weight-bolder ">{{ $t('AddLogistics.VehicleNo')}}:</label>
                                    <input class="form-control  " v-model="logistics.ycoordinates" type="text" />

                                </div>
                                <div class="col-sm-6 form-group ">
                                    <label class="text  font-weight-bolder ">{{ $t('AddLogistics.Address')}} :</label>
                                    <textarea class="form-control  " rows="3" v-model="logistics.address" type="text" />

                                </div>
                                <div class="col-sm-6 form-group ">
                                    <label class="text  font-weight-bolder ">{{ $t('AddLogistics.TermsConditions')}} :</label>
                                    <textarea class="form-control  " rows="3" v-model="logistics.termsAndCondition" type="text" />

                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-6 " v-if="formName=='ClearanceAgent'">
                                    <label class="text  font-weight-bolder ">{{ $t('AddLogistics.ServiceFor')}} :</label>
                                    <input class="form-control  " v-model="logistics.serviceFor" type="text" />

                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-6" v-if="formName=='ClearanceAgent'">
                                    <label>
                                        {{ $t('AddLogistics.Ports')}} :
                                    </label>
                                    <div class="form-group">

                                        <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="logistics.ports" :options="['Dry Port', 'Sea Port','Air Port','Dry Port & Sea Port','Dry Port & Air Port','Sea Port & Air Port' ,'Dry Port,Sea Port & Air Port']" :show-labels="false" placeholder="Select Type">
                                        </multiselect>
                                        <multiselect v-else v-model="logistics.ports" :options="['ميناء جاف', 'الميناء البحري','مطار','الميناء الجاف والميناء البحري','الميناء الجاف والميناء الجوي','الميناء البحري والميناء الجوي' ,'الميناء الجاف والميناء البحري والميناء الجوي']" :show-labels="false" v-bind:placeholder="$t('AddLogistics.SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        </multiselect>

                                    </div>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3" >
                                    <div class="checkbox form-check-inline mx-2">
                                        <input type="checkbox" id="inlineCheckbox1" v-model="logistics.isActive">
                                        <label for="inlineCheckbox1"> {{ $t('AddLogistics.Active') }} </label>
                                    </div>
                                </div>
                                <!--<div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3" v-else>
                                    <div class="checkbox form-check-inline mx-2">
                                        <input type="checkbox" id="inlineCheckbox1" v-model="logistics.isActive">
                                        <label for="inlineCheckbox1"> {{ $t('AddLogistics.Active') }} </label>
                                    </div>
                                </div>-->
                              
                                <!--<div class="col-lg-12" >
        <div style="width: 100%"><iframe width="100%" height="200" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://maps.google.com/maps?width=100%25&amp;height=200&amp;hl=en&amp;q=1%20Grafton%20Street,%20Dublin,%20Ireland+(My%20Business%20Name)&amp;t=&amp;z=14&amp;ie=UTF8&amp;iwloc=B&amp;output=embed"><a href="https://www.gps.ie/car-satnav-gps/">best car gps</a></iframe></div>
    </div>-->


                            </div>


                        </div>


                        <div v-if="!loading" class="card-footer">
                            <div class="row">
                                <div v-if="!loading" class=" col-md-12">
                                    <div class="button-items" v-if=" logistics.id=='00000000-0000-0000-0000-000000000000'">
                                        <button class="btn btn-primary" v-bind:disabled="v$.logistics.$invalid" v-if="  (isValid('CanAddTransporter')  && formName=='Transporter') ||  (isValid('CanAddClearanceAgent')  && formName=='ClearanceAgent') ||  (isValid('CanAddShippingLiner')  && formName=='ShippingLinear')" v-on:click="SaveVoucher"><i class="mdi mdi-check-all me-2"></i>  {{ $t('AddLogistics.Save') }}</button>
                                        <button class="btn btn-danger" v-on:click="onCancel">  {{ $t('AddLogistics.Cancel') }}</button>
                                    </div>
                                    <div class="button-items" v-else>
                                        <button class="btn btn-primary" v-bind:disabled="v$.logistics.$invalid" v-if=" (isValid('CanEditTransporter')  && formName=='Transporter') ||  (isValid('CanEditClearanceAgent')  && formName=='ClearanceAgent') ||  (isValid('CanEditShippingLiner')  && formName=='ShippingLinear')" v-on:click="SaveVoucher"><i class="mdi mdi-check-all me-2"></i> {{ $t('AddLogistics.Update') }}</button>
                                        <button class="btn btn-danger" v-on:click="onCancel">  {{ $t('AddLogistics.Cancel') }}</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                   
                </div>
               
                <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
               
            </div>
        </div>

    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, maxLength, requiredIf, email } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        props: ['formName'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                render: 0,
                logistics: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    englishName: '',
                    arabicName: '',
                    licenseNo: '',
                    address: '',
                    contactName: '',
                    contactNo: '',
                    email: '',
                    website: '',
                    termsAndCondition: '',
                    xcoordinates: '',
                    ycoordinates: '',
                    ports: '',
                    serviceFor: '',
                    logisticsForm: '',
                    isActive: true,
                    branchId: '',


                },
                language: 'Nothing',
                disable: false
            }
        },


        validations() {
            return {
                logistics: {
                    email: {
                        email

                    }, code: {
                        required

                    },
                    englishName: {
                        maxLength: maxLength(50)
                    },
                    arabicName: {
                        // required: requiredIf((x) => {
                        //     if (x.englishName == '' || x.englishName == null)
                        //         return true;
                        //     return false;
                        // }),
                        required: requiredIf(function () {
                            return !this.logistics.name;
                        }),
                        maxLength: maxLength(50)
                    },
                    licenseNo: {
                        maxLength: maxLength(50)
                    },


                }
                }
        },
        methods: {
            languageChange: function (lan) {
                if (this.language == lan) {

                    if (this.logistics.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addlogisticsformName?formName=' + this.formName);
                    }
                    else {

                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },

            GetAutoCodeGenerator: function (value) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Region/LogisticCode?logisticsForm=' + value, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.logistics.code = response.data;
                        root.CodeRander++;

                    }
                });
            },

            SaveVoucher: function () {


                if (this.$i18n.locale == 'ar') {
                    if (this.logistics.ports == 'ميناء جاف') {
                        this.logistics.ports = 1;
                    }
                    else if (this.logistics.ports == 'الميناء البحري') {
                        this.logistics.ports = 2;
                    }
                    else if (this.logistics.ports == 'مطار') {
                        this.logistics.ports = 3;
                    }
                    else if (this.logistics.ports == 'الميناء الجاف والميناء البحري') {
                        this.logistics.ports = 4;
                    }
                    else if (this.logistics.ports == 'الميناء الجاف والميناء الجوي') {
                        this.logistics.ports = 6;
                    }
                    else if (this.logistics.ports == 'الميناء البحري والميناء الجوي') {
                        this.logistics.ports = 6;
                    }
                    else if (this.logistics.ports == 'الميناء الجاف والميناء البحري والميناء الجوي') {
                        this.logistics.ports = 7
                    }
                    else {
                        this.logistics.ports = 0;
                    }



                }
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    if (this.logistics.ports == 'Dry Port') {
                        this.logistics.ports = 1;
                    }
                    else if (this.logistics.ports == 'Sea Port') {
                        this.logistics.ports = 2;
                    }
                    else if (this.logistics.ports == 'Air Port') {
                        this.logistics.ports = 3;
                    }
                    else if (this.logistics.ports == 'Dry Port & Sea Port') {
                        this.logistics.ports = 4;
                    }
                    else if (this.logistics.ports == 'Dry Port & Air Port') {
                        this.logistics.ports = 6;
                    }
                    else if (this.logistics.ports == 'Sea Port & Air Port') {
                        this.logistics.ports = 6;
                    }
                    else if (this.logistics.ports == 'Dry Port,Sea Port & Air Port') {
                        this.logistics.ports = 7
                    }
                    else {
                        this.logistics.ports = 0;
                    }


                }
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.logistics.branchId = localStorage.getItem('BranchId');

                this.$https.post('/Region/SaveLogistic', this.logistics, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Add') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Data has been Saved' : 'حفظ بنجاح', 
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        }).then(function (result) {
                            if (result) {


                                window.location.href = "/AddLogistics?formName=" + root.formName;
                            }
                        });

                    }
                    else if (response.data.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Edit') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Data has been Updated' : 'تم التحديث بنجاح', 
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        }).then(function (result) {
                            if (result) {
                                window.location.href = "/logisticsList?formName=" + root.formName;
                            }
                        });

                    }
                    else if (response.data.message.id == '00000000-0000-0000-0000-000000000000') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: response.data.message.isAddUpdate,
                            type: 'error',
                            confirmButtonClass: "btn btn-info",
                            buttonsStyling: false
                        });
                    }

                }, function (value) {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: value,
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                }
                ).catch(error => {

                    var customError = JSON.stringify(error.response.data.error);
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: customError,
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                    root.loading = false;
                });
            },

            onCancel: function () {
                if ((this.isValid('CanViewTransporter') && this.formName == 'Transporter') || (this.isValid('CanViewClearanceAgent') && this.formName == 'ClearanceAgent') || (this.isValid('CanViewShippingLiner') && this.formName == 'ShippingLinear')) {
                    
                    this.$router.push({
                        path:'/logisticsList',
                        query:{
                            formName : this.formName
                        }
                    })
                }
                else {
                    this.$router.go();
                }
            },
        },
        watch: {
            formName: function () {
                this.$emit('update:modelValue', this.$route.name + this.formName);
                if (this.formName == 'Transporter') {
                    if (this.$route.query.data == undefined) {
                        this.GetAutoCodeGenerator(this.formName);
                        this.logistics.logisticsForm = this.formName;
                    }
                    if (this.$route.query.data != undefined) {
                        this.logistics = this.$route.query.data;
                        this.logistics.logisticsForm = 'Transporter';
                    }
                }
                else if (this.formName == 'ClearanceAgent') {
                    if (this.$route.query.data == undefined) {
                        this.GetAutoCodeGenerator(this.formName);
                        this.logistics.logisticsForm = this.formName;
                    }
                    if (this.$route.query.data != undefined) {
                        this.logistics = this.$route.query.data;
                        this.logistics.logisticsForm = 'Transporter';
                        if (this.$i18n.locale == 'ar') {
                            if (this.logistics.ports == 1) {
                                this.logistics.ports = 'التحقق من';
                            }
                            if (this.logistics.ports == 2) {
                                this.logistics.ports = 'تحويل';
                            }
                            if (this.logistics.ports == 3) {
                                this.logistics.ports = 'الوديعة';
                            }

                            if (this.logistics.paymentMode == 0) {
                                this.logistics.paymentMode = 'السيولة النقدية';
                            }
                            if (this.logistics.paymentMode == 1) {
                                this.logistics.paymentMode = 'مصرف';
                            }



                        }
                        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                            if (this.logistics.ports == 1) {
                                this.logistics.ports = 'Cheque';
                            }
                            if (this.logistics.ports == 2) {
                                this.logistics.ports = 'Transfer';
                            }
                            if (this.logistics.ports == 3) {
                                this.logistics.ports = 'Deposit';
                            }
                            if (this.logistics.paymentMode == 0) {
                                this.logistics.paymentMode = 'Cash';
                            }
                            if (this.logistics.paymentMode == 1) {
                                this.logistics.paymentMode = 'Bank';
                            }

                        }
                    }
                }

            }
        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            this.$emit('update:modelValue', this.$route.name + this.formName);
        },
        mounted: function () {

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');

            this.language = this.$i18n.locale;
            if (this.formName == 'Transporter') {
                if (this.$route.query.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.logistics.logisticsForm = this.formName;

                }
                if (this.$route.query.data != undefined) {
                    this.logistics = this.$route.query.data;
                    this.logistics.logisticsForm = 'Transporter';

                }

            }
            if (this.formName == 'ShippingLinear') {
                if (this.$route.query.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.logistics.logisticsForm = this.formName;

                }
                if (this.$route.query.data != undefined) {
                    this.logistics = this.$route.query.data;
                    this.logistics.logisticsForm = 'ShippingLinear';

                }

            }
            if (this.formName == 'ClearanceAgent') {
                if (this.$route.query.data == undefined) {
                    this.GetAutoCodeGenerator(this.formName);
                    this.logistics.logisticsForm = this.formName;

                }
                if (this.$route.query.data != undefined) {
                    this.logistics = this.$route.query.data;
                    this.logistics.logisticsForm = 'ClearanceAgent';
                    if (this.$i18n.locale == 'ar') {
                        if (this.logistics.ports == 1) {
                            this.logistics.ports = 'ميناء جاف';
                        }
                        else if (this.logistics.ports == 2) {
                            this.logistics.ports = 'الميناء البحري';
                        }
                        else if (this.logistics.ports == 3) {
                            this.logistics.ports = 'مطار';
                        } if (this.logistics.ports == 4) {
                            this.logistics.ports = 'الميناء الجاف والميناء البحري';
                        }
                        else if (this.logistics.ports == 5) {
                            this.logistics.ports = 'الميناء الجاف والميناء الجوي';
                        }
                        else if (this.logistics.ports == 6) {
                            this.logistics.ports = 'الميناء البحري والميناء الجوي';
                        }
                        else if (this.logistics.ports == 7) {
                            this.logistics.ports = 'الميناء الجاف والميناء البحري والميناء الجوي';
                        }



                    }
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        if (this.logistics.ports == 1) {
                            this.logistics.ports = 'Dry Port';
                        }
                        else if (this.logistics.ports == 2) {
                            this.logistics.ports = 'Sea Port';
                        }
                        else if (this.logistics.ports == 3) {
                            this.logistics.ports = 'Air Port';
                        } if (this.logistics.ports == 4) {
                            this.logistics.ports = 'Dry Port & Sea Port';
                        }
                        else if (this.logistics.ports == 5) {
                            this.logistics.ports = 'Dry Port & Air Port';
                        }
                        else if (this.logistics.ports == 6) {
                            this.logistics.ports = 'Sea Port & Air Port';
                        }
                        else if (this.logistics.ports == 7) {
                            this.logistics.ports = 'Dry Port,Sea Port & Air Port';
                        }


                    }

                }
            }

            this.render++;
        }
    }
</script>