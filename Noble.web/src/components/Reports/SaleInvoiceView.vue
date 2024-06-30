<template>
    <div>
        <modal :modalLarge="true" :show="show" id='purchaseInvoice'>
            <div class="modal-content">
                <div class="modal-header">

                    <button type="button" class="btn-close" v-on:click="close()"></button>
                </div>
                <div class="modal-body">
                    <div v-if="printSize=='A4 Invoice' || printSize=='A4 الـــفـــاتــــورة'">
                        <div class="card ml-2 mr-2 " style="color:black !important" v-if="printTemplate=='Default Template' || printTemplate=='الــقـالـب الافــتـراضـيـة' ">
                            <!--page1-->
                            <div>
                                <!--HEADER-->
                                <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="headerFooter.isHeaderFooter">
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
                                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                                    {{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Sale Invoice ' : 'فاتورة البيع' }}
                                                    <span v-if="list.invoiceType==0">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Hold)' : '(معلق)' }} </span>
                                                    <span v-if="list.partiallyInvoice=='Fully' && list.isCredit">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Paid)' : '(مدفوعة)' }} </span>
                                                </span>
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
                                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">{{ $t('SaleInvoiceView.InvoiceNo') }}:</div>
                                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.registrationNo}}</div>
                                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:رقم التسجيل</div>
                                                    </div>
                                                </div>
                                                <!--Row 2-->
                                                <div class="row">
                                                    <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                                                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.cargoName">Date:</div>
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
                                                        <div style="width:41%;font-weight:bolder;color:black !important; padding-right:20px;font-size:15px !important" v-if="headerFooters.customerNumber">
                                                            :رقم العميل
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--Row 4-->
                                                <div class="row">
                                                    <div class="col-md-6" style="display:flex;">
                                                        <div style="width:45%; font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.customerTelephone">Tel:</div>
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
                                <div style="height:170mm;border:2px solid !important;">
                                    <div class="row ">
                                        <div class="col-md-12 ">
                                            <table class="table">
                                                <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                                    <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                                    <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                                    <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="!headerFooters.styleNo && headerFooters.itemPieceSize">اسم الصنف</th>
                                                    <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else-if="headerFooters.styleNo && !headerFooters.itemPieceSize">اسم الصنف</th>
                                                    <th class="text-right" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-else>اسم الصنف</th>
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
                                                    <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>
                                                    <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="!headerFooters.styleNo && headerFooters.itemPieceSize">Product Name</th>
                                                    <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else-if="headerFooters.styleNo && !headerFooters.itemPieceSize">Product Name</th>
                                                    <th class="text-left" style="width:44%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-else>Product Name</th>
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


                                                <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in list.saleItems" v-bind:key="item.id">
                                                    <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.product.styleNumber}}</td>
                                                    <td v-if="english=='true' && arabic=='false' && headerFooters.englishName" class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{item.productName}}</td>
                                                    <td v-else-if="isOtherLang() && english=='false' && headerFooters.arabicName" class="text-left" style="border-top:0 !important; border-bottom:0 !important;padding-top:3px !important; padding-bottom:3px !important;"><span style="float:right !important;color:black !important;">{{item.arabicName}}</span></td>
                                                    <td v-else-if="isOtherLang() && english=='true' && !headerFooters.arabicName && headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.productName}}</td>
                                                    <td v-else-if="isOtherLang() && english=='true' && headerFooters.arabicName && !headerFooters.englishName " class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:right !important;color:black !important;">{{item.arabicName}}</span></td>
                                                    <td v-else style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="float:left !important;color:black !important;">{{item.productName}}</span> <span style="float:right !important;color:black !important;">{{item.arabicName}}</span> </td>
                                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.itemPieceSize">{{item.product.width}}</td>
                                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.newQuantity }}</td>
                                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.quantity }}</td>
                                                    <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                                    <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.total.toFixed(3).slice(0,-1)}}</td>
                                                    <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discount.toFixed(3).slice(0,-1)}}</td>
                                                    <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.taxRate.toFixed(3).slice(0,-1) }}</td>
                                                    <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                                    <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount+item.bundleAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
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
                                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{parseFloat(calulateTotalExclVAT - calulateTotalInclusiveVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((calulateNetTotal - list.discountAmount))   }}</td>
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
                                <div class="col-md-12  " style="height:20mm;" v-if="headerFooter.isHeaderFooter">
                                    <div class="row">
                                        <table class="table text-center">

                                            <tr>
                                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                                    {{ $t('SaleInvoiceView.ReceivedBy') }}::
                                                </td>
                                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                                    {{ $t('SaleInvoiceView.ApprovedBy') }}::
                                                </td>
                                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                                    {{ $t('SaleInvoiceView.PreparedBy') }}::
                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                </div>

                                <!--Footer-->
                                <div class="col-md-12" style="height: 60mm;border:2px solid #000000;">
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

                                <div class="row ml-2 mr-2" v-if="headerFooter.isHeaderFooter">
                                    <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                                    <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
                                </div>
                            </div>

                            <!--page2-->
                            <!--page3-->
                            <!--page4-->



                        </div>
                        <div v-if="printTemplate=='SA – Template 1 A4' || printTemplate=='السـعـوديـة - الــقـالـب A4 1 ' " class="ml-2 mr-2 mt-2">
                            <div class="row" v-if="headerFooter.isHeaderFooter">
                                <div class="col-sm-8 col-md-8 col-lg-8 col-8">
                                    <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px;">
                                </div>
                                <div class="col-sm-4 col-md-4 col-lg-4 col-4 text-right">
                                    <p style="background-color:#F8880A !important;border-top-left-radius: 25px; font-size:30px;color:#ffffff !important;font-weight:bold;margin-bottom:0!important">{{headerFooters.company.nameArabic}}.</p>
                                    <p style="background-color: #F8880A !important;border-bottom-left-radius: 25px; font-size: 25px; color: #ffffff !important; font-weight: bold;">{{headerFooters.company.nameEnglish}}</p>
                                </div>
                            </div>
                            <div style="height:30mm" v-else></div>

                            <div class="row mt-4">

                                <div class="col-sm-8 col-md-8 col-lg-8 col-8">
                                    <p style="font-size:20px;color:black !important;font-weight:bold;margin-bottom:0!important;text-transform:capitalize;margin-bottom:20px;">We Are Improving Your Business</p>
                                    <p style="font-size:15px;color:black !important;margin-bottom:0!important"> <img src="/images/pin.png" style="width:auto;max-width:15px; max-height:15px;">  {{headerFooters.company.addressEnglish}}</p>
                                    <p style="font-size:15px;color:black !important;">{{headerFooters.company.addressArabic}}</p>
                                    <p style="font-size:16px;color:black !important;margin-bottom:0!important"><img src="/images/phone.png" style="width:auto;max-width:15px; max-height:15px;"> {{headerFooters.company.phoneNo}} Fax: {{headerFooters.company.phoneNo}}</p>
                                </div>

                                <div class="col-sm-4 col-md-4 col-lg-4 col-4 text-right">
                                    <p style="font-size:18px;color:black !important;font-weight:bold;">Credit Invoice - فاتورة الائتمان</p>

                                    <table class="table table-borderless" style="background-color:#F9F706 !important;">
                                        <tr>
                                            <th style="color: black !important;background-color:#F9F706 !important; font-size: 19px; text-align: left; border-top: 0 !important; padding-left: 5px !important; padding-right: 5px !important; ">{{list.date}}</th>
                                            <th style="color: black !important; background-color: #F9F706 !important; font-size: 19px; text-align: right; border-top: 0 !important; padding-left: 5px !important; padding-right: 5px !important; ">DATE-التاريخ</th>
                                        </tr>
                                        <tr>
                                            <th style="background-color: #F9F706 !important; color: #EB5100 !important; font-size: 19px; text-align: left; border-top: 0 !important; padding-top: 3px !important; padding-left: 5px !important; padding-right: 5px !important;">{{list.registrationNo}}</th>
                                            <th style="color: black !important; background-color: #F9F706 !important; font-size: 19px; text-align: right; border-top: 0 !important; padding-top: 3px !important; padding-left: 5px !important; padding-right: 5px !important; ">Invoice No.- رقم فاتوره</th>
                                        </tr>
                                    </table>

                                    <table class="table table-borderless" style="background-color:#F9F706 !important;">
                                        <tr>
                                            <th style="color: black !important;background-color:#F9F706 !important; font-size: 19px; text-align: left; border-top: 0 !important; padding-left: 5px !important; padding-right: 5px !important; ">VAT# {{headerFooters.company.phoneNo}}</th>
                                            <th style="color: black !important; background-color: #F9F706 !important; font-size: 19px; text-align: right; border-top: 0 !important; padding-left: 5px !important; padding-right: 5px !important; ">الضريبي</th>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div class="row mt-2 mb-2">
                                <div class="col-sm-6 col-md-6 col-lg-6 col-6">
                                    <p style="font-size:18px;color:black !important;font-weight:bold;margin-bottom:0!important;">Customer VAT# <span v-if="list.cashCustomer != null">{{list.cashCustomerId}}</span> <span v-else>{{list.customerVat}}</span> الرقم الضريبي &nbsp;&nbsp; <span>PO# {{list.saleOrderNo}} </span></p>
                                    <p style="font-size:16px;color:black !important;font-weight:bold;margin-bottom:0!important;">To: </p>
                                    <p style="font-size:16px;color:black !important;font-weight:bold;margin-bottom:0!important;">Company Name: <span v-if="list.cashCustomer != null" style="font-weight:normal;">{{list.cashCustomer}}</span> <span v-else style="font-weight:normal;">{{invoicePrint == 'العربية'? list.customerNameAr : list.customerNameEn}}</span></p>
                                    <p style="font-size:16px;color:black !important;font-weight:bold;margin-bottom:0!important">Tel: </p>
                                    <p style="font-size:16px;color:black !important;font-weight:bold;margin-bottom:0!important">Address: <span style="font-weight:normal;">{{list.customerAddressWalkIn}}</span> </p>
                                </div>
                                <div class="col-sm-6 col-md-6 col-lg-6 col-6 text-right">
                                    <div class="row">
                                        <div class="col-sm-8 col-md-8 col-lg-8 col-8">
                                            <vue-qrcode v-bind:value="qrValue" style="width:120px;" />

                                        </div>
                                        <div class="col-sm-4 col-md-4 col-lg-4 col-4">
                                            <img src="/images/fouryear.jpeg" style="max-height:120px;" alt="Alternate Text" />

                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row ">
                                <div class="col-md-12 ">
                                    <table class="table " style="border-left: 0 !important;border-bottom: 0 !important;">
                                        <tr class="heading" style="font-size:16px !important;">
                                            <th class="text-center" style="width:3%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#BB6935!important;background-color:#FAF4A1 !important;border:1px solid #000000;">#</th>
                                            <th class="text-center" style="width:40%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#BB6935!important;background-color:#FAF4A1 !important;border:1px solid #000000;">Product Name  اسم الصنف</th>
                                            <th class="text-center" style="width:12%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#BB6935!important;background-color:#FAF4A1 !important;border:1px solid #000000;">Model/Style <br /> رقم الموديل</th>
                                            <th class="text-center" style="width:8%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#BB6935!important;background-color:#FAF4A1 !important;border:1px solid #000000;">Qty <br /> الكمية </th>
                                            <th class="text-center" style="width:8%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#BB6935!important;background-color:#FAF4A1 !important;border:1px solid #000000;" v-if="isMultiUnit=='true'">Total Qty <br /> إجمالي الكمية </th>
                                            <th class="text-center" style="width:8%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#BB6935!important;background-color:#FAF4A1 !important;border:1px solid #000000;">U.Price <br /> سعرالوحدة</th>
                                            <th class="text-center" style="width:22%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#BB6935!important;background-color:#FAF4A1 !important;border:1px solid #000000;">Total Price <br /> الاجمالي </th>
                                        </tr>

                                        <template v-for="(item, index) in list.saleItems">
                                            <tr style="font-size:15px;font-weight:bold;" v-if="index<16" v-bind:key="item.id">
                                                <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;">{{index+1}}</td>
                                                <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;font-size:10px;">{{item.productName}} <br /> <span style="font-size: 15px; font-weight: bold">{{item.description}}</span></td>
                                                <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;">{{item.product.styleNumber}}</td>
                                                <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.newQuantity }}</td>
                                                <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;" v-else>{{item.quantity }}</td>
                                                <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;" v-if="isMultiUnit=='true'">{{item.quantity }}</td>
                                                <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                                <td class="text-center" style="color:black !important;background-color:#dfdfdd !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{item.total.toFixed(3).slice(0,-1)}}</span></td>
                                            </tr>
                                        </template>

                                        <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in emptyListCount" v-bind:key="index">
                                            <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;">{{index+1+indexCount}}</td>
                                            <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"></td>
                                            <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"></td>
                                            <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"></td>
                                            <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;" v-if="isMultiUnit=='true'"></td>
                                            <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"></td>
                                            <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"></td>
                                        </tr>

                                        <tr style="font-weight:bold;">
                                            <td class="text-center" style="color: #ffffff !important; background-color: #000000 !important; padding-top: 8px !important; padding-bottom: 8px !important; border:1px solid #000000;" colspan="2">{{headerFooters.bankIcon1}} Bank Details / {{headerFooters.bankAccount1}}</td>
                                            <td class="text-center" style="color: black !important; padding-top: 8px !important; padding-bottom: 8px !important; border: 0 !important;" v-if="isMultiUnit=='true'"></td>
                                            <td class="text-left" style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:0!important" colspan="2">SUBTotal</td>
                                            <td class="text-right" style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:0!important">المجموع</td>
                                            <td class="text-center" style="color:black !important;background-color: #F5E2A5 !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateTotalExclVAT - calulateTotalInclusiveVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
                                        </tr>
                                        <tr style="font-weight:bold;">
                                            <!--<td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:11px!important;border:0!important"></td>-->
                                            <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:12px!important;border:0!important;padding-left:0 !important;" colspan="2">Terms and condition</td>
                                            <td class="text-center" style="color: black !important; padding-top: 8px !important; padding-bottom: 8px !important; border: 0 !important;" v-if="isMultiUnit=='true'"></td>
                                            <td class="text-left" style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:0!important" colspan="2">Discount</td>
                                            <td class="text-right" style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:0!important">الخصم</td>
                                            <td class="text-center" style="color:#BB6935 !important;background-color: #CFCEC9 !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
                                        </tr>
                                        <!--<tr style="font-weight:bold;">-->
                                        <!--<td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:11px!important;border:0!important"></td>-->
                                        <!--<td class="text-center" style="color: black !important; padding-top: 8px !important; padding-bottom: 8px !important; border: 0 !important;" v-if="isMultiUnit=='true'"></td>
                                <td style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:0!important" colspan="3"><span class="float-left">Bundle Amount</span> <span class="float-right">مبلغ الحزمة</span>  </td>
                                <td class="text-center" style="color:black !important;background-color: #6BB6D5 !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateBundleAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
                            </tr>-->
                                        <tr style="font-weight:bold;">
                                            <!--<td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:11px!important;border:0!important"></td>-->
                                            <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:11px!important;border:0!important;padding-left:0 !important;" colspan="2">{{headerFooters.footerEn}}</td>
                                            <td class="text-center" style="color: black !important; padding-top: 8px !important; padding-bottom: 8px !important; border: 0 !important;" v-if="isMultiUnit=='true'"></td>
                                            <td class="text-center" style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:0!important" colspan="3"><span class="float-left">VAT 15%</span><span class="float-right">ضريبة القيمة المضافة</span>   </td>
                                            <td class="text-center" style="color:black !important;background-color: #D3E4DE !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
                                        </tr>
                                        <tr style="font-weight:bold;">
                                            <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:11px!important;border:0!important;padding-left:0 !important;" colspan="2">{{headerFooters.footerAr}}</td>
                                            <td class="text-center" style="color: black !important; padding-top: 8px !important; padding-bottom: 8px !important; border: 0 !important;" v-if="isMultiUnit=='true'"></td>
                                            <td class="text-center" style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:0!important" colspan="3"><span class="float-left">Total Payable</span><span class="float-right">الاجمالي المستحق</span>  </td>
                                            <td class="text-center" style="color:black !important;background-color: #F4D88F !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>


                        </div>

                        <!--<div v-if="printTemplate=='Template2'" class="ml-2 mr-2 mt-2">-->
                        <!--HEADER-->
                        <!--<div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
                <div class="row" style="height:35mm">
                    <div class="col-md-4 ">
                        <table class=" text-left">
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
                        <table class="table text-right">
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
                            <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                                {{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Sale Invoice ' : 'فاتورة البيع' }}
                                <span v-if="list.invoiceType==0">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Hold)' : '(معلق)' }} </span>
                                <span v-if="list.partiallyInvoice=='Fully' && list.isCredit">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Paid)' : '(مدفوعة)' }} </span>
                            </span>
                        </p>
                    </div>
                </div>
            </div>
            <div style="height:60mm;" v-else></div>

            <div style="height:45mm;margin-top:1mm; border:2px solid #000000;">
                <div class="row">
                    <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                        <div>-->
                        <!--Row 1-->
                        <!--<div class="row">
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
            </div>-->
                        <!--Row 2-->
                        <!--<div class="row">
                <div class="col-md-6" style="display:flex;" v-if="headerFooters.cargoName">
                    <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;" v-if="headerFooters.cargoName">Date:</div>
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
            </div>-->
                        <!--Row 3-->
                        <!--<div class="row">
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
            </div>-->
                        <!--Row 4-->
                        <!--<div class="row">
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
            </div>-->
                        <!--Row 5-->
                        <!--<div class="row">
            <div class="col-md-8" style="display:flex;">
                <div style="width:20%; color:black !important;font-weight:bolder;text-align:right;" v-if="headerFooters.customerVat">Cust VAT:</div>
                <div style="width:37%;color:black !important;text-align:center;font-weight:bold;" v-if="headerFooters.customerVat"><span v-if="list.cashCustomer != null">{{list.cashCustomerId}}</span> <span v-else>{{list.customerVat}}</span></div>
                <div style="width:43%; color:black !important; font-weight:bolder;text-align:left;font-size:15px !important" v-if="headerFooters.customerVat">
                    : رقم الضريبة للعميل
                </div>
            </div>-->
                        <!--<div class="col-md-6" style="display:flex;">
                <div style="width:28%; font-weight:bolder;text-align:right; color:black !important;" v-if="headerFooters.customerAddress"><span>Address</span>:</div>
                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                <div style="width:50%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                <div style="width:22%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
            </div>-->
                        <!--</div>-->
                        <!--Row 6-->
                        <!--<div class="row">
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
            </div>-->
                        <!--INFORMATION-->
                        <!--<div style="height:205mm;border:2px solid !important;">
                <div class="row ">
                    <div class="col-md-12 ">
                        <table class="table">
                            <tr class="heading" style="font-size:15px;">
                                <th style="padding-left: 1px; padding-right: 1px; width: 1%; padding-top: 3px !important; padding-bottom: 3px !important; border-bottom: 0px !important; border-right: 1px solid #000000 !important;">#</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 6%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="headerFooters.styleNo">Model/ <br />Style</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 48%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">Product Name</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 3%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="headerFooters.itemPieceSize">Pack size</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 4%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">Qty</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 6%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="isMultiUnit=='true'">Total Qty</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 6%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">U.Price</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 6%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">Total Price</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 4%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">Disc</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 4%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">Tax%</th>
                                <th class="text-right" style="padding-left: 1px; padding-right: 1px; width: 7%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">VAT <br /> AMT.</th>
                                <th class="text-right" style="padding-left: 1px; padding-right: 1px; width: 11%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important;">Grand  <br />Total</th>
                            </tr>
                            <tr class="heading" style="font-size:15px !important;padding-top:5px;border-bottom:1px solid #000000 !important;">
                                <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important;border-right:1px solid #000000 !important;"></th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 6%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 47%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">اسم الصنف</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 3%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="headerFooters.itemPieceSize">حجم </th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 5%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">الكمية </th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 6%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 6%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">سعرالوحدة</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 6%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">الاجمالي </th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 4%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">الخصم </th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 4%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">%الضريية</th>
                                <th class="text-right" style="padding-left: 1px; padding-right: 1px; width: 6%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">قيمة الضريبة</th>
                                <th class="text-right" style="padding-left: 1px; padding-right: 1px; width: 11%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important;">المجموع الإجمالي </th>
                            </tr>



                            <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in list.saleItems" v-bind:key="item.id">
                                <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;border-right:1px solid #000000 !important;">{{index+1}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;" v-if="headerFooters.styleNo">{{item.product.styleNumber}}</td>
                                <td class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;"><p style="margin-bottom: 0;text-align:right;">{{item.arabicName}}</p>  <p style="margin-bottom: 0;font-size:13px;">{{item.productName}}</p> </td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;" v-if="headerFooters.itemPieceSize">{{item.product.width}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.newQuantity }}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;" v-else>{{item.quantity }}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;" v-if="isMultiUnit=='true'">{{item.quantity }}</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;">{{item.total.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;">{{item.discount.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;">{{item.taxRate.toFixed(3).slice(0,-1) }}</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;">{{(((item.total - (item.discountAmount+item.bundleAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
                            </tr>
                            <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in emptyListCount" v-bind:key="index">
                                <td class="text-left" style="border-top:0 !important; border-bottom:0 !important;color:black !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;">{{index+1+indexCount}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;" v-if="headerFooters.styleNo"></td>
                                <td class="text-left" style="border-top:0 !important; border-bottom:0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;"></td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;" v-if="headerFooters.itemPieceSize"></td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;" v-if="isMultiUnit=='true'"></td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;" v-else></td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;" v-if="isMultiUnit=='true'"></td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;"></td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;"></td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;"></td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;"></td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;"></td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;"></td>
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
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{parseFloat(calulateTotalExclVAT - calulateTotalInclusiveVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{ (calulateNetTotal - list.discountAmount) | toWords }}</td>
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
            </div>-->
                        <!--Footer-->
                        <!--<div class="col-md-12" style="height: 52mm;border:2px solid #000000;">
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
                <div class="col-md-12" v-if="isHeaderFooter=='true'">
                    <div style="width:100%;display:flex;">
                        <div class="float-left;" style="width: 50%; color: black !important; font-size: 15px; font-weight: bold;">{{headerFooters.company.addressEnglish}}</div>
                        <div class="float-right text-right" style="width: 50%; color: black !important; font-size: 16px; font-weight: bold;">{{headerFooters.company.addressArabic}}</div>
                    </div>
                </div>
            </div>-->
                    </div>
                    <div v-if="printSize=='Thermal Invoice' || printSize=='الـفـاتـورة طـابـعـة حـراريـة' " class="row">
                        <div v-if="printTemplate=='Default Template' || printTemplate=='الــقـالـب الافــتـراضـيـة' " class="col-lg-5 ml-auto mr-auto">
                            <div ref="mychildcomponent" id='purchaseInvoice' style="z-index:-9999;width:400px;">
                                <!--INFORMATION-->
                                <div style="width:400px;margin-left:20px;">
                                    <!--<div style="width:100%;text-align:center;margin-bottom:2.5rem;margin-top:1rem;">
                    <img :src="headerFooters.company.logoPath" style="width: auto; max-width: 300px; height: auto; max-height: 120px;">
                </div>-->
                                    <!--<div style="width:100%;text-align:center;">
                    <vue-qrcode v-bind:value="qrValue" style="width:200px" v-bind:errorCorrectionLevel="correctionLevel" />
                </div>-->
                                    <div style="width:100%;">
                                        <div style="text-align: center;">
                                            <span style="font-size:30px;font-weight:bold;color:black;">{{headerFooters.company.nameEnglish}} {{headerFooters.company.nameArabic}}</span><br />
                                            <span style="font-size:18px;font-weight:bold;color:black;">{{headerFooters.company.categoryEnglish}} {{headerFooters.company.categoryArabic}}</span><br />
                                            <p style="border:1px solid; width:350px;font-weight:bold;color:black;margin-left:auto;margin-right:auto;margin-top:1.3rem;margin-bottom:1.3rem;">
                                                VAT No. {{headerFooters.company.vatRegistrationNo}} الرقم الضريبي <br />
                                                <span style="font-size:18px;color:black;">{{headerFooters.company.companyNameArabic}}</span><br />
                                                <span style="font-size:17px;color:black;">{{headerFooters.company.companyNameEnglish}}</span>
                                            </p>
                                            <span style="font-size:18px;color:black;">{{headerFooters.company.addressArabic}}</span><br />
                                            <span style="font-size:16px;color:black;">{{headerFooters.company.addressEnglish}}</span><br />
                                            <span style="font-size:15px;color:black;">Mobile No. {{headerFooters.company.phoneNo}} رقم الجوال</span><br />
                                        </div>
                                    </div>


                                    <div style="width:100%;text-align:center;margin-top:2rem;">
                                        <p style="font-size: 15px; font-weight: bold;color:black;margin-bottom:2px;padding-bottom:2px;">VAT Invoice No. <span style="border:4px solid;font-size:25px;font-weight:bold;padding-left:2px;padding-right:2px;color:black;"> {{list.registrationNo}}</span> فاتوره ضريبيه مبسطه</p>
                                        <barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>
                                        <p style="font-size:15px;font-weight:bold;color:black;">Date: <span style="margin-right:20px;margin-left:10px;padding-left:20px;color:black;">22-1-2020 9:18</span>  التاريخ</p>

                                        <table class="table table-borderless " style="width:400px;">
                                            <tr style="font-size:16px;">
                                                <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Counter#</span> رقم الكاونتر  <br>TR-001</td>
                                                <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">User</span> أسم المستخدم <br>Dummy</td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="width:100%;">
                                        <table class="table report-tbl" style="width:400px;">
                                            <tr class="heading" style="font-size:15px;border-bottom:1px solid black;border-top:1px solid; color:black">
                                                <th style="text-align: left; width: 5% ; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">رقم <br />No.</th>
                                                <th style="text-align: center; width: 50%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">الصنف<br />Item</th>
                                                <th style="text-align: center; width: 10%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">الكمية<br />Qty</th>
                                                <th style="text-align: right; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">السعر<br />Price</th>
                                                <th style="text-align: right; width: 20%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">مجموع<br />Total</th>
                                            </tr>

                                            <tr style="font-size:15px;">
                                                <td style="text-align: left;  border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">1</td>
                                                <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;">  معقم اليدين <br>Hand Sanitizer</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">44</td>
                                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">125</td>
                                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">2000</td>
                                            </tr>
                                            <tr style="font-size:15px;">
                                                <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">1</td>
                                                <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;">  معقم اليدين <br>Hand Sanitizer Vitmain E</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">44</td>
                                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">125</td>
                                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">2000</td>
                                            </tr>
                                            <tr style="font-size:15px;">
                                                <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">1</td>
                                                <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;">  معقم اليدين <br>Hand Sanitizer Moisturizer</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">44</td>
                                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">125</td>
                                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">2000</td>
                                            </tr>

                                            <tr class="heading" style="font-size:16px;">
                                                <th style="text-align:center; padding-left:1px;padding-right:1px;color:black;border-bottom: 1px solid #000000;border-top: 1px solid #000000;" colspan="2">T.Items عدد العنف <br />3</th>
                                                <th style="text-align: center; padding-left: 1px; padding-right: 1px; color: black;border-bottom: 1px solid #000000;border-top: 1px solid #000000;" colspan="2">T.Pieces عدد القطع <br /> 72</th>
                                                <th style="text-align: center; padding-left: 1px; padding-right: 1px; color: black;border-bottom: 1px solid #000000;border-top: 1px solid #000000;">G.Total الإجمالي <br />43434.00</th>
                                            </tr>
                                            <tr style="font-size:16px;">
                                                <td colspan="3" style="text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; color: black;">Total wihout VAT الإجمالي قبل ضريبة:</td>
                                                <td colspan="2" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; color: black;">432423</td>
                                            </tr>
                                            <tr style="font-size:16px;">
                                                <td colspan="3" style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black;">Discount قيمة الخصم:</td>
                                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black; ">15</td>
                                            </tr>
                                            <tr style="font-size:16px;">
                                                <td style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black;" colspan="3">Total after Discount الإجمالي بعد الخصم:</td>
                                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black; ">45635.00</td>
                                            </tr>

                                            <tr style="font-size:16px;">
                                                <td style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; padding-bottom: 3px; color: black;" colspan="3">VAT 15% الضريبة:</td>
                                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; padding-bottom: 3px; color: black; ">15%</td>
                                            </tr>
                                            <tr style="font-size:16px;font-weight:bold;" v-if="isCalculationShow">
                                                <td style="text-align: right; padding-left: 1px; padding-right: 0px; padding-top: 5px; color: black;border-top: 0;" colspan="3">Payable Amount الإجمالي المستحق:</td>
                                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; padding-top: 5px; color: black;border-top: 0; ">43534.00</td>
                                            </tr>
                                            <tr style="font-size:16px;font-weight:bold;" v-else>
                                                <td style="text-align: right; padding-left: 1px; padding-right: 0px; padding-top: 5px; color: black;border-top: 0;" colspan="3">Total with Vat إجمالي مع ضريبةالقيمةالمضافة:</td>
                                                <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; padding-top: 5px; color: black;border-top: 0; ">34398.00</td>
                                            </tr>

                                            <tr style="font-size:19px;font-weight:bold;">
                                                <td style="text-align:center; color: black;border-top: 1px solid #000000;" colspan="5">Payment Mode طريقة الدفع</td>
                                            </tr>
                                            <tr style="font-size:17px;padding-bottom:10px;">
                                                <td style="text-align: left; color: black;border-top: 0; padding-bottom:4px;padding-top:4px;" colspan="3">Cash :</td>
                                                <td style="text-align: right; color: black;border-top: 0;padding-bottom:4px;padding-top:4px;" colspan="2">89788</td>
                                            </tr>
                                            <tr style="font-size:17px;padding-bottom:10px;">
                                                <td style="text-align: left; color: black;border-top: 0; padding-bottom:4px;padding-top:4px;" colspan="3">Bank :</td>
                                                <td style="text-align: right; color: black;border-top: 0;padding-bottom:4px;padding-top:4px;" colspan="2">897884</td>
                                            </tr>
                                            <tr style="font-size:17px;" v-if="isCalculationShow">
                                                <td style="text-align: left; border-top: 0; padding-top: 0; color: black;" colspan="3">Invoice Amount قيمة الفاتورة:</td>
                                                <td colspan="2" style="text-align: right; border-top: 0; padding-top: 0; color: black;">3454355</td>
                                            </tr>
                                            <tr style="font-size: 15px; font-weight: bold; border-bottom: 1px solid #000000; border-top: 1px solid #000000 ">
                                                <td style="text-align: left; border-top:0; padding-top: 0; color: black;" colspan="3">Payable Amount الإجمالي المستحق:</td>
                                                <td colspan="2" style="text-align: right; border-top: 0; padding-top: 0; color: black;">65765756</td>
                                            </tr>
                                            <tr style="font-size:17px;">
                                                <th style="text-align: left; border-top: 0; padding-top: 0; color: black;" colspan="3">Change Due:</th>
                                                <th colspan="2" style="text-align: right; border-top: 0; padding-top: 0; color: black;">0.00</th>
                                            </tr>

                                            <!--<tr style="font-size:17px;" v-if="isDoublePrint">
                            <td style="text-align: center; text-transform: capitalize; color: black;border-top: 0;" colspan="5">{{ (totalInvoiceAmount - totalReturnInvoiceAmount).toFixed(3).slice(0,-1) | toWords}}</td>
                        </tr>-->
                                            <tr style="font-size:17px;">
                                                <td style="text-align: center; text-transform: capitalize; color: black;border-top: 0;" colspan="5">6765756</td>
                                            </tr>
                                            <tr style="font-size:17px;">
                                                <td style="text-align: center; text-transform: capitalize; color: black;border-top: 0;" colspan="5">6456456</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div style="width:400px;">
                                    <div style="width:50%;float:left;">
                                        <p style="font-size:16px;color:black;font-weight:bold;">Refund / Exchange Policy</p>
                                        <p style="font-size:15px;color:black;">
                                            {{headerFooters.footerEn}}
                                        </p>
                                    </div>
                                    <div style="width:45%;float:right">
                                        <p style="font-size:17px;text-align:right;color:black;font-weight:bold;">سياسة الإسترجاع و الإستبدال</p>
                                        <p style="font-size:17px;text-align:right;color:black;">{{headerFooters.footerAr}}</p>

                                    </div>
                                </div>

                                <div style="width:400px;display:block;margin-left:20px;margin-top:1.5rem">
                                    <p style="color:transparent;">hidden</p>
                                    <p style="text-align:center;color:black;"><span style="margin-right:5px;color:black;">*</span> <span style="margin-right:5px;color:black;">*</span> <span style="margin-right:10px;color:black;">*</span> <span style="font-size:25px; font-weight:bold;color:black;">شـكـرا لــزيــارتـكـــ</span>  <span style="margin-left: 10px;color:black;">*</span> <span style="margin-left: 5px;color:black;">*</span> <span style="margin-left:5px;color:black;">*</span></p>
                                    <h6 style="text-align:center;color:black;">Thankyou for your visit</h6>
                                </div>
                                <div style="width:400px;display:block;margin-left:20px;margin-top:1.5rem;text-align:center;">
                                    <vue-qrcode v-bind:value="qrValue" style="width:200px" v-bind:errorCorrectionLevel="correctionLevel" />
                                </div>

                            </div>

                        </div>
                        <div v-if="printTemplate=='PK – Template 1 Thermal'" class="col-lg-6 ml-auto mr-auto">
                            <div ref="mychildcomponent" id='purchaseInvoice' style="width:400px;z-index:-9999">
                                <!--INFORMATION-->
                                <div style="width:400px;margin-left:20px;">
                                    <div style="width:100%;text-align:center;margin-bottom:2.5rem;">
                                        <img :src="headerFooters.company.logoPath" style="width: auto; max-width: 300px; height: auto; max-height: 120px;">
                                    </div>
                                    <div style="text-align: center;margin-bottom:15px;">
                                        <span style="font-size:20px;font-weight:bold;color:black;">SALES TAX INVOICE</span><br />

                                    </div>
                                    <div style="width:100%;">



                                        <span style="font-size:18px;color:black !important;text-align:left;padding-right:5px;">Shop Name: </span> <span style="font-size:18px;color:black !important;text-align:left;">{{headerFooters.company.companyNameEnglish}}</span><br />
                                        <span style="font-size:18px;color:black !important;text-align:left;padding-right:5px;">Plot # </span> <span style="font-size:18px;color:black !important;text-align:left;">{{headerFooters.company.addressEnglish}}</span><br />
                                        <span style="font-size:18px;color:black !important;text-align:left;padding-right:5px;">Tel  #: </span> <span style="font-size:18px;color:black !important;text-align:left;">{{headerFooters.company.phoneNo}}</span><br />
                                        <span style="font-size:18px;color:black !important;text-align:left ;padding-right:5px;">NTN  #: </span> <span style="font-size:18px;color:black !important;text-align:left;">{{headerFooters.company.vatRegistrationNo}}</span><br />
                                        <span style="font-size:18px;color:black !important;text-align:left ;padding-right:5px;">STRN  #: </span> <span style="font-size:18px;color:black !important;text-align:left;">{{headerFooters.company.companyRegNo}}</span><br />

                                    </div>


                                    <div style="width:100%;margin-top:2rem;">
                                        <span style="font-size:18px;color:black !important;text-align:left ;padding-right:5px;">Date & Time : </span> <span style="font-size:18px;color:black !important;text-align:left;">{{list.date}}</span><br />
                                        <span style="font-size:18px;color:black !important;text-align:left ;padding-right:5px;">Receipt # : </span> <span style="font-size:18px;color:black !important;text-align:left;">{{list.registrationNo}}</span><br />
                                        <span style="font-size:18px;color:black !important;text-align:left ;padding-right:5px;">Customer Name # : </span>
                                        <span style="font-size:18px;color:black !important;text-align:left;">Dummy</span>
                                        <br />


                                    </div>
                                    <div style="width:100%;margin-top:20px">
                                        <table class="table report-tbl">
                                            <tr class="heading" style="font-size:19px;border:1px solid black !important; color:black !important;padding-top:0px !important;padding-bottom:0px !important">
                                                <th style="text-align: left; width: 5%; border: 1px solid #000000;  color: black !important;padding-top:0px !important;padding-bottom:0px !important"> #</th>
                                                <th style="text-align: left; width: 50%; border: 1px solid #000000;  color: black !important;padding-top:0px !important;padding-bottom:0px !important">Product</th>
                                                <th style="text-align: center; width: 10%; border: 1px solid #000000;  color: black !important;padding-top:0px !important;padding-bottom:0px !important">Qty</th>
                                                <th style="text-align: right; width: 15%; border: 1px solid #000000;  color: black !important;padding-top:0px !important;padding-bottom:0px !important">Price</th>
                                                <th style="text-align: center; width: 15%; border: 1px solid #000000;  color: black !important;padding-top:0px !important;padding-bottom:0px !important">Disc%</th>
                                                <th style="text-align: center; width: 20%; border: 1px solid #000000;  color: black !important;padding-top:0px !important;padding-bottom:0px !important">Total</th>
                                            </tr>

                                            <tr style="font-size:15px;">
                                                <td style="text-align: left; border-top: 0; padding-top: 0px; padding-left: 1px; padding-right: 1px; color: black !important;">1</td>
                                                <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">Hand Sanitizer Small</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">5</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">55</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">5%</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">3000</td>
                                            </tr>
                                            <tr style="font-size:15px;">
                                                <td style="text-align: left; border-top: 0; padding-top: 0px; padding-left: 1px; padding-right: 1px; color: black !important;">2</td>
                                                <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">Hand Sanitizer Medium</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">5</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">55</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">5%</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">3000</td>
                                            </tr>
                                            <tr style="font-size:15px;">
                                                <td style="text-align: left; border-top: 0; padding-top: 0px; padding-left: 1px; padding-right: 1px; color: black !important;">3</td>
                                                <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">Hand Sanitizer Large</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">5</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">55</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">5%</td>
                                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">3000</td>
                                            </tr>

                                        </table>
                                        <table class="table " style="width: 400px; color: black !important; border-color: black !important;margin-top:10px">
                                            <tr style="font-size:15px;">
                                                <td width="70%" style="text-align: left; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; border:1px solid;color:black !important; border-color:black !important; font-weight:bold">Total Quantity:</td>
                                                <td width="30%" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; border:1px solid;color:black !important; border-color:black !important;font-weight:bold"> 3</td>
                                            </tr>
                                            <tr style="font-size:15px;">
                                                <td width="70%" style="text-align: left; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; border:1px solid;color:black !important; border-color:black !important;">Amount Before Discount</td>
                                                <td width="30%" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; border:1px solid;color:black !important; border-color:black !important;"> 234324.00</td>
                                            </tr>
                                            <tr style="font-size:15px;">
                                                <td width="70%" style="text-align: left; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; border:1px solid;color:black !important; border-color:black !important;">Total Discount</td>
                                                <td width="30%" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; border:1px solid;color:black !important; border-color:black !important;"> 5%</td>
                                            </tr>
                                            <tr style="font-size:15px;">
                                                <td width="70%" style="text-align: left; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; border:1px solid;color:black !important; border-color:black !important;">Total Amount Exl.Tax</td>
                                                <td width="30%" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; border:1px solid;color:black !important; border-color:black !important;">234324.00</td>
                                            </tr>
                                            <tr style="font-size:15px;">
                                                <td width="70%" style="text-align: left; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; border:1px solid;color:black !important; border-color:black !important;">Sale Tax 17.00%</td>
                                                <td width="30%" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; border:1px solid;color:black !important; border-color:black !important;">3434.00</td>
                                            </tr>
                                            <tr style="font-size:15px;">
                                                <td width="70%" style="text-align: left; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; border:1px solid;color:black !important; border-color:black !important;font-weight:bold">Total Amount</td>
                                                <td width="30%" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; border:1px solid;color:black !important; border-color:black !important;font-weight:bold"> 233432.00</td>
                                            </tr>
                                        </table>

                                    </div>
                                </div>
                                <div style="width:400px;border-top:1px dotted black !important;padding-top:20px">
                                    <div style="width:100%;float:left;">
                                        <p style="font-size:16px;color:black !important;font-weight:bold;">Return & Exchange Policy</p>
                                        <p style="font-size:15px;color:black !important;">
                                            1- No cash refund.<br />
                                            2- For exchange, all items must be in original condition with tags and labels intact.<br />
                                            3-Exchange of equal value be made within 15 days of purchase if sales receipt is provided.<br />
                                            4- No exchange of discounted or factory outlet products.<br />
                                            5- Groom's wear, customized products, fragrances,cosmetics and jewelry cannot be exchanged or returned.<br />
                                            6- Articles worn/washed/shrunk will not be exchanged.<br />
                                            7- During sale or promotion,items purchased at regular price will be exchanged at discounted Price.<br />



                                        </p>
                                        <p style="font-size:15px;color:black !important;padding-top:15px">  * Prices are inclusive of sales tax levied by Govt<br /></p>

                                        <p style="font-size:15px;color:black !important;padding-top:15px;text-align:center">  Thank You for Shopping at {{headerFooters.company.companyNameEnglish}} </p>
                                        <p style="font-size:15px;color:black !important;text-align:center">Email: {{headerFooters.company.companyEmail}}</p>
                                        <p style="font-size:15px;color:black !important;text-align:center">UAN: {{headerFooters.company.phoneNo}}</p>
                                        <p style="font-size:15px;color:black !important;text-align:center">Log on: {{headerFooters.company.website}}</p>
                                    </div>

                                </div>

                                <div style="width:400px;display:block;margin-left:20px;margin-top:1.5rem">
                                    <p style="color:transparent;">hidden</p>

                                </div>
                                <div style="width:400px;margin-top:1.5rem;text-align:center;">
                                    <barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>
                                </div>
                                <!--<div style="width:50%;display:block;margin-top:1.5rem;text-align:left;">
                <vue-qrcode v-bind:value="qrValue" style="width:100px" v-bind:errorCorrectionLevel="correctionLevel" />
            </div>-->
                                <div style="display:flex;margin-top:1.5rem;width:400px;">
                                    <div style="text-align:left;width:40%;">
                                        <vue-qrcode v-bind:value="qrValue" v-bind:errorCorrectionLevel="correctionLevel" style="width:150px;" />

                                    </div>
                                    <div style="text-align:center;width:58%;">
                                        <img src="/FBR1.png" class="mx-auto d-block" style="width:180px;" />

                                    </div>
                                </div>

                            </div>
                        </div>
                        <div v-if="printTemplate=='PK – Template 2 Thermal'" class="col-lg-6 ml-auto mr-auto">
                            <div>
                                <div ref="mychildcomponent" id='purchaseInvoice' style="width:400px;z-index:-9999">
                                    <!--INFORMATION-->
                                    <div style="width:400px;margin-left:20px;">
                                        <div style="width:100%;text-align:center;margin-bottom:2.5rem;margin-top:10px">
                                            <img :src="headerFooters.company.logoPath" class="rounded-circle" style="width: auto; max-width: 300px; height: auto; max-height: 120px;">
                                        </div>
                                        <div style="text-align: center;margin-bottom:15px;">
                                            <span style="font-size:20px;font-weight:bold;color:black;">{{headerFooters.company.companyNameEnglish}}</span><br />
                                            <span style="font-size:20px;font-weight:bold;color:black;">{{headerFooters.company.addressEnglish}}</span><br />
                                            <span style="font-size:20px;font-weight:bold;color:black;">{{headerFooters.company.phoneNo}}</span><br />

                                        </div>

                                        <div style="width:100%;">
                                            <div style="text-align: center;">
                                                <span style="font-size:18px;font-weight:bold;color:black;">NTN:{{headerFooters.company.vatRegistrationNo}}</span><br />
                                                <span style="font-size:18px;font-weight:bold;color:black;">STRN:{{headerFooters.company.companyRegNo}}</span><br />
                                                <span style="font-size:18px;font-weight:bold;color:black;">Invoice No#{{list.registrationNo}}</span><br />
                                                <span style="font-size:18px;font-weight:bold;color:black;">{{list.date}}</span><br />

                                            </div>
                                            <div style="width:100%;margin-top:2rem;">
                                                <span style="font-size:18px;color:black;text-align:left ;padding-right:5px;font-weight:bold;padding-top:15px">Cashier : </span> <span style="font-size:18px;color:black;text-align:left;">Dummy</span><br />
                                                <span style="font-size:18px;color:black;text-align:left ;padding-right:5px;font-weight:bold;padding-top:15px">Payment Mode : </span>
                                                <span style="font-size:18px;color:black;text-align:left;">Cash</span>
                                                <br />
                                                <span style="font-size:18px;color:black;text-align:left ;padding-right:5px;font-weight:bold;padding-top:15px">Customer Name # : </span>
                                                <span style="font-size:18px;color:black;text-align:left;">Dummy sdl</span>
                                                <br />


                                            </div>
                                        </div>


                                        <div>
                                            <table class="table report-tbl" style="margin-top:15px">
                                                <tr class="heading" style="font-size:15px;border-bottom:1px solid black;border-top:1px solid;color:black ;padding-top:15px">
                                                    <th style="text-align: left;  border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;padding-top:0px !important;padding-bottom:0px !important">Description</th>
                                                    <th style="text-align: right;  border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;padding-top:0px !important;padding-bottom:0px !important ">Price</th>
                                                    <th style="text-align: left; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;padding-top:0px !important;padding-bottom:0px !important">Gst.Rate</th>
                                                    <th style="text-align: center;  border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;padding-top:0px !important;padding-bottom:0px !important ">Qty</th>
                                                    <th style="text-align: center;  border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;padding-top:0px !important;padding-bottom:0px !important">GST</th>
                                                    <th style="text-align: right; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;padding-top:0px !important;padding-bottom:0px !important">Total</th>
                                                </tr>

                                                <tr style="font-size:15px;padding-top:20px">
                                                    <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">Hand Sanitizer Small</td>
                                                    <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">78</td>
                                                    <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">6</td>

                                                    <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">5</td>
                                                    <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">565</td>
                                                    <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">4565</td>
                                                </tr>
                                                <tr style="font-size:15px;padding-top:20px">
                                                    <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">Hand Sanitizer Medium</td>
                                                    <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">78</td>
                                                    <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">6</td>

                                                    <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">5</td>
                                                    <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">565</td>
                                                    <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">4565</td>
                                                </tr>
                                                <tr style="font-size:15px;padding-top:20px">
                                                    <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">Hand Sanitizer Large</td>
                                                    <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">78</td>
                                                    <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">6</td>

                                                    <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">5</td>
                                                    <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">565</td>
                                                    <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">4565</td>
                                                </tr>


                                                <tr style="font-size:16px;border-top:1px solid #000000;border-color:black !important ">
                                                    <td colspan="4" style="text-align: right; border-bottom: 0; padding-bottom: 1px;   color: black;">Total  Amount:</td>
                                                    <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;">4565.00</td>
                                                </tr>
                                                <tr style="font-size:16px;">
                                                    <td colspan="4" style="text-align: right; border-bottom: 0; padding-bottom: 1px;   color: black;">Sale Tax:</td>
                                                    <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;">54655.00</td>
                                                </tr>
                                                <tr style="font-size:16px;">
                                                    <td colspan="4" style="text-align: right; border-bottom: 0; padding-bottom: 1px;   color: black;">Discount:</td>
                                                    <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;">5%</td>
                                                </tr>
                                                <tr style="font-size:16px;">
                                                    <td colspan="4" style="text-align: right; border-bottom: 0; padding-bottom: 1px;   color: black;">Payable:</td>
                                                    <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;">4353454.00</td>
                                                </tr>
                                                <tr style="font-size:16px;">
                                                    <td colspan="4" style="text-align: right; border-bottom: 0; padding-bottom: 1px;   color: black;">Received:</td>
                                                    <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;">453455.00</td>
                                                </tr>
                                                <tr style="font-size:16px;border-bottom:1px solid #000000;border-color:black !important ">
                                                    <td colspan="4" style="text-align: right; border-bottom: 0; padding-bottom: 1px;   color: black;">Customer Balance:</td>
                                                    <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;">453454.00</td>
                                                </tr>


                                            </table>
                                        </div>
                                    </div>
                                    <div style="width:400px;border-top:1px solid #000000;border-color:black !important">

                                        <div style="text-align:center">
                                            <p style="font-size:20px;color:black;font-weight:bold">FBR Invoice #</p>
                                            <p style="font-size:20px;color:black;font-weight:bold">485903894580934858340</p>

                                        </div>
                                        <div style="margin-top:15px;display:flex">
                                            <div style="width:150px;text-align:left;width:60%;">
                                                <img src="/FBRPOS.png" class="mx-auto d-block" style="width:100px;text-align:right;width:50%;" />

                                            </div>
                                            <div style="width:150px;text-align:right;width:40%;">
                                                <vue-qrcode v-bind:value="qrValue" style="width: 150px;" v-bind:errorCorrectionLevel="correctionLevel" />

                                            </div>

                                        </div>
                                        <div style="font-size:20px;color:black;margin:5px">
                                            <p style="font-size:20px;color:black">Verify this invoice through FBR TaxAsan Mobile App or Sms at 9966 and win exciting prize in draw.</p>

                                        </div>
                                    </div>




                                </div>

                            </div>

                        </div>
                        <div v-if="printTemplate=='SA – Template 1 Thermal' || printTemplate=='السـعـوديـة - الــقـالـب 1 حراري'" class="col-lg-6 ml-auto mr-auto">
                            <div>
                                <div ref="mychildcomponent" id='purchaseInvoice' style="width:400px;z-index:-9999">
                                    <!--INFORMATION-->
                                    <div style="width:400px;margin-left:20px;">
                                        <div style="width:100%;text-align:center;margin-top:10px">
                                            <img src="Cart.png" class="rounded-circle" style="width: auto; max-width: 300px; height: auto; max-height: 120px;">
                                        </div>
                                        <div style="text-align: center;margin-bottom:15px;">
                                            <span style="font-size:24px;font-weight:bold;color:black;">{{headerFooters.company.companyNameEnglish}}</span><br />

                                        </div>

                                        <div style="width:100%;">
                                            <div style="text-align: center;">
                                                <span style="font-size:24px;font-weight:bold;color:black;">{{headerFooters.company.companyRegNo}}: رقم تسجيل الشركة  </span><br />
                                                <span style="font-size:18px;font-weight:bold;color:black;">{{headerFooters.company.vatRegistrationNo}}:الرقم الضريبي</span><br />
                                                <span style="font-size:18px;font-weight:bold;color:black;">حي الحرازات الشارع العام</span><br />
                                                <span style="font-size:18px;font-weight:bold;color:black;">{{list.registrationNo}} :رقم الحركة</span><br />

                                            </div>

                                            <table class="table table-borderless " style="width:400px;">
                                                <tr style="font-size:16px;">
                                                    <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;text-align:left;padding-bottom:0px !important">CC-001: رقم الكاونتر</td>
                                                    <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;padding-bottom:0px !important">{{employeeName}}: أسم المستخد </td>
                                                </tr>
                                                <tr style="font-size:16px;" v-if="list.cashCustomer != null">
                                                    <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;text-align:left;padding-top:0px !important;padding-bottom:0px !important">24/02/2022 12:59:17 PM</td>
                                                    <td class="" style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;padding-top:0px !important;padding-bottom:0px !important">{{list.cashCustomer}}: اسم العميل </td>
                                                </tr>
                                                <tr style="font-size:18px;" v-else>
                                                    <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;text-align:left">24/02/2022 12:59:17 PM </td>
                                                    <td class="" style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? (list.customerNameEn != '' && list.customerNameEn != null) ? list.customerNameEn : list.customerNameAr : (list.customerNameAr != '' && list.customerNameAr != null) ? list.customerNameAr : list.customerNameEn}}: اسم العميل</td>
                                                </tr>
                                            </table>


                                        </div>


                                        <div>
                                            <table class="table report-tbl" style="margin-top:15px;width:100%;">
                                                <tr class="heading" style="font-size:15px;border-bottom:1px solid black;border-top:1px solid; color:black">
                                                    <th style="text-align: right; width: 20%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">الإجمالي </th>
                                                    <th style="text-align: right; width: 20%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">قيمة الضريبة </th>
                                                    <th style="text-align: right; width: 20%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">السعر</th>
                                                    <th style="text-align: right; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">الكمية</th>
                                                    <th style="text-align: center; width: 20%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">الصنف</th>
                                                    <th style="text-align: right; width: 5%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">رقم </th>

                                                </tr>
                                                <template>
                                                    <tr style="font-size:15px;">

                                                        <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">600</td>
                                                        <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">560</td>
                                                        <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">16</td>

                                                        <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">2</td>

                                                        <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;">PR-001</td>
                                                        <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">1</td>


                                                    </tr>
                                                    <tr style="font-size:14px;"><td colspan="7" style="text-align: right; border-top: 0; padding-top: 0px !important;padding-bottom:0px !important; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;">10-6 فارمل هندي رجالي جزمGENTS SHOES INDIAN 6-10 (607BL </td></tr>
                                                    <tr style="font-size:15px;">

                                                        <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">600</td>
                                                        <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">560</td>
                                                        <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">16</td>

                                                        <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">2</td>

                                                        <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;">PR-002</td>
                                                        <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">2</td>


                                                    </tr>
                                                    <tr style="font-size:14px;"><td colspan="7" style="text-align: right; border-top: 0; padding-top: 0px !important;padding-bottom:0px !important; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;">11-6 فارمل هندي رجالي جزمGENTS SHOES INDIAN 6-12 (608BL </td></tr>
                                                </template>



                                                <tr style="font-size:16px;border-top:1px solid #000000;border-color:black !important ">
                                                    <td colspan="2" style=" text-align:right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important">{{list.saleItems.filter(prod => prod.quantity>0).length}}</td>
                                                    <td colspan="4" style="text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important">عدد العنف</td>

                                                </tr>
                                                <tr style="font-size:16px;">

                                                    <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important">{{calulateTotalExclVAT.toFixed(3).slice(0,-1)}}</td>
                                                    <td colspan="4" style="text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important"> الإجمالي</td>
                                                </tr>
                                                <tr style="font-size:16px;">

                                                    <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important">{{ calulateDiscountAmount.toFixed(3).slice(0,-1) }}</td>
                                                    <td colspan="4" style="text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important">قيمة الخصم</td>
                                                </tr>
                                                <tr style="font-size:16px;">

                                                    <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important">{{ ((calulateTotalExclVAT - calulateTotalInclusiveVAT) - calulateDiscountAmount).toFixed(3).slice(0,-1) }}</td>
                                                    <td colspan="4" style="text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important">الإجمالي بعد الخصم</td>
                                                </tr>
                                                <tr style="font-size:16px;">

                                                    <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important">{{ calulateTotalVAT.toFixed(3).slice(0,-1) }}</td>
                                                    <td colspan="4" style="text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important"> VAT 15% الضريبة</td>
                                                </tr>
                                                <tr style="font-size:16px;">

                                                    <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important">{{ (calulateNetTotal - calulateDiscountAmount).toFixed(3).slice(0,-1) }}</td>
                                                    <td colspan="4" style="text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important"> إجمالي مع ضريبةالقيمةالمضافة </td>
                                                </tr>








                                            </table>
                                        </div>
                                    </div>
                                    <div style="width:400px;border-top:1px solid #000000;border-color:black !important">


                                        <div style="margin-top:15px;display:flex">
                                            <div style="width:400px;display:block;margin-left:20px;margin-top:1.5rem;text-align:center;">
                                                <vue-qrcode v-bind:value="qrValue" style="width:200px" v-bind:errorCorrectionLevel="correctionLevel" />
                                                <p style="text-align:center;color:black;"><span style="margin-right:5px;color:black;">*</span> <span style="margin-right:5px;color:black;">*</span> <span style="margin-right:10px;color:black;">*</span> <span style="font-size:25px; font-weight:bold;color:black;">شـكـرا لــزيــارتـكـــ</span>  <span style="margin-left: 10px;color:black;">*</span> <span style="margin-left: 5px;color:black;">*</span> <span style="margin-left:5px;color:black;">*</span></p>
                                            </div>

                                        </div>

                                    </div>




                                </div>

                            </div>

                        </div>
                    </div>


                    </div>
                </div>
</modal>
    </div>

</template>

<script>
    import moment from "moment";
    
    import Vue3Barcode from 'vue3-barcode'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['printDetails', 'headerFooter', 'isTouchScreen', 'show', 'printTemplate', 'printSize'],
        components: {
            
            'barcode': Vue3Barcode,
        },
        data: function () {
            return {
                qrValue: "",
                emptyListCount: 9,
                indexCount: 3,
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
                listItemP3Summary: {
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
                listItemP3: [],
                listItemP4: [],
                itemTotal: 0,
                isHeaderFooter: 'true',
                invoicePrint: '',
                IsDeliveryNote: '',
                arabic: '',
                english: '',
                userName: '',
                isCalculationShow: true,
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
            close: function () {
                this.$emit('close');
            },
            calulateDiscountAmount1: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0)
            },
            calulateBundleAmount1: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
            },
            calulateNetTotalWithVAT: function () {
                var total = this.list.saleItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0);
                var grandTotal = parseFloat(total) - (this.calulateDiscountAmount1() + this.calulateBundleAmount1())
                return (parseFloat(grandTotal).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            calulateTotalVATofInvoice: function () {
                var total = this.list.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0);
                return (parseFloat(total).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
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
            
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.invoicePrint = localStorage.getItem('InvoicePrint');
            this.IsDeliveryNote = localStorage.getItem('IsDeliveryNote');
            this.userName = localStorage.getItem('FullName');
            if (this.printDetails.saleItems.length > 0) {
                this.list = this.printDetails;
                this.headerFooters = this.headerFooter;


                {
                   



                    this.list.date = moment().format('DD MMM YYYY');

                }

            }
        },

    }
</script>


