<template>
    <div>
        <div hidden ref="mychildcomponent" id='purchaseInvoice' class="col-md-12" style="color:black !important;background-color:white !important">
            <div class="row">
                <!--HEADER-->
                <div class="col-lg-12 ">
                    <div class="text-center">
                        <h1 style="color: black !important;">{{headerFooters.company.nameEnglish}}</h1>
                    </div>

                </div>
            </div>
            <div class="row ">
                <div class="col-md-12 ">
                    <table class="table">
                        <tr>
                            <th style="color: black !important; border-top: 0 !important; border-left: 0 !important; border-right: 0 !important; border-bottom: 0 !important; font-size: 18px; padding: 0 !important; margin: 0 !important" colspan="2"></th>
                            <th style="color: black !important; border: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;  text-align: left; font-size: 16px " colspan="6"></th>

                            <th class="text-left" style="color: black !important; padding-top: 3px !important; padding-bottom: 3px !important;  border: 1px solid #000000; font-size: 16px;">Person</th>
                            <th class="text-left" style="font-weight: normal; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">
                                {{userName}}

                            </th>
                        </tr>
                        <tr>
                            <th style="border-top:0  !important;border-left:0  !important;border-right:0  !important;border-bottom:1px solid  !important;font-size:21px;padding:0px 0px 3px 0px !important;margin:0 !important;color:black !important;" colspan="2">Buyer Details</th>
                            <th class="text-center" style="color: black !important; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 0 !important;" colspan="6"></th>

                            <th class="text-left" style="color: black !important; padding-top: 3px !important; padding-bottom: 3px !important;  border: 1px solid #000000; font-size: 16px;">Cell #</th>
                            <th class="text-left" style="font-weight: normal; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">
                                <span v-if="list.cashCustomer != null && headerFooters.customerTelephone">{{list.mobile}}</span>
                                <span v-if="list.creditCustomer != null && headerFooters.customerTelephone">{{list.customerTelephone}}</span>

                            </th>
                        </tr>
                        <tr>
                            <th style="color: black !important; border-top: 0 !important; border-left: 0 !important; border-right: 0 !important; border-bottom: 0 !important; font-size: 18px; padding: 0 !important; margin: 0 !important" colspan="2">Company:</th>
                            <th style="color: black !important; border: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;  text-align: left; font-size: 16px " colspan="6"><span v-if="list.cashCustomer != null">{{list.cashCustomer}}</span> <span v-else>{{invoicePrint == 'العربية'? list.customerNameAr : list.customerNameEn}}</span></th>

                            <th class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid #000000;font-size:16px;">NTN #</th>
                            <th class="text-left" style="font-weight: normal; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">
                                <span>{{headerFooters.company.vatRegistrationNo}}</span>

                            </th>
                        </tr>
                        <tr>
                            <th style="color: black !important; border-top: 0 !important; border-left: 0 !important; border-right: 0 !important; border-bottom: 0 !important; font-size: 18px; padding: 0 !important; margin: 0 !important" colspan="2">Propritor:</th>
                            <th style="color: black !important; border: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;  text-align: left; font-size: 16px; background-color: white !important; font-weight: normal " colspan="6"></th>

                            <th class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid #000000;font-size:16px;">STRN #</th>
                            <th class="text-left" style="font-weight: normal; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">
                                <span>{{headerFooters.company.companyRegNo}}</span>

                            </th>
                        </tr>
                        <tr>
                            <th style="color: black !important; border-top: 0 !important; border-left: 0 !important; border-right: 0 !important; border-bottom: 0 !important; font-size: 18px; padding: 0 !important; margin: 0 !important" colspan="2">Contact #</th>
                            <th style="border: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; text-align: left; font-size: 16px; background-color: white !important; font-weight: normal" colspan="6"><span v-if="list.cashCustomer != null && headerFooters.customerTelephone">{{list.mobile}}</span> <span v-if="list.creditCustomer != null && headerFooters.customerTelephone">{{list.customerTelephone}}</span></th>

                            <th class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid #000000;font-size:16px;">invoice #</th>
                            <th class="text-left" style="font-weight: normal; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">
                                <span>{{list.registrationNo}}</span>

                            </th>
                        </tr>
                        <tr>
                            <th style="color: black !important; border-top: 0 !important; border-left: 0 !important; border-right: 0 !important; border-bottom: 0 !important; font-size: 18px; padding: 0 !important; margin: 0 !important" colspan="2">NTN #:</th>
                            <th style="border: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; text-align: left; font-size: 16px; background-color: white !important; font-weight: normal;" colspan="6">{{list.customerVat}}</th>

                            <th class="text-left" style="color:black !important;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid #000000;font-size:16px;">Date</th>
                            <th class="text-left" style="font-weight: normal; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">
                                <span>{{list.date}}</span>

                            </th>
                        </tr>
                        <tr>
                            <th style="color: black !important; border-top: 0 !important; border-left: 0 !important; border-right: 0 !important; border-bottom: 0 !important; font-size: 18px; padding: 0 !important; margin: 0 !important" colspan="2">STRN #:</th>
                            <th style="border: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; text-align: left; font-size: 16px; background-color: white !important; font-weight: normal" colspan="6">{{list.customerCRN}}</th>

                            <th class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid #000000;font-size:16px;">Sale Order.</th>
                            <th class="text-left" style="font-weight: normal; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">
                                <span></span>

                            </th>
                        </tr>
                        <tr>
                            <th style="color: black !important; border-top: 0 !important; border-left: 0 !important; border-right: 0 !important; border-bottom: 0 !important; font-size: 18px; padding: 0 !important; margin: 0 !important" colspan="2">Address:</th>
                            <th style="border: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; text-align: left; font-size: 16px; background-color: white !important; font-weight: normal" colspan="6">{{list.customerAddressWalkIn}}</th>

                            <th class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border:1px solid #000000;font-size:16px;">OGP #</th>
                            <th class="text-left" style="font-weight: normal; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">
                                <span></span>

                            </th>
                        </tr>
                        <tr class="heading" style="font-size:16px;">
                            <th class="text-center" style="background-color: #e7e7e7 !important; width: 11%; padding-top: 3px !important; padding-bottom: 3px !important; border: 1px solid #000000;">Sr.#</th>
                            <th class="text-center" style="background-color: #e7e7e7 !important; width: 10%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">Vendor Code</th>
                            <th class="text-center" style="background-color: #e7e7e7 !important; width: 12%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">PO</th>
                            <th class="text-center" style="background-color: #e7e7e7 !important; width: 10%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;" colspan="2">Article</th>
                            <th class="text-center" style="background-color: #e7e7e7 !important; width: 10%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">Color </th>
                            <th class="text-center" style="background-color: #e7e7e7 !important; width: 6%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">Quantity </th>
                            <th class="text-center" style="background-color: #e7e7e7 !important; width: 6%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">Price</th>
                            <th class="text-center" style="background-color: #e7e7e7 !important; width: 10%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;" v-if="!list.isDiscountOnTransaction">Discount </th>
                            <th class="text-center" style="background-color: #e7e7e7 !important; width: 15%; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; border: 1px solid #000000;">Total Amount</th>
                        </tr>


                        <tr style="font-size:15px;font-weight:bold;border:1px solid" v-for="(item, index) in list.saleItems" v-bind:key="item.id">
                            <td class="text-center pl-1 pr-1" style="font-weight: normal; font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important; border: 1px solid; color: black !important;">{{index+1}}</td>
                            <td class="text-center pl-1 pr-1" style="font-weight: normal; border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;"></td>
                            <td class="text-center pl-1 pr-1" style="font-weight: normal; border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;"></td>
                            <td class="text-center pl-1 pr-1" style="font-weight: normal; border: 1px solid; border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;" colspan="2">{{item.description}}</td>
                            <td class="text-center pl-1 pr-1" style="font-weight: normal; border: 1px solid; border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{item.colorName}}</td>
                            <td class="text-center pl-1 pr-1" style="font-weight: normal; border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{item.quantity }}</td>
                            <td class="text-right pl-1 pr-1" style="font-weight: normal; border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{item.unitPrice.toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            <td class="text-right pl-1 pr-1" style="font-weight: normal; border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;" v-if="!list.isDiscountOnTransaction">{{item.discountAmount.toFixed(3).slice(0,-1)}}</td>
                            <td class="text-right pl-1 pr-1" style="font-weight: normal; border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{(((item.total - (item.discountAmount+item.bundleAmount)) + item.includingVat)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                        </tr>
                        <tr style="font-size:15px;font-weight:bold;border:1px solid">
                            <td class="text-center pl-1 pr-1" style="font-size: 14px !important; padding-top: 3px !important; padding-bottom: 3px !important; border:1px solid; color: black !important;">Total</td>
                            <td class="text-center pl-1 pr-1" style="font-weight: normal; border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;" colspan="6"></td>
                            <td class="text-right pl-1 pr-1" style="font-weight: normal; border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;"></td>
                            <td class="text-right pl-1 pr-1" style="font-weight: normal; border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;" v-if="!list.isDiscountOnTransaction">{{parseFloat(calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                            <td class="text-right pl-1 pr-1" style="font-weight: normal; border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{parseFloat(calulateTotalExclVAT+calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                        </tr>
                        <tr>
                            <th colspan="10" class="text-center" style="border: 1px solid;">Assortment</th>
                        </tr>

                        <tr style="font-size:15px;font-weight:bold;border:1px solid">
                            <th class="text-center pl-1 pr-1" style="border: 1px solid; border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">Article</th>
                            <th class="text-center pl-1 pr-1" style="border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">Color</th>
                            <th v-for="size in saleSizeAssortment" v-bind:key="size.sizeId" class="text-center pl-1 pr-1" style="border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{size.name}}</th>
                            <th class="text-center pl-1 pr-1" style="border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">Total</th>
                        </tr>
                        <tr style="font-size:15px;font-weight:bold;border:1px solid" v-for="item in list.saleItems" v-bind:key="item.id">
                            <td class="text-center pl-1 pr-1" style="font-weight: normal; border: 1px solid; border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{item.description}}</td>

                            <td class="text-center pl-1 pr-1" style="font-weight: normal; border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{item.colorName}}</td>
                            <td v-for="size in item.saleSizeAssortment" v-bind:key="size.sizeId" class="text-center pl-1 pr-1" style="font-weight: normal; border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{size.quantity}}</td>
                            <td class="text-center pl-1 pr-1" style="font-weight: normal; border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{item.quantity }}</td>
                        </tr>

                        <tr style="font-size:15px;font-weight:bold;border:1px solid">
                            <th colspan="9" class="text-center pl-1 pr-1" style="border: 1px solid; border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">Total</th>
                            <th class="text-center pl-1 pr-1" style="border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{calulateTotalQty}}</th>
                        </tr>

                        <tr>
                            <th colspan="10" class="text-center" style="border:none;color:transparent;">Space</th>
                        </tr>

                        <tr>
                            <th colspan="2" class="text-left pl-1 pr-1" style="border-top: 0 !important; border-left: 0 !important; border-right: 0 !important; border-bottom: 1px solid !important; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important; ">Signature/Stamp</th>
                            <th colspan="3" class="text-center pl-1 pr-1" style="border-top: 0 !important; border-left: 0 !important; border-right: 0 !important; border-bottom: 1px solid #ffffff !important; padding-top: 3px !important; padding-bottom: 3px !important;"></th>
                            <th colspan="4" class="text-center pl-1 pr-1" style="border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">Total Quantity</th>
                            <th class="text-center pl-1 pr-1" style="border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{calulateTotalQty}}</th>
                        </tr>
                        <tr>
                            <th colspan="5" class="text-center pl-1 pr-1" style="border: none !important;padding-top: 3px !important; padding-bottom: 3px !important;"></th>
                            <th colspan="4" class="text-center pl-1 pr-1" style="border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">Total Amount Exclusive Sale tax</th>
                            <th class="text-right pl-1 pr-1" style="border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{parseFloat(calulateTotalExclVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</th>
                        </tr>
                        <tr>
                            <th colspan="5" class="text-center pl-1 pr-1" style="border: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;"></th>
                            <th colspan="4" class="text-center pl-1 pr-1" style="border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">Sale Tax 17% Value</th>
                            <th class="text-right pl-1 pr-1" style="border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</th>
                        </tr>
                        <tr>
                            <th colspan="5" class="text-center pl-1 pr-1" style="border: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;"></th>
                            <th colspan="4" class="text-center pl-1 pr-1" style="border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">Total Amount Inclusive Sale tax</th>
                            <th class="text-right pl-1 pr-1" style="border: 1px solid; padding-top: 3px !important; padding-bottom: 3px !important; color: black !important;">{{parseFloat(calulateNetTotal).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</th>
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
    const options = {
        name: '',
        specs: [
            'fullscreen=no',
            'titlebar=yes',
            'scrollbars=yes'
        ],
        styles: [
            '/assets/css/bootstrap.min.css',
            'https://unpkg.com/kidlat-css/css/kidlat.css',

        ],
        timeout: 700,
        autoClose: true,
        windowTitle: window.document.title,

    }
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],

        props: ['printDetails', 'headerFooter', 'isTouchScreen','saleSizeAssortment'],
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


