<template>
    <div>
        <div ref="mychildcomponent" hidden id='purchaseInvoice' class="col-md-12" style="color:black !important">
            <!--page1-->
            <div v-if="itemTotal<=12">
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
                            <div style="margin: 0px; padding: 0px; font-weight:bold;display:flex">
                                <div style="width: 40%;text-align:left">
                                    <span style="font-size:20px;color:black;" v-if="list.invoiceType==0">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Hold' : 'معلق' }} </span>
                                    <span style="font-size:20px;color:black;" v-if="list.invoiceType==1">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Paid' : 'مدفوع' }} </span>
                                    <span style="font-size:20px;color:black;" v-if="list.invoiceType==2">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Credit' : 'آجـل' }} </span>
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

                <div style="height:45mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                            <div>
                                <!--Row 1-->
                                <div class="row">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Store:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{invoicePrint == 'العربية'?list.wareHouseNameAr:list.wareHouseName}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:المستودع </div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">{{ $t('SaleInvoice.InvoiceNo') }}:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.registrationNo}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                    </div>
                                </div>
                                <!--Row 2-->
                                <div class="row">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-if="list.cashCustomer != null">{{list.date}}</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-else>{{list.date}}</div>
                                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.cargoName">Cargo:</div>
                                        <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.cargoName ">{{invoicePrint == 'العربية'?list.logisticNameAr : list.logisticNameEn}}</div>
                                        <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                            :المخرج
                                        </div>
                                    </div>
                                </div>
                                <!--Row 3-->
                                <div class="row">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Invoice Type:</div>
                                        <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="!list.isCredit">{{invoicePrint == 'العربية'? 'نقدي' : 'Cash'}}</div>
                                        <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-else>{{invoicePrint == 'العربية'? 'آجل' : 'Credit' }}</div>
                                        <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">:نوع فاتورة</div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;" v-if="headerFooters.customerNumber">
                                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerNumber">Cust No:</div>
                                        <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerNumber"><span v-if="list.cashCustomer != null">{{list.code}}</span> <span v-else>{{list.customerCode}}</span> </div>
                                        <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="headerFooters.customerNumber">
                                            :رقم العميل
                                        </div>
                                    </div>
                                </div>
                                <!--Row 4-->
                                <div class="row">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerTelephone">Tel:</div>
                                        <div style="width:50%;font-weight:bold;color:black !important;text-align:center;" v-if="list.cashCustomer != null && headerFooters.customerTelephone">{{list.mobile}}</div>
                                        <div style="width:50%;font-weight:bold;color:black !important; text-align:center;" v-if="list.creditCustomer != null && headerFooters.customerTelephone">{{list.customerTelephone}}</div>
                                        <div style="width:22%; font-weight:bolder;font-size:15px !important;color:black !important;" v-if="headerFooters.customerTelephone"><span style="">:هاتف</span> </div>
                                    </div>

                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important; "><span>Customer Name</span>:</div>
                                        <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="list.cashCustomer != null">{{list.cashCustomer}}</div>
                                        <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-else-if="headerFooters.customerNameAr && headerFooters.customerNameEn">{{list.customerNameEn=='' || list.customerNameEn==null? list.customerNameAr : list.customerNameEn}}</div>
                                        <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-else-if="headerFooters.customerNameAr">{{list.customerNameAr=='' || list.customerNameAr==null? list.customerNameEn : list.customerNameAr}}</div>
                                        <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-else>{{list.customerNameEn=='' || list.customerNameEn==null? list.customerNameAr : list.customerNameEn}}</div>
                                        <div style="width:22%; font-weight:bolder;color:black !important; padding-right:6px;font-size:15px !important">: اسم العميل  </div>
                                    </div>
                                </div>

                                <!--Row 5-->
                                <div class="row">
                                    <div class="col-md-8" style="display:flex;" v-if="headerFooters.customerVat">
                                        <div style="width:20%; color:black !important;font-weight:bolder;text-align:right;">Cust VAT:</div>
                                        <div style="width:37%;color:black !important;text-align:center;font-weight:bold;" v-if="headerFooters.customerVat"><span v-if="list.cashCustomer != null">{{list.cashCustomerId}}</span> <span v-else>{{list.customerVat}}</span></div>
                                        <div style="width:43%; color:black !important; font-weight:bolder;text-align:left;font-size:15px !important" v-if="headerFooters.customerVat">
                                            : رقم الضريبة للعميل
                                        </div>
                                    </div>

                                    <!--<div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right; color:black !important;" v-if="headerFooters.customerAddress"><span>Address</span>:</div>
                                        <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                        <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                        <div style="width:22%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                                    </div>-->
                                </div>
                                <!--Row 6-->
                                <div class="row">
                                    <div class="col-md-12" style="display:flex;" v-if="headerFooters.customerAddress">
                                        <div style="width:13%; font-weight:bolder;text-align:right; color:black !important;"><span>Address</span>:</div>
                                        <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                        <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                        <div style="width:11%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div style="height:205mm;border:2px solid !important;">
                    <div class="row ">
                        <div class="col-md-12 ">
                            <table class="table">
                                <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                    <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>-->
                                    <th class="text-right" style="width:54%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">وصف </th>
                                    <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                    <th class="text-center" style="width:6%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                    <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                    <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">سعرالوحدة</th>
                                    <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الاجمالي </th>
                                    <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;"  v-if="!list.isDiscountOnTransaction">الخصم </th>
                                    <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">%الضريية</th>
                                    <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">المجموع الإجمالي </th>
                                </tr>
                                <tr class="heading" style="font-size:16px;" v-else>
                                    <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>-->
                                    <th class="text-left" style="width:54%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Description</th>
                                    <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.itemPieceSize">Package size</th>-->
                                    <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                    <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                    <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                    <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                    <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;"  v-if="!list.isDiscountOnTransaction">Discount</th>
                                    <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                    <th class="text-right" style="width:11%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                                </tr>


                                <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in list.saleItems" v-bind:key="item.id">
                                    <td class="text-left pl-1 pr-1" style="font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important; border-top: 0 !important; border-bottom: 0 !important; color: black !important;">{{index+1}}</td>
                                    <!--<td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.product.styleNumber}}</td>-->
                                    <td class="text-left pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;">{{item.description}}  <span style="color:black !important;" v-if="item.isFree">(Free)</span></td>

                                    <!--<td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.itemPieceSize">{{item.product.width}}</td>-->
                                    <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.newQuantity }}</td>
                                    <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                    <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.quantity }}</td>
                                    <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                    <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.total.toFixed(3).slice(0,-1)}}</td>
                                    <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;"  v-if="!list.isDiscountOnTransaction">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                    <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(3).slice(0,-1) }}</td>
                                    <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.isFree?'-':(((item.total - (item.discountAmount+item.bundleAmount)) )).toFixed(3).slice(0,-1)}}</td>
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
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ calulateTotalQty }} </td>
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ calulateTotalQty }} </td>
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{parseFloat(calulateTotalExclVAT ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)))   }}</td>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode></td>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Adjustment<span style="font-weight:bold;color:black !important;">/ تعديل</span></td>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(list.discount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black ! important;">/ المبلخ الاجمالي</span></td>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(calulateNetTotal +  list.discount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
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
                                    {{ $t('SaleInvoice.ReceivedBy') }}::
                                </td>
                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                    {{ $t('SaleInvoice.ApprovedBy') }}::
                                </td>
                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                    {{ $t('SaleInvoice.PreparedBy') }}::
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
            <div v-else-if="itemTotal>12 && itemTotal<=24">
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
                                <div style="margin: 0px; padding: 0px; font-weight:bold;display:flex">
                                    <div style="width: 40%;text-align:left">
                                        <span style="font-size:20px;color:black;" v-if="list.invoiceType==0">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Hold' : 'معلق' }} </span>
                                        <span style="font-size:20px;color:black;" v-if="list.invoiceType==1">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Paid' : 'مدفوع' }} </span>
                                        <span style="font-size:20px;color:black;" v-if="list.invoiceType==2">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Credit' : 'آجـل' }} </span>
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

                    <div style="height:45mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div>
                                    <!--Row 1-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Store:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{invoicePrint == 'العربية'?list.wareHouseNameAr:list.wareHouseName}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:المستودع </div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">{{ $t('SaleInvoice.InvoiceNo') }}:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.registrationNo}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                        </div>
                                    </div>
                                    <!--Row 2-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-if="list.cashCustomer != null">{{list.date}}</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-else>{{list.date}}</div>
                                            <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.cargoName">Cargo:</div>
                                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.cargoName ">{{invoicePrint == 'العربية'?list.logisticNameAr : list.logisticNameEn}}</div>
                                            <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                                :المخرج
                                            </div>
                                        </div>
                                    </div>
                                    <!--Row 3-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Invoice Type:</div>
                                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="!list.isCredit">{{invoicePrint == 'العربية'? 'نقدي' : 'Cash'}}</div>
                                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-else>{{invoicePrint == 'العربية'? 'آجل' : 'Credit' }}</div>
                                            <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">:نوع فاتورة</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;" v-if="headerFooters.customerNumber">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerNumber">Cust No:</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerNumber"><span v-if="list.cashCustomer != null">{{list.code}}</span> <span v-else>{{list.customerCode}}</span> </div>
                                            <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="headerFooters.customerNumber">
                                                :رقم العميل
                                            </div>
                                        </div>
                                    </div>
                                    <!--Row 4-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerTelephone">Tel:</div>
                                            <div style="width:50%;font-weight:bold;color:black !important;text-align:center;" v-if="list.cashCustomer != null && headerFooters.customerTelephone">{{list.mobile}}</div>
                                            <div style="width:50%;font-weight:bold;color:black !important; text-align:center;" v-if="list.creditCustomer != null && headerFooters.customerTelephone">{{list.customerTelephone}}</div>
                                            <div style="width:22%; font-weight:bolder;font-size:15px !important;color:black !important;" v-if="headerFooters.customerTelephone"><span style="">:هاتف</span> </div>
                                        </div>

                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important; "><span>Customer Name</span>:</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="list.cashCustomer != null">{{list.cashCustomer}}</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-else>{{invoicePrint == 'العربية'? list.customerNameAr=='' || list.customerNameAr==null? list.customerNameEn : list.customerNameAr    : list.customerNameEn=='' || list.customerNameEn==null?list.customerNameAr : list.customerNameEn}}</div>
                                            <div style="width:22%; font-weight:bolder;color:black !important; padding-right:6px;font-size:15px !important">: اسم العميل  </div>
                                        </div>
                                    </div>

                                    <!--Row 5-->
                                    <div class="row">
                                        <div class="col-md-8" style="display:flex;">
                                            <div style="width:20%; color:black !important;font-weight:bolder;text-align:right;" v-if="headerFooters.customerVat">Cust VAT:</div>
                                            <div style="width:37%;color:black !important;text-align:center;font-weight:bold;" v-if="headerFooters.customerVat"><span v-if="list.cashCustomer != null">{{list.cashCustomerId}}</span> <span v-else>{{list.customerVat}}</span></div>
                                            <div style="width:43%; color:black !important; font-weight:bolder;text-align:left;font-size:15px !important" v-if="headerFooters.customerVat">
                                                : رقم الضريبة للعميل
                                            </div>
                                        </div>

                                        <!--<div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right; color:black !important;" v-if="headerFooters.customerAddress"><span>Address</span>:</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                            <div style="width:22%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                                        </div>-->
                                    </div>
                                    <!--Row 6-->
                                    <div class="row">
                                        <div class="col-md-12" style="display:flex;">
                                            <div style="width:13%; font-weight:bolder;text-align:right; color:black !important;" v-if="headerFooters.customerAddress"><span>Address</span>:</div>
                                            <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                            <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                            <div style="width:11%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="height:205mm;border:2px solid !important;">
                        <div class="row ">
                            <div class="col-md-12 ">
                                <table class="table">
                                    <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>-->
                                        <th class="text-right" style="width:54%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">وصف </th>
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
                                        <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>-->
                                        <th class="text-left" style="width:54%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Description</th>
                                        <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.itemPieceSize">Package size</th>-->
                                        <th class="text-center" style="width:6%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Qty </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">Total Qty </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                        <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                                    </tr>


                                    <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in listItemP1" v-bind:key="item.id">
                                        <td class="text-left pl-1 pr-1" style="font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important; border-top: 0 !important; border-bottom: 0 !important; color: black !important;">{{index+1}}</td>
                                        <!--<td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.product.styleNumber}}</td>-->
                                    <td class="text-left pl-1 pr-1" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.description}}  <span style="color:black !important;" v-if="item.isFree">(Free)</span></td>
                                        <!--<td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.itemPieceSize">{{item.product.width}}</td>-->
                                        <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.newQuantity }}</td>
                                        <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                        <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.quantity }}</td>
                                        <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.total.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(3).slice(0,-1) }}</td>
                                        <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount+item.bundleAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
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
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{parseFloat(listItemP1Summary.calulateTotalExclVAT ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((listItemP1Summary.calulateNetTotal - (listItemP1Summary.calulateDiscountAmount + listItemP1Summary.calulateBundleAmount)))   }}</td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP1Summary.calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Bundle Amount<span style="font-weight:bold;color:black !important;">/ مبلغ الحزمة</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP1Summary.calulateBundleAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(listItemP1Summary.calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Sub Total<span style="font-weight:bold;color:black !important;">/ المجموع الفرعي </span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(listItemP1Summary.calulateNetTotal - (listItemP1Summary.calulateDiscountAmount + listItemP1Summary.calulateBundleAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
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
                                        {{ $t('SaleInvoice.ReceivedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('SaleInvoice.ApprovedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('SaleInvoice.PreparedBy') }}::
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
                                <div style="margin: 0px; padding: 0px; font-weight:bold;display:flex">
                                    <div style="width: 40%;text-align:left">
                                        <span style="font-size:20px;color:black;" v-if="list.invoiceType==0">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Hold' : 'معلق' }} </span>
                                        <span style="font-size:20px;color:black;" v-if="list.invoiceType==1">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Paid' : 'مدفوع' }} </span>
                                        <span style="font-size:20px;color:black;" v-if="list.invoiceType==2">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Credit' : 'آجـل' }} </span>
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

                    <div style="height:45mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div>
                                    <!--Row 1-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Store:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{invoicePrint == 'العربية'?list.wareHouseNameAr:list.wareHouseName}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:المستودع </div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">{{ $t('SaleInvoice.InvoiceNo') }}:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.registrationNo}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                        </div>
                                    </div>
                                    <!--Row 2-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-if="list.cashCustomer != null">{{list.date}}</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-else>{{list.date}}</div>
                                            <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.cargoName">Cargo:</div>
                                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.cargoName ">{{invoicePrint == 'العربية'?list.logisticNameAr : list.logisticNameEn}}</div>
                                            <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                                :المخرج
                                            </div>
                                        </div>
                                    </div>
                                    <!--Row 3-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Invoice Type:</div>
                                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="!list.isCredit">{{invoicePrint == 'العربية'? 'نقدي' : 'Cash'}}</div>
                                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-else>{{invoicePrint == 'العربية'? 'آجل' : 'Credit' }}</div>
                                            <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">:نوع فاتورة</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;" v-if="headerFooters.customerNumber">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerNumber">Cust No:</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerNumber"><span v-if="list.cashCustomer != null">{{list.code}}</span> <span v-else>{{list.customerCode}}</span> </div>
                                            <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="headerFooters.customerNumber">
                                                :رقم العميل
                                            </div>
                                        </div>
                                    </div>
                                    <!--Row 4-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerTelephone">Tel:</div>
                                            <div style="width:50%;font-weight:bold;color:black !important;text-align:center;" v-if="list.cashCustomer != null && headerFooters.customerTelephone">{{list.mobile}}</div>
                                            <div style="width:50%;font-weight:bold;color:black !important; text-align:center;" v-if="list.creditCustomer != null && headerFooters.customerTelephone">{{list.customerTelephone}}</div>
                                            <div style="width:22%; font-weight:bolder;font-size:15px !important;color:black !important;" v-if="headerFooters.customerTelephone"><span style="">:هاتف</span> </div>
                                        </div>

                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important; "><span>Customer Name</span>:</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="list.cashCustomer != null">{{list.cashCustomer}}</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-else>{{invoicePrint == 'العربية'? list.customerNameAr=='' || list.customerNameAr==null? list.customerNameEn : list.customerNameAr    : list.customerNameEn=='' || list.customerNameEn==null?list.customerNameAr : list.customerNameEn}}</div>
                                            <div style="width:22%; font-weight:bolder;color:black !important; padding-right:6px;font-size:15px !important">: اسم العميل  </div>
                                        </div>
                                    </div>

                                    <!--Row 5-->
                                    <div class="row">
                                        <div class="col-md-8" style="display:flex;">
                                            <div style="width:20%; color:black !important;font-weight:bolder;text-align:right;" v-if="headerFooters.customerVat">Cust VAT:</div>
                                            <div style="width:37%;color:black !important;text-align:center;font-weight:bold;" v-if="headerFooters.customerVat"><span v-if="list.cashCustomer != null">{{list.cashCustomerId}}</span> <span v-else>{{list.customerVat}}</span></div>
                                            <div style="width:43%; color:black !important; font-weight:bolder;text-align:left;font-size:15px !important" v-if="headerFooters.customerVat">
                                                : رقم الضريبة للعميل
                                            </div>
                                        </div>

                                        <!--<div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right; color:black !important;" v-if="headerFooters.customerAddress"><span>Address</span>:</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                            <div style="width:22%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                                        </div>-->
                                    </div>
                                    <!--Row 6-->
                                    <div class="row">
                                        <div class="col-md-12" style="display:flex;">
                                            <div style="width:13%; font-weight:bolder;text-align:right; color:black !important;" v-if="headerFooters.customerAddress"><span>Address</span>:</div>
                                            <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                            <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                            <div style="width:11%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="height:205mm;border:2px solid !important;">
                        <div class="row ">
                            <div class="col-md-12 ">
                                <table class="table">
                                    <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                        <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <!--<th class="text-center" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>-->
                                        <th class="text-right" style="width:54%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">وصف </th>
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
                                        <!--<th class="text-center" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>-->
                                        <th class="text-left" style="width:54%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Description</th>
                                        <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.itemPieceSize">Package size</th>-->
                                        <th class="text-center" style="width:6%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Qty </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">Total Qty </th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                        <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                        <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                        <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                        <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                        <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                                    </tr>


                                    <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in listItemP2" v-bind:key="index">
                                        <td class="text-left pl-1 pr-1" style="font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important; border-top: 0 !important; border-bottom: 0 !important; color: black !important;">{{index+13}}</td>
                                        <!--<td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.product.styleNumber}}</td>-->
                                    <td class="text-left pl-1 pr-1" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.description}} <span style="color:black !important;" v-if="item.isFree">(Free)</span></td>
                                        <!--<td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.itemPieceSize">{{item.product.width}}</td>-->
                                        <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.newQuantity }}</td>
                                        <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                        <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.quantity }}</td>
                                        <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.total.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(3).slice(0,-1) }}</td>
                                        <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                        <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount+item.bundleAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
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
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;">Page: 2 - 2</td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{parseFloat(calulateTotalExclVAT ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)))   }}</td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Bundle Amount<span style="font-weight:bold;color:black !important;">/ مبلغ الحزمة</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateBundleAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black !important;">/ المبلخ الاجمالي</span></td>
                                        <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
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
                                        {{ $t('SaleInvoice.ReceivedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('SaleInvoice.ApprovedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('SaleInvoice.PreparedBy') }}::
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






            <div v-if="IsDeliveryNote=='true'">
                <p style="page-break-after: always;margin-bottom:0;margin-top:0;"></p>
                <p style="page-break-before: always;margin-bottom:0;margin-top:0;"></p>

                <!--page1-->
                <div v-if="itemTotal<=12">
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
                                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Delivery Note' : 'مذكرة تسليم' }}<span v-if="list.invoiceType==0">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Hold)' : '(معلق)' }} </span></span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div style="height:60mm;" v-else></div>

                    <div style="height:45mm;margin-top:1mm; border:2px solid #000000;">
                        <div class="row">
                            <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                <div>
                                    <!--Row 1-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Store:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{invoicePrint == 'العربية'?list.wareHouseNameAr:list.wareHouseName}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:المستودع </div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">{{ $t('Sale.InvoiceNo') }}:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.registrationNo}}</div>
                                            <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                        </div>
                                    </div>
                                    <!--Row 2-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-if="list.cashCustomer != null">{{list.date}}</div>
                                            <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-else>{{list.date}}</div>
                                            <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.cargoName">Cargo:</div>
                                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.cargoName ">{{invoicePrint == 'العربية'?list.logisticNameAr : list.logisticNameEn}}</div>
                                            <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                                :المخرج
                                            </div>
                                        </div>
                                    </div>
                                    <!--Row 3-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Invoice Type:</div>
                                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="!list.isCredit">{{invoicePrint == 'العربية'? 'نقدي' : 'Cash'}}</div>
                                            <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-else>{{invoicePrint == 'العربية'? 'آجل' : 'Credit' }}</div>
                                            <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">:نوع فاتورة</div>
                                        </div>
                                        <div class="col-md-6" style="display:flex;" v-if="headerFooters.customerNumber">
                                            <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerNumber">Cust No:</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerNumber"><span v-if="list.cashCustomer != null">{{list.code}}</span> <span v-else>{{list.customerCode}}</span> </div>
                                            <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="headerFooters.customerNumber">
                                                :رقم العميل
                                            </div>
                                        </div>
                                    </div>
                                    <!--Row 4-->
                                    <div class="row">
                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerTelephone">Tel:</div>
                                            <div style="width:50%;font-weight:bold;color:black !important;text-align:center;" v-if="list.cashCustomer != null && headerFooters.customerTelephone">{{list.mobile}}</div>
                                            <div style="width:50%;font-weight:bold;color:black !important; text-align:center;" v-if="list.creditCustomer != null && headerFooters.customerTelephone">{{list.customerTelephone}}</div>
                                            <div style="width:22%; font-weight:bolder;font-size:15px !important;color:black !important;" v-if="headerFooters.customerTelephone"><span style="">:هاتف</span> </div>
                                        </div>

                                        <div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important; "><span>Customer Name</span>:</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="list.cashCustomer != null">{{list.cashCustomer}}</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-else>{{invoicePrint == 'العربية'? list.customerNameAr : list.customerNameEn}}</div>
                                            <div style="width:22%; font-weight:bolder;color:black !important; padding-right:6px;font-size:15px !important">: اسم العميل  </div>
                                        </div>
                                    </div>

                                    <!--Row 5-->
                                    <div class="row">
                                        <div class="col-md-8" style="display:flex;">
                                            <div style="width:20%; color:black !important;font-weight:bolder;text-align:right;" v-if="headerFooters.customerVat">Cust VAT:</div>
                                            <div style="width:37%;color:black !important;text-align:center;font-weight:bold;" v-if="headerFooters.customerVat"><span v-if="list.cashCustomer != null">{{list.cashCustomerId}}</span> <span v-else>{{list.customerVat}}</span></div>
                                            <div style="width:43%; color:black !important; font-weight:bolder;text-align:left;font-size:15px !important" v-if="headerFooters.customerVat">
                                                : رقم الضريبة للعميل
                                            </div>
                                        </div>

                                        <!--<div class="col-md-6" style="display:flex;">
                                            <div style="width:28%; font-weight:bolder;text-align:right; color:black !important;" v-if="headerFooters.customerAddress"><span>Address</span>:</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                            <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                            <div style="width:22%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                                        </div>-->
                                    </div>
                                    <!--Row 6-->
                                    <div class="row">
                                        <div class="col-md-12" style="display:flex;">
                                            <div style="width:13%; font-weight:bolder;text-align:right; color:black !important;" v-if="headerFooters.customerAddress"><span>Address</span>:</div>
                                            <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                            <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                            <div style="width:11%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--INFORMATION-->
                    <div style="height:205mm;border:2px solid !important;">
                        <div class="row ">
                            <div class="col-md-12 ">
                                <table class="table">
                                    <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                        <th style="width:3%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>-->
                                        <th class="text-right" style="width:50%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">وصف </th>
                                        <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                        <th class="text-center" style="width:13%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                        <th class="text-center" style="width:14%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                    </tr>
                                    <tr class="heading" style="font-size:16px;" v-else>
                                        <th style="width:3%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                        <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>-->
                                        <th class="text-left" style="width:50%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Description</th>
                                        <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.itemPieceSize">Package size</th>-->
                                        <th class="text-center" style="width:13%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                        <th class="text-center" style="width:14%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                    </tr>


                                    <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in list.saleItems" v-bind:key="item.id">
                                        <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                        <!--<td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.product.styleNumber}}</td>-->
                                        <td class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.description}}</td>
                                        <!--<td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.itemPieceSize">{{item.product.width}}</td>-->
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.newQuantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                        <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.quantity }}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div style="height:41mm;margin-top:1mm;">
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table ">
                                    <tr>
                                        <td style="padding: 3px 7px; font-size: 16px; font-weight: bold; border-left: 2px solid ; border-top: 2px solid; border-right: 0; border-bottom: 2px solid ; width: 60%; color: black !important; "></td>
                                        <td style="padding: 3px 7px; font-size: 16px; font-weight: bold; border-right: 2px solid ; border-top: 2px solid; border-left: 0; border-bottom: 2px solid ; width: 40%; color: black !important; text-align: right; " v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ calulateTotalQty }} </td>
                                        <td style="padding: 3px 7px; font-size: 16px; font-weight: bold; border-right: 2px solid ; border-top: 2px solid; border-left: 0; border-bottom: 2px solid ; width: 40%; color: black !important; text-align: center; " v-else>Total Quantity : {{ calulateTotalQty }} </td>
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
                                        {{ $t('AllReports.ReceivedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('AllReports.ApprovedBy') }}::
                                    </td>
                                    <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                        {{ $t('AllReports.PreparedBy') }}::
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </div>

                <!--page2-->
                <div v-else-if="itemTotal>12 && itemTotal<=24">
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
                                        <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Delivery Note' : 'مذكرة تسليم' }}<span v-if="list.invoiceType==0">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Hold)' : '(معلق)' }} </span></span>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div style="height:60mm;" v-else></div>

                        <div style="height:45mm;margin-top:1mm; border:2px solid #000000;">
                            <div class="row">
                                <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                    <div>
                                        <!--Row 1-->
                                        <div class="row">
                                            <div class="col-md-6" style="display:flex;">
                                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Store:</div>
                                                <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{invoicePrint == 'العربية'?list.wareHouseNameAr:list.wareHouseName}}</div>
                                                <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:المستودع </div>
                                            </div>
                                            <div class="col-md-6" style="display:flex;">
                                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">{{ $t('Sale.InvoiceNo') }}:</div>
                                                <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.registrationNo}}</div>
                                                <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                            </div>
                                        </div>
                                        <!--Row 2-->
                                        <div class="row">
                                            <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                                <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                                <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-if="list.cashCustomer != null">{{list.date}}</div>
                                                <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-else>{{list.date}}</div>
                                                <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                            </div>
                                            <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.cargoName">Cargo:</div>
                                                <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.cargoName ">{{invoicePrint == 'العربية'?list.logisticNameAr : list.logisticNameEn}}</div>
                                                <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                                    :المخرج
                                                </div>
                                            </div>
                                        </div>
                                        <!--Row 3-->
                                        <div class="row">
                                            <div class="col-md-6" style="display:flex;">
                                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Invoice Type:</div>
                                                <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="!list.isCredit">{{invoicePrint == 'العربية'? 'نقدي' : 'Cash'}}</div>
                                                <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-else>{{invoicePrint == 'العربية'? 'آجل' : 'Credit' }}</div>
                                                <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">:نوع فاتورة</div>
                                            </div>
                                            <div class="col-md-6" style="display:flex;" v-if="headerFooters.customerNumber">
                                                <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerNumber">Cust No:</div>
                                                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerNumber"><span v-if="list.cashCustomer != null">{{list.code}}</span> <span v-else>{{list.customerCode}}</span> </div>
                                                <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="headerFooters.customerNumber">
                                                    :رقم العميل
                                                </div>
                                            </div>
                                        </div>
                                        <!--Row 4-->
                                        <div class="row">
                                            <div class="col-md-6" style="display:flex;">
                                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerTelephone">Tel:</div>
                                                <div style="width:50%;font-weight:bold;color:black !important;text-align:center;" v-if="list.cashCustomer != null && headerFooters.customerTelephone">{{list.mobile}}</div>
                                                <div style="width:50%;font-weight:bold;color:black !important; text-align:center;" v-if="list.creditCustomer != null && headerFooters.customerTelephone">{{list.customerTelephone}}</div>
                                                <div style="width:22%; font-weight:bolder;font-size:15px !important;color:black !important;" v-if="headerFooters.customerTelephone"><span style="">:هاتف</span> </div>
                                            </div>

                                            <div class="col-md-6" style="display:flex;">
                                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important; "><span>Customer Name</span>:</div>
                                                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="list.cashCustomer != null">{{list.cashCustomer}}</div>
                                                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-else>{{invoicePrint == 'العربية'? list.customerNameAr=='' || list.customerNameAr==null? list.customerNameEn : list.customerNameAr    : list.customerNameEn=='' || list.customerNameEn==null?list.customerNameAr : list.customerNameEn}}</div>
                                                <div style="width:22%; font-weight:bolder;color:black !important; padding-right:6px;font-size:15px !important">: اسم العميل  </div>
                                            </div>
                                        </div>

                                        <!--Row 5-->
                                        <div class="row">
                                            <div class="col-md-8" style="display:flex;">
                                                <div style="width:20%; color:black !important;font-weight:bolder;text-align:right;" v-if="headerFooters.customerVat">Cust VAT:</div>
                                                <div style="width:37%;color:black !important;text-align:center;font-weight:bold;" v-if="headerFooters.customerVat"><span v-if="list.cashCustomer != null">{{list.cashCustomerId}}</span> <span v-else>{{list.customerVat}}</span></div>
                                                <div style="width:43%; color:black !important; font-weight:bolder;text-align:left;font-size:15px !important" v-if="headerFooters.customerVat">
                                                    : رقم الضريبة للعميل
                                                </div>
                                            </div>

                                            <!--<div class="col-md-6" style="display:flex;">
                                                <div style="width:28%; font-weight:bolder;text-align:right; color:black !important;" v-if="headerFooters.customerAddress"><span>Address</span>:</div>
                                                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                                <div style="width:22%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                                            </div>-->
                                        </div>
                                        <!--Row 6-->
                                        <div class="row">
                                            <div class="col-md-12" style="display:flex;">
                                                <div style="width:13%; font-weight:bolder;text-align:right; color:black !important;" v-if="headerFooters.customerAddress"><span>Address</span>:</div>
                                                <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                                <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                                <div style="width:11%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--INFORMATION-->
                        <div style="height:205mm;border:2px solid !important;">
                            <div class="row ">
                                <div class="col-md-12 ">
                                    <table class="table">
                                        <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                            <th style="width:3%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                            <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>-->
                                            <th class="text-right" style="width:50%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">وصف </th>
                                            <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                            <th class="text-center" style="width:13%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                            <th class="text-center" style="width:14%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                        </tr>
                                        <tr class="heading" style="font-size:16px;" v-else>
                                            <th style="width:3%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                            <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>-->
                                            <th class="text-left" style="width:50%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Description</th>
                                            <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.itemPieceSize">Package size</th>-->
                                            <th class="text-center" style="width:13%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                            <th class="text-center" style="width:14%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                        </tr>


                                        <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in listItemP1" v-bind:key="item.id">
                                            <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                            <!--<td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.product.styleNumber}}</td>-->
                                            <td class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.description}}</td>
                                            <!--<td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.itemPieceSize">{{item.product.width}}</td>-->
                                            <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.newQuantity }}</td>
                                            <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                            <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.quantity }}</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div style="height:41mm;margin-top:1mm;">
                            <div class="row">
                                <div class="col-md-12">
                                    <table class="table ">
                                        <tr>
                                            <td style="padding: 3px 7px; font-size: 16px; font-weight: bold; border-left: 2px solid ; border-top: 2px solid; border-right: 0; border-bottom: 2px solid ; width: 60%; color: black !important; ">Page: 1 - 2</td>
                                            <td style="padding: 3px 7px; font-size: 16px; font-weight: bold; border-right: 2px solid ; border-top: 2px solid; border-left: 0; border-bottom: 2px solid ; width: 40%; color: black !important; text-align: right; " v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ listItemP1Summary.calulateTotalQty }} </td>
                                            <td style="padding: 3px 7px; font-size: 16px; font-weight: bold; border-right: 2px solid ; border-top: 2px solid; border-left: 0; border-bottom: 2px solid ; width: 40%; color: black !important; text-align: center; " v-else>Total Quantity : {{ listItemP1Summary.calulateTotalQty }} </td>
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
                                            {{ $t('AllReports.ReceivedBy') }}::
                                        </td>
                                        <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                            {{ $t('AllReports.ApprovedBy') }}::
                                        </td>
                                        <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                            {{ $t('AllReports.PreparedBy') }}::
                                        </td>
                                    </tr>

                                </table>
                            </div>
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
                                        <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Delivery Note' : 'مذكرة تسليم' }}<span v-if="list.invoiceType==0">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Hold)' : '(معلق)' }} </span></span>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div style="height:60mm;" v-else></div>

                        <div style="height:45mm;margin-top:1mm; border:2px solid #000000;">
                            <div class="row">
                                <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                                    <div>
                                        <!--Row 1-->
                                        <div class="row">
                                            <div class="col-md-6" style="display:flex;">
                                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Store:</div>
                                                <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{invoicePrint == 'العربية'?list.wareHouseNameAr:list.wareHouseName}}</div>
                                                <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:المستودع </div>
                                            </div>
                                            <div class="col-md-6" style="display:flex;">
                                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">{{ $t('Sale.InvoiceNo') }}:</div>
                                                <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.registrationNo}}</div>
                                                <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                            </div>
                                        </div>
                                        <!--Row 2-->
                                        <div class="row">
                                            <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                                <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                                <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-if="list.cashCustomer != null">{{list.date}}</div>
                                                <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" v-else>{{list.date}}</div>
                                                <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                            </div>
                                            <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.cargoName">Cargo:</div>
                                                <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.cargoName ">{{invoicePrint == 'العربية'?list.logisticNameAr : list.logisticNameEn}}</div>
                                                <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                                    :المخرج
                                                </div>
                                            </div>
                                        </div>
                                        <!--Row 3-->
                                        <div class="row">
                                            <div class="col-md-6" style="display:flex;">
                                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Invoice Type:</div>
                                                <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-if="!list.isCredit">{{invoicePrint == 'العربية'? 'نقدي' : 'Cash'}}</div>
                                                <div style="width:50%; text-align:center;font-weight:bold;color:black !important;" v-else>{{invoicePrint == 'العربية'? 'آجل' : 'Credit' }}</div>
                                                <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">:نوع فاتورة</div>
                                            </div>
                                            <div class="col-md-6" style="display:flex;" v-if="headerFooters.customerNumber">
                                                <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerNumber">Cust No:</div>
                                                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerNumber"><span v-if="list.cashCustomer != null">{{list.code}}</span> <span v-else>{{list.customerCode}}</span> </div>
                                                <div style="width:22%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="headerFooters.customerNumber">
                                                    :رقم العميل
                                                </div>
                                            </div>
                                        </div>
                                        <!--Row 4-->
                                        <div class="row">
                                            <div class="col-md-6" style="display:flex;">
                                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerTelephone">Tel:</div>
                                                <div style="width:50%;font-weight:bold;color:black !important;text-align:center;" v-if="list.cashCustomer != null && headerFooters.customerTelephone">{{list.mobile}}</div>
                                                <div style="width:50%;font-weight:bold;color:black !important; text-align:center;" v-if="list.creditCustomer != null && headerFooters.customerTelephone">{{list.customerTelephone}}</div>
                                                <div style="width:22%; font-weight:bolder;font-size:15px !important;color:black !important;" v-if="headerFooters.customerTelephone"><span style="">:هاتف</span> </div>
                                            </div>

                                            <div class="col-md-6" style="display:flex;">
                                                <div style="width:28%; font-weight:bolder;text-align:right;color:black !important; "><span>Customer Name</span>:</div>
                                                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="list.cashCustomer != null">{{list.cashCustomer}}</div>
                                                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-else>{{invoicePrint == 'العربية'? list.customerNameAr=='' || list.customerNameAr==null? list.customerNameEn : list.customerNameAr    : list.customerNameEn=='' || list.customerNameEn==null?list.customerNameAr : list.customerNameEn}}</div>
                                                <div style="width:22%; font-weight:bolder;color:black !important; padding-right:6px;font-size:15px !important">: اسم العميل  </div>
                                            </div>
                                        </div>

                                        <!--Row 5-->
                                        <div class="row">
                                            <div class="col-md-8" style="display:flex;">
                                                <div style="width:20%; color:black !important;font-weight:bolder;text-align:right;" v-if="headerFooters.customerVat">Cust VAT:</div>
                                                <div style="width:37%;color:black !important;text-align:center;font-weight:bold;" v-if="headerFooters.customerVat"><span v-if="list.cashCustomer != null">{{list.cashCustomerId}}</span> <span v-else>{{list.customerVat}}</span></div>
                                                <div style="width:43%; color:black !important; font-weight:bolder;text-align:left;font-size:15px !important" v-if="headerFooters.customerVat">
                                                    : رقم الضريبة للعميل
                                                </div>
                                            </div>

                                            <!--<div class="col-md-6" style="display:flex;">
                                                <div style="width:28%; font-weight:bolder;text-align:right; color:black !important;" v-if="headerFooters.customerAddress"><span>Address</span>:</div>
                                                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                                <div style="width:22%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                                            </div>-->
                                        </div>
                                        <!--Row 6-->
                                        <div class="row">
                                            <div class="col-md-12" style="display:flex;">
                                                <div style="width:13%; font-weight:bolder;text-align:right; color:black !important;" v-if="headerFooters.customerAddress"><span>Address</span>:</div>
                                                <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                                <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                                                <div style="width:11%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--INFORMATION-->
                        <div style="height:205mm;border:2px solid !important;">
                            <div class="row ">
                                <div class="col-md-12 ">
                                    <table class="table">
                                        <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                            <th style="width:3%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                            <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>-->
                                            <th class="text-right" style="width:50%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;"> وصف</th>
                                            <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.itemPieceSize">حجم </th>-->
                                            <th class="text-center" style="width:13%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">الكمية </th>
                                            <th class="text-center" style="width:14%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                        </tr>
                                        <tr class="heading" style="font-size:16px;" v-else>
                                            <th style="width:3%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                            <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>-->
                                            <th class="text-left" style="width:50%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Description</th>
                                            <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.itemPieceSize">Package size</th>-->
                                            <th class="text-center" style="width:13%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                            <th class="text-center" style="width:14%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                        </tr>


                                        <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in listItemP2" v-bind:key="index">
                                            <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+25}}</td>
                                            <!--<td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.product.styleNumber}}</td>-->
                                            <td class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.description}}</td>
                                            <!--<td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.itemPieceSize">{{item.product.width}}</td>-->
                                            <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.newQuantity }}</td>
                                            <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                            <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.quantity }}</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div style="height:41mm;margin-top:1mm;">
                            <div class="row">
                                <div class="col-md-12">
                                    <table class="table ">
                                        <tr>
                                            <td style="padding: 3px 7px; font-size: 16px; font-weight: bold; border-left: 2px solid ; border-top: 2px solid; border-right: 0; border-bottom: 2px solid ; width: 60%; color: black !important; ">Page: 2 - 2</td>
                                            <td style="padding: 3px 7px; font-size: 16px; font-weight: bold; border-right: 2px solid ; border-top: 2px solid; border-left: 0; border-bottom: 2px solid ; width: 40%; color: black !important; text-align: right; " v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ calulateTotalQty }} </td>
                                            <td style="padding: 3px 7px; font-size: 16px; font-weight: bold; border-right: 2px solid ; border-top: 2px solid; border-left: 0; border-bottom: 2px solid ; width: 40%; color: black !important; text-align: center; " v-else>Total Quantity : {{ calulateTotalQty }} </td>
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
                                            {{ $t('AllReports.ReceivedBy') }}::
                                        </td>
                                        <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                            {{ $t('AllReports.ApprovedBy') }}::
                                        </td>
                                        <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                            {{ $t('AllReports.PreparedBy') }}::
                                        </td>
                                    </tr>

                                </table>
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
            'https://unpkg.com/kidlat-css/css/kidlat.css',

        ],
        timeout: 700,
        autoClose: true,
        windowTitle: window.document.title,

    }
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],        props: ['printDetails', 'headerFooter', 'isTouchScreen'],
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
                qrValue: "",
                listItemP1Summary: {
                    calulateTotalQty: 0,
                    calulateNetTotal: 0,
                    calulateTotalExclVAT: 0,
                    calulateTotalVAT: 0,
                    calulateTotalInclusiveVAT: 0,
                    calulateDiscountAmount: 0,
                    calulateBundleAmount: 0,
                },
                listItemP2Summary: {
                    calulateTotalQty: 0,
                    calulateNetTotal: 0,
                    calulateTotalExclVAT: 0,
                    calulateTotalVAT: 0,
                    calulateTotalInclusiveVAT: 0,
                    calulateDiscountAmount: 0,
                    calulateBundleAmount: 0,
                },

                listItemP1: [],
                listItemP2: [],
                itemTotal: 0,
                isHeaderFooter: '',
                invoicePrint: '',
                IsDeliveryNote: '',
                arabic: '',
                english: '',
                userName: '',
                isMultiUnit: '',
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
                return this.list.saleItems.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0)
            },
            calulateNetTotal: function () {
                var withDisc = this.list.saleItems.reduce(function (a, c) { return a + (c.isFree ? 0 : c.total - c.discountAmount) }, 0)
                var totalIncDisc = (this.list.isBeforeTax && this.list.isDiscountOnTransaction && this.list.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(this.list.transactionLevelDiscount) * (withDisc) / 100) : parseFloat(this.calulateDiscountAmount)

                return this.list.saleItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total + c.includingVat) || 0)) }, 0) + parseFloat((this.list.taxMethod == ("Inclusive" || "شامل") ? 0 : this.calulateTotalVAT)) - totalIncDisc
            },
            calulateTotalExclVAT: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total - c.discountAmount) || 0)) }, 0)
            },
            calulateTotalVAT: function () {
                var totalQtyWithotFree = this.list.saleItems.reduce((qty, prod) => qty + (prod.isFree ? 0 : parseInt(prod.quantity == '' ? 0 : prod.quantity)), 0);
                var paidVat = this.list.saleItems
                    .reduce((vat, prod) => (vat + (prod.isFree ? 0 : ((this.list.taxMethod == ("Inclusive" || "شامل")) ? ((parseFloat(prod.total - prod.discountAmount) - (this.list.isBeforeTax ? (((prod.quantity * prod.unitPrice) * this.list.transactionLevelDiscount) / 100) : 0)) * prod.taxRate) / (100 + prod.taxRate) : ((parseFloat(prod.total - prod.discountAmount) - (this.list.isBeforeTax && !this.list.isFixed && this.list.isDiscountOnTransaction ? (((prod.quantity * prod.unitPrice) * this.list.transactionLevelDiscount) / 100) : (this.list.isBeforeTax && this.list.isFixed && this.list.isDiscountOnTransaction ? (this.list.transactionLevelDiscount / parseFloat(totalQtyWithotFree) * prod.quantity) : 0))) * prod.taxRate) / 100))), 0).toFixed(3).slice(0, -1)

                return paidVat;
            },
            calulateTotalInclusiveVAT: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.inclusiveVat) || 0)) }, 0)
            },
            calulateDiscountAmount: function () {
                var totalIncDisc = 0;
                if (this.list.isDiscountOnTransaction) {
                    var withDisc = this.list.saleItems.reduce(function (a, c) { return a + (c.isFree ? 0 : c.total - c.discountAmount) }, 0)

                    var discountForInclusiveVat = parseFloat(this.list.saleItems
                        .reduce((vat, prod) => (vat + (prod.isFree ? 0 : ((this.list.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(prod.total) * prod.taxRate) / (100 + prod.taxRate) : 0))), 0).toFixed(3).slice(0, -1))
                    
                    totalIncDisc = ((this.list.isBeforeTax && this.list.isDiscountOnTransaction) ? (this.list.taxMethod == ("Inclusive" || "شامل") ? (parseFloat(this.list.transactionLevelDiscount) * (withDisc - discountForInclusiveVat) / 100) : (this.list.isFixed ? parseFloat(this.list.transactionLevelDiscount) : parseFloat(this.list.transactionLevelDiscount) * withDisc / 100)) : (this.list.isFixed ? parseFloat(this.list.transactionLevelDiscount) : (parseFloat(withDisc) + (this.list.taxMethod == ("Inclusive" || "شامل") ? 0:parseFloat(this.calulateTotalVAT))) * parseFloat(this.list.transactionLevelDiscount) / 100)).toFixed(3).slice(0, -1)
                   
                }
                else {
                    totalIncDisc = this.list.saleItems.reduce(function (a, c) { return a + (c.isFree ? 0 : c.discountAmount) }, 0)
                }
               
                return totalIncDisc;
            },
            calulateBundleAmount: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number(c.bundleAmount || 0)) }, 0)
            }
        },
        methods: {
            PrintOption: function (x) {

                if (x == undefined || !x.value) {

                    return false;
                }
                return true;
            },
            calulateDiscountAmount1: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number(c.discountAmount || 0)) }, 0)
            },
            calulateBundleAmount1: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number(c.bundleAmount || 0)) }, 0)
            },
            calulateNetTotalWithVAT: function () {
                var total = this.list.saleItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total + c.includingVat) || 0)) }, 0);
                var grandTotal = parseFloat(total) - (this.calulateDiscountAmount1() + this.calulateBundleAmount1())
                return (parseFloat(grandTotal).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            calulateTotalVATofInvoice: function () {
                var total = this.list.saleItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.includingVat + c.inclusiveVat) || 0)) }, 0);
                return (parseFloat(total).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },

            printInvoice: function () {

                var root = this;
                this.$htmlToPaper('purchaseInvoice', options, () => {
                    if (root.isTouchScreen === 'TouchInvoice') {
                        root.$router.go('/TouchInvoice')
                    }
                    else if (root.isTouchScreen === 'addSale') {
                        root.$router.push({
                            query: {
                                data: undefined
                            }
                        });
                        root.$router.go('/addSale')
                    }
                    else if (root.isTouchScreen === 'sale') {
                        root.$router.push('/sale');
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



                if (!this.$ServerIp.includes('localhost')) {
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
                            root.$router.go('/TouchInvoice')
                        }
                        else {
                            root.$router.go('/addSale')
                        }
                    });

                }

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
            this.isMultiUnit = 'false';
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.invoicePrint = localStorage.getItem('InvoicePrint');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.IsDeliveryNote = localStorage.getItem('IsDeliveryNote');
            this.userName = localStorage.getItem('FullName');
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

                    var totalItem = this.printDetails.saleItems.length;

                    this.itemTotal = totalItem;

                    if (totalItem <= 12) {
                        for (var i = 0; i < totalItem; i++) {
                            root.listItemP1.push(root.printDetails.saleItems[i]);
                        }
                    }
                    else if (totalItem > 12 && totalItem <= 24) {
                        for (var k = 0; k < totalItem; k++) {
                            if (k <= 11) {
                                root.listItemP1.push(root.printDetails.saleItems[k]);
                            }
                            else {
                                root.listItemP2.push(root.printDetails.saleItems[k]);
                            }
                        }
                    }

                    /*summary calculate listItemP1Summary*/
                    root.listItemP1Summary.calulateTotalQty = root.listItemP1.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0);

                    root.listItemP1Summary.calulateNetTotal = root.listItemP1.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total + c.includingVat) || 0)) }, 0);

                    root.listItemP1Summary.calulateTotalExclVAT = root.listItemP1.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total) || 0)) }, 0);

                    root.listItemP1Summary.calulateTotalVAT = root.listItemP1.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.includingVat + c.inclusiveVat) || 0)) }, 0);

                    root.listItemP1Summary.calulateTotalInclusiveVAT = root.listItemP1.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.inclusiveVat) || 0)) }, 0);

                    root.listItemP1Summary.calulateDiscountAmount = root.listItemP1.reduce(function (a, c) { return a + (c.isFree ? 0 : Number(c.discountAmount || 0)) }, 0);

                    root.listItemP1Summary.calulateBundleAmount = root.listItemP1.reduce(function (a, c) { return a + (c.isFree ? 0 : Number(c.bundleAmount || 0)) }, 0);

                    /*summary calculate listItemP2Summary*/
                    root.listItemP2Summary.calulateTotalQty = root.listItemP2.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0);

                    root.listItemP2Summary.calulateNetTotal = root.listItemP2.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total + c.includingVat) || 0)) }, 0);

                    root.listItemP2Summary.calulateTotalExclVAT = root.listItemP2.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total) || 0)) }, 0);

                    root.listItemP2Summary.calulateTotalVAT = root.listItemP2.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.includingVat + c.inclusiveVat) || 0)) }, 0);

                    root.listItemP2Summary.calulateTotalInclusiveVAT = root.listItemP2.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.inclusiveVat) || 0)) }, 0);

                    root.listItemP2Summary.calulateDiscountAmount = root.listItemP2.reduce(function (a, c) { return a + (c.isFree ? 0 : Number(c.discountAmount || 0)) }, 0);

                    root.listItemP2Summary.calulateBundleAmount = root.listItemP2.reduce(function (a, c) { return a + (c.isFree ? 0 : Number(c.bundleAmount || 0)) }, 0);


                    this.list.date = moment(this.list.date).format('DD MMM YYYY');
                    setTimeout(function () {
                        root.printInvoice();
                    }, 125)
                }

            }
        },

    }
</script>


