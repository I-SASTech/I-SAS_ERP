<template>
    <div>
        <div ref="mychildcomponent" hidden id='purchaseInvoice' class="col-md-12" style="color:black !important">
            <!--HEADER-->
            <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
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
                                    <p class="text-right mb-0">
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
                                <td style="width: 36%;" class="text-left pt-0 pb-0 pl-0 pr-0">
                                    <span style="font-size:20px;color:black;font-weight:bold;">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Sale Return' : 'عودة البيع' }} </span>
                                </td>
                                <td style="width:64%;" class="pt-0 pb-0 pl-0 pr-0" colspan="2">
                                    <div style="text-align: left;" v-if="b2b && b2c">
                                        <div v-if="list.customerCategory=='B2B – Business to Business'">
                                            <span style="font-size:20px;color:black;font-weight:bold;">{{taxInvoiceLabel}} </span>
                                            <span style="font-size:20px;color:black;font-weight:bold;">{{taxInvoiceLabelAr}} </span>
                                        </div>
                                        <div v-else-if="list.customerCategory=='B2C – Business to Client'">
                                            <span style="font-size:20px;color:black;font-weight:bold;">{{simplifyTaxInvoiceLabel}} </span>
                                            <span style="font-size:20px;color:black;font-weight:bold;">{{simplifyTaxInvoiceLabelAr}} </span>
                                        </div>

                                        <div v-else>
                                            <span style="font-size:20px;color:black;font-weight:bold;">{{simplifyTaxInvoiceLabel}} </span>
                                            <span style="font-size:20px;color:black;font-weight:bold;">{{simplifyTaxInvoiceLabelAr}} </span>
                                        </div>
                                    </div>

                                    <div style="text-align: left;" v-else-if="b2b">
                                        <span style="font-size:20px;color:black;font-weight:bold;">{{taxInvoiceLabel}} </span>
                                        <span style="font-size:20px;color:black;font-weight:bold;">{{taxInvoiceLabelAr}} </span>
                                    </div>

                                    <div style="text-align: left;" v-else-if="b2c">
                                        <span style="font-size:20px;color:black;font-weight:bold;" v-if="b2c">{{simplifyTaxInvoiceLabel}} </span>
                                        <span style="font-size:20px;color:black;font-weight:bold;" v-if="b2c">{{simplifyTaxInvoiceLabelAr}} </span>
                                    </div>
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
            </div>
            <div style="height:45mm;" v-else></div>
            <div style="height:37mm;margin-top:1mm; border:2px solid #000000;">
                <div class="row">
                    <div class="col-md-12 ml-2 mr-2">
                        <table class="table table-borderless">
                            <!--Row 1-->
                            <tr>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">Store:</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{invoicePrint == 'العربية'?list.wareHouseNameAr:list.wareHouseName}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">:المستودع</td>

                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">{{ $t('SaleInvoice.InvoiceNo') }}:</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{list.registrationNo}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">:رقم التسجيل</td>
                            </tr>

                            <!--Row 2-->
                            <tr>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">Date:</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{list.date}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">:تاريخ</td>

                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span v-if="headerFooters.cargoName">Cargo:</span> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"><span v-if="headerFooters.cargoName">{{invoicePrint == 'العربية'?list.logisticNameAr : list.logisticNameEn}}</span> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;"><span v-if="headerFooters.cargoName">:المخرج</span> </td>
                            </tr>

                            <!--Row 3-->
                            <tr>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">Invoice Type:</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;" v-if="!list.isCredit">{{invoicePrint == 'العربية'? 'نقدي' : 'Cash'}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;" v-else>{{invoicePrint == 'العربية'? 'آجل' : 'Credit' }}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">:نوع فاتورة</td>

                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span v-if="headerFooters.customerNumber">Cust No:</span> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"><span v-if="headerFooters.customerNumber"><span v-if="list.cashCustomer != null">{{list.code}}</span> <span v-else>{{list.customerCode}}</span></span> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;"><span v-if="headerFooters.customerNumber">:رقم العميل</span> </td>
                            </tr>

                            <!--Row 4-->
                            <tr>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span v-if="headerFooters.customerTelephone">Tel:</span> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;" v-if="list.cashCustomer != null"><span v-if="headerFooters.customerTelephone">{{list.mobile}}</span> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;" v-if="list.creditCustomer != null"><span v-if="headerFooters.customerTelephone">{{list.customerTelephone}}</span></td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;"><span v-if="headerFooters.customerTelephone">:هاتف</span></td>

                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">Customer Name: </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;" v-if="list.cashCustomer != null"> {{list.cashCustomer}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;" v-else-if="headerFooters.customerNameAr && headerFooters.customerNameEn"> {{list.customerNameEn=='' || list.customerNameEn==null? list.customerNameAr : list.customerNameEn}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;" v-else-if="headerFooters.customerNameAr"> {{list.customerNameAr=='' || list.customerNameAr==null? list.customerNameEn : list.customerNameAr}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;" v-else> {{list.customerNameEn=='' || list.customerNameEn==null? list.customerNameAr : list.customerNameEn}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">:اسم العميل </td>
                            </tr>

                            <!--Row 5-->
                            <tr>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span v-if="headerFooters.customerVat">Cust VAT:</span> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"><span v-if="headerFooters.customerVat"><span v-if="list.cashCustomer != null">{{list.cashCustomerId}}</span> <span v-else>{{list.customerVat}}</span></span> </td>
                                <td colspan="4" class="pl-0 pr-0 pt-0 pb-0" style="width:61%;font-weight:bolder;font-size:14px !important;color:black !important;"><span v-if="headerFooters.customerVat">:رقم الضريبة للعميل</span></td>

                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;"> </td>
                            </tr>

                            <!--Row 6-->
                            <tr>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span v-if="headerFooters.customerAddress">Address:</span> </td>
                                <td colspan="4" class="pl-0 pr-0 pt-0 pb-0" style="width:75%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"><span v-if="headerFooters.customerAddress">{{list.customerAddressWalkIn}} </span> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;"><span v-if="headerFooters.customerAddress">:عنوان</span></td>

                            </tr>
                        </table>
                    </div>
                </div>
            </div>

            <!--INFORMATION-->
            <div style="height:185mm;border:2px solid !important;">
                <div class="row ">
                    <div class="col-md-12 ">
                        <table class="table">
                            <tr class="heading" style="font-size:14px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                <th class="text-right" style="width:41%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="!headerFooters.styleNo && headerFooters.itemPieceSize">اسم الصنف</th>
                                <th class="text-right" style="width:41%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else-if="headerFooters.styleNo && !headerFooters.itemPieceSize">اسم الصنف</th>
                                <th class="text-right" style="width:41%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else>اسم الصنف</th>
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
                            <tr class="heading" style="font-size:14px;" v-else>
                                <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>
                                <th class="text-left" style="width:41%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="!headerFooters.styleNo && headerFooters.itemPieceSize">Product Name</th>
                                <th class="text-left" style="width:41%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else-if="headerFooters.styleNo && !headerFooters.itemPieceSize">Product Name</th>
                                <th class="text-left" style="width:41%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else>Product Name</th>
                                <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.itemPieceSize">Package size</th>
                                <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                <th class="text-right" style="width:11%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                            </tr>


                            <tr style="font-size:12px;font-weight:bold;" v-for="(item, index) in list.saleItems" v-bind:key="item.id">
                                <td class="text-left pl-1 pr-1" style=" padding-top: 3px !important; padding-bottom: 3px !important; border-top: 0 !important; border-bottom: 0 !important; color: black !important;">{{index+1}}</td>
                                <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.product.styleNumber}}</td>
                                <td class="pl-1 pr-1" v-if="headerFooters.arabicName && headerFooters.englishName" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:left !important;color:black !important;">{{item.productName}}</span> <span style="float:right !important;color:black !important;">{{item.arabicName}}</span> </td>
                                <td v-else-if="headerFooters.englishName" class="text-left pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.productName}}</td>
                                <td v-else class="text-left pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:right !important;color:black !important;">{{item.arabicName}}</span></td>

                                <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.itemPieceSize">{{item.product.width}}</td>
                                <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.newQuantity }}</td>
                                <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>-{{item.quantity }}</td>
                                <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.quantity }}</td>
                                <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">-{{item.total.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">-{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">-{{parseFloat(item.taxRate).toFixed(0) }}%</td>
                                <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">-{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">-{{(((item.total - (item.discountAmount+item.bundleAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

            <div style="height:38mm;margin-top:1mm;">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-bordered tbl_padding">
                            <tr>
                                <td style="padding: 2px 7px;font-size:15px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;"></td>
                                <td style="padding: 2px 7px;font-size:15px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ calulateTotalQty }} </td>
                                <td style="padding: 2px 7px;font-size:15px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ calulateTotalQty }} </td>
                                <td style="padding: 2px 7px;font-size:15px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                <td style="padding: 2px 7px;font-size:15px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{parseFloat(calulateTotalExclVAT ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)))   }}</td>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode></td>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold">Bundle Amount<span style="font-weight:bold;color:black !important;">/ مبلغ الحزمة</span></td>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateBundleAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black ! important;">/ المبلخ الاجمالي</span></td>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="height:15mm;" v-if="isHeaderFooter=='true'">
                <div class="row">
                    <table class="table text-center">
                        <tr>
                            <td style="color: black !important;font-size:15px; font-weight:bold;border-top:0 !important;">
                                {{ $t('SaleInvoice.ReceivedBy') }}::
                            </td>
                            <td style="color: black !important;font-size:15px; font-weight:bold;border-top:0 !important;">
                                {{ $t('SaleInvoice.ApprovedBy') }}::
                            </td>
                            <td style="color: black !important;font-size:15px; font-weight:bold;border-top:0 !important;">
                                {{ $t('SaleInvoice.PreparedBy') }}::
                            </td>
                        </tr>

                    </table>
                </div>
            </div>

            <!--Footer-->
            <div class="col-md-12" style="border:2px solid #000000; height:34mm;">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-borderless">
                            <tr>
                                <td class="p-0" style="width:25%;">
                                    <u><span style="font-size:15px;color: black !important;font-weight:bold;">Terms & Conditions</span></u><br />
                                    <span style="font-size:12px;color: black !important;" v-html="headerFooters.footerEn"></span>
                                </td>
                                <td class="pl-0 pr-0 pb-0 text-center" style="width:20%;">
                                    <vue-qrcode v-bind:value="qrValue" style="width:90px;" />
                                </td>

                                <td class="pl-0 pb-0 pt-4 text-center" style="width:30%;">
                                    <table class="table">
                                        <tr>
                                            <td class="p-0 text-center" style="border:1px solid #000000;" colspan="2">Bank Details &nbsp; تفاصيل البنك</td>
                                        </tr>
                                        <tr>
                                            <td class="p-0 text-center" style="border:1px solid #000000;">{{headerFooters.bankAccount1}}</td>
                                            <td class="p-0 text-center" style="border:1px solid #000000;">{{headerFooters.bankIcon1}}</td>
                                        </tr>
                                        <tr>
                                            <td class="p-0 text-center" style="border:1px solid #000000;">{{headerFooters.bankAccount2}}</td>
                                            <td class="p-0 text-center" style="border:1px solid #000000;">{{headerFooters.bankIcon2}}</td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="pr-0 pb-0 pt-0 text-left" style="width:25%;">
                                    <u><span style="font-size:15px;font-weight:bold;">البنود و الظروف</span></u><br />
                                    <span class=" text-center" style="font-size:12px;color: black !important;" v-html="headerFooters.footerAr">

                                    </span>
                                </td>
                            </tr>

                        </table>
                    </div>

                </div>
            </div>

            <div class="row" v-if="isHeaderFooter=='true'">
                <div class="col-md-12">
                    <table class="table table-borderless">
                        <tr>
                            <td class="p-0 font-weight-bold" style="width:50%;font-size:12px;">
                                {{headerFooters.company.addressEnglish}}
                            </td>
                            <td class="p-0 text-right  font-weight-bold" style="width:50%;font-size:12px;">
                                {{headerFooters.company.addressArabic}}
                            </td>
                        </tr>
                    </table>
                </div>

            </div>
        </div>
    </div>
</template>

<script>
    import moment from "moment";
    
    import Vue3Barcode from 'vue3-barcode'
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
                
                this.htmlData.htmlString = this.$refs.mychildcomponent.innerHTML;
                var form = new FormData();
                form.append('htmlString', this.htmlData.htmlString);
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
                        link.setAttribute('download', 'SaleReturn ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();
                    });
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
        },

    }
</script>
