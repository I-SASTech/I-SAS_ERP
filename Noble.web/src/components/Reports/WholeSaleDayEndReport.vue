<template>
    <div>
        <div hidden id='purchaseInvoice' class="col-md-12">
            <!--page1-->
            <div>
                <!--HEADER-->
                <div class="col-md-12" style="height:45mm;border:2px solid #000000;">
                    <div class="row" style="height:35mm">
                        <div class="col-md-4 ">
                            <table class="text-left">
                                <tr>
                                    <td>
                                        <p>
                                            <span style="font-size:25px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                            <span style="font-size:17px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryEnglish}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">Cr No.:{{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:15px;color:black !important;font-weight:bold;">
                                                Tel: {{headerFooters.company.phoneNo}}
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-4 text-center my-5" style="padding:0px !important; margin:0 !important">
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                        </div>
                        <div class="col-md-4 ">
                            <table class="text-right">
                                <tr>
                                    <td>
                                        <p>
                                            <span style="font-size:25px;color:black !important;font-weight:bold;">{{headerFooters.company.nameArabic}}.</span><br />
                                            <span style="font-size:17px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryArabic}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">رقم السجل التجاري :{{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:15px;color:black !important;font-weight:bold;">
                                                هاتف: {{headerFooters.company.phoneNo}}:
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-bottom:10px !important;height:10mm">
                            <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">Day End Report/ تقرير اليوم</span>
                            </p>
                        </div>
                    </div>
                </div>
                

                <div style="margin-top:1mm; border:2px solid #000000;">
                    <div class="row mt-3">
                        <div class="col-md-12 ml-2 mr-2" style="font-size:16px;">
                            <div class="col-12 mb-2">
                                <h4>Sale Information/معلومات البيع</h4>
                            </div>
                            <div class="row ml-2 mr-2">
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Cash Detail/التفاصيل النقدية</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(cash,i) in printDetails.cashSale" v-bind:key="i">
                                            <td style="padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; "> <span>{{cash.name}}</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{cash.amount>=0?'Dr':'Cr'}} {{nonNegative(cash.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style=" padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; font-weight: bold; "> <span>Total</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{caculateCashSaleTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateCashSaleTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Bank Detail/تفاصيل البنك</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(bank,j) in printDetails.bankSale" v-bind:key="j">
                                            <td style="text-align:left;font-weight:bold;color:black !important;padding-top: 0px !important; padding-bottom: 0px !important;"> <span>{{bank.name}}</span></td>
                                            <td style="text-align: right; color: black !important; padding-top: 0px !important; padding-bottom: 0px !important; color: black !important;"> <span>{{bank.amount>=0?'Dr':'Cr'}} {{nonNegative(bank.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style="text-align:left;font-weight:bold;padding-top: 0px !important; padding-bottom: 0px !important; color:black !important"> <span style="padding:0; margin:0;">Total</span></td>
                                            <td style="text-align: right; color: black !important;padding-top: 0px !important; padding-bottom: 0px !important;" colspan="3"> <span>{{caculateBankSaleTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateBankSaleTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row mt-3">
                        <div class="col-md-12 ml-2 mr-2" style="font-size:16px;">
                            <div class="col-12 mb-2">
                                <h4>Expense Information/معلومات المصاريف</h4>
                            </div>
                            <div class="row ml-2 mr-2">
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Cash Detail/التفاصيل النقدية</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(expense,k) in printDetails.cashExpense" v-bind:key="k">
                                            <td style="padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; "> <span>{{expense.name}}</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{expense.amount>=0?'Dr':'Cr'}} {{nonNegative(expense.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style=" padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; font-weight: bold; "> <span>Total</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{caculateCashExpenseTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateCashExpenseTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Bank Detail/تفاصيل البنك</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(expBank,l) in printDetails.bankExpense" v-bind:key="l">
                                            <td style="text-align:left;font-weight:bold;color:black !important;padding-top: 0px !important; padding-bottom: 0px !important;"> <span>{{expBank.name}}</span></td>
                                            <td style="text-align: right; color: black !important; padding-top: 0px !important; padding-bottom: 0px !important; color: black !important;"> <span>{{expBank.amount>=0?'Dr':'Cr'}} {{nonNegative(expBank.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style="text-align:left;font-weight:bold;padding-top: 0px !important; padding-bottom: 0px !important; color:black !important"> <span style="padding:0; margin:0;">Total</span></td>
                                            <td style="text-align: right; color: black !important;padding-top: 0px !important; padding-bottom: 0px !important;" colspan="3"> <span>{{caculateBankExpenseTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateBankExpenseTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row mt-3">
                        <div class="col-md-12 ml-2 mr-2" style="font-size:16px;">
                            <div class="col-12 mb-2">
                                <h4>Customer Pay Information/ معلومات دفع العميل</h4>
                            </div>
                            <div class="row ml-2 mr-2">
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Cash Detail/التفاصيل النقدية</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(receipt,m) in printDetails.cashCustomerReceipt" v-bind:key="m">
                                            <td style="padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; "> <span>{{receipt.name}}</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{receipt.amount>=0?'Dr':'Cr'}} {{nonNegative(receipt.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style=" padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; font-weight: bold; "> <span>Total</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{caculateCashCustomerReceiptTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateCashCustomerReceiptTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Bank Detail/تفاصيل البنك</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(receiptBank,n) in printDetails.bankCustomerReceipt" v-bind:key="n">
                                            <td style="text-align:left;font-weight:bold;color:black !important;padding-top: 0px !important; padding-bottom: 0px !important;"> <span>{{receiptBank.name}}</span></td>
                                            <td style="text-align: right; color: black !important; padding-top: 0px !important; padding-bottom: 0px !important; color: black !important;"> <span>{{receiptBank.amount>=0?'Dr':'Cr'}} {{nonNegative(receiptBank.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style="text-align:left;font-weight:bold;padding-top: 0px !important; padding-bottom: 0px !important; color:black !important"> <span style="padding:0; margin:0;">Total</span></td>
                                            <td style="text-align: right; color: black !important;padding-top: 0px !important; padding-bottom: 0px !important;" colspan="3"> <span>{{caculateBankCustomerReceiptTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateBankCustomerReceiptTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row mt-3">
                        <div class="col-md-12 ml-2 mr-2" style="font-size:16px;">
                            <div class="col-12 mb-2">
                                <h4>Purchase Information/ معلومات الشراء</h4>
                            </div>
                            <div class="row ml-2 mr-2">
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Cash Detail/التفاصيل النقدية</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(purchase,o) in printDetails.cashPurchase" v-bind:key="o">
                                            <td style="padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; "> <span>{{purchase.name}}</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{purchase.amount>=0?'Dr':'Cr'}} {{nonNegative(purchase.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style=" padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; font-weight: bold; "> <span>Total</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{caculateCashPurchaseTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateCashPurchaseTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Bank Detail/تفاصيل البنك</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(purchaseBank,p) in printDetails.bankPurchase" v-bind:key="p">
                                            <td style="text-align:left;font-weight:bold;color:black !important;padding-top: 0px !important; padding-bottom: 0px !important;"> <span>{{purchaseBank.name}}</span></td>
                                            <td style="text-align: right; color: black !important; padding-top: 0px !important; padding-bottom: 0px !important; color: black !important;"> <span>{{purchaseBank.amount>=0?'Dr':'Cr'}} {{nonNegative(purchaseBank.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style="text-align:left;font-weight:bold;padding-top: 0px !important; padding-bottom: 0px !important; color:black !important"> <span style="padding:0; margin:0;">Total</span></td>
                                            <td style="text-align: right; color: black !important;padding-top: 0px !important; padding-bottom: 0px !important;" colspan="3"> <span>{{caculateBankPurchaseTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateBankPurchaseTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row mt-3">
                        <div class="col-md-12 ml-2 mr-2" style="font-size:16px;">
                            <div class="col-12 mb-2">
                                <h4>Purchase Expense Information/معلومات مصاريف الشراء</h4>
                            </div>
                            <div class="row ml-2 mr-2">
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Cash Detail/التفاصيل النقدية</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(pExpense,q) in printDetails.cashPurchaseExpense" v-bind:key="q">
                                            <td style="padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; "> <span>{{pExpense.name}}</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{pExpense.amount>=0?'Dr':'Cr'}} {{nonNegative(pExpense.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style=" padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; font-weight: bold; "> <span>Total</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{caculateCashPurchaseExpenseTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateCashPurchaseExpenseTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Bank Detail/تفاصيل البنك</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(pExpenseBank,r) in printDetails.bankPurchaseExpense" v-bind:key="r">
                                            <td style="text-align:left;font-weight:bold;color:black !important;padding-top: 0px !important; padding-bottom: 0px !important;"> <span>{{pExpenseBank.name}}</span></td>
                                            <td style="text-align: right; color: black !important; padding-top: 0px !important; padding-bottom: 0px !important; color: black !important;"> <span>{{pExpenseBank.amount>=0?'Dr':'Cr'}} {{nonNegative(pExpenseBank.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style="text-align:left;font-weight:bold;padding-top: 0px !important; padding-bottom: 0px !important; color:black !important"> <span style="padding:0; margin:0;">Total</span></td>
                                            <td style="text-align: right; color: black !important;padding-top: 0px !important; padding-bottom: 0px !important;" colspan="3"> <span>{{caculateBankPurchaseExpenseTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateBankPurchaseExpenseTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>
                    <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                    <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>
                    <div class="row mt-3">
                        <div class="col-md-12 ml-2 mr-2" style="font-size:16px;">
                            <div class="col-12 mb-2">
                                <h4>Supplier Paid Information/معلومات المورد المدفوعة</h4>
                            </div>
                            <div class="row ml-2 mr-2">
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Cash Detail/التفاصيل النقدية</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(supplier,s) in printDetails.cashSupplierPay" v-bind:key="s">
                                            <td style="padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; "> <span>{{supplier.name}}</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{supplier.amount>=0?'Dr':'Cr'}} {{nonNegative(supplier.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style=" padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; font-weight: bold; "> <span>Total</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{caculateCashSupplierPayTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateCashSupplierPayTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Bank Detail/تفاصيل البنك</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(supplierBank,t) in printDetails.bankSupplierPay" v-bind:key="t">
                                            <td style="text-align:left;font-weight:bold;color:black !important;padding-top: 0px !important; padding-bottom: 0px !important;"> <span>{{supplierBank.name}}</span></td>
                                            <td style="text-align: right; color: black !important; padding-top: 0px !important; padding-bottom: 0px !important; color: black !important;"> <span>{{supplierBank.amount>=0?'Dr':'Cr'}} {{nonNegative(supplierBank.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style="text-align:left;font-weight:bold;padding-top: 0px !important; padding-bottom: 0px !important; color:black !important"> <span style="padding:0; margin:0;">Total</span></td>
                                            <td style="text-align: right; color: black !important;padding-top: 0px !important; padding-bottom: 0px !important;" colspan="3"> <span>{{caculateBankSupplierPayTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateBankSupplierPayTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row mt-3">
                        <div class="col-md-12 ml-2 mr-2" style="font-size:16px;">
                            <div class="col-12 mb-2">
                                <h4>Other Paid Information/معلومات الدفع الأخرى</h4>
                            </div>
                            <div class="row ml-2 mr-2">
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Cash Detail/التفاصيل النقدية</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(other,u) in printDetails.otherCashPayments" v-bind:key="u">
                                            <td style="padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; "> <span>{{other.name}}</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{other.amount>=0?'Dr':'Cr'}} {{nonNegative(other.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style=" padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; font-weight: bold; "> <span>Total</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{caculateOtherCashPaymentTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateOtherCashPaymentTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Bank Detail/تفاصيل البنك</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(otherBank,v) in printDetails.otherBankPayments" v-bind:key="v">
                                            <td style="text-align:left;font-weight:bold;color:black !important;padding-top: 0px !important; padding-bottom: 0px !important;"> <span>{{otherBank.name}}</span></td>
                                            <td style="text-align: right; color: black !important; padding-top: 0px !important; padding-bottom: 0px !important; color: black !important;"> <span>{{otherBank.amount>=0?'Dr':'Cr'}} {{nonNegative(otherBank.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style="text-align:left;font-weight:bold;padding-top: 0px !important; padding-bottom: 0px !important; color:black !important"> <span style="padding:0; margin:0;">Total</span></td>
                                            <td style="text-align: right; color: black !important;padding-top: 0px !important; padding-bottom: 0px !important;" colspan="3"> <span>{{caculateOtherBankPaymentTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateOtherBankPaymentTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row mt-3">
                        <div class="col-md-12 ml-2 mr-2" style="font-size:16px;">
                            <div class="col-12 mb-2">
                                <h4>Sale Return Information/ معلومات إرجاع البيع</h4>
                            </div>
                            <div class="row ml-2 mr-2">
                                <div class="col-md-6 pl-2 pr-2">
                                    <div class="col-12 mb-2">
                                        <h5>Cash Detail/التفاصيل النقدية</h5>
                                    </div>
                                    <table class="table  ">

                                        <tr style="font-size: 14px;  " v-for="(saleReturn,x) in printDetails.saleReturn" v-bind:key="x">
                                            <td style="padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; "> <span>{{saleReturn.name}}</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{saleReturn.amount>=0?'Dr':'Cr'}} {{nonNegative(saleReturn.amount) }}</span></td>

                                        </tr>

                                        <tr style="font-size:14px; ">
                                            <td style=" padding-top: 0px !important; font-weight: bold; padding-bottom: 0px !important; color: black !important; font-weight: bold; "> <span>Total</span></td>
                                            <td style="padding-top: 0px !important;  padding-bottom: 0px !important; color: black !important; "> <span>{{caculateCahSaleReturnTotal>=0?'Dr':'Cr'}} {{nonNegative(caculateCahSaleReturnTotal) }}</span></td>

                                        </tr>


                                    </table>
                                </div>
                                <div class="col-md-6 pl-2 pr-2">
                                </div>
                            </div>
                        </div>

                    </div>

                   
                </div>

             
            </div>
        </div>
    </div>
</template>

<script>
    import moment from "moment";
    //import axios from 'axios'
    const options = {
        name: '',
        specs: [
            'fullscreen=no',
            'titlebar=yes',
            'scrollbars=yes'
        ],
        styles: [
            'https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css',
            'https://unpkg.com/kidlat-css/css/kidlat.css'
        ],
        timeout: 700,
        autoClose: true,
        windowTitle: window.document.title
    }
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['printDetail', 'headerFooter'],
        data: function () {
            return {
                isBankDetailShow: false,
                isHeaderFooter: '',
                arabic: '',
                english: '',
                UserName: '',
                total: 0,
                grandTotal: 0,
                transactions: 0,
                printDetails: [],

                render: 0,
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                htmlData: {
                    htmlString: ''
                }
            }
        },
        computed: {
            caculateCashSaleTotal: function () {
                return this.printDetails.cashSale.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateBankSaleTotal: function () {
                return this.printDetails.bankSale.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateCashExpenseTotal: function () {
                return this.printDetails.cashExpense.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateBankExpenseTotal: function () {
                return this.printDetails.bankExpense.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateCashCustomerReceiptTotal: function () {
                return this.printDetails.cashCustomerReceipt.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateBankCustomerReceiptTotal: function () {
                return this.printDetails.bankCustomerReceipt.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateCashPurchaseTotal: function () {
                return this.printDetails.cashPurchase.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateBankPurchaseTotal: function () {
                return this.printDetails.bankPurchase.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateCashPurchaseExpenseTotal: function () {
                return this.printDetails.cashPurchaseExpense.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateBankPurchaseExpenseTotal: function () {
                return this.printDetails.bankPurchaseExpense.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateCashSupplierPayTotal: function () {
                return this.printDetails.cashSupplierPay.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateBankSupplierPayTotal: function () {
                return this.printDetails.bankSupplierPay.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateOtherCashPaymentTotal: function () {
                return this.printDetails.otherCashPayments.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateOtherBankPaymentTotal: function () {
                return this.printDetails.otherBankPayments.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
            caculateCahSaleReturnTotal: function () {
                return this.printDetails.saleReturn.reduce(function (a, c) { return a + Number(c.amount || 0) }, 0)
            },
        },
        methods: {
            nonNegative: function (value) {
                return parseFloat(Math.abs(value)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },
            logout: function () {

                var root = this;
                var url = '/account/logout';
                this.$https.post(url, this.login).then(function (response) {

                    if (response.data == "Success") {

                        //root.$session.destroy();
                        root.$store.commit('logout');
                        localStorage.clear();
                        //document.cookie.split(';').forEach(cookie => document.cookie = cookie.replace(/^ +/, '').replace(/=.*/, `=;expires=${new Date(0).toUTCString()};path=/`));

                        root.$router.push('/')
                    }
                    else {
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Error Logging Out'
                            });
                    }

                });

            },
            getDate: function () {

                return moment(this.printDetails.startDate).format(' YYYY-MM-DD');
            },
            getTimeOnly: function (x) {
                return moment(x).format('HH:mm ');
            },
            printInvoice: function () {
                //var root = this;
                //this.$htmlToPaper('purchaseInvoice');

                this.$htmlToPaper('purchaseInvoice', options, () => {
                    //root.$router.go();
                    this.$emit('close', true)

                });
            },
        },
        mounted: function () {
            
            //this.isBankDetailShow = localStorage.getItem('BankDetail') == 'true' ? true : false;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            var root = this;

            this.printDetails = this.printDetail;
            this.printDetails.user = localStorage.getItem('LoginUserName');
            this.headerFooters = this.headerFooter;
            setTimeout(function () {
                root.printInvoice();
            }, 125);
        },

    }
</script>

