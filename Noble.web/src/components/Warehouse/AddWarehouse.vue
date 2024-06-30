<template>
    <div class="row" v-if=" isValid('CanAddWareHouse') || isValid('CanEditWareHouse')|| isValid('Noble Admin')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AddWarehouse.Heading') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('AddWarehouse.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('AddWarehouse.Heading') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="row ">
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddWarehouse.storeId') }} : <span class="text-danger">  *</span></label>
                                    <div>
                                        <input class="form-control" disabled v-model="warehouse.storeID">
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3" v-if="english=='true'">
                                    <label>{{$englishLanguage($t('AddWarehouse.NameEn'))  }} :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.warehouse.name.$error}">
                                        <input class="form-control " v-model="v$.warehouse.name.$model" />
                                        <span v-if="v$.warehouse.name.$error" class="error text-danger">
                                            <span v-if="!v$.warehouse.name.required"> {{ $t('AddWarehouse.Error_name_Required') }}</span>
                                            <span v-if="!v$.warehouse.name.maxLength"> {{ $t('AddWarehouse.Error_name_Length') }}</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3" v-if="isOtherLang() || isValid('Noble Admin')" v-bind:class="{'has-danger' : v$.warehouse.nameArabic.$error}">
                                    <label class="text  font-weight-bolder">{{$arabicLanguage($t('AddWarehouse.NameAr'))  }}: <span class="text-danger"> *</span></label>
                                    <input class="form-control " v-model="v$.warehouse.nameArabic.$model" type="text" />
                                    <span v-if="v$.warehouse.nameArabic.$error" class="error">
                                        <span v-if="!v$.warehouse.nameArabic.required"> {{ $t('AddWarehouse.NameRequired') }}</span>
                                        <span v-if="!v$.warehouse.nameArabic.maxLength">{{ $t('AddWarehouse.NameLength') }}</span>
                                    </span>
                                </div>
                                <!--<div v-if="isOtherLang() || isValid('Noble Admin')" class=" form-group has-label col-sm-6 " v-bind:class="{'has-danger' : v$.warehouse.nameArabic.$error} && ($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                    <label class="text  font-weight-bolder">{{ $t('AddWarehouse.NameAr') |arabicLanguage}}: <span class="text-danger"> *</span></label>
                                    <input class="form-control " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.warehouse.nameArabic.$model" type="text" />
                                    <span v-if="v$.warehouse.nameArabic.$error" class="error">
                                        <span v-if="!v$.warehouse.nameArabic.required"> {{ $t('AddWarehouse.NameRequired') }}</span>
                                        <span v-if="!v$.warehouse.nameArabic.maxLength">{{ $t('AddWarehouse.NameLength') }}</span>
                                    </span>
                                </div>-->
                            <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                <label>{{ $t('AddWarehouse.contactNo') }}:</label>
                                <div v-bind:class="{'has-danger' : v$.warehouse.contactNo.$error}">
                                    <input class="form-control" type="number"  v-model="v$.warehouse.contactNo.$model" />
                                </div>
                                <span v-if="v$.warehouse.contactNo.$error" class="error text-danger">
                                    <span v-if="!v$.warehouse.contactNo.maxLength">{{ $t('AddWarehouse.Error_contactNo_Length') }}</span>
                                </span>
                            </div>
                            <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                <label>{{ $t('AddWarehouse.email') }}:</label>
                                <div v-bind:class="{'has-danger' : v$.warehouse.email.$error}">
                                    <input class="form-control" v-model="v$.warehouse.email.$model" />
                                </div>
                                <span v-if="v$.warehouse.email.$error" class="error text-danger">
                                    <span v-if="!v$.warehouse.email.email">{{ $t('AddWarehouse.Error_email_email') }}</span>
                                </span>
                            </div>
                            <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                <label>{{ $t('AddWarehouse.city') }}</label>
                                <div v-bind:class="{'has-danger' : v$.warehouse.city.$error}">
                                    <input class="form-control" v-model="warehouse.city" />
                                </div>
                                <span v-if="v$.warehouse.city.$error" class="error text-danger">
                                    <span v-if="!v$.warehouse.city.required">{{ $t('AddWarehouse.Error_city_Required') }}</span>
                                    <span v-if="!v$.warehouse.city.maxLength">{{ $t('AddWarehouse.Error_city_Length') }}</span>
                                </span>
                            </div>

                            <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                <label>{{ $t('AddWarehouse.country') }}</label>
                                <div v-bind:class="{'has-danger' : v$.warehouse.country.$error}">
                                    <input class="form-control" v-model="warehouse.country" />
                                </div>
                                <span v-if="v$.warehouse.country.$error" class="error text-danger">
                                    <span v-if="!v$.warehouse.country.required">{{ $t('AddWarehouse.Error_country_Required') }}</span>
                                    <span v-if="!v$.warehouse.country.maxLength">{{ $t('AddWarehouse.Error_country_Length') }}</span>
                                </span>
                            </div>
                            <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                <label>{{ $t('AddWarehouse.cctvLicenseNo') }}</label>
                                <div v-bind:class="{'has-danger' : v$.warehouse.cctvLicenseNo.$error}">
                                    <input class="form-control" type="number" v-model="warehouse.cctvLicenseNo" />
                                </div>
                                <span v-if="v$.warehouse.cctvLicenseNo.$error" class="error text-danger">
                                    <span v-if="!v$.warehouse.cctvLicenseNo.required">{{ $t('AddWarehouse.Error_cctvLicenseNo_Required') }}</span>
                                    <span v-if="!v$.warehouse.cctvLicenseNo.maxLength">{{ $t('AddWarehouse.Error_cctvLicenseNo_Length') }}</span>
                                </span>
                            </div>

                            <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                <label>{{ $t('AddWarehouse.cctvLicenseExpiry') }}</label>
                                <div v-bind:class="{'has-danger' : v$.warehouse.cctvLicenseExpiry.$error}">
                                    <datepicker :key="daterander" v-model="warehouse.cctvLicenseExpiry"></datepicker>
                                </div>
                                <span v-if="v$.warehouse.cctvLicenseExpiry.$error" class="error text-danger">
                                    <span v-if="!v$.warehouse.cctvLicenseExpiry.required">{{ $t('AddWarehouse.Error_cctvLicenseExpiry_Required') }}</span>
                                </span>
                            </div>

                            <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                <label>{{ $t('AddWarehouse.civilDefenceLicenseNo') }}</label>
                                <div v-bind:class="{'has-danger' : v$.warehouse.civilDefenceLicenseNo.$error}">
                                    <input class="form-control" type="number" v-model="warehouse.civilDefenceLicenseNo" />
                                </div>
                                <span v-if="v$.warehouse.civilDefenceLicenseNo.$error" class="error text-danger">
                                    <span v-if="!v$.warehouse.civilDefenceLicenseNo.required">{{ $t('AddWarehouse.Error_civilDefenceLicenseNo_Required') }}</span>
                                    <span v-if="!v$.warehouse.cctvLicenseNo.maxLength">{{ $t('AddWarehouse.Error_civilDefenceLicenseNo_Length') }}</span>
                                </span>
                            </div>
                            <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                <label>{{ $t('AddWarehouse.civilDefenceLicenseExpiry') }}</label>
                                <div v-bind:class="{'has-danger' : v$.warehouse.civilDefenceLicenseExpiry.$error}">
                                    <datepicker :key="daterander" v-model="warehouse.civilDefenceLicenseExpiry"></datepicker>
                                </div>
                                <span v-if="v$.warehouse.civilDefenceLicenseExpiry.$error" class="error text-danger">
                                    <span v-if="!v$.warehouse.civilDefenceLicenseExpiry.required">{{ $t('AddWarehouse.Error_civilDefenceLicenseExpiry_Required') }}</span>
                                </span>
                            </div>


                            <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                <label>{{ $t('AddWarehouse.licenseNo') }}</label>
                                <div v-bind:class="{'has-danger' : v$.warehouse.licenseNo.$error}">
                                    <input class="form-control" type="number" v-model="warehouse.licenseNo" />
                                </div>
                                <span v-if="v$.warehouse.licenseNo.$error" class="error text-danger">
                                    <span v-if="!v$.warehouse.licenseNo.required">{{ $t('AddWarehouse.Error_licenseNo_Required') }}</span>
                                    <span v-if="!v$.warehouse.licenseNo.maxLength">{{ $t('AddWarehouse.Error_licenseNo_Length') }}</span>
                                </span>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddWarehouse.licenseExpiry') }}</label>
                                    <div v-bind:class="{'has-danger' : v$.warehouse.licenseExpiry.$error}">
                                        <datepicker :key="daterander" v-model="warehouse.licenseExpiry"></datepicker>
                                    </div>
                                    <span v-if="v$.warehouse.licenseExpiry.$error" class="error text-danger">
                                        <span v-if="!v$.warehouse.licenseExpiry.required">{{ $t('AddWarehouse.Error_licenseExpiry_Required') }}</span>
                                    </span>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddWarehouse.address') }}:</label>
                                    <div v-bind:class="{'has-danger' : v$.warehouse.address.$error}">
                                        <textarea class="form-control" v-model="v$.warehouse.address.$model" />
                                    </div>
                                    <span v-if="v$.warehouse.address.$error" class="error text-danger">
                                        <span v-if="!v$.warehouse.address.required">{{ $t('AddWarehouse.Error_address_Required') }}</span>
                                        <span v-if="!v$.warehouse.address.maxLength">{{ $t('AddWarehouse.Error_address_Length') }}</span>
                                    </span>
                                </div>
                                <div v-if="companyId != undefined" class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <span>{{ $t('AddWarehouse.Active') }} :</span>
                                    <div class="checkbox  mx-1">
                                        <input type="checkbox" id="inlineCheckbox1" v-model="warehouse.isActive">
                                        <label for="inlineCheckbox1">  </label>
                                    </div>
                                    

                                </div>



                        </div><!--end col-->
                    </div><!--end row-->

                </div>
                <div  class="card-footer">
                    <div class="row">
                        <div  class=" col-md-12">
                            <div class="button-items" v-if="warehouse.id == null || warehouse.id=='00000000-0000-0000-0000-000000000000' && (isValid('CanAddWareHouse') || isValid('Noble Admin'))">
                                    <button class="btn btn-outline-primary  " v-on:click="SaveWarehouseInformation()" :disabled="v$.warehouse.$invalid"><i class="far fa-save"></i>  {{ $t('AddWarehouse.btnSave') }}</button>
                                    <button class="btn btn-danger " v-on:click="BackToList()">{{ $t('AddWarehouse.btnClear') }}</button>

                             </div>
                            <div class="button-items" v-else>
                                <div v-if=" isValid('CanEditWareHouse')|| isValid('Noble Admin')">
                                    <button class="btn btn-outline-primary " v-on:click="UpdateWarehouseInformation()"><i class="far fa-save"></i>  {{ $t('AddWarehouse.btnUpdate') }}</button>
                                    <button class="btn btn-danger " v-on:click="BackToList()">{{ $t('AddWarehouse.btnClear') }}</button>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </div>
      </div>
    </div>
    <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
</div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { maxLength, email, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default ({
        mixins: [clickMixin],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                active: 'AddWarehouse',
                daterander: 0,
                loading: false,
                warehouse: {
                    id: '00000000-0000-0000-0000-000000000000',

                    storeID: '',
                    language: 'Nothing',
                    name: '',
                    nameArabic: '',
                    address: '',
                    city: '',
                    country: '',
                    cctvLicenseNo: '',
                    cctvLicenseExpiry: '',
                    civilDefenceLicenseNo: '',
                    civilDefenceLicenseExpiry: '',
                    contactNo: '',
                    email: '',
                    licenseNo: '',
                    licenseExpiry: '',
                    isActive: false
                },
                companyId: '00000000-0000-0000-0000-000000000000'
            }
        },
        validations() {
            return {
                warehouse:
                {
                    name:
                    {
                        maxLength: maxLength(50)
                    },
                    nameArabic:
                    {
                        required: requiredIf(function () {
                            return !this.warehouse.name;
                        }),
                        // required: requiredIf((x) => {
                        //     if (x.name == '' || x.name == null)
                        //         return true;
                        //     return false;
                        // }),
                        maxLength: maxLength(50)
                    },
                    address:
                    {
                        maxLength: maxLength(250)
                    },
                    city:
                    {

                    },
                    country:
                    {

                    },
                    cctvLicenseNo:
                    {

                    },
                    cctvLicenseExpiry:
                    {

                    },
                    civilDefenceLicenseNo:
                    {

                    },
                    civilDefenceLicenseExpiry:
                    {

                    },
                    contactNo:
                    {
                        maxLength: maxLength(20)
                    },
                    email:
                    {
                        email
                    },
                    licenseNo:
                    {

                    },
                    licenseExpiry:
                    {

                    }
                }
                }
        },
        methods: {
            languageChange: function (lan) {

                if (this.language == lan) {
                    if (this.warehouse.id == '00000000-0000-0000-0000-000000000000') {
                        
                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/AddWarehouse');
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

            AutoIncrementstoreID: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/Company/WarehouseAutoGenerateCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.warehouse.storeID = response.data;
                    }
                });
            },
            BackToList: function () {
                if (this.isValid('CanViewWareHouse')) {
                    this.$router.push('/Warehouse')
                }
                else {
                    this.$router.go();
                }
                
            },
            SaveWarehouseInformation: function () {
                var root = this;
                this.loading = true;
                
                var url = '/Company/SaveWarehouseInformation?companyId=' + root.companyId;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                root.$https
                    .post(url, root.warehouse, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                            root.loading = false
                            root.info = response.data.bpi

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            }).then(function (ok) {
                                if (ok != null) {
                                    if (root.isValid('CanViewWareHouse')) {
                                        root.$router.push('/Warehouse')
                                    }
                                    else {
                                        root.$router.go();
                                    }
                                }
                            });
                        }
                        else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                            root.loading = false
                            root.info = response.data.bpi

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            }).then(function (ok) {
                                if (ok != null) {
                                    if (root.isValid('CanViewWareHouse')) {
                                        root.$router.push('/Warehouse')
                                    }
                                    else {
                                        root.$router.go();
                                    }
                                }
                            });
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: response.data.message.isAddUpdate,
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                icon: 'error',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }         
                        
                    })
                    .catch(error => {
                        console.log(error)
                        this.$swal.fire(
                        {
                            icon: 'error',
                            title: 'Oops...',
                            text: error,
                        });
                        root.errored = true
                    })
                    .finally(() => root.loading = false)
            },
            UpdateWarehouseInformation: function () {
                var root = this;
                
                var url = '/Company/SaveWarehouseInformation?companyId=' + root.companyId;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .post(url, root.warehouse, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                            root.loading = false
                            root.info = response.data.bpi

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            }).then(function (ok) {
                                if (ok != null) {
                                    if (root.isValid('CanViewWareHouse')) {
                                        root.$router.push('/Warehouse')
                                    }
                                    else {
                                        root.$router.go();
                                    }
                                }
                            });
                        }
                        else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                            root.loading = false
                            root.info = response.data.bpi

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            }).then(function (ok) {
                                if (ok != null) {
                                    if (root.isValid('CanViewWareHouse')) {
                                        root.$router.push('/Warehouse')
                                    }
                                    else {
                                        root.$router.go();
                                    }
                                }
                            });
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: response.data.message.isAddUpdate,
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                icon: 'error',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }         
                        
                    })
                    .catch(error => {
                        console.log(error)
                        this.$swal.fire(
                        {
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error,
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,

                        });
                        root.errored = true
                    })
                    .finally(() => root.loading = false)
            }
        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            
            this.language = this.$i18n.locale;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.$route.query.data == undefined) {
                this.AutoIncrementstoreID();
            }

            if (this.$route.query.data != undefined) {
                this.warehouse = this.$route.query.data;
                this.companyId = this.$route.query.companyId == undefined ? '00000000-0000-0000-0000-000000000000': this.$route.query.companyId;
            }
                this.daterander++;
        }
    })
    
</script>