<template>
    <div class="row" v-if="(isValid('CanDraftServiceQuotation') || isValid('CanViewServiceQuotation')) || (isValid('CanViewServiceSaleOrder') || isValid('CanDraftServiceSaleOrder'))">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row" v-if="formName == 'ServiceQuotation'">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Quotation.ServiceQuotation') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Quotation.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('Quotation.ServiceQuotation') }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddServiceQuotation') || isValid('CanDraftServiceQuotation')" v-on:click="AddPurchaseOrder" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('SaleOrder.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('SaleOrder.Close') }}
                                </a>
                            </div>

                        </div>
                        <div class="row" v-if="formName == 'ServiceSaleOrder'">
                            <div class="col">
                                <h4 class="page-title">{{ $t('SaleOrder.ServiceSaleOrder') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('SaleOrder.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('SaleOrder.ServiceSaleOrder') }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanDraftServiceSaleOrder') || isValid('CanAddServiceSaleOrder')" v-on:click="AddPurchaseOrder" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('SaleOrder.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('SaleOrder.Close') }}
                                </a>
                            </div>
                        </div>
                        <div class="row" v-if="formName == 'Quotation'">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Quotation.Quotation') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Quotation.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('Quotation.Quotation') }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddQuotation') || isValid('CanDraftQuotation')" v-on:click="AddPurchaseOrder" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Quotation.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('Quotation.Close') }}
                                </a>
                            </div>
                        </div>
                        <div class="row" v-if="formName == 'SaleOrder'">
                            <div class="col">
                                <h4 class="page-title">{{ $t('SaleOrder.SaleOrder') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('SaleOrder.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('SaleOrder.SaleOrder') }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanDraftSaleOrder') || isValid('CanAddSaleOrder')" v-on:click="AddPurchaseOrder" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('SaleOrder.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('SaleOrder.Close') }}
                                </a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="accordion" id="accordionExample" v-bind:key="RanderAll">

                                <div class="accordion-item">
                                    <h5 class="accordion-header m-0" id="headingTwo">
                                        <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo" v-on:click="GetSaleDashboardRecord">
                                            KPIs Dashboard
                                        </button>
                                    </h5>
                                    <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                                        <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>

                                        <div class="accordion-body">

                                            <div class="row">
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <div class="row align-items-center">
                                                                <div class="col">
                                                                    <h4 class="card-title">{{ $t('Sale.InvoiceType') }}</h4>
                                                                </div>
                                                                <!--end col-->

                                                            </div>
                                                            <!--end row-->
                                                        </div>
                                                        <!--end card-header-->
                                                        <div class="card-body">
                                                            <div class="">
                                                                <apexchart type="donut" width="287" height="330" :options="chartOptions" :series="series"></apexchart>
                                                                <span class="badge badge-soft-primary">Draft</span> : {{parseFloat(saleListModel.draft ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                <span class="badge badge-soft-success">Approved</span> : {{ parseFloat(saleListModel.approved ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}<br>

                                                            </div>

                                                        </div>
                                                        <!--end card-body-->
                                                    </div>
                                                    <!--end card-->
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <div class="row align-items-center">
                                                                <div class="col">
                                                                    <h4 class="card-title">{{ $t('Sale.InvocieTypeByAmount') }}</h4>
                                                                </div>
                                                                <!--end col-->

                                                            </div>
                                                            <!--end row-->
                                                        </div>
                                                        <!--end card-header-->
                                                        <div class="card-body">
                                                            <div class="">
                                                                <apexchart type="donut" width="287" height="330" :options="chartOptions3" :series="series3"></apexchart>
                                                                <span class="badge badge-soft-primary">Draft:</span> : {{currency}} {{parseFloat(saleListModel.totalDraft ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                <span class="badge badge-soft-success">Approved</span> : {{currency}} {{parseFloat( saleListModel.totalApproved ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}<br>
                                                            </div>
                                                        </div>
                                                        <!--end card-body-->
                                                    </div>
                                                    <!--end card-->
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <div class="row align-items-center">
                                                                <div class="col">
                                                                    <h4 class="card-title">Trending Draft Customer By Amount</h4>
                                                                </div>
                                                                <!--end col-->

                                                            </div>
                                                            <!--end row-->
                                                        </div>
                                                        <!--end card-header-->
                                                        <div class="card-body">
                                                            <div class="">
                                                                <apexchart type="line" v-bind:key="randerChart" height="350" :options="chartOptionsOfCustomer" :series="seriesOfCustomer"></apexchart>
                                                            </div>
                                                        </div>
                                                        <!--end card-body-->
                                                    </div>
                                                    <!--end card-->
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <div class="row align-items-center">
                                                                <div class="col">
                                                                    <h4 class="card-title">Trending Approved Customer By Amount</h4>
                                                                </div>
                                                                <!--end col-->

                                                            </div>
                                                            <!--end row-->
                                                        </div>
                                                        <!--end card-header-->
                                                        <div class="card-body">
                                                            <div class="">
                                                                <apexchart type="bar" height="350" v-bind:key="randerChart" :options="earningChartOption" :series="earningSeries"></apexchart>
                                                            </div>
                                                        </div>
                                                        <!--end card-body-->
                                                    </div>
                                                    <!--end card-->
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <div class="row align-items-center">
                                                                <div class="col">
                                                                    <h4 class="card-title"> Trending Draft Customer</h4>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="">
                                                                <apexchart type="area" height="350" v-bind:key="randerChart" :options="chartOptionsPurchase" :series="seriesPurchase"></apexchart>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <div class="card">
                                                        <div class="card-header">
                                                            <div class="row align-items-center">
                                                                <div class="col">
                                                                    <h4 class="card-title">Trending Approved Customer </h4>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="">
                                                                <apexchart type="bar" height="350" v-bind:key="randerChart" :options="earningChartOptionCash" :series="earningSeriesCash"></apexchart>
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
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-8" style="padding-top:20px">
                            <div class="input-group" v-if="formName == 'ServiceQuotation'">
                                <button class="btn btn-soft-primary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                                <input v-model="search" type="text" class="form-control" :placeholder="$t('Quotation.SearchByQuotation')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                <a class="btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2" value="Advance Filter">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                            <div class="input-group" v-if="formName == 'ServiceSaleOrder'">
                                <button class="btn btn-soft-primary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                                <input v-model="search" type="text" class="form-control" :placeholder="$t('SaleOrder.SearchBySO')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                <a class="btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2" value="Advance Filter">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                            <div class="input-group" v-if="formName == 'Quotation'">
                                <button class="btn btn-soft-primary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                                <input v-model="search" type="text" class="form-control" :placeholder="$t('Quotation.SearchByQuotation')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                <a class="btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2" value="Advance Filter">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                            <div class="input-group" v-if="formName == 'SaleOrder'">
                                <button class="btn btn-soft-primary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                                <input v-model="search" v-if="formName == 'SaleOrderTracking'" type="text" class="form-control" :placeholder="$t('SaleOrder.SearchBySO')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                <input v-model="search" v-else type="text" class="form-control" :placeholder="$t('SaleOrder.SearchBySO')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                                <a class="btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2" value="Advance Filter">
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

                    <div class="row " v-if="advanceFilters">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4">
                            <label class="text  font-weight-bolder">{{ $t('Sale.Customer') }}</label>
                            <customerdropdown v-model="customerId" :key="randerforempty" />
                        </div>
                        <div class="col-xs-12  col-lg-2">
                            <div class="form-group">
                                <label>{{ $t('Sale.Month') }}</label>
                                <monthpicker v-model="monthObj" @update:modelValue="GetMonth" v-bind:disabled="isDisableMonth" v-if="!isDisableMonth" v-bind:key="randerforempty" />
                                <input class="form-control" v-else disabled />
                            </div>
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

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">
                            <label class="text  font-weight-bolder"> {{$t('Sale.User1')}}:</label>
                            <usersDropdown v-model="user" ref="userDropdown" :isloginhistory="isloginhistory" />
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
                    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel" style="width:600px !important">
                        <div class="offcanvas-header">
                            <h5 id="offcanvasRightLabel" class="m-0">Delivery Note</h5>
                            <button v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                        </div>
                        <div class="offcanvas-body">
                            <div class="row">
                                <div v-if="isCloseChallan">
                                    <h6 style="color:red">Challan Is Closed</h6>
                                </div>
                                <div class="row" v-else>

                                    <div class="col-lg-4" v-if="isAddChallan">
                                        <a data-bs-dismiss="offcanvas" aria-label="Close" v-on:click="ReservedDeliveryChallan(saleOrderId,true)" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                            <i class="align-self-center icon-xs ti-plus"></i>
                                            Select Items
                                        </a>
                                    </div>
                                    <div class="col-lg-4" v-else>
                                        <a v-on:click="ReservedDeliveryChallan(isReservedId,false)" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                            <i class="align-self-center icon-xs ti-plus"></i>
                                            Edit Items
                                        </a>
                                        <a v-on:click="ReservedDeliveryChallan(isReservedId,false,true)" href="javascript:void(0);" class="btn btn-sm btn-outline-danger mx-1">
                                            <i class="align-self-center icon-xs ti-plus"></i>
                                            Close
                                        </a>
                                    </div>

                                    <div class="col-lg-4" v-if="!isAddChallan && !isCloseChallan">
                                        <a v-on:click="DeliveryChllan(saleOrderId)" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1" data-bs-dismiss="offcanvas" aria-label="Close">
                                            <i class="align-self-center icon-xs ti-plus"></i>
                                            Add Dilvery Challan
                                        </a>
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>#</th>
                                                <th>
                                                    Delivery Order No
                                                </th>
                                                <th>
                                                    Date
                                                </th>

                                                <th>
                                                    Sale Order No
                                                </th>

                                                <th style="width: 70px;" class="text-end">
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(purchaseOrder,index) in deliveryChallanList" v-bind:key="purchaseOrder.registrationNumber">
                                                <td>
                                                    {{index+1}}
                                                </td>

                                                <td>
                                                    <strong>
                                                        <a href="javascript:void(0)" data-bs-dismiss="offcanvas" aria-label="Close" v-if="!isCloseChallan" v-on:click="EditDeliveryChallan(purchaseOrder.id)">{{purchaseOrder.registrationNumber}}</a>
                                                        <a href="javascript:void(0)" data-bs-dismiss="offcanvas" aria-label="Close" v-else>{{purchaseOrder.registrationNumber}}</a>
                                                    </strong>

                                                </td>
                                                <td>
                                                    {{purchaseOrder.date}}
                                                </td>
                                                <td>
                                                    {{purchaseOrder.documentNumberForOrder}}
                                                </td>

                                                <td class="text-end">

                                                    <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('SaleOrder.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                    <div class="dropdown-menu">
                                                        <a class="dropdown-item" href="javascript:void(0)" data-bs-dismiss="offcanvas" aria-label="Close" v-on:click="EditDeliveryChallan(purchaseOrder.id)" v-if="!isCloseChallan">{{ $t('SaleOrder.EditInvoice') }}</a>
                                                        <a class="dropdown-item" href="javascript:void(0)" data-bs-dismiss="offcanvas" aria-label="Close" v-on:click="PrintRdlc(purchaseOrder.id,false)">{{ $t('SaleOrder.ViewInvoice') }}</a>
                                                        <a class="dropdown-item" href="javascript:void(0)" data-bs-dismiss="offcanvas" aria-label="Close" v-on:click="PrintRdlc(purchaseOrder.id,false)">{{ $t('SaleOrder.A4Print') }}</a>
                                                        <a class="dropdown-item" href="javascript:void(0)" data-bs-dismiss="offcanvas" aria-label="Close" v-on:click="PrintRdlc(purchaseOrder.id,true)">{{ $t('PDFDownload') }}</a>
                                                    </div>

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                            </div>
                        </div>
                    </div>

                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item" v-if="isValid('CanDraftServiceSaleOrder') || isValid('CanDraftServiceQuotation') || isValid('CanDraftQuotation') || isValid('CanDraftSaleOrder')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Draft' }" v-on:click="makeActive('Draft')" data-bs-toggle="tab" href="#home" role="tab" aria-selected="true">
                                {{ $t('SaleOrder.Draft') }}
                            </a>
                        </li>
                        <li class="nav-item" v-if="isValid('CanDraftServiceSaleOrder') || isValid('CanViewServiceQuotation') || isValid('CanViewQuotation') || isValid('CanViewSaleOrder')" v-on:click="makeActive('Approved')">
                            <a class="nav-link" v-bind:class="{ active: active == 'Approved' }" data-bs-toggle="tab" href="#profile" role="tab" aria-selected="false">
                                {{ $t('SaleOrder.Post') }}
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
                                            <th>#</th>
                                            <th v-if="formName == 'ServiceQuotation'">
                                                QT Number
                                            </th>
                                            <th v-else-if="formName == 'Quotation'">
                                                QT Number
                                            </th>
                                            <th v-else-if="formName == 'ServiceSaleOrder'">
                                                {{ $t('SaleOrder.SONumber') }}
                                            </th>
                                            <th v-else-if="formName == 'SaleOrder'">
                                                {{ $t('SaleOrder.SONumber') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.Version') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.CreatedDate') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.CustomerName') }}
                                            </th>
                                            <th>
                                                {{ $t('Sale.CreatedBy') }}
                                            </th>
                                            <th>
                                                {{ $t('InvoiceNote')}}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.NetAmount') }}
                                            </th>
                                            <th style="width: 70px;" class="text-end">
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(purchaseOrder,index) in saleOrderList" v-bind:key="purchaseOrder.id">
                                            <td v-if="currentPage === 1">
                                                {{index+1}}
                                            </td>
                                            <td v-else>
                                                {{((currentPage*10)-10) +(index+1)}}
                                            </td>
                                            <td v-if="isValid('CanEditServiceSaleOrder')">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchaseOrder.id,false)">{{purchaseOrder.registrationNumber}}</a>
                                                </strong>

                                            </td>
                                            <td v-else>
                                                {{purchaseOrder.registrationNumber}}
                                            </td>

                                            <td>
                                                {{purchaseOrder.version}}
                                            </td>
                                            <td>
                                                {{purchaseOrder.date}}
                                            </td>
                                            <td>
                                                <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(purchaseOrder.customerId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2" aria-controls="offcanvasRight">{{purchaseOrder.customerName}}</a>
                                            </td>
                                            <td>
                                                {{ purchaseOrder.createdBy }}
                                            </td>
                                            <td v-if="purchaseOrder.invoiceNote != null">
                                                <a href="javascript:void(0)" v-on:click="SaleIdForCanvas(purchaseOrder.deliveryChallanId,purchaseOrder.saleOrderId, purchaseOrder.quotationId,purchaseOrder.proformaId, purchaseOrder.registrationNumber, purchaseOrder.date,purchaseOrder.id)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight3" aria-controls="offcanvasRight">{{purchaseOrder.invoiceNote}}</a>
                                            </td>
                                            <td v-else>
                                                ---
                                            </td>
                                            <td>
                                                {{currency}} {{parseFloat(purchaseOrder.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('SaleOrder.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchaseOrder.id,false,true)">Clone</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchaseOrder.id,false)" v-if="(isValid('CanEditServiceSaleOrder') || isValid('CanEditServiceQuotation') || isValid('CanEditQuotation') || isValid('CanEditSaleOrder')) && (formName == 'ServiceSaleOrder' || formName == 'SaleOrder')">{{ $t('SaleOrder.EditInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchaseOrder.id,false)" v-else>{{ $t('SaleOrder.EditQuotation') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(purchaseOrder.id,false)" v-if="(isValid('CanViewDetailServiceSaleOrder') || isValid('CanViewServiceQuotationDetail') || isValid('CanViewQuotationDetail') || isValid('CanViewDetailSaleOrder')) && (formName == 'ServiceSaleOrder' || formName == 'SaleOrder') ">{{ $t('SaleOrder.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(purchaseOrder.id,false)" v-else>{{ $t('SaleOrder.ViewQuotation') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(purchaseOrder.id,false)" v-if="isValid('CanPrintServiceSaleOrder') || isValid('CanPrintServiceQuotation') || isValid('CanPrintQuotation') || isValid('CanPrintSaleOrder')">{{ $t('SaleOrder.A4Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(purchaseOrder.id,true)" v-if="isValid('CanPrintServiceSaleOrder') || isValid('CanPrintServiceQuotation') || isValid('CanPrintQuotation') || isValid('CanPrintSaleOrder')">{{ $t('SaleOrder.PdfDownload') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="sendEmail(purchaseOrder.id)" v-if="(isValid('CanSendSaleEmailAsLink') || isValid('CanSendSaleEmailAsPdf')) || (isValid('CanSendSaleEmailAsLink') || isValid('CanSendSaleEmailAsPdf')) || (isValid('CanSendSaleEmailAsLink') || isValid('CanSendSaleEmailAsPdf')) || (isValid('CanSendSaleEmailAsLink') || isValid('CanSendSaleEmailAsPdf'))">{{ $t('SaleOrder.Email') }}</a>

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
                                            <th>#</th>
                                            <th v-if="formName == 'ServiceQuotation'">
                                                QT Number
                                            </th>
                                            <th v-else-if="formName == 'Quotation'">
                                                QT Number
                                            </th>
                                            <th v-else-if="formName == 'ServiceSaleOrder'">
                                                {{ $t('SaleOrder.SONumber') }}
                                            </th>
                                            <th v-else-if="formName == 'SaleOrder'">
                                                {{ $t('SaleOrder.SONumber') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.CreatedDate') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.CustomerName') }}
                                            </th>
                                            <th>
                                                {{ $t('Sale.CreatedBy') }}
                                            </th>
                                            <th>
                                                {{ $t('InvoiceNote')}}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.NetAmount') }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t('SaleOrder.Status') }}
                                            </th>
                                            <th style="width: 70px;" class="text-end">

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(purchaseOrder,index) in saleOrderList" v-bind:key="purchaseOrder.id">
                                            <td v-if="currentPage === 1">
                                                {{index+1}}
                                            </td>
                                            <td v-else>
                                                {{((currentPage*10)-10) +(index+1)}}
                                            </td>
                                            <td v-if="(formName == 'ServiceQuotation' || formName == 'Quotation') && (purchaseOrder.editQuotationId == null && !purchaseOrder.isClose)">
                                                <div>
                                                    <strong>
                                                        <a href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchaseOrder.id,false)">{{purchaseOrder.registrationNumber}}</a>
                                                        <br />
                                                    </strong>
                                                    <div v-if="isValid('PremiumARAdvancePayment')">
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
                                                </div>
                                            </td>
                                            <td v-else-if="(formName == 'ServiceSaleOrder' || formName == 'SaleOrder') && (purchaseOrder.editSaleOrderId == null && !purchaseOrder.isClose)">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchaseOrder.id,false)">{{purchaseOrder.registrationNumber}}</a>
                                                </strong>
                                                <br />
                                                <div v-if="isValid('PremiumARAdvancePayment')">
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
                                            <td v-else>
                                                {{purchaseOrder.registrationNumber}}
                                            </td>
                                            <td>
                                                {{purchaseOrder.date}}
                                            </td>
                                            <td>
                                                <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(purchaseOrder.customerId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2" aria-controls="offcanvasRight">{{purchaseOrder.customerName}}</a>
                                            </td>
                                            <td>
                                                {{ purchaseOrder.createdBy }}
                                            </td>
                                            <td v-if="purchaseOrder.invoiceNote != null">
                                                <a href="javascript:void(0)" v-on:click="SaleIdForCanvas(purchaseOrder.deliveryChallanId,purchaseOrder.saleOrderId, purchaseOrder.quotationId,purchaseOrder.proformaId, purchaseOrder.registrationNumber, purchaseOrder.date,purchaseOrder.id)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight3" aria-controls="offcanvasRight">{{purchaseOrder.invoiceNote}}</a>
                                            </td>
                                            <td v-else>
                                                ---
                                            </td>

                                            <td>
                                                {{currency}} {{parseFloat(purchaseOrder.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>

                                            <td class="text-center" v-bind:key="randerToogle">
                                                <toggle-button v-on:change="openmodel(purchaseOrder.id)" class="ml-2 mt-2" color="#3178F6" v-bind:key="randerToogle" v-if="!purchaseOrder.isClose && isValid('CanCloseServiceSaleOrder')" />

                                                <div class="tooltip badge rounded-pill badge-soft-success" v-if="purchaseOrder.isProcessed">
                                                    Processed
                                                    <span class="tooltiptext">{{ purchaseOrder.reason }}</span>
                                                </div>

                                                <div class="tooltip badge rounded-pill badge-soft-danger" v-else-if="purchaseOrder.isClose">
                                                    Closed
                                                    <span class="tooltiptext">{{ purchaseOrder.reason }}</span>
                                                </div>
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('SaleOrder.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchaseOrder.id,false,true)">Clone</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="paymentModel(purchaseOrder.id, purchaseOrder.netAmount, purchaseOrder.customerAdvanceAccountId)" v-if="!purchaseOrder.isClose && isValid('CanServicePayAdvanceFromView')">{{ $t('SaleOrder.Payment') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(purchaseOrder.id,false)" v-if="(isValid('CanViewDetailServiceSaleOrder') || isValid('CanViewServiceQuotationDetail')) && (formName == 'ServiceSaleOrder' || formName == 'SaleOrder') ">{{ $t('SaleOrder.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(purchaseOrder.id,false)" v-else>{{ $t('SaleOrder.ViewQuotation') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(purchaseOrder.id,false)" v-if="isValid('CanPrintServiceSaleOrder') || isValid('CanPrintServiceQuotation')">{{ $t('SaleOrder.A4Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(purchaseOrder.id,true)" v-if="isValid('CanPrintServiceSaleOrder') || isValid('CanPrintServiceQuotation')">{{ $t('SaleOrder.PdfDownload') }}</a>
                                                    <!-- <a class="dropdown-item" href="javascript:void(0)" v-on:click="sendEmail(purchaseOrder.id,false)" v-if="isValid('CanSendSaleEmailAsLink') || isValid('CanSendSaleEmailAsPdf') ">{{ $t('SaleOrder.Email') }}</a> -->
                                                    <a class="dropdown-item" href="javascript:void(0)" v-if="!purchaseOrder.isProcessed && !purchaseOrder.isClose " v-on:click="ConvertToInvoice(purchaseOrder.id)">{{ $t('SaleOrder.ConverttoInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="ConvertToSupplier(purchaseOrder.id)" v-if="isValid('CanViewSupplierQuotation') && formName == 'ServiceQuotation'">Convert To Supplier Quotation</a>
                                                    <a class="dropdown-item" href="javascript:void(0);" v-on:click="ViewDeliveryChallan(purchaseOrder.id)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight"> {{ $t('SaleOrder.IssueDeliveryNote') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0);" v-on:click="ReceiptGenerationandViewing(purchaseOrder.id)" v-if="purchaseOrder.partiallyInvoices != 3 && ( isValid('StandardARAdvancePayment') || isValid('PremiumARAdvancePayment'))">
                                                        Create Payment Receipt
                                                    </a>
                                                    <a class="dropdown-item" href="javascript:void(0);" v-on:click="sendEmail(purchaseOrder.id, purchaseOrder.customerEmail)" v-if="purchaseOrder.isDefault">
                                                       Email
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
                                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

            <reasonsaleorder :show="show1" v-if="show1" @close="CloseRefresh" @SaveRecord="SaveRecord" />


            <purchaseorder-payment :totalAmount="totalAmount" :customerAccountId="customerAccountId" :show="payment" v-if="payment" @close="paymentSave" :isSaleOrder="'true'" :isPurchase="'false'" :purchaseOrderId="purchaseId" :formName="'AdvanceReceipt'" />

            <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>
            <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport" @close="show=false" @IsSave="IsSave" />

            <DeliveryChallanModel :newshow="ReservedDeliveryChallanbool" v-if="ReservedDeliveryChallanbool" :newpurchase="deliveryChallanRecord" :isService="isService" :isReservedChallan="ReservedDeliveryChallanbool" :type="isAdd" :isSaleOrder="true" :deliveryUndefined="deliveryUndefined" @close="GetRecordOfDelivery"></DeliveryChallanModel>
            <SendEmail :show="emailComposeShow" v-if="emailComposeShow" @close="emailComposeShow = false" :id="saleId" :from="formName" :customerEmail="customerEmail"></SendEmail>
        </div>
        <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight3" aria-labelledby="offcanvasRightLabel" style="width:600px !important">
            <div class="offcanvas-header">
                <h5 id="offcanvasRightLabel" class="m-0">{{ $t('Sale.MoreDetails') }} ({{ registrationNo }})</h5>
                <button v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
            </div>
            <div class="offcanvas-body">
                <!-- <div class="row">
                                <a class="btn btn-light my-2 col-md-4" href="javascript:void(0);" v-on:click="ViewDeliveryChallan(saleIdToCanvas)" data-bs-toggle="offcanvas" v-if="isValid('SaleToDeliveryNote') " data-bs-target="#offcanvasRight" aria-controls="offcanvasRight"> {{ $t('Sale.ViewDeliveryNote') }}</a>
                            </div> -->
                <div class="row">
                    <div class="col-md-12" v-if="isValid('DeliveryNoteToSale') && deliveryNo != null && deliveryNo != ''">
                        <div class="row  ">
                            <div class="form-group text-right ">
                                <b> {{ $t('PrintSetting.DeliveryNote') }} </b>
                            </div>
                            <div v-if="deliveryNo != null && deliveryNo != ''" class="col-lg-12 form-group text-left d-flex justify-items-between">
                                <!-- v-if="expandDeliveryChallan" -->
                                <p style="border-bottom: 1px solid #cbcbcb; ">
                                    <span>1- {{ deliveryNo }}--{{getDate(canvasDate) }}</span>

                                </p>
                            </div>

                            <div class="col-lg-12 form-group">

                                <div class="table-responsive mt-3">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>#</th>
                                                <th>{{ $t('AddBundles.ProductName') }}</th>
                                                <th>{{ $t('StockLineDetails.Quantity') }}</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(sale,index ) in saleItem" v-bind:key="index">
                                                <td>{{ index + 1 }}</td>
                                                <td v-if="sale.productName=='' || sale.productName==null">
                                                    {{ sale.description }}
                                                </td>
                                                <td v-else>
                                                    {{ sale.displayName }}
                                                </td>
                                                <td>{{ sale.quantity }}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="col-md-12">

                        <div class="row" v-if="(isValid('CanDraftQuotation') || isValid('CanViewQuotation')) && saleOrderNo != null && saleOrderNo != ''">
                            <div class="form-group text-right " v-if="saleOrderNo != null && saleOrderNo != ''">
                                <b> {{ $t('AddDispatchNote.SaleOrder') }}</b>
                                <!-- <span>{{invoiceNote}}</span> -->
                            </div>
                            <div class="col-lg-12 form-group text-left d-flex">

                                <p style="border-bottom: 1px solid #cbcbcb; ">
                                    <span>1- {{ saleOrderNo }}--{{getDate(canvasDate) }}</span>
                                </p>
                            </div>

                            <div class="col-lg-12 form-group">
                                <div class="table-responsive">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>{{ $t('AddPurchase.DiscountType') }}</th>
                                                <th>{{ $t('AddPurchase.TaxMethod') }}</th>
                                                <th>{{ $t('StockLineDetails.VAT') }}</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td v-if="discountType">
                                                    At Transaction Level
                                                </td>
                                                <td v-else>At Line Item Level</td>
                                                <td>{{ canvasTaxMethod }}</td>
                                                <td>{{ vAT }}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="table-responsive mt-3">
                                    <table class="table mb-0">

                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>#</th>
                                                <th>{{ $t('AddBundles.ProductName') }}</th>
                                                <th>{{ $t('StockLineDetails.Quantity') }}</th>
                                                <th>{{ $t('StockLineDetails.UnitPrice') }}</th>
                                            </tr>
                                        </thead>
                                        <tbody v-for="(sale,index ) in saleItem" v-bind:key="index">
                                            <tr>
                                                <td>{{ index + 1 }}</td>
                                                <td v-if="sale.productName=='' || sale.productName==null">
                                                    {{ sale.description }}
                                                </td>
                                                <td v-else>
                                                    {{ sale.displayName }}
                                                </td>
                                                <td>{{ sale.quantity }}</td>
                                                <td>{{ sale.unitPrice }}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="table-responsive mt-3">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>{{ $t('SaleItem.Subtotal') }}</th>
                                                <th>{{ $t('SaleItem.Discount') }}</th>
                                                <th>{{ $t('Vat Amount') }}</th>
                                                <th>{{ $t('Amount After Discount') }}</th>
                                                <th>{{ $t('SaleItem.TotalDue') }}</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>{{currency}} {{parseFloat(grossAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                <td>
                                                    {{parseFloat(discountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                </td>
                                                <td>
                                                    {{parseFloat(vatAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                </td>
                                                <td>{{parseFloat(afterDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                <td> {{currency}} {{parseFloat(totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="row" v-if="(isValid('CanDraftQuotation') || isValid('CanViewQuotation')) && quotationNo != null && quotationNo != ''">
                            <div class="form-group text-right ">
                                <b> {{ $t('AddQuotation.Quotation') }}</b>
                                <!-- <span>{{invoiceNote}}</span> -->
                            </div>
                            <div class="col-lg-12 form-group text-left d-flex">
                                <p style="border-bottom: 1px solid #cbcbcb; ">
                                    <span>1- {{ quotationNo }}--{{getDate(canvasDate) }}</span>
                                </p>
                            </div>

                            <div class="col-lg-12 form-group">
                                <div class="table-responsive">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>{{ $t('AddPurchase.DiscountType') }}</th>
                                                <th>{{ $t('AddPurchase.TaxMethod') }}</th>
                                                <th>{{ $t('StockLineDetails.VAT') }}</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td v-if="discountType">
                                                    At Transaction Level
                                                </td>
                                                <td v-else>At Line Item Level</td>
                                                <td>{{ canvasTaxMethod }}</td>
                                                <td>{{ vAT }}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="table-responsive mt-3">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>#</th>
                                                <th>{{ $t('AddBundles.ProductName') }}</th>
                                                <th>{{ $t('StockLineDetails.Quantity') }}</th>
                                                <th>{{ $t('StockLineDetails.UnitPrice') }}</th>
                                            </tr>
                                        </thead>
                                        <tbody v-for="(sale,index ) in saleItem" v-bind:key="index">
                                            <tr>
                                                <td>{{ index + 1 }}</td>
                                                <td v-if="sale.productName=='' || sale.productName==null">
                                                    {{ sale.description }}
                                                </td>
                                                <td v-else>
                                                    {{ sale.displayName }}
                                                </td>
                                                <td>{{ sale.quantity }}</td>
                                                <td>{{ sale.unitPrice }}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="table-responsive mt-3">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>{{ $t('SaleItem.Subtotal') }}</th>
                                                <th>{{ $t('SaleItem.Discount') }}</th>
                                                <th>{{ $t('Vat Amount') }}</th>
                                                <th>{{ $t('Amount After Discount') }}</th>
                                                <th>{{ $t('SaleItem.TotalDue') }}</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>{{currency}} {{parseFloat(grossAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                <td>
                                                    {{parseFloat(discountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                </td>
                                                <td>
                                                    {{parseFloat(vatAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

                                                </td>
                                                <td>{{parseFloat(afterDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                <td> {{currency}} {{parseFloat(totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="row" v-if="(isValid('CanViewProforma')) && proformaNo != null && proformaNo != ''">
                            <div class="form-group text-right ">
                                <b> {{ $t('ProformaInvoices.ProformaInvoices') }}</b>
                                <!-- <span>{{invoiceNote}}</span> -->
                            </div>
                            <div class="col-lg-12 form-group text-left d-flex">
                                <p style="border-bottom: 1px solid #cbcbcb; ">
                                    <span>1- {{ proformaNo }}--{{getDate(canvasDate) }}</span>
                                </p>
                            </div>

                            <div class="col-lg-12 form-group">
                                <div class="table-responsive">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>{{ $t('AddPurchase.DiscountType') }}</th>
                                                <th>{{ $t('AddPurchase.TaxMethod') }}</th>
                                                <th>{{ $t('StockLineDetails.VAT') }}</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td v-if="discountType">
                                                    At Transaction Level
                                                </td>
                                                <td v-else>At Line Item Level</td>
                                                <td>{{ canvasTaxMethod }}</td>
                                                <td>{{ vAT }}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="table-responsive mt-3">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>#</th>
                                                <th>{{ $t('AddBundles.ProductName') }}</th>
                                                <th>{{ $t('StockLineDetails.Quantity') }}</th>
                                                <th>{{ $t('StockLineDetails.UnitPrice') }}</th>
                                            </tr>
                                        </thead>
                                        <tbody v-for="(sale,index ) in saleItem" v-bind:key="index">
                                            <tr>
                                                <td>{{ index + 1 }}</td>
                                                <td v-if="sale.productName=='' || sale.productName==null">
                                                    {{ sale.description }}
                                                </td>
                                                <td v-else>
                                                    {{ sale.displayName }}
                                                </td>
                                                <td>{{ sale.quantity }}</td>
                                                <td>{{ sale.unitPrice }}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="table-responsive mt-3">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>{{ $t('SaleItem.Subtotal') }}</th>
                                                <th>{{ $t('SaleItem.Discount') }}</th>
                                                <th>{{ $t('Vat Amount') }}</th>
                                                <th>{{ $t('Amount After Discount') }}</th>
                                                <th>{{ $t('SaleItem.TotalDue') }}</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>{{currency}} {{parseFloat(grossAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                <td>
                                                    {{parseFloat(discountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                </td>
                                                <td>
                                                    {{parseFloat(vatAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                </td>
                                                <td>{{parseFloat(afterDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                <td> {{currency}} {{parseFloat(totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12 ">
                        <div class="row text-center justify-content-center">
                            <button v-if="expandHistory" v-bind:key="randerExpand" v-on:click="DocumentHistory(false)" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-up"></i></button>
                            <button v-else v-on:click="DocumentHistory(true)" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-down"></i></button>

                        </div>

                        <div v-if="expandHistory" class="col-lg-12 form-group">
                            <div class="card">
                                <div class="card-header">
                                    <h4 class="card-title">Document History</h4>
                                </div>
                                <!--end card-header-->
                                <div class="card-body">
                                    <div class="main-timeline mt-3">
                                        <div class="timeline" v-for=" list in historyList" v-bind:key="list.documentName">
                                            <span class="timeline-icon"></span>
                                            <span class="year">{{list.documentName}}</span>
                                            <div class="timeline-content">
                                                <h5 class="title">{{list.registrationNo}}</h5>
                                                <span class="post">{{list.date}}</span>
                                                <!-- <p class="description">
                                                                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus mattis justo id pulvinar suscipit.
                                                            </p> -->
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <!--end card-body-->
                            </div>
                        </div>

                        <!--end col-->

                    </div>
                </div>
            </div>
        </div>
        <div class="offcanvas offcanvas-end px-0" tabindex="-1" id="offcanvasRight2" aria-labelledby="offcanvasRightLabel">
            <div class="offcanvas-header">
                <h5 id="offcanvasRightLabel" class="m-0">Customer Info ({{customerInformation.runningBalance}})</h5>
                <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
            </div>
            <div class="offcanvas-body">
                <div class="row">
                    <div class="col-lg-12 form-group" v-if="english=='true'">
                        <label>{{$englishLanguage($t('Sale.CustomerName'))  }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.englishName" disabled />
                    </div>
                    <div class="col-lg-12 form-group" v-if="isOtherLang()">
                        <label>{{ $t('Sale.CustomerNameArabic') }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.arabicName" disabled />
                    </div>
                    <div class="col-lg-12 form-group">
                        <label>{{ $t('AddCustomer.CustomerPhone') }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.contactNo1" disabled />
                    </div>
                    <div class="col-lg-12 form-group">
                        <label>{{ $t('AddCustomer.CommercialRegistrationNo') }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.commercialRegistrationNo" disabled />
                    </div>
                    <div class="col-lg-12 form-group">
                        <label>{{ $t('AddCustomer.VAT/NTN/Tax No') }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.vatNo" disabled />
                    </div>
                    <div class="col-lg-12 form-group text-center">
                        <button v-if="expandSale" v-on:click="expandSale=false" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-up"></i></button>
                        <button v-else v-on:click="expandSale=true" type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm"><i class="ti-angle-double-down"></i></button>
                    </div>
                    <div v-if="expandSale" class="col-lg-12 form-group">
                        <h6 class="text-danger" v-if="formName == 'ServiceQuotation'">Showing Last Three Month Quotations</h6>
                        <h6 class="text-danger" v-if="formName == 'ServiceSaleOrder'">Showing Last Three Month Sale Order </h6>

                        <p v-for="(sale,index) in customerInformation.invoiceList" v-bind:key="index" style="border-bottom: 1px solid #cbcbcb; ">
                            <a href="javascript:void(0);" data-bs-dismiss="offcanvas" aria-label="Close" v-on:click="ViewInvoice(sale.id)">
                                <span>{{index+1}}- {{sale.registrationNumber}} </span>
                                <span class="float-end">{{currency}} {{parseFloat(sale.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span>
                            </a>
                            <br />
                            <small>{{getDate(sale.date)}}</small>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin';
    import moment from "moment";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    export default {
        mixins: [clickMixin],
        props: ['formName'],
        components: {
            Loading
        },

        data: function () {
            return {
                customerEmail: '',

                expandHistory: false,
                randerExpand: 0,
                historyList: [],
                series: [],
                chartOptions: {
                    labels: ['Draft', 'Approved']
                },

                series3: [],
                chartOptions3: {
                    labels: ['Draft', 'Approved']
                },

                seriesOfCustomer: [{
                    name: "Amount",
                    data: []
                }],
                chartOptionsOfCustomer: {
                    chart: {
                        height: 350,
                        type: 'line',
                        zoom: {
                            enabled: false
                        }
                    },
                    dataLabels: {
                        enabled: false
                    },
                    stroke: {
                        curve: 'straight'
                    },
                    title: {
                        text: 'Trending Customer by Amount',
                        align: 'left'
                    },
                    grid: {
                        row: {
                            colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
                            opacity: 0.5
                        },
                    },
                    xaxis: {
                        categories: [],
                    }
                },

                saleListModel: {
                    draft: 0,
                    totalDraft: 0.0,
                    approved: 0,
                    totalApproved: 0.0,

                },
                earningSeries: [{
                    name: 'Cash Customer',
                    data: []
                }],
                earningChartOption: {
                    chart: {
                        height: 350,
                        type: 'area'
                    },
                    plotOptions: {
                        bar: {
                            horizontal: false,
                            columnWidth: '15%',
                            endingShape: 'rounded'
                        },
                    },
                    dataLabels: {
                        enabled: false
                    },
                    stroke: {
                        curve: 'smooth'
                    },
                    xaxis: {

                        categories: []
                    },

                },
                earningSeriesCash: [{
                    name: 'Cash Customer',
                    data: []
                }],
                earningChartOptionCash: {
                    chart: {
                        height: 350,
                        type: 'area'
                    },
                    plotOptions: {
                        bar: {
                            horizontal: false,
                            columnWidth: '15%',
                            endingShape: 'rounded'
                        },
                    },
                    dataLabels: {
                        enabled: false
                    },
                    stroke: {
                        curve: 'smooth'
                    },
                    xaxis: {

                        categories: []
                    },

                },

                seriesPurchase: [{
                    name: 'Credit Customer',
                    data: []
                },],
                chartOptionsPurchase: {
                    chart: {
                        height: 350,
                        type: 'area'
                    },
                    dataLabels: {
                        enabled: false
                    },
                    stroke: {
                        curve: 'smooth'
                    },
                    xaxis: {
                        categories: []
                    },

                },

                show1: false,
                request: 0,
                purchaseOrderId: '',
                reason: '',
                loading: false,
                advanceFilters: false,
                customerId: '',
                isDisableMonth: false,
                monthObj: '',
                randerforempty: 0,
                randerChart: 0,
                fromDate: '',
                toDate: '',
                isDisable: false,
                user: '',
                isloginhistory: true,
                year: '',
                month: '',
                userId: '',
                documentName: '',
                isService: false,

                expandDeliveryChallan: false,
                expandDeliveryChallan1: false,
                expandDeliveryChallan2: false,
                registrationNo: '',
                saleIdToCanvas: '',
                quotationNo: '',
                saleOrderNo: '',
                deliveryNo: '',
                canvasDate: '',
                canvasSaleOrderId: '',
                qutationId: '',
                deliveryChallanId: '',
                vAT: '',
                canvasTaxMethod: '',
                discountType: false,
                invoiceNote: '',
                saleItem: [],
                proformaNo: '',
                proformaId: '',

                reportname: false,
                deliveryUndefined: false,
                reportsrc: '',
                isQuotation: false,
                orderId: '',
                emailComposeShow: false,
                isDevliveryChallan: false,
                show: false,
                pdfShow: false,
                printpdfRender: 0,
                saleOrderId: '',
                isReservedId: '',
                ReservedDeliveryChallanbool: false,
                isAddChallan: false,
                isCloseChallan: false,
                isAdd: false,
                active: 'Draft',
                isPurchase: false,
                colorVariants: false,
                payment: false,
                totalAmount: 0,
                customerAccountId: '',
                purchaseId: '',
                search: '',
                searchQuery: '',
                saleOrderList: [],
                deliveryChallanList: [],
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                currency: '',
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                printTemplate: '',
                selected: [],
                selectAll: false,
                updateApprovalStatus: {
                    id: '',
                    approvalStatus: ''
                },
                printDetails: [],
                printRender: 0,
                randerToogle: 0,
                randerList: 0,
                RanderAll: 0,
                isFifo: false,
                openBatch: 0,
                english: '',
                arabic: '',

                customerInformation: '',
                expandSale: false,
            }
        },
        computed: {
            isFilterButtonDisabled() {
      return (
        this.customerId ||
        this.monthObj ||
        this.fromDate ||
        this.toDate ||
        this.user
      );
    },
  },
        watch: {
            // search: function (val) {
            //     this.getData(val, 1, this.active, this.fromDate, this.toDate, this.userId, this.customerId);
            // },
            formName: function () {
                if (this.formName == 'ServiceQuotation') {
                    this.makeActive('Draft');
                } else if (this.formName == 'ServiceSaleOrder') {
                    this.makeActive('Draft');
                } else if (this.formName == 'Quotation') {
                    this.makeActive('Draft');
                } else if (this.formName == 'SaleOrder') {
                    this.makeActive('Draft');
                }
                this.request = 0;
                this.RanderAll++;
            }
        },
        methods: {
            ReceiptGenerationandViewing: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/PaymentVoucher/GetSaleToVoucher?Id=' + id + '&formName=' + this.formName, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addPaymentVoucher',
                                query: {
                                    data: '',
                                    Add: false,
                                    formName: 'AdvanceReceipt'
                                }
                            })
                        } else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                icon: 'error',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    });
            },
            DocumentHistory: function (ModelOn) {

                this.expandHistory = ModelOn;

                var root = this;
                var token = '';
                if (ModelOn) {
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    var documentName = this.formName == 'ServiceQuotation' || this.formName == 'Quotation' ? 'Quotation' : this.formName == 'ServiceSaleOrder' || this.formName == 'SaleOrder' ? 'SaleOrder' : '';

                    root.$https.get('/Sale/DocumentHistory?documentName=' + documentName + '&Id=' + this.saleIdToCanvas + '&currentDocument=' + documentName, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {

                                root.historyList = response.data;
                            }
                        });
                }

            },
            GetSaleDashboardRecord: function () {
                if (this.request == 0) {
                    var root = this;
                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    var isQuotation = false;
                    if (this.formName == 'ServiceQuotation') {
                        isQuotation = true;

                    } else if (this.formName == 'ServiceSaleOrder') {
                        isQuotation = false;
                    }
                    var isServiceOrder = localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true;
                    var branchId = localStorage.getItem('BranchId');

                    this.loading = true;
                    this.$https.get('/Sale/SaleOrderDashboardList?isService=' + isServiceOrder + '&IsQuotation=' + isQuotation + '&branchId=' + branchId, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {

                            if (response.data != null) {

                                root.series = [];
                                root.series.push(response.data.draft);
                                root.series.push(response.data.approved);

                                root.series3 = [];
                                root.series3.push(response.data.totalDraft);
                                root.series3.push(response.data.totalApproved);

                                root.seriesOfCustomer[0].data = [];
                                root.chartOptionsOfCustomer.xaxis.categories = [];
                                root.earningSeries[0].data = [];
                                root.earningSeriesCash[0].data = [];
                                root.earningChartOption.xaxis.categories = [];
                                root.earningChartOptionCash.xaxis.categories = [];
                                root.seriesPurchase[0].data = [];
                                root.chartOptionsPurchase.xaxis.categories = [];
                                response.data.creditList.forEach(function (result) {
                                    root.seriesPurchase[0].data.push((parseFloat(result.amount)).toFixed(0));
                                    root.chartOptionsPurchase.xaxis.categories.push((result.name == null || result.name == undefined) ? '' : result.name);
                                });

                                response.data.paidListByAmount.forEach(function (result) {
                                    root.earningSeries[0].data.push((parseFloat(result.amount)).toFixed(0));
                                    root.earningChartOption.xaxis.categories.push((result.name == null || result.name == undefined) ? '' : result.name);
                                });
                                response.data.paidList.forEach(function (result) {
                                    root.earningSeriesCash[0].data.push((parseFloat(result.amount)).toFixed(0));
                                    root.earningChartOptionCash.xaxis.categories.push((result.name == null || result.name == undefined) ? '' : result.name);
                                });
                                response.data.creditListByAmount.forEach(function (result) {

                                    root.seriesOfCustomer[0].data.push((parseFloat(result.amount)).toFixed(0));
                                    root.chartOptionsOfCustomer.xaxis.categories.push((result.name == null || result.name == undefined) ? '' : result.name);
                                });

                                root.randerChart++;

                                root.saleListModel = {
                                    draft: response.data.draft,
                                    approved: response.data.approved,
                                    totalDraft: response.data.totalDraft,
                                    totalApproved: response.data.totalApproved,

                                };
                                root.loading = false;

                            }
                        });

                }
                this.request++;

            },
            openmodel: function (id) {
                this.purchaseOrderId = id;
                this.show1 = true;
            },
            CloseRefresh: function () {

                this.show1 = false;
                this.getPage();
            },
            SaveRecord: function (x) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var saleOrder = {
                    id: this.purchaseOrderId,
                    isClose: true,
                    reason: x
                };
                this.$https.post('/Purchase/SaveSaleOrderInformation', saleOrder, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response != undefined) {
                        root.show1 = false;
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: "Sale Order Closed Successfully!",
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: true
                        });

                        root.getPage();
                    }
                });
            },

            IsSave: function () {
                this.show1 = false;
            },

            AdvanceFilterFor: function () {
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

                    if (this.monthObj != undefined) {
                        this.month = moment(this.monthObj).format("MM");
                        this.year = moment(this.monthObj).format("YYYY");

                    }
                    if (this.user.id != undefined) {
                        this.userId = this.user.id;

                    }

                } else {
                    this.isDisable = false;
                    this.isDisableMonth = false;
                    if (this.$refs.userDropdown != null) {
                        this.$refs.userDropdown.EmptyRecord();
                    }
                    this.user = '';
                    this.userId = '';
                    this.year = '';
                    this.fromDate = '';
                    this.toDate = '';
                    this.month = '';
                    this.monthObj = '';
                    this.customerId = '';
                    this.randerforempty++;

                }

                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.userId, this.customerId);
            },


            search22: function () {

                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.userId, this.customerId);

            },

            clearData: function () {
                this.search = "";

                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.userId, this.customerId);

            },

            GetMonth: function () {
                if (this.monthObj != undefined) {
                    this.isDisable = true;
                    this.fromDate = '';
                    this.toDate = '';
                    this.month = moment(this.monthObj).format("MM");
                    this.year = moment(this.monthObj).format("YYYY");
                }
            },
            ConvertToSupplier: function (id) {

                this.$router.push({
                    path: '/addpurchaseorder',
                    query: {
                        id: id,
                        isQuotation: true,
                        formName: 'SupplierQuotation',
                        Add: true,
                    }
                })

            },
            GetDate1: function () {
                if (this.fromDate != '' || this.toDate != '') {
                    this.isDisableMonth = true;
                    this.year = '';
                    this.month = '';
                    this.monthObj = '';
                } else {
                    this.isDisableMonth = false;
                }
            },
            DeliveryChallanListWithCanvas: function (val, val1, val2) {
                this.expandDeliveryChallan = val;
                this.expandDeliveryChallan1 = val1;
                this.expandDeliveryChallan2 = val2;
            },
            SaleIdForCanvas: function (deliveryChallanId, saleOrderId, quotationId, proformaId, registrationNumber, date, saleId) {

                var root = this;
                var token = '';
                if (deliveryChallanId != null) {
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    root.saleIdToCanvas = saleId;
                    this.expandHistory = false;
                    this.randerExpand++;

                    root.$https.get('/Purchase/DeliveryChallanDetail?id=' + deliveryChallanId + '&simpleQuery=' + true, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                root.registrationNo = registrationNumber + ' - ' + root.getDate(date);
                                root.saleIdToCanvas = saleId;
                                root.canvasDate = response.data.date;
                                root.deliveryNo = response.data.registrationNo;
                                root.quotationNo = null,
                                    root.saleOrderNo = null,
                                    root.proformaNo = null,
                                    root.saleItem = response.data.deliveryChallanItems;
                            }
                        },
                            function (error) {
                                console.log(error);
                            });
                } else if (saleOrderId != null) {
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }

                    root.$https.get('/Purchase/SaleOrderDetail?id=' + saleOrderId + '&simpleQuery=' + true, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                root.registrationNo = registrationNumber + ' - ' + root.getDate(date);
                                root.saleIdToCanvas = saleId;
                                root.quotationNo = null;
                                root.deliveryNo = null;
                                root.proformaNo = null;
                                root.saleOrderNo = response.data.registrationNo;
                                root.canvasDate = response.data.date;
                                root.qutationId = response.data.quotationId;
                                root.vAT = response.data.taxRateName;
                                root.canvasTaxMethod = response.data.taxMethod;
                                root.discountType = response.data.discountType;
                                root.saleItem = response.data.saleOrderItems;

                                root.grossAmount = response.data.grossAmount;
                                root.discountAmount = response.data.discountAmount;
                                root.vatAmount = response.data.vatAmount;
                                root.afterDiscountAmount = response.data.afterDiscountAmount;
                                root.totalAmount = response.data.totalAmount;
                            }
                        },
                            function (error) {
                                console.log(error);
                            });
                } else if (quotationId != null) {
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }

                    root.$https.get('/Purchase/SaleOrderDetail?id=' + quotationId + '&simpleQuery=' + true, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                root.registrationNo = registrationNumber + ' - ' + root.getDate(date);
                                root.saleIdToCanvas = saleId;
                                root.quotationNo = response.data.registrationNo;
                                root.deliveryNo = null;
                                root.saleOrderNo = null;
                                root.proformaNo = null;
                                root.canvasDate = response.data.date;
                                root.canvasSaleOrderId = response.data.saleOrderId;
                                root.vAT = response.data.taxRateName;
                                root.canvasTaxMethod = response.data.taxMethod;
                                root.discountType = response.data.discountType;
                                root.saleItem = response.data.saleOrderItems;

                                root.grossAmount = response.data.grossAmount;
                                root.discountAmount = response.data.discountAmount;
                                root.vatAmount = response.data.vatAmount;
                                root.afterDiscountAmount = response.data.afterDiscountAmount;
                                root.totalAmount = response.data.totalAmount;
                            }
                        },
                            function (error) {
                                console.log(error);
                            });
                } else if (proformaId != null) {
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }

                    root.$https.get('/Sale/SaleDetail?id=' + proformaId + '&simpleQuery=' + true, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                root.registrationNo = registrationNumber + ' - ' + root.getDate(date);
                                root.saleIdToCanvas = saleId;
                                root.proformaNo = response.data.registrationNo;
                                root.quotationNo = null;
                                root.deliveryNo = null;
                                root.saleOrderNo = null;
                                root.canvasDate = response.data.date;
                                root.canvasSaleOrderId = response.data.saleOrderId;
                                root.qutationId = response.data.quotationId;
                                root.deliveryChallanId = response.data.deliveryChallanId;
                                root.vAT = response.data.taxRateName;
                                root.canvasTaxMethod = response.data.taxMethod;
                                root.discountType = response.data.discountType;
                                root.invoiceNote = response.data.invoiceNote;
                                root.saleItem = response.data.saleItems;

                                root.grossAmount = response.data.grossAmount;
                                root.discountAmount = response.data.discountAmount;
                                root.vatAmount = response.data.vatAmount;
                                root.afterDiscountAmount = response.data.afterDiscountAmount;
                                root.totalAmount = response.data.totalAmount;
                            }
                        },
                            function (error) {
                                console.log(error);
                            });
                }
            },
            ViewCustomerInfo: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (this.formName == 'ServiceQuotation') {
                    this.documentName = 'ServiceQuotation';
                    this.isService = true;
                } else if (this.formName == 'ServiceSaleOrder') {
                    this.documentName = 'ServiceSaleOrder';
                    this.isService = true;
                } else if (this.formName == 'Quotation') {
                    this.documentName = 'Quotation';
                    this.isService = false;
                } else if (this.formName == 'SaleOrder') {
                    this.documentName = 'SaleOrder';
                    this.isService = false;
                }

                root.$https.get('/Contact/ContactLedgerDetail?id=' + id + '&documentType=' + this.documentName + '&isService=' + this.isService + '&lastThreeMonth=' + true, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.customerInformation = response.data;
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },

            getDate: function (date) {
                return moment(date).format('LL');
            },

            GetRecordOfDelivery: function () {
                this.ReservedDeliveryChallanbool = false;
                this.ViewDeliveryChallan(this.saleOrderId);

            },
            GotoPage: function (link) {
                this.$router.push({
                    path: link
                });
            },
            sendEmail: function (saleId, email) {
                this.saleId = saleId
                this.customerEmail = email
                this.emailComposeShow = true;
            },
            // sendEmail(id)
            // {
            //     var root = this;
            //     var token = '';

            //     if (token == '') 
            //     {
            //         token = localStorage.getItem('token');
            //     }
            //     this.loading = true;
            //     root.$https.get('Sale/SendSaleEmail?id=' + id + '&isSaleOrder=true' , { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
            //         if (response.data.isSuccess) {
            //             root.$swal({
            //                 title: "Email",
            //                 text: response.data.isAddUpdate,
            //                 type: 'success',
            //                 icon: 'success',
            //                 showConfirmButton: false,
            //                 timer: 1500,
            //                 timerProgressBar: true,
            //             });
            //         }
            //         else
            //         {
            //             root.$swal({
            //                 title: 'Error',
            //                 text: response.data.isAddUpdate,
            //                 type: 'error',
            //                 icon: 'error',
            //                 showConfirmButton: false,
            //                 timer: 1500,
            //                 timerProgressBar: true,
            //             });
            //         }
            //         root.loading = false;
            //     });
            // },
            paymentModel: function (purchaseId, totalAmount, customerAccountId) {
                this.purchaseId = purchaseId;
                this.totalAmount = totalAmount;
                this.customerAccountId = customerAccountId;
                this.payment = true;
            },
            paymentSave: function () {
                this.payment = false;
            },

            DeleteFile: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Purchase/DeletePo', this.selected, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
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
                    for (let i in this.saleOrderList) {
                        this.selected.push(this.saleOrderList[i].id);
                    }
                }
            },
            getPage: function () {
                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.userId, this.customerId);
            },

            makeActive: function (item) {
                this.active = item;
                this.selectAll = false;
                this.selected = [];
                this.getData(this.search, 1, item, this.fromDate, this.toDate, this.userId, this.customerId);
            },
            getData: function (search, currentPage, status, fromDate, toDate, userId, customerId) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.formName == 'ServiceQuotation') {
                    this.isQuotation = true;
                }
                if (this.formName == 'Quotation') {
                    this.isQuotation = true;
                }
                if (this.formName == 'ServiceSaleOrder') {
                    this.isQuotation = false;
                }
                if (this.formName == 'SaleOrder') {
                    this.isQuotation = false;
                }

                var branchId = localStorage.getItem('BranchId');
                localStorage.setItem('currentPage', this.currentPage);
                localStorage.setItem('active', this.active);
                var isSale = localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true;

                this.$https.get('/Purchase/SaleServiceOrderList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&isService=' + isSale + '&isQuotation=' + this.isQuotation + '&fromDate=' + fromDate + '&toDate=' + toDate + '&userId=' + userId + '&month=' + this.month + '&year=' + this.year + '&CustomerId=' + customerId + '&branchId=' + branchId, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        root.randerToogle++;

                        root.saleOrderList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.currentPage = currentPage;
                        root.randerList++;

                    });
            },
            RemovePurchaseOrder: function (id) {

                var root = this;
                // working with IE and Chrome both
                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟',
                    text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You will not be able to recover this!' : 'لن تتمكن من استرداد هذا!',
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!',
                    closeOnConfirm: false,
                    closeOnCancel: false
                }).then(function (result) {
                    if (result) {

                        var token = '';
                        if (token == '') {
                            token = localStorage.getItem('token');
                        }
                        root.$https.get('/Purchase/DeleteSaleOrder?Id=' + id, {
                            headers: {
                                "Authorization": `Bearer ${token}`
                            }
                        })
                            .then(function (response) {
                                if (response.data.message.id != '00000000-0000-0000-0000-000000000000') {
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                        text: response.data.message.isAddUpdate,
                                        type: 'success',
                                        confirmButtonClass: "btn btn-success",
                                        buttonsStyling: false
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
                        this.$swal((this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!', (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Your file is still intact!' : 'ملفك لا يزال سليما!', (this.$i18n.locale == 'en' || root.isLeftToRight()) ? 'info' : 'معلومات');
                    }
                });
            },
            ConvertToInvoice: function (id) {
                this.$router.push({
                    path: '/AddSaleService',
                    query: {
                        token_name: 'Sales_token',
                        id: id,
                        document: this.formName,
                        formName: 'SaleInvoice'
                    }
                })

            },
            AddPurchaseOrder: function () {
                var root = this;

                if (this.formName == 'ServiceQuotation') {
                    root.$router.push({
                        path: '/AddSaleService',
                        query: {
                            Add: true,
                            token_name: 'Sales_token',
                            formName: 'ServiceQuotation',
                        }
                    })
                    localStorage.setItem('IsService', true);
                } else if (this.formName == 'ServiceSaleOrder') {
                    root.$router.push({
                        path: '/AddSaleService',
                        query: {
                            token_name: 'Sales_token',
                            isForm: true,
                            Add: true,
                            formName: 'ServiceSaleOrder'
                        }
                    })
                    localStorage.setItem('IsService', true);
                } else if (this.formName == 'Quotation') {
                    root.$router.push({
                        path: '/AddSaleService',
                        query: {
                            token_name: 'Sales_token',
                            Add: true,
                            formName: 'ServiceQuotation',
                        }
                    })
                } else if (this.formName == 'SaleOrder') {
                    root.$router.push({
                        path: '/AddSaleService',
                        query: {
                            token_name: 'Sales_token',
                            isForm: true,
                            Add: true,
                            formName: 'ServiceSaleOrder',
                        }
                    })
                }
            },
            PrintRdlc: function (id, isDownload) {

                var root = this;
                if (this.formName == 'ServiceSaleOrder' || this.formName == 'SaleOrder') {
                    this.reportname = true;
                }
                else if (this.formName == 'ServiceQuotation' || this.formName == 'Quotation') {
                    this.reportname = false
                }
                var companyId = '';
                companyId = localStorage.getItem('CompanyID');


                if (isDownload) {
                    this.loading = true;
                    this.$https.get(this.$ReportServer + '/SalesModuleReports/SaleOrder/SaleOrderReport.aspx?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + true + '&CompanyId=' + companyId + '&formName=' + this.formName + '&reportName=' + this.reportname + '&isDownload=' + isDownload
                        , { responseType: 'blob' }).then(function (response) {

                            root.loading = false;
                            const url = window.URL.createObjectURL(new Blob([response.data]));
                            const link = document.createElement('a');
                            link.href = url;
                            var date = moment().format('DD MMM YYYY');
                            link.setAttribute('download', root.formName + date + '.pdf');
                            document.body.appendChild(link);
                            link.click();

                        })
                }
                else {
                    var isBlind = localStorage.getItem('IsBlindPrint') == 'true' ? true : false;
                    if (isBlind) {
                        this.show = false;
                    }
                    else {
                        this.show = true;
                    }

                    this.reportsrc = this.$ReportServer + '/SalesModuleReports/SaleOrder/SaleOrderReport.aspx?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + true + '&CompanyId=' + companyId + '&formName=' + this.formName + '&reportName=' + this.reportname + '&isDownload=' + isDownload
                    this.changereport++;
                }
            },
            EditPurchaseOrder: function (id, isEditPaidInvoice, clone) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Purchase/SaleOrderDetail?Id=' + id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {

                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            if (root.formName == 'ServiceQuotation') {
                                root.$router.push({
                                    path: '/AddSaleService',
                                    query: {
                                        data: '',
                                        Add: false,
                                        token_name: 'Sales_token',
                                        formName: 'ServiceQuotation',
                                        isEditPaidInvoice: isEditPaidInvoice,
                                        clone: clone
                                    }
                                })
                            } else if (root.formName == 'ServiceSaleOrder') {
                                root.$router.push({
                                    path: '/AddSaleService',
                                    query: {
                                        data: '',
                                        Add: false,
                                        token_name: 'Sales_token',
                                        formName: 'ServiceSaleOrder',
                                        isEditPaidInvoice: isEditPaidInvoice,
                                        clone: clone
                                    }
                                })
                            } else if (root.formName == 'Quotation') {
                                root.$router.push({
                                    path: '/AddSaleService',
                                    query: {
                                        data: '',
                                        Add: false,
                                        token_name: 'Sales_token',
                                        formName: 'ServiceQuotation',
                                        isEditPaidInvoice: isEditPaidInvoice,
                                    }
                                })
                            } else if (root.formName == 'SaleOrder') {
                                root.$router.push({
                                    path: '/AddSaleService',
                                    query: {
                                        data: '',
                                        Add: false,
                                        token_name: 'Sales_token',
                                        formName: 'ServiceSaleOrder',
                                        isEditPaidInvoice: isEditPaidInvoice,
                                    }
                                })
                            }
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
            DuplicateSaleOrder: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Purchase/SaleOrderDetail?Id=' + id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {
                            response.data.isDuplicate = true;
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/AddSaleServiceOrder',
                                query: {
                                    data: '',
                                    Add: false
                                }
                            })
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },

            ViewInvoice: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Purchase/SaleOrderDetail?Id=' + id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/SaleServiceOrderView',
                                query: {
                                    data: '',
                                    Add: false
                                }
                            })
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },

            ViewDeliveryChallan: function (id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.saleOrderId = id
                root.$https.get('/Purchase/DeliveryChallanList?documentId=' + id + '&isSale=' + false + '&isDropdown=' + true + '&openBatch=' + this.openBatch, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.deliveryChallanList = response.data.deliveryChallanListLookUpModels;
                            if (response.data.isReserved != null) {
                                root.isReservedId = response.data.isReserved
                                root.isCloseChallan = response.data.isClose
                                root.isAddChallan = false;

                            } else {
                                root.isAddChallan = true;
                                root.isCloseChallan = response.data.isClose
                            }
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },

            DeliveryChllan: function (id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Purchase/SaleOrderDetail?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&deliveryChallan=' + true, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/AddDeliveryChallan',
                                query: {
                                    data: response.data,
                                    Add: false,
                                    isService: true,
                                    isSaleOrder: true,
                                    formName: root.formName

                                }
                            })

                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
            ReservedDeliveryChallan: function (id, fromSale, close) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (fromSale) {
                    root.$https.get('/Purchase/SaleOrderDetail?id=' + id + '&isSale=' + false + '&DeliveryChallan=' + true, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                root.deliveryChallanRecord = response.data;
                                root.isAdd = true;
                                root.ReservedDeliveryChallanbool = true;

                                root.deliveryUndefined = true;
                            }
                        },
                            function (error) {
                                this.loading = false;
                                console.log(error);
                            });
                } else {
                    var manualClose = false;
                    if (close == true) {
                        manualClose = true;
                    }
                    root.$https.get('/Purchase/DeliveryChallanDetail?id=' + id + '&isSale=' + false + '&isReserved=' + true + '&manualClose=' + manualClose, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {

                                root.deliveryChallanRecord = response.data;
                                root.isCloseChallan = response.data.isClose;
                                if (root.isCloseChallan) {
                                    root.isAdd = false;
                                    root.ReservedDeliveryChallanbool = false;
                                } else {
                                    root.isAdd = false;
                                    root.ReservedDeliveryChallanbool = true;
                                }

                            }
                        },
                            function (error) {
                                this.loading = false;
                                console.log(error);
                            });
                }

            },

            EditDeliveryChallan: function (id, View) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var isView = false;
                if (View == true) {
                    isView = true;
                }
                root.$https.get('/Purchase/DeliveryChallanDetail?id=' + id + '&isSale=' + false, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/AddDeliveryChallan',
                                query: {
                                    data: '',
                                    Add: false,
                                    isSaleOrder: true,
                                    isService: true,
                                    isView: isView,
                                }
                            })

                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },

        },
        created: function () {
            // this.GetHeaderDetail();
            if (this.$route.query.data == 'AddSaleServiceOrder') {
                this.$emit('update:modelValue', 'AddSaleServiceOrder');

            } else {
                this.$emit('update:modelValue', this.$route.name);

            }
            this.printTemplate = localStorage.getItem('PrintTemplate');
        },
        mounted: function () {

            this.colorVariants = localStorage.getItem('ColorVariants') == 'true' ? true : false;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');

            if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                this.currentPage = parseInt(localStorage.getItem('currentPage') == undefined || localStorage.getItem('currentPage') == 'NaN' ? 1 : localStorage.getItem('currentPage'));

                if (localStorage.getItem('active') == 'Credit') {
                    localStorage.setItem('active', 'Approved');

                }

                this.active = localStorage.getItem('active') == 'Hold' ? 'Draft' : localStorage.getItem('active');
                this.getPage();

            } else {
                if (this.isValid('CanDraftServiceSaleOrder')) {
                    this.makeActive("Draft");
                    this.currentPage = 1;
                } else if (this.isValid('CanViewServiceSaleOrder')) {
                    this.makeActive("Approved");
                    this.currentPage = 1;
                }

            }

            this.currency = localStorage.getItem('currency');
            //this.getData(this.search, 1);
        },
        updated: function () {
            if (this.selected.length < this.saleOrderList.length) {
                this.selectAll = false;
            } else if (this.selected.length == this.saleOrderList.length) {
                if (this.selected.length == 0) {
                    this.selectAll = false;
                } else {
                    this.selectAll = true
                }
            }
        }
    }
</script>

<style scoped>
    /* Tooltip container */

    .timeline:nth-child(2n) .year {
        right: auto !important;
        left: 26% !important;
    }

    .year {
        right: 20% !important;
    }

    .tooltip {
        position: relative;
        display: inline-block;
        opacity: 1 !important;
        z-index: 1 !important;
        /* If you want dots under the hoverable text */
    }

        /* Tooltip text */
        .tooltip .tooltiptext {
            visibility: hidden;
            width: 150px;
            background-color: #555;
            color: #fff;
            text-align: center;
            padding: 5px 0;
            border-radius: 6px;
            /* Position the tooltip text */
            position: absolute;
            z-index: 1 !important;
            bottom: 125%;
            left: 50%;
            margin-left: -60px;
            font-weight: 700 !important;
            /* Fade in tooltip */
        }

            /* Tooltip arrow */
            .tooltip .tooltiptext::after {
                content: "";
                position: absolute;
                top: 100%;
                left: 40%;
                margin-left: -5px;
                border-width: 5px;
                border-style: solid;
                border-color: #555 transparent transparent transparent;
            }

        /* Show the tooltip text when you mouse over the tooltip container */
        .tooltip:hover .tooltiptext {
            visibility: visible;
            opacity: 1;
        }
</style>
