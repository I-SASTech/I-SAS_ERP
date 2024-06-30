<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" > {{ $t('CustomerDropdown.AddCustomer') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group col-sm-12">
                        <label>Mobile :</label>
                        <input ref='focusMe' class="form-control text-left" maxlength="11" v-model="search" @keyup.enter="getData" />
                    </div>

                    <!--<div class="form-group has-label col-sm-12 ">
                        <button type="button" class="btn btn-sm btn-primary" v-on:click="getData" @shortkey="getData" v-shortkey="['enter']"> {{ $t('CustomerDropdown.btnSave') }}</button>

                    </div>-->
                </div>

                <div class="row " v-if="isFind==false">
                    <div class="col-sm-12 form-group">
                        <label>{{$englishLanguage($t('CustomerDropdown.CustomerName(English)'))  }} :<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.customer.englishName.$error}">
                            <input v-bind:key="randerInput" @keyup.enter="SaveCustomer" autofocus class="form-control " v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" v-model="v$.customer.englishName.$model" />
                            <span v-if="v$.customer.englishName.$error" class="error text-danger">
                                <span v-if="!v$.customer.englishName.required">{{ $t('CustomerDropdown.EngValidation') }} </span>
                                <span v-if="!v$.customer.englishName.maxLength">{{ $t('CustomerDropdown.EngMax') }} </span>
                            </span>
                        </div>
                    </div>

                    <div class="col-sm-12 form-group">
                        <label>Oher Mobile :</label>
                        <input class="form-control text-left" v-model="customer.contactNo2" />
                    </div>
                    <div class="col-sm-12 form-group">
                        <label>Address :</label>
                        <div>
                            <textarea class="form-control text-left" v-model="customer.address" />

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveCustomer1(true)" v-bind:disabled="v$.customer.$invalid"> {{ $t('CustomerDropdown.btnSave') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('CustomerDropdown.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>

    </modal>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from 'moment';
    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        name: 'QuickCustomerModel',
        props: ["values", "disable", "paymentTerm", "reparingOrder", "autofocusOn", "newshow", "mobileNo"],
        components: {
            Loading
        },
        mixins: [clickMixin],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                loading: false,
                show: this.newshow,
                rander: 0,
                randerInput: 0,
                b2b: false,
                check: false,
                isFind: null,
                set: false,
                isOn: false,
                b2c: false,
                customerList: [],
                arabic: '',
                search: '',
                unique: '',
                english: '',
                mobile: '',
                options: [],
                optionsb2b: [],
                value: '',
                disableValue: false,
                isSave: false,
                customer: {
                    id: '00000000-0000-0000-0000-000000000000',
                    customerType: 'Factory',
                    category: 'B2C – Business to Client',
                    code: '',
                    registrationDate: moment().format('llll'),
                    englishName: '',
                    arabicName: '',
                    vatNo: '0000000',
                    contactNo1: '',
                    address: '',
                    paymentTerms: 'Credit',
                    isCustomer: true,
                    isActive: true
                },
            }
        },
        validations() {
            return {
                customer: {
                    code: {},
                    customerType: {},
                    registrationDate: {},
                    englishName: {
                        required,
                        maxLength: maxLength(30)
                    },

                    vatNo: {
                    },
                    paymentTerms: {},
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

                //this.GetAutoCodeGenerator();
                this.customer.registrationDate = moment().format('llll');
                this.show = !this.show;
                this.isSave = true;
                this.randerInput++;
                /*this.$refs.email.focus();*/
            },



            SaveCustomer1: function () {
                
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.isFind) {
                    root.show = !root.show;

                    this.$emit('CustomerId', this.customer.id);

                }
                else {
                    this.customer.contactNo1 = this.search;
                    //if (this.customer.englishName == '')
                    //    return;
                    //this.isSave = false;
                    //this.mobile = '';
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
                                this.$emit('CustomerId', response.data.message.id);
                                //root.$emit('update:modelValue', root.values.id, '');



                            }
                        })
                        .catch(error => {
                            console.log(error)
                            root.$swal.fire(
                                {
                                    icon: 'error',
                                    title: root.$i18n.locale == 'en' ? 'Error!' : 'خطأ',
                                    text: error.response.data,
                                    showConfirmButton: false,
                                    timer: 5000,
                                    timerProgressBar: true,
                                });
                            root.show = !root.show;
                            root.loading = false
                        })
                        .finally(() => root.loading = false)
                }


            },
            close: function () {
                
                //this.check = false;
                //this.show = !this.show;
                //this.isSave = false;
                this.$emit('close');
            },



            getData: function () {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.options = [];

                var paymentTerms = '';

                if (this.customer.englishName == '') {
                    if (this.search.length != 11) {
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Complete Your Number',
                                text: 'Complete Your Number',
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                        return;
                    }

                    paymentTerms = this.paymentTerm == 'Credit' || this.paymentTerm == 'آجـل' ? 'Credit' : '';


                    this.$https.get('/Contact/ContactList?IsDropDown=' + true + '&isCustomer=' + true + '&isActive=' + true + '&paymentTerms=' + paymentTerms + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            
                            {
                                
                                if (response.data.results.length == 1) {
                                    root.customer.id = response.data.results[0].id;
                                    root.customer.code = response.data.results[0].code;
                                    root.customer.englishName = response.data.results[0].englishName;
                                    root.customer.contactNo1 = response.data.results[0].contactNo1;
                                    root.isFind = true;
                                    if (root.isFind) {
                                        root.show = !root.show;

                                        root.$emit('CustomerId', root.customer.id);

                                    }
                                }
                                else {
                                    root.isFind = false;
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
                                            name: cust.contactNo1 + ' ' + cust.englishName
                                        });
                                    })
                                }


                            }

                        }
                    }).then(function () {
                        if (root.values != undefined && root.values != '') {

                            root.options.forEach(function (x) {
                                if (x.id === root.values) {

                                    root.value = x;

                                    root.$emit('update:modelValue', x.id);
                                }
                            })
                        }

                    });

                }
                else {
                    this.SaveCustomer1();
                }





            },
        },
        computed: {
            DisplayValue: {

                get: function () {

                    if (this.value != "" || this.value != undefined) {
                        return this.value;
                    }
                    return this.values;
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
            
           
        },
        mounted: function () {
            this.$nextTick(() => {
                if (this.$refs.focusMe) {
                    this.$refs.focusMe.focus();
                }
            });
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
            this.disableValue = this.disable;
            //this.GetAutoCodeGenerator();
            this.search = this.mobileNo;
            if (this.search != '' && this.search != null && this.search != undefined) {
                this.getData();
            }
            

        },
    }
</script>
<style scoped>
</style>