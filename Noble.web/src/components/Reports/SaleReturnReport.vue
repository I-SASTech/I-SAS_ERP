<template>
    <div>
        <div ref="mychildcomponent" hidden id='purchaseInvoice' class="col-md-12" style="color:black !important">
            <!--HEADER-->
            <div class="col-md-12 mb-1 " style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
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
                        <div style="margin: 0px; padding: 0px; font-weight:bold;display:flex">
                            <div style="width: 40%;text-align:left">
                                <span style="font-size:20px;color:black;">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Sale Return' : 'عودة البيع' }} </span>
                            </div>
                            <div style="width: 50%; text-align: left; padding-bottom: 5px;" v-if="b2b && b2c">
                                <div v-if="list.customerCategory=='B2B – Business to Business'">
                                    <span style="font-size:24px;color:black;">{{taxInvoiceLabel}} </span>
                                    <span style="font-size:24px;color:black;">{{taxInvoiceLabelAr}} </span>
                                </div>
                                <div v-else-if="list.customerCategory=='B2C – Business to Client'">
                                    <span style="font-size:24px;color:black;">{{simplifyTaxInvoiceLabel}} </span>
                                    <span style="font-size:24px;color:black;">{{simplifyTaxInvoiceLabelAr}} </span>
                                </div>

                                <div v-else>
                                    <span style="font-size:24px;color:black;">{{simplifyTaxInvoiceLabel}} </span>
                                    <span style="font-size:24px;color:black;">{{simplifyTaxInvoiceLabelAr}} </span>
                                </div>
                            </div>

                            <div style="width: 50%; text-align: left; margin-bottom: 15px;" v-else-if="b2b">
                                <span style="font-size:24px;color:black;">{{taxInvoiceLabel}} </span>
                                <span style="font-size:24px;color:black;">{{taxInvoiceLabelAr}} </span>
                            </div>

                            <div style="width: 50%; text-align: left; margin-bottom: 15px;" v-else-if="b2c">
                                <span style="font-size:24px;color:black;" v-if="b2c">{{simplifyTaxInvoiceLabel}} </span>
                                <span style="font-size:24px;color:black;" v-if="b2c">{{simplifyTaxInvoiceLabelAr}} </span>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div style="height:60mm;" v-else></div>

            <div style="height:40mm;margin-top:1mm; border:2px solid #000000;">
                <div class="row">
                    <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                        <div>
                            <!--Row 1-->
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Store:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{($i18n.locale == 'en' ||isLeftToRight())?list.wareHouseName:(list.wareHouseNameAr==''?list.wareHouseName:list.wareHouseNameAr)}}</div>
                                    <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:المستودع </div>
                                </div>
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">{{ $t('SaleReturnPrint.InvoiceNo') }}:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.registrationNo}}</div>
                                    <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                </div>
                            </div>

                            <!--Row 2-->
                            <div class="row">
                                <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                    <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.cargoName">Date:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-if="list.cashCustomer != null">{{list.date}}</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-if="list.creditCustomer != null">{{list.date}}</div>
                                    <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                </div>

                                <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.cargoName">Cargo:</div>
                                    <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.cargoName ">{{($i18n.locale == 'en' ||isLeftToRight())?list.logisticNameEn:(list.logisticNameAr==''?list.logisticNameEn:list.logisticNameAr)}}</div>
                                    <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                        :المخرج
                                    </div>
                                </div>
                            </div>

                            <!--Row 3-->
                            <div class="row">
                                
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Invoice Type:</div>
                                    <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="list.cashCustomer != null">{{($i18n.locale == 'en' ||isLeftToRight())?'Cash':'السيولة النقدية'}}</div>
                                    <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="list.creditCustomer != null">{{($i18n.locale == 'en' ||isLeftToRight())?'Credit': 'تنسب إليه' }}</div>
                                    <div style="float:right; width:22%;font-weight:bolder;color:black !important;font-size:15px !important">:نوع فاتورة</div>
                                </div>
                                <div class="col-md-6" style="display:flex;" v-if="headerFooters.customerNumber">
                                    <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerNumber">Cust No:</div>
                                    <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerNumber">{{list.customerCode}}</div>
                                    <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="headerFooters.customerNumber">
                                        :رقم العميل
                                    </div>
                                </div>
                            </div>


                            <!--Row 4-->
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerTelephone">Tel:</div>
                                    <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="list.cashCustomer != null && headerFooters.customerTelephone">{{list.customerTelephone}}</div>
                                    <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="list.creditCustomer != null && headerFooters.customerTelephone">{{list.customerTelephone}}</div>
                                    <div style="width:22%; font-weight:bolder;font-size:15px !important;color:black !important;" v-if="headerFooters.customerTelephone"><span style="">:هاتف</span> </div>
                                </div>

                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important; "><span>Customer Name</span>:</div>
                                    <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="list.cashCustomer != null">{{ ($i18n.locale == 'en' ||isLeftToRight())?'Walk-In':'ادخل' }}</div>
                                    <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="list.creditCustomer != null">{{($i18n.locale == 'en' ||isLeftToRight())?list.customerNameEn:(list.customerNameAr==''?list.customerNameEn:list.customerNameAr)}}</div>
                                    <div style="width:22%; font-weight:bolder;color:black !important; padding-right:6px;font-size:15px !important">: اسم العميل  </div>
                                </div>
                            </div>


                            <!--Row 5-->
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; color:black !important;font-weight:bolder;text-align:right;" v-if="headerFooters.customerVat">Cust VAT:</div>
                                    <div style="width:50%;color:black !important;text-align:center;font-weight:bold;" v-if="headerFooters.customerVat">{{list.customerVat}}</div>
                                    <div style="float:right; width:22%; color:black !important; font-weight:bolder;font-size:15px !important" v-if="headerFooters.customerVat">
                                        : رقم الضريبة للعميل
                                    </div>
                                </div>

                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right; color:black !important;" v-if="headerFooters.customerAddress"><span>Address</span>:</div>
                                    <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                    <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddress}}</div>
                                    <div style="width:22%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                                    <!--<div style="float:left;  width:4%;" v-if="headerFooters.customerAddress"></div>-->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!--INFORMATION-->
            <div style="height:200mm;border:2px solid !important;">
                <div class="row mr-2 ml-2">
                    <div class="col-md-12 ">
                        <table class="table table-striped table-hover table_list_bg">
                            <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                <th class="text-center" style="width:9%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                <th class="text-right" style="width:42%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="!headerFooters.styleNo && headerFooters.itemPieceSize">اسم الصنف</th>
                                <th class="text-right" style="width:42%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else-if="headerFooters.styleNo && !headerFooters.itemPieceSize">اسم الصنف</th>
                                <th class="text-right" style="width:42%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else>اسم الصنف</th>
                                <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>
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
                                <th class="text-center" style="width:9%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>
                                <th class="text-left" style="width:42%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="!headerFooters.styleNo && headerFooters.itemPieceSize">Product Name</th>
                                <th class="text-left" style="width:42%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else-if="headerFooters.styleNo && !headerFooters.itemPieceSize">Product Name</th>
                                <th class="text-left" style="width:42%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else>Product Name</th>
                                <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.itemPieceSize">Package size</th>
                                <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">Total Qty </th>
                                <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                            </tr>


                            <tr style="font-size:16px;font-weight:bold;" v-for="(item, index) in list.saleItems" v-bind:key="item.id">
                                <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.product.styleNumber}}</td>
                                <td v-if="english=='true' && arabic=='false' && headerFooters.englishName" class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.productName}}</td>
                                <td v-else-if="isOtherLang() && english=='false' && headerFooters.arabicName" class="text-left" style="border-top:0 !important; border-bottom:0 !important;padding-top:3px !important; padding-bottom:3px !important;"><span style="float:right !important;color:black !important;">{{item.arabicName}}</span></td>
                                <td v-else-if="isOtherLang() && english=='true' && !headerFooters.arabicName && headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.productName}}</td>
                                <td v-else-if="isOtherLang() && english=='true' && headerFooters.arabicName && !headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:right !important;color:black !important;">{{item.arabicName}}</span></td>
                                <td v-else style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:left !important;color:black !important;">{{item.productName}}</span> <span style="float:right !important;color:black !important;">{{item.arabicName}}</span> </td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.itemPieceSize">{{item.product.width}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit == 'true'">-({{item.highQty }} - {{item.newQuantity}})</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>-{{item.quantity }}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">-{{item.quantity }}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">-{{item.total.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;"><span style="color:black !important; font-weight:bold"></span>{{item.discount.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;"><span style="color:black !important; font-weight:bold"></span>{{parseFloat(item.taxRate).toFixed(0) }}%</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;"><span style="color:black !important;width:30px; font-weight:bold"></span>{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;"><span style="color:black !important;width:30px; font-weight:bold"></span>{{(((item.total - (item.discountAmount+item.bundleAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
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
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;"></td>
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">{{ $t('AllReports.TotalQuantity') }}: -{{ calulateTotalQty }}</td>
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount:<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;"><span style="font-size:15px;color:black !important;width:30px; font-weight:bold"> - </span>{{parseFloat(calulateTotalExclVAT - calulateTotalInclusiveVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)))   }}</td>
                                <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode></td>
                                <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount:<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"><span style="font-size:15px;color:black !important;width:30px; font-weight:bold"> - </span>{{parseFloat(calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{ $t('AllReports.BundleAmount') }}<span style="font-weight:bold;color:black !important;">/مبلغ الحزمة</span></td>
                                <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"><span style="font-size:15px;color:black !important;width:30px; font-weight:bold"> - </span>{{parseFloat(calulateBundleAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{ $t('AllReports.VATAmount') }}<span style="font-weight:bold;color:black !important;">/لريال الضريية</span></td>
                                <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"><span style="font-size:15px;color:black !important;width:30px; font-weight:bold"> - </span>{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Toal<span style="font-weight:bold;color:black !important;">/المبلخ الاجمالي</span></td>
                                <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"><span style="font-size:15px;color:black !important;width:30px; font-weight:bold"> - </span> {{parseFloat(calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                        </table>
                    </div>
                    <!--<div class="col-md-8 p-0">
                        <div class="col-md-12" style="height:10mm;">
                            <div class="row">
                                <div class="col-md-6 border-dark border-left border-bottom" style="height:10mm;"></div>
                                <div class="col-md-6 border-dark border-left border-bottom" style="height:10mm;font-size:12px;"> {{ $t('AllReports.TotalQuantity') }}: {{ calulateTotalQty }}</div>
                            </div>
                        </div>
                        <div class="col-md-12 py-3 border-dark  border-bottom border-left border-right" style="height:20mm;font-size:12px;">{{ (calulateNetTotal - list.discountAmount) | toWords }}</div>
                        <div class="col-md-12" style="height:10mm;">
                            <div class="row">
                                <div class="col-md-7 border-dark border-right border-left  border-bottom" style="height:10mm;">

                                </div>
                                <div class="col-md-5 border-dark border-right border-bottom " style="height:10mm;">

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 p-0">
                        <div class="col-md-12" style="height:10mm;">
                            <div class="row">
                                <div class="col-md-8 border-dark border-bottom" style="height:10mm;font-size:12px;"> {{ $t('AllReports.TotalPriceExclVAT') }}:</div>
                                <div class="col-md-4 border border-dark" style="height:10mm;font-size:12px;"> {{ (calulateTotalExclVAT - calulateTotalInclusiveVAT).toFixed(3).slice(0,-1) }}</div>
                            </div>
                        </div>
                        <div class="col-md-12" style="height:10mm;">
                            <div class="row">
                                <div class="col-md-8 border-dark  border-bottom" style="height:10mm;font-size:12px;"> {{ $t('AllReports.DiscAmount') }}: </div>
                                <div class="col-md-4 border-dark  border-bottom  border-right border-left " style="height:10mm;font-size:12px;"> {{ calulateDiscountAmount }}</div>
                            </div>
                        </div>
                        <div class="col-md-12" style="height:10mm;">
                            <div class="row">
                                <div class="col-md-8 border-dark  border-bottom" style="height:10mm;font-size:12px;"> {{ $t('AllReports.BundleAmount') }}: </div>
                                <div class="col-md-4 border-dark  border-bottom  border-right border-left " style="height:10mm;font-size:12px;"> {{ calulateBundleAmount }}</div>
                            </div>
                        </div>
                        <div class="col-md-12" style="height:10mm;">
                            <div class="row">
                                <div class="col-md-8 border-dark  border-bottom" style="height:10mm;font-size:12px;"> {{ $t('AllReports.VATAmount') }}:</div>
                                <div class="col-md-4 border-dark  border-bottom  border-right border-left " style="height:10mm;font-size:12px;">  {{ calulateTotalVAT.toFixed(3).slice(0,-1) }}</div>
                            </div>
                        </div>
                        <div class="col-md-12" style="height:10mm;">
                            <div class="row">
                                <div class="col-md-8 border-dark" style="height:10mm;font-size:12px;">{{ $t('AllReports.NetTotalwithVAT') }}:</div>
                                <div class="col-md-4 border-dark border-right border-left " style="height:10mm;font-size:12px;">  {{ (calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)) }}</div>
                            </div>
                        </div>
                    </div>-->
                </div>
            </div>
            <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter=='true'">
                <div class="row">
                    <table class="table text-center">
                        <tr>
                            <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                {{ $t('SaleReturnPrint.ReceivedBy') }}::
                            </td>
                            <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                {{ $t('SaleReturnPrint.ApprovedBy') }}:
                            </td>
                            <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                {{ $t('SaleReturnPrint.PreparedBy') }}:
                            </td>
                        </tr>

                    </table>
                </div>
            </div>
            <!--<div class="row" v-if="isHeaderFooter=='true'">
                <div class="col-md-6">{{headerFooters.company.addressEnglish}}</div>
                <div class="col-md-6 text-right">{{headerFooters.company.addressArabic}}</div>
            </div>-->
            <!--Footer-->
            <div class="col-md-12 " style="height: 42mm;border:2px solid #000000;">
                <div class="row">
                    <div class="col-md-3">
                        <u><b><span style="font-size:18px;color: black !important;font-weight:bold;">Terms & Conditions</span></b></u><br />
                        <span style="font-size:14px;color: black !important;font-weight:bold;" v-html="headerFooters.footerEn">
                            
                        </span>
                    </div>

                    <div class="col-md-3 text-center mt-1">
                        <vue-qrcode v-bind:value="qrValue" style="width:140px;"  />
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
                        <span class=" text-center" style="font-size:16px;color: black !important;font-weight:bold;" v-html="headerFooters.footerAr">
                            
                        </span>
                    </div>

                </div>
            </div>

            <!--<div style="height: 40mm;" v-else></div>-->
            <div class="row" v-if="isHeaderFooter=='true'">
                <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
            </div>
        </div>
    </div>
</template>

<script>
    import moment from "moment";
    import axios from 'axios'
    
    import Vue3Barcode from 'vue3-barcode'
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
        windowTitle: window.document.title,

    }
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['printDetails', 'headerFooter','isTouchScreen'],
        components: {
            
            'barcode': Vue3Barcode,
        },
        data: function () {
            return {
                b2b: false,
                b2c: false,
                taxInvoiceLabel: "",
                taxInvoiceLabelAr: '',
                simplifyTaxInvoiceLabel: '',
                simplifyTaxInvoiceLabelAr: '',

                isMultiUnit: '',
                qrValue: "",
                isHeaderFooter: '',
                invoicePrint: '',

                arabic: '',
                english: '',
                list: {
                    number: 0,
                    listItemTotal: [],
                    registrationNo: '',
                    date: '',
                    dueDate: '',
                    companyName: '',
                    companyPhoneNo: '',
                    companyAddress: '',
                    discountAmount: '',
                    cashCustomer: '',
                    creditCustomer: '',
                    customerPhoneNo: '',
                    customerAddress: '',
                    paymentMethod: '',
                    paymentMethodNo: '',
                    invocieType: '',
                    saleItems:
                        [

                        ]
                },
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
        filters: {
            toWords: function (value) {
                var converter = require('number-to-words');
                if (!value) return ''
                return converter.toWords(value);
            }
        },
        computed: {
            calulateTotalQty: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.quantity) || 0) }, 0)
            },
            calulateNetTotal: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0)
            },
            calulateTotalExclVAT: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0)
            },
            calulateTotalVAT: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0)
            },
            calulateTotalInclusiveVAT: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.inclusiveVat) || 0) }, 0)
            },
            calulateDiscountAmount: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0)
            },
            calulateBundleAmount: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
            }
        },
        methods: {
            calulateNetTotalWithVAT: function () {
                var total = this.list.saleItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0);
                return (parseFloat(total).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            calulateTotalVATofInvoice: function () {
                var total = this.list.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0);
                return (parseFloat(total).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },

            printInvoice: function () {
                var root = this;
                this.$htmlToPaper('purchaseInvoice', options, () => {
                    if (root.isTouchScreen === 'saleReturn') {
                        root.$router.go('/TouchInvoice')
                    }
                   
                   
                    else {

                        console.log('No Rander the Page');
                    }

                });
            },
            printBlindInvoice: function () {
                var root = this;
                // this.$htmlToPaper('purchaseInvoice');
                this.htmlData.htmlString = this.$refs.mychildcomponent.innerHTML;
                //  var html1 = this.$refs.mychildcomponent.innerHTML;
                //  var data = { html: html1 }
                //
                var printerName = localStorage.getItem('PrinterName')
                var form = new FormData();
                form.append('htmlString', this.htmlData.htmlString);
                form.append('NoOfPagesPrint', 0);
                form.append('PrintType', 'invoice');
                form.append('PrinterName', printerName);
                //this.$htmlToPaper('purchaseInvoice');
                //axios.post('http://localhos:7013/print/from-pdf', form);
                //axios.post('http://127.0.0.1:7013/print/from-pdf', form);
                //alert();
                //var root = this;



                if (this.$ServerIp.includes('localhost')) {
                    axios.post('http://127.0.0.1:7014/print/from-pdf', form).then(function (x) {
                        console.log(x);

                    });
                    //if (root.isTouchScreen === true) {
                    //    root.$router.go('/TouchInvoice')
                    //}
                }
                else {
                    this.$htmlToPaper('purchaseInvoice', options, () => {
                        if (root.isTouchScreen === true) {
                            root.$router.go('/')
                        }
                    });

                }


                //var token = '';
                //if (token == '') {
                //    token = localStorage.getItem('token');
                //}
                //root.loading = true;
                //root.$https.post('/EmployeeRegistration/PrintPos', data, { headers: { "Authorization": `Bearer ${token}` } }).then(function (x) {
                //    alert(x.data)
                //});



            },
            GetTLVForValue: function (tagNumber, tagValue) {
                var tagBuf = Buffer.from([tagNumber], 'utf-8')
                var tagValueLenBuf = Buffer.from([tagValue.length], 'utf-8')
                var tagValueBuf = Buffer.from(tagValue, 'utf-8')
                var bufsArray = [tagBuf, tagValueLenBuf, tagValueBuf]
                return Buffer.concat(bufsArray)
            }

        },
        mounted: function () {
            this.b2b = localStorage.getItem('b2b') == 'true' ? true : false;
            this.b2c = localStorage.getItem('b2c') == 'true' ? true : false;
            this.taxInvoiceLabel = localStorage.getItem('taxInvoiceLabel');
            this.taxInvoiceLabelAr = localStorage.getItem('taxInvoiceLabelAr');
            this.simplifyTaxInvoiceLabel = localStorage.getItem('simplifyTaxInvoiceLabel');
            this.simplifyTaxInvoiceLabelAr = localStorage.getItem('simplifyTaxInvoiceLabelAr');

            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.invoicePrint = localStorage.getItem('InvoicePrint');

            var root = this;
            if (this.printDetails.saleItems.length > 0) {
                this.list = this.printDetails;
                this.headerFooters = this.headerFooter;
                
                if (this.headerFooters.blindPrint) {
                    this.printBlindInvoice();
                }
                else {
                    var sellerNameBuff = root.GetTLVForValue('1', this.headerFooters.company.nameEnglish)
                    var vatRegistrationNoBuff = root.GetTLVForValue('2', this.headerFooters.company.vatRegistrationNo)
                    var timeStampBuff = root.GetTLVForValue('3', this.list.date)
                    var totalWithVat = root.GetTLVForValue('4', this.calulateNetTotalWithVAT())
                    var totalVat = root.GetTLVForValue('5', this.calulateTotalVATofInvoice())
                    var tagArray = [sellerNameBuff, vatRegistrationNoBuff, timeStampBuff, totalWithVat, totalVat]
                    var qrCodeBuff = Buffer.concat(tagArray)
                    root.qrValue = qrCodeBuff.toString('base64')
                    this.listItemTotal = 15 - this.printDetails.saleItems.length;
                    if (this.listItemTotal < 0) {
                        this.listItemTotal = 0;
                    }
                    this.list.date = moment().format('DD MMM YYYY');
                    setTimeout(function () {
                        root.printInvoice();
                    }, 125)
                }

            }
        },

    }
</script>
