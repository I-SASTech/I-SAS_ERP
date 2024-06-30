<template>
    <div>
        <div hidden id='voucherInvoice'>

            <!--HEADER-->
            <div class="row" style="height:35mm;" v-if="isHeaderFooter=='true'">
                <div class="col-4 ">
                    <table class="text-left" style="margin-left:0px !important ; margin-right:0px !important;padding-left:0px !important;padding-right:0px !important">
                        <tr>
                            <td>
                                <p>
                                    <span style="font-size:25px;color:black !important;font-weight:bold;">{{headerFooters.company.nameEnglish}}</span><br />
                                    <span style="font-size:16px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryEnglish}}</span><br />
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
                <div class=" col-4  text-center my-5" style="padding:0px !important; margin:0 !important">
                    <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important">
                </div>
                <div class="col-4" style=" margin-right:0px !important;padding-right:0px !important;text-align:right">
                    <table style=" margin-right:0px !important;padding-right:0px !important;text-align:right">
                        <tr>
                            <td style=" margin-right:0px !important;padding-right:0px !important;text-align:right">
                                <p style=" margin-right:0px !important;padding-right:0px !important;text-align:right">
                                    <span style="font-size:25px;color:black !important;font-weight:bold;">{{headerFooters.company.nameArabic}}</span><br />
                                    <span style="font-size:17px;color:black !important;font-weight:bold;">{{headerFooters.company.categoryArabic}}</span><br />
                                    <span style="font-size:16px;color:black !important;font-weight:bold;">رقم ضريبة القيمة المضافة: {{headerFooters.company.vatRegistrationNo}}</span><br />
                                    <span style="font-size:16px;color:black !important;font-weight:bold;">رقم السجل التجاري :{{headerFooters.company.companyRegNo}}</span><br />
                                    <span style="font-size:15px;color:black !important;font-weight:bold;">
                                        هاتف: {{headerFooters.company.phoneNo}}
                                    </span>
                                </p>
                            </td>
                        </tr>
                    </table>
                </div>

            </div>
            <div style="height:35mm;" v-else></div>

            <div class="col-md-12 col-12 mt-2 " style="border:1px solid;border-color:black !important">
                <div class="row mt-2" style="margin-bottom:10px !important;height:10mm">
                    <div class="col-md-12">
                        <p style="text-align: center; margin: 0px; padding: 0px; line-height: 1; ">
                            <span style="font-size:18px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='BankReceipt' && list.paymentMode==0">Customer Cash Receipt</span>
                            <span style="font-size:18px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='BankReceipt' && list.paymentMode==1">Customer Bank Receipt</span>
                            <span style="font-size:18px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='BankPay' && list.paymentMode==0">Supplier Cash Pay</span>
                            <span style="font-size:18px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='BankPay' && list.paymentMode==1">Supplier Bank Pay</span>
                            <span style="font-size:18px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='BankPay' && list.paymentMode==5">Supplier Bank Pay</span>
                            <span style="font-size:18px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='BankReceipt' && list.paymentMode==5">Customer Bank Receipt</span>

                            <span style="font-size:18px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='PettyCash' ">Petty Cash</span>

                        </p>
                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-md-6 col-6" style="display:flex;">
                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;font-size:14px !important">Date:</div>
                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{convertMainDate(list.date)}}</div>
                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:تاريخ</div>
                    </div>
                    <div class="col-md-6 col-6" style="display:flex;">
                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important">Voucher No:</div>
                        <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{list.voucherNumber}}</div>
                        <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                            :رقم القسيمة
                        </div>
                    </div>
                </div>
                <div class="row mt-2 mb-1">
                    <div class="col-md-6 col-6" style="display:flex;">
                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;font-size:14px !important" v-if="list.paymentMode==0">Cash Account:</div>
                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;font-size:14px !important" v-else-if="list.paymentMode==1">Bank Account:</div>
                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;font-size:14px !important" v-else-if="list.paymentMode==5">Advance Bank Account:</div>
                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;font-size:14px !important" v-else>Bank Account:</div>
                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.bankCashAccountName}}</div>
                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;" v-if="list.paymentMode==0">:حساب نقدي</div>
                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;" v-else-if="list.paymentMode==1">:حساب البنك</div>
                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;" v-else-if="list.paymentMode==5">:الحساب المصرفي المسبق</div>
                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;" v-else>:حساب البنك</div>
                    </div>
                    <div class="col-md-6 col-6" style="display:flex;">
                        <div style="width:30%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important">Employee:</div>
                        <div style="width:45%; text-align:center;font-weight:bold;color:black !important;">{{list.contactAccountName}}</div>
                        <div style="width:25%;font-weight:bolder;color:black !important;font-size:15px !important">
                            :موظف
                        </div>
                    </div>

                    <div class="col-md-6 col-6 mt-3" style="display:flex;">
                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;font-size:14px !important">Payment Type:</div>
                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{paymentMode(list.paymentMode)}}</div>
                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:نوع الدفع</div>
                    </div>
                    <div class="col-md-6 col-6" style="display:flex;" v-if="list.paymentMode==1">
                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;font-size:14px !important">Payment Method:</div>
                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{paymentMethod(list.paymentMethod)}}</div>
                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:طريقة الدفع </div>
                    </div>
                    <div class="col-md-6 col-6 mt-2 mb-2" style="display:flex;" v-if="list.paymentMethod==1">
                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;font-size:14px !important">Cheque:</div>
                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.chequeNumber}}</div>
                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:التحقق من</div>
                    </div>
                    <div class="col-md-6 col-6" style="display:flex;" v-if="formName=='PettyCash'">
                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;font-size:14px !important">Petty Cash Type:</div>
                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{pettyCash(list.pettyCash)}}</div>
                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:نوع النقد</div>
                    </div>
                </div>



            </div>
            <div class="row mt-3" style="">

                <div class="col-md-12 col-12">
                    <table class="table table-bordered tbl_padding">
                        <tr>
                            <td style="padding: 3px 7px;font-size:16px;font-weight:bold; border:1px solid black !important;color:black !important;text-align:right;border-color:black !important">Total Amount/المبلغ الإجمالي</td>
                            <td style="padding: 3px 7px;font-size:16px;font-weight:bold;border:1px solid black !important;color:black !important;text-align:right;border-color:black !important">{{currency}}  {{parseFloat(list.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}} </td>
                        </tr>
                        <tr>
                            <td style="padding: 3px 7px;font-size:16px;font-weight:bold; border:1px solid black !important;color:black !important;text-align:right;border-color:black !important">Amount in Words/ المبلغ بالكلمات</td>
                            <td style="padding: 3px 7px;font-size:16px;font-weight:bold;border:1px solid black !important;color:black !important;text-align:right;border-color:black !important">{{$toWords((list.amount))  }} </td>
                        </tr>
                    </table>



                </div>

            </div>

            <!--Footer-->
            <div class="col-md-12 col-12  ">
                <div class="row mt-2">
                    <div class="col-md-2 col-2">
                        <vue-qrcode v-bind:value="qrValue" style="width:100px;" />
                    </div>
                    <div class="col-md-10 col-10" style="display:flex;">
                        <div style="width:20%; font-weight:bolder;text-align:left; color:black !important;font-size:14px !important"><span>Remarks/For/ملاحظات</span>:</div>
                        <div style="width:80%;font-weight:bold;color:black !important;" class="text-justify">{{list.narration}}</div>
                    </div>
                </div>
                <div class="row mt-3">
                    <div class="col-md-6 col-6" style="display:flex;">
                        <div style="width:28%;font-weight:bolder;text-align:right;color:black !important;font-size:14px !important">User:</div>
                        <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.approvedBy}}</div>
                        <div style="width:22%;font-weight:bolder; padding-right:20px;font-size:15px !important;color:black !important;">:مستخدم</div>
                    </div>
                    <div class="col-md-6 col-6" style="display:flex;">
                        <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;font-size:14px !important">Date:</div>
                        <div style="width:50%; text-align:center;font-weight:bold;color:black !important;">{{convertMainDate(list.approvedDate)}}</div>
                        <div style="width:22%;font-weight:bolder;color:black !important;font-size:15px !important">
                            :تاريخ
                        </div>
                    </div>
                </div>

            </div>
            <!--Footer-->
            <div class="row mt-2" v-if="isHeaderFooter=='true'">
                <div class="col-md-6;" style="color: black !important;font-size:15px;font-weight:bold;">{{headerFooters.company.addressEnglish}}</div>
                <div class="col-md-6 text-right" style="color: black !important;font-size:16px;font-weight:bold;">{{headerFooters.company.addressArabic}}</div>
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
            'https://unpkg.com/kidlat-css/css/kidlat.css',
            './assets/css/Custom.css' // <- inject here
        ],
        timeout: 700,
        autoClose: true,
        windowTitle: window.document.title,

    }
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        components: {
            
        },
        props: ['formName', 'printId', 'headerFooter'],
        data: function () {
            return {
                qrValue: "",
                isHeaderFooter: '',
                invoicePrint: '',
                arabic: '',
                english: '',
                list: [],
                render: 0,
                currency: '',
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

        mounted: function () {
            this.currency = localStorage.getItem('currency');
            this.headerFooters = this.headerFooter;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.invoicePrint = localStorage.getItem('InvoicePrint');

            var root = this;
            if (this.printId != '00000000-0000-0000-0000-000000000000' && this.printId != '' && this.printId != undefined) {
                root.getDetails();
            }
        },

        methods: {
            convertMainDate: function (x) {

                if (x == undefined)
                    return '';
                return moment(x).format('DD MMM YYYY');
            },
            paymentMode: function (x) {
                
                {

                    if (x == 0) {
                        return 'Cash';
                    }
                    if (x == 1) {
                        return 'Bank';
                    } if (x == 5) {
                        return 'Advance';
                    }

                }
            },
            paymentMethod: function (x) {
                if (this.invoicePrint == 'العربية') {
                    if (x == 1) {
                        return 'التحقق من';
                    }
                    else if (x == 2) {
                        return 'تحويل';
                    }
                    else if (x == 3) {
                        return 'الوديعة';
                    }
                    else {
                        return '';
                    }


                }
                else {
                    if (x == 1) {
                        return 'Cheque';
                    }
                    else if (x == 2) {
                        return 'Transfer';
                    }
                    else if (x == 3) {
                        return 'Deposit';
                    }
                    else {
                        return '';
                    }


                }
            },
            convertDate: function (x) {
                return moment(x).format('DD MMM YYYY');
            },

            getDetails: function () {
                var root = this;

                root.$https.get("/PaymentVoucher/TemporaryCashAllocationDetails?id=" + this.printId, { headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },})
                    .then(function (response) {
                        if (response.data != null) {
                            
                            root.list = response.data.message;
                            root.qrValue = 'Voucher No :' + root.list.voucherNumber + '\nAmount:' + root.list.amount;
                            setTimeout(function () {
                                root.printInvoice();
                            }, 125)
                        }
                    });
            },

            printInvoice: function () {
                this.$htmlToPaper('voucherInvoice', options, () => {
                    console.log('No Rander the Page');
                });
            }
        }
    }
</script>
