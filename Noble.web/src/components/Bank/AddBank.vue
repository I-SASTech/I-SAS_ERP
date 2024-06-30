<template>
    <div class="row" v-if="isValid('CanAddBank') || isValid('CanEditBank') ">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AddBank.RegisterBankAccount') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('AddBank.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('AddBank.RegisterBankAccount') }}</li>
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
                                <div v-bind:key="rendered" class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.BankCode') }} : <span class="text-danger">  *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.bank.code.$error}">
                                        <input readonly class="form-control " v-model="v$.bank.code.$model" />
                                        <span v-if="v$.bank.code.$error" class="error">
                                            <span v-if="!v$.bank.code.required"> {{ $t('AddBank.CodeRequired') }}</span>
                                        </span>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3" v-if="english=='true'">
                                    <label>{{$englishLanguage($t('AddBank.BankName'))   }} :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.bank.bankName.$error}">
                                        <input class="form-control " v-model="v$.bank.bankName.$model" />
                                        <span v-if="v$.bank.bankName.$error" class="error text-danger">
                                            <span v-if="!v$.bank.bankName.maxLength"> {{ $t('AddBank.BankNameLength') }}</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3" v-if="isOtherLang()">
                                    <label>{{$arabicLanguage($t('AddBank.BankName'))   }} :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.bank.nameArabic.$error}">
                                        <input class="form-control " v-model="v$.bank.nameArabic.$model" />
                                        <span v-if="v$.bank.nameArabic.$error" class="error text-danger">
                                            <span v-if="!v$.bank.nameArabic.required"> {{ $t('AddBank.NameRequired') }}</span>
                                            <span v-if="!v$.bank.nameArabic.maxLength"> {{ $t('AddBank.BankNameLength') }}</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.ShortName') }} :</label>
                                    <div v-bind:class="{'has-danger' : v$.bank.shortName.$error}">
                                        <input class="form-control " v-model="v$.bank.shortName.$model" />
                                        <span v-if="v$.bank.shortName.$error" class="error text-danger">
                                        </span>
                                    </div>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3" v-if="english=='true'">
                                    <label>{{$englishLanguage($t('AddBank.AccountName'))   }}:<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.bank.accoutName.$error}">
                                        <input class="form-control " v-model="v$.bank.accoutName.$model" />
                                        <span v-if="v$.bank.accoutName.$error" class="error text-danger">
                                            <span v-if="!v$.bank.accoutName.required">  {{ $t('AddBank.NameRequired') }}</span>
                                            <span v-if="!v$.bank.accoutName.maxLength">  {{ $t('AddBank.NameLength') }}</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3" v-if="isOtherLang()">
                                    <label>{{$arabicLanguage($t('AddBank.AccountName'))   }}:<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.bank.accoutNameArabic.$error}">
                                        <input class="form-control " v-model="v$.bank.accoutNameArabic.$model" />
                                        <span v-if="v$.bank.accoutNameArabic.$error" class="error text-danger">
                                            <span v-if="!v$.bank.accoutNameArabic.required">{{ $t('AddBank.AccountRequired') }}</span>
                                        </span>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.AccountNumber') }}:<span class="text-danger"> *</span></label>
                                    <input class="form-control " v-model="v$.bank.accountNumber.$model" />
                                    <span v-if="v$.bank.accountNumber.$error" class="error text-danger">
                                        <span v-if="!v$.bank.accountNumber.required">{{ $t('AddBank.AccountRequired') }}</span>
                                    </span>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.IBNNumber') }}:<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.bank.ibnNumber.$error}">
                                        <input class="form-control " v-model="v$.bank.ibnNumber.$model" />
                                        <span v-if="v$.bank.ibnNumber.$error" class="error text-danger">
                                            <span v-if="!v$.bank.ibnNumber.required">{{ $t('AddBank.IBNRequired') }}</span>
                                        </span>
                                    </div>
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>Account Type:</label>
                                    <multiselect v-model="bank.accountType"  :preselect-first="false"  :options="[ 'Current', 'Saving','Default']" :show-labels="false" :placeholder="$t('AddCustomer.SelectType')">
                                                    </multiselect>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.Reference') }} :</label>
                                    <input class="form-control " v-model="bank.reference" />
                                </div>

                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.BranchName') }} :<span class="text-danger"> *</span></label>
                                    <div v-bind:class="{'has-danger' : v$.bank.branchName.$error}">
                                        <input class="form-control " v-model="v$.bank.branchName.$model" />
                                        <span v-if="v$.bank.branchName.$error" class="error text-danger">
                                            <span v-if="!v$.bank.branchName.required">  {{ $t('AddBank.NameRequired') }}</span>
                                            <span v-if="!v$.bank.branchName.maxLength">  {{ $t('AddBank.NameLength') }}</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.BranchCode') }} :</label>
                                    <input class="form-control " v-model="bank.branchCode" />
                                </div>


                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.Currency') }}:<span class="text-danger"> *</span></label>
                                    <currency-dropdown v-model="bank.currencyId" :modelValue="bank.currencyId">
                                    </currency-dropdown>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.SwiftCode') }}:</label>
                                    <input class="form-control " v-model="bank.swiftCode" />
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.ContactPerson') }}:</label>
                                    <input class="form-control " v-model="bank.contactPerson" />
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.ContactName') }} :</label>
                                    <input class="form-control " v-model="bank.contactName" />
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.ManagerName') }}:</label>
                                    <input class="form-control " v-model="bank.managerName" />
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.ManagerContactualNumber') }}:</label>
                                    <input class="form-control " v-model="bank.managerContectualNumber" />
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">
                                    <label>{{ $t('AddBank.AccounType') }}:</label>
                                    <input class="form-control " v-model="bank.accounType" />
                                </div>

                                <div class="form-group col-sm-12">
                                    <label>{{ $t('AddBank.Location') }} :</label>
                                    <textarea rows="3" class="form-control " v-model="bank.location" />
                                </div>
                                <div class="form-group col-12">

                                    <div class="checkbox form-check-inline mx-2">

                                        <input type="checkbox" id="inlineCheckbox1" v-model="bank.active">
                                        <label for="inlineCheckbox1"> {{ $t('AddBank.Active') }} </label>
                                    </div>
                                </div>
                            </div>


                        </div><!--end col-->
                    </div><!--end row-->

                </div>
                <div v-if="!loading" class="card-footer">
                    <div class="row">
                        <div v-if="!loading" class=" col-md-12">
                            <div class="button-items">
                                <button class="btn btn-primary" v-bind:disabled="v$.bank.$invalid" v-if="bank.id=='00000000-0000-0000-0000-000000000000' && isValid('CanAddBank') " v-on:click="SaveBank"><i class="mdi mdi-check-all me-2"></i> {{ $t('AddBank.btnSave') }}</button>
                                <button class="btn btn-primary" v-bind:disabled="v$.bank.$invalid" v-if="bank.id!='00000000-0000-0000-0000-000000000000' && isValid('CanEditBank') " v-on:click="SaveBank"><i class="mdi mdi-check-all me-2"></i> {{ $t('AddBank.btnUpdate') }}</button>
                                <button class="btn btn-danger" v-on:click="GoToBank"> {{ $t('AddBank.btnClear') }}</button>
                            </div>
                        </div>
                    </div>
                </div>
                 <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        <chequelistmodel :show="show"
                         v-if="show"
                         @close="show=false" />
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'

    import useVuelidate from '@vuelidate/core'
    import { required, maxLength } from '@vuelidate/validators'; 
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default ({
        mixins: [clickMixin],
        setup() {
            return { v$: useVuelidate() }
        },
        components: {
            Multiselect,
            Loading
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                show: false,
                value: '',
                active: 'personal',
                rendered: 0,
                bank: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    bankName: '',
                    nameArabic: '',
                    accoutName: '',
                    branchName: '',
                    accountNumber: '',
                    accountType: '',
                    accoutNameArabic: '',
                    branchCode: '',
                    swiftCode: '',
                    ibnNumber: '',
                    currencyId: '',
                    reference: '',
                    shortName: '',
                    active: true,
                },
                loading: false
            }
        },
        validations() {
            return {
                bank:
                {
                    code: {
                        required,
                        maxLength: maxLength(30)
                    },

                    bankName: {
                        maxLength: maxLength(200)
                    },
                   nameArabic: {
                      
                        //required: requiredIf((x) => {
                        //    if (x.name == '' || x.name == null)
                        //        return true;
                        //    return false;
                        //}),
                        maxLength: maxLength(250)
                    },
                    accoutNameArabic: {
                        
                        // required: requiredIf((x) => {
                        //     if (x.accoutName == '' || x.accoutName == null)
                        //         return true;
                        //     return false;
                        // }),
                        maxLength: maxLength(200)
                    },
                    accoutName: {
                        maxLength: maxLength(30)
                    },
                    branchName: {
                        required,
                        maxLength: maxLength(30)
                    },
                    accountNumber: {
                        required
                    },
                    ibnNumber: {
                        required
                    },
                    currencyId: {
                        required
                    },
                    shortName: {

                    }
                }

            }
        },
        methods: {

            GoToBank: function () {
                this.$router.push('/Bank');
            },
            SaveBank: function () {

                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https
                    .post('/Accounting/SaveBank', this.bank, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.loading = false
                        root.info = response.data.bpi

                        root.$swal.fire({
                            icon: 'success',
                            title: root.$t('AddBank.SavedSuccessfully'),
                            showConfirmButton: false,

                            timer: 800,
                            timerProgressBar: true,

                        });
                        root.$router.push('/Bank');
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: error.response.data,
                                text: root.$t('AddBank.SomethingWrong'),
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
            AutoIncrementCode: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/Accounting/BankCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.bank.code = response.data;
                            root.rendered++
                        }
                    });
            },
        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            this.$emit('update:modelValue', this.$route.name);

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.$route.query.data == undefined) {
                /*  this.bank.currency = localStorage.getItem('currency');*/
                this.AutoIncrementCode();
            }
            if (this.$route.query.data != undefined) {

                this.bank = this.$route.query.data;
                /* this.bank.currency = localStorage.getItem('currency');*/

            }
        },
        mounted: function () {

        }
    })

</script>