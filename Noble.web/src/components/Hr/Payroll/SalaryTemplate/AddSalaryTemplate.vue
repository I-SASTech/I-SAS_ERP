<template>
    <div class="row" v-if="isValid('CanAddSaleryTemplate') || isValid('CanEditSaleryTemplate')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="salaryTemplate.id === '00000000-0000-0000-0000-000000000000'"
                                    class="page-title">{{
                                            $t('AddSalaryTemplate.AddSalaryTemplate')
                                    }}</h4>
                                <h4 v-else class="page-title">{{ $t('AddSalaryTemplate.UpdatePayrollSchedule') }}</h4>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('AddSalaryTemplate.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />

            <div class="row mb-5">
                <div class="col-lg-6">

                    <div class="row form-group" v-bind:class="{ 'has-danger': v$.salaryTemplate.code.$error }">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline "> {{
                                    $t('AddSalaryTemplate.Code')
                            }} <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="v$.salaryTemplate.code.$model" disabled class="form-control" type="text">
                        </div>
                    </div>


                    <div class="row form-group" v-bind:class="{ 'has-danger': v$.salaryTemplate.code.$error }">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{
                                    $t('AddSalaryTemplate.StructureName')
                            }}</span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="v$.salaryTemplate.structureName.$model" class="form-control" type="text">
                        </div>
                    </div>

                </div>
                
                <hr class="hr-dashed hr-menu mt-0"/>
                <div class="col-lg-6">
                    <div class="row form-group">
                        <h4>{{ $t('AddSalaryTemplate.AddAllowance') }}</h4>

                        <div class="inline-fields col-lg-12">
                            <allowanceDropdown @update:modelValue="PushToList(allowanceId)" v-model="allowanceId">
                            </allowanceDropdown>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="  mt-3">
                            <table class="table mb-0" style="table-layout:fixed;">
                                <thead class="thead-light">
                                    <tr>
                                        <th>#</th>

                                        <th v-if="english == 'true'">
                                            {{ $t('AddSalaryTemplate.AllowanceNameEnglish') }}
                                        </th>
                                        <th v-if="isOtherLang()"
                                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('AddSalaryTemplate.AllowanceNameArabic') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AddSalaryTemplate.CalculateAmount') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AddSalaryTemplate.AmountPercentage') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AddSalaryTemplate.Taxable') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AddSalaryTemplate.Action') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(allowance, index) in allowanceList" v-bind:key="allowance.id">
                                        <td>
                                            {{ index + 1 }}
                                        </td>

                                        <td v-if="english == 'true'">
                                            {{ allowance.allowanceNameEn }}
                                        </td>
                                        <td v-if="isOtherLang()">
                                            {{ allowance.allowanceNameAr }}
                                        </td>

                                        <td style="width:20%">
                                            <multiselect :options="calculateAmountOptions"
                                                v-model="allowance.amountType"
                                                @update:modelValue="EditAllowance('amountType', allowance.amountType, index)"
                                                :show-labels="false" v-bind:placeholder="$t('SelectMethod')"
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                            </multiselect>
                                        </td>
                                        <td class="text-center" style="width:20%">
                                            <div class="input-group">
                                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                                    <i v-if="allowance.amountType == '% of Salary' || allowance.amountType == '٪ من الراتب'"
                                                        class="fa fa-percent"></i>
                                                    <i v-else>{{ currency }}</i></button>
                                                <input v-model="allowance.amount" type="text" class="form-control"
                                                    @blur="EditAllowance('amount', allowance.amount, index)"
                                                    @focus="$event.target.select()"
                                                    aria-label="Example text with button addon"
                                                    aria-describedby="button-addon1">
                                            </div>

                                        </td>
                                        <td class="text-center">
                                            <div class="checkbox form-check-inline mx-2 ">
                                                <input type="checkbox" id="isEditAllowance"  v-model="allowance.taxPlan" v-on:change="EditAllowance('taxPlan', $event.target.checked, index)">
                                                <label for="isEditAllowance"></label>
                                            </div>
                                            
                                        </td>
                                        <td class="text-center">
                                            <a href="javascript:void(0);" @click="RemoveAllowance(index)"><i
                                                    class="las la-trash-alt text-secondary font-16"></i></a>

                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>
                <hr  class="hr-dashed hr-menu mt-0"/>
                <div class="col-lg-6 mt-3">

                    <div class="row form-group">
                        <h4>{{ $t('AddSalaryTemplate.AddDeduction') }}</h4>

                        <div class="inline-fields col-lg-12">
                            <deductionDropdown @update:modelValue="PushToDeductionList(deductionId)" v-model="deductionId">
                            </deductionDropdown>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="  mt-3">
                            <table class="table mb-0" style="table-layout:fixed;">
                                <thead class="thead-light">
                                    <tr>
                                        <th>#</th>

                                        <th v-if="english == 'true'">
                                            {{ $t('AddSalaryTemplate.DeductionNameEnglish') }}
                                        </th>
                                        <th v-if="isOtherLang()"
                                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('AddSalaryTemplate.DeductionNameArabic') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AddSalaryTemplate.CalculateAmount') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AddSalaryTemplate.AmountPercentage') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AddSalaryTemplate.Taxable') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AddSalaryTemplate.Action') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(deduction, index) in deductionList" v-bind:key="deduction.id">
                                        <td>
                                            {{ index + 1 }}
                                        </td>
                                        <td v-if="english == 'true'">
                                            {{ deduction.nameInPayslip }}
                                        </td>
                                        <td v-if="isOtherLang()">
                                            {{ deduction.nameInPayslipArabic }}
                                        </td>
                                        <td style="width:20%">
                                            <multiselect :options="calculateAmountOptions"
                                                v-model="deduction.amountType"
                                                @update:modelValue="EditDeduction('amountType', deduction.amountType, index)"
                                                :show-labels="false" v-bind:placeholder="$t('SelectMethod')"
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                            </multiselect>
                                        </td>
                                        <td class="text-center" style="width:20%">
                                            <div class="input-group">
                                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                                    <i v-if="deduction.amountType == '% of Salary' || deduction.amountType == '٪ من الراتب'"
                                                        class="fa fa-percent"></i>
                                                    <i v-else>{{ currency }}</i></button>
                                                <input v-model="deduction.amount" type="text" class="form-control"
                                                    @blur="EditDeduction('amount', deduction.amount, index)"
                                                    @focus="$event.target.select()"
                                                    aria-label="Example text with button addon"
                                                    aria-describedby="button-addon1">
                                            </div>
                                        </td>
                                        
                                        <td class="text-center">
                                            <div class="checkbox form-check-inline mx-2 ">
                                                <input type="checkbox" id="isEditDeduction"  v-model="deduction.taxPlan" v-on:change="EditDeduction('taxPlan', $event.target.checked, index)">
                                                <label for="isEditDeduction"></label>
                                            </div>
                                          
                                        </td>
                                        <td class="text-center">
                                            <a href="javascript:void(0);" @click="RemoveDeduction(index)"><i
                                                    class="las la-trash-alt text-secondary font-16"></i></a>

                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>

                <hr  class="hr-dashed hr-menu mt-0"/>
                <div class="col-lg-6">
                    <div class="row form-group">


                        <h4>{{ $t('AddSalaryTemplate.AddContribution') }}</h4>

                        <div class="inline-fields col-lg-12">
                            <contributionDropdown @update:modelValue="PushToContributionList(contributionId)"
                                v-model="contributionId"></contributionDropdown>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="  mt-3">
                            <table class="table mb-0" style="table-layout:fixed;">
                                <thead class="thead-light">
                                    <tr>
                                        <th>#</th>

                                        <th v-if="english == 'true'">
                                            {{ $t('AddSalaryTemplate.ContributionNameEnglish') }}
                                        </th>
                                        <th v-if="isOtherLang()"
                                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('AddSalaryTemplate.ContributionNameArabic') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AddSalaryTemplate.CalculateAmount') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('AddSalaryTemplate.AmountPercentage') }}
                                        </th>

                                        <th class="text-center">
                                            {{ $t('AddSalaryTemplate.Action') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(contribution, index) in contributionList" v-bind:key="contribution.id">
                                        <td>
                                            {{ index + 1 }}
                                        </td>
                                        <td v-if="english == 'true'">
                                            {{ contribution.nameInPayslip }}
                                        </td>
                                        <td v-if="isOtherLang()">
                                            {{ contribution.nameInPayslipArabic }}
                                        </td>
                                        <td style="width:20%">
                                            <multiselect :options="calculateAmountOptions"
                                                v-model="contribution.amountType"
                                                @update:modelValue="EditContribution('amountType', contribution.amountType, index)"
                                                :show-labels="false" v-bind:placeholder="$t('SelectMethod')"
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                            </multiselect>
                                        </td>
                                        <td class="text-center" style="width:20%">
                                            <div class="input-group">
                                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                                    <i v-if="contribution.amountType == '% of Salary' || contribution.amountType == '٪ من الراتب'"
                                                        class="fa fa-percent"></i>
                                                    <i v-else>{{ currency }}</i></button>
                                                <input v-model="contribution.amount" type="text" class="form-control"
                                                    @blur="EditContribution('amount', contribution.amount, index)"
                                                    @focus="$event.target.select()"
                                                    aria-label="Example text with button addon"
                                                    aria-describedby="button-addon1">
                                            </div>
                                            
                                        </td>

                                        <td class="text-center">
                                            <a href="javascript:void(0);" @click="RemoveContribution(index)"><i
                                                    class="las la-trash-alt text-secondary font-16"></i></a>

                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>


            </div>
            <div class="row">
                <div class="col-lg-12 invoice-btn-fixed-bottom ">

                    <div class="button-items">
                        <button class="btn btn-outline-primary  mr-2" v-on:click="SaveSalaryTemplate"
                            v-if="salaryTemplate.id == '00000000-0000-0000-0000-000000000000' && isValid('CanAddSaleryTemplate')"
                            :disabled="v$.salaryTemplate.$invalid">
                            <i class="far fa-save"></i> {{ $t('AddSalaryTemplate.Save') }}
                        </button>

                        <button class="btn btn-outline-primary  mr-2" v-on:click="SaveSalaryTemplate"
                            v-if="salaryTemplate.id != '00000000-0000-0000-0000-000000000000' && isValid('CanEditSaleryTemplate')"
                            :disabled="v$.salaryTemplate.$invalid">
                            <i class="far fa-save"></i> {{ $t('AddSalaryTemplate.Update') }}
                        </button>

                        <button class="btn btn-danger  mr-2" v-on:click="Close()">
                            {{ $t('AddSalaryTemplate.Cancel') }}
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
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
// import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
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
             loading: false,
            currency: '',
            allowanceId: '',
            deductionId: '',
            contributionId: '',
            arabic: '',
            english: '',
            salaryTemplate: {
                id: '00000000-0000-0000-0000-000000000000',
                code: '',
                structureName: '',
                date: '',
                salaryAllowances: [],
                salaryDeductions: [],
                salaryContributions: [],
            },
            dateRender: 0,
            allowanceList: [],
            deductionList: [],
            contributionList: [],
            calculateAmountOptions: [],

            language: 'Nothing',
        }
    },
    validations() {
     return{
           salaryTemplate:
        {
            structureName: {
                required,
                maxLength: maxLength(50)
            },
            code: {
                maxLength: maxLength(30)
            },

        },
     }

    },
    methods: {
        GotoPage: function (link) {
                this.$router.push({path: link});
            },
        GetAutoCodeGenerator: function () {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Payroll/SalaryTemplateAutoGenerateNo', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    root.salaryTemplate.code = response.data;
                }
            });
        },

        EditContribution: function (label, value, index) {

            if (label == 'amount') {
                this.contributionList[index].amount = value;
            }
            else if (label == 'amountType') {
                this.contributionList[index].amountType = value;
            }



        },
        EditDeduction: function (label, value, index) {

            if (label == 'amount') {
                this.deductionList[index].amount = value;
            }
            else if (label == 'amountType') {
                this.deductionList[index].amountType = value;
            }
            else if (label == 'taxPlan') {
                this.deductionList[index].taxPlan = value;

            }


        },
        RemoveContribution: function (index) {
            this.contributionList.splice(index, 1);
        },
        RemoveDeduction: function (index) {
            this.deductionList.splice(index, 1);
        },
        RemoveAllowance: function (index) {
            this.allowanceList.splice(index, 1);
        },
        PushToList: function (list) {

            var alreadyExist = false;
            alreadyExist = this.allowanceList.find(function (x) {
                if (x.id === list.id)
                    return true;
                return false;
            });
            if (alreadyExist) {
                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Already Exist!' : 'موجود مسبقا!', 
                    text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Already Exist' : 'موجود مسبقا', 
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    icon: 'error',
                    timer: 4000,
                    timerProgressBar: true,
                });

            }
            else {
                if (list.taxPlan == 2) {
                    list.taxPlan = false;
                }
                else if (list.taxPlan == 1) {
                    list.taxPlan = true;
                }
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    if (list.amountType == 1) {
                        list.amountType = '% of Salary';
                    }
                    else if (list.amountType == 2) {
                        list.amountType = 'Fixed';
                    }



                }
                else {

                    if (list.amountType == 1) {
                        list.amountType = '٪ من الراتب';
                    }
                    else if (list.amountType == 2) {
                        list.amountType = 'مثبت';
                    }


                }
                this.allowanceList.push({
                    id: list.id,
                    allowanceNameEn: list.allowanceNameEn,
                    allowanceNameAr: list.allowanceNameAr,
                    amountType: list.amountType,
                    taxPlan: list.taxPlan,
                    amount: list.amount,

                });
            }


        },
        PushToDeductionList: function (result) {

            var alreadyExist = false;
            alreadyExist = this.deductionList.find(function (x) {
                if (x.id === result.id)
                    return true;
                return false;
            });
            if (alreadyExist) {
                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Already Exist!' : 'موجود مسبقا!',
                    text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Already Exist' : 'موجود مسبقا', 
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    icon: 'error',
                    timer: 4000,
                    timerProgressBar: true,
                });

            }
            else {
                if (result.taxPlan == 2) {
                    result.taxPlan = false;
                }
                else if (result.taxPlan == 1) {
                    result.taxPlan = true;
                }
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    if (result.amountType == 1) {
                        result.amountType = '% of Salary';
                    }
                    else if (result.amountType == 2) {
                        result.amountType = 'Fixed';
                    }


                }
                else {

                    if (result.amountType == 1) {
                        result.amountType = '٪ من الراتب';
                    }
                    else if (result.amountType == 2) {
                        result.amountType = 'مثبت';
                    }

                }
                this.deductionList.push({
                    id: result.id,
                    nameInPayslip: result.nameInPayslip,
                    nameInPayslipArabic: result.nameInPayslipArabic,
                    amountType: result.amountType,
                    taxPlan: result.taxPlan,
                    amount: result.amount,

                });
            }



        },
        PushToContributionList: function (result) {

            var alreadyExist = false;
            alreadyExist = this.contributionList.find(function (x) {
                if (x.id === result.id)
                    return true;
                return false;
            });
            if (alreadyExist) {
                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Already Exist!' : 'موجود مسبقا!',
                    text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Already Exist' : 'موجود مسبقا', 
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    icon: 'error',
                    timer: 4000,
                    timerProgressBar: true,
                });

            }
            else {

                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    if (result.amountType == 1) {
                        result.amountType = '% of Salary';
                    }
                    else if (result.amountType == 2) {
                        result.amountType = 'Fixed';
                    }


                }
                else {

                    if (result.amountType == 1) {
                        result.amountType = '٪ من الراتب';
                    }
                    else if (result.amountType == 2) {
                        result.amountType = 'مثبت';
                    }

                }
                this.contributionList.push({
                    id: result.id,
                    nameInPayslip: result.nameInPayslip,
                    nameInPayslipArabic: result.nameInPayslipArabic,
                    amountType: result.amountType,
                    amount: result.amount,

                });
            }



        },
        languageChange: function (lan) {

            if (this.language == lan) {
                if (this.salaryTemplate.id == '00000000-0000-0000-0000-000000000000') {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/addSale');
                }
                else {
                    this.$swal({
                        title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
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
        Close: function () {
            this.$router.push('/SalaryTemplate');
        },
        SaveSalaryTemplate: function () {

            this.loading = true;
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.salaryTemplate.salaryAllowances = [];
            root.salaryTemplate.salaryDeductions = [];
            root.salaryTemplate.salaryContributions = [];


            root.allowanceList.forEach(function (result) {
                root.salaryTemplate.salaryAllowances.push({
                    id: result.id,
                    amountType: result.amountType == '% of Salary' || result.amountType == '٪ من الراتب' ? 1 : 2,
                    taxPlan: result.taxPlan == true ? 1 : 2,
                    amount: result.amount,
                })
            });
            root.deductionList.forEach(function (result) {
                root.salaryTemplate.salaryDeductions.push({
                    id: result.id,
                    amountType: result.amountType == '% of Salary' || result.amountType == '٪ من الراتب' ? 1 : 2,
                    taxPlan: result.taxPlan == true ? 1 : 2,
                    amount: result.amount,
                })
            });
            root.contributionList.forEach(function (result) {
                root.salaryTemplate.salaryContributions.push({
                    id: result.id,
                    amountType: result.amountType == '% of Salary' || result.amountType == '٪ من الراتب' ? 1 : 2,
                    amount: result.amount,
                })
            });

            root.$https
                .post('/Payroll/SaveSalaryTemplate', root.salaryTemplate, { headers: { "Authorization": `Bearer ${token}` } })
                .then(response => {

                    if (response.data.isSuccess) {
                        root.loading = false

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        }).then(function (ok) {
                            if (ok != null) {
                                root.$router.push('/SalaryTemplate');
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
        this.$emit('update:modelValue', this.$route.name);
    },
    mounted: function () {
        var root = this;
        this.currency = localStorage.getItem('currency');
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        if (this.salaryTemplate.id == '00000000-0000-0000-0000-000000000000' || this.salaryTemplate.id == undefined || this.salaryTemplate.id == '')
            this.GetAutoCodeGenerator();
        this.language = this.$i18n.locale;

        if (this.$route.query.data != undefined) {


            this.salaryTemplate = this.$route.query.data;


            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                root.salaryTemplate.salaryAllowances.forEach(function (result) {
                    root.allowanceList.push({
                        id: result.id,
                        allowanceNameEn: result.allowanceNameEn,
                        allowanceNameAr: result.allowanceNameAr,
                        amountType: result.amountType == 1 ? '% of Salary' : 'Fixed',
                        taxPlan: result.taxPlan == 1 ? true : false,
                        amount: result.amount,
                    })
                });
                root.salaryTemplate.salaryDeductions.forEach(function (result) {
                    root.deductionList.push({
                        id: result.id,
                        nameInPayslip: result.nameInPayslip,
                        nameInPayslipArabic: result.nameInPayslipArabic,
                        amountType: result.amountType == 1 ? '% of Salary' : 'Fixed',
                        taxPlan: result.taxPlan == 1 ? true : false,
                        amount: result.amount,
                    })
                });
                root.salaryTemplate.salaryContributions.forEach(function (result) {
                    root.contributionList.push({
                        id: result.id,
                        nameInPayslip: result.nameInPayslip,
                        nameInPayslipArabic: result.nameInPayslipArabic,
                        amountType: result.amountType == 1 ? '% of Salary' : 'Fixed',
                        amount: result.amount,
                    })
                });
            }
            else {
                root.salaryTemplate.salaryAllowances.forEach(function (result) {
                    root.allowanceList.push({
                        id: result.id,
                        nameInPayslip: result.nameInPayslip,
                        nameInPayslipArabic: result.nameInPayslipArabic,
                        amountType: result.amountType == 1 ? '٪ من الراتب' : 'مثبت',
                        taxPlan: result.taxPlan == 1 ? true : false,
                        amount: result.amount,
                    })
                });
                root.salaryTemplate.salaryDeductions.forEach(function (result) {
                    root.deductionList.push({
                        id: result.id,
                        nameInPayslip: result.nameInPayslip,
                        nameInPayslipArabic: result.nameInPayslipArabic,
                        amountType: result.amountType == 1 ? '٪ من الراتب' : 'مثبت',
                        taxPlan: result.taxPlan == 1 ? true : false,
                        amount: result.amount,
                    })
                });
                root.salaryTemplate.salaryContributions.forEach(function (result) {
                    root.contributionList.push({
                        id: result.id,
                        amountType: result.amountType == 1 ? '٪ من الراتب' : 'مثبت',
                        amount: result.amount,
                    })
                });
            }




        }
        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
            this.calculateAmountOptions = ['% of Salary', 'Fixed'];
        }
        else {
            this.calculateAmountOptions = ['٪ من الراتب', 'مثبت'];
        }
    }
})
</script>
