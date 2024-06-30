<template>
    <div hidden>
        <div ref="mychildcomponent" id='purchaseInvoice' style="width:400px;z-index:-9999">
            <!--INFORMATION-->
            <div style="width:400px;margin-left:20px;">
                <div style="width:100%;text-align:center;margin-bottom:2.5rem;">
                    <img :src="headerFooters.company.logoPath" style="width: auto; max-width: 300px; height: auto; max-height: 120px;">
                </div>
                <div style="width:100%;text-align:center;">
                    <!--<vue-qrcode v-bind:value="qrValue" v-bind:errorCorrectionLevel="correctionLevel" />-->
                </div>
                <div style="width:100%;">
                    <div style="text-align: center;">
                        <span style="font-size:30px;font-weight:bold;color:black;">{{headerFooters.company.nameEnglish}} {{headerFooters.company.nameArabic}}</span><br />
                        <span style="font-size:18px;font-weight:bold;color:black;">{{headerFooters.company.categoryEnglish}} {{headerFooters.company.categoryArabic}}</span><br />
                    </div>
                </div>
                <hr />
                <div style="width: 100%; margin-top: 40px; font-size: 22px; text-align: center; margin-bottom:30px;font-weight:bold;">
                    <span>Cash Voucher : {{list.cashVoucher.voucherNo}}</span> <br />
                    <span>Amount : {{parseFloat(list.cashVoucher.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span>

                </div>
            </div>

            <div style="width:400px;margin-left:20px;">
                <hr />
                <div style="width:100%;text-align:center">
                    <p style="font-size:16px;color:black;margin-bottom:0;">Cash Voucher expiry date </p>
                    <p style="font-size:16px;color:black;">تاريخ انتهاء صلاحية القسيمة النقدية</p>
                    <p style="font-size: 15px; color: black; font-weight: bold;">
                        {{list.cashVoucher.date}}
                    </p>
                </div>
            </div>
            <div style="width:400px;display:block;margin-left:20px;margin-top:1.5rem">
                <p style="color:transparent;">hidden</p>
                <p style="text-align:center;color:black;"><span style="margin-right:5px;color:black;">*</span> <span style="margin-right:5px;color:black;">*</span> <span style="margin-right:10px;color:black;">*</span> <span style="font-size:25px; font-weight:bold;color:black;">شـكـرا لــزيــارتـكـــ</span>  <span style="margin-left: 10px;color:black;">*</span> <span style="margin-left: 5px;color:black;">*</span> <span style="margin-left:5px;color:black;">*</span></p>
                <h6 style="text-align:center;color:black;">Thankyou for your visit</h6>
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
        props: ['printDetail', 'headerFooter', 'isTouchScreen'],

        data: function () {
            return {
                qrValue: '',
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

        methods: {
            printInvoice: function () {
                var root = this;
                this.htmlData.htmlString = this.$refs.mychildcomponent.innerHTML;

                var form = new FormData();
                form.append('htmlString', this.htmlData.htmlString);
                //this.$htmlToPaper('purchaseInvoice');
                //axios.post('http://localhos:7013/print/from-pdf', form);
                //axios.post('http://127.0.0.1:7013/print/from-pdf', form);

                if (this.$ServerIp.includes('localhost')) {
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


        },
        created: function () {
            if (this.printDetail.saleItems.length > 0) {
                this.list = this.printDetail;
                this.list.cashVoucher.date = moment(this.list.cashVoucher.date).format('DD/MM/YYYY');
                this.qrValue = this.list.cashVoucher.voucherNo;
            }
        },
        mounted: function () {

            var root = this;
            if (this.printDetail.saleItems.length > 0) {



                this.headerFooters = this.headerFooter;
                this.list.date = moment().format('DD/MM/YYYY hh:mm:ss A');
                root.CompanyID = localStorage.getItem('CompanyID');
                root.UserID = localStorage.getItem('UserID');
                root.employeeId = localStorage.getItem('EmployeeId');
                root.EmployeeData(root.employeeId);

                setTimeout(function () {
                    root.printInvoice();

                }, 125)
            }

        },

    }
</script>

