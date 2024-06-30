<template>
    <div>
        <div hidden id='purchaseInvoice' class="col-md-12">
            <!--page1-->
            <div>
                <!--HEADER-->
                <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
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
                            <table class="text-right" v-if="arabic=='true'">
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
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">Expense Bill/ Expense Document</span>
                            </p>
                        </div>
                    </div>
                </div>
                <div style="height:60mm;" v-else></div>

                <div style="height:40mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row mt-3">
                        <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                            <div>
                                <!--Row 1-->
                                <div class="row">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Bill No:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.registrationNo}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;" v-if="arabic=='true'">:رقم الفاتوره</div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Account:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.billerAccount}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;" v-if="arabic=='true'">:الحساب</div>
                                    </div>
                                </div>

                                <!--Row 2-->
                                <div class="row">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.date}}</div>
                                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;" v-if="arabic=='true'">:تاريخ</div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Due Date.:</div>
                                        <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{list.dueDate}}</div>
                                        <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important" v-if="arabic=='true'">
                                            : تاريخ الاستحقاق
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-12" style="display:flex;">
                                        <div style="width:13%; font-weight:bolder;text-align:right; color:black !important;"><span>Refrence</span>:</div>
                                        <div style="width:76%;text-align:left;font-weight:bold;color:black !important;padding-left:5px">{{list.reference}}</div>
                                        <div style="width:11%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="arabic=='true'">  :المرجعي</div>
                                    </div>
                                </div>

                             
                                <div class="row">
                                    <div class="col-md-12" style="display:flex;">
                                        <div style="width:13%; font-weight:bolder;text-align:right; color:black !important;"><span>Narration</span>:</div>
                                        <div style="width:76%;text-align:left;font-weight:bold;color:black !important;padding-left:5px">{{list.narration}}</div>
                                        <div style="width:11%; font-weight:bolder; font-size:15px !important;color:black !important;" v-if="arabic=='true'">: السرد</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div style="height:205mm;border:2px solid !important;">
                    <div class="row ">
                        <div class="col-md-12 ">
                            <table class="table">
                                <tr class="heading" style="font-size:16px !important;padding-top:5px;border-bottom:1px solid" v-if="invoicePrint == 'العربية'">
                                    <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-left" style="width:40%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Description</th>
                                    <th class="text-left" style="width:30%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Account</th>
                                    <th class="text-center" style="width:28%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Amount</th>
                                </tr>
                                <tr class="heading" style="font-size:16px;border-bottom:1px solid" v-else>
                                    <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-left" style="width:40%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Description</th>
                                    <th class="text-left" style="width:30%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Account</th>
                                    <th class="text-center" style="width:28%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Amount</th>
                                </tr>


                                <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in list.purchaseBillItems" v-bind:key="item.id">
                                    <td class="text-left" style="padding-top:8px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                    <td class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.description}}</td>
                                    <td class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.accountName }}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.amount.toFixed(3).slice(0,-1)}}</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div style="height:20mm;margin-top:1mm;">
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-bordered tbl_padding">
                                <tr>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold;text-align:right">Grand Total<span style="font-weight:bold;color:black !important;">/ المبلخ الاجمالي</span></td>
                                    <td style="padding: 3px 7px;font-size:16px;border:2px solid black !important;color:black !important;font-weight:bold"> {{parseFloat(totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
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
                                    {{ $t('BillsReport.ReceivedBy') }}::
                                </td>
                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                    {{ $t('BillsReport.ApprovedBy') }}::
                                </td>
                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                    {{ $t('BillsReport.PreparedBy') }}::
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
                <!--Footer-->
                <div class="col-md-12 " style="height: 42mm;border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-4">
                            <u><b><span style="font-size:18px;color: black !important;font-weight:bold;">Terms & Conditions</span></b></u><br />
                            <span style="font-size:14px;color: black !important;font-weight:bold;" v-html="headerFooters.footerEn">
                                
                            </span>
                        </div>

                        <div class="col-md-4 pt-4">

                            <div style=" width:90%;border:1px solid;text-align:center;font-size:15px;font-weight:bold;">
                                Bank Details &nbsp; تفاصيل البنك
                            </div>
                            <div style="width:90%;display:flex;border:1px solid;">
                                <div style="width:70%;text-align:center;border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                    {{headerFooters.bankAccount1}}
                                </div>
                                <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                    {{headerFooters.bankIcon1}}
                                </div>
                            </div>
                            <div style="width:90%;display:flex;border:1px solid;">
                                <div style="width:70%;text-align:center; border-right:1px solid;font-size:14px;color: black !important;font-weight:bold;">
                                    {{headerFooters.bankAccount2}}
                                </div>
                                <div style="width:30%;text-align:center;font-size:14px;color: black !important;font-weight:bold;">
                                    {{headerFooters.bankIcon2}}
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 text-right">
                            <u><b><span style="font-size:20px;font-weight:bold;">البنود و الظروف</span></b></u><br />
                            <span class=" text-center" style="font-size:16px;color: black !important;font-weight:bold;" v-html="headerFooters.footerAr">
                                
                            </span>
                        </div>

                    </div>
                </div>
                <div class="row" v-if="isHeaderFooter=='true'">
                    <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                    <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
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
        props: ['printDetails', 'headerFooter'],
       
        data: function () {
            return {
              
              
                isHeaderFooter: '',
                invoicePrint: '',
                arabic: '',
                english: '',
                list: {
                    registrationNo: '',
                    date: '',
                    dueDate: '',
                    purchaseBillItems:[]  
                },
                render: 0,
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
               
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
            totalAmount: function () {
                
                return this.list.purchaseBillItems.reduce(function (a, c) { return a + Number((c.amount) || 0) }, 0)
            },
        },
        methods: {
           
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
            if (this.printDetails.purchaseBillItems.length > 0) {
                this.list = this.printDetails;
                this.headerFooters = this.headerFooter;
                this.list.date = moment().format('DD MMM YYYY');
                this.list.dueDate = moment().format('DD MMM YYYY');

                setTimeout(function () {
                    root.printInvoice();
                }, 125)
            }
        },

    }
</script>

