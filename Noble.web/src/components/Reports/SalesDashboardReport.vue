<template>
    <div v-if="isValid('CanViewSaleInvoiceReport')">
        <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-6 col-lg-6">
                            <h5 class="ml-3 DayHeading">{{ $t('SalesDashboardReport.SalesDashboardReport') }}</h5>
                        </div>
                        <div class="col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">

                            <router-link :to="'/AllReports'">
                                <a href="javascript:void(0)" class="btn btn-outline-primary  "><i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                            </router-link>
                            <!--<a href="javascript:void(0)" class="btn btn-outline-primary" v-on:click="PrintCsv">Export CSV</a>-->
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label v-if="formName=='Customer'">{{ $t('CustomerBalanceReport.CustomerName') }}</label>
                                    <label v-if="formName=='Supplier'">{{ $t('CustomerBalanceReport.SupplierName') }}</label>
                                    <accountdropdown v-model="contactId" :formName="formName" @update:modelValue="valueChange(contactId)" :modelValue="contactId" :key="render" />
                                </div>
                            </div>
                            <div class="col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label>{{ $t('BalanceSheetReport.FromDate') }}</label>
                                    <datepicker v-model="fromDate" />
                                </div>
                            </div>
                            <div class="col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label>{{ $t('BalanceSheetReport.ToDate') }}</label>
                                    <datepicker v-model="toDate" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-12">


                            <ul class="nav nav-tabs" data-tabs="tabs">
                                <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'Invoices'}" v-on:click="makeActive('Invoices')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">{{ $t('SalesDashboardReport.Invoices') }}</a></li>
                                <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'Payments'}" v-on:click="makeActive('Payments')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('SalesDashboardReport.Payments') }}</a></li>

                            </ul>


                            <div class="tab-content mt-3" id="nav-tabContent">
                                <div v-if="active == 'Invoices'">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class=" mb-1">
                                                    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'arabicLanguage'">
                                                        <ul class="nav nav-tabs" data-tabs="tabs">
                                                            <li class="nav-item" v-if="isValid('CanViewHoldInvoices')"><a class="nav-link" v-bind:class="{active:activeTab == 'Hold'}" v-on:click="makeActiveTabs('Hold')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">{{ $t('Sale.Hold') }} <span class="badge badge-danger">{{hold}}</span> </a></li>
                                                            <li class="nav-item" v-if="isValid('CanViewPaidInvoices')"><a class="nav-link" v-bind:class="{active:activeTab == 'Paid'}" v-on:click="makeActiveTabs('Paid')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('Sale.Paid') }} <span class="badge badge-success">{{paid}}</span></a></li>
                                                            <li class="nav-item" v-if="isValid('CanViewCreditInvoices')"><a class="nav-link" v-bind:class="{active:activeTab == 'Credit'}" v-on:click="makeActiveTabs('Credit')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('Sale.Credit') }} <span class="badge badge-primary">{{credit}}</span></a></li>

                                                        </ul>
                                                    </div>
                                                </div>
                                                <div class="tab-content mt-3" id="nav-tabContent">
                                                    <div v-if="activeTab == 'Hold'">
                                                        <div class="table-responsive">
                                                            <table class="table table-hover table-striped table_list_bg">
                                                                <thead class="">
                                                                    <tr>
                                                                        <th  >#</th>
                                                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{ $t('Sale.InvoiceNo') }}
                                                                        </th>


                                                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{ $t('Sale.Date') }}
                                                                        </th>
                                                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="english=='true'">
                                                                            {{$englishLanguage($t('Sale.CustomerName'))  }}
                                                                        </th>
                                                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="isOtherLang()">
                                                                            {{$arabicLanguage($t('Sale.CustomerNameArabic'))  }}
                                                                        </th>
                                                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{ $t('Sale.CreatedBy') }}
                                                                        </th>
                                      
                                                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{ $t('Sale.NetAmount') }}
                                                                        </th>
                                                                        <th class="text-center">
                                                                            {{ $t('Sale.RePrint') }}
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr v-for="(sale,index) in holdList" v-bind:key="index">
                                                                        <td v-if="currentPage === 1">
                                                                            {{index+1}}
                                                                        </td>
                                                                        <td v-else>
                                                                            {{((currentPage*10)-10) +(index+1)}}
                                                                        </td>
                                                                       
                                                                        <td  v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{sale.registrationNumber}}
                                                                            <span class="small badge badge-success">{{sale.isCredit ? 'Credit':'Cash'}}</span>
                                                                        </td>
                                                                        <!--<td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{sale.isCredit ? 'Credit':'Cash'}}
                                                                        </td>-->

                                                                       
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{getDate(sale.date)}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="english=='true'">
                                                                            {{sale.customerName}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="isOtherLang()">
                                                                            {{sale.customerNameArabic}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{sale.userName}} <br />
                                                                            <!--<span v-if="isDayStarts=='true'">{{sale.counterName}}</span>-->
                                                                        </td>
                                                                        <!--<td v-if="isDayStarts=='true'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{sale.counterName}}
                                                                        </td>-->
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{currency}}  {{parseFloat(sale.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                                                            <button class="btn btn-primary btn-sm btn-icon" v-on:click="DuplicateInvoice(sale.id)" v-if="isValid('CanDuplicateInvoices') && (isValid('CreditInvoices') || isValid('CashInvoices')) "><i class="fas fa-copy"></i></button>
                                                                            <button class="btn btn-primary btn-sm btn-icon mr-1  ml-1" v-on:click="ViewInvoice(sale.id)" v-if="isValid('CanViewInvoiceDetail') "><i class="fas fa-eye"></i></button>
                                                                            <a href="javascript:void(0)" title="A4" class="btn btn-primary btn-sm btn-icon " v-on:click="PrintInvoice(sale.id)" v-if="isValid('CanA4Print') "><i class=" fa fa-print"></i></a>
                                                                            <a href="javascript:void(0)" title="POS" class="btn btn-primary btn-sm btn-icon ml-1 mr-1" v-on:click="PrintInvoicePos(sale.id, sale.counterName)" v-if="isValid('CanPosPrint') "><i class=" fa fa-print"></i></a>
                                                                            <button class="btn btn-danger btn-sm btn-icon " @click="RemoveSale(sale.id)" v-if="isValid('CanDeleteHoldInvoices') "><i class="fa fa-trash"></i></button>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="font-size:15px;font-weight:bold;background-color:azure">
                                                                        <td colspan="7" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'" style="padding-top:60px">
                                                                            {{ $t('InvoicePrintReport.Total') }}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" style="padding-top:60px">
                                                                            {{currency}} {{parseFloat(holdListTotal).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

                                                                        </td>

                                                                        <td class="text-center" style="padding-top:60px">
                                                                        </td>


                                                                    </tr>
                                                                </tbody>
                                                            </table>

                                                        </div>




                                                    </div>
                                                    <div v-if="activeTab == 'Paid'">
                                                        <div class="table-responsive">
                                                            <table class="table table-hover table-striped table_list_bg">
                                                                <thead class="">
                                                                    <tr>
                                                                        <th >#</th>
                                                                        <th  v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{ $t('Sale.InvoiceNo') }}
                                                                        </th>
                                                                        
                                                                      
                                                                        <th  v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{ $t('Sale.Date') }}
                                                                        </th>
                                                                        <th  v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="english=='true'">
                                                                            {{$englishLanguage($t('Sale.CustomerName'))  }}
                                                                        </th>
                                                                        <th  v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="isOtherLang()">
                                                                            {{$arabicLanguage($t('Sale.CustomerNameArabic'))  }}
                                                                        </th>
                                                                        <th  v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{ $t('Sale.CreatedBy') }}
                                                                        </th>
                                                                        <!--<th v-if="isDayStarts=='true'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{ $t('DailyExpense.CounterId') }}
                                                                        </th>-->
                                                                        <th  v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{ $t('Sale.NetAmount') }}
                                                                        </th>
                                                                        <th  class="text-center">
                                                                            {{ $t('Sale.RePrint') }}
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr v-for="(sale,index) in paidList" v-bind:key="index">
                                                                        <td v-if="currentPage === 1">
                                                                            {{index+1}}
                                                                        </td>
                                                                        <td v-else>
                                                                            {{((currentPage*10)-10) +(index+1)}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            
                                                                                {{sale.registrationNumber}}
                                                                             <br />
                                                                            <span class="small badge badge-success">{{($i18n.locale == 'en' ||isLeftToRight()) ? 'paid' : 'دفع'}}</span>
                                                                        </td>
                                                                       
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{getDate(sale.date)}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="english=='true'">
                                                                            {{sale.customerName}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="isOtherLang()">
                                                                            {{sale.customerNameArabic}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{sale.userName}} <br />
                                                                            <!--<span v-if="isDayStarts=='true'">{{sale.counterName}}</span>-->
                                                                        </td>
                                                                        <!--<td v-if="isDayStarts=='true'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{sale.counterName}}
                                                                        </td>-->
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{currency}} {{parseFloat(sale.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" >
                                                                            <button class="btn btn-primary btn-sm btn-icon" v-on:click="DuplicateInvoice(sale.id)" v-if="isValid('CanDuplicateInvoices') && (isValid('CreditInvoices') || isValid('CashInvoices'))"><i class="fas fa-copy"></i></button>
                                                                            <button class="btn btn-primary btn-sm btn-icon mr-1 ml-1" v-on:click="ViewInvoice(sale.id)" v-if="isValid('CanViewInvoiceDetail') "><i class="fas fa-eye"></i></button>
                                                                            <a href="javascript:void(0)" title="A4" class="btn btn-primary btn-sm btn-icon" v-on:click="PrintInvoice(sale.id)" v-if="isValid('CanA4Print') "><i class=" fa fa-print"></i></a>
                                                                            <a href="javascript:void(0)" title="POS" class="btn btn-primary btn-sm btn-icon ml-1 mr-1" v-on:click="PrintInvoicePos(sale.id,sale.counterName)" v-if="isValid('CanPosPrint') "><i class=" fa fa-print"></i></a>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="font-size:15px;font-weight:bold;background-color:azure">
                                                                        <td colspan="7" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'" style="padding-top:60px">
                                                                            {{ $t('InvoicePrintReport.Total') }}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" style="padding-top:60px">
                                                                            {{currency}} {{parseFloat(paidListTotal).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

                                                                        </td>

                                                                        <td class="text-center" style="padding-top:60px">
                                                                        </td>


                                                                    </tr>
                                                                </tbody>
                                                            </table>

                                                        </div>




                                                    </div>
                                                    <div v-if="activeTab == 'Credit'">
                                                        <div class="table-responsive">
                                                            <table class="table table-hover table-striped table_list_bg">
                                                                <thead class="">
                                                                    <tr>
                                                                        <th >#</th>
                                                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{ $t('Sale.InvoiceNo') }}
                                                                        </th>


                                                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{ $t('Sale.Date') }}
                                                                        </th>
                                                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="english=='true'">
                                                                            {{$englishLanguage($t('Sale.CustomerName'))  }}
                                                                        </th>
                                                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="isOtherLang()">
                                                                            {{$arabicLanguage($t('Sale.CustomerNameArabic'))  }}
                                                                        </th>
                                                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{ $t('Sale.CreatedBy') }}
                                                                        </th>
                                                                   
                                                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{ $t('Sale.NetAmount') }}
                                                                        </th>
                                                                        <th class="text-center">
                                                                            {{ $t('Sale.RePrint') }}
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr v-for="(sale,index) in creditList" v-bind:key="index">
                                                                        <td v-if="currentPage === 1">
                                                                            {{index+1}}
                                                                        </td>
                                                                        <td v-else>
                                                                            {{((currentPage*10)-10) +(index+1)}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                           
                                                                                {{sale.registrationNumber}}
                                                                             <br />
                                                                            <div class="badge badge-danger" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="sale.partiallyInvoices==1">
                                                                                {{($i18n.locale == 'en' ||isLeftToRight())?'Un-Paid':'غير مدفوعة'}}
                                                                            </div>
                                                                            <div class="badge badge-primary" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="sale.partiallyInvoices==2">
                                                                                {{($i18n.locale == 'en' ||isLeftToRight())?' Partially Paid':'المدفوعة جزئيا'}}

                                                                            </div>
                                                                            <div class="badge badge-success" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="sale.partiallyInvoices==3">

                                                                                {{($i18n.locale == 'en' ||isLeftToRight())?' Fully Paid':'مدفوعة بالكامل'}}
                                                                            </div>
                                                                        </td>
                                                                        <!--<td>
                                                                            <div class="badge badge-danger" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="sale.partiallyInvoices==1">
                                                                                {{($i18n.locale == 'en' ||isLeftToRight())?'Un-Paid':'غير مدفوعة'}}
                                                                            </div>
                                                                            <div class="badge badge-primary" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="sale.partiallyInvoices==2">
                                                                                {{($i18n.locale == 'en' ||isLeftToRight())?' Partially Paid':'المدفوعة جزئيا'}}

                                                                            </div>
                                                                            <div class="badge badge-success" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="sale.partiallyInvoices==3">
                                                                                {{($i18n.locale == 'en' ||isLeftToRight())?' Fully Paid':'مدفوعة بالكامل'}}
                                                                            </div>
                                                                        </td>-->
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{sale.saleOrderNo==null? '---':sale.saleOrderNo}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{getDate(sale.date)}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="english=='true'">
                                                                            {{sale.customerName}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-if="isOtherLang()">
                                                                            {{sale.customerNameArabic}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{sale.userName}}
                                                                        </td>
                                                                        <!--<td v-if="isDayStarts=='true'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{sale.counterName}}
                                                                        </td>-->
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            {{currency}} {{parseFloat(sale.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                                                            <button class="btn btn-primary btn-sm btn-icon" v-on:click="DuplicateInvoice(sale.id)" v-if="isValid('CanDuplicateInvoices') && (isValid('CreditInvoices') || isValid('CashInvoices')) "><i class="fas fa-copy"></i></button>
                                                                            <button class="btn btn-primary btn-sm btn-icon ml-1 mr-1" v-on:click="ViewInvoice(sale.id)" v-if="isValid('CanViewInvoiceDetail') "><i class="fas fa-eye"></i></button>
                                                                            <a href="javascript:void(0)" title="A4" class="btn btn-primary btn-sm btn-icon" v-on:click="PrintInvoice(sale.id)" v-if="isValid('CanA4Print') "><i class=" fa fa-print"></i></a>
                                                                            <a href="javascript:void(0)" title="POS" class="btn btn-primary btn-sm btn-icon ml-1 mr-1" v-on:click="PrintInvoicePos(sale.id,sale.counterName)" v-if="isValid('CanPosPrint') "><i class=" fa fa-print"></i></a>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="font-size:15px;font-weight:bold;background-color:azure">
                                                                        <td colspan="7" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'" style="padding-top:60px">
                                                                            {{ $t('InvoicePrintReport.Total') }}
                                                                        </td>
                                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" style="padding-top:60px">
                                                                            {{currency}} {{parseFloat(creditListTotal).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                        </td>

                                                                        <td class="text-center" style="padding-top:60px">
                                                                        </td>


                                                                    </tr>
                                                                </tbody>
                                                            </table>

                                                        </div>




                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>


                                </div>
                                <div v-if="active == 'Payments'">
                                    
                                    <div class="col-lg-12" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">


                                        <ul class="nav nav-tabs" data-tabs="tabs">
                                            <li class="nav-item" v-if="(isValid('CanDraftPettyCash') ) || (isValid('CanDraftCPR') ) ||(isValid('CanDraftSPR') )"><a class="nav-link" v-bind:class="{active:activeTab == 'Draft'}" v-on:click="makeActiveTabs('Draft')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">{{ $t('PaymentVoucherList.Draft') }} <span class="badge badge-danger">{{hold}}</span></a></li>
                                            <li class="nav-item" v-if="  (isValid('CanViewPettyCash')) || (isValid('CanViewCPR') )||(isValid('CanViewSPR') ) " v-on:click="makeActiveTabs('Approved')"><a class="nav-link" v-bind:class="{active:activeTab == 'Approved'}" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('PaymentVoucherList.Post') }} <span class="badge badge-success">{{paid}}</span></a></li>

                                        </ul>
                                        <div v-if="activeTab == 'Draft'">
                                            

                                            <div class=" table-responsive">
                                                <table class="table table-hover table-striped table_list_bg mt-3 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                    <thead class="">
                                                        <tr>

                                                            <th class="text-center">#</th>
                                                            <th class="text-center">
                                                                {{ $t('PaymentVoucherList.VoucherNumber') }}
                                                            </th>
                                                            <th class="text-center" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                                {{ $t('PaymentVoucherList.CreatedDate') }}
                                                            </th>
                                                            <th class="text-center">
                                                                {{ $t('PaymentVoucherList.DraftBy') }}
                                                            </th>
                                                            <th class="text-center">
                                                                {{ $t('PaymentVoucherList.PaymentMode') }}
                                                            </th>
                                                            <th class="text-center">
                                                                {{ $t('PaymentVoucherList.BankCashAccount') }}

                                                            </th>
                                                            <th class="text-center">
                                                                <span v-if="fm=='CashReceipt' || fm=='BankReceipt'|| fm=='PettyCash'">
                                                                    {{ $t('PaymentVoucherList.CustomerAccount') }}
                                                                </span>
                                                                <span v-if="fm=='BankPay' || fm=='CashPay'">
                                                                    {{ $t('PaymentVoucherList.SupplierAccount') }}
                                                                </span>
                                                            </th>
                                                            <th class="text-center">
                                                                {{ $t('PaymentVoucherList.NetAmount') }}
                                                            </th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr v-for="(voucher,index) in holdList" v-bind:key="voucher.id">

                                                            <td class="text-center" v-if="currentPage === 1">
                                                                {{index+1}}
                                                            </td>
                                                            <td class="text-center" v-else>
                                                                {{((currentPage*10)-10) +(index+1)}}
                                                            </td>


                                                            <td class="text-center">
                                                                {{voucher.voucherNumber}}
                                                            </td>
                                                            <td class="text-center">
                                                                {{getDate(voucher.date)}}
                                                            </td>
                                                            <td class="text-center">
                                                                {{voucher.draftBy}}
                                                            </td>
                                                            <td class="text-center">
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
                                                            <td class="text-center">
                                                                {{voucher.bankCashAccountName}}
                                                            </td>
                                                            <td class="text-center">
                                                                {{voucher.contactAccountName}}
                                                            </td>

                                                            <td class="text-center">
                                                                {{currency}}  {{parseFloat(voucher.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                            </td>

                                                        </tr>
                                                        <tr style="font-size:15px;font-weight:bold;background-color:azure">
                                                            <td colspan="7" class="text-center" style="padding-top:60px">
                                                                {{ $t('InvoicePrintReport.Total') }}
                                                            </td>
                                                            <td class="text-center" style="padding-top:60px">
                                                                {{currency}} {{parseFloat(holdListTotal).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

                                                            </td>

                                                            <td class="text-center" style="padding-top:60px">
                                                            </td>


                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>

                                        </div>
                                        <div v-if="activeTab == 'Approved'">
                                            <table class="table table-hover table-striped table_list_bg mt-3 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                <thead class="">
                                                    <tr>

                                                        <th class="text-center">#</th>
                                                        <th class="text-center">
                                                            {{ $t('PaymentVoucherList.VoucherNumber') }}
                                                        </th>
                                                        <th class="text-center" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                            {{ $t('PaymentVoucherList.CreatedDate') }}
                                                        </th>
                                                        <th class="text-center">
                                                            {{ $t('PaymentVoucherList.DraftBy') }}
                                                        </th>
                                                        <th class="text-center">
                                                            {{ $t('PaymentVoucherList.PaymentMode') }}
                                                        </th>
                                                        <th class="text-center">
                                                            {{ $t('PaymentVoucherList.BankCashAccount') }}

                                                        </th>
                                                        <th class="text-center">
                                                            <span v-if="fm=='CashReceipt' || fm=='BankReceipt'|| fm=='PettyCash'">
                                                                {{ $t('PaymentVoucherList.CustomerAccount') }}
                                                            </span>
                                                            <span v-if="fm=='BankPay' || fm=='CashPay'">
                                                                {{ $t('PaymentVoucherList.SupplierAccount') }}
                                                            </span>
                                                        </th>
                                                        <th class="text-center">
                                                            {{ $t('PaymentVoucherList.NetAmount') }}
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(voucher,index) in paidList" v-bind:key="voucher.id">

                                                        <td class="text-center" v-if="currentPage === 1">
                                                            {{index+1}}
                                                        </td>
                                                        <td class="text-center" v-else>
                                                            {{((currentPage*10)-10) +(index+1)}}
                                                        </td>


                                                        <td class="text-center">
                                                            {{voucher.voucherNumber}}
                                                        </td>
                                                        <td class="text-center">
                                                            {{getDate(voucher.date)}}
                                                        </td>
                                                        <td class="text-center">
                                                            {{voucher.draftBy}}
                                                        </td>
                                                        <td class="text-center">
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
                                                        <td class="text-center">
                                                            {{voucher.bankCashAccountName}}
                                                        </td>
                                                        <td class="text-center">
                                                            {{voucher.contactAccountName}}
                                                        </td>

                                                        <td class="text-center">
                                                            {{currency}}  {{parseFloat(voucher.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                        </td>

                                                    </tr>
                                                    <tr style="font-size:15px;font-weight:bold;background-color:azure">
                                                        <td colspan="7" class="text-center" style="padding-top:60px">
                                                            {{ $t('InvoicePrintReport.Total') }}
                                                        </td>
                                                        <td class="text-center" style="padding-top:60px">
                                                            {{currency}} {{parseFloat(holdListTotal).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

                                                        </td>

                                                        <td class="text-center" style="padding-top:60px">
                                                        </td>


                                                    </tr>
                                                </tbody>
                                            </table>

                                        </div>
                                        <div v-if="active == 'Rejected'">
                                            <table class="table table-hover table-striped table_list_bg mt-3 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                <thead class="">
                                                    <tr>

                                                        <th class="text-center">#</th>
                                                        <th class="text-center">
                                                            {{ $t('PaymentVoucherList.VoucherNumber') }}
                                                        </th>
                                                        <th class="text-center" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                            {{ $t('PaymentVoucherList.CreatedDate') }}
                                                        </th>
                                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                                            {{ $t('PaymentVoucherList.DraftBy') }}
                                                        </th>
                                                        <th>
                                                            {{ $t('PaymentVoucherList.PaymentMode') }}
                                                        </th>
                                                        <th>
                                                            {{ $t('PaymentVoucherList.BankCashAccount') }}

                                                        </th>
                                                        <th>
                                                            <span v-if="fm=='CashReceipt' || fm=='BankReceipt'|| fm=='PettyCash'">
                                                                {{ $t('PaymentVoucherList.CustomerAccount') }}
                                                            </span>
                                                            <span v-if="fm=='BankPay' || fm=='CashPay'">
                                                                {{ $t('PaymentVoucherList.SupplierAccount') }}
                                                            </span>
                                                        </th>
                                                        <th>
                                                            {{ $t('PaymentVoucherList.NetAmount') }}
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(voucher,index) in holdList" v-bind:key="voucher.id">

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
                                                            {{voucher.contactAccountName}}
                                                        </td>

                                                        <td>
                                                            {{currency}}  {{parseFloat(voucher.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                        </td>

                                                    </tr>
                                                    <tr style="font-size:15px;font-weight:bold;background-color:azure">
                                                        <td colspan="7" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'" style="padding-top:60px">
                                                            {{ $t('InvoicePrintReport.Total') }}
                                                        </td>
                                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" style="padding-top:60px">
                                                            {{currency}} {{parseFloat(holdListTotal).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

                                                        </td>

                                                        <td class="text-center" style="padding-top:60px">
                                                        </td>


                                                    </tr>
                                                </tbody>
                                            </table>

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
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'


    import moment from "moment";
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
                date: '',
                currency: '',
                english: '',
                arabic: '',
                search: '',
                fromTime: '',
                toTime: '',
                terminalId: '',
                contactId: '',
                userId: '',
                active: 'Invoices',
                activeTab: 'Paid',
                fromDate: '',
                formName: 'Customer',
                fm: 'BankReceipt',
                toDate: '',
                rander: 0,
                randerFromDate: 0,
                render: 0,
                printRender: 0,
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                isShown: false,
                assets: [],
                liabilities: [],
                equity: [],
                records: [],
                advanceFilters: false,
                combineDate: '',
                language: 'Nothing',
                totalAssets: 0,
                totalLiabilities: 0,
                totalEquity: 0,
                totalEquityLiability: 0,
                totalIncome: 0,
                totalExpense: 0,
                paidListTotal: 0,
                holdListTotal: 0,
                creditListTotal: 0,
                hold: 0,
                paid: 0,
                credit: 0,
                NetIncome: 0,
                counter: 0,
                isNegative: false,
                holdList: [],
                paidList: [],
                creditList: [],





            }
        },
        watch: {
            fromDate: function (fromDate) {
                

                this.counter++;
                if (this.counter != 1) {
                    this.getData(this.activeTab, fromDate, this.toDate, this.contactId);
                }



            },
            toDate: function (toDate) {
                

                this.counter++;
                if (this.counter != 2) {
                    this.getData(this.activeTab, this.fromDate, toDate, this.contactId);

                }



            },
        },
        methods: {
            getDate: function (date) {
                return moment(date).format('LLL');
            },
          
            valueChange: function (contactId) {
                

                this.getData(this.activeTab, this.fromDate, this.toDate, contactId);

            },
            makeActive: function (item) {
                
                this.active = item;
                if (this.active == 'Invoices') {
                    this.activeTab = 'Paid';
                    
                    this.getData(this.activeTab, this.fromDate, this.toDate, this.contactId);

                }
                else if (this.active == 'Payments') {
                    this.activeTab = 'Approved';
                    
                    this.getData(this.activeTab, this.fromDate, this.toDate, this.contactId);

                }
            },
            GetPaymentVoucherData: function ( status, fromDate, toDate, contactId) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('PaymentVoucher/PaymentVoucherList?paymentVoucherType=' + 'BankReceipt' + '&status=' + status + '&fromDate=' + fromDate + '&toDate=' + toDate + '&contactId=' + contactId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        
                        root.vouchersList = response.data.results.paymentVouchers;
                    }
                });
            },

            makeActiveTabs: function (item) {

                this.activeTab = item;
            },
            getData: function (status, fromDate, toDate, contactId) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }


                if (this.active == 'Invoices') {
                    this.$https.get('/Sale/SaleList?status=' + status + '&fromDate=' + fromDate + '&toDate=' + toDate + '&contactId=' + contactId + '&isDropdown=' + true + '&isSaleReturnPost=' + false + '&isExpense=' + false, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            

                            root.hold = 0;
                            root.paid = 0;
                            root.credit = 0;
                            root.paidListTotal = 0;
                            root.holdListTotal = 0;
                            root.creditListTotal = 0;
                            root.paidList = [];
                            root.holdList = [];
                            root.creditList = [];

                            if (response.data != null) {


                                response.data.results.sales.forEach(function (result) {
                                    if (result.invoiceType == 1) {
                                        root.paid++;
                                        root.paidListTotal += result.netAmount;
                                        root.paidList.push(result);
                                    }
                                    else if (result.invoiceType == 0) {
                                        root.hold++;
                                        root.holdListTotal += result.netAmount;

                                        root.holdList.push(result);
                                    }
                                    else if (result.invoiceType == 2) {
                                        root.credit++;
                                        root.creditListTotal += result.netAmount;
                                        root.creditList.push(result);
                                    }

                                })

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
                }
                else if (this.active == 'Payments') {
                   
                    root.$https.get('PaymentVoucher/PaymentVoucherList?paymentVoucherType=' + 'BankReceipt' + '&status=' + status + '&fromDate=' + fromDate + '&toDate=' + toDate + '&contactId=' + contactId + '&isDashboard=' + true, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.hold = 0;
                            root.paid = 0;
                            root.credit = 0;
                            root.paidListTotal = 0;
                            root.holdListTotal = 0;
                            root.creditListTotal = 0;
                            root.paidList = [];
                            root.holdList = [];
                            root.creditList = [];
                            
                            if (response.data != null) {


                                response.data.results.paymentVouchers.forEach(function (result) {
                                    if (result.approvalStatus == 3) {
                                        root.paid++;
                                        root.paidListTotal += result.amount;
                                        root.paidList.push(result);
                                    }
                                    else if (result.approvalStatus == 4) {
                                        root.hold++;
                                        root.holdListTotal += result.amount;

                                        root.holdList.push(result);
                                    }
                                  

                                })

                            }
                        }
                    });
                }
                
            },



            convertDate: function (date) {
                return moment(date).format('l');
            },


        },
        created: function () {
           
            if (this.$route.query.accountId != undefined) {
                this.contactId = this.$route.query.accountId;
                this.fromDate = this.$route.query.fromDate;
                this.toDate = this.$route.query.toDate;
                this.rander++;
                this.randerFromDate++;


            }
            else {
                this.english = localStorage.getItem('English');
                this.arabic = localStorage.getItem('Arabic');
                this.language = this.$i18n.locale;
                this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
                this.toDate = moment().format("DD MMM YYYY");
                this.rander++;
                this.randerFromDate++;
            }
        },
        mounted: function () {
            if (this.active == 'Invoices') {
                this.activeTab = 'Paid';
                
                this.getData(this.activeTab, this.fromDate, this.toDate, this.contactId);

            }

        }
    }
</script>
<style scoped>

    table {
        width: 100%;
    }

    thead, tbody tr {
        display: table;
        width: 100%;
        table-layout: fixed;
    }

    tbody {
        display: block;
        overflow-y: auto;
        table-layout: fixed;
        max-height: 600px;
    }

    ::-webkit-scrollbar {
        width: 11px !important;
        height: 10px !important;
    }
</style>