<template>
    <div class="col-md-8 ml-auto mr-auto">
        <div class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="card-header" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                            <h5 class="text-right">معلومات الشركة الإضافية</h5>
                        </div>
                        <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                            <div class="row text-right" :key="render">
                                <div class="col-sm-6">
                                    <label for="company.nameArabic">اسم الشركة</label>
                                    <div v-bind:class="{'has-danger' : v$.company.nameArabic.$error}">
                                        <input class="form-control text-right" v-model="v$.company.nameArabic.$model" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.company.nameArabic.$error" class="error text-danger">
                                            <span v-if="!v$.company.nameArabic.required">اسم الشركة مطلوب</span>
                                            <span v-if="!v$.company.nameArabic.maxLength">اسم الشركة له الحد الأقصى للطول 50</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label for="company.commercialRegNo">رقم السجل التجاري</label>
                                    <div v-bind:class="{'has-danger' : v$.company.commercialRegNo.$error}">
                                        <input class="form-control text-right" type="number" v-model="v$.company.commercialRegNo.$model" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.company.commercialRegNo.$error" class="error text-danger">
                                            <span v-if="!v$.company.commercialRegNo.required">رقم السجل التجاري مطلوب</span>
                                            <span v-if="!v$.company.commercialRegNo.maxLength">رقم السجل التجاري الحد الأقصى للطول 20</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label for="company.vatRegistrationNo">VAT / NTN / TAX No</label>
                                    <div v-bind:class="{'has-danger' : v$.company.vatRegistrationNo.$error}">
                                        <input class="form-control text-right" type="number" v-model="v$.company.vatRegistrationNo.$model" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.company.vatRegistrationNo.$error" class="error text-danger">
                                            <span v-if="!v$.company.vatRegistrationNo.required">رقم تسجيل ضريبة القيمة المضافة مطلوب</span>
                                            <span v-if="!v$.company.vatRegistrationNo.maxLength">
                                                تسجيل قيمة الضريبة المضافة
                                                الرقم له أقصى طول 20
                                            </span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label for="company.city">مدينة</label>
                                    <div v-bind:class="{'has-danger' : v$.company.cityArabic.$error}">
                                        <input class="form-control text-right" v-model="v$.company.cityArabic.$model" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.company.cityArabic.$error" class="error text-danger">
                                            <span v-if="!v$.company.cityArabic.required">المدينة مطلوبة</span>
                                            <span v-if="!v$.company.cityArabic.maxLength">الحد الأقصى للطول في المدينة 30</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label for="company.country">دولة</label>
                                    <div v-bind:class="{'has-danger' : v$.company.countryArabic.$error}">
                                        <input class="form-control text-right" v-model="v$.company.countryArabic.$model" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.company.countryArabic.$error" class="error text-danger">
                                            <span v-if="!v$.company.countryArabic.required">الدولة مطلوبة</span>
                                            <span v-if="!v$.company.countryArabic.maxLength">البلد لديه الحد الأقصى للطول 30</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label for="company.Mobile">رقم الاتصال (جوال)</label>
                                    <div v-bind:class="{'has-danger' : v$.company.mobile.$error}">
                                        <input class="form-control  text-right" type="number" v-model="v$.company.mobile.$model" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.company.mobile.$error" class="error text-danger">
                                            <span v-if="!v$.company.mobile.required">رقم الهاتف المحمول مطلوب</span>
                                            <span v-if="!v$.company.mobile.maxLength">رقم الجوال لديه أقصى طول 15</span>

                                        </span>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label for="company.Phone">رقم الاتصال (هاتف أرضي)</label>
                                    <div v-bind:class="{'has-danger' : v$.company.phoneNo.$error}">
                                        <input class="form-control text-right" type="number" v-model="v$.company.phoneNo.$model" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.company.phoneNo.$error" class="error text-danger">
                                            <span v-if="!v$.company.phoneNo.required">رقم الهاتف الأرضي مطلوب</span>
                                            <span v-if="!v$.company.phoneNo.maxLength">رقم الهاتف الأرضي لديه الحد الأقصى للطول 15</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label for="company.email">البريد الإلكتروني</label>
                                    <div v-bind:class="{'has-danger' : v$.company.email.$error}">
                                        <input class="form-control text-right" type="email" v-model="v$.company.email.$model" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.company.email.$error" class="error text-danger">
                                            <span v-if="!v$.company.email.required">البريد الالكتروني مطلوب</span>
                                            <span v-if="!v$.company.email.email">البريد الإلكتروني غير صالح</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label for="company.website">موقع الكتروني</label>
                                    <div v-bind:class="{'has-danger' : v$.company.website.$error}">
                                        <input class="form-control text-right" v-model="v$.company.website.$model" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.company.website.$error" class="error text-danger">
                                            <span v-if="!v$.company.website.required">الموقع مطلوب</span>
                                            <span v-if="!v$.company.website.maxLength">الحد الأقصى لطول موقع الويب 30</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label for="company.addressArabic">عنوان</label>
                                    <div v-bind:class="{'has-danger' : v$.company.addressArabic.$error}">
                                        <textarea class="form-control text-right" v-model="v$.company.addressArabic.$model" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                                        <span v-if="v$.company.addressArabic.$error" class="error text-danger">
                                            <span v-if="!v$.company.addressArabic.required">العنوان مطلوب</span>
                                            <span v-if="!v$.company.addressArabic.maxLength">الحد الأقصى لطول العنوان 40</span>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-footer text-right">
                <button class="btn btn-primary  float-right mr-2" v-if="company.id=='00000000-0000-0000-0000-000000000000'" :disabled="v$.company.$invalid" v-on:click="SaveAdditionalCompanyInformation()">يحفظ</button>
                <button class="btn btn-primary  float-right mr-2" v-else disabled v-on:click="SaveAdditionalCompanyInformation()">يحفظ</button>
                <button class="btn btn-danger  float-right mr-2" v-on:click="BackToList()">يلغي</button>
            </div>
        </div>
    </div>
</template>
<script>
    import { required, maxLength, email } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default ({
          setup() {
            return { v$: useVuelidate() }
        },

        data: function () {
            return {
                company: {
                    id: '00000000-0000-0000-0000-000000000000',
                    nameArabic: '',
                    commercialRegNo: '',
                    cityArabic: '',
                    countryArabic: '',
                    phoneNo: '',
                    mobile: '',
                    addressArabic: '',
                    email: '',
                    website: '',
                    vatRegistrationNo: ''
                },
                render: 0
            }
        },
        validations() {
           return{
             company:
            {
                nameArabic:
                {
                    required,
                    maxLength: maxLength(50)
                },
                commercialRegNo:
                {
                    required,
                    maxLength: maxLength(20)
                },
                vatRegistrationNo:
                {
                    required,
                    maxLength: maxLength(20)

                },
                cityArabic:
                {
                    required,
                    maxLength: maxLength(30)
                },
                countryArabic:
                {
                    required,
                    maxlength: maxLength(30)
                },
                mobile:
                {
                    required,
                    maxLength: maxLength(15)
                },
                phoneNo:
                {
                    required,
                    maxLength: maxLength(20)
                },
                email:
                {
                    required,
                    email
                },
                website:
                {
                    required,
                    maxLength: maxLength(30)
                },
                addressArabic:
                {
                    required,
                    maxLength: maxLength(30)
                }
            },
           }
        },
        methods: {
            SaveAdditionalCompanyInformation: function () {
                var root = this;
                var url = '/Company/SaveCompanyInformation';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .post(url, root.company, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data != null) {
                            root.$swal({
                                title: "Success!",
                                text: "Saved Successfully.",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                                confirmButtonClass: "btn btn-success",
                                buttonsStyling: false
                            });
                            root.$router.push('/CompanyAdditionalInfo').catch(() => { });
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.errored = true
                    })
                    .finally(() => root.loading = false)
            },
            BackToList: function () {
                this.$router.push('/CompanyAdditionalInfo').catch(() => { });
            },
            GetCompanyData: function () {
                var root = this;
                var url = '/Company/GetCompanyInformation';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.companyInformation.length > 0) {
                        root.company = response.data.companyInformation[0];
                    }
                    else {
                        root.company = {
                            id: '00000000-0000-0000-0000-000000000000',
                            nameArabic: '',
                            commercialRegNo: '',
                            cityArabic: '',
                            countryArabic: '',
                            phoneNo: '',
                            mobile: '',
                            addressArabic: '',
                            email: '',
                            website: '',
                            vatRegistrationNo: ''
                        }
                    }
                });
            }
        },
        mounted: function () {
            this.GetCompanyData();
            this.render++;
        }
    })
</script>