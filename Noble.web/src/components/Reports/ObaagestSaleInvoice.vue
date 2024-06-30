<template>
    <div>
        <div ref="mychildcomponent" hidden id='purchaseInvoice' class="col-md-12" style="color:black !important;background-color:white !important">
            <!--HEADER-->
            <div class="row" style="background-color:white !important">
                <div class="col-md-6" style="background-color:white !important">
                    <img :src="headerFooters.company.logoPath" style="width:auto;max-width:250px; max-height:120px;">
                </div>
                <div class="col-md-6 text-right" style="background-color:white !important">
                    <p style="color: #EB6331 !important; font-size: 25px;  font-weight: bold;">FACTURA PRO-FORMA</p>
                    <div class="row" style="background-color:white !important">
                        <div class="col-9 text-right" style="background-color:white !important">
                            <p style="color: #EB6331 !important; font-size: 15px;"> <span>Nº Pro-Forma::</span></p>

                        </div>
                        <div class="col-3 text-right">  <p style="color: black !important; font-size: 15px;  "><span>{{list.registrationNo}}</span></p></div>
                        <div class="col-9 text-right">
                            <p style="color: #EB6331 !important; font-size: 15px;"> <span>Date :</span></p>

                        </div>
                        <div class="col-3 text-right">
                            <p style="color: black !important; font-size: 15px; "> {{list.date}}</p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="background-color:white !important">
                <div class="col-md-5">
                    <p style="color: #EB6331 !important; font-size: 16px; ">Easy People, Lda</p>
                    <p style="color: black !important; font-size: 14px; ">{{headerFooters.company.addressEnglish}}</p>
                    <p style="color: black !important; font-size: 14px; ">Telef:{{headerFooters.company.phoneNo}}</p>
                    <p style="color: black !important; font-size: 14px; ">{{headerFooters.company.companyEmail}}</p>
                    <p style="color: black !important; font-size: 14px; ">Contribuinte: {{headerFooters.company.vatRegistrationNo}}</p>

                </div>
                <div class="col-md-7 ">
                    <div class="row">
                        <!--Row 1sd row-->
                        <div class="col-4 text-right" style="padding-right:0px !important;margin-right:0px !important">
                            <p style="color: #EB6331 !important; font-size: 14px;">Cliente:</p>

                        </div>
                        <div class="col-8 text-left" v-if="list.cashCustomer != null">  <p style="color: black !important; font-size: 14px;font-weight:bold  ">{{list.cashCustomer}}</p></div>
                        <div class="col-8 text-left" v-else> <p style="color: black !important; font-size: 14px;font-weight:bold  "> {{list.customerNameEn=='' || list.customerNameEn==null? list.customerNameAr : list.customerNameEn}}</p></div>

                        <!--Row 2nd row-->
                        <div class="col-4 text-right" style="padding-right:0px !important;margin-right:0px !important">
                        </div>
                        <div class="col-8 text-left"> <p style="color: black !important; font-size: 14px  "> {{list.customerAddressWalkIn}}</p></div>
                        <!--Row 3nd row-->
                        <div class="col-4 text-right" style="padding-right:0px !important;margin-right:0px !important">
                            <p style="color: #EB6331 !important; font-size: 14px;">Contribuinte Nº:</p>

                        </div>
                        <div class="col-8 text-left">  <p style="color: black !important; font-size: 14px;font-weight:bold  "></p></div>
                        <!--Row 4th row-->
                        <div class="col-4 text-right" style="padding-right:0px !important;margin-right:0px !important">
                            <p style="color: #EB6331 !important; font-size: 14px;">Contacto:</p>

                        </div>
                        <div class="col-8 text-left">  <p style="color: black !important; font-size: 14px;font-weight:bold  ">{{list.contactPerson1}}</p></div>

                        <!--Row 5th row-->
                        <div class="col-4 text-right" style="padding-right:0px !important;margin-right:0px !important">
                            <p style="color: #EB6331 !important; font-size: 14px;">E-mail:</p>

                        </div>
                        <div class="col-8 text-left">  <p style="color: black !important; font-size: 14px;font-weight:bold  ">{{list.email}}</p></div>
                        <!--Row 6th row-->
                        <div class="col-4 text-right" style="padding-right:0px !important;margin-right:0px !important">
                            <p style="color: #EB6331 !important; font-size: 15px;">Contacto Telefónico:</p>

                        </div>
                        <div class="col-8 text-left">  <p style="color: black !important; font-size: 15px;font-weight:bold  ">{{list.customerTelephone}}</p></div>
                    </div>
                </div>
            </div>




            <div class="row mt-2 " >
                <div class="col-md-12 ">
                    <table class="table " style="border-left: 0 !important;border-bottom: 0 !important; height:180mm;">
                        <tr class="heading" style="font-size:15px !important;">
                            <th class="text-center" style="width:3%;color:black !important;padding-top:2px !important; padding-bottom:8px !important;color:white!important;background-color:#EB6331 !important;border:1px solid #BFBFBF;">#</th>
                            <th class="text-center" style="width:12%;color:black !important;padding-top:2px !important; padding-bottom:8px !important;color:white!important;background-color:#EB6331 !important;border:1px solid #BFBFBF;">Referência</th>
                            <th class="text-center" style="width:45%;color:black !important;padding-top:2px !important; padding-bottom:8px !important;color:white!important;background-color:#EB6331 !important;border:1px solid #BFBFBF;" colspan="3">Descrição</th>
                            <th class="text-center" style="width:14%;color:black !important;padding-top:2px !important; padding-bottom:8px !important;color:white!important;background-color:#EB6331 !important;border:1px solid #BFBFBF;">Preço Unitário</th>

                            <th class="text-center" style="width:8%;color:black !important;padding-top:2px !important; padding-bottom:8px !important;color:white!important;background-color:#EB6331 !important;border:1px solid #BFBFBF">Quantidade </th>
                            <th class="text-center" style="width:18%;color:black !important;padding-top:2px !important; padding-bottom:8px !important;color:white!important;background-color:#EB6331 !important;border:1px solid #BFBFBF">Valor </th>
                        </tr>

                        <template v-for="(item, index) in list.saleItems">
                            <tr style="font-size:14px;font-weight:bold;" v-if="index<12" v-bind:key="item.id">
                                <td class="text-center" style="color:black !important;padding-top:2px !important; padding-bottom:8px !important;border:1px solid #BFBFBF;">{{index+1}}</td>
                                <td class="text-center" style="color:black !important;padding-top:2px !important; padding-bottom:8px !important;border:1px solid #BFBFBF;">{{item.product.Code}}</td>
                                <td class="text-left" style="color:black !important;padding-top:2px !important; padding-bottom:8px !important;border:1px solid #BFBFBF;font-size:10px;" colspan="3">{{item.productName}} <br /> <span style="font-size: 13px; font-weight: bold">{{item.description}}</span></td>
                                <td class="text-center" style="color:black !important;padding-top:2px !important; padding-bottom:8px !important;border:1px solid #BFBFBF;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>

                                <td class="text-center" style="color:black !important;padding-top:2px !important; padding-bottom:8px !important;border:1px solid #BFBFBF;">{{item.quantity }}</td>
                                <td class="text-center" style="color:black !important; padding-bottom:8px !important;border:1px solid #BFBFBF;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{item.total.toFixed(3).slice(0,-1)}}</span></td>
                            </tr>
                        </template>
                        <tr style="font-size:14px;font-weight:bold;" v-for="(item, index) in emptyListCount" v-bind:key="index">
                            <td class="text-center" style="color:black !important;padding-top:2px !important; padding-bottom:8px !important;border:1px solid #BFBFBF;">{{index+1+indexCount}}</td>
                            <td class="text-center" style="color:black !important;padding-top:2px !important; padding-bottom:8px !important;border:1px solid #BFBFBF;"></td>
                            <td class="text-left" style="color:black !important;padding-top:2px !important; padding-bottom:8px !important;border:1px solid #BFBFBF;font-size:10px;" colspan="3"></td>
                            <td class="text-center" style="color:black !important;padding-top:2px !important; padding-bottom:8px !important;border:1px solid #BFBFBF;"></td>

                            <td class="text-center" style="color:black !important;padding-top:2px !important; padding-bottom:8px !important;border:1px solid #BFBFBF;"></td>
                            <td class="text-center" style="color:black !important; padding-bottom:8px !important;border:1px solid #BFBFBF;"></td>
                        </tr>



                        <tr style="font-weight:bold;">
                            <td class="text-left" style="color: black !important;  padding-top: 2px !important; padding-bottom: 2px !important; border:0px !important" colspan="5">Documento Processado por Computador</td>
                            <td class="text-left" style="color:white !important;font-size:16px;padding-top:2px !important; padding-bottom:8px !important;border:0!important;background-color: #EB6331 !important;border:2px solid #BFBFBF;" colspan="2">TOTAL ÍLIQUIDO</td>
                            <td class="text-center" style="color:black !important;font-size:16px;padding-top:2px !important; padding-bottom:8px !important;border:1px solid #BFBFBF;">{{parseFloat(calulateTotalExclVAT - calulateTotalInclusiveVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                        </tr>
                        <tr style="font-weight:bold;">
                            <td class="text-center" style="color: white !important;  padding-top: 2px !important; padding-bottom: 2px !important; border:1px solid #BFBFBF !important;background-color: #EB6331 !important;font-size:13px" colspan="2">Valor Base</td>
                            <td class="text-center" style="color: white !important;  padding-top: 2px !important; padding-bottom: 2px !important; border:1px solid #BFBFBF !important;background-color: #EB6331 !important;font-size:13px">Taxa </td>
                            <td class="text-center" style="color: white !important;  padding-top: 2px !important; padding-bottom: 2px !important; border:1px solid #BFBFBF !important;background-color: #EB6331 !important;font-size:13px">IVA </td>
                            <td class="text-center" style="color: white !important;  padding-top: 2px !important; padding-bottom: 2px !important; border:1px transparent !important"> &nbsp;</td>
                            <td class="text-left" style="color:white !important;font-size:16px;padding-top:2px !important; padding-bottom:8px !important;border:0!important;background-color: #EB6331 !important;border:2px solid #BFBFBF;" colspan="2">TOTAL DO IVA</td>
                            <td class="text-center" style="color:black !important;font-size:16px;padding-top:2px !important; padding-bottom:8px !important;border:1px solid #BFBFBF;"> {{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                        </tr>


                        <tr style="font-weight:bold;" v-for="item in list.saleItems" v-bind:key="item.id">
                            <td class="text-center" style="color: black !important;  padding-top: 2px !important; padding-bottom: 2px !important; border:1px solid #BFBFBF !important;background-color: white !important;font-size:13px" colspan="2">{{item.total.toFixed(3).slice(0,-1)}}</td>
                            <td class="text-center" style="color: black !important;  padding-top: 2px !important; padding-bottom: 2px !important; border:1px solid #BFBFBF !important;background-color: white !important;font-size:13px">{{item.taxRate.toFixed(3).slice(0,-1) }} % </td>
                            <td class="text-center" style="color: black !important;  padding-top: 2px !important; padding-bottom: 2px !important; border:1px solid #BFBFBF !important;background-color: white !important;font-size:13px">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}} </td>
                            <td class="text-center" style="color: black !important;  padding-top: 2px !important; padding-bottom: 2px !important; border:0px !important" colspan="3"> &nbsp;</td>
                        </tr>
                        <tr style="font-weight:bold;">
                            <td class="text-center" style="  padding-top: 2px !important; padding-bottom: 2px !important; border:0px !important;background-color: white !important;font-size:13px" colspan="2"></td>
                            <td class="text-center" style="color: white !important;  padding-top: 2px !important; padding-bottom: 2px !important; border:1px solid #BFBFBF !important;background-color: #EB6331 !important;font-size:13px">TOTAL IVA</td>
                            <td class="text-center" style="color: white !important;  padding-top: 2px !important; padding-bottom: 2px !important; border:1px solid #BFBFBF !important;background-color: #EB6331 !important;font-size:13px">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}} </td>
                            <td class="text-center" style="color: white !important;  padding-top: 2px !important; padding-bottom: 2px !important; border:1px transparent !important" colspan="3"> &nbsp;</td>
                        </tr>



                        <tr style="font-weight:bold;">
                            <td class="text-center" style="color: black !important;  padding-top: 2px !important; padding-bottom: 2px !important; border:0px !important" colspan="5"></td>
                            <td class="text-left" style="color:white !important;font-size:19px;padding-top:2px !important; padding-bottom:8px !important;border:0!important;background-color: #EB6331 !important;border:3px solid #BFBFBF;" colspan="2">TOTAL</td>
                            <td class="text-center" style="color:white !important;font-size:19px;padding-top:2px !important; padding-bottom:8px !important;border:3px solid #BFBFBF;background-color: #EB6331 !important;">{{parseFloat(calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                        </tr>

                    </table>
                </div>
            </div>
            <div class="row" style="background-color:white !important">
                <div class="col-md-4" style="background-color:white !important">
                    <p style="color: #EB6331 !important; font-size: 18px; ">Dados Bancários para Pagamento</p>
                    <p style=" font-size: 18px; "><span>{{headerFooters.bankIcon1}}</span>/-<span>{{headerFooters.bankAccount1}}</span></p>


                </div>
                <div class="col-md-4 ">
                    <vue-qrcode v-bind:value="qrValue" style="width:120px;" />

                </div>
                    <div class="col-md-4 ">
                        <div class="row" style="background-color:white !important">
                            <div class="col-6 text-right" style="color: #EB6331 !important; font-size: 16px; ">Validade Pro-Forma:</div>
                            <div class="col-6 text-left" style="color: black !important; font-size: 16px; ">15 dias</div>
                        </div>
                        <div class="row" style="background-color:white !important">
                            <div class="col-6 text-right" style="color: #EB6331 !important; font-size: 16px; ">Condições Pagamento:</div>
                            <div class="col-6 text-left" style="color: black !important; font-size: 16px; ">Pronto Pagamento</div>
                        </div>
                        <div class="row" style="background-color:white !important">
                            <div class="col-6 text-right" style="color: #EB6331 !important; font-size: 16px; ">Prazo de Entrega:</div>
                            <div class="col-6 text-left" style="color: black !important; font-size: 16px; "> 4 Semanas</div>
                        </div>
                    </div>
                </div>


            </div>
    </div>

</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    
    //import Vue3Barcode from 'vue3-barcode'

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


