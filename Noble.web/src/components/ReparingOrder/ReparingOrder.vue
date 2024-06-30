<template>
    <div class="row" v-if="isValid('CanViewPurchaseDraft')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h5 class="page_title">{{ $t('ReparingOrder.ReparingOrder') }}</h5>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Sale.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('ReparingOrder.ReparingOrder') }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">

                                <a href="javascript:void(0);" v-on:click="GotoPage('/AddMultiReparingOrder')" class="btn btn-sm btn-outline-primary mx-1">
                                    Add Multiple UPS
                                </a>
                                <a v-on:click="ImportDataFromXlsx" href="javascript:void(0);" class="btn btn-sm btn-outline-primary">
                                    Import Record
                                </a>
                                <a v-on:click="AddPurchaseOrder" href="javascript:void(0);" v-shortkey="[ 'enter']" @shortkey="AddPurchaseOrder"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    {{ $t('Purchase.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Sale.Close') }}
                                </a>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card">

                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-5" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-soft-primary" type="button" id="button-addon1">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" class="form-control" placeholder="Search by Order no,Customer or Employee"
                                       aria-label="Example text with button addon" aria-describedby="button-addon1">

                            </div>
                        </div>
                        <div class="col-lg-5" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-soft-primary" type="button" id="button-addon2">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="mobileNo" type="text" class="form-control" placeholder="Search by Mobile No"
                                       aria-label="Example text with button addon" aria-describedby="button-addon2">

                            </div>
                        </div>
                        <div class=" col-lg-2 mt-1" v-if="!advanceFilters">

<button v-on:click="search22(true)" type="button" :disabled ="!isFilterButtonDisabled" class="btn btn-outline-primary mt-3">
    {{ $t('Sale.ApplyFilter') }}
</button>
<button v-on:click="clearData(false)" type="button" :disabled ="!isFilterButtonDisabled" class="btn btn-outline-primary mx-2 mt-3">
    {{ $t('Sale.ClearFilter') }}
</button>

</div>
                    </div>
                </div>
                <div class="card-body">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link" v-bind:class="{active:active == 'All'}"
                               v-on:click="makeActive('All')" data-bs-toggle="tab" href="#All" role="tab" aria-selected="true">
                                All
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" v-bind:class="{active:active == 'InProgress'}"
                               v-on:click="makeActive('InProgress')" data-bs-toggle="tab" href="#InProgress" role="tab" aria-selected="true">
                                In Progress
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" v-bind:class="{active:active == 'Repared'}"
                               v-on:click="makeActive('Repared')" data-bs-toggle="tab" href="#Repared" role="tab" aria-selected="true">
                                Repared
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" v-bind:class="{active:active == 'PaymentReceived'}"
                               v-on:click="makeActive('PaymentReceived')" data-bs-toggle="tab" href="#PaymentReceived" role="tab" aria-selected="true">
                                Payment Received
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" v-bind:class="{active:active == 'Returned'}"
                               v-on:click="makeActive('Returned')" data-bs-toggle="tab" href="#Returned" role="tab" aria-selected="true">
                                Returned
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" v-bind:class="{active:active == 'CompletedOrders'}"
                               v-on:click="makeActive('CompletedOrders')" data-bs-toggle="tab" href="#CompletedOrders" role="tab" aria-selected="true">
                                Completed Orders
                            </a>
                        </li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div class="tab-pane pt-3" id="All" role="tabpanel" v-bind:class="{ active: active == 'All' }">

                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <!--<th style="width:40px;">#</th>-->
                                            <th class="text-center">
                                                Order No
                                            </th>
                                            <th class="text-center">
                                                Date Received
                                            </th>
                                            <th class="text-center">
                                                Employee
                                            </th>

                                            <th v-if="english=='true'" class="text-center">
                                                Customer
                                            </th>
                                            <th class="text-center">
                                                Mobile
                                            </th>
                                            <th class="text-center">
                                                UPS Desc
                                            </th>
                                            <th class="text-center">
                                                Issue
                                            </th>
                                            <th class="text-center">
                                                {{ $t('Purchase.Status') }}
                                            </th>
                                            <th class="text-center">
                                                Acessories
                                            </th>
                                            <th class="text-center">
                                                Done By
                                            </th>
                                            <th class="text-center">
                                                Amount
                                            </th>
                                            <th class="text-center">
                                                Payment
                                            </th>
                                            <th class="text-center">
                                                Balance
                                            </th>
                                            <!--<th  class="text-center">
                                        {{ $t('Purchase.RePrint') }}
                                    </th>-->
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(purchase) in purchasePostList" v-bind:key="purchase.id" class="text-center">

                                            <!--<td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*50)-50) +(index+1)}}
                                    </td>-->
                                            <td class="text-center">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchase.id,false)">  {{purchase.registrationNo}}</a>
                                                </strong>
                                            </td>
                                            <td class="text-center">
                                                {{purchase.dates}}
                                            </td>

                                            <td class="text-center">
                                                {{purchase.employeeNameEn}}

                                            </td>


                                            <td v-if="english=='true'" class="text-center">
                                                {{purchase.customerNameEn}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.mobile}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.upsDes}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.issue}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.status}}

                                            </td>
                                            <td class="text-center">
                                                {{purchase.accessor}}

                                            </td>
                                            <td class="text-center">
                                                {{purchase.doneBy}}

                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.advanceAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.cashAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.remaningPrice).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td style="font-size:15px !important;padding:0px !important;margin:0px !important" class="text-center">
                                    <a href="javascript:void(0)" class="btn  btn-primary btn-sm" v-on:click="EditPurchaseOrder(purchase.id,true)"><i class=" fas fa-eye"></i></a>
                                    <a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm mr-1 " v-on:click="PrintInvoice(purchase.id,true)"><i class=" fa fa-print"></i></a>
                                        <a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm mr-1 " v-on:click="PrintInvoice(purchase.id,false)"><i class=" fa fa-print"></i></a>
                                    </td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount < 50">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount >= 51  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*50}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*50}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*50)-49}} {{ $t('Pagination.to') }} {{currentPage*50}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*50)-49}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class=" float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage"
                                                      :total-rows="rowCount"
                                                      :per-page="50"
                                                      :first-text="$t('Table.First')"
                                                      :prev-text="$t('Table.Previous')"
                                                      :next-text="$t('Table.Next')"
                                                      :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane pt-3" id="InProgress" role="tabpanel" v-bind:class="{ active: active == 'InProgress' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <!--<th style="width:40px;">#</th>-->
                                            <th class="text-center">
                                                Order No
                                            </th>
                                            <th class="text-center">
                                                Date Received
                                            </th>
                                            <th class="text-center">
                                                Employee
                                            </th>



                                            <th v-if="english=='true'" class="text-center">
                                                Customer
                                            </th>
                                            <th class="text-center">
                                                Mobile
                                            </th>
                                            <th class="text-center">
                                                UPS Desc
                                            </th>
                                            <th class="text-center">
                                                Issue
                                            </th>
                                            <th class="text-center">
                                                {{ $t('Purchase.Status') }}
                                            </th>
                                            <th class="text-center">
                                                Acessories
                                            </th>
                                            <th class="text-center">
                                                Done By
                                            </th>
                                            <th class="text-center">
                                                Amount
                                            </th>
                                            <th class="text-center">
                                                Payment
                                            </th>
                                            <th class="text-center">
                                                Balance
                                            </th>
                                            <!--<th  class="text-center">
                                        {{ $t('Purchase.RePrint') }}
                                    </th>-->
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(purchase) in purchasePostList" v-bind:key="purchase.id">

                                            <!--<td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*50)-50) +(index+1)}}
                                    </td>-->
                                            <td class="text-center">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchase.id,false)">  {{purchase.registrationNo}}</a>
                                                </strong>
                                            </td>
                                            <td class="text-center">
                                                {{purchase.dates}}
                                            </td>

                                            <td class="text-center">
                                                {{purchase.employeeNameEn}}

                                            </td>


                                            <td v-if="english=='true'" class="text-center">
                                                {{purchase.customerNameEn}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.mobile}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.upsDes}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.issue}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.status}}

                                            </td>
                                            <td class="text-center">
                                                {{purchase.accessor}}

                                            </td>
                                            <td class="text-center">
                                                {{purchase.doneBy}}

                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.advanceAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.cashAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.remaningPrice).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <!--<td style="font-size:15px !important;padding:0px !important;margin:0px !important" class="text-center">-->
                                            <!--<a href="javascript:void(0)" class="btn  btn-primary btn-sm" v-on:click="EditPurchaseOrder(purchase.id,true)"><i class=" fas fa-eye"></i></a>-->
                                            <!--<a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm mr-1 " v-on:click="PrintInvoice(purchase.id,true)"><i class=" fa fa-print"></i></a>
                                        <a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm mr-1 " v-on:click="PrintInvoice(purchase.id,false)"><i class=" fa fa-print"></i></a>
                                    </td>-->
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount < 50">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount >= 51  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*50}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*50}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*50)-49}} {{ $t('Pagination.to') }} {{currentPage*50}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*50)-49}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class=" float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage"
                                                      :total-rows="rowCount"
                                                      :per-page="50"
                                                      :first-text="$t('Table.First')"
                                                      :prev-text="$t('Table.Previous')"
                                                      :next-text="$t('Table.Next')"
                                                      :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane pt-3" id="Repared" role="tabpanel" v-bind:class="{ active: active == 'Repared' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <!--<th style="width:40px;">#</th>-->
                                            <th class="text-center">
                                                Order No
                                            </th>
                                            <th class="text-center">
                                                Date Received
                                            </th>
                                            <th class="text-center">
                                                Employee
                                            </th>
                                            <th v-if="english=='true'" class="text-center">
                                                Customer
                                            </th>
                                            <th class="text-center">
                                                Mobile
                                            </th>
                                            <th class="text-center">
                                                UPS Desc
                                            </th>
                                            <th class="text-center">
                                                Issue
                                            </th>
                                            <th class="text-center">
                                                {{ $t('Purchase.Status') }}
                                            </th>
                                            <th class="text-center">
                                                Acessories
                                            </th>
                                            <th class="text-center">
                                                Done By
                                            </th>
                                            <th class="text-center">
                                                Amount
                                            </th>
                                            <th class="text-center">
                                                Payment
                                            </th>
                                            <th class="text-center">
                                                Balance
                                            </th>
                                            <!--<th  class="text-center">
                                        {{ $t('Purchase.RePrint') }}
                                    </th>-->
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(purchase) in purchasePostList" v-bind:key="purchase.id">

                                            <!--<td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*50)-50) +(index+1)}}
                                    </td>-->

                                            <td class="text-center">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchase.id,false)">  {{purchase.registrationNo}}</a>
                                                </strong>
                                            </td>
                                            <td class="text-center">
                                                {{purchase.dates}}
                                            </td>

                                            <td class="text-center">
                                                {{purchase.employeeNameEn}}
                                            </td>
                                            <td v-if="english=='true'" class="text-center">
                                                {{purchase.customerNameEn}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.mobile}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.upsDes}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.issue}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.status}}

                                            </td>
                                            <td class="text-center">
                                                {{purchase.accessor}}

                                            </td>
                                            <td class="text-center">
                                                {{purchase.doneBy}}

                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.advanceAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.cashAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.remaningPrice).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <!--<td style="font-size:15px !important;padding:0px !important;margin:0px !important" class="text-center">-->
                                            <!--<a href="javascript:void(0)" class="btn  btn-primary btn-sm" v-on:click="EditPurchaseOrder(purchase.id,true)"><i class=" fas fa-eye"></i></a>-->
                                            <!--<a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm mr-1 " v-on:click="PrintInvoice(purchase.id,true)"><i class=" fa fa-print"></i></a>
                                        <a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm mr-1 " v-on:click="PrintInvoice(purchase.id,false)"><i class=" fa fa-print"></i></a>
                                    </td>-->
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount < 50">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount >= 51  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*50}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*50}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*50)-49}} {{ $t('Pagination.to') }} {{currentPage*50}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*50)-49}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class=" float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage"
                                                      :total-rows="rowCount"
                                                      :per-page="50"
                                                      :first-text="$t('Table.First')"
                                                      :prev-text="$t('Table.Previous')"
                                                      :next-text="$t('Table.Next')"
                                                      :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane pt-3" id="PaymentReceived" role="tabpanel" v-bind:class="{ active: active == 'PaymentReceived' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <!--<th style="width:40px;">#</th>-->
                                            <th class="text-center">
                                                Order No
                                            </th>
                                            <th class="text-center">
                                                Date Received
                                            </th>
                                            <th class="text-center">
                                                Employee
                                            </th>
                                            <th v-if="english=='true'" class="text-center">
                                                Customer
                                            </th>
                                            <th class="text-center">
                                                Mobile
                                            </th>
                                            <th class="text-center">
                                                UPS Desc
                                            </th>
                                            <th class="text-center">
                                                Issue
                                            </th>
                                            <th class="text-center">
                                                {{ $t('Purchase.Status') }}
                                            </th>
                                            <th class="text-center">
                                                Acessories
                                            </th>
                                            <th class="text-center">
                                                Done By
                                            </th>
                                            <th class="text-center">
                                                Amount
                                            </th>
                                            <th class="text-center">
                                                Payment
                                            </th>
                                            <th class="text-center">
                                                Balance
                                            </th>
                                            <!--<th  class="text-center">
                                        {{ $t('Purchase.RePrint') }}
                                    </th>-->
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(purchase) in purchasePostList" v-bind:key="purchase.id">

                                            <!--<td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*50)-50) +(index+1)}}
                                    </td>-->

                                            <td class="text-center">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchase.id,false)">  {{purchase.registrationNo}}</a>
                                                </strong>
                                            </td>
                                            <td class="text-center">
                                                {{purchase.dates}}
                                            </td>

                                            <td class="text-center">
                                                {{purchase.employeeNameEn}}
                                            </td>
                                            <td v-if="english=='true'" class="text-center">
                                                {{purchase.customerNameEn}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.mobile}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.upsDes}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.issue}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.status}}

                                            </td>
                                            <td class="text-center">
                                                {{purchase.accessor}}

                                            </td>
                                            <td class="text-center">
                                                {{purchase.doneBy}}

                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.advanceAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.cashAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.remaningPrice).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <!--<td style="font-size:15px !important;padding:0px !important;margin:0px !important" class="text-center">-->
                                            <!--<a href="javascript:void(0)" class="btn  btn-primary btn-sm" v-on:click="EditPurchaseOrder(purchase.id,true)"><i class=" fas fa-eye"></i></a>-->
                                            <!--<a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm mr-1 " v-on:click="PrintInvoice(purchase.id,true)"><i class=" fa fa-print"></i></a>
                                        <a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm mr-1 " v-on:click="PrintInvoice(purchase.id,false)"><i class=" fa fa-print"></i></a>
                                    </td>-->
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount < 50">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount >= 51  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*50}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*50}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*50)-49}} {{ $t('Pagination.to') }} {{currentPage*50}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*50)-49}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class=" float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage"
                                                      :total-rows="rowCount"
                                                      :per-page="50"
                                                      :first-text="$t('Table.First')"
                                                      :prev-text="$t('Table.Previous')"
                                                      :next-text="$t('Table.Next')"
                                                      :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane pt-3" id="Returned" role="tabpanel" v-bind:class="{ active: active == 'Returned' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <!--<th style="width:40px;">#</th>-->
                                            <th class="text-center">
                                                Order No
                                            </th>
                                            <th class="text-center">
                                                Date Received
                                            </th>
                                            <th class="text-center">
                                                Employee
                                            </th>
                                            <th v-if="english=='true'" class="text-center">
                                                Customer
                                            </th>
                                            <th class="text-center">
                                                Mobile
                                            </th>
                                            <th class="text-center">
                                                UPS Desc
                                            </th>
                                            <th class="text-center">
                                                Issue
                                            </th>
                                            <th class="text-center">
                                                {{ $t('Purchase.Status') }}
                                            </th>
                                            <th class="text-center">
                                                Acessories
                                            </th>
                                            <th class="text-center">
                                                Done By
                                            </th>
                                            <th class="text-center">
                                                Amount
                                            </th>
                                            <th class="text-center">
                                                Payment
                                            </th>
                                            <th class="text-center">
                                                Balance
                                            </th>
                                            <!--<th  class="text-center">
                                        {{ $t('Purchase.RePrint') }}
                                    </th>-->
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(purchase) in purchasePostList" v-bind:key="purchase.id">

                                            <!--<td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*50)-50) +(index+1)}}
                                    </td>-->

                                            <td class="text-center">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchase.id,false)">  {{purchase.registrationNo}}</a>
                                                </strong>
                                            </td>
                                            <td class="text-center">
                                                {{purchase.dates}}
                                            </td>

                                            <td class="text-center">
                                                {{purchase.employeeNameEn}}
                                            </td>
                                            <td v-if="english=='true'" class="text-center">
                                                {{purchase.customerNameEn}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.mobile}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.upsDes}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.issue}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.status}}

                                            </td>
                                            <td class="text-center">
                                                {{purchase.accessor}}

                                            </td>
                                            <td class="text-center">
                                                {{purchase.doneBy}}

                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.advanceAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.cashAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.remaningPrice).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <!--<td style="font-size:15px !important;padding:0px !important;margin:0px !important" class="text-center">-->
                                            <!--<a href="javascript:void(0)" class="btn  btn-primary btn-sm" v-on:click="EditPurchaseOrder(purchase.id,true)"><i class=" fas fa-eye"></i></a>-->
                                            <!--<a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm mr-1 " v-on:click="PrintInvoice(purchase.id,true)"><i class=" fa fa-print"></i></a>
                                        <a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm mr-1 " v-on:click="PrintInvoice(purchase.id,false)"><i class=" fa fa-print"></i></a>
                                    </td>-->
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount < 50">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount >= 51  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*50}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*50}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*50)-49}} {{ $t('Pagination.to') }} {{currentPage*50}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*50)-49}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class=" float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage"
                                                      :total-rows="rowCount"
                                                      :per-page="50"
                                                      :first-text="$t('Table.First')"
                                                      :prev-text="$t('Table.Previous')"
                                                      :next-text="$t('Table.Next')"
                                                      :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane pt-3" id="CompletedOrders" role="tabpanel" v-bind:class="{ active: active == 'CompletedOrders' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <!--<th style="width:40px;">#</th>-->
                                            <th class="text-center">
                                                Order No
                                            </th>
                                            <th class="text-center">
                                                Date Received
                                            </th>
                                            <th class="text-center">
                                                Employee
                                            </th>
                                            <th v-if="english=='true'" class="text-center">
                                                Customer
                                            </th>
                                            <th class="text-center">
                                                Mobile
                                            </th>
                                            <th class="text-center">
                                                UPS Desc
                                            </th>
                                            <th class="text-center">
                                                Issue
                                            </th>
                                            <th class="text-center">
                                                {{ $t('Purchase.Status') }}
                                            </th>
                                            <th class="text-center">
                                                Acessories
                                            </th>
                                            <th class="text-center">
                                                Done By
                                            </th>
                                            <th class="text-center">
                                                Amount
                                            </th>
                                            <th class="text-center">
                                                Payment
                                            </th>
                                            <th class="text-center">
                                                Balance
                                            </th>
                                            <!--<th  class="text-center">
                                        {{ $t('Purchase.RePrint') }}
                                    </th>-->
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(purchase) in purchasePostList" v-bind:key="purchase.id">

                                            <!--<td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*50)-50) +(index+1)}}
                                    </td>-->

                                            <td class="text-center">
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditPurchaseOrder(purchase.id,false)">  {{purchase.registrationNo}}</a>
                                                </strong>
                                            </td>
                                            <td class="text-center">
                                                {{purchase.dates}}
                                            </td>

                                            <td class="text-center">
                                                {{purchase.employeeNameEn}}
                                            </td>
                                            <td v-if="english=='true'" class="text-center">
                                                {{purchase.customerNameEn}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.mobile}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.upsDes}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.issue}}
                                            </td>
                                            <td class="text-center">
                                                {{purchase.status}}

                                            </td>
                                            <td class="text-center">
                                                {{purchase.accessor}}

                                            </td>
                                            <td class="text-center">
                                                {{purchase.doneBy}}

                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.advanceAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.cashAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{currency}}   {{parseFloat(purchase.remaningPrice).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <!--<td style="font-size:15px !important;padding:0px !important;margin:0px !important" class="text-center">-->
                                            <!--<a href="javascript:void(0)" class="btn  btn-primary btn-sm" v-on:click="EditPurchaseOrder(purchase.id,true)"><i class=" fas fa-eye"></i></a>-->
                                            <!--<a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm mr-1 " v-on:click="PrintInvoice(purchase.id,true)"><i class=" fa fa-print"></i></a>
                                        <a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm mr-1 " v-on:click="PrintInvoice(purchase.id,false)"><i class=" fa fa-print"></i></a>
                                    </td>-->
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount < 50">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1 && rowCount >= 51  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*50}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*50}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*50)-49}} {{ $t('Pagination.to') }} {{currentPage*50}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*50)-49}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class=" float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage"
                                                      :total-rows="rowCount"
                                                      :per-page="50"
                                                      :first-text="$t('Table.First')"
                                                      :prev-text="$t('Table.Previous')"
                                                      :next-text="$t('Table.Next')"
                                                      :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <ReparingOrderThermalPrint :newprintDetails="printDetails" :headerFooter="headerFooter" v-if="isPrint" v-bind:key="printRander" />
            <ReparingOrderPaymentPrint :printDetails="printDetails" :headerFooter="headerFooter" v-if="isPrint2" v-bind:key="printRander2" />
        </div>
    </div>

    <div v-else> <acessdenied></acessdenied></div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin';
    import moment from "moment";


    export default {
        mixins: [clickMixin],

        data: function () {
            return {
                advanceFilters: false,
                expenseToGst: '',
                fromDate: '',
                toDate: '',
                arabic: '',
                english: '',
                active: 'All',
                search: '',
                mobileNo: '',
                searchQuery: '',
                purchaseOrderList: [],
                purchasePostList: [],
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                currency: '',
                selected: [],
                selectAll: false,
                isPrint: false,
                isPrint2: false,
                printRander: 0,
                printRander2: 0,
                updateApprovalStatus: {
                    id: '',
                    approvalStatus: ''
                },
                isMultiUnit: false,
                download: false,
                printDetails: [],
                printRender: 0,
                counter: 0,
                randerList: 0,
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
        this.search ||
        this.mobileNo 
      );
    },
  },
        watch: {
            // mobileNo: function (mobileNo) {

            //     {
            //         this.getPostData(this.search, this.currentPage, this.active, mobileNo);
            //     }
            // },
            // search: function (val) {

            //     {
            //         this.getPostData(val, this.currentPage, this.active, this.mobileNo);
            //     }
            // },

        },
        methods: {

            search22: function () {
                this.getPostData(this.search, this.currentPage, this.active, this.mobileNo);
            },

            clearData: function () {
                this.search="";
                this.mobileNo="";
                this.getPostData(this.search, this.currentPage, this.active, this.mobileNo);

            },




            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            ImportDataFromXlsx: function () {

                var root = this;
                root.$router.push({
                    path: '/ImportReparingOrder',
                })
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

                            root.getBase64Image(root.headerFooter.company.logoPath);


                        }
                    });
            },

            makeActive: function (item) {
                this.active = item;
                this.getPostData(this.search, this.currentPage, this.active, this.mobileNo);
            },

            getPage: function () {
                this.getPostData(this.search, this.currentPage, this.active, this.mobileNo);
            },
            getDate: function (date) {
                if (date == null || date == undefined) {
                    return "";
                }
                else {
                    return moment(date).format('L');
                }
            },

            getPostData: function (search, currentPage, status, mobileNo) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var branchId = localStorage.getItem('BranchId');

                this.$https.get('/ReparingOrder/ReparingOrderList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&mobileNo=' + mobileNo + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })

                    .then(function (response) {
                        if (response.data != null) {
                            root.purchasePostList = response.data.results;
                            root.pageCount = response.data.pageCount;
                            root.rowCount = response.data.rowCount;
                            root.currentPage = currentPage;
                            root.randerList++;
                        }
                    });
            },

            AddPurchaseOrder: function () {
                var root = this;
                if (this.search == '') {
                    root.$router.push({
                        path: '/AddReparingOrder',
                        query: {
                            mobileNo: root.mobileNo,
                            Add: true,
                        }
                    });
                }
                else {
                    return;
                }
            },
            EditPurchaseOrder: function (id, isView) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/ReparingOrder/ReparingOrderDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/AddReparingOrder',
                                query: {
                                    data: '',
                                    Add: false,
                                    isView: isView
                                }
                            })
                        }
                    },
                        function (error) {
                            this.loading = false;
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
            PrintInvoice: function (value, isPrint) {
                var id = value;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get("/ReparingOrder/ReparingOrderDetail?Id=" + id, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {

                            root.printDetails = response.data;
                            if (isPrint) {
                                root.isPrint = true;

                                root.printRander++;
                            }
                            else {
                                root.isPrint2 = true;

                                root.printRander2++;
                            }
                        }
                    });
            }
        },
        created: function () {

        },
        mounted: function () {
            
            //this.GetHeaderDetail();
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.getPostData(this.search, 1, 'All', this.mobileNo);
        },
    }
</script>