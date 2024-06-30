<template>
    <div class="row" v-if=" isValid('CanViewTCAllocation') || isValid('CanDraftTCAllocation')">
        <div class="col-lg-12 col-sm-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('TemporaryCashAllocation.TemporaryCashAllocation') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PaymentVoucherList.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('TemporaryCashAllocation.TemporaryCashAllocation') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if=" isValid('CanAddTCAllocation') || isValid('CanDraftTCAllocation') " v-on:click="AddPaymentVoucher" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('PaymentVoucherList.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('PaymentVoucherList.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('PaymentVoucherList.Search')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>

                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <ul class="nav nav-tabs" data-tabs="tabs">

                                <li class="nav-item" v-if=" isValid('CanDraftTCAllocation') "><a class="nav-link" v-bind:class="{active:active == 'Draft'}" v-on:click="makeActive('Draft')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">{{ $t('PaymentVoucherList.Draft') }}</a></li>
                                <li class="nav-item" v-if=" isValid('CanViewTCAllocation') "><a class="nav-link" v-bind:class="{active:active == 'Approved'}" v-on:click="makeActive('Approved')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('PaymentVoucherList.Post') }}</a></li>
                                <li class="nav-item" v-if="isValid('CanDraftTCAllocation')  "><a class="nav-link" v-bind:class="{active:active == 'Rejected'}" v-on:click="makeActive('Rejected')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('PaymentVoucherList.Rejected') }}</a></li>
                            </ul>

                            <div class="tab-content mt-3" id="nav-tabContent">
                                <div v-if="active == 'Draft'">
                                    <!--<div class="row" v-if="selected.length > 0">
            <div class="col-md-3 ">
                <div class="dropdown">
                    <button class="dropdown-toggle btn btn-primary  btn-block" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        {{ $t('SaleOrder.BulkAction') }}
                    </button>
                    <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton">

                        <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Approved')"> {{ $t('SaleOrder.Approve') }}</a>
                        <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Rejected')">{{ $t('SaleOrder.Reject') }}</a>
                    </div>
                </div>
            </div>
        </div>-->




                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th style="width:40px;">#</th>
                                                    <th>
                                                        {{ $t('PaymentVoucherList.VoucherNumber') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PaymentVoucherList.CreatedDate') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PaymentVoucherList.DraftBy') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PaymentVoucherList.PaymentMode') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PaymentVoucherList.BankCashAccount') }}

                                                    </th>
                                                    <th>
                                                        {{ $t('AddTemporaryCashRequest.Employee') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PaymentVoucherList.NetAmount') }}
                                                    </th>
                                                </tr>
                                            </thead>


                                            <tbody>
                                                <tr v-for="(voucher,index) in vouchersList" v-bind:key="voucher.id">
                                                    <td v-if="currentPage === 1">
                                                        {{index+1}}
                                                    </td>
                                                    <td v-else>
                                                        {{((currentPage*10)-10) +(index+1)}}
                                                    </td>

                                                    <td v-if=" isValid('CanDraftTCAllocation')">
                                                        <strong>
                                                            <a href="javascript:void(0)" v-on:click="EditPaymentVoucher(voucher.id)">{{voucher.voucherNumber}}</a>
                                                        </strong>
                                                    </td>
                                                    <td v-else>
                                                        {{voucher.voucherNumber}}
                                                    </td>
                                                    <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                        {{getDate(voucher.date)}}
                                                    </td>
                                                    <td>
                                                        {{voucher.draftBy}}
                                                    </td>
                                                    <td>
                                                        <div class="badge badge-primary" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="voucher.paymentMode==0">
                                                            {{($i18n.locale == 'en' ||isLeftToRight())?' Cash':'السيولة النقدية'}}

                                                        </div>
                                                        <div class="badge badge-success" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="voucher.paymentMode==1">
                                                            {{($i18n.locale == 'en' ||isLeftToRight())?' Bank':'مصرف'}}
                                                        </div>
                                                        <div class="badge badge-info" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="voucher.paymentMode==5">
                                                            {{($i18n.locale == 'en' ||isLeftToRight())?' Advance':'يتقدم'}}
                                                        </div>
                                                    </td>
                                                    <td>
                                                        {{voucher.bankCashAccountName}}
                                                    </td>
                                                    <td>
                                                        {{voucher.userName}}
                                                    </td>

                                                    <td>
                                                        {{currency}}  {{parseFloat(voucher.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>
                                                    <td>
                                                        <a href="javascript:void(0)" title="A5" class="btn  btn-icon btn-primary btn-sm" v-if="isValid('CanPrintTCAllocation')" v-on:click="PrintPaymentVoucher(voucher.id)"><i class=" fa fa-print"></i></a>
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
                                                <b-pagination pills size="sm" v-model="currentPage"
                                                    :total-rows="rowCount"
                                                    :per-page="10"
                                                    :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')"
                                                    :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')" >
                                                </b-pagination>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div v-if="active == 'Approved'">

                                    <!--<div class="row" v-if="selected.length > 0">
            <div class="col-md-3 ">
                <div class="dropdown">
                    <button class="dropdown-toggle btn btn-primary  btn-block" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        {{ $t('SaleOrder.BulkAction') }}
                    </button>
                    <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton">

                        <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Approved')">{{ $t('SaleOrder.Approve') }}</a>
                        <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Rejected')">{{ $t('SaleOrder.Reject') }}</a>
                    </div>
                </div>
            </div>
        </div>-->
                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th style="width:40px;">#</th>
                                                    <th style="width:150px;">
                                                        {{ $t('PaymentVoucherList.VoucherNumber') }}
                                                    </th>
                                                    <th style="width:130px;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                        {{ $t('PaymentVoucherList.CreatedDate') }}
                                                    </th>
                                                    <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                        {{ $t('PaymentVoucherList.ApprovedBy') }}

                                                    </th>
                                                    <th>
                                                        {{ $t('PaymentVoucherList.PaymentMode') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PaymentVoucherList.BankCashAccount') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('AddTemporaryCashRequest.Employee') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PaymentVoucherList.NetAmount') }}
                                                    </th>

                                                    <th>

                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(voucher,index) in vouchersList" v-bind:key="voucher.id">
                                                    <td v-if="currentPage === 1">
                                                        {{index+1}}
                                                    </td>
                                                    <td v-else>
                                                        {{((currentPage*10)-10) +(index+1)}}
                                                    </td>


                                                    <td>
                                                        {{voucher.voucherNumber}}
                                                    </td>
                                                    <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                        {{getDate(voucher.date)}}
                                                    </td>
                                                    <td>
                                                        {{voucher.approvedBy}}
                                                    </td>
                                                    <td>
                                                        <div class="badge badge-primary" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="voucher.paymentMode==0">
                                                            {{($i18n.locale == 'en' ||isLeftToRight())?' Cash':'السيولة النقدية'}}

                                                        </div>
                                                        <div class="badge badge-success" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-else-if="voucher.paymentMode==1">
                                                            {{($i18n.locale == 'en' ||isLeftToRight())?' Bank':'مصرف'}}
                                                        </div>
                                                        <div class="badge badge-info" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="voucher.paymentMode==5">
                                                            {{($i18n.locale == 'en' ||isLeftToRight())?' Advance':'يتقدم'}}
                                                        </div>
                                                    </td>
                                                    <td>
                                                        {{voucher.bankCashAccountName}}
                                                    </td>
                                                    <td>
                                                        {{voucher.userName}}
                                                    </td>

                                                    <td>
                                                        {{currency}}  {{parseFloat(voucher.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>-->

                                                    <td>
                                                        <a href="javascript:void(0)" title="A5" class="btn  btn-icon btn-primary btn-sm" v-if=" isValid('CanPrintTCAllocation')" v-on:click="PrintPaymentVoucher(voucher.id,true)"><i class=" fa fa-print"></i></a>
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
                                                <b-pagination pills size="sm" v-model="currentPage"
                                                    :total-rows="rowCount"
                                                    :per-page="10"
                                                    :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')"
                                                    :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')" >
                                                </b-pagination>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="active == 'Rejected'">
                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th style="width:40px;">#</th>
                                                    <th style="width:150px;">
                                                        {{ $t('PaymentVoucherList.VoucherNumber') }}
                                                    </th>
                                                    <th style="width:130px;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                        {{ $t('PaymentVoucherList.CreatedDate') }}
                                                    </th>
                                                    <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                        {{ $t('PaymentVoucherList.ApprovedBy') }}

                                                    </th>
                                                    <th>
                                                        {{ $t('PaymentVoucherList.PaymentMode') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PaymentVoucherList.BankCashAccount') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('AddTemporaryCashRequest.Employee') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PaymentVoucherList.NetAmount') }}
                                                    </th>

                                                    <th>

                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(voucher,index) in vouchersList" v-bind:key="voucher.id">
                                                    <td v-if="currentPage === 1">
                                                        {{index+1}}
                                                    </td>
                                                    <td v-else>
                                                        {{((currentPage*10)-10) +(index+1)}}
                                                    </td>


                                                    <td>
                                                        {{voucher.voucherNumber}}
                                                    </td>
                                                    <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                        {{getDate(voucher.date)}}
                                                    </td>
                                                    <td>
                                                        {{voucher.approvedBy}}
                                                    </td>
                                                    <td>
                                                        <div class="badge badge-primary" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="voucher.paymentMode==0">
                                                            {{($i18n.locale == 'en' ||isLeftToRight())?' Cash':'السيولة النقدية'}}

                                                        </div>
                                                        <div class="badge badge-success" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-else-if="voucher.paymentMode==1">
                                                            {{($i18n.locale == 'en' ||isLeftToRight())?' Bank':'مصرف'}}
                                                        </div>
                                                        <div class="badge badge-info" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="voucher.paymentMode==5">
                                                            {{($i18n.locale == 'en' ||isLeftToRight())?' Advance':'يتقدم'}}
                                                        </div>
                                                    </td>
                                                    <td>
                                                        {{voucher.bankCashAccountName}}
                                                    </td>
                                                    <td>
                                                        {{voucher.userName}}
                                                    </td>

                                                    <td>
                                                        {{currency}}  {{parseFloat(voucher.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>-->

                                                    <td>
                                                        <a href="javascript:void(0)" title="A5" class="btn  btn-icon btn-primary btn-sm" v-if=" isValid('CanPrintTCAllocation')" v-on:click="PrintPaymentVoucher(voucher.id,true)"><i class=" fa fa-print"></i></a>
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
                                                <b-pagination pills size="sm" v-model="currentPage"
                                                    :total-rows="rowCount"
                                                    :per-page="10"
                                                    :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')"
                                                    :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')" >
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
        <temporary-cash-allocation-Print :headerFooter="headerFooter" :printId="printId" v-if="show" v-bind:key="printRender" />
        </div>
        <div v-else> <acessdenied></acessdenied></div>
    </template>





   

                                                        <script>
                                                            import moment from "moment";
                                                            import clickMixin from '@/Mixins/clickMixin'
                                                            export default {
                                                                mixins: [clickMixin],
                                                                props: ['formName'],
                                                                data: function () {
                                                                    return {
                                                                        selected: [],
                                                                        selectAll: false,
                                                                        search: '',
                                                                        randerList: 0,
                                                                        show: false,
                                                                        show2: false,
                                                                        vouchersList: [],
                                                                        type: '',
                                                                        currentPage: 1,
                                                                        pageCount: '',
                                                                        rowCount: '',
                                                                        currency: '',
                                                                        active: 'Draft',
                                                                        printDetails: [],
                                                                        printId: '00000000-0000-0000-0000-000000000000',
                                                                        printRender: 0,
                                                                        printed: false,
                                                                        updateApprovalStatus: {
                                                                            approvalStatus: '',
                                                                            id: [],
                                                                        },
                                                                        paymentVoucher: {
                                                                            id: '00000000-0000-0000-0000-000000000000',
                                                                            date: '',
                                                                            voucherNumber: '',
                                                                            chequeNumber: '',
                                                                            narration: '',
                                                                            paymentVoucherType: '',
                                                                            amount: 0,
                                                                            approvalStatus: 'Void',
                                                                            purchaseInvoice: '00000000-0000-0000-0000-000000000000',
                                                                            saleInvoice: '00000000-0000-0000-0000-000000000000',
                                                                            bankCashAccountId: '00000000-0000-0000-0000-000000000000',
                                                                            contactAccountId: '00000000-0000-0000-0000-000000000000',
                                                                            userName: ''
                                                                        },
                                                                        headerFooter: {
                                                                            footerEn: '',
                                                                            footerAr: '',
                                                                            company: ''
                                                                        },
                                                                    }
                                                                },

                                                                methods: {
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
                                                                                    this.loading = false;
                                                                                    console.log(error);
                                                                                });
                                                                    },
                                                                    //UpdateStatusToVoid: function (id) {

                                                                    //    var root = this;
                                                                    //    var token = '';
                                                                    //    this.paymentVoucher.approvalStatus = "Void";
                                                                    //    this.paymentVoucher.id = id;
                                                                    //    this.paymentVoucher.date = moment().format("llll");
                                                                    //    this.paymentVoucher.paymentVoucherType = this.formName;
                                                                    //    this.paymentVoucher.userName = localStorage.getItem('LoginUserName');
                                                                    //    if (token == '') {
                                                                    //        token = localStorage.getItem('token');
                                                                    //    }
                                                                    //    this.$https.post('/PaymentVoucher/AddPaymentVoucher', this.paymentVoucher, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                                                                    //        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Add') {
                                                                    //            root.$swal({
                                                                    //                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                                                    //                text: response.data.message.isAddUpdate,
                                                                    //                type: 'success',
                                                                    //                icon: 'success',
                                                                    //                showConfirmButton: false,
                                                                    //                timer: 2000,
                                                                    //                timerProgressBar: true,
                                                                    //                confirmButtonClass: "btn btn-success",
                                                                    //                buttonsStyling: false
                                                                    //            }).then(function (result) {
                                                                    //                if (result) {

                                                                    //                    if (root.ispayable) {
                                                                    //                        window.location.href = "/addPaymentVoucher?formName=" + root.formName;
                                                                    //                    }
                                                                    //                }
                                                                    //            });

                                                                    //        }
                                                                    //        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Edit') {
                                                                    //            root.$swal({
                                                                    //                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                                                    //                text: response.data.message.isAddUpdate,
                                                                    //                type: 'success',
                                                                    //                icon: 'success',
                                                                    //                showConfirmButton: false,
                                                                    //                timer: 2000,
                                                                    //                timerProgressBar: true,
                                                                    //                confirmButtonClass: "btn btn-success",
                                                                    //                buttonsStyling: false
                                                                    //            }).then(function (result) {
                                                                    //                if (result) {

                                                                    //                    root.makeActive('Void');
                                                                    //                }
                                                                    //            });

                                                                    //        }
                                                                    //        else if (response.data.message.id == '00000000-0000-0000-0000-000000000000') {
                                                                    //            root.$swal({
                                                                    //                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                                                    //                text: response.data.message.isAddUpdate,
                                                                    //                type: 'error',
                                                                    //                confirmButtonClass: "btn btn-info",
                                                                    //                buttonsStyling: false
                                                                    //            });
                                                                    //        }

                                                                    //    }, function (value) {
                                                                    //        root.$swal({
                                                                    //            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                                                    //            text: value,
                                                                    //            type: 'error',
                                                                    //            confirmButtonClass: "btn btn-info",
                                                                    //            buttonsStyling: false
                                                                    //        });
                                                                    //    }
                                                                    //    ).catch(error => {

                                                                    //        var customError = JSON.stringify(error.response.data.error);
                                                                    //        root.$swal({
                                                                    //            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                                                    //            text: customError,
                                                                    //            type: 'error',
                                                                    //            confirmButtonClass: "btn btn-info",
                                                                    //            buttonsStyling: false
                                                                    //        });
                                                                    //        root.loading = false;
                                                                    //    });
                                                                    //},
                                                                    //UpdateApprovalStatus: function (approvalStatus) {

                                                                    //    var root = this;
                                                                    //    var token = '';
                                                                    //    if (token == '') {
                                                                    //        token = localStorage.getItem('token');
                                                                    //    }
                                                                    //    this.updateApprovalStatus.userName = localStorage.getItem('LoginUserName');
                                                                    //    this.updateApprovalStatus.approvalStatus = approvalStatus;
                                                                    //    this.updateApprovalStatus.id = this.selected;
                                                                    //    this.$https.post('/PaymentVoucher/UpdateApprovalStatus', this.updateApprovalStatus, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                                                                    //        if (response.data.id != '00000000-0000-0000-0000-000000000000') {
                                                                    //            root.$swal({
                                                                    //                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                                                    //                text: response.data.isAddUpdate,
                                                                    //                type: 'success',
                                                                    //                icon: 'success',
                                                                    //                showConfirmButton: false,
                                                                    //                timer: 2000,
                                                                    //                timerProgressBar: true,
                                                                    //                confirmButtonClass: "btn btn-success",
                                                                    //                buttonsStyling: false
                                                                    //            });
                                                                    //            if (approvalStatus == 'Approved') {
                                                                    //                root.makeActive('Approved');

                                                                    //            }
                                                                    //            if (approvalStatus == 'Rejected') {
                                                                    //                root.makeActive('Rejected');

                                                                    //            }

                                                                    //        }
                                                                    //        else if (response.data.id == '00000000-0000-0000-0000-000000000000') {
                                                                    //            root.$swal({
                                                                    //                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                                                    //                text: response.data.isAddUpdate,
                                                                    //                type: 'error',
                                                                    //                confirmButtonClass: "btn btn-info",
                                                                    //                buttonsStyling: false
                                                                    //            });
                                                                    //        }

                                                                    //    }, function (value) {
                                                                    //        root.$swal({
                                                                    //            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                                                    //            text: value,
                                                                    //            type: 'error',
                                                                    //            confirmButtonClass: "btn btn-info",
                                                                    //            buttonsStyling: false
                                                                    //        });
                                                                    //    }
                                                                    //    ).catch(error => {

                                                                    //        var customError = JSON.stringify(error.response.data.error);
                                                                    //        root.$swal({
                                                                    //            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                                                    //            text: customError,
                                                                    //            type: 'error',
                                                                    //            confirmButtonClass: "btn btn-info",
                                                                    //            buttonsStyling: false
                                                                    //        });
                                                                    //        root.loading = false;
                                                                    //    });
                                                                    //},
                                                                    //checkAlreadyPaid: function (data) {

                                                                    //    var root = this;
                                                                    //    var token = '';
                                                                    //    if (token == '') {
                                                                    //        token = localStorage.getItem('token');
                                                                    //    }

                                                                    //    var status = 'Credit';
                                                                    //    this.$https.get('/Sale/SaleList?status=' + status + '&isDropdown=' + true + '&isExpense=' + true + '&contactId=' + data.message.contactAccountId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                                                                    //        if (response.data != null) {
                                                                    //            var a = response.data.results.findIndex(function (i) {
                                                                    //                return i.id === data.message.saleInvoice
                                                                    //            });
                                                                    //            //var a = response.data.results.find(x.id === data.message.saleInvoice)
                                                                    //            if (a >= 0) {
                                                                    //                root.$router.push({
                                                                    //                    path: '/addPaymentVoucher?formName=' + root.formName,
                                                                    //                    query: { data: data }
                                                                    //                })
                                                                    //            }
                                                                    //            else {
                                                                    //                root.$swal({
                                                                    //                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                                                    //                    text: "You already paid this voucher in other transaction",
                                                                    //                    type: 'error',
                                                                    //                    confirmButtonClass: "btn btn-info",
                                                                    //                    buttonsStyling: false
                                                                    //                });
                                                                    //            }
                                                                    //        }
                                                                    //        else {
                                                                    //            root.$swal({
                                                                    //                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                                                    //                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                                                    //                type: 'error',
                                                                    //                confirmButtonClass: "btn btn-info",
                                                                    //                buttonsStyling: false
                                                                    //            });
                                                                    //        }
                                                                    //    });
                                                                    //},
                                                                    select: function () {


                                                                        this.selected = [];
                                                                        if (!this.selectAll) {
                                                                            for (let i in this.vouchersList) {
                                                                                this.selected.push(this.vouchersList[i].id);
                                                                            }
                                                                        }
                                                                    },
                                                                    getDate: function (x) {
                                                                        return moment(x).format('DD MMM YYYY');
                                                                    },
                                                                    makeActive: function (item) {
                                                                        this.active = item;
                                                                        this.selectAll = false;
                                                                        this.selected = [];
                                                                        this.GetPaymentVoucherData(item, this.search, this.currentPage);
                                                                    },

                                                                    AddPaymentVoucher: function () {
                                                                        var root = this;
                                                                 //       this.$router.push('/AddTemporaryCashAllocation');
                                                                        root.$router.push({
                                                                                    path: '/AddTemporaryCashAllocation',
                                                                                    query: { 
                                                                                             Add: true, }
                                                                                })
                                                                    },
                                                                    getPage: function () {

                                                                        this.GetPaymentVoucherData(this.active, this.search, this.currentPage);
                                                                    },
                                                                    GetPaymentVoucherData: function (status, search, currentPage) {

                                                                        var root = this;
                                                                        var token = '';
                                                                        if (token == '') {
                                                                            token = localStorage.getItem('token');
                                                                        }
                                                                        localStorage.setItem('currentPage', this.currentPage);
                                                                        localStorage.setItem('active', this.active);
                                                                        search == undefined ? '' : search;
                                                                        root.$https.get('PaymentVoucher/TemporaryCashAllocationList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                                                                            if (response.data != null) {
                                                                                root.vouchersList = response.data.results;
                                                                                root.pageCount = response.data.pageCount;
                                                                                root.rowCount = response.data.rowCount;
                                                                                root.currentPage = currentPage;
                                                                                root.randerList++;
                                                                            }
                                                                        });
                                                                    },
                                                                    RemovePaymentVoucher: function (id) {
                                                                        var root = this;
                                                                        this.$swal({
                                                                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟', 
                                                                            text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You will not be able to recover this!' : 'لن تتمكن من استرداد هذا!', 
                                                                            type: "warning",
                                                                            showCancelButton: true,
                                                                            confirmButtonColor: "#DD6B55",
                                                                            confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!', 
                                                                            closeOnConfirm: false,
                                                                            closeOnCancel: true
                                                                        }).then(function (result) {
                                                                            if (result.isConfirmed == true) {
                                                                                var token = '';
                                                                                if (token == '') {
                                                                                    token = localStorage.getItem('token');
                                                                                }
                                                                                root.$https.get('/PaymentVoucher/PaymentVoucherDelete?id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                                                                                    .then(function (response) {

                                                                                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000') {
                                                                                            root.$swal({
                                                                                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                                                                                text: response.data.message.isDeleted,
                                                                                                type: 'success',
                                                                                                icon: 'success',
                                                                                                showConfirmButton: false,
                                                                                                timer: 800,
                                                                                                timerProgressBar: true,
                                                                                                confirmButtonClass: "btn btn-success",
                                                                                                buttonsStyling: false
                                                                                            });
                                                                                        }
                                                                                    },
                                                                                        function () {
                                                                                            root.$swal({
                                                                                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                                                                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Delete UnSuccessfully' : 'حذف غير ناجح',
                                                                                                type: 'error',
                                                                                                confirmButtonClass: "btn btn-danger",
                                                                                                buttonsStyling: false
                                                                                            });
                                                                                        });
                                                                            }
                                                                            else {
                                                                                root.$swal({
                                                                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!',
                                                                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!',
                                                                                    type: 'success',
                                                                                    icon: 'success',
                                                                                    showConfirmButton: false,
                                                                                    timer: 800,
                                                                                    timerProgressBar: true,
                                                                                    confirmButtonClass: "btn btn-success",
                                                                                    buttonsStyling: false
                                                                                });
                                                                            }
                                                                        });
                                                                    },
                                                                    EditPaymentVoucher: function (id) {
                                                                        var root = this;
                                                                        var token = '';
                                                                        if (token == '') {
                                                                            token = localStorage.getItem('token');
                                                                        }

                                                                        root.$https.get('/PaymentVoucher/TemporaryCashAllocationDetails?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                                                                            if (response.data != null) {
                                                                                root.$store.GetEditObj(response.data);
                                                                                root.$router.push({
                                                                                    path: '/AddTemporaryCashAllocation',
                                                                                    query: { data: '',
                                                                                             Add: false, }
                                                                                })
                                                                            }
                                                                        });

                                                                    },
                                                                    PrintPaymentVoucher: function (value) {

                                                                        this.GetHeaderDetail();
                                                                        this.printId = value;

                                                                        this.show = true;
                                                                        this.printRender++;
                                                                    },
                                                                },
                                                                updated: function () {
                                                                    if (this.selected.length < this.vouchersList.length) {
                                                                        this.selectAll = false;
                                                                    } else if (this.selected.length == this.vouchersList.length) {

                                                                        if (this.selected.length == 0) {
                                                                            this.selectAll = false;
                                                                        }
                                                                        else {
                                                                            this.selectAll = true
                                                                        }
                                                                    }
                                                                },
                                                                watch: {
                                                                    search: function (val) {
                                                                        if (val != '' && val != undefined && val != null) {
                                                                            this.GetPaymentVoucherData(this.active, val, 1,);
                                                                        }
                                                                        else {
                                                                            this.GetPaymentVoucherData(this.active, '', 1,);
                                                                        }
                                                                    },
                                                                },
                                                                created: function () {

                                                                    this.$emit('update:modelValue', this.$route.name);
                                                                    if (this.isValid('CanDraftTCAllocation')) {
                                                                        this.makeActive("Draft");
                                                                    }
                                                                    else if (this.isValid('CanViewTCAllocation')) {
                                                                        this.makeActive("Approved");
                                                                    }
                                                                    //this.GetPaymentVoucherData(this.active, this.search, this.currentPage);



                                                                },
                                                                mounted: function () {

                                                                    this.currency = localStorage.getItem('currency');
                                                                }
                                                            }
                                                        </script>
