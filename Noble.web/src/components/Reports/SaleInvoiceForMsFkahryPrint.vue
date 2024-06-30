<template>

    <div ref="mychildcomponent" hidden id='purchaseInvoice' class="row" style="color:black !important">
        <!--page1-->
        <div v-if="itemTotal<=12">
            <!--HEADER-->
            <div class="col-md-12" style="height: 50mm; border: 1px solid #000000; border-radius: 8px;" v-if="isHeaderFooter=='true'">
                <div class="row" style="height:35mm">
                    <div class="col-md-6 ">
                        <table class=" text-left">
                            <tr>
                                <td>

                                    <p style="font-size:30px;color:black !important;font-weight:bold;margin:0px !important">{{headerFooters.company.nameEnglish}}</p>
                                    <p style="font-size: 18px; color: black !important; font-weight: bold; margin: 3px !important ">{{headerFooters.company.categoryEnglish}}</p>
                                    <p style="font-size: 16px; color: black !important; font-weight: bold; margin: 3px !important">{{headerFooters.company.addressEnglish}}</p>
                                    <p style="font-size: 16px; color: black !important; font-weight: bold; margin: 3px !important">VAT No.: {{headerFooters.company.vatRegistrationNo}}</p>


                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="col-md-6 ">
                        <table class="table text-right">
                            <tr>
                                <td>

                                    <p style="font-size:40px;color:black !important;font-weight:bold;margin:0px !important">{{headerFooters.company.nameArabic}}.</p>
                                    <p style="font-size: 18px; color: black !important; font-weight: bold; margin: 3px !important">{{headerFooters.company.categoryArabic}}</p>
                                    <p style="font-size: 16px; color: black !important; font-weight: bold; margin: 3px !important">{{headerFooters.company.addressArabic}}</p>
                                    <p style="font-size: 16px; color: black !important; font-weight: bold; margin: 3px !important">الرقم الضريبي:{{headerFooters.company.vatRegistrationNo}}</p>

                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="row ">
                    <div class="col-md-4"></div>
                    <div class="col-md-4" style=" border: 1px solid #000000; border-radius: 8px; background-color: #E0DCD9; height: 40px; margin-top: 28px">
                        <div style="margin: 0px; padding: 0px; font-weight:bold">
                            <!--<div style="text-align: center;padding-bottom:5px;" v-if="b2b && b2c">
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
                    </div>-->
                            <!--<div style="text-align: center;margin-bottom:15px;" v-else-if="b2b">
                        <span style="font-size:24px;color:black;">{{taxInvoiceLabel}} </span>
                        <span style="font-size:24px;color:black;">{{taxInvoiceLabelAr}} </span>
                    </div>-->

                            <div style="text-align: center">
                                <p style="font-size:24px;color:black;">فاتورة مبيعات لفدية </p>

                            </div>
                        </div>
                        <!--<p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                    <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">
                        {{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Sale Invoice ' : 'فاتورة البيع' }}
                        <span v-if="list.invoiceType==0">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Hold)' : '(معلق)' }} </span>
                        <span v-if="list.partiallyInvoice=='Fully' && list.isCredit">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? '(Paid)' : '(مدفوعة)' }} </span>
                    </span>
                </p>-->
                    </div>
                    <div class="col-md-4"></div>

                </div>
            </div>

            <div style="height: 25mm; margin-top: 24px; border: 1px solid #000000; border-radius: 8px">
                <div class="row">
                    <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                        <div>
                            <!--Row 1-->
                            <div class="row" style="padding-top:5px">
                                <div class="col-md-4" style="padding-left:25px;padding-right:25px ">
                                    <div style="display: flex; background-color: #E0DCD9; border-radius: 8px; border: 1px solid #000000; ">
                                        <div style="width:70%; text-align:center;color:black !important;font-weight:bold;">1</div>
                                        <div style="width:30%;font-weight:bolder;font-size:15px !important;color:black !important;font-size:20px">:الصفحة</div>
                                    </div>

                                </div>

                                <div class="col-md-4">
                                    <div style="display: flex;  ">
                                        <div style="width:70%; text-align:center;color:black !important;font-weight:bold;"> </div>
                                        <div style="width:30%;font-weight:bolder;font-size:15px !important;color:black !important;font-size:20px">:رقم </div>
                                    </div>

                                </div>
                                <div class="col-md-4" style="padding-left:25px;padding-right:35px ">
                                    <div style="display: flex; background-color: #E0DCD9; border-radius: 8px; border: 1px solid #000000; ">
                                        <div style="width:70%; text-align:center;color:black !important;font-weight:bold;"> {{list.date}}</div>
                                        <div style="width:30%;font-weight:bolder;font-size:15px !important;color:black !important;font-size:20px;text-align:right">: التاريخ &nbsp;</div>
                                    </div>

                                </div>
                            </div>
                            <div class="row" style="padding-top:5px">
                                <div class="col-md-3" style="padding-left:25px;padding-right:25px ">
                                    <div style="display: flex; background-color: #E0DCD9; border-radius: 8px;height:40px; border: 1px solid #000000; ">
                                        <div style="width:100%; text-align:center;color:black !important;font-weight:bold;">&nbsp;<span v-if="list.cashCustomer != null">{{list.cashCustomerId}}</span> <span v-else>{{list.customerVat}}</span></div>
                                    </div>

                                </div>

                                <div class="col-md-2">
                                    <div style="display: flex;  ">
                                        <div style="width:100%;font-weight:bolder;font-size:15px !important;color:black !important;font-size:20px">:الرقم الضريبي للعميل </div>
                                    </div>

                                </div>
                                <div class="col-md-7
                                     " style="padding-left:25px;padding-right:35px ">
                                    <div style="display: flex;  border-radius: 8px; border: 1px solid #000000; ">
                                        <div style="width: 80%; text-align: center; color: black !important; font-weight: bold; height: 40px">
                                            <span v-if="list.cashCustomer != null">{{list.cashCustomer}}</span>
                                            <span v-else>{{invoicePrint == 'العربية'? list.customerNameAr : list.customerNameEn}}</span>
                                        </div>
                                        <div style="width:20%;font-weight:bolder;font-size:15px !important;color:black !important;font-size:20px;text-align:right">: العميل &nbsp;&nbsp;</div>
                                    </div>

                                </div>
                            </div>
                            <!--Row 2-->
                            <!--<div class="row">
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
                        <div class="col-md-6" style="display:flex;" v-if="headerFooters.customerTelephone">
                            <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Tel:</div>
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
                    <div class="col-md-8" style="display:flex;" v-if="headerFooters.customerVat">
                        <div style="width:20%; color:black !important;font-weight:bolder;text-align:right;">Cust VAT:</div>
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
                        <div class="col-md-12" style="display:flex;" v-if="headerFooters.customerAddress">
                            <div style="width:13%; font-weight:bolder;text-align:right; color:black !important;"><span>Address</span>:</div>
                            <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">{{list.customerAddressWalkIn}}</div>
                            <div style="width:76%;text-align:center;font-weight:bold;color:black !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">{{list.customerAddressWalkIn}}</div>
                            <div style="width:11%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="headerFooters.customerAddress">: عنوان</div>
                        </div>
                    </div>-->
                        </div>
                    </div>
                </div>
            </div>

            <!--INFORMATION-->
            <div style="height:205mm;border:2px solid !important;">
                <div class="row ">
                    <div class="col-md-12 ">
                        <table class="table">
                            <tr class="heading" style="font-size:15px;">
                                <th style="padding-left: 1px; padding-right: 1px; width: 1%; padding-top: 3px !important; padding-bottom: 3px !important; border-bottom: 0px !important; border-right: 1px solid #000000 !important;">#</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 6%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="headerFooters.styleNo">Model/ <br />Style</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 48%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">Product Name</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 5%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="headerFooters.itemPieceSize">Pack size</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 5%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">Qty</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 7%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="isMultiUnit=='true'">Total Qty</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 7%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">U.Price</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 7%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">Total Price</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 5%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="!list.isDiscountOnTransaction">Disc</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 4%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">Tax%</th>
                                <th class="text-right" style="padding-left: 1px; padding-right: 1px; width: 11%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-bottom: 0px !important; border-left: 1px solid #000000 !important;">Grand  <br />Total</th>
                            </tr>
                            <tr class="heading" style="font-size:15px !important;padding-top:5px;border-bottom:1px solid #000000 !important;">
                                <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important;border-right:1px solid #000000 !important;"></th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 6%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="headerFooters.styleNo">رقم الموديل</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 48%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">اسم الصنف</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 5%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="headerFooters.itemPieceSize">حجم </th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 5%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">الكمية </th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 7%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="isMultiUnit=='true'">إجمالي الكمية </th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 7%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">سعرالوحدة</th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 7%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">الاجمالي </th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 5%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;" v-if="!list.isDiscountOnTransaction">الخصم </th>
                                <th class="text-center" style="padding-left: 1px; padding-right: 1px; width: 4%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important; border-right: 1px solid #000000 !important;">%الضريية</th>
                                <th class="text-right" style="padding-left: 1px; padding-right: 1px; width: 11%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border-top: 0px !important; border-left: 1px solid #000000 !important;">المجموع الإجمالي </th>
                            </tr>



                            <tr style="font-size:17px;font-weight:bold;" v-for="(item, index) in list.saleItems" v-bind:key="item.id">
                                <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;border-right:1px solid #000000 !important;">{{index+1}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;" v-if="headerFooters.styleNo">{{item.product.styleNumber}}</td>
                                <td class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;"><p style="margin-bottom: 0;text-align:right;">{{item.arabicName}}</p>  <p style="margin-bottom: 0;font-size:13px;">{{item.productName}}</p> </td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;" v-if="headerFooters.itemPieceSize">{{item.product.width}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.newQuantity }}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;" v-else>{{item.quantity }}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;" v-if="isMultiUnit=='true'">{{item.quantity }}</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;">{{item.total.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;" v-if="!list.isDiscountOnTransaction">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;">{{item.taxRate.toFixed(3).slice(0,-1) }}</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;border-left:1px solid #000000 !important;">{{(((item.total - (item.discountAmount+item.bundleAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
                            </tr>
                            <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in emptyListCount" v-bind:key="index">
                                <td class="text-left" style="border-top:0 !important; border-bottom:0 !important;color:transparent !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;">{{index+1+indexCount}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;" v-if="headerFooters.styleNo"></td>
                                <td class="text-left" style="border-top:0 !important; border-bottom:0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;"></td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;" v-if="headerFooters.itemPieceSize"></td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;" v-if="isMultiUnit=='true'"></td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;" v-else></td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;" v-if="isMultiUnit=='true'"></td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;"></td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;"></td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; color:black !important;border-left:1px solid #000000 !important;border-right:1px solid #000000 !important;padding-top: 18px !important; padding-bottom: 18px !important;" v-if="!list.isDiscountOnTransaction"></td>
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
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{parseFloat(calulateTotalExclVAT ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((calulateNetTotal - calulateDiscountAmount))   }}</td>
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
                                {{ $t('SaleInvoiceDotMatrixPrint.ReceivedBy') }}::
                            </td>
                            <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                {{ $t('SaleInvoiceDotMatrixPrint.ApprovedBy') }}::
                            </td>
                            <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                {{ $t('SaleInvoiceDotMatrixPrint.PreparedBy') }}::
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
    import axios from 'axios'
    //import Vue3Barcode from 'vue3-barcode'
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
        mixins: [clickMixin],
        props: ['printDetails', 'headerFooter', 'isTouchScreen'],
      
        data: function () {
            return {
                b2b: false,
                b2c: false,
                taxInvoiceLabel: "",
                taxInvoiceLabelAr: '',
                simplifyTaxInvoiceLabel: '',
                simplifyTaxInvoiceLabelAr: '',
                emptyListCount: 0,
                indexCount: 0,
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
            calulateDiscountAmount: function () {
                var totalIncDisc = 0;
                if (this.list.isDiscountOnTransaction) {
                    var withDisc = this.list.saleItems.reduce(function (a, c) { return a + (c.isFree ? 0 : c.total - c.discountAmount) }, 0)

                    var discountForInclusiveVat = parseFloat(this.list.saleItems
                        .reduce((vat, prod) => (vat + (prod.isFree ? 0 : ((this.list.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(prod.total) * prod.taxRate) / (100 + prod.taxRate) : 0))), 0).toFixed(3).slice(0, -1))

                    totalIncDisc = ((this.list.isBeforeTax && this.list.isDiscountOnTransaction) ? (this.list.taxMethod == ("Inclusive" || "شامل") ? (parseFloat(this.list.transactionLevelDiscount) * (withDisc - discountForInclusiveVat) / 100) : (this.list.isFixed ? parseFloat(this.list.transactionLevelDiscount) : parseFloat(this.list.transactionLevelDiscount) * withDisc / 100)) : (this.list.isFixed ? parseFloat(this.list.transactionLevelDiscount) : (parseFloat(withDisc) + (this.list.taxMethod == ("Inclusive" || "شامل") ? 0 : parseFloat(this.calulateTotalVAT))) * parseFloat(this.list.transactionLevelDiscount) / 100)).toFixed(3).slice(0, -1)

                }
                else {
                    totalIncDisc = this.list.saleItems.reduce(function (a, c) { return a + (c.isFree ? 0 : c.discountAmount) }, 0)
                }

                return totalIncDisc;
            }
        },
        methods: {

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

                        root.$router.go({
                            path: '/addSale',
                            value: true
                            
                        });
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
                        this.emptyListCount = 12 - totalItem;
                        this.indexCount = 12 - this.emptyListCount;
                        for (var i = 0; i < totalItem; i++) {
                            root.listItemP1.push(root.printDetails.saleItems[i]);
                        }
                    }
                    else if (totalItem > 12 && totalItem <= 24) {
                        
                        this.emptyListCount = 24 - totalItem;
                        this.indexCount = 24 - this.emptyListCount;

                        for (var k = 0; k < totalItem; k++) {
                            if (k <= 11) {
                                root.listItemP1.push(root.printDetails.saleItems[k]);
                            }
                            else {
                                root.listItemP2.push(root.printDetails.saleItems[k]);
                            }
                        }
                    }
                    else if (totalItem > 24 && totalItem <= 36) {
                        this.emptyListCount = 36 - totalItem;
                        this.indexCount = 36 - this.emptyListCount;

                        for (var j = 0; j < totalItem; j++) {
                            if (j <= 11) {
                                root.listItemP1.push(root.printDetails.saleItems[j]);
                            }
                            else if (j > 11 && j <= 23) {
                                root.listItemP2.push(root.printDetails.saleItems[j]);
                            }
                            else {
                                root.listItemP3.push(root.printDetails.saleItems[j]);
                            }
                        }
                    }
                    else if (totalItem > 36 && totalItem <= 48) {
                        this.emptyListCount = 48 - totalItem;
                        this.indexCount = 48 - this.emptyListCount;

                        for (var l = 0; l < totalItem; l++) {
                            if (l <= 11) {
                                root.listItemP1.push(root.printDetails.saleItems[l]);
                            }
                            else if (l > 11 && l <= 23) {
                                root.listItemP2.push(root.printDetails.saleItems[l]);
                            }
                            else if (l > 23 && l <= 35) {
                                root.listItemP3.push(root.printDetails.saleItems[l]);
                            }
                            else {
                                root.listItemP4.push(root.printDetails.saleItems[l]);
                            }
                        }
                    }
                    /*summary calculate listItemP1Summary*/
                    root.listItemP1Summary.calulateTotalQty = root.listItemP1.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0);

                    root.listItemP1Summary.calulateNetTotal = root.listItemP1.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0);

                    root.listItemP1Summary.calulateTotalExclVAT = root.listItemP1.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0);

                    root.listItemP1Summary.calulateTotalVAT = root.listItemP1.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0);

                    root.listItemP1Summary.calulateTotalInclusiveVAT = root.listItemP1.reduce(function (a, c) { return a + Number((c.inclusiveVat) || 0) }, 0);

                    root.listItemP1Summary.calulateDiscountAmount = root.listItemP1.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0);

                    root.listItemP1Summary.calulateBundleAmount = root.listItemP1.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0);

                    /*summary calculate listItemP2Summary*/
                    root.listItemP2Summary.calulateTotalQty = root.listItemP2.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0);

                    root.listItemP2Summary.calulateNetTotal = root.listItemP2.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0);

                    root.listItemP2Summary.calulateTotalExclVAT = root.listItemP2.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0);

                    root.listItemP2Summary.calulateTotalVAT = root.listItemP2.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0);

                    root.listItemP2Summary.calulateTotalInclusiveVAT = root.listItemP2.reduce(function (a, c) { return a + Number((c.inclusiveVat) || 0) }, 0);

                    root.listItemP2Summary.calulateDiscountAmount = root.listItemP2.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0);

                    root.listItemP2Summary.calulateBundleAmount = root.listItemP2.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0);

                    /*summary calculate listItemP3Summary*/
                    root.listItemP3Summary.calulateTotalQty = root.listItemP3.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0);

                    root.listItemP3Summary.calulateNetTotal = root.listItemP3.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0);

                    root.listItemP3Summary.calulateTotalExclVAT = root.listItemP3.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0);

                    root.listItemP3Summary.calulateTotalVAT = root.listItemP3.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0);

                    root.listItemP3Summary.calulateTotalInclusiveVAT = root.listItemP3.reduce(function (a, c) { return a + Number((c.inclusiveVat) || 0) }, 0);

                    root.listItemP3Summary.calulateDiscountAmount = root.listItemP3.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0);

                    root.listItemP3Summary.calulateBundleAmount = root.listItemP3.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0);

                    this.list.date = moment(this.list.date).format('DD MMM YYYY');
                    setTimeout(function () {
                        root.printInvoice();
                    }, 125)
                }

            }
        },

    }
</script>

