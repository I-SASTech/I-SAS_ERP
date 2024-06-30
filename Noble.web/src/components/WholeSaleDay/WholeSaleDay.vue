<template>
    <div class="row pl-2 pr-2 " v-if="canDayStart">
        <div class="col-lg-12 col-sm-12 "
            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="row">
                <div class="col-6" style="padding-left:0px !important">
                    <h3 class="DayHeading" style="padding-bottom:0px ;margin-bottom:0px;padding-left:0px;">{{
                        $t('WholeSaleDay.StartYourDay') }}</h3>
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><router-link :to="'/StartScreen'"><a href="javascript:void(0)"> {{
                                $t('WholeSaleDay.Home') }}</a></router-link></li>

                            <li class="breadcrumb-item active" aria-current="page"> {{ $t('WholeSaleDay.DayStart') }}</li>
                        </ol>
                    </nav>
                </div>
                <div class="col-6">
                </div>
            </div>
            <div class="row ">
                <div v-if="nobleRole == 'Sales Man' || (nobleRole == 'Admin')" class="col-lg-2 col-sm-4 col-md-4 pl-3 pt-2 "
                    v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'padding-right:22px !important' : 'padding-left:22px !important'">
                    <div class="row cardStyle " :disabled="isDayAlreadyStart"
                        v-bind:class="isDayAlreadyStart ? 'opacity50' : 'cardHover'" v-if="isValid('StartDay')"
                        v-on:click.once="AddUpdateDayStart">
                        <div class="col-4">
                            <div class="cardIcon">
                                <div class="text-center cardIconCenter">
                                    <img src="person.png" />
                                </div>
                            </div>

                        </div>
                        <div class="col-8   ">
                            <div class=" CardHeading">
                                {{ $t('WholeSaleDay.DayStart') }}
                            </div>
                        </div>
                    </div>
                </div>

                <div v-if="nobleRole == 'Sales Man' || (nobleRole == 'Admin')" class="col-lg-2 col-sm-4 col-md-4   pt-2 "
                    v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'padding-right:22px !important' : 'padding-left:22px !important'">
                    <div class="row cardStyle" :disabled="!isDayAlreadyStart"
                        v-bind:class="!isDayAlreadyStart ? 'opacity50' : 'cardHover'" v-if="isValid('CloseDay')"
                        v-on:click.once="AddUpdateDayEnd">
                        <div class="col-4  ">
                            <div class="cardIcon">
                                <div class="text-center cardIconCenter">
                                    <img src="person-2.png" />
                                </div>
                            </div>
                        </div>
                        <div class="col-8  ">
                            <div class=" CardHeading">
                                {{ $t('WholeSaleDay.DayEnd') }}
                            </div>
                        </div>
                    </div>

                </div>


            </div>
            <div class="row" v-if="isDayAlreadyStart">

                <div class="col-12 " style="padding-left:0; padding-right:0;padding-top:8px;padding-bottom:0;">
                    <div>
                        <div class="col-lg-12"
                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="tab-content" id="nav-tabContent">

                                <div class="col-12 mb-2"
                                    v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'padding-left:0px' : 'padding-right:0px;'">
                                    <h6 class="DayHeading1">{{ $t('WholeSaleDay.SaleInformation') }}</h6>
                                </div>


                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2 m-0">
                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style=" padding-left:3px;"> {{ $t('WholeSaleDay.CashDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(cash, i) in cashSale" v-bind:key="i">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ cash.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ cash.amount >= 0 ? 'Dr' : 'Cr' }} {{ nonNegative(cash.amount)
                                                        }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateCashSaleTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateCashSaleTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2">


                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style="padding-left:3px;">{{ $t('WholeSaleDay.BankDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(bank, j) in bankSale" v-bind:key="j">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ bank.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ bank.amount >= 0 ? 'Dr' : 'Cr' }} {{ nonNegative(bank.amount)
                                                        }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateBankSaleTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateBankSaleTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>


                                </div>




                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 " style="padding-left:0; padding-right:0;padding-top:0px;padding-bottom:0;">
                    <div>
                        <div class="col-lg-12"
                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="tab-content" id="nav-tabContent">

                                <div class="col-12 mb-2"
                                    v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'padding-left:0px' : 'padding-right:0px;'">
                                    <h6 class="DayHeading1">{{ $t('WholeSaleDay.ExpenseInformation') }}</h6>
                                </div>


                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2 m-0">
                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style=" padding-left:3px;"> {{ $t('WholeSaleDay.CashDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(expense, k) in cashExpense" v-bind:key="k">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ expense.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ expense.amount >= 0 ? 'Dr' : 'Cr' }} {{ nonNegative(expense.amount)
                                                        }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateCashExpenseTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateCashExpenseTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2">


                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style="padding-left:3px;"> {{ $t('WholeSaleDay.BankDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(expBank, l) in bankExpense" v-bind:key="l">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ expBank.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ expBank.amount >= 0 ? 'Dr' : 'Cr' }} {{ nonNegative(expBank.amount)
                                                        }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateBankExpenseTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateBankExpenseTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>


                                </div>




                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 " style="padding-left:0; padding-right:0;padding-top:0px;padding-bottom:0;">
                    <div>
                        <div class="col-lg-12"
                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="tab-content" id="nav-tabContent">

                                <div class="col-12 mb-2"
                                    v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'padding-left:0px' : 'padding-right:0px;'">
                                    <h6 class="DayHeading1">{{ $t('WholeSaleDay.CustomerPayInformation') }}</h6>
                                </div>


                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2 m-0">
                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style=" padding-left:3px;"> {{ $t('WholeSaleDay.CashDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(receipt, m) in cashCustomerReceipt" v-bind:key="m">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ receipt.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ receipt.amount >= 0 ? 'Dr' : 'Cr' }} {{ nonNegative(receipt.amount)
                                                        }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateCashCustomerReceiptTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateCashCustomerReceiptTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2">


                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style="padding-left:3px;"> {{ $t('WholeSaleDay.BankDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(receiptBank, n) in bankCustomerReceipt" v-bind:key="n">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ receiptBank.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ receiptBank.amount >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(receiptBank.amount) }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateBankCustomerReceiptTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateBankCustomerReceiptTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>


                                </div>




                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 " style="padding-left:0; padding-right:0;padding-top:0px;padding-bottom:0;">
                    <div>
                        <div class="col-lg-12"
                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="tab-content" id="nav-tabContent">

                                <div class="col-12 mb-2"
                                    v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'padding-left:0px' : 'padding-right:0px;'">
                                    <h6 class="DayHeading1">{{ $t('WholeSaleDay.PurchaseInformation') }}</h6>
                                </div>


                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2 m-0">
                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style=" padding-left:3px;"> {{ $t('WholeSaleDay.CashDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(purchase, o) in cashPurchase" v-bind:key="o">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ purchase.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ purchase.amount >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(purchase.amount) }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateCashPurchaseTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateCashPurchaseTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2">


                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style="padding-left:3px;"> {{ $t('WholeSaleDay.BankDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(purchaseBank, p) in bankPurchase" v-bind:key="p">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ purchaseBank.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ purchaseBank.amount >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(purchaseBank.amount) }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateBankPurchaseTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateBankPurchaseTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>


                                </div>




                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 " style="padding-left:0; padding-right:0;padding-top:0px;padding-bottom:0;">
                    <div>
                        <div class="col-lg-12"
                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="tab-content" id="nav-tabContent">

                                <div class="col-12 mb-2"
                                    v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'padding-left:0px' : 'padding-right:0px;'">
                                    <h6 class="DayHeading1">{{ $t('WholeSaleDay.PurchaseExpenseInformation') }}</h6>
                                </div>


                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2 m-0">
                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style=" padding-left:3px;"> {{ $t('WholeSaleDay.CashDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(pExpense, q) in cashPurchaseExpense" v-bind:key="q">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ pExpense.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ pExpense.amount >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(pExpense.amount) }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateCashPurchaseExpenseTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateCashPurchaseExpenseTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2">


                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style="padding-left:3px;"> {{ $t('WholeSaleDay.BankDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(pExpenseBank, r) in bankPurchaseExpense" v-bind:key="r">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ pExpenseBank.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ pExpenseBank.amount >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(pExpenseBank.amount) }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateBankPurchaseExpenseTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateBankPurchaseExpenseTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>


                                </div>




                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 " style="padding-left:0; padding-right:0;padding-top:0px;padding-bottom:0;">
                    <div>
                        <div class="col-lg-12"
                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="tab-content" id="nav-tabContent">

                                <div class="col-12 mb-2"
                                    v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'padding-left:0px' : 'padding-right:0px;'">
                                    <h6 class="DayHeading1">{{ $t('SupplierPaidInformation') }}</h6>
                                </div>


                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2 m-0">
                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style=" padding-left:3px;"> {{ $t('WholeSaleDay.CashDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(supplier, s) in cashSupplierPay" v-bind:key="s">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ supplier.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ supplier.amount >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(supplier.amount) }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateCashSupplierPayTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateCashSupplierPayTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2">


                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style="padding-left:3px;"> {{ $t('WholeSaleDay.BankDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(supplierBank, t) in bankSupplierPay" v-bind:key="t">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ supplierBank.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ supplierBank.amount >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(supplierBank.amount) }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateBankSupplierPayTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateBankSupplierPayTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>


                                </div>




                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 " style="padding-left:0; padding-right:0;padding-top:0px;padding-bottom:0;">
                    <div>
                        <div class="col-lg-12"
                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="tab-content" id="nav-tabContent">

                                <div class="col-12 mb-2"
                                    v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'padding-left:0px' : 'padding-right:0px;'">
                                    <h6 class="DayHeading1">{{ $t('WholeSaleDay.OtherPaymentInformation') }}</h6>
                                </div>


                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2 m-0">
                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style=" padding-left:3px;"> {{ $t('WholeSaleDay.CashDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(other, u) in otherCashPayments" v-bind:key="u">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ other.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ other.amount >= 0 ? 'Dr' : 'Cr' }} {{ nonNegative(other.amount)
                                                        }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateOtherCashPaymentTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateOtherCashPaymentTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2">


                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style="padding-left:3px;"> {{ $t('WholeSaleDay.BankDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(otherBank, v) in otherBankPayments" v-bind:key="v">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ otherBank.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ otherBank.amount >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(otherBank.amount) }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateOtherBankPaymentTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateOtherBankPaymentTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>


                                </div>




                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 " style="padding-left:0; padding-right:0;padding-top:0px;padding-bottom:0;">
                    <div>
                        <div class="col-lg-12"
                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="tab-content" id="nav-tabContent">

                                <div class="col-12 mb-2"
                                    v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'padding-left:0px' : 'padding-right:0px;'">
                                    <h6 class="DayHeading1">{{ $t('WholeSaleDay.SaleReturnInformation') }}</h6>
                                </div>


                                <div class="row">
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">
                                        <div class="card p-2 m-0">
                                            <h6 class="modal-title labelHeading" id="myModalLabel"
                                                style=" padding-left:3px;"> {{ $t('WholeSaleDay.CashDetail') }}</h6>
                                            <table class="table ">

                                                <tr style="font-size: 12px; padding-bottom: 4px; "
                                                    v-for="(saleReturn, x) in saleReturn" v-bind:key="x">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ saleReturn.name }}</span></td>
                                                    <td style="color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ saleReturn.amount >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(saleReturn.amount) }}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="font-weight:bold;color:black !important"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:left' : 'text-align:right'">
                                                        <span>{{ $t('WholeSaleDay.Total') }}</span></td>
                                                    <td style="color:black !important" colspan="3"
                                                        v-bind:style="($i18n.locale == 'en' || isLeftToRight()) ? 'text-align:right' : 'text-align:left'">
                                                        <span>{{ caculateCahSaleReturnTotal >= 0 ? 'Dr' : 'Cr' }}
                                                            {{ nonNegative(caculateCahSaleReturnTotal) }}</span></td>

                                                </tr>


                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 ">

                                    </div>


                                </div>




                            </div>
                        </div>
                    </div>
                </div>





            </div>

        </div>

        <wholeSaleDayEndReport :key="printRander" v-if="isReport" :printDetail="printDetail" v-on:close="onCloseEvent"
            :headerFooter="headerFooter"></wholeSaleDayEndReport>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'

export default {
    mixins: [clickMixin],
    data: function () {
        return {
            canDayStart: false,
            report: false,
            isReport: false,
            isDayAlreadyStart: false,
            nobleRole: '',
            cashSale: [],
            bankSale: [],
            cashPurchase: [],
            bankPurchase: [],
            cashExpense: [],
            bankExpense: [],
            cashCustomerReceipt: [],
            bankCustomerReceipt: [],
            cashSupplierPay: [],
            bankSupplierPay: [],
            saleReturn: [],
            cashPurchaseExpense: [],
            bankPurchaseExpense: [],
            otherCashPayments: [],
            otherBankPayments: [],
            printDetail: [],
            printRander: 0,
            headerFooter: {
                footerEn: '',
                footerAr: '',
                company: ''
            },
        }
    },
    computed: {
        caculateCashSaleTotal: function () {
            return this.cashSale.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateBankSaleTotal: function () {
            return this.bankSale.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateCashExpenseTotal: function () {
            return this.cashExpense.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateBankExpenseTotal: function () {
            return this.bankExpense.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateCashCustomerReceiptTotal: function () {
            return this.cashCustomerReceipt.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateBankCustomerReceiptTotal: function () {
            return this.bankCustomerReceipt.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateCashPurchaseTotal: function () {
            return this.cashPurchase.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateBankPurchaseTotal: function () {
            return this.bankPurchase.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateCashPurchaseExpenseTotal: function () {
            return this.cashPurchaseExpense.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateBankPurchaseExpenseTotal: function () {
            return this.bankPurchaseExpense.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateCashSupplierPayTotal: function () {
            return this.cashSupplierPay.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateBankSupplierPayTotal: function () {
            return this.bankSupplierPay.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateOtherCashPaymentTotal: function () {
            return this.otherCashPayments.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateOtherBankPaymentTotal: function () {
            return this.otherBankPayments.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
        caculateCahSaleReturnTotal: function () {
            return this.saleReturn.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
        },
    },
    methods: {
        onCloseEvent: function () {
            this.isReport = false;
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
                        root.printRander++;
                        root.isReport = true;
                    }
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
        nonNegative: function (value) {
            return parseFloat(Math.abs(value)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
        },
        AddUpdateDayStart: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            //var dayStartId = localStorage.getItem('DayStartId') == null ? '' : localStorage.getItem('DayStartId');

            root.$https.get('/Product/AddWholeSaleDayStart', { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data.isDayStart) {
                        localStorage.setItem('IsDayStart', response.data.isDayStart)
                        localStorage.setItem('DayStartId', response.data.dayStartId)

                        localStorage.setItem('CounterId', response.data.counterId);
                        localStorage.setItem('token', response.data.token);
                        root.isDayAlreadyStart = true;
                        root.GetTransactionDetail();
                    }
                });
        },
        IsDayStart: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/IsWholeSaleDayStart', { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data.isDayStart) {

                        localStorage.setItem('IsDayStart', response.data.isDayStart)
                        localStorage.setItem('DayStartId', response.data.dayStartId)
                        localStorage.setItem('CounterId', response.data.counterId);
                        root.isDayAlreadyStart = true;
                        root.GetTransactionDetail();
                    }
                });
        },
        AddUpdateDayEnd: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var dayStartId = localStorage.getItem('DayStartId') == null ? '' : localStorage.getItem('DayStartId');

            root.$https.get('/Product/AddWholeSaleDayStart?id=' + dayStartId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data.isDayStart == false) {


                        localStorage.setItem('IsDayStart', '')
                        localStorage.setItem('DayStartId', '')

                        localStorage.setItem('CounterId', '');
                        root.isDayAlreadyStart = false;
                        root.GetHeaderDetail();
                    }
                });
        },
        GetTransactionDetail: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var dayStartId = localStorage.getItem('DayStartId') == null ? '' : localStorage.getItem('DayStartId');

            root.$https.get('/Product/GetTransactionDetailWholeSale?id=' + dayStartId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    if (response.data.cashSale != null) {
                        root.cashSale = response.data.cashSale;
                        root.bankSale = response.data.bankSale;
                        root.cashPurchase = response.data.cashPurchase;
                        root.bankPurchase = response.data.bankPurchase;
                        root.cashExpense = response.data.cashExpense;
                        root.bankExpense = response.data.bankExpense;
                        root.cashCustomerReceipt = response.data.cashCustomerReceipt;
                        root.bankCustomerReceipt = response.data.bankCustomerReceipt;
                        root.cashSupplierPay = response.data.cashSupplierPay;
                        root.bankSupplierPay = response.data.bankSupplierPay;
                        root.saleReturn = response.data.saleReturn;
                        root.cashPurchaseExpense = response.data.cashPurchaseExpense;
                        root.bankPurchaseExpense = response.data.bankPurchaseExpense;
                        root.otherCashPayments = response.data.otherCashPayments;
                        root.otherBankPayments = response.data.otherBankPayments;
                        root.printDetail = response.data;

                    }
                });
        },

    },
    created: function () {

    },
    mounted: function () {

        this.canDayStart = localStorage.getItem('DayStart')
        this.isDayAlreadyStart = false
        this.nobleRole = localStorage.getItem('NobleRole');
        this.IsDayStart()

    }
}
</script>
<style scoped>
body {
    font-family: Ubuntu !important;
}

@media (min-width: 1250px) and (max-width: 1538px) {
    .table-responsive {
        overflow-x: auto !important;
    }
}

@media (min-width: 1538px) {
    .tableActionWidth {
        width: 12%
    }
}

@media (min-width: 1349px) and (max-width: 1538px) {
    .tableActionWidth {
        width: 18%
    }
}

@media (min-width: 500px) and (max-width: 1348px) {
    .tableActionWidth {
        width: 19%
    }
}



@media screen and (min-width: 1781px) {

    .cardIconCenter {
        padding-top: 22px;
    }

    .CardHeading {
        margin-top: 38px;
        font-family: Ubuntu;
        position: absolute;
        width: 112px;
        height: 46px;
        font-style: normal;
        font-weight: 500;
        font-size: 24px;
        line-height: 26px;
        letter-spacing: 0.01em;
        color: #35353D;
    }

    .CardImg {
        position: absolute;
        width: 14px;
        height: 25px;
        left: 5px;
        top: 0px;
    }

    .cardIcon {
        margin-top: 38px;
        border-radius: 50%;
        width: 70px;
        height: 70px;
        background: rgba(49, 120, 246, 0.1);
    }



    .cardStyle {
        width: 266px;
        height: 151px;
        background: #FFFFFF;
        border-radius: 10px;
    }
}

@media (min-width: 1393px) and (max-width: 1780px) {

    .CardHeading {
        margin-top: 48px;
        font-family: Ubuntu;
        position: absolute;
        width: 100px;
        height: 46px;
        font-style: normal;
        font-weight: 500;
        font-size: 22px;
        line-height: 26px;
        /* or 108% */

        letter-spacing: 0.01em;
        color: #35353D;
    }

    .CardImg {
        position: absolute;
        width: 10px;
        height: 25px;
        left: 5px;
        top: 0px;
    }

    .cardIcon {
        margin-top: 46px;
        border-radius: 50%;
        width: 60px;
        height: 60px;
        background: rgba(49, 120, 246, 0.1);
    }

    .cardIconCenter {
        padding-top: 19px;
    }



    .cardStyle {
        height: 151px;
        background: #FFFFFF;
        border-radius: 10px;
    }
}


@media (min-width: 500px) and (max-width: 992px) {


    .CardHeading {
        margin-top: 48px;
        font-family: Ubuntu;
        position: absolute;
        width: 100px;
        height: 46px;
        font-style: normal;
        font-weight: 500;
        font-size: 22px;
        line-height: 26px;
        /* or 108% */

        letter-spacing: 0.01em;
        color: #35353D;
    }

    .CardImg {
        position: absolute;
        width: 10px;
        height: 25px;
        left: 5px;
        top: 0px;
    }

    .cardIcon {
        margin-top: 46px;
        border-radius: 50%;
        width: 60px;
        height: 60px;
        background: rgba(49, 120, 246, 0.1);
    }

    .cardIconCenter {
        padding-top: 19px;
    }



    .cardStyle {
        height: 151px;
        background: #FFFFFF;
        border-radius: 10px;
    }
}


@media (min-width: 1243px) and (max-width: 1392px) {

    .CardHeading {
        margin-top: 33px;
        padding-left: 12px;
        font-family: Ubuntu;
        position: absolute;
        width: 70px;
        height: 38px;
        font-style: normal;
        font-weight: 500;
        font-size: 16px;
        line-height: 26px;
        letter-spacing: 0.01em;
        color: #35353D;
    }

    .CardImg {
        position: absolute;
        width: 10px;
        height: 18px;
        left: 5px;
        top: 0px;
    }

    .cardIcon {
        margin-top: 32px;
        border-radius: 50%;
        width: 60px;
        height: 60px;
        background: rgba(49, 120, 246, 0.1);
    }



    .cardStyle {
        height: 130px;
        background: #FFFFFF;
        border-radius: 10px;
    }

    .cardIconCenter {
        padding-top: 22px;
    }
}

@media (min-width: 1125px) and (max-width: 1242px) {
    .cardIconCenter {
        padding-top: 13px;
    }

    .CardHeading {
        margin-top: 35px;
        font-family: Ubuntu;
        position: absolute;
        width: 50px;
        height: 38px;
        font-style: normal;
        font-weight: 500;
        font-size: 14px;
        line-height: 26px;
        letter-spacing: 0.01em;
        color: #35353D;
    }

    .CardImg {
        position: absolute;
        width: 10px;
        height: 18px;
        left: 5px;
        top: 0px;
    }

    .cardIcon {
        margin-top: 36px;
        border-radius: 50%;
        width: 50px;
        height: 50px;
        background: rgba(49, 120, 246, 0.1);
    }



    .cardStyle {
        height: 130px;
        background: #FFFFFF;
        border-radius: 10px;
    }
}

@media (min-width: 993px) and (max-width: 1124px) {
    .cardIconCenter {
        padding-top: 13px;
    }

    .CardHeading {
        margin-top: 35px;
        font-family: Ubuntu;
        position: absolute;
        width: 50px;
        height: 38px;
        font-style: normal;
        font-weight: 500;
        font-size: 14px;
        line-height: 26px;
        letter-spacing: 0.01em;
        color: #35353D;
    }

    .CardImg {
        position: absolute;
        width: 10px;
        height: 18px;
        left: 5px;
        top: 0px;
    }

    .cardIcon {
        margin-top: 36px;
        border-radius: 50%;
        width: 50px;
        height: 50px;
        background: rgba(49, 120, 246, 0.1);
    }



    .cardStyle {
        height: 130px;
        background: #FFFFFF;
        border-radius: 10px;
    }
}

.btn-SuperVisor {
    font-family: Ubuntu;
    width: 150px;
    height: 40px;
    border-radius: 5px;
}

.btn-LogOut {
    height: 40px;
    width: 40px;
    border-radius: 5px;
}

.FontFamily {
    font-family: Ubuntu;
}

.opacity50 {
    opacity: 0.7 !important;
}





.Daytext {
    font-size: 16px;
    font-style: normal;
    font-weight: 500;
    line-height: 26px;
    letter-spacing: 0.01em;
    color: #35353D;
}

.UserName {
    font-family: Ubuntu;
    font-size: 12px;
    font-style: normal;
    font-weight: 700;
    line-height: 14px;
    letter-spacing: 0px;
    width: 838px;
    height: 838px;
    top: 32px;
}



.cardHover:hover {
    -webkit-box-shadow: 0px 1px 1px #3178F6;
    -moz-box-shadow: 0px 1px 1px #3178F6;
    box-shadow: 0px 1px 1px #3178F6;
    /*        border-bottom-color: #35353D !important;
    */
}





.round {
    -webkit-border-top-left-radius: 1px;
    -webkit-border-top-right-radius: 2px;
    -webkit-border-bottom-right-radius: 3px;
    -webkit-border-bottom-left-radius: 4px;
    -moz-border-radius-topleft: 1px;
    -moz-border-radius-topright: 2px;
    -moz-border-radius-bottomright: 3px;
    -moz-border-radius-bottomleft: 4px;
    border-top-left-radius: 1px;
    border-top-right-radius: 2px;
    border-bottom-right-radius: 3px;
    border-bottom-left-radius: 4px;
}



.cardStyleForDay {
    width: 266px;
    height: 500px;
    background: #FFFFFF;
    border-radius: 10px;
}

.LinkStyle {
    height: 4px;
    width: 4px;
    border-radius: 0px;
    font-family: Ubuntu;
    color: #A6ACBE;
    height: 4px;
    width: 4px;
}

.DotSize {
    font-family: Ubuntu;
    color: #A6ACBE;
    border-radius: 0px;
}

.DayHeading {
    font-size: 32px;
    font-style: normal;
    font-family: Ubuntu;
    font-size: 25px;
    font-style: normal;
    font-weight: 700;
    line-height: 37px;
    letter-spacing: 0.01em;
    color: #3178F6;
}

.DayHeading1 {
    font-style: normal;
    font-family: Ubuntu;
    font-size: 17px;
    font-style: normal;
    font-weight: 700;
    line-height: 37px;
    letter-spacing: 0.01em;
    color: #3178F6;
}

.labelHeading {
    font-style: normal;
    font-family: Ubuntu;
    font-style: normal;
    font-weight: 600;
    line-height: 37px;
    letter-spacing: 0.01em;
    color: black;
}

.FontSizeSmall {
    font-size: 12px;
}

.TableHeadingForTable {
    background: #3178F6;
    color: white;
    font-size: 14px !important
}</style>