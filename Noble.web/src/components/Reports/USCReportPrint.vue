<template>

    <div hidden id='purchaseInvoice'>
        <!--HEADER-->
        <div class="row ml-1 mr-1" style="height:52mm;border:2px solid #000000;background-color: #eeeeee !important;">
            <div class="col-md-5">
                <h3>{{headerFooters.company.nameArabic}} </h3>
                <p class="mb-0 font-weight-bold">{{headerFooters.company.nameArabic}}</p>
                <p class="mb-0 font-weight-bold">{{headerFooters.company.categoryArabic}}</p>
                <p class="mb-0 font-weight-bold">هاتف: {{headerFooters.company.phoneNo}}</p>
                <p class="mb-0 font-weight-bold">سجل تجاري: {{headerFooters.company.companyRegNo}}</p>
                <p class="mb-0 font-weight-bold">الرقم الضريبي:  {{headerFooters.company.vatRegistrationNo}}</p>
            </div>

            <div class="col-md-2">
                <h1 class="text-center mt-3" style="font-size: 70px;"><img :src="headerFooters.company.logoPath" style="width:auto;max-width:300px; max-height:100px; padding:5px !important; margin:0 !important"></h1>
                <h5 class="text-center mt-4">Invoice/فاتورة</h5>
            </div>

            <div class="col-md-5">
                <h3>{{headerFooters.company.nameEnglish}}</h3>
                <p class="mb-0 font-weight-bold">{{headerFooters.company.nameEnglish}}</p>
                <p class="mb-0 font-weight-bold">{{headerFooters.company.categoryEnglish}}</p>
                <p class="mb-0 font-weight-bold">Tel: {{headerFooters.company.phoneNo}}</p>
                <p class="mb-0 font-weight-bold">CR : {{headerFooters.company.companyRegNo}}</p>
                <p class="mb-0 font-weight-bold">VAT NO: {{headerFooters.company.vatRegistrationNo}}</p>
            </div>
        </div>

        <!--HEADER Invoice-->
        <div class="row">
            <div class="col-md-12">
                <div style="margin-left: 4px;margin-right: 4px;">
                    <table class="table table-borderless mb-0" style="background-color: #eeeeee !important;">
                        <tr>
                            <th class="text-center" style="border: 1px solid #000000; width:30%;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">Client no/كود العميل</th>
                            <th class="text-center" style="border: 1px solid #000000; width:30%;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">Invoice no/رقم الفاتوره</th>
                            <th class="text-center" style="border: 1px solid #000000; width:20%;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">Invoice Date/تاريخ الفاتوره</th>
                            <th class="text-center" style="border: 1px solid #000000; width:20%;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">Due date/تاريخ الإستحقاق</th>
                        </tr>
                        <tr>
                            <th class="text-center" style="border: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;"><span v-if="list.cashCustomer != null">{{list.code}}</span> <span v-else>{{list.customerCode}}</span></th>
                            <th class="text-center" style="border: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">{{list.registrationNo}}</th>
                            <th class="text-center" style="border: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">{{list.date}}</th>
                            <th class="text-center" style="border: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;"></th>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <!--HEADER Invoice-->
        <div class="row ml-1 mr-1" style="height:52mm;border:2px solid #000000;">
            <div class="col-md-5" style="border-right:2px solid #000000;background-color: #eeeeee !important;">
                <table class="table table-borderless mt-2" style="background-color: #eeeeee !important;">
                    <tr>
                        <th class="p-0" style="background-color: #eeeeee !important;">Invoice to/فاتوره إلى </th>
                        <th class="p-0" style="background-color: #eeeeee !important;"></th>
                    </tr>
                    <tr>
                        <th class="p-0" colspan="2" style="background-color: #eeeeee !important;" v-if="list.cashCustomer != null">{{list.cashCustomer}}</th>
                        <th class="p-0" colspan="2" style="background-color: #eeeeee !important;" v-else-if="headerFooters.customerNameAr && headerFooters.customerNameEn">{{list.customerNameEn=='' || list.customerNameEn==null? list.customerNameAr : list.customerNameEn}}</th>
                        <th class="p-0" colspan="2" style="background-color: #eeeeee !important;" v-else-if="headerFooters.customerNameAr">{{list.customerNameAr=='' || list.customerNameAr==null? list.customerNameEn : list.customerNameAr}}</th>
                        <th class="p-0" colspan="2" style="background-color: #eeeeee !important;" v-else>{{list.customerNameEn=='' || list.customerNameEn==null? list.customerNameAr : list.customerNameEn}}</th>
                    </tr>
                    <tr>
                        <th class="p-0" colspan="2" style="background-color: #eeeeee !important;" v-if="headerFooters.customerAddress && list.cashCustomer != null ">Address: {{list.customerAddressWalkIn}} </th>
                        <th class="p-0" colspan="2" style="background-color: #eeeeee !important;" v-if="headerFooters.customerAddress && list.creditCustomer != null ">Address: {{list.customerAddressWalkIn}} </th>
                    </tr>
                    <!--<tr>
                        <th class="p-0" style="background-color: #eeeeee !important;">المنطقة الصناعية - المرحلة الرابعة </th>
                        <th class="p-0" style="background-color: #eeeeee !important;"></th>
                    </tr>-->
                    <tr>
                        <th class="p-0" colspan="2" style="background-color: #eeeeee !important;" v-if="list.cashCustomer != null && headerFooters.customerTelephone">Tel: {{list.mobile}}</th>
                        <th class="p-0" colspan="2" style="background-color: #eeeeee !important;" v-if="list.creditCustomer != null && headerFooters.customerTelephone">Tel: {{list.customerTelephone}}</th>
                    </tr>
                    <tr>
                        <th class="p-0" style="background-color: #eeeeee !important;">VAT:رقم ضريبة القيمة المضافة</th>
                        <th class="p-0" style="background-color: #eeeeee !important;"><span v-if="list.cashCustomer != null">{{list.cashCustomerId}}</span> <span v-else>{{list.customerVat}}</span></th>
                    </tr>
                </table>
            </div>
            <div class="col-md-7"></div>
        </div>

        <!--HEADER Invoice-->
        <div class="row">
            <div class="col-md-12">
                <div style="margin-left: 4px;margin-right: 4px;">
                    <table class="table table-borderless mb-0">
                        <tr style="border-bottom:1px solid #000000;border-top:1px solid #000000;">
                            <th class="text-center" style="border-left: 1px solid #000000; width:24%;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">Type of Service/خدمة الشحن</th>
                            <th class="text-center" style=" width:17%;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">Carrier /شركة الملاحة</th>
                            <th class="text-center" style=" width:18%;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">Port of /ميناء </th>
                            <th class="text-center" style=" width:25%;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">Port of Destination/ميناء الوصول</th>
                            <th class="text-center" style="border-right: 1px solid #000000; width:16%;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">B/L Number/رقم</th>
                        </tr>
                        <tr style="border-bottom:1px solid #000000;">
                            <th class="text-center" style="border-left: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">Export = EXW</th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">PIL</th>
                            <th class="text-center" style="padding-top: 5px; padding-bottom: 5px;background-color: #eeeeee !important;"></th>
                            <th class="text-center" style="padding-top: 5px; padding-bottom: 5px;background-color: #eeeeee !important;">Algeria--Skikda</th>
                            <th class="text-center" style="border-right: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;"></th>
                        </tr>

                        <tr style="border-bottom:1px solid #000000;">
                            <th class="text-center" style="border-left: 1px solid #000000; width:24%;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;">Customs Reference/رقم البيان</th>
                            <th class="text-center" style="padding-top: 5px; padding-bottom: 5px; width: 17%;background-color: #eeeeee !important;" colspan="2">Quantity Container</th>
                            <th class="text-center" style="border-right: 1px solid #000000; width:16%;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;" colspan="2">Commodity/نوع البضاعه</th>
                        </tr>
                        <tr style="border-bottom:1px solid #000000;">
                            <th class="text-center" style="border-left: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;"></th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;" colspan="2">- 20 FT &nbsp;&nbsp;&nbsp;&nbsp; 1 40 HC</th>
                            <th class="text-center" style="border-right: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;" colspan="2"></th>
                        </tr>
                        <tr style="border-bottom:1px solid #000000;border-top:1px solid #000000;">
                            <th colspan="5" class="text-center" style="border-left: 1px solid #000000;border-right: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;font-size:20px;background-color: #eeeeee !important;">Invoice Detail/تفاصيل الفاتوره </th>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <!--HEADER Invoice-->
        <div class="row">
            <div class="col-md-12">
                <div style="margin-left: 4px;margin-right: 4px;">
                    <table class="table table-borderless mb-0">
                        <tr style="border-top:1px solid #000000;">
                            <th class="text-center" style="border-left: 1px solid #000000; width:20%;padding-top: 5px;padding-bottom: 5px;">Charge</th>
                            <th class="text-center" style=" width:7%;padding-top: 5px;padding-bottom: 5px;">Quantity</th>
                            <th class="text-center" style=" width:10%;padding-top: 5px;padding-bottom: 5px;">Rate</th>
                            <th class="text-center" style=" width:7%;padding-top: 5px;padding-bottom: 5px;">Currency</th>
                            <th class="text-center" style=" width:13%;padding-top: 5px;padding-bottom: 5px;">Total curr.</th>
                            <th class="text-center" style=" width:14%;padding-top: 5px;padding-bottom: 5px;">Currency ExchangeRate</th>
                            <th class="text-center" style=" width:14%;padding-top: 5px;padding-bottom: 5px;">Total SAR</th>
                            <th class="text-center" style="border-right: 1px solid #000000; width:15%;padding-top: 5px;padding-bottom: 5px;">VAT SAR</th>
                        </tr>
                        <tr style="border-bottom:1px solid #000000;">
                            <th class="text-center" style="border-left: 1px solid #000000; width:20%;padding-top: 5px;padding-bottom: 5px;">وصف الرسوم</th>
                            <th class="text-center" style=" width:7%;padding-top: 5px;padding-bottom: 5px;">الكمية</th>
                            <th class="text-center" style=" width:10%;padding-top: 5px;padding-bottom: 5px;">سعر الوحده</th>
                            <th class="text-center" style=" width:7%;padding-top: 5px;padding-bottom: 5px;">العملة</th>
                            <th class="text-center" style=" width:13%;padding-top: 5px;padding-bottom: 5px;">مجموع الوحدات</th>
                            <th class="text-center" style=" width:14%;padding-top: 5px;padding-bottom: 5px;">معادل تحويل العمله</th>
                            <th class="text-center" style=" width:14%;padding-top: 5px;padding-bottom: 5px;">المجموع بالريال</th>
                            <th class="text-center" style="border-right: 1px solid #000000; width:15%;padding-top: 5px;padding-bottom: 5px;">ضريبة قيمة مضافهه15%</th>
                        </tr>

                        <tr v-for="(item) in list.saleItems" v-bind:key="item.id">
                            <th class="text-center" style="border-left: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;"><span v-if="headerFooters.englishName">{{item.productName}}</span><span v-else>{{item.productName}}</span> </th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;">{{item.quantity }}</th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;"> <span class="float-left">$</span> <span class="float-right">{{item.unitPrice.toFixed(3).slice(0,-1)}}</span></th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;">USD</th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;">$ {{item.total.toFixed(3).slice(0,-1)}}</th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;">3.7600</th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;"> <span class="float-left">{{currency}}</span> <span class="float-right">{{item.total.toFixed(3).slice(0,-1)}}</span></th>
                            <th class="text-center" style="border-right: 1px solid #000000; padding-top: 5px;padding-bottom: 5px;"> <span class="float-left">{{currency}}</span> <span class="float-right">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</span></th>
                        </tr>


                        <tr>
                            <th class="text-center" style="border-left: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;"></th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;"></th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;"> </th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;"></th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;"></th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;"></th>
                            <th class="text-center" style="padding-top: 5px;padding-bottom: 5px;"> </th>
                            <th class="text-center" style="border-right: 1px solid #000000; padding-top: 5px;padding-bottom: 5px;"> <span class="float-left">SAR</span> <span class="float-right">-</span></th>
                        </tr>

                        <tr style="border-top:1px solid #000000;">
                            <th style="border-left: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;" colspan="7">VAT Aplicable amount (SAR)/المبلغ الخاضع للطريبه</th>
                            <th class="text-center" style="border-right: 1px solid #000000; padding-top: 5px;padding-bottom: 5px;"> <span class="float-left">{{currency}}</span> <span class="float-right">{{parseFloat(listItemP1Summary.calulateTotalVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></th>
                        </tr>
                        <tr style="border-top:1px solid #000000;">
                            <th style="border-left: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;" colspan="6">Total (SAR)/المجموع بالريال</th>
                            <th class="text-center" style="border-right: 1px solid #000000; padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;" colspan="2"> <span class="float-left">{{currency}}</span> <span class="float-right">{{parseFloat(listItemP1Summary.calulateNetTotal - (listItemP1Summary.calulateDiscountAmount + listItemP1Summary.calulateBundleAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></th>
                        </tr>
                        <tr style="border-top:1px solid #000000;">
                            <th style="border-left: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;" colspan="2">Total In Words/المجموع بالريال</th>
                            <th class="text-left" style="border-right: 1px solid #000000; padding-top: 5px;padding-bottom: 5px;background-color: #eeeeee !important;" colspan="6"> {{$toWords((listItemP1Summary.calulateNetTotal - (listItemP1Summary.calulateDiscountAmount + listItemP1Summary.calulateBundleAmount)))   }}</th>
                        </tr>

                        <tr style="border-top:1px solid #000000;">
                            <th style="border-left: 1px solid #000000;border-right: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;" colspan="8">
                                
                                <span class="text-center mb-0" v-html="headerFooters.footerEn">
                                    
                                </span>
                                <span class="text-center" v-html="headerFooters.footerAr">
                                    
                                </span>
                            </th>
                        </tr>
                        <tr style="border-top:1px solid #000000;border-bottom:1px solid #000000;">
                            <th style="border-left: 1px solid #000000;border-right: 1px solid #000000;padding-top: 5px;padding-bottom: 5px;" colspan="8">
                                <h4>Bank Account Details/تفاصيل الحساب البنكي:</h4>
                                <h4>united seas company LLC/تفاصيل الحساب البنكي:</h4>
                                <h4>Alinma Bank/مصرف الإنماء:</h4>
                                <h4>Account Number 68203173884000</h4>
                                <h4>IBAN Number SA 6205 0000 68203173884000</h4>
                                <h4>Jeddah, KSA0</h4>
                            </th>
                        </tr>
                    </table>
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
            'https://unpkg.com/kidlat-css/css/kidlat.css',

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
                render: 0,
                headerFooters: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                listItemP1Summary: {
                    calulateTotalQty: 0,
                    calulateNetTotal: 0,
                    calulateTotalExclVAT: 0,
                    calulateTotalVAT: 0,
                    calulateTotalInclusiveVAT: 0,
                    calulateDiscountAmount: 0,
                    calulateBundleAmount: 0,
                },
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
                currency: '',
            }
        },
        filters: {
            toWords: function (value) {
                var converter = require('number-to-words');
                if (!value) return ''
                return converter.toWords(value);
            }
        },
        methods: {

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
            this.currency = localStorage.getItem('currency');

            var root = this;
            this.headerFooters = this.headerFooter;
            this.list = this.printDetails;
            this.list.date = moment(this.list.date).format('DD MMM YYYY');

            /*summary calculate listItemP1Summary*/
            root.listItemP1Summary.calulateTotalQty = root.list.saleItems.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0);

            root.listItemP1Summary.calulateNetTotal = root.list.saleItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0);

            root.listItemP1Summary.calulateTotalExclVAT = root.list.saleItems.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0);

            root.listItemP1Summary.calulateTotalVAT = root.list.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0);

            root.listItemP1Summary.calulateTotalInclusiveVAT = root.list.saleItems.reduce(function (a, c) { return a + Number((c.inclusiveVat) || 0) }, 0);

            root.listItemP1Summary.calulateDiscountAmount = root.list.saleItems.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0);

            root.listItemP1Summary.calulateBundleAmount = root.list.saleItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0);

            
            
            setTimeout(function () {
                root.printInvoice();
            }, 125)
        },

    }
</script>

