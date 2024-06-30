<template>
    <div v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
        <multiselect 
                     v-model="DisplayValue"
                     tag-placeholder="Add New"
                     :options="options" 
                     v-bind:disabled="disable"
                     :multiple="false"
                     ref="multiselect"
                      @open="open"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     v-bind:placeholder="$t('CustomerDropdown.SelectOption')"
                     :taggable="true"
                     @tag="AddCustomer"
                     v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'arabicLanguage '">
                     <template v-slot:noResult>
                     <span   class="btn btn-primary "   v-on:click="AddCustomer(true)" >{{ $t('CustomerDropdown.CreateQuickCustomer') }}</span><br />
        </template>
                    </multiselect>


        <modal :show="show" v-if="show ">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title m-0" id="exampleModalDefaultLabel">{{ $t('CustomerDropdown.AddCustomer') }}</h6>
                    <button type="button" class="btn-close" v-on:click="close()"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="" v-if="reparingOrder==true">
                                <div class="card-body">
                                    <div class="row ">
                                        <div class="col-sm-12">
                                            <label>{{ $t('CustomerDropdown.CustomerCode') }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.customer.code.$error}">
                                                <input readonly class="form-control " v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" v-model="v$.customer.code.$model" />
                                                <span v-if="v$.customer.code.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>


                                        <div v-if="english=='true'" class="col-sm-12">
                                            <label>{{$englishLanguage($t('CustomerDropdown.CustomerName(English)'))  }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.customer.englishName.$error}">
                                                <input v-bind:key="randerInput" @keyup.enter="SaveCustomer" autofocus class="form-control " v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" v-model="v$.customer.englishName.$model" />
                                                <span v-if="v$.customer.englishName.$error" class="error text-danger">
                                                    <span v-if="!v$.customer.englishName.required">{{ $t('CustomerDropdown.EngValidation') }} </span>
                                                    <span v-if="!v$.customer.englishName.maxLength">{{ $t('CustomerDropdown.EngMax') }} </span>
                                                </span>
                                            </div>
                                        </div>
                                        <div v-if="arabic=='true'" class="col-sm-12">
                                            <label>{{$arabicLanguage($t('CustomerDropdown.CustomerName(Arabic)'))  }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.customer.arabicName.$error}">
                                                <input class="form-control arabicLanguage" v-model="v$.customer.arabicName.$model" @keyup.enter="SaveCustomer" />
                                                <span v-if="v$.customer.arabicName.$error" class="error text-danger">

                                                    <span v-if="!v$.customer.arabicName.maxLength">{{ $t('CustomerDropdown.ArMax') }} </span>
                                                </span>
                                            </div>
                                        </div>



                                        <div class="col-sm-12">
                                            <label>{{ $t('CustomerDropdown.Mobile') }} :</label>
                                            <input class="form-control text-left" @keyup.enter="SaveCustomer" v-model="customer.contactNo1" />
                                        </div>



                                    </div>
                                </div>
                            </div>
                            <div class="" v-else>
                                <div class="card-body">
                                    <div class="row ">
                                        <div class="col-sm-12">
                                            <label>{{ $t('CustomerDropdown.CustomerCode') }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.customer.code.$error}">
                                                <input readonly class="form-control " v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" v-model="v$.customer.code.$model" />
                                                <span v-if="v$.customer.code.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <label> {{ $t('Customer.CustomerType') }}   :</label>
                                            <div>
                                                <multiselect v-model="customer.category" :options="optionsb2b" :show-labels="false" placeholder="Select Type">
                                                </multiselect>

                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <label> {{ $t('Customer.CustomerCategory') }}:<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.customer.customerType.$error}">

                                                <multiselect v-if="$i18n.locale == 'en' " v-model="v$.customer.customerType.$model" :options="['Individual', 'Establishment', 'Company','Factory','Manufacturer']" :show-labels="false" placeholder="Select Type">
                                                </multiselect>
                                                <multiselect v-else v-model="v$.customer.customerType.$model" :options="['فرد', 'مؤسسة', 'شركة']" :show-labels="false" v-bind:placeholder="$t('SelectOption')" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                                </multiselect>
                                                <span v-if="v$.customer.customerType.$error" class="error text-danger">

                                                </span>
                                            </div>
                                        </div>
                                        <div v-if="english=='true'" class="col-sm-12">
                                            <label>{{$englishLanguage($t('CustomerDropdown.CustomerName(English)'))  }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.customer.englishName.$error}">
                                                <input class="form-control " v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" v-model="v$.customer.englishName.$model" />
                                                <span v-if="v$.customer.englishName.$error" class="error text-danger">
                                                    <span v-if="!v$.customer.englishName.required">{{ $t('CustomerDropdown.EngValidation') }} </span>
                                                    <span v-if="!v$.customer.englishName.maxLength">{{ $t('CustomerDropdown.EngMax') }} </span>
                                                </span>
                                            </div>
                                        </div>
                                        <div v-if="arabic=='true'" class="col-sm-12">
                                            <label>{{$arabicLanguage($t('CustomerDropdown.CustomerName(Arabic)'))  }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.customer.arabicName.$error}">
                                                <input class="form-control arabicLanguage" v-model="v$.customer.arabicName.$model" />
                                                <span v-if="v$.customer.arabicName.$error" class="error text-danger">

                                                    <span v-if="!v$.customer.arabicName.maxLength">{{ $t('CustomerDropdown.ArMax') }} </span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <label>{{ $t('CustomerDropdown.RegistrationDate') }}  :</label>
                                            <div v-bind:class="{'has-danger' : v$.customer.registrationDate.$error}">
                                                <datepicker v-model="v$.customer.registrationDate.$model"></datepicker>
                                                <span v-if="v$.customer.registrationDate.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <label>{{ $t('CustomerDropdown.VAT/NTN/Tax No') }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.customer.vatNo.$error}">
                                                <input class="form-control " v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" v-model="v$.customer.vatNo.$model" />
                                                <span v-if="v$.customer.vatNo.$error" class="error text-danger">
                                                    <span v-if="!v$.customer.vatNo.required">{{ $t('CustomerDropdown.VatNoRequired') }}</span>
                                                </span>
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <label>{{ $t('CustomerDropdown.Mobile') }} :</label>
                                            <input class="form-control text-left" v-model="customer.contactNo1" />
                                        </div>
                                        <div class="col-sm-12">
                                            <label>{{ $t('CustomerDropdown.Address') }} :</label>
                                            <div>
                                                <textarea class="form-control text-left" v-model="customer.address" />

                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <label>{{ $t('CustomerDropdown.PaymentTerms') }} :<span class="text-danger"> *</span></label>


                                            <multiselect v-model="customer.paymentTerms" v-if="$i18n.locale == 'en' " :options="[ 'Cash', 'Credit']" :show-labels="false" placeholder="Select Type">
                                            </multiselect>
                                            <multiselect v-else v-model="customer.paymentTerms" :options="[ 'نقد', 'آجل']" :show-labels="false" v-bind:placeholder="$t('CustomerDropdown.SelectOption')" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                            </multiselect>
                                            <span v-if="v$.customer.paymentTerms.$error" class="error text-danger">
                                                <span v-if="!v$.customer.paymentTerms.required">{{ $t('CustomerDropdown.PaymentValidation') }} </span>
                                            </span>

                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer justify-content-right">
                                <button type="button" class="btn btn-primary  " v-on:click="SaveCustomer(true)" v-shortkey="['enter']" @shortkey="SaveCustomer(true)" v-bind:disabled="v$.customer.$invalid"> {{ $t('CustomerDropdown.btnSave') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close" v-shortkey="['shift', 'del']" @shortkey="close">{{ $t('CustomerDropdown.btnClear') }}</button>
                            </div>
                        </div>

                    </div>
                </div>
                 <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            </div>
        </modal>
        
    </div>
</template>
<script>
       import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    import moment from 'moment';
    import Multiselect from 'vue-multiselect'
    import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        name: 'CustomerDropdown',
        props: ["modelValue", "disable", "paymentTerm", "reparingOrder", "autofocusOn"],
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
                rander: 0,
                randerInput: 0,
                b2b: false,
                check: false,
                set: false,
                isOn: false,
                b2c: false,
                customerList: [],
                arabic: '',
                unique: '',
                english: '',
                mobile: '',
                options: [],
                optionsb2b: [],
                value: '',
                disableValue: false,
                show: false,
                isSave: false,
                customer: {
                    id: '00000000-0000-0000-0000-000000000000',
                    customerType: '',
                    code: '',
                    registrationDate: '',
                    englishName: '',
                    arabicName: '',
                    vatNo: '',
                    contactNo1: '',
                    address: '',
                    paymentTerms: '',
                    isCustomer: true,
                    isActive: true
                },
            }
        },
        validations() {
            return{
                customer: {
                code: { required },
                customerType: { required },
                registrationDate: { required },
                englishName: {
                    maxLength: maxLength(30)
                },
                arabicName: {
                    required: requiredIf(function () {
                            return !this.customer.englishName;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.englishName == '' || x.englishName == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(40)
                },
                vatNo: {
                    required
                },
                paymentTerms: { required },
            }
            }
        },
        methods: {

           
            //asyncFind: function (search) {
            //    
            //    this.mobile = search;
            //    this.value = '';
            //    this.check = false;

            //    console.log(this.check);
            //},

            Value: function (search) {
                
                console.log(search)
            },
           
            GetAutoCodeGenerator: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/Contact/AutoGenerateCode?isCustomer=true'+ '&isCashCustomer=' + false, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.customer.code = response.data.contact;

                        }
                    });
            },
            AddCustomer: function (newEmail) {
                
                console.log(newEmail);
               
               
                this.v$.$reset();
                if (this.reparingOrder == true) {
                    this.customer = {
                        id: '00000000-0000-0000-0000-000000000000',
                        code: '',
                        customerType: 'Factory',
                        category: 'B2C – Business to Client',
                        registrationDate: '',
                        englishName: '',
                        arabicName: '',
                        vatNo: '0000000',
                        paymentTerms: 'Credit',
                        contactNo1: newEmail,
                        address: '',
                        isCustomer: true,
                        isActive: true
                    }
                }
                else {
                    this.customer = {
                        id: '00000000-0000-0000-0000-000000000000',
                        code: '',
                        customerType: '',
                        registrationDate: '',
                        englishName: '',
                        arabicName: '',
                        vatNo: '',
                        contactNo1: '',
                        address: '',
                        isCustomer: true,
                        isActive: true
                    }
                }

                this.GetAutoCodeGenerator();
                this.customer.registrationDate = moment().format('llll');
                this.show = !this.show;
                this.isSave = true;
                this.randerInput++;
                /*this.$refs.email.focus();*/
            },
           
           

            SaveCustomer: function () {
                
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.customer.englishName == '' )
                    return;
                this.isSave = false;
                this.mobile = '';
                root.$https
                    .post('/Contact/SaveContact', this.customer, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                            root.loading = false
                            root.info = response.data.bpi
                            //root.$swal({
                            //    title: root.$i18n.locale == 'en' ? 'Saved Successfully' : 'حفظ بنجاح',
                            //    text: root.$i18n.locale == 'en' ? 'Saved' : 'تم الحفظ',
                            //    type: 'success',
                            //    confirmButtonClass: "btn btn-success",
                            //    buttonStyling: false,
                            //    icon: 'success',
                            //    timer: 1500,
                            //    timerProgressBar: true
                            //});
                            

                            root.show = !root.show;
                            this.getData(true);
                            root.modelValue = response.data.message.id;
                            //root.$emit('update:modelValue', root.modelValue.id, '');



                        }
                    });
                   
            },
            open() {
                this.$nextTick(() => {
                    
                    if (this.value.name != undefined) {
                        this.$refs.multiselect.placeholder = this.value.name
                    }

                });
            },
            close: function () {
                this.check = false;
                this.show = false;
                this.isSave = false;
            },
          


            getData: function () {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

              
                    root.options = [];
              
              var  search = '';
                var paymentTerms = '';


                paymentTerms = this.paymentTerm == 'Credit' || this.paymentTerm == 'آجـل' ? 'Credit' : '';


                this.$https.get('/Contact/ContactList?IsDropDown=' + true + '&isCustomer=' + true + '&isActive=' + true + '&paymentTerms=' + paymentTerms + '&searchTerm=' + search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        if (root.reparingOrder) {
                            response.data.results.forEach(function (cust) {
                                root.customerList = response.data.results;
                                if (cust.englishName == null)
                                    cust.englishName = '';
                                if (cust.contactNo1 == null)
                                    cust.contactNo1 = '';

                                root.options.push({
                                    id: cust.id,
                                    address: cust.address,
                                    accountId: cust.accountId,
                                    mobile: cust.contactNo1,
                                    name: cust.contactNo1 + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + cust.englishName 
                                })
                            })

                        }
                        else {
                            response.data.results.forEach(function (cust) {
                                root.customerList = response.data.results;
                                root.options.push({
                                    id: cust.id,
                                    address: cust.address,
                                    mobile: cust.contactNo1,
                                    advanceAccountId: cust.advanceAccountId,
                                    name:cust.contactNo1+ '   ||   ' +   cust.englishName 
                                })
                            })
                        }

                    }
                }).then(function () {
                    if (root.modelValue != undefined && root.modelValue != '') {
                        
                        root.options.forEach(function (x) {
                            if (x.id === root.modelValue) {

                                root.value = x;
                               
                                root.$emit('update:modelValue', x.id);
                            }
                        })
                    }
                   
                });
                
               
            },
        },
        computed: {
            DisplayValue: {

                get: function () {
                    
                      if (this.value != "" || this.value != undefined) {
                        return this.value;
                    }
                    return this.modelValue;
                },
                set: function (value) {
                    
                    if (value == null) {
                        this.value = value;
                        this.$emit('update:modelValue', value);
                    } else {

                        this.value = value;
                        this.$emit('update:modelValue', value.id);
                    }
                }
            }
        },
        created: function () {
            //var root = this;
            //
            //localStorage.setItem("BarcodeScan", 'AddReparingOrderForCustomer')

            //if (localStorage.getItem("BarcodeScan") != 'AddReparingOrderForCustomer')
            //    return


            //var interval;
            //
            //document.addEventListener('keydown', function (evt) {
            //    if (interval)
            //        clearInterval(interval);
            //    if (evt.code === 'Enter') {
            //        if (root.isSave == true) {
            //            if (this.customer == undefined) {
            //                root.AddCustomer(true);
            //            }
            //           else if (this.customer.englishName != '' || this.customer.englishName != undefined || this.customer.englishName != null)
            //                root.SaveCustomer();
            //            else {
            //                root.AddCustomer(true);

            //            }

            //        }
            //        else
            //        {
            //            if (this.value == "" || this.value == undefined) {
            //                root.AddCustomer(true);
            //            }

            //        }



            //    }
            //    else
            //        return;

            //});
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.b2b = localStorage.getItem('b2b') == 'true' ? true : false;
            this.b2c = localStorage.getItem('b2c') == 'true' ? true : false;
            if (this.b2b && this.b2c) {
                this.optionsb2b = ['B2B – Business to Business', 'B2C – Business to Client']
            }
            else if (this.b2b) {
                this.optionsb2b = ['B2B – Business to Business']
            }
            else if (this.b2c) {
                this.optionsb2b = ['B2C – Business to Client']
            }
            this.getData();
            this.disableValue = this.disable;


        },
    }
</script>