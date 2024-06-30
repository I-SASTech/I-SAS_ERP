<template>
    <div class="row" v-if="isValid('CanViewAccountLedger')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Advance {{ $t('AccountLedger.AccountLedger') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);"> {{
                                        $t('AccountLedger.Home')
                                    }}</a></li>
                                    <li class="breadcrumb-item active">Advance {{ $t('AccountLedger.AccountLedger') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="PrintRdlc()" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="fas fa-print font-14"></i>
                                    {{ $t('StockReport.Print') }}
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
            <div class="row align-items-center">
                <div class="row align-items-end">
                    <div class=" col-lg-3   form-group">
                        <label>Select a Period:</label>
                        <multiselect v-model="reportOpt" :disabled="disablePeriod" :key="disablePeriodRender"
                            :options="['Today', 'This Week', 'This Month', 'This Quarter', 'This Year', 'Yesterday', 'Previous Week', 'Previous Month', 'Previous Quarter', 'Previous Year', 'Custom']"
                            :show-labels="false" v-bind:placeholder="$t('Select an Option')" @update:modelValue="GetDateTime()">
                        </multiselect>
                    </div>
                    <div class=" col-lg-3   form-group me-0 pe-0">
                        <label>{{ $t('AccountLedger.AccountName') }}</label>
                        <accountdropdown v-model="accountId" :modelValue="accountId" @update:modelValue="accountValue(accountId)"
                            :key="render" />      
                    </div>
                    <div class=" col-lg-3 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>
                    <div class=" col-lg-3  form-group pe-0">
                        <a class="btn btn-soft-primary me-2" v-on:click="AdvanceFilters()" id="button-addon2"
                                value="Advance Filter">
                                <i class="fa fa-filter"></i>
                            </a>
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

            <div class="d-flex bor" id="bx" v-if="showCompareTable" ref="scrollable" @pointerdown="onPointerDown" @pointerup="onPointerUp">
                <div class="card col-md-8 pointers" v-if="accountLedgerCompareList1.length > 0">
                    <div class="card-body">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in accountLedgerCompareList1" :key="item.compareWithValue">
                                <div class="row">
                                    <h6>{{ item.compareWithValue }}</h6>
                                    <h6 class="col-md-12">
                                        {{ $t('AccountLedger.OpeningBalance') }} :{{ item.openingBalance > 0 ? 'Dr' : 'Cr' }}
                                        {{ nonNegative(item.openingBalance) }}
                                    </h6>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('AddCompanyAction.Date') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(ledger, index) in item.transactionList" v-bind:key="ledger.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td class="text-center">
                                                {{ ledger.date }}
                                            </td>
                                            <td class="text-center">
                                                {{ ledger.documentNumber }}
                                            </td>
                                            <td>

                                                {{ getTransactionType(ledger.description) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.debitAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.creditAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }} {{
                                                    nonNegative(ledger.openingBalance)
                                                }}
                                            </td>
                                        </tr>

                                        <tr v-if="ledger.transactionList.length > 0">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ $t('AccountLedger.ClosingBalance') }}:</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6> {{ nonNegative(item.totalDebit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ nonNegative(item.totalCredit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ item.runningBalance > 0 ? 'Dr' : 'Cr' }}
                                                    {{ nonNegative(item.runningBalance) }}</h6>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-md-8 pointers" v-if="accountLedgerCompareList2.length > 0">
                    <div class="card-body">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in accountLedgerCompareList2" :key="item.compareWithValue">
                                <div class="row">
                                    <h6>{{ item.compareWithValue }}</h6>
                                    <h6 class="col-md-12">
                                        {{ $t('AccountLedger.OpeningBalance') }} :{{ item.openingBalance > 0 ? 'Dr' : 'Cr' }}
                                        {{ nonNegative(item.openingBalance) }}
                                    </h6>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('AddCompanyAction.Date') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(ledger, index) in item.transactionList" v-bind:key="ledger.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td class="text-center">
                                                {{ ledger.date }}
                                            </td>
                                            <td class="text-center">
                                                {{ ledger.documentNumber }}
                                            </td>
                                            <td>

                                                {{ getTransactionType(ledger.description) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.debitAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.creditAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }} {{
                                                    nonNegative(ledger.openingBalance)
                                                }}
                                            </td>
                                        </tr>

                                        <tr v-if="ledger.transactionList.length > 0">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ $t('AccountLedger.ClosingBalance') }}:</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6> {{ nonNegative(item.totalDebit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ nonNegative(item.totalCredit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ item.runningBalance > 0 ? 'Dr' : 'Cr' }}
                                                    {{ nonNegative(item.runningBalance) }}</h6>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-md-8 pointers" v-if="accountLedgerCompareList3.length > 0">
                    <div class="card-body">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in accountLedgerCompareList3" :key="item.compareWithValue">
                                <div class="row">
                                    <h6>{{ item.compareWithValue }}</h6>
                                    <h6 class="col-md-12">
                                        {{ $t('AccountLedger.OpeningBalance') }} :{{ item.openingBalance > 0 ? 'Dr' : 'Cr' }}
                                        {{ nonNegative(item.openingBalance) }}
                                    </h6>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('AddCompanyAction.Date') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(ledger, index) in item.transactionList" v-bind:key="ledger.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td class="text-center">
                                                {{ ledger.date }}
                                            </td>
                                            <td class="text-center">
                                                {{ ledger.documentNumber }}
                                            </td>
                                            <td>

                                                {{ getTransactionType(ledger.description) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.debitAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.creditAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }} {{
                                                    nonNegative(ledger.openingBalance)
                                                }}
                                            </td>
                                        </tr>

                                        <tr v-if="ledger.transactionList.length > 0">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ $t('AccountLedger.ClosingBalance') }}:</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6> {{ nonNegative(item.totalDebit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ nonNegative(item.totalCredit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ item.runningBalance > 0 ? 'Dr' : 'Cr' }}
                                                    {{ nonNegative(item.runningBalance) }}</h6>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-md-8 pointers" v-if="accountLedgerCompareList4.length > 0">
                    <div class="card-body">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in accountLedgerCompareList4" :key="item.compareWithValue">
                                <div class="row">
                                    <h6>{{ item.compareWithValue }}</h6>
                                    <h6 class="col-md-12">
                                        {{ $t('AccountLedger.OpeningBalance') }} :{{ item.openingBalance > 0 ? 'Dr' : 'Cr' }}
                                        {{ nonNegative(item.openingBalance) }}
                                    </h6>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('AddCompanyAction.Date') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(ledger, index) in item.transactionList" v-bind:key="ledger.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td class="text-center">
                                                {{ ledger.date }}
                                            </td>
                                            <td class="text-center">
                                                {{ ledger.documentNumber }}
                                            </td>
                                            <td>

                                                {{ getTransactionType(ledger.description) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.debitAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.creditAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }} {{
                                                    nonNegative(ledger.openingBalance)
                                                }}
                                            </td>
                                        </tr>

                                        <tr v-if="ledger.transactionList.length > 0">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ $t('AccountLedger.ClosingBalance') }}:</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6> {{ nonNegative(item.totalDebit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ nonNegative(item.totalCredit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ item.runningBalance > 0 ? 'Dr' : 'Cr' }}
                                                    {{ nonNegative(item.runningBalance) }}</h6>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-md-8 pointers" v-if="accountLedgerCompareList5.length > 0">
                    <div class="card-body">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in accountLedgerCompareList5" :key="item.compareWithValue">
                                <div class="row">
                                    <h6>{{ item.compareWithValue }}</h6>
                                    <h6 class="col-md-12">
                                        {{ $t('AccountLedger.OpeningBalance') }} :{{ item.openingBalance > 0 ? 'Dr' : 'Cr' }}
                                        {{ nonNegative(item.openingBalance) }}
                                    </h6>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('AddCompanyAction.Date') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(ledger, index) in item.transactionList" v-bind:key="ledger.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td class="text-center">
                                                {{ ledger.date }}
                                            </td>
                                            <td class="text-center">
                                                {{ ledger.documentNumber }}
                                            </td>
                                            <td>

                                                {{ getTransactionType(ledger.description) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.debitAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.creditAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }} {{
                                                    nonNegative(ledger.openingBalance)
                                                }}
                                            </td>
                                        </tr>

                                        <tr v-if="ledger.transactionList.length > 0">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ $t('AccountLedger.ClosingBalance') }}:</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6> {{ nonNegative(item.totalDebit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ nonNegative(item.totalCredit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ item.runningBalance > 0 ? 'Dr' : 'Cr' }}
                                                    {{ nonNegative(item.runningBalance) }}</h6>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-md-8 pointers" v-if="accountLedgerCompareList6.length > 0">
                    <div class="card-body">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in accountLedgerCompareList6" :key="item.compareWithValue">
                                <div class="row">
                                    <h6>{{ item.compareWithValue }}</h6>
                                    <h6 class="col-md-12">
                                        {{ $t('AccountLedger.OpeningBalance') }} :{{ item.openingBalance > 0 ? 'Dr' : 'Cr' }}
                                        {{ nonNegative(item.openingBalance) }}
                                    </h6>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('AddCompanyAction.Date') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(ledger, index) in item.transactionList" v-bind:key="ledger.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td class="text-center">
                                                {{ ledger.date }}
                                            </td>
                                            <td class="text-center">
                                                {{ ledger.documentNumber }}
                                            </td>
                                            <td>

                                                {{ getTransactionType(ledger.description) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.debitAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.creditAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }} {{
                                                    nonNegative(ledger.openingBalance)
                                                }}
                                            </td>
                                        </tr>

                                        <tr v-if="ledger.transactionList.length > 0">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ $t('AccountLedger.ClosingBalance') }}:</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6> {{ nonNegative(item.totalDebit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ nonNegative(item.totalCredit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ item.runningBalance > 0 ? 'Dr' : 'Cr' }}
                                                    {{ nonNegative(item.runningBalance) }}</h6>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-md-8 pointers" v-if="accountLedgerCompareList7.length > 0">
                    <div class="card-body">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in accountLedgerCompareList7" :key="item.compareWithValue">
                                <div class="row">
                                    <h6>{{ item.compareWithValue }}</h6>
                                    <h6 class="col-md-12">
                                        {{ $t('AccountLedger.OpeningBalance') }} :{{ item.openingBalance > 0 ? 'Dr' : 'Cr' }}
                                        {{ nonNegative(item.openingBalance) }}
                                    </h6>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('AddCompanyAction.Date') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(ledger, index) in item.transactionList" v-bind:key="ledger.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td class="text-center">
                                                {{ ledger.date }}
                                            </td>
                                            <td class="text-center">
                                                {{ ledger.documentNumber }}
                                            </td>
                                            <td>

                                                {{ getTransactionType(ledger.description) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.debitAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.creditAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }} {{
                                                    nonNegative(ledger.openingBalance)
                                                }}
                                            </td>
                                        </tr>

                                        <tr v-if="ledger.transactionList.length > 0">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ $t('AccountLedger.ClosingBalance') }}:</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6> {{ nonNegative(item.totalDebit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ nonNegative(item.totalCredit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ item.runningBalance > 0 ? 'Dr' : 'Cr' }}
                                                    {{ nonNegative(item.runningBalance) }}</h6>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-md-8 pointers" v-if="accountLedgerCompareList8.length > 0">
                    <div class="card-body">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in accountLedgerCompareList8" :key="item.compareWithValue">
                                <div class="row">
                                    <h6>{{ item.compareWithValue }}</h6>
                                    <h6 class="col-md-12">
                                        {{ $t('AccountLedger.OpeningBalance') }} :{{ item.openingBalance > 0 ? 'Dr' : 'Cr' }}
                                        {{ nonNegative(item.openingBalance) }}
                                    </h6>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('AddCompanyAction.Date') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(ledger, index) in item.transactionList" v-bind:key="ledger.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td class="text-center">
                                                {{ ledger.date }}
                                            </td>
                                            <td class="text-center">
                                                {{ ledger.documentNumber }}
                                            </td>
                                            <td>

                                                {{ getTransactionType(ledger.description) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.debitAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.creditAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }} {{
                                                    nonNegative(ledger.openingBalance)
                                                }}
                                            </td>
                                        </tr>

                                        <tr v-if="ledger.transactionList.length > 0">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ $t('AccountLedger.ClosingBalance') }}:</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6> {{ nonNegative(item.totalDebit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ nonNegative(item.totalCredit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ item.runningBalance > 0 ? 'Dr' : 'Cr' }}
                                                    {{ nonNegative(item.runningBalance) }}</h6>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-md-8 pointers" v-if="accountLedgerCompareList9.length > 0">
                    <div class="card-body">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in accountLedgerCompareList9" :key="item.compareWithValue">
                                <div class="row">
                                    <h6>{{ item.compareWithValue }}</h6>
                                    <h6 class="col-md-12">
                                        {{ $t('AccountLedger.OpeningBalance') }} :{{ item.openingBalance > 0 ? 'Dr' : 'Cr' }}
                                        {{ nonNegative(item.openingBalance) }}
                                    </h6>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('AddCompanyAction.Date') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(ledger, index) in item.transactionList" v-bind:key="ledger.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td class="text-center">
                                                {{ ledger.date }}
                                            </td>
                                            <td class="text-center">
                                                {{ ledger.documentNumber }}
                                            </td>
                                            <td>

                                                {{ getTransactionType(ledger.description) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.debitAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.creditAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }} {{
                                                    nonNegative(ledger.openingBalance)
                                                }}
                                            </td>
                                        </tr>

                                        <tr v-if="ledger.transactionList.length > 0">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ $t('AccountLedger.ClosingBalance') }}:</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6> {{ nonNegative(item.totalDebit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ nonNegative(item.totalCredit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ item.runningBalance > 0 ? 'Dr' : 'Cr' }}
                                                    {{ nonNegative(item.runningBalance) }}</h6>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-md-8 pointers" v-if="accountLedgerCompareList10.length > 0">
                    <div class="card-body">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in accountLedgerCompareList10" :key="item.compareWithValue">
                                <div class="row">
                                    <h6>{{ item.compareWithValue }}</h6>
                                    <h6 class="col-md-12">
                                        {{ $t('AccountLedger.OpeningBalance') }} :{{ item.openingBalance > 0 ? 'Dr' : 'Cr' }}
                                        {{ nonNegative(item.openingBalance) }}
                                    </h6>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('AddCompanyAction.Date') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(ledger, index) in item.transactionList" v-bind:key="ledger.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td class="text-center">
                                                {{ ledger.date }}
                                            </td>
                                            <td class="text-center">
                                                {{ ledger.documentNumber }}
                                            </td>
                                            <td>

                                                {{ getTransactionType(ledger.description) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.debitAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.creditAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }} {{
                                                    nonNegative(ledger.openingBalance)
                                                }}
                                            </td>
                                        </tr>

                                        <tr v-if="ledger.transactionList.length > 0">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ $t('AccountLedger.ClosingBalance') }}:</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6> {{ nonNegative(item.totalDebit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ nonNegative(item.totalCredit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ item.runningBalance > 0 ? 'Dr' : 'Cr' }}
                                                    {{ nonNegative(item.runningBalance) }}</h6>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-md-8 pointers" v-if="accountLedgerCompareList11.length > 0">
                    <div class="card-body">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in accountLedgerCompareList11" :key="item.compareWithValue">
                                <div class="row">
                                    <h6>{{ item.compareWithValue }}</h6>
                                    <h6 class="col-md-12">
                                        {{ $t('AccountLedger.OpeningBalance') }} :{{ item.openingBalance > 0 ? 'Dr' : 'Cr' }}
                                        {{ nonNegative(item.openingBalance) }}
                                    </h6>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('AddCompanyAction.Date') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(ledger, index) in item.transactionList" v-bind:key="ledger.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td class="text-center">
                                                {{ ledger.date }}
                                            </td>
                                            <td class="text-center">
                                                {{ ledger.documentNumber }}
                                            </td>
                                            <td>

                                                {{ getTransactionType(ledger.description) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.debitAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.creditAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }} {{
                                                    nonNegative(ledger.openingBalance)
                                                }}
                                            </td>
                                        </tr>

                                        <tr v-if="ledger.transactionList.length > 0">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ $t('AccountLedger.ClosingBalance') }}:</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6> {{ nonNegative(item.totalDebit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ nonNegative(item.totalCredit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ item.runningBalance > 0 ? 'Dr' : 'Cr' }}
                                                    {{ nonNegative(item.runningBalance) }}</h6>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card col-md-8 pointers" v-if="accountLedgerCompareList12.length > 0">
                    <div class="card-body">
                        <div class="col-md-12">
                            <div class="table-responsive" v-for="item in accountLedgerCompareList12" :key="item.compareWithValue">
                                <div class="row">
                                    <h6>{{ item.compareWithValue }}</h6>
                                    <h6 class="col-md-12">
                                        {{ $t('AccountLedger.OpeningBalance') }} :{{ item.openingBalance > 0 ? 'Dr' : 'Cr' }}
                                        {{ nonNegative(item.openingBalance) }}
                                    </h6>
                                </div>
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('AddCompanyAction.Date') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(ledger, index) in item.transactionList" v-bind:key="ledger.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td class="text-center">
                                                {{ ledger.date }}
                                            </td>
                                            <td class="text-center">
                                                {{ ledger.documentNumber }}
                                            </td>
                                            <td>

                                                {{ getTransactionType(ledger.description) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.debitAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.creditAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }} {{
                                                    nonNegative(ledger.openingBalance)
                                                }}
                                            </td>
                                        </tr>

                                        <tr v-if="ledger.transactionList.length > 0">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ $t('AccountLedger.ClosingBalance') }}:</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6> {{ nonNegative(item.totalDebit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ nonNegative(item.totalCredit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ item.runningBalance > 0 ? 'Dr' : 'Cr' }}
                                                    {{ nonNegative(item.runningBalance) }}</h6>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" v-if="showTable">
                <div class="card col-md-12">
                    <div class="card-body">
                        <h6 class="col-md-12"
                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">
                            {{ $t('AccountLedger.OpeningBalance') }} :{{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }}
                            {{ nonNegative(ledger.openingBalance) }}
                        </h6>
                        <div>
                            <div class="table-responsive">
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th class="text-center">
                                                {{ $t('AddCompanyAction.Date') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('AccountLedger.DocumentCode') }}
                                            </th>
                                            <th>
                                                {{ $t('AccountLedger.Narration') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Debit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Credit') }}
                                            </th>
                                            <th
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ $t('AccountLedger.Balance') }}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(ledger, index) in ledger.transactionList" v-bind:key="ledger.id">
                                            <td>
                                                {{ index + 1 }}
                                            </td>

                                            <td class="text-center">
                                                {{ ledger.date }}
                                            </td>
                                            <td class="text-center">
                                                {{ ledger.documentNumber }}
                                            </td>
                                            <td>

                                                {{ getTransactionType(ledger.description) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.debitAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ nonNegative(ledger.creditAmount) }}
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                {{ ledger.openingBalance > 0 ? 'Dr' : 'Cr' }} {{
                                                    nonNegative(ledger.openingBalance)
                                                }}
                                            </td>
                                        </tr>

                                        <tr v-if="ledger.transactionList.length > 0">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ $t('AccountLedger.ClosingBalance') }}:</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6> {{ nonNegative(ledger.totalDebit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ nonNegative(ledger.totalCredit) }}</h6>
                                            </td>
                                            <td
                                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                                <h6>{{ ledger.runningBalance > 0 ? 'Dr' : 'Cr' }}
                                                    {{ nonNegative(ledger.runningBalance) }}</h6>
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
        <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc1" :changereport="changereportt"
            @close="show = false" @IsSave="IsSave" />

        <ledgerPrintReport :headerFooter="headerFooter" :printDetails="ledger" :fromDate="fromDate" :toDate="toDate"
            :formName="formName" :isPrint="isShown" :isShown="advanceFilters" v-if="isShown" v-bind:key="printRender">
        </ledgerPrintReport>
        <LedgerDownloadReport :headerFooter="headerFooter" :printDetails="ledger" :fromDate="fromDate" :toDate="toDate"
            :formName="formName" :isPrint="isDownload" :isShown="advanceFilters" v-if="isDownload"
            v-bind:key="printRenderPdf">
        </LedgerDownloadReport>
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
            isPeriod: true,
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
            reportOpt: "",
            dateRender: 0,
            rander: 0,
            

            disablePeriod: false,
            disablePeriodRender:0,

            reportsrc: '',
            changereport: 0,
            reportsrc1: '',
            changereportt: 0,
            show: false,
            render: 0,
            accountId: '00000000-0000-0000-0000-000000000000',
            fromDate: '',
            toDate: '',
            ledger: {
                transactionList: [],
                openingBalance: 0,
                runningBalance: 0,
                totalCredit: 0,
                totalDebit: 0,
            },

            isShown: false,
            isDownload: false,
            formName: 'Account Ledger',
            printRender: 0,
            printRenderPdf: 0,
            advanceFilters: false,
            combineDate: '',
            dateType: '',
            language: 'Nothing',
            headerFooter: {
                footerEn: '',
                footerAr: '',
                company: ''
            },

            showCompareTable: false,
            costCenters: [],
            compareWithValue: '',
            accountLedgerCompareList1: [],
            accountLedgerCompareList2: [],
            accountLedgerCompareList3: [],
            accountLedgerCompareList4: [],
            accountLedgerCompareList5: [],
            accountLedgerCompareList6: [],
            accountLedgerCompareList7: [],
            accountLedgerCompareList8: [],
            accountLedgerCompareList9: [],
            accountLedgerCompareList10: [],
            accountLedgerCompareList11: [],
            accountLedgerCompareList12: [],
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
        AdvanceFilters: function () {
            this.fromDate = moment().format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.reportOpt = '';
            this.advanceFilters = !this.advanceFilters;
            this.showDates = false;
            this.disablePeriod = !this.disablePeriod;
            this.disablePeriodRender++;
            this.showDates = false;
            this.compareWith = "";
            this.numberOfPeriods = "";
            this.showTable = false;
            this.showCompareTable = false;
        },
        RemoveFilters: function () {
            this.reportOpt = '';
            this.show = false;
            this.numberOfPeriods = '';
            this.compareWith = '';
            this.showTable = false;
            this.showComparisonTable = false;
            this.showCompareTable = false;
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
                this.isPeriod = false;
                this.numberOfPeriods = '';
            }
            if (this.compareWith == 'Previous Period(s)') {
                this.isPreviousYear = false;
                this.isPreviousPeriod = true;
                this.isPreviousQuarter = false;
                this.isPreviousMonth = false;
                this.getFinancialYears();
                this.numberOfPeriods = '';
                this.isPeriod = false;
            }
            if (this.compareWith == 'Previous Quarter(s)') {
                this.financialYears = [];
                this.isPreviousYear = false;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = true;
                this.isPreviousMonth = false;
                this.numberOfPeriods = '';
                this.isPeriod = false;
            }
            if (this.compareWith == 'Previous Month(s)') {
                this.financialYears = [];
                this.isPreviousYear = false;
                this.isPreviousPeriod = false;
                this.isPreviousQuarter = false;
                this.isPreviousMonth = true;
                this.numberOfPeriods = '';
                this.isPeriod = false;
            }
        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        getTransactionType(transactionType) {
            if (transactionType == 'StockOut') return 'Stock Out'
            else if (transactionType == 'JournalVoucher') return 'Journal Voucher'
            else if (transactionType == 'BankPay') return 'Bank Pay'
            else if (transactionType == 'ExpenseVoucher') return 'Expense Voucher'
            else if (transactionType == 'Expense') return 'Expense'
            else if (transactionType == 'CashPay') return 'Cash Pay'

            else if (transactionType == 'BankReceipt') return 'Bank Receipt'
            else if (transactionType == 'StockIn') return 'Stock In'
            else if (transactionType == 'SaleInvoice') return 'Sale Invoice'
            else if (transactionType == 'PurchaseReturn') return 'PurchaseReturn'
            else if (transactionType == 'PurchasePost') return 'Purchase'
            else if (transactionType == 'CashReceipt') return 'Cash Receipt'
            else {
                return transactionType;
            }

        },

        nonNegative: function (value) {
            return parseFloat(Math.abs(value)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
        },
        GetInventoryList: function () {
            var root = this;
            root.showTable = false;
            root.showComparisonTable = false;
            var token = '';
            this.loading = true;
            if (token == '') {
                token = localStorage.getItem('token');
            }
            //todate = moment(todate).add(1, 'days').format("DD MMM YYYY")
            
            this.isShown = false;

            this.$https.get('/Company/GetAdvanceAccountLedger?fromDate=' + root.fromDate + '&toDate=' + root.toDate + '&isLedger=true' + '&accountId=' + this.accountId + '&dateType=' + this.dateType + '&numberOfPeriods=' + this.numberOfPeriods + '&compareWith=' + this.compareWith + '&branchId=' + this.branchId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        if (root.compareWith != '' && root.compareWith != null) {
                            root.showCompareTable = true;
                            const compareWithList = response.data.compareWithList;

                            compareWithList.forEach((item, index)  => {
                                if(index == 0)
                                {
                                    const transactionList = item.transactionList;
                                    const openingBalance = item.openingBalance;
                                    const runningBalance = item.runningBalance;
                                    const totalDebit = item.totalDebit;
                                    const totalCredit = item.totalCredit;

                                    root.accountLedgerCompareList1 = [];
                                    root.accountLedgerCompareList1 = [{transactionList:transactionList, openingBalance: openingBalance,runningBalance: runningBalance,totalDebit: totalDebit,totalCredit : totalCredit, compareWithValue: item.compareWith}];
                                    root.accountLedgerCompareList2 = [];
                                    root.accountLedgerCompareList3 = [];
                                    root.accountLedgerCompareList4 = [];
                                    root.accountLedgerCompareList5 = [];
                                    root.accountLedgerCompareList6 = [];
                                    root.accountLedgerCompareList7 = [];
                                    root.accountLedgerCompareList8 = [];
                                    root.accountLedgerCompareList9 = [];
                                    root.accountLedgerCompareList10 = [];
                                    root.accountLedgerCompareList11 = [];
                                    root.accountLedgerCompareList12 = [];
                                }
                                if(index == 1)
                                {
                                    const transactionList = item.transactionList;
                                    const openingBalance = item.openingBalance;
                                    const runningBalance = item.runningBalance;
                                    const totalDebit = item.totalDebit;
                                    const totalCredit = item.totalCredit;

                                    root.accountLedgerCompareList2 = [];
                                    root.accountLedgerCompareList2 = [{transactionList:transactionList, openingBalance: openingBalance,runningBalance: runningBalance,totalDebit: totalDebit,totalCredit : totalCredit, compareWithValue: item.compareWith}];
                                    root.accountLedgerCompareList3 = [];
                                    root.accountLedgerCompareList4 = [];
                                    root.accountLedgerCompareList5 = [];
                                    root.accountLedgerCompareList6 = [];
                                    root.accountLedgerCompareList7 = [];
                                    root.accountLedgerCompareList8 = [];
                                    root.accountLedgerCompareList9 = [];
                                    root.accountLedgerCompareList10 = [];
                                    root.accountLedgerCompareList11 = [];
                                    root.accountLedgerCompareList12 = [];
                                }
                                if(index == 2)
                                {
                                    const transactionList = item.transactionList;
                                    const openingBalance = item.openingBalance;
                                    const runningBalance = item.runningBalance;
                                    const totalDebit = item.totalDebit;
                                    const totalCredit = item.totalCredit;

                                    root.accountLedgerCompareList3 = [];
                                    root.accountLedgerCompareList3 = [{transactionList:transactionList, openingBalance: openingBalance,runningBalance: runningBalance,totalDebit: totalDebit,totalCredit : totalCredit, compareWithValue: item.compareWith}];
                                    root.accountLedgerCompareList4 = [];
                                    root.accountLedgerCompareList5 = [];
                                    root.accountLedgerCompareList6 = [];
                                    root.accountLedgerCompareList7 = [];
                                    root.accountLedgerCompareList8 = [];
                                    root.accountLedgerCompareList9 = [];
                                    root.accountLedgerCompareList10 = [];
                                    root.accountLedgerCompareList11 = [];
                                    root.accountLedgerCompareList12 = [];
                                }
                                if(index == 3)
                                {
                                    const transactionList = item.transactionList;
                                    const openingBalance = item.openingBalance;
                                    const runningBalance = item.runningBalance;
                                    const totalDebit = item.totalDebit;
                                    const totalCredit = item.totalCredit;

                                    root.accountLedgerCompareList4 = [];
                                    root.accountLedgerCompareList4 = [{transactionList:transactionList, openingBalance: openingBalance,runningBalance: runningBalance,totalDebit: totalDebit,totalCredit : totalCredit, compareWithValue: item.compareWith}];
                                    root.accountLedgerCompareList5 = [];
                                    root.accountLedgerCompareList6 = [];
                                    root.accountLedgerCompareList7 = [];
                                    root.accountLedgerCompareList8 = [];
                                    root.accountLedgerCompareList9 = [];
                                    root.accountLedgerCompareList10 = [];
                                    root.accountLedgerCompareList11 = [];
                                    root.accountLedgerCompareList12 = [];
                                }
                                if(index == 4)
                                {
                                    const transactionList = item.transactionList;
                                    const openingBalance = item.openingBalance;
                                    const runningBalance = item.runningBalance;
                                    const totalDebit = item.totalDebit;
                                    const totalCredit = item.totalCredit;

                                    root.accountLedgerCompareList5 = [];
                                    root.accountLedgerCompareList5 = [{transactionList:transactionList, openingBalance: openingBalance,runningBalance: runningBalance,totalDebit: totalDebit,totalCredit : totalCredit, compareWithValue: item.compareWith}];
                                    root.accountLedgerCompareList6 = [];
                                    root.accountLedgerCompareList7 = [];
                                    root.accountLedgerCompareList8 = [];
                                    root.accountLedgerCompareList9 = [];
                                    root.accountLedgerCompareList10 = [];
                                    root.accountLedgerCompareList11 = [];
                                    root.accountLedgerCompareList12 = [];
                                }
                                if(index == 5)
                                {
                                    const transactionList = item.transactionList;
                                    const openingBalance = item.openingBalance;
                                    const runningBalance = item.runningBalance;
                                    const totalDebit = item.totalDebit;
                                    const totalCredit = item.totalCredit;

                                    root.accountLedgerCompareList6 = [];
                                    root.accountLedgerCompareList6 = [{transactionList:transactionList, openingBalance: openingBalance,runningBalance: runningBalance,totalDebit: totalDebit,totalCredit : totalCredit, compareWithValue: item.compareWith}];
                                    root.accountLedgerCompareList7 = [];
                                    root.accountLedgerCompareList8 = [];
                                    root.accountLedgerCompareList9 = [];
                                    root.accountLedgerCompareList10 = [];
                                    root.accountLedgerCompareList11 = [];
                                    root.accountLedgerCompareList12 = [];
                                }
                                if(index == 6)
                                {
                                    const transactionList = item.transactionList;
                                    const openingBalance = item.openingBalance;
                                    const runningBalance = item.runningBalance;
                                    const totalDebit = item.totalDebit;
                                    const totalCredit = item.totalCredit;

                                    root.accountLedgerCompareList7 = [];
                                    root.accountLedgerCompareList7 = [{transactionList:transactionList, openingBalance: openingBalance,runningBalance: runningBalance,totalDebit: totalDebit,totalCredit : totalCredit, compareWithValue: item.compareWith}];
                                    root.accountLedgerCompareList8 = [];
                                    root.accountLedgerCompareList9 = [];
                                    root.accountLedgerCompareList10 = [];
                                    root.accountLedgerCompareList11 = [];
                                    root.accountLedgerCompareList12 = [];
                                }
                                if(index == 7)
                                {
                                    const transactionList = item.transactionList;
                                    const openingBalance = item.openingBalance;
                                    const runningBalance = item.runningBalance;
                                    const totalDebit = item.totalDebit;
                                    const totalCredit = item.totalCredit;

                                    root.accountLedgerCompareList8 = [];
                                    root.accountLedgerCompareList8 = [{transactionList:transactionList, openingBalance: openingBalance,runningBalance: runningBalance,totalDebit: totalDebit,totalCredit : totalCredit, compareWithValue: item.compareWith}];
                                    root.accountLedgerCompareList9 = [];
                                    root.accountLedgerCompareList10 = [];
                                    root.accountLedgerCompareList11 = [];
                                    root.accountLedgerCompareList12 = [];
                                }
                                if(index == 8)
                                {
                                    const transactionList = item.transactionList;
                                    const openingBalance = item.openingBalance;
                                    const runningBalance = item.runningBalance;
                                    const totalDebit = item.totalDebit;
                                    const totalCredit = item.totalCredit;

                                    root.accountLedgerCompareList9 = [];
                                    root.accountLedgerCompareList9 = [{transactionList:transactionList, openingBalance: openingBalance,runningBalance: runningBalance,totalDebit: totalDebit,totalCredit : totalCredit, compareWithValue: item.compareWith}];
                                    root.accountLedgerCompareList10 = [];
                                    root.accountLedgerCompareList11 = [];
                                    root.accountLedgerCompareList12 = [];
                                }
                                if(index == 9)
                                {
                                    const transactionList = item.transactionList;
                                    const openingBalance = item.openingBalance;
                                    const runningBalance = item.runningBalance;
                                    const totalDebit = item.totalDebit;
                                    const totalCredit = item.totalCredit;

                                    root.accountLedgerCompareList10 = [];
                                    root.accountLedgerCompareList10 = [{transactionList:transactionList, openingBalance: openingBalance,runningBalance: runningBalance,totalDebit: totalDebit,totalCredit : totalCredit, compareWithValue: item.compareWith}];
                                    root.accountLedgerCompareList11 = [];
                                    root.accountLedgerCompareList12 = [];
                                }
                                if(index == 10)
                                {
                                    const transactionList = item.transactionList;
                                    const openingBalance = item.openingBalance;
                                    const runningBalance = item.runningBalance;
                                    const totalDebit = item.totalDebit;
                                    const totalCredit = item.totalCredit;

                                    root.accountLedgerCompareList11 = [];
                                    root.accountLedgerCompareList11 = [{transactionList:transactionList, openingBalance: openingBalance,runningBalance: runningBalance,totalDebit: totalDebit,totalCredit : totalCredit, compareWithValue: item.compareWith}];
                                    root.accountLedgerCompareList12 = [];
                                }
                                if(index == 11)
                                {
                                    const transactionList = item.transactionList;
                                    const openingBalance = item.openingBalance;
                                    const runningBalance = item.runningBalance;
                                    const totalDebit = item.totalDebit;
                                    const totalCredit = item.totalCredit;

                                    root.accountLedgerCompareList12 = [];
                                    root.accountLedgerCompareList12 = [{transactionList:transactionList, openingBalance: openingBalance,runningBalance: runningBalance,totalDebit: totalDebit,totalCredit : totalCredit, compareWithValue: item.compareWith}];
                                }
                            })
                        }
                        else {
                            root.showTable = true;
                            root.ledger.transactionList = response.data.transactionList;
                            root.ledger.openingBalance = response.data.openingBalance;
                            root.ledger.runningBalance = response.data.runningBalance;
                            root.ledger.totalDebit = response.data.totalDebit;
                            root.ledger.totalCredit = response.data.totalCredit;
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
        IsSave:function(){
                this.showReport = !this.showReport;
            },
        PrintRdlc:function() {
            
            var companyId = '';
                        companyId = localStorage.getItem('CompanyID');
                    

                        this.reportsrc1=  this.$ReportServer+'/ReportManagementModule/LedgerReports/LedgerReports.aspx?fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&isLedger=true' + '&accountId=' + this.accountId + '&dateType=' + this.dateType + '&numberOfPeriods=' + this.numberOfPeriods + '&compareWith=' + this.compareWith+'&formName=AdvanceAccountLedger'+'&companyId='+companyId 
                        this.changereportt++;
                        this.show = !this.show;
                },
    },
    created: function () {
        this.language = this.$i18n.locale;
        this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
        this.toDate = moment().format("DD MMM YYYY");
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
    }
}
</script>
<style scoped>
.bor {
    border: 1px solid #e3ebf6;
    overflow: auto;
    width: 90vw;
}
.table-responsive{
    overflow-x: hidden !important;
}
.pointers {
        cursor: pointer;
    }
    .pointers:last-child{
        margin-right: 155px !important;
    }
</style>