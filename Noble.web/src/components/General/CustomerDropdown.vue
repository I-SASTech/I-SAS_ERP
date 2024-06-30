<template>
    <div>
        <multiselect :disabled="disableValue" v-if="isMultiple" v-model="DisplayValue" :options="options" @update:modelValue="UpdateData" :multiple="true" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" v-bind:placeholder="$t('CustomerDropdown.SelectOption')" v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'arabicLanguage '">
            <template v-slot:noResult>
                <span class="btn btn-primary " v-on:click="AddCustomer()" v-if="isValid('CanAddCustomer')">{{ $t('CustomerDropdown.CreateQuickCustomer') }}</span>
                <br />
            </template>
        </multiselect>
        <multiselect :disabled="disableValue" v-else v-model="DisplayValue" :options="options" @update:modelValue="UpdateData" :multiple="false" track-by="name" ref="multiselect" :searchable="true" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" v-bind:placeholder="$t('CustomerDropdown.SelectOption')" v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'arabicLanguage '">
            <template v-slot:noResult>
                <span class="btn btn-primary " v-on:click="AddCustomer()" v-if="isValid('CanAddCustomer')">{{ $t('CustomerDropdown.CreateQuickCustomer') }}</span>
                <br />
            </template>
        </multiselect>

        <modal :show="show" v-if="show">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title m-0" id="exampleModalDefaultLabel">{{ $t('CustomerDropdown.AddCustomer') }}</h6>
                    <h6 class="text-right" style="color: white;">&nbsp;&nbsp;&nbsp;<span v-if="customer.isCashCustomer">{{cashCustomerCode}}</span><span v-else>{{contactCode}}</span></h6>
                    <div class="form-check form-check-inline " v-if="isCashCustomer" style="color: white !important;padding-left: 90px;">
                        <input v-model="customer.isCashCustomer" @change="isCashCustomerFunc()" name="contact-sub-type12" id="a49946487" class=" form-check-input" type="radio" v-bind:value="true">
                        <label class="form-check-label pl-0" for="a49946487">Walk-In</label>
                    </div>
                    <div class="form-check form-check-inline text-right" v-if="isCashCustomer" style="color: white !important;">
                        <input v-model="customer.isCashCustomer" @change="isCashCustomerFunc()" name="contact-sub-type12" id="a9ff8eb45" class=" form-check-input" type="radio" v-bind:value="false">
                        <label class="form-check-label pl-0" for="a9ff8eb45">Cash/Credit</label>
                    </div>
                    <button type="button" class="btn-close" v-on:click="close()"></button>
                </div>
                <div class="modal-body">

                    <div class="row ">
                        <div v-if="english=='true'" class="col-sm-12">
                            <label>{{$englishLanguage($t('CustomerDropdown.CustomerName(English)'))  }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.customer.englishName.$error}">
                                <input class="form-control " @update:modelValue="DisplayName()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.customer.englishName.$model" />
                                <span v-if="v$.customer.englishName.$error" class="error text-danger">
                                    <span v-if="!v$.customer.englishName.required">{{ $t('CustomerDropdown.EngValidation') }} </span>
                                    <span v-if="!v$.customer.englishName.maxLength">{{ $t('CustomerDropdown.EngMax') }} </span>
                                </span>
                            </div>
                        </div>
                        <div v-if="isOtherLang()" class="col-sm-12">
                            <label>{{$arabicLanguage($t('CustomerDropdown.CustomerName(Arabic)'))  }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.customer.arabicName.$error}">
                                <input class="form-control " @update:modelValue="DisplayName()" v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.customer.arabicName.$model" />
                                <span v-if="v$.customer.arabicName.$error" class="error text-danger">

                                    <span v-if="!v$.customer.arabicName.maxLength">{{ $t('CustomerDropdown.ArMax') }} </span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <label>{{ $t('AddCustomer.CustomerDisplayName') }} :<span class="text-danger"> *</span></label>
                            <display-name-dropdown v-model="customer.customerDisplayName" :modelValue="customer.customerDisplayName" :newCustomer="customer" :key="salutatioRender" />

                        </div>
                        <div class="col-sm-12" v-if="!customer.isCashCustomer">
                            <label>{{ $t('CustomerDropdown.RegistrationDate') }} :</label>
                            <div v-bind:class="{'has-danger' : v$.customer.registrationDate.$error}">
                                <datepicker v-model="v$.customer.registrationDate.$model"></datepicker>
                                <span v-if="v$.customer.registrationDate.$error" class="error text-danger">
                                </span>
                            </div>
                        </div>

                        <div class="col-12" v-if="!customer.isCashCustomer">
                            <label> {{ $t('AddCustomer.CustomerCategory') }}:<span class="text-danger"> *</span></label>
                            <div v-bind:class="{'has-danger' : v$.customer.customerType.$error}">

                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="v$.customer.customerType.$model" :options="['Individual', 'Establishment', 'Company','Factory','Manufacturer']" :show-labels="false" placeholder="Select Type">
                                </multiselect>
                                <multiselect v-else v-model="v$.customer.customerType.$model" :options="['فرد', 'مؤسسة', 'شركة']" :show-labels="false" v-bind:placeholder="$t('SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                </multiselect>
                                <span v-if="v$.customer.customerType.$error" class="error text-danger">

                                </span>
                            </div>
                        </div>
                        <div class="col-sm-12" v-if="!customer.isCashCustomer">
                            <label>{{ $t('CustomerDropdown.PaymentTerms') }} :<span class="text-danger"> *</span></label>

                            <multiselect v-model="customer.paymentTerms" v-if="($i18n.locale == 'en' ||isLeftToRight()) " :options="[ 'Cash', 'Credit']" :show-labels="false" placeholder="Select Type">
                            </multiselect>
                            <multiselect v-else v-model="customer.paymentTerms" :options="[ 'نقد', 'آجل']" :show-labels="false" v-bind:placeholder="$t('CustomerDropdown.SelectOption')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                            <span v-if="v$.customer.paymentTerms.$error" class="error text-danger">
                                <span v-if="!v$.customer.paymentTerms.required">{{ $t('CustomerDropdown.PaymentValidation') }} </span>
                            </span>

                        </div>
                        <div class="col-sm-12">
                            <label v-if="!customer.isCashCustomer">{{ $t('CustomerDropdown.VAT/NTN/Tax No') }} :<span class="text-danger"> *</span></label>
                            <label v-else>Customer Id :</label>
                            <div v-bind:class="{'has-danger' : v$.customer.vatNo.$error}">
                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.customer.vatNo.$model" />
                                <span v-if="v$.customer.vatNo.$error" class="error text-danger">
                                    <span v-if="!v$.customer.vatNo.required">{{ $t('CustomerDropdown.VatNoRequired') }}</span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <label>Customer Code :</label>
                            <input class="form-control " v-model="customer.customerCode" />

                        </div>

                        <div class="col-sm-12">
                            <label>Mobile :</label>
                            <input class="form-control text-left" v-model="customer.contactNo1" />
                        </div>

                        <div class="col-sm-12">
                            <label>Address :</label>
                            <div>
                                <textarea class="form-control text-left" v-model="customer.address" />

                            </div>
                        </div>
                        <!-- <div class="col-12">
                            <label> {{ $t('AddCustomer.CustomerType') }}    :</label>
                            <div>
                                <multiselect v-model="customer.category" :options="optionsb2b" :show-labels="false" placeholder="Select Type">
                                </multiselect>

                            </div>
                        </div> -->

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveCustomer" v-bind:disabled="v$.customer.$invalid"> {{ $t('CustomerDropdown.btnSave') }}</button>
                    <button type="button" class="btn btn-soft-secondary btn-sm " v-on:click="close()">{{ $t('CustomerDropdown.btnClear') }}</button>
                </div>

            </div>
        </modal>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
</template>

<script>
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    import moment from 'moment';
    import Multiselect from 'vue-multiselect'
    import {
        required,
        maxLength,
        requiredIf
    } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        name: 'CustomerDropdown',
        props: ["modelValue", "disable", "paymentTerm", "isMultiple", "isCredit", "formName", 'customerList'],
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                b2b: false,
                salutatioRender: 0,
                b2c: false,
                isCashCustomer: false,
                arabic: '',
                english: '',
                contactCode: '',
                cashCustomerCode: '',
                selectedValue: [],
                options: [],
                optionsb2b: [],
                modelValues: '',
                disableValue: false,
                show: false,
                customer: {
                    id: '00000000-0000-0000-0000-000000000000',
                    customerType: '',
                    category: '',
                    code: '',
                    registrationDate: '',
                    englishName: '',
                    arabicName: '',
                    vatNo: '',
                    customerCode: '',
                    customerDisplayName: '',
                    contactNo1: '',
                    address: '',
                    paymentTerms: '',
                    isCustomer: true,
                    isCashCustomer: false,
                    isActive: true
                },
            }
        },
        validations() {
            return {
                customer: {
                    code: {},
                    customerType: {
                        required
                    },
                    registrationDate: {
                        required
                    },
                    englishName: {
                        maxLength: maxLength(30)
                    },
                    arabicName: {
                        required: requiredIf(function () {
                            return !this.customer.englishName;
                        }),
                        maxLength: maxLength(40)
                    },
                    vatNo: {
                        required: requiredIf(function () {
                            return this.customer.isCashCustomer == true ? false : true;
                        }),


                    },
                    customerDisplayName: {
                        required
                    },
                    paymentTerms: {
                        required
                    },
                }
            }
        },
        methods: {
            EmptyRecord: function () {

                this.DisplayValue = '';

            },
            Remove: function () {
                this.modelValues = '';

            },

            CompareWalkIn: function (isCashCustomer) {
                if (isCashCustomer == true) {
                    var root = this;
                    var walkInCustomer = root.options.find(function (x) {
                        return x.englishName == 'Walk-In';
                    });
                    this.value = walkInCustomer;
                    if (walkInCustomer == null || walkInCustomer == '') {
                        return null;
                    }
                    return walkInCustomer.id;
                }
            },

            isCashCustomerFunc: function () {

                if (this.customer.isCashCustomer) {

                    this.customer.customerType = 'Individual';
                    this.customer.paymentTerms = 'Cash';
                    this.customer.vatNo = '';
                    this.customer.registrationDate = moment().format('LL');

                } else {
                    this.customer.vatNo = '';
                    this.customer.customerType = '';
                }
            },
            asyncFind: function (search) {

                this.getData(true, search);
            },
            GetAutoCodeGenerator: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/Contact/AutoGenerateCode?isCustomer=true' + '&isCashCustomer=' + this.isCashCustomer, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {
                            root.contactCode = response.data.contact;
                            root.cashCustomerCode = response.data.cashCustomer;

                        }
                    });
            },
            DisplayName: function () {
                this.salutatioRender++;
            },
            AddCustomer: function () {

                var name = this.$refs.multiselect.search;

                this.isCashCustomer = localStorage.getItem('CashCustomer') == 'true' ? true : false;

                this.v$.$reset();

                {
                    this.customer = {
                        id: '00000000-0000-0000-0000-000000000000',
                        code: '',
                        customerType: '',
                        paymentTerms: '',
                        category: '',
                        registrationDate: '',
                        englishName: name,
                        arabicName: '',
                        vatNo: '',
                        customerCode: '',
                        contactNo1: '',
                        address: '',
                        customerDisplayName: '',
                        isCustomer: true,
                        isActive: true,
                        isCashCustomer: false,
                    }
                }

                if (this.isCashCustomer) {
                    this.customer.isCashCustomer = true;
                    this.isCashCustomerFunc();

                }
                if (this.isCredit == true) {
                    this.customer.isCashCustomer = false;
                }
                this.b2b = localStorage.getItem('b2b') == 'true' ? true : false;
                this.b2c = localStorage.getItem('b2c') == 'true' ? true : false;
                if (this.b2b && !this.b2c) {
                    this.customer.category = 'B2B – Business to Business';
                }
                if (!this.b2c && this.b2c) {
                    this.customer.category = 'B2C – Business to Client';
                } else {
                    this.customer.category = 'B2C – Business to Client';
                }

                this.GetAutoCodeGenerator();
                this.customer.registrationDate = moment().format('llll');
                this.show = !this.show;
            },
            SaveCustomer: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.customer.isCashCustomer) {
                    this.customer.code = this.cashCustomerCode;

                } else {
                    this.customer.code = this.contactCode;
                }

                root.$https
                    .post('/Contact/SaveContact', this.customer, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                    .then(response => {
                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                            root.loading = false
                            root.info = response.data.bpi
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved' : 'تم الحفظ',
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true
                            });
                            root.show = !root.show;
                            root.getData(true);
                            root.$emit('update:modelValue', response.data.message.id);

                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire({
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: error.response.data,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                        root.show = !root.show;
                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
            close: function () {
                this.show = false;
            },
            GetCustomerAddress: function () {

                if (this.modelValues != null && this.modelValues != '') {
                    if (this.modelValues.length > 0)
                        return this.modelValues[0];
                    else
                        return this.modelValues;
                } else {
                    return {
                        address: '',
                        mobile: '',
                        email: '',

                    }
                }

            },
            UpdateData: function () {
                
                var selectedvalue = this.modelValues.id;
                if (!this.emptyselect) {
                    this.selectedValue = [];
                }
                this.$emit('update:modelValue', selectedvalue, this.options.find(x => x.id == selectedvalue));

            },
            getData: function (quick, search) {


                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (quick) {
                    root.options = [];
                }
                if (search == undefined || search == null) {
                    search = '';
                }
                var paymentTerms = '';
                var multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;

                paymentTerms = this.paymentTerm == 'Credit' || this.paymentTerm == 'آجـل' ? 'Credit' : '';

                this.$https.get('/Contact/ContactList?IsDropDown=' + true + '&isCustomer=' + true + '&isActive=' + true + '&paymentTerms=' + paymentTerms + '&searchTerm=' + search + '&isCashCustomer=' + this.isCashCustomer + '&multipleAddress=' + multipleAddress, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {
                        response.data.results.forEach(function (cust) {
                            if (cust.contactNo1 == null)
                                cust.contactNo1 = '';
                            if (cust.customerCode == null)
                                cust.customerCode = '';

                            var displayName = '';
                            if (cust.customerDisplayName != null && cust.customerDisplayName != "") {
                                displayName = '\u202A' + cust.customerDisplayName + '\u202C' + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + cust.contactNo1 + cust.customerCode;
                            }
                            else if (cust.englishName != '' && cust.englishName != null) {
                                displayName = cust.englishName + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + cust.contactNo1 + cust.customerCode;
                            }
                            else if (cust.arabicName != '' && cust.arabicName != null) {
                                displayName = '\u202A' + cust.arabicName + '\u202C' + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + cust.contactNo1 + cust.customerCode;
                            }
                            else {
                                displayName = cust.englishName + cust.customerCode;
                            }

                            var addressList = [];
                            if (cust.deliveryAddresses != null) {
                                addressList = cust.deliveryAddresses.filter(x => x.isActive)
                            }
                            root.options.push({
                                id: cust.id,
                                deliveryAddressList: addressList,
                                code: cust.code,
                                customerDisplayName: cust.customerDisplayName,
                                isCashCustomer: cust.isCashCustomer,
                                englishName: cust.englishName,
                                arabicName: cust.arabicName,
                                commercialRegistrationNo: cust.commercialRegistrationNo,
                                vatNo: cust.vatNo,
                                contactNo1: cust.contactNo1,
                                email: cust.email,
                                address: cust.address,
                                shippingAddress: cust.shippingAddress,
                                regionId: cust.regionId,
                                creditDays: cust.creditDays,
                                creditLimit: cust.creditLimit,
                                creditPeriod: cust.creditPeriod,
                                advanceAccountId: cust.advanceAccountId,
                                deliveryAddress: cust.deliveryAddress,
                                shippingOther: cust.shippingOther,
                                deliveryOther: cust.deliveryOther,
                                paymentTerms: cust.paymentTerms,
                                customerCode: cust.customerCode,
                                name: displayName,
                                priceLabelId: cust.priceLabelId,
                                companyNameEnglish: cust.companyNameEnglish,
                                companyNameArabic: cust.companyNameArabic,
                                prefix: cust.prefix,
                            });

                        });

                        if (root.formName == 'ServiceQuotation' || root.formName == 'ServiceSaleOrder' || root.formName == 'ServiceProformaInvoice') {
                            const index = root.options.findIndex(x => x.englishName === 'Walk-In');
                            if (index !== -1) {
                                //   const newObj = { ...root.options[index]};
                                root.options.splice(index, 1);
                            }

                        }
                        else if (root.formName == 'SaleInvoice') {
                            if (root.isCashCustomer) {
                                if (localStorage.getItem('IsCashCustomer') == 'true') {
                                    if (root.isCredit == false) {
                                        if (root.modelValue == null || root.modelValue == undefined || root.modelValue == '') {
                                            var walkInCustomer = root.options.find(function (x) {
                                                return x.englishName == 'Walk-In';
                                            })
                                            if (walkInCustomer != undefined && walkInCustomer != null) {

                                                root.$emit('update:modelValue', walkInCustomer.id, walkInCustomer.advanceAccountId, walkInCustomer);
                                            }

                                        }

                                    }

                                }
                            }
                        }
                        else {
                            const index = root.options.findIndex(x => x.englishName === 'Walk-In');
                            if (index !== -1) {
                                //   const newObj = { ...root.options[index]};
                                root.options.splice(index, 1);
                            }

                        }
                    }
                }).then(function () {

                    root.modelValues = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                    if (root.modelValues == null || root.modelValues == undefined || root.modelValues == '') {
                        console.log();
                    }
                    else {
                        root.$emit("update:modelValue", root.modelValues.id, root.modelValues.advanceAccountId, root.modelValues);
                    }
                });
            },
        },
        computed: {
            DisplayValue: {

                get: function () {
                    if (this.modelValues != "" || this.modelValues != undefined) {
                        return this.modelValues;
                    }
                    return this.modelValue;
                },
                set: function (value) {
                    
                    if (value == null || value == undefined) {
                        this.modelValues = value;
                        this.$emit("update:modelValue", '', '');
                    }
                    else if (this.isMultiple == true) {
                        this.$emit("update:modelValue", value, value);

                    }
                    else {
                        
                        this.modelValues = value;
                        this.$emit("update:modelValue", value.id, value.advanceAccountId, value);
                    }
                }
            }
        },
        mounted: function () {
            this.isCashCustomer = localStorage.getItem('CashCustomer') == 'true' ? true : false;

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');

            this.getData();
            this.disableValue = this.disable;
        },
    }
</script>
