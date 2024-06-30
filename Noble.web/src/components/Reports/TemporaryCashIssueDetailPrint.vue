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
                                <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">Temporary Cash Issue Detail</span>
                            </p>
                        </div>
                    </div>
                </div>
                <div style="height:60mm;" v-else></div>

                <div style="height:20mm;margin-top:1mm; border:2px solid #000000;">
                    <div class="row">
                        <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                            <div>
                                <!--Row 1-->
                                <div class="row">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Employee:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{employeeName}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:موظف </div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Opening:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{amount}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:افتتاح </div>
                                    </div>
                                </div>
                                <!--Row 2-->
                                <div class="row">
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Date:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{date}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:تاريخ </div>
                                    </div>
                                    <div class="col-md-6" style="display:flex;">
                                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Note:</div>
                                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{temporaryCashRequest.note}}</div>
                                        <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:ملحوظة </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--INFORMATION-->
                <div style="height:220mm;border:2px solid !important;">
                    <div class="row ">
                        <div class="col-md-12 ">
                            <table class="table table-striped table-hover table_list_bg">
                                <tr class="heading" style="font-size:16px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                    <th style="width:5%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-left" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">تاريخ</th>
                                    <th class="text-left" style="width:20%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">موظف</th>
                                    <th class="text-left" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">فاتورة </th>
                                    <th class="text-left" style="width:15%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">مقدار</th>
                                    <th class="text-left" style="width:15%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">المبلغ العائد</th>
                                    <th class="text-left" style="width:20%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">وثيقة</th>
                                    
                                </tr>
                                <tr class="heading" style="font-size:16px;" v-else>
                                    <th style="width:5%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-left" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;">Date</th>
                                    <th class="text-left" style="width:20%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Employee</th>
                                    <th class="text-left" style="width:10%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Voucher#</th>
                                    <th class="text-right" style="width:15%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Amount</th>
                                    <th class="text-right" style="width:15%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Return Amount</th>
                                    <th class="text-right" style="width:25%;padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">Document</th>
                                    
                                </tr>


                                <tr style="font-size:15px;font-weight:bold;" v-for="(item, index) in temporaryCashRequest.temporaryCashRequestItems" v-bind:key="item.id">
                                    <td class="text-left" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                    <td class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.date}}</td>
                                    <td class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.userName}}</td>
                                    <td class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.registrationNo}}</td>
                                    <td class="text-right" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{currency}} {{RoundAmount(item.amount)}}</td>
                                    <td class="text-right" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;">{{currency}} {{RoundAmount(item.returnAmount)}}</td>
                                    <td class="text-right" style="border-top:0 !important; border-bottom:0 !important; padding-top:3px !important; padding-bottom:3px !important;color:black !important;"><span v-for="(expense) in item.temporaryCashIssueExpenses" v-bind:key="expense.id">{{expense.documentNo}} {{currency}} {{expense.amount}}, </span></td>
                                    
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div style="height:41mm;margin-top:1mm;">
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-bordered tbl_padding">
                                <tr>
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;">Page: 1 - 1</td>
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">إجمالي البند : {{ calulateTotalItem }} </td>
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Item : {{ calulateTotalItem }} </td>
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;">Total Amount<span style="font-weight:bold;color:black !important;">/ المبلغ الإجمالي</span></td>
                                    <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:15%;border:2px solid black !important;color:black !important;">{{parseFloat(calulateTotalQty).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="padding: 3px 7px;font-size:16px;border:2px solid black !important;border-top: 0px !important;color:black !important;font-weight:bold;">{{$toWords(calulateTotalQty)   }}</td>
                                    
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
                                    {{ $t('TheSaleOrder.ReceivedBy') }}::
                                </td>
                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                    {{ $t('TheSaleOrder.ApprovedBy') }}::
                                </td>
                                <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                    {{ $t('TheSaleOrder.PreparedBy') }}::
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
        props: ['printDetails', 'headerFooter', 'employeeName', 'amount'],
        
        data: function () {
            return {
                isHeaderFooter: '',
                invoicePrint: '',
                currency: '',
                arabic: '',
                english: '',
                date: '',
                temporaryCashRequest: {
                    date: '',
                    registrationNo: '',
                    userName: '',
                    note: '',
                    temporaryCashRequestItems: []
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
            calulateTotalItem: function () {
                return this.temporaryCashRequest.temporaryCashRequestItems.length;
            },
            calulateTotalQty: function () {
                return this.temporaryCashRequest.temporaryCashRequestItems.reduce(function (a, c) { return a + Number((c.amount) || 0) }, 0)
            },
        },

        methods: {
            RoundAmount: function (value) {
                return parseFloat(value).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
            },

            printInvoice: function () {

                var root = this;
                this.$htmlToPaper('purchaseInvoice', options, () => {
                    if (root.isTouchScreen === 'SaleOrder') {
                        root.$router.go('')
                    }
                    else {

                        console.log('No Rander the Page');
                    }
                });
            },
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.currency = localStorage.getItem('currency');

            var root = this;
            
            if (this.printDetails.length > 0) {

                this.temporaryCashRequest.temporaryCashRequestItems = this.printDetails;

                this.headerFooters = this.headerFooter;
                this.date = moment().format('DD MMM YYYY');

                setTimeout(function () {
                    root.printInvoice();
                }, 125)
            }
        },

    }
</script>

