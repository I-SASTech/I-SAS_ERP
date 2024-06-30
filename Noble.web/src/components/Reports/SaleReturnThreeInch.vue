<template>
    <div hidden>
        <div ref="mychildcomponent" id='purchaseInvoice' style="width:400px;z-index:-9999">
            <!--INFORMATION-->
            <div style="width:400px;margin-left:20px;">
                <!--<div style="width:100%;text-align:center;margin-bottom:2.5rem;margin-top:1rem;">
                    <img :src="headerFooters.company.logoPath" style="width: auto; max-width: 300px; height: auto; max-height: 120px;">
                </div>-->
                <!--<div style="width:100%;text-align:center;">
                    <vue-qrcode v-bind:value="qrValue" style="width:200px" v-bind:errorCorrectionLevel="correctionLevel" />
                </div>-->
                <div style="width:100%;">
                    <div style="text-align: center;">
                        <span style="font-size:30px;font-weight:bold;color:black;">{{headerFooters.company.nameEnglish}} {{headerFooters.company.nameArabic}}</span><br />
                        <span style="font-size:18px;font-weight:bold;color:black;">{{headerFooters.company.categoryEnglish}} {{headerFooters.company.categoryArabic}}</span><br />
                        <p style="border:1px solid; width:350px;font-weight:bold;color:black;margin-left:auto;margin-right:auto;margin-top:1.3rem;margin-bottom:1.3rem;">
                            VAT No. {{headerFooters.company.vatRegistrationNo}} الرقم الضريبي <br />
                            <span style="font-size:18px;color:black;">{{headerFooters.company.companyNameArabic}}</span><br />
                            <span style="font-size:17px;color:black;">{{headerFooters.company.companyNameEnglish}}</span>
                        </p>
                        <span style="font-size:18px;color:black;">{{headerFooters.company.addressArabic}}</span><br />
                        <span style="font-size:16px;color:black;">{{headerFooters.company.addressEnglish}}</span><br />
                        <span style="font-size:15px;color:black;">Mobile No. {{headerFooters.company.phoneNo}} رقم الجوال</span><br />
                    </div>
                </div>


                <div style="width:100%;text-align:center;margin-top:2rem;">
                    <p style="font-size: 15px; font-weight: bold;color:black;margin-bottom:2px;padding-bottom:2px;">VAT Invoice No. <span style="border:4px solid;font-size:25px;font-weight:bold;padding-left:2px;padding-right:2px;color:black;"> {{list.registrationNo}}</span> فاتوره ضريبيه مبسطه</p>
                    <barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>
                    <p style="font-size:15px;font-weight:bold;color:black;">Date: <span style="margin-right:20px;margin-left:10px;padding-left:20px;color:black;">{{list.date}}</span>  التاريخ</p>

                    <table class="table table-borderless " style="width:400px;">
                        <tr style="font-size:16px;">
                            <td class="" style="border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">Counter#</span> رقم الكاونتر  <br>{{counterCode}}</td>
                            <td style="text-align:right; border-top: 0;padding-left:1px;padding-right:1px;color:black;"><span style="font-weight:bold;color:black;">User</span> أسم المستخدم <br>{{employeeName}}</td>
                        </tr>
                    </table>
                </div>
                <div style="width:100%;">
                    <table class="table report-tbl" style="width:400px;">
                        <tr class="heading" style="font-size:15px;border-bottom:1px solid black;border-top:1px solid; black">
                            <th style="text-align: left; width: 5%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">رقم <br />No.</th>
                            <th style="text-align: center; width: 50%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">الصنف<br />Item</th>
                            <th style="text-align: center; width: 10%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">الكمية<br />Qty</th>
                            <th style="text-align: right; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">السعر<br />Price</th>
                            <th style="text-align: right; width: 20%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;">مجموع<br />Total</th>
                        </tr>
                        <span v-for="(item, index) in list.saleItems" v-bind:key="item.id">
                        <tr style="font-size:15px;">
                            <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{index+1}}</td>
                            <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black; font-weight: 600;">  {{item.arabicName}} <br>{{item.productName }}</td>
                            <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{item.quantity }}</td>
                            <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                            <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">-{{(item.total).toFixed(3).slice(0,-1)}}</td>
                        </tr>
                        </span>
                        <tr class="heading" style="font-size:16px;">
                            <th style="text-align:center; padding-left:1px;padding-right:1px;color:black;border-bottom: 1px solid #000000;border-top: 1px solid #000000;" colspan="2">T.Items عدد العنف <br /> {{list.saleItems.filter(prod => prod.quantity>0).length}}</th>
                            <th style="text-align: center; padding-left: 1px; padding-right: 1px; color: black;border-bottom: 1px solid #000000;border-top: 1px solid #000000;" colspan="2">T.Pieces عدد القطع <br /> {{calulateTotalQty}}</th>
                            <th style="text-align: center; padding-left: 1px; padding-right: 1px; color: black;border-bottom: 1px solid #000000;border-top: 1px solid #000000;">G.Total الإجمالي <br /> -{{calulateTotalExclVAT.toFixed(3).slice(0,-1)}}</th>
                        </tr>
                        <tr style="font-size:16px;">
                            <td colspan="3" style="text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; color: black;">Total wihout VAT الإجمالي قبل ضريبة:</td>
                            <td colspan="2" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; color: black;">-{{(calulateTotalExclVAT - calulateTotalInclusiveVAT).toFixed(3).slice(0,-1)}}</td>
                        </tr>
                        <tr style="font-size:16px;">
                            <td colspan="3" style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black;">Discount قيمة الخصم:</td>
                            <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black; ">-{{ calulateDiscountAmount.toFixed(3).slice(0,-1) }}</td>
                        </tr>
                        <tr style="font-size:16px;">
                            <td style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black;" colspan="3">Total after Discount الإجمالي بعد الخصم:</td>
                            <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; border-bottom: 0; padding-bottom: 1px; color: black; ">-{{ ((calulateTotalExclVAT - calulateTotalInclusiveVAT) - calulateDiscountAmount).toFixed(3).slice(0,-1) }}</td>
                        </tr>

                        <tr style="font-size:16px;">
                            <td style="text-align: right; padding-left: 1px; padding-right: 0px; border-top: 0; padding-top: 0; padding-bottom: 3px; color: black;" colspan="3">VAT 15% الضريبة:</td>
                            <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; border-top: 0; padding-top: 0; padding-bottom: 3px; color: black; ">-{{ calulateTotalVAT.toFixed(3).slice(0,-1) }}</td>
                        </tr>
                        <tr style="font-size:16px;font-weight:bold;">
                            <td style="text-align: right; padding-left: 1px; padding-right: 0px; padding-top: 5px; color: black;border-top: 0;" colspan="3">Payable Amount الإجمالي المستحق:</td>
                            <td colspan="2" style="padding-right: 3rem; text-align: right; padding-left: 10px; padding-right: 1px; padding-top: 5px; color: black;border-top: 0; ">-{{ (calulateNetTotal - calulateDiscountAmount).toFixed(3).slice(0,-1) }}</td>
                        </tr>

                        <tr style="font-size:19px;font-weight:bold;">
                            <td style="text-align:center; color: black;border-top: 1px solid #000000;" colspan="5">Payment Mode طريقة الدفع</td>
                        </tr>
                        <tr style="font-size:17px;padding-bottom:10px;" v-for="payment in list.paymentTypes" v-bind:key="payment.id">
                            <td style="text-align: left; color: black;border-top: 0; padding-bottom:4px;padding-top:4px;" colspan="3">{{payment.name}} <span v-if="payment.paymentType==2">-(Rate={{payment.amountCurrency}}* Amount={{payment.rate}})</span>:</td>
                            <td style="text-align: right; color: black;border-top: 0;padding-bottom:4px;padding-top:4px;" colspan="2">-{{parseFloat(payment.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                        </tr>
                        <tr style="font-size:17px;">
                            <th style="text-align: left; border-top: 0; padding-top: 0; color: black;" colspan="3">Change Due:</th>
                            <th colspan="2" style="text-align: right; border-top: 0; padding-top: 0; color: black;">{{ list.change }}</th>
                        </tr>

                        <tr style="font-size:17px;">
                            <td style="text-align: center; text-transform: capitalize; color: black;border-top: 0;" colspan="5">{{ (calulateNetTotal - calulateDiscountAmount).toFixed(3).slice(0,-1) }}</td>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="width:400px;margin-left:20px;display:flex;">
                <div style="width:55%;" class="float-left">
                    <p style="font-size:16px;color:black;font-weight:bold;">Refund / Exchange Policy</p>
                    <p style="font-size:15px;color:black;">
                        {{headerFooters.footerEn}}
                    </p>
                </div>
                <div style="width:45%;" class="float-right">
                    <p style="font-size:17px;text-align:right;color:black;font-weight:bold;">سياسة الإسترجاع و الإستبدال</p>
                    <p style="font-size:17px;text-align:right;color:black;">{{headerFooters.footerAr}}</p>

                </div>
            </div>

            <div style="width:400px;display:block;margin-left:20px;margin-top:1.5rem">
                <p style="color:transparent;">hidden</p>
                <p style="text-align:center;color:black;"><span style="margin-right:5px;color:black;">*</span> <span style="margin-right:5px;color:black;">*</span> <span style="margin-right:10px;color:black;">*</span> <span style="font-size:25px; font-weight:bold;color:black;">شـكـرا لــزيــارتـكـــ</span>  <span style="margin-left: 10px;color:black;">*</span> <span style="margin-left: 5px;color:black;">*</span> <span style="margin-left:5px;color:black;">*</span></p>
                <h6 style="text-align:center;color:black;">Thankyou for your visit</h6>
            </div>
            <div style="width:400px;display:block;margin-left:20px;margin-top:1.5rem;text-align:center;">
                <vue-qrcode v-bind:value="qrValue" style="width:200px" v-bind:errorCorrectionLevel="correctionLevel" />
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios'
    import moment from "moment";
    
    import Vue3Barcode from 'vue3-barcode';
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
        windowTitle: window.document.title
    }
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['printDetail', 'headerFooter','isTouchScreen'],
        components: {
            
            'barcode': Vue3Barcode,
        },
        data: function () {
            return {
                counterCode: "",
                qrValue: "",
                correctionLevel: "H",
                printInterval: '',
                jsonData: '',
                list: {
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
                CompanyID: '',
                UserID: '',
                employeeId: '',
                employeeName: '',
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
                return this.list.saleItems.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0):0) }, 0)
            },
            calulateNetTotal: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0)
            },
            calulateTotalExclVAT: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0)
            },
            calulateTotalVAT: function () {
               return this.list.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0)
            },
            calulateTotalInclusiveVAT: function () {
                return  this.list.saleItems.reduce(function (a, c) { return a + Number((c.inclusiveVat) || 0) }, 0)
            },
            calulateDiscountAmount: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.discountAmount) || 0) }, 0)
            },
            calulateBundleAmount: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
            }
        },
        methods: {
            calulateNetTotalWithVAT: function () {
                var total = this.list.saleItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0);
               return (parseFloat(total).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            calulateTotalVATofInvoice: function () {
                var total= this.list.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0);
                return (parseFloat(total).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            printInvoice: function () {
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
                var isBlindPrint = localStorage.getItem("IsBlindPrint")
                
                if (isBlindPrint === 'true') {
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
            EmployeeData: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/EmployeeRegistration/EmployeeDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.employeeName = response.data.englishName;
                        }
                    },
                        function (error) {
                            console.log(error);
                        });
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

            var root = this;

            if (this.printDetail.saleItems.length > 0) {
                this.list = this.printDetail;
                this.headerFooters = this.headerFooter;
                this.list.date = moment().format('DD/MM/YYYY hh:mm:ss A');
                if (token == '') {

                    root.counterCode = localStorage.getItem('CounterCode');
                    root.CompanyID = localStorage.getItem('CompanyID');
                    root.UserID = localStorage.getItem('UserID');
                    root.employeeId = localStorage.getItem('EmployeeId');
                    root.EmployeeData(root.employeeId);
                    var sellerNameBuff = root.GetTLVForValue('1', this.headerFooters.company.nameEnglish)
                    var vatRegistrationNoBuff = root.GetTLVForValue('2', this.headerFooters.company.vatRegistrationNo)
                    var timeStampBuff = root.GetTLVForValue('3', this.list.date)
                    var totalWithVat = root.GetTLVForValue('4', this.calulateNetTotalWithVAT())
                    var totalVat = root.GetTLVForValue('5', this.calulateTotalVATofInvoice())
                    var tagArray = [sellerNameBuff, vatRegistrationNoBuff, timeStampBuff, totalWithVat, totalVat]
                    var qrCodeBuff = Buffer.concat(tagArray)
                    root.qrValue = qrCodeBuff.toString('base64')


                }
                setTimeout(function () {
                    root.printInvoice();

                }, 125)

            }

        },

    }
</script>

