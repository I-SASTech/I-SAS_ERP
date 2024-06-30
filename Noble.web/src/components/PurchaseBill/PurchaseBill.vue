<template>
    <div class="row" v-if="isValid('CanDraftExpenseBill') || isValid('CanViewExpenseBill')">
        <div class="col-lg-12 col-sm-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('PurchaseBill.Bills') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PurchaseBill.Home')
                                    }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('PurchaseBill.Bills') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanDraftExpenseBill') || isValid('CanAddExpenseBill')"
                                    v-on:click="AddPurchaseOrder" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('PurchaseBill.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('PurchaseBill.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="row align-items-center justify-content-center">
                        <div class="col-md-9">
                            <div class="input-group">
                                <button class="btn btn-secondary" type="button" id="button-addon1"><i
                                        class="fas fa-search"></i></button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('PurchaseBill.Search')" aria-label="Example text with button addon"
                                    aria-describedby="button-addon1">
                                <a class="btn btn-outline-secondary" v-on:click="AdvanceFilter" id="button-addon2">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-md-3" v-if="advanceFilters"></div>
                        <div class="col-md-3" v-if="!advanceFilters">
                            <button v-on:click="search22(true)" :disabled ="!search" type="button" class="btn btn-outline-primary mx-2">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled ="!search" type="button" class="btn btn-outline-primary">
                                {{ $t('Sale.ClearFilter') }}
                            </button>
                        </div>
                    </div>
                    <div class="row mt-2">
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
                        
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12" v-if="advanceFilters">
                            <div class="form-group">
                                <label> {{ $t('Reference') }}</label>
                                <input type="text" class="form-control" v-model="reference" placeholder="Give Reference" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12" v-if="advanceFilters">
                            <div class="form-group">
                                <label> {{ $t('Amount') }}</label>
                                <input type="number" class="form-control"
                                    v-model="amount" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12" v-if="advanceFilters">
                            <div class="form-group">
                                <label> {{ $t('Status') }}</label>
                                <multiselect v-model="status" :options="['UnPaid', 'Fully Paid', 'Partially']" :show-labels="false" placeholder="Select Status Type">
                                                </multiselect>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12" v-if="advanceFilters">
                            <div class="form-group">
                                <label> {{ $t('AddPurchaseBill.Account') }}</label>
                                <accountdropdown v-model="billerId"  :key="render"></accountdropdown>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3" id="hide" v-if="advanceFilters">
                            <button v-on:click="search22(true)" :disabled ="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled ="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>

                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <ul class="nav nav-tabs" data-tabs="tabs">
                                <li class="nav-item" v-if="isValid('CanDraftExpenseBill')"><a class="nav-link"
                                        v-bind:class="{ active: active == 'Draft' }" v-on:click="makeActive('Draft')"
                                        id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab"
                                        aria-controls="v-pills-home" aria-selected="true">{{ $t('PurchaseBill.Draft') }}</a>
                                </li>
                                <li class="nav-item" v-if="isValid('CanViewExpenseBill')"><a class="nav-link"
                                        v-bind:class="{ active: active == 'Approved' }" v-on:click="makeActive('Approved')"
                                        id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab"
                                        aria-controls="v-pills-profile" aria-selected="false">{{ $t('PurchaseBill.Post')
                                        }}</a></li>
                            </ul>
                            <div class="tab-content mt-3" id="nav-tabContent">
                                <div v-if="active == 'Draft'">
                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-primary  btn-block" type="button"
                                                    id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true"
                                                    aria-expanded="false">
                                                    {{ $t('PurchaseBill.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right"
                                                    aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Approved')"> {{
                                                            $t('PurchaseBill.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Rejected')">{{
                                                            $t('PurchaseBill.Reject') }}</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>#</th>

                                                    <th>
                                                        {{ $t('PurchaseBill.BillNo') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('Created Date') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('Bill Date') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PurchaseBill.DueDate') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PurchaseBill.Account') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('Reference') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('PurchaseBill.Amount') }}
                                                    </th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(purchaseOrder, index) in purchaseBillList"
                                                    v-bind:key="purchaseOrder.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>
                                                    <td v-if="isValid('CanEditExpenseBill')">
                                                        <strong>
                                                            <a href="javascript:void(0)"
                                                                v-on:click="EditPurchaseOrder(purchaseOrder.id)">{{
                                                                    purchaseOrder.registrationNo }}</a>
                                                        </strong>

                                                    </td>
                                                    <td v-else>
                                                        {{ purchaseOrder.registrationNo }}

                                                    </td>

                                                    <td>
                                                        {{ purchaseOrder.date }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.billDate }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.dueDate }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.billerAccount }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.reference }}
                                                    </td>
                                                    <td class="text-center">
                                                        {{ parseFloat(purchaseOrder.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}
                                                    </td>
                                                    <td class="text-end">
                                                        <button type="button" class="btn btn-light dropdown-toggle"
                                                            data-bs-toggle="dropdown" aria-expanded="false"> {{
                                                                $t('PurchaseBill.Action') }} <i
                                                                class="mdi mdi-chevron-down"></i></button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="ViewPurchaseBill(purchaseOrder.id)"
                                                                v-if="isValid('CanViewDetailExpenseBill')"> {{
                                                                    $t('PurchaseBill.ViewInvoice') }}</a>
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="PrintPurchaseBill(purchaseOrder.id)"
                                                                v-if="isValid('CanPrintExpenseBill')"> {{
                                                                    $t('PurchaseBill.PrintInvoice') }}</a>
                                                        </div>
                                                    </td>
                                                    <!-- <td >
                                                        <button class="btn btn-icon btn-sm  btn-primary mr-1" v-on:click="ViewPurchaseBill(purchaseOrder.id)" v-if="isValid('CanViewDetailExpenseBill')"><i class="fas fa-eye"></i></button>
                                                        <button class="btn btn-icon btn-sm  btn-primary mr-1" v-on:click="PrintPurchaseBill(purchaseOrder.id)" v-if="isValid('CanPrintExpenseBill')"><i class=" fa fa-print"></i></button>
                                                    </td> -->
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>

                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <span v-if="currentPage === 1 && rowCount === 0">
                                                {{ $t('Pagination.ShowingEntries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1 && rowCount < 10">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }} {{ $t('Pagination.to') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1 && rowCount >= 11">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }} of {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                                {{ $t('Pagination.Showing') }}
                                                {{ (currentPage * 10) - 9 }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === pageCount">
                                                {{ $t('Pagination.Showing') }}
                                                {{ (currentPage * 10) - 9 }}
                                                {{ $t('Pagination.to') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                        </div>
                                        <div class=" col-lg-6">
                                            <div class=" float-end" v-on:click="getPage()">
                                                <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                    :per-page="10" :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')">
                                                </b-pagination>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="active == 'Approved'">

                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-primary  btn-block" type="button"
                                                    id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true"
                                                    aria-expanded="false">
                                                    {{ $t('PurchaseBill.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right"
                                                    aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Approved')"> {{
                                                            $t('PurchaseBill.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Rejected')">{{
                                                            $t('PurchaseBill.Reject') }}</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>#</th>

                                                    <th>
                                                        {{ $t('PurchaseBill.BillNo') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('Created Date') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('Bill Date') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PurchaseBill.DueDate') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PurchaseBill.Account') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('Reference') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('PurchaseBill.Amount') }}
                                                    </th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(purchaseOrder, index) in purchaseBillList"
                                                    v-bind:key="purchaseOrder.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>
                                                    <td v-if="isValid('CanEditExpenseBill')">
                                                        <strong>
                                                            <a href="javascript:void(0)"
                                                                v-on:click="EditPurchaseOrder(purchaseOrder.id)">{{
                                                                    purchaseOrder.registrationNo }}</a>
                                                            <br />
                                                        <div v-if="isValid('PremiumARAdvancePayment')" >
                                                            <div class="badge bg-danger" v-if="purchaseOrder.partiallyInvoices==1">
                                                                {{($i18n.locale == 'en' ||isLeftToRight())?'Un-Paid':'غير مدفوعة'}}
                                                            </div>
                                                            <div class="badge bg-primary" v-if="purchaseOrder.partiallyInvoices==2">
                                                                {{($i18n.locale == 'en' ||isLeftToRight())?' Partially Paid':'المدفوعة جزئيا'}}

                                                            </div>
                                                            <div class="badge bg-success" v-if="purchaseOrder.partiallyInvoices==3">

                                                                {{($i18n.locale == 'en' ||isLeftToRight())?' Fully Paid':'مدفوعة بالكامل'}}
                                                            </div>
                                                        </div>
                                                        </strong>
                                                    </td>
                                                    <td v-else>
                                                        <a href="javascript:void(0)">
                                                            {{ purchaseOrder.registrationNo }}
                                                        </a>
                                                        <br />
                                                        <div v-if="isValid('PremiumARAdvancePayment')" >
                                                            <div class="badge bg-danger" v-if="purchaseOrder.partiallyInvoices==1">
                                                                {{($i18n.locale == 'en' ||isLeftToRight())?'Un-Paid':'غير مدفوعة'}}
                                                            </div>
                                                            <div class="badge bg-primary" v-if="purchaseOrder.partiallyInvoices==2">
                                                                {{($i18n.locale == 'en' ||isLeftToRight())?' Partially Paid':'المدفوعة جزئيا'}}

                                                            </div>
                                                            <div class="badge bg-success" v-if="purchaseOrder.partiallyInvoices==3">

                                                                {{($i18n.locale == 'en' ||isLeftToRight())?' Fully Paid':'مدفوعة بالكامل'}}
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.date }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.billDate }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.dueDate }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.billerAccount }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.reference }}
                                                    </td>
                                                    <td class="text-center">
                                                        {{ parseFloat(purchaseOrder.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}
                                                    </td>
                                                    <td class="text-end">
                                                        <button type="button" class="btn btn-light dropdown-toggle"
                                                            data-bs-toggle="dropdown" aria-expanded="false">{{
                                                                $t('PurchaseBill.Action') }} <i
                                                                class="mdi mdi-chevron-down"></i></button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-if="purchaseOrder.netAmount > purchaseOrder.remainingAmount && isValid('CanPayExpenseBill')"
                                                                v-on:click="AddToDailyExpense(purchaseOrder.id)">{{
                                                                    $t('PurchaseBill.Payment') }}</a>
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="ViewPurchaseBill(purchaseOrder.id)"
                                                                v-if="isValid('CanViewDetailExpenseBill')">{{
                                                                    $t('PurchaseBill.ViewInvoice') }}</a>
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="PrintPurchaseBill(purchaseOrder.id)"
                                                                v-if="isValid('CanPrintExpenseBill')">{{
                                                                    $t('PurchaseBill.PrintInvoice') }}</a>
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
                                                {{ $t('Pagination.ShowingEntries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1 && rowCount < 10">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }} {{ $t('Pagination.to') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1 && rowCount >= 11">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }} of {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                                {{ $t('Pagination.Showing') }}
                                                {{ (currentPage * 10) - 9 }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === pageCount">
                                                {{ $t('Pagination.Showing') }}
                                                {{ (currentPage * 10) - 9 }}
                                                {{ $t('Pagination.to') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                        </div>
                                        <div class=" col-lg-6">
                                            <div class=" float-end" v-on:click="getPage()">
                                                <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                    :per-page="10" :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')">
                                                </b-pagination>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>


                        </div>
                    </div>
                </div>
            </div>

        </div>
        <PaymentModel :totalAmount="totalAmount" :expenseId="expenseId" :remainingAmount="remainingAmount" :show="payment"
            v-if="payment" @close="payment = false" @RefreshList="RefreshList" :billsId="billsId" />

        <billsReport :printDetails="printDetails" :headerFooter="headerFooter" v-if="printDetails.length != 0"
            v-bind:key="printRender" />

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
import Multiselect from 'vue-multiselect';
import clickMixin from '@/Mixins/clickMixin';
// import moment from "moment";

export default {
    mixins: [clickMixin],
    components: 
    {
            Multiselect,
    },
    data: function () {
        return {
            advanceFilters: false,
            fromDate: '',
            toDate: '',
            reference:'',
            amount: 0,
            billerId:'',
            status:'',
            render:0,

            active: 'Draft',
            search: '',
            searchQuery: '',
            billsId: '',
            expenseId: '',
            purchaseBillList: [],
            currentPage: 1,
            pageCount: '',
            rowCount: '',


            selected: [],
            selectAll: false,
            payment: false,
            updateApprovalStatus: {
                id: '',
                approvalStatus: ''
            },
            headerFooter: {
                footerEn: '',
                footerAr: '',
                company: ''
            },
            printDetails: [],
            printRender: 0,
            totalAmount: 0,
            remainingAmount: 0,
            randerList: 0,
        }
    },
    computed: {
            isFilterButtonDisabled() {
      return (
        this.billerId ||
        this.status ||
        this.amount ||
        this.fromDate ||
        this.toDate ||
        this.reference
      );
    },
  },
    watch: {
    },
    methods: {
        AdvanceFilter: function () 
        {
            this.advanceFilters = !this.advanceFilters;
            if (!this.advanceFilters) 
            {
                this.fromDate= '';
                this.toDate= '';
                this.reference='';
                this.amount= 0;
                this.billerId='';
                this.status='';

                this.getData(this.search, this.currentPage, this.active);
            }
        },
        search22: function () {
            this.getData(this.search, this.currentPage, this.active);
        },
        clearData: function () {
            this.search = "";
            if (this.advanceFilters) 
            {
                this.fromDate= '';
                this.toDate= '';
                this.reference='';
                this.amount= 0;
                this.billerId='';
                this.status='';
                this.render++;
            }
            this.getData(this.search, this.currentPage, this.active);

        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        RefreshList: function () {
            this.payment = false;
            this.getData(this.search, 1, 'Approved');
        },
        paymentModel: function (billsId, totalAmount, remainingAmount, expenseId) {
            this.billsId = billsId;
            this.totalAmount = totalAmount;
            this.remainingAmount = remainingAmount;
            this.expenseId = expenseId;
            this.payment = true;
        },
        DeleteFile: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Batch/DeletePo', this.selected, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000') {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonsStyling: false
                            }).then(function (result) {
                                if (result) {
                                    root.$router.push('/purchase');
                                }
                            });
                        } else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: response.data.message.isAddUpdate,
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        }
                    }
                },
                    function () {

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update UnSuccessfully' : 'التحديث غير ناجح',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            buttonsStyling: false
                        });
                    });
        },
        select: function () {
            this.selected = [];
            if (!this.selectAll) {
                for (let i in this.purchaseBillList) {
                    this.selected.push(this.purchaseBillList[i].id);
                }
            }
        },
        getPage: function () {
            this.getData(this.search, this.currentPage, this.active);
        },
        makeActive: function (item) {
            this.active = item;
            this.selectAll = false;
            this.selected = [];
            this.getData(this.search, 1, item);
        },
        getData: function (search, currentPage, status) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            localStorage.setItem('currentPage', this.currentPage);
            localStorage.setItem('active', this.active);
            var branchId = localStorage.getItem('BranchId');

            this.$https.get('/Purchase/PurchaseBillList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&branchId=' + branchId +'&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&reference=' + this.reference + '&billerId=' + this.billerId + '&amount=' + this.amount + '&paymentStatus=' + this.status, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    root.purchaseBillList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.rowCount = response.data.rowCount;
                    root.currentPage = currentPage;
                    root.randerList++;

                });
        },
        AddPurchaseOrder: function () {
            var root = this;
          //  this.$router.push('/AddPurchaseBill');
            root.$router.push({
                            path: '/AddPurchaseBill',
                            query: { 
                                Add: true, }
                        })
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
        PrintPurchaseBill: function (id) {
            this.GetHeaderDetail();


            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Purchase/PurchaseBillDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.printDetails = response.data;
                        root.printRender++;
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        ViewPurchaseBill: function (id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Purchase/PurchaseBillDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/PurchaseBillView',
                            query: { data: '',
                                Add: false, }
                        })
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        EditPurchaseOrder: function (id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Purchase/PurchaseBillDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddPurchaseBill',
                            query: { data: '',
                                Add: false, }
                        })
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        AddToDailyExpense: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var isPayement = true;
            root.$https.get('/Purchase/PurchaseBillDetail?Id=' + id + '&isPayement=' + isPayement, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                            path: '/adddailyexpense',
                            query: {
                                data: '',
                                Add: false,
                                formName: "ExpenseBills"
                            }
                        })
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
    },
    created() {
        // this.toDate = moment().format("DD MMM YYYY");
        // this.fromDate = moment().add(-7, 'days').format("DD MMM YYYY");

        if (this.$route.query.data == 'PurchaseBills') {
            this.$emit('update:modelValue', 'PurchaseBills');

        }
        else {
            this.$emit('update:modelValue', this.$route.name);

        }
    },
    mounted: function () {
        if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
            this.currentPage = parseInt(localStorage.getItem('currentPage'));
            this.active = (localStorage.getItem('active'));
            this.getPage();


        }
        else {
            if (this.isValid('CanDraftExpenseBill')) {
                this.makeActive("Draft");
            }
            else if (this.isValid('CanViewExpenseBill')) {
                this.makeActive("Approved");
            }



        }

        //this.getData(this.search, 1);
    },
    updated: function () {
        if (this.selected.length < this.purchaseBillList.length) {
            this.selectAll = false;
        } else if (this.selected.length == this.purchaseBillList.length) {
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