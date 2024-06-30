<template>
    <modal :show="show" :modalLarge="true" :extraLarge="true">

        <div class="card">
            <div class="modal-header">
                <div class="row">
                    <div class="col-md-6">
                        <h5 class="modal-title DayHeading " id="myModalLabel" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">{{ $t('salaryTemplateModel.SalaryDetails') }}</h5>
                    </div>
                    <div class="col-md-6">
                        <p class="modal-title font-weight-bold" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">{{ $t('salaryTemplateModel.BaseSalary') }}: {{salaryTemplate.baseSalary}}</p>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="row mt-4">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <h6 class="text  font-weight-bolder ">
                            {{ $t('salaryTemplateModel.ProceedWithZero') }}
                            <toggle-button v-model="salaryTemplate.zeroSalary" v-on:change="ProceedZeroSalary()" class="pr-2 pl-2 pt-2" color="#3178F6" />
                        </h6>
                    </div>

                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" v-if="salaryTemplate.zeroSalary">
                        <div class="form-group" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                            <label >{{ $t('salaryTemplateModel.Reason') }}</label>
                            <textarea type="text" class="form-control" v-model="salaryTemplate.reason" />
                        </div>
                    </div>
                </div>

                <div v-if="!salaryTemplate.zeroSalary">

                    <div class="row mt-4">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <h6 class="text font-weight-bolder" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{ $t('salaryTemplateModel.AddAllowance') }} :<span class="text-danger"> *</span></h6>
                            <allowanceDropdown @update:modelValue="addProduct(allowanceId.id,allowanceId,'allowance')" v-model="allowanceId" />
                        </div>
                    </div>

                    <div class="row mt-2">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <table class="table add_table_list_bg" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <thead v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                    <tr>
                                        <th style="width:5%;">#</th>

                                        <th style="width:20%;" v-if="english=='true'">
                                            {{ $t('salaryTemplateModel.AllowanceNameEnglish')}}
                                        </th>
                                        <th style="width:20%;" v-if="isOtherLang()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('salaryTemplateModel.AllowanceNameArabic')}}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('salaryTemplateModel.Percentage')}}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('salaryTemplateModel.Amount')}}
                                        </th>
                                        <th style="width:10%;" class="text-center">
                                            {{ $t('salaryTemplateModel.Taxable') }}
                                        </th>
                                        <th style="width:5%;" class="text-center">
                                            {{ $t('salaryTemplateModel.Action') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                    <template v-for="(allowance,index) in salaryTemplate.salaryDetailList">
                                        <tr v-if="allowance.type=='allowance'" v-bind:key="index">
                                            <td>
                                                {{index+1}}
                                            </td>

                                            <td v-if="english=='true'">
                                                {{allowance.nameInPayslip}}
                                            </td>
                                            <td v-if="isOtherLang()">
                                                {{allowance.nameInPayslipArabic}}
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'padding: 5px 15px 5px 5px;' : 'padding: 5px 5px 5px 15px;' "><i class="fa fa-percent"></i></span>
                                                    </div>
                                                    <input type="number" @focus="$event.target.select()" class="form-control amount_field" @keyup="updateLineTable(allowance.percent, allowance ,'percent')" v-model="allowance.percent" />
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'padding: 5px 15px 5px 5px;' : 'padding: 5px 5px 5px 15px;' "> <i>{{currency}}</i></span>
                                                    </div>
                                                    <input type="number" @focus="$event.target.select()" class="form-control amount_field" @keyup="updateLineTable(allowance.amount, allowance ,'amount')" v-model="allowance.amount" />
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <input type="checkbox" class="checkBoxHeight" v-model="allowance.taxPlan"
                                                       v-on:change="updateBaseSalary()">
                                            </td>
                                            <td class="text-center">
                                                <a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon btn-round" v-on:click="removeItem(allowance.rowId)"><i class=" fa fa-trash"></i></a>
                                            </td>
                                        </tr>

                                    </template>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="row mt-4">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <h6 class="text  font-weight-bolder" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{ $t('salaryTemplateModel.AddDeduction')}} :<span class="text-danger"> *</span></h6>
                            <deductionDropdown @update:modelValue="addProduct(deductionId.id,deductionId,'deduction')" v-model="deductionId" />
                        </div>
                    </div>

                    <div class="row mt-2">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <table class="table add_table_list_bg" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <thead v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                    <tr>
                                        <th style="width:5%;">#</th>

                                        <th style="width:20%;" v-if="english=='true'">
                                            {{ $t('salaryTemplateModel.DeductionNameEnglish')}}
                                        </th>
                                        <th style="width:20%;" v-if="isOtherLang()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('salaryTemplateModel.DeductionNameArabic')}}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('salaryTemplateModel.Percentage')}}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('salaryTemplateModel.Amount')}}
                                        </th>
                                        <th style="width:10%;" class="text-center">
                                            {{ $t('salaryTemplateModel.Taxable') }}
                                        </th>
                                        <th style="width:5%;" class="text-center">
                                            {{ $t('salaryTemplateModel.Action')}}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                    <template v-for="(deduction ,index) in salaryTemplate.salaryDetailList">
                                        <tr v-if="deduction.type==='deduction'" v-bind:key="index">
                                            <td>
                                                {{index+1}}
                                            </td>
                                            <td v-if="english=='true'">
                                                {{deduction.nameInPayslip}}
                                            </td>
                                            <td v-if="isOtherLang()">
                                                {{deduction.nameInPayslipArabic}}
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'padding: 5px 15px 5px 5px;' : 'padding: 5px 5px 5px 15px;' "><i class="fa fa-percent"></i></span>
                                                    </div>
                                                    <input type="number" @focus="$event.target.select()" class="form-control amount_field" @keyup="updateLineTable(deduction.percent, deduction ,'percent')" v-model="deduction.percent" />
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'padding: 5px 15px 5px 5px;' : 'padding: 5px 5px 5px 15px;' "> <i>{{currency}}</i></span>
                                                    </div>
                                                    <input type="number" @focus="$event.target.select()" class="form-control amount_field" @keyup="updateLineTable(deduction.amount, deduction ,'amount')" v-model="deduction.amount" />
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <input type="checkbox" class="checkBoxHeight" v-model="deduction.taxPlan"
                                                       v-on:change="updateBaseSalary()">
                                            </td>
                                            <td class="text-center">
                                                <a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon btn-round" v-on:click="removeItem(deduction.rowId)"><i class=" fa fa-trash"></i></a>
                                            </td>
                                        </tr>

                                    </template>
                                </tbody>
                            </table>
                        </div>
                    </div>


                    <div class="row mt-4">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <h6 class="text  font-weight-bolder" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{ $t('salaryTemplateModel.AddContribution')}} :<span class="text-danger"> *</span></h6>
                            <contributionDropdown @update:modelValue="addProduct(contributionId.id,contributionId,'contribution')" v-model="contributionId" />
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <table class="table add_table_list_bg" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <thead v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                    <tr>
                                        <th style="width:5%;">#</th>

                                        <th style="width:25%;" v-if="english=='true'">
                                            {{ $t('salaryTemplateModel.ContributionNameEnglish')}}
                                        </th>
                                        <th style="width:25%;" v-if="isOtherLang()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('salaryTemplateModel.ContributionNameArabic')}}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('salaryTemplateModel.Percentage')}}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('salaryTemplateModel.Amount')}}
                                        </th>

                                        <th style="width:5%;" class="text-center">
                                            {{ $t('salaryTemplateModel.Action')}}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                    <template v-for="(contribution ,index) in salaryTemplate.salaryDetailList">
                                        <tr v-if="contribution.type=='contribution'" v-bind:key="index">
                                            <td>
                                                {{index+1}}
                                            </td>
                                            <td v-if="english=='true'">
                                                {{contribution.nameInPayslip}}
                                            </td>
                                            <td v-if="isOtherLang()">
                                                {{contribution.nameInPayslipArabic}}
                                            </td>
                                            <td style="width:20%">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'padding: 5px 15px 5px 5px;' : 'padding: 5px 5px 5px 15px;' "><i class="fa fa-percent"></i> </span>
                                                    </div>
                                                    <input type="number" @focus="$event.target.select()" class="form-control amount_field" @keyup="updateLineTable(contribution.percent, contribution ,'percent')" v-model="contribution.percent" />
                                                </div>
                                            </td>
                                            <td class="text-center" style="width:20%">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'padding: 5px 15px 5px 5px;' : 'padding: 5px 5px 5px 15px;' "><i>{{currency}}</i></span>
                                                    </div>
                                                    <input type="number" @focus="$event.target.select()" class="form-control amount_field" @keyup="updateLineTable(contribution.amount, contribution ,'amount')" v-model="contribution.amount" />
                                                </div>
                                            </td>

                                            <td class="text-center">
                                                <a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon btn-round" v-on:click="removeItem(contribution.rowId)"><i class=" fa fa-trash"></i></a>
                                            </td>
                                        </tr>

                                    </template>
                                </tbody>
                            </table>
                        </div>
                    </div>


                    <div class="row mt-4">
                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                            <h6 class="text  font-weight-bolder" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{ $t('salaryTemplateModel.IncomeTax')}} :<span class="text-danger"> *</span></h6>
                        </div>

                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                            <toggle-button v-model="salaryTemplate.incomeTax" v-on:change="addIncomeTax()" class="pr-2 pl-2 pt-2" color="#3178F6" />
                        </div>
                    </div>

                    <div class="row mt-2" v-if="salaryTemplate.incomeTax">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <table class="table add_table_list_bg" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <thead v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                    <tr>
                                        <th style="width:5%;">#</th>

                                        <th style="width:20%;" v-if="english=='true'">
                                            {{ $t('salaryTemplateModel.DeductionNameEnglish')}}
                                        </th>
                                        <th style="width:20%;" v-if="isOtherLang()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('salaryTemplateModel.DeductionNameArabic')}}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('salaryTemplateModel.Percentage')}}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('salaryTemplateModel.Amount')}}
                                        </th>
                                        <th style="width:10%;" class="text-center">
                                            {{ $t('salaryTemplateModel.AutoManual')}}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                    <template v-for="(deduction ,index) in salaryTemplate.salaryDetailList">
                                        <tr v-if="deduction.type==='Income Tax'" v-bind:key="index">
                                            <td>
                                                {{index+1}}
                                            </td>
                                            <td v-if="english=='true'">
                                                {{deduction.nameInPayslip}}
                                            </td>
                                            <td v-if="isOtherLang()">
                                                {{deduction.nameInPayslipArabic}}
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'padding: 5px 15px 5px 5px;' : 'padding: 5px 5px 5px 15px;' "><i class="fa fa-percent"></i></span>
                                                    </div>
                                                    <input type="number" @focus="$event.target.select()" disabled class="form-control amount_field" @keyup="updateLineTable(deduction.percent, deduction ,'percent')" v-model="deduction.percent" />
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'padding: 5px 15px 5px 5px;' : 'padding: 5px 5px 5px 15px;' "> <i>{{currency}}</i></span>
                                                    </div>
                                                    <input type="number" @focus="$event.target.select()" v-bind:disabled="salaryTemplate.autoIncomeTax && deduction.nameInPayslip=='Income Tax'" class="form-control amount_field" @keyup="updateLineTable(deduction.amount, deduction ,'amount')" v-model="deduction.amount" />
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <toggle-button v-on:change="updateBaseSalary()" v-model="salaryTemplate.autoIncomeTax" class="pr-2 pl-2 pt-2" color="#3178F6" />
                                            </td>
                                        </tr>

                                    </template>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="row mt-2">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <hr />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                            <h5 class="font-weight-bold">{{ $t('salaryTemplateModel.FinalSalaryDetails')}}</h5>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                            <table class="table table-borderless">
                                <tr>
                                    <td>{{ $t('salaryTemplateModel.GrossSalary')}}</td>
                                    <td class="text-right">{{ parseFloat(summary.grossSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr>
                                    <td>{{ $t('salaryTemplateModel.TaxableSalary')}}</td>
                                    <td class="text-right">{{ parseFloat(summary.taxableSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr>
                                    <td>{{ $t('salaryTemplateModel.TaxPerPeriod')}}</td>
                                    <td class="text-right">{{ parseFloat(summary.taxPerPeriod).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr>
                                    <td class="text-success font-weight-bold">{{ $t('salaryTemplateModel.NetSalary')}}</td>
                                    <td class="text-right text-success font-weight-bold">{{parseFloat(summary.netSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                            </table>
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="col-sm-12 arabicLanguage" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                        <button type="button" class="btn btn-danger" v-on:click="Close"> {{ $t('salaryTemplateModel.Cancel') }} </button>
                        <button type="button" class="btn btn-success" v-bind:disabled="v$.salaryTemplate.$invalid" v-on:click="SaveSalaryTemplate"><i class="far fa-save"></i> {{ $t('salaryTemplateModel.Update') }} </button>
                    </div>
                </div>
            </div>
             <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </modal>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'

    // import { required, requiredIf } from 'vuelidate/lib/validators'
    import { required, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'

    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],

        props: ['show', 'data'],
setup() {
            return { v$: useVuelidate() }
        },
         components: {
            Loading
        },
        data: function () {
            return {
                currency: '',
                allowanceId: '',
                deductionId: '',
                contributionId: '',
                arabic: '',
                english: '',
                salaryTemplate: {
                    id: '00000000-0000-0000-0000-000000000000',
                    salaryType: '',
                    payPeriodId: '',
                    baseSalary: 0,
                    salaryTemplateId: '',
                    employeeId: '',
                    startingDate: '',
                    autoIncomeTax: true,
                    incomeTax: false,
                    zeroSalary: false,
                    reason: '',
                    salaryTaxSlabList: '',
                    salaryDetailList: [],
                },
                summary: {
                    allowanceAmount: 0,
                    deductionAmount: 0,
                    contributionAmount: 0,
                    grossSalary: 0,
                    taxableSalary: 0,
                    taxPerPeriod: 0,
                    netSalary: 0
                },
                salaryTypeOptions: [],
                salaryTaxSlab: [],
                dateRender: 0,
                language: 'Nothing',
            }
        },

        validations() {
         return{
               salaryTemplate:
            {
                salaryType: {
                    required
                },
                reason: {
                    required: requiredIf(function () {
                            return this.salaryTemplate.zeroSalary ? true : false;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.zeroSalary)
                    //         return true;
                    //     return false;
                    // }),
                },
            },
         }

        },

        methods: {
            ProceedZeroSalary: function () {
                this.calcuateSummary();
            },

            addIncomeTax: function () {
                
                if (this.salaryTemplate.incomeTax) {
                    this.salaryTemplate.autoIncomeTax = true;
                    var rowId = this.createUUID();
                    this.salaryTemplate.salaryDetailList.push({
                        rowId: rowId,
                        itemId: rowId,
                        type: 'Income Tax',
                        nameInPayslip: 'Income Tax',
                        nameInPayslipArabic: 'ضريبة الدخل',
                        amountType: 2,
                        taxPlan: false,
                        amountOrPercent: 0,
                        amount: 0,
                        percent: 0,
                    });

                    var item = this.salaryTemplate.salaryDetailList.find((x) => {
                        return x.rowId == rowId;
                    });
                    this.updateLineTotal(item.amountType, item);
                }
                else {
                    this.salaryTemplate.salaryDetailList = this.salaryTemplate.salaryDetailList.filter((prod) => {
                        return prod.type != 'Income Tax';
                    });
                    this.salaryTemplate.autoIncomeTax = false;
                    this.calcuateSummary();
                }


            },

            calcuateSummary: function () {
                //Gross Salary
                this.summary.grossSalary = this.salaryTemplate.salaryDetailList.reduce(function (sum, record) {
                    if (record.type == 'allowance') {
                        return sum + parseFloat(record.amount);
                    }
                    else {
                        return sum
                    }
                }, 0);

                this.summary.grossSalary = parseFloat(this.summary.grossSalary + parseFloat(this.salaryTemplate.baseSalary)).toFixed(0);

                //Taxable Salary
                var taxableGrossSalary = this.salaryTemplate.salaryDetailList.reduce(function (sum, record) {
                    if (record.type == 'allowance' && record.taxPlan) {
                        return sum + parseFloat(record.amount);
                    }
                    else {
                        return sum
                    }
                }, 0);

                var nonTaxableDeductionSalary = this.salaryTemplate.salaryDetailList.reduce(function (sum, record) {
                    if (record.type == 'deduction' && !record.taxPlan) {
                        return sum + parseFloat(record.amount);
                    }
                    else {
                        return sum
                    }
                }, 0);
                this.summary.taxableSalary = parseFloat((taxableGrossSalary - nonTaxableDeductionSalary) + parseFloat(this.salaryTemplate.baseSalary)).toFixed(0);



                //Tax Per Period
                var incomeTax = this.salaryTemplate.salaryDetailList.find(x => x.type == 'Income Tax' && x.nameInPayslip == 'Income Tax');
                if (incomeTax != undefined) {

                    if (this.salaryTemplate.autoIncomeTax) {

                        if (this.salaryTaxSlab != null && this.salaryTaxSlab.length > 0) {
                            var totalTaxableSalary = this.summary.taxableSalary * 12;

                            var slab = this.salaryTaxSlab.find(x => x.incomeFrom < totalTaxableSalary && (x.incomeTo >= totalTaxableSalary || x.incomeTo == 0));
                            if (slab != undefined) {
                                this.summary.taxPerPeriod = (((parseFloat(totalTaxableSalary) - parseFloat(slab.incomeFrom)) / 100) * parseFloat(slab.rate)) + parseFloat(slab.fixedTax);
                                this.summary.taxPerPeriod = this.summary.taxPerPeriod / 12;
                                incomeTax.amount = this.summary.taxPerPeriod;
                            }
                        }
                        else {
                            this.summary.taxPerPeriod = 0;
                        }
                    }
                    else {
                        this.summary.taxPerPeriod = this.salaryTemplate.salaryDetailList.reduce(function (sum, record) {
                            if (record.type == 'Income Tax') {
                                return sum + parseFloat(record.amount);
                            }
                            else {
                                return sum
                            }
                        }, 0);
                    }

                }
                else {
                    this.summary.taxPerPeriod = 0;
                }


                //Net Salary
                var deduction = this.salaryTemplate.salaryDetailList.reduce(function (sum, record) {
                    if (record.type == 'deduction') {
                        return sum + parseFloat(record.amount);
                    }
                    else {
                        return sum
                    }
                }, 0);

                var contribution = this.salaryTemplate.salaryDetailList.reduce(function (sum, record) {
                    if (record.type == 'contribution') {
                        return sum + parseFloat(record.amount);
                    }
                    else {
                        return sum
                    }
                }, 0);
                this.summary.netSalary = parseFloat(parseFloat(this.summary.grossSalary) - parseFloat(deduction + contribution + this.summary.taxPerPeriod)).toFixed(0);
            },

            updateBaseSalary: function () {

                var root = this;
                if (this.salaryTemplate.salaryDetailList.length > 0) {
                    this.salaryTemplate.salaryDetailList.forEach(function (result) {
                        if (result.amountType == 1) {
                            root.updateLineTable(result.percent, result, 'percent');
                        }
                        if (result.amountType == 2) {
                            root.updateLineTable(result.amount, result, 'amount');
                        }
                    });
                }
                else {
                    this.calcuateSummary();
                }
            },

            updateLineTable: function (e, item, prop) {

                if (prop == 'percent') {
                    item.percent = e;
                    item.amount = (this.salaryTemplate.baseSalary / 100) * e;
                    item.amountType = 1;
                }

                if (prop == 'amount') {
                    item.amount = Math.round(e);
                    item.percent = this.salaryTemplate.baseSalary == 0 ? 0 : (e / this.salaryTemplate.baseSalary) * 100;
                    item.amountType = 2;
                }

                this.calcuateSummary();
            },

            updateLineTotal: function (e, item) {

                if (e == 1) {
                    item.percent = item.amountOrPercent;
                    item.amount = (this.salaryTemplate.baseSalary / 100) * item.amountOrPercent;
                }

                if (e == 2) {
                    item.amount = item.amountOrPercent;
                    item.percent = this.salaryTemplate.baseSalary == 0 ? 0 : (item.amountOrPercent / this.salaryTemplate.baseSalary) * 100;
                }

                this.calcuateSummary();
            },

            addProduct: function (itemId, newItem, prop) {
                var rowId = '';
                var existingItem = this.salaryTemplate.salaryDetailList.find((x) => {
                    return x.itemId == itemId;
                });

                if (existingItem == undefined) {

                    if (prop == 'allowance') {
                        rowId = this.createUUID();
                        this.salaryTemplate.salaryDetailList.push({
                            rowId: rowId,
                            itemId: itemId,
                            type: prop,
                            nameInPayslip: newItem.allowanceNameEn,
                            nameInPayslipArabic: newItem.allowanceNameAr,
                            amountType: newItem.amountType,
                            taxPlan: newItem.taxPlan == 1 ? true : false,
                            amountOrPercent: newItem.amount,
                            amount: 0,
                            percent: 0,
                        });

                        var item = this.salaryTemplate.salaryDetailList.find((x) => {
                            return x.rowId == rowId;
                        });
                        this.updateLineTotal(item.amountType, item);

                    }

                    if (prop == 'deduction') {

                        rowId = this.createUUID();
                        this.salaryTemplate.salaryDetailList.push({
                            rowId: rowId,
                            itemId: itemId,
                            type: prop,
                            nameInPayslip: newItem.nameInPayslip,
                            nameInPayslipArabic: newItem.nameInPayslipArabic,
                            amountType: newItem.amountType,
                            taxPlan: newItem.taxPlan == 1 ? true : false,
                            amountOrPercent: newItem.amount,
                            amount: 0,
                            percent: 0,
                        });

                        var deduction = this.salaryTemplate.salaryDetailList.find((x) => {
                            return x.rowId == rowId;
                        });
                        this.updateLineTotal(deduction.amountType, deduction);
                    }

                    if (prop == 'contribution') {
                        rowId = this.createUUID();
                        this.salaryTemplate.salaryDetailList.push({
                            rowId: rowId,
                            itemId: itemId,
                            type: prop,
                            nameInPayslip: newItem.nameInPayslip,
                            nameInPayslipArabic: newItem.nameInPayslipArabic,
                            amountType: newItem.amountType,
                            taxPlan: false,
                            amountOrPercent: newItem.amount,
                            amount: 0,
                            percent: 0,
                        })

                        var contribution = this.salaryTemplate.salaryDetailList.find((x) => {
                            return x.rowId == rowId;
                        });
                        this.updateLineTotal(contribution.amountType, contribution);
                    }
                }
            },

            removeItem: function (id) {

                this.salaryTemplate.salaryDetailList = this.salaryTemplate.salaryDetailList.filter((prod) => {
                    return prod.rowId != id;
                });

                this.calcuateSummary();
            },

            createUUID: function () {

                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },

            GetTaxSlab: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Payroll/SalaryTaxSlabDetail', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data != null) {
                            root.salaryTaxSlab = response.data.salaryTaxSlabList;
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });

            },

            languageChange: function (lan) {

                if (this.language == lan) {
                    if (this.salaryTemplate.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/AddEmployeeSalary');
                    }
                    else {
                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text:(this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
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
                this.$emit('close');
            },

            SaveSalaryTemplate: function () {
                this.loading = true;

                this.summary.allowanceAmount = this.salaryTemplate.salaryDetailList.reduce(function (sum, record) {
                    if (record.type == 'allowance') {
                        return sum + parseFloat(record.amount);
                    }
                    else {
                        return sum
                    }
                }, 0);

                this.summary.deductionAmount = this.salaryTemplate.salaryDetailList.reduce(function (sum, record) {
                    if (record.type == 'deduction') {
                        return sum + parseFloat(record.amount);
                    }
                    else {
                        return sum
                    }
                }, 0);

                this.summary.contributionAmount = this.salaryTemplate.salaryDetailList.reduce(function (sum, record) {
                    if (record.type == 'contribution') {
                        return sum + parseFloat(record.amount);
                    }
                    else {
                        return sum
                    }
                }, 0);
                
                this.$emit('update:modelValue', this.summary, this.salaryTemplate);
            }
        },

        created: function () {
            
            if (this.data != undefined) {
                this.salaryTemplate.id = this.data.id;
                this.salaryTemplate.salaryType = this.data.salaryType;
                //this.salaryTemplate.payPeriodId = this.data.payPeriodId;
                this.salaryTemplate.baseSalary = this.data.baseSalary;
                //this.salaryTemplate.salaryTemplateId = this.data.salaryTemplateId;
                //this.salaryTemplate.employeeId = this.data.employeeId;
                //this.salaryTemplate.startingDate = this.data.startingDate;
                this.salaryTemplate.autoIncomeTax = this.data.autoIncomeTax;
                this.salaryTemplate.incomeTax = this.data.incomeTax;
                this.salaryTemplate.salaryTaxSlabList = this.data.salaryTaxSlabList;
                this.salaryTemplate.zeroSalary = this.data.zeroSalary;
                this.salaryTemplate.reason = this.data.reason;
                this.salaryTaxSlab = this.data.salaryTaxSlabList;

                this.data.salaryDetailList.forEach(function (result) {
                    result.rowId = result.id;
                });

                this.salaryTemplate.salaryDetailList = this.data.salaryDetailList;
                this.updateBaseSalary();
            }
            this.GetTaxSlab();

        },
        mounted: function () {
            this.currency = localStorage.getItem('currency');
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.language = this.$i18n.locale;

            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.salaryTypeOptions = ['Salary Based', 'Hourly Based'];
            }
            else {
                this.salaryTypeOptions = ['Salary Based', 'Hourly Based'];
            }
        }
    }
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