<template>
    <div hidden  >
        <div ref="mychildcomponent" id='purchaseInvoice' style="width:400px;z-index:-9999">
            <!--INFORMATION-->
            <div style="width:400px;margin-left:20px;">
                <div style="text-align: center;margin-bottom:15px;" v-if="b2b && b2c">
                    <span style="font-size:24px;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Invoice Type (Ar)'))">{{simplifyTaxInvoiceLabelAr}}<br /></span>
                    <span style="font-size:24px;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Invoice Type (Eng)'))">{{simplifyTaxInvoiceLabel}}<br /></span>

                </div>
                <div style="text-align: center;margin-bottom:15px;" v-else-if="b2b">
                    <span style="font-size:24px;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Invoice Type (Ar)'))">{{taxInvoiceLabelAr}}<br /></span>
                    <span style="font-size:24px;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Invoice Type (Eng)'))">{{taxInvoiceLabel}}<br /></span>


                </div>
                <div style="text-align: center;margin-bottom:15px;" v-else-if="b2c">
                    <span style="font-size:24px;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Invoice Type (Ar)'))">{{simplifyTaxInvoiceLabelAr}}<br /></span>
                    <span style="font-size:24px;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Invoice Type (Eng)'))">{{simplifyTaxInvoiceLabel}}<br /></span>

                </div>
                <div style="width:100%;text-align:center;margin-top:10px" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Print Logo')) ">
                    <img :src="headerFooters.company.logoPath" style="width: auto; max-width: 150px; height: auto; max-height: 120px;">

                </div>
                <div style="text-align: center;margin-bottom:3px;">
                    <div style="text-align: center;">
                        <span style="font-size:30px;font-weight:bold;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Business Name (Ar)'))">{{headerFooters.company.nameArabic}}<br /></span>
                        <span style="font-size:30px;font-weight:bold;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Business Name (Eng)'))">{{headerFooters.company.nameEnglish}}<br /></span>
                        <span style="font-size:18px;font-weight:bold;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Business Type (Ar)'))   && headerFooters.company.categoryArabic!='null'">{{headerFooters.company.categoryArabic}}<br /></span>
                        <span style="font-size:18px;font-weight:bold;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Business Type (Eng)') ) && headerFooters.company.categoryEnglish!='null'">{{headerFooters.company.categoryEnglish}}<br /></span>
                        <span style="font-size:18px;font-weight:bold;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Welcome/Tagline (Ar)')) ">{{headerFooters.welcomeLineAr}}<br /></span>
                        <span style="font-size:18px;font-weight:bold;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Welcome/Tagline (Eng)'))">{{headerFooters.welcomeLineEn}}<br /></span>
                        <span v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Management No (Eng)')) || PrintOption(headerFooters.printOptions.find(x => x.label=='Management No (Ar)'))" style="font-size:15px;color:black;"><span v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Management No (Eng)'))">Management No: </span> {{headerFooters.managementNo}}<span v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Management No (Ar)'))"> :رقم الإدارة</span> <br /></span>
                        <span v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Store Contact No (Eng)')) || PrintOption(headerFooters.printOptions.find(x => x.label=='Store Contact No (Ar)'))" style="font-size:15px;color:black;"><span v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Store Contact No (Eng)'))">Store Contact Number: </span> {{headerFooters.contactNo}}<span v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Store Contact No (Ar)'))"> :الـرقم الإدارة / المحل</span> <br /></span>

                        <p style="border:1px solid; width:350px;color:black;margin-left:auto;margin-right:auto;margin-top:0.1rem;margin-bottom:0.3rem;">
                            <span style="font-weight: bold;"><span style="font-weight: bold;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='VAT / Tax ID (Eng)'))">VAT No :</span> {{headerFooters.company.vatRegistrationNo}}<span style="font-weight: bold;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='VAT / Tax ID (Ar)'))"> :الرقم الضريبي</span>    <br /></span>
                            <span style="font-weight: bold;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='CR Number (Eng)')) || PrintOption(headerFooters.printOptions.find(x => x.label=='CR Number (Ar)'))"><span style="font-weight: bold;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='CR Number (Eng)'))">CR Number  :</span> {{headerFooters.company.companyRegNo}}<span style="font-weight: bold;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='CR Number (Ar)'))"> :الرقـم السـجـل الـتـجـارى </span>    <br /></span>

                            <span style="font-size:18px;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Company Name (Ar)'))">{{headerFooters.company.companyNameArabic}}<br /></span>
                            <span style="font-size:18px;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Company Name (Eng)'))">{{headerFooters.company.companyNameEnglish}}<br /></span>
                            
                            <span style="font-size:16px;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Company Address (Ar)'))">{{headerFooters.company.addressArabic}}<br /></span>
                            <span style="font-size:16px;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Company Address (Eng)'))">{{headerFooters.company.addressEnglish}}<br /></span>
                        </p>

                        <p style="font-size:16px;color:black;" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Business Address (Arabic)')) || PrintOption(headerFooters.printOptions.find(x => x.label=='Business Address (English)'))">
                            <span v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Business Address (Arabic)'))">{{headerFooters.businessAddressArabic}}</span> <br />
                            <span v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Business Address (English)'))">{{headerFooters.businessAddressEnglish}}</span>
                        </p>
                    </div>

                </div>

                <div style="width:100%; margin-top:15px;">
                    <div style="text-align: center;">
                        <p v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Invoice Number(Eng)')) && PrintOption(headerFooters.printOptions.find(x => x.label=='Invoice Number(Ar)'))" style="font-size: 15px; font-weight: bold;color:black;margin-bottom:2px;padding-bottom:2px;"><span> Invoice No.</span> <span style="border:4px solid;font-size:25px;font-weight:bold;padding-left:2px;padding-right:2px;color:black;text-align:center"> {{list.registrationNo}}</span> <span style="font-size: 15px; font-weight: bold;color:black;margin-bottom:2px;padding-bottom:2px;text-align:right">رقم الفاتورة</span></p>
                        <p  v-else-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Invoice Number(Eng)'))" style="font-size: 15px; font-weight: bold;color:black;margin-bottom:2px;padding-bottom:2px;"><span> Invoice No.</span> <span style="border:4px solid;font-size:25px;font-weight:bold;padding-left:2px;padding-right:2px;color:black;text-align:center"> {{list.registrationNo}}</span> </p>
                        <p v-else-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Invoice Number(Ar)'))" style="font-size: 15px; font-weight: bold;color:black;margin-bottom:2px;padding-bottom:2px;"><span style="border:4px solid;font-size:25px;font-weight:bold;padding-left:2px;padding-right:2px;color:black;text-align:center"> {{list.registrationNo}}</span> <span style="font-size: 15px; font-weight: bold;color:black;margin-bottom:2px;padding-bottom:2px;text-align:right">رقم الفاتورة</span></p>

                        <p v-else style="font-size: 15px; font-weight: bold;color:black;margin-bottom:2px;padding-bottom:2px;"><span style="border:4px solid;font-size:25px;font-weight:bold;padding-left:2px;padding-right:2px;color:black;text-align:center"> {{list.registrationNo}}</span> </p>
                        <barcode v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Print Barcode'))" :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>

                    </div>

                    <table class="table table-borderless " style="width:400px;">
                        <tr style="font-size:16px;">
                            <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;text-align:right;padding-bottom:0px !important" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Counter Number (Ar)'))">{{list.counterCode}}: رقم الكاونتر</td>
                            <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;padding-bottom:0px !important" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='User Name (Ar)'))">{{list.userName}}: أسم المستخدم </td>
                        </tr>
                        <tr style="font-size:16px;" v-if="list.cashCustomer != null">
                            <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;text-align:left;padding-top:0px !important;padding-bottom:0px !important">{{getDate(list.date)}}:</td>
                            <td class="" style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;;padding-top:0px !important;padding-bottom:0px !important">{{list.cashCustomer}}: اسم العميل </td>
                        </tr>
                        <tr style="font-size:18px;" v-else>
                            <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;text-align:left">{{getDate(list.date)}}</td>
                            <td class="" style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? (list.customerNameEn != '' && list.customerNameEn != null) ? list.customerNameEn : list.customerNameAr : (list.customerNameAr != '' && list.customerNameAr != null) ? list.customerNameAr : list.customerNameEn}}: اسم العميل</td>
                        </tr>
                    </table>


                </div>


                <div style="width:100%;">
                    <table class="table report-tbl" style="width:400px;margin-top:15px">
                        <tr class="heading" style="font-size:15px;border-bottom:1px solid black;border-top:1px solid; color:black">
                            <th style="text-align: right; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">الإجـمـالي </th>
                            <th style="text-align: right; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;"> الضـريبـة </th>
                            <th style="text-align: right; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">السـعـر</th>
                            <th style="text-align: center; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">الكـمـية</th>
                            <th style="text-align: right; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">الصـنـف</th>
                            <th style="text-align: right; width: 5%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">رقـم </th>

                        </tr>
                        <span v-for="(item, index) in list.saleItems" v-bind:key="item.id">
                            <tr style="font-size:15px;">

                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{(item.total).toFixed(3).slice(0,-1)}}</td>
                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{(item.vatAmount).toFixed(3).slice(0,-1)}}</td>
                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>

                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{item.quantity }}</td>

                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black; ">{{item.code}}</td>
                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{index+1}}</td>


                            </tr>
                            <tr style="font-size:14px;" v-if="headerFooters.invoicePrint=='English & Arabic'"><td colspan="7" style="text-align: right; border-top: 0; padding-top: 0px !important;padding-bottom:0px !important; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;">{{item.arabicName}} &nbsp; &nbsp; &nbsp; {{item.productName }}</td></tr>
                            <tr style="font-size:14px;" v-else-if="headerFooters.invoicePrint=='العربية'"><td colspan="7" style="text-align: right; border-top: 0; padding-top: 0px !important;padding-bottom:0px !important; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;">{{item.arabicName}}</td></tr>
                            <tr style="font-size:14px;" v-else-if="headerFooters.invoicePrint=='English'"><td colspan="7" style="text-align: right; border-top: 0; padding-top: 0px !important;padding-bottom:0px !important; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;">{{item.productName }}</td></tr>
                            <tr style="font-size:14px;" v-else><td colspan="7" style="text-align: right; border-top: 0; padding-top: 0px !important;padding-bottom:0px !important; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;">{{item.arabicName}} &nbsp; &nbsp; &nbsp; {{item.productName }}</td></tr>
                        </span>



                        <tr style="font-size:16px;border-top:1px solid #000000;border-color:black !important ">
                            <td colspan="1" style=" text-align:right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important;font-weight:bold;text-align:center">{{list.saleItems.filter(prod => prod.quantity>0).length}}</td>
                            <td colspan="5" style="text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important">عدد الصـنـف</td>

                        </tr>
                        <tr style="font-size:16px;">

                            <td colspan="1" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important;font-weight:bold;text-align:center">{{calulateNetTotal.toFixed(3).slice(0,-1)}}</td>
                            <td colspan="5" style="text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important"> Total without VAT / Tax  الإجمالي بدون الضريبة</td>
                        </tr>
                        <tr style="font-size:16px;">

                            <td colspan="1" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important;font-weight:bold;text-align:center">{{ calulateDiscountAmount.toFixed(3).slice(0,-1) }}</td>
                            <td colspan="5" style="text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important">الـخـصـم</td>
                        </tr>
                        <tr style="font-size:16px;">

                            <td colspan="1" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important;font-weight:bold;text-align:center">{{ ((calulateNetTotal) - calulateDiscountAmount).toFixed(3).slice(0,-1) }}</td>
                            <td colspan="5" style="text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important">Total after Discount  المجموع بعد الخصم</td>
                        </tr>
                        <tr style="font-size:16px;">

                            <td colspan="1" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important;font-weight:bold;text-align:center">{{ calulateTotalVAT.toFixed(3).slice(0,-1) }}</td>
                            <td colspan="5" style="text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important"> Total VAT / Tax   إجمالي الضريبة</td>
                        </tr>
                        <tr style="font-size:16px;">

                            <td colspan="1" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;padding-top:0px !important;font-weight:bold;text-align:center">{{ (calulateTotalInclusiveVAT).toFixed(3).slice(0,-1) }}</td>
                            <td colspan="5" style="text-align: left; border-bottom: 0; padding-bottom: 1px;   color: black;border-left:1px solid;padding-top:0px !important"> Total with VAT / Tax   الإجمالي مع  الضريبة </td>
                        </tr>








                    </table>
                </div>
            </div>
            <div style="width:400px;border-top:1px solid #000000;border-color:black !important">

               
                <div style="margin-top:15px;display:flex">
                    <div style="width:400px;display:block;margin-left:20px;margin-top:1.5rem;text-align:center;">
                        <vue-qrcode v-bind:value="qrValue" style="width:200px" v-bind:errorCorrectionLevel="correctionLevel" v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Print QR'))" />
                        <p v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Gratitude Message (Ar)'))" style="text-align:center;color:black;"> <span style="margin-right:5px;color:black;">*</span> <span style="margin-right:10px;color:black;">*</span> <span style="font-size:25px; font-weight:bold;color:black;">{{headerFooters.closingLineAr}}</span>  <span style="margin-left: 10px;color:black;">*</span> <span style="margin-left: 5px;color:black;">*</span> </p>
                        <p v-if="PrintOption(headerFooters.printOptions.find(x => x.label=='Gratitude Message (Eng)'))" style="text-align:center;color:black;"> <span style="font-size:25px; font-weight:bold;color:black;">{{headerFooters.closingLineEn}}</span>   </p>
                    </div>

                </div>
              
            </div>

            
           

        </div>

    </div>
</template>

<script>
    import axios from 'axios'
    import moment from "moment";
    
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
        windowTitle: window.document.title
    }
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['printDetail', 'headerFooter', 'isTouchScreen', 'isDoublePrint', 'saleId', 'saleReturnRegNo'],
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
                totalReturnAmount: 0,
                totalInvoiceAmount: 0,
                totalNewInvoiceAmount: 0,
                totalReturnInvoiceAmount: 0,
                totalPaidAmount: 0,
                isCalculationShow: false,
                counterCode: "",
                qrValue: "",
                qrValueReturn: "",
                correctionLevel: "H",
                printInterval: '',
                jsonData: '',
                list: {
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
                returnList: {
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
                    paymentTypes: [],
                    saleItems:
                        [

                        ],
                    returnList: ''
                },
                render: 0,
                CompanyID: '',
                UserID: '',
                employeeId: '',
                employeeName: '',
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                htmlData: {
                    htmlString: ''
                },
                htmlDataReturn: {
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
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0)
            },
            calulateTotalExclVAT: function () {
                return this.list.saleItems.redcalulateTotalInclusiveVATuce(function (a, c) { return a + Number((c.total-c.inclusiveVat) || 0) }, 0)
            },
            calulateTotalVAT: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.vatAmount) || 0) }, 0)
            },
            calulateTotalInclusiveVAT: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number(((c.total - c.discountAmount) + c.vatAmount) || 0) }, 0)
            },
            calulateDiscountAmount: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.discountAmount) || 0) }, 0)
            },
            calulateBundleAmount: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
            },
            calulateTotalQtyR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0)
            },
            calulateNetTotalR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0)
            },
            calulateTotalExclVATR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0)
            },
            calulateTotalVATR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0)
            },
            calulateTotalInclusiveVATR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.inclusiveVat) || 0) }, 0)
            },
            calulateDiscountAmountR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.discountAmount) || 0) }, 0)
            },
            calulateBundleAmountR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
            }
        },
        methods: {
            getDate: function (x) {
                return moment(x).format('lll')
            },
            PrintOption: function (x) {
                if (x == undefined || !x.value) {

                    return false;
                }
                return true;
            },
            calulateDiscountAmount1: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.discountAmount) || 0) }, 0)
            },
            calulateBundleAmount1: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
            },
            calulateNetTotalWithVAT: function () {
                var total= this.list.saleItems.reduce(function (a, c) { return a + Number(((c.total - c.discountAmount) + c.vatAmount) || 0) }, 0);
                return parseFloat(total).toFixed(3).slice(0, -1);
            },

            calulateTotalVATofInvoice: function () {
                var total = this.list.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0);
                return (parseFloat(total).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            calulateNetTotalWithVATReturn: function () {
                var total = this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0);
                return (parseFloat(total).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            calulateTotalVATofInvoiceReturn: function () {
                var total = this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0);
                return (parseFloat(total).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            printInvoice: function () {
                var root = this;
                // this.$htmlToPaper('purchaseInvoice');
                this.htmlData.htmlString = this.$refs.mychildcomponent.innerHTML;
                //  var html1 = this.$refs.mychildcomponent.innerHTML;
                //  var data = { html: html1 }
                //
                var printerName = localStorage.getItem('PrinterName')
                var form = new FormData();
                form.append('htmlString', this.htmlData.htmlString);
                form.append('PrintType', 'invoice');
                form.append('PrinterName', printerName);
                //this.$htmlToPaper('purchaseInvoice');
                //axios.post('http://localhos:7013/print/from-pdf', form);
                //axios.post('http://127.0.0.1:7013/print/from-pdf', form);
                //alert();
                //var root = this;
                var isBlindPrint = localStorage.getItem("IsBlindPrint")

                if (isBlindPrint === 'true') {
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
            printReturnInvoice: function () {
                var root = this;

                // this.$htmlToPaper('purchaseInvoice');
                this.htmlDataReturn.htmlString = this.$refs.myReturnComponent.innerHTML;
                //  var html1 = this.$refs.mychildcomponent.innerHTML;
                //  var data = { html: html1 }
                //
                var printerName = localStorage.getItem('PrinterName')
                var form = new FormData();
                form.append('htmlString', this.htmlDataReturn.htmlString);
                form.append('PrintType', 'invoice');
                form.append('PrinterName', printerName);
                //this.$htmlToPaper('purchaseInvoice');
                //axios.post('http://localhos:7013/print/from-pdf', form);
                //axios.post('http://127.0.0.1:7013/print/from-pdf', form);
                //alert();
                //var root = this;
                var isBlindPrint = localStorage.getItem("IsBlindPrint")

                if (isBlindPrint === 'true') {
                    axios.post('http://127.0.0.1:7014/print/from-pdf', form).then(function (x) {
                        console.log(x);
                        if (root.isDoublePrint) {
                            root.printInvoice()


                        }
                    });
                    //if (root.isTouchScreen === true) {
                    //    root.$router.go('/TouchInvoice')
                    //}
                }
                else {
                    this.$htmlToPaper('saleReturn', options, () => {
                        if (root.isDoublePrint) {
                            root.printInvoice()


                        }
                        if (root.isTouchScreen === true) {
                            root.$router.go('/TouchInvoice')
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
            EmployeeData: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/EmployeeRegistration/EmployeeDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.employeeName = response.data.englishName;
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
            },
            saleReturnData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get("/Sale/SaleDetail?id=" + root.saleId, { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            
                            root.list = response.data;

                            //root.list.date = moment().format('DD/MM/YYYY hh:mm:ss A');
                            var sellerNameBuff = root.GetTLVForValue('1', root.headerFooters.company.nameEnglish)
                            var vatRegistrationNoBuff = root.GetTLVForValue('2', root.headerFooters.company.vatRegistrationNo)
                            var timeStampBuff = root.GetTLVForValue('3', root.list.date)
                            var totalWithVat = root.GetTLVForValue('4', root.calulateNetTotalWithVATReturn())
                            var totalVat = root.GetTLVForValue('5', root.calulateTotalVATofInvoiceReturn())
                            var tagArray = [sellerNameBuff, vatRegistrationNoBuff, timeStampBuff, totalWithVat, totalVat]
                            var qrCodeBuff = Buffer.concat(tagArray)
                            root.qrValue = qrCodeBuff.toString('base64')
                            if (root.isDoublePrint) {
                                if (root.list.paymentTypes[root.list.paymentTypes.length - 1].name.includes("SIR")) {
                                    root.totalPaidAmount = root.list.paymentTypes[root.list.paymentTypes.length - 1].amount
                                    root.isCalculationShow = true
                                }
                            }
                            root.PrintData();
                        }
                    });
            },
            PrintData: function () {
                var root = this;
                if (!this.printDetail.registrationNo.includes("SIR")) {
                    if (this.printDetail.saleItems.length > 0) {
                        this.list = this.printDetail;
                        if (root.list.paymentTypes.length > 0) {
                            
                            if (root.list.paymentTypes[root.list.paymentTypes.length - 1].name.includes("SIR")) {
                                root.totalPaidAmount = root.list.paymentTypes[root.list.paymentTypes.length - 1].amount
                                root.isCalculationShow = true
                            }
                        }
                        //this.list.date = moment().format('DD/MM/YYYY hh:mm:ss A');
                    


                            root.EmployeeData(root.employeeId);
                            var sellerNameBuff = root.GetTLVForValue('1', this.headerFooters.company.nameEnglish)
                            var vatRegistrationNoBuff = root.GetTLVForValue('2', this.headerFooters.company.vatRegistrationNo)
                            var timeStampBuff = root.GetTLVForValue('3', this.list.date)
                            var totalWithVat = root.GetTLVForValue('4', this.calulateNetTotalWithVAT())
                            var totalVat = root.GetTLVForValue('5', this.calulateTotalVATofInvoice())
                            var tagArray = [sellerNameBuff, vatRegistrationNoBuff, timeStampBuff, totalWithVat, totalVat]
                            var qrCodeBuff = Buffer.concat(tagArray)
                            root.qrValue = qrCodeBuff.toString('base64')


                        
                        setTimeout(function () {
                            root.printInvoice();

                        }, 125)

                    }
                }
                else {
                    if (root.printDetail.saleItems.length > 0) {
                        root.returnList = root.printDetail;
                        //root.returnList.date = moment().format('DD/MM/YYYY hh:mm:ss A');
                       
                            root.EmployeeData(root.employeeId);
                            var sellerNameBuffR = root.GetTLVForValue('1', root.headerFooters.company.nameEnglish)
                            var vatRegistrationNoBuffR = root.GetTLVForValue('2', root.headerFooters.company.vatRegistrationNo)
                            var timeStampBuffR = root.GetTLVForValue('3', root.returnList.date)
                            var totalWithVatR = root.GetTLVForValue('4', root.calulateNetTotalWithVATReturn())
                            var totalVatR = root.GetTLVForValue('5', root.calulateTotalVATofInvoiceReturn())
                            var tagArrayR = [sellerNameBuffR, vatRegistrationNoBuffR, timeStampBuffR, totalWithVatR, totalVatR]
                            var qrCodeBuffR = Buffer.concat(tagArrayR)
                            root.qrValueReturn = qrCodeBuffR.toString('base64')
                            setTimeout(function () {
                                root.printReturnInvoice();

                            }, 125)


                        
                    }
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
            
            var root = this;
            
            root.b2b = localStorage.getItem('b2b') == 'true' ? true : false;
            root.b2c = localStorage.getItem('b2c') == 'true' ? true : false;
            if (root.b2b && root.b2c && root.printDetail.customerCategory === 'B2B – Business to Business') {
                this.simplifyTaxInvoiceLabel = localStorage.getItem('taxInvoiceLabel');
                this.simplifyTaxInvoiceLabelAr = localStorage.getItem('taxInvoiceLabelAr');
            }
            else {
                this.simplifyTaxInvoiceLabel = localStorage.getItem('simplifyTaxInvoiceLabel');
                this.simplifyTaxInvoiceLabelAr = localStorage.getItem('simplifyTaxInvoiceLabelAr');
            }
            root.taxInvoiceLabel = localStorage.getItem('taxInvoiceLabel');
            this.taxInvoiceLabelAr = localStorage.getItem('taxInvoiceLabelAr');
            
            root.isCalculationShow = false
            root.counterCode = localStorage.getItem('CounterCode');
            root.CompanyID = localStorage.getItem('CompanyID');
            root.UserID = localStorage.getItem('UserID');
            root.employeeId = localStorage.getItem('EmployeeId');
            this.headerFooters = this.headerFooter;

            if (this.isDoublePrint) {
                root.saleReturnData();

            }
            else {
                root.PrintData()
            }





        },

    }
</script>

