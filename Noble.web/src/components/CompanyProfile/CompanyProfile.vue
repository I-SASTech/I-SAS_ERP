<template>
    <div class="row" v-if="isValid('CanUpdateCompanyInfo')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('CompanyProfile.CompanyProfile') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('CompanyProfile.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('CompanyProfile.CompanyProfile') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">                  
                    <form  class="form-horizontal form-wizard-wrapper wizard clearfix" role="application">
                        <div class="content clearfix">
                            <fieldset id="form-horizontal-p-0" aria-labelledby="form-horizontal-h-0" class="current" aria-hidden="false">
                                <div class="row">
                                    <div class="col-md-12 card-header mb-4" style="padding-top:0px !important;">
                                        <h4 class="page-title my-2">{{ $t('CompanyProfile.BusinessInfo') }}</h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group row" v-if="english=='true'">
                                            <label class="col-lg-4 col-form-label">{{$englishLanguage($t('CompanyProfile.BusinessNameInEnglish'))  }} :<span class="text-danger"> *</span></label>
                                            <div class="col-lg-8" v-bind:class="{'has-danger' : v$.companyProfile.nameInEnglish.$error}">
                                                <input class="form-control "  v-model="v$.companyProfile.nameInEnglish.$model" />
                                                <span v-if="v$.companyProfile.nameInEnglish.$error" class="error text-danger">
                                                    <span v-if="!v$.companyProfile.nameInEnglish.required">{{ $t('CompanyProfile.NameRequired') }}</span>
                                                    <span v-if="!v$.companyProfile.nameInEnglish.maxLength"> {{ $t('CompanyProfile.Namelength') }}</span>
                                                </span>
                                            </div>

                                        </div><!--end form-group-->
                                    </div><!--end col-->
                                    <div class="col-md-6">
                                        <div class="form-group row" v-if="isOtherLang()">
                                            <label class="col-lg-4 col-form-label">{{$arabicLanguage($t('CompanyProfile.BusinessName(InArabic)'))  }}  :<span class="text-danger"> *</span>.</label>
                                            <div class="col-lg-8" v-bind:class="{'has-danger' :  v$.companyProfile.nameInArabic.$error}">
                                                <input class="form-control "  v-model="v$.companyProfile.nameInArabic.$model" />
                                                <span v-if="v$.companyProfile.nameInArabic.$error" class="error text-danger">
                                                    <span v-if="!v$.companyProfile.nameInArabic.required"> {{ $t('CompanyProfile.NameRequired') }}</span>
                                                    <span v-if="!v$.companyProfile.nameInArabic.maxLength"> {{ $t('CompanyProfile.Namelength') }}</span>
                                                </span>
                                            </div>
                                        </div><!--end form-group-->
                                    </div><!--end col-->
                                </div><!--end row-->
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group row" v-if="english=='true'">
                                            <label class="col-lg-4 col-form-label">{{$englishLanguage($t('CompanyProfile.BusinessType(InEnglish)'))  }}  :<span class="text-danger"> *</span></label>
                                            <div class="col-lg-8" v-bind:class="{'has-danger' :  v$.companyProfile.categoryInEnglish.$error}">
                                                <input class="form-control "  v-model="v$.companyProfile.categoryInEnglish.$model" />
                                                <span v-if="v$.companyProfile.categoryInEnglish.$error" class="error text-danger">
                                                    <span v-if="!v$.companyProfile.categoryInEnglish.required"> {{ $t('CompanyProfile.NameRequired') }}</span>
                                                    <span v-if="!v$.companyProfile.categoryInEnglish.maxLength"> {{ $t('CompanyProfile.NameMaximum') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                    </div><!--end form-group-->

                                    <div class="col-md-6">
                                        <div class="form-group row">
                                            <label  class="col-lg-4 col-form-label">{{$arabicLanguage($t('CompanyProfile.BusinessType(InArabic)'))  }} :<span class="text-danger"> *</span></label>
                                            <div class="col-lg-8" v-bind:class="{'has-danger' : v$.companyProfile.categoryInArabic.$error}">
                                                <input class="form-control "  v-model="v$.companyProfile.categoryInArabic.$model" />
                                                <span v-if="v$.companyProfile.categoryInArabic.$error" class="error text-danger">
                                                    <span v-if="!v$.companyProfile.categoryInArabic.required"> {{ $t('CompanyProfile.NameRequired') }}</span>
                                                    <span v-if="!v$.companyProfile.categoryInArabic.maxLength"> {{ $t('CompanyProfile.NameMaximum') }}</span>
                                                </span>
                                            </div>

                                        </div><!--end form-group-->
                                    </div><!--end col-->
                                </div><!--end row-->
                                <div class="row">
                                    <div class="col-lg-12 card-header mb-4">
                                        <h4 class="page-title">{{ $t('CompanyProfile.CompanyProfile') }}</h4>
                                    </div>
                                </div><!--end row-->
                                  <div class="row">
                                    <div class="col-md-9">
                                        <div>
                                            <div class="form-group row">
                                                <div class="col-md-6">
                                                    <div class="form-group row" v-if="english=='true'">
                                                        <label class="col-lg-12 col-form-label">{{$englishLanguage($t('CompanyProfile.CompanyName(InEnglish)'))  }}  :<span class="text-danger"> *</span></label>
                                                        <div class="col-lg-12" v-bind:class="{'has-danger' : v$.companyProfile.companyNameEnglish.$error}">
                                                            <input class="form-control "  v-model="v$.companyProfile.companyNameEnglish.$model" />
                                                            <span v-if="v$.companyProfile.companyNameEnglish.$error" class="error text-danger">
                                                                <span v-if="!v$.companyProfile.companyNameEnglish.required"> {{ $t('CompanyProfile.NameRequired') }}</span>
                                                                <span v-if="!v$.companyProfile.companyNameEnglish.maxLength"> {{ $t('CompanyProfile.NameMaximum') }}</span>
                                                            </span>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-md-6" v-if="isOtherLang()">
                                                    <div class="form-group row">
                                                        <label class="col-lg-12 col-form-label">{{$arabicLanguage($t('CompanyProfile.CompanyName(InArabic)'))  }}:<span class="text-danger"> *</span></label>
                                                        <div class="col-lg-12" v-bind:class="{'has-danger' : v$.companyProfile.companyNameArabic.$error}">
                                                            <input class="form-control text-right "  v-bind:disabled="isDisable" v-model="v$.companyProfile.companyNameArabic.$model" />
                                                            <span v-if="v$.companyProfile.companyNameArabic.$error" class="error text-danger">
                                                                <span v-if="!v$.companyProfile.companyNameArabic.required"> {{ $t('CompanyProfile.NameRequired') }}</span>
                                                                <span v-if="!v$.companyProfile.companyNameArabic.maxLength"> {{ $t('CompanyProfile.NameMaximum') }}</span>
                                                            </span>
                                                        </div>

                                                    </div><!--end form-group-->
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-6">
                                                    <div class="form-group row">
                                                        <label class="col-lg-12 col-form-label">{{ $t('CompanyProfile.CommercialRegistrationNo') }} :<span class="text-danger"> *</span></label>
                                                        <div class="col-lg-12" v-bind:class="{'has-danger' : v$.companyProfile.companyRegNo.$error}">
                                                            <input class="form-control " disabled v-model="v$.companyProfile.companyRegNo.$model" />
                                                            <span v-if="v$.companyProfile.companyRegNo.$error" class="error text-danger">
                                                                <span v-if="!v$.companyProfile.companyRegNo.required">{{ $t('CompanyProfile.RegistrationRequired') }} </span>
                                                                <span v-if="!v$.companyProfile.companyRegNo.maxLength"> {{ $t('CompanyProfile.RegistrationLength') }}</span>
                                                            </span>
                                                        </div>
                                                    </div><!--end form-group-->
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group row">
                                                        <label for="companyProfile.TaxRegisterNo" class="col-lg-12 col-form-label">{{ $t('CompanyProfile.VATNo') }} : <span class="text-danger"> *</span></label>
                                                        <div class="col-lg-12" v-bind:class="{'has-danger' : v$.companyProfile.vatRegistrationNo.$error}">
                                                            <input class="form-control " disabled v-model="v$.companyProfile.vatRegistrationNo.$model" />
                                                            <span v-if="v$.companyProfile.vatRegistrationNo.$error" class="error text-danger">
                                                                <span v-if="!v$.companyProfile.vatRegistrationNo.required">{{ $t('CompanyProfile.VatNoRequired') }}</span>
                                                                <span v-if="!v$.companyProfile.vatRegistrationNo.maxLength">{{ $t('CompanyProfile.VatMaximum') }}</span>
                                                            </span>
                                                        </div>
                                                    </div><!--end form-group-->
                                                </div><!--end col-->
                                            </div>
                                        </div><!--end form-group-->
                                    </div><!--end col-->
                                    <div class="col-md-3">
                                        <div class="form-group row" >
                                              <div class="col-sm-12 mt-2  card" :key="renderedImage" style="height: 170px;background:transparent;border:none;">
                                            <AddProductImage v-on:picPath="getImage" v-bind:path="image" />
                                        </div>
                                        </div><!--end form-group-->
                                    </div><!--end col-->
                                </div><!--end row-->
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group row">
                                            <label class="col-lg-4 col-form-label">{{ $t('CompanyProfile.contactNo')}}:</label>
                                            <div class="col-lg-8">
                                                <input class="form-control"  v-model="companyProfile.phoneNo" />
                                            </div>
                                        </div><!--end form-group-->
                                    </div><!--end col-->
                                    <div class="col-md-6">
                                        <div class="form-group row">
                                            <label class="col-lg-4 col-form-label">{{ $t('CompanyProfile.Email')}}:</label>
                                            <div class="col-lg-8">
                                                <input class="form-control"  v-model="companyProfile.companyEmail" />
                                            </div>

                                        </div><!--end form-group-->
                                    </div><!--end col-->
                                </div><!--end row-->
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group row" v-if="english=='true'">
                                            <label class="col-lg-4 col-form-label" for="companyProfile.address">{{ $t('CompanyProfile.AddressEn') }}  : <span class="text-danger"> *</span></label>
                                            <div class="col-lg-8" v-bind:class="{'has-danger' : v$.companyProfile.addressInEnglish.$error}">
                                                <textarea rows="3"  class="form-control" v-model="v$.companyProfile.addressInEnglish.$model" />
                                                <span v-if="v$.companyProfile.addressInEnglish.$error" class="error text-danger">
                                                    <span v-if="!v$.companyProfile.addressInEnglish.required">{{ $t('CompanyProfile.AddressRequired') }}</span>
                                                    <span v-if="!v$.companyProfile.addressInEnglish.maxLength">{{ $t('CompanyProfile.AddressMaximum') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                        <div class="col-md-6" v-if="isOtherLang()">
                                            <div class="form-group row">
                                                <label class="col-lg-4 col-form-label" for="companyProfile.address">{{ $t('CompanyProfile.AddressAr') }} : <span class="text-danger"> *</span></label>
                                                <div class="col-lg-8" v-bind:class="{'has-danger' : v$.companyProfile.addressInArabic.$error}">
                                                    <textarea rows="3" class="form-control text-right"  v-model="v$.companyProfile.addressInArabic.$model" />
                                                    <span v-if="v$.companyProfile.addressInArabic.$error" class="error text-danger">
                                                        <span v-if="!v$.companyProfile.addressInArabic.required">{{ $t('CompanyProfile.AddressRequired') }}</span>
                                                        <span v-if="!v$.companyProfile.addressInArabic.maxLength">{{ $t('CompanyProfile.AddressMaximum') }}</span>
                                                    </span>
                                                </div>

                                            </div><!--end form-group-->
                                        </div><!--end col-->
                                    </div><!--end row-->
                                    <!-- <div class="row">
                                        <div class="col-sm-12 mt-2  card" :key="renderedImage" style="height: 200px;padding:15px">
                                            <AddProductImage v-on:picPath="getImage" v-bind:path="image" />
                                        </div>
                                    </div> -->
                              
                            </fieldset><!--end fieldset-->
                        </div>
                    </form>
                </div>
                <div class="card-footer">
                    <div class="row">
                        <div class=" col-md-12">
                            <div class="button-items">
                                <button class="btn btn-primary mr-2" v-on:click="Attachment()"><span v-if="AttachmentLength>0">({{AttachmentLength}})</span> {{ $t('AddSaleOrder.Attachment') }}</button>
                                <button class="btn btn-outline-primary   mr-2" v-on:click="SaveCompanyAccountSetup()"> <i class="far fa-save "></i>  {{ $t('CompanyProfile.Update') }}</button>
                                <button class="btn  btn-danger   mr-2" v-on:click="BackToList()">{{ $t('CompanyProfile.btnClear') }}</button>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <bulk-attachment :attachmentList="companyProfile.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
    </div>
    <div v-else> <acessdenied></acessdenied></div>
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
                    companyEmail: '',
                    phoneNo: '',
                    attachmentList: []
                },
                companyId: '',
                image: '',
                renderedImage: 0,
                isDisable: false,
                isDelete: false,
                show: false,
                arabic: '',
                english: ''
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
        computed: {
            AttachmentLength() {
                return this.companyProfile.attachmentList.length;
            }
        },
        methods: {
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function (attachment) {
                this.companyProfile.attachmentList = attachment;
                this.show = false;
            },

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
                this.$router.push('/StartScreen')


            },
            SaveCompanyAccountSetup: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.post('/Company/SaveLocation', this.companyProfile, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

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
                        });
                        root.$router.push('/StartScreen')

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
                        root.companyProfile.companyEmail = response.data.companyEmail;
                        root.companyProfile.phoneNo = response.data.phoneNo;
                        root.companyProfile.attachmentList = response.data.attachmentList;



                        if (response.data.logoPath != null) {
                            root.getBase64Image(response.data.logoPath);
                        }


                    }
                });
            }
        },
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');

            this.isDisable = true;
            this.companyId = localStorage.getItem('CompanyID');
            this.GetCompanyDetails(this.companyId);

        }
    })

</script>