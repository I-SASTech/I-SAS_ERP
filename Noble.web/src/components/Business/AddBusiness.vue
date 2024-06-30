<template>
    <div>
        <div class="col-lg-12 ">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AddBusiness.Heading') }}</h4>

                            </div>
                            <div class="col-auto align-self-center">

                                <!--<a v-on:click="syncSetup()" href="javascript:void(0);"
                           class="btn btn-sm btn-outline-primary mx-1">
                            <i class="align-self-center icon-xs ti-plus"></i>
                            Sync Setup
                        </a>-->


                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-12">
                <div class="row">

                    <div class="col-md-6">
                        <div class="card ">
                            <div class="card-body">
                                <div class="pb-2">
                                    <h5 class="card-title   ">{{ $t('AddBusiness.EnglishInfo') }}</h5>
                                </div>
                                <div class="row  " v-if="($i18n.locale == 'en' ||isLeftToRight())">

                                    <div class="col-md-6 " v-bind:class="{'has-danger' : v$.business.nameInEnglish.$error}">
                                        <label class="text  font-weight-bolder ">{{ $t('AddBusiness.NameEn') }} :<span class="text-danger"> *</span></label>
                                        <input class="form-control" v-model="v$.business.nameInEnglish.$model" type="text" />
                                        <span v-if="v$.business.nameInEnglish.$error" class="error text-danger">
                                            <span v-if="!v$.business.nameInEnglish.required">{{ $t('AddBusiness.Name') }}</span>
                                            <span v-if="!v$.business.nameInEnglish.maxLength">{{ $t('AddBusiness.NameLength') }}</span>
                                        </span>
                                    </div>
                                    <div class="col-md-6 " v-bind:class="{'has-danger' : v$.business.categoryInEnglish.$error}">
                                        <label class="text  font-weight-bolder">{{ $t('AddBusiness.BusinessTypeEng') }} :<span class="text-danger"> *</span></label>
                                        <input class="form-control" v-model="v$.business.categoryInEnglish.$model" type="text" />
                                        <span v-if="v$.business.categoryInEnglish.$error" class="error text-danger">
                                            <span v-if="!v$.business.categoryInEnglish.required">{{ $t('AddBusiness.Category') }}</span>
                                            <span v-if="!v$.business.categoryInEnglish.maxLength">{{ $t('AddBusiness.CategoryLength') }}</span>
                                        </span>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <label for="business.address">Address  (English): <span class="text-danger"> *</span></label>
                                        <textarea class="form-control" v-model="business.addressInEnglish" />
                                    </div>
                                </div>
                                <div class="row  " v-else>

                                    <div class="col-md-6 " v-bind:class="{'has-danger' : v$.business.categoryInEnglish.$error}">
                                        <label class="text  font-weight-bolder">{{ $t('AddBusiness.BusinessTypeEng') }} :<span class="text-danger"> *</span></label>
                                        <input class="form-control" v-model="v$.business.categoryInEnglish.$model" type="text" />
                                        <span v-if="v$.business.categoryInEnglish.$error" class="error text-danger">
                                            <span v-if="!v$.business.categoryInEnglish.required">{{ $t('AddBusiness.Category') }}</span>
                                            <span v-if="!v$.business.categoryInEnglish.maxLength">{{ $t('AddBusiness.CategoryLength') }}</span>
                                        </span>
                                    </div>
                                    <div class="col-md-6 " v-bind:class="{'has-danger' : v$.business.nameInEnglish.$error}">
                                        <label class="text  font-weight-bolder ">{{ $t('AddBusiness.NameEn') }} :<span class="text-danger"> *</span></label>
                                        <input class="form-control" v-model="v$.business.nameInEnglish.$model" type="text" />
                                        <span v-if="v$.business.nameInEnglish.$error" class="error text-danger">
                                            <span v-if="!v$.business.nameInEnglish.required">{{ $t('AddBusiness.Name') }}</span>
                                            <span v-if="!v$.business.nameInEnglish.maxLength">{{ $t('AddBusiness.NameLength') }}</span>
                                        </span>
                                    </div>

                                </div>
                            </div>
                          



                        </div>
                    </div>

                    <div class="col-md-6   ">
                        <div class="card ">
                            <div class="card-body">
                                <div class="pb-2">
                                    <h5 class="card-title  ">{{ $t('AddBusiness.ArabicInfo') }}</h5>
                                </div>
                                <div class="row" v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                    <div class="col-md-6 " v-bind:class="{'has-danger' : v$.business.nameInArabic.$error}">
                                        <label class="text  font-weight-bolder ">{{ $t('AddBusiness.NameArabic') }} :<span class="text-danger"> *</span></label>
                                        <input class="form-control text-right" v-model="v$.business.nameInArabic.$model" type="text" v-bind:class="$i18n.locale == 'ar' ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.business.nameInArabic.$error" class="error text-danger">
                                            <span v-if="!v$.business.nameInArabic.required">{{ $t('AddBusiness.Name') }}</span>
                                            <span v-if="!v$.business.nameInArabic.maxLength">{{ $t('AddBusiness.NameLength') }}</span>
                                        </span>
                                    </div>
                                    <div class="col-md-6 " v-bind:class="{'has-danger' : v$.business.categoryInArabic.$error}">
                                        <label class="text  font-weight-bolder">{{ $t('AddBusiness.BusinessTypeArabic') }} :<span class="text-danger"> *</span></label>
                                        <input class="form-control text-right" v-model="v$.business.categoryInArabic.$model" type="text" v-bind:class="$i18n.locale == 'ar' ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.business.categoryInArabic.$error" class="error text-danger">
                                            <span v-if="!v$.business.categoryInArabic.required">{{ $t('AddBusiness.Category') }}</span>
                                            <span v-if="!v$.business.categoryInArabic.maxLength">{{ $t('AddBusiness.CategoryLength') }}</span>
                                        </span>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <label for="business.address">Address (Arabic): <span class="text-danger"> *</span></label>
                                        <textarea class="form-control" v-model="business.addressInArabic" />
                                    </div>
                                </div>
                                <div class="row" v-else>
                                    <div class="col-md-6 " v-bind:class="{'has-danger' : v$.business.categoryInArabic.$error}">
                                        <label class="text  font-weight-bolder">{{ $t('AddBusiness.BusinessTypeArabic') }} :<span class="text-danger"> *</span></label>
                                        <input class="form-control text-right" v-model="v$.business.categoryInArabic.$model" type="text" v-bind:class="$i18n.locale == 'ar' ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.business.categoryInArabic.$error" class="error text-danger">
                                            <span v-if="!v$.business.categoryInArabic.required">{{ $t('AddBusiness.Category') }}</span>
                                            <span v-if="!v$.business.categoryInArabic.maxLength">{{ $t('AddBusiness.CategoryLength') }}</span>
                                        </span>
                                    </div>
                                    <div class="col-md-6 " v-bind:class="{'has-danger' : v$.business.nameInArabic.$error}">
                                        <label class="text  font-weight-bolder ">{{ $t('AddBusiness.NameArabic') }} :<span class="text-danger"> *</span></label>
                                        <input class="form-control text-right" v-model="v$.business.nameInArabic.$model" type="text" v-bind:class="$i18n.locale == 'ar' ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.business.nameInArabic.$error" class="error text-danger">
                                            <span v-if="!v$.business.nameInArabic.required">{{ $t('AddBusiness.Name') }}</span>
                                            <span v-if="!v$.business.nameInArabic.maxLength">{{ $t('AddBusiness.NameLength') }}</span>
                                        </span>
                                    </div>

                                </div>
                            </div>
                          




                        </div>
                    </div>

                </div>



            </div>
            <div class="card ">



                <div class="card-body ">
                    <h5 class="card-title  ">{{ $t('AddBusiness.AddSuperAdmin') }}</h5>

                    <div class="row">
                        <div class="col-md-3">
                            <label for="business.email">{{ $t('AddBusiness.RegisterUser_EmailID') }}  :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.business.email.$error}">
                                <input class="form-control" type="email" v-model="v$.business.email.$model" />
                                <span v-if="v$.business.email.$error" class="error text-danger">
                                    <span v-if="!v$.business.email.required">{{ $t('AddBusiness.RegisterUser_EmailID') }}</span>
                                    <span v-if="!v$.business.email.email">{{ $t('AddBusiness.RegisterUser_Error_Format_EmailID') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label for="business.email">{{ $t('AddBusiness.RegisterUser_UserName') }}  :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.business.userId.$error}">
                                <input class="form-control" type="text" v-model="v$.business.userId.$model" />
                                <span v-if="v$.business.userId.$error" class="error text-danger">
                                    <span v-if="!v$.business.userId.required">{{ $t('AddBusiness.RegisterUser_Error_Required_DisplayName') }}</span>
                                    <span v-if="!v$.business.userId.maxLength">{{ $t('AddBusiness.RegisterUser_Error_Length_DisplayName') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label for="business.password">{{ $t('AddBusiness.RegisterUser_Password') }}  :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.business.password.$error}">
                                <input class="form-control" type="password" v-model="v$.business.password.$model" />
                                <span v-if="v$.business.password.$error" class="error text-danger">
                                    <span v-if="!v$.business.password.required">{{ $t('AddBusiness.RegisterUser_Error_Required_Password') }}</span>
                                    <span v-if="!v$.business.password.strongPassword">{{ $t('AddBusiness.RegisterUser_Error_Format_Password') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label for="business.confirmPassword">{{ $t('AddBusiness.RegisterUser_ConfirmPassword') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.business.confirmPassword.$error}">
                                <input class="form-control" type="password" v-model="v$.business.confirmPassword.$model" />
                                <span v-if="v$.business.confirmPassword.$error" class="error text-danger">
                                    <span v-if="!v$.business.confirmPassword.required">{{ $t('AddBusiness.RegisterUser_Error_Required_ConfirmPassword') }}</span>
                                    <span v-if="!v$.business.confirmPassword.sameAsPassword">{{ $t('AddBusiness.RegisterUser_Error_SameAs_ConfirmPassword') }}</span>
                                </span>
                            </div>
                        </div>

                    </div>

                    <div v-if="!loading" class=" col-md-12 mt-2 " >
                        <div v-if="($i18n.locale == 'en' ||isLeftToRight())">
                            <a href="javascript:void(0)" class="btn btn-outline-primary  me-2" v-on:click="SaveBusiness" v-bind:disabled="v$.business.$invalid"><i class="fa fa-save"></i> {{ $t('AddBusiness.btnSave') }}</a>
                            <a href="javascript:void(0)" class="btn btn-outline-danger " v-on:click="GoToBusiness"> {{ $t('AddBusiness.btnClear') }}</a>

                        </div>
                        <div v-else>
                            <a href="javascript:void(0)" class="btn btn-outline-danger  me-2" v-on:click="GoToBusiness"> {{ $t('AddBusiness.btnClear') }}</a>
                            <a href="javascript:void(0)" class="btn btn-outline-primary  " v-on:click="SaveBusiness" v-bind:disabled="v$.business.$invalid"><i class="fa fa-save"></i> {{ $t('AddBusiness.btnSave') }}</a>
                        </div>


                    </div>
                    <div class=" col-md-3 mt-2" v-else>
                        <loading v-model:active="loading" :can-cancel="true" :on-cancel="onCancel" :is-full-page="true"></loading>
                    </div>
                </div>


            </div>
        </div>

    </div>


</template>
<script>
    
    import { required, requiredIf, maxLength, email, sameAs } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default ({
        components: {
            
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                active: 'business',
                country: '',
                country1: '',
                business: {
                    id: '',
                    nameInArabic: '',
                    nameInEnglish: '',
                    categoryInArabic: '',
                    categoryInEnglish: '',
                    cityInEnglish: '',
                    cityInArabic: '',
                    countryInEnglish: '',
                    countryInArabic: '',
                    addressInEnglish: '',
                    addressInArabic: '',
                    PhoneNumberInArabic: '',
                    PhoneNumberInEnglish: '',
                    userId: '',
                    password: '',
                    confirmPassword: '',
                    email: '',
                    clientParentId: '00000000-0000-0000-0000-000000000000'

                },
                loading: false,

            }
        },

        validations() {

          return{
              business: {
                nameInArabic: {
                    required: requiredIf(function () {
                            return !this.business.nameInEnglish;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.nameInEnglish == '' || x.nameInEnglish == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(50)
                },
                nameInEnglish: {
                    maxLength: maxLength(50)
                },
                categoryInArabic: {
                    required: requiredIf(function () {
                            return !this.business.categoryInEnglish;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.categoryInEnglish == '' || x.categoryInEnglish == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(50)
                },
                categoryInEnglish: {
                    maxLength: maxLength(50)
                },

                userId: {
                    required,
                    maxLength: maxLength(30)
                },


                email:
                {
                    required,
                    email
                },
                password:
                {
                    required,
                    strongPassword(password) {
                        return /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/.test(password);
                    }
                },
                confirmPassword:
                {
                    required,
                    sameAsPassword: sameAs('password')
                },
            },

          }
        },
        methods: {

            GoToBusiness: function () {
                if (this.$route.query.data != undefined) {
                    this.$router.push('/clientManagement');
                } else {
                    this.$router.push('/clientManagement');
                }
            },
            SaveBusiness: function () {
                //eslint-disable-line

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.$route.query.data != undefined) {
                    this.business.clientParentId = this.$route.query.data;
                }
                this.$https
                    .post('/Company/SaveBusiness', this.business, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        this.info = response.data.bpi
                        this.loading = false
                        this.$swal.fire({
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1000,
                            timerProgressBar: true,
                            title: 'Saved Successfully'
                        });
                        if (this.$route.query.data != undefined) {
                            this.$router.push('/clientManagement');
                        } else {
                            this.$router.push('/clientManagement');
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        this.$swal.fire(
                            {
                                icon: 'error',
                                title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error,
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,

                            });

                        this.loading = false
                    })
                    .finally(() => this.loading = false)
            }
        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
              if (this.$route.query.data != undefined) {
                this.company = this.$route.query.data;

                this.country = this.business.countryInEnglish;
                this.country1 = this.business.countryInArabic;
            }

        },
        mounted: function () {



          
        }
    })
</script>