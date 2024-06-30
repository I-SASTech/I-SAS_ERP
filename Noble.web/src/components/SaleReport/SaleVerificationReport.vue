<template>
    <div class="row" v-if="isValid('CanViewHoldInvoices') || isValid('CanViewPaidInvoices') || isValid('CanViewCreditInvoices') || isValid('CanViewHoldServiceInvoices') || isValid('CanViewPaidServiceInvoices') || isValid('CanViewCreditServiceInvoices')">

        <div class="col-lg-12" v-if="isDayAlreadyStart">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h5 class="page_title">{{ $t('Sale.SaleInvoice') }}</h5>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Sale.Home') }}</a></li>
                                    <li class="breadcrumb-item active">
                                        {{ $t('Sale.SaleInvoice') }}
                                    </li>
                                </ol>
                            </div>

                            <div class="col-auto align-self-center">
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
                        <div class="col-lg-8" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-soft-primary" type="button" id="button-addon1">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" class="form-control" :placeholder="$t('Sale.SearchOfInvoice')"
                                       aria-label="Example text with button addon" aria-describedby="button-addon1, button-addon2">

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
                    <div class="row " v-if="advanceFilters">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('Sale.FromDate') }}</label>
                                <datepicker v-model="fromDate" v-bind:key="randerforempty" />
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('Sale.ToDate') }}</label>
                                <datepicker v-model="toDate" v-bind:key="randerforempty" />
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('Sale.User') }}</label>
                                <usersDropdown v-model="userId" ref="userDropdown" :alluser="true" />
                            </div>
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
                        <div class="col-lg-3" v-if="selected.length > 0">
                            <button type="button" class="btn btn-soft-primary dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">Bulk Action <i class="mdi mdi-chevron-down"></i></button>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="javascript:void(0);" v-on:click="SaveVerifiedInvoice(true)">Completed</a>
                                <a class="dropdown-item" href="javascript:void(0);" v-on:click="SaveVerifiedInvoice(false)">InCompleted</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item"
                            v-if="isValid('CanViewHoldInvoices')">
                            <a class="nav-link" v-bind:class="{active:active == 'Paid'}"
                               v-on:click="makeActive('Paid')" data-bs-toggle="tab" href="#Paid" role="tab" aria-selected="true">
                                {{ $t('Sale.Paid') }}
                            </a>
                        </li>
                        <li class="nav-item"
                            v-if="isValid('CanViewHoldInvoices')">
                            <a class="nav-link" v-bind:class="{active:active == 'Credit'}"
                               v-on:click="makeActive('Credit')" data-bs-toggle="tab" href="#Credit" role="tab" aria-selected="true">
                                {{ $t('Sale.Credit') }}
                            </a>
                        </li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div class="tab-pane pt-3" id="Paid" role="tabpanel" v-bind:class="{ active: active == 'Paid' }">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>

                                            <th>
                                                <label class="form-checkbox">
                                                    <input type="checkbox" v-model="selectAll" @click="select">
                                                    <i class="form-icon"></i>
                                                </label>
                                            </th>
                                            <th style="width:40px;">#</th>
                                            <th style="width:105px;">
                                                {{ $t('Sale.InvoiceNo') }}
                                            </th>
                                            <th style="width:100px;">
                                                {{ $t('Sale.Status') }}
                                            </th>
                                            <!--<th >
                                            {{ $t('Sale.Type') }}
                                        </th>-->
                                            <th style="width: 220px;">
                                                {{ $t('Sale.Date') }}
                                            </th>
                                            <th style="width: 220px;">
                                                {{ $t('Sale.CustomerName')}}
                                            </th>
                                            <th style="width: 120px;">
                                                {{ $t('Sale.CreatedBy') }}
                                            </th>
                                            <!--<th v-if="isDayStarts=='true'" >
                                            {{ $t('DailyExpense.CounterId') }}
                                        </th>-->
                                            <th style="width: 120px;">
                                                {{ $t('Sale.NetAmount') }}
                                            </th>
                                            <th style="width: 70px;" class="text-end">
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(sale,index) in saleList" v-bind:key="index">
                                            <td>
                                                <label class="form-checkbox">
                                                    <input type="checkbox" :value="sale.id" v-model="selected">
                                                    <i class="form-icon"></i>
                                                </label>
                                            </td>

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
                                                <span class="badge badge-boxed  badge-outline-success">{{($i18n.locale == 'en' ||isLeftToRight()) ? 'paid' : 'دفع'}}</span>
                                            </td>
                                            <td>
                                                <span v-if="sale.markAsCompleted" class="badge rounded-pill bg-primary">Completed</span>
                                                <span v-else class="badge rounded-pill bg-warning">InCompleted</span>
                                            </td>
                                            <td>
                                                {{getDate(sale.date)}}
                                            </td>
                                            <td >
                                                <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(sale.customerId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">{{sale.customerName}}</a>
                                            </td>
                                            <td>
                                                {{sale.userName}} <br />
                                                <span v-if="isDayStarts=='true'">{{sale.counterName}}</span>
                                            </td>
                                            <!--<td v-if="isDayStarts=='true'" >
                                            {{sale.counterName}}
                                        </td>-->
                                            <td>
                                                {{currency}} {{parseFloat(sale.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-end">

                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('Sale.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-if="isValid('CanDuplicateInvoices') && (isValid('CreditInvoices') || isValid('CashInvoices')) " v-on:click="DuplicateInvoice(sale.id)">{{ $t('Sale.DuplicateInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="ViewInvoice(sale.id)" v-if="isValid('CanViewInvoiceDetail') && !IsPaksitanClient">{{ $t('Sale.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="ViewInvoiceForPakistan(sale.id)" v-else-if="isValid('CanViewInvoiceDetail') && IsPaksitanClient">{{ $t('Sale.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoice(sale.id, 'print')" v-if="isValid('CanA4Print') ">{{ $t('Sale.A4Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoice(sale.id, 'download')" v-if="isValid('CanA4Print') ">{{ $t('Sale.PdfDownload') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoicePos(sale.id, sale.counterName)" v-if="isValid('CanPosPrint') ">{{ $t('Sale.POSPrint') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="sendEmail(sale.id)" v-if="isValid('CanSendSaleEmailAsLink') || isValid('CanSendSaleEmailAsPdf') ">{{ $t('Sale.Email') }}</a>

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
                                        <b-pagination pills size="sm" v-model="currentPage"
                                                      :total-rows="rowCount"
                                                      :per-page="10"
                                                      :first-text="$t('Table.First')"
                                                      :prev-text="$t('Table.Previous')"
                                                      :next-text="$t('Table.Next')"
                                                      :last-text="$t('Table.Last')">
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
                                            <th>
                                                <label class="form-checkbox">
                                                    <input type="checkbox" v-model="selectAll" @click="select">
                                                    <i class="form-icon"></i>
                                                </label>
                                            </th>
                                            <th style="width:40px;">#</th>
                                            <th style="width:105px;">
                                                {{ $t('Sale.InvoiceNo') }}
                                            </th>
                                            <th style="width:100px;">
                                                {{ $t('Sale.Status') }}
                                            </th>
                                            <!--<th >
                                            {{ $t('Sale.Type') }}
                                        </th>-->
                                            <th style="width: 220px;">
                                                {{ $t('Sale.Date') }}
                                            </th>
                                            <th style="width: 220px;" >
                                                {{ $t('Sale.CustomerName') }}
                                            </th>
                                            <th style="width: 120px;">
                                                {{ $t('Sale.CreatedBy') }}
                                            </th>
                                            <!--<th v-if="isDayStarts=='true'" >
                                            {{ $t('DailyExpense.CounterId') }}
                                        </th>-->
                                            <th style="width: 120px;">
                                                {{ $t('Sale.NetAmount') }}
                                            </th>
                                            <th style="width: 120px;" v-if="isValid('CanViewDueReceivePay')">
                                                Received
                                            </th>
                                            <th style="width: 120px;" v-if="isValid('CanViewDueReceivePay')">
                                                Due Amount
                                            </th>
                                            <th style="width: 70px;" class="text-end">
                                                <!--{{ $t('Sale.RePrint') }}-->
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(sale,index) in saleList" v-bind:key="index">
                                            <td>
                                                <label class="form-checkbox">
                                                    <input type="checkbox" :value="sale.id" v-model="selected">
                                                    <i class="form-icon"></i>
                                                </label>
                                            </td>
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
                                                <div class="badge badge-success" v-if="sale.partiallyInvoices==3">

                                                    {{($i18n.locale == 'en' ||isLeftToRight())?' Fully Paid':'مدفوعة بالكامل'}}
                                                </div>
                                            </td>
                                            <!--<td>
                                            <div class="badge badge-danger"  v-if="sale.partiallyInvoices==1">
                                                {{($i18n.locale == 'en' ||isLeftToRight())?'Un-Paid':'غير مدفوعة'}}
                                            </div>
                                            <div class="badge badge-primary"  v-if="sale.partiallyInvoices==2">
                                                {{($i18n.locale == 'en' ||isLeftToRight())?' Partially Paid':'المدفوعة جزئيا'}}

                                            </div>
                                            <div class="badge badge-success"  v-if="sale.partiallyInvoices==3">
                                                {{($i18n.locale == 'en' ||isLeftToRight())?' Fully Paid':'مدفوعة بالكامل'}}
                                            </div>
                                        </td>-->
                                            <td>
                                                <span v-if="sale.markAsCompleted" class="badge rounded-pill bg-primary">Completed</span>
                                                <span v-else class="badge rounded-pill bg-warning">InCompleted</span>
                                            </td>
                                            <td>
                                                {{getDate(sale.date)}}
                                            </td>
                                            <td >
                                                <a href="javascript:void(0)" v-on:click="ViewCustomerInfo(sale.customerId)" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">{{sale.customerName}}</a>
                                            </td>
                                            <td>
                                                {{sale.userName}}
                                            </td>
                                            <!--<td v-if="isDayStarts=='true'" >
                                            {{sale.counterName}}
                                        </td>-->
                                            <td>
                                                {{currency}} {{parseFloat(sale.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td v-if="isValid('CanViewDueReceivePay')">
                                                {{currency}} {{parseFloat(sale.receivedAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td v-if="isValid('CanViewDueReceivePay')">

                                                {{currency}} {{parseFloat(sale.dueAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                <div class="badge badge-danger" v-if="sale.dueAmount > 0 && sale.dueDate < sale.date">
                                                    Outdated
                                                </div>
                                            </td>
                                            <td class="text-end">
                                                <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('Sale.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="javascript:void(0)" v-if="isValid('CanDuplicateInvoices') && (isValid('CreditInvoices') || isValid('CashInvoices')) " v-on:click="DuplicateInvoice(sale.id)">{{ $t('Sale.DuplicateInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="ViewInvoice(sale.id)" v-if="isValid('CanViewInvoiceDetail') && !IsPaksitanClient">{{ $t('Sale.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="ViewInvoiceForPakistan(sale.id)" v-else-if="isValid('CanViewInvoiceDetail') && IsPaksitanClient">{{ $t('Sale.Action') }}{{ $t('Sale.ViewInvoice') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoice(sale.id, 'print')" v-if="isValid('CanA4Print') ">{{ $t('Sale.A4Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoice(sale.id, 'download')" v-if="isValid('CanA4Print') ">{{ $t('Sale.PdfDownload') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoicePos(sale.id, sale.counterName)" v-if="isValid('CanPosPrint') ">{{ $t('Sale.POSPrint') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="sendEmail(sale.id)" v-if="isValid('CanSendSaleEmailAsLink') || isValid('CanSendSaleEmailAsPdf') ">{{ $t('Sale.Email') }}</a>

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
                                        <b-pagination pills size="sm" v-model="currentPage"
                                                      :total-rows="rowCount"
                                                      :per-page="10"
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

            <usc-report-print :printDetails="printDetails" :isTouchScreen="sale" :headerFooter="headerFooter" v-if="printDetails.length != 0 && printSize=='A4' && printTemplate=='Template4' && isPrint && !download" v-bind:key="printRender" />
            <saleInvoice :printDetails="printDetails" :isTouchScreen="sale" :headerFooter="headerFooter" v-if="printDetails.length != 0 && printSize=='A4' && printTemplate=='Default' && isPrint && !download" v-bind:key="printRender" />
            
            <SaleInvoiceTemplate5 :printDetails="printDetails" :isTouchScreen="sale" :headerFooter="headerFooter" :saleSizeAssortment="saleSizeAssortment" v-if="printDetails.length != 0 && printSize=='A4' && printTemplate=='Template5' && isPrint && !download" v-bind:key="printRender" />
            <ObaagestSaleInvoice :printDetails="printDetails" :isTouchScreen="sale" :headerFooter="headerFooter" :saleSizeAssortment="saleSizeAssortment" v-if="printDetails.length != 0 && printSize=='A4' && printTemplate=='Template8' && isPrint && !download" v-bind:key="printRender" />
            <saleInvoice-template-one :printDetails="printDetails" :isTouchScreen="sale" :headerFooter="headerFooter" v-if="printDetails.length != 0 && printSize=='A4' && printTemplate=='Template1' && isPrint && !download" v-bind:key="printRender" />

            <SalesThermalpkReport :printDetail="printDetailsPos" :isTouchScreen="sale" :headerFooter="headerFooter" v-if="printDetailsPos.length != 0 && printSize=='Thermal' && printTemplate=='PkTemplate1'" v-bind:key="printRenderPos" />
            <SalesThermalpkReport2 :printDetail="printDetailsPos" :isTouchScreen="sale" :headerFooter="headerFooter" v-if="printDetailsPos.length != 0 && printSize=='Thermal' && printTemplate=='PkTemplate2'" v-bind:key="printRenderPos" />
            <SalesThermalSaudiReports3 :printDetail="printDetailsPos" :isTouchScreen="sale" :headerFooter="headerFooter" v-if="printDetailsPos.length != 0 && printSize=='Thermal' && printTemplate=='RetailSaTemplate1'" v-bind:key="printRenderPos" />
            <email-compose :show="emailComposeShow" v-if="emailComposeShow" @close="emailComposeShow = false" :documentId="saleId" :headerFooter="headerFooter" :formName="'Invoice'"></email-compose>
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


        <div class="offcanvas offcanvas-end px-0" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel">
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
                    <!--<div v-if="expandSale" class="col-lg-12 form-group text-center">
                    <h5 v-for="(sale,index) in saleList" v-bind:key="index"><a href="javascript:void(0);">{{sale.invoiceNo}}</a></h5>
                </div>-->
                </div>
            </div>
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
                randerforempty:0,
                saleSizeAssortment: [],
                saleId: '',
                customer: '',
                customerId: '',
                saleOrderId: '',
                colorVariants: false,
                IsPaksitanClient: false,
                isPrint: false,
                emailComposeShow: false,
                download: false,
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
                active: 'Paid',
                isDisabled: false,
                advanceFilters: false,
                printInterval: '',
                search: '',
                english: '',
                arabic: '',
                searchQuery: '',
                saleList: [],
                purchasePostList: [],
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
                amount: 0,
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
                isFifo: false,
                hold: 0,
                credit: 0,
                cash: 0,
                partially: 0,
                unPaid: 0,
                fullyPaid: 0,

                customerInformation: '',
                expandSale: false,
            }
        },
        computed: {
            isFilterButtonDisabled() {
      return (
        this.fromDate ||
        this.toDate ||
        this.userId
      );
    },
  },
        watch: {


            // search: function (search) {
            //     this.getData(search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId);
            // },
            // fromDate: function (fromDate) {
            //     this.counter++;
            //     if (this.counter != 1) {
            //         localStorage.setItem('fromDate', fromDate);
            //         this.getData(this.search, this.currentPage, this.active, fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId);
            //     }
            // },
            // toDate: function (toDate) {
            //     this.counter++;
            //     if (this.counter != 2) {
            //         localStorage.setItem('toDate', toDate);
            //         this.getData(this.search, this.currentPage, this.active, this.fromDate, toDate, this.fromTime, this.toTime, this.terminalId, this.userId);
            //     }
            // },
            // userId: function () {

            //     this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId);

            // },
        },
        methods: {
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
    this.terminalId = '';
    this.customerId = '';
    this.customerType = '';
    this.search="";
    this.randerforempty++;

    if (this.$refs.CustomerDropdown != undefined)
        this.$refs.CustomerDropdown.Remove();

    if (this.$refs.TerminalDropDown != undefined)
        this.$refs.TerminalDropDown.Remove();

}

this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId, this.customerId, this.customerType);

},

search22: function () {
    this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId);
            },

            clearData: function () {
                this.search="";
                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId);

            },



            ViewCustomerInfo: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Contact/ContactLedgerDetail?id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.customerInformation = response.data;
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },

            select: function () {
                this.selected = [];
                if (!this.selectAll) {
                    for (let i in this.saleList) {
                        this.selected.push(this.saleList[i].id);
                    }
                }
            },

            GotoPage: function (link) {
                this.$router.push({ path: link });
            },

            GetFilter: function () {
                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId);
            },

            getOriginId: function (value) {
                var originId = [];
                for (var i = 0; i < value.length; i++) {
                    originId[i] = value[i].id
                }
                return originId;
            },
            sendEmail: function (saleId) {
                this.saleId = saleId
                this.emailComposeShow = true;
            },
            //dateFilter: function (fromDate, toDate) {
            //
            //    this.getData(this.search, this.currentPage, this.active, fromDate, toDate);
            //},
            //DeleteFile: function () {
            //    var root = this;
            //    var token = '';
            //    if (token == '') {
            //        token = localStorage.getItem('token');
            //    }
            //    this.$https.get('/Sale/DeleteSale', this.selected, { headers: { "Authorization": `Bearer ${token}` } })
            //        .then(function (response) {
            //            if (response.data != null) {
            //                if (response.data.message.id != '00000000-0000-0000-0000-000000000000') {
            //                    root.$swal({
            //                        title: 'Deleted!',
            //                        text: response.data.message.isAddUpdate,
            //                        type: 'success',
            //                        confirmButtonClass: "btn btn-success",
            //                        buttonsStyling: false
            //                    }).then(function (result) {
            //                        if (result) {
            //                            root.$router.push('/purchase');
            //                        }
            //                    });
            //                } else {
            //                    root.$swal({
            //                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
            //                        text: response.data.message.isAddUpdate,
            //                        type: 'error',
            //                        confirmButtonClass: "btn btn-danger",
            //                        buttonsStyling: false
            //                    });
            //                }
            //            }
            //        },
            //            function () {

            //                root.$swal({
            //                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
            //                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update UnSuccessfully' : 'التحديث غير ناجح',
            //                    type: 'error',
            //                    confirmButtonClass: "btn btn-danger",
            //                    buttonsStyling: false
            //                });
            //            });
            //},

            //select: function () {
            //    this.selected = [];
            //    if (!this.selectAll) {
            //        for (let i in this.saleList) {
            //            this.selected.push(this.saleList[i].id);
            //        }
            //    }
            //},
            GetAmountFilter: function () {
                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId);

            },
            GetUserId: function (x) {
                this.userId = x.id;
            }
            , getDate: function (date) {
                return moment(date).format('ll');
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
            isDayStart: function () {
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loading = true;
                root.$https.get('/Product/IsDayStart?userId=' + this.UserID + '&employeeId=' + this.employeeId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isDayStart == true) {
                            root.isDayAlreadyStart = true;
                            localStorage.setItem('token', response.data.token);
                        } else {
                            root.isDayAlreadyStart = false;
                        }
                        root.loading = false;

                    });
            },

            EditSale: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/SaleDetail?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&colorVariants=' + this.colorVariants, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addSale',
                                query: {
                                    data: '',
                                    Add: false,
                                    page: root.currentPage
                                }

                            })
                        }
                        else {
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

                root.$https.get('/Sale/SaleDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/TouchScreen',
                                query: { data: '', Add: false }
                            })
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },


            DuplicateInvoice: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/SaleDetail?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&colorVariants=' + this.colorVariants, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            response.data.isDuplicate = true;
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/addSale',
                                query: { data: '',Add:false, token_name: 'Sales_token', fromDashboard: 'true', isDuplicate: 'true' }
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

                root.$https.get('/Sale/SaleDetail?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&colorVariants=' + this.colorVariants, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/InvoiceView',
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
            ViewInvoiceForPakistan: function (id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/SaleDetail?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&colorVariants=' + this.colorVariants, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/InvoiceViewFroPaksitaniClient',
                                query: {
                                    data: '',
                                    Add: false,
                                    headerFooter: root.headerFooter,
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

                root.$https.get('/Sale/SaleDetail?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&colorVariants=' + this.colorVariants, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/SalePaymentDetail',
                                query: { data: '', Add: false }
                            })
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },

            AdvanceFilter: function () {


                this.advanceFilters = !this.advanceFilters;
                if (this.advanceFilters == false) {
                    this.fromTime = '';
                    this.toTime = '';
                    this.userId = '';
                    this.terminalId = '';
                    this.amount = '';
                    this.saleOrderId = '';
                    this.customerId = '';
                    this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId);

                }



            },
            makeActive: function (item) {

                this.active = item;
                this.selectAll = false;
                this.selected = [];
                this.getData(this.search, 1, item, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId);
            },
            getPage: function () {

                this.getData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.terminalId, this.userId);
            },
            getData: function (search, currentPage, status, fromDate, toDate, fromTime, toTime, terminalId, userId) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                localStorage.setItem('currentPage', this.currentPage);
                localStorage.setItem('active', this.active);
                var branchId = localStorage.getItem('BranchId');


                this.$https.get('/Sale/SaleList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&fromDate=' + fromDate + '&toDate=' + toDate + '&fromTime=' + fromTime + '&toTime=' + toTime + '&terminalId=' + terminalId + '&userId=' + userId + '&customerId=' + this.customerId + '&saleOrderId=' + this.saleOrderId + '&amount=' + this.amount + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        
                        if (response.data != null) {
                            root.currentPage = response.data.currentPage;
                            root.pageCount = response.data.pageCount;
                            root.rowCount = response.data.rowCount;
                            root.saleList = response.data.results.sales;
                            //root.hold = response.data.results.hold;
                            //root.credit = response.data.results.credit;
                            //root.cash = response.data.results.cash;
                            //root.partially = response.data.results.partially;
                            //root.unPaid = response.data.results.unPaid;
                            //root.fullyPaid = response.data.results.fullyPaid;
                            root.currentPage = currentPage;
                            root.rendr++;
                        }
                    }).catch(error => {
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                    });
            },

            SaveVerifiedInvoice: function (status) {
                var root = this;

                var url = '/Sale/SaveVerifiedInvoice?status=' + status
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .post(url, root.selected, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.getPage();
                            root.selectAll = false;
                            root.selected = [];

                            root.$swal({
                                text: root.$t('AddCurrency.Saved'),
                                title: root.$t('AddCurrency.SavedSuccessfully'),
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                        }
                        else {
                            root.$swal({
                                title: root.$t('Error'),
                                text: root.$t('NameAlreadyExist'),
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                        }
                    });
            },


            AddSale: function () {
                this.$router.push('/addSale');
            },
            getBase64Image: function (path) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';

                if (path != null && path != '' && path != undefined) {
                    root.$https.get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        }

                    });
                }


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


                            if (root.overWrite) {

                                root.headerFooter.company.nameArabic = root.businessNameArabic;
                                root.headerFooter.company.nameEnglish = root.businessNameEnglish;
                                root.headerFooter.company.categoryArabic = root.businessTypeArabic;
                                root.headerFooter.company.categoryEnglish = root.businessTypeEnglish;

                                root.headerFooter.company.companyNameArabic = root.companyNameArabic;
                                root.headerFooter.company.companyNameEnglish = root.companyNameEnglish;
                                root.headerFooter.company.logoPath = root.businessLogo;

                            }
                            root.getBase64Image(root.headerFooter.company.logoPath);
                        }
                    });
            },
            PrintInvoice: function (value, prop) {
                var root = this;
                root.download = false;
                root.isPrint = false;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.printDetailsPos = [];
                root.$https.get("/Sale/SaleDetail?id=" + value + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&colorVariants=' + this.colorVariants, { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {

                            root.printDetails = response.data;

                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                root.printDetails.saleItems.forEach(function (x) {
                                    x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                    x.newQuantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                root.printDetails.saleItems.forEach(function (x) {
                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.newQuantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }

                            if (prop == 'download') {
                                root.download = true;
                                root.isPrint = false;
                            }
                            else {
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
                            root.headerFooter.warrantyImage = response.data.footerImageForPrint;
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
                root.$https.get("/Sale/SaleDetail?id=" + value + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&colorVariants=' + this.colorVariants, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            root.printDetailsPos = response.data;
                            root.printDetailsPos.counterName = counterName;
                            root.printRenderPos++;
                        }
                    });
            },

            GetSizeData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/SizeList?isActive=true' + '&isVariance=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.saleSizeAssortment = [];
                        response.data.results.sizes.forEach(function (item) {
                            root.saleSizeAssortment.push({
                                sizeId: item.id,
                                name: item.name,
                                quantity: 0,
                            });

                        });

                    }
                });
            },
        },
        created: function () {

            this.GetHeaderDetail();
            this.GetSizeData();

            if (this.$route.query.data == 'AddSales') {
                this.$emit('update:modelValue', 'AddSales');

            }
            else {
                this.$emit('update:modelValue', this.$route.name);

            }
        },
        mounted: function () {
            this.IsPaksitanClient = localStorage.getItem('IsPaksitanClient') == "true" ? true : false;

            this.overWrite = localStorage.getItem('overWrite') == 'true' ? true : false;
            this.colorVariants = localStorage.getItem('ColorVariants') == 'true' ? true : false;

            this.businessLogo = localStorage.getItem('BusinessLogo');
            this.businessNameArabic = localStorage.getItem('BusinessNameArabic');
            this.businessNameEnglish = localStorage.getItem('BusinessNameEnglish');
            this.businessTypeArabic = localStorage.getItem('BusinessTypeArabic');
            this.businessTypeEnglish = localStorage.getItem('BusinessTypeEnglish');
            this.companyNameArabic = localStorage.getItem('CompanyNameArabic');
            this.companyNameEnglish = localStorage.getItem('CompanyNameEnglish');

            this.bankDetail = localStorage.getItem('BankDetail') == 'true' ? true : false;
            if (localStorage.getItem('fromDate') != null && localStorage.getItem('fromDate') != '' && localStorage.getItem('fromDate') != undefined) {
                this.fromDate = localStorage.getItem('fromDate');

            }
            else {
                this.fromDate = moment().add(-7, 'days').format("DD MMM YYYY");

            }
            if (localStorage.getItem('toDate') != null && localStorage.getItem('toDate') != '' && localStorage.getItem('toDate') != undefined) {
                this.toDate = localStorage.getItem('toDate');

            }
            else {
                this.toDate = moment().format("DD MMM YYYY");
            }
            var IsDayStart = localStorage.getItem('DayStart');
            this.isDayStarts = localStorage.getItem('DayStart');
            var IsDayStartOn = localStorage.getItem('IsDayStart');
            this.printTemplate = localStorage.getItem('PrintTemplate');
            this.printSize = localStorage.getItem('PrintSizeA4');
            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            var batch = localStorage.getItem('openBatch')
            if (batch != undefined && batch != null && batch != "") {
                this.openBatch = batch
            }
            else {
                this.openBatch = 1
            }

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

                }
                else {
                    if (this.isValid('CanViewPaidInvoices')) {
                        this.active = 'Paid';
                    }
                    else if (this.isValid('CanViewCreditInvoices')) {
                        this.active = 'Credit';
                    }

                    this.getPage();

                }
                this.currency = localStorage.getItem('currency');
                this.companyId = localStorage.getItem('CompanyID');
                //this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
                //this.headerFooter.footerAr = localStorage.getItem('TermsInAr');
            }
            else {
                if (AllowAll == 'true') {
                    this.isDayAlreadyStart = true;
                    this.english = localStorage.getItem('English');
                    this.arabic = localStorage.getItem('Arabic');
                    this.search = '';
                    if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                        this.currentPage = parseInt(localStorage.getItem('currentPage'));

                        this.getPage();

                    }
                    else {
                        if (this.isValid('CanViewPaidInvoices')) {
                            this.active = 'Paid';
                        }
                        else if (this.isValid('CanViewCreditInvoices')) {
                            this.active = 'Credit';
                        }

                        this.getPage();

                    }
                    this.currency = localStorage.getItem('currency');
                    this.companyId = localStorage.getItem('CompanyID');
                    //this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
                    //this.headerFooter.footerAr = localStorage.getItem('TermsInAr');
                }
                else if (IsDayStartOn == 'true') {
                    this.isDayAlreadyStart = true;
                    this.english = localStorage.getItem('English');
                    this.arabic = localStorage.getItem('Arabic');
                    this.search = '';
                    if (localStorage.getItem('active') != null && localStorage.getItem('active') != '' && localStorage.getItem('active') != undefined) {
                        if (localStorage.getItem('currentPage') != null && localStorage.getItem('currentPage') != '' && localStorage.getItem('currentPage') != undefined) {
                            this.currentPage = parseInt(localStorage.getItem('currentPage'));

                        }
                        else {
                            this.currentPage = 1;
                        }
                        this.getPage();

                    }
                    else {
                        if (this.isValid('CanViewPaidInvoices')) {
                            this.active = 'Paid';
                        }
                        else if (this.isValid('CanViewCreditInvoices')) {
                            this.active = 'Credit';
                        }

                        this.getPage();

                    }
                    this.currency = localStorage.getItem('currency');
                    this.companyId = localStorage.getItem('CompanyID');
                    //this.headerFooter.footerEn = localStorage.getItem('TermsInEng');
                    //this.headerFooter.footerAr = localStorage.getItem('TermsInAr');
                }
                else {

                    this.CompanyID = localStorage.getItem('CompanyID');
                    this.UserID = localStorage.getItem('UserID');
                    this.employeeId = localStorage.getItem('EmployeeId');
                    this.isDayAlreadyStart = false;
                }
            }


        },

        updated: function () {
            if (this.selected.length < this.saleList.length) {
                this.selectAll = false;
            } else if (this.selected.length == this.saleList.length) {
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