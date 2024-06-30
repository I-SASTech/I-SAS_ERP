<template>
    <div>
        <div ref="mychildcomponent" hidden id='purchaseInvoice' class="col-md-12" style="color:black !important;background-color:#ffffff!important;">
            <div>
                <!--HEADER-->
                <div class="row" v-if="isHeaderFooter=='true'">
                    <div class="col-sm-8 col-md-8 col-lg-8 col-8">
                        <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px;">
                    </div>
                    <div class="col-sm-4 col-md-4 col-lg-4 col-4 text-right">
                        <p style="background-color:#F8880A !important;border-top-left-radius: 25px; font-size:30px;color:#ffffff !important;font-weight:bold;margin-bottom:0!important">{{headerFooters.company.nameArabic}}.</p>
                        <p style="background-color: #F8880A !important;border-bottom-left-radius: 25px; font-size: 25px; color: #ffffff !important; font-weight: bold;">{{headerFooters.company.nameEnglish}}</p>
                    </div>
                </div>
                <div v-else style="height:30mm;"></div>

                <div class="row mt-4">
                    <div class="col-sm-8 col-md-8 col-lg-8 col-8">
                        <p style="font-size:20px;color:black !important;font-weight:bold;margin-bottom:0!important;text-transform:capitalize;margin-bottom:20px;">WE ARE IMPROVING YOUR BUSINESS</p>
                        <p style="font-size:15px;color:black !important;margin-bottom:0!important"> <img src="/images/pin.png" style="width:auto;max-width:15px; max-height:15px;">  {{headerFooters.company.addressEnglish}}</p>
                        <p style="font-size:16px;color:black !important;margin-bottom:0!important">{{headerFooters.company.addressArabic}}</p>
                        <p style="font-size:16px;color:black !important;margin-bottom:0!important"><img src="/images/phone.png" style="width:auto;max-width:15px; max-height:15px;"> {{headerFooters.company.phoneNo}} &nbsp;&nbsp; Fax: {{headerFooters.company.phoneNo}}</p>
                    </div>

                    <div class="col-sm-4 col-md-4 col-lg-4 col-4 text-right">
                        <p v-if="list.invoiceType==4" style="font-size:18px;color:black !important;font-weight:bold;">Proforma Invoice الفاتورة الأولية</p>
                        <p v-else style="font-size:18px;color:black !important;font-weight:bold;">Credit Invoice - فاتورة الائتمان</p>

                        <table class="table table-borderless" style="background-color:#F9F706 !important;">
                            <tr>
                                <th style="color: black !important;background-color:#F9F706 !important; font-size: 19px; text-align: left; border-top: 0 !important; padding-left: 5px !important; padding-right: 5px !important; ">{{list.date}}</th>
                                <th style="color: black !important; background-color: #F9F706 !important; font-size: 19px; text-align: right; border-top: 0 !important; padding-left: 5px !important; padding-right: 5px !important; ">DATE-التاريخ</th>
                            </tr>
                            <tr>
                                <th style="background-color: #F9F706 !important; color: #EB5100 !important; font-size: 19px; text-align: left; border-top: 0 !important; padding-top: 3px !important; padding-left: 5px !important; padding-right: 5px !important;">{{list.registrationNo}}</th>
                                <th style="color: black !important; background-color: #F9F706 !important; font-size: 19px; text-align: right; border-top: 0 !important; padding-top: 3px !important; padding-left: 5px !important; padding-right: 5px !important; ">Invoice No.- الرقم الفاتورة</th>
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

                <div class="row " style="">
                    <div class="col-md-12 ">
                        <table class="table " style="border-left: 0 !important;border-bottom: 0 !important; height:180mm;">
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
                                <tr style="font-size:15px;font-weight:bold;" v-if="index<12" v-bind:key="item.id">
                                    <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;">{{index+1}}</td>
                                    <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;font-size:10px;">{{item.productName}} <br /> <span style="font-size: 13px; font-weight: bold">{{item.description}}</span></td>
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
                                <td class="text-left" style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:0!important" colspan="3"> <span class="float-left">Total Amount</span> <span class="float-right">المبلغ الإجمالي</span></td>
                                <td class="text-center" style="color:black !important;background-color: #F5E2A5 !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateTotalExclVAT - calulateTotalInclusiveVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
                            </tr>
                            <tr style="font-weight:bold;">
                                <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:14px!important;border:0!important;padding-left:0 !important;" colspan="2">Terms and conditions:-</td>
                                <td class="text-center" style="color: black !important; padding-top: 8px !important; padding-bottom: 8px !important; border: 0 !important;" v-if="isMultiUnit=='true'"></td>
                                <td class="text-left" style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:0!important" colspan="2">Discount</td>
                                <td class="text-right" style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:0!important">الخصم</td>
                                <td class="text-center" style="color:#BB6935 !important;background-color: #CFCEC9 !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
                            </tr>
                            <tr style="font-weight:bold;">
                                <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:11px!important;border:0!important;padding-left:0 !important;font-size:11px !important;" colspan="2" rowspan="2">{{headerFooters.footerEn}} <br /><br />{{headerFooters.footerAr}}</td>
                                <td class="text-center" style="color: black !important; padding-top: 8px !important; padding-bottom: 8px !important; border: 0 !important;" v-if="isMultiUnit=='true'"></td>
                                <td class="text-center" style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:0!important" colspan="3"><span class="float-left">VAT 15%</span><span class="float-right">ضريبة القيمة المضافة</span>   </td>
                                <td class="text-center" style="color:black !important;background-color: #D3E4DE !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
                            </tr>
                            <tr style="font-weight:bold;">
                                <td class="text-center" style="color: black !important; padding-top: 8px !important; padding-bottom: 8px !important; border: 0 !important;" v-if="isMultiUnit=='true'"></td>
                                <td class="text-center" style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:0!important" colspan="3"><span class="float-left">Total Payable</span><span class="float-right">الاجمالي المستحق</span>  </td>
                                <td class="text-center" style="color:black !important;background-color: #F4D88F !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
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
    
    import axios from 'axios'
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
        components: {
            
        },
        props: ['printDetails', 'headerFooter', 'isTouchScreen'],
        mixins: [clickMixin],
        data: function () {
            return {
                currency: "",
                qrValue: "",
                isHeaderFooter: '',
                invoicePrint: '',
                IsDeliveryNote: '',
                arabic: '',
                english: '',
                userName: '',
                emptyListCount: 0,
                indexCount: 0,
                page: 0,
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
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.inclusiveVat) || 0) }, 0)
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
            },
            calulateBundleAmount: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
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
            

            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.invoicePrint = localStorage.getItem('InvoicePrint');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.IsDeliveryNote = localStorage.getItem('IsDeliveryNote');
            this.userName = localStorage.getItem('FullName');
            this.currency = localStorage.getItem('currency');
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
                    
                    var count = this.printDetails.saleItems.length;
                    this.page = Math.ceil(count / 2);
                    if (count <= 20) {
                        this.emptyListCount = 12 - count;
                        this.indexCount = 12 - this.emptyListCount;

                    }
                    else {
                        console.log(count, this.page);
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


