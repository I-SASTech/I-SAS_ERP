<template>
    <div hidden>
        <div ref="mychildcomponent" id='purchaseInvoice' style="width:400px;z-index:-9999">
            <!--INFORMATION-->
            <div style="width:400px;margin-left:20px;">
                <div style="width:100%;text-align:center;margin-bottom:2.5rem;">
                    <img :src="headerFooters.company.logoPath" style="width: auto; max-width: 300px; height: auto; max-height: 120px;">
                </div>
                <div style="text-align: center;margin-bottom:15px;">
                    <span style="font-size:20px;font-weight:bold;color:black;">SALES TAX INVOICE</span><br />

                </div>
                <div style="width:100%;">



                    <span style="font-size:18px;color:black !important;text-align:left;padding-right:5px;">Shop Name: </span> <span style="font-size:18px;color:black !important;text-align:left;">{{headerFooters.company.companyNameEnglish}}</span><br />
                    <span style="font-size:18px;color:black !important;text-align:left;padding-right:5px;">Plot # </span> <span style="font-size:18px;color:black !important;text-align:left;">{{headerFooters.company.addressEnglish}}</span><br />
                    <span style="font-size:18px;color:black !important;text-align:left;padding-right:5px;">Tel  #: </span> <span style="font-size:18px;color:black !important;text-align:left;">{{headerFooters.company.phoneNo}}</span><br />
                    <span style="font-size:18px;color:black !important;text-align:left ;padding-right:5px;">NTN  #: </span> <span style="font-size:18px;color:black !important;text-align:left;">{{headerFooters.company.vatRegistrationNo}}</span><br />
                    <span style="font-size:18px;color:black !important;text-align:left ;padding-right:5px;">STRN  #: </span> <span style="font-size:18px;color:black !important;text-align:left;">{{headerFooters.company.companyRegNo}}</span><br />

                </div>


                <div style="width:100%;margin-top:2rem;">
                    <span style="font-size:18px;color:black !important;text-align:left ;padding-right:5px;">Date & Time : </span> <span style="font-size:18px;color:black !important;text-align:left;">{{list.date}}</span><br />
                    <span style="font-size:18px;color:black !important;text-align:left ;padding-right:5px;">Receipt # : </span> <span style="font-size:18px;color:black !important;text-align:left;">{{list.registrationNo}}</span><br />
                    <span style="font-size:18px;color:black !important;text-align:left ;padding-right:5px;">Customer Name # : </span>
                    <span style="font-size:18px;color:black !important;text-align:left;" v-if="list.cashCustomer != null">{{list.cashCustomer}}</span>
                    <span style="font-size:18px;color:black !important;text-align:left;" v-else>{{list.customerNameEn}}</span>
                    <br />


                </div>
                <div style="width:100%;margin-top:20px">
                    <table class="table report-tbl" style="width:400px;">
                        <tr class="heading" style="font-size:19px;border:1px solid black !important; color:black !important;padding-top:0px !important;padding-bottom:0px !important">
                            <th style="text-align: left; width: 5%; border: 1px solid #000000;  color: black !important;padding-top:0px !important;padding-bottom:0px !important"> #</th>
                            <th style="text-align: left; width: 50%; border: 1px solid #000000;  color: black !important;padding-top:0px !important;padding-bottom:0px !important">Product</th>
                            <th style="text-align: center; width: 10%; border: 1px solid #000000;  color: black !important;padding-top:0px !important;padding-bottom:0px !important">Qty</th>
                            <th style="text-align: right; width: 15%; border: 1px solid #000000;  color: black !important;padding-top:0px !important;padding-bottom:0px !important">Price</th>
                            <th style="text-align: center; width: 15%; border: 1px solid #000000;  color: black !important;padding-top:0px !important;padding-bottom:0px !important">Disc%</th>
                            <th style="text-align: right; width: 20%; border: 1px solid #000000;  color: black !important;padding-top:0px !important;padding-bottom:0px !important">Total</th>
                        </tr>
                        <span v-for="(item, index) in list.saleItems" v-bind:key="item.id">
                            <tr style="font-size:15px;">
                                <td style="text-align: left; border-top: 0; padding-top: 0px; padding-left: 1px; padding-right: 1px; color: black !important;">{{index+1}}</td>
                                <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">{{item.productName }}</td>
                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">{{item.quantity }}</td>
                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">{{item.discount.toFixed(3).slice(0,-1)}}%</td>
                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black !important;">{{(item.total).toFixed(3).slice(0,-1)}}</td>
                            </tr>
                        </span>
                    </table>
                    <table class="table " style="width: 400px; color: black !important; border-color: black !important;margin-top:10px">
                        <tr style="font-size:15px;">
                            <td width="70%" style="text-align: left; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; border:1px solid;color:black !important; border-color:black !important; font-weight:bold">Total Quantity:</td>
                            <td width="30%" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; border:1px solid;color:black !important; border-color:black !important;font-weight:bold"> {{list.saleItems.filter(prod => prod.quantity>0).length}}</td>
                        </tr>
                        <tr style="font-size:15px;">
                            <td width="70%" style="text-align: left; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; border:1px solid;color:black !important; border-color:black !important;">Amount Before Discount</td>
                            <td width="30%" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; border:1px solid;color:black !important; border-color:black !important;"> {{(calulateTotalExclVAT - calulateTotalInclusiveVAT).toFixed(3).slice(0,-1)}}</td>
                        </tr>
                        <tr style="font-size:15px;">
                            <td width="70%" style="text-align: left; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; border:1px solid;color:black !important; border-color:black !important;">Total Discount</td>
                            <td width="30%" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; border:1px solid;color:black !important; border-color:black !important;"> {{ calulateDiscountAmount.toFixed(3).slice(0,-1) }}</td>
                        </tr>
                        <tr style="font-size:15px;">
                            <td width="70%" style="text-align: left; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; border:1px solid;color:black !important; border-color:black !important;">Total Amount Exl.Tax</td>
                            <td width="30%" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; border:1px solid;color:black !important; border-color:black !important;"> {{ ((calulateTotalExclVAT - calulateTotalInclusiveVAT) - calulateDiscountAmount).toFixed(3).slice(0,-1) }}</td>
                        </tr>
                        <tr style="font-size:15px;">
                            <td width="70%" style="text-align: left; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; border:1px solid;color:black !important; border-color:black !important;">Sale Tax 17.00%</td>
                            <td width="30%" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; border:1px solid;color:black !important; border-color:black !important;">{{ calulateTotalVAT.toFixed(3).slice(0,-1) }}</td>
                        </tr>
                        <tr style="font-size:15px;">
                            <td width="70%" style="text-align: left; border-bottom: 0; padding-bottom: 1px; padding-left: 1px; padding-right: 0px; border:1px solid;color:black !important; border-color:black !important;font-weight:bold">Total Amount</td>
                            <td width="30%" style="padding-right:3rem; text-align: right; border-bottom: 0; padding-bottom: 1px; padding-left: 10px; padding-right: 1px; border:1px solid;color:black !important; border-color:black !important;font-weight:bold"> {{parseFloat(calulateNetTotal - (calulateDiscountAmount + calulateBundleAmount)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                        </tr>
                    </table>

                </div>
            </div>
            <div style="width:400px;border-top:1px dotted black !important;padding-top:20px">
                <div style="width:100%;float:left;">
                    <p style="font-size:16px;color:black !important;font-weight:bold;">Return & Exchange Policy</p>
                    <p style="font-size:15px;color:black !important;">
                        1- No cash refund.<br />
                        2- For exchange, all items must be in original condition with tags and labels intact.<br />
                        3-Exchange of equal value be made within 15 days of purchase if sales receipt is provided.<br />
                        4- No exchange of discounted or factory outlet products.<br />
                        5- Groom's wear, customized products, fragrances,cosmetics and jewelry cannot be exchanged or returned.<br />
                        6- Articles worn/washed/shrunk will not be exchanged.<br />
                        7- During sale or promotion,items purchased at regular price will be exchanged at discounted Price.<br />



                    </p>
                    <p style="font-size:15px;color:black !important;padding-top:15px">  * Prices are inclusive of sales tax levied by Govt<br /></p>

                    <p style="font-size:15px;color:black !important;padding-top:15px;text-align:center">  Thank You for Shopping at {{headerFooters.company.companyNameEnglish}} </p>
                    <p style="font-size:15px;color:black !important;text-align:center">Email: {{headerFooters.company.companyEmail}}</p>
                    <p style="font-size:15px;color:black !important;text-align:center">UAN: {{headerFooters.company.phoneNo}}</p>
                    <p style="font-size:15px;color:black !important;text-align:center">Log on: {{headerFooters.company.website}}</p>
                </div>

            </div>

            <div style="width:400px;display:block;margin-left:20px;margin-top:1.5rem">
                <p style="color:transparent;">hidden</p>

            </div>
            <div style="width:400px;margin-top:1.5rem;text-align:center;">
                <barcode :width="1.9" :height="50" v-bind:value="list.barCode"></barcode>
            </div>
            <!--<div style="width:50%;display:block;margin-top:1.5rem;text-align:left;">
                <vue-qrcode v-bind:value="qrValue" style="width:100px" v-bind:errorCorrectionLevel="correctionLevel" />
            </div>-->
            <div style="display:flex;margin-top:1.5rem;width:400px;">
                <div style="text-align:left;width:40%;">
                    <vue-qrcode v-bind:value="qrValue" v-bind:errorCorrectionLevel="correctionLevel" style="width:150px;" />

                </div>
                <div style="text-align:center;width:58%;">
                    <img src="/FBR1.png" class="mx-auto d-block" style="width:180px;" />

                </div>
            </div>

        </div>
    </div>
</template>

<script>
    import axios from 'axios'
    import moment from "moment";
    
    import Vue3Barcode from 'vue3-barcode'
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
        props: ['printDetail', 'headerFooter', 'isTouchScreen', 'isDoublePrint', 'saleId', 'saleReturnRegNo'],
        components: {
            
            'barcode': Vue3Barcode,
        },
        data: function () {
            return {
                totalReturnAmount: 0,
                totalInvoiceAmount: 0,
                totalNewInvoiceAmount: 0,
                totalReturnInvoiceAmount: 0,
                totalPaidAmount: 0,
                isCalculationShow: false,
                counterCode: "",
                qrValue: "",
                qrValueReturn: "",
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
                returnList: {
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
                    paymentTypes: [],
                    saleItems:
                        [

                        ],
                    returnList: ''
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
                },
                htmlDataReturn: {
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
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0)
            },
            calulateTotalExclVAT: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0)
            },
            calulateTotalVAT: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0)
            },
            calulateTotalInclusiveVAT: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.inclusiveVat) || 0) }, 0)
            },
            calulateDiscountAmount: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.discountAmount) || 0) }, 0)
            },
            calulateBundleAmount: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
            },
            calulateTotalQtyR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0)
            },
            calulateNetTotalR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0)
            },
            calulateTotalExclVATR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0)
            },
            calulateTotalVATR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0)
            },
            calulateTotalInclusiveVATR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.inclusiveVat) || 0) }, 0)
            },
            calulateDiscountAmountR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.discountAmount) || 0) }, 0)
            },
            calulateBundleAmountR: function () {
                return this.returnList.saleItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
            }
        },
        methods: {
            calulateDiscountAmount1: function () {
                return this.list.saleItems.reduce(function (a, c) { return a + Number((c.discountAmount) || 0) }, 0)
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
            calulateNetTotalWithVATReturn: function () {
                var total = this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0);
                return (parseFloat(total).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            calulateTotalVATofInvoiceReturn: function () {
                var total = this.returnList.saleItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0);
                return (parseFloat(total).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
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
            printReturnInvoice: function () {
                var root = this;

                // this.$htmlToPaper('purchaseInvoice');
                this.htmlDataReturn.htmlString = this.$refs.myReturnComponent.innerHTML;
                //  var html1 = this.$refs.mychildcomponent.innerHTML;
                //  var data = { html: html1 }
                //
                var printerName = localStorage.getItem('PrinterName')
                var form = new FormData();
                form.append('htmlString', this.htmlDataReturn.htmlString);
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
                        if (root.isDoublePrint) {
                            root.printInvoice()


                        }
                    });
                    //if (root.isTouchScreen === true) {
                    //    root.$router.go('/TouchInvoice')
                    //}
                }
                else {
                    this.$htmlToPaper('saleReturn', options, () => {
                        if (root.isDoublePrint) {
                            root.printInvoice()


                        }
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
            saleReturnData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get("/Sale/SaleDetail?id=" + root.saleId, { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            
                            root.list = response.data;

                            root.list.date = moment().format('DD/MM/YYYY hh:mm:ss A');
                            var sellerNameBuff = root.GetTLVForValue('1', root.headerFooters.company.nameEnglish)
                            var vatRegistrationNoBuff = root.GetTLVForValue('2', root.headerFooters.company.vatRegistrationNo)
                            var timeStampBuff = root.GetTLVForValue('3', root.list.date)
                            var totalWithVat = root.GetTLVForValue('4', root.calulateNetTotalWithVATReturn())
                            var totalVat = root.GetTLVForValue('5', root.calulateTotalVATofInvoiceReturn())
                            var tagArray = [sellerNameBuff, vatRegistrationNoBuff, timeStampBuff, totalWithVat, totalVat]
                            var qrCodeBuff = Buffer.concat(tagArray)
                            root.qrValue = qrCodeBuff.toString('base64')
                            if (root.isDoublePrint) {
                                if (root.list.paymentTypes[root.list.paymentTypes.length - 1].name.includes("SIR")) {
                                    root.totalPaidAmount = root.list.paymentTypes[root.list.paymentTypes.length - 1].amount
                                    root.isCalculationShow = true
                                }
                            }
                            root.PrintData();
                        }
                    });
            },
            PrintData: function () {
                var root = this;
                if (!this.printDetail.registrationNo.includes("SIR")) {
                    if (this.printDetail.saleItems.length > 0) {
                        this.list = this.printDetail;
                        
                        if (root.list.paymentTypes.length > 0) {
                            
                            if (root.list.paymentTypes[root.list.paymentTypes.length - 1].name.includes("SIR")) {
                                root.totalPaidAmount = root.list.paymentTypes[root.list.paymentTypes.length - 1].amount
                                root.isCalculationShow = true
                            }
                        }
                        this.list.date = moment().format('DD/MM/YYYY hh:mm:ss A');
                        


                            root.EmployeeData(root.employeeId);
                            var sellerNameBuff = root.GetTLVForValue('1', this.headerFooters.company.nameEnglish)
                            var vatRegistrationNoBuff = root.GetTLVForValue('2', this.headerFooters.company.vatRegistrationNo)
                            var timeStampBuff = root.GetTLVForValue('3', this.list.date)
                            var totalWithVat = root.GetTLVForValue('4', this.calulateNetTotalWithVAT())
                            var totalVat = root.GetTLVForValue('5', this.calulateTotalVATofInvoice())
                            var tagArray = [sellerNameBuff, vatRegistrationNoBuff, timeStampBuff, totalWithVat, totalVat]
                            var qrCodeBuff = Buffer.concat(tagArray)
                            root.qrValue = qrCodeBuff.toString('base64')


                        
                        setTimeout(function () {
                            root.printInvoice();

                        }, 125)

                    }
                }
                else {
                    if (root.printDetail.saleItems.length > 0) {
                        root.returnList = root.printDetail;
                        root.returnList.date = moment().format('DD/MM/YYYY hh:mm:ss A');
                       
                            root.EmployeeData(root.employeeId);
                            var sellerNameBuffR = root.GetTLVForValue('1', root.headerFooters.company.nameEnglish)
                            var vatRegistrationNoBuffR = root.GetTLVForValue('2', root.headerFooters.company.vatRegistrationNo)
                            var timeStampBuffR = root.GetTLVForValue('3', root.returnList.date)
                            var totalWithVatR = root.GetTLVForValue('4', root.calulateNetTotalWithVATReturn())
                            var totalVatR = root.GetTLVForValue('5', root.calulateTotalVATofInvoiceReturn())
                            var tagArrayR = [sellerNameBuffR, vatRegistrationNoBuffR, timeStampBuffR, totalWithVatR, totalVatR]
                            var qrCodeBuffR = Buffer.concat(tagArrayR)
                            root.qrValueReturn = qrCodeBuffR.toString('base64')
                            setTimeout(function () {
                                root.printReturnInvoice();

                            }, 125)


                        
                    }
                }
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

            root.isCalculationShow = false
            root.counterCode = localStorage.getItem('CounterCode');
            root.CompanyID = localStorage.getItem('CompanyID');
            root.UserID = localStorage.getItem('UserID');
            root.employeeId = localStorage.getItem('EmployeeId');
            this.headerFooters = this.headerFooter;
            
            if (this.isDoublePrint) {
                root.saleReturnData();

            }
            else {
                root.PrintData()
            }





        },

    }
</script>

