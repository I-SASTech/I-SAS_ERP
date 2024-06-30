<template>
    <div>
        <div ref="mychildcomponent" hidden id='purchaseInvoice' class="col-md-12" style="color:black !important">
            <!--HEADER-->
            <div class="col-md-12" style="height:45mm;border:2px solid #000000;" v-if="isHeaderFooter=='true'">
                <div class="row">
                    <div class="col-md-4 text-center">
                        <table class="m-auto">
                            <tr>
                                <td>
                                    <p class="text-center">
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
                        <img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; height:auto; max-height:120px; padding:5px !important; margin:0 !important">
                    </div>
                    <div class="col-md-4">
                        <table class="m-auto"  v-if="arabic=='true'">
                            <tr>
                                <td>
                                    <p class="text-center">
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
                    <div class="col-md-12">
                        <p style="text-align: center; margin: 0px;; padding: 0px; line-height: 1; ">
                            <span style="font-size:25px;color:black !important;font-weight:bold;padding-bottom:5px !important">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? 'Expenses' : 'نفقات' }}</span>
                        </p>
                    </div>
                </div>
            </div>
            <div style="height:60mm;" v-else></div>

            <div style="height:40mm;margin-top:1mm; border:2px solid #000000;">
                <div class="row">
                    <div class="col-md-12 ml-2 mr-2" style="height:6mm;font-size:16px;">
                        <div>
                            <!--Row 1-->
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Expense No:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{list.voucherNo}}</div>
                                    <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;"  v-if="arabic=='true'">:رقم المصاريف </div>
                                </div>
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Spent Date:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.date}}</div>
                                    <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;"  v-if="arabic=='true'">:تاريخ</div>
                                </div>
                            </div>
    <!--Row 1-->
                            <div class="row" v-if="IsExpenseAccount">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Payment Mode:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{paymentMode(list.paymentMode)}}</div>
                                    <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;"  v-if="arabic=='true'">:طريقة الدفع</div>
                                </div>
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Bank/Cash Account:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;" > {{ ($i18n.locale == 'en' ||isLeftToRight()) ? (list.accountName != '' && list.accountName != null) ? list.accountName : list.nameArabic : (list.nameArabic != '' && list.nameArabic != null) ? list.nameArabic : list.accountName}}</div>
                                    <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;">:حساب مصرفي / نقدي</div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Reference No:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{list.referenceNo}}</div>
                                    <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;"  v-if="arabic=='true'">:رقم المرجع</div>
                                </div>
                                <div class="col-md-6" style="display:flex;" v-if="IsExpenseAccount">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">TaxId:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;">{{list.taxId}}</div>
                                    <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;"  v-if="arabic=='true'">:الرقم الضريبي</div>
                                </div>
                            </div>
                            <div class="row" v-if="IsExpenseAccount">
                                <div class="col-md-6" style="display:flex;">
                                    <div style="width:28%; font-weight:bolder;text-align:right;color:black !important;">Name:</div>
                                    <div style="width:50%; text-align:center;color:black !important;font-weight:bold;"> {{list.name}}</div>
                                    <div style="width:22%;font-weight:bolder;font-size:15px !important;color:black !important;"  v-if="arabic=='true'">:اسم</div>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!--INFORMATION-->
            <div style="height:270mm;border:2px solid !important;">
                <div class="row ">
                    <div class="col-md-12 ">
                        <table class="table">
                            <tr class="heading" style="font-size:17px !important;padding-top:5px;" v-if="invoicePrint == 'العربية'">
                                <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important" class="text-center">#</th>
                                <th style="width:50%;" v-if="!IsExpenseAccount">وصف المصاريف</th>
                                <th style="width:20%;" v-else>وصف المصاريف</th>
                                <th style="width:15%;" v-if="IsExpenseAccount">حساب المصاريف</th>
                                <th style="width:30%;" class="text-center" v-if="!IsExpenseAccount">مقدار</th>
                                <th style="width:10%;" class="text-center" v-if="IsExpenseAccount">مقدار</th>
                                <th style="width:15%;" class="text-center" v-if="IsExpenseAccount">الطريقة الضريبية</th>
                                <th style="width:15%;" class="text-center" v-if="IsExpenseAccount">%الضريية</th>

                                <th style="width:10%;" class="text-center" v-if="IsExpenseAccount">قيمة الضريبة</th>
                                <th style="width:10%;" class="text-center" v-if="IsExpenseAccount">مجموع</th>
                            </tr>
                           
                            <tr class="heading" style="font-size:17px !important;padding-top:5px;" v-else>
                                <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important" class="text-center">#</th>
                                <th style="width:50%;" v-if="!IsExpenseAccount" class="text-center">Expense Description</th>
                                <th style="width:20%;" v-else>Expense Description</th>
                                <th style="width:15%;" v-if="IsExpenseAccount">Expense Account</th>
                                <th style="width:30%;" class="text-center" v-if="!IsExpenseAccount">Amount</th>
                                <th style="width:10%;" class="text-center" v-if="IsExpenseAccount">Amount</th>
                                <th style="width:15%;" class="text-center" v-if="IsExpenseAccount">Tax Method</th>
                                <th style="width:15%;" class="text-center" v-if="IsExpenseAccount">Tax</th>

                                <th style="width:10%;" class="text-center" v-if="IsExpenseAccount">Total Vat</th>
                                <th style="width:10%;" class="text-center" v-if="IsExpenseAccount">Total</th>

                            </tr>

                            <tr style="font-size:16px;font-weight:bold;" v-for="(item, index) in list.dailyExpenseDetails" v-bind:key="item.id">
                                <td class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                <td class="text-left" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{item.description}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="IsExpenseAccount">{{ ($i18n.locale == 'en' ||isLeftToRight()) ? (item.accountName != '' && item.accountName != null) ? item.accountName : item.nameArabic : (item.nameArabic != '' && item.nameArabic != null) ? item.nameArabic : item.accountName}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" >{{item.amount}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="IsExpenseAccount">{{item.taxMethod}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="IsExpenseAccount">{{item.taxName}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="IsExpenseAccount">{{(item.exclusiveTotalVat+item.inclusiveTotalVat).toFixed(3).slice(0,-1)}}</td>
                                <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;" v-if="IsExpenseAccount">{{(item.exclusiveTotalVat+item.amount).toFixed(3).slice(0,-1)}}</td>
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
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;"></td>
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'">المبلغ الإجمالي : {{currency}} {{$roundOffFilter(totalAmount)  }} </td>
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Amount : {{currency}} {{$roundOffFilter(totalAmount) }} </td>

                            </tr>
                            <tr v-if="IsExpenseAccount">
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;"></td>
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'"> قيمة الضريبة : {{currency}} {{$roundOffFilter(totalVat)  }} </td>
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Total Tax : {{currency}} {{$roundOffFilter(totalVat) }} </td>

                            </tr>
                            <tr v-if="IsExpenseAccount">
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold; width:25%;border:2px solid black !important;color:black !important;"></td>
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important; text-align:right;" v-if="invoicePrint == 'العربية'"> المجموع الإجمالي : {{currency}} {{$roundOffFilter(grandTotal)  }} </td>
                                <td style="padding: 3px 7px;font-size:16px;font-weight:bold;width:25%;border:2px solid black !important;color:black !important;" v-else>Grand Total : {{currency}} {{$roundOffFilter(grandTotal)  }} </td>

                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-md-12  " style="height:20mm;" v-if="isHeaderFooter!='true'">
                <div class="row">
                    <table class="table text-center">
                        <tr>

                            <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                {{ $t('DailyExpenseA4Report.PreparedBy') }}::{{userName}}
                            </td>
                            <td style="color: black !important;font-size:17px; font-weight:bold;border-top:0 !important;">
                                {{ $t('DailyExpenseA4Report.ApprovedBy') }}
                            </td>
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
        props: ['printDetails', 'headerFooter', 'isTouchScreen','formName'],
        data: function () {
            return {
                qrValue: "",
                IsExpenseAccount: false,
                isHeaderFooter: '',
                currency: '',
                invoicePrint: '',
                IsDeliveryNote: '',
                arabic: '',
                english: '',
                userName: '',

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
        computed: {
            totalAmount: function () {
                var total = 0;
                if (this.list.dailyExpenseDetails !== null && this.list.dailyExpenseDetails.length > 0) {
                    this.list.dailyExpenseDetails.forEach(function (dailyExpenses) {

                        total = parseFloat(total) + parseFloat(dailyExpenses.amount);

                    })
                }

                return total;
            },
            totalVat: function () {
                return this.list.dailyExpenseDetails.reduce(function (a, c) { return a + Number((c.inclusiveTotalVat + c.exclusiveTotalVat) || 0) }, 0)
            },
            grandTotal: function () {
                return this.list.dailyExpenseDetails.reduce(function (a, c) { return a + Number((c.amount + c.exclusiveTotalVat) || 0) }, 0)
            },
            //TotalVat: function () {
            //    return this.list.saleItems.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0)
            //},
           
            
        },
        methods: {
          
            paymentMode: function (x) {
                if (this.invoicePrint == 'العربية') {

                    if (x == 0) {
                        return 'السيولة النقدية';
                    }
                    if (x == 1) {
                        return 'مصرف';
                    }
                    if (x == 5) {
                        return 'يتقدم';
                    }



                }
                else {

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

            printInvoice: function () {
                
                this.$htmlToPaper('purchaseInvoice', options, () => {

                });
            },
            printBlindInvoice: function () {
                this.htmlData.htmlString = this.$refs.mychildcomponent.innerHTML;
                var printerName = localStorage.getItem('PrinterName')
                var form = new FormData();
                form.append('htmlString', this.htmlData.htmlString);
                form.append('NoOfPagesPrint', 0);
                form.append('PrintType', 'invoice');
                form.append('PrinterName', printerName);



                if (!this.$ServerIp.includes('localhost')) {
                    axios.post('http://127.0.0.1:7014/print/from-pdf', form).then(function () {

                    });
                }
                else {
                    this.$htmlToPaper('purchaseInvoice', options, () => {
                    });

                }
            },

        },
        created: function () {
            if (this.printDetails.dailyExpenseDetails.length > 0) {
                this.list = this.printDetails;
                this.headerFooters = this.headerFooter;

            }
        },
        mounted: function () {
            
            if (this.formName == 'dailyexpense') {

                this.IsExpenseAccount = false;
            }
            else {
                this.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;
            }

            this.currency = localStorage.getItem('currency');
            this.invoicePrint = localStorage.getItem('InvoicePrint');
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isHeaderFooter = localStorage.getItem('IsHeaderFooter');
            this.userName = localStorage.getItem('FullName');
            var root = this;
            if (this.printDetails.dailyExpenseDetails.length > 0) {
                this.list.date = moment().format('DD MMM YYYY');
                setTimeout(function () {
                    root.printInvoice();
                }, 125)

            }
        },

    }
</script>