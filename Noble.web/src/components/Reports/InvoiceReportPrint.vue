<template>
    <div>
        <div hidden id='purchaseInvoice' class="col-md-12" style="background-color:white">
            <!--page1-->
            <div style="background-color:white" v-if="itemTotal<=20">
                <!--HEADER-->
                <div class="col-md-12" style="height:45mm;border:1px solid #000000;background-color:white" v-if="IsPaksitanClient">
                    <div class="row" style="height:35mm">
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
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>



                </div>

                <div class="border border-dark col-md-12 mb-1 " style="height:40mm;" v-else-if="isHeaderFooter=='true'">
                    <div class="row">
                        <div class="col-md-4 text-center">
                            <table class="m-auto">
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
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; height:auto; max-height:120px; padding:5px !important; margin:0 !important">
                            <p style="text-align: center; margin: 0px; padding: 0px; font-size: 10px; line-height: 1; font-family: 'Times New Roman', Times;">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                            </p>
                        </div>
                        <div class="col-md-4 " style="height:40mm;">
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
                </div>
                <div style="height:45mm;" v-else></div>

                <div class="col-12 border border-dark" style="height:15mm;margin-top:1mm;">
                    <div class="row pt-3" style="margin-top:1mm;background-color:white">
                        <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{getDate(fromDate)}}</div>
                                    <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;"><span v-if="arabic=='true'">:تاريخ</span></div>
                                </div>
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">To Date.:</div>
                                    <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{getDate(toDate)}}</div>
                                    <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                        <span v-if="arabic=='true'"> : تاريخ الاستحقاق</span>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div style="border:1px solid black;background-color:white">
                    <div class="col-12">
                        <div class=" pt-4 " style="height: 300mm;background-color:white">
                            <table class="table">
                                <tr class="heading" style="font-size:18px !important;padding-top:5px;border-bottom:1px solid;" v-if="$i18n.locale != 'en'">
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
                                <tr class="heading" style="font-size:18px;border-bottom:1px solid;padding-bottom:15px" v-else>
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


                                <tr style="font-size:17px;font-weight:bold;" v-for="(item, index) in list" v-bind:key="item.id">
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
                                    <td colspan="4" class="text-center" style="padding-top:60px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;"> {{ $t('Total') }}</td>

                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(grossAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(amountwithDiscount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(vaTamount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(totalAmount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>


                            </table>
                        </div>
                    </div>
                    <div class="col-12 pl-2 pr-2" style=" background-color:white;font-size:16px">
                        <div class="col-12">
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
                <!--<div style="height:41mm;margin-top:1mm;">
        <div class="row">
            <div class="col-md-12">
                <table class="table table-bordered tbl_padding">


                    <tr>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold;text-align:right;color:black !important;">Total Disc %<span style="font-weight:bold;color:black !important;">/ إجمالي القرص</span></td>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{(parseFloat(discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                    </tr>
                    <tr>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold;text-align:right;color:black !important;">Total Disc Amount<span style="font-weight:bold;color:black !important;">/ إجمالي مبلغ القرص</span></td>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{(parseFloat(amountwithDiscount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                    </tr>
                    <tr>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold;text-align:right;color:black !important;">Total VAT Amount<span style="font-weight:bold;color:black !important;">/  إجمالي مبلغ ضريبة القيمة المضافة</span></td>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{(parseFloat(vaTamount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                    </tr>
                    <tr>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold;text-align:right;color:black !important;">Grand Total<span style="font-weight:bold;color:black !important;">/  المجموع الإجمالي</span></td>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{(parseFloat(totalAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                    </tr>
                </table>
            </div>
        </div>
    </div>-->
                <!--Footer-->
            </div>

            <div style="background-color:white" v-else-if="itemTotal>20 && itemTotal<40">
                <!--HEADER-->
                <div class="col-md-12" style="height:45mm;border:1px solid #000000;background-color:white" v-if="IsPaksitanClient">
                    <div class="row" style="height:35mm">
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
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>



                </div>

                <div class="border border-dark col-md-12 mb-1 " style="height:40mm;" v-else-if="isHeaderFooter=='true'">
                    <div class="row">
                        <div class="col-md-4 text-center">
                            <table class="m-auto">
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
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; height:auto; max-height:120px; padding:5px !important; margin:0 !important">
                            <p style="text-align: center; margin: 0px; padding: 0px; font-size: 10px; line-height: 1; font-family: 'Times New Roman', Times;">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                            </p>
                        </div>
                        <div class="col-md-4 " style="height:40mm;">
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
                </div>
                <div style="height:45mm;" v-else></div>

                <div class="col-12 border border-dark" style="height:15mm;margin-top:1mm;">
                    <div class="row pt-3" style="margin-top:1mm;background-color:white">
                        <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{getDate(fromDate)}}</div>
                                    <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;"><span v-if="arabic=='true'">:تاريخ</span></div>
                                </div>
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">To Date.:</div>
                                    <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{getDate(toDate)}}</div>
                                    <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                        <span v-if="arabic=='true'"> : تاريخ الاستحقاق</span>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div style="border:1px solid black;background-color:white">
                    <div class="col-12">
                        <div class=" pt-4 " style="height: 315mm;background-color:white">
                            <table class="table">
                                <tr class="heading" style="font-size:18px !important;padding-top:5px;border-bottom:1px solid;" v-if="$i18n.locale != 'en'">
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
                                <tr class="heading" style="font-size:18px;border-bottom:1px solid;padding-bottom:15px" v-else>
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


                                <tr style="font-size:17px;font-weight:bold;" v-for="(item, index) in listItemP1" v-bind:key="item.id">
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
                <div style="text-align:right">Page 1-3</div>

                <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>
                <div style="background-color:white">
                    <!--HEADER-->
                    <div class="col-md-12" style="height:45mm;border:1px solid #000000;background-color:white" v-if="IsPaksitanClient">
                        <div class="row" style="height:35mm">
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
                                                <span style="font-size:18px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span><br />
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                        </div>



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
                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">Sale Invoice Report</span>
                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">Purchase Invoice Report</span>
                                </p>
                            </div>
                            <div class="col-md-12" style="margin-bottom:10px !important;height:10mm" v-else>
                                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div style="height:45mm;" v-else></div>

                    <div class="col-12" style=" border:2px solid #000000;background-color:white">
                        <div class="row pt-3" style="margin-top:1mm;background-color:white">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div class="row">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{getDate(fromDate)}}</div>
                                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;"><span v-if="arabic=='true'">:تاريخ</span></div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">To Date.:</div>
                                        <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{getDate(toDate)}}</div>
                                        <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                            <span v-if="arabic=='true'"> : تاريخ الاستحقاق</span>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="border:1px solid black;background-color:white">
                        <div class="col-12">
                            <div class=" pt-4 " style="height: 300mm;background-color:white">
                                <table class="table">
                                    <tr class="heading" style="font-size:18px !important;padding-top:5px;border-bottom:1px solid;" v-if="$i18n.locale != 'en'">
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
                                    <tr class="heading" style="font-size:18px;border-bottom:1px solid;padding-bottom:15px" v-else>
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


                                    <tr style="font-size:17px;font-weight:bold;" v-for="(item, index) in listItemP2" v-bind:key="item.id">
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
                                        <td colspan="4" class="text-center" style="padding-top:60px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;"> {{ $t('Total') }}</td>

                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(grossAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(amountwithDiscount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(vaTamount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(totalAmount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>


                                </table>
                            </div>
                        </div>
                        <div class="col-12 pl-2 pr-2" style=" background-color:white;font-size:16px">
                            <div class="col-12">
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
                    <div style="text-align:right">Page 2-2</div>


                    <!--Footer-->
                </div>


            </div>

            <div style="background-color:white" v-else-if="itemTotal>=40 && itemTotal<=58">
                <!--HEADER-->
                <div class="col-md-12" style="height:45mm;border:1px solid #000000;background-color:white" v-if="IsPaksitanClient">
                    <div class="row" style="height:35mm">
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
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>



                </div>

                <div class="border border-dark col-md-12 mb-1 " style="height:40mm;" v-else-if="isHeaderFooter=='true'">
                    <div class="row">
                        <div class="col-md-4 text-center">
                            <table class="m-auto">
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
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; height:auto; max-height:120px; padding:5px !important; margin:0 !important">
                            <p style="text-align: center; margin: 0px; padding: 0px; font-size: 10px; line-height: 1; font-family: 'Times New Roman', Times;">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                            </p>
                        </div>
                        <div class="col-md-4 " style="height:40mm;">
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
                </div>
                <div style="height:45mm;" v-else></div>

                <div class="col-12 border border-dark" style="height:15mm;margin-top:1mm;">
                    <div class="row pt-3" style="margin-top:1mm;background-color:white">
                        <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{getDate(fromDate)}}</div>
                                    <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;"><span v-if="arabic=='true'">:تاريخ</span></div>
                                </div>
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">To Date.:</div>
                                    <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{getDate(toDate)}}</div>
                                    <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                        <span v-if="arabic=='true'"> : تاريخ الاستحقاق</span>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div style="border:1px solid black;background-color:white">
                    <div class="col-12">
                        <div class=" pt-4 " style="height: 315mm;background-color:white">
                            <table class="table">
                                <tr class="heading" style="font-size:18px !important;padding-top:5px;border-bottom:1px solid;" v-if="$i18n.locale != 'en'">
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
                                <tr class="heading" style="font-size:18px;border-bottom:1px solid;padding-bottom:15px" v-else>
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


                                <tr style="font-size:17px;font-weight:bold;" v-for="(item, index) in listItemP1" v-bind:key="item.id">
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
                <div style="text-align:right">Page 1-3</div>

                <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>



                <div class="col-md-12" style="height:45mm;border:1px solid #000000;background-color:white" v-if="IsPaksitanClient">
                    <div class="row" style="height:35mm">
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
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>



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
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">Sale Invoice Report</span>
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">Purchase Invoice Report</span>
                            </p>
                        </div>
                        <div class="col-md-12" style="margin-bottom:10px !important;height:10mm" v-else>
                            <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                            </p>
                        </div>
                    </div>
                </div>
                <div style="height:45mm;" v-else></div>

                <div class="col-12" style=" border:2px solid #000000;background-color:white">
                    <div class="row pt-3" style="margin-top:1mm;background-color:white">
                        <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{getDate(fromDate)}}</div>
                                    <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;"><span v-if="arabic=='true'">:تاريخ</span></div>
                                </div>
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">To Date.:</div>
                                    <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{getDate(toDate)}}</div>
                                    <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                        <span v-if="arabic=='true'"> : تاريخ الاستحقاق</span>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div style="border:1px solid black;background-color:white">
                    <div class="col-12">
                        <div class=" pt-4 " style="height: 315mm;background-color:white">
                            <table class="table">
                                <tr class="heading" style="font-size:18px !important;padding-top:5px;border-bottom:1px solid;" v-if="$i18n.locale != 'en'">
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
                                <tr class="heading" style="font-size:18px;border-bottom:1px solid;padding-bottom:15px" v-else>
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


                                <tr style="font-size:17px;font-weight:bold;" v-for="(item, index) in listItemP2" v-bind:key="item.id">
                                    <td class="text-center" style="padding-top:8px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+21}}</td>
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
                <div style="text-align:right">Page 2-3</div>

                <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>




                <div style="background-color:white">
                    <!--HEADER-->
                    <div class="col-md-12" style="height:45mm;border:1px solid #000000;background-color:white" v-if="IsPaksitanClient">
                        <div class="row" style="height:35mm">
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
                                                <span style="font-size:18px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span><br />
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                                </span>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                        </div>



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
                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">Sale Invoice Report</span>
                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">Purchase Invoice Report</span>
                                </p>
                            </div>
                            <div class="col-md-12" style="margin-bottom:10px !important;height:10mm" v-else>
                                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div style="height:45mm;" v-else></div>

                    <div class="col-12" style=" border:2px solid #000000;background-color:white">
                        <div class="row pt-3" style="margin-top:1mm;background-color:white">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div class="row">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{getDate(fromDate)}}</div>
                                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;"><span v-if="arabic=='true'">:تاريخ</span></div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">To Date.:</div>
                                        <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{getDate(toDate)}}</div>
                                        <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                            <span v-if="arabic=='true'"> : تاريخ الاستحقاق</span>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="border:1px solid black;background-color:white">
                        <div class="col-12">
                            <div class=" pt-4 " style="height: 300mm;background-color:white">
                                <table class="table">
                                    <tr class="heading" style="font-size:18px !important;padding-top:5px;border-bottom:1px solid;" v-if="$i18n.locale != 'en'">
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
                                    <tr class="heading" style="font-size:18px;border-bottom:1px solid;padding-bottom:15px" v-else>
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


                                    <tr style="font-size:17px;font-weight:bold;" v-for="(item, index) in listItemP3" v-bind:key="item.id">
                                        <td class="text-center" style="padding-top:8px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+40}}</td>
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
                                        <td colspan="4" class="text-center" style="padding-top:60px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;"> {{ $t('Total') }}</td>

                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(grossAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(amountwithDiscount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(vaTamount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(totalAmount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>


                                </table>
                            </div>
                        </div>
                        <div class="col-12 pl-2 pr-2" style=" background-color:white;font-size:16px">
                            <div class="col-12">
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
                    <div style="text-align:right">Page 3-3</div>


                    <!--Footer-->
                </div>


            </div>



            <div style="background-color:white" v-else>
                <!--HEADER-->

                <div class="col-md-12" style="height:45mm;border:1px solid #000000;background-color:white" v-if="IsPaksitanClient">
                    <div class="row" style="height:35mm">
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
                                            <span style="font-size:18px;color:black !important;font-weight:bold;float:right">STR:&nbsp;&nbsp;&nbsp;   {{headerFooters.company.companyRegNo}}</span><br />
                                            <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{$t('InvoicePrintReport.SaleInvoiceReport')}}</span>
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{$t('InvoicePrintReport.PurchaseInvoiceReport')}}</span>
                                            </span>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>



                </div>

                <div class="border border-dark col-md-12 mb-1 " style="height:40mm;" v-else-if="isHeaderFooter=='true'">
                    <div class="row">
                        <div class="col-md-4 text-center">
                            <table class="m-auto">
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
                            <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; height:auto; max-height:120px; padding:5px !important; margin:0 !important">
                            <p style="text-align: center; margin: 0px; padding: 0px; font-size: 10px; line-height: 1; font-family: 'Times New Roman', Times;">
                                <span style="font-size:14px;">{{ $t('StockReportPrint.StockReport') }}</span>
                            </p>
                        </div>
                        <div class="col-md-4 " style="height:40mm;">
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
                </div>
                <div style="height:45mm;" v-else></div>

                <div class="col-12 border border-dark" style="height:15mm;margin-top:1mm;">
                    <div class="row pt-3" style="margin-top:1mm;background-color:white">
                        <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{getDate(fromDate)}}</div>
                                    <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;"><span v-if="arabic=='true'">:تاريخ</span></div>
                                </div>
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">To Date.:</div>
                                    <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{getDate(toDate)}}</div>
                                    <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                        <span v-if="arabic=='true'"> : تاريخ الاستحقاق</span>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div style="background-color:white">
                    <div class="col-12">
                        <div class=" pt-4 " style="height: 300mm;background-color:white">
                            <table class="table">
                                <tr class="heading" style="font-size:18px !important;padding-top:5px;border-bottom:1px solid;" v-if="$i18n.locale != 'en'">
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
                                <tr class="heading" style="font-size:18px;border-bottom:1px solid;padding-bottom:15px" v-else>
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


                                <tr style="font-size:17px;font-weight:bold;" v-for="(item, index) in list" v-bind:key="item.id">
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
                                    <td colspan="4" class="text-center" style="padding-top:60px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;"> {{ $t('Total') }}</td>

                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(grossAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(amountwithDiscount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(vaTamount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(totalAmount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>


                            </table>
                        </div>
                    </div>
                </div>
                <!--<div style="height:41mm;margin-top:1mm;">
        <div class="row">
            <div class="col-md-12">
                <table class="table table-bordered tbl_padding">


                    <tr>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold;text-align:right;color:black !important;">Total Disc %<span style="font-weight:bold;color:black !important;">/ إجمالي القرص</span></td>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{(parseFloat(discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                    </tr>
                    <tr>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold;text-align:right;color:black !important;">Total Disc Amount<span style="font-weight:bold;color:black !important;">/ إجمالي مبلغ القرص</span></td>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{(parseFloat(amountwithDiscount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                    </tr>
                    <tr>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold;text-align:right;color:black !important;">Total VAT Amount<span style="font-weight:bold;color:black !important;">/  إجمالي مبلغ ضريبة القيمة المضافة</span></td>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{(parseFloat(vaTamount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                    </tr>
                    <tr>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold;text-align:right;color:black !important;">Grand Total<span style="font-weight:bold;color:black !important;">/  المجموع الإجمالي</span></td>
                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{(parseFloat(totalAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                    </tr>
                </table>
            </div>
        </div>
    </div>-->
                <!--Footer-->
            </div>
        </div>
    </div>

</template>

<script>
    import moment from "moment";
    import clickMixin from '@/Mixins/clickMixin'


    const options = {
        name: '',
        specs: [
            'fullscreen=no',
            'titlebar=yes',
            'scrollbars=yes'
        ],
        styles: [
            'https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css',
            'https://unpkg.com/kidlat-css/css/kidlat.css',
        ],
        timeout: 700,
        autoClose: true,
        windowTitle: window.document.title,

    }
    export default {
        mixins: [clickMixin],

        props: ['printDetails', 'headerFooter', 'formName', 'fromDate', 'toDate', 'invoice'],

        data: function () {
            return {
                itemTotal: 0,
                listItemP1: [],
                listItemP2: [],
                listItemP3: [],

                isHeaderFooter: '',
                IsPaksitanClient: false,
                invoicePrint: '',
                arabic: '',
                english: '',
                list: [],
                render: 0,
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                }
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

                this.$htmlToPaper('purchaseInvoice', options, () => {




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

                root.Print = root.isPrint;
                {
                    
                    var totalItem = this.printDetails.length;
                    this.itemTotal = totalItem;
                    if (totalItem < 21) {
                        for (var i = 0; i < totalItem; i++) {
                            root.listItemP1.push(root.printDetails[i]);
                        }
                    }
                    else if (totalItem >= 21 && totalItem < 40) {
                        for (var k = 0; k < totalItem; k++) {
                            if (k < 21) {
                                root.listItemP1.push(root.printDetails[k]);
                            }
                            else {
                                root.listItemP2.push(root.printDetails[k]);
                            }
                        }
                    }
                    else if (totalItem >= 40 && totalItem <= 58) {
                        for (var l = 0; l < totalItem; l++) {
                            if (l < 21) {
                                root.listItemP1.push(root.printDetails[l]);
                            }
                            else if (l > 21 && l <= 41) {
                                root.listItemP2.push(root.printDetails[l]);
                            }
                            else {
                                root.listItemP3.push(root.printDetails[l]);
                            }
                        }
                    }


                    setTimeout(function () {
                        root.printInvoice();
                    }, 125)
                }

            }
        }
    }
</script>

