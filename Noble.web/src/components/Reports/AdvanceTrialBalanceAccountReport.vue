<template>
    <div class="row" v-if="isValid('CanViewAccountLedger')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('TrialBalanceAccountReport.TrialBalanceReport') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item">
                                        <a href="javascript:void(0);">
                                            {{
                                        $t('TrialBalanceAccountReport.Home')
                                            }}
                                        </a>
                                    </li>
                                    <li class="breadcrumb-item active">
                                        {{
 $t('TrialBalanceAccountReport.TrialBalanceReport')
                                        }}
                                    </li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="PrintRdlc()" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="fas fa-print font-14"></i>
                                    {{ $t('Print') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />
            <div class="row align-items-end">
                <div class=" col-lg-4   form-group">
                    <label>Select a Period:</label>
                    <div class="d-flex">
                        <multiselect v-model="reportOpt" :disabled="disablePeriod" :key="disablePeriodRender"
                                     :options="['Today', 'This Week', 'This Month', 'This Quarter', 'This Year', 'Yesterday', 'Previous Week', 'Previous Month', 'Previous Quarter', 'Previous Year', 'Custom']"
                                     :show-labels="false" v-bind:placeholder="$t('Select an Option')" @update:modelValue="GetDateTime()">
                        </multiselect>
                        <a class="btn btn-soft-primary mx-2" v-on:click="AdvanceFilters()" id="button-addon2"
                           value="Advance Filter">
                            <i class="fa fa-filter"></i>
                        </a>
                    </div>
                </div>
                <div class=" col-lg-4 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>
                <div class=" col-lg-4  form-group">
                    <button v-if="(reportOpt == '' || reportOpt == null) && (numberOfPeriods == 0)" disabled
                            href="javascript:void(0);" class="btn btn-outline-primary me-2">
                        Apply Filters
                    </button>
                    <button v-else v-on:click="GetInventoryList()" href="javascript:void(0);"
                            class="btn btn-outline-primary me-2">
                        Apply Filters
                    </button>
                    <a v-on:click="RemoveFilters()" href="javascript:void(0);" class="btn btn-outline-danger">
                        Clear Filters
                    </a>
                </div>
            </div>
            <div class="row">
                <div class=" col-lg-4   form-group" v-if="showDates">
                    <label>From Date:</label>
                    <datepicker v-model="fromDate" :period="true" :key="rander" />
                </div>
                <div class=" col-lg-4   form-group" v-if="showDates">
                    <label>To Date:</label>
                    <datepicker v-model="toDate" :period="true" :key="rander" />
                </div>
                <div class=" col-lg-4   form-group" v-if="advanceFilters">
                    <label>Compare With:</label>
                    <multiselect v-model="compareWith"
                                 :options="['Previous Year(s)', 'Previous Period(s)', 'Previous Quarter(s)', 'Previous Month(s)']"
                                 :show-labels="false" v-bind:placeholder="$t('Select an Option')" @update:modelValue="GetPeriods()">
                    </multiselect>
                    <div class="mt-1 d-flex align-items-center">
                        <input type="checkbox" class="me-1"><span>Arrange period/year from latest to oldest</span>
                    </div>
                </div>
                <div class=" col-lg-4   form-group" v-if="isPreviousYear && advanceFilters">
                    <label>Number of Year(s)</label>
                    <multiselect v-model="numberOfPeriods" :options="['1', '2', '3', '4', '5']" :show-labels="false"
                                 v-bind:placeholder="$t('Select an Option')">
                    </multiselect>
                </div>
                <div class=" col-lg-4   form-group" v-if="isPreviousPeriod && advanceFilters">
                    <label>Number of Period(s)</label>
                    <multiselect v-model="numberOfPeriods" :options="financialYears" :show-labels="false"
                                 v-bind:placeholder="$t('Select an Option')">
                    </multiselect>
                </div>
                <div class=" col-lg-4   form-group" v-if="isPreviousQuarter && advanceFilters">
                    <label>Number of Quarter(s)</label>
                    <multiselect v-model="numberOfPeriods" :options="['1', '2', '3', '4']" :show-labels="false"
                                 v-bind:placeholder="$t('Select an Option')">
                    </multiselect>
                </div>
                <div class=" col-lg-4   form-group" v-if="isPreviousMonth && advanceFilters">
                    <label>Number of Month(s)</label>
                    <multiselect v-model="numberOfPeriods"
                                 :options="['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12']" :show-labels="false"
                                 v-bind:placeholder="$t('Select an Option')">
                    </multiselect>
                </div>


            </div>
            <hr class="hr-dashed hr-menu mt-0" />
            <div class="d-flex border" v-if="showComparisonTable">
                <div class="card col-md-5 border-0" v-if="trialBalanceAccountCompareList1.length > 0">
                    <div class="card-body">
                        <div class="table-responsive" v-for="item in trialBalanceAccountCompareList1" :key="item.compareWith">
                            <div class="row">
                                <h5 class="opac">{{ item.compareWithValue }}</h5>
                            </div>
                            <table class="table table-striped table-hover table_list_bg">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            {{ $t('TrialBalanceAccountReport.Code') }}
                                        </th>
                                        <th>
                                            {{ $t('TrialBalanceAccountReport.Name') }}
                                        </th>

                                        <th>
                                            {{ $t('TrialBalanceAccountReport.CostCenter') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(trans, index) in item.transactionDetail" v-bind:key="index">
                                        <td>
                                            {{ index + 1 }}<br />
                                        </td>
                                        <td>
                                            {{ trans.accountCode }}
                                        </td>
                                        <td v-if="trans.accountName != ''">
                                            {{ getFirst30Characters(trans.accountName) }}
                                        </td>
                                        <td v-else>
                                            {{ getFirst30Characters(trans.accountNameArabic) }}
                                        </td>
                                        <td>
                                            {{ getFirst30Characters(trans.costCenter) }}
                                        </td>

                                    </tr>
                                    <tr style="font-size:13px;">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td class="text-right">
                                            <h6>{{ $t('TrialBalanceAccountReport.Total') }}</h6>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="d-flex bor" id="bx" ref="scrollable" @pointerdown="onPointerDown" @pointerup="onPointerUp">
                    <div class="card col-md-5 pointers border-0" v-if="trialBalanceAccountCompareList1.length > 0">
                        <div class="card-body">
                            <div class="table-responsive" v-for="item in trialBalanceAccountCompareList1" :key="item.compareWith">
                                <div class="row">
                                    <h5>{{ item.compareWithValue }}</h5>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Debit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Credit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(trans, index) in item.transactionDetail" v-bind:key="index">
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.debit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.credit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ trans.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(trans.total) }}
                                            </td>
                                        </tr>
                                        <tr style="font-size:13px;">
                                            <td class="text-right">
                                                <h6>{{ item.totalDebit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6>{{ item.totalCredit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6> {{ item.total > 0 ? 'Dr' : 'Cr' }} {{ item.total }}</h6>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="card col-md-5 pointers border-0" v-if="trialBalanceAccountCompareList2.length > 0">
                        <div class="card-body">
                            <div class="table-responsive" v-for="item in trialBalanceAccountCompareList2" :key="item.compareWith">
                                <div class="row">
                                    <h5>{{ item.compareWithValue }}</h5>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Debit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Credit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(trans, index) in item.transactionDetail" v-bind:key="index">
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.debit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.credit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ trans.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(trans.total) }}
                                            </td>
                                        </tr>
                                        <tr style="font-size:13px;">
                                            <td class="text-right">
                                                <h6>{{ item.totalDebit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6>{{ item.totalCredit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6> {{ item.total > 0 ? 'Dr' : 'Cr' }} {{ item.total }}</h6>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="card col-md-5 pointers border-0" v-if="trialBalanceAccountCompareList3.length > 0">
                        <div class="card-body">
                            <div class="table-responsive" v-for="item in trialBalanceAccountCompareList3" :key="item.compareWith">
                                <div class="row">
                                    <h5>{{ item.compareWithValue }}</h5>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Debit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Credit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(trans, index) in item.transactionDetail" v-bind:key="index">
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.debit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.credit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ trans.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(trans.total) }}
                                            </td>
                                        </tr>
                                        <tr style="font-size:13px;">
                                            <td class="text-right">
                                                <h6>{{ item.totalDebit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6>{{ item.totalCredit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6> {{ item.total > 0 ? 'Dr' : 'Cr' }} {{ item.total }}</h6>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="card col-md-5 pointers border-0" v-if="trialBalanceAccountCompareList4.length > 0">
                        <div class="card-body">
                            <div class="table-responsive" v-for="item in trialBalanceAccountCompareList4" :key="item.compareWith">
                                <div class="row">
                                    <h5>{{ item.compareWithValue }}</h5>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Debit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Credit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(trans, index) in item.transactionDetail" v-bind:key="index">
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.debit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.credit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ trans.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(trans.total) }}
                                            </td>
                                        </tr>
                                        <tr style="font-size:13px;">
                                            <td class="text-right">
                                                <h6>{{ item.totalDebit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6>{{ item.totalCredit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6> {{ item.total > 0 ? 'Dr' : 'Cr' }} {{ item.total }}</h6>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="card col-md-5 pointers border-0" v-if="trialBalanceAccountCompareList5.length > 0">
                        <div class="card-body">
                            <div class="table-responsive" v-for="item in trialBalanceAccountCompareList5" :key="item.compareWith">
                                <div class="row">
                                    <h5>{{ item.compareWithValue }}</h5>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Debit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Credit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(trans, index) in item.transactionDetail" v-bind:key="index">
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.debit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.credit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ trans.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(trans.total) }}
                                            </td>
                                        </tr>
                                        <tr style="font-size:13px;">
                                            <td class="text-right">
                                                <h6>{{ item.totalDebit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6>{{ item.totalCredit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6> {{ item.total > 0 ? 'Dr' : 'Cr' }} {{ item.total }}</h6>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="card col-md-5 pointers border-0" v-if="trialBalanceAccountCompareList6.length > 0">
                        <div class="card-body">
                            <div class="table-responsive" v-for="item in trialBalanceAccountCompareList6" :key="item.compareWith">
                                <div class="row">
                                    <h5>{{ item.compareWithValue }}</h5>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Debit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Credit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(trans, index) in item.transactionDetail" v-bind:key="index">
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.debit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.credit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ trans.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(trans.total) }}
                                            </td>
                                        </tr>
                                        <tr style="font-size:13px;">
                                            <td class="text-right">
                                                <h6>{{ item.totalDebit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6>{{ item.totalCredit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6> {{ item.total > 0 ? 'Dr' : 'Cr' }} {{ item.total }}</h6>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="card col-md-5 pointers border-0" v-if="trialBalanceAccountCompareList7.length > 0">
                        <div class="card-body">
                            <div class="table-responsive" v-for="item in trialBalanceAccountCompareList7" :key="item.compareWith">
                                <div class="row">
                                    <h5>{{ item.compareWithValue }}</h5>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Debit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Credit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(trans, index) in item.transactionDetail" v-bind:key="index">
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.debit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.credit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ trans.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(trans.total) }}
                                            </td>
                                        </tr>
                                        <tr style="font-size:13px;">
                                            <td class="text-right">
                                                <h6>{{ item.totalDebit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6>{{ item.totalCredit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6> {{ item.total > 0 ? 'Dr' : 'Cr' }} {{ item.total }}</h6>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="card col-md-5 pointers border-0" v-if="trialBalanceAccountCompareList8.length > 0">
                        <div class="card-body">
                            <div class="table-responsive" v-for="item in trialBalanceAccountCompareList8" :key="item.compareWith">
                                <div class="row">
                                    <h5>{{ item.compareWithValue }}</h5>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Debit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Credit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(trans, index) in item.transactionDetail" v-bind:key="index">
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.debit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.credit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ trans.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(trans.total) }}
                                            </td>
                                        </tr>
                                        <tr style="font-size:13px;">
                                            <td class="text-right">
                                                <h6>{{ item.totalDebit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6>{{ item.totalCredit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6> {{ item.total > 0 ? 'Dr' : 'Cr' }} {{ item.total }}</h6>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="card col-md-5 pointers border-0" v-if="trialBalanceAccountCompareList9.length > 0">
                        <div class="card-body">
                            <div class="table-responsive" v-for="item in trialBalanceAccountCompareList9" :key="item.compareWith">
                                <div class="row">
                                    <h5>{{ item.compareWithValue }}</h5>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Debit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Credit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(trans, index) in item.transactionDetail" v-bind:key="index">
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.debit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.credit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ trans.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(trans.total) }}
                                            </td>
                                        </tr>
                                        <tr style="font-size:13px;">
                                            <td class="text-right">
                                                <h6>{{ item.totalDebit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6>{{ item.totalCredit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6> {{ item.total > 0 ? 'Dr' : 'Cr' }} {{ item.total }}</h6>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="card col-md-5 pointers border-0" v-if="trialBalanceAccountCompareList10.length > 0">
                        <div class="card-body">
                            <div class="table-responsive" v-for="item in trialBalanceAccountCompareList10" :key="item.compareWith">
                                <div class="row">
                                    <h5>{{ item.compareWithValue }}</h5>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Debit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Credit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(trans, index) in item.transactionDetail" v-bind:key="index">
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.debit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.credit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ trans.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(trans.total) }}
                                            </td>
                                        </tr>
                                        <tr style="font-size:13px;">
                                            <td class="text-right">
                                                <h6>{{ item.totalDebit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6>{{ item.totalCredit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6> {{ item.total > 0 ? 'Dr' : 'Cr' }} {{ item.total }}</h6>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="card col-md-5 pointers border-0" v-if="trialBalanceAccountCompareList11.length > 0">
                        <div class="card-body">
                            <div class="table-responsive" v-for="item in trialBalanceAccountCompareList11" :key="item.compareWith">
                                <div class="row">
                                    <h5>{{ item.compareWithValue }}</h5>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Debit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Credit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(trans, index) in item.transactionDetail" v-bind:key="index">
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.debit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.credit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ trans.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(trans.total) }}
                                            </td>
                                        </tr>
                                        <tr style="font-size:13px;">
                                            <td class="text-right">
                                                <h6>{{ item.totalDebit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6>{{ item.totalCredit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6> {{ item.total > 0 ? 'Dr' : 'Cr' }} {{ item.total }}</h6>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="card col-md-5 pointers border-0" v-if="trialBalanceAccountCompareList12.length > 0">
                        <div class="card-body">
                            <div class="table-responsive" v-for="item in trialBalanceAccountCompareList12" :key="item.compareWith">
                                <div class="row">
                                    <h5>{{ item.compareWithValue }}</h5>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Debit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Credit') }}
                                            </th>
                                            <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('TrialBalanceAccountReport.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(trans, index) in item.transactionDetail" v-bind:key="index">
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.debit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(trans.credit) }}
                                            </td>
                                            <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ trans.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(trans.total) }}
                                            </td>
                                        </tr>
                                        <tr style="font-size:13px;">
                                            <td class="text-right">
                                                <h6>{{ item.totalDebit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6>{{ item.totalCredit }}</h6>
                                            </td>
                                            <td class="text-right">
                                                <h6> {{ item.total > 0 ? 'Dr' : 'Cr' }} {{ item.total }}</h6>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="card col-md-12" v-if="showTable">
                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table table-striped table-hover table_list_bg">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            {{ $t('TrialBalanceAccountReport.Code') }}
                                        </th>
                                        <th>
                                            {{ $t('TrialBalanceAccountReport.Name') }}
                                        </th>

                                        <th>
                                            {{ $t('TrialBalanceAccountReport.CostCenter') }}
                                        </th>
                                        <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{ $t('TrialBalanceAccountReport.Debit') }}
                                        </th>
                                        <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{ $t('TrialBalanceAccountReport.Credit') }}
                                        </th>
                                        <th v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{ $t('TrialBalanceAccountReport.Balance') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(trans, index) in transactionDetail" v-bind:key="index">
                                        <td>
                                            {{ index + 1 }}<br />
                                        </td>
                                        <td>
                                            {{ trans.accountCode }}
                                        </td>
                                        <td v-if="trans.accountName != ''">
                                            {{ trans.accountName }}
                                        </td>
                                        <td v-else>
                                            {{ trans.accountNameArabic }}
                                        </td>
                                        <td>
                                            {{ trans.costCenter }}
                                        </td>
                                        <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{ nonNegative(trans.debit) }}
                                        </td>
                                        <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{ nonNegative(trans.credit) }}
                                        </td>
                                        <td v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{ trans.total > 0 ? 'Dr' : 'Cr' }} {{ nonNegative(trans.total) }}
                                        </td>
                                    </tr>
                                    <tr style="font-size:13px;">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td class="text-right">
                                            <h6>{{ $t('TrialBalanceAccountReport.Total') }}</h6>
                                        </td>
                                        <td class="text-right">
                                            <h6>{{ getCreditTotal }}</h6>
                                        </td>
                                        <td class="text-right">
                                            <h6>{{ getDebitTotal }}</h6>
                                        </td>
                                        <td class="text-right">
                                            <h6> {{ getLineTotal > 0 ? 'Dr' : 'Cr' }} {{ getLineTotal }}</h6>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <invoicedetailsprint :show="show1" v-if="show1" :reportsrc="reportsrc1" :changereport="changereportt" @close="show1=false" @IsSave="IsSave" />

        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>

    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    import Multiselect from "vue-multiselect";


    export default {

        mixins: [clickMixin],
        props: ['formNameProp'],
        components: {
            Multiselect,

        },
        data: function () {
            return {
                allowBranches: false,
                branchIds: [],
                branchId: '',
                showDates: false,
                financialYears: [],
                showComparisonTable: false,
                compareWith: '',
                isPreviousYear: false,
                isPreviousPeriod: false,
                isPreviousQuarter: false,
                isPreviousMonth: false,
                numberOfPeriods: '',

                showTable: false,
                loading: false,
                rander: 0,

                disablePeriod: false,
                disablePeriodRender: 0,

                reportOpt: "",
                dateRender: 0,

                reportsrc1: '',
                changereportt: 0,
                show1: false,
                render: 0,
                accountId: '00000000-0000-0000-0000-000000000000',
                fromDate: '',
                toDate: '',
                transactionDetail: [],

                isShown: false,
                formName: 'Account Ledger',
                printRender: 0,
                advanceFilters: false,
                combineDate: '',
                dateType: '',
                language: 'Nothing',
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                sumDebit: 0,
                sumCredit: 0,
                sumTotal: 0,
                compareWithValue: '',
                costCenters: [],
                trialBalanceAccountCompareList1: [],
                trialBalanceAccountCompareList2: [],
                trialBalanceAccountCompareList3: [],
                trialBalanceAccountCompareList4: [],
                trialBalanceAccountCompareList5: [],
                trialBalanceAccountCompareList6: [],
                trialBalanceAccountCompareList7: [],
                trialBalanceAccountCompareList8: [],
                trialBalanceAccountCompareList9: [],
                trialBalanceAccountCompareList10: [],
                trialBalanceAccountCompareList11: [],
                trialBalanceAccountCompareList12: [],
                pointerFrom: 0,
                elementFrom: 0,
                pointerDown: false,
            }
        },
        watch: {
            branchIds: function () {
                var root = this;
                this.branchId = '';
                this.branchIds.forEach(function (result) {
                    root.branchId = root.branchId == '' ? result.id : root.branchId + ',' + result.id;
                })
                this.GetInventoryList();
            }
        },
        computed: {
            getCreditTotal: function () {
                var sum = 0;
                this.transactionDetail.forEach(function (x) {
                    sum += x.credit;
                })
                return parseFloat(Math.abs(sum)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },
            getDebitTotal: function () {
                var sum = 0;
                this.transactionDetail.forEach(function (x) {
                    sum += x.debit;
                })
                return parseFloat(Math.abs(sum)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },
            getLineTotal: function () {
                var sum = 0;
                this.transactionDetail.forEach(function (x) {
                    sum += x.total;
                })
                return parseFloat(Math.abs(sum)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },

        },

        methods: {
            getFirst30Characters: function (str) {
                if (str === null) {
                    return '';
                }
                return str.substring(0, Math.min(25, str.length));
            },
            AdvanceFilters: function () {
                this.fromDate = moment().format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
                this.reportOpt = '';
                this.advanceFilters = !this.advanceFilters;
                this.showDates = false;
                this.disablePeriod = !this.disablePeriod;
                this.disablePeriodRender++;
                this.compareWith = "";
                this.numberOfPeriods = "";
            },
            RemoveFilters: function () {
                this.reportOpt = '';
                this.show = false;
                this.numberOfPeriods = '';
                this.compareWith = '';
                this.showTable = false;
                this.showComparisonTable = false;
                this.advanceFilters = false;
                this.disablePeriod = false;
                this.disablePeriodRender++;
                this.disablePeriod = false;
                this.disablePeriodRender++;
                this.showDates = false;
            },
            convertDate: function (date) {
                return moment(date).format("l");
            },
            GetDateTime: function () {

                if (this.reportOpt == 'Today') {
                    this.fromDate = moment().format("DD MMM YYYY");
                    this.toDate = moment().format("DD MMM YYYY");
                    this.show = false;
                    this.showDates = false;
                    this.showTable = false;
                    this.showComparisonTable = false;
                }
                if (this.reportOpt == 'This Week') {
                    // Get the current date
                    const currentDate = moment();

                    // Get the Monday date of the current week (first day of the week)
                    const firstDayOfWeek = moment(currentDate).startOf('week');

                    // Format the dates as "DD MMM YYYY"
                    this.fromDate = firstDayOfWeek.format("DD MMM YYYY");
                    this.toDate = moment().format("DD MMM YYYY");
                    this.show = false;
                    this.showDates = false;
                    this.showTable = false;
                    this.showComparisonTable = false;
                }
                if (this.reportOpt == 'This Month') {
                    // Get the current date
                    const currentDate = moment();

                    // Get the first day of the current month
                    const firstDayOfMonth = moment(currentDate).startOf('month');

                    // Format the dates as "DD MMM YYYY"
                    this.fromDate = firstDayOfMonth.format("DD MMM YYYY");
                    this.toDate = moment().format("DD MMM YYYY");

                    this.show = false;
                    this.showDates = false;
                    this.showTable = false;
                    this.showComparisonTable = false;
                }
                if (this.reportOpt == 'This Quarter') {
                    // Get the current date
                    const currentDate = moment();

                    // Get the first day of the current quarter
                    const firstDayOfQuarter = moment(currentDate).startOf('quarter');

                    // Format the dates as "DD MMM YYYY"
                    this.fromDate = firstDayOfQuarter.format("DD MMM YYYY");
                    this.toDate = moment().format("DD MMM YYYY");

                    this.show = false;
                    this.showDates = false;
                    this.showTable = false;
                    this.showComparisonTable = false;
                }
                if (this.reportOpt == 'This Year') {
                    // Get the current date
                    const currentDate = moment();

                    // Get the first day of the current year
                    const firstDayOfYear = moment(currentDate).startOf('year');

                    // Format the dates as "DD MMM YYYY"
                    this.fromDate = firstDayOfYear.format("DD MMM YYYY");
                    this.toDate = moment().format("DD MMM YYYY");
                    this.show = false;
                    this.showDates = false;
                    this.showTable = false;
                    this.showComparisonTable = false;
                }
                if (this.reportOpt == 'Yesterday') {
                    // Get the current date
                    const currentDate = moment();

                    // Get yesterday's date
                    const yesterday = moment(currentDate).subtract(1, 'day');

                    // Format the dates as "DD MMM YYYY"
                    this.fromDate = yesterday.format("DD MMM YYYY");
                    this.toDate = moment().format("DD MMM YYYY");
                    this.show = false;
                    this.showDates = false;
                    this.showTable = false;
                    this.showComparisonTable = false;
                }
                if (this.reportOpt == 'Previous Week') {
                    // Get the current date
                    const currentDate = moment();

                    // Get the first day (Monday) of the previous week
                    const firstDayOfPreviousWeek = moment(currentDate).subtract(1, 'week').startOf('isoWeek');

                    // Get the end day (Sunday) of the previous week
                    const endDayOfPreviousWeek = moment(currentDate).subtract(1, 'week').endOf('isoWeek');

                    // Format the dates as "DD MMM YYYY"
                    this.fromDate = firstDayOfPreviousWeek.format("DD MMM YYYY");
                    this.toDate = endDayOfPreviousWeek.format("DD MMM YYYY");
                    this.show = false;
                    this.showDates = false;
                    this.showTable = false;
                    this.showComparisonTable = false;
                }
                if (this.reportOpt == 'Previous Month') {
                    // Get the current date
                    const currentDate = moment();

                    // Get the first day of the previous month
                    const firstDayOfPreviousMonth = moment(currentDate).subtract(1, 'month').startOf('month');

                    // Get the end day of the previous month
                    const endDayOfPreviousMonth = moment(currentDate).subtract(1, 'month').endOf('month');

                    // Format the dates as "DD MMM YYYY"
                    this.fromDate = firstDayOfPreviousMonth.format("DD MMM YYYY");
                    this.toDate = endDayOfPreviousMonth.format("DD MMM YYYY");
                    this.show = false;
                    this.showDates = false;
                    this.showTable = false;
                    this.showComparisonTable = false;
                }
                if (this.reportOpt == 'Previous Quarter') {
                    // Get the current date
                    const currentDate = moment();

                    // Get the first day of the previous quarter
                    const firstDayOfPreviousQuarter = moment(currentDate).subtract(1, 'quarter').startOf('quarter');

                    // Get the end day of the previous quarter
                    const endDayOfPreviousQuarter = moment(currentDate).subtract(1, 'quarter').endOf('quarter');

                    // Format the dates as "DD MMM YYYY"
                    this.fromDate = firstDayOfPreviousQuarter.format("DD MMM YYYY");
                    this.toDate = endDayOfPreviousQuarter.format("DD MMM YYYY");
                    this.show = false;
                    this.showDates = false;
                    this.showTable = false;
                    this.showComparisonTable = false;
                }
                if (this.reportOpt == 'Previous Year') {
                    // Get the current date
                    const currentDate = moment();

                    // Get the first day of the previous year
                    const firstDayOfPreviousYear = moment(currentDate).subtract(1, 'year').startOf('year');

                    // Get the end day of the previous year
                    const endDayOfPreviousYear = moment(currentDate).subtract(1, 'year').endOf('year');

                    // Format the dates as "DD MMM YYYY"
                    this.fromDate = firstDayOfPreviousYear.format("DD MMM YYYY");
                    this.toDate = endDayOfPreviousYear.format("DD MMM YYYY");

                    this.show = false;
                    this.showDates = false;
                    this.showTable = false;
                    this.showComparisonTable = false;
                }
                if (this.reportOpt == 'Custom') {

                    this.toDate = moment().format("DD MMM YYYY");
                    const yesterday = moment(this.toDate).subtract(6, 'day');
                    // Format the dates as "DD MMM YYYY"
                    this.fromDate = yesterday.format("DD MMM YYYY");
                    this.show = false;
                    this.showDates = true
                    this.dateRender++;
                    this.showTable = false;
                    this.showComparisonTable = false;
                }
            },
            GetPeriods: function () {
                if (this.compareWith == 'Previous Year(s)') {
                    this.financialYears = [];
                    this.isPreviousYear = true;
                    this.isPreviousPeriod = false;
                    this.isPreviousQuarter = false;
                    this.isPreviousMonth = false;
                    this.numberOfPeriods = '';
                }
                if (this.compareWith == 'Previous Period(s)') {
                    this.isPreviousYear = false;
                    this.isPreviousPeriod = true;
                    this.isPreviousQuarter = false;
                    this.isPreviousMonth = false;
                    this.getFinancialYears();
                    this.numberOfPeriods = '';
                }
                if (this.compareWith == 'Previous Quarter(s)') {
                    this.financialYears = [];
                    this.isPreviousYear = false;
                    this.isPreviousPeriod = false;
                    this.isPreviousQuarter = true;
                    this.isPreviousMonth = false;
                    this.numberOfPeriods = '';
                }
                if (this.compareWith == 'Previous Month(s)') {
                    this.financialYears = [];
                    this.isPreviousYear = false;
                    this.isPreviousPeriod = false;
                    this.isPreviousQuarter = false;
                    this.isPreviousMonth = true;
                    this.numberOfPeriods = '';
                }
            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            IsSave: function () {
                this.showReport = !this.showReport;
            },

            nonNegative: function (value) {
                return parseFloat(Math.abs(value)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },
            languageChange: function (lan) {
                if (this.language == lan) {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/FreeofCostSale');

                }
            },

            findDataByCode: function (code, list) {
                return list.find((item) => item.accountCode === code);
            },
            GetInventoryList: function () {
                var root = this;
                var token = '';
                this.loading = true;
                this.showTable = false;
                this.showComparisonTable = false;
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.isShown = false;
                //todate = moment().add(1, 'days').format("DD MMM YYYY")

                this.$https.get('/Report/GetAdvanceTrialBalanceAccount?fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&numberOfPeriods=' + this.numberOfPeriods + '&compareWith=' + this.compareWith + '&branchId=' + this.branchId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data != null) {
                            if (root.compareWith != '' && root.compareWith != null) {
                                root.showComparisonTable = true;
                                const results = response.data;

                                results.forEach((item, index) => {
                                    if (index == 0) {
                                        const transactionDetail = item.compareWithList;
                                        root.costCenters.forEach((item) => {
                                            const existingData = root.findDataByCode(item.code, transactionDetail);
                                            if (!existingData) {
                                                // If the code doesn't exist in data, create a new object
                                                const newData = {
                                                    accountCode: item.code,
                                                    accountName: item.accountName,
                                                    accountNameArabic: item.accountNameArabic,
                                                    costCenter: item.costCenterName,
                                                    accountType: '',
                                                    debit: 0,
                                                    credit: 0,
                                                    total: 0,
                                                };
                                                transactionDetail.push(newData);
                                            }
                                        });

                                        transactionDetail.sort((a, b) => a.accountCode.localeCompare(b.accountCode));

                                        root.sumDebit = 0;
                                        root.sumCredit = 0;
                                        root.sumTotal = 0;
                                        transactionDetail.forEach(function (x) {
                                            root.sumCredit += x.credit;
                                            root.sumDebit += x.debit;
                                            root.sumTotal += x.total;
                                        })
                                        const totalDebit = parseFloat(Math.abs(root.sumDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const totalCredit = parseFloat(Math.abs(root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const total = parseFloat(Math.abs(root.sumDebit - root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");

                                        root.trialBalanceAccountCompareList1 = [];
                                        root.trialBalanceAccountCompareList1.push({ transactionDetail: transactionDetail, compareWithValue: item.compareWith, totalDebit: totalDebit, totalCredit: totalCredit, total: total });
                                        root.trialBalanceAccountCompareList2 = [];
                                        root.trialBalanceAccountCompareList3 = [];
                                        root.trialBalanceAccountCompareList4 = [];
                                        root.trialBalanceAccountCompareList5 = [];
                                        root.trialBalanceAccountCompareList6 = [];
                                        root.trialBalanceAccountCompareList7 = [];
                                        root.trialBalanceAccountCompareList8 = [];
                                        root.trialBalanceAccountCompareList9 = [];
                                        root.trialBalanceAccountCompareList10 = [];
                                        root.trialBalanceAccountCompareList11 = [];
                                        root.trialBalanceAccountCompareList12 = [];
                                    }
                                    if (index == 1) {
                                        const transactionDetail = item.compareWithList;
                                        root.costCenters.forEach((item) => {
                                            const existingData = root.findDataByCode(item.code, transactionDetail);
                                            if (!existingData) {
                                                // If the code doesn't exist in data, create a new object
                                                const newData = {
                                                    accountCode: item.code,
                                                    accountName: item.accountName,
                                                    AccountNameArabic: item.accountNameArabic,
                                                    CostCenter: item.costCenterName,
                                                    accountType: '',
                                                    debit: 0,
                                                    credit: 0,
                                                    total: 0,
                                                };
                                                transactionDetail.push(newData);
                                            }
                                        });

                                        transactionDetail.sort((a, b) => a.accountCode.localeCompare(b.accountCode));
                                        root.sumDebit = 0;
                                        root.sumCredit = 0;
                                        root.sumTotal = 0;
                                        transactionDetail.forEach(function (x) {
                                            root.sumCredit += x.credit;
                                            root.sumDebit += x.debit;
                                            root.sumTotal += x.total;
                                        })
                                        const totalDebit = parseFloat(Math.abs(root.sumDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const totalCredit = parseFloat(Math.abs(root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const total = parseFloat(Math.abs(root.sumDebit - root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        root.trialBalanceAccountCompareList2 = [];
                                        root.trialBalanceAccountCompareList2.push({ transactionDetail: transactionDetail, compareWithValue: item.compareWith, totalDebit: totalDebit, totalCredit: totalCredit, total: total });
                                        root.trialBalanceAccountCompareList3 = [];
                                        root.trialBalanceAccountCompareList4 = [];
                                        root.trialBalanceAccountCompareList5 = [];
                                        root.trialBalanceAccountCompareList6 = [];
                                        root.trialBalanceAccountCompareList7 = [];
                                        root.trialBalanceAccountCompareList8 = [];
                                        root.trialBalanceAccountCompareList9 = [];
                                        root.trialBalanceAccountCompareList10 = [];
                                        root.trialBalanceAccountCompareList11 = [];
                                        root.trialBalanceAccountCompareList12 = [];
                                    }
                                    if (index == 2) {
                                        const transactionDetail = item.compareWithList;
                                        root.costCenters.forEach((item) => {
                                            const existingData = root.findDataByCode(item.code, transactionDetail);
                                            if (!existingData) {
                                                // If the code doesn't exist in data, create a new object
                                                const newData = {
                                                    accountCode: item.code,
                                                    accountName: item.accountName,
                                                    AccountNameArabic: item.accountNameArabic,
                                                    CostCenter: item.costCenterName,
                                                    accountType: '',
                                                    debit: 0,
                                                    credit: 0,
                                                    total: 0,
                                                };
                                                transactionDetail.push(newData);
                                            }
                                        });

                                        transactionDetail.sort((a, b) => a.accountCode.localeCompare(b.accountCode));
                                        root.sumDebit = 0;
                                        root.sumCredit = 0;
                                        root.sumTotal = 0;
                                        transactionDetail.forEach(function (x) {
                                            root.sumCredit += x.credit;
                                            root.sumDebit += x.debit;
                                            root.sumTotal += x.total;
                                        })
                                        const totalDebit = parseFloat(Math.abs(root.sumDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const totalCredit = parseFloat(Math.abs(root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const total = parseFloat(Math.abs(root.sumDebit - root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        root.trialBalanceAccountCompareList3 = [];
                                        root.trialBalanceAccountCompareList3.push({ transactionDetail: transactionDetail, compareWithValue: item.compareWith, totalDebit: totalDebit, totalCredit: totalCredit, total: total });
                                        root.trialBalanceAccountCompareList4 = [];
                                        root.trialBalanceAccountCompareList5 = [];
                                        root.trialBalanceAccountCompareList6 = [];
                                        root.trialBalanceAccountCompareList7 = [];
                                        root.trialBalanceAccountCompareList8 = [];
                                        root.trialBalanceAccountCompareList9 = [];
                                        root.trialBalanceAccountCompareList10 = [];
                                        root.trialBalanceAccountCompareList11 = [];
                                        root.trialBalanceAccountCompareList12 = [];
                                    }
                                    if (index == 3) {
                                        const transactionDetail = item.compareWithList;
                                        root.costCenters.forEach((item) => {
                                            const existingData = root.findDataByCode(item.code, transactionDetail);
                                            if (!existingData) {
                                                // If the code doesn't exist in data, create a new object
                                                const newData = {
                                                    accountCode: item.code,
                                                    accountName: item.accountName,
                                                    AccountNameArabic: item.accountNameArabic,
                                                    CostCenter: item.costCenterName,
                                                    accountType: '',
                                                    debit: 0,
                                                    credit: 0,
                                                    total: 0,
                                                };
                                                transactionDetail.push(newData);
                                            }
                                        });

                                        transactionDetail.sort((a, b) => a.accountCode.localeCompare(b.accountCode));
                                        root.sumDebit = 0;
                                        root.sumCredit = 0;
                                        root.sumTotal = 0;
                                        transactionDetail.forEach(function (x) {
                                            root.sumCredit += x.credit;
                                            root.sumDebit += x.debit;
                                            root.sumTotal += x.total;
                                        })
                                        const totalDebit = parseFloat(Math.abs(root.sumDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const totalCredit = parseFloat(Math.abs(root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const total = parseFloat(Math.abs(root.sumDebit - root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        root.trialBalanceAccountCompareList4 = [];
                                        root.trialBalanceAccountCompareList4.push({ transactionDetail: transactionDetail, compareWithValue: item.compareWith, totalDebit: totalDebit, totalCredit: totalCredit, total: total });
                                        root.trialBalanceAccountCompareList5 = [];
                                        root.trialBalanceAccountCompareList6 = [];
                                        root.trialBalanceAccountCompareList7 = [];
                                        root.trialBalanceAccountCompareList8 = [];
                                        root.trialBalanceAccountCompareList9 = [];
                                        root.trialBalanceAccountCompareList10 = [];
                                        root.trialBalanceAccountCompareList11 = [];
                                        root.trialBalanceAccountCompareList12 = [];
                                    }
                                    if (index == 4) {
                                        const transactionDetail = item.compareWithList;
                                        root.costCenters.forEach((item) => {
                                            const existingData = root.findDataByCode(item.code, transactionDetail);
                                            if (!existingData) {
                                                // If the code doesn't exist in data, create a new object
                                                const newData = {
                                                    accountCode: item.code,
                                                    accountName: item.accountName,
                                                    AccountNameArabic: item.accountNameArabic,
                                                    CostCenter: item.costCenterName,
                                                    accountType: '',
                                                    debit: 0,
                                                    credit: 0,
                                                    total: 0,
                                                };
                                                transactionDetail.push(newData);
                                            }
                                        });

                                        transactionDetail.sort((a, b) => a.accountCode.localeCompare(b.accountCode));
                                        root.sumDebit = 0;
                                        root.sumCredit = 0;
                                        root.sumTotal = 0;
                                        transactionDetail.forEach(function (x) {
                                            root.sumCredit += x.credit;
                                            root.sumDebit += x.debit;
                                            root.sumTotal += x.total;
                                        })
                                        const totalDebit = parseFloat(Math.abs(root.sumDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const totalCredit = parseFloat(Math.abs(root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const total = parseFloat(Math.abs(root.sumDebit - root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        root.trialBalanceAccountCompareList5 = [];
                                        root.trialBalanceAccountCompareList5.push({ transactionDetail: transactionDetail, compareWithValue: item.compareWith, totalDebit: totalDebit, totalCredit: totalCredit, total: total });
                                        root.trialBalanceAccountCompareList6 = [];
                                        root.trialBalanceAccountCompareList7 = [];
                                        root.trialBalanceAccountCompareList8 = [];
                                        root.trialBalanceAccountCompareList9 = [];
                                        root.trialBalanceAccountCompareList10 = [];
                                        root.trialBalanceAccountCompareList11 = [];
                                        root.trialBalanceAccountCompareList12 = [];
                                    }
                                    if (index == 5) {
                                        const transactionDetail = item.compareWithList;
                                        root.costCenters.forEach((item) => {
                                            const existingData = root.findDataByCode(item.code, transactionDetail);
                                            if (!existingData) {
                                                // If the code doesn't exist in data, create a new object
                                                const newData = {
                                                    accountCode: item.code,
                                                    accountName: item.accountName,
                                                    AccountNameArabic: item.accountNameArabic,
                                                    CostCenter: item.costCenterName,
                                                    accountType: '',
                                                    debit: 0,
                                                    credit: 0,
                                                    total: 0,
                                                };
                                                transactionDetail.push(newData);
                                            }
                                        });

                                        transactionDetail.sort((a, b) => a.accountCode.localeCompare(b.accountCode));
                                        root.sumDebit = 0;
                                        root.sumCredit = 0;
                                        root.sumTotal = 0;
                                        transactionDetail.forEach(function (x) {
                                            root.sumCredit += x.credit;
                                            root.sumDebit += x.debit;
                                            root.sumTotal += x.total;
                                        })
                                        const totalDebit = parseFloat(Math.abs(root.sumDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const totalCredit = parseFloat(Math.abs(root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const total = parseFloat(Math.abs(root.sumDebit - root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        root.trialBalanceAccountCompareList6 = [];
                                        root.trialBalanceAccountCompareList6.push({ transactionDetail: transactionDetail, compareWithValue: item.compareWith, totalDebit: totalDebit, totalCredit: totalCredit, total: total });
                                        root.trialBalanceAccountCompareList7 = [];
                                        root.trialBalanceAccountCompareList8 = [];
                                        root.trialBalanceAccountCompareList9 = [];
                                        root.trialBalanceAccountCompareList10 = [];
                                        root.trialBalanceAccountCompareList11 = [];
                                        root.trialBalanceAccountCompareList12 = [];
                                    }
                                    if (index == 6) {
                                        const transactionDetail = item.compareWithList;
                                        root.costCenters.forEach((item) => {
                                            const existingData = root.findDataByCode(item.code, transactionDetail);
                                            if (!existingData) {
                                                // If the code doesn't exist in data, create a new object
                                                const newData = {
                                                    accountCode: item.code,
                                                    accountName: item.accountName,
                                                    AccountNameArabic: item.accountNameArabic,
                                                    CostCenter: item.costCenterName,
                                                    accountType: '',
                                                    debit: 0,
                                                    credit: 0,
                                                    total: 0,
                                                };
                                                transactionDetail.push(newData);
                                            }
                                        });

                                        transactionDetail.sort((a, b) => a.accountCode.localeCompare(b.accountCode));
                                        root.sumDebit = 0;
                                        root.sumCredit = 0;
                                        root.sumTotal = 0;
                                        transactionDetail.forEach(function (x) {
                                            root.sumCredit += x.credit;
                                            root.sumDebit += x.debit;
                                            root.sumTotal += x.total;
                                        })
                                        const totalDebit = parseFloat(Math.abs(root.sumDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const totalCredit = parseFloat(Math.abs(root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const total = parseFloat(Math.abs(root.sumDebit - root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        root.trialBalanceAccountCompareList7 = [];
                                        root.trialBalanceAccountCompareList7.push({ transactionDetail: transactionDetail, compareWithValue: item.compareWith, totalDebit: totalDebit, totalCredit: totalCredit, total: total });
                                        root.trialBalanceAccountCompareList8 = [];
                                        root.trialBalanceAccountCompareList9 = [];
                                        root.trialBalanceAccountCompareList10 = [];
                                        root.trialBalanceAccountCompareList11 = [];
                                        root.trialBalanceAccountCompareList12 = [];
                                    }
                                    if (index == 7) {
                                        const transactionDetail = item.compareWithList;
                                        root.costCenters.forEach((item) => {
                                            const existingData = root.findDataByCode(item.code, transactionDetail);
                                            if (!existingData) {
                                                // If the code doesn't exist in data, create a new object
                                                const newData = {
                                                    accountCode: item.code,
                                                    accountName: item.accountName,
                                                    AccountNameArabic: item.accountNameArabic,
                                                    CostCenter: item.costCenterName,
                                                    accountType: '',
                                                    debit: 0,
                                                    credit: 0,
                                                    total: 0,
                                                };
                                                transactionDetail.push(newData);
                                            }
                                        });

                                        transactionDetail.sort((a, b) => a.accountCode.localeCompare(b.accountCode));
                                        root.sumDebit = 0;
                                        root.sumCredit = 0;
                                        root.sumTotal = 0;
                                        transactionDetail.forEach(function (x) {
                                            root.sumCredit += x.credit;
                                            root.sumDebit += x.debit;
                                            root.sumTotal += x.total;
                                        })
                                        const totalDebit = parseFloat(Math.abs(root.sumDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const totalCredit = parseFloat(Math.abs(root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const total = parseFloat(Math.abs(root.sumDebit - root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        root.trialBalanceAccountCompareList8 = [];
                                        root.trialBalanceAccountCompareList8.push({ transactionDetail: transactionDetail, compareWithValue: item.compareWith, totalDebit: totalDebit, totalCredit: totalCredit, total: total });
                                        root.trialBalanceAccountCompareList9 = [];
                                        root.trialBalanceAccountCompareList10 = [];
                                        root.trialBalanceAccountCompareList11 = [];
                                        root.trialBalanceAccountCompareList12 = [];
                                    }
                                    if (index == 8) {
                                        const transactionDetail = item.compareWithList;
                                        root.costCenters.forEach((item) => {
                                            const existingData = root.findDataByCode(item.code, transactionDetail);
                                            if (!existingData) {
                                                // If the code doesn't exist in data, create a new object
                                                const newData = {
                                                    accountCode: item.code,
                                                    accountName: item.accountName,
                                                    AccountNameArabic: item.accountNameArabic,
                                                    CostCenter: item.costCenterName,
                                                    accountType: '',
                                                    debit: 0,
                                                    credit: 0,
                                                    total: 0,
                                                };
                                                transactionDetail.push(newData);
                                            }
                                        });

                                        transactionDetail.sort((a, b) => a.accountCode.localeCompare(b.accountCode));
                                        root.sumDebit = 0;
                                        root.sumCredit = 0;
                                        root.sumTotal = 0;
                                        transactionDetail.forEach(function (x) {
                                            root.sumCredit += x.credit;
                                            root.sumDebit += x.debit;
                                            root.sumTotal += x.total;
                                        })
                                        const totalDebit = parseFloat(Math.abs(root.sumDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const totalCredit = parseFloat(Math.abs(root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const total = parseFloat(Math.abs(root.sumDebit - root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        root.trialBalanceAccountCompareList9 = [];
                                        root.trialBalanceAccountCompareList9.push({ transactionDetail: transactionDetail, compareWithValue: item.compareWith, totalDebit: totalDebit, totalCredit: totalCredit, total: total });
                                        root.trialBalanceAccountCompareList10 = [];
                                        root.trialBalanceAccountCompareList11 = [];
                                        root.trialBalanceAccountCompareList12 = [];
                                    }
                                    if (index == 9) {
                                        const transactionDetail = item.compareWithList;
                                        root.costCenters.forEach((item) => {
                                            const existingData = root.findDataByCode(item.code, transactionDetail);
                                            if (!existingData) {
                                                // If the code doesn't exist in data, create a new object
                                                const newData = {
                                                    accountCode: item.code,
                                                    accountName: item.accountName,
                                                    AccountNameArabic: item.accountNameArabic,
                                                    CostCenter: item.costCenterName,
                                                    accountType: '',
                                                    debit: 0,
                                                    credit: 0,
                                                    total: 0,
                                                };
                                                transactionDetail.push(newData);
                                            }
                                        });

                                        transactionDetail.sort((a, b) => a.accountCode.localeCompare(b.accountCode));
                                        root.sumDebit = 0;
                                        root.sumCredit = 0;
                                        root.sumTotal = 0;
                                        transactionDetail.forEach(function (x) {
                                            root.sumCredit += x.credit;
                                            root.sumDebit += x.debit;
                                            root.sumTotal += x.total;
                                        })
                                        const totalDebit = parseFloat(Math.abs(root.sumDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const totalCredit = parseFloat(Math.abs(root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const total = parseFloat(Math.abs(root.sumDebit - root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        root.trialBalanceAccountCompareList10 = [];
                                        root.trialBalanceAccountCompareList10.push({ transactionDetail: transactionDetail, compareWithValue: item.compareWith, totalDebit: totalDebit, totalCredit: totalCredit, total: total });
                                        root.trialBalanceAccountCompareList11 = [];
                                        root.trialBalanceAccountCompareList12 = [];
                                    }
                                    if (index == 10) {
                                        const transactionDetail = item.compareWithList;
                                        root.costCenters.forEach((item) => {
                                            const existingData = root.findDataByCode(item.code, transactionDetail);
                                            if (!existingData) {
                                                // If the code doesn't exist in data, create a new object
                                                const newData = {
                                                    accountCode: item.code,
                                                    accountName: item.accountName,
                                                    AccountNameArabic: item.accountNameArabic,
                                                    CostCenter: item.costCenterName,
                                                    accountType: '',
                                                    debit: 0,
                                                    credit: 0,
                                                    total: 0,
                                                };
                                                transactionDetail.push(newData);
                                            }
                                        });

                                        transactionDetail.sort((a, b) => a.accountCode.localeCompare(b.accountCode));
                                        root.sumDebit = 0;
                                        root.sumCredit = 0;
                                        root.sumTotal = 0;
                                        transactionDetail.forEach(function (x) {
                                            root.sumCredit += x.credit;
                                            root.sumDebit += x.debit;
                                            root.sumTotal += x.total;
                                        })
                                        const totalDebit = parseFloat(Math.abs(root.sumDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const totalCredit = parseFloat(Math.abs(root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const total = parseFloat(Math.abs(root.sumDebit - root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        root.trialBalanceAccountCompareList11 = [];
                                        root.trialBalanceAccountCompareList11.push({ transactionDetail: transactionDetail, compareWithValue: item.compareWith, totalDebit: totalDebit, totalCredit: totalCredit, total: total });
                                        root.trialBalanceAccountCompareList12 = [];
                                    }
                                    if (index == 11) {
                                        const transactionDetail = item.compareWithList;
                                        root.costCenters.forEach((item) => {
                                            const existingData = root.findDataByCode(item.code, transactionDetail);
                                            if (!existingData) {
                                                // If the code doesn't exist in data, create a new object
                                                const newData = {
                                                    accountCode: item.code,
                                                    accountName: item.accountName,
                                                    AccountNameArabic: item.accountNameArabic,
                                                    CostCenter: item.costCenterName,
                                                    accountType: '',
                                                    debit: 0,
                                                    credit: 0,
                                                    total: 0,
                                                };
                                                transactionDetail.push(newData);
                                            }
                                        });

                                        transactionDetail.sort((a, b) => a.accountCode.localeCompare(b.accountCode));
                                        root.sumDebit = 0;
                                        root.sumCredit = 0;
                                        root.sumTotal = 0;
                                        transactionDetail.forEach(function (x) {
                                            root.sumCredit += x.credit;
                                            root.sumDebit += x.debit;
                                            root.sumTotal += x.total;
                                        })
                                        const totalDebit = parseFloat(Math.abs(root.sumDebit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const totalCredit = parseFloat(Math.abs(root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        const total = parseFloat(Math.abs(root.sumDebit - root.sumCredit)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                                        root.trialBalanceAccountCompareList12 = [];
                                        root.trialBalanceAccountCompareList12.push({ transactionDetail: transactionDetail, compareWithValue: item.compareWith, totalDebit: totalDebit, totalCredit: totalCredit, total: total });
                                    }
                                });
                            }
                            else {
                                root.showTable = true;
                                root.transactionDetail = response.data;
                            }
                        }
                        root.loading = false;
                    });
            },
            PrintRdlc: function () {
                var companyId = localStorage.getItem('CompanyID');
                this.reportsrc1 = this.$ReportServer + '/ReportManagementModule/AccountFinanceReports/AccountFinanceReports.aspx?fromDate=' + this.fromDate + "&toDate=" + this.toDate + '&numberOfPeriods=' + this.numberOfPeriods + '&compareWith=' + this.compareWith + '&companyId=' + companyId + '&formName=AdvanceTrialBalanceAccount'
                this.changereportt++;
                this.show1 = !this.show1;
            },
            GetHeaderDetail: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;
                            root.GetInvoicePrintSetting();
                            root.getBase64Image(root.headerFooter.company.logoPath);
                        }
                    });
            },
            GetInvoicePrintSetting: function () {
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/printSettingDetail', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.headerFooter.footerEn = response.data.termsInEng;
                            root.headerFooter.footerAr = response.data.termsInAr;
                            root.headerFooter.isHeaderFooter = response.data.isHeaderFooter;
                            root.headerFooter.englishName = response.data.englishName;
                            root.headerFooter.arabicName = response.data.arabicName;
                            root.headerFooter.customerAddress = response.data.customerAddress;
                            root.headerFooter.customerVat = response.data.customerVat;
                            root.headerFooter.customerNumber = response.data.customerNumber;
                            root.headerFooter.cargoName = response.data.cargoName;
                            root.headerFooter.customerTelephone = response.data.customerTelephone;
                            root.headerFooter.itemPieceSize = response.data.itemPieceSize;
                            root.headerFooter.styleNo = response.data.styleNo;
                            root.headerFooter.blindPrint = response.data.blindPrint;
                            root.headerFooter.showBankAccount = response.data.showBankAccount;
                            root.headerFooter.bankAccount1 = response.data.bankAccount1;
                            root.headerFooter.bankAccount2 = response.data.bankAccount2;
                            root.headerFooter.bankIcon1 = response.data.bankIcon1;
                            root.headerFooter.bankIcon2 = response.data.bankIcon2;
                            root.headerFooter.customerNameEn = response.data.customerNameEn;
                            root.headerFooter.customerNameAr = response.data.customerNameAr;
                        }
                    },
                        function (error) {
                            //this.loading = false;
                            console.log(error);
                        });
            },
            PrintDetails: function () {
                var root = this;

                root.isShown = true;
                root.combineDate = 'From Date: ' + this.fromDate + ' To Date: ' + this.toDate;
                root.printRender++;

            },
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        }
                    });
            },
            getFinancialYears: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                this.$https.get("/Report/GetYearlyPeriodList", { headers: { Authorization: `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            const financialYear = response.data;

                            financialYear.forEach((item) => {
                                root.financialYears = [];
                                root.financialYears.push(item.name);
                            })
                        }
                    });
            },
            getAccounts: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                this.$https.get("/Report/GetAllAccounts", { headers: { Authorization: `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.costCenters = response.data;
                        }
                    });
            },
            onDrag: function (event) {
                if (event.pointerType === 'mouse') {
                    this.$refs.scrollable.scrollLeft = this.elementFrom - event.clientX + this.pointerFrom;
                }
            },
            onPointerDown: function (event) {
                if (event.pointerType === 'mouse') {
                    this.pointerDown = true;
                    this.pointerFrom = event.clientX;
                    this.elementFrom = this.$refs.scrollable.scrollLeft;

                    document.addEventListener('pointermove', this.onDrag);
                }
            },
            onPointerUp: function (event) {
                if (event.pointerType === 'mouse') {
                    document.removeEventListener('pointermove', this.onDrag);
                }
            },
        },
        created: function () {
            this.GetHeaderDetail();
            this.language = this.$i18n.locale;
            this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.getAccounts();
            this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
        }
    }
</script>
<style scoped>
    .bor {
        overflow: auto;
        width: 70vw;
    }

    .opac {
        opacity: 0 !important;
    }

    .table-responsive {
        overflow-x: hidden !important;
    }

    .pointers {
        cursor: pointer;
    }

        .pointers:last-child {
            margin-right: 155px !important;
        }
</style>