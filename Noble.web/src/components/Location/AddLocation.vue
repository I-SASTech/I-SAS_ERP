<template>
    <div class="col-md-12 ">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">{{ $t('AddLocation.AddLocation') }}</h4>

                        </div>
                        <div class="col-auto align-self-center">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-body">

                <div class="">
                    <div class="row ">
                        <div class="col-sm-4">
                            <label>{{ $t('AddLocation.BusinessName') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.addLocation.nameInEnglish.$error}">
                                <input class="form-control " v-model="v$.addLocation.nameInEnglish.$model" />
                                <span v-if="v$.addLocation.nameInEnglish.$error" class="error text-danger">
                                    <span v-if="!v$.addLocation.nameInEnglish.required"> {{ $t('AddLocation.NameRequired') }}</span>
                                    <span v-if="!v$.addLocation.nameInEnglish.maxLength"> {{ $t('AddLocation.NameMaximum') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label>{{ $t('AddLocation.CommercialRegistrationNo') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.addLocation.companyRegNo.$error}">
                                <input class="form-control " v-model="v$.addLocation.companyRegNo.$model" />
                                <span v-if="v$.addLocation.companyRegNo.$error" class="error text-danger">
                                    <span v-if="!v$.addLocation.companyRegNo.maxLength"> {{ $t('AddLocation.ComercialMaximum') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label for="addLocation.TaxRegisterNo">{{ $t('AddLocation.VATNo') }}   <span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.addLocation.vatRegistrationNo.$error}">
                                <input class="form-control " v-model="v$.addLocation.vatRegistrationNo.$model" />
                                <span v-if="v$.addLocation.vatRegistrationNo.$error" class="error text-danger">
                                    <span v-if="!v$.addLocation.vatRegistrationNo.required">{{ $t('AddLocation.VatNoRequired') }}</span>
                                    <span v-if="!v$.addLocation.vatRegistrationNo.maxLength">{{ $t('AddLocation.VatMaximum') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label for="addLocation.email">{{ $t('AddLocation.LocationEmail') }} : <span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.addLocation.email.$error,'has-blue':(v$.addLocation.email.required && v$.addLocation.email.email)}">
                                <input class="form-control " v-model="v$.addLocation.email.$model" />
                                <span v-if="v$.addLocation.email.$error" class="error text-danger">
                                    <span v-if="!v$.addLocation.email.required">{{ $t('AddLocation.EmailRequired') }} </span>
                                    <span v-if="!v$.addLocation.email.email">{{ $t('AddLocation.EmailInvalid') }} </span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label>{{ $t('AddLocation.Website') }}</label>
                            <div>
                                <input class="form-control " v-model="addLocation.website" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label>{{ $t('AddLocation.PhoneNumber') }}: <span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.addLocation.phoneNumber.$error}">
                                <input class="form-control " v-model="v$.addLocation.phoneNumber.$model" type="number" />
                                <span v-if="v$.addLocation.phoneNumber.$error" class="error text-danger">
                                    <span v-if="!v$.addLocation.phoneNumber.required">{{ $t('AddLocation.PhoneRequired') }} </span>
                                    <span v-if="!v$.addLocation.phoneNumber.maxLength">{{ $t('AddLocation.PhoneMaximum') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <label>{{ $t('AddLocation.LandlineNumber') }} : </label>
                            <div v-bind:class="{'has-danger' : v$.addLocation.landLine.$error}">
                                <input class="form-control " type="number" v-model="v$.addLocation.landLine.$model" />
                                <span v-if="v$.addLocation.landLine.$error" class="error text-danger">
                                    <span v-if="!v$.addLocation.landLine.maxLength">{{ $t('AddLocation.LandLineMaximum') }}</span>
                                </span>
                            </div>
                        </div>
                        <!--<div class="col-sm-4">
                            <label for="addLocation.country">{{ $t('AddLocation.Country') }}: </label>
                            <div v-bind:class="{'has-danger' : v$.addLocation.countryInEnglish.$error}">
                                <country-select class="form-control" v-model="addLocation.countryInEnglish" :country="addLocation.countryInEnglish" topCountry="SA" />
                                <span v-if="v$.addLocation.countryInEnglish.$error" class="error text-danger">
                                    <span v-if="!v$.addLocation.countryInEnglish.maxLength">{{ $t('AddLocation.CountryMaximum') }}</span>
                                </span>
                            </div>
                        </div>-->
                        <div class="col-sm-4">
                            <label for="addLocation.city">{{ $t('AddLocation.City') }}: </label>
                            <div v-bind:class="{'has-danger' : v$.addLocation.cityInEnglish.$error}">
                                <input class="form-control " v-model="v$.addLocation.cityInEnglish.$model" />
                                <span v-if="v$.addLocation.cityInEnglish.$error" class="error text-danger">
                                    <span v-if="!v$.addLocation.cityInEnglish.maxLength">{{ $t('AddLocation.CityMaximum') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-8">
                            <label for="addLocation.address">{{ $t('AddLocation.Address') }}: <span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.addLocation.addressInEnglish.$error}">
                                <textarea class="form-control" v-model="v$.addLocation.addressInEnglish.$model" />
                                <span v-if="v$.addLocation.addressInEnglish.$error" class="error text-danger">
                                    <span v-if="!v$.addLocation.addressInEnglish.required">{{ $t('AddLocation.AddressRequired') }}</span>
                                    <span v-if="!v$.addLocation.addressInEnglish.maxLength">{{ $t('AddLocation.AddressMaximum') }}</span>
                                </span>
                            </div>
                        </div>
                        <div v-if="$route.query.data!=undefined" class="card-footer col-md-12 text-right">
                            <a href="javascript:void(0)" class="btn btn-outline-primary  mr-2" v-on:click="SaveaddLocation"><i class="fa fa-save"></i> {{ $t('AddLocation.Update') }}</a>
                            <a href="javascript:void(0)" class="btn btn-outline-reset " v-on:click="GoToCompany"><i class="fa fa-refresh"></i>  {{ $t('AddLocation.Cancel') }}</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-body  ">

                <h5 class="card-title pb-2 "> {{ $t('AddLocation.AdminInfo') }}</h5>

                <div class="row">
                    <div class="col-sm-3">
                        <label for="addLocation.email">{{ $t('AddLocation.Email') }}:<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.addLocation.userEmail.$error,'has-blue':(v$.addLocation.userEmail.required && v$.addLocation.userEmail.email)}">
                            <input class="form-control " type="text" v-model="v$.addLocation.userEmail.$model" />
                            <span v-if="v$.addLocation.userEmail.$error" class="error text-danger">
                                <span v-if="!v$.addLocation.userEmail.required">{{ $t('AddLocation.EmailRequired') }} </span>
                                <span v-if="!v$.addLocation.userEmail.email">{{ $t('AddLocation.EmailInvalid') }}</span>
                            </span>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <label>{{ $t('AddLocation.RegisterUser_UserName') }}:<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.addLocation.firstName.$error}">
                            <input class="form-control " autofocus="autofocus" v-model="v$.addLocation.firstName.$model" />
                            <span v-if="v$.addLocation.firstName.$error" class="error text-danger">
                                <span v-if="!v$.addLocation.firstName.required">{{ $t('AddLocation.RegisterUser_Error_Required_DisplayName') }}  </span>
                                <span v-if="!v$.addLocation.firstName.maxLength">{{ $t('AddLocation.RegisterUser_Error_Length_DisplayName') }}</span>
                            </span>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <label for="addLocation.password">{{ $t('AddLocation.RegisterUser_Password') }} :<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.addLocation.password.$error,'has-blue':(v$.addLocation.password.required && v$.addLocation.password.strongPassword)}">
                            <input class="form-control " type="password" v-model="v$.addLocation.password.$model" />
                            <span v-if="v$.addLocation.password.$error" class="error text-danger">
                                <span v-if="!v$.addLocation.password.required">{{ $t('AddLocation.RegisterUser_Error_Required_Password') }} </span>
                                <span v-if="!v$.addLocation.password.strongPassword">{{ $t('AddLocation.RegisterUser_Error_Format_Password') }}</span>
                            </span>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <label for="addLocation.confirmPassword">{{ $t('AddLocation.RegisterUser_ConfirmPassword') }}:<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.addLocation.confirmPassword.$error,'has-blue':(v$.addLocation.confirmPassword.required && v$.addLocation.confirmPassword.sameAsPassword)}">
                            <input class="form-control " type="password" v-model="v$.addLocation.confirmPassword.$model" />
                            <span v-if="v$.addLocation.confirmPassword.$error" class="error text-danger">
                                <span v-if="!v$.addLocation.confirmPassword.required">{{ $t('AddLocation.RegisterUser_Error_Required_ConfirmPassword') }}  </span>
                                <span v-if="!v$.addLocation.confirmPassword.sameAsPassword">{{ $t('AddLocation.RegisterUser_Error_SameAs_ConfirmPassword') }}</span>
                            </span>
                        </div>
                    </div>
                    <div v-if="!loading" class=" col-md-12 mt-2 text-right">
                        <a href="javascript:void(0)" class="btn btn-outline-primary  me-2" v-on:click="SaveaddLocation" v-bind:disabled="v$.addLocation.$invalid"><i class="fa fa-save"></i> {{ $t('AddLocation.btnSave') }}</a>
                        <a href="javascript:void(0)" class="btn btn-outline-danger " v-on:click="BackToList"> {{ $t('AddLocation.btnClear') }}</a>
                    </div>
                    <div class="card-footer col-md-3 " v-else>
                        <loading v-model:active="loading" :can-cancel="true" :on-cancel="onCancel" :is-full-page="true"></loading>
                    </div>
                </div>
                <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>


            </div>


          
        </div>
    </div>

</template>
<script>
    import useVuelidate from '@vuelidate/core'
    import { required, maxLength, sameAs, email } from '@vuelidate/validators';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    //import Vue from 'vue';
    //import vueCountryRegionSelect from 'vue3-country-region-select'
    //Vue.use(vueCountryRegionSelect);
    export default ({
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                active: 'addLocation',
                loading: false,
                country: '',
                addLocation: {
                    id: '',
                    nameInEnglish: '',
                    nameInArabic: '',
                    vatRegistrationNo: '',
                    companyRegNo: '',
                    cityInEnglish: '',
                    cityInArabic: '',
                    countryInEnglish: '',
                    countryInArabic: '',
                    phoneNumber: '',
                    website: '',
                    userEmail: '',
                    email: '',
                    addressInEnglish: '',
                    addressInArabic: '',
                    landLine: '',
                    firstName: '',
                    password: '',
                    confirmPassword: '',
                    clientParentId: '00000000-0000-0000-0000-000000000000',
                    businessParentId: '00000000-0000-0000-0000-000000000000',
                }
            }
        },
        validations() {
            return {
                addLocation:
                {
                    nameInEnglish:
                    {
                        required,
                        maxLength: maxLength(90)
                    },
                    vatRegistrationNo:
                    {
                        required,
                        maxLength: maxLength(50)
                    },
                    companyRegNo:
                    {
                        required,
                        maxLength: maxLength(50)
                    },
                    cityInEnglish:
                    {

                        maxLength: maxLength(70)
                    },
                    //countryInEnglish:
                    //{

                    //    maxLength: maxLength(60)
                    //},
                    phoneNumber:
                    {
                        required,
                        maxLength: maxLength(20)
                    },
                    addressInEnglish:
                    {
                        required,
                        maxLength: maxLength(140)
                    },
                    firstName:
                    {
                        required,
                        maxLength: maxLength(30)
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
                    email:
                    {
                        required,
                        email
                    },
                    userEmail:
                    {
                        required,
                        email
                    },
                    landLine:
                    {

                        maxLength: maxLength(15)
                    }
                }
                }
        },
        methods: {
            makeActive: function (tab) {
                this.active = tab;
            },
            BackToList: function () {
                if (this.$route.query.clientParentId != undefined) {
                    this.$router.push('/clientManagement');
                } else {
                    this.$router.push('/clientManagement');
                }
            },
            SaveaddLocation: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (this.$route.query.clientParentId != undefined) {
                    this.addLocation.clientParentId = this.$route.query.clientParentId;
                    this.addLocation.businessParentId = this.$route.query.busId;
                }
                this.$https
                    .post('/Company/SaveLocation', this.addLocation, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.loading = false
                        root.info = response.data.bpi
                        root.$swal.fire({
                            icon: 'success',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                            showConfirmButton: false,
                            timer: 1000,
                            timerProgressBar: true,

                        });
                        if (root.$route.query.clientParentId != undefined) {
                            root.$router.push('/clientManagement');
                        } else {
                            root.$router.push('/clientManagement');
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error,
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,

                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
            GetLocationData: function () {
                var root = this;
                var url = '/Company/LocationDetails';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.addLocation.nameInEnglish = '';
                        root.addLocation.companyRegNo = response.data.companyRegNo;
                        root.addLocation.vatRegistrationNo = response.data.vatRegistrationNo;
                        root.addLocation.email = response.data.companyEmail;
                        root.addLocation.website = response.data.website;
                        root.addLocation.phoneNumber = response.data.phoneNo;
                        root.addLocation.landLine = response.data.landLine;
                        root.addLocation.countryInEnglish = response.data.countryEnglish;
                        root.addLocation.cityInEnglish = response.data.cityEnglish;
                        root.addLocation.addressInEnglish = response.data.addressEnglish;
                    }
                });
            },
        },
        mounted: function () {
            this.GetLocationData();
            this.country = this.addLocation.countryInEnglish;
        }
    })
</script>