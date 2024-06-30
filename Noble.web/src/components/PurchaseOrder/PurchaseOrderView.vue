<template>
    <div class="row" v-if="isValid('CanViewOrderDetail') ">
        <div class="col-lg-12">
            <div class="row">
                <div class="col">
                    <h5 class="page_title">{{ $t('PurchaseOrderView.PurchaseOrder') }}</h5>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PurchaseOrderView.Home') }}</a></li>
                        <li class="breadcrumb-item active">
                            {{ $t('PurchaseOrderView.PurchaseOrder') }}
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
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-9">
            <div class="card">
                <div class="card-body">


                    <div class="row">

                          <div class="col-lg-6">
                            <h5>Total VAT:</h5>
                        </div>
                        <div class="col-lg-6">
                            {{currency}} {{parseFloat(purchase.vatAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

                        </div>

                        <div class="col-lg-6">
                            <h5>Total Amount:</h5>
                        </div>
                        <div class="col-lg-6">
                            {{currency}} {{parseFloat(purchase.netAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                        </div>


                        <!-- <div class="col-lg-6">
                            <h5>{{ $t('PurchaseOrderView.TaxMethod') }}:</h5>
                        </div>
                        <div class="col-lg-6">
                            {{purchase.taxMethod}}
                        </div>

                        <div class="col-lg-6">
                            <h5>{{ $t('PurchaseOrderView.VAT%') }}:</h5>
                        </div>
                        <div class="col-lg-6">
                            {{purchase.taxRatesName}}
                        </div> -->
                    </div>

                    <br />
                    <!-- <purchase-view-item @update:modelValue="SavePurchaseItems" ref="childComponentRef" :taxMethod="purchase.taxMethod" :taxRateId="purchase.taxRateId" :raw="purchase.isRaw" :isSerial="true" :key="rander" :po="true" :purchaseOrderId="purchase.purchaseOrderId" @discountChanging="updateDiscountChanging" :adjustmentProp="purchase.discount" :adjustmentSignProp="adjustmentSignProp" :isDiscountOnTransaction="purchase.isDiscountOnTransaction" :transactionLevelDiscountProp="purchase.transactionLevelDiscount" :isFixed="purchase.isFixed" :isBeforeTax="purchase.isBeforeTax" /> -->
                    <!-- <div class="col-lg-12 mt-4 mb-5">
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                        <div class="form-group pe-3">
                                            <label>{{ $t('PurchaseOrderView.TermandCondition') }}:</label>
                                            <textarea class="form-control " disabled rows="3" v-model="purchase.note" />
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group ps-3">
                                            <div class="font-xs mb-1">{{ $t('PurchaseOrderView.AttachFiles') }}</div>
                                            <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> {{ $t('PurchaseView.Attachment') }} </button>
                                            <div>
                                                <small class="text-muted">
                                                    {{ $t('PurchaseOrderView.FileSize') }}
                                                </small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div> -->

                    <div class="accordion mb-5" id="accordionExample" v-if="(purchase.approvalStatus === 3 || purchase.approvalStatus === 5 || purchase.approvalStatus === 9) && purchase.id != '00000000-0000-0000-0000-000000000000' && internationalPurchase=='true'">
                        <div class="accordion-item" v-if="isValid('CanUploadAttachment') || isValid('CanDownloadAttachment')">
                            <h5 class="accordion-header m-0" id="headingOne">
                                <button class="accordion-button fw-semibold " type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                    {{ $t('PurchaseOrderView.Attachment') }}
                                </button>
                            </h5>
                            <div id="collapseOne" class="accordion-collapse collapse show" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <import-attachment :purchase="purchase" :show="attachments" v-if="attachments" @close="attachmentSave" :document="'Purchase'" />
                                    <div>
                                        <div class="row" v-if="!purchase.isClose && internationalPurchase=='true' && (purchase.approvalStatus === 5 || purchase.approvalStatus === 3) && purchase.id != '00000000-0000-0000-0000-000000000000' && isValid('CanUploadAttachment')">
                                            <div class="col-md-12 text-right">
                                                <a href="javascript:void(0)" class="btn btn-sm btn-outline-primary " v-on:click="attachments=true"> Upload</a>
                                            </div>
                                        </div>
                                        <div class=" table-responsive">
                                            <table class="table ">
                                                <thead class="thead-light table-hover">
                                                    <tr>
                                                        <th>#</th>
                                                        <th>{{ $t('PurchaseOrderView.Date') }} </th>
                                                        <th>{{ $t('PurchaseOrderView.Description') }} </th>
                                                        <th v-if="isValid('CanDownloadAttachment')">{{ $t('PurchaseOrderView.Attachment') }}</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(contact,index) in purchase.purchaseAttachments" v-bind:key="index">
                                                        <td>
                                                            {{index+1}}
                                                        </td>
                                                        <th>{{getDate(contact.date)}}</th>
                                                        <th>{{contact.description}}</th>

                                                        <td v-if="isValid('CanDownloadAttachment')">
                                                            <button class="btn btn-sm btn-primary  btn-icon mr-2"
                                                                    v-if="contact.path != ''"
                                                                    v-on:click="DownloadAttachment(contact.path)">
                                                                <i class="fa fa-download"></i>
                                                            </button>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="accordion-item" v-if="isValid('CanAddOrderAction') || isValid('CanViewOrderAction')">
                            <h5 class="accordion-header m-0" id="headingTwo">
                                <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    {{ $t('PurchaseOrderView.Actions') }}
                                </button>
                            </h5>
                            <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <add-company-action :action="action" :show="show" v-if="show" @close="IsSave" :document="'Purchase'" />
                                    <div class="row">
                                        <div class="col-md-12" v-if="!purchase.isClose && internationalPurchase=='true' && (purchase.approvalStatus === 5 || purchase.approvalStatus === 3|| purchase.approvalStatus === 3) && purchase.id != '00000000-0000-0000-0000-000000000000' && isValid('CanAddOrderAction')">
                                            <div class="col-sm-6 ">
                                                <a href="javascript:void(0)" class="btn btn-sm btn-outline-primary  " v-on:click="show=true"> Action</a>
                                            </div>
                                        </div>
                                        <div class=" table-responsive" v-if="isValid('CanViewOrderAction')">
                                            <table class="table ">
                                                <thead class="thead-light table-hover">
                                                    <tr>
                                                        <th>#</th>
                                                        <th>{{ $t('PurchaseOrderView.Status') }}</th>
                                                        <th>{{ $t('PurchaseOrderView.Date') }} </th>
                                                        <th>{{ $t('PurchaseOrderView.Description/Reason') }}</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(process,index) in purchase.actionProcess" v-bind:key="process.id">
                                                        <td>
                                                            {{index+1}}
                                                        </td>
                                                        <th><span class="badge badge-primary">{{process.processName}}</span></th>
                                                        <th>{{getDate(process.date)}}</th>
                                                        <th>{{process.description}}</th>

                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="accordion-item"  v-if="isValid('CanAddAdvancePayment') || isValid('CanViewAdvancePayment') || isValid('CanViewDetailAdvancePayment')">
                            <h5 class="accordion-header m-0" id="headingThree">
                                <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                    {{ $t('PurchaseOrderView.Payment') }}
                                </button>
                            </h5>
                            <div id="collapseThree" class="accordion-collapse collapse" aria-labelledby="headingThree" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <purchaseorder-payment :totalAmount="totalAmount" :customerAccountId="advanceAccountId" :show="payment" v-if="payment" @close="paymentSave" :isSaleOrder="'false'" :isPurchase="'true'" :purchaseOrderId="purchase.id" :formName="'AdvancePay'" />
                                    <div>
                                        <div class="row" v-if="!purchase.isClose && internationalPurchase=='true' && (purchase.approvalStatus === 5 || purchase.approvalStatus === 3) && purchase.id != '00000000-0000-0000-0000-000000000000' && isValid('CanAddAdvancePayment')">
                                            <div class="col-md-12 text-right">
                                                <a href="javascript:void(0)" class="btn btn-sm btn-outline-primary " v-on:click="payment=true"> Add Payment</a>
                                            </div>
                                        </div>
                                        <div class=" table-responsive" v-if="isValid('CanViewAdvancePayment')">
                                            <table class="table ">
                                                <thead class="thead-light table-hover">
                                                    <tr>
                                                        <th>#</th>
                                                        <th style="width:20%;">{{ $t('PurchaseOrderView.Date') }} </th>
                                                        <th style="width:20%;">{{ $t('PurchaseOrderView.VoucherNumber') }} </th>
                                                        <th class="text-right">{{ $t('PurchaseOrderView.Amount') }} </th>
                                                        <th class="text-center">{{ $t('PurchaseOrderView.PaymentMode') }} </th>
                                                        <th></th>
                                                        <th>{{ $t('PurchaseOrderView.Description') }} </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(payment,index) in purchase.paymentVoucher" v-bind:key="index">
                                                        <td>
                                                            {{index+1}}
                                                        </td>
                                                        <th>{{getDate(payment.date)}}</th>
                                                        <th>{{payment.voucherNumber}}</th>
                                                        <th class="text-right">{{currency}} {{parseFloat(payment.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</th>
                                                        <th class="text-center"><span v-if="payment.paymentMode==0"> {{ $t('PurchaseOrderView.Cash') }}</span>
                                                                                <span v-if="payment.paymentMode==1"> {{ $t('PurchaseOrderView.Bank') }}</span></th>
                                                        <th>{{payment.narration}}</th>
                                                        <th>
                                                            <a href="javascript:void(0)" title="Payment View" class="btn   btn-primary btn-sm" v-on:click="ViewPaymentVoucher(payment.id, false)" v-if="isValid('CanViewDetailAdvancePayment')"><i class=" fas fa-eye"></i></a>
                                                        </th>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="accordion-item" v-if="isValid('CanViewOrderExpense') || isValid('CanAddOrderExpense') || isValid('CanViewDetailOrderExpense')">
                            <h5 class="accordion-header m-0" id="headingfour">
                                <button class="accordion-button collapsed fw-semibold" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                                    {{ $t('PurchaseOrderView.Expense') }}
                                </button>
                            </h5>
                            <div id="collapseFour" class="accordion-collapse collapse" aria-labelledby="headingfour" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <purchaseorder-expense :show="expense" v-if="expense" @close="expenseSave" :newisPurchase="'true'" :purchaseOrderId="purchase.id" :formName="'AdvanceExpense'" />
                                    <purchaseorder-expensebill :show="bill" v-if="bill" @close="expenseSave" :isInvoice="false" :purchaseId="purchase.id" />
                                    <div>
                                        <div class="row" v-if="(purchase.approvalStatus === 5 || purchase.approvalStatus === 3) && !purchase.isClose && purchase.id != '00000000-0000-0000-0000-000000000000' && isValid('CanAddOrderExpense')">
                                            <div class="col-md-12 text-right">
                                                <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="bill=true"> Add Bill</a>
                                                <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="expense=true"> Add Expense</a>
                                            </div>
                                        </div>
                                        <div class=" table-responsive" v-if="isValid('CanViewOrderExpense')">
                                            <table class="table ">
                                                <thead class="thead-light table-hover">
                                                    <tr>
                                                        <th>#</th>
                                                        <th style="width:20%;">{{ $t('PurchaseOrderView.Date') }} </th>
                                                        <th style="width:20%;">{{ $t('PurchaseOrderView.VoucherNumber') }} </th>
                                                        <th class="text-right">{{ $t('PurchaseOrderView.Amount') }} </th>
                                                        <th class="text-right">{{ $t('PurchaseOrderView.UsedAmount') }} </th>
                                                        <th class="text-center">{{ $t('PurchaseOrderView.PaymentMode') }} </th>
                                                        <th>{{ $t('PurchaseOrderView.Description') }} </th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(payment,index) in purchase.purchaseOrderExpenses" v-bind:key="index">
                                                        <td>
                                                            {{index+1}}
                                                        </td>
                                                        <th>{{getDate(payment.date)}}</th>
                                                        <th>{{payment.voucherNumber}}</th>
                                                        <th class="text-right">{{currency}} {{parseFloat(payment.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</th>
                                                        <th class="text-right">{{currency}} {{parseFloat(payment.usedAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</th>
                                                        <th class="text-center"><span v-if="payment.paymentMode==0">Cash</span><span v-if="payment.paymentMode==1">Bank</span></th>
                                                        <th>{{payment.narration}}</th>
                                                        <th>
                                                            <a href="javascript:void(0)" title="Payment View" class="btn  btn-icon btn-primary btn-sm" v-on:click="ViewPaymentVoucher(payment.id, true)" v-if="isValid('CanViewDetailOrderExpense')"><i class=" fas fa-eye"></i></a>
                                                        </th>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>                  
                    <div class="row">
                        <div class="col-md-12 ">

                            <button class="btn btn-sm btn-outline-danger" v-on:click="goToPurchase">
                                {{ $t('PurchaseOrderView.Cancel') }}
                            </button>
                        </div>

                    </div>
                </div>
               

            </div>

        </div>
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">
            <div class="card">
                <div class="card-body">
                    <div class="row">

                        <div class=" col-lg-12">
                            <h5 class="view_page_title">{{ $t('PurchaseView.BasicInfo') }}</h5>

                        </div>
                        
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <label class="invoice_lbl">{{ $t('PurchaseOrderView.PurchaseOrder') }}#</label>
                            <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                            <label> {{purchase.registrationNo}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <label class="invoice_lbl">{{ $t('PurchaseView.PurchaseOrder') }}#</label>
                            <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                            <label>{{purchase.purchaseOrderNo}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                       
                       
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('PurchaseView.Date') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{purchase.date}}</label>
                            <div v-if="isValid('CanViewPreviousVersion') && (purchase.approvalStatus === 5 || purchase.approvalStatus === 3)">
                                <span v-for="(version,index) in purchase.purchaseOrderVersion" :key="index">
                                    <a href="javascript:void(0)" v-if="index!=0" title="View" class="btn  btn-primary btn-sm">{{version.version}}</a>
                                </span>
                            </div>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('PurchaseOrderView.Supplier') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{ purchase.supplierName }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl"> {{ $t('PurchaseOrderView.SupplierQuotationNumber') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{ purchase.invoiceNo }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>





                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('PurchaseOrderView.SendCopyTo') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{ $t('PurchaseOrderView.Email') }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <!--<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                        <button class="btn btn-primary btn-block">Send Invoice</button>
                    </div>-->
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left">PDF <i class="fas fa-file-pdf float-right" style="color:#EB5757;"></i></button>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6 mt-2">
                            <button class="btn btn-light btn-block text-left">Sheets <i class="fas fa-file-excel float-right" style="color:#198754;"></i></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        <bulk-attachment :documentid="purchase.id" :show="isAttachshow" v-if="isAttachshow" @close="attachmentSaved" />
        <purchase-order-payment-view :data="paymentview" :formName="'AdvancePay'" @close="paymentView" :show="isPaymentview" v-if="isPaymentview" />
        <purchase-order-payment-view :data="paymentview" :formName="'AdvanceExpense'" @close="paymentView" :show="isExpenseview" v-if="isExpenseview" />
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                discountTypeOption: 'At Line Item Level',
                adjustmentSignProp: '+',

                //versionAllow: '',
                internationalPurchase: '',
                currency: '',
                daterander: 0,
                rander: 0,
                totalAmount: 0,
                isAttachshow: false,
                bill: false,
                attachment: false,
                attachments: false,
                show: false,
                payment: false,
                expense: false,
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    registrationNo: "",
                    supplierId: "",
                    invoiceNo: "",
                    invoiceDate: "",
                    purchaseOrder: "",
                    note: '',
                    purchaseOrderItems: [],
                    path: '',
                    isRaw: false,
                    discount: 0,
                    isDiscountOnTransaction: false,
                    isFixed: false,
                    isBeforeTax: true,
                    transactionLevelDiscount: 0
                },
                raw: '',
                loading: false,
                language: 'Nothing',
                options: [],
                supplierRender: 0,
                advanceAccountId: '',
                paymentview: '',
                isPaymentview: false,
                isExpenseview: false,
                action: {
                    id: '00000000-0000-0000-0000-000000000000',
                    purchaseOrderId: '',
                    processId: '',
                    date: '',
                    description: '',
                },
            };
        },
        validations() {
            return {
                purchase: {
                    date: { required },
                    registrationNo: { required },
                    supplierId: { required },
                    invoiceNo: {},
                    invoiceDate: {},

                    purchaseOrderItems: { required },
                },
                }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            Attachment: function () {
                this.isAttachshow = true;
            },

            attachmentSaved: function () {
                this.isAttachshow = false;
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
            attachmentSave: function () {
                this.GetAttachment();
                this.GetProcessType();
                this.attachments = false;
            },
            IsSave: function () {
                this.show = false;
                this.GetProcessType();
            },
            paymentSave: function () {
                this.payment = false;
                this.GetPaymentVoucher();
                this.GetProcessType();
            },
            expenseSave: function () {
                this.expense = false;
                this.bill = false;
                this.GetExpenseVoucher();
                this.GetProcessType();
            },
            GetPaymentVoucher: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Purchase/PurchaseOrderPaymentList?id=' + this.purchase.id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.purchase.paymentVoucher = response.data;
                        }
                    });
            },
            GetExpenseVoucher: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Purchase/PurchaseOrderExpensePaymentList?id=' + this.purchase.id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.purchase.purchaseOrderExpenses = response.data;
                        }
                    });
            },
            GetAttachment: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Purchase/PurchaseOrderAttachmentList?id=' + this.purchase.id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.purchase.purchaseAttachments = response.data;
                        }
                    });
            },
            GetProcessType: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Purchase/PurchaseOrderActionList?id=' + this.purchase.id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.purchase.actionProcess = response.data;
                        }
                    });
            },
            ChangeSupplier: function () {
                this.supplierRender++;
            },
            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.purchase.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addproduct');
                    }
                    else {
                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text:(this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },

            DownloadAttachment(path) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var ext = path.split('.')[1];
                root.$https.get('/Contact/DownloadFile?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'file.' + ext);
                        document.body.appendChild(link);
                        link.click();
                    });
            },

            uploadImage() {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var file = null;

                file = this.$refs.imgupload1.files;

                var fileData = new FormData();
                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                }
                root.$https.post('/Company/UploadFilesAsync', fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            root.purchase.path = response.data;

                        }
                    },
                        function () {
                            this.loading = false;
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        });
            },

            AutoIncrementCode: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/Purchase/PurchaseOrderAutoGenerateNo", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.purchase.registrationNo = response.data;
                        }
                    });
            },
            SavePurchaseItems: function (purchaseOrderItems) {
                this.purchase.purchaseOrderItems = purchaseOrderItems;
                this.getTotalAmount();
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
                    .post('/Purchase/SavePurchaseOrderInformation', root.purchase, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                            root.loading = false
                            root.info = response.data.bpi

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            }).then(function (ok) {
                                if (ok != null) {
                                    root.$router.go('addpurchaseorder');
                                }
                            });
                        }
                        else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                            root.loading = false
                            root.info = response.data.bpi

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            }).then(function (ok) {
                                if (ok != null) {
                                    root.$router.push("purchaseorder");
                                }
                            });
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: response.data.message.isAddUpdate,
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                icon: 'error',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
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

            goToPurchase: function () {
                this.$router.push({
                    path: '/purchaseorder',
                    query: {
                        data: 'purchaseorders'
                    }
                })
            },
            ViewPaymentVoucher: function (id, expense) {
                var root = this;
                
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Purchase/PurchaseOrderPaymentDetail?Id=' + id + '&expense=' + expense, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$https.get('/PaymentVoucher/PaymentVoucherDetails?Id=' + response.data.paymentVoucherId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                            if (response.data != null) {
                                root.paymentview = response.data;
                                if (expense) {
                                    root.isExpenseview = true;
                                }
                                else {
                                    root.isPaymentview = true;
                                }
                            }
                        });
                    }
                });
            },
            paymentView: function () {
                this.isPaymentview = false;
                this.isExpenseview = false;
            },

        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            if (this.$route.query.data != undefined) {

                this.purchase = this.$route.query.data;
                this.advanceAccountId = this.$route.query.data.advanceAccountId;
                this.purchase.date = moment(this.purchase.date).format("LLL");
                this.action.purchaseOrderId = this.purchase.id;
                this.attachment = true;
                this.rander++;
                this.rendered++;
            }
        },
        mounted: function () {
            this.language = this.$i18n.locale;
            this.currency = localStorage.getItem('currency');
            this.internationalPurchase = localStorage.getItem('InternationalPurchase');
            //this.versionAllow = localStorage.getItem('VersionAllow');
            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.options = ['Inclusive', 'Exclusive'];
            }
            else {
                this.options = ['شامل', 'غير شامل'];
            }
                this.raw = localStorage.getItem('IsProduction');
            





        },
    };
</script>
