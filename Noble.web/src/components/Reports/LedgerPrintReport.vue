<template>
    <div>
        <div hidden id='inventoryDetailReport' class="col-md-12" style="background-color:white;padding-left:40px !important;padding-right:40px !important ">
            <div style="background-color:white" v-if="itemTotal<=22">
                <!--HEADER-->
                <div class="col-md-12" style="height:35mm;background-color:white;border:2px solid #000000" v-if="IsPaksitanClient">
                    <div class="row" style="height:35mm;background-color:white">
                        <div class="col-md-4  my-5" style="padding:0px !important; margin:0 !important">
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px;  margin:0 !important;padding:5px">
                        </div>
                        <div class="col-md-8 ">
                            <table class="text-center">
                                <tr>
                                    <td>
                                        <p>
                                            <u style="font-size:16px;color:black !important;font-weight:bold;">
                                                Sales Tax Invoice
                                            </u><br />
                                            <span style="font-size:27px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">{{headerFooters.company.addressEnglish}}</span><br />
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:left">NTN :&nbsp;&nbsp;&nbsp; {{headerFooters.company.vatRegistrationNo}}</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <!--<div class="col-md-4 ">
                <table class="text-right" v-if="arabic=='true'">
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
            </div>-->
                    </div>
                    <!--<div class="row " style="margin-bottom:10px !important;height:10mm">
            <div class="col-md-12">
                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                        {{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Sale Invoice ' : 'فاتورة البيع' }}
                        <span v-if="list.invoiceType==0">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Hold)' : '(معلق)' }} </span>
                        <span v-if="list.partiallyInvoice=='Fully' && list.isCredit">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Paid)' : '(مدفوعة)' }} </span>
                    </span>
                </p>
            </div>
        </div>-->
                </div>
                <div class="col-md-12" style="height:45mm;border:2px solid #000000;background-color:white" v-else-if="isHeaderFooter=='true'">
                    <div class="row" style="height:35mm;background-color:white">
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
                            <table class="text-right" v-if="arabic=='true'">
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
                    <div class="row" style="background-color:white">
                        <div class="col-md-12" style="margin-bottom:10px !important;height:10mm" v-if="($i18n.locale == 'en' ||isLeftToRight())">
                            <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" >Account Ledger Report</span>
                            </p>
                        </div>
                       
                    </div>
                </div>
                <div style="height:45mm;" v-else></div>
                <!--<div v-else style="height: 40mm;">

    </div>-->
                <!--INFORMATION-->
                <div class=" col-md-12 " style="background-color:white;border:2px solid #000000;">
                    <div class="row" v-if="isShown || ledger.transactionList.length > 0" style="background-color:white">
                        <div class="col-md-6" style="display:flex;">
                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{fDate}}</div>
                            <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important" v-if="arabic=='true'">:من التاريخ</div>
                        </div>
                        <div class="col-md-6" style="display:flex;">
                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">To Date:</div>
                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;"><span>{{tDate}}</span> </div>
                            <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="arabic=='true'">
                                :حتي اليوم
                            </div>
                        </div>
                        <div class="col-md-6" style="display:flex;">
                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Account Name:</div>
                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{ledger.transactionList[0].accountName}}</div>
                            <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important" v-if="arabic=='true'">:أسم الحساب</div>
                        </div>
                        <div class="col-md-6" style="display:flex;">
                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Opening Balanace:</div>
                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;"><span>{{ledger.openingBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.openingBalance) }}</span> </div>
                            <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="arabic=='true'">
                                :الرصيد الافتتاحي
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 " style="background-color:white;border:2px solid #000000">
                    <div class="row p-1" style="background-color:white;height:294mm !important;">

                        <div class="col-12">
                            <table class="table " style="background-color:white;">
                            <tr style="font-size:14px;color:black !important;color:black;border-top:0px !important">
                                <th class="text-left" style="border-top:0px !important">#</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.Date') }}</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.TransactionType') }}</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.DocumentNumber') }}</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.Narration') }}</th>
                                <th class="text-right" style="border-top:0px !important">{{ $t('LedgerPrintReport.Debit') }}</th>
                                <th class="text-right" style="border-top:0px !important">{{ $t('LedgerPrintReport.Credit') }}</th>
                                <th class="text-right" style="border-top:0px !important;width:15%" >{{ $t('LedgerPrintReport.Balance') }}</th>
                            </tr>
                            <tr style="font-size:13px;height: 20px; color:black" v-for="(item, index) in ledger.transactionList" v-bind:key="item.id">
                                <td class="text-left">{{index+1}}</td>
                                <td class="text-left">{{item.date}}</td>
                                <td class="text-left">{{getTransactionType(item.transactionType)}}</td>
                                <td class="text-left">{{item.documentNumber}}</td>
                                <td class="text-left">{{getTransactionType(item.description)}}</td>
                                <td class="text-right">
                                    {{  nonNegative(item.debitAmount)}}
                                </td>
                                <td class="text-right">
                                    {{nonNegative(item.creditAmount)}}
                                </td>
                                <td class="text-right">
                                    {{item.openingBalance>0?'Dr':'Cr'}} {{nonNegative(item.openingBalance) }}
                                </td>
                            </tr>

                            <tr style="font-size:13px;color:black">
                                <td class="text-right" colspan="5">
                                    <h6>{{ $t('LedgerPrintReport.ClosingBalance') }}:</h6>
                                </td>
                                <td class="text-right">
                                    <h6>{{  nonNegative(ledger.totalDebit)}}</h6>
                                </td>
                                <td class="text-right">
                                    <h6>{{nonNegative(ledger.totalCredit)}}</h6>
                                </td>
                                <td class="text-right">
                                    <h6> {{ledger.runningBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.runningBalance) }}</h6>
                                </td>
                            </tr>

                        </table>
                        </div>
                       


                    </div>
                    <div class="row pt-1" style=" background-color:white;font-size:16px">
                        <table class="table text-center">
                            <tr>
                                <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                                    Prepared By
                                </td>
                                <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                                    Approved By
                                </td>
                                <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                                    Received By

                                </td>
                            </tr>


                        </table>
                        <table class="table text-center">

                            <tr>
                                <td style="width: 20%;border:0px;color:black;font-weight:bold">
                                </td>
                                <td style="width: 60%;border:0px;color:black;font-weight:bold">
                                    Tel: &nbsp; &nbsp; &nbsp;{{headerFooters.company.phoneNo}} &nbsp; &nbsp; &nbsp; Email:&nbsp; &nbsp; &nbsp;{{headerFooters.company.email}}
                                </td>
                                <td style="width: 20%;border:0px;color:black;font-weight:bold">
                                </td>
                            </tr>

                        </table>
                    </div>

                </div>
            </div>
            <div style="background-color:white" v-else-if="itemTotal>22 && itemTotal<=44">
                <!--HEADER-->
                <div style="background-color:white;">
                    <div class="col-md-12" style="height:35mm;background-color:white;border:2px solid #000000" v-if="IsPaksitanClient">
                        <div class="row" style="height:35mm;background-color:white">
                            <div class="col-md-4  my-5" style="padding:0px !important; margin:0 !important">
                                <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px;  margin:0 !important;padding:5px">
                            </div>
                            <div class="col-md-8 ">
                                <table class="text-center">
                                    <tr>
                                        <td>
                                            <p>
                                                <u style="font-size:16px;color:black !important;font-weight:bold;">
                                                    Sales Tax Invoice
                                                </u><br />
                                                <span style="font-size:27px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                                <span style="font-size:16px;color:black !important;font-weight:bold;">{{headerFooters.company.addressEnglish}}</span><br />
                                                <span style="font-size:18px;color:black !important;font-weight:bold;float:left">NTN :&nbsp;&nbsp;&nbsp; {{headerFooters.company.vatRegistrationNo}}</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <span style="font-size:18px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <!--<div class="col-md-4 ">
                    <table class="text-right" v-if="arabic=='true'">
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
                </div>-->
                        </div>
                        <!--<div class="row " style="margin-bottom:10px !important;height:10mm">
                <div class="col-md-12">
                    <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                        <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                            {{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Sale Invoice ' : 'فاتورة البيع' }}
                            <span v-if="list.invoiceType==0">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Hold)' : '(معلق)' }} </span>
                            <span v-if="list.partiallyInvoice=='Fully' && list.isCredit">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Paid)' : '(مدفوعة)' }} </span>
                        </span>
                    </p>
                </div>
            </div>-->
                    </div>
                    <div class="col-md-12" style="height:45mm;border:2px solid #000000;background-color:white" v-else-if="isHeaderFooter=='true'">
                        <div class="row" style="height:35mm;background-color:white">
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
                                <table class="text-right" v-if="arabic=='true'">
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
                        <div class="row" style="background-color:white">
                            <div class="col-md-12" style="margin-bottom:10px !important;height:10mm" v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">Account Ledger Report</span>
                                </p>
                            </div>

                        </div>
                    </div>
                    <div style="height:45mm;" v-else></div>
                    <!--<div v-else style="height: 40mm;">

        </div>-->
                    <!--INFORMATION-->
                    <div class=" col-md-12 " style="background-color:white;border:2px solid #000000;">
                        <div class="row" v-if="isShown || ledger.transactionList.length > 0" style="background-color:white">
                            <div class="col-md-6" style="display:flex;">
                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                                <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{fDate}}</div>
                                <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important" v-if="arabic=='true'">:من التاريخ</div>
                            </div>
                            <div class="col-md-6" style="display:flex;">
                                <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">To Date:</div>
                                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;"><span>{{tDate}}</span> </div>
                                <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="arabic=='true'">
                                    :حتي اليوم
                                </div>
                            </div>
                            <div class="col-md-6" style="display:flex;">
                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Account Name:</div>
                                <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{ledger.transactionList[0].accountName}}</div>
                                <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important" v-if="arabic=='true'">:أسم الحساب</div>
                            </div>
                            <div class="col-md-6" style="display:flex;">
                                <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Opening Balanace:</div>
                                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;"><span>{{ledger.openingBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.openingBalance) }}</span> </div>
                                <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="arabic=='true'">
                                    :الرصيد الافتتاحي
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12 " style="background-color:white;border:2px solid #000000;padding-left:40px !important;padding-right:40px !important">
                        <div class="row p-1" style="background-color:white;height:310mm;">
                            <div class="col-12">
                                <table class="table col-md-12 ">
                                <tr style="font-size:14px;color:black !important;color:black;border-top:0px !important">
                                    <th class="text-left" style="border-top:0px !important">#</th>
                                    <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.Date') }}</th>
                                    <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.TransactionType') }}</th>
                                    <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.DocumentNumber') }}</th>
                                    <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.Narration') }}</th>
                                    <th class="text-right" style="border-top:0px !important">{{ $t('LedgerPrintReport.Debit') }}</th>
                                    <th class="text-right" style="border-top:0px !important">{{ $t('LedgerPrintReport.Credit') }}</th>
                                    <th class="text-right" style="border-top:0px !important;width:15%">{{ $t('LedgerPrintReport.Balance') }}</th>
                                </tr>
                                <tr style="font-size:13px; page-break-after: always;color:black" v-for="(item, index) in listItemP1" v-bind:key="item.id">
                                    <td class="text-left">{{index+1}}</td>
                                    <td class="text-left">{{item.date}}</td>
                                    <td class="text-left">{{getTransactionType(item.transactionType)}}</td>
                                    <td class="text-left">{{item.documentNumber}}</td>
                                    <td class="text-left">{{getTransactionType(item.description)}}</td>
                                    <td class="text-right">
                                        {{  nonNegative(item.debitAmount)}}
                                    </td>
                                    <td class="text-right">
                                        {{nonNegative(item.creditAmount)}}
                                    </td>
                                    <td class="text-right">
                                        {{item.openingBalance>0?'Dr':'Cr'}} {{nonNegative(item.openingBalance) }}
                                    </td>
                                </tr>


                            </table>

                            </div>

                            

                        </div>


                    </div>
                </div>
                <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>


                <!--HEADER-->
                <div class="col-md-12" style="height:35mm;background-color:white;border:2px solid #000000" v-if="IsPaksitanClient">
                    <div class="row" style="height:35mm;background-color:white">
                        <div class="col-md-4  my-5" style="padding:0px !important; margin:0 !important">
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px;  margin:0 !important;padding:5px">
                        </div>
                        <div class="col-md-8 ">
                            <table class="text-center">
                                <tr>
                                    <td>
                                        <p>
                                            <u style="font-size:16px;color:black !important;font-weight:bold;">
                                                Sales Tax Invoice
                                            </u><br />
                                            <span style="font-size:27px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">{{headerFooters.company.addressEnglish}}</span><br />
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:left">NTN :&nbsp;&nbsp;&nbsp; {{headerFooters.company.vatRegistrationNo}}</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <!--<div class="col-md-4 ">
                <table class="text-right" v-if="arabic=='true'">
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
            </div>-->
                    </div>
                    <!--<div class="row " style="margin-bottom:10px !important;height:10mm">
            <div class="col-md-12">
                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                        {{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Sale Invoice ' : 'فاتورة البيع' }}
                        <span v-if="list.invoiceType==0">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Hold)' : '(معلق)' }} </span>
                        <span v-if="list.partiallyInvoice=='Fully' && list.isCredit">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Paid)' : '(مدفوعة)' }} </span>
                    </span>
                </p>
            </div>
        </div>-->
                </div>
                <div class="col-md-12" style="height:45mm;border:2px solid #000000;background-color:white" v-else-if="isHeaderFooter=='true'">
                    <div class="row" style="height:35mm;background-color:white">
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
                            <table class="text-right" v-if="arabic=='true'">
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
                    <div class="row" style="background-color:white">
                        <div class="col-md-12" style="margin-bottom:10px !important;height:10mm" v-if="($i18n.locale == 'en' ||isLeftToRight())">
                            <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">Account Ledger Report</span>
                            </p>
                        </div>

                    </div>
                </div>
                <div style="height:45mm;" v-else></div>
                <!--<div v-else style="height: 40mm;">

    </div>-->
                <!--INFORMATION-->
                <div class=" col-md-12 " style="background-color:white;border:2px solid #000000;">
                    <div class="row" v-if="isShown || ledger.transactionList.length > 0" style="background-color:white">
                        <div class="col-md-6" style="display:flex;">
                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{fDate}}</div>
                            <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important" v-if="arabic=='true'">:من التاريخ</div>
                        </div>
                        <div class="col-md-6" style="display:flex;">
                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">To Date:</div>
                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;"><span>{{tDate}}</span> </div>
                            <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="arabic=='true'">
                                :حتي اليوم
                            </div>
                        </div>
                        <div class="col-md-6" style="display:flex;">
                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Account Name:</div>
                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{ledger.transactionList[0].accountName}}</div>
                            <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important" v-if="arabic=='true'">:أسم الحساب</div>
                        </div>
                        <div class="col-md-6" style="display:flex;">
                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Opening Balanace:</div>
                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;"><span>{{ledger.openingBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.openingBalance) }}</span> </div>
                            <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="arabic=='true'">
                                :الرصيد الافتتاحي
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 " style="background-color:white;border:2px solid #000000;padding-left:40px !important;padding-right:40px !important">
                    <div class="row p-1" style="background-color:white;height:290mm;">
                        <div class="col-12">
                            <table class="table col-md-12 ">
                            <tr style="font-size:14px;color:black !important;color:black;border-top:0px !important">
                                <th class="text-left" style="border-top:0px !important">#</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.Date') }}</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.TransactionType') }}</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.DocumentNumber') }}</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.Narration') }}</th>
                                <th class="text-right" style="border-top:0px !important">{{ $t('LedgerPrintReport.Debit') }}</th>
                                <th class="text-right" style="border-top:0px !important">{{ $t('LedgerPrintReport.Credit') }}</th>
                                <th class="text-right" style="border-top:0px !important">{{ $t('LedgerPrintReport.Balance') }}</th>
                            </tr>
                            <tr style="font-size:13px; page-break-after: always;color:black" v-for="(item, index) in listItemP2" v-bind:key="item.id">
                                <td class="text-left">{{index+26}}</td>
                                <td class="text-left">{{item.date}}</td>
                                <td class="text-left">{{getTransactionType(item.transactionType)}}</td>
                                <td class="text-left">{{item.documentNumber}}</td>
                                <td class="text-left">{{getTransactionType(item.description)}}</td>
                                <td class="text-right">
                                    {{  nonNegative(item.debitAmount)}}
                                </td>
                                <td class="text-right">
                                    {{nonNegative(item.creditAmount)}}
                                </td>
                                <td class="text-right">
                                    {{item.openingBalance>0?'Dr':'Cr'}} {{nonNegative(item.openingBalance) }}
                                </td>
                            </tr>

                            <tr style="font-size:13px;color:black">
                                <td class="text-right" colspan="5">
                                    <h6>{{ $t('LedgerPrintReport.ClosingBalance') }}:</h6>
                                </td>
                                <td class="text-right">
                                    <h6>{{  nonNegative(ledger.totalDebit)}}</h6>
                                </td>
                                <td class="text-right">
                                    <h6>{{nonNegative(ledger.totalCredit)}}</h6>
                                </td>
                                <td class="text-right">
                                    <h6> {{ledger.runningBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.runningBalance) }}</h6>
                                </td>
                            </tr>

                        </table>
                        </div>

                        


                    </div>
                    <div class="row pt-1" style=" background-color:white;font-size:16px">
                        <table class="table text-center">
                            <tr>
                                <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                                    Prepared By
                                </td>
                                <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                                    Approved By
                                </td>
                                <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                                    Received By

                                </td>
                            </tr>


                        </table>
                        <table class="table text-center">

                            <tr>
                                <td style="width: 20%;border:0px;color:black;font-weight:bold">
                                </td>
                                <td style="width: 60%;border:0px;color:black;font-weight:bold">
                                    Tel: &nbsp; &nbsp; &nbsp;{{headerFooters.company.phoneNo}} &nbsp; &nbsp; &nbsp; Email:&nbsp; &nbsp; &nbsp;{{headerFooters.company.email}}
                                </td>
                                <td style="width: 20%;border:0px;color:black;font-weight:bold">
                                </td>
                            </tr>

                        </table>
                    </div>

                </div>

            </div>
            <div style="background-color:white" v-else>
                <!--HEADER-->
                <div class="col-md-12" style="height:35mm;background-color:white;border:2px solid #000000" v-if="IsPaksitanClient">
                    <div class="row" style="height:35mm;background-color:white">
                        <div class="col-md-4  my-5" style="padding:0px !important; margin:0 !important">
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px;  margin:0 !important;padding:5px">
                        </div>
                        <div class="col-md-8 ">
                            <table class="text-center">
                                <tr>
                                    <td>
                                        <p>
                                            <u style="font-size:16px;color:black !important;font-weight:bold;">
                                                Sales Tax Invoice
                                            </u><br />
                                            <span style="font-size:27px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                            <span style="font-size:16px;color:black !important;font-weight:bold;">{{headerFooters.company.addressEnglish}}</span><br />
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:left">NTN :&nbsp;&nbsp;&nbsp; {{headerFooters.company.vatRegistrationNo}}</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <!--<div class="col-md-4 ">
                <table class="text-right" v-if="arabic=='true'">
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
            </div>-->
                    </div>
                    <!--<div class="row " style="margin-bottom:10px !important;height:10mm">
            <div class="col-md-12">
                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                        {{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Sale Invoice ' : 'فاتورة البيع' }}
                        <span v-if="list.invoiceType==0">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Hold)' : '(معلق)' }} </span>
                        <span v-if="list.partiallyInvoice=='Fully' && list.isCredit">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Paid)' : '(مدفوعة)' }} </span>
                    </span>
                </p>
            </div>
        </div>-->
                </div>
                <div class="col-md-12" style="height:45mm;border:2px solid #000000;background-color:white" v-else-if="isHeaderFooter=='true'">
                    <div class="row" style="height:35mm;background-color:white">
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
                            <table class="text-right" v-if="arabic=='true'">
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
                    <div class="row" style="background-color:white">
                        <div class="col-md-12" style="margin-bottom:10px !important;height:10mm" v-if="($i18n.locale == 'en' ||isLeftToRight())">
                            <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">Account Ledger Report</span>
                            </p>
                        </div>

                    </div>
                </div>
                <div style="height:45mm;" v-else></div>
                <!--<div v-else style="height: 40mm;">

    </div>-->
                <!--INFORMATION-->
                <div class=" col-md-12 " style="background-color:white;border:2px solid #000000;">
                    <div class="row" v-if="isShown || ledger.transactionList.length > 0" style="background-color:white">
                        <div class="col-md-6" style="display:flex;">
                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{fDate}}</div>
                            <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important" v-if="arabic=='true'">:من التاريخ</div>
                        </div>
                        <div class="col-md-6" style="display:flex;">
                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">To Date:</div>
                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;"><span>{{tDate}}</span> </div>
                            <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="arabic=='true'">
                                :حتي اليوم
                            </div>
                        </div>
                        <div class="col-md-6" style="display:flex;">
                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Account Name:</div>
                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{ledger.transactionList[0].accountName}}</div>
                            <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important" v-if="arabic=='true'">:أسم الحساب</div>
                        </div>
                        <div class="col-md-6" style="display:flex;">
                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Opening Balanace:</div>
                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;"><span>{{ledger.openingBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.openingBalance) }}</span> </div>
                            <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="arabic=='true'">
                                :الرصيد الافتتاحي
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 " style="background-color:white;padding-left:40px !important;padding-right:40px !important">
                    <div class="row p-1" style="background-color:white;height:310mm;">
                        <div class="col-12">
                            <table class="table col-md-12 ">
                            <tr style="font-size:14px;color:black !important;color:black;border-top:0px !important">
                                <th class="text-left" style="border-top:0px !important">#</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.Date') }}</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.TransactionType') }}</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.DocumentNumber') }}</th>
                                <th class="text-left" style="border-top:0px !important">{{ $t('LedgerPrintReport.Narration') }}</th>
                                <th class="text-right" style="border-top:0px !important">{{ $t('LedgerPrintReport.Debit') }}</th>
                                <th class="text-right" style="border-top:0px !important">{{ $t('LedgerPrintReport.Credit') }}</th>
                                <th class="text-right" style="border-top:0px !important;width:15%">{{ $t('LedgerPrintReport.Balance') }}</th>
                            </tr>
                            <tr style="font-size:13px; page-break-after: always;color:black" v-for="(item, index) in ledger.transactionList" v-bind:key="item.id">
                                <td class="text-left">{{index+1}}</td>
                                <td class="text-left">{{item.date}}</td>
                                <td class="text-left">{{getTransactionType(item.transactionType)}}</td>
                                <td class="text-left">{{item.documentNumber}}</td>
                                <td class="text-left">{{getTransactionType(item.description)}}</td>
                                <td class="text-right">
                                    {{  nonNegative(item.debitAmount)}}
                                </td>
                                <td class="text-right">
                                    {{nonNegative(item.creditAmount)}}
                                </td>
                                <td class="text-right">
                                    {{item.openingBalance>0?'Dr':'Cr'}} {{nonNegative(item.openingBalance) }}
                                </td>
                            </tr>

                            <tr style="font-size:13px;color:black">
                                <td class="text-right" colspan="5">
                                    <h6>{{ $t('LedgerPrintReport.ClosingBalance') }}:</h6>
                                </td>
                                <td class="text-right">
                                    <h6>{{  nonNegative(ledger.totalDebit)}}</h6>
                                </td>
                                <td class="text-right">
                                    <h6>{{nonNegative(ledger.totalCredit)}}</h6>
                                </td>
                                <td class="text-right">
                                    <h6> {{ledger.runningBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.runningBalance) }}</h6>
                                </td>
                            </tr>

                        </table>
                        </div>

                      


                    </div>
                    <!--<div class="row pt-1" style=" background-color:white;font-size:16px">
            <table class="table text-center">
                <tr>
                    <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                        Prepared By
                    </td>
                    <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                        Approved By
                    </td>
                    <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                        Received By

                    </td>
                </tr>


            </table>
            <table class="table text-center">

                <tr>
                    <td style="width: 20%;border:0px;color:black;font-weight:bold">
                    </td>
                    <td style="width: 60%;border:0px;color:black;font-weight:bold">
                        Tel: &nbsp; &nbsp; &nbsp;{{headerFooters.company.phoneNo}} &nbsp; &nbsp; &nbsp; Email:&nbsp; &nbsp; &nbsp;{{headerFooters.company.email}}
                    </td>
                    <td style="width: 20%;border:0px;color:black;font-weight:bold">
                    </td>
                </tr>

            </table>
        </div>-->

                </div>
            </div>



        </div>
    </div>

</template>

<script>
    import moment from "moment";

    export default {
        props: ['printDetails', 'isShown', 'formName', 'isPrint', 'dates', 'headerFooter', 'fromDate', 'toDate', ''],
        data: function () {
            return {
                ledger: {
                    transactionList: [],
                    openingBalance: 0,
                    runningBalance: 0,
                    totalCredit: 0,
                    totalDebit: 0,
                },
                itemTotal: 0,
                fDate: '',
                tDate: '',
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                IsPaksitanClient: false,
                Print: false,
                render: 0,
                arabic: '',
                english: '',
                listItemP1: [],
                listItemP2: [],
                listItemP3: [],
            }
        },
        mounted: function () {

            this.fDate = this.fromDate;
            this.tDate = this.toDate;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.IsPaksitanClient = localStorage.getItem('IsPaksitanClient') == "true" ? true : false;

            var root = this;

            this.headerFooters = this.headerFooter
            root.Print = root.isPrint;
            if (this.printDetails.transactionList.length > 0 && root.Print) {
                
                this.ledger = this.printDetails;
                //var totalItem = this.printDetails.transactionList.length;
                //this.itemTotal = totalItem;
                //if (totalItem < 22) {
                //    for (var i = 0; i < totalItem; i++) {
                //        root.listItemP1.push(root.printDetails.transactionList[i]);
                //    }
                //}
                //else if (totalItem >= 22 && totalItem < 44) {
                //    for (var k = 0; k < totalItem; k++) {
                //        if (k < 22) {
                //            root.listItemP1.push(root.printDetails.transactionList[k]);
                //        }
                //        else {
                //            root.listItemP2.push(root.printDetails.transactionList[k]);
                //        }
                //    }
                //}

                setTimeout(function () {
                    root.printInvoice();
                }, 125)
            }
        },
        methods: {
            getTransactionType(transactionType) {
                if (transactionType == 'StockOut') return 'Stock Out'
                else if (transactionType == 'JournalVoucher') return 'Journal Voucher'
                else if (transactionType == 'BankPay') return 'Bank Pay'
                else if (transactionType == 'ExpenseVoucher') return 'Expense Voucher'
                else if (transactionType == 'Expense') return 'Expense'
                else if (transactionType == 'CashPay') return 'Cash Pay'

                else if (transactionType == 'BankReceipt') return 'Bank Receipt'
                else if (transactionType == 'StockIn') return 'Stock In'
                else if (transactionType == 'SaleInvoice') return 'Sale Invoice'
                else if (transactionType == 'PurchaseReturn') return 'PurchaseReturn'
                else if (transactionType == 'PurchasePost') return 'Purchase'
                else if (transactionType == 'CashReceipt') return 'Cash Receipt'
                else {
                    return transactionType;
                }

            },
            nonNegative: function (value) {
                return parseFloat(Math.abs(value)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },
            convertDate: function (x) {
                return moment(x).format('DD MMM YYYY');
            },
            printInvoice: function () {

                this.$htmlToPaper('inventoryDetailReport');
            }
        }
    }
</script>


<style scoped>
    #font11 {
        font-size: 11px;
        line-height: 0;
    }

    #font16 {
        font-size: 16px;
    }
</style>