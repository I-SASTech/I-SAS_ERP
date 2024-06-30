<template>
    <div class="row"
        v-if="(((isValid('CanViewJV') || isValid('CanDraftJV')) && documentName == 'JournalVoucher') || ((isValid('CanViewOC') || isValid('CanDraftOC')) && documentName == 'OpeningCash'))">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page_title" v-if="documentName == 'JournalVoucher'">
                                    {{
                                        $t('JournalVoucherView.JournalVouchers')
                                    }}
                                </h4>
                                <h4 class="page_title" v-if="documentName == 'OpeningCash'">
                                    {{
                                        $t('JournalVoucherView.OpeningCashList')
                                    }}
                                </h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item">
                                        <a href="javascript:void(0);">
                                            {{
                                                $t('JournalVoucherView.Home')
                                            }}
                                        </a>
                                    </li>
                                    <li class="breadcrumb-item active" aria-current="page"
                                        v-if="documentName == 'JournalVoucher'">
                                        {{ $t('JournalVoucherView.JournalVouchers') }}
                                    </li>
                                    <li class="breadcrumb-item active" aria-current="page"
                                        v-if="documentName == 'OpeningCash'">
                                        {{
                                            $t('JournalVoucherView.OpeningCashList')
                                        }}
                                    </li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanDraftJV') || isValid('CanAddJV')" v-on:click="AddJournalVoucher"
                                    href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('JournalVoucherView.AddNew') }}
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

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-9" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('JournalVoucherView.Search')"
                                    aria-label="Example text with button addon" aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class=" col-lg-3 mt-1" v-if="!advanceFilters">

                            <button v-on:click="search22(true)" :disabled ="!search" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled ="!search" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div>
                        <ul class="nav nav-tabs" data-tabs="tabs">
                            <li class="nav-item">
                                <a class="nav-link" v-bind:class="{ active: active == 'Draft' }"
                                    v-if="isValid('CanDraftJV') && documentName == 'JournalVoucher'"
                                    v-on:click="makeActive('Draft')" id="v-pills-home-tab" data-toggle="pill"
                                    href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">
                                    {{ $t('JournalVoucherView.Draft') }}
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" v-bind:class="{ active: active == 'Draft' }"
                                    v-if="isValid('CanDraftOC') && documentName == 'OpeningCash'"
                                    v-on:click="makeActive('Draft')" id="v-pills-home-tab" data-toggle="pill"
                                    href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">
                                    {{ $t('JournalVoucherView.Draft') }}
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" v-bind:class="{ active: active == 'Approved' }"
                                    v-if="isValid('CanViewJV') && documentName == 'JournalVoucher'"
                                    v-on:click="makeActive('Approved')" id="v-pills-profile-tab" data-toggle="pill"
                                    href="#v-pills-profile" role="tab" aria-controls="v-pills-profile"
                                    aria-selected="false">
                                    {{ $t('JournalVoucherView.Post') }}
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" v-bind:class="{ active: active == 'Approved' }"
                                    v-if="isValid('CanViewOC') && documentName == 'OpeningCash'"
                                    v-on:click="makeActive('Approved')" id="v-pills-profile-tab" data-toggle="pill"
                                    href="#v-pills-profile" role="tab" aria-controls="v-pills-profile"
                                    aria-selected="false">
                                    {{ $t('JournalVoucherView.Post') }}
                                </a>
                            </li>
                        </ul>
                    </div>
                    <div class="tab-content mt-3" id="nav-tabContent">
                        <div v-if="active == 'Draft'">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th width="5%">#</th>
                                                    <th v-if="documentName == 'JournalVoucher'" width="15%">
                                                        {{ $t('JournalVoucherView.VoucherNo') }}
                                                    </th>
                                                    <th v-if="documentName == 'OpeningCash'" width="15%">
                                                        {{ $t('JournalVoucherView.DocumentNumber') }}
                                                    </th>
                                                    <th width="10%">
                                                        {{ $t('JournalVoucherView.Date') }}
                                                    </th>
                                                    <th width="10%">
                                                        Created By
                                                    </th>
                                                    <th width="20%">
                                                        Description
                                                    </th>
                                                    <th width="10%" v-if="allowBranches">
                                                        {{ $t('DailyExpense.BranchCode') }}
                                                    </th>
                                                    <th width="10%">
                                                        {{ $t('JournalVoucherView.TotalDebit') }}
                                                    </th>
                                                    <th width="10%">
                                                        {{ $t('JournalVoucherView.TotalCredit') }}
                                                    </th>
                                                    <th width="10%"
                                                        v-if="(isValid('CanViewDetailOC') && documentName == 'OpeningCash') || (isValid('CanViewDetailJV') && documentName == 'JournalVoucher')"
                                                        class="text-center">
                                                        {{ $t('JournalVoucherView.Action') }}

                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(journalVoucher, index) in journalVoucherList"
                                                    v-bind:key="journalVoucher.id">

                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>


                                                    <td
                                                        v-if="(isValid('CanEditJV') && documentName === 'JournalVoucher') || (isValid('CanEditOC') && documentName === 'OpeningCash')">
                                                        <strong>
                                                            <a href="javascript:void(0)"
                                                                v-on:click="EditJV(journalVoucher.id, false)">
                                                                {{
                                                                    journalVoucher.voucherNumber
                                                                }}
                                                            </a>
                                                        </strong>
                                                    </td>

                                                    <td v-else>
                                                        {{ journalVoucher.voucherNumber }}
                                                    </td>
                                                    <td>
                                                        {{ getDate(journalVoucher.date) }}
                                                    </td>
                                                    <td>
                                                        {{ journalVoucher.createdUser }}
                                                    </td>
                                                    <td>
                                                        {{ journalVoucher.narration }}
                                                    </td>
                                                    <td v-if="allowBranches">
                                                        {{ journalVoucher.branchCode }}
                                                    </td>
                                                    <td>
                                                        {{ currency }}
                                                        {{
                                                            parseFloat(journalVoucher.totalDr).toFixed(3).slice(0,
                                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                    "$1,")
                                                        }}
                                                    </td>
                                                    <td>
                                                        {{ currency }}
                                                        {{
                                                            parseFloat(journalVoucher.totalCr).toFixed(3).slice(0,
                                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                    "$1,")
                                                        }}
                                                    </td>
                                                    <td v-if="(isValid('CanViewDetailOC') && documentName == 'OpeningCash') || (isValid('CanViewDetailJV') && documentName == 'JournalVoucher')"
                                                        class="text-end">

                                                        <button type="button" class="btn btn-light dropdown-toggle"
                                                            data-bs-toggle="dropdown" aria-expanded="false">
                                                            {{
                                                                $t('JournalVoucherView.Action')
                                                            }} <i class="mdi mdi-chevron-down"></i>
                                                        </button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item" hhref="javascript:void(0)"
                                                                v-on:click="EditJV(journalVoucher.id, true)">
                                                                {{
                                                                    $t('JournalVoucherView.ViewInvoice')
                                                                }}
                                                            </a>
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="PrintRdlc(journalVoucher.id, false)"
                                                                v-if="isValid('CanViewDetailJV')">
                                                                {{
                                                                    $t('JournalVoucherView.PrintA4')
                                                                }}
                                                            </a>
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="PrintRdlc(journalVoucher.id, true)"
                                                                v-if="isValid('CanViewDetailJV')">{{ $t('Download') }}</a>
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
                        <div v-if="active == 'Approved'">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>

                                                    <th width="5%">#</th>
                                                    <th v-if="documentName == 'JournalVoucher'" width="15%">
                                                        {{ $t('JournalVoucherView.VoucherNo') }}
                                                    </th>
                                                    <th v-if="documentName == 'OpeningCash'" width="15%">
                                                        {{ $t('JournalVoucherView.DocumentNumber') }}
                                                    </th>
                                                    <th width="10%">
                                                        {{ $t('JournalVoucherView.Date') }}
                                                    </th>
                                                    <th width="10%">
                                                        Created By
                                                    </th>
                                                    <th width="20%">
                                                        Description
                                                    </th>
                                                    <th width="10%" v-if="allowBranches">
                                                        {{ $t('DailyExpense.BranchCode') }}
                                                    </th>
                                                    <th width="10%">
                                                        {{ $t('JournalVoucherView.TotalDebit') }}
                                                    </th>
                                                    <th width="10%">
                                                        {{ $t('JournalVoucherView.TotalCredit') }}
                                                    </th>
                                                    <th width="10%"
                                                        v-if="(isValid('CanViewDetailOC') && documentName == 'OpeningCash') || (isValid('CanViewDetailJV') && documentName == 'JournalVoucher')"
                                                        class="text-center">
                                                        {{ $t('JournalVoucherView.Action') }}
                                                    </th>


                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(journalVoucher, index) in journalVoucherList"
                                                    v-bind:key="journalVoucher.id">

                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>

                                                    <td
                                                        v-if="(isValid('CanEditJV') && documentName === 'JournalVoucher') || (isValid('CanEditOC') && documentName === 'OpeningCash')">
                                                        <strong>
                                                            <a href="javascript:void(0)"
                                                                v-on:click="EditJV(journalVoucher.id, false)">
                                                                {{
                                                                    journalVoucher.voucherNumber
                                                                }}
                                                            </a>
                                                        </strong>
                                                    </td>
                                                    <td v-else>

                                                        {{ journalVoucher.voucherNumber }}

                                                    </td>
                                                    <td>
                                                        {{ getDate(journalVoucher.date) }}
                                                    </td>
                                                    <td>
                                                        {{ journalVoucher.createdUser }}
                                                    </td>
                                                    <td>
                                                        {{ journalVoucher.narration }}
                                                    </td>
                                                    <td v-if="allowBranches">
                                                        {{ journalVoucher.branchCode }}
                                                    </td>
                                                    <td>
                                                        {{ currency }}
                                                        {{
                                                            parseFloat(journalVoucher.totalDr).toFixed(3).slice(0,
                                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                    "$1,")
                                                        }}
                                                    </td>
                                                    <td>
                                                        {{ currency }}
                                                        {{
                                                            parseFloat(journalVoucher.totalCr).toFixed(3).slice(0,
                                                                -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                    "$1,")
                                                        }}
                                                    </td>
                                                    <td v-if="(isValid('CanViewDetailOC') && documentName == 'OpeningCash') || (isValid('CanViewDetailJV') && documentName == 'JournalVoucher')"
                                                        class="text-end">
                                                        <button type="button" class="btn btn-light dropdown-toggle"
                                                            data-bs-toggle="dropdown" aria-expanded="false">
                                                            {{
                                                                $t('JournalVoucherView.Action')
                                                            }} <i class="mdi mdi-chevron-down"></i>
                                                        </button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item" hhref="javascript:void(0)"
                                                                v-on:click="EditJV(journalVoucher.id, true)">
                                                                {{
                                                                    $t('JournalVoucherView.ViewInvoice')
                                                                }}
                                                            </a>
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="PrintRdlc(journalVoucher.id, false)"
                                                                v-if="isValid('CanViewDetailJV')">
                                                                {{
                                                                    $t('JournalVoucherView.PrintA4')
                                                                }}
                                                            </a>
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="PrintRdlc(journalVoucher.id, true)"
                                                                v-if="isValid('CanViewDetailJV')">
                                                                {{
                                                                    $t('PdfDownload')
                                                                }}
                                                            </a>
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
        <loading v-model:active="loadingrc" :can-cancel="true" :is-full-page="false"></loading>
        <JournalVoucherPrint :printDetails="printDetails" :headerFooter="headerFooter" v-if="printDetails.length != 0"
            v-bind:key="printRender"></JournalVoucherPrint>
        <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport"
            @close="show = false" @IsSave="IsSave" />

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>

import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
export default {
    components: {

    },
    props: ['formName'],
    mixins: [clickMixin],
    data: function () {

        return {
            documentName: this.formName,
            advanceFilters: false,
            allowBranches: false,
            show: false,
            loadingrc: false,
            reportsrc: '',
            changereport: 0,
            active: 'Draft',
            search: '',
            journalVoucherList: [],
            currentPage: 1,
            randerList: 0,
            pageCount: '',
            rowCount: '',
            loading: true,
            currency: '',
            disable: false,

            selected: [],
            selectAll: false,
            updateApprovalStatus: {
                id: '',
                approvalStatus: ''
            },
            printDetails: '',
            headerFooter: {
                footerEn: '',
                footerAr: '',
                company: ''
            },
            printRender: 0,



        }
    },
    watch: {
        // search: function (val) {
        //     this.getData(val, 1, this.active);
        // },
        documentName: function () {

            if (this.$route.query.data == 'JournalVoucherViews' + this.documentName) {
                this.$emit('update:modelValue', 'JournalVoucherViews' + this.documentName);
                if (this.documentName == 'JournalVoucher') {
                    this.documentName = 'JournalVoucher';
                    if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                        this.currentPage = parseInt(localStorage.getItem('currentPage'));
                        this.active = (localStorage.getItem('active'));
                        this.getPage();


                    }

                }
                if (this.documentName == 'OpeningCash') {
                    this.documentName = 'OpeningCash';
                    if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                        this.currentPage = parseInt(localStorage.getItem('currentPage'));
                        this.active = (localStorage.getItem('active'));
                        this.getPage();


                    }

                }

            }
            else {
                this.currentPage = 1;
                this.active = 'Draft';
                if (this.documentName == 'JournalVoucher') {
                    this.documentName = 'JournalVoucher';
                    this.getData(this.search, this.currentPage, this.active, this.documentName);

                }
                if (this.documentName == 'OpeningCash') {
                    this.documentName = 'OpeningCash';
                    this.getData(this.search, this.currentPage, this.active, this.documentName);

                }
                this.$emit('update:modelValue', this.$route.name + this.documentName);

            }


        }

    },
    methods: {
        search22: function () {
            this.getData(this.search, this.currentPage, this.active, this.documentName);
        },

        clearData: function () {
            this.search = "";
            this.getData(this.search, this.currentPage, this.active, this.documentName);

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
        GotoPage: function (link) {
            this.$router.push({ path: link });
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
        PrintInvoice: function (value) {
            this.GetHeaderDetail();
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.printDetails = [];
            root.$https.get("/JournalVoucher/JournalVoucherDetail?id=" + value, {
                headers: { Authorization: `Bearer ${token}` },
            })
                .then(function (response) {
                    if (response.data != null) {

                        root.printDetails = response.data;
                        root.printRender++;
                    }
                });
        },
        EditJV: function (id, view) {

            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }
            var childFormName = this.documentName;

            root.$https.get('/JournalVoucher/JournalVoucherDetail?id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                root.jv = response.data;
                root.$store.GetEditObj(root.jv);
                root.$router.push({
                    path: '/addjournalvoucher',
                    query:
                    {
                        data: '',
                        Add: false,
                        view: view,
                        formName: childFormName,
                    }
                })
            });
        },
        RemoveJV: function (Id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/JournalVoucher/DeleteJournalVoucher?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    if (response.data.id != '00000000-0000-0000-0000-000000000000') {

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                            text: response.data.isAddUpdate,
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        });
                    } else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: response.data.isAddUpdate,
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            buttonsStyling: false
                        });
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

        AddJournalVoucher: function () {
            this.$router.push({
                path: '/addjournalvoucher',
                query: {
                    Add: true,
                    formName: this.documentName,
                }
            });

        },
        getDate: function (date) {
            return moment(date).format('l');
        },
        makeActive: function (item) {


            this.active = item;
            this.selectAll = false;
            this.selected = [];
            this.getData(this.search, 1, item, this.documentName);
        },
        UpdateApprovalStatus: function (approvalStatus) {


            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.updateApprovalStatus.approvalStatus = approvalStatus;
            this.updateApprovalStatus.id = this.selected;
            root.$https.post('/JournalVoucher/UpdateApprovalStatus', this.updateApprovalStatus, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000') {

                            root.$swal({
                                title: 'Status Changed!',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonsStyling: false
                            }).then(function (result) {
                                if (result) {
                                    window.location.href = "/JournalVoucher";
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
                });
        },
        select: function () {
            this.selected = [];
            if (!this.selectAll) {
                for (let i in this.journalVoucherList) {
                    this.selected.push(this.journalVoucherList[i].id);
                }
            }
        },

        getPage: function () {


            this.getData(this.search, this.currentPage, this.active, this.documentName);
        },
        IsSave: function () {
            this.show = false;
        },
        PrintRdlc: function (id, isDownload) {
            var companyId = '';
            companyId = localStorage.getItem('CompanyID');

            var root = this;
            if (isDownload) {
                this.loadingrc = true;
                this.$https.get(this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?Id=' + id + '&isFifo=' + false + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + false + '&CompanyId=' + companyId + '&formName=' + this.documentName + '&isDownload=' + isDownload
                    , { responseType: 'blob' }).then(function (response) {
                        root.loadingrc = false;

                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        var date = moment().format('DD MMM YYYY');
                        link.setAttribute('download', root.documentName + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();

                    })
            }
            else {

                var isBlind = localStorage.getItem('IsBlindPrint') == 'true' ? true : false;
                if (isBlind) {

                    this.show = false;
                } else {
                    this.show = true;
                }
                this.reportsrc = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?Id=' + id + '&isFifo=' + false + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + false + '&CompanyId=' + companyId + '&formName=' + this.documentName + '&isDownload=' + isDownload
                this.changereport++;
            }
        },
        getData: function (search, currentPage, status, documentName) {
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            var isOpeningCash = false;
            if (documentName == 'OpeningCash') {
                isOpeningCash = true;
            }
            else {
                isOpeningCash = false;
            }
            localStorage.setItem('currentPage', this.currentPage);
            localStorage.setItem('active', this.active);
            var branchId = localStorage.getItem('BranchId');

            root.$https.get('/JournalVoucher/JournalVoucherList?searchTerm=' + search + '&pageNumber=' + currentPage + '&status=' + status + '&isOpeningCash=' + isOpeningCash + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    root.loading = false;

                    root.journalVoucherList = response.data.results;
                    if (root.documentName == 'OpeningCash') {

                        if (response.data.rowCount > 1) {
                            root.disable = true;
                        }
                    }
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.currentPage = currentPage;
                    root.randerList++;
                    root.loading = false;
                });
        },



    },
    created: function () {

        if (this.$route.query.data == 'JournalVoucherViews' + this.documentName) {
            this.$emit('update:modelValue', 'JournalVoucherViews' + this.documentName);


        }
        else {
            this.$emit('update:modelValue', this.$route.name + this.documentName);
            localStorage.removeItem("fromDate");
            localStorage.removeItem("toDate");
            localStorage.removeItem("active");
            localStorage.removeItem("currentPage");
        }
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;

    },
    mounted: function () {


        if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {

            this.currentPage = parseInt(localStorage.getItem('currentPage'));
            if (this.currentPage == undefined || isNaN(this.currentPage)) {
                this.currentPage = 1;
            }
            this.active = (localStorage.getItem('active'));
            this.getPage();


        }
        else {

            if (this.isValid('CanDraftJV') && this.documentName == 'JournalVoucher') {
                this.makeActive("Draft");
            }
            else if (this.isValid('CanViewJV') && this.documentName == 'JournalVoucher') {
                this.makeActive("Approved");
            }
            else if (this.isValid('CanViewOC') && this.documentName == 'OpeningCash') {
                this.makeActive("Approved");
            }
            else if (this.isValid('CanDraftOC') && this.documentName == 'OpeningCash') {
                this.makeActive("Draft");
            }
        }

        this.currency = localStorage.getItem('currency');
    }

}
</script>
