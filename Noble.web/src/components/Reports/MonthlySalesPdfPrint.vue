<template>
    <div>
        <div ref="mychildcomponent" hidden id='purchaseInvoice' class="col-md-12">
            <!--page1-->
            <div>
                <!--HEADER-->
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
                                <p class="text-right mb-0" v-if="arabic=='true'">
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
                                    <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Sale'">{{ $t('MonthlySales.MonthlySalesReport') }}&nbsp;{{ month}}-{{year  }}</span>
                                    <span style="font-size:20px;color:black !important;font-weight:bold;padding-bottom:5px !important" v-if="formName=='Purchase'">{{ $t('MonthlySales.MonthlyPurchaseReport') }}&nbsp;{{ month}}-{{year  }}</span>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>                <!--<div style="height:60mm;" v-else></div>-->

               

                <!--INFORMATION-->
                <div >
                    <div class="row ">
                        <div class="col-md-12 mt-4 ">
                            <table class="table">
                                <tr class="heading" style="font-size:18px !important;padding-top:5px;border-bottom:1px solid" v-if="$i18n.locale != 'en'">
                                    <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;"> تاريخ</th>

                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Sale'">إجمالي المبيعات</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Purchase'">إجمالي المشتريات</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">خصم</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">يعود</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">المجموع</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">ضريبة</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">صافي البيع</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">نقدي</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">بنك</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">الإئتمان</th>
                                </tr>
                                <tr class="heading" style="font-size:18px;border-bottom:1px solid;padding-bottom:15px" v-else>
                                    <th style="width:2%;padding-top:3px !important; padding-bottom:3px !important;border-top:0px !important">#</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important;"> {{ $t('MonthlySales.Date') }}</th>

                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Sale'">Gross Sales</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Purchase'">  Gross Purchase</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">{{ $t('MonthlySales.Discount') }}</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">{{ $t('MonthlySales.Return') }}</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Purchase'">Net Purchase Amount</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;" v-if="formName=='Sale'">Net Sale Amount</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">  {{ $t('MonthlySales.VATAmount') }}</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;"> Total Amount</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">{{ $t('MonthlySales.Cash') }}</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">{{ $t('MonthlySales.Bank') }}</th>
                                    <th class="text-center" style="padding-top:3px !important; padding-bottom:3px !important;color:black !important;border-top:0px !important ;">{{ $t('MonthlySales.Credit') }}</th>
                                </tr>


                                <tr style="font-size:17px;font-weight:bold;" v-for="(invoices, index) in list" v-bind:key="invoices.id">
                                    <td class="text-center" style="padding-top:8px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;">{{index+1}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{invoices.date }}</td>

                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(invoices.grossAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(totalReturns)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(invoices.grossAmount+-invoices.totalReturns-invoices.discount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(invoices.totalTax)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat((invoices.grossAmount-invoices.totalReturns-invoices.discount)-invoices.totalTax)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(invoices.cash)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(invoices.bank)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 3px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(invoices.credit)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr> 
                                
                                <tr style="font-size:15px;font-weight:bold;">
                                    <td colspan="2" class="text-center" style="padding-top:60px !important; padding-bottom:3px !important;border-top:0 !important; border-bottom:0 !important;color:black !important;"> </td>


                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(totalAmount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(discount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(totalReturns))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(total))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(totalTax))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(NetAmount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(cashAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(bankAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                    <td class="text-center" style="border-top: 0 !important; border-bottom: 0 !important; padding-top: 60px !important; padding-bottom: 3px !important;color:black !important;">{{(parseFloat(Math.abs(creditAmount))).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                               

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

    
    export default {
        props: ['printDetails', 'headerFooter', 'formName', 'year', 'month','invoice'],
       
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
        filters: {
            toWords: function (value) {
                var converter = require('number-to-words');
                if (!value) return ''
                return converter.toWords(value);
            }
        },
        computed: {

            cashAmount: function () {
                return this.printDetails.reduce(function (a, c) {


                    return a + Number((c.cash.toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            bankAmount: function () {
                return this.printDetails.reduce(function (a, c) {


                    return a + Number((c.bank.toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            creditAmount: function () {
                return this.printDetails.reduce(function (a, c) {


                    return a + Number((c.credit.toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            totalAmount: function () {
                return this.printDetails.reduce(function (a, c) {


                    return a + Number(((c.grossAmount).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            discount: function () {
                return this.printDetails.reduce(function (a, c) {


                    return a + Number(((c.discount).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            totalReturns: function () {
                return this.printDetails.reduce(function (a, c) {


                    return a + Number(((c.totalReturns).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            total: function () {
                return this.printDetails.reduce(function (a, c) {


                    return a + Number(((c.grossAmount - c.totalReturns - c.discount).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            totalTax: function () {
                return this.printDetails.reduce(function (a, c) {


                    return a + Number(((c.totalTax).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },
            NetAmount: function () {
                return this.printDetails.reduce(function (a, c) {


                    return a + Number(((((c.grossAmount - c.totalReturns - c.discount) - c.totalTax)).toFixed(3).slice(0, -1)) || 0)


                }, 0)
            },


        },
     
        methods: {
           
            printInvoice: function () {
                
                var form = new FormData();
                form.append('htmlString', this.$refs.mychildcomponent.innerHTML);
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

                        if (root.formName == 'Sale') {
                            link.setAttribute('download', 'Monthly Sale Report ' + date + '.pdf');

                        }

                        if (root.formName == 'Purchase') {
                            link.setAttribute('download', 'Monthly Purchase Report ' + date + '.pdf');

                        }
                        document.body.appendChild(link);
                        link.click();

                    });
            }



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

