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
                            <tr class="heading" style="font-size:14px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">رقم الموديل</th>-->
                                <th class="text-right" style="width:41%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">وصف </th>
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
                            <tr class="heading" style="font-size:14px;" v-else>
                                <th style="width:1%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                <!--<th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;" v-if="headerFooters.styleNo">Model/Style</th>-->
                                <th class="text-left" style="width:41%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Description</th>
                                <!--<th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="headerFooters.itemPieceSize">Package size</th>-->
                                <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Qty</th>
                                <th class="text-center" style="width:7%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="isMultiUnit=='true'">Total Qty</th>
                                <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">U.Price</th>
                                <th class="text-center" style="width:3%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Total Price</th>
                                <th class="text-center" style="width:4%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Discount</th>
                                <th class="text-center" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Tax%</th>
                                <th class="text-right" style="width:8%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">VAT AMT.</th>
                                <th class="text-right" style="width:11%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Grand Total</th>
                            </tr>


                            <tr style="font-size:12px;font-weight:bold;" v-for="(item, index) in list.saleOrderItems" v-bind:key="item.id">
                                <td class="text-left pl-1 pr-1" style=" padding-top: 3px !important; padding-bottom: 3px !important; border-top: 0 !important; border-bottom: 0 !important; color: black !important;">{{index+1}}</td>
                                <!--<td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.styleNo">{{item.product.styleNumber}}</td>-->
                                <td class="text-left pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;"><span style="color:black !important;">{{item.description}}</span> <span style="color:black !important;" v-if="item.isFree">(Free)</span></td>

                                <!--<td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="headerFooters.itemPieceSize">{{item.product.width}}</td>-->
                                <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.highQty }} - {{item.newQuantity }}</td>
                                <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-else>{{item.quantity }}</td>
                                <td class="text-center pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="isMultiUnit=='true'">{{item.quantity }}</td>
                                <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.total.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{parseFloat(item.taxRate).toFixed(0) }}%</td>
                                <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                <td class="text-right pl-1 pr-1" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(((item.total - (item.discountAmount)) + item.includingVat)).toFixed(3).slice(0,-1)}}</td>
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
                                <td style="padding: 2px 7px;font-size:15px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;"></td>
                                <td style="padding: 2px 7px;font-size:15px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">الكمية الإجمالية : {{ calulateTotalQty }} </td>
                                <td style="padding: 2px 7px;font-size:15px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Quantity : {{ calulateTotalQty }} </td>
                                <td style="padding: 2px 7px;font-size:15px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                <td style="padding: 2px 7px;font-size:15px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{parseFloat(calulateTotalExclVAT ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;" rowspan="4">{{$toWords((calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)))   }}</td>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;border-top: 0px !important;text-align:center;color:black !important;" rowspan="4"><!--<barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>--></td>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold">Discount<span style="font-weight:bold;color:black !important;">/ خصم</span></td>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>

                            <tr>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold">VAT Amount<span style="font-weight:bold;color:black !important;">/ لريال الضريية</span></td>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            </tr>
                            <tr>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold">Grand Total<span style="font-weight:bold;color:black ! important;">/ المبلخ الاجمالي</span></td>
                                <td style="padding: 2px 7px;font-size:15px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(calulateNetTotal - (calulateDiscountAmount )).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
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
                                    <!--<vue-qrcode v-bind:value="qrValue" style="width:90px;" />-->
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
                invoicePrint: '',
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
                
                return this.list.saleOrderItems.reduce(function (a, c) { return a + Number((c.quantity) || 0) }, 0)
            },
            calulateNetTotal: function () {
                return this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total + c.includingVat) || 0)) }, 0)
            },
            calulateTotalExclVAT: function () {
                return this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.total) || 0)) }, 0)
            },
            calulateTotalVAT: function () {
                return this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.includingVat + c.inclusiveVat) || 0)) }, 0)
            },
            calulateTotalInclusiveVAT: function () {
                return this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number((c.inclusiveVat) || 0)) }, 0)
            },
            calulateDiscountAmount: function () {
                return this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number(c.discountAmount || 0)) }, 0)
            },
            calulateBundleAmount: function () {
                return this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number(c.bundleAmount || 0)) }, 0)
            }
        },

        methods: { 
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
            
            this.invoicePrint = localStorage.getItem('InvoicePrint');

            if (this.fromEmailLink) {
                this.isMultiUnit = 'false';
                this.english = this.$route.query.english;
                this.arabic = this.$route.query.arabic;
                this.isHeaderFooter = this.$route.query.isHeaderFooter;
            }
            else {
                this.isMultiUnit = 'false';
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

