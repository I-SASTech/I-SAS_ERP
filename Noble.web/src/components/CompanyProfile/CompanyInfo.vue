<template>
    <div>
        <div class="col-md-12  mt-4" >
            <div class="card">
                <div class="card-header DayHeading "  >
                    <span style="font-size:30px;font-weight:bold">{{ $t('CompanyInfo.CompanyProfile') }}</span>
                </div>
                <div class="card-body" >
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="row">
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4" v-if="english=='true'">
                                    <label>{{$englishLanguage($t('CompanyInfo.BusinessNameInEnglish'))  }} :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.companyProfile.nameInEnglish.$error}">
                                        <input class="form-control " v-bind:disabled="isDisable" v-model="v$.companyProfile.nameInEnglish.$model" />
                                        <span v-if="v$.companyProfile.nameInEnglish.$error" class="error text-danger">
                                            <span v-if="!v$.companyProfile.nameInEnglish.required">{{ $t('CompanyInfo.NameRequired') }}</span>
                                            <span v-if="!v$.companyProfile.nameInEnglish.maxLength"> {{ $t('CompanyInfo.Namelength') }}</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4" v-if="isOtherLang()">
                                    <label>{{$arabicLanguage($t('CompanyInfo.BusinessName(InArabic)'))  }} :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.companyProfile.nameInArabic.$error}">
                                        <input class="form-control text-right " v-bind:disabled="isDisable" v-model="v$.companyProfile.nameInArabic.$model" />
                                        <span v-if="v$.companyProfile.nameInArabic.$error" class="error text-danger">
                                            <span v-if="!v$.companyProfile.nameInArabic.required"> {{ $t('CompanyInfo.NameRequired') }}</span>
                                            <span v-if="!v$.companyProfile.nameInArabic.maxLength"> {{ $t('CompanyInfo.Namelength') }}</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4">
                                    <label>{{ $t('CompanyInfo.CommercialRegistrationNo') }} :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.companyProfile.companyRegNo.$error}">
                                        <input class="form-control "   v-model="v$.companyProfile.companyRegNo.$model" />
                                        <span v-if="v$.companyProfile.companyRegNo.$error" class="error text-danger">
                                            <span v-if="!v$.companyProfile.companyRegNo.required">{{ $t('CompanyInfo.RegistrationRequired') }} </span>
                                            <span v-if="!v$.companyProfile.companyRegNo.maxLength"> {{ $t('CompanyInfo.RegistrationLength') }}</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4">
                                    <label for="companyProfile.TaxRegisterNo">{{ $t('CompanyInfo.VATNo') }} : <span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.companyProfile.vatRegistrationNo.$error}">
                                        <input class="form-control "   v-model="v$.companyProfile.vatRegistrationNo.$model" />
                                        <span v-if="v$.companyProfile.vatRegistrationNo.$error" class="error text-danger">
                                            <span v-if="!v$.companyProfile.vatRegistrationNo.required">{{ $t('CompanyInfo.VatNoRequired') }}</span>
                                            <span v-if="!v$.companyProfile.vatRegistrationNo.maxLength">{{ $t('CompanyInfo.VatMaximum') }}</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4" v-if="english=='true'">
                                    <label>{{$englishLanguage($t('CompanyInfo.CompanyName(InEnglish)'))  }}  :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.companyProfile.companyNameEnglish.$error}">
                                        <input class="form-control " v-bind:disabled="isDisable"  v-model="v$.companyProfile.companyNameEnglish.$model" />
                                        <span v-if="v$.companyProfile.companyNameEnglish.$error" class="error text-danger">
                                            <span v-if="!v$.companyProfile.companyNameEnglish.required"> {{ $t('CompanyInfo.NameRequired') }}</span>
                                            <span v-if="!v$.companyProfile.companyNameEnglish.maxLength"> {{ $t('CompanyInfo.NameMaximum') }}</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4" v-if="isOtherLang()">
                                    <label>{{$arabicLanguage($t('CompanyInfo.CompanyName(InArabic)'))  }}:<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.companyProfile.companyNameArabic.$error}">
                                        <input class="form-control text-right " v-bind:disabled="isDisable"  v-model="v$.companyProfile.companyNameArabic.$model" />
                                        <span v-if="v$.companyProfile.companyNameArabic.$error" class="error text-danger">
                                            <span v-if="!v$.companyProfile.companyNameArabic.required"> {{ $t('CompanyInfo.NameRequired') }}</span>
                                            <span v-if="!v$.companyProfile.companyNameArabic.maxLength"> {{ $t('CompanyInfo.NameMaximum') }}</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4" v-if="english=='true'">
                                    <label>{{$englishLanguage($t('CompanyInfo.BusinessType(InEnglish)'))  }} :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.companyProfile.categoryInEnglish.$error}">
                                        <input class="form-control " v-bind:disabled="isDisable"  v-model="v$.companyProfile.categoryInEnglish.$model" />
                                        <span v-if="v$.companyProfile.categoryInEnglish.$error" class="error text-danger">
                                            <span v-if="!v$.companyProfile.categoryInEnglish.required"> {{ $t('CompanyInfo.NameRequired') }}</span>
                                            <span v-if="!v$.companyProfile.categoryInEnglish.maxLength"> {{ $t('CompanyInfo.NameMaximum') }}</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4" v-if="isOtherLang()">
                                    <label>{{$arabicLanguage($t('CompanyInfo.BusinessType(InArabic)'))  }} :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.companyProfile.categoryInArabic.$error}">
                                        <input class="form-control text-right " v-bind:disabled="isDisable"  v-model="v$.companyProfile.categoryInArabic.$model" />
                                        <span v-if="v$.companyProfile.categoryInArabic.$error" class="error text-danger">
                                            <span v-if="!v$.companyProfile.categoryInArabic.required"> {{ $t('CompanyInfo.NameRequired') }}</span>
                                            <span v-if="!v$.companyProfile.categoryInArabic.maxLength"> {{ $t('CompanyInfo.NameMaximum') }}</span>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4  card" :key="renderedImage" style="height: 200px;padding:15px">
                            <AddProductImage v-on:picPath="getImage" v-bind:path="image" />

                        </div>
                    </div>
                   

                    <div class="row">

                        <div class="col-sm-12" v-if="english=='true'">
                            <label for="companyProfile.address">{{$englishLanguage($t('CompanyInfo.Address(InEnglish)'))  }}  : <span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.companyProfile.addressInEnglish.$error}">
                                <textarea class="form-control" v-model="v$.companyProfile.addressInEnglish.$model" />
                                <span v-if="v$.companyProfile.addressInEnglish.$error" class="error text-danger">
                                    <span v-if="!v$.companyProfile.addressInEnglish.required">{{ $t('CompanyInfo.AddressRequired') }}</span>
                                    <span v-if="!v$.companyProfile.addressInEnglish.maxLength">{{ $t('CompanyInfo.AddressMaximum') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-12" v-if="isOtherLang()">
                            <label for="companyProfile.address">{{$arabicLanguage($t('CompanyInfo.Address(InArabic)'))  }} : <span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.companyProfile.addressInArabic.$error}">
                                <textarea class="form-control text-right" v-bind:disabled="isDisable" v-model="v$.companyProfile.addressInArabic.$model" />
                                <span v-if="v$.companyProfile.addressInArabic.$error" class="error text-danger">
                                    <span v-if="!v$.companyProfile.addressInArabic.required">{{ $t('CompanyInfo.AddressRequired') }}</span>
                                    <span v-if="!v$.companyProfile.addressInArabic.maxLength">{{ $t('CompanyInfo.AddressMaximum') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-12 mt-3">
                            <div class="button-items">
                                <button class="btn btn-outline-primary   mr-2" v-bind:disabled="v$.companyProfile.$invalid" v-on:click="SaveCompanyAccountSetup()"> <i class="far fa-save "></i>  {{ $t('CompanyProfile.Update') }}</button>
                                <button class="btn  btn-danger   mr-2" v-on:click="BackToList()">{{ $t('CompanyProfile.btnClear') }}</button>

                            </div>
                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--<div v-else> <acessdenied></acessdenied></div>-->

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'

    export default ({

        mixins: [clickMixin],
         setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                companyProfile: {
                    id: '00000000-0000-0000-0000-000000000000',
                    nameInArabic: '',
                    nameInEnglish: '',
                    categoryInEnglish: '',
                    categoryInArabic: '',
                    CompanyRegNo: '',
                    vatRegistrationNo: '',
                    companyNameArabic: '',
                    companyNameEnglish: '',
                    addressInEnglish: '',
                    addressInArabic: '',
                    logoPath: '',
                    countryInArabic: '',
                    countryInEnglish: '',
                    cityInEnglish: '',
                    cityInArabic: '',
                    landLine: '',
                },
                stepsVm: {
                    companyId: '',
                    step1: false,
                    step2: false,
                    step3: false,
                    step4: false,
                },
                companyId: '',
                image: '',
                renderedImage: 0,
                isDisable: false,
                isDelete: false,
                arabic: '',
                english: '',
            }
        },
        validations() {
          return{
              companyProfile:
            {
                nameInEnglish: {
                    
                    maxLength: maxLength(50)
                },
                nameInArabic: {
                        required: requiredIf(function () {
                            return !this.companyProfile.nameInEnglish;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.nameInEnglish == '' || x.nameInEnglish == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(50)
                },

                companyNameEnglish: {
                    
                    maxLength: maxLength(50)
                },
                companyNameArabic: {
                    required: requiredIf(function () {
                            return !this.companyProfile.companyNameEnglish;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.companyNameEnglish == '' || x.companyNameEnglish == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(50)
                },
               
                companyRegNo: {
                    required,
                    maxLength: maxLength(50)
                },
                vatRegistrationNo: {
                    required,
                    maxLength: maxLength(50)
                },

                categoryInEnglish: {
                    
                    maxLength: maxLength(50)
                },
                categoryInArabic: {
                     required: requiredIf(function () {
                            return !this.companyProfile.categoryInEnglish;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.categoryInEnglish == '' || x.categoryInEnglish == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(50)
                },

                addressInEnglish: {
                    
                    maxLength: maxLength(300)
                },
                addressInArabic: {
                     required: requiredIf(function () {
                            return !this.companyProfile.addressInEnglish;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.addressInEnglish == '' || x.addressInEnglish == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(300)
                },
                
            }
          }
        },
        methods: {
            DeleteAttachment(path) {

                
                var token = '';
                var root = this;
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Contact/DeleteAttachment?path=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data == true) {
                        
                        root.renderedImage++;
                        root.isDelete = false;
                        root.image = null;
                        root.company.logoPath = null;

                    }
                });
            },
            getBase64Image: function (path) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {

                            root.image = response.data;

                            root.renderedImage++;

                            root.isDelete = true;
                        }
                    });
            },
            getImage: function (value) {
                this.companyProfile.logoPath = value;
                this.isDelete = true;
            },
            BackToList: function () {
                this.$router.push('/Setup')
            },
            SaveCompanyAccountSetup: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.post('/Company/SaveLocation', this.companyProfile, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        
                        if (root.$route.query.isProceed == undefined) {
                            root.$swal({
                                title: root.$t('CompanyInfo.Saved'),
                                text: root.$t('CompanyInfo.SavedSuccessfully'),
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                                confirmButtonClass: "btn btn-success",
                                buttonsStyling: false
                            })
                        }
                        else {


                            root.$swal({
                                title: root.$t('CompanyInfo.Saved'),
                                text: root.$t('CompanyInfo.SavedSuccessfully'),
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                                confirmButtonClass: "btn btn-success",
                                buttonsStyling: false
                            }).then(
                                root.stepsVm.companyId = localStorage.getItem('CompanyID'),
                                root.stepsVm.step2 = true,
                                root.$https.post('/account/SetupUpdateInCompany', root.stepsVm, { headers: { "Authorization": `Bearer ${token}` } })
                                    .then(function (response) {

                                        if (response.data) {
                                            localStorage.setItem('companyProfile', true);
                                          
                                                root.$router.push({
                                                    path: '/Setup',
                                                    query: {
                                                        account: true
                                                    }
                                                });
                                            
                                        } else {
                                            console.log("error: something wrong from db.");
                                        }
                                    })


                            )
                        }


                    }
                });
            },
            GetCompanyDetails: function (Id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/Company/EditCompany?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        
                        root.companyProfile.id = response.data.id;
                        root.companyProfile.nameInEnglish = response.data.nameEnglish;
                        root.companyProfile.nameInArabic = response.data.nameArabic;

                        root.companyProfile.cityInEnglish = response.data.cityEnglish;
                        root.companyProfile.cityInArabic = response.data.cityArabic;
                        root.companyProfile.addressInEnglish = response.data.addressEnglish;
                        root.companyProfile.addressInArabic = response.data.addressArabic;

                        root.companyProfile.countryInEnglish = response.data.countryEnglish;
                        root.companyProfile.landLine = response.data.landLine;
                        root.companyProfile.countryInArabic = response.data.countryArabic;

                        root.companyProfile.categoryInEnglish = response.data.categoryEnglish;
                        root.companyProfile.categoryInArabic = response.data.categoryArabic;

                        root.companyProfile.companyRegNo = response.data.comercialRegNo;
                        root.companyProfile.vatRegistrationNo = response.data.vatRegistrationNo;

                        root.companyProfile.companyNameEnglish = response.data.companyNameEnglish;
                        root.companyProfile.companyNameArabic = response.data.companyNameArabic;
                        root.companyProfile.logoPath = response.data.logoPath;



                        if (response.data.logoPath != null) {
                            root.getBase64Image(response.data.logoPath);
                        }


                    }
                });
            }
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.$route.query.isProceed == undefined) {
                this.isDisable = true;
            }
            this.companyId = localStorage.getItem('CompanyID');

            this.GetCompanyDetails(this.companyId);

        }
    })

</script>