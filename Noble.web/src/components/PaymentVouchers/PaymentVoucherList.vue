<template>
    <div class="row" v-if="isValid('CanDraftCPR') || isValid('CanViewCPR') || isValid('CanViewSPR') || isValid('CanDraftSPR') || isValid('CanViewPettyCash') || isValid('CanDraftPettyCash')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col" v-if="formName == 'BankReceipt'">
                                <h4 class="page-title">{{ $t('PaymentVoucherList.CustomerPayReceipt') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PaymentVoucherList.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('PaymentVoucherList.CustomerPayReceipt') }}
                                    </li>
                                </ol>
                            </div>
                            <div class="col" v-if="formName == 'BankPay'">
                                <h4 class="page-title">{{ $t('PaymentVoucherList.SupplierPaymentReceipt') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PaymentVoucherList.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        {{$t('PaymentVoucherList.SupplierPaymentReceipt')}}
                                    </li>
                                </ol>
                            </div>
                            <div class="col" v-if="formName == 'AdvancePurchase'">
                                <h4 class="page-title">Advance Payment</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PaymentVoucherList.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        Advance Payment
                                    </li>
                                </ol>
                            </div>
                            <div class="col" v-if="formName == 'AdvanceReceipt' ">
                                <h4 class="page-title">{{ $t('PurchaseOrderPayment.AdvancePayment')}}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PaymentVoucherList.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('PurchaseOrderPayment.AdvancePayment')}}
                                    </li>
                                </ol>
                            </div>
                            <div class="col" v-if="formName == 'PettyCash'">
                                <h4 class="page-title">{{ $t('PaymentVoucherList.PettyCash') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PaymentVoucherList.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('PaymentVoucherList.PettyCash') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="((isValid('CanDraftPettyCash') || isValid('CanAddPettyCash')) && formName == 'PettyCash') || ((isValid('CanDraftCPR') || isValid('CanAddCPR')) && formName == 'BankReceipt') || ((isValid('CanDraftSPR') || isValid('CanAddSPR')) && (formName == 'BankPay' || formName=='AdvancePurchase')) || ((isValid('CanDraftSPR') || isValid('CanAddSPR')) && formName == 'AdvanceReceipt')" v-on:click="AddPaymentVoucher" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('PaymentVoucherList.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
                                </a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="accordion" id="accordionExample">
                                <div class="accordion-item">
                                    <h5 class="accordion-header m-0" id="headingTwo">
                                        <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo" v-on:click="GetSaleDashboardRecord" v-bind:key="randerDiv">
                                            KPIs Dashboard
                                        </button>
                                    </h5>
                                    <div :class="['accordion-collapse', { show: isAccordionOpen }]" id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                                        <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>
                                        <div class="accordion-body">
                                            <div class="row p-2 ">
                                                <div class="col-lg-4 col-md-6 col-12" v-if="formName=='BankPay' || formName=='BankReceipt' || formName=='AdvanceReceipt' || formName=='AdvancePurchase'">
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <div class="row align-items-center">
                                                                <div class="col">
                                                                    <h4 class="card-title" v-if="formName=='AdvanceReceipt'">Advance Payment Receipt</h4>
                                                                    <h4 class="card-title" v-else>{{$t('PaymentVoucherList.CustomerReceiptDetail')}}</h4>
                                                                </div>
                                                                
                                                                <!--end col-->
                                                            </div>
                                                            <!--end row-->
                                                        </div>
                                                        <!--end card-header-->
                                                        <div class="card-body">
                                                            <div class="">
                                                                <apexchart type="donut" width="340" height="330" :options="chartOptions" :series="series"></apexchart>
                                                                <span class="badge badge-soft-primary">Post</span> : {{ paymentVoucherLookUp.posted }} &nbsp; &nbsp; &nbsp; &nbsp;
                                                                <span class="badge badge-soft-success">Draft</span> : {{ paymentVoucherLookUp.draft }}<br>
                                                                <span class="badge badge-soft-danger">Rejected</span> : {{ paymentVoucherLookUp.rejected }}&nbsp; &nbsp; &nbsp; &nbsp;
                                                                <span class="badge badge-soft-dark">Total</span> :{{ paymentVoucherLookUp.totalReceipt }}
                                                            </div>
                                                        </div>
                                                        <!--end card-body-->
                                                    </div>
                                                    <!--end card-->
                                                </div>
                                                <div class="col-lg-4 col-md-6 col-12" v-if="formName=='BankPay' || formName=='BankReceipt' || formName=='AdvanceReceipt' || formName=='AdvancePurchase'">
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <div class="row align-items-center">
                                                                <div class="col">
                                                                    <h4 class="card-title">{{$t('PaymentVoucherList.ModeOfPayment')}}</h4>
                                                                </div>
                                                                <!--end col-->
                                                            </div>
                                                            <!--end row-->
                                                        </div>
                                                        <!--end card-header-->
                                                        <div class="card-body">
                                                            <div class="">
                                                                <apexchart type="pie" width="320" height="330" :options="chartOptions2" :series="series2"></apexchart>
                                                                <span class="badge badge-soft-primary">Cash Receipt:</span> :{{currency}} {{parseFloat(paymentVoucherLookUp.totalCashReceipt ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}<br>
                                                                <span class="badge badge-soft-success">Bank Receipt</span> :{{currency}} {{parseFloat(paymentVoucherLookUp.totalBankReceipt ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                
                                                                <span v-if="formName == 'BankReceipt' && (isValid('MultipleAdvancePayment') || isValid('PremiumAdvancePayment'))" class="badge badge-soft-success">
    Advance Receipt: {{ currency }} {{ parseFloat(paymentVoucherLookUp.advancebalacne).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}
</span>
                                                                <span class="badge badge-soft-dark">Total</span> :{{currency}} {{parseFloat(paymentVoucherLookUp.totalCashBank ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                            </div>
                                                        </div>
                                                        <!--end card-body-->
                                                    </div>
                                                    <!--end card-->
                                                </div>
                                                <div class="col-lg-4 col-md-6 col-12" v-if="formName=='BankPay' || formName=='AdvanceReceipt' || formName=='AdvancePurchase'">
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <div class="row align-items-center">
                                                                <div class="col">
                                                                    <h4 class="card-title">{{$t('PaymentVoucherList.NatureOfPayment')}}</h4>
                                                                </div>
                                                                <!--end col-->
                                                            </div>
                                                            <!--end row-->
                                                        </div>
                                                        <!--end card-header-->
                                                        <div class="card-body">
                                                            <div class="">
                                                                <apexchart type="donut" width="340" height="330" :options="chartOptions3" :series="series3"></apexchart>
                                                                <span class="badge badge-soft-primary" v-if="formName != 'AdvanceReceipt'">Normal :{{currency}} {{parseFloat(paymentVoucherLookUp.normal ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span>
                                                                <span class="badge badge-soft-success">Advance</span> :{{currency}} {{parseFloat(paymentVoucherLookUp.advance ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}<br>
                                                                <span class="badge badge-soft-danger">Security</span> :{{currency}} {{parseFloat(paymentVoucherLookUp.security ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                <span class="badge badge-soft-dark">Total</span>  :{{currency}} {{parseFloat(paymentVoucherLookUp.totalNormal ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                            </div>
                                                        </div>
                                                        <!--end card-body-->
                                                    </div>
                                                    <!--end card-->
                                                </div>

                                            </div>

                                        </div>
                                        
                                    </div>
                                </div>

                            </div>
                        </div>

                        <!--end row-->
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-8" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-soft-primary"  type="button" id="button-addon1">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" v-if="formName=='CashReceipt'" class="form-control" :placeholder="$t('PaymentVoucherList.SearchByVoucher')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                <input v-model="search" type="text" v-if="formName=='BankReceipt' || formName=='AdvanceReceipt' " class="form-control" :placeholder="$t('PaymentVoucherList.SearchByVoucherBank')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                <input v-model="search" type="text" v-if="formName=='CashPay'" class="form-control" :placeholder="$t('PaymentVoucherList.SearchByVoucherCashExpense')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                <input v-model="search" type="text" v-if="formName=='BankPay' || formName=='AdvancePurchase'  " class="form-control" :placeholder="$t('PaymentVoucherList.SearchByVoucherBankExpense')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                <input v-model="search" type="text" v-if="formName=='PettyCash'" class="form-control" :placeholder="$t('PaymentVoucherList.SearchByPettyCash')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                <!-- <input v-model="search" type="text" class="form-control" :placeholder="$t('Sale.SearchOfInvoice')"
                                           aria-label="Example text with button addon" aria-describedby="button-addon1, button-addon2"> -->
                                <a class="btn btn-soft-primary" v-on:click="AdvanceFilter" id="button-addon2" value="Advance Filter">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">
                                <button v-on:click="search22(true)" :disabled="!search" type="button" class="btn btn-outline-primary mt-3">
                                    {{ $t('Sale.ApplyFilter') }}
                              </button>
                          <button v-on:click="clearData(false)" :disabled="!search" type="button"
                            class="btn btn-outline-primary mx-2 mt-3">
                            {{ $t('Sale.ClearFilter') }}
                          </button>

                          </div>
                    </div>
                    <div class="row pb-3" v-if="advanceFilters">
                        <!-- <div class="col-lg-3">
                            <div class="form-group">
                                <label>Year</label>
                                <yeardropdown ref="yearDropdown" v-model="year"></yeardropdown>

                            </div>
                        </div> -->
                        <div class=" col-lg-3 form-group" v-if="allowBranches && mainBranch">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="false" :islayout="false" ref="BranchDropdown"/>
                </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Month</label>
                                <monthpicker v-model="monthObj" @update:modelValue="GetMonth" v-bind:disabled="isDisableMonth" v-if="!isDisableMonth" v-bind:key="randerforempty" />
                                <input class="form-control" v-else disabled />
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <label class="text  font-weight-bolder"> Date Selection :</label>
                            <multiselect v-model="natureOfPayment" :options="['Created Date', 'Voucher Date']" :show-labels="false" placeholder="Date Selection">
                            </multiselect>

                        </div>
                        
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('Sale.FromDate') }}</label>
                                <datepicker v-model="fromDate" v-bind:isDisable="isDisable" @update:modelValue="GetDate1" v-bind:key="randerforempty" />
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('Sale.ToDate') }}</label>
                                <datepicker v-model="toDate" v-bind:isDisable="isDisable" @update:modelValue="GetDate1" v-bind:key="randerforempty" />
                            </div>
                        </div>

                        <div class="col-lg-3" >
                                            <label>
                                                {{ $t('AddPaymentVoucher.PaymentMode') }}:
                                                
                                            </label>

                                            <div class="form-group">

                                                <multiselect v-model="paymentMode" :options="paymentModeOpt" :show-labels="false" placeholder="Select Type">
                                                </multiselect>

                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <label>
                                                {{ $t('AddPaymentVoucher.PaymentType') }}:
                                            </label>
                                                <multiselect  v-model="paymentMethod" :options="paymentTypeOptions" :show-labels="false" placeholder="Select Type">
                                                </multiselect>
                                        </div>
                        

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">
                            <label class="text  font-weight-bolder"> Created By :</label>
                            <usersDropdown v-model="user" ref="userDropdown" :isloginhistory="isloginhistory" />
                        </div>
                        <div class="col-lg-3" v-if="formName == 'AdvanceReceipt'">
                            <label class="text  font-weight-bolder"> Nature of Payment :</label>
                            <multiselect v-model="newNatureOfPayment" :options="['Advance', 'Security']" :show-labels="false" placeholder="Select Payment Nature">
                            </multiselect>

                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">
                            <button v-on:click="FilterRecord(true)" :disabled ="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="FilterRecord(false)" :disabled ="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item" v-if="(isValid('CanDraftPettyCash') && formName == 'PettyCash') || (isValid('CanDraftCPR') && (formName == 'BankReceipt') || formName=='AdvanceReceipt') || (isValid('CanDraftSPR') && formName == 'BankPay' || formName=='AdvancePurchase')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Draft' }" v-on:click="makeActive('Draft')" data-bs-toggle="tab" href="#home" role="tab" aria-selected="true">
                                {{$t('PaymentVoucherList.Draft')}}
                            </a>
                        </li>
                        <li class="nav-item" v-if="(isValid('CanViewPettyCash') && formName == 'PettyCash') || (isValid('CanViewCPR') && (formName == 'BankReceipt') || formName=='AdvanceReceipt')|| (isValid('CanViewSPR') && formName == 'BankPay' || formName=='AdvancePurchase')" v-on:click="makeActive('Approved')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Approved' }" data-bs-toggle="tab" href="#profile" role="tab" aria-selected="false">
                                {{$t('PaymentVoucherList.Post')}}
                            </a>
                        </li>
                        <!--<li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'Void'}" v-if="  isValid('Can Void Bank Pay') && formName=='PettyCash'" v-on:click="makeActive('Void')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('PaymentVoucherList.Void') }}</a></li>-->
                        <li class="nav-item" v-if="(isValid('CanRejectPettyCash') && formName == 'PettyCash') || (isValid('CanRejectCPR') && (formName == 'BankReceipt') || formName=='AdvanceReceipt') || (isValid('CanRejectSPR') && formName == 'BankPay' || formName=='AdvancePurchase')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Rejected' }" v-on:click="makeActive('Rejected')" data-bs-toggle="tab" href="#settings" role="tab" aria-selected="false">
                                {{$t('PaymentVoucherList.Rejected')}}
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
                                            <th style="width:40px;">#</th>
                                            <th style="width:150px;">
                                                {{ $t('PaymentVoucherList.VoucherNumber') }}
                                            </th>
                                            <th style="width:130px;">
                                                {{ $t('PaymentVoucherList.CreatedDate') }}
                                            </th>
                                            <th>
                                                {{ $t('Voucher Date') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.CreatedBy') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.DraftBy') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.PaymentMode') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.PaymentType') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.BankCashAccount') }}

                                            </th>
                                            <th>
                                                <span v-if="formName == 'CashReceipt'  || formName == 'AdvanceReceipt' || formName == 'BankReceipt' || formName == 'PettyCash'">
                                                    {{ $t('PaymentVoucherList.CustomerAccount') }}
                                                </span>
                                                <span v-if="formName == 'BankPay' || formName=='AdvancePurchase' || formName == 'CashPay'">
                                                    {{ $t('PaymentVoucherList.SupplierAccount') }}
                                                </span>
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.NetAmount') }}
                                            </th>
                                            <th>

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(voucher, index) in vouchersList" v-bind:key="voucher.id">

                                            <td v-if="currentPage === 1">
                                                {{ index + 1 }}
                                            </td>
                                            <td v-else>
                                                {{ ((currentPage * 10) - 10) + (index + 1) }}
                                            </td>

                                            <td v-if="(isValid('CanEditPettyCash') && formName == 'PettyCash') || (isValid('CanEditCPR') && (formName == 'BankReceipt' || formName == 'AdvanceReceipt') || formName=='AdvanceReceipt') || (isValid('CanEditSPR') && formName == 'BankPay' || formName=='AdvancePurchase')">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditPaymentVoucher(voucher.id)">
                                                        {{ voucher.voucherNumber }}
                                                    </a>
                                                </strong>
                                            </td>
                                            <td v-else>
                                                {{ voucher.voucherNumber }}
                                            </td>
                                            <td>
                                                {{ getDate(voucher.date) }}
                                            </td>
                                            <td>
                                                {{ getDate(voucher.paymentDate) }}
                                            </td>
                                            <td>
                                                {{ voucher.draftBy }}
                                            </td>
                                            <td>
                                                {{ voucher.draftBy }}
                                            </td>
                                            <td>
                                                <div class="badge badge-soft-primary" v-if="voucher.paymentMode == 0">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Cash' : 'السيول النقدية' }}

                                                </div>
                                                <div class="badge badge-soft-success" v-if="voucher.paymentMode == 1">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Bank' : 'مصرف' }}
                                                </div>
                                                <div class="badge badge-soft-info" v-if="voucher.paymentMode == 5">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Advance' : 'يتقدم' }}
                                                </div>
                                            </td>
                                            <td v-if="voucher.paymentMethods=='Default'">
                                                ---
                                            </td>
                                            <td v-else>
                                                {{ voucher.paymentMethods }}
                                            </td>
                                            <td>
                                                {{ voucher.bankCashAccountName }}
                                            </td>

                                            <td>
                                                {{ voucher.contactAccountName }}
                                            </td>

                                            <td v-if="allowBranches">
                                                {{ voucher.branchCode }}
                                            </td>
                                            <td>
                                                {{ currency }}
                                                {{ parseFloat(voucher.amount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}
                                            </td>
                                            <td>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-if="(isValid('CanViewDetailPettyCash') && formName == 'PettyCash') || (isValid('CanViewDetailCPR') && (formName == 'BankReceipt' || formName == 'AdvanceReceipt') ) || (isValid('CanViewDetailSPR') &&( formName == 'BankPay' || formName=='AdvancePurchase')) || (isValid('CanViewDetailSPR') && formName == 'AdvanceReceipt')" v-on:click="EditPaymentVoucher(voucher.id, true)"> {{ $t('PaymentVoucherList.View') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id, 'A4')" v-if="(isValid('CanPrintPettyCashTemplate2') && formName == 'PettyCash') || (isValid('CanPrintCPR') && (formName == 'BankReceipt' || formName == 'AdvanceReceipt')) || (isValid('CanPrintSPR') && formName == 'BankPay' || formName=='AdvancePurchase') || (isValid('CanViewDetailSPR') && formName == 'AdvanceReceipt')">{{ $t('PaymentVoucherList.A5Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id, 'A4')" v-if="IsPaksitanClient">{{ $t('PaymentVoucherList.A4Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id,'A5')" v-if="(isValid('CanPrintPettyCashTemplate1') && formName=='PettyCash') || (isValid('CanPrintCPR') && (formName == 'BankReceipt') || formName=='AdvanceReceipt') || (isValid('CanPrintSPR') && formName=='BankPay') || (isValid('CanViewDetailSPR') && formName == 'AdvanceReceipt')">{{ $t('PaymentVoucherList.A5Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id,'A3')" v-if="(isValid('CanPrintPettyCashTemplate1') && formName=='PettyCash') || (isValid('CanPrintCPR') && (formName == 'BankReceipt') || formName=='AdvanceReceipt') || (isValid('CanPrintSPR') && formName=='BankPay') || (isValid('CanViewDetailSPR') && formName == 'AdvanceReceipt')">{{ $t('A3') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id,'Print')" v-if="(isValid('CanPrintPettyCashTemplate1') && formName=='PettyCash') || (isValid('CanPrintCPR') && (formName == 'BankReceipt') || formName=='AdvanceReceipt') || (isValid('CanPrintSPR') && formName=='BankPay') || (isValid('CanViewDetailSPR') && formName == 'AdvanceReceipt')">{{ $t('print') }}</a>
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
                                        {{ currentPage }} 
                                        {{ $t('Pagination.to') }} 
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
                                        {{ currentPage * 10 }} of 
                                        {{ rowCount }} 
                                        {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{ $t('Pagination.Showing') }} 
                                        {{ (currentPage * 10) - 9 }} 
                                        {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }} 
                                        {{ $t('Pagination.of') }} 
                                        {{ rowCount }} 
                                        {{  $t('Pagination.entries') }}
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
                                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane pt-3" id="profile" role="tabpanel" v-bind:class="{ active: active == 'Approved' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th style="width:40px;">#</th>
                                            <th style="width:150px;">
                                                {{ $t('PaymentVoucherList.VoucherNumber') }}
                                            </th>
                                            <th style="width:130px;">
                                                {{ $t('PaymentVoucherList.CreatedDate') }}
                                            </th>
                                            <th>
                                                {{ $t('Voucher Date') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.CreatedBy') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.ApprovedBy') }}

                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.PaymentMode') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.PaymentType') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.BankCashAccount') }}
                                            </th>
                                            <th>
                                                <span v-if="formName == 'CashReceipt' || formName == 'BankReceipt' || formName == 'AdvanceReceipt' || formName == 'PettyCash'">
                                                    {{ $t('PaymentVoucherList.CustomerAccount') }}
                                                </span>
                                                <span v-if="formName == 'BankPay' || formName=='AdvancePurchase' || formName == 'CashPay'">
                                                    {{ $t('PaymentVoucherList.SupplierAccount') }}
                                                </span>
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.NetAmount') }}
                                            </th>

                                            <th>

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(voucher, index) in vouchersList" v-bind:key="voucher.id">
                                            <td v-if="currentPage === 1">
                                                {{ index + 1 }}
                                            </td>
                                            <td v-else>
                                                {{ ((currentPage * 10) - 10) + (index + 1) }}
                                            </td>

                                            <td v-if="(isValid('CanEditPettyCash') && formName == 'PettyCash') || (isValid('CanEditCPR') && formName == 'BankReceipt') || (isValid('CanEditSPR') && formName == 'BankPay')">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditPaymentVoucher(voucher.id,false)">{{ voucher.voucherNumber }}</a>
                                                </strong>
                                            </td>
                                            <td v-else>
                                                {{ voucher.voucherNumber }}
                                            </td>
                                            <td>
                                                {{ getDate(voucher.date) }}
                                            </td>
                                            <td>
                                                {{ getDate(voucher.paymentDate) }}
                                            </td>
                                            <td>
                                                {{ voucher.approvedBy }}
                                            </td>
                                            <td>
                                                {{ voucher.approvedBy }}
                                            </td>
                                            <td>
                                                <div class="badge badge-soft-primary" v-if="voucher.paymentMode == 0">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Cash' : 'السيولة النقدية' }}

                                                </div>
                                                <div class="badge badge-soft-success" v-else-if="voucher.paymentMode == 1">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Bank' : 'مصرف' }}
                                                </div>
                                                <div class="badge badge-soft-info" v-if="voucher.paymentMode == 5">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Advance' : 'يتقدم' }}
                                                </div>
                                            </td>
                                            <td v-if="voucher.paymentMethods== 'Default'">
                                                ---
                                            </td>
                                            <td v-else>
                                                {{ voucher.paymentMethods }}
                                            </td>
                                            <td>
                                                {{ voucher.bankCashAccountName }}
                                            </td>
                                            <td>
                                                {{ voucher.contactAccountName }}
                                            </td>
                                            <td v-if="allowBranches">
                                                {{ voucher.branchCode }}
                                            </td>

                                            <td>
                                                {{ currency }} {{ parseFloat(voucher.amount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>

                                            <td>
                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('PaymentVoucherList.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-if="(isValid('CanViewDetailPettyCash') && formName == 'PettyCash') || (isValid('CanViewDetailCPR') && formName == 'BankReceipt' || formName=='AdvanceReceipt') || (isValid('CanViewDetailSPR') && formName == 'BankPay' || formName=='AdvancePurchase') || (isValid('CanViewDetailSPR') && formName == 'AdvanceReceipt')" v-on:click="EditPaymentVoucher(voucher.id,true)">{{ $t('PaymentVoucherList.View') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id, 'A4')" v-if="(isValid('CanPrintPettyCashTemplate2') && formName == 'PettyCash') || (isValid('CanPrintCPR') && (formName == 'BankReceipt' || formName=='AdvanceReceipt')) || (isValid('CanPrintSPR') && formName == 'BankPay' || formName=='AdvancePurchase') || (isValid('CanViewDetailSPR') && formName == 'AdvanceReceipt')">{{ $t('PaymentVoucherList.A5Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id, 'A4')" v-if="IsPaksitanClient">{{ $t('PaymentVoucherList.A4Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id,'A5')" v-if="(isValid('CanPrintPettyCashTemplate1') && formName=='PettyCash') || (isValid('CanPrintCPR') && (formName == 'BankReceipt' || formName=='AdvanceReceipt')) || (isValid('CanPrintSPR') && formName=='BankPay') || (isValid('CanViewDetailSPR') && formName == 'AdvanceReceipt')">{{ $t('PaymentVoucherList.A5Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id,'A3')" v-if="(isValid('CanPrintPettyCashTemplate1') && formName=='PettyCash') || (isValid('CanPrintCPR') && (formName == 'BankReceipt' || formName=='AdvanceReceipt')) || (isValid('CanPrintSPR') && formName=='BankPay') || (isValid('CanViewDetailSPR') && formName == 'AdvanceReceipt')">{{ $t('A3') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id,'print')" v-if=" isValid('MultiplePayment') &&  (isValid('CanPrintPettyCashTemplate1') && formName=='PettyCash') || (isValid('CanPrintCPR') && (formName == 'BankReceipt' || fromName=='AdvanceReceipt')) || (isValid('CanPrintSPR') && formName=='BankPay') || (isValid('CanViewDetailSPR') && formName == 'AdvanceReceipt')">{{ $t('print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="RefundModel(voucher)" v-if="formName == 'BankReceipt' && !voucher.isRefund">Refund</a>
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
                                        {{ currentPage }} 
                                        {{ $t('Pagination.to') }} 
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
                                        {{ currentPage * 10 }} of 
                                        {{ rowCount }} 
                                        {{  $t('Pagination.entries') }}
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
                                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane pt-3" id="settings" role="tabpanel" v-bind:class="{ active: active == 'Rejected' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th style="width:150px;">
                                                {{ $t('PaymentVoucherList.VoucherNumber') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.CreatedDate') }}
                                            </th>
                                            <th>
                                                {{ $t('Voucher Date') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.CreatedBy') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.RejectBy') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.ModeOfPayment') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.PaymentMethod') }}
                                            </th>

                                            <th>
                                                {{ $t('PaymentVoucherList.BankCashAccount') }}
                                            </th>
                                            <th>
                                                <span v-if="formName == 'CashReceipt' || formName == 'BankReceipt' || formName == 'PettyCash'">
                                                    {{ $t('PaymentVoucherList.CustomerAccount') }}
                                                </span>
                                                <span v-if="formName == 'BankPay' || formName=='AdvancePurchase' || formName == 'CashPay'">
                                                    {{ $t('PaymentVoucherList.SupplierAccount') }}
                                                </span>
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th>
                                                {{ $t('PaymentVoucherList.NetAmount') }}
                                            </th>
                                            <th>

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(voucher, index) in vouchersList" v-bind:key="voucher.id">
                                            <td v-if="currentPage === 1">
                                                {{ index + 1 }}
                                            </td>
                                            <td v-else>
                                                {{ ((currentPage * 10) - 10) + (index + 1) }}
                                            </td>

                                            <td>
                                                {{ voucher.voucherNumber }}
                                            </td>
                                            <td>
                                                {{ getDate(voucher.date) }}
                                            </td>
                                            <td>
                                                {{ getDate(voucher.paymentDate) }}
                                            </td>
                                            <td>
                                                {{ voucher.rejectBy }}
                                            </td>
                                            <td>
                                                {{ voucher.rejectBy }}
                                            </td>
                                            <td>
                                                <div class="badge badge-soft-primary" v-if="voucher.paymentMode == 0">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Cash' : 'السيولة النقدية' }}

                                                </div>
                                                <div class="badge badge-soft-success" v-else-if="voucher.paymentMode == 1">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Bank' : 'مصرف' }}
                                                </div>
                                                <div class="badge badge-soft-info" v-if="voucher.paymentMode == 5">
                                                    {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Advance' : 'يتقدم' }}
                                                </div>
                                            </td>
                                            <td v-if="voucher.paymentMethods == 'Default'">
                                                ---
                                            </td>
                                            <td v-else>
                                                {{ voucher.paymentMethods }}
                                            </td>
                                            <td>
                                                {{ voucher.bankCashAccountName }}
                                            </td>
                                            <td>
                                                {{ voucher.contactAccountName }}
                                            </td>
                                            <td v-if="allowBranches">
                                                {{ voucher.branchCode }}
                                            </td>

                                            <td>
                                                {{ currency }}
                                                {{ parseFloat(voucher.amount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,") }}
                                            </td>
                                            <td>
                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('PaymentVoucherList.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-if="(isValid('CanViewDetailPettyCash') && formName == 'PettyCash') || (isValid('CanViewDetailCPR') && formName == 'BankReceipt') || (isValid('CanViewDetailSPR') && formName == 'BankPay' || formName=='AdvancePurchase')" v-on:click="EditPaymentVoucher(voucher.id, true)"> {{ $t('PaymentVoucherList.View') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id, 'A4')" v-if="(isValid('CanPrintPettyCashTemplate2') && formName == 'PettyCash') || (isValid('CanPrintCPR') && formName == 'BankReceipt') || (isValid('CanPrintSPR') && formName == 'BankPay' || formName=='AdvancePurchase')">{{ $t('PaymentVoucherList.A5Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id, 'A4')" v-if="IsPaksitanClient">{{ $t('PaymentVoucherList.A4Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(voucher.id,'A5')" v-if="(isValid('CanPrintPettyCashTemplate1') && formName=='PettyCash') || (isValid('CanPrintCPR') && formName=='BankReceipt') || (isValid('CanPrintSPR') && formName=='BankPay')">{{ $t('PaymentVoucherList.A5Print') }}</a>
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
                                        {{ currentPage }} 
                                        {{ $t('Pagination.to') }} 
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
                                        {{ currentPage * 10 }} of 
                                        {{ rowCount }} 
                                        {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{ $t('Pagination.Showing') }} 
                                        {{ (currentPage * 10) - 9 }} 
                                        {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }} 
                                        {{ $t('Pagination.of') }} 
                                        {{ rowCount }} 
                                        {{$t('Pagination.entries') }}
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
                                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport" @close="show=false" @IsSave="IsSave" />
            
            <payment-refund-model v-if="showRefund" :show="showRefund" :newpaymentvoucher="refundVoucher" @close="showRefund=false" @save="getPage()" />
        </div>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
    import moment from "moment";
    import Multiselect from 'vue-multiselect'

    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        props: ['formName'],
        data: function () {
            return {
                request: 0,
                randerDiv: 0,
                allowBranches: false,
                mainBranch:false,
                branchIds: '',
                branchId: '',
                isAccordionOpen: false,
                loading: false,
                series: [],
                chartOptions: {
                    labels: ['Post', 'Draft', 'Rejected']
                },

                series2: [],
                chartOptions2: {
                    labels: []
                },
                series3: [],
                chartOptions3: {
                    labels: []
                },
                paymentVoucherLookUp: {
                    posted: 0,
                    draft: 0,
                    rejected: 0,
                    totalReceipt: 0,
                    cashReceipt: 0,
                    totalCashReceipt: 0,
                    bankReceipt: 0,
                    totalBankReceipt: 0,
                    totalCashBank: 0,
                    normal: 0,
                    advance: 0,
                    security: 0,
                    totalNormal: 0,
                    advancebalacne: 0
                },
                changereport: 0,
                isloginhistory: true,
                isDisable: false,
                isDisableMonth: false,

                counter: 0,
                reportsrc: '',
                user: '',
                natureOfPayment: '',
                newNatureOfPayment:'',
                fromDate: '',
                toDate: '',
                monthObj: '',
                year: 0,
                randerforempty: 0,
                month: 0,

                selected: [],
                optionsOfYear: [],
                selectAll: false,
                advanceFilters: false,
                search: '',
                randerList: 0,
                show3: false,
                show: false,
                show2: false,
                vouchersList: [],
                type: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                currency: '',
                IsPaksitanClient: false,
                userID: '',

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
                refundVoucher: {},
                showRefund: false,

                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                paymentMode: '',
                paymentMethod:'',
                //Payment Type Options
                paymentTypeOptions:[],
                paymentTypeOptionsAr:[],

                //Payment Mode Options
                paymentModeOpt: [],
                paymentModeOptAr: [],

                
            }
        },
        computed: {
            isFilterButtonDisabled() {
      return (
        this.branchIds ||
        this.paymentMode ||
        this.monthObj ||
        this.toDate ||
        this.fromDate ||
        this.paymentMethod ||
        this.newNatureOfPayment ||
        this.user || this.natureOfPayment
      );
    },
  },
        
        watch: {
            branchIds: function () {
                this.branchId = '';  
            },
            formName: function () {
                
                this.request = 0;
                this.isAccordionOpen = false;

                this.randerDiv++;
                if (this.formName == 'BankReceipt') {
                    this.makeActive('Draft');
                    this.GetOptions();
                }
                else if (this.formName == 'CashReceipt') {
                    this.makeActive('Draft');
                }
                else if (this.formName == 'BankPay') {
                    this.makeActive('Draft');
                    this.GetOptions();
                }
                else if (this.formName == 'AdvancePurchase') {
                    this.makeActive('Draft');
                    this.GetOptions();
                }
                else if (this.formName == 'PettyCash') {
                    this.makeActive('Draft');
                }
                else if (this.formName == 'CashPay') {
                    this.makeActive('Draft');
                }
                else if (this.formName == 'AdvanceReceipt') {
                    this.makeActive('Draft');
                    this.GetOptions();
                }
                else {
                    this.makeActive('Draft');
                    this.GetOptions();
                }

                if (this.$route.query.data == 'PaymentVoucherLists' + this.formName) {
                    this.$emit('update:modelValue', 'PaymentVoucherLists' + this.formName);

                } else {

                    localStorage.removeItem("active");
                    localStorage.removeItem("currentPage");
                    this.$emit('update:modelValue', this.$route.name + this.formName);

                }

                if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                    this.currentPage = parseInt(localStorage.getItem('currentPage'));
                    this.active = (localStorage.getItem('active'));
                    this.GetPaymentVoucherData(this.formName, this.active, this.search, this.currentPage);

                } else {
                    this.GetPaymentVoucherData(this.formName, 'Draft', this.search, 1);

                }
            },
        },

        methods: {
            RefundModel: function (voucher) {
                var paymentMode = '';

                if (voucher.paymentMode == 0) {
                    paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cash' : 'السيولة النقدية';
                }
                if (voucher.paymentMode == 1) {
                    paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'مصرف';
                }
                if (voucher.paymentMode == 5) {
                    paymentMode = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Advance' : 'يتقدم';
                }

                this.refundVoucher = {
                    id: '00000000-0000-0000-0000-000000000000',
                    paymentVoucherId: voucher.id,
                    date: voucher.date,
                    voucherNumber: '',
                    narration: '',
                    chequeNumber: '',
                    amount: voucher.amount,
                    approvalStatus: 'Approved',
                    paymentMode: paymentMode,
                    paymentMethod: voucher.paymentMethods,
                    bankCashAccountId: voucher.bankCashAccountId,
                    contactAccountId: voucher.contactAccountId,

                    taxMethod: '',
                    taxRateId: ''
                };

                this.showRefund = true;
            },

            GetDate1: function () {

                if (this.fromDate != '' || this.toDate != '') {
                    this.isDisableMonth = true;
                    this.year = 0;
                    this.month = 0;
                    this.monthObj = '';

                } else {
                    this.isDisableMonth = false;
                }

            },
            GetMonth: function () {


                if (this.monthObj != undefined && this.monthObj != ''  ) {
                    this.isDisable = true;
                    this.fromDate = '';
                    this.toDate = '';

                    this.month = moment(this.monthObj).format("MM");
                    this.year = moment(this.monthObj).format("YYYY");

                }

            },

            AdvanceFilter: function () {

                this.advanceFilters = !this.advanceFilters;
                if (this.advanceFilters == false) {
                    this.FilterRecord(false);
                }

            },
            FilterRecord: function (type) {

                if (type) {
                    if (this.fromDate != '') {
                        if (this.toDate == '') {
                            this.$swal({
                                title: 'Error',
                                text: "Please Select To Date ",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 2000,
                                timerProgressBar: true,
                            });

                            return;

                        }
                    }
                    if (this.toDate != '') {
                        if (this.fromDate == '') {
                            this.$swal({
                                title: 'Error',
                                text: "Please Select From Date ",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 2000,
                                timerProgressBar: true,
                            });
                            return;
                        }
                    }

                    if (this.monthObj != undefined && this.monthObj != '') {
                        this.month = moment(this.monthObj).format("MM");
                        this.year = moment(this.monthObj).format("YYYY");
                    }
                    if (this.user.id != undefined) {
                        this.userId = this.user.id;
                    }

                    this.GetPaymentVoucherData(this.formName, this.active, '', 1);

                } else {
                    this.isDisable = false;
                    this.isDisableMonth = false;
                    if (this.$refs.userDropdown != null) {
                        this.$refs.userDropdown.EmptyRecord();
                    }
                    this.user = '';
                    this.userId = '';
                    if (this.$refs.yearDropdown != null) {
                        this.$refs.yearDropdown.EmptyRecord();
                    }
                    this.year = 0;
                    this.fromDate = '';
                    this.toDate = '';
                    this.natureOfPayment = '';
                    this.newNatureOfPayment ='';
                    this.paymentMode = '';
                    this.paymentMethod= '';
                    this.month = 0;
                    this.monthObj = '';
                    this.randerforempty++;
                    this.GetPaymentVoucherData(this.formName, this.active, '', 1);

                    this.branchIds= '';

                    if (this.$refs.BranchDropdown != undefined)
                    this.$refs.BranchDropdown.Remove();

                }

            },

            // search22: function () {
            //     this.GetPaymentVoucherData(this.search,this.formName, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId, this.customerId, this.customerType,this.natureOfPayment,this.year,this.month);
            // },
            search22: function (val) {
                if (val != '' && val != undefined && val != null) {
                    this.GetPaymentVoucherData(this.formName, this.active, this.search, 1,this.currentPage, this.fromDate, this.toDate, this.terminalId, this.userId, this.customerId, this.customerType,this.natureOfPayment,this.year,this.month);
                } else {
                    this.GetPaymentVoucherData(this.formName, this.active, '', 1,this.currentPage, this.fromDate, this.toDate,  this.terminalId, this.userId, this.customerId, this.customerType,this.natureOfPayment,this.year,this.month);
                }
            },

            clearData: function () {
                this.search="";
                this.GetPaymentVoucherData(this.formName, this.active, '', 1,);

            },

            IsSave: function () {
                this.show = false;
            },
            GotoPage: function (link) {
                this.$router.push({
                    path: link
                });
            },
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {

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

                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), {
                    headers: {
                        Authorization: `Bearer ${token}`
                    },
                })
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

                root.$https.get('/Sale/printSettingDetail?branchId=' + localStorage.getItem('BranchId'), {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
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
            UpdateStatusToVoid: function (id) {

                var root = this;
                var token = '';
                this.paymentVoucher.approvalStatus = "Void";
                this.paymentVoucher.id = id;
                this.paymentVoucher.date = moment().format("llll");
                this.paymentVoucher.paymentVoucherType = this.formName;
                this.paymentVoucher.userName = localStorage.getItem('LoginUserName');
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/PaymentVoucher/AddPaymentVoucher', this.paymentVoucher, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Add') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        }).then(function (result) {
                            if (result) {

                                if (root.ispayable) {
                                    window.location.href = "/addPaymentVoucher?formName=" + root.formName;
                                }
                            }
                        });

                    }
                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.type == 'Edit') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Updated!' : 'تم التحديث!',
                            text: response.data.message.isAddUpdate,
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        }).then(function (result) {
                            if (result) {

                                root.makeActive('Void');
                            }
                        });

                    } else if (response.data.message.id == '00000000-0000-0000-0000-000000000000') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: response.data.message.isAddUpdate,
                            type: 'error',
                            confirmButtonClass: "btn btn-info",
                            buttonsStyling: false
                        });
                    }

                }, function (value) {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: value,
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                }).catch(error => {

                    var customError = JSON.stringify(error.response.data.error);
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: customError,
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                    root.loading = false;
                });
            },
            UpdateApprovalStatus: function (approvalStatus) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.updateApprovalStatus.userName = localStorage.getItem('LoginUserName');
                this.updateApprovalStatus.approvalStatus = approvalStatus;
                this.updateApprovalStatus.id = this.selected;
                this.$https.post('/PaymentVoucher/UpdateApprovalStatus', this.updateApprovalStatus, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {

                    if (response.data.id != '00000000-0000-0000-0000-000000000000') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: response.data.isAddUpdate,
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        });
                        if (approvalStatus == 'Approved') {
                            root.makeActive('Approved');

                        }
                        if (approvalStatus == 'Rejected') {
                            root.makeActive('Rejected');

                        }

                    } else if (response.data.id == '00000000-0000-0000-0000-000000000000') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: response.data.isAddUpdate,
                            type: 'error',
                            confirmButtonClass: "btn btn-info",
                            buttonsStyling: false
                        });
                    }

                }, function (value) {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: value,
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                }).catch(error => {

                    var customError = JSON.stringify(error.response.data.error);
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: customError,
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    });
                    root.loading = false;
                });
            },
            checkAlreadyPaid: function (data) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var status = 'Credit';
                this.$https.get('/Sale/SaleList?status=' + status + '&isDropdown=' + true + '&isExpense=' + true + '&contactId=' + data.message.contactAccountId + '&branchId=' + localStorage.getItem('BranchId'), {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {

                    if (response.data != null) {
                        var a = response.data.results.sales.findIndex(function (i) {
                            return i.id === data.message.saleInvoice
                        });
                        //var a = response.data.results.find(x.id === data.message.saleInvoice)
                        if (a >= 0) {
                            root.$store.GetEditObj(data);
                            root.$router.push({
                                path: '/addPaymentVoucher',
                                query: {
                                    data: '',
                                    Add:false,
                                    formName : root.formName,
                                }
                            })
                        } else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'You already paid this voucher in other transaction' : 'لقد دفعت بالفعل هذه القسيمة في معاملة أخرى',
                                type: 'error',
                                confirmButtonClass: "btn btn-info",
                                buttonsStyling: false
                            });
                        }
                    } else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            type: 'error',
                            confirmButtonClass: "btn btn-info",
                            buttonsStyling: false
                        });
                    }
                });
            },
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
                this.GetPaymentVoucherData(this.formName, item, this.search, this.currentPage);
            },

            AddPaymentVoucher: function () {
                var root = this;
                root.$router.push({
                                path: '/addPaymentVoucher',
                                query: {
                                    Add: true,
                                    formName : this.formName,
                                }
                            })
            },
            getPage: function () {

                this.GetPaymentVoucherData(this.formName, this.active, this.search, this.currentPage);
            },
            GetSaleDashboardRecord: function () {
                this.isAccordionOpen = !this.isAccordionOpen;
                if (this.request == 0) {
                    var root = this;
                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    this.loading = true;
                    var branchId = localStorage.getItem('BranchId');


                    this.$https.get('/PaymentVoucher/PaymentVoucherListDashboard?paymentVoucherType=' + this.formName + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {

                            if (response.data != null) {

                                root.series = [];
                                root.series.push(response.data.posted);
                                root.series.push(response.data.draft);
                                root.series.push(response.data.rejected);
                                root.series2 = [];
                                root.series2.push(response.data.totalCashReceipt);
                                root.series2.push(response.data.totalBankReceipt);
                                if((root.isValid('MultipleAdvancePayment') || root.isValid('PremiumAdvancePayment')) && root.formName =='BankReceipt')
                                {
                                    root.series2.push(response.data.advancebalacne);
                                }
                                root.series3 = [];
                                if(root.formName =='AdvancePurchase' && root.formName =='BankPay' )
                                {
                                    root.series3.push(response.data.normal);
                                }
                                
                                root.series3.push(response.data.advance);
                                root.series3.push(response.data.security);
                                root.paymentVoucherLookUp = {
                                    posted: response.data.posted,
                                    draft: response.data.draft,
                                    rejected: response.data.rejected,
                                    totalReceipt: response.data.totalReceipt,
                                    cashReceipt: response.data.cashReceipt,
                                    totalCashReceipt: response.data.totalCashReceipt,
                                    bankReceipt: response.data.bankReceipt,
                                    totalBankReceipt: response.data.totalBankReceipt,
                                    totalCashBank: response.data.totalCashBank,
                                    normal: response.data.normal,
                                    advance: response.data.advance,
                                    security: response.data.security,
                                    totalNormal: response.data.totalNormal,
                                    advancebalacne: response.data.advancebalacne
                                };
                                root.loading = false;

                            }
                        });

                }
                this.request++;

            },
            GetPaymentVoucherData: function (vtype, status, search, currentPage) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if(isNaN(currentPage))
                {
                    currentPage = 1
                }
                var branchId ='';
                if(root.branchIds != null && root.branchIds != undefined && root.branchIds != ''){
                    branchId = root.branchIds;
                }
                else{
                     branchId = localStorage.getItem('BranchId');
                }
                


                localStorage.setItem('currentPage', this.currentPage);
                localStorage.setItem('active', this.active);
                search == undefined ? '' : search;
                root.$https.get('PaymentVoucher/PaymentVoucherList?paymentVoucherType=' + vtype + '&status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&natureOfPayment=' + this.natureOfPayment + '&newNatureOfPayment=' + this.newNatureOfPayment + '&paymentMode=' + this.paymentMode + '&paymentMethod=' + this.paymentMethod + '&userId=' + this.userId + '&month=' + this.month + '&year=' + this.year + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.vouchersList = response.data.results.paymentVouchers;
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
                        root.$https.get('/PaymentVoucher/PaymentVoucherDelete?id=' + id, {
                            headers: {
                                "Authorization": `Bearer ${token}`
                            }
                        })
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
                    } else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cancelled' : 'ألغيت!',
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
            PrintRdlc: function (id, val) {
                var isBlind = localStorage.getItem('IsBlindPrint') == 'true' ? true : false;
                if (isBlind) {
                    this.show = false;
                } else {
                    this.show = true;
                }
                var companyId = '';
                companyId = localStorage.getItem('CompanyID');
                
                var agging=false;
                if(val=='print')
                {
                    agging=true;
                 }

                this.reportsrc = this.$ReportServer + '/SalesModuleReports/CustomerPaymentReceipt/CustomerPaymentReceiptReport.aspx?Id=' + id + '&isFifo=' + false + '&openBatch=' + 0 + '&isReturn=' + false +
                 '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + false + 
                 '&CompanyId=' + companyId + '&formName=' + this.formName + '&PageType=' + val+'&aging='+agging ;
                this.changereport++;
            },
            EditPaymentVoucher: function (id, view) {
                var root = this;
                var childFormName = this.formName;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (view) {
                    root.$https.get('/PaymentVoucher/PaymentVoucherDetails?Id=' + id, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addPaymentVoucher',
                                query: {
                                    data:'',
                                    Add: false,
                                    isView: true,
                                    formName : childFormName,
                                }
                            })
                        }
                    });
                } else {
                    root.$https.get('/PaymentVoucher/PaymentVoucherDetails?Id=' + id, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addPaymentVoucher',
                                query: {
                                    data:'',
                                    Add: false,
                                    formName : childFormName,
                                }
                            })
                        }
                    });
                }
            },
            PrintPaymentVoucher: function (value, print2) {

                if (print2 == 'A4') {
                    this.show3 = true;
                } else {
                    this.show = false;
                    this.show2 = false;
                    if (print2) {
                        this.show2 = true;
                    } else {
                        this.show = true;
                    }
                }

                this.GetHeaderDetail();
                var id = value;
                this.printId = id;
                this.printRender++;
            },
            GetOptions: function()
            {
                
                if((this.isValid('SimpleAdvancePayment') && this.formName=='BankReceipt') || (this.isValid('SimpleSupplierAdvancePayment') && this.formName == 'BankPay'))
                {
                    this.paymentModeOpt = ['Cash', 'Bank'];
                    this.paymentModeOptAr = [ 'السيولة النقدية', 'مصرف'];

                    this.paymentTypeOptions=['Cheque', 'Transfer','Deposit','Card'];
                    this.paymentTypeOptionsAr=['بطاقة','التحقق من', 'تحويل','الوديعة'];
                }
                else if((this.isValid('StandardAdvancePayment') && this.formName=='BankReceipt') || (this.isValid('StandardSupplierAdvancePayment')  && this.formName == 'BankPay'))
                {
                    this.paymentModeOpt = ['Cash', 'Bank'];
                    this.paymentModeOptAr = [ 'السيولة النقدية', 'مصرف'];

                    this.paymentTypeOptions=['Cheque', 'Transfer','Deposit','Debit Card', 'Credit Card'];
                    this.paymentTypeOptionsAr=['بطاقة إئتمان','بطاقة ائتمان','التحقق من', 'تحويل','الوديعة'];
                }
                else if((this.isValid('PremiumAdvancePayment') && this.formName=='BankReceipt') || (this.isValid('PremiumSupplierAdvancePayment') && this.formName == 'BankPay'))
                {
                    this.paymentModeOpt = ['Cash', 'Bank', 'Advance'];
                    this.paymentModeOptAr = [ 'يتقدم','السيولة النقدية', 'مصرف'];

                    this.paymentTypeOptions=['Cheque', 'Transfer', 'Int. Transfer','Deposit','Debit Card', 'Credit Card Master', 'Credit Card Visa', 'American Express'];
                    this.paymentTypeOptionsAr=['أمريكان اكسبريس','تأشيرة بطاقة الائتمان','سيد بطاقة الائتمان','بطاقة ائتمان','التحقق من','كثافة العمليات. تحويل', 'تحويل','الوديعة'];
                }
                else if((this.isValid('MultipleAdvancePayment') && this.formName=='BankReceipt' ) || (this.isValid('MultipleSupplierAdvancePayment') && this.formName == 'BankPay'))
                {
                    this.paymentModeOpt = ['Cash', 'Bank', 'Advance'];
                    this.paymentModeOptAr = [ 'يتقدم','السيولة النقدية', 'مصرف'];

                    this.paymentTypeOptions=['Cheque', 'Transfer', 'Int. Transfer','Deposit','Debit Card', 'Credit Card Master', 'Credit Card Visa', 'American Express'];
                    this.paymentTypeOptionsAr=['أمريكان اكسبريس','تأشيرة بطاقة الائتمان','سيد بطاقة الائتمان','بطاقة ائتمان','التحقق من','كثافة العمليات. تحويل', 'تحويل','الوديعة'];
                }
                else 
                {
                    this.paymentModeOpt = ['Cash', 'Bank'];
                    this.paymentModeOptAr = [ 'السيولة النقدية', 'مصرف'];


                    this.paymentTypeOptions=['Cheque', 'Transfer','Deposit','Debit Card', 'Credit Card'];
                    this.paymentTypeOptionsAr=['بطاقة إئتمان','بطاقة ائتمان','التحقق من', 'تحويل','الوديعة'];

                    
                }
                
                if((this.isValid('MultipleAdvancePayment') || this.isValid('PremiumAdvancePayment')) && this.formName =='BankReceipt' ) 
                {
                    this.chartOptions2.labels=[];
                  this.chartOptions2.labels = ['Cash', 'Bank','Advance'];
                }
                else if(this.formName == 'AdvanceReceipt')
                {
                    this.chartOptions2.labels=[];
                    this.chartOptions3.labels=[];

                    this.chartOptions3.labels=['Advance', 'Security']
                    this.chartOptions2.labels = ['Cash', 'Bank'];
                }
                else if(this.formName == 'AdvancePurchase')
                {
                    this.chartOptions2.labels=[];
                    this.chartOptions3.labels=[];
                    this.chartOptions3.labels=['Normal','Advance', 'Security']
                    this.chartOptions2.labels = ['Cash', 'Bank'];
                }
                else if(this.formName == 'BankPay')
                {
                    this.chartOptions2.labels=[];
                    this.chartOptions3.labels=[];
                    this.chartOptions3.labels=['Normal','Advance', 'Security']
                    this.chartOptions2.labels = ['Cash', 'Bank'];
                }
                else{
                    this.chartOptions2.labels = ['Cash', 'Bank'];

                    this.chartOptions3.labels = ['Normal','Advance', 'Security'];
                }

            },
        },
        updated: function () {
            if (this.selected.length < this.vouchersList.length) {
                this.selectAll = false;
            } else if (this.selected.length == this.vouchersList.length) {

                if (this.selected.length == 0) {
                    this.selectAll = false;
                } else {
                    this.selectAll = true
                }
            }
        },
        
        
        
        created: function () {

            this.GetOptions();
            if (this.$route.query.data == 'PaymentVoucherLists' + this.formName) {
                this.$emit('update:modelValue', 'PaymentVoucherLists' + this.formName);

            } else {
                this.$emit('update:modelValue', this.$route.name + this.formName);

                localStorage.removeItem("active");
                localStorage.removeItem("currentPage");

            }
            if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                this.currentPage = parseInt(localStorage.getItem('currentPage'));
                this.active = (localStorage.getItem('active'));
                this.GetPaymentVoucherData(this.formName, this.active, this.search, this.currentPage);

            } else {

                if ((this.isValid('CanDraftPettyCash') && this.formName == 'PettyCash') || (this.isValid('CanDraftCPR') && (this.formName == 'BankReceipt' || this.formName == 'AdvanceReceipt'  ))) {
                    this.active = 'Draft'
                    this.GetPaymentVoucherData(this.formName, 'Draft', this.search, 1);
                } else if ((this.isValid('CanViewPettyCash') && this.formName == 'PettyCash') || (this.isValid('CanViewCPR') && (this.formName == 'BankReceipt' || this.formName == 'AdvanceReceipt'  ))) {
                    this.active = 'Approved'
                    this.GetPaymentVoucherData(this.formName, 'Approved', this.search, 1);
                } else if ((this.isValid('CanRejectPettyCash') && this.formName == 'PettyCash') || (this.isValid('CanRejectCPR') && (this.formName == 'BankReceipt' || this.formName == 'AdvanceReceipt'  ))) {
                    this.active = 'Rejected'
                    this.GetPaymentVoucherData(this.formName, 'Rejected', this.search, 1);
                }
            }
            this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
            this.mainBranch = localStorage.getItem('MainBranch') == 'true' ? true : false;

        },
        mounted: function () {
           
            this.IsPaksitanClient = localStorage.getItem('IsPaksitanClient') == "true" ? true : false;
            this.currency = localStorage.getItem('currency');
        }
    }
</script>
