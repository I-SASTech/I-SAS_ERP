<template>
    <div class="row" v-if="isValid('CanViewProforma') || isValid('ViewCustomerPO')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row" v-if="formName == 'ServiceProformaInvoice'">
                            <div class="col">
                                <h5 class="page_title">
                                    {{ $t("ProformaInvoices.ServiceProformaInvoice") }}
                                </h5>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item">
                                        <a href="javascript:void(0);">
                                            {{ $t("ProformaInvoices.Home") }}
                                        </a>
                                    </li>
                                    <li class="breadcrumb-item active">
                                        {{ $t("ProformaInvoices.ServiceProformaInvoice") }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">
                                <a href="javascript:void(0)" class="btn btn-sm btn-outline-primary mx-1"
                                    v-on:click="AddSaleReturn" v-if="isValid('CanAddProforma')">
                                    <i class="fa fa-plus"></i>
                                    {{ $t("SaleReturn.AddNew") }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t("Sale.Close") }}
                                </a>
                            </div>
                        </div>
                        <div class="row" v-if="formName == 'ProformaInvoice'">
                            <div class="col">
                                <h5 class="page_title">
                                    {{ $t("ProformaInvoices.ProformaInvoices") }}
                                </h5>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item">
                                        <a href="javascript:void(0);">
                                            {{ $t("ProformaInvoices.Home") }}
                                        </a>
                                    </li>
                                    <li class="breadcrumb-item active">
                                        {{ $t("ProformaInvoices.ProformaInvoices") }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">
                                <a href="javascript:void(0)" class="btn btn-sm btn-outline-primary mx-1"
                                    v-on:click="AddSaleReturn" v-if="isValid('CanAddProforma')">
                                    <i class="fa fa-plus"></i>
                                    {{ $t("SaleReturn.AddNew") }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t("Sale.Close") }}
                                </a>
                            </div>
                        </div>
                        <div class="row" v-if="formName == 'PurchaseOrder'">
                            <div class="col">
                                <h5 class="page_title">
                                    {{ $t("Dashboard.CustomerPurchaseOrder") }}
                                </h5>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item">
                                        <a href="javascript:void(0);">
                                            {{ $t("ProformaInvoices.Home") }}
                                        </a>
                                    </li>
                                    <li class="breadcrumb-item active">
                                        {{ $t("Dashboard.CustomerPurchaseOrder") }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">
                                <a href="javascript:void(0)" class="btn btn-sm btn-outline-primary mx-1"
                                    v-on:click="AddSaleReturn" v-if="isValid('CanAddProforma')">
                                    <i class="fa fa-plus"></i>
                                    {{ $t("SaleReturn.AddNew") }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t("Sale.Close") }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" v-bind:key="RanderAll">
                <div class="accordion" id="accordionExample">
                    <div class="accordion-item">
                        <h5 class="accordion-header m-0" id="headingTwo">
                            <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse"
                                data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo"
                                v-on:click="GetSaleDashboardRecord">
                                KPIs Dashboard
                            </button>
                        </h5>
                        <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo"
                            data-bs-parent="#accordionExample">
                            <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>

                            <div class="accordion-body">
                                <div class="row">
                                    <div class="col-md-6 col-lg-6 col-12">
                                        <div class="card report-card">
                                            <div class="card-body">
                                                <div class="row d-flex justify-content-center">
                                                    <div class="col">
                                                        <p class="text-dark mb-0 fw-semibold"
                                                            v-if="formName == 'PurchaseOrder'">
                                                            {{ $t("ProformaInvoices.CustomerPO") }}
                                                        </p>
                                                        <p class="text-dark mb-0 fw-semibold" v-else>
                                                            {{ $t("ProformaInvoices.ProformaInvoice") }}
                                                        </p>
                                                        <h3 class="m-0">{{ credit }}</h3>
                                                    </div>
                                                    <div class="col-auto align-self-center">
                                                        <div class="report-main-icon bg-light-alt">
                                                            <i data-feather="tag"
                                                                class="align-self-center text-muted icon-sm"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end col-->
                                    <div class="col-md-6 col-lg-6 col-12">
                                        <div class="card report-card">
                                            <div class="card-body">
                                                <div class="row d-flex justify-content-center">
                                                    <div class="col">
                                                        <p class="text-dark mb-0 fw-semibold"
                                                            v-if="formName == 'PurchaseOrder'">
                                                            {{ $t("ProformaInvoices.CustomerPO(Amount)") }}
                                                        </p>
                                                        <p class="text-dark mb-0 fw-semibold" v-else>
                                                            {{
                                                                $t("ProformaInvoices.ProformaInvoice(Amount)")
                                                            }}
                                                        </p>
                                                        <h3 class="m-0">
                                                            {{ currency }}
                                                            {{
                                                                parseFloat(saleListModel.totalCredit)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </h3>
                                                    </div>
                                                    <div class="col-auto align-self-center">
                                                        <div class="report-main-icon bg-light-alt">
                                                            <i data-feather="zap"
                                                                class="align-self-center text-muted icon-sm"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end col-->
                                </div>
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <div class="card">
                                            <div class="card-header">
                                                <div class="row align-items-center">
                                                    <div class="col">
                                                        <h4 class="card-title">
                                                            {{
                                                                $t("ProformaInvoices.TrendingCustomerByAmount")
                                                            }}
                                                        </h4>
                                                    </div>
                                                    <!--end col-->
                                                </div>
                                                <!--end row-->
                                            </div>
                                            <!--end card-header-->
                                            <div class="card-body">
                                                <div class="">
                                                    <apexchart type="line" v-bind:key="randerChart" height="350"
                                                        :options="chartOptionsOfCustomer" :series="seriesOfCustomer">
                                                    </apexchart>
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
                                                        <h4 class="card-title">
                                                            {{ $t("ProformaInvoices.TrendingCustomer") }}
                                                        </h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="">
                                                    <apexchart type="area" height="350" v-bind:key="randerChart"
                                                        :options="chartOptionsPurchase" :series="seriesPurchase">
                                                    </apexchart>
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
            <div class="card mt-3">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-8" style="padding-top: 20px">
                            <div class="input-group">
                                <button class="btn btn-soft-primary" type="button" id="button-addon1"
                                    v-on:click="search22(true)">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('Sale.SearchOfInvoice')" aria-label="Example text with button addon"
                                    aria-describedby="button-addon1" />

                                <a class="btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2"
                                    value="Advance Filter">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

                            <button v-on:click="search22(true)" :disabled="!search" type="button"
                                class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled="!search" type="button"
                                class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t("Sale.ClearFilter") }}
                            </button>
                        </div>
                    </div>

                    <div class="row" v-if="advanceFilters">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-4">
                            <label class="text font-weight-bolder">
                                {{
                                    $t("Sale.Customer")
                                }}
                            </label>
                            <customerdropdown v-model="customerId" ref="CustomerDropdown" />
                        </div>
                        <div class="col-xs-12 col-lg-2">
                            <div class="form-group">
                                <label>{{ $t("Sale.Month") }}</label>
                                <monthpicker v-model="monthObj" @update:modelValue="GetMonth()" @select="GetMonth()"
                                    v-bind:disabled="isDisableMonth" v-if="!isDisableMonth" v-bind:key="randerforempty" />

                                <input class="form-control" v-else disabled />
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t("Sale.FromDate") }}</label>
                                <datepicker v-model="fromDate" v-bind:isDisable="isDisable" @update:modelValue="GetDate1"
                                    v-bind:key="randerforempty" />
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t("Sale.ToDate") }}</label>
                                <datepicker v-model="toDate" v-bind:isDisable="isDisable" @update:modelValue="GetDate1"
                                    v-bind:key="randerforempty" />
                            </div>
                        </div>

                        <div class="col-xs-12  col-lg-3 "
                            v-if="isValid('CanViewTerminal') || isValid('MachineWisePrefix') || isValid('Terminal') || isValid('CanStartDay') || isValid('TouchInvoiceTemplate1')">
                            <label class="text  font-weight-bolder"> {{ $t('Sale.Counter') }}:</label>
                            <terminal-dropdown v-model="terminalId" :terminalType="terminalType"
                                :terminalUserType="'Offline'" :isSelect="true" ref="terminalDropdown" />

                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">
                            <label class="text font-weight-bolder">
                                {{ $t("Sale.User1") }}:
                            </label>
                            <usersDropdown v-model="user" ref="userDropdown" :isloginhistory="isloginhistory" />
                        </div>

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">

                            <button v-on:click="FilterRecord(true)" :disabled="!isFilterButtonDisabled" type="button"
                                class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="FilterRecord(false)" :disabled="!isFilterButtonDisabled" type="button"
                                class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t("Sale.ClearFilter") }}
                            </button>
                        </div>
                    </div>
                </div>

                <div class="card-body">
                    <!-- Tab panes -->
                    <div class="row">
                        <div class="col-12">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th>
                                                {{ $t("Sale.InvoiceNo") }}
                                            </th>
                                            <th>
                                                {{ $t("Sale.Date") }}
                                            </th>
                                            <th>
                                                {{ $t("Sale.CustomerName") }}
                                            </th>
                                            <th>
                                                {{ $t("Sale.CreatedBy") }}
                                            </th>
                                            <th>
                                                {{ $t("InvoiceNote") }}
                                            </th>
                                            <th class="text-center">
                                                {{ $t("SaleOrder.Status") }}
                                            </th>
                                            <th>
                                                {{ $t("Sale.NetAmount") }}
                                            </th>
                                            <th class="text-end"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(sale, index) in saleList" v-bind:key="index">
                                            <td v-if="currentPage === 1">
                                                {{ index + 1 }}
                                            </td>
                                            <td v-else>
                                                {{ currentPage * 10 - 10 + (index + 1) }}
                                            </td>
                                            <td v-if="isValid('CanEditProforma')">
                                                <strong>
                                                    {{ sale.registrationNumber }}
                                                </strong>
                                                <br />
                                                <div v-if="isValid('PremiumARAdvancePayment')">
                                                    <div class="badge bg-danger" v-if="sale.partiallyInvoices == 1">
                                                        {{ ($i18n.locale == 'en' || isLeftToRight()) ? 'Un-Paid' : 'غي مدفوعة' }}
                                                    </div>
                                                    <div class="badge bg-primary" v-if="sale.partiallyInvoices == 2">
                                                        {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Partially  Paid':'المدفوعة جزئيا'}}
                                                    </div>
                                                    <div class="badge bg-success" v-if="sale.partiallyInvoices == 3">
                                                        {{
                                                            ($i18n.locale == 'en' || isLeftToRight()) ? ' Fully Paid' :
                                                            'مدفوع بالكامل'
                                                        }}
                                                    </div>
                                                </div>
                                            </td>
                                            <td v-else-if="formName == 'PurchaseOrder' &&
                                                    sale.editPurchaseOrderId == null &&
                                                    !sale.isClose
                                                    ">
                                                <a href="javascript:void(0)" v-on:click="EditSale(sale.id)">
                                                    <strong>
                                                        {{ sale.registrationNumber }}
                                                    </strong>
                                                    <br />
                                                    <div v-if="isValid('PremiumARAdvancePayment')">
                                                        <div class="badge bg-danger" v-if="sale.partiallyInvoices == 1">
                                                            {{
                                                                $i18n.locale == "en" || isLeftToRight()
                                                                ? "Un-Paid"
                                                                : "غي مدفوعة"
                                                            }}
                                                        </div>
                                                        <div class="badge bg-primary" v-if="sale.partiallyInvoices == 2">
                                                            {{
                                                                $i18n.locale == "en" || isLeftToRight()
                                                                ? " Partially Paid"
                                                                : "المدفوعة جزئيا"
                                                            }}
                                                        </div>
                                                        <div class="badge bg-success" v-if="sale.partiallyInvoices == 3">
                                                            {{
                                                                $i18n.locale == "en" || isLeftToRight()
                                                                ? " Fully Paid"
                                                                : "مدفوع بالكامل"
                                                            }}
                                                        </div>
                                                    </div>
                                                </a>
                                            </td>
                                            <td v-else-if="(formName == 'ServiceProformaInvoice' ||
                                                        formName == 'ProformaInvoice') &&
                                                    sale.editProformaId == null &&
                                                    !sale.isClose
                                                    ">
                                                <a href="javascript:void(0)" v-on:click="EditSale(sale.id)">
                                                    <strong>
                                                        {{ sale.registrationNumber }}
                                                    </strong>
                                                    <br />
                                                    <div v-if="isValid('PremiumARAdvancePayment')">
                                                        <div class="badge bg-danger" v-if="sale.partiallyInvoices == 1">
                                                            {{
                                                                $i18n.locale == "en" || isLeftToRight()
                                                                ? "Un-Paid"
                                                                : "غيرمدفوعة"
                                                            }}
                                                        </div>
                                                        <div class="badge bg-primary" v-if="sale.partiallyInvoices == 2">
                                                            {{
                                                                $i18n.locale == "en" || isLeftToRight()
                                                                ? " Partially Paid"
                                                                : "المدفوعة جزئيا"
                                                            }}
                                                        </div>
                                                        <div class="badge bg-success" v-if="sale.partiallyInvoices == 3">
                                                            {{
                                                                $i18n.locale == "en" || isLeftToRight()
                                                                ? " Fully Paid"
                                                                : "مدفوع بالكامل"
                                                            }}
                                                        </div>
                                                    </div>
                                                </a>
                                            </td>
                                            <td v-else>
                                                {{ sale.registrationNumber }}
                                            </td>
                                            <td>
                                                {{ getDate(sale.date) }}
                                            </td>
                                            <td>
                                                <a href="javascript:void(0)" v-on:click="
                                                    ViewCustomerInfo(
                                                        sale.customerId,
                                                        sale.cashCustomerId
                                                    )
                                                    " data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2"
                                                    aria-controls="offcanvasRight">{{ sale.customerName }}</a>
                                            </td>
                                            <td>
                                                {{ sale.userName }} <br />
                                                <span v-if="isDayStarts == 'true'">
                                                    {{
                                                        sale.counterName
                                                    }}
                                                </span>
                                            </td>
                                            <td v-if="sale.invoiceNote != null">
                                                <a href="javascript:void(0)" v-on:click="
                                                    SaleIdForCanvas(
                                                        sale.deliveryChallanId,
                                                        sale.saleOrderId,
                                                        sale.qutationId,
                                                        sale.proformaId,
                                                        sale.registrationNumber,
                                                        sale.date,
                                                        sale.id
                                                    )
                                                    " data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight3"
                                                    aria-controls="offcanvasRight">{{ sale.invoiceNote }}</a>
                                            </td>
                                            <td v-else>---</td>
                                            <td class="text-center" v-bind:key="randerToogle">
                                                <toggle-button v-on:change="openmodel(sale.id)" class="ml-2 mt-2"
                                                    color="#3178F6" v-bind:key="randerToogle" v-if="!sale.isClose" />

                                                <!-- <span class="d-inline-block " tabindex="0" data-bs-placement="top" data-toggle="tooltip" :title="purchaseOrder.reason" v-if="purchaseOrder.isProcessed">
                                                                        <span class="badge rounded-pill badge-soft-success">Processed</span>
                                                                    </span> -->
                                                <div class="tooltip badge rounded-pill badge-soft-success"
                                                    v-else-if="sale.isProcessed">
                                                    Processed
                                                    <span class="tooltiptext">{{ sale.reason }}</span>
                                                </div>

                                                <div class="tooltip badge rounded-pill badge-soft-danger"
                                                    v-else-if="sale.isClose">
                                                    Closed
                                                    <span class="tooltiptext">{{ sale.reason }}</span>
                                                </div>
                                            </td>
                                            <td>
                                                {{ currency }}
                                                {{
                                                    parseFloat(sale.netAmount)
                                                        .toFixed(3)
                                                        .slice(0, -1)
                                                        .replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
                                                }}
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle"
                                                    data-bs-toggle="dropdown" aria-expanded="false">
                                                    {{ $t("ProformaInvoices.Action") }}
                                                    <i class="mdi mdi-chevron-down"></i>
                                                </button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="EditSale(sale.id, true)">Clone</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-if="isValid('CanViewServiceInvoiceDetail') &&
                                                        printTemplate == 'Template6'
                                                        " v-on:click="PrintRdlc(sale.id, false)">
                                                        {{ $t("ProformaInvoices.TemplateView") }}
                                                    </a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(sale.id, false)" v-if="isValid('CanViewServiceInvoiceDetail') &&
                                                            printTemplate != 'Template6'
                                                            ">
                                                        {{ $t("ProformaInvoices.ViewInvoice") }}
                                                    </a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(sale.id, false)"
                                                        v-if="isValid('CanA4ServicePrint')">
                                                        {{ $t("ProformaInvoices.A4-Print") }}
                                                    </a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(sale.id, true)"
                                                        v-if="isValid('CanA4ServicePrint')">
                                                        {{ $t("ProformaInvoices.PdfDownload") }}
                                                    </a>
                                                    <!-- <a class="dropdown-item"
                                                       href="javascript:void(0)"
                                                       v-on:click="sendEmail(sale.id)"
                                                       v-if="
                              isValid('CanSendSaleEmailAsLink') ||
                              isValid('CanSendSaleEmailAsPdf')
                            ">
                                                        {{ $t("ProformaInvoices.Email") }}
                                                    </a> -->
                                                    <a class="dropdown-item" href="javascript:void(0);"
                                                        v-on:click="ReceiptGenerationandViewing(sale.id)"
                                                        v-if="sale.partiallyInvoices != 3 && ((isValid('StandardARAdvancePayment') || isValid('PremiumARAdvancePayment')) && formName == 'ServiceProformaInvoice')">
                                                        Create Payment Receipt
                                                    </a>
                                                    <a class="dropdown-item" href="javascript:void(0);"
                                                        v-on:click="ViewDeliveryChallan(sale.id)" data-bs-toggle="offcanvas"
                                                        data-bs-target="#offcanvasRight" aria-controls="offcanvasRight"> {{
                                                            $t('SaleOrder.IssueDeliveryNote') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-if="!sale.isProcessed && !sale.isClose"
                                                        v-on:click="GetCreateSaleInvoice(sale.id, sale.date, sale.registrationNumber, sale.netAmount)">Create
                                                        Sale Invoice</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="sendEmail(sale.id, sale.customerEmail)"
                                                        v-if="sale.isDefault">{{ $t('Sale.Email') }}</a>
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
                                        {{ $t("Pagination.ShowingEntries") }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount < 10">
                                        {{ $t("Pagination.Showing") }}
                                        {{ currentPage }} {{ $t("Pagination.to") }} {{ rowCount }}
                                        {{ $t("Pagination.of") }} {{ rowCount }}
                                        {{ $t("Pagination.entries") }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount >= 11">
                                        {{ $t("Pagination.Showing") }}
                                        {{ currentPage }} {{ $t("Pagination.to") }}
                                        {{ currentPage * 10 }} {{ $t("Pagination.of") }}
                                        {{ rowCount }} {{ $t("Pagination.entries") }}
                                    </span>
                                    <span v-else-if="currentPage === 1">
                                        {{ $t("Pagination.Showing") }} {{ currentPage }}
                                        {{ $t("Pagination.to") }} {{ currentPage * 10 }} of
                                        {{ rowCount }} {{ $t("Pagination.entries") }}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{ $t("Pagination.Showing") }} {{ currentPage * 10 - 9 }}
                                        {{ $t("Pagination.to") }} {{ currentPage * 10 }}
                                        {{ $t("Pagination.of") }} {{ rowCount }}
                                        {{ $t("Pagination.entries") }}
                                    </span>
                                    <span v-else-if="currentPage === pageCount">
                                        {{ $t("Pagination.Showing") }}
                                        {{ currentPage * 10 - 9 }} {{ $t("Pagination.to") }}
                                        {{ rowCount }} {{ $t("Pagination.of") }} {{ rowCount }}
                                        {{ $t("Pagination.entries") }}
                                    </span>
                                </div>
                                <div class="col-lg-6">
                                    <div class="float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                            :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                            :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight"
                        aria-labelledby="offcanvasRightLabel" style="width: 600px !important">
                        <div class="offcanvas-header">
                            <h5 id="offcanvasRightLabel" class="m-0">Delivery Note</h5>
                            <button v-bind:style="$i18n.locale == 'en' || isLeftToRight()
                                    ? ''
                                    : 'margin-left:0px !important'
                                " type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                        </div>
                        <div class="offcanvas-body">
                            <div class="row">
                                <div v-if="isCloseChallan">
                                    <h6 style="color: red">Challan Is Closed</h6>
                                </div>
                                <div class="row" v-else>
                                    <div class="col-lg-4" v-if="isAddChallan">
                                        <a v-on:click="ReservedDeliveryChallan(saleOrderId, true)"
                                            href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                            <i class="align-self-center icon-xs ti-plus"></i>
                                            Select Items
                                        </a>
                                    </div>
                                    <div class="col-lg-4" v-else>
                                        <a v-on:click="ReservedDeliveryChallan(isReservedId, false)"
                                            href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                            <i class="align-self-center icon-xs ti-plus"></i>
                                            Edit Items
                                        </a>
                                        <a v-on:click="
                                            ReservedDeliveryChallan(isReservedId, false, true)
                                            " href="javascript:void(0);" class="btn btn-sm btn-outline-danger mx-1">
                                            <i class="align-self-center icon-xs ti-plus"></i>
                                            Close
                                        </a>
                                    </div>

                                    <div class="col-lg-4" v-if="!isAddChallan && !isCloseChallan">
                                        <a v-on:click="DeliveryChllan(saleOrderId)" href="javascript:void(0);"
                                            class="btn btn-sm btn-outline-primary mx-1" data-bs-dismiss="offcanvas"
                                            aria-label="Close">
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
                                                <th>Delivery Order No</th>
                                                <th>Date</th>

                                                <th>Sale Order No</th>

                                                <th style="width: 70px" class="text-end"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(purchaseOrder, index) in deliveryChallanList"
                                                v-bind:key="purchaseOrder.registrationNumber">
                                                <td>
                                                    {{ index + 1 }}
                                                </td>

                                                <td>
                                                    <strong>
                                                        <a href="javascript:void(0)" data-bs-dismiss="offcanvas"
                                                            aria-label="Close" v-if="!isCloseChallan"
                                                            v-on:click="EditDeliveryChallan(purchaseOrder.id)">{{
                                                                purchaseOrder.registrationNumber }}</a>
                                                        <a href="javascript:void(0)" data-bs-dismiss="offcanvas"
                                                            aria-label="Close" v-else>{{ purchaseOrder.registrationNumber
                                                            }}</a>
                                                    </strong>
                                                </td>
                                                <td>
                                                    {{ purchaseOrder.date }}
                                                </td>
                                                <td>
                                                    {{ purchaseOrder.documentNumberForOrder }}
                                                </td>

                                                <td class="text-end">
                                                    <button type="button" class="btn btn-light dropdown-toggle"
                                                        data-bs-toggle="dropdown" aria-expanded="false">
                                                        {{ $t("SaleOrder.Action") }}
                                                        <i class="mdi mdi-chevron-down"></i>
                                                    </button>
                                                    <div class="dropdown-menu">
                                                        <a class="dropdown-item" href="javascript:void(0)"
                                                            data-bs-dismiss="offcanvas" aria-label="Close"
                                                            v-on:click="EditDeliveryChallan(purchaseOrder.id)"
                                                            v-if="!isCloseChallan">{{ $t("SaleOrder.EditInvoice") }}</a>
                                                        <a class="dropdown-item" href="javascript:void(0)"
                                                            data-bs-dismiss="offcanvas" aria-label="Close"
                                                            v-on:click="PrintRdlc(purchaseOrder.id, false)">{{
                                                                $t("SaleOrder.ViewInvoice") }}</a>
                                                        <a class="dropdown-item" href="javascript:void(0)"
                                                            data-bs-dismiss="offcanvas" aria-label="Close"
                                                            v-on:click="PrintRdlc(purchaseOrder.id, false)">{{
                                                                $t("SaleOrder.A4Print") }}</a>
                                                        <a class="dropdown-item" href="javascript:void(0)"
                                                            data-bs-dismiss="offcanvas" aria-label="Close"
                                                            v-on:click="PrintRdlc(purchaseOrder.id, true)">{{
                                                                $t("PDFDownload") }}</a>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight3"
                        aria-labelledby="offcanvasRightLabel" style="width: 600px !important">
                        <div class="offcanvas-header">
                            <h5 id="offcanvasRightLabel" class="m-0">
                                {{ $t("Sale.MoreDetails") }} ({{ registrationNo }})
                            </h5>
                            <button v-bind:style="$i18n.locale == 'en' || isLeftToRight()
                                    ? ''
                                    : 'margin-left:0px !important'
                                " type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                        </div>
                        <div class="offcanvas-body">
                            <!-- <div class="row">
                                          <a class="btn btn-light my-2 col-md-4" href="javascript:void(0);" v-on:click="ViewDeliveryChallan(saleIdToCanvas)" data-bs-toggle="offcanvas" v-if="isValid('SaleToDeliveryNote') " data-bs-target="#offcanvasRight" aria-controls="offcanvasRight"> {{ $t('Sale.ViewDeliveryNote') }}</a>
                                      </div> -->
                            <div class="row">
                                <div class="col-md-12" v-if="isValid('DeliveryNoteToSale') &&
                                    deliveryNo != null &&
                                    deliveryNo != ''
                                    ">
                                    <div class="row">
                                        <div class="form-group text-right">
                                            <b> {{ $t("PrintSetting.DeliveryNote") }} </b>
                                        </div>
                                        <div v-if="deliveryNo != null && deliveryNo != ''"
                                            class="col-lg-12 form-group text-left d-flex justify-items-between">
                                            <!-- v-if="expandDeliveryChallan" -->
                                            <p style="border-bottom: 1px solid #cbcbcb">
                                                <span>1- {{ deliveryNo }}--{{ getDate(canvasDate) }}</span>
                                            </p>
                                        </div>

                                        <div class="col-lg-12 form-group">
                                            <div class="table-responsive mt-3">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                        <th>#</th>
                                                        <th>{{ $t("AddBundles.ProductName") }}</th>
                                                        <th>{{ $t("StockLineDetails.Quantity") }}</th>
                                                    </thead>
                                                    <tbody v-for="(sale, index) in saleItem" v-bind:key="index">
                                                        <td>{{ index + 1 }}</td>
                                                        <td v-if="sale.productName == '' ||
                                                            sale.productName == null
                                                            ">
                                                            {{ sale.description }}
                                                        </td>
                                                        <td v-else>
                                                            {{ sale.displayName }}
                                                        </td>
                                                        <td>{{ sale.quantity }}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="row" v-if="(isValid('CanDraftQuotation') ||
                                            isValid('CanViewQuotation')) &&
                                        saleOrderNo != null &&
                                        saleOrderNo != ''
                                        ">
                                        <div class="form-group text-right" v-if="saleOrderNo != null && saleOrderNo != ''">
                                            <b> {{ $t("AddDispatchNote.SaleOrder") }}</b>
                                            <!-- <span>{{invoiceNote}}</span> -->
                                        </div>
                                        <div class="col-lg-12 form-group text-left d-flex">
                                            <p style="border-bottom: 1px solid #cbcbcb">
                                                <span>1- {{ saleOrderNo }}--{{ getDate(canvasDate) }}</span>
                                            </p>
                                        </div>

                                        <div class="col-lg-12 form-group">
                                            <div class="table-responsive">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                        <th>{{ $t("AddPurchase.DiscountType") }}</th>
                                                        <th>{{ $t("AddPurchase.TaxMethod") }}</th>
                                                        <th>{{ $t("StockLineDetails.VAT") }}</th>
                                                    </thead>
                                                    <tbody>
                                                        <td v-if="discountType">At Transaction Level</td>
                                                        <td v-else>At Line Item Level</td>
                                                        <td>{{ canvasTaxMethod }}</td>
                                                        <td>{{ vAT }}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="table-responsive mt-3">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                        <th>#</th>
                                                        <th>{{ $t("AddBundles.ProductName") }}</th>
                                                        <th>{{ $t("StockLineDetails.Quantity") }}</th>
                                                        <th>{{ $t("StockLineDetails.UnitPrice") }}</th>
                                                    </thead>
                                                    <tbody v-for="(sale, index) in saleItem" v-bind:key="index">
                                                        <td>{{ index + 1 }}</td>
                                                        <td v-if="sale.productName == '' ||
                                                            sale.productName == null
                                                            ">
                                                            {{ sale.description }}
                                                        </td>
                                                        <td v-else>
                                                            {{ sale.displayName }}
                                                        </td>
                                                        <td>{{ sale.quantity }}</td>
                                                        <td>{{ sale.unitPrice }}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="table-responsive mt-3">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                        <th>{{ $t("SaleItem.Subtotal") }}</th>
                                                        <th>{{ $t("SaleItem.Discount") }}</th>
                                                        <th>{{ $t("Vat Amount") }}</th>
                                                        <th>{{ $t("Amount After Discount") }}</th>
                                                        <th>{{ $t("SaleItem.TotalDue") }}</th>
                                                    </thead>
                                                    <tbody>
                                                        <td>
                                                            {{ currency }}
                                                            {{
                                                                parseFloat(grossAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                        <td>
                                                            {{
                                                                parseFloat(discountAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                        <td>
                                                            {{
                                                                parseFloat(vatAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                        <td>
                                                            {{
                                                                parseFloat(afterDiscountAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                        <td>
                                                            {{ currency }}
                                                            {{
                                                                parseFloat(totalAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="row" v-if="(isValid('CanDraftQuotation') ||
                                                isValid('CanViewQuotation')) &&
                                            quotationNo != null &&
                                            quotationNo != ''
                                            ">
                                        <div class="form-group text-right">
                                            <b> {{ $t("AddQuotation.Quotation") }}</b>
                                            <!-- <span>{{invoiceNote}}</span> -->
                                        </div>
                                        <div class="col-lg-12 form-group text-left d-flex">
                                            <p style="border-bottom: 1px solid #cbcbcb">
                                                <span>1- {{ quotationNo }}--{{ getDate(canvasDate) }}</span>
                                            </p>
                                        </div>

                                        <div class="col-lg-12 form-group">
                                            <div class="table-responsive">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                        <th>{{ $t("AddPurchase.DiscountType") }}</th>
                                                        <th>{{ $t("AddPurchase.TaxMethod") }}</th>
                                                        <th>{{ $t("StockLineDetails.VAT") }}</th>
                                                    </thead>
                                                    <tbody>
                                                        <td v-if="discountType">At Transaction Level</td>
                                                        <td v-else>At Line Item Level</td>
                                                        <td>{{ canvasTaxMethod }}</td>
                                                        <td>{{ vAT }}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="table-responsive mt-3">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                        <th>#</th>
                                                        <th>{{ $t("AddBundles.ProductName") }}</th>
                                                        <th>{{ $t("StockLineDetails.Quantity") }}</th>
                                                        <th>{{ $t("StockLineDetails.UnitPrice") }}</th>
                                                    </thead>
                                                    <tbody v-for="(sale, index) in saleItem" v-bind:key="index">
                                                        <td>{{ index + 1 }}</td>
                                                        <td v-if="sale.productName == '' ||
                                                            sale.productName == null
                                                            ">
                                                            {{ sale.description }}
                                                        </td>
                                                        <td v-else>
                                                            {{ sale.displayName }}
                                                        </td>
                                                        <td>{{ sale.quantity }}</td>
                                                        <td>{{ sale.unitPrice }}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="table-responsive mt-3">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                        <th>{{ $t("SaleItem.Subtotal") }}</th>
                                                        <th>{{ $t("SaleItem.Discount") }}</th>
                                                        <th>{{ $t("Vat Amount") }}</th>
                                                        <th>{{ $t("Amount After Discount") }}</th>
                                                        <th>{{ $t("SaleItem.TotalDue") }}</th>
                                                    </thead>
                                                    <tbody>
                                                        <td>
                                                            {{ currency }}
                                                            {{
                                                                parseFloat(grossAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                        <td>
                                                            {{
                                                                parseFloat(discountAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                        <td>
                                                            {{
                                                                parseFloat(vatAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                        <td>
                                                            {{
                                                                parseFloat(afterDiscountAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                        <td>
                                                            {{ currency }}
                                                            {{
                                                                parseFloat(totalAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="row" v-if="isValid('CanViewProforma') &&
                                            proformaNo != null &&
                                            proformaNo != ''
                                            ">
                                        <div class="form-group text-right">
                                            <b> {{ $t("ProformaInvoices.ProformaInvoices") }}</b>
                                            <!-- <span>{{invoiceNote}}</span> -->
                                        </div>
                                        <div class="col-lg-12 form-group text-left d-flex">
                                            <p style="border-bottom: 1px solid #cbcbcb">
                                                <span>1- {{ proformaNo }}--{{ getDate(canvasDate) }}</span>
                                            </p>
                                        </div>

                                        <div class="col-lg-12 form-group">
                                            <div class="table-responsive">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                        <th>{{ $t("AddPurchase.DiscountType") }}</th>
                                                        <th>{{ $t("AddPurchase.TaxMethod") }}</th>
                                                        <th>{{ $t("StockLineDetails.VAT") }}</th>
                                                    </thead>
                                                    <tbody>
                                                        <td v-if="discountType">At Transaction Level</td>
                                                        <td v-else>At Line Item Level</td>
                                                        <td>{{ canvasTaxMethod }}</td>
                                                        <td>{{ vAT }}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="table-responsive mt-3">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                        <th>#</th>
                                                        <th>{{ $t("AddBundles.ProductName") }}</th>
                                                        <th>{{ $t("StockLineDetails.Quantity") }}</th>
                                                        <th>{{ $t("StockLineDetails.UnitPrice") }}</th>
                                                    </thead>
                                                    <tbody v-for="(sale, index) in saleItem" v-bind:key="index">
                                                        <td>{{ index + 1 }}</td>
                                                        <td v-if="sale.productName == '' ||
                                                            sale.productName == null
                                                            ">
                                                            {{ sale.description }}
                                                        </td>
                                                        <td v-else>
                                                            {{ sale.displayName }}
                                                        </td>
                                                        <td>{{ sale.quantity }}</td>
                                                        <td>{{ sale.unitPrice }}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="table-responsive mt-3">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                        <th>{{ $t("SaleItem.Subtotal") }}</th>
                                                        <th>{{ $t("SaleItem.Discount") }}</th>
                                                        <th>{{ $t("Vat Amount") }}</th>
                                                        <th>{{ $t("Amount After Discount") }}</th>
                                                        <th>{{ $t("SaleItem.TotalDue") }}</th>
                                                    </thead>
                                                    <tbody>
                                                        <td>
                                                            {{ currency }}
                                                            {{
                                                                parseFloat(grossAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                        <td>
                                                            {{
                                                                parseFloat(discountAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                        <td>
                                                            {{
                                                                parseFloat(vatAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                        <td>
                                                            {{
                                                                parseFloat(afterDiscountAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                        <td>
                                                            {{ currency }}
                                                            {{
                                                                parseFloat(totalAmount)
                                                                    .toFixed(3)
                                                                    .slice(0, -1)
                                                                    .replace(
                                                                        /(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                                        "$1,"
                                                                    )
                                                            }}
                                                        </td>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="row text-center justify-content-center">
                                        <button v-if="expandHistory" v-bind:key="randerExpand"
                                            v-on:click="DocumentHistory(false)" type="button"
                                            class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                                            <i class="ti-angle-double-up"></i>
                                        </button>
                                        <button v-else v-on:click="DocumentHistory(true)" v-bind:key="randerExpand"
                                            type="button" class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                                            <i class="ti-angle-double-down"></i>
                                        </button>
                                    </div>

                                    <div v-if="expandHistory" class="col-lg-12 form-group">
                                        <div class="card">
                                            <div class="card-header">
                                                <h4 class="card-title">Document History</h4>
                                            </div>
                                            <!--end card-header-->
                                            <div class="card-body">
                                                <div class="main-timeline mt-3">
                                                    <div class="timeline" v-for="list in historyList"
                                                        v-bind:key="list.documentName">
                                                        <span class="timeline-icon"></span>
                                                        <span class="year">{{ list.documentName }}</span>
                                                        <div class="timeline-content">
                                                            <h5 class="title">{{ list.registrationNo }}</h5>
                                                            <span class="post">{{ list.date }}</span>
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
                </div>
            </div>
        </div>
        <div class="offcanvas offcanvas-end px-0" tabindex="-1" id="offcanvasRight2" aria-labelledby="offcanvasRightLabel">
            <div class="offcanvas-header">
                <h5 id="offcanvasRightLabel" class="m-0">
                    Customer Info ({{ customerInformation.runningBalance }})
                </h5>
                <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
            </div>
            <div class="offcanvas-body">
                <div class="row">
                    <div class="col-lg-12 form-group" v-if="english == 'true'">
                        <label>{{ $englishLanguage($t("Sale.CustomerName")) }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.englishName" disabled />
                    </div>
                    <div class="col-lg-12 form-group" v-if="isOtherLang()">
                        <label>{{ $t("Sale.CustomerNameArabic") }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.arabicName" disabled />
                    </div>
                    <div class="col-lg-12 form-group">
                        <label>{{ $t("AddCustomer.CustomerPhone") }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.contactNo1" disabled />
                    </div>
                    <div class="col-lg-12 form-group">
                        <label>{{ $t("AddCustomer.CommercialRegistrationNo") }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.commercialRegistrationNo"
                            disabled />
                    </div>
                    <div class="col-lg-12 form-group">
                        <label>{{ $t("AddCustomer.VAT/NTN/Tax No") }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.vatNo" disabled />
                    </div>
                    <div class="col-lg-12 form-group text-center">
                        <button v-if="expandSale" v-on:click="expandSale = false" type="button"
                            class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                            <i class="ti-angle-double-up"></i>
                        </button>
                        <button v-else v-on:click="expandSale = true" type="button"
                            class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                            <i class="ti-angle-double-down"></i>
                        </button>
                    </div>
                    <div v-if="expandSale" class="col-lg-12 form-group">
                        <h6 class="text-danger">Showing Last Three Month Invoices</h6>
                        <p v-for="(sale, index) in customerInformation.invoiceList" v-bind:key="index"
                            style="border-bottom: 1px solid #cbcbcb">
                            <a href="javascript:void(0);" data-bs-dismiss="offcanvas" aria-label="Close"
                                v-on:click="ViewInvoice(sale.id)">
                                <span>{{ index + 1 }}- {{ sale.registrationNumber }} </span>
                                <span class="float-end">
                                    {{ currency }}
                                    {{
                                        parseFloat(sale.netAmount)
                                            .toFixed(3)
                                            .slice(0, -1)
                                            .replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
                                    }}
                                </span>
                            </a>
                            <br />
                            <small>{{ getDate(sale.date) }}</small>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport"
            @close="show = false" @IsSave="IsSave" />

        <sale-invoice-service-default-Download :printDetails="printDetails" :isTouchScreen="sale"
            :headerFooter="headerFooter" v-if="download" @close="download = false" />
        <ObaagestSaleInvoice :printDetails="printDetails" :isTouchScreen="sale" :headerFooter="headerFooter"
            :saleSizeAssortment="saleSizeAssortment" v-if="printDetails.length != 0 &&
                printSize == 'A4' &&
                printTemplate == 'Template8' &&
                isPrint &&
                !download
                " v-bind:key="printRender" />

        <sale-invoice-service-default-Download :printDetails="printDetails" :isTouchScreen="sale"
            :headerFooter="headerFooter" v-if="download && printTemplate == 'Default'" @close="download = false" />

        <sale-invoice-default :printDetails="printDetails" :isTouchScreen="sale" :headerFooter="headerFooter" v-if="printDetails.length != 0 &&
            printSize == 'A4' &&
            printTemplate == 'Default' &&
            isPrint &&
            !download
            " v-bind:key="printRender" />

        <email-compose :show="emailComposeShow" v-if="emailComposeShow" @close="emailComposeShow = false"
            :documentId="saleId" :headerFooter="headerFooter" :formName="'Invoice'"></email-compose>
        <reasonsaleorder :show="show1" v-if="show1" @close="CloseRefresh" @SaveRecord="SaveRecord" />

        <div class="offcanvas offcanvas-end px-0" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel">
            <div class="offcanvas-header">
                <h5 id="offcanvasRightLabel" class="m-0">
                    Customer Info ({{ customerInformation.runningBalance }})
                </h5>
                <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
            </div>
            <div class="offcanvas-body">
                <div class="row">
                    <div class="col-lg-12 form-group" v-if="english == 'true'">
                        <label>{{ $englishLanguage($t("Sale.CustomerName")) }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.englishName" disabled />
                    </div>
                    <div class="col-lg-12 form-group" v-if="isOtherLang()">
                        <label>{{ $t("Sale.CustomerNameArabic") }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.arabicName" disabled />
                    </div>
                    <div class="col-lg-12 form-group">
                        <label>{{ $t("AddCustomer.CustomerPhone") }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.contactNo1" disabled />
                    </div>
                    <div class="col-lg-12 form-group">
                        <label>{{ $t("AddCustomer.CommercialRegistrationNo") }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.commercialRegistrationNo"
                            disabled />
                    </div>
                    <div class="col-lg-12 form-group">
                        <label>{{ $t("AddCustomer.VAT/NTN/Tax No") }}</label>
                        <input class="form-control" type="text" v-model="customerInformation.vatNo" disabled />
                    </div>
                    <div class="col-lg-12 form-group text-center">
                        <button v-if="expandSale" v-on:click="expandSale = false" type="button"
                            class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                            <i class="ti-angle-double-up"></i>
                        </button>
                        <button v-else v-on:click="expandSale = true" type="button"
                            class="btn btn-outline-info btn-icon-circle btn-icon-circle-sm">
                            <i class="ti-angle-double-down"></i>
                        </button>
                    </div>
                    <div v-if="expandSale" class="col-lg-12 form-group">
                        <p v-for="(sale, index) in customerInformation.invoiceList" v-bind:key="index"
                            style="border-bottom: 1px solid #cbcbcb">
                            <a href="javascript:void(0);" data-bs-dismiss="offcanvas" aria-label="Close"
                                v-on:click="ViewInvoice(sale.id)">
                                <span>{{ index + 1 }}- {{ sale.registrationNumber }} </span>
                                <span class="float-end">
                                    {{ currency }}
                                    {{
                                        parseFloat(sale.netAmount)
                                            .toFixed(3)
                                            .slice(0, -1)
                                            .replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
                                    }}
                                </span>
                            </a>
                            <br />
                            <small>{{ getDate(sale.date) }}</small>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <SendEmail :show="emailComposeShow" v-if="emailComposeShow" @close="emailComposeShow = false" :id="saleId" :from="'SaleInvocie'" :customerEmail="customerEmail"></SendEmail>
        <DeliveryChallanModel :newshow="reservedDeliveryChallanbool" v-if="reservedDeliveryChallanbool"
            :newpurchase="deliveryChallanRecord" :isReservedChallan="reservedDeliveryChallanbool" :type="isAdd"
            :isSaleOrder="true" :deliveryUndefined="deliveryUndefined" @close="GetRecordOfDelivery"></DeliveryChallanModel>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
import clickMixin from "@/Mixins/clickMixin";
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';
import moment from "moment";
export default {
    mixins: [clickMixin],
    props: ["formName"],
    components: {
        Loading
    },
    data: function () {
        return {
            customerEmail: '',

            isReservedId: "",
            monthObj: "",
            isAddChallan: false,
            isCloseChallan: false,
            reservedDeliveryChallanbool: false,
            deliveryChallanList: [],
            isAdd: false,
            deliveryUndefined: false,

            expandHistory: false,
            randerExpand: 0,
            historyList: [],

            seriesOfCustomer: [
                {
                    name: "Amount",
                    data: [],
                },
            ],
            chartOptionsOfCustomer: {
                chart: {
                    height: 350,
                    type: "line",
                    zoom: {
                        enabled: false,
                    },
                },
                dataLabels: {
                    enabled: false,
                },
                stroke: {
                    curve: "straight",
                },
                title: {
                    text: "Trending Customer by Amount",
                    align: "left",
                },
                grid: {
                    row: {
                        colors: ["#f3f3f3", "transparent"], // takes an array which will be repeated on columns
                        opacity: 0.5,
                    },
                },
                xaxis: {
                    categories: [],
                },
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
                totalCash: 0.0,
            },

            seriesPurchase: [
                {
                    name: "Credit Customer",
                    data: [],
                },
            ],
            chartOptionsPurchase: {
                chart: {
                    height: 350,
                    type: "area",
                },
                dataLabels: {
                    enabled: false,
                },
                stroke: {
                    curve: "smooth",
                },
                xaxis: {
                    categories: [],
                },
            },

            customerId: "",
            loading: false,
            request: 0,
            randerChart: 0,
            hold: 0,
            credit: 0,
            cash: 0,
            month: "",
            year: "",
            randerforempty: 0,
            isDisable: false,
            show1: false,
            isDisableMonth: false,
            saleId: "",
            isPrint: false,
            isService: false,
            isPorforma: false,
            download: false,
            emailComposeShow: false,
            bankDetail: false,
            printSize: "",
            printTemplate: "",
            filePath: null,
            fromDate: "",
            toDate: "",
            user: "",
            fromTime: "",
            toTime: "",
            userId: "",
            terminalId: "",
            active: "ProformaInvoice",
            isDisabled: false,
            advanceFilters: false,
            printInterval: "",
            search: "",
            english: "",
            arabic: "",
            searchQuery: "",
            saleList: [],
            purchasePostList: [],
            currentPage: 1,
            pageCount: "",
            rowCount: "",
            currency: "",

            selected: [],
            selectAll: false,
            updateApprovalStatus: {
                id: "",
                approvalStatus: "",
            },

            printDetails: [],
            printDetailsPos: [],
            printRender: 0,
            printRenderPos: 0,
            headerFooter: {
                footerEn: "",
                footerAr: "",
                company: "",
            },
            companyId: "00000000-0000-0000-0000-000000000000",
            CompanyID: "",
            UserID: "",
            employeeId: "",
            isDayAlreadyStart: false,
            IsProduction: false,
            AllowAll: false,
            sale: false,
            isDayStarts: "",
            rendr: 0,
            openBatch: 0,
            counter: 0,
            terminalType: "CashCounter",
            allShow: true,
            isloginhistory: true,
            overWrite: false,
            businessLogo: "",
            businessNameArabic: "",
            businessNameEnglish: "",
            businessTypeArabic: "",
            businessTypeEnglish: "",
            companyNameArabic: "",
            companyNameEnglish: "",
            show: false,
            customerInformation: "",
            expandSale: false,
            expandDeliveryChallan: false,
            expandDeliveryChallan1: false,
            expandDeliveryChallan2: false,
            registrationNo: "",
            saleIdToCanvas: "",
            quotationNo: "",
            saleOrderNo: "",
            deliveryNo: "",
            canvasDate: "",
            canvasSaleOrderId: "",
            qutationId: "",
            deliveryChallanId: "",
            vAT: "",
            canvasTaxMethod: "",
            discountType: false,
            invoiceNote: "",
            purchaseOrderId: "",
            saleItem: [],
            RanderAll: 0,
            randerToogle: 0,
            proformaNo: '',
        }
    },
    watch: {
        formName: function () {
            if (this.formName == "PurchaseOrder") {
                this.active = "PurchaseOrder";
                this.advanceFilters = false;
                this.currentPage = 1;
                this.request = 0;
                this.RanderAll++;
                this.getPage();
            } else {
                this.advanceFilters = false;

                this.active = "ProformaInvoice";
                this.currentPage = 1;
                this.request = 0;
                this.RanderAll++;
                this.getPage();
            }
            this.expandHistory = false;
            this.randerExpand = 0;
        },
    },
    computed: {
        isFilterButtonDisabled() {
            return (
                this.customerId ||
                this.monthObj ||
                this.fromDate ||
                this.toDate ||
                this.terminalId ||
                this.user
            );
        },
    },



    methods: {
        GetCreateSaleInvoice: function (id, date, registrationNumber, netAmount) {
            var details = {
                id: id,
                registrationNumber: registrationNumber,
                date: date,
                netAmount: netAmount,
                fromForm: 'purchaseOrder',

            }
            this.$router.push({
                path: '/AddSaleService?formName=' + 'SaleInvoice',
                query: {
                    token_name: 'Sales_token',
                    id: details,
                    document: 'PurchaseOrder'
                }
            })
        },
        ViewDeliveryChallan: function (id) {
            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }
            root.saleOrderId = id;
            root.$https
                .get(
                    "/Purchase/DeliveryChallanList?documentId=" +
                    id +
                    "&isSale=" +
                    false +
                    "&isDropdown=" +
                    true +
                    "&openBatch=" +
                    this.openBatch,
                    {
                        headers: {
                            Authorization: `Bearer ${token}`,
                        },
                    }
                )
                .then(
                    function (response) {
                        if (response.data != null) {
                            root.deliveryChallanList =
                                response.data.deliveryChallanListLookUpModels;
                            if (response.data.isReserved != null) {
                                root.isReservedId = response.data.isReserved;
                                root.isCloseChallan = response.data.isClose;
                                root.isAddChallan = false;
                            } else {
                                root.isAddChallan = true;
                                root.isCloseChallan = response.data.isClose;
                            }
                        }
                    },
                    function (error) {
                        this.loading = false;
                        console.log(error);
                    }
                );
        },

        ReservedDeliveryChallan: function (id, fromSale, close) {

            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }
            if (fromSale) {
                root.$https
                    .get(
                        "/Sale/SaleDetail?id=" +
                        id +
                        "&isSale=" +
                        false +
                        "&DeliveryChallan=" +
                        true,
                        {
                            headers: {
                                Authorization: `Bearer ${token}`,
                            },
                        }
                    )
                    .then(
                        function (response) {
                            if (response.data != null) {
                                root.deliveryChallanRecord = response.data;
                                root.isAdd = true;
                                root.reservedDeliveryChallanbool = true;

                                root.deliveryUndefined = true;
                                // root.$router.push({
                                //     query: {
                                //         data: undefined,

                                //     }
                                // })
                            }
                        },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        }
                    );
            } else {
                var manualClose = false;
                if (close == true) {
                    manualClose = true;
                }
                root.$https
                    .get(
                        "/Purchase/DeliveryChallanDetail?id=" +
                        id +
                        "&isSale=" +
                        false +
                        "&isReserved=" +
                        true +
                        "&manualClose=" +
                        manualClose,
                        {
                            headers: {
                                Authorization: `Bearer ${token}`,
                            },
                        }
                    )
                    .then(
                        function (response) {
                            if (response.data != null) {
                                root.deliveryChallanRecord = response.data;
                                root.isCloseChallan = response.data.isClose;
                                if (root.isCloseChallan) {
                                    root.isAdd = false;
                                    root.reservedDeliveryChallanbool = false;
                                } else {
                                    root.isAdd = false;
                                    root.reservedDeliveryChallanbool = true;
                                }
                            }
                        },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        }
                    );
            }
        },

        GetRecordOfDelivery: function () {
            this.reservedDeliveryChallanbool = false;
            this.ViewDeliveryChallan(this.saleOrderId);
        },

        ReceiptGenerationandViewing: function (id) {
            var root = this;

            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }

            root.$https
                .get(
                    "/PaymentVoucher/GetSaleToVoucher?Id=" +
                    id +
                    "&formName=ProfomaInvoice",
                    {
                        headers: {
                            Authorization: `Bearer ${token}`,
                        },
                    }
                )
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: "/addPaymentVoucher",
                            query: {
                                data: '',
                                Add: false,
                                formName: 'AdvanceReceipt',
                                isFormProforma: root.formName
                            },
                        });
                    } else {
                        root.$swal({
                            title:
                                root.$i18n.locale == "en" || root.isLeftToRight()
                                    ? "Error!"
                                    : "خطأ",
                            text:
                                root.$i18n.locale == "en" || root.isLeftToRight()
                                    ? "Something Went Wrong!"
                                    : "هل هناك خطب ما!",
                            type: "error",
                            confirmButtonClass: "btn btn-danger",
                            icon: "error",
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                });
        },
        DocumentHistory: function (ModelOn) {
            this.expandHistory = ModelOn;

            var root = this;
            var token = "";
            if (ModelOn) {
                if (token == "") {
                    token = localStorage.getItem("token");
                }
                var documentName =
                    this.formName == "PurchaseOrder"
                        ? "PurchaseOrder"
                        : this.formName == "ProformaInvoice" ||
                            this.formName == "ServiceProformaInvoice"
                            ? "ProformaInvoice"
                            : "";

                root.$https
                    .get(
                        "/Sale/DocumentHistory?documentName=" +
                        documentName +
                        "&Id=" +
                        this.saleIdToCanvas +
                        "&currentDocument=" +
                        documentName,
                        {
                            headers: {
                                Authorization: `Bearer ${token}`,
                            },
                        }
                    )
                    .then(function (response) {
                        if (response.data != null) {
                            root.historyList = response.data;
                        }
                    });
            }
        },
        ViewCustomerInfo: function (id, cashCustomerId) {
            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }

            var cashCustomer = false;
            if (id == null) {
                cashCustomer = true;
                id = cashCustomerId;
            }
            var documentType =
                this.formName == "ServiceProformaInvoice"
                    ? "ProformaInvoice"
                    : "PurchaseOrder";
            root.$https
                .get(
                    "/Contact/ContactLedgerDetail?id=" +
                    id +
                    "&documentType=" +
                    documentType +
                    "&cashCustomer=" +
                    cashCustomer +
                    "&isService=" +
                    this.isService +
                    "&lastThreeMonth=" +
                    true,
                    {
                        headers: {
                            Authorization: `Bearer ${token}`,
                        },
                    }
                )
                .then(
                    function (response) {
                        if (response.data != null) {
                            root.customerInformation = response.data;
                        }
                    },
                    function (error) {
                        console.log(error);
                    }
                );
        },
        CloseRefresh: function () {
            this.show1 = false;
            this.getPage();
        },
        SaveRecord: function (x) {
            this.loading = true;
            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }

            var saleOrder = {
                id: this.purchaseOrderId,
                isClose: true,
                reason: x,
            };
            this.$https
                .post("/sale/SaveSaleInformationReason", saleOrder, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                })
                .then(function (response) {
                    if (response != undefined) {
                        root.show1 = false;
                        root.$swal({
                            title:
                                root.$i18n.locale == "en" || root.isLeftToRight()
                                    ? "Saved!"
                                    : "!تم الحفظ",
                            text: "Sale Order Closed Successfully!",
                            type: "success",
                            icon: "success",
                            showConfirmButton: true,
                        });

                        root.getPage();
                    }
                });
        },
        openmodel: function (id) {
            this.purchaseOrderId = id;
            this.show1 = true;
        },
        GetDate1: function () {
            if (this.fromDate != "" || this.toDate != "") {
                this.isDisableMonth = true;
                this.year = "";
                this.monthObj = "";
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

        DeliveryChallanListWithCanvas: function (val, val1, val2) {
            this.expandDeliveryChallan = val;
            this.expandDeliveryChallan1 = val1;
            this.expandDeliveryChallan2 = val2;
        },
        SaleIdForCanvas: function (
            deliveryChallanId,
            saleOrderId,
            quotationId,
            proformaId,
            registrationNumber,
            date,
            saleId
        ) {
            var root = this;
            var token = "";
            if (deliveryChallanId != null) {
                if (token == "") {
                    token = localStorage.getItem("token");
                }
                root.saleIdToCanvas = saleId;
                this.expandHistory = false;
                this.randerExpand++;

                root.$https
                    .get(
                        "/Purchase/DeliveryChallanDetail?id=" +
                        deliveryChallanId +
                        "&simpleQuery=" +
                        true,
                        {
                            headers: {
                                Authorization: `Bearer ${token}`,
                            },
                        }
                    )
                    .then(
                        function (response) {
                            if (response.data != null) {
                                root.registrationNo =
                                    registrationNumber + " - " + root.getDate(date);
                                root.saleIdToCanvas = saleId;
                                root.canvasDate = response.data.date;
                                root.deliveryNo = response.data.registrationNo;
                                (root.quotationNo = null),
                                    (root.saleOrderNo = null),
                                    (root.proformaNo = null),
                                    (root.saleItem = response.data.deliveryChallanItems);
                            }
                        },
                        function (error) {
                            console.log(error);
                        }
                    );
            } else if (saleOrderId != null) {
                if (token == "") {
                    token = localStorage.getItem("token");
                }

                root.$https
                    .get(
                        "/Purchase/SaleOrderDetail?id=" +
                        saleOrderId +
                        "&simpleQuery=" +
                        true,
                        {
                            headers: {
                                Authorization: `Bearer ${token}`,
                            },
                        }
                    )
                    .then(
                        function (response) {
                            if (response.data != null) {
                                root.registrationNo =
                                    registrationNumber + " - " + root.getDate(date);
                                root.saleIdToCanvas = saleId;
                                root.quotationNo = null;
                                root.deliveryNo = null;
                                (root.proformaNo = null),
                                    (root.saleOrderNo = response.data.registrationNo),
                                    (root.canvasDate = response.data.date);
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
                        }
                    );
            } else if (quotationId != null) {
                if (token == "") {
                    token = localStorage.getItem("token");
                }

                root.$https
                    .get(
                        "/Purchase/SaleOrderDetail?id=" +
                        quotationId +
                        "&simpleQuery=" +
                        true,
                        {
                            headers: {
                                Authorization: `Bearer ${token}`,
                            },
                        }
                    )
                    .then(
                        function (response) {
                            if (response.data != null) {
                                root.registrationNo =
                                    registrationNumber + " - " + root.getDate(date);
                                root.saleIdToCanvas = saleId;
                                root.quotationNo = response.data.registrationNo;
                                root.deliveryNo = null;
                                (root.saleOrderNo = null),
                                    (root.proformaNo = null),
                                    (root.canvasDate = response.data.date);
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
                        }
                    );
            } else if (proformaId != null) {
                if (token == "") {
                    token = localStorage.getItem("token");
                }

                root.$https
                    .get("/Sale/SaleDetail?id=" + proformaId + "&simpleQuery=" + true, {
                        headers: {
                            Authorization: `Bearer ${token}`,
                        },
                    })
                    .then(
                        function (response) {
                            if (response.data != null) {
                                root.registrationNo =
                                    registrationNumber + " - " + root.getDate(date);
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
                        }
                    );
            }
        },

        AddSaleReturn: function () {
            var root = this;
            if (root.formName == "ServiceProformaInvoice") {
                root.$router.push({
                    path: "/AddSaleService",
                    query: {
                        Add: true,
                        token_name: "Sales_token",
                        isForm: true,
                        formName: 'ServiceProformaInvoice',
                    },
                });
                localStorage.setItem("IsService", true);
            } else if (root.formName == "PurchaseOrder") {
                root.$router.push({
                    path: "/AddSaleService",
                    query: {
                        Add: true,
                        token_name: "Sales_token",
                        isForm: true,
                        formName: 'PurchaseOrder',
                    },
                });
                localStorage.setItem("IsService", true);
            } else if (root.formName == "ProformaInvoice") {
                root.$router.push({
                    path: "/AddSaleService",
                    query: {
                        Add: true,
                        token_name: "Sales_token",
                        isForm: true,
                        formName: "ProformaInvoice",
                    },

                });
                localStorage.setItem("IsService", false);

            }

            // this.$router.push('/AddServiceProformaInvoice');
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
         sendEmail: function (saleId, email) {
                this.saleId = saleId
                this.customerEmail = email
                this.emailComposeShow = true;
            },
        GetUserId: function (x) {
            this.userId = x.id;
        },
        AdvanceFilterFor: function () {
            this.advanceFilters = !this.advanceFilters;
            if (this.advanceFilters == false) {
                this.FilterRecord(false);
            }
        },
        FilterRecord: function (type) {
            if (type) {
                if (this.fromDate != "") {
                    if (this.toDate == "") {
                        this.$swal({
                            title: "Error",
                            text: "Please Select To Date ",
                            type: "error",
                            icon: "error",
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                        });

                        return;
                    }
                }
                if (this.toDate != "") {
                    if (this.fromDate == "") {
                        this.$swal({
                            title: "Error",
                            text: "Please Select From Date ",
                            type: "error",
                            icon: "error",
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
            } else {
                this.isDisable = false;
                this.isDisableMonth = false;
                if (this.$refs.userDropdown != null) {
                    this.$refs.userDropdown.EmptyRecord();
                }
                if (this.$refs.terminalDropdown != null) {
                    this.$refs.terminalDropdown.Remove();
                }
                this.user = '';
                this.userId = '';

                this.year = "";
                this.search = "";
                this.fromDate = "";
                this.toDate = "";
                this.month = "";
                this.monthObj = '';
                this.terminalId = "";
                this.customerId = "";
                this.customerType = "";
                this.randerforempty++;

                if (this.$refs.CustomerDropdown != undefined)
                    this.$refs.CustomerDropdown.Remove();
            }

            this.getData(
                this.search,
                this.currentPage,
                this.active,
                this.fromDate,
                this.toDate,
                this.fromTime,
                this.toTime,
                this.terminalId,
                this.userId,
                this.customerId,
                this.customerType
            );
        },

        search22: function () {
            this.getData(
                this.search,
                this.currentPage,
                this.active,
                this.fromDate,
                this.toDate,
                this.fromTime,
                this.toTime,
                this.terminalId,
                this.userId,
                this.customerId,
                this.customerType
            );
        },

        clearData: function () {
            this.search = "";
            this.getData(
                this.search,
                this.currentPage,
                this.active,
                this.fromDate,
                this.toDate,
                this.fromTime,
                this.toTime,
                this.terminalId,
                this.userId,
                this.customerId,
                this.customerType
            );
        },
        getDate: function (date) {
            return moment(date).format("LLL");
        },
        PrinterInterval: function () {
            var root = this;

            this.printInterval = setInterval(() => {
                root.isDisabled = false;
            }, 3000);
        },
        GotoPage: function (link) {
            this.$router.push({
                path: link,
            });
        },
        ClearIntervalStatus: function () {
            clearInterval(this.printInterval);
        },

        ConvertToInvoice: function (id) {
            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }

            root.$https
                .get("/Sale/SaleDetail?Id=" + id, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                })
                .then(
                    function (response) {
                        if (response.data != null) {
                            root.$router.push({
                                path: "/AddSaleService",
                                query: {
                                    data: response.data,
                                    page: root.currentPage,
                                },
                            });
                        } else {
                            root.$swal({
                                title:
                                    root.$i18n.locale == "en" || root.isLeftToRight()
                                        ? "Error!"
                                        : "خطأ",
                                text:
                                    root.$i18n.locale == "en" || root.isLeftToRight()
                                        ? "Something Went Wrong!"
                                        : "هل هناك خطب ما!",
                                type: "error",
                                confirmButtonClass: "btn btn-danger",
                                icon: "error",
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    },
                    function (error) {
                        console.log(error);
                    }
                );
        },

        EditSale: function (id, clone) {
            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }

            root.$https
                .get("/Sale/SaleDetail?Id=" + id, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                })
                .then(
                    function (response) {
                        if (response.data != null) {
                            if (root.formName == "ServiceProformaInvoice") {
                                root.$store.GetEditObj(response.data);
                                root.$router.push({
                                    path:
                                        "/AddSaleService",
                                    query: {
                                        formName: "ServiceProformaInvoice",
                                        clone: clone,
                                        data: '',
                                        Add: false,
                                        token_name: "Sales_token",
                                        isForm: true,
                                    },
                                });
                                localStorage.setItem("IsService", true);
                            } else if (root.formName == "PurchaseOrder") {
                                root.$store.GetEditObj(response.data);
                                root.$router.push({
                                    path: "/AddSaleService",
                                    query: {
                                        data: '',
                                        Add: false,
                                        token_name: "Sales_token",
                                        isForm: true,
                                        formName: "PurchaseOrder",
                                        clone: clone,
                                    },
                                });
                                localStorage.setItem("IsService", true);
                            } else if (root.formName == "ProformaInvoice") {
                                root.$store.GetEditObj(response.data);
                                root.$router.push({
                                    path: "/AddSaleService",
                                    query: {
                                        data: '',
                                        Add: false,
                                        token_name: "Sales_token",
                                        isForm: true,
                                        formName: "ServiceProformaInvoice",
                                        clone: clone,
                                    },
                                });
                            }
                        } else {
                            root.$swal({
                                title:
                                    root.$i18n.locale == "en" || root.isLeftToRight()
                                        ? "Error!"
                                        : "خطأ",
                                text:
                                    root.$i18n.locale == "en" || root.isLeftToRight()
                                        ? "Something Went Wrong!"
                                        : "هل هناك خطب ما!",
                                type: "error",
                                confirmButtonClass: "btn btn-danger",
                                icon: "error",
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    },
                    function (error) {
                        console.log(error);
                    }
                );
        },

        DuplicateInvoice: function (id) {
            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }

            root.$https
                .get("/Sale/SaleDetail?Id=" + id, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                })
                .then(
                    function (response) {
                        if (response.data != null) {
                            response.data.isDuplicate = true;
                            root.$router.push({
                                path: "/AddSaleService",
                                query: {
                                    data: response.data,
                                    isDuplicate: "true",
                                    formName: "ServiceProformaInvoice",
                                },
                            });
                        }
                    },
                    function (error) {
                        console.log(error);
                    }
                );
        },
        ViewInvoice: function (id) {
            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }

            root.$https
                .get("/Sale/SaleDetail?Id=" + id, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                })
                .then(
                    function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: "/InvoiceServiceView",
                                query: {
                                    data: "",
                                    Add: false,
                                    page: root.currentPage,
                                    active: root.active,
                                },
                            });
                        }
                    },
                    function (error) {
                        console.log(error);
                    }
                );
        },
        goToPaymentDetail: function (id) {
            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }

            root.$https
                .get("/Sale/SaleDetail?Id=" + id, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                })
                .then(
                    function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: "/SalePaymentDetail",
                                query: {
                                    data: "",
                                    Add: false,
                                },
                            });
                        }
                    },
                    function (error) {
                        console.log(error);
                    }
                );
        },

        AdvanceFilter: function () {
            this.advanceFilters = !this.advanceFilters;
            if (this.advanceFilters == false) {
                this.fromTime = "";
                this.toTime = "";
                this.userId = "";
                this.terminalId = "";
            }
        },
        makeActive: function (item) {
            this.active = item;
            this.selectAll = false;
            this.selected = [];
            this.getData(
                this.search,
                1,
                item,
                this.fromDate,
                this.toDate,
                this.fromTime,
                this.toTime,
                this.terminalId,
                this.userId
            );
        },

        GetSaleDashboardRecord: function () {
            if (this.request == 0) {
                var root = this;
                var token = "";
                if (token == "") {
                    token = localStorage.getItem("token");
                }
                var branchId = localStorage.getItem("BranchId");

                var isPurchaseOrder = this.formName == "PurchaseOrder" ? true : false;
                this.loading = true;
                this.$https
                    .get(
                        "/Sale/SaleDashboardList?isService=" +
                        this.isService +
                        "&isProforma=" +
                        true +
                        "&isPurchaseOrder=" +
                        isPurchaseOrder +
                        "&branchId=" +
                        branchId,
                        {
                            headers: {
                                Authorization: `Bearer ${token}`,
                            },
                        }
                    )
                    .then(function (response) {
                        if (response.data != null) {
                            root.seriesOfCustomer[0].data = [];
                            root.chartOptionsOfCustomer.xaxis.categories = [];
                            root.seriesPurchase[0].data = [];
                            root.chartOptionsPurchase.xaxis.categories = [];
                            response.data.creditList.forEach(function (result) {
                                root.seriesPurchase[0].data.push(
                                    parseFloat(result.amount).toFixed(0)
                                );
                                root.chartOptionsPurchase.xaxis.categories.push(result.name);
                            });

                            root.hold = response.data.hold;
                            root.credit = response.data.credit;
                            root.cash = response.data.cash;

                            response.data.creditListByAmount.forEach(function (result) {
                                root.seriesOfCustomer[0].data.push(
                                    parseFloat(result.amount).toFixed(0)
                                );
                                root.chartOptionsOfCustomer.xaxis.categories.push(result.name);
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
                                totalCash: response.data.totalCash,
                            };
                            root.loading = false;
                        }
                    });
            }
            this.request++;
        },
        getPage: function () {
            this.getData(
                this.search,
                this.currentPage,
                this.active,
                this.fromDate,
                this.toDate,
                this.fromTime,
                this.toTime,
                this.terminalId,
                this.userId
            );
        },
        getData: function (
            search,
            currentPage,
            status,
            fromDate,
            toDate,
            fromTime,
            toTime,
            terminalId,
            userId
        ) {


            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }
            localStorage.setItem("currentPage", this.currentPage);
            var branchId = localStorage.getItem("BranchId");

            this.$https
                .get(
                    "/Sale/SaleInvoiceList?status=" +
                    status +
                    "&searchTerm=" +
                    search +
                    "&pageNumber=" +
                    currentPage +
                    "&fromDate=" +
                    fromDate +
                    "&toDate=" +
                    toDate +
                    "&fromTime=" +
                    fromTime +
                    "&toTime=" +
                    toTime +
                    "&terminalId=" +
                    terminalId +
                    "&userId=" +
                    userId +
                    "&isService=" +
                    this.isService +
                    "&month=" +
                    this.month +
                    "&year=" +
                    this.year +
                    "&CustomerId=" +
                    this.customerId +
                    "&branchId=" +
                    branchId,
                    {
                        headers: {
                            Authorization: `Bearer ${token}`,
                        },
                    }
                )
                .then(function (response) {
                    if (response.data != null) {
                        root.randerToogle++;
                        root.currentPage = response.data.currentPage;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.saleList = response.data.results.sales;
                        root.currentPage = currentPage;
                        root.rendr++;
                    }
                })
                .catch((error) => {
                    root.$swal.fire({
                        icon: "error",
                        title:
                            root.$i18n.locale == "en" || root.isLeftToRight()
                                ? "Error!"
                                : "خطأ",
                        text: error.response.data,
                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,
                    });
                });
        },

        RemoveSale: function (id) {
            var root = this;
            this.$swal({
                title:
                    this.$i18n.locale == "en" || this.isLeftToRight()
                        ? "Are you sure?"
                        : "هل أنت متأكد؟",
                text:
                    this.$i18n.locale == "en" || this.isLeftToRight()
                        ? "You will not be able to recover this!"
                        : "لن تتمكن من استرداد هذا!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText:
                    this.$i18n.locale == "en" || this.isLeftToRight()
                        ? "Yes, delete it!"
                        : "نعم ، احذفها!",
                closeOnConfirm: false,
                closeOnCancel: false,
            }).then(function (result) {
                if (result) {
                    var token = "";
                    if (token == "") {
                        token = localStorage.getItem("token");
                    }
                    root.$https
                        .get("/Sale/DeleteSale?Id=" + id, {
                            headers: {
                                Authorization: `Bearer ${token}`,
                            },
                        })
                        .then(
                            function (response) {
                                if (
                                    response.data.message.id !=
                                    "00000000-0000-0000-0000-000000000000"
                                ) {
                                    root.saleList.splice(
                                        root.saleList.findIndex(function (i) {
                                            return i.id === response.data.message.id;
                                        }),
                                        1
                                    );
                                    root.$swal({
                                        title:
                                            root.$i18n.locale == "en" || root.isLeftToRight()
                                                ? "Deleted!"
                                                : "تم الحذف!",
                                        text: response.data.message.isAddUpdate,
                                        type: "success",
                                        confirmButtonClass: "btn btn-success",
                                        buttonsStyling: false,
                                    });
                                } else {
                                    root.$swal({
                                        title:
                                            root.$i18n.locale == "en" || root.isLeftToRight()
                                                ? "Error!"
                                                : "خطأ",
                                        text: response.data.message.isAddUpdate,
                                        type: "error",
                                        confirmButtonClass: "btn btn-danger",
                                        buttonsStyling: false,
                                    });
                                }
                            },
                            function () {
                                root.$swal({
                                    title:
                                        root.$i18n.locale == "en" || root.isLeftToRight()
                                            ? "Error!"
                                            : "خطأ",
                                    text:
                                        root.$i18n.locale == "en" || root.isLeftToRight()
                                            ? "Delete UnSuccessfully"
                                            : "حذف غير ناجح",
                                    type: "error",
                                    confirmButtonClass: "btn btn-danger",
                                    buttonsStyling: false,
                                });
                            }
                        );
                } else {
                    this.$swal("Cancelled", "Your invoice is still intact", "info");
                }
            });
        },
        AddSale: function () {
            this.$router.push("/addSaleService");
        },
        getBase64Image: function (path) {
            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }

            this.headerFooter.company.logoPath =
                "data:image/png;base64," +
                "iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=";

            if (path != null && path != "" && path != undefined) {
                root.$https
                    .get("/Contact/GetBaseImage?filePath=" + path, {
                        headers: {
                            Authorization: `Bearer ${token}`,
                        },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath =
                                "data:image/png;base64," + root.filePath;
                        }
                    });
            }
        },
        GetHeaderDetail: function () {
            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }
            root.$https
                .get(
                    "/Company/GetCompanyDetail?id=" + localStorage.getItem("CompanyID"),
                    {
                        headers: {
                            Authorization: `Bearer ${token}`,
                        },
                    }
                )
                .then(function (response) {
                    if (response.data != null) {
                        root.headerFooter.company = response.data;
                        root.GetInvoicePrintSetting();

                        if (root.overWrite) {
                            root.headerFooter.company.nameArabic = root.businessNameArabic;
                            root.headerFooter.company.nameEnglish = root.businessNameEnglish;
                            root.headerFooter.company.categoryArabic =
                                root.businessTypeArabic;
                            root.headerFooter.company.categoryEnglish =
                                root.businessTypeEnglish;

                            root.headerFooter.company.companyNameArabic =
                                root.companyNameArabic;
                            root.headerFooter.company.companyNameEnglish =
                                root.companyNameEnglish;
                            root.headerFooter.company.logoPath = root.businessLogo;
                        }
                        root.getBase64Image(root.headerFooter.company.logoPath);
                    }
                });
        },
        ViewInvoiceTemplate: function (value) {
            this.GetHeaderDetail();

            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }
            root.printDetailsPos = [];
            root.$https
                .get("/Sale/SaleDetail?id=" + value, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                })
                .then(function (response) {
                    if (response.data != null) {
                        root.$router.push({
                            path: "/SaleServiceView",
                            query: {
                                printDetails: response.data,
                                headerFooter: root.headerFooter,
                            },
                        });
                    }
                });
        },
        PrintInvoice: function (value, prop) {
            this.GetHeaderDetail();

            var root = this;
            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }
            root.printDetailsPos = [];
            root.$https
                .get("/Sale/SaleDetail?id=" + value, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                })
                .then(function (response) {
                    if (response.data != null) {
                        root.printDetails = response.data;

                        if (prop == "download") {
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
        PrintRdlc: function (id, isDownload) {
            var root = this;
            var companyId = "";
            companyId = localStorage.getItem("CompanyID");
            if (isDownload) {
                this.loading = true;
                this.$https
                    .get(
                        this.$ReportServer +
                        "/SalesModuleReports/ProformaInvoice/ProformaInvoiceReport.aspx?Id=" +
                        id +
                        "&isFifo=" +
                        false +
                        "&openBatch=" +
                        this.openBatch +
                        "&isReturn=" +
                        false +
                        "&deliveryChallan=" +
                        false +
                        "&simpleQuery=" +
                        false +
                        "&colorVariants=" +
                        false +
                        "&CompanyId=" +
                        companyId +
                        "&formName=" +
                        this.formName +
                        "&IsCustomerPO=" +
                        IsCustomerPO +
                        "&isDownload=" +
                        isDownload,
                        { responseType: "blob" }
                    )
                    .then(function (response) {
                        root.loading = false;

                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement("a");
                        link.href = url;
                        var date = moment().format("DD MMM YYYY");
                        link.setAttribute("download", root.formName + date + ".pdf");
                        document.body.appendChild(link);
                        link.click();
                    });
            } else {
                var isBlind =
                    localStorage.getItem("IsBlindPrint") == "true" ? true : false;
                if (isBlind) {
                    this.show = false;
                } else {
                    this.show = true;
                }
                var form = "";
                if (this.formName == "PurchaseOrder") {
                    form = "CustomerPurchaseOrder";
                } else {
                    form = "ProformaInvoices";
                }
                var IsCustomerPO = this.formName == "PurchaseOrder" ? true : false;

                this.reportsrc =
                    this.$ReportServer +
                    "/SalesModuleReports/ProformaInvoice/ProformaInvoiceReport.aspx?Id=" +
                    id +
                    "&isFifo=" +
                    false +
                    "&openBatch=" +
                    this.openBatch +
                    "&isReturn=" +
                    false +
                    "&deliveryChallan=" +
                    false +
                    "&simpleQuery=" +
                    false +
                    "&colorVariants=" +
                    false +
                    "&CompanyId=" +
                    companyId +
                    "&formName=" +
                    form +
                    "&IsCustomerPO=" +
                    IsCustomerPO +
                    "&isDownload=" +
                    isDownload;
                this.changereport++;
            }
        },

        GetInvoicePrintSetting: function () {
            var root = this;

            var token = "";
            if (token == "") {
                token = localStorage.getItem("token");
            }
            var branchId = localStorage.getItem("BranchId");

            root.$https
                .get("/Sale/printSettingDetail?branchId=" + branchId, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                .then(
                    function (response) {
                        if (response.data != null && response.data != "") {
                            root.headerFooter.footerEn = response.data.termsInEng;
                            root.headerFooter.footerAr = response.data.termsInAr;
                            root.headerFooter.isHeaderFooter = response.data.isHeaderFooter;
                            root.headerFooter.englishName = response.data.englishName;
                            root.headerFooter.arabicName = response.data.arabicName;
                            root.headerFooter.customerAddress = response.data.customerAddress;
                            root.headerFooter.customerVat = response.data.customerVat;
                            root.headerFooter.customerNumber = response.data.customerNumber;
                            root.headerFooter.cargoName = response.data.cargoName;
                            root.headerFooter.customerTelephone =
                                response.data.customerTelephone;
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

                            root.headerFooter.businessAddressArabic =
                                response.data.businessAddressArabic;
                            root.headerFooter.businessAddressEnglish =
                                response.data.businessAddressEnglish;
                            root.headerFooter.headerImage = response.data.headerImageForPrint;
                            root.headerFooter.footerImage = response.data.footerImageForPrint;
                        }
                    },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    }
                );
        },
        PrintInvoicePos: function (value, counterName) {
            this.GetHeaderDetail();
            var root = this;
            var token = "";
            this.isDisabled = true;
            this.PrinterInterval();
            if (token == "") {
                token = localStorage.getItem("token");
            }
            root.printDetails = [];
            root.$https
                .get("/Sale/SaleDetail?id=" + value, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                })
                .then(function (response) {
                    if (response.data != null) {
                        root.printDetailsPos = response.data;
                        root.printDetailsPos.counterName = counterName;
                        root.printRenderPos++;
                    }
                });
        },
    },
    created: function () {
        this.isService =
            localStorage.getItem("IsSimpleInvoice") == "true" ? false : true;
        if (this.formName == "PurchaseOrder") {
            this.active = "PurchaseOrder";
        }

        this.GetHeaderDetail();

        if (this.$route.query.data == "addSaleService") {
            this.$emit("update:modelValue", "addSaleService");
        } else {
            this.$emit("update:modelValue", this.$route.name);
        }
    },
    mounted: function () {
        if (this.formName == "PurchaseOrder") {
            this.active = "PurchaseOrder";
        }
        this.isService =
            localStorage.getItem("IsSimpleInvoice") == "true" ? false : true;
        this.overWrite = localStorage.getItem("overWrite") == "true" ? true : false;

        this.businessLogo = localStorage.getItem("BusinessLogo");
        this.businessNameArabic = localStorage.getItem("BusinessNameArabic");
        this.businessNameEnglish = localStorage.getItem("BusinessNameEnglish");
        this.businessTypeArabic = localStorage.getItem("BusinessTypeArabic");
        this.businessTypeEnglish = localStorage.getItem("BusinessTypeEnglish");
        this.companyNameArabic = localStorage.getItem("CompanyNameArabic");
        this.companyNameEnglish = localStorage.getItem("CompanyNameEnglish");

        this.bankDetail =
            localStorage.getItem("BankDetail") == "true" ? true : false;
        // if (localStorage.getItem('fromDate') != null && localStorage.getItem('fromDate') != '' && localStorage.getItem('fromDate') != undefined) {
        //     this.fromDate = localStorage.getItem('fromDate');

        // } else {
        //     this.fromDate = moment().add(-7, 'days').format("DD MMM YYYY");

        // }
        // if (localStorage.getItem('toDate') != null && localStorage.getItem('toDate') != '' && localStorage.getItem('toDate') != undefined) {
        //     this.toDate = localStorage.getItem('toDate');

        // } else {
        //     this.toDate = moment().format("DD MMM YYYY");
        // }
        var IsDayStart = localStorage.getItem("DayStart");
        this.isDayStarts = localStorage.getItem("DayStart");
        var IsDayStartOn = localStorage.getItem("IsDayStart");
        this.printTemplate = localStorage.getItem("PrintTemplate");
        this.printSize = localStorage.getItem("PrintSizeA4");

        var AllowAll = localStorage.getItem("AllowAll");
        if (IsDayStart != "true") {
            this.isDayAlreadyStart = true;
            this.english = localStorage.getItem("English");
            this.arabic = localStorage.getItem("Arabic");
            this.search = "";
            if (
                localStorage.getItem("active") != null &&
                localStorage.getItem("active") != "" &&
                localStorage.getItem("active") != undefined
            ) {
                this.currentPage = parseInt(localStorage.getItem("currentPage"));

                this.getPage();
            } else {
                this.getPage();
            }
            this.currency = localStorage.getItem("currency");
            this.companyId = localStorage.getItem("CompanyID");
            //this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
            //this.headerFooter.footerAr = localStorage.getItem('TermsInAr');
        } else {
            if (AllowAll == "true") {
                this.isDayAlreadyStart = true;
                this.english = localStorage.getItem("English");
                this.arabic = localStorage.getItem("Arabic");
                this.search = "";
                if (
                    localStorage.getItem("active") != null &&
                    localStorage.getItem("active") != "" &&
                    localStorage.getItem("active") != undefined
                ) {
                    this.currentPage = parseInt(localStorage.getItem("currentPage"));

                    this.getPage();
                } else {
                    this.getPage();
                }
                this.currency = localStorage.getItem("currency");
                this.companyId = localStorage.getItem("CompanyID");
                //this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
                //this.headerFooter.footerAr = localStorage.getItem('TermsInAr');
            } else if (IsDayStartOn == "true") {
                this.isDayAlreadyStart = true;
                this.english = localStorage.getItem("English");
                this.arabic = localStorage.getItem("Arabic");
                this.search = "";
                if (
                    localStorage.getItem("active") != null &&
                    localStorage.getItem("active") != "" &&
                    localStorage.getItem("active") != undefined
                ) {
                    if (
                        localStorage.getItem("currentPage") != null &&
                        localStorage.getItem("currentPage") != "" &&
                        localStorage.getItem("currentPage") != undefined
                    ) {
                        this.currentPage = parseInt(localStorage.getItem("currentPage"));
                    } else {
                        this.currentPage = 1;
                    }

                    this.getPage();
                } else {
                    this.getPage();
                }
                this.currency = localStorage.getItem("currency");
                this.companyId = localStorage.getItem("CompanyID");
                //this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
                //this.headerFooter.footerAr = localStorage.getItem('TermsInAr');
            } else {
                this.CompanyID = localStorage.getItem("CompanyID");
                this.UserID = localStorage.getItem("UserID");
                this.employeeId = localStorage.getItem("EmployeeId");
                this.isDayAlreadyStart = false;
            }
        }
    },
};
</script>

<style scoped>
.timeline:nth-child(2n) .year {
    right: auto !important;
    left: 26% !important;
}

.year {
    right: 20% !important;
}

.vue__time-picker input.display-time {
    height: 40px !important;
    background-color: #f2f6f9;
    border: 1px solid #f2f6f9;
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
