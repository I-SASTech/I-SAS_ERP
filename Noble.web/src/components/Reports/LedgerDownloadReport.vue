<template>
    <div>
        <div ref="mychildcomponent" hidden id='inventoryDetailReport' class="col-md-7">
            <!--HEADER-->
            <div v-if="itemTotal<=22">
                <div class="col-md-12" style="height:45mm;border:2px solid #000000;background-color:white" v-if="IsPaksitanClient">
                    <table class="table table-borderless">
                        <tr>
                            <td style="width:30%;" class="text-left pt-0 pb-0 pl-0 pr-0">
                                <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px;  margin:0 !important;padding:5px">
                            </td>
                            <td style="width:40%;" class="text-center ">

                                <u style="font-size:14px;color:black !important;font-weight:bold;">
                                    Sales Tax Invoice
                                </u><br />
                                <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                <span style="font-size:14px;color:black !important;font-weight:bold;">{{headerFooters.company.addressEnglish}}</span><br />
                                <span style="font-size:14px;color:black !important;font-weight:bold;float:left">NTN :&nbsp;&nbsp;&nbsp; {{headerFooters.company.vatRegistrationNo}}</span>&nbsp;
                                <span style="font-size:14px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span><br />
                                <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important">Account Ledger Report</span>
                                </span>

                            </td>
                            <td style="width:30%;" class="text-left "> </td>

                        </tr>


                    </table>



                </div>

                <div class="col-md-12" v-else-if="isHeaderFooter=='true'">
                    <table class="table table-borderless">
                        <tr>
                            <td style="width:36%;" class="text-left pt-0 pb-0 pl-0 pr-0">
                                <p class="mb-0">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                    <span style="font-size:15px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryEnglish}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">Cr No.:{{headerFooters.company.companyRegNo}}</span><br />
                                    <span style="font-size:13px;color:black !important;font-weight:bold;">
                                        Tel: {{headerFooters.company.phoneNo}}
                                    </span>
                                </p>
                            </td>
                            <td style="width:26%;" class="text-center pt-0 pb-0 pl-0 pr-0">
                                <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                            </td>
                            <td style="width:38%;" class="pt-0 pb-0 pl-0 pr-0">
                                <p class="text-right mb-0" v-if="arabic=='true'">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameArabic}}.</span><br />
                                    <span style="font-size:15px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryArabic}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم السجل التجاري :{{headerFooters.company.companyRegNo}}</span><br />
                                    <span style="font-size:13px;color:black !important;font-weight:bold;">
                                        هاتف: {{headerFooters.company.phoneNo}}:
                                    </span>
                                </p>
                            </td>
                        </tr>

                        <tr>

                            <td style="width:100%;" class="pt-0 pb-0 pl-0 pr-0" colspan="3">
                                <div style="text-align: center;">
                                    <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important">{{ $t('AccountLedger.AccountLedger') }}</span>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>            <!--INFORMATION-->
                <div v-else style="height: 45mm;">

                </div>
                <!--INFORMATION-->
                <div style="height:25mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-12 ml-2 mr-2">
                            <table class="table table-borderless">
                                <!--Row 1-->
                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">From Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{fromDate}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">:من التاريخ</td>

                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">To Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{toDate}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">  :حتي اليوم</td>
                                </tr>




                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span>Account Name:</span> </td>
                                    <td colspan="4" class="pl-0 pr-0 pt-0 pb-0" style="width:75%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"><span>{{ledger.transactionList[0].accountName}}</span> </td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;"><span>:أسم الحساب</span></td>

                                </tr>
                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span>Opening Balance:</span> </td>
                                    <td colspan="4" class="pl-0 pr-0 pt-0 pb-0" style="width:60%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"><span>{{ledger.openingBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.openingBalance) }}</span> </td>
                                    <td class="pr-3 pt-0 pb-0" style="width:26%;font-weight:bolder;font-size:14px !important;color:black !important;"><span>:الرصيد الافتتاحي</span></td>

                                </tr>





                            </table>
                        </div>

                    </div>
                </div>
                <div >
                    <div class="col-md-12 " style="background-color:white;border:2px solid #000000;">
                        <div  style="background-color:white;height:250mm;">

                            <table class="table col-md-12 ">
                                <tr class="heading" style="font-size:14px;">
                                    <th class="text-left" style="color:black !important;">#</th>
                                    <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.Date') }}</th>
                                    <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.TransactionType') }}</th>
                                    <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.DocumentNumber') }}</th>
                                    <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.Narration') }}</th>
                                    <th class="text-right" style="color:black !important;">{{ $t('LedgerPrintReport.Debit') }}</th>
                                    <th class="text-right" style="color:black !important;">{{ $t('LedgerPrintReport.Credit') }}</th>
                                    <th class="text-right" style="color:black !important;">{{ $t('LedgerPrintReport.Balance') }}</th>
                                </tr>
                                <tr style="font-size:13px; page-break-after: always;" v-for="(item, index) in ledger.transactionList" v-bind:key="item.id">
                                    <td class="text-left" style="color:black !important;">{{index+1}}</td>
                                    <td class="text-left" style="color:black !important;">{{item.date}}</td>
                                    <td class="text-left" style="color:black !important;">{{getTransactionType(item.transactionType)}}</td>
                                    <td class="text-left" style="color:black !important;">{{item.documentNumber}}</td>
                                    <td class="text-left" style="color:black !important;">{{getTransactionType(item.description)}}</td>
                                    <td class="text-right" style="color:black !important;">
                                        {{  nonNegative(item.debitAmount)}}
                                    </td>
                                    <td class="text-right" style="color:black !important;">
                                        {{nonNegative(item.creditAmount)}}
                                    </td>
                                    <td class="text-right" style="color:black !important;">
                                        {{item.openingBalance>0?'Dr':'Cr'}} {{nonNegative(item.openingBalance) }}
                                    </td>
                                </tr>

                                <tr style="font-size:13px;">
                                    <td class="text-right" colspan="5" style="color:black !important;">
                                        <h6>{{ $t('LedgerPrintReport.ClosingBalance') }}:</h6>
                                    </td>
                                    <td class="text-right" style="color:black !important;">
                                        <h6>{{  nonNegative(ledger.totalDebit)}}</h6>
                                    </td>
                                    <td class="text-right" style="color:black !important;">
                                        <h6>{{nonNegative(ledger.totalCredit)}}</h6>
                                    </td>
                                    <td class="text-right" style="color:black !important;">
                                        <h6> {{ledger.runningBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.runningBalance) }}</h6>
                                    </td>
                                </tr>
                            </table>

                        </div>
                        <div class="row pt-1" style=" background-color:white;font-size:16px">
                            <table class="table text-center">
                                <tr>
                                    <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                                        <span style=" border-top: 1px solid black;">
                                            Prepared By
                                        </span>
                                    </td>
                                    <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                                        <span style=" border-top: 1px solid black;">
                                            Approved By
                                        </span>

                                    </td>
                                    <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">

                                        <span style=" border-top: 1px solid black;">
                                            Received By
                                        </span>


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
            </div>
            <div v-else-if="itemTotal>22 && itemTotal<=41">
                <div style="background-color:white">
                    <div class="col-md-12" style="height:45mm;border:2px solid #000000;background-color:white" v-if="IsPaksitanClient">
                        <table class="table table-borderless">
                            <tr>
                                <td style="width:30%;" class="text-left pt-0 pb-0 pl-0 pr-0">
                                    <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px;  margin:0 !important;padding:5px">
                                </td>
                                <td style="width:40%;" class="text-center ">

                                    <u style="font-size:14px;color:black !important;font-weight:bold;">
                                        Sales Tax Invoice
                                    </u><br />
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">{{headerFooters.company.addressEnglish}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;float:left">NTN :&nbsp;&nbsp;&nbsp; {{headerFooters.company.vatRegistrationNo}}</span>&nbsp;
                                    <span style="font-size:14px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span><br />
                                    <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                        <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important">Account Ledger Report </span>
                                    </span>

                                </td>
                                <td style="width:30%;" class="text-left "> </td>

                            </tr>


                        </table>



                    </div>

                    <div class="col-md-12" v-else-if="isHeaderFooter=='true'">
                        <table class="table table-borderless">
                            <tr>
                                <td style="width:36%;" class="text-left pt-0 pb-0 pl-0 pr-0">
                                    <p class="mb-0">
                                        <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                        <span style="font-size:15px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryEnglish}}</span><br />
                                        <span style="font-size:14px;color:black !important;font-weight:bold;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                        <span style="font-size:14px;color:black !important;font-weight:bold;">Cr No.:{{headerFooters.company.companyRegNo}}</span><br />
                                        <span style="font-size:13px;color:black !important;font-weight:bold;">
                                            Tel: {{headerFooters.company.phoneNo}}
                                        </span>
                                    </p>
                                </td>
                                <td style="width:26%;" class="text-center pt-0 pb-0 pl-0 pr-0">
                                    <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                                </td>
                                <td style="width:38%;" class="pt-0 pb-0 pl-0 pr-0">
                                    <p class="text-right mb-0" v-if="arabic=='true'">
                                        <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameArabic}}.</span><br />
                                        <span style="font-size:15px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryArabic}}</span><br />
                                        <span style="font-size:14px;color:black !important;font-weight:bold;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                        <span style="font-size:14px;color:black !important;font-weight:bold;">رقم السجل التجاري :{{headerFooters.company.companyRegNo}}</span><br />
                                        <span style="font-size:13px;color:black !important;font-weight:bold;">
                                            هاتف: {{headerFooters.company.phoneNo}}:
                                        </span>
                                    </p>
                                </td>
                            </tr>

                            <tr>

                                <td style="width:100%;" class="pt-0 pb-0 pl-0 pr-0" colspan="3">
                                    <div style="text-align: center;">
                                        <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important">{{ $t('AccountLedger.AccountLedger') }}</span>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>            <!--INFORMATION-->
                    <div v-else style="height: 45mm;">

                    </div>
                    <!--INFORMATION-->
                    <div style="height:25mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2">
                                <table class="table table-borderless">
                                    <!--Row 1-->
                                    <tr>
                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">From Date:</td>
                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{fromDate}}</td>
                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">:من التاريخ</td>

                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">To Date:</td>
                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{toDate}}</td>
                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">  :حتي اليوم</td>
                                    </tr>




                                    <tr>
                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span>Account Name:</span> </td>
                                        <td colspan="4" class="pl-0 pr-0 pt-0 pb-0" style="width:75%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"><span>{{ledger.transactionList[0].accountName}}</span> </td>
                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;"><span>:أسم الحساب</span></td>

                                    </tr>
                                    <tr>
                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span>Opening Balance:</span> </td>
                                        <td colspan="4" class="pl-0 pr-0 pt-0 pb-0" style="width:60%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"><span>{{ledger.openingBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.openingBalance) }}</span> </td>
                                        <td class="pr-3 pt-0 pb-0" style="width:26%;font-weight:bolder;font-size:14px !important;color:black !important;"><span>:الرصيد الافتتاحي</span></td>

                                    </tr>





                                </table>
                            </div>

                        </div>
                    </div>
                    <div class="col-md-12 " style="background-color:white;border:2px solid #000000;">
                        <div class="row" style="background-color:white;height:285mm;">

                            <table class="table col-md-12 ">
                                <tr class="heading" style="font-size:14px;">
                                    <th class="text-left" style="color:black !important;">#44</th>
                                    <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.Date') }}</th>
                                    <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.TransactionType') }}</th>
                                    <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.DocumentNumber') }}</th>
                                    <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.Narration') }}</th>
                                    <th class="text-right" style="color:black !important;">{{ $t('LedgerPrintReport.Debit') }}</th>
                                    <th class="text-right" style="color:black !important;">{{ $t('LedgerPrintReport.Credit') }}</th>
                                    <th class="text-right" style="color:black !important;">{{ $t('LedgerPrintReport.Balance') }}</th>
                                </tr>
                                <tr style="font-size:13px; page-break-after: always;" v-for="(item, index) in listItemP1" v-bind:key="item.id">
                                    <td class="text-left" style="color:black !important;">{{index+1}}</td>
                                    <td class="text-left" style="color:black !important;">{{item.date}}</td>
                                    <td class="text-left" style="color:black !important;">{{getTransactionType(item.transactionType)}}</td>
                                    <td class="text-left" style="color:black !important;">{{item.documentNumber}}</td>
                                    <td class="text-left" style="color:black !important;">{{getTransactionType(item.description)}}</td>
                                    <td class="text-right" style="color:black !important;">
                                        {{  nonNegative(item.debitAmount)}}
                                    </td>
                                    <td class="text-right" style="color:black !important;">
                                        {{nonNegative(item.creditAmount)}}
                                    </td>
                                    <td class="text-right" style="color:black !important;">
                                        {{item.openingBalance>0?'Dr':'Cr'}} {{nonNegative(item.openingBalance) }}
                                    </td>
                                </tr>

                              
                            </table>

                        </div>

                    </div>
                </div>
                <div style="text-align:right;height:15mm">Page 1-2</div>


                <div class="col-md-12" style="height:45mm;border:2px solid #000000;background-color:white" v-if="IsPaksitanClient">
                    <table class="table table-borderless">
                        <tr>
                            <td style="width:30%;" class="text-left pt-0 pb-0 pl-0 pr-0">
                                <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px;  margin:0 !important;padding:5px">
                            </td>
                            <td style="width:40%;" class="text-center ">

                                <u style="font-size:14px;color:black !important;font-weight:bold;">
                                    Sales Tax Invoice
                                </u><br />
                                <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                <span style="font-size:14px;color:black !important;font-weight:bold;">{{headerFooters.company.addressEnglish}}</span><br />
                                <span style="font-size:14px;color:black !important;font-weight:bold;float:left">NTN :&nbsp;&nbsp;&nbsp; {{headerFooters.company.vatRegistrationNo}}</span>&nbsp;
                                <span style="font-size:14px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span><br />
                                <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important">Account Ledger Report</span>
                                </span>

                            </td>
                            <td style="width:30%;" class="text-left "> </td>

                        </tr>


                    </table>



                </div>

                <div class="col-md-12" v-else-if="isHeaderFooter=='true'">
                    <table class="table table-borderless">
                        <tr>
                            <td style="width:36%;" class="text-left pt-0 pb-0 pl-0 pr-0">
                                <p class="mb-0">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                    <span style="font-size:15px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryEnglish}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">Cr No.:{{headerFooters.company.companyRegNo}}</span><br />
                                    <span style="font-size:13px;color:black !important;font-weight:bold;">
                                        Tel: {{headerFooters.company.phoneNo}}
                                    </span>
                                </p>
                            </td>
                            <td style="width:26%;" class="text-center pt-0 pb-0 pl-0 pr-0">
                                <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                            </td>
                            <td style="width:38%;" class="pt-0 pb-0 pl-0 pr-0">
                                <p class="text-right mb-0" v-if="arabic=='true'">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameArabic}}.</span><br />
                                    <span style="font-size:15px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryArabic}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم السجل التجاري :{{headerFooters.company.companyRegNo}}</span><br />
                                    <span style="font-size:13px;color:black !important;font-weight:bold;">
                                        هاتف: {{headerFooters.company.phoneNo}}:
                                    </span>
                                </p>
                            </td>
                        </tr>

                        <tr>

                            <td style="width:100%;" class="pt-0 pb-0 pl-0 pr-0" colspan="3">
                                <div style="text-align: center;">
                                    <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important">{{ $t('AccountLedger.AccountLedger') }}</span>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>            <!--INFORMATION-->
                <div v-else style="height: 45mm;">

                </div>
                <!--INFORMATION-->
                <div style="height:25mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-12 ml-2 mr-2">
                            <table class="table table-borderless">
                                <!--Row 1-->
                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">From Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{fromDate}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">:من التاريخ</td>

                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">To Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{toDate}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">  :حتي اليوم</td>
                                </tr>




                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span>Account Name:</span> </td>
                                    <td colspan="4" class="pl-0 pr-0 pt-0 pb-0" style="width:75%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"><span>{{ledger.transactionList[0].accountName}}</span> </td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;"><span>:أسم الحساب</span></td>

                                </tr>
                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span>Opening Balance:</span> </td>
                                    <td colspan="4" class="pl-0 pr-0 pt-0 pb-0" style="width:60%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"><span>{{ledger.openingBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.openingBalance) }}</span> </td>
                                    <td class="pr-3 pt-0 pb-0" style="width:26%;font-weight:bolder;font-size:14px !important;color:black !important;"><span>:الرصيد الافتتاحي</span></td>

                                </tr>





                            </table>
                        </div>

                    </div>
                </div>
                <div class="col-md-12 " style="background-color:white;border:2px solid #000000;">
                    <div class="row" style="background-color:white;height:245mm;">

                        <table class="table col-md-12 ">
                            <tr class="heading" style="font-size:14px;">
                                <th class="text-left" style="color:black !important;">#</th>
                                <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.Date') }}</th>
                                <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.TransactionType') }}</th>
                                <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.DocumentNumber') }}</th>
                                <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.Narration') }}</th>
                                <th class="text-right" style="color:black !important;">{{ $t('LedgerPrintReport.Debit') }}</th>
                                <th class="text-right" style="color:black !important;">{{ $t('LedgerPrintReport.Credit') }}</th>
                                <th class="text-right" style="color:black !important;">{{ $t('LedgerPrintReport.Balance') }}</th>
                            </tr>
                            <tr style="font-size:13px; page-break-after: always;" v-for="(item, index) in listItemP2" v-bind:key="item.id">
                                <td class="text-left" style="color:black !important;">{{index+23}}</td>
                                <td class="text-left" style="color:black !important;">{{item.date}}</td>
                                <td class="text-left" style="color:black !important;">{{getTransactionType(item.transactionType)}}</td>
                                <td class="text-left" style="color:black !important;">{{item.documentNumber}}</td>
                                <td class="text-left" style="color:black !important;">{{getTransactionType(item.description)}}</td>
                                <td class="text-right" style="color:black !important;">
                                    {{  nonNegative(item.debitAmount)}}
                                </td>
                                <td class="text-right" style="color:black !important;">
                                    {{nonNegative(item.creditAmount)}}
                                </td>
                                <td class="text-right" style="color:black !important;">
                                    {{item.openingBalance>0?'Dr':'Cr'}} {{nonNegative(item.openingBalance) }}
                                </td>
                            </tr>

                            <tr style="font-size:13px;">
                                <td class="text-right" colspan="5" style="color:black !important;">
                                    <h6>{{ $t('LedgerPrintReport.ClosingBalance') }}:</h6>
                                </td>
                                <td class="text-right" style="color:black !important;">
                                    <h6>{{  nonNegative(ledger.totalDebit)}}</h6>
                                </td>
                                <td class="text-right" style="color:black !important;">
                                    <h6>{{nonNegative(ledger.totalCredit)}}</h6>
                                </td>
                                <td class="text-right" style="color:black !important;">
                                    <h6> {{ledger.runningBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.runningBalance) }}</h6>
                                </td>
                            </tr>
                        </table>

                    </div>
                    <div class="row pt-1" style=" background-color:white;font-size:16px">
                        <table class="table text-center">
                            <tr>
                                <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                                    <span style=" border-top: 1px solid black;">
                                        Prepared By
                                    </span>
                                </td>
                                <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">
                                    <span style=" border-top: 1px solid black;">
                                        Approved By
                                    </span>

                                </td>
                                <td style="width: 33%;text-decoration-line: overline;border:0px;color:black;font-weight:bold">

                                    <span style=" border-top: 1px solid black;">
                                        Received By
                                    </span>


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
                <div style="text-align:right;">Page 2-2</div>

            </div>

            <div v-else>
                <div class="col-md-12" style="height:45mm;border:2px solid #000000;background-color:white" v-if="IsPaksitanClient">
                    <table class="table table-borderless">
                        <tr>
                            <td style="width:30%;" class="text-left pt-0 pb-0 pl-0 pr-0">
                                <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px;  margin:0 !important;padding:5px">
                            </td>
                            <td style="width:40%;" class="text-center ">

                                <u style="font-size:14px;color:black !important;font-weight:bold;">
                                    Sales Tax Invoice
                                </u><br />
                                <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                <span style="font-size:14px;color:black !important;font-weight:bold;">{{headerFooters.company.addressEnglish}}</span><br />
                                <span style="font-size:14px;color:black !important;font-weight:bold;float:left">NTN :&nbsp;&nbsp;&nbsp; {{headerFooters.company.vatRegistrationNo}}</span>&nbsp;
                                <span style="font-size:14px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span><br />
                                <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important">Account Ledger Report4554</span>
                                </span>

                            </td>
                            <td style="width:30%;" class="text-left "> </td>

                        </tr>


                    </table>



                </div>

                <div class="col-md-12" v-else-if="isHeaderFooter=='true'">
                    <table class="table table-borderless">
                        <tr>
                            <td style="width:36%;" class="text-left pt-0 pb-0 pl-0 pr-0">
                                <p class="mb-0">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                    <span style="font-size:15px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryEnglish}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">Cr No.:{{headerFooters.company.companyRegNo}}</span><br />
                                    <span style="font-size:13px;color:black !important;font-weight:bold;">
                                        Tel: {{headerFooters.company.phoneNo}}
                                    </span>
                                </p>
                            </td>
                            <td style="width:26%;" class="text-center pt-0 pb-0 pl-0 pr-0">
                                <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                            </td>
                            <td style="width:38%;" class="pt-0 pb-0 pl-0 pr-0">
                                <p class="text-right mb-0" v-if="arabic=='true'">
                                    <span style="font-size:23px;color:black !important;font-weight:bold;">{{headerFooters.company.nameArabic}}.</span><br />
                                    <span style="font-size:15px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryArabic}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                    <span style="font-size:14px;color:black !important;font-weight:bold;">رقم السجل التجاري :{{headerFooters.company.companyRegNo}}</span><br />
                                    <span style="font-size:13px;color:black !important;font-weight:bold;">
                                        هاتف: {{headerFooters.company.phoneNo}}:
                                    </span>
                                </p>
                            </td>
                        </tr>

                        <tr>

                            <td style="width:100%;" class="pt-0 pb-0 pl-0 pr-0" colspan="3">
                                <div style="text-align: center;">
                                    <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important">{{ $t('AccountLedger.AccountLedger') }}</span>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>            <!--INFORMATION-->
                <div v-else style="height: 45mm;">

                </div>
                <!--INFORMATION-->
                <div style="height:25mm;margin-top:1mm; ">
                    <div class="row">
                        <div class="col-md-12 ml-2 mr-2">
                            <table class="table table-borderless">
                                <!--Row 1-->
                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">From Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{fromDate}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">:من التاريخ</td>

                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">To Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{toDate}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">  :حتي اليوم</td>
                                </tr>




                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span>Account Name:</span> </td>
                                    <td colspan="4" class="pl-0 pr-0 pt-0 pb-0" style="width:75%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"><span>{{ledger.transactionList[0].accountName}}</span> </td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;"><span>:أسم الحساب</span></td>

                                </tr>
                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span>Opening Balance:</span> </td>
                                    <td colspan="4" class="pl-0 pr-0 pt-0 pb-0" style="width:60%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"><span>{{ledger.openingBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.openingBalance) }}</span> </td>
                                    <td class="pr-3 pt-0 pb-0" style="width:26%;font-weight:bolder;font-size:14px !important;color:black !important;"><span>:الرصيد الافتتاحي</span></td>

                                </tr>





                            </table>
                        </div>

                    </div>
                </div>
                <div class="col-md-12 " style="background-color:white;border:2px solid #000000;">
                    <div class="row" style="background-color:white;height:290mm;">

                        <table class="table col-md-12 ">
                            <tr class="heading" style="font-size:14px;">
                                <th class="text-left" style="color:black !important;">#</th>
                                <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.Date') }}</th>
                                <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.TransactionType') }}</th>
                                <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.DocumentNumber') }}</th>
                                <th class="text-left" style="color:black !important;">{{ $t('LedgerPrintReport.Narration') }}</th>
                                <th class="text-right" style="color:black !important;">{{ $t('LedgerPrintReport.Debit') }}</th>
                                <th class="text-right" style="color:black !important;">{{ $t('LedgerPrintReport.Credit') }}</th>
                                <th class="text-right" style="color:black !important;">{{ $t('LedgerPrintReport.Balance') }}</th>
                            </tr>
                            <tr style="font-size:13px; page-break-after: always;" v-for="(item, index) in ledger.transactionList" v-bind:key="item.id">
                                <td class="text-left" style="color:black !important;">{{index+1}}</td>
                                <td class="text-left" style="color:black !important;">{{item.date}}</td>
                                <td class="text-left" style="color:black !important;">{{getTransactionType(item.transactionType)}}</td>
                                <td class="text-left" style="color:black !important;">{{item.documentNumber}}</td>
                                <td class="text-left" style="color:black !important;">{{getTransactionType(item.description)}}</td>
                                <td class="text-right" style="color:black !important;">
                                    {{  nonNegative(item.debitAmount)}}
                                </td>
                                <td class="text-right" style="color:black !important;">
                                    {{nonNegative(item.creditAmount)}}
                                </td>
                                <td class="text-right" style="color:black !important;">
                                    {{item.openingBalance>0?'Dr':'Cr'}} {{nonNegative(item.openingBalance) }}
                                </td>
                            </tr>

                            <tr style="font-size:13px;">
                                <td class="text-right" colspan="5" style="color:black !important;">
                                    <h6>{{ $t('LedgerPrintReport.ClosingBalance') }}:</h6>
                                </td>
                                <td class="text-right" style="color:black !important;">
                                    <h6>{{  nonNegative(ledger.totalDebit)}}</h6>
                                </td>
                                <td class="text-right" style="color:black !important;">
                                    <h6>{{nonNegative(ledger.totalCredit)}}</h6>
                                </td>
                                <td class="text-right" style="color:black !important;">
                                    <h6> {{ledger.runningBalance>0?'Dr':'Cr'}} {{nonNegative(ledger.runningBalance) }}</h6>
                                </td>
                            </tr>
                        </table>

                    </div>

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
                fDate: '',
                tDate: '',
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                Print: false,
                render: 0,
                arabic: '',
                english: '',

                itemTotal: 0,
                listItemP1: [],
                listItemP2: [],
                listItemP3: [],
                IsPaksitanClient: false,

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
                //else if (totalItem >= 22 && totalItem <=41) {
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

                var form = new FormData();
                form.append('htmlString', this.$refs.mychildcomponent.innerHTML);
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.post('/Report/PrintPdf', form, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        var date = moment().format('DD MMM YYYY');


                        link.setAttribute('download',root.ledger.transactionList[0].accountName==null?'':root.ledger.transactionList[0].accountName +' '+  'Account Ledger Report ' + date + '.pdf');                        document.body.appendChild(link);
                        link.click();

                        root.$emit('close');
                    });
            }
        }
    }
</script>
