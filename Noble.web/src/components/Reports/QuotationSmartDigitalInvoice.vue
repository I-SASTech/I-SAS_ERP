<template>
    <div hidden ref="mychildcomponent"  id='purchaseInvoice' class="col-md-12" style="color:black !important;background-color:white !important;width:100%">
        <!--HEADER-->
        <div class="row" v-if="headerFooters.headerImage!='' && headerFooters.headerImage!=null && headerFooters.headerImage!=undefined" style="background-color:white !important">
            <div class="col-sm-12 " style="background-color:white !important">
                <img :src="'data:image/png;base64,' + headerFooters.headerImage" style="width:100%; max-height:400px;">
            </div>
        </div>
        <div style="height:42mm;background-color:white !important" v-else></div>

        <div class="row pt-2 pb-2" style="background-color:white !important">
            <div class="col-7" style="background-color:white !important">
                <div class="row" style="font-size:16px;background-color:white !important ">
                    <div class="col-2" style="text-align:left;font-weight:bold;padding-right:0px !important;margin-right:0px !important;background-color:white !important">
                        <p style="font-size:16px;color:black !important;font-weight:normal;margin-bottom:0!important;padding-left:0px !important;margin-left:0px !important">To:</p>
                    </div>
                    <div class="col-10" style="padding:0px !important;margin:0px !important;background-color:white !important">
                        <p style="font-size:14px !important;color:black !important;font-weight:normal;margin-bottom:0!important;padding-left:2px !important;margin:0px !important;padding-right:0px !important;margin-right:0px !important"> <span style="font-weight:bold;color:#2304FF !important;font-size:16px"> {{list.customerNameEn}}</span></p>

                    </div>
                    <div class="col-2" style="text-align:left;font-weight:bold;padding-right:0px !important;margin-right:0px !important;background-color:white !important">
                        <p style="font-size:16px;color:black !important;font-weight:normal;margin-bottom:0!important;padding-left:0px !important;margin-left:0px !important;background-color:white !important">Address:</p>
                    </div>
                    <div class="col-10" style="padding:0px !important;margin:0px !important">
                        <p style="font-size:14px !important;color:black !important;font-weight:normal;margin-bottom:0!important;padding-left:2px !important;margin:0px !important"> <span style="font-weight:bold;color:#2304FF !important;font-size:16px">{{list.customerAddress}}</span></p>

                    </div>

                    <div class="col-2" style="text-align:left;font-weight:bold;padding-right:0px !important;margin-right:0px !important;background-color:white !important">
                        <p style="font-size:16px;color:black !important;font-weight:normal;margin-bottom:0!important;padding-left:0px !important;margin-left:0px !important;background-color:white !important">Attn:</p>
                    </div>
                    <div class="col-10" style="padding:0px !important;margin:0px !important;background-color:white !important">
                        <p style="font-size:16px;color:black !important;font-weight:normal;margin-bottom:0!important;padding-left:2px !important;margin:0px !important;background-color:white !important"> <span style="font-weight:bold;color:#2304FF !important;font-size:16px"></span></p>

                    </div>
                    <div class="col-2" style="text-align:left;font-weight:bold;padding-right:0px !important;margin-right:0px !important;background-color:white !important">
                        <p style="font-size:14px;color:black !important;font-weight:normal;margin-bottom:0!important;padding-left:0px !important;margin-left:0px !important;background-color:white !important">Contact No:</p>
                    </div>

                    <div class="col-10" style="padding:0px !important;margin:0px !important;background-color:white !important">
                        <p style="font-size:16px;color:black !important;font-weight:normal;margin-bottom:0!important;padding-left:2px !important;margin:0px !important;background-color:white !important"> <span style="font-weight:bold;color:#2304FF !important;font-size:16px"> {{list.mobile}}</span></p>

                    </div>






                    <div class="col-2" style="text-align:left;font-weight:bold;padding-right:0px !important;margin-right:0px !important;background-color:white !important">
                        <p style="font-size:16px;color:black !important;font-weight:normal;margin-bottom:0!important;padding-left:0px !important;margin-left:0px !important;background-color:white !important">Email:</p>
                    </div>
                    <div class="col-10" style="padding:0px !important;margin:0px !important;background-color:white !important">
                        <p style="font-size:16px;color:black !important;font-weight:normal;margin-bottom:0!important;padding-left:2px !important;margin:0px !important;background-color:white !important"> <span style="font-weight:bold;color:#2304FF !important;font-size:16px"> {{list.email}}</span></p>

                    </div>


                </div>
                <!--<p style="font-size:14px;color:black !important;font-weight:bold;margin-bottom:0!important;">Address :<span style="font-weight:bold;color:#2304FF !important;font-size:16px;padding-left:10px"></span></p>
                <p style="font-size:14px;color:black !important;font-weight:bold;margin-bottom:0!important">Tel: <span style="font-weight:bold;color:#2304FF !important;font-size:16px;padding-left:10px">{{list.mobile}}</span> </p>
                <p style="font-size:14px;color:black !important;font-weight:bold;margin-bottom:0!important">Fax: <span style="font-weight:bold;color:#2304FF !important;font-size:16px;padding-left:10px"></span> </p>
                <p style="font-size:14px;color:black !important;font-weight:bold;margin-bottom:0!important">Gsm: <span style="font-weight:bold;color:#2304FF !important;font-size:16px;padding-left:10px"></span> </p>
                <p style="font-size:14px;color:black !important;font-weight:bold;margin-bottom:0!important">Email: <span style="font-weight:bold;color:#2304FF !important;font-size:16px;padding-left:10px">{{list.email}}</span> </p>-->
            </div>
            <div class="col-2" style="padding:0px !important; margin:0px !important;margin-left:0px !important;padding-left:0px !important;background-color:white !important">
                <div  v-if="headerFooters.proposalImageForPrint!='' && headerFooters.proposalImageForPrint!=null && headerFooters.proposalImageForPrint!=undefined">
                    <img :src="'data:image/png;base64,' + headerFooters.proposalImageForPrint" style="width:100% !important; height:150px;">

                </div>
                <div v-else style="width:100% !important; height:150px;" >

                </div>
            </div>
            <div class="col-3" style="background-color:yellow !important">
                <p style="font-size:22px;color:black !important;font-weight:bold;margin-bottom:0!important;text-align:center">{{list.purpose}}</p>
                <p style="font-size:22px;color:black !important;font-weight:bold;margin-bottom:0!important;text-align:center">{{list.registrationNo}}</p>
                <p style="font-size:16px;color:black !important;font-weight:bold;margin-bottom:0!important;background-color:#4BACC6 !important">{{list.for}}</p>
                <p style="font-size:18px;color:black !important;font-weight:bold;margin-bottom:0!important;"><span>Date:</span><span>{{list.date}}</span> </p>
                <p style="font-size:18px;color:black !important;font-weight:bold;margin-bottom:0!important;background-color:white"><span>VAT No:</span><span>{{headerFooters.company.vatRegistrationNo}}</span> </p>

            </div>
        </div>
        <div class="row pt-2" style="border-top:1px solid black;background-color:white !important">
            <div class="col-sm-9 " style="background-color:white !important">

                <div v-if="headerFooters.tagImageForPrint!='' && headerFooters.tagImageForPrint!=null && headerFooters.tagImageForPrint!=undefined">
                    <img :src="'data:image/png;base64,' + headerFooters.tagImageForPrint" style="width:100%; height:130px !important;background-color:white !important">

                </div>
                <div v-else style="width:100%; height:130px !important;background-color:white !important">

                </div>

            </div>
            <div class="col-sm-3 text-center pt-1 " style="background-color: white !important">
                <vue-qrcode v-bind:value="qrValue" style="width:120px;" />


            </div>

        </div>

        <div class="row " style="background-color: white !important;width:100%;height:1050px !important">
            <div class="col-md-12 " style="background-color: white !important;width:100%">
                <table class="table " style="border: 0 !important; height:180mm;width:100%;background-color: white !important">
                    <tr class="heading" style="font-size:16px !important;width:100%">
                        <th class="text-center" style="width:3%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#C00000 !important;background-color:#FABF8F !important;border:1px solid #000000;">No</th>
                        <th class="text-center" style="width:37%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#C00000 !important;background-color:#FABF8F !important;border:1px solid #000000;">Description </th>
                        <th class="text-center" style="width:10%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#C00000 !important;background-color:#FABF8F !important;border:1px solid #000000;">Picture  </th>
                        <th class="text-center" style="width:12%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#C00000 !important;background-color:#FABF8F !important;border:1px solid #000000;">Model   </th>
                        <th class="text-center" style="width:7%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#C00000 !important;background-color:#FABF8F !important;border:1px solid #000000;">U.Price  </th>
                        <th class="text-center" style="width:7%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#C00000 !important;background-color:#FABF8F !important;border:1px solid #000000;">Qty </th>
                        <th class="text-center" style="width:7%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#C00000 !important;background-color:#FABF8F !important;border:1px solid #000000;"> Discount </th>
                        <th class="text-center" style="width:17%;color:black !important;padding-top:8px !important; padding-bottom:8px !important;color:#C00000 !important;background-color:#FABF8F !important;border:1px solid #000000;">Total Price   </th>
                    </tr>

                    <template v-for="(item, index) in list.saleOrderItems">
                        <tr style="font-size: 15px; font-weight: bold; width: 100%" v-if="index<12" v-bind:key="item.id">
                            <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;">{{index+1}}</td>
                            <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;font-size:14px;">{{item.productNameEn}}</td>
                            <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;font-size:10px;" v-if="item.productImage=='' || item.productImage==null || item.productImage==undefined">
                            </td>
                            <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;font-size:10px;" v-else>
                                <img :src="'data:image/png;base64,' + item.productImage" style="width:100%; max-height:50px;">
                            </td>
                            <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;">{{item.product==null?'' : item.product.styleNumber}}</td>
                            <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;background-color:#FABF8F !important">{{item.isFree? (item.unitPrice>0? item.unitPrice.toFixed(3).slice(0,-1) : '-') : item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                            <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;">{{item.isFree? (item.quantity>0? item.quantity : '-') : item.quantity }}</td>
                            <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;">{{item.isFree? '-' : item.discountAmount }}</td>
                            <td class="text-center" style="color:black !important;background-color:#FABF8F !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{item.isFree? '-' : item.total.toFixed(3).slice(0,-1)}}</span></td>
                        </tr>
                    </template>

                    <tr style="font-size:15px;font-weight:bold;width:100%" v-for="(item, index) in emptyListCount" v-bind:key="index" >
                        <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;">&nbsp;</td>
                        <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"></td>
                        <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"></td>
                        <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"></td>
                        <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;background-color:#FABF8F !important"></td>
                        <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"></td>
                        <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"></td>
                        <td class="text-center" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"></td>
                    </tr>




                    <tr style="font-weight:bold;">
                        <td class="text-left" style="color:black !important;font-size:19px;background-color:#FABF8F !important;padding-top:8px !important; padding-bottom:8px !important;border:1px solid black !important;text-align:right !important" colspan="7"> <span>Sub Total</span> </td>
                        <td class="text-center" style="color:black !important;background-color:#FABF8F !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateTotalExclVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
                    </tr>
                    <tr style="font-weight:bold;">
                        <td class="text-center" style="color: #ffffff !important; background-color: #000000 !important;  padding:0px !important;border: 1px solid black;" colspan="4">Bank Details / {{headerFooters.bankIcon1}}  {{headerFooters.bankAccount1}}</td>
                        <td class="text-left" style="color:black !important;font-size:19px ;border:0!important;background-color:red !important;padding:0px 2px 0px 0px !important;text-align:right !important;border:1px solid black !important" colspan="3">Discount</td>
                        <td class="text-center" style="color:black !important;background-color: #CFCEC9 !important;font-size:19px;border:1px solid black;background-color:red !important;padding:0px !important"><span style="float:left;padding-left:3px !important">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
                    </tr>
                    <tr style="font-weight:bold;">
                        <td class="text-center" style="color: #ffffff !important; padding:0px !important" colspan="4"></td>
                        <td class="text-left" style="color:black !important;font-size:19px ;border:0!important;background-color:gray !important;padding:0px 2px 0px 0px !important;text-align:right !important" colspan="3">Total After Discount</td>
                        <td class="text-center" style="color:black !important;background-color: #CFCEC9 !important;font-size:19px;border:1px solid black;background-color:gray !important;padding:0px !important"><span style="float:left;padding-left:3px !important">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateTotalExclVAT-calulateDiscountAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
                    </tr>
                    <tr style="font-weight:bold;">
                        <td class="text-left" style=" color: black !important; padding-top: 8px !important; padding-bottom: 8px !important; border-top:1px solid white;border-bottom:1px solid white;" colspan="4"></td>
                        <td class="text-center" style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border-top:1px solid silver !important;border-bottom:1px solid silver !important;background-color:silver !important;text-align:right !important" colspan="3"><span>VAT 15%</span>   </td>
                        <td class="text-center" style="color:black !important;background-color:silver !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
                    </tr>
                    <tr style="font-weight:bold;">
                        <td class="text-left" style="color: #ffffff !important; background-color: #000000 !important; padding-top: 2px !important; padding-bottom: 2px !important; border:1px solid #000000;" colspan="3">Terms and conditions:-</td>
                        <td class="text-left" style="color: #000000 !important; padding-top: 2px !important; padding-bottom: 2px !important; border-bottom:1px solid #ffffff !important;"></td>
                        <td class="text-center" style="color:black !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border-bottom:1px solid #FABF8F !important;background-color:#FABF8F !important;text-align:right !important" colspan="3"><span>Total Amount</span>  </td>
                        <td class="text-center" style="color:black !important;background-color:#FABF8F !important;font-size:19px;padding-top:8px !important; padding-bottom:8px !important;border:1px solid #000000;"><span style="float:left;">{{currency}}</span> <span style="float:right;">{{parseFloat(calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>
                    </tr>
                    <tr style="font-weight:bold;">
                        <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:11px!important;border:0!important;padding-left:0 !important;font-size:16px !important;" colspan="7"><span style="color:#2304FF !important">Validity Period:</span> <span>15 days for current prices</span></td>
                    </tr>
                    <tr style="font-weight:bold;">
                        <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:11px!important;border:0!important;padding-left:0 !important;font-size:16px !important;" colspan="7"><span style="color:#2304FF !important">Delivery:</span> <span>4-12 weeks from date to order</span></td>
                    </tr>
                    <tr style="font-weight:bold;">
                        <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:11px!important;border:0!important;padding-left:0 !important;font-size:16px !important;" colspan="7"><span style="color:#2304FF !important">Payment:</span> <span>100 % Advance Payment</span></td>
                    </tr>
                    <tr style="font-weight:bold;">
                        <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:11px!important;border:0!important;padding-left:0 !important;font-size:16px !important;" colspan="7"><span style="color:#2304FF !important">Training: </span> <span>Free visits for one month and any extra training after the month it will be 1000 SR. per day.</span></td>
                    </tr>
                    <tr style="font-weight:bold;">
                        <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:11px!important;border:0!important;padding-left:0 !important;font-size:16px !important;" colspan="7"><span style="color:#2304FF !important">Warranty:  </span> <span>1 years warrantee for all manufacturing defects plus support.</span></td>
                    </tr>
                    <tr style="font-weight:bold;">
                        <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:11px!important;border:0!important;padding-left:0 !important;font-size:16px !important;" colspan="7"><span style="color:#2304FF !important">Note  </span> <span>Please confirm us in a written with this proposal to do the necessary for this items.</span></td>
                    </tr>
                    <tr style="font-weight:bold;">
                        <td class="text-left" style="color:black !important;padding-top:8px !important; padding-bottom:8px !important;font-size:11px!important;border:0!important;padding-left:0 !important;font-size:16px !important;" colspan="7"><span style="color:lightblue !important">Note  </span> <span>Please confirm us in a written with this proposal to do the necessary for this items.</span></td>
                    </tr>


                </table>
            </div>
        </div>

        <!--<div v-else style="height:30mm;"></div>-->
    </div>

</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    

    const options = {
        name: '',
        specs: [
            'fullscreen=no',
            'titlebar=yes',
            'scrollbars=yes'
        ],
        styles: [
            '/assets/css/bootstrap.min.css',

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

            calulateDiscountAmount1: function () {
                return this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number(c.discountAmount || 0)) }, 0)
            },
            calulateBundleAmount1: function () {
                return this.list.saleOrderItems.reduce(function (a, c) { return a + (c.isFree ? 0 : Number(c.bundleAmount || 0)) }, 0)
            },
            calulateNetTotalWithVAT: function () {
                var total = this.list.saleOrderItems.reduce(function (a, c) { return a + c.isFree ? 0 : (Number((c.total + c.includingVat) || 0)) }, 0);
                var grandTotal = parseFloat(total) - (this.calulateDiscountAmount1() + this.calulateBundleAmount1())
                return (parseFloat(grandTotal).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            calulateTotalVATofInvoice: function () {
                var total = this.list.saleOrderItems.reduce(function (a, c) { return a + c.isFree ? 0 : (Number((c.includingVat + c.inclusiveVat) || 0)) }, 0);
                return (parseFloat(total).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            GetTLVForValue: function (tagNumber, tagValue) {
                var tagBuf = Buffer.from([tagNumber], 'utf-8')
                var tagValueLenBuf = Buffer.from([tagValue.length], 'utf-8')
                var tagValueBuf = Buffer.from(tagValue, 'utf-8')
                var bufsArray = [tagBuf, tagValueLenBuf, tagValueBuf]
                return Buffer.concat(bufsArray)
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

            if (this.printDetails.saleOrderItems.length > 0) {
                this.list = this.printDetails;
                this.headerFooters = this.headerFooter;

                {

                    var sellerNameBuff = root.GetTLVForValue('1', this.headerFooters.company.nameEnglish)
                    var vatRegistrationNoBuff = root.GetTLVForValue('2', this.headerFooters.company.vatRegistrationNo)
                    var timeStampBuff = root.GetTLVForValue('3', this.list.date)
                    var totalWithVat = root.GetTLVForValue('4', this.calulateNetTotalWithVAT())
                    var totalVat = root.GetTLVForValue('5', this.calulateTotalVATofInvoice())
                    var tagArray = [sellerNameBuff, vatRegistrationNoBuff, timeStampBuff, totalWithVat, totalVat]
                    var qrCodeBuff = Buffer.concat(tagArray)
                    root.qrValue = qrCodeBuff.toString('base64')

                    var count = this.printDetails.saleOrderItems.length;
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


