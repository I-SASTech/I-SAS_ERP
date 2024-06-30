<template>
    <div class="row" v-if="isValid('CanDraftExpense') || isValid('CanViewExpense')">
        <div class="col-lg-12" v-if="isDayAlreadyStart">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('DailyExpense.DailyExpense') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('DailyExpense.Home')
                                    }}</a></li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('DailyExpense.DailyExpense') }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddExpense') || isValid('CanDraftExpense')"
                                    v-on:click="AddPurchaseOrder" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary ">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('SaleOrder.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger mx-2">
                                    {{ $t('SaleOrder.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-4" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-secondary" type="button" id="button-addon1" v-on:click="GetFilter()">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('DailyExpense.Search')" aria-label="Example text with button addon"
                                    aria-describedby="button-addon1, button-addon2">
                                <a class="btn btn-outline-secondary" v-on:click="AdvanceFilter" id="button-addon2">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                        </div>

                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

                            <button v-on:click="search22(true)" :disabled="!search" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled="!search" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12" v-if="advanceFilters">
                            <div class="form-group">
                                <label>{{ $t('Sale.FromDate') }}</label>
                                <datepicker v-model="fromDate" :key="render" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12" v-if="advanceFilters">
                            <div class="form-group">
                                <label>{{ $t('Sale.ToDate') }}</label>
                                <datepicker v-model="toDate" :key="render" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12" v-if="IsExpenseAccount && advanceFilters">
                            <div class="form-group">
                                <label> {{ $t('DailyExpense.PaymentMode') }}</label>
                                <multiselect v-if="$i18n.locale == 'en'" v-model="paymentMode" :options="['Cash', 'Bank']"
                                    :show-labels="false" v-bind:placeholder="$t('AddDailyExpense.SelectOption')">
                                </multiselect>
                                <multiselect v-else v-model="paymentMode" :options="['السيولة النقدية', 'مصرف']"
                                    :show-labels="false" v-bind:placeholder="$t('AddDailyExpense.SelectOption')"
                                    v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                </multiselect>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12" v-if="advanceFilters">
                            <div class="form-group">
                                <label> {{ $t('DailyExpense.ReferenceNo') }}</label>
                                <input type="text" class="form-control" v-model="referenceNo" />


                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12" v-if="advanceFilters">
                            <div class="form-group">
                                <label> {{ $t('DailyExpense.TotalAmount') }}</label>
                                <input type="number" class="form-control" @focus="$event.target.select()"
                                    v-model="totalAmount" />


                            </div>

                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3" id="hide" v-if="advanceFilters">

                            <button v-on:click="GetFilter()" :disabled ="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearFilter()" :disabled ="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>


                </div>
                <div class="card-body">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item" v-if="isValid('CanDraftExpense')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Draft' }"
                                v-on:click="makeActive('Draft')" data-bs-toggle="tab" href="#home" role="tab"
                                aria-selected="true">
                                {{ $t('DailyExpense.Draft') }}
                            </a>
                        </li>
                        <li class="nav-item" v-if="isValid('CanViewExpense')" v-on:click="makeActive('Approved')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Approved' }" data-bs-toggle="tab"
                                href="#profile" role="tab" aria-selected="false">
                                {{ $t('DailyExpense.Post') }}
                            </a>
                        </li>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div class="tab-pane pt-3" id="home" role="tabpanel" v-bind:class="{ active: active == 'Draft' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>
                                                #
                                            </th>
                                            <th>
                                                {{ $t('DailyExpense.VoucherNo') }}
                                            </th>
                                            <th v-if="isValid('CanViewTerminal')">
                                                {{ $t('DailyExpense.CounterName') }}
                                            </th>
                                            <th>
                                                {{ $t('DailyExpense.CreatedBy') }}
                                            </th>

                                            <th>
                                                {{ $t('Created Date') }}
                                            </th>
                                            <th v-if="IsExpenseAccount">
                                                {{ $t('Expense Date') }}
                                            </th>
                                            <th v-if="IsExpenseAccount">
                                                {{ $t('DailyExpense.PaymentMode') }}
                                            </th>
                                            <th>
                                                {{ $t('DailyExpense.ReferenceNo') }}
                                            </th>
                                            <th>
                                                {{ $t('DailyExpense.Description') }}
                                            </th>
                                            <th v-if="IsExpenseAccount">
                                                {{ $t('DailyExpense.TotalVAT') }}
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th>
                                                {{ $t('DailyExpense.TotalAmount') }}
                                            </th>
                                            <th style="width: 70px;" class="text-end">
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(details, index) in dailyExpenseList" v-bind:key="details.id">
                                            <td v-if="currentPage === 1">
                                                {{ index + 1 }}
                                            </td>
                                            <td v-else>
                                                {{ ((currentPage * 10) - 10) + (index + 1) }}
                                            </td>
                                            <td v-if="isValid('CanEditExpense')">
                                                <strong>
                                                    <a href="javascript:void(0)"
                                                        v-on:click="DailyExpenseDetail(details.id)">{{ details.voucherNo }}</a>
                                                </strong>

                                            </td>
                                            <td v-else>{{ details.voucherNo }}</td>
                                            <td v-if="isValid('CanViewTerminal')">
                                                {{ details.counterName }}
                                            </td>
                                            <td>
                                                {{ details.createdUser }}
                                            </td>

                                            <td>
                                                {{ details.date }}
                                            </td>
                                            <td v-if="IsExpenseAccount">
                                                {{ details.dateOfExpense }}
                                            </td>
                                            <td v-if="IsExpenseAccount">
                                                {{ details.paymentMode == 'Default' ? '' : details.paymentMode }}
                                            </td>
                                            <td>
                                                {{ details.referenceNo }}
                                            </td>
                                            <td>
                                                {{ details.description }}
                                            </td>
                                            <td v-if="IsExpenseAccount">
                                                {{ parseFloat(details.totalVat).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}
                                            </td>
                                            <td v-if="allowBranches">
                                                {{ details.branchCode }}
                                            </td>
                                            <td>
                                                {{ currency }}
                                                {{ parseFloat(details.totalAmount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}
                                            </td>

                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle"
                                                    data-bs-toggle="dropdown" aria-expanded="false">{{
                                                        $t('DailyExpense.Action') }} <i
                                                        class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintPosInvoice(details.id)"
                                                        v-if="isValid('CanPrintExpense') && isValid('CanPosServicePrint')">{{ $t('DailyExpense.POS-Print')
                                                        }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintInvoice(details.id)"
                                                        v-else>{{ $t('DailyExpense.A4-Print')
                                                        }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="ViewDailyExpense(details.id)"
                                                        v-if="isValid('CanViewDetailExpense')">{{ $t('DailyExpense.View')
                                                        }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="DeleteDailyExpense(details.id)"
                                                        v-if="isValid('CanDeleteExpense')">{{ $t('DailyExpense.Delete')
                                                        }}</a>

                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage === 1 && rowCount === 0">
                                        {{
                                            $t('Pagination.ShowingEntries')
                                        }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount < 10">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                        {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount >= 11">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                            $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1">
                                        {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                            $t('Pagination.to')
                                        }} {{ currentPage * 10 }} of {{ rowCount }} {{
    $t('Pagination.entries')
}}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{
                                            $t('Pagination.Showing')
                                        }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                            $t('Pagination.entries')
                                        }}
                                    </span>
                                    <span v-else-if="currentPage === pageCount">
                                        {{ $t('Pagination.Showing') }}
                                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                            $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class=" float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                            :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                            :next-text="$t('Table.Next')" :last-text="$t('Table.Last')"></b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane pt-3" id="profile" role="tabpanel"
                            v-bind:class="{ active: active == 'Approved' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>
                                                #
                                            </th>
                                            <th>
                                                {{ $t('DailyExpense.VoucherNo') }}
                                            </th>
                                            <th v-if="isValid('CanViewTerminal')">
                                                {{ $t('DailyExpense.CounterName') }}
                                            </th>
                                            <th>
                                                {{ $t('DailyExpense.CreatedBy') }}
                                            </th>
                                            <th>
                                                {{ $t('DailyExpense.ApprovedBy') }}
                                            </th>

                                            <th>
                                                {{ $t('Created Date') }}
                                            </th>
                                            <th v-if="IsExpenseAccount">
                                                {{ $t('Expense Date') }}
                                            </th>
                                            <th v-if="IsExpenseAccount">
                                                {{ $t('DailyExpense.PaymentMode') }}
                                            </th>
                                            <th>
                                                {{ $t('DailyExpense.ReferenceNo') }}
                                            </th>
                                            <th>
                                                {{ $t('DailyExpense.Description') }}
                                            </th>

                                            <th v-if="IsExpenseAccount">
                                                {{ $t('DailyExpense.TotalVAT') }}
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th>
                                                {{ $t('DailyExpense.TotalAmount') }}
                                            </th>
                                            <th style="width: 70px;" class="text-end">

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(details, index) in dailyExpenseList" v-bind:key="details.id">
                                            <td v-if="currentPage === 1">
                                                {{ index + 1 }}
                                            </td>
                                            <td v-else>
                                                {{ ((currentPage * 10) - 10) + (index + 1) }}
                                            </td>
                                            <td>
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="DailyExpenseDetail(details.id)">{{details.voucherNo}}</a>
                                                </strong>
                                            </td>
                                            <!-- <td>{{ details.voucherNo }}</td> -->
                                            <td v-if="isValid('CanViewTerminal')">
                                                {{ details.counterName }}
                                            </td>
                                            <td>
                                                {{ details.createdUser }}
                                            </td>
                                            <td>
                                                {{ details.userName }}
                                            </td>
                                            <td>
                                                {{ details.date }}
                                            </td>
                                            <td v-if="IsExpenseAccount">
                                                {{ details.dateOfExpense }}
                                            </td>
                                            <td v-if="IsExpenseAccount">
                                                {{ details.paymentMode == 'Default' ? '' : details.paymentMode }}
                                            </td>
                                            <td>
                                                {{ details.referenceNo }}
                                            </td>

                                            <td>
                                                {{ details.description }}
                                            </td>
                                            <td v-if="IsExpenseAccount">
                                                {{ parseFloat(details.totalVat).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}
                                            </td>
                                            <td v-if="allowBranches">
                                                {{ details.branchCode }}
                                            </td>

                                            <td>
                                                {{ currency }}
                                                {{ parseFloat(details.totalAmount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                    "$1,") }}
                                            </td>

                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle"
                                                    data-bs-toggle="dropdown" aria-expanded="false">{{
                                                        $t('DailyExpense.Action') }} <i
                                                        class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintPosInvoice(details.id)"
                                                        v-if="isValid('CanPrintExpense') && isValid('CanPosServicePrint')">{{ $t('DailyExpense.POS-Print')
                                                        }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintInvoice(details.id)"
                                                        v-else>{{ $t('DailyExpense.A4-Print')
                                                        }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="ViewDailyExpense(details.id)"
                                                        v-if="isValid('CanViewDetailExpense')">{{ $t('DailyExpense.View')
                                                        }}</a>

                                                </div>

                                            </td>
                                            <!--<td v-if="!details.isActive">
                                                <a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon " v-on:click="DeleteDailyExpense(details.id)"><i class=" fa fa-trash"></i></a>
                                            </td>-->
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage === 1 && rowCount === 0">
                                        {{
                                            $t('Pagination.ShowingEntries')
                                        }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount < 10">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                        {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount >= 11">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                            $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1">
                                        {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                            $t('Pagination.to')
                                        }} {{ currentPage * 10 }} of {{ rowCount }} {{
    $t('Pagination.entries')
}}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{
                                            $t('Pagination.Showing')
                                        }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                            $t('Pagination.entries')
                                        }}
                                    </span>
                                    <span v-else-if="currentPage === pageCount">
                                        {{ $t('Pagination.Showing') }}
                                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                            $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class="float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                            :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                            :next-text="$t('Table.Next')" :last-text="$t('Table.Last')"></b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <actionmodal :approvalStatusVm="updateApprovalStatus" :show="show" v-if="show" @close="close" />
            <dailyExpenseA4 :printDetails="printDetails" :headerFooter="headerFooter" :formName="formName"
                v-if="printDetails.length != 0" v-bind:key="printRender"></dailyExpenseA4>
            
        </div>
        <div class="row d-flex justify-content-center align-items-center" style="height: 70vh;" v-else>
            <div class="col-lg-6 col-sm-6 ml-auto mr-auto">
                <div class="card p-3 text-center " v-if="bankDetail">
                    <h4 class="">{{ $t('Dashboard.FirstStartInvoice') }}</h4>
                    <router-link
                        :to="{ path: '/WholeSaleDay', query: { token_name: 'DayStart_token', fromDashboard: 'true' } }"><a
                            href="javascript:void(0)" class="btn btn-outline-danger ">{{ $t('Dashboard.DayStart')
                            }}</a></router-link>
                </div>
                <div class="card p-5 text-center" v-else>
                    <h4 class="">{{ $t('Dashboard.FirstStartInvoice') }}</h4>
                    <router-link :to="{ path: '/dayStart', query: { token_name: 'DayStart_token', fromDashboard: 'true' } }"><a
                            href="javascript:void(0)" class="btn btn-outline-danger ">{{ $t('Dashboard.DayStart')
                            }}</a></router-link>
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
import Multiselect from 'vue-multiselect'
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
//import moment from "moment";
export default {
    mixins: [clickMixin],
    props: ['formName'],
    components: {
        Multiselect,
        Loading
    },
    data: function () {
        return {
            allowBranches: false,
            advanceFilters: false,
            bankDetail: false,
            fromDate: '',
            toDate: '',
            render: 0,
            paymentMode: '',
            referenceNo: '',
            totalAmount: 0,
            active: 'Draft',
            search: '',
            searchQuery: '',
            dailyExpenseList: [],
            purchasePostList: [],
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            currency: '',
            printRender: 0,
            randerList: 0,
            printPosRender: 0,
            selected: [],
            selectAll: false,
            updateApprovalStatus: {
                action: '',
                selected: [],
                total: 0,
                accountSelect: 'ForExpense'
            },
            show: false,
            IsExpenseAccount: false,
            CompanyID: '',
            UserID: '',
            employeeId: '',
            isDayAlreadyStart: false,
            IsProduction: false,
            userName: '',
            counter: 0,
            printDetails: '',
            printDetailsPos: '',
            headerFooter: {
                footerEn: '',
                footerAr: '',
                company: ''
            },
        }
    },
    computed: {
            isFilterButtonDisabled() {
      return (
        this.paymentMode ||
        this.referenceNo ||
        this.fromDate ||
        this.toDate ||
        this.totalAmount
      );
    },
  },
    watch: {

    },
    fromDate: function (fromDate) {

        this.counter++;
        if (this.counter != 1) {
            localStorage.setItem('fromDate', fromDate);

            this.getData(this.search, this.currentPage, this.active, fromDate, this.toDate);
        }
    },
    toDate: function (toDate) {

        this.counter++;
        if (this.counter != 2) {
            localStorage.setItem('toDate', toDate);

            this.getData(this.search, this.currentPage, this.active, this.fromDate, toDate);
        }
    },
    formName: function () {

        if (this.$route.query.data == 'AddDailyExpense') {
            this.$emit('update:modelValue', 'AddDailyExpense');

        }
        else {
            this.$emit('update:modelValue', this.$route.name);
            localStorage.removeItem("fromDate");
            localStorage.removeItem("toDate");

        }


        if (this.formName == 'dailyexpense') {

            this.IsExpenseAccount = false;
        }
        else {
            this.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;
        }

        this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);

    },

    methods: {

        search22: function () {
            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);
        },

        clearData: function () {
            this.search = "";
            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);

        },

        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        GetFilter: function (filter) {

            if (filter == 'paymentMode') {

                if (this.paymentMode == null || this.paymentMode == undefined) {
                    this.paymentMode = '';
                }
                else {

                    if (this.paymentMode == 'السيولة النقدية') {
                        this.paymentMode = 'Cash';
                    }
                    if (this.paymentMode == 'مصرف') {
                        this.paymentMode = 'Bank';
                    }
                }

            }
            else if (filter == 'referenceNo') {

                if (this.referenceNo == null || this.referenceNo == undefined) {
                    this.referenceNo = '';
                }

            }
            else if (filter == 'totalAmount') {

                if (this.totalAmount == null || this.totalAmount == undefined) {
                    this.totalAmount = '';
                }

            }

            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);



        },
        AdvanceFilter: function () {

            this.advanceFilters = !this.advanceFilters;
            if (this.advanceFilters == false) {
                this.totalAmount = 0;
                this.referenceNo = '';
                this.paymentMode = '';
                this.fromDate = '';
                this.toDate = '';
                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);
            }


        },
        clearFilter: function () {



            this.totalAmount = 0;
            this.referenceNo = '';
            this.paymentMode = '';
            this.fromDate = '';
            this.toDate = '';
            this.search = '';
            this.render++;
            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);



        },
        getBase64Image: function (path) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.headerFooter.company.logoPath = 'data:image/png;base64,' + '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';

            root.$https
                .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.filePath = response.data;
                        root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        root.printRender++;
                    }

                })
        },

        GetInvoicePrintSetting: function () {
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Sale/printSettingDetail?branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } })
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
                        root.loading = false;
                        console.log(error);
                    });
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
        PrintInvoice: function (value) {
            this.GetHeaderDetail();
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.printDetails = [];
            root.printDetailsPos = [];
            root.$https.get("/Company/EditDailyExpenseDetail?id=" + value, {
                headers: { Authorization: `Bearer ${token}` },
            })
                .then(function (response) {
                    if (response.data != null) {

                        root.printDetails = response.data;
                        root.printRender++;
                    }
                });
        },
        PrintPosInvoice: function (value) {

            this.GetHeaderDetail1();
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.printDetails = [];
            root.printDetailsPos = [];
            root.$https.get("/Company/EditDailyExpenseDetail?id=" + value, {
                headers: { Authorization: `Bearer ${token}` },
            })
                .then(function (response) {
                    if (response.data != null) {

                        root.printDetailsPos = response.data;
                    }
                });
        },
        ViewDailyExpense: function (id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Company/EditDailyExpenseDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/DailyExpenseView',
                            query: {
                                data: '',
                                Add: false,
                                formName: root.formName
                            }
                        })
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },

        DailyExpenseDetail: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Company/EditDailyExpenseDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.$store.GetEditObj(response.data);
                    root.$router.push({
                        path: '/adddailyexpense',
                        query: {
                            data: '',
                            Add: false,
                            formName: root.formName
                        }
                    })
                }
            });

        },
        GetHeaderDetail1: function () {


            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                .then(function (response) {
                    if (response.data != null) {
                        root.headerFooter.company = response.data;
                        root.printPosRender++;

                    }
                });
        },

        select: function () {

            this.selected = [];
            if (!this.selectAll) {
                for (let i in this.dailyExpenseList) {
                    this.selected.push(this.dailyExpenseList[i]);
                }
            }
        },
        getPage: function () {

            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);
        },
        close: function (x) {

            if (x == true) {
                this.show = false;
            }
            else {
                this.show = false;
                this.makeActive(x);
            }

        },
        makeActive: function (item) {


            this.active = item;
            this.selectAll = false;
            this.selected = [];

            this.getData(this.search, 1, item, this.fromDate, this.toDate);
        },
        UpdateApprovalStatus: function (action) {


            this.updateApprovalStatus.action = action;
            var total = 0;
            for (let item in this.selected) {
                total = total + this.selected[item].totalAmount;
            }
            this.updateApprovalStatus.total = total;
            this.updateApprovalStatus.selected = this.selected;
            this.show = !this.show;

        },
        getData: function (search, currentPage, status, fromDate, toDate) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            var branchId = localStorage.getItem('BranchId');
            localStorage.setItem('currentPage', this.currentPage);
            localStorage.setItem('active', this.active);
            this.$https.get('/Company/DailyExpenseList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&fromDate=' + fromDate + '&toDate=' + toDate + '&paymentMode=' + this.paymentMode + '&referenceNo=' + this.referenceNo + '&totalAmount=' + this.totalAmount + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    if (response.data != null) {
                        root.dailyExpenseList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.currentPage = currentPage;
                        root.randerList++;
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            buttonsStyling: false
                        });
                    }
                });
        },
        DeleteDailyExpense: function (id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Company/DailyExpenseDelete?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.$swal.fire({
                        icon: 'warning',
                        title: 'Deleted Successfully',
                        showConfirmButton: false,
                        timer: 800,
                        timerProgressBar: true,
                    });
                    root.getPage();
                }
            }).catch(error => {
                console.log(error)
                root.$swal.fire(
                    {
                        icon: 'error',
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                        text: error.response.data,

                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,
                    });

                root.loading = false
            })
                .finally(() => root.loading = false);
        },
        AddPurchaseOrder: function () {

            this.$router.push({
                path: '/adddailyexpense',
                query: {
                    formName: this.formName,
                    Add: true,
                    isDeliveryChallan: true,
                }

            });



        }
    },
    created: function () {

        if (this.$route.query.data == 'AddDailyExpense') {
            this.$emit('update:modelValue', 'AddDailyExpense' + this.formName);

        }
        else {
            this.$emit('update:modelValue', this.$route.name + this.formName);
            localStorage.removeItem("fromDate");
            localStorage.removeItem("toDate");

        }

        // if (localStorage.getItem('fromDate') != null && localStorage.getItem('fromDate') != '' && localStorage.getItem('fromDate') != undefined) {
        //     this.fromDate = localStorage.getItem('fromDate');

        // }
        // else {
        //     this.fromDate = moment().add(-7, 'days').format("DD MMM YYYY");

        // }
        // if (localStorage.getItem('toDate') != null && localStorage.getItem('toDate') != '' && localStorage.getItem('toDate') != undefined) {
        //     this.toDate = localStorage.getItem('toDate');

        // }
        // else {
        //     this.toDate = moment().format("DD MMM YYYY");
        // }
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;

    },
    mounted: function () {

        if (this.formName == 'dailyexpense') {

            this.IsExpenseAccount = false;
        }
        else {
            this.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;
        }
        this.bankDetail = localStorage.getItem('BankDetail') == 'true' ? true : false;



        var IsDayStart = localStorage.getItem('DayStart');
        var IsDayStartOn = localStorage.getItem('IsDayStart');
        var AllowAll = localStorage.getItem('AllowAll');
        if (IsDayStart != 'true') {
            this.isDayAlreadyStart = true;
            if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                this.currentPage = parseInt(localStorage.getItem('currentPage'));
                this.active = (localStorage.getItem('active'));
                this.getPage();

            }
            else {
                if (this.isValid('CanDraftExpense')) {
                    this.makeActive("Draft");
                }
                else if (this.isValid('CanViewExpense')) {
                    this.makeActive("Approved");
                }


            }
            this.currency = localStorage.getItem('currency');
        }
        else {
            if (AllowAll == 'false') {
                this.CompanyID = localStorage.getItem('CompanyID');
                this.UserID = localStorage.getItem('UserID');
                this.employeeId = localStorage.getItem('EmployeeId');
                if (IsDayStartOn == 'true') {
                    this.isDayAlreadyStart = true;
                    if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                        this.currentPage = parseInt(localStorage.getItem('currentPage'));
                        this.active = (localStorage.getItem('active'));
                        this.getPage();

                    }
                    else {
                        if (this.isValid('CanDraftExpense')) {
                            this.makeActive("Draft");
                        }
                        else if (this.isValid('CanViewExpense')) {
                            this.makeActive("Approved");
                        }

                    }
                }
                this.currency = localStorage.getItem('currency');

            }
            else {
                this.isDayAlreadyStart = true;
                if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                    this.currentPage = parseInt(localStorage.getItem('currentPage'));
                    this.active = (localStorage.getItem('active'));
                    this.getPage();


                }
                else {
                    if (this.isValid('CanDraftExpense')) {
                        this.makeActive("Draft");
                    }
                    else if (this.isValid('CanViewExpense')) {
                        this.makeActive("Approved");
                    }

                } this.currency = localStorage.getItem('currency');
            }
        }

    },
    updated: function () {

        if (this.selected.length < this.dailyExpenseList.length) {
            this.selectAll = false;
        } else if (this.selected.length == this.dailyExpenseList.length) {
            if (this.selected.length == 0) {
                this.selectAll = false;
            }
            else {
                this.selectAll = true
            }
        }
    }
}
</script>