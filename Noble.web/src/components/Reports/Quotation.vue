<template>
    <div>
        <div hidden id='purchaseInvoice' class="col-md-12">
            <!--HEADER-->
            <div class="border border-dark col-md-12 mb-1 " style="height:50mm;" v-if="isHeaderFooter=='true'">
                <div class="row">
                    <div class="col-md-4 text-center">
                        <table class="m-auto">
                            <tr>
                                <td>
                                    <p class="text-center">
                                        <span style="font-size:23px;">{{headerFooters.company.nameEnglish}}</span><br />
                                        <span style="font-size:17px;">{{headerFooters.company.categoryEnglish}}</span><br />
                                        <span style="font-size:17px;">VAT No.: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                        <span style="font-size:16px;">{{headerFooters.company.companyNameEnglish}}</span><br />
                                        <span style="font-size:15px;">
                                            {{headerFooters.company.addressEnglish}}<br />
                                            Tel: {{headerFooters.company.phoneNo}}
                                        </span>
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-4 text-center my-5" style="padding:0px !important; margin:0 !important">
                        <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; height:auto; max-height:120px; padding:5px !important; margin:0 !important">
                        <p style="text-align: center; margin: 0px; padding: 0px; font-size: 15px; line-height: 1; font-family: 'Times New Roman', Times;">
                            <span style="font-size:14px;">{{ $t('TheQuotation.Quotation') }}</span>
                        </p>
                    </div>
                    <div class="col-md-4 " style="height:40mm;">
                        <table class="m-auto">
                            <tr>
                                <td>
                                    <p class="text-center">
                                        <span style="font-size:23px;">{{headerFooters.company.nameArabic}}.</span><br />
                                        <span style="font-size:17px;">{{headerFooters.company.categoryArabic}}</span><br />
                                        <span style="font-size:17px;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                        <span style="font-size:16px;">{{headerFooters.company.companyNameArabic}}</span><br />
                                        <span style="font-size:15px;">
                                            {{headerFooters.company.addressArabic}}<br />
                                            هاتف: {{headerFooters.company.phoneNo}}:
                                        </span>
                                    </p>
                                </td>

                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div style="height:50mm;" v-else></div>

            <div class="border border-dark" style="height:30mm;margin-top:1mm;">
                <div class="row">
                    <div class="col-md-12 ml-2 mr-2" style="height:8mm;font-size:16px;">
                        <div class="row">
                            <div class="col-md-2">Date:</div>
                            <div class="col-md-2 text-center">{{list.date}}</div>
                            <div class="col-md-2">:تاريخ</div>
                            
                            <div class="col-md-2">Quotation No.:</div>
                            <div class="col-md-2 text-center">{{list.registrationNo}}</div>
                            <div class="col-md-2">:سؤال رقم</div>                         
                        </div>
                    </div>
                    <div class="col-md-12 ml-2 mr-2" style="height:8mm;font-size:14px;">
                        <div class="row">
                            <div class="col-md-2">Customer Name:</div>
                            <div class="col-md-2 text-center">{{list.customerNameEn}}</div>
                            <div class="col-md-2">:اسم الزبون</div>

                            <div class="col-md-2">Refrence:</div>
                            <div class="col-md-2 text-center">{{list.refrence}}</div>
                            <div class="col-md-2">:المرجعي</div>
                        </div>
                    </div>
                    <!-- style="border-bottom:black 1px dashed;" -->
                    <div class="col-md-12 ml-2 mr-2" style="height:8mm;font-size:14px;">
                        <div class="row">                           

                            <div class="col-md-2">client Purchase No:</div>
                            <div class="col-md-2 text-center">{{list.clientPurchaseNo}}</div>
                            <div class="col-md-2">:العميل شراء لا</div>
                        </div>
                    </div>
                </div>
            </div>

            <!--INFORMATION-->
            <div class="border border-dark" style="height:230mm;">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-striped table-hover table_list_bg">
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
            <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter!='true'">
                <div class="row">
                    <table class="table text-center">
                        <tr>
                            <td>
                                {{ $t('TheQuotation.ReceivedBy') }}::
                            </td>
                            <td>
                                {{ $t('TheQuotation.ApprovedBy') }}:
                            </td>
                            <td>
                                {{ $t('TheQuotation.PreparedBy') }}:
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!--Footer-->
            <div class="border border-dark col-md-12 " style="height: 30mm;" v-if="isHeaderFooter!='true'">
                <div class="row">
                    <div class="col-md-3 text-center">
                        <u><b><span style="font-size:14px;">Terms & Conditions</span></b></u><br />
                        <span style="font-size:13px;" v-html="headerFooters.footerEn">
                            
                        </span>
                    </div>
                    <div class="col-md-6  text-center">
                        <p style="padding-top:15px;font-size:13px;">
                            Warranty period 5 days after receiving the goods.
                        </p>
                    </div>
                    <div class="col-md-3 text-center">
                        <u><b><span style="font-size:14px;">البنود و الظروف</span></b></u><br />
                        <span class=" text-center" style="font-size:13px;" v-html="headerFooters.footerAr">
                           
                        </span>
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
        props: ['printDetails', 'headerFooter'],
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
                
                this.$htmlToPaper('purchaseInvoice');
            }

        },
        mounted: function () {
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            var root = this;
            if (this.printDetails.saleOrderItems.length > 0) {
                this.list = this.printDetails;
                this.headerFooters = this.headerFooter;
                this.list.date = moment().format('DD MMM YYYY');
                setTimeout(function () {
                    root.printInvoice();
                }, 125)
            }
        },

    }
</script>

