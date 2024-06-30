<template>
    <div class="row" v-if="isValid('CanViewDetailSaleOrder') || (isValid('CanViewDetailSaleOrder') && formName == 'SaleOrderTracking')" >
        <div class="col-lg-12">
            <div class="row">
                <div class="col" v-if="formName == 'SaleOrderTracking'">
                    <h5 class="page_title">{{ $t('SaleOrderView.SalesOrderTracking') }}</h5>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('SaleOrderView.Home') }}</a></li>
                        <li class="breadcrumb-item active">
                            {{ $t('SaleOrderView.SalesOrderTracking') }}
                        </li> 
                    </ol>
                </div>
                <div class="col" v-else>
                    <h5 class="page_title">{{ $t('SaleOrderView.SaleOrder') }}</h5>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('SaleOrderView.Home') }}</a></li>
                        <li class="breadcrumb-item active">
                            {{ $t('SaleOrderView.SaleOrder') }}
                        </li> 
                    </ol>
                </div>

                <div class="col-auto align-self-center">

                    <a v-on:click="goToPurchase" href="javascript:void(0);"
                       class="btn btn-sm btn-outline-danger mx-1">
                        <i class="fas fa-arrow-circle-left fa-lg"></i>

                    </a>
                    <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                       class="btn btn-sm btn-outline-danger">
                        {{ $t('SaleOrder.Close') }}
                    </a>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
            <div class="card">
                <div class="card-body">
                    <div class="row" >
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" >
                            <h5>{{ $t('SaleOrderView.From') }}</h5>
                            <div class="card" style="border: 1px #dddddd solid;">
                                <div class="card-body">
                                    <label>{{($i18n.locale == 'en' ||isLeftToRight())?headerFooter.company.nameEnglish:headerFooter.company.companyNameArabic}}</label>
                                    <br />
                                    <label>{{($i18n.locale == 'en' ||isLeftToRight())?headerFooter.company.addressEnglish:headerFooter.company.addressArabic}}</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" >
                            <h5>{{ $t('SaleOrderView.InvoiceTo') }}</h5>
                            <div class="card" style="border: 1px #dddddd solid;">
                                <div class="card-body">
                                    <label>{{($i18n.locale == 'en' ||isLeftToRight())?purchase.customerNameEn:purchase.customerNameAr}}</label>
                                    <br />
                                    <label>{{purchase.customerAddress}}</label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" >
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" >
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="fw-bold">{{ $t('SaleOrderView.Mobile') }}</label>
                                    <p>{{ purchase.mobile }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="fw-bold">{{ $t('SaleOrderView.ClientPurchaseNo') }}</label>
                                    <p>{{ purchase.clientPurchaseNo }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="fw-bold">{{ $t('SaleOrderView.Quotation') }}</label>
                                    <p>{{ purchase.quotationNo }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="fw-bold">{{ $t('SaleOrderView.PaymentMethod') }}</label>
                                    <p>{{ purchase.paymentMethod }}</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" >
                            <div class="row" v-if="isRaw=='true'">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="fw-bold">{{ $t('SaleOrderView.SheduleDelivery') }}</label>
                                    <p>{{ purchase.sheduleDelivery }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="fw-bold">{{ $t('SaleOrderView.Days') }}</label>
                                    <p>{{ purchase.days }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="fw-bold">{{ $t('SaleOrderView.SaleOrder') }}</label>
                                    <p>{{purchase.isClose==false?$t('SaleOrderView.Open'):$t('SaleOrderView.Close')}}</p>
                                </div>
                            </div>
                            <div class="row" v-if="isRaw=='true'">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="fw-bold">{{ $t('SaleOrderView.DeliveryTerms') }}</label>
                                    <p>
                                        {{ $t('SaleOrderView.Fregiht') }} <span class="small badge badge-success">{{ purchase.isFreight }}</span>
                                        {{ $t('SaleOrderView.Labour') }} <span class="small badge badge-success">{{ purchase.isLabour }}</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'arabicLanguage'">
                        <div class="col-md-12">
                            <saleorder-view-item @update:modelValue="SavePurchaseItems" ref="childComponentRef" :key="rander" :wareHouseId="purchase.wareHouseId" :taxMethod="purchase.taxMethod" :taxRateId="purchase.taxRateId"  :adjustmentProp="purchase.discount" :adjustmentSignProp="adjustmentSignProp" :isDiscountOnTransaction="purchase.isDiscountOnTransaction" :transactionLevelDiscountProp="purchase.transactionLevelDiscount" :isFixed="purchase.isFixed" :isBeforeTax="purchase.isBeforeTax" />

                            <div class="row">
                                <div class="col-lg-12 mt-4 mb-2">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                                    <div class="form-group pe-3">
                                                        <label>{{ $t('SaleOrderView.TermandCondition') }}:</label>
                                                        <textarea class="form-control " readonly rows="3" v-model="purchase.note" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <div class="form-group ps-3">
                                                        <div class="font-xs mb-1">{{ $t('SaleOrderView.Attachment') }}</div>
                                                        <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i>  {{ $t('SaleOrderView.Attachment') }} </button>
                                                        <div>
                                                            <small class="text-muted">
                                                                {{ $t('SaleOrderView.FileSize') }}
                                                            </small>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" v-if="purchase.id != '00000000-0000-0000-0000-000000000000' && purchase.approvalStatus==3 && isValid('CanPayAdvanceFromView') ">
                                <div class="col-lg-12">
                                    <div class="accordion" role="tablist">
                                        <b-card no-body class="mb-4">
                                            <b-card-header header-tag="header" class="p-0" role="tab">
                                                <b-button block v-b-toggle.accordion-3 variant="primary" class="btn" style="width:100%">Payment</b-button>
                                            </b-card-header>
                                            <b-collapse id="accordion-3" accordion="my-accordion" role="tabpanel">
                                                <b-card-body>
                                                    <purchaseorder-payment :totalAmount="totalAmount" :customerAccountId="customerAccountId" :show="payment" v-if="payment" @close="paymentSave" :isSaleOrder="'true'" :isPurchase="'false'" :purchaseOrderId="purchase.id" :formName="'AdvanceReceipt'" />
                                                    <div>
                                                        <div class="row" v-if="!purchase.isClose">
                                                            <div class="col-md-12 text-right pb-3">
                                                                <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="payment=true"> Add Payment</a>
                                                            </div>
                                                        </div>
                                                        <div class=" table-responsive">
                                                            <table class="table ">
                                                                <thead class="thead-light table-hover">
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th style="width:20%;">{{ $t('SaleOrderView.VoucherNumber') }} </th>
                                                                        <th style="width:20%;">{{ $t('SaleOrderView.Date') }} </th>
                                                                        <th class="text-right">{{ $t('SaleOrderView.Amount') }} </th>
                                                                        <th class="text-center">{{ $t('SaleOrderView.PaymentMode') }} </th>
                                                                        <th>{{ $t('SaleOrderView.Description') }} </th>
                                                                        <th></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr v-for="(payment,index) in purchase.paymentVoucher" v-bind:key="index">
                                                                        <td>
                                                                            {{index+1}}
                                                                        </td>
                                                                        <th>{{payment.voucherNumber}}</th>
                                                                        <th>{{getDate(payment.date)}}</th>
                                                                        <th class="text-right">{{currency}} {{parseFloat(payment.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</th>
                                                                        <th class="text-center"><span v-if="payment.paymentMode==0">Cash</span><span v-if="payment.paymentMode==1">Bank</span></th>
                                                                        <th>{{payment.narration}}</th>
                                                                        <th>
                                                                            <a href="javascript:void(0)" title="Payment View" class="btn  btn-icon btn-primary btn-sm" v-on:click="ViewPaymentVoucher(payment.id)"><i class=" fas fa-eye"></i></a>
                                                                        </th>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </b-card-body>
                                            </b-collapse>
                                        </b-card>

                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 ">
                                <button class="btn btn-sm btn-outline-danger" v-on:click="goToPurchase">
                                    {{ $t('SaleOrderView.Cancel') }}
                                </button>
                            </div>
                           
                        </div>
                      
                    </div>

                </div>
            </div>
        </div>

        <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3" >
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <h5 class="view_page_title">{{ $t('SaleOrderView.BasicInfo') }}</h5>
                        </div>
                        <div class="col-lg-12 text-center">
                            <vue-qrcode v-bind:value="qrValue" style="width:120px;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <label class="invoice_lbl">{{ $t('SaleOrderView.SaleOrder') }}#</label>
                            <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                            <label>{{purchase.registrationNo}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('SaleOrderView.Date') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{purchase.date}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('SaleOrderView.Refrence') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{ purchase.refrence }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2" v-if="isValid('CanSendSaleEmailAsLink') || isValid('CanSendSaleEmailAsPdf')">
                            <label class="invoice_lbl">Send Copy To</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label style="margin-right:5px">{{ $t('SaleOrderView.Email') }}: </label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2" v-if="isValid('CanSendSaleEmailAsLink') || isValid('CanSendSaleEmailAsPdf')">
                            <button class="btn btn-primary btn-block" v-on:click="SendEmail">{{ $t('SaleOrderView.SendSaleOrder') }}</button>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left">PDF <i class="fas fa-file-pdf float-right" style="color:#EB5757;"></i></button>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left">Sheets <i class="fas fa-file-excel float-right" style="color:#198754;"></i></button>
                        </div>
                    </div>
                </div>
            </div>
            <div >
                <loading :name="loading" v-model:active="loading"
                         :can-cancel="false"
                         :is-full-page="true"></loading>
            </div>
        </div>
        <email-compose :show="emailComposeShow" v-if="emailComposeShow" @close="emailComposeShow = false" :documentId="orderId" :headerFooter="headerFooter" :email="purchase.email" :formName="'Order'"></email-compose>

        <bulk-attachment :documentid="purchase.id" :show="show" v-if="show" @close="attachmentSave" />
        <purchase-order-payment-view :data="paymentview" :formName="'AdvanceReceipt'" @close="paymentView" :show="isPaymentview" v-if="isPaymentview" />
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    


    //import Vue3Barcode from 'vue3-barcode'
    export default {
        components: {
            
            Loading
        },
        mixins: [clickMixin],
        props: ['formName'],
        data: function () {
            return {
                discountTypeOption: 'At Line Item Level',
                adjustmentSignProp: '+',

                orderId:'',
                emailComposeShow: false,
                qrValue: '',
                printDetails:[],
                show: false,
                payment: false,
                currency: '',
                isRaw: '',
                saleOrder: '',
                daterander: 0,
                rander: 0,
                render: 0,
                customerRender: 0,
                printRenderEmail: 0,
                totalAmount: 0,
                customerId: '',
                customerAccountId: '',
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    registrationNo: "",
                    quotationId: "",
                    customerId: "",
                    refrence: "",
                    days: '',
                    purchaseOrder: "",
                    paymentMethod: "",
                    sheduleDelivery: "",
                    note: '',
                    isFreight: false,
                    isLabour: false,
                    isQuotation: false,
                    saleOrderItems: [],
                    path: '',
                    clientPurchaseNo: '',
                    email: '',
                    taxMethod: '',
                    taxRateId: '',


                    discount: 0,
                    isDiscountOnTransaction: false,
                    isFixed: false,
                    isBeforeTax: true,
                    transactionLevelDiscount: 0

                },
                loading: false,
                paymentview: '',
                isPaymentview: false,
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                emailData: {
                    emailPath: '',
                    email: '',
                    subject: 'Sale Order',
                    companyName: '',
                    buttonName: 'View Sale Order'
                },
            };
        },
        computed: {
           
            calulateTotalQty: function () {
                return this.purchase.saleOrderItems.reduce(function (a, c) { return a + (Number((c.quantity) || 0) > 0 ? Number((c.quantity) || 0) : 0) }, 0)
            },
            calulateNetTotal: function () {
                return this.purchase.saleOrderItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0)
            },
            calulateTotalExclVAT: function () {
                return this.purchase.saleOrderItems.reduce(function (a, c) { return a + Number((c.total) || 0) }, 0)
            },
            calulateTotalVAT: function () {
                return this.purchase.saleOrderItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0)
            },
            calulateTotalInclusiveVAT: function () {
                return this.purchase.saleOrderItems.reduce(function (a, c) { return a + Number((c.inclusiveVat) || 0) }, 0)
            },
            calulateDiscountAmount: function () {
                return this.purchase.saleOrderItems.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0)
            },
            calulateBundleAmount: function () {
                return this.purchase.saleOrderItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
            }

        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            SendEmail: function () {
                this.orderId = this.purchase.id
                this.emailComposeShow = true;
            },
            calulateDiscountAmount1: function () {
                return this.purchase.saleOrderItems.reduce(function (a, c) { return a + Number(c.discountAmount || 0) }, 0)
            },
            calulateBundleAmount1: function () {
                return this.purchase.saleOrderItems.reduce(function (a, c) { return a + Number(c.bundleAmount || 0) }, 0)
            },
            calulateNetTotalWithVAT: function () {
                var total = this.purchase.saleOrderItems.reduce(function (a, c) { return a + Number((c.total + c.includingVat) || 0) }, 0);
                var grandTotal = parseFloat(total) - (this.calulateDiscountAmount1() + this.calulateBundleAmount1())
                return (parseFloat(grandTotal).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            calulateTotalVATofInvoice: function () {
                var total = this.purchase.saleOrderItems.reduce(function (a, c) { return a + Number((c.includingVat + c.inclusiveVat) || 0) }, 0);
                return (parseFloat(total).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,"));
            },
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function () {
                this.show = false;
            },

            getTotalAmount: function () {
                this.totalAmount = this.$refs.childComponentRef.getTotalAmount();
            },

            getDate: function (date) {
                if (date == null || date == undefined) {
                    return "";
                }
                else {
                    return moment(date).format('LLL');
                }
            },
            SavePurchaseItems: function (saleOrderItems, discount, adjustmentSignProp, transactionLevelDiscount) {

                this.purchase.saleOrderItems = saleOrderItems;
                this.purchase.discount = (discount == '' || discount == null) ? 0 : (adjustmentSignProp == '+' ? parseFloat(discount) : (-1) * parseFloat(discount))

                this.purchase.transactionLevelDiscount = (transactionLevelDiscount == '' || transactionLevelDiscount == null) ? 0 : parseFloat(transactionLevelDiscount)
            },

            savePurchase: function (status) {
                this.purchase.approvalStatus = status
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https
                    .post('/Purchase/SaveSaleOrderInformation', root.purchase, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.loading = false
                        root.info = response.data

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Data Saved Successfully!' : '!حفظ بنجاح',
                            type: 'success',
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,
                        }).then(function (response) {
                            if (response != undefined) {
                                if (root.purchase.id == "00000000-0000-0000-0000-000000000000") {
                                    root.$router.go('AddSaleOrder');

                                } else {
                                    root.$router.push("SaleOrder");
                                }
                            }
                        });

                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)

            },
            GetPaymentVoucher: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Purchase/SaleOrderPaymentList?id=' + this.purchase.id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.purchase.paymentVoucher = response.data;
                        }
                    });
            },
            paymentSave: function () {
                this.payment = false;
                this.GetPaymentVoucher();
            },

            goToPurchase: function () {
                this.$router.push({
                    path: '/SaleOrder',
                    query: {
                        data: 'AddSaleOrders' + this.formName,
                        formName: this.formName
                    }
                })
            },
            ViewPaymentVoucher: function (id) {
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Purchase/SaleOrderPaymentDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$https.get('/PaymentVoucher/PaymentVoucherDetails?Id=' + response.data.paymentVoucherId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                            if (response.data != null) {
                                root.paymentview = response.data;
                                root.isPaymentview = true;
                            }
                        });
                    }
                });
            },
            paymentView: function () {
                this.isPaymentview = false;
            },
            GetHeaderDetail: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;
                            var sellerNameBuff = root.GetTLVForValue('1', root.headerFooter.company.nameEnglish)
                            var vatRegistrationNoBuff = root.GetTLVForValue('2', root.headerFooter.company.vatRegistrationNo)
                            var timeStampBuff = root.GetTLVForValue('3', root.purchase.date)
                            var totalWithVat = root.GetTLVForValue('4', root.calulateNetTotalWithVAT())
                            var totalVat = root.GetTLVForValue('5', root.calulateTotalVATofInvoice())
                            var tagArray = [sellerNameBuff, vatRegistrationNoBuff, timeStampBuff, totalWithVat, totalVat]
                            var qrCodeBuff = Buffer.concat(tagArray)
                            root.qrValue = qrCodeBuff.toString('base64')
                        }
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
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
             root.$route.query.data = this.$store.isGetEdit;
            }

            if (this.$route.query.data != undefined) {

                this.purchase = this.$route.query.data;
                this.customerAccountId = this.$route.query.data.customerAccountId;
                this.purchase.date = moment(this.purchase.date).format('llll');
                
                if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {

                    this.purchase.saleOrderItems.forEach(function (x) {

                        x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                        x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack));
                        x.unitPerPack = x.product.unitPerPack;
                    });
                }
                if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {

                    this.purchase.saleOrderItems.forEach(function (x) {

                        x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                        x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                        x.unitPerPack = x.product.unitPerPack;
                    });
                }
                this.discountTypeOption = this.purchase.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'
                this.adjustmentSignProp = this.purchase.discount >= 0 ? '+' : '-'
                this.attachment = true;
                this.rander++;
                this.render++;
            }
            this.GetHeaderDetail();
        },
        mounted: function () {
            this.saleOrder = localStorage.getItem('SaleOrder');
            this.isRaw = localStorage.getItem('IsProduction');
            this.currency = localStorage.getItem('currency');
            if (this.$route.query.data == undefined) {
                this.purchase.date = moment().format('llll');
                this.daterander++;
            }
        },
    };
</script>
