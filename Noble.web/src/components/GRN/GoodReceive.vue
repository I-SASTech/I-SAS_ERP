<template>
    <div class="row" v-if="isValid('CanViewGoodsReceiveasDraft') || isValid('CanViewGoodsReceiveasPost')">

        <div class="col-lg-12 col-sm-12 ">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('GoodReceive.GoodReceive') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a
                                            href="javascript:void(0);">{{ $t('PurchaseOrder.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('GoodReceive.GoodReceive') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddGoodsReceiveAsDraft') || isValid('CanAddGoodsReceiveasPost')"
                                    v-on:click="AddPurchaseOrder" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('PurchaseOrder.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('PurchaseOrder.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="card">
                <div class="card-header">
                    <div class="row align-items-center">
                        <div class="col-md-8 col-sm-12 " style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('AutoPurchaseTemplate.Search')"
                                    aria-label="Example text with button addon"
                                    aria-describedby="button-addon1, button-addon2">
                                <a class="btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2"
                                    value="Advance Filter">
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


                    <div class="row " v-if="advanceFilters">
                        <div class="col-md-3 col-sm-12">
                            <div class="form-group">
                                <label>{{ $t('AutoPurchaseTemplate.FromDate') }}</label>
                                <datepicker v-model="fromDate" />
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-12">
                            <div class="form-group">
                                <label>{{ $t('AutoPurchaseTemplate.ToDate') }}</label>
                                <datepicker v-model="toDate" />
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">

                            <button v-on:click="FilterRecord(true)" :disabled ="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="FilterRecord(false)" :disabled ="!isFilterButtonDisabled" type="button"
                                class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>

                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div>
                                <ul class="nav nav-tabs" data-tabs="tabs">
                                    <li v-if="isValid('CanViewGoodsReceiveasDraft')" class="nav-item"><a class="nav-link"
                                            v-bind:class="{ active: active == 'Draft' }" v-on:click="makeActive('Draft')"
                                            id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab"
                                            aria-controls="v-pills-home" aria-selected="true">{{ $t('PurchaseOrder.Draft')
                                            }}</a></li>
                                    <li v-if="isValid('CanViewGoodsReceiveasPost')" class="nav-item"><a class="nav-link"
                                            v-bind:class="{ active: active == 'Approved' }" v-on:click="makeActive('Approved')"
                                            id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab"
                                            aria-controls="v-pills-profile" aria-selected="false">{{
                                                $t('PurchaseOrder.Post') }}</a></li>

                                </ul>
                            </div>
                            <div class="tab-content mt-3" id="nav-tabContent">
                                <div v-if="active == 'Draft'">


                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>#</th>

                                                    <th>
                                                        {{ $t('PurchaseOrder.PONumber') }}
                                                    </th>


                                                    <th>
                                                        {{ $t('PurchaseOrder.CreatedDate') }}
                                                    </th>
                                                    <th v-if="english == 'true'">
                                                        {{ $t('PurchaseOrder.SupplierName') }}
                                                    </th>
                                                    <th v-else-if="isOtherLang()">
                                                        {{ $t('PurchaseOrder.SupplierNameAr') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('Sale.CreatedBy') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('InvoiceNote') }}
                                                    </th>
                                                   
                                                    <th>
                                                        {{ $t('Reference') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PurchaseOrder.NetAmount') }}
                                                    </th>
                                                  
                                                    <th>

                                                    </th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr v-for="(purchaseOrder, index) in purchaseOrderList"
                                                    v-bind:key="purchaseOrder.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>
                                                    <td v-if="isValid('CanEditGoodsReceiveasDraft')">
                                                        <strong>
                                                            <a href="javascript:void(0)"
                                                                v-on:click="EditPurchaseOrder(purchaseOrder.id)">{{ purchaseOrder.registrationNumber }}</a>
                                                        </strong>



                                                    </td>
                                                    <td v-else>
                                                        {{ purchaseOrder.registrationNumber }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.date }}
                                                    </td>
                                                    <td v-if="english=='true'">
                                                        <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(purchaseOrder.supplierId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2" aria-controls="offcanvasRight2">{{purchaseOrder.supplierName}}</a>
                                                    </td>
                                                    <td v-else-if="isOtherLang()">
                                                        <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(purchaseOrder.supplierId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2" aria-controls="offcanvasRight2">{{purchaseOrder.supplierNameArabic}}</a>
                                                    </td>
                                                  
                                                    <td v-if="purchaseOrder.createdBy != null">
                                                        {{ purchaseOrder.createdBy }}
                                                    </td>
                                                    <td v-else>
                                                        ---
                                                    </td>
                                                    <td v-if="purchaseOrder.createdFrom != null && purchaseOrder.supplierQuotationId!=null">
                                                        <a href="javascript:void(0)"
                                                            v-on:click="SaleIdForCanvas(purchaseOrder.registrationNumber, purchaseOrder.date, purchaseOrder.supplierQuotationId)"
                                                            data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight3"
                                                            aria-controls="offcanvasRight">{{ purchaseOrder.createdFrom
                                                            }}</a>
                                                    </td>
                                                    <td v-else-if="purchaseOrder.createdFrom != null && purchaseOrder.purchaseOrderId!=null">
                                                        <a href="javascript:void(0)"
                                                            v-on:click="SaleIdForCanvas(purchaseOrder.registrationNumber, purchaseOrder.date, purchaseOrder.purchaseOrderId)"
                                                            data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight3"
                                                            aria-controls="offcanvasRight">{{ purchaseOrder.createdFrom
                                                            }}</a>
                                                    </td>
                                                    <td v-else>
                                                        ---
                                                    </td>
                                                   
                                                    <td
                                                        v-if="purchaseOrder.reference != '' && purchaseOrder.reference != null">
                                                        {{ purchaseOrder.reference }}
                                                    </td>
                                                    <td v-else>
                                                        ---
                                                    </td>
                                                    <td>
                                                        {{ currency }}
                                                        {{ parseFloat(purchaseOrder.netAmount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                            "$1,") }}
                                                    </td>
                                                    <td class="text-end">
                                                        <button type="button" class="btn btn-light dropdown-toggle"
                                                            data-bs-toggle="dropdown" aria-expanded="false"> {{
                                                                $t('PurchaseOrder.Action') }} <i
                                                                class="mdi mdi-chevron-down"></i></button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="ViewPurchaseOrder(purchaseOrder.id)"
                                                                v-if="isValid('CanViewOrderDetail')">{{
                                                                    $t('PurchaseOrder.ViewInvoice') }} </a>
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="PrintInvoice(purchaseOrder.id, false)">{{
                                                                    $t('PurchaseOrder.A4Print') }}</a>
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="PrintRdlc(purchaseOrder.id)">{{
                                                                    $t('PurchaseOrder.Pdf') }}</a>
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
                                            <div class="float-end" v-on:click="getPage()">
                                                <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                    :per-page="10" :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')"></b-pagination>
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
                                                    {{ $t('PurchaseOrder.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right"
                                                    aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Approved')"> {{
                                                            $t('PurchaseOrder.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Rejected')">{{
                                                            $t('PurchaseOrder.Reject') }}</a>
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
                                                        {{ $t('PurchaseOrder.PONumber') }}
                                                    </th>


                                                    <th>
                                                        {{ $t('PurchaseOrder.CreatedDate') }}
                                                    </th>
                                                    <th v-if="english == 'true'">
                                                        {{ $t('PurchaseOrder.SupplierName') }}
                                                    </th>
                                                    <th v-else-if="isOtherLang()">
                                                        {{ $t('PurchaseOrder.SupplierNameAr') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('Sale.CreatedBy') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('InvoiceNote') }}
                                                    </th>
                                                   
                                                    <th>
                                                        {{ $t('Reference') }}
                                                    </th>
                                                    <th v-if="isValid('CanCloseOrder')">
                                                        {{ $t('PurchaseOrder.Status') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('PurchaseOrder.NetAmount') }}
                                                    </th>
                                                   
                                                    <th>

                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(purchaseOrder, index) in purchaseOrderList"
                                                    v-bind:key="purchaseOrder.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>
                                                    <td v-if="isValid('CanEditGoodsReceiveasDraft')">
                                                        <strong>
                                                            {{ purchaseOrder.registrationNumber }}
                                                        </strong>
                                                        <br />


                                                        <div class="badge bg-primary"
                                                            v-if="purchaseOrder.partiallyReceived == 2">
                                                            {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Partially Closed':'المدفوعة جزئيا'}}

                                                        </div>
                                                        <div class="badge bg-success"
                                                            v-if="purchaseOrder.partiallyReceived == 3">

                                                            {{ ($i18n.locale == 'en' || isLeftToRight()) ? ' Fully Closed':'مدفوعة بالكامل'}}
                                                        </div>


                                                    </td>
                                                    <td v-else>
                                                        {{ purchaseOrder.registrationNumber }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.date }}
                                                    </td>
                                                    <td v-if="english=='true'">
                                                        <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(purchaseOrder.supplierId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2" aria-controls="offcanvasRight2">{{purchaseOrder.supplierName}}</a>
                                                    </td>
                                                    <td v-else-if="isOtherLang()">
                                                        <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(purchaseOrder.supplierId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight2" aria-controls="offcanvasRight2">{{purchaseOrder.supplierNameArabic}}</a>
                                                    </td>
                                                    <td v-if="purchaseOrder.createdBy != null">
                                                        {{ purchaseOrder.createdBy }}
                                                    </td>
                                                    <td v-else>
                                                        ---
                                                    </td>
                                                    <td v-if="purchaseOrder.createdFrom != null && purchaseOrder.supplierQuotationId!=null">
                                                        <a href="javascript:void(0)"
                                                            v-on:click="SaleIdForCanvas(purchaseOrder.registrationNumber, purchaseOrder.date, purchaseOrder.supplierQuotationId,'goodReceive')"
                                                            data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight3"
                                                            aria-controls="offcanvasRight">{{ purchaseOrder.createdFrom
                                                            }}</a>
                                                    </td>
                                                    <td v-else-if="purchaseOrder.createdFrom != null && purchaseOrder.purchaseOrderId!=null">
                                                        <a href="javascript:void(0)"
                                                            v-on:click="SaleIdForCanvas(purchaseOrder.registrationNumber, purchaseOrder.date, purchaseOrder.purchaseOrderId,'purchaseOrder')"
                                                            data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight3"
                                                            aria-controls="offcanvasRight">{{ purchaseOrder.createdFrom
                                                            }}</a>
                                                    </td>
                                                    <td v-else>
                                                        ---
                                                    </td>
                                                  
                                                    <td
                                                        v-if="purchaseOrder.reference != '' && purchaseOrder.reference != null">
                                                        {{ purchaseOrder.reference }}
                                                    </td>
                                                    <td v-else>
                                                        ---
                                                    </td>
                                                    <td>
                                                        <toggle-button v-on:change="openmodel(purchaseOrder.id)" v-if="!purchaseOrder.isClose && !purchaseOrder.isProcessed && isValid('CanCloseOrder')" class="ml-2 mt-2" color="#3178F6" v-bind:key="randerToogle" />
                                                        <div v-on:click="GetDocumentRecord(purchaseOrder.id)"  data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight3" class="tooltip badge rounded-pill badge-soft-success" v-if="purchaseOrder.isProcessed || purchaseOrder.isClose">
                                                            Processed
                                                            <span class="tooltiptext">{{ purchaseOrder.reason }}</span>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        {{ currency }}
                                                        {{ parseFloat(purchaseOrder.netAmount).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                            "$1,") }}
                                                    </td>
                                                    <td class="text-end">
                                                        <button type="button" class="btn btn-light dropdown-toggle"
                                                            data-bs-toggle="dropdown" aria-expanded="false"> {{
                                                                $t('PurchaseOrder.Action') }} <i
                                                                class="mdi mdi-chevron-down"></i></button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="ViewPurchaseOrder(purchaseOrder.id)"
                                                                v-if="isValid('CanViewOrderDetail')"> {{
                                                                    $t('PurchaseOrder.ViewInvoice') }}</a>
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="PrintInvoice(purchaseOrder.id, false)"> {{
                                                                    $t('PurchaseOrder.A4Print') }}</a>
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="PrintInvoice(purchaseOrder.id, true)"> {{
                                                                    $t('PurchaseOrder.Pdf') }}</a>
                                                            <a class="dropdown-item" href="javascript:void(0);"  v-on:click="EditPurchaseOrder(purchaseOrder.id, true,'GoodReceive')"  v-if="isValid('CanViewPurchasePost') && !purchaseOrder.isClose   ">
                                                                Create Purchase Invoice
                                                            </a>
                                                            <a class="dropdown-item" href="javascript:void(0);"  v-on:click="sendEmail(purchaseOrder.id, purchaseOrder.customerEmail)"  v-if="purchaseOrder.isDefault">
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
                                            <div class="float-end" v-on:click="getPage()">
                                                <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                    :per-page="10" :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')"></b-pagination>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>


                        </div>
                    </div>
                </div>
            
            </div>


            <div class="offcanvas offcanvas-end px-0" tabindex="-1" id="offcanvasRight2" aria-labelledby="offcanvasRightLabel">
            <div class="offcanvas-header">
                <h5 id="offcanvasRightLabel" class="m-0">Supplier Info ({{customerInformation.runningBalance}})</h5>
                <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
            </div>
            <div class="offcanvas-body">
                <div class="row">
                    <div class="col-lg-12 form-group" v-if="english=='true'">
                        <label>Supplier Name</label>
                        <input class="form-control" type="text" v-model="customerInformation.englishName" disabled />
                    </div>
                    <div class="col-lg-12 form-group" v-if="isOtherLang()">
                        <label>Supplier Name</label>
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


            <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight3" aria-labelledby="offcanvasRightLabel" style="width:600px !important">
                        <div class="offcanvas-header">
                            <h5 id="offcanvasRightLabel" class="m-0">{{ $t('Sale.MoreDetails') }} ({{ registrationNo }})</h5>
                            <button v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                        </div>
                        <div class="offcanvas-body">
                            <!-- <div class="row">
                                <a class="btn btn-light my-2 col-md-4" href="javascript:void(0);" v-on:click="ViewDeliveryChallan(saleIdToCanvas)" data-bs-toggle="offcanvas" v-if="isValid('SaleToDeliveryNote') " data-bs-target="#offcanvasRight" aria-controls="offcanvasRight"> {{ $t('Sale.ViewDeliveryNote') }}</a>
                            </div> -->
                            <div class="row" v-if=" isList==true">
                                        <div class="row" v-for="item in documentRecord " v-bind:key="item.id"  >
                                        <div class="form-group text-right ">
                                            <b>{{ item.reference }}</b>
                                            <!-- <span>{{invoiceNote}}</span> -->
                                        </div>
                                        <div class="col-lg-12 form-group text-left d-flex">
                                            <p style="border-bottom: 1px solid #cbcbcb; ">
                                                <span>1- {{ item.registrationNo }}--{{getDate(item.date) }}</span>
                                            </p>
                                        </div>

                                        <div class="col-lg-12 form-group">
                                            <div class="table-responsive">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                    <th>{{ $t('AddPurchase.DiscountType') }}</th>
                                                    <th>{{ $t('AddPurchase.TaxMethod') }}</th>
                                                    <th>{{ $t('StockLineDetails.VAT') }}</th>
                                                    </thead>
                                                    <tbody>
                                                    <td v-if="item.discountType">
                                                        At Transaction Level
                                                    </td>
                                                    <td v-else>At Line Item Level</td>
                                                    <td>{{ item.taxMethod }}</td>
                                                    <td>{{ item.taxRatesName }}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="table-responsive mt-3">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                    <th>#</th>
                                                    <th>{{ $t('AddBundles.ProductName') }}</th>
                                                    <th>{{ $t('StockLineDetails.Quantity') }}</th>
                                                    <th>{{ $t('StockLineDetails.UnitPrice') }}</th>
                                                    </thead>
                                                    <tbody v-for="(sale,index ) in item.purchaseOrderItems" v-bind:key="index">
                                                    <td>{{ index + 1 }}</td>
                                                    <td v-if="sale.productName=='' || sale.productName==null">
                                                        {{ sale.description }}
                                                    </td>
                                                    <td v-else>
                                                        {{ sale.displayName }}
                                                    </td>
                                                    <td>{{ sale.quantityforCanvas }}</td>                                                    <td>{{ sale.unitPrice }}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="table-responsive mt-3">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                    <th>{{ $t('SaleItem.Subtotal') }}</th>
                                                    <th>{{ $t('SaleItem.Discount') }}</th>
                                                    <th>{{ $t('Vat Amount') }}</th>
                                                    <th>{{ $t('Amount After Discount') }}</th>
                                                    <th>{{ $t('SaleItem.TotalDue') }}</th>
                                                    </thead>
                                                    <tbody>
                                                    <td>{{currency}} {{parseFloat(item.grossAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                    <td>
                                                        {{parseFloat(item.discountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>
                                                    <td>
                                                        {{parseFloat(item.vatAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>
                                                    <td>{{parseFloat(item.totalAfterDiscount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                    <td> {{currency}} {{parseFloat(item.totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <hr>
                                    </div>


                                    </div>
                            <div class="row" v-else>
                               
                                <div class="col-md-12">
                                    <div class="row" v-if="quotationNo != null && quotationNo != ''">
                                        <div class="form-group text-right ">
                                            <b v-if="documentType=='purchaseOrder'">Purchase Order</b>
                                            <b v-else>Supplier Quotation</b>
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
                                                    <th>{{ $t('AddPurchase.DiscountType') }}</th>
                                                    <th>{{ $t('AddPurchase.TaxMethod') }}</th>
                                                    <th>{{ $t('StockLineDetails.VAT') }}</th>
                                                    </thead>
                                                    <tbody>
                                                    <td v-if="discountType">
                                                        At Transaction Level
                                                    </td>
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
                                                    <th>{{ $t('AddBundles.ProductName') }}</th>
                                                    <th>{{ $t('StockLineDetails.Quantity') }}</th>
                                                    <th>{{ $t('StockLineDetails.UnitPrice') }}</th>
                                                    </thead>
                                                    <tbody v-for="(sale,index ) in saleItem" v-bind:key="index">
                                                    <td>{{ index + 1 }}</td>
                                                    <td v-if="sale.productName=='' || sale.productName==null">
                                                        {{ sale.description }}
                                                    </td>
                                                    <td v-else>
                                                        {{ sale.displayName }}
                                                    </td>
                                                    <td>{{ sale.quantityforCanvas }}</td>                                                    <td>{{ sale.unitPrice }}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="table-responsive mt-3">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                    <th>{{ $t('SaleItem.Subtotal') }}</th>
                                                    <th>{{ $t('SaleItem.Discount') }}</th>
                                                    <th>{{ $t('Vat Amount') }}</th>
                                                    <th>{{ $t('Amount After Discount') }}</th>
                                                    <th>{{ $t('SaleItem.TotalDue') }}</th>
                                                    </thead>
                                                    <tbody>
                                                    <td>{{currency}} {{parseFloat(grossAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                    <td>
                                                        {{parseFloat(discountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>
                                                    <td>
                                                        {{parseFloat(vatAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>
                                                    <td>{{parseFloat(afterDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                    <td> {{currency}} {{parseFloat(totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                
                            </div>
                        </div>
                    </div>
             <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport" @close="show=false" @IsSave="IsSave" />
             <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>
            <reasonsaleorder :show="show1" v-if="show1" @close="CloseRefresh" @SaveRecord="SaveRecord" />
            <SendEmail :show="emailComposeShow" v-if="emailComposeShow" @close="emailComposeShow = false" :id="saleId" :from="'GoodsReceive'" :customerEmail="customerEmail"></SendEmail>

        </div>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';
export default {
    mixins: [clickMixin],
    components: {
            Loading
        },
    data: function () {
        return {
            customerEmail:'',
            saleId:'',
            emailComposeShow: false,

            loading: false,
            show: false,
            changereport: 0,
            reportsrc: '',
            customerInformation: '',
            expandSale:false,
            documentRecord: [],
            isList: false,
            purchaseOrderIdToCanvas: '',
            documentType: '',
            canvasDate: '',
            purchaseOrderNo: '',
            PurchaseOrderItems: [],
            historyList: [],
            vAT: '',
            canvasTaxMethod: '',

            registrationNo: '',
            quotationNo: '',
            discountType: false,
            grossAmount: 0,
            discountAmount: 0,
            vatAmount: 0,
            afterDiscountAmount: 0,
            randerToogle: 0,


            allowBranches: false,
            show1: false,
            advanceFilters: false,
            //versionAllow: '',
            printRender: 0,
            download: false,
            printShow: false,
            internationalPurchase: '',
            fromDate: '',
            toDate: '',
            arabic: '',
            english: '',
            active: 'Draft',
            search: '',
            searchQuery: '',
            purchaseOrderList: [],
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            currency: '',
            purchaseId: '',
            totalAmount: 0,
            customerAccountId: '',
            payment: false,
            isMultiUnit: false,


            selected: [],
            selectAll: false,
            updateApprovalStatus: {
                id: '',
                approvalStatus: ''
            },
            counter: 0,
            randerList: 0,
            printDetails: [],
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
        this.fromDate ||
        this.toDate 
      );
    },
  },
    watch: {
      
    },
    methods: {
        sendEmail: function (saleId, email) {
            this.saleId = saleId
            this.customerEmail = email
            this.emailComposeShow = true;
        },
          IsSave: function () {
            this.show = false;
        },
        getDate: function (date) {
                return moment(date).format('LLL');
            },
        GetDocumentRecord: function (documentId) {
            
            var root = this;
            var token = '';
            this.expandHistory = false;
            this.randerExpand++;
            if (documentId != null) {
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                let supplierQuotation='GoodReceive';
                root.$https.get('/Purchase/ProccessedByDocumentId?id=' + documentId + '&documentName='+ supplierQuotation, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                .then(function (response) {
                    if (response.data != null) {
                        
                        if(response.data.lookups.length>0)
                        {
                            root.isList=true;
                            root.registrationNo='';
                            root.documentRecord = response.data.lookups;
                        }
                        else
                        {
                            root.registrationNo='';
                            root.isList=false;
                        }
                       
                    }
                },
                function (error) {
                    console.log(error);
                });
            }
        },
        
        SaleIdForCanvas: function ( registrationNumber, date, saleId,documentType) {
                 this.isList=false;

                var root = this;
                var token = '';
                root.saleIdToCanvas = saleId;
                root.documentType = documentType;
                this.expandHistory = false;
                this.randerExpand++;
                if (saleId != null) {
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }

                    var url='';
                    if(documentType=='goodReceive')
                    {
                        url='/Purchase/PurchaseOrderDetail?id=';

                    }
                    else
                    {
                        url='/Purchase/PurchaseOrderDetail?id=';

                    }



                    root.$https.get(url + saleId + '&simpleQuery=' + true, {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }
                    })
                        .then(function (response) {
                            if (response.data != null) {
                                root.registrationNo = registrationNumber + ' - ' + root.getDate(date);
                                root.canvasDate = response.data.date;
                                root.quotationNo = response.data.registrationNo;
                                root.saleItem = response.data.purchaseOrderItems;
                                root.grossAmount = response.data.grossAmount;
                                root.discountAmount = response.data.discountAmount;
                                root.vatAmount = response.data.vatAmount;
                                root.afterDiscountAmount = response.data.totalAfterDiscount;
                                root.totalAmount = response.data.totalAmount;
                                root.vAT = response.data.taxRateName;
                                root.canvasTaxMethod = response.data.taxMethod;
                                root.discountType = response.data.discountType;
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
                

                
                root.$https.get('/Contact/ContactLedgerDetail?id=' + id + '&documentType=' + "GoodReceive"   + '&isService=' + false + '&lastThreeMonth=' + true, {
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
        SaveRecord: function (x) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            var purchaseOrder = {
                id: this.purchaseOrderId,
                isClose: true,
                reason: x,
                documentTypeForClose:'GoodReceive'
            };
            this.$https.post('/Purchase/UpdatePurchaseOrderCloseReason', purchaseOrder, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
                if (response.data.isSuccess) {
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
                else
                {
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
        CloseRefresh: function () {
            this.show1 = false;
            this.getPage();
        },
        openmodel: function (id) {
            this.purchaseOrderId = id;
            this.show1 = true;
        },

        search22: function () {
            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);
        },

        clearData: function () {
            this.search = "";
            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);

        },

        AdvanceFilterFor: function () {

            this.advanceFilters = !this.advanceFilters;
            if (this.advanceFilters == false) {
                this.FilterRecord(false);
            }
        },
        FilterRecord: function (type) {

            if (type) {
                // if (this.fromDate != '') {
                //     if (this.toDate == '') {
                //         this.$swal({
                //             title: 'Error',
                //             text: "Please Select To Date ",
                //             type: 'error',
                //             icon: 'error',
                //             showConfirmButton: false,
                //             timer: 2000,
                //             timerProgressBar: true,
                //         });

                //         return;

                //     }
                // }
                // if (this.toDate != '') {
                //     if (this.fromDate == '') {
                //         this.$swal({
                //             title: 'Error',
                //             text: "Please Select From Date ",
                //             type: 'error',
                //             icon: 'error',
                //             showConfirmButton: false,
                //             timer: 2000,
                //             timerProgressBar: true,
                //         });

                //         return;

                //     }
                // }



            } else {

                this.fromDate = '';
                this.toDate = '';


            }

            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);

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


        changeStatus: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            var purchase = { id: id, isClose: true };
            this.$https.post('/Purchase/SavePurchaseOrderInformation', purchase, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data.isSuccess == true) {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                        text: "Purchase Order Closed Successfully!",
                        type: 'success',
                        icon: 'success',
                        showConfirmButton: true
                    });
                    root.getPage();
                }
            });
        },
        paymentSave: function () {
            this.payment = false;
        },
        paymentModel: function (purchaseId, totalAmount, customerAccountId) {

            this.purchaseId = purchaseId;
            this.totalAmount = totalAmount;
            this.customerAccountId = customerAccountId;
            this.payment = true;
        },
        DeleteFile: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Purchase/DeletePo', this.selected, { headers: { "Authorization": `Bearer ${token}` } })
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
                for (let i in this.purchaseOrderList) {
                    this.selected.push(this.purchaseOrderList[i].id);
                }
            }
        },
        getPage: function () {
            this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate);
        },

        makeActive: function (item) {
            this.active = item;
            this.selectAll = false;
            this.selected = [];
            this.getData(this.search, 1, item, this.fromDate, this.toDate);
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
            this.$https.get('/Purchase/GoodReceiveList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&fromDate=' + fromDate + '&toDate=' + toDate + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.purchaseOrderList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.currentPage = currentPage;
                        root.randerList++;
                    }
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
                    root.$https.get('/Purchase/DeletePurchaseOrder?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
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
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Delete UnSuccessfully!' : 'حذف غير ناجح!',
                                    type: 'error',
                                    confirmButtonClass: "btn btn-danger",
                                    buttonsStyling: false
                                });
                            });
                }
                else {
                    this.$swal((this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!', (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Your file is still intact!' : 'ملفك لا يزال سليما!', (this.$i18n.locale == 'en' || root.isLeftToRight()) ? 'info' : 'معلومات');
                }
            });
        },
        AddPurchaseOrder: function () {

            //this.$router.push('/AddGoodReceive');
            this.$router.push({
                                path: '/AddGoodReceive' ,
                                query: {
                                       Add: true,
                                }
                            })
        },
        ViewPurchaseOrder: function (id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Purchase/GoodReceiveDetail?Id=' + id + '&isPoView=' + true + '&isMultiUnit=' + this.isMultiUnit, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/GoodReceiveView',
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
        EditPurchaseOrder: function (id,clone, isPurchaseOrder) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Purchase/GoodReceiveDetail?Id=' + id + '&isMultiUnit=' + this.isMultiUnit, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        if(isPurchaseOrder == "GoodReceive")
                        {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addpurchase',
                                query: {
                                       data: '',
                                       Add: false,
                                       clone:'GoodReceive',
                                       isConversion : true
                                }
                            })
                        }
                        else
                        {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                            path: '/AddGoodReceive',
                            query: { 
                               data: '',
                                Add: false,
                            }
                        })
                        }


                       
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },

         PrintInvoice: function (id, isDownload) {
            var root = this;
            var companyId = '';
            companyId = localStorage.getItem('CompanyID');
            
            if (isDownload) {
                this.loading = true;
                this.$https.get(this.$ReportServer + '/PurchaseModule/GoodsReceive/GoodsReceiveReport.aspx?Id=' + id 
                + '&isFifo=' + false 
                + '&openBatch=' + 0 
                + '&isReturn=' + false 
                + '&deliveryChallan=' + false 
                + '&simpleQuery=' + false 
                + '&colorVariants=' + false 
                + '&isMultiUnit=' + this.isMultiUnit 
                + '&isReturnView=' + true 
                + '&CompanyId=' + companyId 
                + '&formName=GoodReceive' 
                + '&isDownload=' + isDownload
                    , { responseType: 'blob' }).then(function (response) {
                        root.loading = false;
                        
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        var date = moment().format('DD MMM YYYY');
                        link.setAttribute('download', 'PurchaseOrder' + date + '.pdf');
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
                


                this.reportsrc = this.$ReportServer + '/PurchaseModule/GoodsReceive/GoodsReceiveReport.aspx?Id=' + id + '&isFifo=' + false + '&openBatch=' + 0 + '&isReturn=' + false + '&deliveryChallan=' + false + '&simpleQuery=' + false + '&colorVariants=' + false + '&isMultiUnit=' + this.isMultiUnit + '&isReturnView=' + true + '&CompanyId=' + companyId + '&formName=GoodReceive' + '&isDownload=' + isDownload
                this.changereport++;
            }
        },
        


    },
    created: function () {

        if (this.$route.query.data == 'goodReceives') {
            this.$emit('update:modelValue', 'goodReceives');

        }
        else {
            this.$emit('update:modelValue', this.$route.name);

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
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.internationalPurchase = localStorage.getItem('InternationalPurchase');
        this.isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
        //this.versionAllow = localStorage.getItem('VersionAllow');
        if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
            this.currentPage = parseInt(localStorage.getItem('currentPage'));
            this.active = (localStorage.getItem('active'));
            this.getPage();

        }
        else {

            this.makeActive("Draft");
        }




        this.currency = localStorage.getItem('currency');
        this.GetHeaderDetail();
    },
    updated: function () {
        if (this.selected.length < this.purchaseOrderList.length) {
            this.selectAll = false;
        } else if (this.selected.length == this.purchaseOrderList.length) {
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

<style scoped>
    /* Tooltip container */
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
