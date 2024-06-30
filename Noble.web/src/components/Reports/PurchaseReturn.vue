<template>

    <div>
        <div ref="mychildcomponent" hidden id='purchaseInvoice' class="col-md-12" style="color:black !important">

            <!--page1-->
            <div v-if="itemTotal<=24">
                <!--HEADER-->
                <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
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
                    <div class="row " style="margin-bottom:10px !important;height:10mm">
                        <div class="col-md-12">
                            <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                <span style="font-size:16px; padding-top:3px; font-weight:bolder">{{ $t('PurchaseReturn.PurchaseReturn') }}</span>
                            </p>
                        </div>
                    </div>
                </div>
                <div style="height:60mm;" v-else></div>

                <div style="height:20mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                            <div>
                                <!--Row 1-->
                                <div class="row">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceDate}}</div>
                                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Inv.No:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceNo}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                    </div>
                                </div>

                                <!--Row 3-->
                                <div class="row">

                                    <div class="col-md-12" style="display:flex;">
                                        <div style="width:13%;font-weight:bolder;text-align:right;color:black !important;">Supplier Name:</div>
                                        <div style="width:73%;text-align:center;font-weight:bold;color:black !important;">{{list.supplierName}}</div>
                                        <div style="width:12%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important">
                                            :اسم المورد
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div style="height:230mm;border:2px solid !important;">
                    <div class="row ">
                        <div class="col-md-12 ">
                            <table class="table">
                                <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                    <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                    <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo ">اسم الصنف</th>
                                    <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else>اسم الصنف</th>
                                    <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                    <th class="text-center" style="width:6%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                    <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                    <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">سعرالوحدة</th>
                                    <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الاجمالي </th>
                                    <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الخصم </th>
                                    <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">%الضريية</th>
                                    <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">قيمة الضريبة</th>
                                    <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">المجموع الإجمالي </th>
                                </tr>
                                <tr class="heading" style="font-size:16px;" v-else>
                                    <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>
                                    <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.styleNo">Product Name</th>
                                    <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else>Product Name</th>
                                    <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                    <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                    <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                    <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                    <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                    <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                    <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                    <th class="text-right" style="width:11%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                                </tr>


                                <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in list.purchasePostItems" v-bind:key="item.id">
                                    <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.productId==null? '': item.product.styleNumber}}</td>
                                    <td v-if="english=='true' && arabic=='false' && headerFooters.englishName" class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.productName}}</td>
                                    <td v-else-if="isOtherLang() && english=='false' && headerFooters.arabicName" class="text-left" style="border-top:0 !important; border-bottom:0 !important;padding-top:3px !important; padding-bottom:3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                    <td v-else-if="isOtherLang() && english=='true' && !headerFooters.arabicName && headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.productName}}</td>
                                    <td v-else-if="isOtherLang() && english=='true' && headerFooters.arabicName && !headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                    <td v-else style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:left !important;color:black !important;">{{item.productName}}</span> <span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span> </td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.quantity }}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{calulateLineTotalQty(item) }}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">  {{item.total.toFixed(3).slice(0,-1)}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(0) }}%</td>
                                    <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                    <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div style="height:41mm;margin-top:1mm;">
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-bordered tbl_padding">
                                <tr>
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;">Page: 1 - 1</td>
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ calulateTotalQty }} </td>
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ calulateTotalQty }} </td>
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{ calulateTotalExclVAT.toFixed(3).slice(0,-1) }}</td>
                                </tr>
                                <tr>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((calulateNetTotal - calulateDiscountAmount))   }}</td>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><!--<barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>--></td>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>

                                <tr>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black !important;">/ المبلخ الاجمالي</span></td>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(calulateNetTotal - calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter=='true'">
                    <div class="row">
                        <table class="table text-center">

                            <tr>
                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                    {{ $t('PurchaseInvoice.ReceivedBy') }}::
                                </td>
                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                    {{ $t('PurchaseInvoice.ApprovedBy') }}::
                                </td>
                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                    {{ $t('PurchaseInvoice.PreparedBy') }}::
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>

                <!--Footer-->
                <div class="col-md-12" style="height: 42mm;border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-3">
                            <u><b><span style="font-size:18px;color: black !important;font-weight:bold;">Terms & Conditions</span></b></u><br />
                            <span style="font-size:14px;color: black !important;" v-html="headerFooters.footerEn">

                            </span>
                        </div>

                        <div class="col-md-3 text-center mt-1">

                            <vue-qrcode v-bind:value="qrValue" style="width:140px;" />
                        </div>
                        <div class="col-md-3 pt-4">

                            <div style=" width:90%;border:1px solid;text-align:center;font-size:15px;font-weight:bold;">
                                Bank Details &nbsp; تفاصيل البنك
                            </div>
                            <div style="width:90%;display:flex;border:1px solid;">
                                <div style="width:70%;text-align:center;border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                    {{headerFooters.bankAccount1}}
                                </div>
                                <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                    {{headerFooters.bankIcon1}}
                                </div>
                            </div>
                            <div style="width:90%;display:flex;border:1px solid;">
                                <div style="width:70%;text-align:center; border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                    {{headerFooters.bankAccount2}}
                                </div>
                                <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                    {{headerFooters.bankIcon2}}
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 text-right">
                            <u><b><span style="font-size:20px;font-weight:bold;">البنود و الظروف</span></b></u><br />
                            <span class=" text-center" style="font-size:16px;color: black !important;" v-html="headerFooters.footerAr">

                            </span>
                        </div>

                    </div>
                </div>

                <div class="row" v-if="isHeaderFooter=='true'">
                    <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                    <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
                </div>
            </div>

            <!--page2-->
            <div v-else-if="itemTotal>24 && itemTotal<=48">
                <div>
                    <!--HEADER-->
                    <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
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
                        <div class="row " style="margin-bottom:10px !important;height:10mm">
                            <div class="col-md-12">
                                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                    <span style="font-size:16px; padding-top:3px; font-weight:bolder">{{ $t('PurchaseReturn.PurchaseReturn') }}</span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div style="height:60mm;" v-else></div>

                    <div style="height:20mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div>
                                    <!--Row 1-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceDate}}</div>
                                            <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Inv.No:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceNo}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                        </div>
                                    </div>

                                    <!--Row 3-->
                                    <div class="row">

                                        <div class="col-md-12" style="display:flex;">
                                            <div style="width:13%;font-weight:bolder;text-align:right;color:black !important;">Supplier Name:</div>
                                            <div style="width:73%;text-align:center;font-weight:bold;color:black !important;">{{list.supplierName}}</div>
                                            <div style="width:12%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important">
                                                :اسم المورد
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="height:230mm;border:2px solid !important;">
                        <div class="row ">
                            <div class="col-md-12 ">
                                <table class="table">
                                    <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo ">اسم الصنف</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else>اسم الصنف</th>
                                        <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                        <th class="text-center" style="width:6%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">سعرالوحدة</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الاجمالي </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الخصم </th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">%الضريية</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">قيمة الضريبة</th>
                                        <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">المجموع الإجمالي </th>
                                    </tr>
                                    <tr class="heading" style="font-size:16px;" v-else>
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.styleNo">Product Name</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else>Product Name</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                        <th class="text-right" style="width:11%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                                    </tr>


                                    <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in listItemP1" v-bind:key="item.id">
                                        <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.productId==null? '': item.product.styleNumber}}</td>
                                        <td v-if="english=='true' && arabic=='false' && headerFooters.englishName" class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='false' && headerFooters.arabicName" class="text-left" style="border-top:0 !important; border-bottom:0 !important;padding-top:3px !important; padding-bottom:3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else-if="isOtherLang() && english=='true' && !headerFooters.arabicName && headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='true' && headerFooters.arabicName && !headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:left !important;color:black !important;">{{item.productName}}</span> <span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span> </td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{calulateLineTotalQty(item) }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">  {{item.total.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(0) }}%</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div style="height:41mm;margin-top:1mm;">
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-bordered tbl_padding">
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;">Page: 1 - 2</td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ listItemP1Summary.calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ listItemP1Summary.calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{ listItemP1Summary.calulateTotalExclVAT.toFixed(3).slice(0,-1) }}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((listItemP1Summary.calulateNetTotal - listItemP1Summary.calulateDiscountAmount))   }}</td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><!--<barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>--></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP1Summary.calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP1Summary.calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black !important;">/ المبلخ الاجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(listItemP1Summary.calulateNetTotal - listItemP1Summary.calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter=='true'">
                        <div class="row">
                            <table class="table text-center">

                                <tr>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ReceivedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ApprovedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.PreparedBy') }}::
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>

                    <!--Footer-->
                    <div class="col-md-12" style="height: 42mm;border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-3">
                                <u><b><span style="font-size:18px;color: black !important;font-weight:bold;">Terms & Conditions</span></b></u><br />
                                <span style="font-size:14px;color: black !important;" v-html="headerFooters.footerEn">

                                </span>
                            </div>

                            <div class="col-md-3 text-center mt-1">

                                <vue-qrcode v-bind:value="qrValue" style="width:140px;" />
                            </div>
                            <div class="col-md-3 pt-4">

                                <div style=" width:90%;border:1px solid;text-align:center;font-size:15px;font-weight:bold;">
                                    Bank Details &nbsp; تفاصيل البنك
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center;border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount1}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon1}}
                                    </div>
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center; border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount2}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon2}}
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 text-right">
                                <u><b><span style="font-size:20px;font-weight:bold;">البنود و الظروف</span></b></u><br />
                                <span class=" text-center" style="font-size:16px;color: black !important;" v-html="headerFooters.footerAr">
                                </span>
                            </div>

                        </div>
                    </div>

                    <div class="row" v-if="isHeaderFooter=='true'">
                        <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                        <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
                    </div>
                </div>

                <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>

                <div>
                    <!--HEADER-->
                    <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
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
                        <div class="row " style="margin-bottom:10px !important;height:10mm">
                            <div class="col-md-12">
                                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                    <span style="font-size:16px; padding-top:3px; font-weight:bolder">{{ $t('PurchaseReturn.PurchaseReturn') }}</span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div style="height:60mm;" v-else></div>

                    <div style="height:20mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div>
                                    <!--Row 1-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceDate}}</div>
                                            <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Inv.No:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceNo}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                        </div>
                                    </div>

                                    <!--Row 3-->
                                    <div class="row">

                                        <div class="col-md-12" style="display:flex;">
                                            <div style="width:13%;font-weight:bolder;text-align:right;color:black !important;">Supplier Name:</div>
                                            <div style="width:73%;text-align:center;font-weight:bold;color:black !important;">{{list.supplierName}}</div>
                                            <div style="width:12%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important">
                                                :اسم المورد
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="height:230mm;border:2px solid !important;">
                        <div class="row ">
                            <div class="col-md-12 ">
                                <table class="table">
                                    <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo ">اسم الصنف</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else>اسم الصنف</th>
                                        <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                        <th class="text-center" style="width:6%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">سعرالوحدة</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الاجمالي </th>
                                         <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الخصم </th>
                                         <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">%الضريية</th>
                                         <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">قيمة الضريبة</th>
                                         <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">المجموع الإجمالي </th>
                                     </tr>
                                     <tr class="heading" style="font-size:16px;" v-else>
                                         <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                         <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>
                                         <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.styleNo">Product Name</th>
                                         <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else>Product Name</th>
                                         <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                         <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                         <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                         <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                         <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                         <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                         <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                         <th class="text-right" style="width:11%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                                     </tr>
                                     <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in listItemP2" v-bind:key="item.id">
                                         <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+25}}</td>
                                         <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.productId==null? '': item.product.styleNumber}}</td>
                                         <td v-if="english=='true' && arabic=='false' && headerFooters.englishName" class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.productName}}</td>
                                         <td v-else-if="isOtherLang() && english=='false' && headerFooters.arabicName" class="text-left" style="border-top:0 !important; border-bottom:0 !important;padding-top:3px !important; padding-bottom:3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                         <td v-else-if="isOtherLang() && english=='true' && !headerFooters.arabicName && headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.productName}}</td>
                                         <td v-else-if="isOtherLang() && english=='true' && headerFooters.arabicName && !headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                         <td v-else style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:left !important;color:black !important;">{{item.productName}}</span> <span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span> </td>
                                         <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.quantity }}</td>
                                         <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                         <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{calulateLineTotalQty(item) }}</td>
                                         <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                         <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">  {{item.total.toFixed(3).slice(0,-1)}}</td>
                                         <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                         <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(0) }}%</td>
                                         <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                         <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div style="height:41mm;margin-top:1mm;">
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-bordered tbl_padding">
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;">Page: 1 - 2</td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{ calulateTotalExclVAT.toFixed(3).slice(0,-1) }}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((calulateNetTotal - calulateDiscountAmount))   }}</td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><!--<barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>--></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black !important;">/ المبلخ الاجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(calulateNetTotal - calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter=='true'">
                        <div class="row">
                            <table class="table text-center">

                                <tr>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ReceivedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ApprovedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.PreparedBy') }}::
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>

                    <!--Footer-->
                    <div class="col-md-12" style="height: 42mm;border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-3">
                                <u><b><span style="font-size:18px;color: black !important;font-weight:bold;">Terms & Conditions</span></b></u><br />
                                <span style="font-size:14px;color: black !important;" v-html="headerFooters.footerEn">

                                </span>
                            </div>

                            <div class="col-md-3 text-center mt-1">

                                <vue-qrcode v-bind:value="qrValue" style="width:140px;" />
                            </div>
                            <div class="col-md-3 pt-4">

                                <div style=" width:90%;border:1px solid;text-align:center;font-size:15px;font-weight:bold;">
                                    Bank Details &nbsp; تفاصيل البنك
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center;border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount1}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon1}}
                                    </div>
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center; border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount2}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon2}}
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 text-right">
                                <u><b><span style="font-size:20px;font-weight:bold;">البنود و الظروف</span></b></u><br />
                                <span class=" text-center" style="font-size:16px;color: black !important;" v-html="headerFooters.footerAr">

                                </span>
                            </div>

                        </div>
                    </div>

                    <div class="row" v-if="isHeaderFooter=='true'">
                        <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                        <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
                    </div>
                </div>

            </div>

            <!--page3-->
            <div v-else-if="itemTotal>48 && itemTotal<=72">
                <div>
                    <!--HEADER-->
                    <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
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
                        <div class="row " style="margin-bottom:10px !important;height:10mm">
                            <div class="col-md-12">
                                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                    <span style="font-size:16px; padding-top:3px; font-weight:bolder">{{ $t('PurchaseReturn.PurchaseReturn') }}</span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div style="height:60mm;" v-else></div>

                    <div style="height:20mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div>
                                    <!--Row 1-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceDate}}</div>
                                            <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Inv.No:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceNo}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                        </div>
                                    </div>

                                    <!--Row 3-->
                                    <div class="row">

                                        <div class="col-md-12" style="display:flex;">
                                            <div style="width:13%;font-weight:bolder;text-align:right;color:black !important;">Supplier Name:</div>
                                            <div style="width:73%;text-align:center;font-weight:bold;color:black !important;">{{list.supplierName}}</div>
                                            <div style="width:12%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important">
                                                :اسم المورد
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="height:230mm;border:2px solid !important;">
                        <div class="row ">
                            <div class="col-md-12 ">
                                <table class="table">
                                    <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo ">اسم الصنف</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else>اسم الصنف</th>
                                        <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                        <th class="text-center" style="width:6%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">سعرالوحدة</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الاجمالي </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الخصم </th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">%الضريية</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">قيمة الضريبة</th>
                                        <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">المجموع الإجمالي </th>
                                    </tr>
                                    <tr class="heading" style="font-size:16px;" v-else>
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.styleNo">Product Name</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else>Product Name</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                        <th class="text-right" style="width:11%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                                    </tr>


                                    <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in listItemP1" v-bind:key="item.id">
                                        <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.productId==null? '': item.product.styleNumber}}</td>
                                        <td v-if="english=='true' && arabic=='false' && headerFooters.englishName" class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='false' && headerFooters.arabicName" class="text-left" style="border-top:0 !important; border-bottom:0 !important;padding-top:3px !important; padding-bottom:3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else-if="isOtherLang() && english=='true' && !headerFooters.arabicName && headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='true' && headerFooters.arabicName && !headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:left !important;color:black !important;">{{item.productName}}</span> <span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span> </td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{calulateLineTotalQty(item) }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">  {{item.total.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(0) }}%</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div style="height:41mm;margin-top:1mm;">
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-bordered tbl_padding">
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;">Page: 1 - 3</td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ listItemP1Summary.calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ listItemP1Summary.calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{ listItemP1Summary.calulateTotalExclVAT.toFixed(3).slice(0,-1) }}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((listItemP1Summary.calulateNetTotal - listItemP1Summary.calulateDiscountAmount))   }}</td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><!--<barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>--></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP1Summary.calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP1Summary.calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black !important;">/ المبلخ الاجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(listItemP1Summary.calulateNetTotal - listItemP1Summary.calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter=='true'">
                        <div class="row">
                            <table class="table text-center">

                                <tr>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ReceivedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ApprovedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.PreparedBy') }}::
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>

                    <!--Footer-->
                    <div class="col-md-12" style="height: 42mm;border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-3">
                                <u><b><span style="font-size:18px;color: black !important;font-weight:bold;">Terms & Conditions</span></b></u><br />
                                <span style="font-size:14px;color: black !important;" v-html="headerFooters.footerEn">

                                </span>
                            </div>

                            <div class="col-md-3 text-center mt-1">

                                <vue-qrcode v-bind:value="qrValue" style="width:140px;" />
                            </div>
                            <div class="col-md-3 pt-4">

                                <div style=" width:90%;border:1px solid;text-align:center;font-size:15px;font-weight:bold;">
                                    Bank Details &nbsp; تفاصيل البنك
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center;border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount1}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon1}}
                                    </div>
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center; border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount2}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon2}}
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 text-right">
                                <u><b><span style="font-size:20px;font-weight:bold;">البنود و الظروف</span></b></u><br />
                                <span class=" text-center" style="font-size:16px;color: black !important;" v-html="headerFooters.footerAr">

                                </span>
                            </div>

                        </div>
                    </div>

                    <div class="row" v-if="isHeaderFooter=='true'">
                        <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                        <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
                    </div>
                </div>

                <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>

                <div>
                    <!--HEADER-->
                    <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
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
                        <div class="row " style="margin-bottom:10px !important;height:10mm">
                            <div class="col-md-12">
                                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                    <span style="font-size:16px; padding-top:3px; font-weight:bolder">{{ $t('PurchaseReturn.PurchaseReturn') }}</span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div style="height:60mm;" v-else></div>

                    <div style="height:20mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div>
                                    <!--Row 1-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceDate}}</div>
                                            <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Inv.No:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceNo}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                        </div>
                                    </div>

                                    <!--Row 3-->
                                    <div class="row">

                                        <div class="col-md-12" style="display:flex;">
                                            <div style="width:13%;font-weight:bolder;text-align:right;color:black !important;">Supplier Name:</div>
                                            <div style="width:73%;text-align:center;font-weight:bold;color:black !important;">{{list.supplierName}}</div>
                                            <div style="width:12%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important">
                                                :اسم المورد
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="height:230mm;border:2px solid !important;">
                        <div class="row ">
                            <div class="col-md-12 ">
                                <table class="table">
                                    <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo ">اسم الصنف</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else>اسم الصنف</th>
                                        <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                        <th class="text-center" style="width:6%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">سعرالوحدة</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الاجمالي </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الخصم </th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">%الضريية</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">قيمة الضريبة</th>
                                        <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">المجموع الإجمالي </th>
                                    </tr>
                                    <tr class="heading" style="font-size:16px;" v-else>
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.styleNo">Product Name</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else>Product Name</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                        <th class="text-right" style="width:11%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                                    </tr>


                                    <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in listItemP2" v-bind:key="item.id">
                                        <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+25}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.productId==null? '': item.product.styleNumber}}</td>
                                        <td v-if="english=='true' && arabic=='false' && headerFooters.englishName" class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='false' && headerFooters.arabicName" class="text-left" style="border-top:0 !important; border-bottom:0 !important;padding-top:3px !important; padding-bottom:3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else-if="isOtherLang() && english=='true' && !headerFooters.arabicName && headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='true' && headerFooters.arabicName && !headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:left !important;color:black !important;">{{item.productName}}</span> <span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span> </td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{calulateLineTotalQty(item) }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">  {{item.total.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(0) }}%</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div style="height:41mm;margin-top:1mm;">
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-bordered tbl_padding">
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;">Page: 2 - 3</td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ listItemP2Summary.calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ listItemP2Summary.calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{ listItemP2Summary.calulateTotalExclVAT.toFixed(3).slice(0,-1) }}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((listItemP2Summary.calulateNetTotal - listItemP2Summary.calulateDiscountAmount))   }}</td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><!--<barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>--></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP2Summary.calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP2Summary.calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black !important;">/ المبلخ الاجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(listItemP2Summary.calulateNetTotal - listItemP2Summary.calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter=='true'">
                        <div class="row">
                            <table class="table text-center">

                                <tr>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ReceivedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ApprovedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.PreparedBy') }}::
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>

                    <!--Footer-->
                    <div class="col-md-12" style="height: 42mm;border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-3">
                                <u><b><span style="font-size:18px;color: black !important;font-weight:bold;">Terms & Conditions</span></b></u><br />
                                <span style="font-size:14px;color: black !important;" v-html="headerFooters.footerEn">

                                </span>
                            </div>

                            <div class="col-md-3 text-center mt-1">

                                <vue-qrcode v-bind:value="qrValue" style="width:140px;" />
                            </div>
                            <div class="col-md-3 pt-4">

                                <div style=" width:90%;border:1px solid;text-align:center;font-size:15px;font-weight:bold;">
                                    Bank Details &nbsp; تفاصيل البنك
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center;border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount1}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon1}}
                                    </div>
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center; border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount2}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon2}}
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 text-right">
                                <u><b><span style="font-size:20px;font-weight:bold;">البنود و الظروف</span></b></u><br />
                                <span class=" text-center" style="font-size:16px;color: black !important;" v-html="headerFooters.footerAr">

                                </span>
                            </div>

                        </div>
                    </div>

                    <div class="row" v-if="isHeaderFooter=='true'">
                        <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                        <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
                    </div>
                </div>

                <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>

                <div>
                    <!--HEADER-->
                    <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
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
                        <div class="row " style="margin-bottom:10px !important;height:10mm">
                            <div class="col-md-12">
                                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                    <span style="font-size:16px; padding-top:3px; font-weight:bolder">{{ $t('PurchaseReturn.PurchaseReturn') }}</span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div style="height:60mm;" v-else></div>

                    <div style="height:20mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div>
                                    <!--Row 1-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceDate}}</div>
                                            <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Inv.No:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceNo}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                        </div>
                                    </div>

                                    <!--Row 3-->
                                    <div class="row">

                                        <div class="col-md-12" style="display:flex;">
                                            <div style="width:13%;font-weight:bolder;text-align:right;color:black !important;">Supplier Name:</div>
                                            <div style="width:73%;text-align:center;font-weight:bold;color:black !important;">{{list.supplierName}}</div>
                                            <div style="width:12%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important">
                                                :اسم المورد
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="height:230mm;border:2px solid !important;">
                        <div class="row ">
                            <div class="col-md-12 ">
                                <table class="table">
                                    <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo ">اسم الصنف</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else>اسم الصنف</th>
                                        <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                        <th class="text-center" style="width:6%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">سعرالوحدة</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الاجمالي </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الخصم </th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">%الضريية</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">قيمة الضريبة</th>
                                        <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">المجموع الإجمالي </th>
                                    </tr>
                                    <tr class="heading" style="font-size:16px;" v-else>
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.styleNo">Product Name</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else>Product Name</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                        <th class="text-right" style="width:11%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                                    </tr>


                                    <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in listItemP3" v-bind:key="item.id">
                                        <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+49}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.productId==null? '': item.product.styleNumber}}</td>
                                        <td v-if="english=='true' && arabic=='false' && headerFooters.englishName" class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='false' && headerFooters.arabicName" class="text-left" style="border-top:0 !important; border-bottom:0 !important;padding-top:3px !important; padding-bottom:3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else-if="isOtherLang() && english=='true' && !headerFooters.arabicName && headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='true' && headerFooters.arabicName && !headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:left !important;color:black !important;">{{item.productName}}</span> <span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span> </td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{calulateLineTotalQty(item) }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">  {{item.total.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(0) }}%</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div style="height:41mm;margin-top:1mm;">
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-bordered tbl_padding">
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;">Page: 3 - 3</td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{ calulateTotalExclVAT.toFixed(3).slice(0,-1) }}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((calulateNetTotal - calulateDiscountAmount))   }}</td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><!--<barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>--></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black !important;">/ المبلخ الاجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(calulateNetTotal - calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter=='true'">
                        <div class="row">
                            <table class="table text-center">

                                <tr>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ReceivedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ApprovedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.PreparedBy') }}::
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>

                    <!--Footer-->
                    <div class="col-md-12" style="height: 42mm;border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-3">
                                <u><b><span style="font-size:18px;color: black !important;font-weight:bold;">Terms & Conditions</span></b></u><br />
                                <span style="font-size:14px;color: black !important;" v-html="headerFooters.footerEn">

                                </span>
                            </div>

                            <div class="col-md-3 text-center mt-1">

                                <vue-qrcode v-bind:value="qrValue" style="width:140px;" />
                            </div>
                            <div class="col-md-3 pt-4">

                                <div style=" width:90%;border:1px solid;text-align:center;font-size:15px;font-weight:bold;">
                                    Bank Details &nbsp; تفاصيل البنك
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center;border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount1}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon1}}
                                    </div>
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center; border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount2}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon2}}
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 text-right">
                                <u><b><span style="font-size:20px;font-weight:bold;">البنود و الظروف</span></b></u><br />
                                <span class=" text-center" style="font-size:16px;color: black !important;" v-html="headerFooters.footerAr">

                                </span>
                            </div>

                        </div>
                    </div>

                    <div class="row" v-if="isHeaderFooter=='true'">
                        <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                        <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
                    </div>
                </div>

            </div>


            <!--page4-->
            <div v-else-if="itemTotal>72 && itemTotal<=96">
                <div>
                    <!--HEADER-->
                    <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
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
                        <div class="row " style="margin-bottom:10px !important;height:10mm">
                            <div class="col-md-12">
                                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                    <span style="font-size:16px; padding-top:3px; font-weight:bolder">{{ $t('PurchaseReturn.PurchaseReturn') }}</span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div style="height:60mm;" v-else></div>

                    <div style="height:20mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div>
                                    <!--Row 1-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceDate}}</div>
                                            <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Inv.No:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceNo}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                        </div>
                                    </div>

                                    <!--Row 3-->
                                    <div class="row">

                                        <div class="col-md-12" style="display:flex;">
                                            <div style="width:13%;font-weight:bolder;text-align:right;color:black !important;">Supplier Name:</div>
                                            <div style="width:73%;text-align:center;font-weight:bold;color:black !important;">{{list.supplierName}}</div>
                                            <div style="width:12%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important">
                                                :اسم المورد
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="height:230mm;border:2px solid !important;">
                        <div class="row ">
                            <div class="col-md-12 ">
                                <table class="table">
                                    <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo ">اسم الصنف</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else>اسم الصنف</th>
                                        <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                        <th class="text-center" style="width:6%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">سعرالوحدة</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الاجمالي </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الخصم </th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">%الضريية</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">قيمة الضريبة</th>
                                        <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">المجموع الإجمالي </th>
                                    </tr>
                                    <tr class="heading" style="font-size:16px;" v-else>
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.styleNo">Product Name</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else>Product Name</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                        <th class="text-right" style="width:11%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                                    </tr>


                                    <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in listItemP1" v-bind:key="item.id">
                                        <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.productId==null? '': item.product.styleNumber}}</td>
                                        <td v-if="english=='true' && arabic=='false' && headerFooters.englishName" class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='false' && headerFooters.arabicName" class="text-left" style="border-top:0 !important; border-bottom:0 !important;padding-top:3px !important; padding-bottom:3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else-if="isOtherLang() && english=='true' && !headerFooters.arabicName && headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='true' && headerFooters.arabicName && !headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:left !important;color:black !important;">{{item.productName}}</span> <span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span> </td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{calulateLineTotalQty(item) }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">  {{item.total.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(0) }}%</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div style="height:41mm;margin-top:1mm;">
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-bordered tbl_padding">
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;">Page: 1 - 4</td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ listItemP1Summary.calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ listItemP1Summary.calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{ listItemP1Summary.calulateTotalExclVAT.toFixed(3).slice(0,-1) }}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((listItemP1Summary.calulateNetTotal - listItemP1Summary.calulateDiscountAmount))   }}</td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><!--<barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>--></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP1Summary.calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP1Summary.calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black !important;">/ المبلخ الاجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(listItemP1Summary.calulateNetTotal - listItemP1Summary.calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter=='true'">
                        <div class="row">
                            <table class="table text-center">

                                <tr>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ReceivedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ApprovedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.PreparedBy') }}::
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>

                    <!--Footer-->
                    <div class="col-md-12" style="height: 42mm;border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-3">
                                <u><b><span style="font-size:18px;color: black !important;font-weight:bold;">Terms & Conditions</span></b></u><br />
                                <span style="font-size:14px;color: black !important;" v-html="headerFooters.footerEn">

                                </span>
                            </div>

                            <div class="col-md-3 text-center mt-1">

                                <vue-qrcode v-bind:value="qrValue" style="width:140px;" />
                            </div>
                            <div class="col-md-3 pt-4">

                                <div style=" width:90%;border:1px solid;text-align:center;font-size:15px;font-weight:bold;">
                                    Bank Details &nbsp; تفاصيل البنك
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center;border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount1}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon1}}
                                    </div>
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center; border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount2}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon2}}
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 text-right">
                                <u><b><span style="font-size:20px;font-weight:bold;">البنود و الظروف</span></b></u><br />
                                <span class=" text-center" style="font-size:16px;color: black !important;" v-html="headerFooters.footerAr">

                                </span>
                            </div>

                        </div>
                    </div>

                    <div class="row" v-if="isHeaderFooter=='true'">
                        <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                        <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
                    </div>
                </div>

                <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>

                <div>
                    <!--HEADER-->
                    <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
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
                        <div class="row " style="margin-bottom:10px !important;height:10mm">
                            <div class="col-md-12">
                                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                    <span style="font-size:16px; padding-top:3px; font-weight:bolder">{{ $t('PurchaseReturn.PurchaseReturn') }}</span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div style="height:60mm;" v-else></div>

                    <div style="height:20mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div>
                                    <!--Row 1-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceDate}}</div>
                                            <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Inv.No:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceNo}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                        </div>
                                    </div>

                                    <!--Row 3-->
                                    <div class="row">

                                        <div class="col-md-12" style="display:flex;">
                                            <div style="width:13%;font-weight:bolder;text-align:right;color:black !important;">Supplier Name:</div>
                                            <div style="width:73%;text-align:center;font-weight:bold;color:black !important;">{{list.supplierName}}</div>
                                            <div style="width:12%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important">
                                                :اسم المورد
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="height:230mm;border:2px solid !important;">
                        <div class="row ">
                            <div class="col-md-12 ">
                                <table class="table">
                                    <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo ">اسم الصنف</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else>اسم الصنف</th>
                                        <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                        <th class="text-center" style="width:6%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">سعرالوحدة</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الاجمالي </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الخصم </th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">%الضريية</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">قيمة الضريبة</th>
                                        <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">المجموع الإجمالي </th>
                                    </tr>
                                    <tr class="heading" style="font-size:16px;" v-else>
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.styleNo">Product Name</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else>Product Name</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                        <th class="text-right" style="width:11%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                                    </tr>


                                    <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in listItemP2" v-bind:key="item.id">
                                        <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+25}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.productId==null? '': item.product.styleNumber}}</td>
                                        <td v-if="english=='true' && arabic=='false' && headerFooters.englishName" class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='false' && headerFooters.arabicName" class="text-left" style="border-top:0 !important; border-bottom:0 !important;padding-top:3px !important; padding-bottom:3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else-if="isOtherLang() && english=='true' && !headerFooters.arabicName && headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='true' && headerFooters.arabicName && !headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:left !important;color:black !important;">{{item.productName}}</span> <span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span> </td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{calulateLineTotalQty(item) }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">  {{item.total.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(0) }}%</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div style="height:41mm;margin-top:1mm;">
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-bordered tbl_padding">
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;">Page: 2 - 4</td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ listItemP2Summary.calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ listItemP2Summary.calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{ listItemP2Summary.calulateTotalExclVAT.toFixed(3).slice(0,-1) }}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((listItemP2Summary.calulateNetTotal - listItemP2Summary.calulateDiscountAmount))   }}</td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><!--<barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>--></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP2Summary.calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP2Summary.calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black !important;">/ المبلخ الاجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(listItemP2Summary.calulateNetTotal - listItemP2Summary.calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter=='true'">
                        <div class="row">
                            <table class="table text-center">

                                <tr>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ReceivedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ApprovedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.PreparedBy') }}::
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>

                    <!--Footer-->
                    <div class="col-md-12" style="height: 42mm;border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-3">
                                <u><b><span style="font-size:18px;color: black !important;font-weight:bold;">Terms & Conditions</span></b></u><br />
                                <span style="font-size:14px;color: black !important;" v-html="headerFooters.footerEn">

                                </span>
                            </div>

                            <div class="col-md-3 text-center mt-1">

                                <vue-qrcode v-bind:value="qrValue" style="width:140px;" />
                            </div>
                            <div class="col-md-3 pt-4">

                                <div style=" width:90%;border:1px solid;text-align:center;font-size:15px;font-weight:bold;">
                                    Bank Details &nbsp; تفاصيل البنك
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center;border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount1}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon1}}
                                    </div>
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center; border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount2}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon2}}
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 text-right">
                                <u><b><span style="font-size:20px;font-weight:bold;">البنود و الظروف</span></b></u><br />
                                <span class=" text-center" style="font-size:16px;color: black !important;" v-html="headerFooters.footerAr">

                                </span>
                            </div>

                        </div>
                    </div>

                    <div class="row" v-if="isHeaderFooter=='true'">
                        <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                        <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
                    </div>
                </div>

                <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>

                <div>
                    <!--HEADER-->
                    <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
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
                        <div class="row " style="margin-bottom:10px !important;height:10mm">
                            <div class="col-md-12">
                                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                    <span style="font-size:16px; padding-top:3px; font-weight:bolder">{{ $t('PurchaseReturn.PurchaseReturn') }}</span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div style="height:60mm;" v-else></div>

                    <div style="height:20mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div>
                                    <!--Row 1-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceDate}}</div>
                                            <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Inv.No:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceNo}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                        </div>
                                    </div>

                                    <!--Row 3-->
                                    <div class="row">

                                        <div class="col-md-12" style="display:flex;">
                                            <div style="width:13%;font-weight:bolder;text-align:right;color:black !important;">Supplier Name:</div>
                                            <div style="width:73%;text-align:center;font-weight:bold;color:black !important;">{{list.supplierName}}</div>
                                            <div style="width:12%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important">
                                                :اسم المورد
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="height:230mm;border:2px solid !important;">
                        <div class="row ">
                            <div class="col-md-12 ">
                                <table class="table">
                                    <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo ">اسم الصنف</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else>اسم الصنف</th>
                                        <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                        <th class="text-center" style="width:6%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">سعرالوحدة</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الاجمالي </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الخصم </th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">%الضريية</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">قيمة الضريبة</th>
                                        <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">المجموع الإجمالي </th>
                                    </tr>
                                    <tr class="heading" style="font-size:16px;" v-else>
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.styleNo">Product Name</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else>Product Name</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                        <th class="text-right" style="width:11%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                                    </tr>


                                    <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in listItemP3" v-bind:key="item.id">
                                        <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+49}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.productId==null? '': item.product.styleNumber}}</td>
                                        <td v-if="english=='true' && arabic=='false' && headerFooters.englishName" class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='false' && headerFooters.arabicName" class="text-left" style="border-top:0 !important; border-bottom:0 !important;padding-top:3px !important; padding-bottom:3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else-if="isOtherLang() && english=='true' && !headerFooters.arabicName && headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='true' && headerFooters.arabicName && !headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:left !important;color:black !important;">{{item.productName}}</span> <span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span> </td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{calulateLineTotalQty(item) }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">  {{item.total.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(0) }}%</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div style="height:41mm;margin-top:1mm;">
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-bordered tbl_padding">
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;">Page: 3 - 4</td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ listItemP3Summary.calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ listItemP3Summary.calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{ listItemP3Summary.calulateTotalExclVAT.toFixed(3).slice(0,-1) }}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((listItemP3Summary.calulateNetTotal - listItemP3Summary.calulateDiscountAmount))   }}</td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><!--<barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>--></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP3Summary.calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP3Summary.calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black !important;">/ المبلخ الاجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(listItemP3Summary.calulateNetTotal - listItemP3Summary.calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter=='true'">
                        <div class="row">
                            <table class="table text-center">

                                <tr>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ReceivedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ApprovedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.PreparedBy') }}::
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>

                    <!--Footer-->
                    <div class="col-md-12" style="height: 42mm;border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-3">
                                <u><b><span style="font-size:18px;color: black !important;font-weight:bold;">Terms & Conditions</span></b></u><br />
                                <span style="font-size:14px;color: black !important;" v-html="headerFooters.footerEn">

                                </span>
                            </div>

                            <div class="col-md-3 text-center mt-1">

                                <vue-qrcode v-bind:value="qrValue" style="width:140px;" />
                            </div>
                            <div class="col-md-3 pt-4">

                                <div style=" width:90%;border:1px solid;text-align:center;font-size:15px;font-weight:bold;">
                                    Bank Details &nbsp; تفاصيل البنك
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center;border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount1}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon1}}
                                    </div>
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center; border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount2}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon2}}
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 text-right">
                                <u><b><span style="font-size:20px;font-weight:bold;">البنود و الظروف</span></b></u><br />
                                <span class=" text-center" style="font-size:16px;color: black !important;" v-html="headerFooters.footerAr">

                                </span>
                            </div>

                        </div>
                    </div>

                    <div class="row" v-if="isHeaderFooter=='true'">
                        <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                        <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
                    </div>
                </div>

                <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>

                <div>
                    <!--HEADER-->
                    <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
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
                        <div class="row " style="margin-bottom:10px !important;height:10mm">
                            <div class="col-md-12">
                                <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                    <span style="font-size:16px; padding-top:3px; font-weight:bolder">{{ $t('PurchaseReturn.PurchaseReturn') }}</span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div style="height:60mm;" v-else></div>

                    <div style="height:20mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div>
                                    <!--Row 1-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceDate}}</div>
                                            <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Inv.No:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.invoiceNo}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                        </div>
                                    </div>

                                    <!--Row 3-->
                                    <div class="row">

                                        <div class="col-md-12" style="display:flex;">
                                            <div style="width:13%;font-weight:bolder;text-align:right;color:black !important;">Supplier Name:</div>
                                            <div style="width:73%;text-align:center;font-weight:bold;color:black !important;">{{list.supplierName}}</div>
                                            <div style="width:12%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important">
                                                :اسم المورد
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="height:230mm;border:2px solid !important;">
                        <div class="row ">
                            <div class="col-md-12 ">
                                <table class="table">
                                    <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo ">اسم الصنف</th>
                                        <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else>اسم الصنف</th>
                                        <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                        <th class="text-center" style="width:6%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">سعرالوحدة</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الاجمالي </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الخصم </th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">%الضريية</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">قيمة الضريبة</th>
                                        <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">المجموع الإجمالي </th>
                                    </tr>
                                    <tr class="heading" style="font-size:16px;" v-else>
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.styleNo">Product Name</th>
                                        <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else>Product Name</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                        <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                        <th class="text-right" style="width:11%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                                    </tr>


                                    <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in listItemP4" v-bind:key="item.id">
                                        <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+73}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.productId==null? '': item.product.styleNumber}}</td>
                                        <td v-if="english=='true' && arabic=='false' && headerFooters.englishName" class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='false' && headerFooters.arabicName" class="text-left" style="border-top:0 !important; border-bottom:0 !important;padding-top:3px !important; padding-bottom:3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else-if="isOtherLang() && english=='true' && !headerFooters.arabicName && headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.productName}}</td>
                                        <td v-else-if="isOtherLang() && english=='true' && headerFooters.arabicName && !headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span></td>
                                        <td v-else style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:left !important;color:black !important;">{{item.productName}}</span> <span style="float:right !important;color:black !important;">{{item.productNameArabic}}</span> </td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{calulateLineTotalQty(item) }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">  {{item.total.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(0) }}%</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div style="height:41mm;margin-top:1mm;">
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-bordered tbl_padding">
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;">Page: 4 - 4</td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{ calulateTotalExclVAT.toFixed(3).slice(0,-1) }}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((calulateNetTotal - calulateDiscountAmount))   }}</td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><!--<barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>--></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black !important;">/ المبلخ الاجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(calulateNetTotal - calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter=='true'">
                        <div class="row">
                            <table class="table text-center">

                                <tr>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ReceivedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.ApprovedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('PurchaseInvoice.PreparedBy') }}::
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>

                    <!--Footer-->
                    <div class="col-md-12" style="height: 42mm;border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-3">
                                <u><b><span style="font-size:18px;color: black !important;font-weight:bold;">Terms & Conditions</span></b></u><br />
                                <span style="font-size:14px;color: black !important;" v-html="headerFooters.footerEn">

                                </span>
                            </div>

                            <div class="col-md-3 text-center mt-1">

                                <vue-qrcode v-bind:value="qrValue" style="width:140px;" />
                            </div>
                            <div class="col-md-3 pt-4">

                                <div style=" width:90%;border:1px solid;text-align:center;font-size:15px;font-weight:bold;">
                                    Bank Details &nbsp; تفاصيل البنك
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center;border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount1}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon1}}
                                    </div>
                                </div>
                                <div style="width:90%;display:flex;border:1px solid;">
                                    <div style="width:70%;text-align:center; border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankAccount2}}
                                    </div>
                                    <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                        {{headerFooters.bankIcon2}}
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 text-right">
                                <u><b><span style="font-size:20px;font-weight:bold;">البنود و الظروف</span></b></u><br />
                                <span class=" text-center" style="font-size:16px;color: black !important;" v-html="headerFooters.footerAr">

                                </span>
                            </div>

                        </div>
                    </div>

                    <div class="row" v-if="isHeaderFooter=='true'">
                        <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                        <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
                    </div>
                </div>

            </div>
        </div>

      
    </div>

</template>

<script>
    import moment from "moment";
    

    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        components: {
            
        },
        props: ['printDetails', 'headerFooter'],
        data: function () {
            
            return {
                itemTotal: 0,
                listItemP1: [],
                listItemP2: [],
                listItemP3: [],
                listItemP4: [],
                isMultiUnit: '',
                qrValue: '',
                list: {
                    invoiceNo: '',
                    invoiceDate: '',
                    dueDate: '',
                    companyName: '',
                    companyPhoneNo: '',
                    companyAddress: '',
                    discountAmount: '',
                    supplierName: '',
                    customerPhoneNo: '',
                    customerAddress: '',
                    paymentMethod: '',
                    paymentMethodNo: '',
                    invocieType: '',
                    totalQty: 0,
                    purchasePostItems:
                        [

                        ]
                },
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                render: 0,
                arabic: '',
                english: '',
                isHeaderFooter: '',
                invoicePrint: '',
                listItemP1Summary: {
                    calulateDiscountAmount: 0,
                    calulateTotalQty: 0,
                    calulateNetTotal: 0,
                    calulateTotalExclVAT: 0,
                    calulateTotalVAT: 0,
                },
                listItemP2Summary: {
                    calulateDiscountAmount: 0,
                    calulateTotalQty: 0,
                    calulateNetTotal: 0,
                    calulateTotalExclVAT: 0,
                    calulateTotalVAT: 0,
                },
                listItemP3Summary: {
                    calulateDiscountAmount: 0,
                    calulateTotalQty: 0,
                    calulateNetTotal: 0,
                    calulateTotalExclVAT: 0,
                    calulateTotalVAT: 0,
                },
                listItemP4Summary: {
                    calulateDiscountAmount: 0,
                    calulateTotalQty: 0,
                    calulateNetTotal: 0,
                    calulateTotalExclVAT: 0,
                    calulateTotalVAT: 0,
                },
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
            calulateDiscountAmount: function () {
                return this.list.purchasePostItems.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0)
            },
            calulateTotalQty: function () {
                return this.list.purchasePostItems.reduce(function (a, c) { return a + Number((c.quantity) || 0) }, 0)
            },
            calulateNetTotal: function () {
                return this.list.purchasePostItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0)
            },
            calulateTotalExclVAT: function () {
                return this.list.purchasePostItems.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0)
            },
            calulateTotalVAT: function () {
                
                return this.list.purchasePostItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0)
            }
        },
        
        methods: {
            GetTLVForValue: function (tagNumber, tagValue) {
                var tagBuf = Buffer.from([tagNumber], 'utf-8')
                var tagValueLenBuf = Buffer.from([tagValue.length], 'utf-8')
                var tagValueBuf = Buffer.from(tagValue, 'utf-8')
                var bufsArray = [tagBuf, tagValueLenBuf, tagValueBuf]
                return Buffer.concat(bufsArray)
            },
            calulateDiscountAmount1: function () {
                return this.list.purchasePostItems.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0)
            },
            calulateBundleAmount1: function () {
                return this.list.purchasePostItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
            },
            calulateNetTotalWithVAT: function () {
                var total = this.list.purchasePostItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0);
                var grandTotal = parseFloat(total) - (this.calulateDiscountAmount1() + this.calulateBundleAmount1())
                return (parseFloat(grandTotal).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            calulateTotalVATofInvoice: function () {
                var total = this.list.purchasePostItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0);
                return (parseFloat(total).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            calulateLineTotalQty: function (item) {
                return parseInt((parseInt(item.highQty) * parseInt(item.unitPerPack)) + parseInt(item.quantity))
            },
            printInvoice: function () {

                this.$htmlToPaper('purchaseInvoice');
            }
        },

        mounted: function () {
            var root = this;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.invoicePrint = localStorage.getItem('InvoicePrint');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.headerFooters = this.headerFooter;

            if (this.printDetails.purchasePostItems.length > 0) {
                this.isMultiUnit = localStorage.getItem('IsMultiUnit');
                this.list = this.printDetails;
                this.list.invoiceDate = moment().format('DD MMM YYYY');
                var sellerNameBuff = root.GetTLVForValue('1', this.headerFooters.company.nameEnglish);
                var vatRegistrationNoBuff = root.GetTLVForValue('2', this.headerFooters.company.vatRegistrationNo);
                var timeStampBuff = root.GetTLVForValue('3', this.list.invoiceDate);
                var totalWithVat = root.GetTLVForValue('4', this.calulateNetTotalWithVAT());
                var totalVat = root.GetTLVForValue('5', this.calulateTotalVATofInvoice());
                var tagArray = [sellerNameBuff, vatRegistrationNoBuff, timeStampBuff, totalWithVat, totalVat];
                var qrCodeBuff = Buffer.concat(tagArray);
                root.qrValue = qrCodeBuff.toString('base64');

                var totalItem = this.printDetails.purchasePostItems.length;

                this.itemTotal = totalItem;

                if (totalItem <= 24) {
                    for (var i = 0; i < totalItem; i++) {
                        root.listItemP1.push(root.printDetails.purchasePostItems[i]);
                    }
                }
                else if (totalItem > 24 && totalItem <= 48) {
                    for (var k = 0; k < totalItem; k++) {
                        if (k <= 23) {
                            root.listItemP1.push(root.printDetails.purchasePostItems[k]);
                        }
                        else {
                            root.listItemP2.push(root.printDetails.purchasePostItems[k]);
                        }
                    }
                }
                else if (totalItem > 48 && totalItem <= 72) {
                    for (var j = 0; j < totalItem; j++) {
                        if (j <= 23) {
                            root.listItemP1.push(root.printDetails.purchasePostItems[j]);
                        }
                        else if (j > 23 && j <= 47) {
                            root.listItemP2.push(root.printDetails.purchasePostItems[j]);
                        }
                        else {
                            root.listItemP3.push(root.printDetails.purchasePostItems[j]);
                        }
                    }
                }

                else if (totalItem > 72 && totalItem <= 96) {
                    for (var l = 0; l < totalItem; l++) {
                        if (l <= 23) {
                            root.listItemP1.push(root.printDetails.purchasePostItems[l]);
                        }
                        else if (l > 23 && l <= 47) {
                            root.listItemP2.push(root.printDetails.purchasePostItems[l]);
                        }
                        else if (l > 47 && l <= 71) {
                            root.listItemP3.push(root.printDetails.purchasePostItems[l]);
                        }

                        else {
                            root.listItemP4.push(root.printDetails.purchasePostItems[l]);
                        }
                    }
                }

                /*summary calculate listItemP1Summary*/
                root.listItemP1Summary.calulateDiscountAmount = this.listItemP1.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0)

                root.listItemP1Summary.calulateTotalQty = this.listItemP1.reduce(function (a, c) { return a + Number((c.quantity) || 0) }, 0)

                root.listItemP1Summary.calulateNetTotal = this.listItemP1.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0)

                root.listItemP1Summary.calulateTotalExclVAT = this.listItemP1.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0)

                root.listItemP1Summary.calulateTotalVAT = this.listItemP1.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0)


                /*summary calculate listItemP2Summary*/
                root.listItemP2Summary.calulateDiscountAmount = this.listItemP2.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0)

                root.listItemP2Summary.calulateTotalQty = this.listItemP2.reduce(function (a, c) { return a + Number((c.quantity) || 0) }, 0)

                root.listItemP2Summary.calulateNetTotal = this.listItemP2.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0)

                root.listItemP2Summary.calulateTotalExclVAT = this.listItemP2.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0)

                root.listItemP2Summary.calulateTotalVAT = this.listItemP2.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0)

                /*summary calculate listItemP3Summary*/
                root.listItemP3Summary.calulateDiscountAmount = this.listItemP3.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0)

                root.listItemP3Summary.calulateTotalQty = this.listItemP3.reduce(function (a, c) { return a + Number((c.quantity) || 0) }, 0)

                root.listItemP3Summary.calulateNetTotal = this.listItemP3.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0)

                root.listItemP3Summary.calulateTotalExclVAT = this.listItemP3.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0)

                root.listItemP3Summary.calulateTotalVAT = this.listItemP3.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0)



                /*summary calculate listItemP4Summary*/
                root.listItemP4Summary.calulateDiscountAmount = this.listItemP4.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0)

                root.listItemP4Summary.calulateTotalQty = this.listItemP4.reduce(function (a, c) { return a + Number((c.quantity) || 0) }, 0)

                root.listItemP4Summary.calulateNetTotal = this.listItemP4.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0)

                root.listItemP4Summary.calulateTotalExclVAT = this.listItemP4.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0)

                root.listItemP4Summary.calulateTotalVAT = this.listItemP4.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0)


                setTimeout(function () {
                    root.printInvoice();
                }, 125)
            }
        },
    }
</script>

