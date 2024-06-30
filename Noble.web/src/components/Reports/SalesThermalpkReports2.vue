<template>
    <div hidden>
        <div ref="mychildcomponent" id='purchaseInvoice' style="width:400px;z-index:-9999">
            <!--INFORMATION-->
            <div style="width:400px;margin-left:20px;">
                <div style="width:100%;text-align:center;margin-bottom:2.5rem;margin-top:10px">
                    <img :src="headerFooters.company.logoPath" class="rounded-circle" style="width: auto; max-width: 300px; height: auto; max-height: 120px;">
                </div>
                <div style="text-align: center;margin-bottom:15px;">
                    <span style="font-size:20px;font-weight:bold;color:black;">{{headerFooters.company.companyNameEnglish}}</span><br />
                    <span style="font-size:20px;font-weight:bold;color:black;">{{headerFooters.company.addressEnglish}}</span><br />
                    <span style="font-size:20px;font-weight:bold;color:black;">{{headerFooters.company.phoneNo}}</span><br />

                </div>

                <div style="width:100%;">
                    <div style="text-align: center;">
                        <span style="font-size:18px;font-weight:bold;color:black;">NTN:{{headerFooters.company.vatRegistrationNo}}</span><br />
                        <span style="font-size:18px;font-weight:bold;color:black;">STRN:{{headerFooters.company.companyRegNo}}</span><br />
                        <span style="font-size:18px;font-weight:bold;color:black;">Invoice No#{{list.registrationNo}}</span><br />
                        <span style="font-size:18px;font-weight:bold;color:black;">{{list.date}}</span><br />

                    </div>
                    <div style="width:100%;margin-top:2rem;">
                        <span style="font-size:18px;color:black;text-align:left ;padding-right:5px;font-weight:bold;padding-top:15px">Cashier : </span> <span style="font-size:18px;color:black;text-align:left;">{{employeeName}}</span><br />
                        <span style="font-size:18px;color:black;text-align:left ;padding-right:5px;font-weight:bold;padding-top:15px">Payment Mode : </span> 
                        <span style="font-size:18px;color:black;text-align:left;" v-if="list.cashCustomer != null">Cash</span>
                        <span style="font-size:18px;color:black;text-align:left;" v-else>Credit Card</span>
                        <br />
                        <span style="font-size:18px;color:black;text-align:left ;padding-right:5px;font-weight:bold;padding-top:15px">Customer Name # : </span>
                        <span style="font-size:18px;color:black;text-align:left;" v-if="list.cashCustomer != null">{{list.cashCustomer}}</span>
                        <span style="font-size:18px;color:black;text-align:left;" v-else>{{list.customerNameEn}}</span>
                        <br />


                    </div>
                </div>

               
                <div style="width:100%;">
                    <table class="table report-tbl" style="width:400px;margin-top:15px">
                        <tr class="heading" style="font-size:15px;border-bottom:1px solid black;border-top:1px solid;color:black ;padding-top:15px">
                            <th style="text-align: left; width: 50%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;padding-top:0px !important;padding-bottom:0px !important">Description</th>
                            <th style="text-align: right; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;padding-top:0px !important;padding-bottom:0px !important ">Price</th>
                            <th style="text-align: right; width: 15%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;padding-top:0px !important;padding-bottom:0px !important">GstRate</th>
                            <th style="text-align: center; width: 10%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;padding-top:0px !important;padding-bottom:0px !important ">Qty</th>
                            <th style="text-align: center; width: 10%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;padding-top:0px !important;padding-bottom:0px !important">GST</th>
                            <th style="text-align: right; width: 20%; border-bottom: 1px solid #000000;border-top: 1px solid #000000; padding-left: 1px; padding-right: 1px; color: black;padding-top:0px !important;padding-bottom:0px !important">Total</th>
                        </tr>
                        <span v-for="item in list.saleItems" v-bind:key="item.id">
                            <tr style="font-size:15px;padding-top:20px">
                                <td style="text-align: left; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{item.productName }}</td>
                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{item.unitPrice.toFixed(3).slice(0,-1)}}</td>
                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{item.taxRate.toFixed(3).slice(0,-1) }}</td>

                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{item.quantity }}</td>
                                <td style="text-align: center; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{(item.includingVat+item.inclusiveVat).toFixed(3).slice(0,-1)}}</td>
                                <td style="text-align: right; border-top: 0; padding-top: 0; padding-left: 1px; padding-right: 1px; color: black;">{{(item.total).toFixed(3).slice(0,-1)}}</td>
                            </tr>
                        </span>
                      
                        <tr style="font-size:16px;border-top:1px solid #000000;border-color:black !important ">
                            <td colspan="4" style="text-align: right; border-bottom: 0; padding-bottom: 1px;   color: black;">Total  Amount:</td>
                            <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;">{{(calulateTotalExclVAT - calulateTotalInclusiveVAT).toFixed(3).slice(0,-1)}}</td>
                        </tr> <tr style="font-size:16px;">
                            <td colspan="4" style="text-align: right; border-bottom: 0; padding-bottom: 1px;   color: black;">Sale Tax:</td>
                            <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;">{{ calulateTotalVAT.toFixed(3).slice(0,-1) }}</td>
                        </tr> 
                        <tr style="font-size:16px;">
                            <td colspan="4" style="text-align: right; border-bottom: 0; padding-bottom: 1px;   color: black;">Discount:</td>
                            <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;">{{ calulateDiscountAmount.toFixed(3).slice(0,-1) }}</td>
                        </tr>
                        <tr style="font-size:16px;">
                            <td colspan="4" style="text-align: right; border-bottom: 0; padding-bottom: 1px;   color: black;">Payable:</td>
                            <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;">{{ (totalInvoiceAmount + totalPaidAmount).toFixed(3).slice(0,-1) }}</td>
                        </tr> 
                        <tr style="font-size:16px;">
                            <td colspan="4" style="text-align: right; border-bottom: 0; padding-bottom: 1px;   color: black;">Received:</td>
                            <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;">{{(calulateTotalExclVAT - calulateTotalInclusiveVAT).toFixed(3).slice(0,-1)}}</td>
                        </tr>
                        <tr style="font-size:16px;border-bottom:1px solid #000000;border-color:black !important ">
                            <td colspan="4" style="text-align: right; border-bottom: 0; padding-bottom: 1px;   color: black;">Customer Balance:</td>
                            <td colspan="2" style=" text-align: right; border-bottom: 0; padding-bottom: 1px;  color: black;">{{ list.change }}</td>
                        </tr>
                       
                     
                    </table>
                </div>
            </div>
            <div style="width:400px;border-top:1px solid #000000;border-color:black !important">

                <div style="text-align:center">
                    <p style="font-size:20px;color:black;font-weight:bold">FBR Invoice #</p>
                    <p style="font-size:20px;color:black;font-weight:bold">485903894580934858340</p>

                </div>
                <div style="margin-top:15px;display:flex">
                    <div style="width:150px;text-align:left;width:60%;">
                        <img src="/FBRPOS.png" class="mx-auto d-block" style="width:100px;text-align:right;width:50%;" />

                    </div>
                    <div style="width:150px;text-align:right;width:40%;">
                        <vue-qrcode v-bind:value="qrValue" style="width: 150px;" v-bind:errorCorrectionLevel="correctionLevel" />

                    </div>

                </div>
                <div style="font-size:20px;color:black;margin:5px" >
                    <p style="font-size:20px;color:black" >Verify this invoice through FBR TaxAsan Mobile App or Sms at 9966 and win exciting prize in draw.</p>

                </div>
            </div>

            
           

        </div>

    </div>
</template>

<script>
    import axios from 'axios'
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
        windowTitle: window.document.title
    }
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ['printDetail', 'headerFooter', 'isTouchScreen', 'isDoublePrint', 'saleId', 'saleReturnRegNo'],
        components: {
            
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

