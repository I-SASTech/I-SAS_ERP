<template>
    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
            <div class="card">
                <div class="card-body">
                    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                        <div class="col-xs-8 col-sm-8 col-md-8 col-lg-8" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <h5 class="view_page_title">{{ $t('SaleOrderView.SaleOrder') }}</h5>
                        </div>
                        <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">


                        </div>

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <h5>{{ $t('SaleOrderView.From') }}</h5>
                            <div class="card" style="border: 1px #dddddd solid;">
                                <div class="card-body">
                                    <label>{{($i18n.locale == 'en' ||isLeftToRight())?headerFooter.company.nameEnglish:headerFooter.company.companyNameArabic}}</label>
                                    <br />
                                    <label>{{($i18n.locale == 'en' ||isLeftToRight())?headerFooter.company.addressEnglish:headerFooter.company.addressArabic}}</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
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

                    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('SaleOrderView.Mobile') }}</label>
                                    <p>{{ purchase.mobile }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('SaleOrderView.ClientPurchaseNo') }}</label>
                                    <p>{{ purchase.clientPurchaseNo }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('SaleOrderView.Quotation') }}</label>
                                    <p>{{ purchase.quotationNo }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('SaleOrderView.PaymentMethod') }}</label>
                                    <p>{{ purchase.paymentMethod }}</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="row" v-if="isRaw=='true'">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('SaleOrderView.SheduleDelivery') }}</label>
                                    <p>{{ purchase.sheduleDelivery }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('SaleOrderView.Days') }}</label>
                                    <p>{{ purchase.days }}</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('SaleOrderView.SaleOrder') }}</label>
                                    <p>{{purchase.isClose==false?'Opened':'Closed'}}</p>
                                </div>
                            </div>
                            <div class="row" v-if="isRaw=='true'">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <label class="font-weight-bold">{{ $t('SaleOrderView.DeliveryTerms') }}</label>
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
                            <saleorder-email-item @update:modelValue="SavePurchaseItems" ref="childComponentRef" :purchase="purchase" :key="rander" />
                            <div class="row">
                                <div class="col-sm-8">
                                    <label>{{ $t('SaleOrderView.TermandCondition') }}:</label>
                                    {{purchase.note}}
                                </div>
                            </div>
                            <div class="row" v-if="purchase.id != '00000000-0000-0000-0000-000000000000' && purchase.approvalStatus==3 && isValid('CanPayAdvanceFromView') ">
                                <div class="col-lg-12">
                                    <div class="accordion" role="tablist">
                                        <b-card no-body class="mb-1">
                                            <b-card-header header-tag="header" class="p-1" role="tab">
                                                <b-button block v-b-toggle.accordion-3 variant="primary">Payment</b-button>
                                            </b-card-header>
                                            <b-collapse id="accordion-3" accordion="my-accordion" role="tabpanel">
                                                <b-card-body>
                                                    <purchaseorder-payment :totalAmount="totalAmount" :customerAccountId="customerAccountId" :show="payment" v-if="payment" @close="paymentSave" :isSaleOrder="'true'" :isPurchase="'false'" :purchaseOrderId="purchase.id" :formName="'AdvanceReceipt'" />
                                                    <div>
                                                        <div class="row" v-if="!purchase.isClose">
                                                            <div class="col-md-12 text-right">
                                                                <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="payment=true"> Add Payment</a>
                                                            </div>
                                                        </div>
                                                        <div class=" table-responsive">
                                                            <table class="table ">
                                                                <thead class="m-0">
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

                    </div>

                </div>
            </div>
        </div>

        <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                            <h5 class="view_page_title">{{ $t('SaleOrderView.BasicInfo') }}</h5>
                        </div>
                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                            <vue-qrcode v-bind:value="qrValue" style="width:140px;" />
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

                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left" v-on:click="DownloadPdf">PDF <i class="fas fa-file-pdf float-right" style="color:#EB5757;"></i></button>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left">Sheets <i class="fas fa-file-excel float-right" style="color:#198754;"></i></button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-footer col-md-3">
                <loading :name="loading" v-model:active="loading"
                         :can-cancel="false"
                         :is-full-page="true"></loading>
            </div>
        </div>
        <SaleOrderPdf :printDetails="printDetails" :headerFooter="headerFooter" v-if="printDetails.length != 0 && isDownloadPdf" v-bind:key="printRenderEmail" :fromEmailLink="true"/>

        <bulk-attachment :documentid="purchase.id" :show="show" v-if="show" @close="attachmentSave" />
        <purchase-order-payment-view :data="paymentview" :formName="'AdvanceReceipt'" @close="paymentView" :show="isPaymentview" v-if="isPaymentview" />
    </div>

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

        data: function () {
            return {
                isDownloadPdf: false,
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
                    email: ''
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
            SavePurchaseItems: function (saleOrderItems) {

                this.purchase.saleOrderItems = saleOrderItems;
                this.getTotalAmount();
            },
            
            GetPaymentVoucher: function () {
                var root = this;
                var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiOGU2MTI1NzYtMDNhNy00MDk0LTg2ZWEtNjAwODViY2E5OTk5IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjU3MDk3NTE2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.IjtMbckhrVhVabG1D-DhjTtidoDRPXxw-qCtL0yKhLY'


                root.$https.get('Purchase/SaleOrderPaymentList?id=' + this.purchase.id + '&isEmail=' + true, { headers: { "Authorization": `Bearer ${token}` } })
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
                        data: 'AddSaleOrders'
                    }
                })
            },
            ViewPaymentVoucher: function (id) {
                var root = this;

                var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiOGU2MTI1NzYtMDNhNy00MDk0LTg2ZWEtNjAwODViY2E5OTk5IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjU3MDk3NTE2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.IjtMbckhrVhVabG1D-DhjTtidoDRPXxw-qCtL0yKhLY'

                root.$https.get('/Purchase/SaleOrderPaymentDetail?Id=' + id + '&isEmail=' + true, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$https.get('/PaymentVoucher/PaymentVoucherDetails?Id=' + response.data.paymentVoucherId + '&isEmail=' + true, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
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
                var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiOGU2MTI1NzYtMDNhNy00MDk0LTg2ZWEtNjAwODViY2E5OTk5IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjU3MDk3NTE2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.IjtMbckhrVhVabG1D-DhjTtidoDRPXxw-qCtL0yKhLY'

                root.$https.get("/Sale/CompanyDetailForEmail?id=" + this.$route.query.cId , { headers: { Authorization: `Bearer ${token}` }, })
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
            },
            GetSaleOrderDataForEmail: function () {
                var root = this;
                root.loading = true
                var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiOGU2MTI1NzYtMDNhNy00MDk0LTg2ZWEtNjAwODViY2E5OTk5IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjU3MDk3NTE2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.IjtMbckhrVhVabG1D-DhjTtidoDRPXxw-qCtL0yKhLY'
                root.printDetailsPos = [];
                root.$https.get("/Purchase/SaleOrderDetail?Id=" + this.$route.query.id + '&isFifo=' + root.$route.query.fifo + '&openBatch=' + root.$route.query.oBatch + '&isEmail=' + true, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            
                            root.purchase = response.data;
                            root.customerAccountId = response.data.customerAccountId;
                            root.purchase.date = moment(root.purchase.date).format('llll');

                            if (root.$route.query.unit == 'true' && root.$route.query.decimal == 'true') {

                                root.purchase.saleOrderItems.forEach(function (x) {

                                    x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                    x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                                
                            }
                            if (root.$route.query.unit == 'true' && root.$route.query.decimal != 'true') {

                                root.purchase.saleOrderItems.forEach(function (x) {

                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                                
                            }
                            
                            root.attachment = true;
                            root.rander++;
                           
                            root.loading = false
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
            
            DownloadPdf: function () {
                var root = this;
                root.loading = true
                var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiOGU2MTI1NzYtMDNhNy00MDk0LTg2ZWEtNjAwODViY2E5OTk5IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjU3MDk3NTE2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.IjtMbckhrVhVabG1D-DhjTtidoDRPXxw-qCtL0yKhLY'
                root.printDetailsPos = [];
                root.$https.get("/Purchase/SaleOrderDetail?Id=" + this.$route.query.id + '&isFifo=' + root.$route.query.fifo + '&openBatch=' + root.$route.query.oBatch + '&isEmail=' + true, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {
                            
                            root.printDetails = response.data;
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                root.printDetails.saleOrderItems.forEach(function (x) {
                                    x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                    x.newQuantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                root.printDetails.saleOrderItems.forEach(function (x) {
                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.newQuantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            root.printRenderEmail++
                            root.isDownloadPdf = true
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
        },
        created: function () {

            
            this.GetHeaderDetail();
            this.GetSaleOrderDataForEmail();
        },
        mounted: function () {
            
            this.isRaw = this.$route.query.isProd;
            this.currency = this.$route.query.cur;
            if (this.$route.query.data == undefined) {
                this.purchase.date = moment().format('llll');
                this.daterander++;
            }
        },
    };
</script>
