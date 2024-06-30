<template>
    <div>
        <div ref="mychildcomponent" hidden id='purchaseInvoice' class="col-md-12">
            <!--page1-->
            <div v-if="itemTotal<=22">
                <!--HEADER-->
                <div class="col-md-12" style="height:45mm;border:1px solid #000000;background-color:white" v-if="IsPaksitanClient">
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
                                    <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                    <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                </span>

                            </td>
                            <td style="width:30%;" class="text-left "> </td>

                        </tr>


                    </table>



                </div>

                <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-else-if="isHeaderFooter=='true'">
                    <div class="row">
                        <div class="col-md-12">
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
                                            <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                            <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div style="height:45mm;" v-else></div>

                <div style="height:15mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-12 ml-2 mr-2">
                            <table class="table table-borderless">
                                <!--Row 1-->
                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">From Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{getDate(fromDate)}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">:من التاريخ</td>

                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">To Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{getDate(toDate)}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">:حتى الآن</td>
                                </tr>
                            </table>
                        </div>

                    </div>
                </div>

                <!--INFORMATION-->
                <div style="border:1px solid black;background-color:white">
                    <div class="col-12 ">
                        <div class="col-md-12" style="height: 270mm;background-color:white">
                            <table class="table">
                                <tr class="heading" style="font-size:15px !important;padding-top:5px;" v-if="$i18n.locale != 'en'">
                                    <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;"> Inv. No</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">تاريخ</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Sale'">اسم الزبون</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Purchase'">اسم المورد</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">القيمة الإجمالية</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">مقدار الخصم</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">مبلغ البيع الصافي</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">قيمة الضريبة</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">المبلغ الإجمالي</th>
                                </tr>
                                <tr class="heading" style="font-size:15px;padding-bottom:15px;" v-else>
                                    <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;"> Inv. No</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Date</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Sale'">Cust Name</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Purchase'">Supplier Name</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Gross Value</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Disount Amount</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Net Sale Amount</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT Amt</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total.Amt</th>
                                </tr>


                                <tr style="font-size:13px;font-weight:bold;" v-for="(item, index) in list" v-bind:key="item.id">
                                    <td class="text-center" style="padding-top:8px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.invoiceNo}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{getDate(item.date) }}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="formName=='Purchase'">  {{ ($i18n.locale == 'en' ||isLeftToRight()) ? (item.englishName != '' && item.englishName != null) ? item.englishName : item.arabicName : (item.arabicName != '' && item.arabicName != null) ? item.arabicName : item.englishName}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="formName=='Sale'">   {{ ($i18n.locale == 'en' ||isLeftToRight()) ? (item.customerName != '' && item.customerName != null) ? item.customerName : item.customerNameArabic==null || item.customerName=='' ?'Walk-In':item.customerNameArabic : (item.customerNameArabic != '' && item.customerNameArabic != null) ? item.customerNameArabic : item.customerName==null || item.customerName=='' ?'Walk-In':item.customerName}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">
                                        <span v-if="item.taxMethod=='Inclusive' || item.TaxMethod == 'شامل' ">
                                            {{(parseFloat(item.amount-item.vaTamount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </span>
                                        <span v-else>
                                            {{(parseFloat(item.amount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </span>
                                    </td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(item.discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">
                                        <span v-if="item.taxMethod=='Inclusive' || item.TaxMethod == 'شامل' ">
                                            {{(parseFloat(Math.abs(item.amountwithDiscount-item.vaTamount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </span>
                                        <span v-else>
                                            {{(parseFloat(Math.abs(item.amountwithDiscount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </span>
                                    </td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(item.vaTamount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(item.totalAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>

                                <tr style="font-size:15px;font-weight:bold;">
                                    <td colspan="4" class="text-center" style="padding-top:30px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;"> {{ $t('Total') }}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(grossAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(amountwithDiscount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(vaTamount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(totalAmount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-12 pl-2 pr-2" style=" background-color:white;font-size:16px">
                            <div class="col-12">
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
            </div>
            <div v-else-if="itemTotal>22 && itemTotal<=37">

                <div>
                    <!--HEADER-->
                    <div class="col-md-12" style="height:45mm;border:1px solid #000000;background-color:white" v-if="IsPaksitanClient">
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
                                        <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                        <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                    </span>

                                </td>
                                <td style="width:30%;" class="text-left "> </td>

                            </tr>


                        </table>



                    </div>

                    <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-else-if="isHeaderFooter=='true'">
                        <div class="row">
                            <div class="col-md-12">
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
                                                <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                                <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div style="height:45mm;" v-else></div>

                    <div style="height:15mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2">
                                <table class="table table-borderless">
                                    <!--Row 1-->
                                    <tr>
                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">From Date:</td>
                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{getDate(fromDate)}}</td>
                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">:من التاريخ</td>

                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">To Date:</td>
                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{getDate(toDate)}}</td>
                                        <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">:حتى الآن</td>
                                    </tr>
                                </table>
                            </div>

                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="border:1px solid black;background-color:white">
                        <div class="col-12 ">
                            <div class="col-md-12  " style="height: 290mm;background-color:white">
                                <table class="table">
                                    <tr class="heading" style="font-size:15px !important;padding-top:5px;" v-if="$i18n.locale != 'en'">
                                        <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;"> Inv. No</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">تاريخ</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Sale'">اسم الزبون</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Purchase'">اسم المورد</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">القيمة الإجمالية</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">مقدار الخصم</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">مبلغ البيع الصافي</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">قيمة الضريبة</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">المبلغ الإجمالي</th>
                                    </tr>
                                    <tr class="heading" style="font-size:15px;padding-bottom:15px;" v-else>
                                        <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;"> Inv. No</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Date</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Sale'">Cust Name</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Purchase'">Supplier Name</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Gross Value</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Disount Amount</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Net Sale Amount</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT Amt</th>
                                        <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total.Amt</th>
                                    </tr>


                                    <tr style="font-size:13px;font-weight:bold;" v-for="(item, index) in listItemP1" v-bind:key="item.id">
                                        <td class="text-center" style="padding-top:8px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.invoiceNo}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{getDate(item.date) }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="formName=='Purchase'">  {{ ($i18n.locale == 'en' ||isLeftToRight()) ? (item.englishName != '' && item.englishName != null) ? item.englishName : item.arabicName : (item.arabicName != '' && item.arabicName != null) ? item.arabicName : item.englishName}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="formName=='Sale'">   {{ ($i18n.locale == 'en' ||isLeftToRight()) ? (item.customerName != '' && item.customerName != null) ? item.customerName : item.customerNameArabic==null || item.customerName=='' ?'Walk-In':item.customerNameArabic : (item.customerNameArabic != '' && item.customerNameArabic != null) ? item.customerNameArabic : item.customerName==null || item.customerName=='' ?'Walk-In':item.customerName}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">
                                            <span v-if="item.taxMethod=='Inclusive' || item.TaxMethod == 'شامل' ">
                                                {{(parseFloat(item.amount-item.vaTamount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </span>
                                            <span v-else>
                                                {{(parseFloat(item.amount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </span>
                                        </td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(item.discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">
                                            <span v-if="item.taxMethod=='Inclusive' || item.TaxMethod == 'شامل' ">
                                                {{(parseFloat(Math.abs(item.amountwithDiscount-item.vaTamount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </span>
                                            <span v-else>
                                                {{(parseFloat(Math.abs(item.amountwithDiscount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </span>
                                        </td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(item.vaTamount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(item.totalAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>


                                </table>
                            </div>
                        </div>
                    </div>
                    <div style="text-align:right;height:17mm">Page 1-2</div>
                  
                </div>

                

                <!--HEADER-->
                <div class="col-md-12" style="height:45mm;border:1px solid #000000;background-color:white" v-if="IsPaksitanClient">
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
                                    <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                    <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                </span>

                            </td>
                            <td style="width:30%;" class="text-left "> </td>

                        </tr>


                    </table>



                </div>

                <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-else-if="isHeaderFooter=='true'">
                    <div class="row">
                        <div class="col-md-12">
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
                                            <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                            <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div style="height:45mm;" v-else></div>

                <div style="height:15mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-12 ml-2 mr-2">
                            <table class="table table-borderless">
                                <!--Row 1-->
                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">From Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{getDate(fromDate)}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">:من التاريخ</td>

                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">To Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{getDate(toDate)}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">:حتى الآن</td>
                                </tr>
                            </table>
                        </div>

                    </div>
                </div>

                <!--INFORMATION-->
                <div style="border:1px solid black;background-color:white">
                    <div class="col-12 ">
                        <div class="col-md-12" style="height: 255mm;background-color:white">
                            <table class="table">
                                <tr class="heading" style="font-size:15px !important;padding-top:5px;" v-if="$i18n.locale != 'en'">
                                    <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;"> Inv. No</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">تاريخ</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Sale'">اسم الزبون</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Purchase'">اسم المورد</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">القيمة الإجمالية</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">مقدار الخصم</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">مبلغ البيع الصافي</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">قيمة الضريبة</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">المبلغ الإجمالي</th>
                                </tr>
                                <tr class="heading" style="font-size:15px;padding-bottom:15px;" v-else>
                                    <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;"> Inv. No</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Date</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Sale'">Cust Name</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Purchase'">Supplier Name</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Gross Value</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Disount Amount</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Net Sale Amount</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT Amt</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total.Amt</th>
                                </tr>


                                <tr style="font-size:13px;font-weight:bold;" v-for="(item, index) in listItemP2" v-bind:key="item.id">
                                    <td class="text-center" style="padding-top:8px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+22}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.invoiceNo}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{getDate(item.date) }}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="formName=='Purchase'">  {{ ($i18n.locale == 'en' ||isLeftToRight()) ? (item.englishName != '' && item.englishName != null) ? item.englishName : item.arabicName : (item.arabicName != '' && item.arabicName != null) ? item.arabicName : item.englishName}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="formName=='Sale'">   {{ ($i18n.locale == 'en' ||isLeftToRight()) ? (item.customerName != '' && item.customerName != null) ? item.customerName : item.customerNameArabic==null || item.customerName=='' ?'Walk-In':item.customerNameArabic : (item.customerNameArabic != '' && item.customerNameArabic != null) ? item.customerNameArabic : item.customerName==null || item.customerName=='' ?'Walk-In':item.customerName}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">
                                        <span v-if="item.taxMethod=='Inclusive' || item.TaxMethod == 'شامل' ">
                                            {{(parseFloat(item.amount-item.vaTamount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </span>
                                        <span v-else>
                                            {{(parseFloat(item.amount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </span>
                                    </td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(item.discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">
                                        <span v-if="item.taxMethod=='Inclusive' || item.TaxMethod == 'شامل' ">
                                            {{(parseFloat(Math.abs(item.amountwithDiscount-item.vaTamount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </span>
                                        <span v-else>
                                            {{(parseFloat(Math.abs(item.amountwithDiscount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </span>
                                    </td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(item.vaTamount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(item.totalAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>

                                <tr style="font-size:15px;font-weight:bold;">
                                    <td colspan="4" class="text-center" style="padding-top:30px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;"> {{ $t('Total') }}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(grossAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(amountwithDiscount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(vaTamount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(totalAmount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-12 pl-2 pr-2" style=" background-color:white;font-size:16px">
                            <div class="col-12">
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
                <div style="text-align:right;height:13mm">Page 2-2</div>

            </div>

            <div v-else>
                <!--HEADER-->
                <div class="col-md-12" style="height:45mm;border:1px solid #000000;background-color:white" v-if="IsPaksitanClient">
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
                                    <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                    <span style="font-size:23px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                </span>

                            </td>
                            <td style="width:30%;" class="text-left "> </td>

                        </tr>


                    </table>



                </div>

                <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-else-if="isHeaderFooter=='true'">
                    <div class="row">
                        <div class="col-md-12">
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
                                            <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                            <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div style="height:45mm;" v-else></div>

                <div style="height:15mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-12 ml-2 mr-2">
                            <table class="table table-borderless">
                                <!--Row 1-->
                                <tr>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">From Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{getDate(fromDate)}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">:من التاريخ</td>

                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">To Date:</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{getDate(toDate)}}</td>
                                    <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;" v-if="arabic=='true'">:حتى الآن</td>
                                </tr>
                            </table>
                        </div>

                    </div>
                </div>

                <!--INFORMATION-->
                <div style="background-color:white">
                    <div class="col-12 ">
                        <div class="col-md-12  pt-4 " style="height: 300mm;background-color:white">
                            <table class="table">
                                <tr class="heading" style="font-size:15px !important;padding-top:5px;" v-if="$i18n.locale != 'en'">
                                    <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;"> Inv. No</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">تاريخ</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Sale'">اسم الزبون</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Purchase'">اسم المورد</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">القيمة الإجمالية</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">مقدار الخصم</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">مبلغ البيع الصافي</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">قيمة الضريبة</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">المبلغ الإجمالي</th>
                                </tr>
                                <tr class="heading" style="font-size:15px;padding-bottom:15px;" v-else>
                                    <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;"> Inv. No</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Date</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Sale'">Cust Name</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Purchase'">Supplier Name</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Gross Value</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Disount Amount</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Net Sale Amount</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT Amt</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total.Amt</th>
                                </tr>


                                <tr style="font-size:13px;font-weight:bold;" v-for="(item, index) in list" v-bind:key="item.id">
                                    <td class="text-center" style="padding-top:8px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.invoiceNo}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{getDate(item.date) }}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="formName=='Purchase'">  {{ ($i18n.locale == 'en' ||isLeftToRight()) ? (item.englishName != '' && item.englishName != null) ? item.englishName : item.arabicName : (item.arabicName != '' && item.arabicName != null) ? item.arabicName : item.englishName}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="formName=='Sale'">   {{ ($i18n.locale == 'en' ||isLeftToRight()) ? (item.customerName != '' && item.customerName != null) ? item.customerName : item.customerNameArabic==null || item.customerName=='' ?'Walk-In':item.customerNameArabic : (item.customerNameArabic != '' && item.customerNameArabic != null) ? item.customerNameArabic : item.customerName==null || item.customerName=='' ?'Walk-In':item.customerName}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">
                                        <span v-if="item.taxMethod=='Inclusive' || item.TaxMethod == 'شامل' ">
                                            {{(parseFloat(item.amount-item.vaTamount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </span>
                                        <span v-else>
                                            {{(parseFloat(item.amount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </span>
                                    </td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(item.discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">
                                        <span v-if="item.taxMethod=='Inclusive' || item.TaxMethod == 'شامل' ">
                                            {{(parseFloat(Math.abs(item.amountwithDiscount-item.vaTamount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </span>
                                        <span v-else>
                                            {{(parseFloat(Math.abs(item.amountwithDiscount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </span>
                                    </td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(item.vaTamount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(item.totalAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>

                                <tr style="font-size:15px;font-weight:bold;">
                                    <td colspan="4" class="text-center" style="padding-top:30px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;"> {{ $t('Total') }}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(grossAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(amountwithDiscount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(vaTamount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 30px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(totalAmount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>

<script>
    import moment from "moment";
    export default {
        props: ['printDetails', 'headerFooter', 'formName', 'fromDate', 'toDate','invoice'],
       
        data: function () {
            return {
                itemTotal: 0,
                listItemP1: [],
                listItemP2: [],
                listItemP3: [],
                IsPaksitanClient: false,

                isHeaderFooter: '',
                invoicePrint: '',
             
                list: [],
                render: 0,
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                arabic: '',
                english: '',
            }
        },
        filters: {
            toWords: function (value) {
                var converter = require('number-to-words');
                if (!value) return ''
                return converter.toWords(value);
            }
        },
        computed: {
            grossAmount: function () {
                if (this.invoice == 'Both' || this.invoice == 'كلاهما') {
                    return this.list.reduce(function (a, c) {
                        if (c.saleInvoiceId != null || c.purchaseInvoiceId != null) {
                            if (c.taxMethod == 'Inclusive' || c.TaxMethod == 'شامل') {
                                return a - Number(((c.amount - c.vaTamount).toFixed(3).slice(0, -1)) || 0)
                            }
                            else {
                                return a - Number((c.amount.toFixed(3).slice(0, -1)) || 0)
                            }

                        }
                        else {
                            if (c.taxMethod == 'Inclusive' || c.TaxMethod == 'شامل') {
                                return a + Number(((c.amount - c.vaTamount).toFixed(3).slice(0, -1)) || 0)
                            }
                            else {
                                return a + Number((c.amount.toFixed(3).slice(0, -1)) || 0)
                            }
                        }
                    }, 0)
                }
                else {
                    return this.list.reduce(function (a, c) {
                        if (c.taxMethod == 'Inclusive' || c.TaxMethod == 'شامل') {
                            return a + Number(((c.amount - c.vaTamount).toFixed(3).slice(0, -1)) || 0)
                        }
                        else {
                            return a + Number((c.amount.toFixed(3).slice(0, -1)) || 0)
                        }

                    }, 0)
                }
            },
            discount: function () {
                if (this.invoice == 'Both' || this.invoice == 'كلاهما') {
                    return this.list.reduce(function (a, c) {
                        if (c.saleInvoiceId != null || c.purchaseInvoiceId != null) {
                            return a - Number((c.discount.toFixed(3).slice(0, -1) < 0 ? c.discount.toFixed(3).slice(0, -1) * -1 : c.discount.toFixed(3).slice(0, -1)) || 0)
                        }
                        else {
                            return a + Number((c.discount.toFixed(3).slice(0, -1) < 0 ? c.discount.toFixed(3).slice(0, -1) * -1 : c.discount.toFixed(3).slice(0, -1)) || 0)
                        }
                    }, 0)
                }
                else {
                    return this.list.reduce(function (a, c) {

                        return a + Number((c.discount.toFixed(3).slice(0, -1) < 0 ? c.discount.toFixed(3).slice(0, -1) * -1 : c.discount.toFixed(3).slice(0, -1)) || 0)
                    }, 0)
                }
            },
            amountwithDiscount: function () {
                if (this.invoice == 'Both' || this.invoice == 'كلاهما') {
                    return this.list.reduce(function (a, c) {
                        if (c.saleInvoiceId != null || c.purchaseInvoiceId != null) {
                            if (c.taxMethod == 'Inclusive' || c.TaxMethod == 'شامل') {
                                return a - Number(((c.amountwithDiscount - c.vaTamount).toFixed(3).slice(0, -1)) || 0)
                            }
                            else {
                                return a - Number((c.amountwithDiscount.toFixed(3).slice(0, -1)) || 0)
                            }
                        }
                        else {
                            if (c.taxMethod == 'Inclusive' || c.TaxMethod == 'شامل') {
                                return a + Number(((c.amountwithDiscount - c.vaTamount).toFixed(3).slice(0, -1)) || 0)
                            }
                            else {
                                return a + Number((c.amountwithDiscount.toFixed(3).slice(0, -1) < 0 ? c.amountwithDiscount.toFixed(3).slice(0, -1) * -1 : c.amountwithDiscount.toFixed(3).slice(0, -1)) || 0)
                            }

                        }
                    }, 0)
                }
                else {
                    return this.list.reduce(function (a, c) {

                        if (c.taxMethod == 'Inclusive' || c.TaxMethod == 'شامل') {
                            return a + Number(((c.amountwithDiscount - c.vaTamount).toFixed(3).slice(0, -1)) || 0)
                        }
                        else {
                            return a + Number((c.amountwithDiscount.toFixed(3).slice(0, -1) < 0 ? c.amountwithDiscount.toFixed(3).slice(0, -1) * -1 : c.amountwithDiscount.toFixed(3).slice(0, -1)) || 0)
                        }
                    }, 0)
                }

            },
            vaTamount: function () {
                if (this.invoice == 'Both' || this.invoice == 'كلاهما') {
                    return this.list.reduce(function (a, c) {
                        
                        if (c.saleInvoiceId != null || c.purchaseInvoiceId != null) {
                            return a - Number((c.vaTamount.toFixed(3).slice(0, -1) < 0 ? c.vaTamount.toFixed(3).slice(0, -1) * -1 : c.vaTamount.toFixed(3).slice(0, -1)) || 0)
                        }
                        else {
                            return a + Number((c.vaTamount.toFixed(3).slice(0, -1) < 0 ? c.vaTamount.toFixed(3).slice(0, -1) * -1 : c.vaTamount.toFixed(3).slice(0, -1)) || 0)

                        }

                    }, 0)
                }
                else {
                    return this.list.reduce(function (a, c) {
                        


                        return a + Number((c.vaTamount.toFixed(3).slice(0, -1) < 0 ? c.vaTamount.toFixed(3).slice(0, -1) * -1 : c.vaTamount.toFixed(3).slice(0, -1)) || 0)


                    }, 0)
                }
            },

            totalAmount: function () {
                if (this.invoice == 'Both' || this.invoice == 'كلاهما') {
                    return this.list.reduce(function (a, c) {
                        if (c.saleInvoiceId != null || c.purchaseInvoiceId != null) {
                            return a - Number((c.totalAmount.toFixed(3).slice(0, -1) < 0 ? c.totalAmount.toFixed(3).slice(0, -1) * -1 : c.totalAmount.toFixed(3).slice(0, -1)) || 0)
                        }
                        else {
                            return a + Number((c.totalAmount.toFixed(3).slice(0, -1) < 0 ? c.totalAmount.toFixed(3).slice(0, -1) * -1 : c.totalAmount.toFixed(3).slice(0, -1)) || 0)

                        }
                    }, 0)
                }
                else {
                    return this.list.reduce(function (a, c) {


                        return a + Number((c.totalAmount.toFixed(3).slice(0, -1) < 0 ? c.totalAmount.toFixed(3).slice(0, -1) * -1 : c.totalAmount.toFixed(3).slice(0, -1)) || 0)


                    }, 0)
                }
            },

          
            

        },
     
        methods: {
            getDate: function (date) {
                return moment(date).format('l');
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
                        link.setAttribute('download', 'Invoice Report ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();

                        root.$emit('close');
                    });
            },



        },
        mounted: function () {
            
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.invoicePrint = localStorage.getItem('InvoicePrint');
            this.IsPaksitanClient = localStorage.getItem('IsPaksitanClient') == "true" ? true : false;

            var root = this;
            if (this.printDetails.length > 0) {
                this.list = this.printDetails;
                this.headerFooters = this.headerFooter;
                
                var totalItem = this.printDetails.length;
                this.itemTotal = totalItem;
                if (totalItem < 22) {
                    for (var i = 0; i < totalItem; i++) {
                        root.listItemP1.push(root.printDetails[i]);
                    }
                }
                else if (totalItem >= 22 && totalItem < 37) {
                    for (var k = 0; k < totalItem; k++) {
                        if (k < 22) {
                            root.listItemP1.push(root.printDetails[k]);
                        }
                        else {
                            root.listItemP2.push(root.printDetails[k]);
                        }
                    }
                }

                setTimeout(function () {
                    root.printInvoice();
                }, 125)
            }
        },

    }
</script>

