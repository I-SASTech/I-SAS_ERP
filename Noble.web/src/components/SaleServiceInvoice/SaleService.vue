<template>
    <div class="row" v-if="isValid('CanViewHoldInvoices') || isValid('CanViewPaidInvoices') || isValid('CanViewCreditInvoices')">
        <div class="col-lg-12" v-if="isDayAlreadyStart">
            <div class="row ">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h5 class="page_title" v-if="isService">{{ $t('Sale.SaleServiceInvoice') }}</h5>
                                <h5 class="page_title" v-else>{{ $t('Sale.SaleInvoice') }}</h5>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);"> {{ $t('Sale.Home') }}</a></li>
                                    <li class="breadcrumb-item active" v-if="isService">
                                        {{ $t('Sale.SaleServiceInvoice') }}
                                    </li>
                                    <li class="breadcrumb-item active" v-else>
                                        {{ $t('Sale.SaleInvoice') }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">

                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('Sale.Close') }}
                                </a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="accordion" id="accordionExample">

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

                                                <div class="col-md-6 col-lg-2">
                                                    <div class="card report-card">
                                                        <div class="card-body">
                                                            <div class="row d-flex justify-content-center">
                                                                <div class="col">
                                                                    <p class="text-dark mb-0 fw-semibold">{{ $t('Sale.Hold') }}</p>
                                                                    <h3 class="m-0">{{hold}}</h3>
                                                                </div>
                                                                <div class="col-auto align-self-center">
                                                                    <div class="report-main-icon bg-light-alt">
                                                                        <i data-feather="tag" class="align-self-center text-muted icon-sm"></i>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-md-6 col-lg-2">
                                                    <div class="card report-card">
                                                        <div class="card-body">
                                                            <div class="row d-flex justify-content-center">
                                                                <div class="col">
                                                                    <p class="text-dark mb-0 fw-semibold">{{ $t('Sale.Cash') }}</p>
                                                                    <h3 class="m-0">{{cash}}</h3>
                                                                </div>
                                                                <div class="col-auto align-self-center">
                                                                    <div class="report-main-icon bg-light-alt">
                                                                        <i data-feather="zap" class="align-self-center text-muted icon-sm"></i>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-md-6 col-lg-2">
                                                    <div class="card report-card">
                                                        <div class="card-body">
                                                            <div class="row d-flex justify-content-center">
                                                                <div class="col">
                                                                    <p class="text-dark mb-0 fw-semibold">{{ $t('Sale.Credit') }}</p>
                                                                    <h3 class="m-0">{{credit}}</h3>
                                                                </div>
                                                                <div class="col-auto align-self-center">
                                                                    <div class="report-main-icon bg-light-alt">
                                                                        <i data-feather="lock" class="align-self-center text-muted icon-sm"></i>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-md-6 col-lg-2">
                                                    <div class="card report-card">
                                                        <div class="card-body">
                                                            <div class="row d-flex justify-content-center">
                                                                <div class="col">
                                                                    <p class="text-dark mb-0 fw-semibold">{{ $t('Sale.UnPaid') }}</p>
                                                                    <h3 class="m-0">{{unPaid}}</h3>
                                                                </div>
                                                                <div class="col-auto align-self-center">
                                                                    <div class="report-main-icon bg-light-alt">
                                                                        <i data-feather="calendar" class="align-self-center text-muted icon-sm"></i>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-md-6 col-lg-2">
                                                    <div class="card report-card">
                                                        <div class="card-body">
                                                            <div class="row d-flex justify-content-center">
                                                                <div class="col">
                                                                    <p class="text-dark mb-0 fw-semibold">{{ $t('Sale.Partially') }}</p>
                                                                    <h3 class="m-0">{{partially}}</h3>
                                                                </div>
                                                                <div class="col-auto align-self-center">
                                                                    <div class="report-main-icon bg-light-alt">
                                                                        <i data-feather="activity" class="align-self-center text-muted icon-sm"></i>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end col-->
                                                <div class="col-md-6 col-lg-2">
                                                    <div class="card report-card">
                                                        <div class="card-body">
                                                            <div class="row d-flex justify-content-center">
                                                                <div class="col">
                                                                    <p class="text-dark mb-0 fw-semibold">{{ $t('Sale.FullPaid') }}</p>
                                                                    <h3 class="m-0">{{fullyPaid}}</h3>
                                                                </div>
                                                                <div class="col-auto align-self-center">
                                                                    <div class="report-main-icon bg-light-alt">
                                                                        <i data-feather="briefcase" class="align-self-center text-muted icon-sm"></i>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end col-->

                                            </div>
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
                                                                <span class="badge badge-soft-primary">Hold</span> : {{parseFloat(saleListModel.hold ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                <span class="badge badge-soft-success">Cash</span> : {{ parseFloat(saleListModel.cash ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}<br>
                                                                <span class="badge badge-soft-danger">Credit</span> : {{ parseFloat(saleListModel.credit ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                <!-- <span class="badge badge-soft-dark">Total</span> :{{ saleListModel.totalReceipt }} -->

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
                                                                    <h4 class="card-title">{{ $t('Sale.InvoicePaymentType') }}</h4>
                                                                </div>
                                                                <!--end col-->

                                                            </div>
                                                            <!--end row-->
                                                        </div>
                                                        <!--end card-header-->
                                                        <div class="card-body">
                                                            <div class="">
                                                                <apexchart type="pie" width="300" height="330" :options="chartOptions2" :series="series2"></apexchart>
                                                                <span class="badge badge-soft-primary">UnPaid</span> : {{parseFloat(saleListModel.unPaid ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                <span class="badge badge-soft-success">Partially</span> : {{parseFloat(saleListModel.partially ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}<br>
                                                                <span class="badge badge-soft-dark">FullyPaid</span> : {{parseFloat(saleListModel.fullyPaid).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
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
                                                                <span class="badge badge-soft-primary">Hold:</span> : {{currency}} {{parseFloat(saleListModel.totalHold ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                <span class="badge badge-soft-success">Cash</span> : {{currency}} {{parseFloat( saleListModel.totalCash ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}<br>
                                                                <span class="badge badge-soft-danger">Credit</span> : {{currency}} {{parseFloat(saleListModel.totalCredit ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                <!-- <span class="badge badge-soft-dark">Total</span> :{{ saleListModel.totalNormal }} -->
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
                                                                    <h4 class="card-title">{{ $t('Sale.TrendingCreditCustomerbyAmount') }}</h4>
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
                                                                    <h4 class="card-title">{{ $t('Sale.TrendingCashCustomerbyAmount') }}</h4>
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
                                                                    <h4 class="card-title"> {{ $t('Sale.TrendingCreditCustomer') }}</h4>
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
                                                                    <h4 class="card-title">{{ $t('Sale.TrendingCashCustomer') }} </h4>
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
            <div class="card ">
                <div class="card-header ">
                    <div class="row">
                        <div class="col-lg-8" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-soft-primary" type="button" id="button-addon1" v-on:click="search22(true)">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" class="form-control" :placeholder="$t('Sale.SearchOfInvoice')" aria-label="Example text with button addon" aria-describedby="button-addon1">

                                <a class="btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2" value="Advance Filter">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">
                            <button v-on:click="search22()" :disabled="!search" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData()" :disabled="!search" type="button"
                                    class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>
                        </div>
                    </div>
                    <div class="row " v-if="advanceFilters">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4">
                            <label class="text  font-weight-bolder">{{ $t('Sale.Customer') }}</label>
                            <customerdropdown v-model="customerId" ref="CustomerDropdown" />
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

                        <div class="col-xs-12  col-lg-3 " v-if="isValid('CanViewTerminal') || isValid('MachineWisePrefix') || isValid('Terminal') || isValid('CanStartDay') ||  isValid('TouchInvoiceTemplate1')">
                            <label class="text  font-weight-bolder"> {{ $t('Sale.Counter') }}</label>
                            <terminal-dropdown v-model="terminalId" :terminalType="terminalType" :terminalUserType="'Offline'" :isSelect="true" ref="TerminalDropDown" />

                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">
                            <label class="text  font-weight-bolder"> {{$t('Sale.User1')}}</label>
                            <usersDropdown v-model="user" ref="userDropdown" :isloginhistory="isloginhistory" />
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">
                            <label class="text  font-weight-bolder"> {{$t('Sale.CustomerType')}}</label>
                            <multiselect :options="['Cash', 'Credit','Walk-In']" v-model="customerType">
                            </multiselect>
                        </div>

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">

                            <button v-on:click="FilterRecord(true)" :disabled="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="FilterRecord(false)" :disabled="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mx-2 mt-3">
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
                                    <h6 style="color:red">Delivery Note Issued</h6>
                                </div>
                                <div class="row" v-else>
                                    <div class="col-lg-4" v-if="isAddChallan">
                                        <a v-on:click="ReservedDeliveryChallan(SaleInvoiceId,true)" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
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
                                    <div class="col-lg-4" v-if="!isAddChallan">
                                        <a v-on:click="DeliveryChllan(SaleInvoiceId)" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                            <i class="align-self-center icon-xs ti-plus"></i>
                                            Add Dilvery Note
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
                                                    Invoice No
                                                </th>

                                                <th style="width: 70px;" class="text-end">
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(purchaseOrder,index) in deliveryChallanList" v-bind:key="purchaseOrder.id">
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
                                                    {{purchaseOrder.documentNumberForSale}}
                                                </td>

                                                <td class="text-end">

                                                    <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('SaleOrder.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                    <div class="dropdown-menu">
                                                        <!-- <a class="dropdown-item" href="javascript:void(0)" v-if="!isCloseChallan" v-on:click="EditDeliveryChallan(purchaseOrder.id)">{{ $t('SaleOrder.EditInvoice') }}</a> -->
                                                        <a class="dropdown-item" href="javascript:void(0)" v-on:click="EditDeliveryChallan(purchaseOrder.id,true)">View</a>
                                                        <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintDeliveryChallan(purchaseOrder.id)">{{ $t('SaleOrder.A4Print') }}</a>
                                                        <a class="dropdown-item" href="javascript:void(0)">Issue Gate Pass</a>

                                                    </div>

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight3" aria-labelledby="offcanvasRightLabel" style="width:600px !important">
                        <div class="offcanvas-header">
                            <h5 id="offcanvasRightLabel" class="m-0">{{ $t('Sale.MoreDetails') }} ({{ registrationNo }})</h5>
                            <button v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                        </div>
                        <div class="offcanvas-body">
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
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item" v-if="isValid('CanViewHoldInvoices')">
                            <a class="nav-link" v-bind:class="{active:active == 'Hold'}" v-on:click="makeActive('Hold')" data-bs-toggle="tab" href="#Hold" role="tab" aria-selected="true">
                                {{ $t('Sale.Hold') }}
                            </a>
                        </li>
                        <li class="nav-item" v-if="isValid('CanViewHoldInvoices')">
                            <a class="nav-link" v-bind:class="{active:active == 'Credit'}" v-on:click="makeActive('Credit')" data-bs-toggle="tab" href="#Credit" role="tab" aria-selected="true" v-if="isService">
                                {{ $t('Sale.Post') }}
                            </a>
                            <a class="nav-link" v-bind:class="{active:active == 'Credit'}" v-on:click="makeActive('Credit')" data-bs-toggle="tab" href="#Credit" role="tab" aria-selected="true" v-else>
                                {{ $t('Sale.Post') }}
                            </a>
                        </li>
                        <li class="nav-item" v-if="documentName=='Document'">
                            <a class="nav-link" v-bind:class="{active:active == 'GlobalInvoice'}" v-on:click="makeActive('GlobalInvoice')" data-bs-toggle="tab" href="#GlobalInvoice" role="tab" aria-selected="true">
                                {{ $t('Sale.GlobalInvoice') }}
                            </a>
                        </li>
                        <li class="nav-item" v-if="documentName=='Document'">
                            <a class="nav-link" v-bind:class="{active:active == 'ReceiptInvoice'}" v-on:click="makeActive('ReceiptInvoice')" data-bs-toggle="tab" href="#ReceiptInvoice" role="tab" aria-selected="true">
                                {{ $t('Sale.ReceiptInvoice') }}
                            </a>
                        </li>
                        <li class="nav-item" v-if="documentName=='Document'">
                            <a class="nav-link" v-bind:class="{active:active == 'SaleOrder'}" v-on:click="makeActive('SaleOrder')" data-bs-toggle="tab" href="#SaleOrder" role="tab" aria-selected="true">
                                {{ $t('SaleOrder.SaleOrder') }}
                            </a>
                        </li>
                        <li class="nav-item" v-if="documentName=='Document'">
                            <a class="nav-link" v-bind:class="{active:active == 'Quotation'}" v-on:click="makeActive('Quotation')" data-bs-toggle="tab" href="#Quotation" role="tab" aria-selected="true">
                                {{ $t('Quotation.Quotation') }}
                            </a>
                        </li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div class="tab-pane pt-3" id="Hold" role="tabpanel" v-bind:class="{ active: active == 'Hold' }">
                            <div class="row">
                                <div class="inline-fields col-md-4 mb-2" v-if="isValid('SaleHoldSetup')">
                                    <multiselect :options="['Deleted','Used','All']" v-model="saleHoldType" v-bind:placeholder="$t('Select Hold Type')" @update:modelValue="GetSaleHoldTypes()">
                                    </multiselect>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th>
                                                {{ $t('Sale.InvoiceNo') }}
                                            </th>
                                            <th>
                                                {{ $t('Sale.Date') }}
                                            </th>
                                            <th>
                                                {{ $t('Sale.CustomerName')}}
                                            </th>
                                            <th>
                                                {{ $t('Sale.CreatedBy') }}
                                            </th>
                                            <th>
                                                {{ $t('InvoiceNote')}}
                                            </th>
                                            <th v-if="isValid('SaleHoldSetup')">
                                                {{ $t('Sale.SaleHoldType') }}
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th>
                                                {{ $t('Sale.NetAmount') }}
                                            </th>
                                            <th class="text-end">

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(sale,index) in saleList" v-bind:key="index">
                                            <td v-if="currentPage === 1">
                                                {{index+1}}
                                            </td>
                                            <td v-else>
                                                {{((currentPage*10)-10) +(index+1)}}
                                            </td>
                                            <td v-if="isValid('CanEditHoldInvoices') && (isValid('CreditInvoices') || isValid('CashInvoices'))">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditSale(sale.id,false)">{{sale.registrationNumber}}</a>
                                                </strong> <br />
                                                <span class="badge bg-success">{{sale.isCredit ? 'Credit':'Cash'}}</span>
                                            </td>
                                            <td v-else>
                                                {{sale.registrationNumber}}
                                                <span class="badge bg-success">{{sale.isCredit ? 'Credit':'Cash'}}</span>

                                            </td>
                                            <td>
                                                {{getDate(sale.date)}}
                                            </td>
                                            <td>
                                                <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(sale.customerId, sale.cashCustomerId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2" aria-controls="offcanvasRight">{{sale.customerName}}</a>
                                            </td>
                                            <td>
                                                {{sale.userName}} <br />
                                                <span v-if="isDayStarts=='true'">{{sale.counterName}}</span>
                                            </td>
                                            <td v-if="sale.invoiceNote != null">
                                                <a href="javascript:void(0)" v-on:click="SaleIdForCanvas(sale.deliveryChallanId,sale.saleOrderId, sale.qutationId,sale.proformaId, sale.registrationNumber, sale.date,sale.id)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight3" aria-controls="offcanvasRight">{{sale.invoiceNote}}</a>
                                            </td>
                                            <td v-else>
                                                ---
                                            </td>
                                            <td v-if="sale.saleHoldTypes == 1 && isValid('SaleHoldSetup')">
                                                Hold
                                            </td>
                                            <td v-else-if="sale.saleHoldTypes == 2 && isValid('SaleHoldSetup')">
                                                Deleted
                                            </td>
                                            <td v-else-if="sale.saleHoldTypes == 3 && isValid('SaleHoldSetup')">
                                                Used
                                            </td>
                                            <td v-if="allowBranches">
                                                {{sale.branchCode}}
                                            </td>
                                            <td>
                                                {{currency}} {{parseFloat(sale.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('Sale.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="EditSale(sale.id,false)" v-if="isValid('CanEditHoldInvoices') && (isValid('CreditInvoices') || isValid('CashInvoices'))">{{ $t('Sale.EditInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="DuplicateInvoice(sale.id)" v-if="isValid('CanDuplicateInvoices') && (isValid('CreditInvoices') || isValid('CashInvoices')) ">{{ $t('Sale.DuplicateInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(sale.id,false)" title="Template View" v-if="isValid('CanViewInvoiceDetail') && printTemplate=='Template6'">{{ $t('Sale.TemplateView') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(sale.id,false)" v-if="isValid('CanViewInvoiceDetail') && printTemplate!='Template6'">{{ $t('Sale.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(sale.id,false)" v-if="isValid('CanA4Print') ">{{ $t('Sale.A4Print') }} </a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(sale.id,true)" v-if="isValid('CanA4Print') ">{{ $t('Sale.PdfDownload') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" @click="RemoveSale(sale.id,false)" v-if="(isValid('CanDeleteHoldInvoices')) && sale.isDeleted == false">{{ $t('Sale.Delete') }} </a>
                                                    <a class="dropdown-item" href="javascript:void(0)" @click="RemoveSale(sale.id,true)" v-if="(isValid('CanDeleteHoldInvoices') && isValid('SaleHoldSetup')) && sale.isDeleted == true ">{{ $t('Convert To Hold') }} </a>
                                                    <a class="dropdown-item" href="javascript:void(0)" @click="DeleteSalePermanently(sale.id)" v-if="isValid('CanDeleteHoldInvoices') ">{{ $t('Sale.PermanentDelete') }} </a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="sendEmail(sale.id, sale.customerEmail)" v-if="isValid('CanSendSaleEmailAsLink') || isValid('CanSendSaleEmailAsPdf') ">{{ $t('Sale.Email') }}</a>
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

                        <div class="tab-pane pt-3" id="Credit" role="tabpanel" v-bind:class="{ active: active == 'Credit' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th>
                                                {{ $t('Sale.InvoiceNo') }}
                                            </th>
                                            <th>
                                                {{ $t('Sale.Date') }}
                                            </th>
                                            <th>
                                                {{ $t('Sale.CustomerName')}}
                                            </th>
                                            <th>
                                                {{ $t('Sale.CreatedBy') }}
                                            </th>
                                            <th>
                                                {{ $t('InvoiceNote')}}
                                            </th>
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th>
                                                {{ $t('Sale.NetAmount') }}
                                            </th>
                                            <th class="text-end">

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(sale,index) in saleList" v-bind:key="index">
                                            <td v-if="currentPage === 1">
                                                {{index+1}}
                                            </td>
                                            <td v-else>
                                                {{((currentPage*10)-10) +(index+1)}}
                                            </td>
                                            <td>
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditSale(sale.id,true)">{{sale.registrationNumber}}</a>
                                                </strong> <br />

                                                <div v-if="sale.isSaleReturn">
                                                    <div class="badge bg-info">
                                                        {{($i18n.locale == 'en' ||isLeftToRight())?' Returned Invoice':'الفاتورة المرتجعة'}}
                                                    </div>
                                                </div>
                                                <div v-else>
                                                    <div class="badge bg-danger" v-if="sale.partiallyInvoices==1">
                                                        {{($i18n.locale == 'en' ||isLeftToRight())?'Un-Paid':'غير مدفوعة'}}
                                                    </div>
                                                    <div class="badge bg-primary" v-if="sale.partiallyInvoices==2">
                                                        {{($i18n.locale == 'en' ||isLeftToRight())?' Partially Paid':'المدفوعة جزئيا'}}

                                                    </div>
                                                    <div class="badge bg-success" v-if="sale.partiallyInvoices==3">

                                                        {{($i18n.locale == 'en' ||isLeftToRight())?' Fully Paid':'مدفوعة بالكامل'}}
                                                    </div>
                                                </div>

                                            </td>
                                            <td>
                                                {{getDate(sale.date)}}
                                            </td>
                                            <td>
                                                <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(sale.customerId, sale.cashCustomerId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2" aria-controls="offcanvasRight">{{sale.customerName}}</a>
                                            </td>

                                            <td>
                                                {{sale.userName}}
                                            </td>
                                            <td v-if="sale.invoiceNote != null">
                                                <a href="javascript:void(0)" v-on:click="SaleIdForCanvas(sale.deliveryChallanId,sale.saleOrderId, sale.qutationId,sale.proformaId, sale.registrationNumber, sale.date,sale.id)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight3" aria-controls="offcanvasRight">{{sale.invoiceNote}}</a>
                                            </td>
                                            <td v-else>
                                                ---
                                            </td>
                                            <td v-if="allowBranches">
                                                {{sale.branchCode}}
                                            </td>
                                            <td>
                                                {{currency}} {{parseFloat(sale.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('Sale.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="EditSale(sale.id,true)" v-if="isValid('CanEditHoldInvoices') && (isValid('CreditInvoices') || isValid('CashInvoices'))">{{ $t('Sale.EditInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(sale.id,false)" title="Template View" v-if="isValid('CanViewInvoiceDetail') && printTemplate=='Template6'">{{ $t('Sale.TemplateView') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(sale.id,false)" v-if="isValid('CanViewInvoiceDetail') && printTemplate!='Template6'">{{ $t('Sale.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(sale.id,false)" v-if="isValid('CanA4Print') ">{{ $t('Sale.A4Print') }} </a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintRdlc(sale.id,true)" v-if="isValid('CanA4Print') ">{{ $t('Sale.PdfDownload') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="DuplicateInvoice(sale.id)" v-if="isValid('CanDuplicateInvoices') && (isValid('CreditInvoices') || isValid('CashInvoices')) ">{{ $t('Sale.DuplicateInvoice') }}</a>
                                                    <!-- <a class="dropdown-item" href="javascript:void(0)" v-on:click="sendEmail(sale.id)" v-if="sale.isDefault">{{ $t('Sale.Email') }}</a> -->
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="sendEmail(sale.id, sale.customerEmail)" >{{ $t('Sale.Email') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0);" v-on:click="ViewDeliveryChallan(sale.id)" data-bs-toggle="offcanvas" v-if="isValid('SaleToDeliveryNote') " data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">Create | View Delivery Note</a>
                                                    <a class="dropdown-item" href="javascript:void(0);" v-on:click="ReceiptGenerationandViewing(sale.id)" v-if="sale.partiallyInvoices != 3 && ( isValid('PremiumAdvancePayment') || isValid('MultipleAdvancePayment'))">Create Payment Receipt</a>
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

                        <div class="tab-pane pt-3" id="GlobalInvoice" role="tabpanel" v-bind:class="{ active: active == 'GlobalInvoice' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th style="width:40px;">#</th>
                                            <th style="width:105px;">
                                                {{ $t('Sale.InvoiceNo') }}
                                            </th>
                                            <th style="width:105px;">
                                                {{ $t('Sale.SONumber') }}

                                            </th>
                                            <th style="width: 200px;">
                                                {{ $t('Sale.Date') }}
                                            </th>
                                            <th style="width: 220px;" v-if="english=='true'">

                                                {{$englishLanguage($t('Sale.CustomerName'))  }}
                                            </th>
                                            <th style="width: 220px;" v-if="isOtherLang()">
                                                {{ $t('Sale.CustomerNameArabic') }}
                                            </th>
                                            <th style="width: 120px;">
                                                {{ $t('Sale.CreatedBy') }}
                                            </th>
                                            <th style="width: 120px;" v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th style="width: 120px;">
                                                {{ $t('Sale.NetAmount') }}
                                            </th>
                                            <th style="width: 70px;" class="text-end" v-if="isValid('CanViewDueReceivePay')">

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(sale,index) in saleList" v-bind:key="index">
                                            <td v-if="currentPage === 1">
                                                {{index+1}}
                                            </td>
                                            <td v-else>
                                                {{((currentPage*10)-10) +(index+1)}}
                                            </td>
                                            <td>
                                                <strong>
                                                    {{sale.registrationNumber}}
                                                </strong> <br />
                                                <div class="badge badge-danger" v-if="sale.partiallyInvoices==1">
                                                    {{($i18n.locale == 'en' ||isLeftToRight())?'Un-Paid':'غير مدفوعة'}}
                                                </div>
                                                <div class="badge badge-primary" v-if="sale.partiallyInvoices==2">
                                                    {{($i18n.locale == 'en' ||isLeftToRight())?' Partially Paid':'المدفوعة جزئيا'}}

                                                </div>
                                                <div class="badge badge-outline-success" v-if="sale.partiallyInvoices==3">

                                                    {{($i18n.locale == 'en' ||isLeftToRight())?' Fully Paid':'مدفوعة بالكامل'}}
                                                </div>
                                            </td>
                                            <td>
                                                {{sale.saleOrderNo==null? '---':sale.saleOrderNo}}
                                            </td>
                                            <td>
                                                {{getDate(sale.date)}}
                                            </td>
                                            <td v-if="english=='true'">
                                                <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(sale.customerId, sale.cashCustomerId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">{{sale.customerName}}</a>
                                            </td>
                                            <td v-if="isOtherLang()">
                                                <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(sale.customerId, sale.cashCustomerId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">{{sale.customerNameArabic}}</a>
                                            </td>
                                            <td>
                                                {{sale.userName}}

                                            </td>
                                            <td v-if="allowBranches">
                                                {{sale.branchCode}}

                                            </td>
                                            <td v-if="isValid('CanViewDueReceivePay')">
                                                {{currency}} {{parseFloat(sale.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('Sale.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="DuplicateInvoice(sale.id)" v-if="isValid('CanDuplicateInvoices') && (isValid('CreditInvoices') || isValid('CashInvoices')) ">{{ $t('Sale.DuplicateInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="ViewInvoiceTemplate(sale.id)" title="Template View" v-if="isValid('CanViewInvoiceDetail') && printTemplate=='Template6'">{{ $t('Sale.TemplateView') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="ViewInvoice(sale.id)" v-if="isValid('CanViewInvoiceDetail') && printTemplate!='Template6'">{{ $t('Sale.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoice(sale.id, 'print')" v-if="isValid('CanA4Print') ">{{ $t('Sale.A4Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoice(sale.id, 'download')" v-if="isValid('CanA4Print') ">{{ $t('Sale.PdfDownload') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="sendEmail(sale.id)" v-if="isValid('CanSendSaleEmailAsLink') || isValid('CanSendSaleEmailAsPdf') ">{{ $t('Sale.Email') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0);" v-on:click="ViewDeliveryChallan(sale.id)" data-bs-toggle="offcanvas" v-if="isValid('SaleToDeliveryNote') " data-bs-target="#offcanvasRight" aria-controls="offcanvasRight"> Issue Delivery Note</a>

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

                        <div class="tab-pane pt-3" id="ReceiptInvoice" role="tabpanel" v-bind:class="{ active: active == 'ReceiptInvoice' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th style="width:40px;">#</th>
                                            <th style="width:105px;">
                                                {{ $t('Sale.InvoiceNo') }}
                                            </th>
                                            <th style="width:105px;">
                                                {{ $t('Sale.SONumber') }}

                                            </th>
                                            <!--<th >
                                                {{ $t('Sale.Type') }}
                                            </th>-->
                                            <th style="width: 200px;">
                                                {{ $t('Sale.Date') }}
                                            </th>
                                            <th style="width: 220px;" v-if="english=='true'">

                                                {{$englishLanguage($t('Sale.CustomerName'))  }}
                                            </th>
                                            <th style="width: 220px;" v-if="isOtherLang()">
                                                {{ $t('Sale.CustomerNameArabic') }}
                                            </th>
                                            <th style="width: 120px;">
                                                {{ $t('Sale.CreatedBy') }}
                                            </th>
                                            <th style="width: 120px;" v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th style="width: 120px;">
                                                {{ $t('Sale.NetAmount') }}
                                            </th>
                                            <th style="width: 70px;" class="text-end" v-if="isValid('CanViewDueReceivePay')">

                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(sale,index) in saleList" v-bind:key="index">
                                            <td v-if="currentPage === 1">
                                                {{index+1}}
                                            </td>
                                            <td v-else>
                                                {{((currentPage*10)-10) +(index+1)}}
                                            </td>
                                            <td>
                                                <strong>
                                                    {{sale.registrationNumber}}
                                                </strong> <br />
                                                <div class="badge badge-danger" v-if="sale.partiallyInvoices==1">
                                                    {{($i18n.locale == 'en' ||isLeftToRight())?'Un-Paid':'غير مدفوعة'}}
                                                </div>
                                                <div class="badge badge-primary" v-if="sale.partiallyInvoices==2">
                                                    {{($i18n.locale == 'en' ||isLeftToRight())?' Partially Paid':'المدفوعة جزئيا'}}

                                                </div>
                                                <div class="badge badge-outline-success" v-if="sale.partiallyInvoices==3">

                                                    {{($i18n.locale == 'en' ||isLeftToRight())?' Fully Paid':'مدفوعة بالكامل'}}
                                                </div>
                                            </td>
                                            <td>
                                                {{sale.saleOrderNo==null? '---':sale.saleOrderNo}}
                                            </td>
                                            <td>
                                                {{getDate(sale.date)}}
                                            </td>
                                            <td v-if="english=='true'">
                                                <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(sale.customerId, sale.cashCustomerId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">{{sale.customerName}}</a>
                                            </td>
                                            <td v-if="isOtherLang()">
                                                <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(sale.customerId, sale.cashCustomerId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">{{sale.customerNameArabic}}</a>
                                            </td>
                                            <td>
                                                {{sale.userName}}

                                            </td>
                                            <td v-if="allowBranches">
                                                {{sale.branchCode}}
                                            </td>
                                            <td v-if="isValid('CanViewDueReceivePay')">
                                                {{currency}} {{parseFloat(sale.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('Sale.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="DuplicateInvoice(sale.id)" v-if="isValid('CanDuplicateInvoices') && (isValid('CreditInvoices') || isValid('CashInvoices')) ">{{ $t('Sale.DuplicateInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="ViewInvoiceTemplate(sale.id)" title="Template View" v-if="isValid('CanViewInvoiceDetail') && printTemplate=='Template6'">{{ $t('Sale.TemplateView') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="ViewInvoice(sale.id)" v-if="isValid('CanViewInvoiceDetail') && printTemplate!='Template6'">{{ $t('Sale.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoice(sale.id, 'print')" v-if="isValid('CanA4Print') ">{{ $t('Sale.A4Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoice(sale.id, 'download')" v-if="isValid('CanA4Print') ">{{ $t('Sale.PdfDownload') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="sendEmail(sale.id)" v-if="isValid('CanSendSaleEmailAsLink') || isValid('CanSendSaleEmailAsPdf') ">{{ $t('Sale.Email') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0);" v-on:click="ViewDeliveryChallan(sale.id,sale.invoiceNote)" data-bs-toggle="offcanvas" v-if="isValid('SaleToDeliveryNote') " data-bs-target="#offcanvasRight" aria-controls="offcanvasRight"> View Delivery Note</a>

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

                        <div class="tab-pane pt-3" id="SaleOrder" role="tabpanel" v-bind:class="{ active: active == 'SaleOrder' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th>
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
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.NetAmount') }}
                                            </th>
                                            <th style="width: 70px;" class="text-end">
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(purchaseOrder,index) in saleList" v-bind:key="index">
                                            <td v-if="currentPage === 1">
                                                {{index+1}}
                                            </td>
                                            <td v-else>
                                                {{((currentPage*10)-10) +(index+1)}}
                                            </td>
                                            <td v-if="isValid('CanEditServiceSaleOrder')">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchaseOrder.id)">{{purchaseOrder.registrationNumber}}</a>
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
                                            <td v-if="allowBranches">
                                                {{purchaseOrder.branchCode}}
                                            </td>
                                            <td>
                                                {{currency}} {{parseFloat(purchaseOrder.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('SaleOrder.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="ViewSaleOrder(purchaseOrder.id)" v-if="isValid('CanViewDetailServiceSaleOrder')">{{ $t('SaleOrder.ViewInvoice') }}</a>

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

                        <div class="tab-pane pt-3" id="Quotation" role="tabpanel" v-bind:class="{ active: active == 'Quotation' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th>
                                                {{ $t('SaleOrder.SONumber') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.CreatedDate') }}
                                            </th>
                                            <th>
                                                {{ $t('SaleOrder.CustomerName')}}
                                            </th>
                                            <!-- <th>
                                                {{ $t('SaleOrder.CustomerName') |arabicLanguage}}
                                            </th> -->
                                            <th v-if="allowBranches">
                                                {{ $t('DailyExpense.BranchCode') }}
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
                                        <tr v-for="(purchaseOrder,index) in saleList" v-bind:key="index">
                                            <td v-if="currentPage === 1">
                                                {{index+1}}
                                            </td>
                                            <td v-else>
                                                {{((currentPage*10)-10) +(index+1)}}
                                            </td>

                                            <td>
                                                {{purchaseOrder.registrationNumber}}
                                            </td>
                                            <td>
                                                {{purchaseOrder.date}}
                                            </td>
                                            <td>
                                                <a href="javascript:void(0)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">{{purchaseOrder.customerName}}</a>

                                            </td>
                                            <!-- <td>
                                                <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(purchaseOrder.customerId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">{{purchaseOrder.customerArabicName}}</a>

                                            </td> -->
                                            <td v-if="allowBranches">
                                                {{purchaseOrder.branchCode}}
                                            </td>
                                            <td>
                                                {{currency}} {{parseFloat(purchaseOrder.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                <toggle-button v-model="purchaseOrder.isClos" v-on:change="changeStatus(purchaseOrder.id)" class="ml-2 mt-2" color="#3178F6" v-if="!purchaseOrder.isClose" />
                                                <span class="badge badge-boxed  badge-outline-success" v-if="purchaseOrder.isClose">{{ $t('Close') }}</span>
                                            </td>
                                            <td class="text-end">

                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('Quotation.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="ViewQuotationInvoice(purchaseOrder.id)" v-if="isValid('CanViewServiceQuotationDetail')">{{ $t('Quotation.ViewQuotation') }}</a>

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

                    </div>

                </div>
            </div>

        </div>
        <div class="col-lg-6 col-sm-6 ml-auto mr-auto" v-else>
            <div class="card p-3 text-center " v-if="bankDetail">
                <h4 class="">{{ $t('FirstStartInvoice') }}</h4>
                <router-link :to="{path: '/WholeSaleDay', query: { token_name:'DayStart_token', fromDashboard:'true' } }"><a href="javascript:void(0)" class="btn btn-outline-danger ">{{ $t('Dashboard.DayStart') }}</a></router-link>
            </div>
            <div class="card p-3 text-center " v-else>
                <h4 class="">{{ $t('FirstStartInvoice') }}</h4>
                <router-link :to="{path: '/dayStart', query: { token_name:'DayStart_token', fromDashboard:'true' } }"><a href="javascript:void(0)" class="btn btn-outline-danger ">{{ $t('Dashboard.DayStart') }}</a></router-link>
            </div>
        </div>

        <!--<ObaagestSaleInvoice :printDetails="printDetails" :isTouchScreen="sale" :headerFooter="headerFooter" :saleSizeAssortment="saleSizeAssortment" v-if="printDetails.length != 0 && printSize=='A4' && printTemplate=='Template8' && isPrint && !download" v-bind:key="printRender" />

        <sale-invoice-service-default-Download :printDetails="printDetails" :isTouchScreen="sale" :headerFooter="headerFooter" v-if="download && printTemplate=='Default'" @close="download=false" />

        <sale-invoice-default :printDetails="printDetails" :isTouchScreen="sale" :headerFooter="headerFooter" v-if="printDetails.length != 0 && printSize=='A4' && printTemplate=='Default' && isPrint && !download" v-bind:key="printRender" />-->


        <!-- <email-compose :show="emailComposeShow" v-if="emailComposeShow" @close="emailComposeShow = false" :documentId="saleId" :headerFooter="headerFooter" :formName="'Invoice'"></email-compose> -->
        <SendEmail :show="emailComposeShow" v-if="emailComposeShow" @close="emailComposeShow = false" :id="saleId" :from="'SaleInvocie'" :customerEmail="customerEmail"></SendEmail>
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
                        <h6 class="text-danger">Showing Last Three Month Invoices</h6>
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
        <!-- <email-compose :show="emailComposeShow" v-if="emailComposeShow" @close="emailComposeShow = false" :documentId="saleId" :headerFooter="headerFooter" :formName="'Invoice'"></email-compose> -->
        <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport" @close="show=false" @IsSave="IsSave" />

        <DeliveryChallanModel :newshow="ReservedDeliveryChallanbool" v-if="ReservedDeliveryChallanbool" :newpurchase="deliveryChallanRecord" :isReservedChallan="ReservedDeliveryChallanbool" :type="isAdd" :isService="isService" @close="GetRecordOfDelivery"></DeliveryChallanModel>

        <!-- <addholdsetup :brand="holdSetup" :show="holtSetupShow" v-if="holtSetupShow" @close="IsSave1"/> -->
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'

    import Multiselect from "vue-multiselect";

    import moment from "moment";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        props: ['formName'],
        components: {
            Multiselect,
            Loading
        },

        mixins: [clickMixin],
        data: function () {
            return {
                documentName: this.formName,
                allowBranches: false,
                customerEmail: '',
                customerType: '',
                currentDocument: '',
                registrationNo: '',
                loading: false,
                holtSetupShow: false,
                expandHistory: false,
                randerExpand: 0,
                holdSetup: {
                    Id: '',
                    holdRecordType: '',
                    isActive: true,
                },
                customerId: '',
                deleteMonths: '',

                request: 0,

                saleHoldType: '',

                historyList: [],

                series: [],
                chartOptions: {
                    labels: ['Hold', 'Cash', 'Credit']
                },
                series2: [],
                chartOptions2: {
                    labels: ['UnPaid ', 'Partially ', 'FullyPaid ']
                },
                series3: [],
                chartOptions3: {
                    labels: ['Hold', 'Cash', 'Credit']
                },
                series4: [],
                chartOptions4: {
                    labels: ['Un-Paid ', 'Partially ', 'Fully-Paid ']
                },

                seriesOfCustomerInvoice: [{
                    name: 'Cash',
                    data: [31, 40, 28, 51, 42]
                }, {
                    name: 'Credit',
                    data: [11, 32, 45, 32, 34]
                }],
                chartOptionsOfCustomerInvoice: {
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
                        categories: ["Walk-in", "Ahmed", "Usaama", "Zafar"]
                    },

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
                    hold: 0,
                    totalHold: 0.0,
                    unPaid: 0,
                    totalUnPaid: 0.0,
                    fullyPaid: 0,
                    totalFullyPaid: 0.0,
                    partially: 0,
                    totalPartially: 0.0,
                    credit: 0,
                    totalCredit: 0.0,
                    cash: 0,
                    totalCash: 0.0
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
                monthObj: '',
                year: 0,
                randerforempty: 0,
                month: 0,
                isDisable: false,
                isDisableMonth: false,
                changereport: 0,
                hold: 0,
                credit: 0,
                cash: 0,
                partially: 0,
                unPaid: 0,
                randerChart: 0,
                fullyPaid: 0,
                reportsrc: '',
                show: false,

                deliveryChallanRecord: [],
                ReservedDeliveryChallanbool: false,
                expandDeliveryChallan: false,
                expandDeliveryChallan1: false,
                expandDeliveryChallan2: false,
                SaleInvoiceId: '',
                isReservedId: '',
                qt: false,
                isService: false,
                code: '',
                canvasDate: '',
                canvasSaleOrderId: '',
                qutationId: '',
                deliveryChallanId: '',
                vAT: '',
                canvasTaxMethod: '',
                quotationNo: '',
                saleOrderNo: '',
                invoiceNote: '',
                deliveryNo: '',
                discountType: false,
                isAddChallan: false,
                isCloseChallan: false,
                isAdd: false,
                saleId: '',
                saleIdToCanvas: '',
                isDiscountBeforeVat: false,
                isPrint: false,
                download: false,
                emailComposeShow: false,
                bankDetail: false,
                printSize: '',
                printTemplate: '',
                filePath: null,
                fromDate: '',
                toDate: '',
                user: '',
                fromTime: '',
                toTime: '',
                userId: '',
                terminalId: '',
                active: 'Hold',
                isDisabled: false,
                colorVariants: false,
                advanceFilters: false,
                printInterval: '',
                search: '',
                english: '',
                arabic: '',
                searchQuery: '',
                saleList: [],
                saleItem: [],
                purchasePostList: [],
                deliveryChallanList: [],
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                currency: '',

                selected: [],
                selectAll: false,
                updateApprovalStatus: {
                    id: '',
                    approvalStatus: ''
                },

                printDetails: [],
                printDetailsPos: [],
                printRender: 0,
                printRenderPos: 0,
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                companyId: '00000000-0000-0000-0000-000000000000',
                CompanyID: '',
                UserID: '',
                employeeId: '',
                isDayAlreadyStart: false,
                IsProduction: false,
                AllowAll: false,
                sale: false,
                isDayStarts: '',
                rendr: 0,
                counter: 0,
                terminalType: 'CashCounter',
                allShow: true,
                isloginhistory: true,
                overWrite: false,
                businessLogo: '',
                businessNameArabic: '',
                businessNameEnglish: '',
                businessTypeArabic: '',
                businessTypeEnglish: '',
                companyNameArabic: '',
                companyNameEnglish: '',

                customerInformation: '',
                expandSale: false,
                isFifo: false,
                openBatch: 0,

                grossAmount: 0,
                discountAmount: 0,
                vatAmount: 0,
                afterDiscountAmount: 0,
                totalAmount: 0,
                proformaId: '',
                proformaNo: '',
            }
        },
        computed: {
            isFilterButtonDisabled() {
                return (
                    this.customerId ||
                    this.monthObj ||
                    this.fromDate ||
                    this.toDate ||
                    this.terminalId ||
                    this.user || this.customerType
                );
            },
        },
        watch: {
            documentName: function () {

                if (this.documentName == 'Document') {

                    this.makeActive('Hold');
                }

            },



        },

        methods: {
            // sendEmail(id)
            // {
            //     var root = this;
            //     var token = '';

            //     if (token == '') 
            //     {
            //         token = localStorage.getItem('token');
            //     }
            //     this.loading = true;
            //     root.$https.get('Sale/SendSaleEmail?id=' + id , { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
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
            searchData: function () {
                this.getData(this.search, 1, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId, this.customerId, this.customerType);
            },

            DocumentHistory: function (ModelOn) {

                this.expandHistory = ModelOn;

                var root = this;
                var token = '';
                if (ModelOn) {
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    var documentName = 'Sales';

                    root.$https.get('/Sale/DocumentHistory?documentName=' + documentName + '&Id=' + this.saleIdToCanvas + '&currentDocument=' + 'Sales', {
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

            GetSaleHoldTypes: function () {

                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId, this.customerId, this.customerType)
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
                

                if (this.monthObj != undefined && this.monthObj != null && this.monthObj != '') {
                    this.isDisable = true;
                    this.fromDate = '';
                    this.toDate = '';

                    this.month = moment(this.monthObj).format("MM");
                    this.year = moment(this.monthObj).format("YYYY");
                }
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

                    if (this.monthObj != undefined && this.monthObj!='' ) {
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

                    this.year = 0;
                    this.fromDate = '';
                    this.toDate = '';
                    this.month = 0;
                    this.monthObj = '';
                    this.terminalId = '';
                    this.customerId = '';
                    this.customerType = '';
                    this.search = "";
                    this.randerforempty++;

                    if (this.$refs.CustomerDropdown != undefined)
                        this.$refs.CustomerDropdown.Remove();

                    if (this.$refs.TerminalDropDown != undefined)
                        this.$refs.TerminalDropDown.Remove();

                }

                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId, this.customerId, this.customerType);

            },
            search22: function () {
                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId, this.customerId, this.customerType);
            },

            clearData: function () {
                this.search = "";
                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId, this.customerId, this.customerType);

            },

            ReceiptGenerationandViewing: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/PaymentVoucher/GetSaleToVoucher?Id=' + id + '&formName=SaleInvoice', {
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
                                    formName: 'BankReceipt'
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

            SaleIdForCanvas: function (deliveryChallanId, saleOrderId, quotationId, proformaId, registrationNumber, date, saleId) {

                var root = this;
                var token = '';
                root.saleIdToCanvas = saleId;
                this.expandHistory = false;
                this.randerExpand++;
                if (deliveryChallanId != null) {
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    root.$https.get('/Purchase/DeliveryChallanDetail?id=' + deliveryChallanId + '&simpleQuery=' + true, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                root.registrationNo = registrationNumber + ' - ' + root.getDate(date);
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
                }
                else if (saleOrderId != null) {
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
                                root.proformaNo = null,
                                    root.saleOrderNo = response.data.registrationNo,
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
                }
                else if (quotationId != null) {
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
                                root.saleOrderNo = null,
                                    root.proformaNo = null,
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
                }
                else if (proformaId != null) {
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
            ViewCustomerInfo: function (id, cashCustomerId) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var cashCustomer = false;
                if (id == null) {
                    cashCustomer = true;
                    id = cashCustomerId;
                }
                root.$https.get('/Contact/ContactLedgerDetail?id=' + id + '&documentType=' + "SaleInvoice" + '&cashCustomer=' + cashCustomer + '&isService=' + this.isService + '&lastThreeMonth=' + true, {
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

            GetRecordOfDelivery: function () {

                this.ReservedDeliveryChallanbool = false;
                this.ViewDeliveryChallan(this.SaleInvoiceId);

            },
            ReservedDeliveryChallan: function (id, fromSale, close) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (fromSale) {
                    root.$https.get('/Sale/SaleDetail?id=' + id + '&isSale=' + true + '&DeliveryChallan=' + true, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                root.deliveryChallanRecord = response.data;
                                root.isAdd = true;
                                root.ReservedDeliveryChallanbool = true;

                                root.$router.push({
                                    query: {
                                        data: undefined,

                                    }
                                })

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
                    root.$https.get('/Purchase/DeliveryChallanDetail?id=' + id + '&isSale=' + true + '&isReserved=' + true + '&manualClose=' + manualClose, {
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
            GetUserId: function (x) {
                this.userId = x.id;
            },
            getDate: function (date) {
                return moment(date).format('LLL');
            },
            PrinterInterval: function () {
                var root = this;

                this.printInterval = setInterval(() => {
                    root.isDisabled = false;
                }, 3000);
            },
            ClearIntervalStatus: function () {
                clearInterval(this.printInterval)
            },

            EditSale: function (id, isEditPaidInvoice) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/SaleDetail?Id=' + id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addSaleService',
                                query: {
                                    data: '',
                                    Add: false,
                                    page: root.currentPage,
                                    isEditPaidInvoice: isEditPaidInvoice,
                                    formName: 'SaleInvoice',
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
                    },
                        function (error) {
                            console.log(error);
                        });
            },
            EditSaleTouch: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/SaleDetail?Id=' + id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/TouchScreen',
                                query: {
                                    data: '',
                                    Add: false,
                                }
                            })
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },

            IsSave: function () {
                this.show = false;
            },
            PrintRdlc: function (id, isDownload) {
                var root = this;
                var companyId = '';
                companyId = localStorage.getItem('CompanyID');
                if (isDownload) {
                    this.loading = true;
                    this.$https.get(this.$ReportServer + '/SalesModuleReports/SaleInvoice/SaleReport.aspx?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + this.colorVariants + '&CompanyId=' + companyId + '&formName=SaleRecored' + "&isQuotation=" + true + "&IsDayStart=" + this.isDayStart + '&isDownload=' + isDownload
                        , { responseType: 'blob' }).then(function (response) {
                            root.loading = false;

                            const url = window.URL.createObjectURL(new Blob([response.data]));
                            const link = document.createElement('a');
                            link.href = url;
                            var date = moment().format('DD MMM YYYY');
                            link.setAttribute('download', 'SaleRecored ' + date + '.pdf');
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


                    this.reportsrc = this.$ReportServer + '/SalesModuleReports/SaleInvoice/SaleReport.aspx?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + this.colorVariants + '&CompanyId=' + companyId + '&formName=SaleRecored' + "&isQuotation=" + true + "&IsDayStart=" + this.isDayStart + '&isDownload=' + isDownload
                    this.changereport++;
                }


            },

            DuplicateInvoice: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/SaleDetail?Id=' + id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {
                            response.data.isDuplicate = true;
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addSaleService',
                                query: {
                                    data: '',
                                    Add: false,
                                    isDuplicate: 'true',
                                    formName: 'SaleInvoice',
                                }
                            })
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },
            ViewInvoice: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/SaleDetail?Id=' + id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/InvoiceServiceView',
                                query: {
                                    data: '',
                                    Add: false,
                                    page: root.currentPage,
                                    active: root.active
                                }
                            })
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },
            goToPaymentDetail: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/SaleDetail?Id=' + id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/SalePaymentDetail',
                                query: {
                                    data: '',
                                    Add: false
                                }
                            })
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },

            makeActive: function (item) {

                this.active = item;
                this.selectAll = false;
                this.selected = [];
                this.getData(this.search, 1, item, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId, this.customerId, this.customerType);
            },
            getPage: function () {

                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId, this.customerId, this.customerType);
            },
            GetSaleDashboardRecord: function () {

                if (this.request == 0) {
                    var root = this;
                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }

                    var branchId = localStorage.getItem('BranchId');

                    this.loading = true;
                    this.$https.get('/Sale/SaleDashboardList?isService=' + this.isService + '&branchId=' + branchId, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {

                            if (response.data != null) {

                                root.series = [];
                                root.series.push(response.data.hold);
                                root.series.push(response.data.cash);
                                root.series.push(response.data.credit);
                                root.series2 = [];
                                root.series2.push(response.data.unPaid);
                                root.series2.push(response.data.partially);
                                root.series2.push(response.data.fullyPaid);
                                root.series3 = [];
                                root.series3.push(response.data.totalHold);
                                root.series3.push(response.data.totalCash);
                                root.series3.push(response.data.totalCredit);
                                root.series4 = [];
                                root.series4.push(response.data.totalUnPaid);
                                root.series4.push(response.data.totalPartially);
                                root.series4.push(response.data.totalFullyPaid);
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
                                    root.chartOptionsPurchase.xaxis.categories.push(result.name);
                                });

                                root.hold = response.data.hold;
                                root.credit = response.data.credit;
                                root.cash = response.data.cash;
                                root.partially = response.data.partially;
                                root.unPaid = response.data.unPaid;
                                root.fullyPaid = response.data.fullyPaid;
                                response.data.paidListByAmount.forEach(function (result) {
                                    root.earningSeries[0].data.push((parseFloat(result.amount)).toFixed(0));
                                    root.earningChartOption.xaxis.categories.push(result.name == null ? '' : result.name);
                                });
                                response.data.paidList.forEach(function (result) {
                                    root.earningSeriesCash[0].data.push((parseFloat(result.amount)).toFixed(0));
                                    root.earningChartOptionCash.xaxis.categories.push(result.name == null ? '' : result.name);
                                });
                                response.data.creditListByAmount.forEach(function (result) {

                                    root.seriesOfCustomer[0].data.push((parseFloat(result.amount)).toFixed(0));
                                    root.chartOptionsOfCustomer.xaxis.categories.push(result.name == null ? '' : result.name);
                                });

                                root.randerChart++;

                                root.saleListModel = {
                                    hold: response.data.hold,
                                    totalHold: response.data.totalHold,
                                    unPaid: response.data.unPaid,
                                    totalUnPaid: response.data.totalUnPaid,
                                    fullyPaid: response.data.fullyPaid,
                                    totalFullyPaid: response.data.totalFullyPaid,
                                    partially: response.data.partially,
                                    totalPartially: response.data.totalPartially,
                                    credit: response.data.credit,
                                    totalCredit: response.data.totalCredit,
                                    cash: response.data.cash,
                                    totalCash: response.data.totalCash
                                };
                                root.loading = false;

                            }
                        });

                }
                this.request++;

            },
            getData: function (search, currentPage, status, fromDate, toDate, fromTime, toTime, terminalId, userId, customerId, customerType) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var branchId = localStorage.getItem('BranchId');

                localStorage.setItem('currentPage', this.currentPage);
                localStorage.setItem('active', this.active);
                var isQuotation = status == 'Quotation' ? true : false;

                if (status == 'SaleOrder' || status == 'Quotation') {
                    this.$https.get('/Purchase/SaleServiceOrderList?status=' + 'Approved' + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&isQuotation=' + isQuotation + '&isService=' + this.isService + '&branchId=' + branchId, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {

                            root.saleList = response.data.results;
                            root.pageCount = response.data.pageCount;
                            root.rowCount = response.data.rowCount;
                            root.currentPage = response.data.currentPage;

                        });
                } else {

                    this.$https.get('/Sale/SaleInvoiceList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&saleHoldType=' + this.saleHoldType + '&fromDate=' + fromDate + '&toDate=' + toDate + '&fromTime=' + fromTime + '&toTime=' + toTime + '&terminalId=' + terminalId + '&userId=' + userId + '&isDiscountBeforeVat=' + this.isDiscountBeforeVat + '&isService=' + this.isService + '&month=' + this.month + '&year=' + this.year + '&CustomerId=' + customerId + '&customerType=' + customerType + '&branchId=' + branchId, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {

                            if (response.data != null) {
                                root.currentPage = response.data.currentPage;
                                root.pageCount = response.data.pageCount;
                                root.rowCount = response.data.rowCount;
                                root.saleList = response.data.results.sales;
                                root.currentPage = currentPage;
                                root.rendr++;
                            }
                        }).catch(error => {
                            root.$swal.fire({
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        });
                }

            },

            ViewSaleOrder: function (id) {
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

            ViewQuotationInvoice: function (id) {
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
                                path: '/ServiceQuotationView',
                                query: {
                                    data: '',
                                    Add: false,
                                }
                            })
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            },

            ViewDeliveryChallan: function (id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var branchId = localStorage.getItem('BranchId');

                this.SaleInvoiceId = id;
                root.$https.get('/Purchase/DeliveryChallanList?documentId=' + id + '&isSale=' + true + '&openBatch=' + this.openBatch + '&IsDropDown=' + true + '&branchId=' + branchId, {
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

                root.$https.get('/Sale/SaleDetail?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&colorVariants=' + this.colorVariants + '&DeliveryChallan=' + true, {
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
                                    page: root.currentPage,
                                    Add: false,
                                    isService: root.isService,
                                    isSaleOrder: false,
                                }
                            })
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },

            PrintDeliveryChallan: function (id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Purchase/DeliveryChallanDetail?id=' + id + '&isSale=' + true, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                    .then(function (response) {

                        if (response.data != null) {

                            root.printDetails = response.data;
                            root.printRender++;
                            root.isDevliveryChallan = true;
                        }
                        alert(response.data);
                    });

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

                root.$https.get('/Purchase/DeliveryChallanDetail?id=' + id + '&isSale=' + true, {
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
                                    isSaleOrder: false,
                                    isService: root.isService,
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
            DeleteSalePermanently: function (id) {
                var root = this;
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
                        root.$https.get('/Sale/DeleteSalePermanently?id=' + id, {
                            headers: {
                                "Authorization": `Bearer ${token}`
                            }
                        })
                            .then(function (response) {
                                if (response.data.isAddUpdate != "") {
                                    root.getPage();
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
                                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Delete UnSuccessfully' : 'حذف غير ناجح',
                                        type: 'error',
                                        confirmButtonClass: "btn btn-danger",
                                        buttonsStyling: false
                                    });
                                });
                    } else {
                        this.$swal('Cancelled', 'Your invoice is still intact', 'info');
                    }
                });

            },
            RemoveSale: function (id, val) {
                var root = this;
                if (val) {

                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    root.$https.get('/Sale/DeleteSale?Id=' + id + '&isNormal=' + val, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data.message.id != '00000000-0000-0000-0000-000000000000') {

                                root.saleList.splice(root.saleList.findIndex(function (i) {
                                    return i.id === response.data.message.id;
                                }), 1);
                                root.$swal({
                                    title: 'Updated',
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
                            root.$https.get('/Sale/DeleteSale?Id=' + id + '&isNormal=' + val, {
                                headers: {
                                    "Authorization": `Bearer ${token}`
                                }
                            })
                                .then(function (response) {
                                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000') {

                                        root.saleList.splice(root.saleList.findIndex(function (i) {
                                            return i.id === response.data.message.id;
                                        }), 1);
                                        root.$swal({
                                            title: 'Updated',
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
                            this.$swal('Cancelled', 'Your invoice is still intact', 'info');
                        }
                    });
                }
            },
            AddSale: function () {
                this.$router.push('/addSaleService');
            },
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';

                if (path != null && path != '' && path != undefined) {
                    root.$https.get('/Contact/GetBaseImage?filePath=' + path, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        }

                    });
                }

            },
            ViewInvoiceTemplate: function (value) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.printDetailsPos = [];
                root.$https.get("/Sale/SaleDetail?id=" + value, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$router.push({

                                path: '/SaleServiceView',
                                query: {
                                    printDetails: response.data,
                                    headerFooter: root.headerFooter,
                                }
                            })

                        }
                    });
            },
            PrintInvoice: function (value, prop) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.printDetailsPos = [];
                root.$https.get("/Sale/SaleDetail?id=" + value, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    },
                })
                    .then(function (response) {
                        if (response.data != null) {

                            root.printDetails = response.data;

                            if (prop == 'download') {
                                root.download = true;
                                root.isPrint = false;
                            } else {
                                root.download = false;
                                root.isPrint = true;
                                root.printRender++;
                            }
                        }
                    });
            },
            GetInvoicePrintSetting: function () {
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var branchId = localStorage.getItem('BranchId');


                root.$https.get('/Sale/printSettingDetail?branchId=' + branchId, {
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
                            root.headerFooter.exchangeDays = response.data.exchangeDays;
                            root.headerFooter.printOptions = response.data.printOptions;
                            root.headerFooter.invoicePrint = response.data.invoicePrint;
                            root.headerFooter.welcomeLineEn = response.data.welcomeLineEn;
                            root.headerFooter.welcomeLineAr = response.data.welcomeLineAr;
                            root.headerFooter.closingLineEn = response.data.closingLineEn;
                            root.headerFooter.closingLineAr = response.data.closingLineAr;
                            root.headerFooter.contactNo = response.data.contactNo;
                            root.headerFooter.managementNo = response.data.managementNo;

                            root.headerFooter.businessAddressArabic = response.data.businessAddressArabic;
                            root.headerFooter.businessAddressEnglish = response.data.businessAddressEnglish;
                            root.headerFooter.headerImage = response.data.headerImageForPrint;
                            root.headerFooter.footerImage = response.data.footerImageForPrint;

                            root.headerFooter.withSubTotal = response.data.withSubTotal;
                            root.headerFooter.continueWithPage = response.data.continueWithPage;

                        }

                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
            PrintInvoicePos: function (value, counterName) {

                var root = this;
                var token = '';
                this.isDisabled = true;
                this.PrinterInterval();
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.printDetails = [];
                root.$https.get("/Sale/SaleDetail?id=" + value, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.printDetailsPos = response.data;
                            root.printDetailsPos.counterName = counterName;
                            root.printRenderPos++;
                        }
                    });
            }
        },
        created: function () {
            this.documentName = this.$route.query.formName;

            // this.GetHeaderDetail();

            if (this.$route.query.data == 'addSaleService') {
                this.$emit('update:modelValue', 'addSaleService');

            } else {
                this.$emit('update:modelValue', this.$route.name);

            }
            this.isService = localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true;

            this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;

        },
        mounted: function () {
            this.isService = localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true;

            this.overWrite = localStorage.getItem('overWrite') == 'true' ? true : false;
            this.isDiscountBeforeVat = localStorage.getItem('DiscountBeforeVat') == 'true' ? true : false
            this.businessLogo = localStorage.getItem('BusinessLogo');
            this.businessNameArabic = localStorage.getItem('BusinessNameArabic');
            this.businessNameEnglish = localStorage.getItem('BusinessNameEnglish');
            this.businessTypeArabic = localStorage.getItem('BusinessTypeArabic');
            this.businessTypeEnglish = localStorage.getItem('BusinessTypeEnglish');
            this.companyNameArabic = localStorage.getItem('CompanyNameArabic');
            this.companyNameEnglish = localStorage.getItem('CompanyNameEnglish');

            this.bankDetail = localStorage.getItem('BankDetail') == 'true' ? true : false;

            var IsDayStart = localStorage.getItem('DayStart');
            this.isDayStarts = localStorage.getItem('DayStart');
            var IsDayStartOn = localStorage.getItem('IsDayStart');
            this.printTemplate = localStorage.getItem('PrintTemplate');
            this.printSize = localStorage.getItem('PrintSizeA4');

            var AllowAll = localStorage.getItem('AllowAll');
            if (IsDayStart != 'true') {
                this.isDayAlreadyStart = true;
                this.english = localStorage.getItem('English');
                this.arabic = localStorage.getItem('Arabic');
                this.search = '';
                if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                    this.currentPage = parseInt(localStorage.getItem('currentPage'));
                    this.active = (localStorage.getItem('active'));
                    if (this.active == 'Draft' || this.active == 'post' || this.active == 'Approved' || this.active == 'Void' || this.active == 'Rejected') {
                        this.active = 'Hold';

                    }
                    this.getPage();

                } else {
                    if (this.isValid('CanViewHoldInvoices')) {
                        this.active = 'Hold';
                    } else if (this.isValid('CanViewPaidInvoices')) {
                        this.active = 'Paid';
                    } else if (this.isValid('CanViewCreditInvoices')) {
                        this.active = 'Credit';
                    }

                    this.getPage();

                }
                this.currency = localStorage.getItem('currency');
                this.companyId = localStorage.getItem('CompanyID');
            } else {
                if (AllowAll == 'true') {
                    this.isDayAlreadyStart = true;
                    this.english = localStorage.getItem('English');
                    this.arabic = localStorage.getItem('Arabic');
                    this.search = '';
                    if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                        this.currentPage = parseInt(localStorage.getItem('currentPage'));
                        this.active = (localStorage.getItem('active'));
                        if (this.active == 'Draft') {
                            this.active = 'Hold';

                        }
                        this.getPage();

                    } else {
                        if (this.isValid('CanViewHoldInvoices')) {
                            this.active = 'Hold';
                        } else if (this.isValid('CanViewPaidInvoices')) {
                            this.active = 'Paid';
                        } else if (this.isValid('CanViewCreditInvoices')) {
                            this.active = 'Credit';
                        }

                        this.getPage();

                    }
                    this.currency = localStorage.getItem('currency');
                    this.companyId = localStorage.getItem('CompanyID');
                } else if (IsDayStartOn == 'true') {
                    this.isDayAlreadyStart = true;
                    this.english = localStorage.getItem('English');
                    this.arabic = localStorage.getItem('Arabic');
                    this.search = '';
                    if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                        if (localStorage.getItem('currentPage') != null && localStorage.getItem('currentPage') != '' && localStorage.getItem('currentPage') != undefined) {
                            this.currentPage = parseInt(localStorage.getItem('currentPage'));

                        } else {
                            this.currentPage = 1;
                        }
                        this.active = (localStorage.getItem('active'));
                        if (this.active == 'Draft') {
                            this.active = 'Hold';

                        }
                        if (this.active == 'Approved') {
                            this.active = 'Hold';

                        }
                        this.getPage();

                    } else {
                        if (this.isValid('CanViewHoldInvoices')) {
                            this.active = 'Hold';
                        } else if (this.isValid('CanViewPaidInvoices')) {
                            this.active = 'Paid';
                        } else if (this.isValid('CanViewCreditInvoices')) {
                            this.active = 'Credit';
                        }
                        this.getPage();

                    }
                    this.currency = localStorage.getItem('currency');
                    this.companyId = localStorage.getItem('CompanyID');
                } else {

                    this.CompanyID = localStorage.getItem('CompanyID');
                    this.UserID = localStorage.getItem('UserID');
                    this.employeeId = localStorage.getItem('EmployeeId');
                    this.isDayAlreadyStart = false;
                }
            }
        },
    }
</script>

<style scoped>
    .vue__time-picker input.display-time {
        height: 40px !important;
        background-color: #f2f6f9;
        border: 1px solid #f2f6f9;
    }


    .timeline:nth-child(2n) .year {
        right: auto !important;
        left: 26% !important;
    }

    .year {
        right: 20% !important;
    }
</style>
