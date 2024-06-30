<template>
    <div>
        <div hidden id='purchaseInvoice' class="col-md-12">
            <!--page1-->
            <div>
                <!--HEADER-->
                <div class="col-md-12" style="height:45mm;border:2px solid #000000;">
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
                    <div class="row">
                        <div class="col-md-12" style="margin-bottom:10px !important;height:10mm">
                            <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">Product Comparision Report</span>
                            </p>
                        </div>

                    </div>
                </div>
                <!--<div style="height:60mm;" v-else></div>-->

                <div style="height:15mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row mt-3">
                        <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">From Date:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{getDate(fromDate)}}</div>
                                    <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                                </div>
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">To Date.:</div>
                                    <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{getDate(toDate)}}</div>
                                    <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                                        : تاريخ الاستحقاق
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div class="col-md-12 mt-4">
                    <div class="row">
                        <div class="col-md-12" v-for="product in list" v-bind:key="product.name">
                           
                            <table class="table mb-0">
                                <thead class="thead-light">
                                    <tr>
                                        <td colspan="6">
                                            <h5>
                                                {{product.name}}
                                            </h5>
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                        <th colspan="2" class="text-center">
                                            Inventory
                                        </th>

                                        <th colspan="2" class="text-center">
                                            COGS

                                        </th>
                                        <th colspan="2" class="text-center">
                                            Sale

                                        </th>


                                    </tr>
                                </thead>
                                <thead class="thead-light">
                                    <tr>
                                        <th class="text-center">
                                            Quantity
                                        </th>

                                        <th class="text-center">
                                            Balance

                                        </th>
                                        <th class="text-center">
                                            Quantity
                                        </th>

                                        <th class="text-center">
                                            Balance

                                        </th>
                                        <th class="text-center">
                                            Quantity
                                        </th>

                                        <th class="text-center">
                                            Balance

                                        </th>


                                    </tr>
                                </thead>
                                <tbody>


                                    <tr>
                                        <th class="text-center">
                                            {{product.quantity}}
                                        </th>

                                        <th class="text-center">
                                            {{ product.inventoryBalance > 0 ? 'Dr' : 'Cr' }}  {{nonNegative(product.inventoryBalance)}}

                                        </th>
                                        <th class="text-center">
                                            {{product.quantity}}
                                        </th>

                                        <th class="text-center">
                                            {{ product.inventoryBalance > 0 ? 'Dr' : 'Cr' }} {{nonNegative(product.cogsBalance)}}
                                        </th>
                                        <th class="text-center">
                                            {{product.quantity}}
                                        </th>

                                        <th class="text-center">
                                            {{ product.inventoryBalance > 0 ? 'Dr' : 'Cr' }} {{nonNegative(product.saleBalance)}}

                                        </th>


                                    </tr>
                                </tbody>
                            </table>

                        </div>
                    </div>
                </div>




            </div>
        </div>
    </div>

</template>

<script>
    import moment from "moment";


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
        windowTitle: window.document.title,

    }
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['printDetails', 'headerFooter', 'formName', 'fromDate', 'toDate', 'invoice'],

        data: function () {
            return {


                isHeaderFooter: '',
                invoicePrint: '',
                arabic: '',
                english: '',
                list: [],
                render: 0,
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                }
            }
        },


        methods: {
            nonNegative: function (value) {



                return parseFloat(Math.abs(value)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },
            getDate: function (date) {
                return moment(date).format('l');
            },
            printInvoice: function () {

                this.$htmlToPaper('purchaseInvoice', options, () => {




                });
            },



        },
        mounted: function () {
            
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.invoicePrint = localStorage.getItem('InvoicePrint');
            var root = this;
            if (this.printDetails.length > 0) {
                this.list = this.printDetails;
                this.headerFooters = this.headerFooter;

                setTimeout(function () {
                    root.printInvoice();
                }, 125)
            }
        },

    }
</script>

