<template>
    <div>
        <modal :show="show" :modalLarge="true">

            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title m-0"> {{ $t('SalesDashboardReport.SalesDashboardReport') }}</h6>
                    <button type="button" class="btn-close" v-on:click="close()"></button>
                </div>

            </div>

            <div class="modal-body">
                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-md-4 col-lg-4">
                            <div class="form-group">
                                <label>{{ $t('CustomerBalanceReport.CustomerName') }}</label>
                                <accountdropdown v-model="contactId" :formName="'Customer'" @update:modelValue="valueChange(contactId)" :modelValue="contactId" :key="render" />
                            </div>
                        </div>
                        <div class="col-md-4 col-lg-4">
                            <div class="form-group">
                                <label>{{ $t('BalanceSheetReport.FromDate') }}</label>
                                <datepicker v-model="fromDate" />
                            </div>
                        </div>
                        <div class="col-md-4 col-lg-4">
                            <div class="form-group">
                                <label>{{ $t('BalanceSheetReport.ToDate') }}</label>
                                <datepicker v-model="toDate" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="modal-body">
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
                                                        <li class="nav-item" v-if="isValid('CanViewHoldInvoices')"><a class="nav-link" v-bind:class="{active:activeTab == 'Hold'}" v-on:click="makeActiveTabs('Hold')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">{{ $t('Sale.Hold') }} <span class="badge badge-boxed  badge-outline-danger">{{hold}}</span> </a></li>
                                                        <li class="nav-item" v-if="isValid('CanViewPaidInvoices')"><a class="nav-link" v-bind:class="{active:activeTab == 'Paid'}" v-on:click="makeActiveTabs('Paid')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('Sale.Paid') }} <span class="badge badge-boxed  badge-outline-success">{{paid}}</span></a></li>
                                                        <li class="nav-item" v-if="isValid('CanViewCreditInvoices')"><a class="nav-link" v-bind:class="{active:activeTab == 'Credit'}" v-on:click="makeActiveTabs('Credit')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('Sale.Credit') }} <span class="badge badge-boxed  badge-outline-primary">{{credit}}</span></a></li>

                                                    </ul>
                                                </div>
                                            </div>
                                            <div class="tab-content mt-3" id="nav-tabContent">
                                                <div v-if="activeTab == 'Hold'">
                                                    <div class="table-responsive">
                                                        <table class="table">
                                                            <thead class="thead-light">

                                                                <tr>
                                                                    <th style="width:40px;">#</th>
                                                                    <th style="width:105px;" >
                                                                        {{ $t('Sale.InvoiceNo') }}
                                                                    </th>

                                                                    <th style="width: 125px;" >
                                                                        {{ $t('Sale.Date') }}
                                                                    </th>
                                                                    <th style="width: 220px;"  v-if="english=='true'">
                                                                        {{$englishLanguage($t('Sale.CustomerName')) }}
                                                                    </th>
                                                                    <th style="width: 220px;"  v-if="isOtherLang()">
                                                                        {{$arabicLanguage($t('Sale.CustomerNameArabic'))  }}
                                                                    </th>
                                                                    <th style="width: 120px;" >
                                                                        {{ $t('Sale.CreatedBy') }}
                                                                    </th>

                                                                    <th style="width: 120px;" >
                                                                        {{ $t('Sale.NetAmount') }}
                                                                    </th>
                                                                    <th style="width: 100px;" class="text-center">
                                                                        {{ $t('Sale.RePrint') }}
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr v-for="(sale,index) in holdList" v-bind:key="'a'+ index">

                                                                    <td style="width:40px;">
                                                                        {{((currentPage*10)-10) +(index+1)}}
                                                                    </td>

                                                                    <td style="width:105px;" >
                                                                        {{sale.registrationNumber}}
                                                                        <span class="small badge badge-success">{{sale.isCredit ? 'Credit':'Cash'}}</span>
                                                                    </td>



                                                                    <td style="width: 125px;" >
                                                                        {{getDate(sale.date)}}
                                                                    </td>
                                                                    <td style="width: 220px;"  v-if="english=='true'">
                                                                        {{sale.customerName}}
                                                                    </td>
                                                                    <td style="width: 220px;"  v-if="isOtherLang()">
                                                                        {{sale.customerNameArabic}}
                                                                    </td>
                                                                    <td style="width: 120px;" >
                                                                        {{sale.userName}}
                                                                    </td>

                                                                    <td style="width: 120px;" >
                                                                        {{currency}}  {{parseFloat(sale.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                    </td>
                                                                    <td style="width: 100px;" class="text-center">
                                                                        <a href="javascript:void(0)" title="A4" class="btn btn-primary btn-sm btn-icon " v-on:click="PrintInvoice(sale.id,false)" v-if="isValid('CanViewCustomerBalance') "><i class=" fa fa-print"></i></a>
                                                                        <a href="javascript:void(0)" title="POS" class="btn btn-primary btn-sm btn-icon ml-1 mr-1" v-on:click="PrintInvoicePos(sale.id, sale.counterName)" v-if="isValid('CanViewCustomerBalance') "><i class=" fa fa-print"></i></a>
                                                                    </td>
                                                                </tr>
                                                                <tr style="font-size:15px;font-weight:bold;">
                                                                    <td colspan="6" class="text-center" style="padding-top:60px">
                                                                        {{ $t('InvoicePrintReport.Total') }}
                                                                    </td>
                                                                    <td class="text-center" style="padding-top:60px">
                                                                        {{currency}} {{parseFloat(paidListTotal).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

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
                                                        <table class="table">
                                                            <thead class="thead-light">

                                                                <tr>
                                                                    <th style="width:40px;">#</th>
                                                                    <th style="width:105px;" >
                                                                        {{ $t('Sale.InvoiceNo') }}
                                                                    </th>

                                                                    <th style="width: 125px;" >
                                                                        {{ $t('Sale.Date') }}
                                                                    </th>
                                                                    <th style="width: 220px;"  v-if="english=='true'">
                                                                        {{$englishLanguage($t('Sale.CustomerName'))  }}
                                                                    </th>
                                                                    <th style="width: 220px;"  v-if="isOtherLang()">
                                                                        {{$arabicLanguage($t('Sale.CustomerNameArabic'))  }}
                                                                    </th>
                                                                    <th style="width: 120px;" >
                                                                        {{ $t('Sale.CreatedBy') }}
                                                                    </th>

                                                                    <th style="width: 120px;" >
                                                                        {{ $t('Sale.NetAmount') }}
                                                                    </th>
                                                                    <th style="width: 100px;" class="text-center">
                                                                        {{ $t('Sale.RePrint') }}
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr v-for="(sale,index) in paidList" v-bind:key="'b'+ index">

                                                                    <td style="width:40px;">
                                                                        {{((currentPage*10)-10) +(index+1)}}
                                                                    </td>

                                                                    <td style="width:105px;" >
                                                                        {{sale.registrationNumber}}
                                                                        <span class="small badge badge-success">{{sale.isCredit ? 'Credit':'Cash'}}</span>
                                                                    </td>



                                                                    <td style="width: 125px;" >
                                                                        {{getDate(sale.date)}}
                                                                    </td>
                                                                    <td style="width: 220px;"  v-if="english=='true'">
                                                                        {{sale.customerName}}
                                                                    </td>
                                                                    <td style="width: 220px;"  v-if="isOtherLang()">
                                                                        {{sale.customerNameArabic}}
                                                                    </td>
                                                                    <td style="width: 120px;" >
                                                                        {{sale.userName}}
                                                                    </td>

                                                                    <td style="width: 120px;" >
                                                                        {{currency}} {{parseFloat(sale.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

                                                                    </td>
                                                                    <td style="width: 100px;" class="text-center"></td>
                                                                    <td style="width: 100px;" class="text-center">
                                                                        <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">Action <i class="mdi mdi-chevron-down"></i></button>
                                                                        <div class="dropdown-menu">
                                                                            <a class="dropdown-item" href="javascript:void(0)" v-on:click="ViewInvoice(sale.id)">View Invoice</a>
                                                                            <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoice(sale.id, 'print')" v-if="isValid('CanA4Print') ">A4 Print</a>
                                                                            <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoice(sale.id, 'download')" v-if="isValid('CanA4Print') ">PDF Download</a>

                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr style="font-size:15px;font-weight:bold;">
                                                                    <td colspan="6" class="text-center" style="padding-top:60px">
                                                                        {{ $t('InvoicePrintReport.Total') }}
                                                                    </td>
                                                                    <td class="text-center" style="padding-top:60px">
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
                                                        <table class="table">
                                                            <thead class="thead-light">

                                                                <tr>
                                                                    <th style="width:40px;">#</th>
                                                                    <th style="width:105px;" >
                                                                        {{ $t('Sale.InvoiceNo') }}
                                                                    </th>

                                                                    <th style="width: 125px;" >
                                                                        {{ $t('Sale.Date') }}
                                                                    </th>
                                                                    <th style="width: 220px;"  v-if="english=='true'">
                                                                        {{$englishLanguage($t('Sale.CustomerName'))  }}
                                                                    </th>
                                                                    <th style="width: 220px;"  v-if="isOtherLang()">
                                                                        {{$arabicLanguage($t('Sale.CustomerNameArabic'))  }}
                                                                    </th>
                                                                    <th style="width: 120px;" >
                                                                        {{ $t('Sale.CreatedBy') }}
                                                                    </th>

                                                                    <th style="width: 120px;" >
                                                                        {{ $t('Sale.NetAmount') }}
                                                                    </th>
                                                                    <th style="width: 100px;" class="text-center">
                                                                        {{ $t('Sale.RePrint') }}
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr v-for="(sale,index) in creditList" v-bind:key="'c'+ index">

                                                                    <td style="width:40px;">
                                                                        {{((currentPage*10)-10) +(index+1)}}
                                                                    </td>

                                                                    <td style="width:105px;" >
                                                                        {{sale.registrationNumber}}
                                                                        <span class="small badge badge-success">{{sale.isCredit ? 'Credit':'Cash'}}</span>
                                                                    </td>



                                                                    <td style="width: 125px;" >
                                                                        {{getDate(sale.date)}}
                                                                    </td>
                                                                    <td style="width: 220px;"  v-if="english=='true'">
                                                                        {{sale.customerName}}
                                                                    </td>
                                                                    <td style="width: 220px;"  v-if="isOtherLang()">
                                                                        {{sale.customerNameArabic}}
                                                                    </td>
                                                                    <td style="width: 120px;" >
                                                                        {{sale.userName}}
                                                                    </td>

                                                                    <td style="width: 120px;" >
                                                                        {{currency}}  {{parseFloat(sale.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                                    </td>
                                                                    <td style="width: 100px;" class="text-center">
                                                                        <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">Action <i class="mdi mdi-chevron-down"></i></button>
                                                                        <div class="dropdown-menu">
                                                                            <a class="dropdown-item" href="javascript:void(0)" v-on:click="ViewInvoice(sale.id)" >View Invoice</a>
                                                                            <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoice(sale.id, 'print')" v-if="isValid('CanA4Print') ">A4 Print</a>
                                                                            <a class="dropdown-item" href="javascript:void(0)" v-on:click="PrintInvoice(sale.id, 'download')" v-if="isValid('CanA4Print') ">PDF Download</a>

                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr style="font-size:15px;font-weight:bold;">
                                                                    <td colspan="6" class="text-center" style="padding-top:60px">
                                                                        {{ $t('InvoicePrintReport.Total') }}
                                                                    </td>
                                                                    <td class="text-center" style="padding-top:60px">
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

                                <div class="col-lg-12">


                                    <ul class="nav nav-tabs" data-tabs="tabs">
                                        <li class="nav-item" v-if="(isValid('CanDraftPettyCash') ) || (isValid('CanDraftCPR') ) ||(isValid('CanDraftSPR') )"><a class="nav-link" v-bind:class="{active:activeTab == 'Draft'}" v-on:click="makeActiveTabs('Draft')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">{{ $t('PaymentVoucherList.Draft') }} <span class="badge badge-boxed  badge-outline-danger">{{hold}}</span></a></li>
                                        <li class="nav-item" v-if="  (isValid('CanViewPettyCash')) || (isValid('CanViewCPR') )||(isValid('CanViewSPR') ) " v-on:click="makeActiveTabs('Approved')"><a class="nav-link" v-bind:class="{active:activeTab == 'Approved'}" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('PaymentVoucherList.Post') }} <span class="badge badge-boxed  badge-outline-success">{{paid}}</span></a></li>

                                    </ul>
                                    <div v-if="activeTab == 'Draft'">


                                        <table class="table mt-3 ">
                                            <thead class="thead-light">
                                                <tr>

                                                    <th class="text-center">#</th>
                                                    <th class="text-center">
                                                        {{ $t('PaymentVoucherList.VoucherNumber') }}
                                                    </th>
                                                    <th class="text-center">
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
                                                    <th>
                                                        {{ $t('PaymentVoucherList.Action') }}
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
                                                        <div class="badge badge-primary"  v-if="voucher.paymentMode==0">
                                                            {{($i18n.locale == 'en' ||isLeftToRight())?' Cash':'السيولة النقدية'}}

                                                        </div>
                                                        <div class="badge badge-success"  v-if="voucher.paymentMode==1">
                                                            {{($i18n.locale == 'en' ||isLeftToRight())?' Bank':'مصرف'}}
                                                        </div>
                                                        <div class="badge badge-info"  v-if="voucher.paymentMode==5">
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
                                                    <td>
                                                        <a href="javascript:void(0)" title="A5" class="btn  btn-icon btn-primary btn-sm" v-on:click="PrintPaymentVoucher(voucher.id)"><i class=" fa fa-print"></i></a>
                                                    </td>

                                                </tr>
                                                <tr style="font-size:15px;font-weight:bold;">
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
                                    <div v-if="activeTab == 'Approved'">
                                        <table class="table mt-3 ">
                                            <thead class="thead-light">
                                                <tr>

                                                    <th class="text-center">#</th>
                                                    <th class="text-center">
                                                        {{ $t('PaymentVoucherList.VoucherNumber') }}
                                                    </th>
                                                    <th class="text-center">
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
                                                    <th>
                                                        {{ $t('PaymentVoucherList.Action') }}
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
                                                        <div class="badge badge-primary"  v-if="voucher.paymentMode==0">
                                                            {{($i18n.locale == 'en' ||isLeftToRight())?' Cash':'السيولة النقدية'}}

                                                        </div>
                                                        <div class="badge badge-success"  v-if="voucher.paymentMode==1">
                                                            {{($i18n.locale == 'en' ||isLeftToRight())?' Bank':'مصرف'}}
                                                        </div>
                                                        <div class="badge badge-info"  v-if="voucher.paymentMode==5">
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
                                                    <td>
                                                        <a href="javascript:void(0)" title="A5" class="btn  btn-icon btn-primary btn-sm" v-on:click="PrintPaymentVoucher(voucher.id)"><i class=" fa fa-print"></i></a>
                                                    </td>

                                                </tr>
                                                <tr style="font-size:15px;font-weight:bold;">
                                                    <td colspan="7" class="text-center" style="padding-top:60px">
                                                        {{ $t('InvoicePrintReport.Total') }}
                                                    </td>
                                                    <td class="text-center" style="padding-top:60px">
                                                        {{currency}} {{parseFloat(paidListTotal).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

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


        </modal>
        



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


</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'


    import moment from "moment";
    export default {
        props: ['query',  'show'],

        mixins: [clickMixin],
        data: function () {
            return {
                saleId: '',
                showPayment: false,
                isPrint: false,
                emailComposeShow: false,
                download: false,
                bankDetail: false,
                printSize: '',
                printTemplate: '',
                filePath: null,
                date: '',
                currency: '',
                english: '',
                arabic: '',
                search: '',
                fromTime: '',
                toTime: '',
                terminalId: '',
                colorVariants: false,

                contactId: '',
                userId: '',
                active: 'Invoices',
                activeTab: 'Paid',
                fromDate: '',
                formName: 'BankReceipt',
                fm: 'BankReceipt',
                toDate: '',
                rander: 0,
                randerFromDate: 0,
                render: 0,
                printRenderForPayment: 0,
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
                selected: [],
                selectAll: false,
                updateApprovalStatus: {
                    id: '',
                    approvalStatus: ''
                },
                purchasePostList: [],

                printDetails: [],
                printDetailsPos: [],
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
                openBatch: 0,





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
            PrintPaymentVoucher: function (value, print2) {

                this.showPayment = false;
                this.show2 = false;
                if (print2) {
                    this.show2 = true;
                }
                else {
                    this.showPayment = true;
                }


                this.GetHeaderDetail();
                var id = value;
                this.printId = id;
                this.printRenderForPayment++;
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
            ViewInvoice: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/SaleDetail?Id=' + id + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch + '&colorVariants=' + this.colorVariants, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$router.push({
                                path: '/InvoiceView',
                                query: {
                                    data: response.data,
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
                
                this.GetHeaderDetail();


                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                root.printDetailsPos = [];
                root.$https.get("/Sale/SaleDetail?id=" + value + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch, { headers: { Authorization: `Bearer ${token}` }, })
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
                            }
                            else {
                                root.isPrint = true;
                            }
                            root.printRender++;
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
                        }

                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
            PrintInvoicePos: function (value, counterName) {

                this.GetHeaderDetail();
                var root = this;
                var token = '';
                this.isDisabled = true;
                this.PrinterInterval();
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.printDetails = [];
                root.$https.get("/Sale/SaleDetail?id=" + value + '&isFifo=' + this.isFifo + '&openBatch=' + this.openBatch, {
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
            close: function () {
                this.$emit('close');
            },
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
            GetPaymentVoucherData: function (status, fromDate, toDate, contactId) {

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
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.language = this.$i18n.locale;
            this.overWrite = localStorage.getItem('overWrite') == 'true' ? true : false;
            this.currency = localStorage.getItem('currency');
            this.companyId = localStorage.getItem('CompanyID');
            this.businessLogo = localStorage.getItem('BusinessLogo');
            this.businessNameArabic = localStorage.getItem('BusinessNameArabic');
            this.businessNameEnglish = localStorage.getItem('BusinessNameEnglish');
            this.businessTypeArabic = localStorage.getItem('BusinessTypeArabic');
            this.businessTypeEnglish = localStorage.getItem('BusinessTypeEnglish');
            this.companyNameArabic = localStorage.getItem('CompanyNameArabic');
            this.companyNameEnglish = localStorage.getItem('CompanyNameEnglish');
            this.printTemplate = localStorage.getItem('PrintTemplate');
            this.printSize = localStorage.getItem('PrintSizeA4');
            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            if (this.query.accountId != undefined) {
                this.contactId = this.query.accountId;
                this.fromDate = this.query.fromDate;
                this.toDate = this.query.toDate;
                this.rander++;
                this.randerFromDate++;


            }
            else {
               
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