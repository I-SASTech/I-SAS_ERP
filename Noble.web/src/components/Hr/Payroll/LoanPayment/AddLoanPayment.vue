<template>
    <div class="row" v-if="isValid('CanAddLoanPayment') || isValid('CanEditLoanPayment')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="loanPayment.id === '00000000-0000-0000-0000-000000000000'"
                                    class="page-title">{{
                                            $t('AddLoanPayment.AddLoanPayment')
                                    }}</h4>
                                <h4 v-else class="page-title">{{ $t('AddLoanPayment.UpdateLoanPayment') }}</h4>
                            </div>
                            <div class="col-auto align-self-center">
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />

            <div class="row mb-5">

                
                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddLoanPayment.Employee') }} </span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <employeeDropdown v-model="loanPayment.employeeRegistrationId" ref="employeeDropDown" @update:modelValue="GetSalary" :modelValue="loanPayment.employeeRegistrationId" />
                        </div>
                    </div>
                </div>

                
                <div class="col-lg-6">
                    <div class="row form-group " >
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddLoanPayment.BaseSalary') }} </span><span class="text-danger"> *</span>
                        </label>
                        
                        <div class="inline-fields col-lg-8">
                            <div class="input-group">
                                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                                    <i>{{ currency }}</i></button>
                                                <input v-model="loanPayment.employeeSalary" type="text" class="form-control" disabled
                                                    aria-label="Example text with button addon" style="border: 1px dashed #1761fd;"
                                                    aria-describedby="button-addon1">
                                            </div>
                            <!-- <input class="form-control" type="text" :value="loanPayment.employeeSalary" style="border: 1px dashed #1761fd;" /> -->
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">

                 
                    
                    <div class="row form-group" >
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('LoanPayment.LoanType') }} </span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect :options="loanType" v-model="loanPayment.loanType" @update:modelValue="LoanTypeSelection" :show-labels="false" v-bind:placeholder="$t('AddLoanPayment.SelectMethod')" >
                                </multiselect>
                        </div>
                    </div>
                    


                </div>
                <div class="col-lg-6">
                    <div class="row form-group" v-bind:class="{'has-danger' : v$.loanPayment.description.$error}">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddLoanPayment.Description') }} </span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" v-model="v$.loanPayment.description.$model" type="text" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />
                        </div>
                    </div>
                  
                  

                    <div class="row form-group"  v-if="loanPayment.loanType=='Provident Fund' || loanPayment.loanType=='صندوق التوفير او الادخار' ">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline "> {{ $t('AddLoanPayment.ProvidentFundType') }} <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8 mt-2">
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" v-model="loanPayment.providentFundType" name="inlineRadioOptions" id="inlineRadio1" value=0>
                                    <label class="form-check-label" for="inlineRadio1">{{ $t('AddLoanPayment.Temporary') }}</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" v-model="loanPayment.providentFundType" name="inlineRadioOptions" id="inlineRadio2" value=1>
                                    <label class="form-check-label" for="inlineRadio2">{{ $t('AddLoanPayment.Permanent') }}</label>
                                </div>
                        </div>
                    </div>

                  
                </div>
                
                <hr class="hr-dashed hr-menu mt-0"/>
                <div class="row">
                            <div class="col">
                               
                                <h5 class="page-title"> {{ $t('AddLoanPayment.LoanRecoveryDetails') }}</h5>
                            </div>
                            <div class="col-auto align-self-center">
                               
                            </div>
                        </div>
               
                <div class="col-lg-6">


                    <div class="row form-group" >
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddLoanPayment.LoanRecoveryMethod') }} </span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect :options="recoveryMethod" v-model="loanPayment.recoveryMethod" :show-labels="false" v-bind:placeholder="$t('AddLoanPayment.SelectMethod')" >
                                </multiselect>
                        </div>
                    </div>
                    <div class="row form-group" >
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddLoanPayment.PaymentInstallmentMethod') }} </span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect :options="installmentMethod" v-model="loanPayment.installmentMethod" @update:modelValue="PaymentMethod()" v-bind:disabled="disableField" :show-labels="false" v-bind:placeholder="$t('AddLoanPayment.SelectMethod')">
                                </multiselect>
                        </div>
                    </div>

                    <div class="row form-group" >
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddLoanPayment.LoanTakenDate') }}</span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="loanPayment.loanTakenDate" @update:modelValue="SetPaymentStartDate()" :key="daterander"></datepicker>
                        </div>
                    </div>
                    <div class="row form-group" >
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddLoanPayment.PaymentStartDate') }} </span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="loanPayment.paymentStartDate" :key="startDaterander"></datepicker>
                        </div>
                    </div>


                    </div>
                    <div class="col-lg-6">



                    <div class="row form-group" >
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddLoanPayment.LoanAmount') }} </span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <div class="input-group">
                                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                                    <i>{{ currency }}</i></button>
                                                <input v-model="loanPayment.loanAmount" type="text" class="form-control"
                                                    @focus="$event.target.select()"
                                                    aria-label="Example text with button addon"
                                                    aria-describedby="button-addon1">
                                            </div>
                        </div>
                    </div>

                    <div class="row form-group" >
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddLoanPayment.RecoveryLoanAmount') }} </span><span class="text-danger"> *</span>
                         </label>
                        <div class="inline-fields col-lg-8 ">
                            <div class="input-group">
                                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                                    <i>{{ currency }}</i></button>
                                                <input type="text" class="form-control"
                                                @keyup="CalculateInstallment" v-model="loanPayment.recoveryLoanAmount"
                                                    @focus="$event.target.select()"
                                                    aria-label="Example text with button addon"
                                                    aria-describedby="button-addon1">
                                            </div>
                        </div>
                    </div>

                    <div class="row form-group"  v-bind:class="{'has-danger' : v$.loanPayment.deductionValue.$error} ">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddLoanPayment.DeductionValue') }} </span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <div class="input-group">
                                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                                    <i v-if="loanPayment.installmentMethod=='% of Salary' || loanPayment.installmentMethod=='٪ من الراتب'"
                                                        class="fa fa-percent"></i>
                                                    <i v-else>{{ currency }}</i></button>
                                                <input v-model="loanPayment.deductionValue" type="text" class="form-control"
                                                @focus="$event.target.select()" @keyup="CalculateInstallment" v-bind:disabled="disableField" 
                                                    aria-label="Example text with button addon"
                                                    aria-describedby="button-addon1">
                                            </div>
                        </div>
                    </div>

                    <div class="row form-group"  v-if="loanPayment.deductionValue>0 && (loanPayment.installmentMethod=='% of Salary' || loanPayment.installmentMethod=='٪ من الراتب')">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline "> {{ $t('AddLoanPayment.ProvidentFundType') }} <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8 ">
                            <input class="form-control" disabled v-model="loanPayment.installmentBaseSalary" type="text" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />
                        </div>
                    </div>



                    </div>
            </div>
            <div class="row">


                <div class="col-lg-12 invoice-btn-fixed-bottom ">

                    <div class="button-items">
                        <button class="btn btn-outline-primary  mr-2" v-on:click="SaveLoanPayment"
                        v-if="loanPayment.id=='00000000-0000-0000-0000-000000000000'  && isValid('CanAddLoanPayment') "
                            v-bind:disabled="v$.loanPayment.$invalid || percentageError || message || isDeductionRequired">
                            <i class="far fa-save"></i> {{ $t('AddLoanPayment.Save') }}
                        </button>

                        <button class="btn btn-outline-primary  mr-2" v-on:click="SaveLoanPayment"
                        v-if="loanPayment.id!='00000000-0000-0000-0000-000000000000'   && isValid('CanEditLoanPayment')"
                            v-bind:disabled="v$.loanPayment.$invalid || percentageError || message || isDeductionRequired">
                            <i class="far fa-save"></i> {{ $t('AddLoanPayment.Update') }}
                        </button>

                        <button class="btn btn-danger  mr-2" v-on:click="Close()">
                            {{ $t('AddLoanPayment.Cancel') }}
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
  </template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from 'moment';
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    // import { required, maxLength, minValue } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import { required, maxLength, minValue } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Multiselect from 'vue-multiselect'


    export default ({
        components: {
            Multiselect,
            Loading
        },
        mixins: [clickMixin],
 setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                loanPayment: {
                    id: '00000000-0000-0000-0000-000000000000',
                    description: '',
                    employeeSalary: 0,
                    employeeRegistrationId: '',
                    loanType: '',
                    recoveryMethod: '',
                    installmentMethod: '',
                    paymentStartDate: '',
                    loanTakenDate: '',
                    loanAmount: 0,
                    providentFundType: 0,
                    recoveryLoanAmount: 0,
                    deductionValue: 0,
                    installmentBaseSalary: 0,
                    isActive: false
                },
                currency: '',
                loanType: [],
                recoveryMethod: [],
                installmentMethod: [],
                loading: false,
                message: false,
                disableField: false,
                percentageError: false,
                isDeductionRequired: false,
                daterander: 0,
                startDaterander: 0,
                dateRender: 0,
                language: 'Nothing',
            }
        },
        validations() {
          return{
              loanPayment:
            {
                description: {
                    required,
                    maxLength: maxLength(50)
                },
                employeeRegistrationId: {
                    required,
                },
                loanType: {
                    required,
                },
                recoveryMethod: {
                    required,
                },
                installmentMethod: {
                    required,
                },
                loanAmount: {

                    minValue: minValue(1),


                },
                recoveryLoanAmount: {
                    minValue: minValue(1),
                },
                deductionValue: {


                },
                paymentStartDate: { required },
                loanTakenDate: { required }



            },
          }

        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({path: link});
            },
            SetPaymentStartDate: function () {
                
                if (this.loanPayment.loanTakenDate != undefined || this.loanPayment.loanTakenDate != '') {
                    this.loanPayment.paymentStartDate = moment(this.loanPayment.loanTakenDate).add(1, 'M').format('llll');
                    this.startDaterander++;
                }

            },

            LoanTypeSelection: function () {
                if (this.loanPayment.loanType == 'Advance' || this.loanPayment.loanType == 'تقدم') {
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {

                        this.loanPayment.installmentMethod = 'Fixed'

                    }
                    else {

                        this.loanPayment.installmentMethod = 'مثبت'
                    }
                    this.disableField = true;

                }
                else {
                    this.disableField = false;
                    this.loanPayment.installmentMethod = '';
                    this.loanPayment.deductionValue = 0;
                }

            },
            GetSalary: function () {
                
                this.loanPayment.employeeSalary = this.$refs.employeeDropDown.GetSalaryOfSelected();
                if (this.loanPayment.employeeSalary == null) {
                    this.message = true;
                    this.loanPayment.employeeSalary = 0;
                }
                else {
                    this.message = false;

                }

            },
            languageChange: function (lan) {

                if (this.language == lan) {
                    if (this.loanPayment.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/AddLoanPayment');
                    }
                    else {
                        this.$swal({
                            itle: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
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
            CalculateInstallment: function () {
                
                var isCheck = parseFloat(this.loanPayment.deductionValue);
                if (this.loanPayment.installmentMethod == '% of Salary' || this.loanPayment.installmentMethod == '٪ من الراتب') {
                    if (isCheck < 0 || isCheck > 100) {
                        this.percentageError = true;
                    }
                    else {
                        this.percentageError = false;
                    }
                    //var value = (parseFloat(this.loanPayment.deductionValue) / parseFloat(this.loanPayment.recoveryLoanAmount)) * 100;
                    this.loanPayment.installmentBaseSalary = (parseFloat((this.loanPayment.employeeSalary / 100) * this.loanPayment.deductionValue)).toFixed(3).slice(0, -1)

                }
                else {
                    this.loanPayment.installmentBaseSalary = 0;
                }
                if (this.loanPayment.loanType == 'Advance' || this.loanPayment.loanType == 'تقدم') {
                    this.isDeductionRequired = false;

                }
                else {
                    if (this.loanPayment.deductionValue == 0 || this.loanPayment.deductionValue == null || this.loanPayment.deductionValue == '') {
                        this.isDeductionRequired = true;
                        return;
                    }
                    else {
                        this.isDeductionRequired = false;
                    }

                }



            },

            PaymentMethod: function () {
                this.loanPayment.deductionValue = 0;
                this.loanPayment.installmentBaseSalary = 0;
            },

            Close: function () {
                this.$router.push('/LoanPayment');
            },

            SaveLoanPayment: function () {
                
                this.loading = true;
                var root = this;


                if (this.loanPayment.loanType == 'Advance' || this.loanPayment.loanType == 'تقدم') {
                    this.isDeductionRequired = false;

                }
                else {
                    if (this.loanPayment.deductionValue == 0 || this.loanPayment.deductionValue == null || this.loanPayment.deductionValue == '') {
                        this.isDeductionRequired = true;
                        return;
                    }
                    else {
                        this.isDeductionRequired = false;
                    }

                }



                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (this.loanPayment.providentFundType == '1') {
                    this.loanPayment.providentFundType = 1
                }
                else {
                    this.loanPayment.providentFundType = 0
                }


                var installmentBaseSalary = parseFloat(this.loanPayment.installmentBaseSalary);
                var recoveryLoanAmount = parseFloat(this.loanPayment.recoveryLoanAmount);

                if (installmentBaseSalary > recoveryLoanAmount) {
                    this.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: 'Loan amount should be greater than or equal to the payment installment amount',
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        icon: 'error',
                        timer: 4000,
                        timerProgressBar: true,
                    });
                }
                else {
                    if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                        // Loan Type
                        if (this.loanPayment.loanType == 'Loan') {
                            this.loanPayment.loanType = 'Loan'
                        }
                        else if (this.loanPayment.loanType == 'Advance') {
                            this.loanPayment.loanType = 'Advance'
                        }
                        else if (this.loanPayment.loanType == 'Provident Fund') {
                            this.loanPayment.loanType = 'ProvidentFund'
                        }


                        // installmentMethod
                        if (this.loanPayment.installmentMethod == 'Fixed') {
                            this.loanPayment.installmentMethod = 'Fixed'
                        }
                        else if (this.loanPayment.installmentMethod == '% of Salary') {
                            this.loanPayment.installmentMethod = 'PercentageOfSalary'
                        }

                        // Recovery Method

                        if (this.loanPayment.recoveryMethod == 'Salary') {
                            this.loanPayment.recoveryMethod = 'Salary'
                        }
                        else if (this.loanPayment.recoveryMethod == 'Cash') {
                            this.loanPayment.recoveryMethod = 'Cash'
                        }
                    }
                    else {
                        // Loan Type
                        if (this.loanPayment.loanType == 'يقرض') {
                            this.loanPayment.loanType = 1
                        }
                        else if (this.loanPayment.loanType == 'تقدم') {
                            this.loanPayment.loanType = 2
                        }
                        else if (this.loanPayment.loanType == 'صندوق التوفير او الادخار') {
                            this.loanPayment.loanType = 3
                        }


                        // installmentMethod
                        if (this.loanPayment.installmentMethod == 'مثبت') {
                            this.loanPayment.installmentMethod = 1
                        }
                        else if (this.loanPayment.loanType == '٪ من الراتب') {
                            this.loanPayment.installmentMethod = 2
                        }

                        // Recovery Method

                        if (this.loanPayment.recoveryMethod == 'مرتب') {
                            this.loanPayment.recoveryMethod = 1
                        }
                        else if (this.loanPayment.recoveryMethod == 'نقدي') {
                            this.loanPayment.recoveryMethod = 2
                        }
                    }

                    var dateCheck = moment(this.loanPayment.loanTakenDate).isBefore(this.loanPayment.paymentStartDate)
                    if (!dateCheck) {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: 'Payment Start Date Required greater then Loan Taken Date',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                    else {

                        root.$https
                            .post('/Payroll/SaveLoanPaymentInformation', root.loanPayment, { headers: { "Authorization": `Bearer ${token}` } })
                            .then(response => {

                                if (response.data.isSuccess == true) {
                                    root.loading = false
                                    root.info = response.data.bpi

                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                        type: 'success',
                                        icon: 'success',
                                        showConfirmButton: false,
                                        timer: 1500,
                                        timerProgressBar: true,
                                    }).then(function (ok) {
                                        if (ok != null) {
                                            root.$router.push('/LoanPayment');
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

                                this.loading = false
                            })
                            .finally(() => this.loading = false)
                    }

                }


            }
        },
        created: function () {

            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.language = this.$i18n.locale;
            this.currency = localStorage.getItem('currency');

            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.loanType = ['Loan', 'Advance', 'Provident Fund'];
                this.installmentMethod = ['Fixed', '% of Salary'];
                this.recoveryMethod = ['Salary', 'Cash'];
            }
            else {
                this.loanType = ['صندوق التوفير او الادخار', 'تقدم', 'يقرض'];
                this.installmentMethod = ['مثبت', '٪ من الراتب'];
                this.recoveryMethod = ['مرتب', 'نقدي'];
            }
            if (this.$route.query.data == undefined) {
                this.loanPayment.loanTakenDate = moment().format('llll');
                this.loanPayment.paymentStartDate = moment().format('llll');
                this.daterander++;
                this.startDaterander++;
            }
            
            if (this.$route.query.data != undefined) {
                this.loanPayment = this.$route.query.data;
                if (this.loanPayment.providentFundType == 1) {
                    this.loanPayment.providentFundType = '1'
                }
                else {
                    this.loanPayment.providentFundType = '0'
                }
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    // Loan Type
                    if (this.loanPayment.loanType == 1) {
                        this.loanPayment.loanType = 'Loan'
                    }
                    else if (this.loanPayment.loanType == 2) {
                        this.loanPayment.loanType = 'Advance'
                    }
                    else if (this.loanPayment.loanType == 3) {
                        this.loanPayment.loanType = 'Provident Fund'
                    }


                    // installmentMethod
                    if (this.loanPayment.installmentMethod == 1) {
                        this.loanPayment.installmentMethod = '% of Salary'

                    }
                    else if (this.loanPayment.installmentMethod == 2) {
                        this.loanPayment.installmentMethod = 'Fixed'

                    }

                    // Recovery Method

                    if (this.loanPayment.recoveryMethod == 1) {
                        this.loanPayment.recoveryMethod = 'Salary'
                    }
                    else if (this.loanPayment.recoveryMethod == 2) {
                        this.loanPayment.recoveryMethod = 'Cash'
                    }
                }
                else {
                    // Loan Type
                    if (this.loanPayment.loanType == 1) {
                        this.loanPayment.loanType = 'يقرض'
                    }
                    else if (this.loanPayment.loanType == 2) {
                        this.loanPayment.loanType = 'تقدم'
                    }
                    else if (this.loanPayment.loanType == 3) {
                        this.loanPayment.loanType = 'صندوق التوفير او الادخار'
                    }


                    // installmentMethod
                    if (this.loanPayment.installmentMethod == 1) {
                        this.loanPayment.installmentMethod = 'مثبت'
                    }
                    else if (this.loanPayment.installmentMethod == 2) {
                        this.loanPayment.installmentMethod = '٪ من الراتب'
                    }

                    // Recovery Method

                    if (this.loanPayment.recoveryMethod == 1) {
                        this.loanPayment.recoveryMethod = 'مرتب'
                    }
                    else if (this.loanPayment.recoveryMethod == 2) {
                        this.loanPayment.recoveryMethod = 'نقدي'
                    }
                }
                this.CalculateInstallment();

                this.dateRender++;
            }
        }
    })
</script>

<style scoped>
    .input-group-append .input-group-text, .input-group-prepend .input-group-text {
        background-color: #e3ebf1;
        border: 1px solid #e3ebf1;
        color: #000000;
    }

    .input-group .form-control {
        border-left: 1px solid #e3ebf1;
    }

        .input-group .form-control:focus {
            border-left: 1px solid #3178F6;
        }

    .input-group-text {
        border-radius: 0;
    }
</style>