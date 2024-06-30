<template>
    <div class="row" v-if="isValid('CanAddEmployeeSalary') || isValid('CanEditEmployeeSalary')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">

                                <h4 class="page-title">{{ $t('AddEmployeeSalary.SalaryDetails') }}</h4>
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
                            <span id="ember695" class="tooltip-container text-dashed-underline "> {{
                                    $t('AddEmployeeSalary.SalaryType')
                            }} <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect :options="salaryTypeOptions" v-model="salaryTemplate.salaryType"
                                @update:modelValue="GetPeriod()" :show-labels="false"
                                v-bind:placeholder="$t('AddEmployeeSalary.SelectType')">
                            </multiselect>
                        </div>
                    </div>


                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{
                                    $t('AddEmployeeSalary.PayPeriod')
                            }}</span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <payroll-schedule-dropdown v-model="salaryTemplate.payPeriodId" :isEmployee="true"
                                @update:modelValue="GetpayPeriod" :key="periodRender" :modelValue="salaryTemplate.payPeriodId"
                                :salaryType="salaryTemplate.salaryType" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{
                                    $t('AddEmployeeSalary.Employee')
                            }}</span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <employeeDropdown v-model="salaryTemplate.employeeId"
                                :modelValue="salaryTemplate.employeeId" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{
                                    $t('AddEmployeeSalary.GosiRate')
                            }}</span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" @focus="$event.target.select()"
                                v-model="salaryTemplate.gosiRate" type="number" />
                        </div>
                    </div>


                </div>
                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline "> {{
                                    $t('AddEmployeeSalary.BaseSalary')
                            }} <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input @focus="$event.target.select()" @keyup="updateBaseSalary()"
                                v-model="salaryTemplate.baseSalary" class="form-control" type="number">
                        </div>
                    </div>


                    <div class="row form-group"
                        v-if="salaryTemplate.salaryType == 'Hourly Based' || salaryTemplate.salaryType == 'بالساعة'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{
                                    $t('AddEmployeeSalary.WeekdayHourlySalary')
                            }}</span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="salaryTemplate.weekdayHourlySalary" @focus="$event.target.select()"
                                class="form-control" type="number">
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{
                                    $t('AddEmployeeSalary.SalaryTemplate')
                            }}</span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <salary-template-dropdown v-model="salaryTemplate.salaryTemplateId"
                                @update:modelValue="GetTemplate(salaryTemplate.salaryTemplateId)"
                                :modelValue="salaryTemplate.salaryTemplateId" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{
                                    $t('AddEmployeeSalary.StartingDate')
                            }}</span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="salaryTemplate.startingDate" />
                        </div>
                    </div>


                </div>

                <hr class="hr-dashed hr-menu mt-0" />
                <div class="col-lg-6">
                    <div class="row form-group">
                        <h4>{{ $t('AddEmployeeSalary.AddAllowance') }}</h4>

                        <div class="inline-fields col-lg-12">
                            <allowanceDropdown @update:modelValue="addProduct(allowanceId.id, allowanceId, 'allowance')"
                                v-model="allowanceId" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class=" table-responsive mt-3">
                            <table class="table mb-0" style="table-layout:fixed;">
                                <thead class="thead-light">
                                    <tr>
                                        <th style="width:5%;">#</th>

                                        <th style="width:20%;" v-if="english == 'true'">
                                            {{ $t('AddEmployeeSalary.AllowanceNameEnglish') }}
                                        </th>
                                        <th style="width:20%;" v-if="isOtherLang()"
                                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('AddEmployeeSalary.AllowanceNameArabic') }}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.Percentage') }}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.Amount') }}
                                        </th>
                                        <th style="width:10%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.Taxable') }}
                                        </th>
                                        <th style="width:5%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.Action') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <template v-for="(allowance, index) in salaryTemplate.salaryDetailList">
                                        <tr v-if="allowance.type == 'allowance'" v-bind:key="index">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td v-if="english == 'true'">
                                                {{ allowance.nameInPayslip }}
                                            </td>
                                            <td v-if="isOtherLang()">
                                                {{ allowance.nameInPayslipArabic }}
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <button class="btn btn-secondary" type="button" id="button-addon1">
                                                        <i class="fa fa-percent"></i></button>
                                                    <input v-model="allowance.percent" type="number"
                                                        class="form-control"
                                                        @keyup="updateLineTable(allowance.percent, allowance, 'percent')"
                                                        @focus="$event.target.select()"
                                                        aria-label="Example text with button addon"
                                                        aria-describedby="button-addon1">
                                                </div>

                                            </td>
                                            <td class="text-center">
                                                <div class="input-group">
                                                    <button class="btn btn-secondary" type="button" id="button-addon1">
                                                        <i>{{ currency }}</i></button>
                                                    <input v-model="allowance.amount" type="number" class="form-control"
                                                        @keyup="updateLineTable(allowance.amount, allowance, 'amount')"
                                                        @focus="$event.target.select()"
                                                        aria-label="Example text with button addon"
                                                        aria-describedby="button-addon1">
                                                </div>


                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox form-check-inline mx-2 ">
                                                    <input type="checkbox" id="isEditAllowance"
                                                        v-model="allowance.taxPlan" v-on:change="updateBaseSalary()">
                                                    <label for="isEditAllowance"></label>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <a href="javascript:void(0);" @click="removeItem(allowance.rowId)"><i
                                                        class="las la-trash-alt text-secondary font-16"></i></a>
                                            </td>
                                        </tr>
                                    </template>
                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>
                <hr class="hr-dashed hr-menu mt-0" />
                <div class="col-lg-6 mt-3">
                    
                    <div class="row form-group">
                        <h4>{{ $t('AddEmployeeSalary.AddDeduction') }}</h4>

                        <div class="inline-fields col-lg-12">
                            <deductionDropdown @update:modelValue="addProduct(deductionId.id, deductionId, 'deduction')" v-model="deductionId" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class=" table-responsive mt-3">
                            <table class="table mb-0" style="table-layout:fixed;">
                                <thead class="thead-light">
                                    <tr>
                                        <th style="width:5%;">#</th>

                                        <th style="width:20%;" v-if="english == 'true'">
                                            {{ $t('AddEmployeeSalary.DeductionNameEnglish') }}
                                        </th>
                                        <th style="width:20%;" v-if="isOtherLang()"
                                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('AddEmployeeSalary.DeductionNameArabic') }}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.Percentage') }}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.Amount') }}
                                        </th>
                                        <th style="width:10%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.Taxable') }}
                                        </th>
                                        <th style="width:5%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.Action') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <template v-for="(deduction, index) in salaryTemplate.salaryDetailList">
                                    <tr v-if="deduction.type === 'deduction'" v-bind:key="index">
                                        <td>
                                            {{ index + 1 }}
                                        </td>
                                        <td v-if="english == 'true'">
                                            {{ deduction.nameInPayslip }}
                                        </td>
                                        <td v-if="isOtherLang()">
                                            {{ deduction.nameInPayslipArabic }}
                                        </td>
                                        <td>
                                            <div class="input-group">
                                                    <button class="btn btn-secondary" type="button" id="button-addon1">
                                                        <i class="fa fa-percent"></i></button>
                                                    <input v-model="deduction.percent" type="number"
                                                        class="form-control"
                                                        @keyup="updateLineTable(deduction.percent, deduction, 'percent')"
                                                        @focus="$event.target.select()"
                                                        aria-label="Example text with button addon"
                                                        aria-describedby="button-addon1">
                                                </div>

                                            
                                        </td>
                                        <td class="text-center">
                                            <div class="input-group">
                                                    <button class="btn btn-secondary" type="button" id="button-addon1">
                                                        <i>{{ currency }}</i></button>
                                                    <input v-model="deduction.amount" type="number" class="form-control"
                                                        @keyup="updateLineTable(deduction.amount, deduction, 'amount')"
                                                        @focus="$event.target.select()"
                                                        aria-label="Example text with button addon"
                                                        aria-describedby="button-addon1">
                                                </div>
                                            
                                        </td>
                                        <td class="text-center">
                                            <div class="checkbox form-check-inline mx-2 ">
                                                    <input type="checkbox" id="isEditDeduction"
                                                        v-model="deduction.taxPlan" v-on:change="updateBaseSalary()">
                                                    <label for="isEditDeduction"></label>
                                                </div>
                                        </td>
                                        <td class="text-center">
                                            <a href="javascript:void(0);" @click="removeItem(deduction.rowId)"><i
                                                        class="las la-trash-alt text-secondary font-16"></i></a>
                                        </td>
                                    </tr>
                                    </template>
                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>

                <hr class="hr-dashed hr-menu mt-0" />
                <div class="col-lg-6">
                    <div class="row form-group">


                        <h4>{{ $t('AddEmployeeSalary.AddContribution') }}</h4>

                        <div class="inline-fields col-lg-12">
                            <contributionDropdown @update:modelValue="addProduct(contributionId.id, contributionId, 'contribution')"
                v-model="contributionId" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class=" table-responsive mt-3">
                            <table class="table mb-0" style="table-layout:fixed;">
                                <thead class="thead-light">
                                    <tr>
                                        <th style="width:5%;">#</th>

                                        <th style="width:25%;" v-if="english == 'true'">
                                            {{ $t('AddEmployeeSalary.ContributionNameEnglish') }}
                                        </th>
                                        <th style="width:25%;" v-if="isOtherLang()">
                                            {{ $t('AddEmployeeSalary.ContributionNameArabic') }}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.Percentage') }}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.Amount') }}
                                        </th>

                                        <th style="width:5%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.Action') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <template v-for="(contribution, index) in salaryTemplate.salaryDetailList">
                                        <tr v-if="contribution.type == 'contribution'" v-bind:key="index">
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
                                                <div class="input-group">
                                                    <button class="btn btn-secondary" type="button" id="button-addon1">
                                                        <i class="fa fa-percent"></i></button>
                                                    <input v-model="contribution.percent" type="number"
                                                        class="form-control"
                                                        @keyup="updateLineTable(contribution.percent, contribution, 'percent')"
                                                        @focus="$event.target.select()"
                                                        aria-label="Example text with button addon"
                                                        aria-describedby="button-addon1">
                                                </div>
                                                
                                            </td>
                                            <td class="text-center" style="width:20%">
                                                <div class="input-group">
                                                    <button class="btn btn-secondary" type="button" id="button-addon1">
                                                        <i>{{ currency }}</i></button>
                                                    <input v-model="contribution.amount" type="number" class="form-control"
                                                        @keyup="updateLineTable(contribution.amount, contribution, 'amount')"
                                                        @focus="$event.target.select()"
                                                        aria-label="Example text with button addon"
                                                        aria-describedby="button-addon1">
                                                </div>

                                            </td>

                                            <td class="text-center">
                                                <a href="javascript:void(0);" @click="removeItem(contribution.rowId)"><i
                                                        class="las la-trash-alt text-secondary font-16"></i></a>
                                               
                                            </td>
                                        </tr>

                                    </template>

                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>

                <hr class="hr-dashed hr-menu mt-0" />
                <div class="form-group col-12">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="incomTax" v-model="salaryTemplate.incomeTax" v-on:change="addIncomeTax()">
                            <label for="incomTax"> {{ $t('AddEmployeeSalary.IncomeTax') }} </label>
                        </div>
                </div>


                <div class="row" v-if="salaryTemplate.incomeTax">
                    <div class="col-lg-12">
                        <div class=" table-responsive mt-3">
                            <table class="table mb-0" style="table-layout:fixed;">
                                <thead class="thead-light">
                                    <tr>
                                        <th style="width:5%;">#</th>

                                        <th style="width:20%;" v-if="english == 'true'">
                                            {{ $t('AddEmployeeSalary.DeductionNameEnglish') }}
                                        </th>
                                        <th style="width:20%;" v-if="isOtherLang()"
                                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('AddEmployeeSalary.DeductionNameArabic') }}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.Percentage') }}
                                        </th>
                                        <th style="width:20%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.Amount') }}
                                        </th>
                                        <th style="width:10%;" class="text-center">
                                            {{ $t('AddEmployeeSalary.AutoManual') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <template v-for="(deduction, index) in salaryTemplate.salaryDetailList">
                                        <tr v-if="deduction.type === 'Income Tax'" v-bind:key="index">
                                            <td>
                                                {{ index + 1 }}
                                            </td>
                                            <td v-if="english == 'true'">
                                                {{ deduction.nameInPayslip }}
                                            </td>
                                            <td v-if="isOtherLang()">
                                                {{ deduction.nameInPayslipArabic }}
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <button class="btn btn-secondary" type="button" id="button-addon1">
                                                        <i class="fa fa-percent"></i></button>
                                                    <input v-model="deduction.percent" type="number" disabled
                                                        class="form-control"
                                                        @keyup="updateLineTable(deduction.percent, deduction, 'percent')"
                                                        @focus="$event.target.select()"
                                                        aria-label="Example text with button addon"
                                                        aria-describedby="button-addon1">
                                                </div>
                                                
                                            </td>
                                            <td class="text-center">
                                                <div class="input-group">
                                                    <button class="btn btn-secondary" type="button" id="button-addon1">
                                                        <i>{{ currency }}</i></button>
                                                    <input  v-model="deduction.amount" type="number" class="form-control"
                                                    v-bind:disabled="salaryTemplate.autoIncomeTax && deduction.nameInPayslip == 'Income Tax'"
                                                    @keyup="updateLineTable(deduction.amount, deduction, 'amount')"
                                                        @focus="$event.target.select()"
                                                        aria-label="Example text with button addon"
                                                        aria-describedby="button-addon1">
                                                </div>

                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox form-check-inline mx-2 ">
                                                <input type="checkbox" id="autoIncomeTax"  v-model="salaryTemplate.autoIncomeTax" v-on:change="updateBaseSalary()">
                                                <label for="autoIncomeTax"></label>
                                            </div>
                                               
                                            </td>
                                        </tr>

                                    </template>

                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>


                <hr class="hr-dashed hr-menu mt-0" />
                


                <div class="row" >
                    <div class="form-group col-6">
                        <h5>{{ $t('AddEmployeeSalary.FinalSalaryDetails') }}</h5>
                    </div>
                    <div class="col-lg-6">
                        <div class=" table-responsive mt-3">
                            <table class="table mb-0" style="table-layout:fixed;">
                                <tr>
                                    <td>{{ $t('AddEmployeeSalary.GrossSalary') }}</td>
                                    <td class="text-right">{{
                                            parseFloat(summary.grossSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
                                    }}</td>
                                </tr>
                                <tr>
                                    <td>{{ $t('AddEmployeeSalary.TaxableSalary') }}</td>
                                    <td class="text-right">{{
                                            parseFloat(summary.taxableSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
                                    }}
                                    </td>
                                </tr>
                                <tr>
                                    <td>{{ $t('AddEmployeeSalary.TaxPerPeriod') }}</td>
                                    <td class="text-right">{{
                                            parseFloat(summary.taxPerPeriod).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
                                    }}
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-success font-weight-bold">{{ $t('AddEmployeeSalary.NetSalary') }}</td>
                                    <td class="text-right text-success font-weight-bold">{{ summary.netSalary }}</td>
                                </tr>
                            </table>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">

                <div class="col-lg-12 invoice-btn-fixed-bottom ">

                    <div class="button-items">
                        <button class="btn btn-outline-primary  mr-2" v-on:click="SaveSalaryTemplate"
                            v-if="salaryTemplate.id == '00000000-0000-0000-0000-000000000000' && isValid('CanAddEmployeeSalary')"
                            :disabled="v$.salaryTemplate.$invalid">
                            <i class="far fa-save"></i> {{ $t('AddEmployeeSalary.Save') }}
                        </button>

                        <button class="btn btn-outline-primary  mr-2" v-on:click="SaveSalaryTemplate"
                            v-if="salaryTemplate.id != '00000000-0000-0000-0000-000000000000' && isValid('CanEditEmployeeSalary')"
                            :disabled="v$.salaryTemplate.$invalid">
                            <i class="far fa-save"></i> {{ $t('AddEmployeeSalary.Update') }}
                        </button>

                        <button class="btn btn-danger  mr-2" v-on:click="Close()">
                            {{ $t('AddEmployeeSalary.Cancel') }}
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
import clickMixin from '@/Mixins/clickMixin';
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
// import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
import Multiselect from 'vue-multiselect';


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
                weekdayHourlySalary: 0,
                weekendDayHourlySalary: 0,
                salaryTemplateId: '',
                employeeId: '',
                startingDate: '',
                autoIncomeTax: true,
                incomeTax: false,
                gosiRate: 0,
                salaryDetailList: [],
            },
            summary: {
                grossSalary: 0,
                taxableSalary: 0,
                taxPerPeriod: 0,
                netSalary: 0
            },
            salaryTypeOptions: [],
            salaryTaxSlab: [],
            dateRender: 0,
            periodRender: 0,
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
            employeeId: {
                required
            },
            payPeriodId: {
                required
            },
            startingDate: {
                required
            },

        },
       }

    },

    methods: {
        GotoPage: function (link) {
                this.$router.push({path: link});
            },
        GetPeriod: function () {
            //if (this.salaryTemplate.salaryType =='Hourly Based') {
            //    this.salaryTemplate.salaryDetailList = [];
            //    this.salaryTemplate.incomeTax = false;
            //    this.salaryTemplate.autoIncomeTax = true;
            //    this.salaryTemplate.salaryTemplateId = '';
            //}
            this.periodRender++;
        },
        GetpayPeriod: function (data) {
            if (data == null) {
                this.salaryTemplate.payPeriodId = '';
            }
            else {
                this.salaryTemplate.payPeriodId = data.id;
            }
        },

        GetTemplate: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Payroll/SalaryTemplateDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {

                        root.salaryTemplate.salaryDetailList = [];
                        root.salaryTemplate.incomeTax = false;
                        root.salaryTemplate.autoIncomeTax = false;

                        if ((root.$i18n.locale == 'en' || root.isLeftToRight())) {
                            response.data.salaryAllowances.forEach(function (result) {
                                root.addProduct(result.id, result, 'allowance');
                            });

                            response.data.salaryDeductions.forEach(function (result) {
                                root.addProduct(result.id, result, 'deduction');
                            });

                            response.data.salaryContributions.forEach(function (result) {
                                root.addProduct(result.id, result, 'contribution');
                            });
                        }
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });

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
            /*Gross Salary*/
            this.summary.grossSalary = this.salaryTemplate.salaryDetailList.reduce(function (sum, record) {
                if (record.type == 'allowance') {
                    return sum + parseFloat(record.amount);
                }
                else {
                    return sum
                }
            }, 0);

            this.summary.grossSalary = parseFloat(this.summary.grossSalary + parseFloat(Math.round(this.salaryTemplate.baseSalary))).toFixed(0);

            /*Taxable Salary*/
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
            this.summary.taxableSalary = parseFloat((taxableGrossSalary - nonTaxableDeductionSalary) + parseFloat(Math.round(this.salaryTemplate.baseSalary))).toFixed(0);



            /*Tax Per Period*/
            var incomeTax = this.salaryTemplate.salaryDetailList.find(x => x.type == 'Income Tax' && x.nameInPayslip == 'Income Tax');
            if (incomeTax != undefined) {

                if (this.salaryTemplate.autoIncomeTax) {
                    console.log(this.salaryTaxSlab);

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


            /*Net Salary*/
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
            this.summary.netSalary = parseFloat(parseFloat(this.summary.grossSalary) - parseFloat(deduction + contribution + this.summary.taxPerPeriod)).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
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
                item.amount = (Math.round(this.salaryTemplate.baseSalary) / 100) * e;
                item.amountType = 1;
            }

            if (prop == 'amount') {
                item.amount = Math.round(e);
                item.percent = Math.round(this.salaryTemplate.baseSalary) == 0 ? 0 : (e / Math.round(this.salaryTemplate.baseSalary)) * 100;
                item.amountType = 2;
            }

            this.calcuateSummary();
        },

        updateLineTotal: function (e, item) {

            if (e == 1) {
                item.percent = item.amountOrPercent;
                item.amount = (Math.round(this.salaryTemplate.baseSalary) / 100) * item.amountOrPercent;
            }

            if (e == 2) {
                item.amount = item.amountOrPercent;
                item.percent = Math.round(this.salaryTemplate.baseSalary) == 0 ? 0 : (item.amountOrPercent / Math.round(this.salaryTemplate.baseSalary)) * 100;
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

        Close: function () {
            this.$router.push('/EmployeeSalary');
        },

        SaveSalaryTemplate: function () {

            this.loading = true;
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https
                .post('/Payroll/SaveEmployeeSalary', root.salaryTemplate, { headers: { "Authorization": `Bearer ${token}` } })
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
                                root.$router.push('/EmployeeSalary');
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
                            text: error.response.data,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,

                        });

                    this.loading = false
                })
                .finally(() => this.loading = false)
        }
    },

    created: function () {
        this.$emit('update:modelValue', this.$route.name);

        if (this.$route.query.data != undefined) {

            var data = this.$route.query.data;
            this.salaryTemplate.id = data.id;
            this.salaryTemplate.salaryType = data.salaryType;
            this.salaryTemplate.payPeriodId = data.payPeriodId;
            this.salaryTemplate.baseSalary = data.baseSalary;
            this.salaryTemplate.weekdayHourlySalary = data.weekdayHourlySalary;
            this.salaryTemplate.weekendDayHourlySalary = data.weekendDayHourlySalary;
            this.salaryTemplate.salaryTemplateId = data.salaryTemplateId;
            this.salaryTemplate.employeeId = data.employeeId;
            this.salaryTemplate.startingDate = data.startingDate;
            this.salaryTemplate.autoIncomeTax = data.autoIncomeTax;
            this.salaryTemplate.incomeTax = data.incomeTax;
            this.salaryTemplate.gosiRate = data.gosiRate;
            this.salaryTaxSlab = data.salaryTaxSlabList;

            data.salaryDetailList.forEach(function (result) {
                result.rowId = result.id;
            });

            this.salaryTemplate.salaryDetailList = data.salaryDetailList;
            this.updateBaseSalary();
        }
        else {
            this.GetTaxSlab();
        }
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
            this.salaryTypeOptions = ['على أساس الراتب', 'بالساعة'];
        }
    }
})
</script>
