<template>
    <div>
        <div hidden ref="mychildcomponent" id='quotation' class="col-md-12">
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

                                <td style="width:100%;" class="pt-0 pb-0 pl-0 pr-0" colspan="3">

                                    <div style="text-align: center;">
                                        <span style="font-size:20px;color:black;font-weight:bold;">Quotation</span>
                                    </div>


                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div style="height:45mm;" v-else></div>
            <div style="height:25mm;margin-top:1mm; border:2px solid #000000;">
                <div class="row">
                    <div class="col-md-12 ml-2 mr-2">
                        <table class="table table-borderless">
                            <!--Row 1-->
                            <tr>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">Date:</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{list.date}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">:تاريخ</td>

                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">Sale Order No:</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{list.registrationNo}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">:رقم التسجيل</td>
                            </tr>

                            <!--Row 2-->
                            <tr>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">Customer Name:</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{list.customerNameEn}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">:اسم الزبون</td>

                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span>Refrence:</span> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"><span>{{list.refrence}}</span> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;"><span v-if="headerFooters.cargoName">: المرجعي </span> </td>
                            </tr>

                            <!--Row 3-->
                            <tr>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;">Client Purchase No:</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;">{{list.clientPurchaseNo}}</td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;">:العميل شراء لا</td>

                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:14%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important;"><span></span> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:25%; text-align:center;color:black !important;font-weight:bold;font-size:14px !important;"> </td>
                                <td class="pl-0 pr-0 pt-0 pb-0" style="width:11%;font-weight:bolder;font-size:14px !important;color:black !important;"><span v-if="headerFooters.customerNumber"> </span> </td>
                            </tr>





                        </table>
                    </div>

                </div>
            </div>


            <!--INFORMATION-->
            <div style="height:200mm;border:2px solid !important;">
                <div class="row ">
                    <div class="col-md-12 ">
                        <table class="table">
                            <tr class="heading" style="font-size:15px;">
                                <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;">#</th>
                                <th style="width:50%;padding-top:3px !important; padding-bottom:3px !important;">{{ $t('TheQuotation.Description') }}</th>
                                <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;">{{ $t('TheQuotation.UnitPrice') }}</th>
                                <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;">Quantity</th>
                                <th class="text-center" style="width:15%;padding-top:3px !important; padding-bottom:3px !important;">Discount Amount</th>
                                <th class="text-right" style="width:12%;padding-top:3px !important; padding-bottom:3px !important;">{{ $t('TheQuotation.TotalAmount') }}</th>
                            </tr>


                            <tr style="font-size:16px;" v-for="(item, index) in list.saleOrderItems" v-bind:key="item.id">
                                <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;">{{index+1}}</td>
                                <td v-if="english=='true' && arabic=='false'" class="text-left" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;">{{item.productNameEn}}</td>
                                <td v-else-if="isOtherLang() && english=='false'" class="text-left" style="border-top:0 !important; border-bottom:0 !important;padding-top:3px !important; padding-bottom:3px !important;">{{item.productNameAr}}</td>
                                <td v-else class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;">{{item.productNameEn}}  {{item.productNameAr}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;">{{item.isFree? (item.unitPrice>0? item.unitPrice.toFixed(3).slice(0,-1) : '-') : item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;">{{item.isFree? (item.quantity>0? item.quantity : '-') : item.quantity }}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;">{{item.isFree? '-' : item.discountAmount }}</td>
                                <td class="text-right" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;">{{item.isFree? '-' : item.total.toFixed(3).slice(0,-1)}}</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div style="height:35mm;margin-top:1mm;">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-bordered tbl_padding">
                            <tr>
                                <td style="padding: 3px 7px;font-size:14px;width:30%;border:1px solid black !important;"></td>
                                <td style="padding: 3px 7px;font-size:14px;width:30%;border:1px solid black !important;">{{ $t('TheQuotation.TotalQuantity') }}: {{ calulateTotalQty }}</td>
                                <td style="padding: 3px 7px;font-size:14px;width:15%;border:1px solid black !important;">Sub Total:</td>
                                <td style="padding: 3px 7px;font-size:14px;width:15%;border:1px solid black !important;">{{ (calulateTotalExclVAT ).toFixed(3).slice(0,-1) }}</td>
                            </tr>
                            <tr>
                                <td style="padding: 3px 7px;font-size:14px;border:1px solid black !important;border-bottom:0!important;border-top:0!important;" colspan="2">{{$toWords((calulateNetTotal - list.discountAmount))   }}</td>
                                <td style="padding: 3px 7px;font-size:14px;border:1px solid black !important;">{{ $t('TheQuotation.DiscAmount') }}:</td>
                                <td style="padding: 3px 7px;font-size:14px;border:1px solid black !important;">{{ calulateDiscountAmount }}</td>
                            </tr>
                            <tr>
                                <td style="padding: 3px 7px;font-size:14px;border:1px solid black !important;border-top:0!important;" colspan="2"></td>
                                <td style="padding: 3px 7px;font-size:14px;border:1px solid black !important;">{{ $t('TheQuotation.VATAmount') }}:</td>
                                <td style="padding: 3px 7px;font-size:14px;border:1px solid black !important;">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 3px 7px;font-size:14px;border:1px solid black !important;border-top:0!important;" colspan="2"></td>
                                <td style="padding: 3px 7px;font-size:14px;border:1px solid black !important;">Total After Discount:</td>
                                <td style="padding: 3px 7px;font-size:14px;border:1px solid black !important;">{{parseFloat(calulateTotalExclVAT-calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 3px 7px;font-size:14px;border:1px solid black !important;" colspan="2"></td>
                                <td style="padding: 3px 7px;font-size:14px;border:1px solid black !important;">Ttoal with VAT:</td>
                                <td style="padding: 3px 7px;font-size:14px;border:1px solid black !important;">{{parseFloat(calulateNetTotal + list.discount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-md-12  " style="height:15mm;" v-if="isHeaderFooter=='true'">
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
            <div class="col-md-12" style="border:2px solid #000000; height:34mm;" v-if="isHeaderFooter=='true'">
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

            <div style="height: 40mm;" v-else></div>
        </div>
    </div>

</template>

<script>
    import moment from "moment";
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['printDetails', 'headerFooter', 'isEmail', 'fromEmailLink'],
        data: function () {
            return {
                isHeaderFooter: '',
                arabic: '',
                english: '',
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
                    saleOrderItems:
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
                return this.list.saleOrderItems.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0)
            },
            calulateNetTotal: function () {
                var withDisc = this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : c.total - c.discountAmount) }, 0)
                var totalIncDisc = (this.list.isBeforeTax && this.list.isDiscountOnTransaction && this.list.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(this.list.transactionLevelDiscount) * (withDisc) / 100) : parseFloat(this.calulateDiscountAmount)

                return this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total + c.includingVat) || 0)) }, 0) + parseFloat((this.list.taxMethod == ("Inclusive" || "شامل") ? 0 : this.calulateTotalVAT)) - totalIncDisc
            },
            calulateTotalExclVAT: function () {
                return this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total - c.discountAmount) || 0)) }, 0)
            },
            calulateTotalVAT: function () {
                var totalQtyWithotFree = this.list.saleOrderItems.reduce((qty, prod) => qty + (prod.isFree ? 0 : parseInt(prod.quantity == '' ? 0 : prod.quantity)), 0);
                var paidVat = this.list.saleOrderItems
                    .reduce((vat, prod) => (vat + (prod.isFree ? 0 : ((this.list.taxMethod == ("Inclusive" || "شامل")) ? ((parseFloat(prod.total - prod.discountAmount) - (this.list.isBeforeTax ? (((prod.quantity * prod.unitPrice) * this.list.transactionLevelDiscount) / 100) : 0)) * prod.taxRate) / (100 + prod.taxRate) : ((parseFloat(prod.total - prod.discountAmount) - (this.list.isBeforeTax && !this.list.isFixed && this.list.isDiscountOnTransaction ? (((prod.quantity * prod.unitPrice) * this.list.transactionLevelDiscount) / 100) : (this.list.isBeforeTax && this.list.isFixed && this.list.isDiscountOnTransaction ? (this.list.transactionLevelDiscount / parseFloat(totalQtyWithotFree) * prod.quantity) : 0))) * prod.taxRate) / 100))), 0).toFixed(3).slice(0, -1)

                return paidVat;
            },
            calulateDiscountAmount: function () {
                var totalIncDisc = 0;
                if (this.list.isDiscountOnTransaction) {
                    var withDisc = this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : c.total - c.discountAmount) }, 0)

                    var discountForInclusiveVat = parseFloat(this.list.saleOrderItems
                        .reduce((vat, prod) => (vat + (prod.isFree ? 0 : ((this.list.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(prod.total) * prod.taxRate) / (100 + prod.taxRate) : 0))), 0).toFixed(3).slice(0, -1))

                    totalIncDisc = ((this.list.isBeforeTax && this.list.isDiscountOnTransaction) ? (this.list.taxMethod == ("Inclusive" || "شامل") ? (parseFloat(this.list.transactionLevelDiscount) * (withDisc - discountForInclusiveVat) / 100) : (this.list.isFixed ? parseFloat(this.list.transactionLevelDiscount) : parseFloat(this.list.transactionLevelDiscount) * withDisc / 100)) : (this.list.isFixed ? parseFloat(this.list.transactionLevelDiscount) : (parseFloat(withDisc) + (this.list.taxMethod == ("Inclusive" || "شامل") ? 0 : parseFloat(this.calulateTotalVAT))) * parseFloat(this.list.transactionLevelDiscount) / 100)).toFixed(3).slice(0, -1)

                }
                else {
                    totalIncDisc = this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : c.discountAmount) }, 0)
                }

                return totalIncDisc;
            }

        },

        methods: { 
            calulateNetTotalForQr: function () {
                var withDisc = this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : c.total - c.discountAmount) }, 0)
                var totalIncDisc = (this.list.isBeforeTax && this.list.isDiscountOnTransaction && this.list.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(this.list.transactionLevelDiscount) * (withDisc) / 100) : parseFloat(this.calulateDiscountAmount)

                return (this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total + c.includingVat) || 0)) }, 0) + parseFloat((this.list.taxMethod == ("Inclusive" || "شامل") ? 0 : this.calulateTotalVAT)) - totalIncDisc).toFixed(3);
            },
            calulateTotalVATForQr: function () {
                var totalQtyWithotFree = this.list.saleOrderItems.reduce((qty, prod) => qty + (prod.isFree ? 0 : parseInt(prod.quantity == '' ? 0 : prod.quantity)), 0);
                var paidVat = this.list.saleOrderItems
                    .reduce((vat, prod) => (vat + (prod.isFree ? 0 : ((this.list.taxMethod == ("Inclusive" || "شامل")) ? ((parseFloat(prod.total - prod.discountAmount) - (this.list.isBeforeTax ? (((prod.quantity * prod.unitPrice) * this.list.transactionLevelDiscount) / 100) : 0)) * prod.taxRate) / (100 + prod.taxRate) : ((parseFloat(prod.total - prod.discountAmount) - (this.list.isBeforeTax && !this.list.isFixed && this.list.isDiscountOnTransaction ? (((prod.quantity * prod.unitPrice) * this.list.transactionLevelDiscount) / 100) : (this.list.isBeforeTax && this.list.isFixed && this.list.isDiscountOnTransaction ? (this.list.transactionLevelDiscount / parseFloat(totalQtyWithotFree) * prod.quantity) : 0))) * prod.taxRate) / 100))), 0).toFixed(3).slice(0, -1)

                return parseFloat(paidVat).toFixed(3);
            },
            calulateTotalQtySub: function (val) {
                return val.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0)
            },
            calulateNetTotalSub: function (val) {
                var withDisc = val.reduce(function (a, c) { return a + (c.isFree ? 0 : c.total - c.discountAmount) }, 0)
                var totalIncDisc = (this.list.isBeforeTax && this.list.isDiscountOnTransaction && this.list.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(this.list.transactionLevelDiscount) * (withDisc) / 100) : parseFloat(this.calulateDiscountAmount)

                return val.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total + c.includingVat) || 0)) }, 0) + parseFloat((this.list.taxMethod == ("Inclusive" || "شامل") ? 0 : this.calulateTotalVAT)) - totalIncDisc
            },
            calulateTotalExclVATSub: function (val) {
                return val.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total - c.discountAmount) || 0)) }, 0)
            },
            calulateTotalVATSub: function (val) {
                var totalQtyWithotFree = val.reduce((qty, prod) => qty + (prod.isFree ? 0 : parseInt(prod.quantity == '' ? 0 : prod.quantity)), 0);
                var paidVat = val
                    .reduce((vat, prod) => (vat + (prod.isFree ? 0 : ((this.list.taxMethod == ("Inclusive" || "شامل")) ? ((parseFloat(prod.total - prod.discountAmount) - (this.list.isBeforeTax ? (((prod.quantity * prod.unitPrice) * this.list.transactionLevelDiscount) / 100) : 0)) * prod.taxRate) / (100 + prod.taxRate) : ((parseFloat(prod.total - prod.discountAmount) - (this.list.isBeforeTax && !this.list.isFixed && this.list.isDiscountOnTransaction ? (((prod.quantity * prod.unitPrice) * this.list.transactionLevelDiscount) / 100) : (this.list.isBeforeTax && this.list.isFixed && this.list.isDiscountOnTransaction ? (this.list.transactionLevelDiscount / parseFloat(totalQtyWithotFree) * prod.quantity) : 0))) * prod.taxRate) / 100))), 0).toFixed(3).slice(0, -1)

                return paidVat;
            },
            calulateDiscountAmountSub: function (val) {
                var totalIncDisc = 0;
                if (this.list.isDiscountOnTransaction) {
                    var withDisc = val.reduce(function (a, c) { return a + (c.isFree ? 0 : c.total - c.discountAmount) }, 0)

                    var discountForInclusiveVat = parseFloat(val
                        .reduce((vat, prod) => (vat + (prod.isFree ? 0 : ((this.list.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(prod.total) * prod.taxRate) / (100 + prod.taxRate) : 0))), 0).toFixed(3).slice(0, -1))

                    totalIncDisc = ((this.list.isBeforeTax && this.list.isDiscountOnTransaction) ? (this.list.taxMethod == ("Inclusive" || "شامل") ? (parseFloat(this.list.transactionLevelDiscount) * (withDisc - discountForInclusiveVat) / 100) : (this.list.isFixed ? parseFloat(this.list.transactionLevelDiscount) : parseFloat(this.list.transactionLevelDiscount) * withDisc / 100)) : (this.list.isFixed ? parseFloat(this.list.transactionLevelDiscount) : (parseFloat(withDisc) + (this.list.taxMethod == ("Inclusive" || "شامل") ? 0 : parseFloat(this.calulateTotalVAT))) * parseFloat(this.list.transactionLevelDiscount) / 100)).toFixed(3).slice(0, -1)

                }
                else {
                    totalIncDisc = val.reduce(function (a, c) { return a + (c.isFree ? 0 : c.discountAmount) }, 0)
                }

                return totalIncDisc;
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
                        link.setAttribute('download', 'Quotation ' + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();
                    });
            },
            printInvoiceEmail: function () {
                
                this.htmlData.htmlString = this.$refs.mychildcomponent.innerHTML;
                this.$emit('invoiceEmail', this.htmlData);

            },

        },
        mounted: function () {
            if (this.fromEmailLink) {
                this.isMultiUnit = this.$route.query.unit;
                this.english = this.$route.query.english;
                this.arabic = this.$route.query.arabic;
                this.isHeaderFooter = this.$route.query.isHeaderFooter;
            }
            else {
                this.isMultiUnit = localStorage.getItem('IsMultiUnit');
                this.english = localStorage.getItem('English');
                this.arabic = localStorage.getItem('Arabic');
                this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            }
            
            var root = this;
            if (this.printDetails.saleOrderItems.length > 0) {
                this.list = this.printDetails;
                this.headerFooters = this.headerFooter;
                this.list.date = moment().format('DD MMM YYYY');


                if (this.isEmail) {
                    setTimeout(function () {
                        root.printInvoiceEmail();
                    }, 125)
                }
                else {
                    setTimeout(function () {
                        root.printInvoice();
                    }, 125)
                }
               
            }
        },

    }
</script>

