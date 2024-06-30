<template>
    <div class="row" v-if="isValid('CanViewStockReport')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Advance Trial Balance') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item">
                                        <a href="javascript:void(0);">
                                            {{ $t("BalanceSheetReport.Home") }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('Advance Trial Balance') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="PrintRdlc()" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="far fa-file-excel font-14"></i>
                                    {{ $t("Print") }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t("Categories.Close") }}
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
            <div class="d-flex" v-if="showCompareTable">
                <div class="col-md-4 d-flex border">
                    <div class="card border-0" v-if="trialBalanceCompareList1.length > 0">
                        <div class="card-body border-0">
                            <div class="col-lg-12">
                                <div class="table-responsive" v-for="item in trialBalanceCompareList1"
                                    :key="item.compareWith">
                                    <div class="row">
                                        <h5 class="opac">{{ item.compareWith }}</h5>
                                    </div>
                                    <table class="table table-striped table-hover table_list_bg">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th style="width: 10%;">#</th>
                                                <th style="width: 20%;">
                                                    {{ $t('TrialBalanceReport.Code') }}
                                                </th>

                                                <th style="width: 70%;">
                                                    {{ $t('TrialBalanceReport.Name') }}
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(account, index) in item.accounts" v-bind:key="account.Code">
                                                <td style="width: 10%;">
                                                    {{ index + 1 }}
                                                </td>

                                                <td style="width: 20%;"> 
                                                    <span>{{ account.code }}</span>

                                                </td>

                                                <td style="width: 70%;">
                                                    <span v-if="language == 'en'">
                                                        <strong>
                                                            <a href="javascript:void(0)"
                                                                v-on:click="EditCity(account.trialBalance)"> {{ getFirst30Characters(account.name)
                                                                }}</a>
                                                        </strong>
                                                    </span>
                                                    <span v-else>
                                                        <strong>
                                                            <a href="javascript:void(0)"
                                                                v-on:click="EditCity(account.trialBalance)">
                                                                {{ getFirst30Characters(account.nameArabic) }}</a>
                                                        </strong>

                                                    </span>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td><b> {{ $t('TrialBalanceReport.Total') }} </b></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-8 d-flex bor border" ref="scrollable" @pointerdown="onPointerDown" @pointerup="onPointerUp">
                    <div class="card col-md-4 pointers border-0" v-if="trialBalanceCompareList1.length > 0">
                            <div class="card-body">
                                <div class="col-lg-12">
                                    <div class="table-responsive" v-for="item in trialBalanceCompareList1"
                                        :key="item.compareWith">
                                        <div class="row">
                                            <h5>{{ item.compareWith }}</h5>
                                        </div>
                                        <table class="table table-striped table-hover table_list_bg">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Debit') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Credit') }}
                                                    </th>
                                                    <th>
                                                        Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(account) in item.accounts" v-bind:key="account.Code">

                                                    <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}
                                                    </td>
                                                    <td>{{
                                                        Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString()
                                                    }}
                                                    </td>
                                                    <td>{{ Number(Math.abs(parseFloat(account.debit -
                                                        account.credit).toFixed(2))).toLocaleString() }}
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalDebit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{ Number(parseFloat(item.totalDebit -
                                                            item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-4 pointers border-0" v-if="trialBalanceCompareList2.length > 0">
                            <div class="card-body">
                                <div class="col-lg-12">
                                    <div class="table-responsive" v-for="item in trialBalanceCompareList2"
                                        :key="item.compareWith">
                                        <div class="row">
                                            <h5>{{ item.compareWith }}</h5>
                                        </div>
                                        <table class="table table-striped table-hover table_list_bg">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Debit') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Credit') }}
                                                    </th>
                                                    <th>
                                                        Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(account) in item.accounts" v-bind:key="account.Code">

                                                    <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}
                                                    </td>
                                                    <td>{{
                                                        Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString()
                                                    }}
                                                    </td>
                                                    <td>{{ Number(Math.abs(parseFloat(account.debit -
                                                        account.credit).toFixed(2))).toLocaleString() }}
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalDebit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{ Number(parseFloat(item.totalDebit -
                                                            item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-4 pointers border-0" v-if="trialBalanceCompareList3.length > 0">
                            <div class="card-body">
                                <div class="col-lg-12">
                                    <div class="table-responsive" v-for="item in trialBalanceCompareList3"
                                        :key="item.compareWith">
                                        <div class="row">
                                            <h5>{{ item.compareWith }}</h5>
                                        </div>
                                        <table class="table table-striped table-hover table_list_bg">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Debit') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Credit') }}
                                                    </th>
                                                    <th>
                                                        Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(account) in item.accounts" v-bind:key="account.Code">

                                                    <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}
                                                    </td>
                                                    <td>{{
                                                        Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString()
                                                    }}
                                                    </td>
                                                    <td>{{ Number(Math.abs(parseFloat(account.debit -
                                                        account.credit).toFixed(2))).toLocaleString() }}
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalDebit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{ Number(parseFloat(item.totalDebit -
                                                            item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-4 pointers border-0" v-if="trialBalanceCompareList4.length > 0">
                            <div class="card-body">
                                <div class="col-lg-12">
                                    <div class="table-responsive" v-for="item in trialBalanceCompareList4"
                                        :key="item.compareWith">
                                        <div class="row">
                                            <h5>{{ item.compareWith }}</h5>
                                        </div>
                                        <table class="table table-striped table-hover table_list_bg">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Debit') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Credit') }}
                                                    </th>
                                                    <th>
                                                        Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(account) in item.accounts" v-bind:key="account.Code">

                                                    <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}
                                                    </td>
                                                    <td>{{
                                                        Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString()
                                                    }}
                                                    </td>
                                                    <td>{{ Number(Math.abs(parseFloat(account.debit -
                                                        account.credit).toFixed(2))).toLocaleString() }}
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalDebit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{ Number(parseFloat(item.totalDebit -
                                                            item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-4 pointers border-0" v-if="trialBalanceCompareList5.length > 0">
                            <div class="card-body">
                                <div class="col-lg-12">
                                    <div class="table-responsive" v-for="item in trialBalanceCompareList5"
                                        :key="item.compareWith">
                                        <div class="row">
                                            <h5>{{ item.compareWith }}</h5>
                                        </div>
                                        <table class="table table-striped table-hover table_list_bg">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Debit') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Credit') }}
                                                    </th>
                                                    <th>
                                                        Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(account) in item.accounts" v-bind:key="account.Code">

                                                    <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}
                                                    </td>
                                                    <td>{{
                                                        Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString()
                                                    }}
                                                    </td>
                                                    <td>{{ Number(Math.abs(parseFloat(account.debit -
                                                        account.credit).toFixed(2))).toLocaleString() }}
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalDebit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{ Number(parseFloat(item.totalDebit -
                                                            item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-4 pointers border-0" v-if="trialBalanceCompareList6.length > 0">
                            <div class="card-body">
                                <div class="col-lg-12">
                                    <div class="table-responsive" v-for="item in trialBalanceCompareList6"
                                        :key="item.compareWith">
                                        <div class="row">
                                            <h5>{{ item.compareWith }}</h5>
                                        </div>
                                        <table class="table table-striped table-hover table_list_bg">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Debit') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Credit') }}
                                                    </th>
                                                    <th>
                                                        Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(account) in item.accounts" v-bind:key="account.Code">

                                                    <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}
                                                    </td>
                                                    <td>{{
                                                        Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString()
                                                    }}
                                                    </td>
                                                    <td>{{ Number(Math.abs(parseFloat(account.debit -
                                                        account.credit).toFixed(2))).toLocaleString() }}
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalDebit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{ Number(parseFloat(item.totalDebit -
                                                            item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-4 pointers border-0" v-if="trialBalanceCompareList7.length > 0">
                            <div class="card-body">
                                <div class="col-lg-12">
                                    <div class="table-responsive" v-for="item in trialBalanceCompareList7"
                                        :key="item.compareWith">
                                        <div class="row">
                                            <h5>{{ item.compareWith }}</h5>
                                        </div>
                                        <table class="table table-striped table-hover table_list_bg">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Debit') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Credit') }}
                                                    </th>
                                                    <th>
                                                        Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(account) in item.accounts" v-bind:key="account.Code">

                                                    <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}
                                                    </td>
                                                    <td>{{
                                                        Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString()
                                                    }}
                                                    </td>
                                                    <td>{{ Number(Math.abs(parseFloat(account.debit -
                                                        account.credit).toFixed(2))).toLocaleString() }}
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalDebit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{ Number(parseFloat(item.totalDebit -
                                                            item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-4 pointers border-0" v-if="trialBalanceCompareList8.length > 0">
                            <div class="card-body">
                                <div class="col-lg-12">
                                    <div class="table-responsive" v-for="item in trialBalanceCompareList8"
                                        :key="item.compareWith">
                                        <div class="row">
                                            <h5>{{ item.compareWith }}</h5>
                                        </div>
                                        <table class="table table-striped table-hover table_list_bg">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Debit') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Credit') }}
                                                    </th>
                                                    <th>
                                                        Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(account) in item.accounts" v-bind:key="account.Code">

                                                    <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}
                                                    </td>
                                                    <td>{{
                                                        Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString()
                                                    }}
                                                    </td>
                                                    <td>{{ Number(Math.abs(parseFloat(account.debit -
                                                        account.credit).toFixed(2))).toLocaleString() }}
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalDebit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{ Number(parseFloat(item.totalDebit -
                                                            item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-4 pointers border-0" v-if="trialBalanceCompareList9.length > 0">
                            <div class="card-body">
                                <div class="col-lg-12">
                                    <div class="table-responsive" v-for="item in trialBalanceCompareList9"
                                        :key="item.compareWith">
                                        <div class="row">
                                            <h5>{{ item.compareWith }}</h5>
                                        </div>
                                        <table class="table table-striped table-hover table_list_bg">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Debit') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Credit') }}
                                                    </th>
                                                    <th>
                                                        Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(account) in item.accounts" v-bind:key="account.Code">

                                                    <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}
                                                    </td>
                                                    <td>{{
                                                        Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString()
                                                    }}
                                                    </td>
                                                    <td>{{ Number(Math.abs(parseFloat(account.debit -
                                                        account.credit).toFixed(2))).toLocaleString() }}
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalDebit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{ Number(parseFloat(item.totalDebit -
                                                            item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-4 pointers border-0" v-if="trialBalanceCompareList10.length > 0">
                            <div class="card-body">
                                <div class="col-lg-12">
                                    <div class="table-responsive" v-for="item in trialBalanceCompareList10"
                                        :key="item.compareWith">
                                        <div class="row">
                                            <h5>{{ item.compareWith }}</h5>
                                        </div>
                                        <table class="table table-striped table-hover table_list_bg">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Debit') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Credit') }}
                                                    </th>
                                                    <th>
                                                        Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(account) in item.accounts" v-bind:key="account.Code">

                                                    <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}
                                                    </td>
                                                    <td>{{
                                                        Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString()
                                                    }}
                                                    </td>
                                                    <td>{{ Number(Math.abs(parseFloat(account.debit -
                                                        account.credit).toFixed(2))).toLocaleString() }}
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalDebit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{ Number(parseFloat(item.totalDebit -
                                                            item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-4 pointers border-0" v-if="trialBalanceCompareList11.length > 0">
                            <div class="card-body">
                                <div class="col-lg-12">
                                    <div class="table-responsive" v-for="item in trialBalanceCompareList11"
                                        :key="item.compareWith">
                                        <div class="row">
                                            <h5>{{ item.compareWith }}</h5>
                                        </div>
                                        <table class="table table-striped table-hover table_list_bg">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Debit') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Credit') }}
                                                    </th>
                                                    <th>
                                                        Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(account) in item.accounts" v-bind:key="account.Code">

                                                    <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}
                                                    </td>
                                                    <td>{{
                                                        Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString()
                                                    }}
                                                    </td>
                                                    <td>{{ Number(Math.abs(parseFloat(account.debit -
                                                        account.credit).toFixed(2))).toLocaleString() }}
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalDebit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{ Number(parseFloat(item.totalDebit -
                                                            item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card col-md-4 pointers border-0" v-if="trialBalanceCompareList12.length > 0">
                            <div class="card-body">
                                <div class="col-lg-12">
                                    <div class="table-responsive" v-for="item in trialBalanceCompareList12"
                                        :key="item.compareWith">
                                        <div class="row">
                                            <h5>{{ item.compareWith }}</h5>
                                        </div>
                                        <table class="table table-striped table-hover table_list_bg">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Debit') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TrialBalanceReport.Credit') }}
                                                    </th>
                                                    <th>
                                                        Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(account) in item.accounts" v-bind:key="account.Code">

                                                    <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}
                                                    </td>
                                                    <td>{{
                                                        Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString()
                                                    }}
                                                    </td>
                                                    <td>{{ Number(Math.abs(parseFloat(account.debit -
                                                        account.credit).toFixed(2))).toLocaleString() }}
                                                    </td>

                                                </tr>

                                                <tr>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalDebit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{
                                                            Number(parseFloat(item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                    <td>
                                                        <b>{{ Number(parseFloat(item.totalDebit -
                                                            item.totalCredit).toFixed(2)).toLocaleString()
                                                        }}</b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
            </div>

            <div class="card" v-if="showTable">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="table-responsive">
                            <div class="row">
                                <h5>{{ fromDate }} - {{ toDate }}</h5>
                            </div>
                            <table class="table table-striped table-hover table_list_bg">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            {{ $t('TrialBalanceReport.Code') }}
                                        </th>

                                        <th>
                                            {{ $t('TrialBalanceReport.Name') }}
                                        </th>
                                        <th>
                                            {{ $t('TrialBalanceReport.Debit') }}
                                        </th>
                                        <th>
                                            {{ $t('TrialBalanceReport.Credit') }}
                                        </th>
                                        <th>
                                            Total
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(account, index) in accounts" v-bind:key="account.Code">
                                        <td>
                                            {{ index + 1 }}
                                        </td>

                                        <td>
                                            <span>{{ account.code }}</span>

                                        </td>

                                        <td>
                                            <span v-if="language == 'en'">
                                                <strong>
                                                    <a href="javascript:void(0)"
                                                        v-on:click="EditCity(account.trialBalance)"> {{ account.name }}</a>
                                                </strong>
                                            </span>
                                            <span v-else>
                                                <strong>
                                                    <a href="javascript:void(0)"
                                                        v-on:click="EditCity(account.trialBalance)">
                                                        {{ account.nameArabic }}</a>
                                                </strong>

                                            </span>
                                        </td>
                                        <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}</td>
                                        <td>{{ Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString() }}
                                        </td>
                                        <td>{{ Number(Math.abs(parseFloat(account.debit -
                                            account.credit).toFixed(2))).toLocaleString() }}
                                        </td>

                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td><b> {{ $t('TrialBalanceReport.Total') }} </b></td>
                                        <td>
                                            <b>{{ Number(parseFloat(totalDebit).toFixed(2)).toLocaleString() }}</b>
                                        </td>
                                        <td>
                                            <b>{{ Number(parseFloat(totalCredit).toFixed(2)).toLocaleString() }}</b>
                                        </td>
                                        <td>
                                            <b>{{ Number(parseFloat(totalDebit - totalCredit).toFixed(2)).toLocaleString()
                                            }}</b>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <invoicedetailsprint :show="show1" v-if="show1" :reportsrc="reportsrc1" :changereport="changereportt" @close="show1=false" @IsSave="IsSave" />
            <trialSubAccount :subAccount="subAccount" :show="show" v-if="show" @close="show = false" />
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>

    </div>

    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from "vue-multiselect";


import moment from "moment";

export default {
    mixins: [clickMixin],
    components: {
        Multiselect,
        
    },
    data: function () {
        return {
            allowBranches: false,
            branchIds: [],
            branchId: '',

            reportsrc1:'',
            changereportt:0,
            show1:false,
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

            disablePeriod: false,
            disablePeriodRender: 0,

            reportOpt: "",
            dateRender: 0,

            reportsrc: '',
            changereport: 0,
            showrpt: false,
            date: '',
            fromDate: '',
            toDate: '',
            rander: 0,
            printRender: 0,
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            isShown: false,
            accounts: [],
            subAccount: [],
            advanceFilters: false,
            show: false,
            showReport: false,
            combineDate: '',
            language: 'Nothing',
            totalDebit: 0,
            totalCredit: 0,

            showCompareTable: false,
            costCenters: [],
            compareWithValue: '',
            trialBalanceCompareList1: [],
            trialBalanceCompareList2: [],
            trialBalanceCompareList3: [],
            trialBalanceCompareList4: [],
            trialBalanceCompareList5: [],
            trialBalanceCompareList6: [],
            trialBalanceCompareList7: [],
            trialBalanceCompareList8: [],
            trialBalanceCompareList9: [],
            trialBalanceCompareList10: [],
            trialBalanceCompareList11: [],
            trialBalanceCompareList12: [],
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
    methods: {
        getFirst30Characters :function (str) {
            
            if (str === null) {
                return '';
            }
            
            var aa = str.substring(0, Math.min(25, str.length));
            return aa;
        },
        AdvanceFilters: function () {
            this.fromDate = moment().format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.reportOpt = '';
            this.advanceFilters = !this.advanceFilters;
            this.showDates = false;
            this.disablePeriod = !this.disablePeriod;
            this.disablePeriodRender++;
            this.showTable = false;
            this.showComparisonTable = false;
            this.showCompareTable = false;
            this.numberOfPeriods = '';
            this.compareWith = '';
        },
        RemoveFilters: function () {
            this.reportOpt = '';
            this.show = false;
            this.numberOfPeriods = '';
            this.compareWith = '';
            this.showTable = false;
            this.showCompareTable = false;
            this.advanceFilters = false;
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
        EditCity: function (subAccount) {
            this.subAccount = subAccount

            this.show = !this.show;
        },
        IsSave:function(){
                this.showReport = !this.showReport;
            },
        PrintRdlc:function() {
            var companyId = '';
                        companyId = localStorage.getItem('CompanyID');
                    

                        this.reportsrc1=  this.$ReportServer+'/ReportManagementModule/AccountFinanceReports/AccountFinanceReports.aspx?fromDate=' + this.fromDate + "&toDate=" + this.toDate + '&numberOfPeriods=' + this.numberOfPeriods + '&compareWith=' + this.compareWith+'&companyId='+companyId+'&formName=AdvanceTrialBalance' + '&branchId=' + this.branchId
                        this.changereportt++;
                        this.show1 = !this.show1;
                },
        languageChange: function (lan) {
            if (this.language == lan) {

                var getLocale = this.$i18n.locale;
                this.language = getLocale;

                this.$router.go('/TrialBalanceReport');

            }
        },

        findDataByCode: function (code, list) {
            return list.find((item) => item.code === code);
        },
        GetInventoryList: function () {
            var root = this;
            root.showTable = false;
            root.showCompareTable = false;
            var token = '';
            this.loading = true;
            if (token == '') {
                token = localStorage.getItem('token');
            }

            this.$https.get('/Report/GetAdvanceTrialBalanceReport?fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&numberOfPeriods=' + this.numberOfPeriods + '&compareWith=' + this.compareWith + '&branchId=' + this.branchId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        if (root.compareWith != '' && root.compareWith != null) {
                            root.showCompareTable = true;
                            const compareWithList = response.data;
                            compareWithList.forEach((item, index) => {
                                if (index == 0) {
                                    const accounts = item.listOfAdvanceTrialBalances;

                                    const totalDebit = accounts.reduce(function (prev, item) {
                                        return prev + Number(item.debit);
                                    }, 0);

                                    const totalCredit = Math.abs(accounts.reduce(function (prev, item) {
                                        return Math.abs(prev + Number(item.credit));
                                    }, 0));

                                    root.costCenters.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, accounts);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                name: item.costCenterName,
                                                debit: 0,
                                                credit: 0,
                                            };
                                            accounts.push(newData);
                                        }
                                    });

                                    accounts.sort((a, b) => a.code.localeCompare(b.code));

                                    root.trialBalanceCompareList1 = [];
                                    root.trialBalanceCompareList1.push({ accounts: accounts, totalDebit: totalDebit, totalCredit: totalCredit, compareWith: item.compareWithValue });
                                    root.trialBalanceCompareList2 = [];
                                    root.trialBalanceCompareList3 = [];
                                    root.trialBalanceCompareList4 = [];
                                    root.trialBalanceCompareList5 = [];
                                    root.trialBalanceCompareList6 = [];
                                    root.trialBalanceCompareList7 = [];
                                    root.trialBalanceCompareList8 = [];
                                    root.trialBalanceCompareList9 = [];
                                    root.trialBalanceCompareList10 = [];
                                    root.trialBalanceCompareList11 = [];
                                    root.trialBalanceCompareList12 = [];
                                }
                                else if (index == 1) {
                                    const accounts = item.listOfAdvanceTrialBalances;

                                    const totalDebit = accounts.reduce(function (prev, item) {
                                        return prev + Number(item.debit);
                                    }, 0);

                                    const totalCredit = Math.abs(accounts.reduce(function (prev, item) {
                                        return Math.abs(prev + Number(item.credit));

                                    }, 0));


                                    root.costCenters.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, accounts);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                name: item.costCenterName,
                                                debit: 0,
                                                credit: 0,
                                            };
                                            accounts.push(newData);
                                        }
                                    });

                                    accounts.sort((a, b) => a.code.localeCompare(b.code));


                                    root.trialBalanceCompareList2 = [];
                                    root.trialBalanceCompareList2.push({ accounts: accounts, totalDebit: totalDebit, totalCredit: totalCredit, compareWith: item.compareWithValue });
                                    root.trialBalanceCompareList3 = [];
                                    root.trialBalanceCompareList4 = [];
                                    root.trialBalanceCompareList5 = [];
                                    root.trialBalanceCompareList6 = [];
                                    root.trialBalanceCompareList7 = [];
                                    root.trialBalanceCompareList8 = [];
                                    root.trialBalanceCompareList9 = [];
                                    root.trialBalanceCompareList10 = [];
                                    root.trialBalanceCompareList11 = [];
                                    root.trialBalanceCompareList12 = [];
                                }
                                else if (index == 2) {
                                    const accounts = item.listOfAdvanceTrialBalances;

                                    const totalDebit = accounts.reduce(function (prev, item) {
                                        return prev + Number(item.debit);
                                    }, 0);

                                    const totalCredit = Math.abs(accounts.reduce(function (prev, item) {
                                        return Math.abs(prev + Number(item.credit));

                                    }, 0));

                                    root.costCenters.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, accounts);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                name: item.costCenterName,
                                                debit: 0,
                                                credit: 0,
                                            };
                                            accounts.push(newData);
                                        }
                                    });

                                    accounts.sort((a, b) => a.code.localeCompare(b.code));

                                    root.trialBalanceCompareList3 = [];
                                    root.trialBalanceCompareList3.push({ accounts: accounts, totalDebit: totalDebit, totalCredit: totalCredit, compareWith: item.compareWithValue });
                                    root.trialBalanceCompareList4 = [];
                                    root.trialBalanceCompareList5 = [];
                                    root.trialBalanceCompareList6 = [];
                                    root.trialBalanceCompareList7 = [];
                                    root.trialBalanceCompareList8 = [];
                                    root.trialBalanceCompareList9 = [];
                                    root.trialBalanceCompareList10 = [];
                                    root.trialBalanceCompareList11 = [];
                                    root.trialBalanceCompareList12 = [];
                                }
                                else if (index == 3) {
                                    const accounts = item.listOfAdvanceTrialBalances;

                                    const totalDebit = accounts.reduce(function (prev, item) {
                                        return prev + Number(item.debit);
                                    }, 0);

                                    const totalCredit = Math.abs(accounts.reduce(function (prev, item) {
                                        return Math.abs(prev + Number(item.credit));

                                    }, 0));

                                    root.costCenters.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, accounts);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                name: item.costCenterName,
                                                debit: 0,
                                                credit: 0,
                                            };
                                            accounts.push(newData);
                                        }
                                    });

                                    accounts.sort((a, b) => a.code.localeCompare(b.code));

                                    root.trialBalanceCompareList4 = [];
                                    root.trialBalanceCompareList4.push({ accounts: accounts, totalDebit: totalDebit, totalCredit: totalCredit, compareWith: item.compareWithValue });
                                    root.trialBalanceCompareList5 = [];
                                    root.trialBalanceCompareList6 = [];
                                    root.trialBalanceCompareList7 = [];
                                    root.trialBalanceCompareList8 = [];
                                    root.trialBalanceCompareList9 = [];
                                    root.trialBalanceCompareList10 = [];
                                    root.trialBalanceCompareList11 = [];
                                    root.trialBalanceCompareList12 = [];
                                }
                                else if (index == 4) {
                                    const accounts = item.listOfAdvanceTrialBalances;

                                    const totalDebit = accounts.reduce(function (prev, item) {
                                        return prev + Number(item.debit);
                                    }, 0);

                                    const totalCredit = Math.abs(accounts.reduce(function (prev, item) {
                                        return Math.abs(prev + Number(item.credit));

                                    }, 0));

                                    root.costCenters.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, accounts);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                name: item.costCenterName,
                                                debit: 0,
                                                credit: 0,
                                            };
                                            accounts.push(newData);
                                        }
                                    });

                                    accounts.sort((a, b) => a.code.localeCompare(b.code));

                                    root.trialBalanceCompareList5 = [];
                                    root.trialBalanceCompareList5.push({ accounts: accounts, totalDebit: totalDebit, totalCredit: totalCredit, compareWith: item.compareWithValue });
                                    root.trialBalanceCompareList6 = [];
                                    root.trialBalanceCompareList7 = [];
                                    root.trialBalanceCompareList8 = [];
                                    root.trialBalanceCompareList9 = [];
                                    root.trialBalanceCompareList10 = [];
                                    root.trialBalanceCompareList11 = [];
                                    root.trialBalanceCompareList12 = [];
                                }
                                else if (index == 5) {
                                    const accounts = item.listOfAdvanceTrialBalances;

                                    const totalDebit = accounts.reduce(function (prev, item) {
                                        return prev + Number(item.debit);
                                    }, 0);

                                    const totalCredit = Math.abs(accounts.reduce(function (prev, item) {
                                        return Math.abs(prev + Number(item.credit));

                                    }, 0));

                                    root.costCenters.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, accounts);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                name: item.costCenterName,
                                                debit: 0,
                                                credit: 0,
                                            };
                                            accounts.push(newData);
                                        }
                                    });

                                    accounts.sort((a, b) => a.code.localeCompare(b.code));

                                    root.trialBalanceCompareList6 = [];
                                    root.trialBalanceCompareList6.push({ accounts: accounts, totalDebit: totalDebit, totalCredit: totalCredit, compareWith: item.compareWithValue });
                                    root.trialBalanceCompareList7 = [];
                                    root.trialBalanceCompareList8 = [];
                                    root.trialBalanceCompareList9 = [];
                                    root.trialBalanceCompareList10 = [];
                                    root.trialBalanceCompareList11 = [];
                                    root.trialBalanceCompareList12 = [];
                                }
                                else if (index == 6) {
                                    const accounts = item.listOfAdvanceTrialBalances;

                                    const totalDebit = accounts.reduce(function (prev, item) {
                                        return prev + Number(item.debit);
                                    }, 0);

                                    const totalCredit = Math.abs(accounts.reduce(function (prev, item) {
                                        return Math.abs(prev + Number(item.credit));

                                    }, 0));

                                    root.costCenters.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, accounts);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                name: item.costCenterName,
                                                debit: 0,
                                                credit: 0,
                                            };
                                            accounts.push(newData);
                                        }
                                    });

                                    accounts.sort((a, b) => a.code.localeCompare(b.code));

                                    root.trialBalanceCompareList7 = [];
                                    root.trialBalanceCompareList7.push({ accounts: accounts, totalDebit: totalDebit, totalCredit: totalCredit, compareWith: item.compareWithValue });
                                    root.trialBalanceCompareList8 = [];
                                    root.trialBalanceCompareList9 = [];
                                    root.trialBalanceCompareList10 = [];
                                    root.trialBalanceCompareList11 = [];
                                    root.trialBalanceCompareList12 = [];
                                }
                                else if (index == 7) {
                                    const accounts = item.listOfAdvanceTrialBalances;

                                    const totalDebit = accounts.reduce(function (prev, item) {
                                        return prev + Number(item.debit);
                                    }, 0);

                                    const totalCredit = Math.abs(accounts.reduce(function (prev, item) {
                                        return Math.abs(prev + Number(item.credit));

                                    }, 0));

                                    root.costCenters.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, accounts);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                name: item.costCenterName,
                                                debit: 0,
                                                credit: 0,
                                            };
                                            accounts.push(newData);
                                        }
                                    });

                                    accounts.sort((a, b) => a.code.localeCompare(b.code));

                                    root.trialBalanceCompareList8 = [];
                                    root.trialBalanceCompareList8.push({ accounts: accounts, totalDebit: totalDebit, totalCredit: totalCredit, compareWith: item.compareWithValue });
                                    root.trialBalanceCompareList9 = [];
                                    root.trialBalanceCompareList10 = [];
                                    root.trialBalanceCompareList11 = [];
                                    root.trialBalanceCompareList12 = [];
                                }
                                else if (index == 8) {
                                    const accounts = item.listOfAdvanceTrialBalances;

                                    const totalDebit = accounts.reduce(function (prev, item) {
                                        return prev + Number(item.debit);
                                    }, 0);

                                    const totalCredit = Math.abs(accounts.reduce(function (prev, item) {
                                        return Math.abs(prev + Number(item.credit));

                                    }, 0));

                                    root.costCenters.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, accounts);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                name: item.costCenterName,
                                                debit: 0,
                                                credit: 0,
                                            };
                                            accounts.push(newData);
                                        }
                                    });

                                    accounts.sort((a, b) => a.code.localeCompare(b.code));

                                    root.trialBalanceCompareList9 = [];
                                    root.trialBalanceCompareList9.push({ accounts: accounts, totalDebit: totalDebit, totalCredit: totalCredit, compareWith: item.compareWithValue });
                                    root.trialBalanceCompareList10 = [];
                                    root.trialBalanceCompareList11 = [];
                                    root.trialBalanceCompareList12 = [];
                                }
                                else if (index == 9) {
                                    const accounts = item.listOfAdvanceTrialBalances;

                                    const totalDebit = accounts.reduce(function (prev, item) {
                                        return prev + Number(item.debit);
                                    }, 0);

                                    const totalCredit = Math.abs(accounts.reduce(function (prev, item) {
                                        return Math.abs(prev + Number(item.credit));

                                    }, 0));

                                    root.costCenters.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, accounts);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                name: item.costCenterName,
                                                debit: 0,
                                                credit: 0,
                                            };
                                            accounts.push(newData);
                                        }
                                    });

                                    accounts.sort((a, b) => a.code.localeCompare(b.code));

                                    root.trialBalanceCompareList10 = [];
                                    root.trialBalanceCompareList10.push({ accounts: accounts, totalDebit: totalDebit, totalCredit: totalCredit, compareWith: item.compareWithValue });
                                    root.trialBalanceCompareList11 = [];
                                    root.trialBalanceCompareList12 = [];
                                }
                                else if (index == 10) {
                                    const accounts = item.listOfAdvanceTrialBalances;

                                    const totalDebit = accounts.reduce(function (prev, item) {
                                        return prev + Number(item.debit);
                                    }, 0);

                                    const totalCredit = Math.abs(accounts.reduce(function (prev, item) {
                                        return Math.abs(prev + Number(item.credit));

                                    }, 0));

                                    root.costCenters.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, accounts);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                name: item.costCenterName,
                                                debit: 0,
                                                credit: 0,
                                            };
                                            accounts.push(newData);
                                        }
                                    });

                                    accounts.sort((a, b) => a.code.localeCompare(b.code));


                                    root.trialBalanceCompareList11 = [];
                                    root.trialBalanceCompareList11.push({ accounts: accounts, totalDebit: totalDebit, totalCredit: totalCredit, compareWith: item.compareWithValue });
                                }
                                else if (index == 11) {
                                    const accounts = item.listOfAdvanceTrialBalances;

                                    const totalDebit = accounts.reduce(function (prev, item) {
                                        return prev + Number(item.debit);
                                    }, 0);

                                    const totalCredit = Math.abs(accounts.reduce(function (prev, item) {
                                        return Math.abs(prev + Number(item.credit));

                                    }, 0));

                                    root.costCenters.forEach((item) => {
                                        const existingData = root.findDataByCode(item.code, accounts);
                                        if (!existingData) {
                                            // If the code doesn't exist in data, create a new object
                                            const newData = {
                                                code: item.code,
                                                name: item.costCenterName,
                                                debit: 0,
                                                credit: 0,
                                            };
                                            accounts.push(newData);
                                        }
                                    });

                                    accounts.sort((a, b) => a.code.localeCompare(b.code));

                                    root.trialBalanceCompareList12 = [];
                                    root.trialBalanceCompareList12.push({ accounts: accounts, totalDebit: totalDebit, totalCredit: totalCredit, compareWith: item.compareWithValue });
                                }
                            })
                           
                        }
                        else {
                            root.showTable = true;

                            root.accounts = response.data;
                            root.totalDebit = root.accounts.reduce(function (prev, item) {
                                return prev + Number(item.debit);
                            }, 0);

                            root.totalCredit = Math.abs(root.accounts.reduce(function (prev, item) {
                                return Math.abs(prev + Number(item.credit));

                            }, 0));
                        }
                    }
                    root.loading = false;
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
                            root.financialYears.push(item.name);
                        })
                    }
                });
        },

        getCostCenter: function () {
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }
            this.$https.get("/Report/GetCostCenterList?isList=" + true, { headers: { Authorization: `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.costCenters = response.data.costCenters;
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
    mounted: function () {
        this.language = this.$i18n.locale;
        this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
        this.toDate = moment().format("DD MMM YYYY");
        this.rander++;
        this.getCostCenter();
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
       
    }
}
</script>
<style scoped>
.bor {
    border: 1px solid #e3ebf6;
    overflow: auto;
}
.table-responsive{
    overflow-x: hidden !important;
}
</style>



