<template>
    <div class="row" v-if="isValid('CanViewProductionBatch')">
        <div class="col-lg-12 col-sm-12 ">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('ProductionBatch.ProductionBatch') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('ProductionBatch.Home')
                                    }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('ProductionBatch.ProductionBatch') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddProductionBatch')" v-on:click="AddPurchaseOrder"
                                    href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('ProductionBatch.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('ProductionBatch.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-8" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-secondary" type="button" id="button-addon1"><i
                                        class="fas fa-search"></i></button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('ProductionBatch.Search')" aria-label="Example text with button addon"
                                    aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" >

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
                    <div class="row">
                        <div class="col-lg-12">
                            <div>
                                <div>
                                    <ul class="nav nav-tabs" data-tabs="tabs">
                                        <li class="nav-item"><a class="nav-link" v-bind:class="{ active: active == 'Draft' }"
                                                v-if="isValid('CanViewProductionBatch')" v-on:click="makeActive('Draft')"
                                                id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab"
                                                aria-controls="v-pills-home" aria-selected="true">{{
                                                    $t('ProductionBatch.Draft') }}</a></li>
                                        <li class="nav-item"><a class="nav-link"
                                                v-bind:class="{ active: active == 'Approved' }"
                                                v-if="isValid('CanViewProductionBatch')" v-on:click="makeActive('Approved')"
                                                id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile"
                                                role="tab" aria-controls="v-pills-profile" aria-selected="false">{{
                                                    $t('ProductionBatch.Post') }}</a></li>
                                        <li class="nav-item"><a class="nav-link"
                                                v-bind:class="{ active: active == 'InProcess' }"
                                                v-on:click="makeActive('InProcess')" id="v-pills-profile-tab"
                                                data-toggle="pill" href="#v-pills-profile" role="tab"
                                                aria-controls="v-pills-profile" aria-selected="false">{{
                                                    $t('ProductionBatch.InProcess') }}</a></li>
                                        <li class="nav-item"><a class="nav-link"
                                                v-bind:class="{ active: active == 'Complete' }"
                                                v-on:click="makeActive('Complete')" id="v-pills-profile-tab"
                                                data-toggle="pill" href="#v-pills-profile" role="tab"
                                                aria-controls="v-pills-profile" aria-selected="false">{{
                                                    $t('ProductionBatch.Complete') }}</a></li>
                                        <li class="nav-item"><a class="nav-link"
                                                v-bind:class="{ active: active == 'Transfer' }"
                                                v-on:click="makeActive('Transfer')" id="v-pills-profile-tab"
                                                data-toggle="pill" href="#v-pills-profile" role="tab"
                                                aria-controls="v-pills-profile" aria-selected="false">{{
                                                    $t('ProductionBatch.Transfer') }}</a></li>
                                        <li class="nav-item"><a class="nav-link"
                                                v-bind:class="{ active: active == 'Rejected' }"
                                                v-on:click="makeActive('Rejected')" id="v-pills-profile-tab"
                                                data-toggle="pill" href="#v-pills-profile" role="tab"
                                                aria-controls="v-pills-profile" aria-selected="false">{{
                                                    $t('ProductionBatch.Rejected') }}</a></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="tab-content mt-3" id="nav-tabContent">
                                <div v-if="active == 'Draft'">
                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-outline-primary  btn-block"
                                                    type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                    aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('ProductionBatch.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right"
                                                    aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Approved')"> {{
                                                            $t('ProductionBatch.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Rejected')">{{
                                                            $t('ProductionBatch.Reject') }}</a>
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
                                                        {{ $t('ProductionBatch.ProductionBatchNo') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.CreatedDate') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.CreatedBy') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.RecipeNumber') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.TotalQuantity') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('ProductionBatch.Action') }}
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(purchaseOrder, index) in productionBatchList"
                                                    v-bind:key="purchaseOrder.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>
                                                    <td
                                                        v-if="isValid('CanAddProductionBatch') || isValid('CanAddProductionBatch')">
                                                        <strong>
                                                            <a href="javascript:void(0)"
                                                                v-on:click="EditPurchaseOrder(purchaseOrder.id)">{{ purchaseOrder.registrationNumber }}</a>
                                                        </strong>

                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.date }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.createdBy }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.reciptName }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.netAmount }}
                                                    </td>
                                                    <td class="text-center">
                                                        <button class="btn btn-icon btn-sm  btn-outline-primary mr-1"
                                                            v-on:click="ViewBatch(purchaseOrder.id)"><i
                                                                class="fas fa-eye"></i></button>
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
                                <div v-if="active == 'Rejected'">

                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-outline-primary  btn-block"
                                                    type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                    aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('ProductionBatch.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right"
                                                    aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Approved')"> {{
                                                            $t('ProductionBatch.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Rejected')">{{
                                                            $t('ProductionBatch.Reject') }}</a>
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
                                                        {{ $t('ProductionBatch.ProductionBatchNo') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.CreatedDate') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.CreatedBy') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.RecipeNumber') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.TotalQuantity') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('ProductionBatch.Action') }}
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(purchaseOrder, index) in productionBatchList"
                                                    v-bind:key="purchaseOrder.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>

                                                    <td>
                                                        {{ purchaseOrder.registrationNumber }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.date }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.createdBy }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.reciptName }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.netAmount }}
                                                    </td>
                                                    <td class="text-center">
                                                        <button class="btn btn-icon btn-sm  btn-outline-primary mr-1"
                                                            v-on:click="ViewBatch(purchaseOrder.id)"><i
                                                                class="fas fa-eye"></i></button>
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

                                <div v-if="active == 'Approved'">

                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-outline-primary  btn-block"
                                                    type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                    aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('ProductionBatch.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right"
                                                    aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Approved')"> {{
                                                            $t('ProductionBatch.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Rejected')">{{
                                                            $t('ProductionBatch.Reject') }}</a>
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
                                                        {{ $t('ProductionBatch.ProductionBatchNo') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.CreatedDate') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.CreatedBy') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.RecipeNumber') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.TotalQuantity') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('ProductionBatch.Action') }}
                                                    </th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(purchaseOrder, index) in productionBatchList"
                                                    v-bind:key="purchaseOrder.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>

                                                    <td>
                                                        {{ purchaseOrder.registrationNumber }}
                                                    </td>

                                                    <td>
                                                        {{ purchaseOrder.date }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.createdBy }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.reciptName }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.netAmount }}
                                                    </td>
                                                    <td class="text-center">
                                                        <button class="btn btn-success  mr-1"
                                                            v-on:click="openmodel(purchaseOrder, false)">
                                                            {{ $t('ProductionBatch.Process') }}
                                                        </button>
                                                        <button title="View"
                                                            class="btn btn-icon btn-sm  btn-outline-primary "
                                                            v-on:click="ViewBatch(purchaseOrder.id)"><i class="fas fa-eye"
                                                                title="View "></i></button>
                                                        <a href="javascript:void(0)" class="btn btn-outline-primary  mr-2" v-on:click="openmodel(purchaseOrder,false)">Process</a>
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
                                <div v-if="active == 'InProcess'">

                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-outline-primary  btn-block"
                                                    type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                    aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('ProductionBatch.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right"
                                                    aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Approved')"> {{
                                                            $t('ProductionBatch.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Rejected')">{{
                                                            $t('ProductionBatch.Reject') }}</a>
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
                                                        {{ $t('ProductionBatch.ProductionBatchNo') }}
                                                    </th>

                                                    <th>
                                                        {{ $t('ProductionBatch.ProcessDate') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.CreatedBy') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.ProcessBy') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.Reason') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.RecipeNumber') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.TotalQuantity') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('ProductionBatch.Action') }}
                                                    </th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(purchaseOrder, index) in productionBatchList"
                                                    v-bind:key="purchaseOrder.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>

                                                    <td>
                                                        {{ purchaseOrder.registrationNumber }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.processDate }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.createdBy }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.processBy }}
                                                    </td>
                                                    <td>
                                                        <span>
                                                            {{ purchaseOrder.lateReason }}
                                                        </span>

                                                        <!--{{purchaseOrder.lateReason}}-->
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.reciptName }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.netAmount }}
                                                    </td>
                                                    <td class="text-center">
                                                        <button class="btn btn-success  mr-1"
                                                            v-on:click="openmodel(purchaseOrder, true)">
                                                            {{ $t('ProductionBatch.Complete') }}
                                                        </button>
                                                        <button class="btn btn-icon btn-sm  btn-outline-primary"
                                                            v-on:click="ViewBatch(purchaseOrder.id)"><i
                                                                class="fas fa-eye"></i></button>
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
                                                    :last-text="$t('Table.Last')">
                                                </b-pagination>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="active == 'Complete'">

                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-outline-primary  btn-block"
                                                    type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                    aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('ProductionBatch.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right"
                                                    aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Approved')"> {{
                                                            $t('ProductionBatch.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Rejected')">{{
                                                            $t('ProductionBatch.Reject') }}</a>
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
                                                        {{ $t('ProductionBatch.ProductionBatchNo') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.CompletionDate') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.CreatedBy') }}
                                                    </th>

                                                    <th>
                                                        {{ $t('ProductionBatch.CompleteBy') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.Reason') }}
                                                    </th>

                                                    <th>
                                                        {{ $t('ProductionBatch.RecipeNumber') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.TotalQuantity') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('ProductionBatch.Action') }}
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(purchaseOrder, index) in productionBatchList"
                                                    v-bind:key="purchaseOrder.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>

                                                    <td>
                                                        {{ purchaseOrder.registrationNumber }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.completeDate }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.createdBy }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.completeBy }}
                                                    </td>
                                                    <td>
                                                        <span>
                                                            {{ purchaseOrder.lateReasonCompletion }}
                                                        </span>
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.reciptName }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.netAmount }}
                                                    </td>
                                                    <td class="text-center">
                                                        <button class="btn btn-success  mr-1"
                                                            v-on:click="transferModel(purchaseOrder)">
                                                            {{ $t('ProductionBatch.Transfer') }}
                                                        </button>
                                                        <button class="btn btn-icon btn-sm  btn-outline-primary mr-1"
                                                            v-on:click="ViewBatch(purchaseOrder.id)"><i
                                                                class="fas fa-eye"></i></button>
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
                                <div v-if="active == 'Transfer'">

                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-outline-primary  btn-block"
                                                    type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                    aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('ProductionBatch.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right"
                                                    aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Approved')"> {{
                                                            $t('ProductionBatch.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="UpdateApprovalStatus('Rejected')">{{
                                                            $t('ProductionBatch.Reject') }}</a>
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
                                                        {{ $t('ProductionBatch.ProductionBatchNo') }}
                                                    </th>

                                                    <th>
                                                        {{ $t('ProductionBatch.TransferDate') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.CreatedBy') }}
                                                    </th>

                                                    <th>
                                                        {{ $t('ProductionBatch.TransferBy') }}
                                                    </th>

                                                    <th>
                                                        {{ $t('ProductionBatch.RecipeNumber') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('ProductionBatch.TotalQuantity') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('ProductionBatch.Action') }}
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(purchaseOrder, index) in productionBatchList"
                                                    v-bind:key="purchaseOrder.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>

                                                    <td>
                                                        {{ purchaseOrder.registrationNumber }}
                                                    </td>

                                                    <td>
                                                        {{ purchaseOrder.transferDate }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.createdBy }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.transferBy }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.reciptName }}
                                                    </td>
                                                    <td>
                                                        {{ purchaseOrder.netAmount }}
                                                    </td>
                                                    <td class="text-center">
                                                        <button class="btn btn-icon btn-sm  btn-outline-primary mr-1"
                                                            v-on:click="ProductionBatchView(purchaseOrder.id)"><i
                                                                class="fas fa-eye"></i></button>
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
            <ProcessModel :newPurchase="purchase" :show="show" v-if="show" @close="show = false" @RefreshList="RefreshList" />

            <CompletionModel :newPurchase="purchase" :show="completeshow" v-if="completeshow" @close="completeshow = false"
                @RefreshList="RefreshList" />
            <TransferModel :newPurchase="purchase" :show="tranfershow" v-if="tranfershow" @close="tranfershow = false"
                @RefreshList="RefreshList" />
        </div>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";

export default {
    mixins: [clickMixin],
    data: function () {

        return {
            active: 'Draft',
            search: '',
            UserName: '',
            searchQuery: '',
            productionBatchList: [],
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            show: false,
            completeshow: false,
            tranfershow: false,
            purchase: {

            },
            selected: [],
            selectAll: false,
            updateApprovalStatus: {
                id: '',
                approvalStatus: ''
            }
        }
    },
    watch: {
        // search: function (val) {
        //     this.getData(val, 1, this.active);
        // }
    },
    methods: {

        search22: function () {
           this.getData(this.search, this.currentPage, this.active);
            },

            clearData: function () {
                this.search="";
                this.getData(this.search, this.currentPage, this.active);

            },



        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        RefreshList: function (x) {

            if (x == 'InProcess') {
                this.makeActive(x);

            }
            else if (x == 'Transfer') {
                this.makeActive(x);

            }
            else {
                x = 'Complete';
                this.makeActive(x);
            }

        },
        transferModel: function (purchase) {
            this.purchase = purchase;
            this.tranfershow = !this.tranfershow;
        },
        openmodel: function (purchase, x) {

            var root = this;
            if (x) {
                // For Completion
                this.purchase = purchase;
                this.completeshow = !this.completeshow;
                //var systemTime2 = moment(this.purchase.endTime).format('DD/MM/YYYY HH:mm:ss');
                //var endTime = moment(this.purchase.endTime).add(15, 'minutes').format('DD/MM/YYYY HH:mm:ss');
                //console.log(systemTime2, endTime);
            }
            else {
                // For IN PROCESS


                this.purchase = purchase;
                this.purchase.processBy = this.UserName;
                if (purchase.startTime != null) {
                    var systemTime = moment().add(1, 'hour').format('YYYY-MM-DD HH');
                    var startTime = moment(this.purchase.startTime).add(1, 'hour').format('YYYY-MM-DD HH');
                    var years = moment(systemTime).isSame(startTime, 'year');
                    if (years) {
                        var month = moment(systemTime).isSame(startTime, 'month ');
                        if (month) {
                            var days = moment(systemTime).isSame(startTime, 'day');
                            if (days) {
                                var hour = moment(systemTime).isSame(startTime, 'hour');
                                if (hour) {
                                    this.purchase.approvalStatus = 'InProcess';
                                    var token = '';
                                    if (token == '') {
                                        token = localStorage.getItem('token');
                                    }
                                    this.$https.post('/Batch/BatchStatus', this.purchase, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                                        if (response.data) {


                                            root.$swal({
                                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                                type: 'success',
                                                icon: 'success',
                                                showConfirmButton: false,
                                                timer: 1500,
                                                timerProgressBar: true,
                                            });
                                            root.makeActive('InProcess');
                                        }

                                        else {
                                            root.$swal({
                                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'There is some Error On Status Change!' : 'هناك بعض الخطأ في تغيير الحالة!',
                                                type: 'error',
                                                icon: 'error',
                                                showConfirmButton: false,
                                                timer: 1500,
                                                timerProgressBar: true,
                                            });
                                        }
                                    });
                                }
                                else {
                                    this.purchase = purchase;
                                    this.show = !this.show;
                                }

                            }
                            else {
                                this.purchase = purchase;
                                this.show = !this.show;
                            }
                        }
                        else {
                            this.purchase = purchase;
                            this.show = !this.show;
                        }
                    }
                    else {
                        this.purchase = purchase;
                        this.show = !this.show;
                    }
                }
                else {

                    this.purchase.approvalStatus = 'InProcess';


                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    this.$https.post('/Batch/BatchStatus', this.purchase, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data) {


                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.makeActive('InProcess');
                        }

                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'There is some Error On Status Change!' : 'هناك بعض الخطأ في تغيير الحالة!',
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    });
                }
                //var ms = moment(systemTime, "DD/MM/YYYY HH:mm:ss").diff(moment(startTime, "DD/MM/YYYY HH:mm:ss"));

                //var month = startTime.format('M');
                //var day = startTime.format('D');
                //var year = startTime.format('YYYY');
                //console.log(month, day, year);

                //console.log(diff);
                //console.log(ms);
                //if (ms > 0 || ms > 900000) {
                //    this.purchase = purchase;
                //    this.show = !this.show;

                //}
                //else {


                //}

            }
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
                for (let i in this.productionBatchList) {
                    this.selected.push(this.productionBatchList[i].id);
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
            var branchId = localStorage.getItem('BranchId');

            this.$https.get('/Batch/ProductionBatchList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    root.productionBatchList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;

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
                    root.$https.get('/Batch/DeleteProductionBatch?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
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
                }
                else {
                    this.$swal((this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!', (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Your file is still intact!' : 'ملفك لا يزال سليما!', (this.$i18n.locale == 'en' || root.isLeftToRight()) ? 'info' : 'معلومات');
                }
            });
        },
        AddPurchaseOrder: function () {
            var root =  this;
        //    this.$router.push('/AddProductionBatch');
            root.$router.push({
                            path: '/AddProductionBatch',
                            query: { 
                                Add: true, }
                        })
        },
        EditPurchaseOrder: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Batch/ProductionBatchDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/ProcessBatchScreen',
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
        ViewBatch: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Batch/ProductionBatchDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/BatchView',
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
        ProductionBatchView: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Batch/ProductionBatchDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/ProductionBatchView',
                            query: { data: '',
                                Add: false, }
                        })
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        }
    },
    created() {
        this.$emit('update:modelValue', this.$route.name);
    },
    mounted: function () {

        if (this.$route.query.data != undefined) {
            this.makeActive(this.$route.query.data);
        }
        else {
            this.makeActive("Draft");

        }
        this.UserName = localStorage.getItem('UserName');
        //this.getData(this.search, 1);
    },
    updated: function () {
        if (this.selected.length < this.productionBatchList.length) {
            this.selectAll = false;
        } else if (this.selected.length == this.productionBatchList.length) {
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