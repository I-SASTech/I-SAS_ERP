<template>

    <div class="row" v-if="isValid('CanPurchaseInvoiceCosting')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">
                                    {{ $t('TheAddPurchaseCosting.PurchaseInvoiceCosting') }}
                                </h4>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Sale.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-lg-12">
                    <div class="card p-3">
                        <div>
                            <div class="row">
                                <div class="col-lg-5">
                                    <h5>{{ $t('TheAddPurchaseCosting.PurchaseInvoiceCosting') }} - {{purchase.registrationNo}}</h5>

                                </div>
                                <div class="col-lg-3">
                                    <div v-if="raw=='true'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        <label class="ml-4 mr-4">{{ $t('TheAddPurchaseCosting.RawProduct') }}  : </label>
                                        <toggle-button v-model="purchase.isRaw" @change="ChangeSupplier" class="ml-2 mt-2" color="#3178F6" />
                                    </div>
                                </div>
                                <div class="col-lg-4" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    <span>
                                        {{purchase.date}}
                                    </span>
                                </div>
                            </div>
                        </div>
                        <hr class="hr-dashed hr-menu mt-0" />
                        <div class="row">
                            <div class="col-sm-3 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <label>{{ $t('TheAddPurchaseCosting.Supplier') }} :<span class="text-danger"> *</span></label>
                                <div v-bind:class="{'has-danger': v$.purchase.supplierId.$error}" :key="supplierRender">
                                    <supplierdropdown v-model="v$.purchase.supplierId.$model" :disable="purchase.purchaseOrderId!=null && purchase.purchaseOrderId!=''" :modelValue="purchase.supplierId" :status="purchase.isRaw" :key="rander" />
                                    <span v-if="v$.purchase.supplierId.$error"
                                          class="error text-danger">
                                    </span>
                                </div>
                            </div>

                            <div class="col-sm-3 form-group " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <label v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'pt-2'">{{ $t('TheAddPurchaseCosting.SupplierInvoiceNumber') }}  :</label>
                                <div v-bind:class="{'has-danger': v$.purchase.invoiceNo.$error}">
                                    <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'"
                                           v-model="v$.purchase.invoiceNo.$model"
                                           disabled />
                                    <span v-if="v$.purchase.invoiceNo.$error"
                                          class="error text-danger">
                                    </span>
                                </div>
                            </div>

                            <div class="col-sm-3 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <label>{{ $t('TheAddPurchaseCosting.SupplierInvoiceDate') }}  :</label>
                                <div>
                                    <datepicker @pick="purchase.invoiceDate = $event" :disabled="true" v-bind:key="rander" />
                                </div>
                            </div>
                            <div class="col-sm-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <label>{{ $t('TheAddPurchaseCosting.WareHouse') }} :<span class="text-danger"> *</span></label>
                                <div>
                                    <warehouse-dropdown :disable="true" v-model="v$.purchase.wareHouseId.$model" />
                                    <span v-if="v$.purchase.wareHouseId.$error" class="error text-danger"> </span>
                                </div>
                            </div>

                            <div v-if="isValid('CreditPurchase') && (isValid('CanViewPostOrder')|| isValid('CanAddPurchaseOrder'))" class="col-sm-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <label>{{ $t('TheAddPurchaseCosting.PurchaseOrder') }}  :</label>
                                <purchase-order-dropdown :disable="true" @update:modelValue="GetPoData(purchase.purchaseOrderId)" v-model="purchase.purchaseOrderId" :modelValue="purchase.purchaseOrderId" />
                            </div>
                            <div class="col-sm-3" v-bind:class=" ($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <label>{{ $t('TheAddPurchaseCosting.TaxMethod') }} :<span class="text-danger"> *</span></label>
                                <multiselect :options="options" v-bind:disabled="true" v-model="purchase.taxMethod" :show-labels="false" v-bind:placeholder="$t('SelectMethod')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                </multiselect>
                            </div>
                            <div class="col-sm-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <label>{{ $t('TheAddPurchaseCosting.VAT%') }} :<span class="text-danger"> *</span></label>
                                <taxratedropdown v-model="purchase.taxRateId" :modelValue="purchase.taxRateId" :isDisable="true" v-bind:key="rander" />
                            </div>

                        </div>

                        <br />

                        <purchase-costing-item @update:modelValue="SavePurchaseItems" ref="childComponentRef" :purchaseItems="purchase.purchaseItems" :taxMethod="purchase.taxMethod" :taxRateId="purchase.taxRateId" :raw="purchase.isRaw" :isSerial="true" :purchaseOrderId="purchase.purchaseOrderId" :key="randerLineItem" />



                        <div class="accordion" role="tablist" v-if="internationalPurchase=='true' && purchase.purchaseOrderId!=null && purchase.purchaseOrderId!=''">
                            <b-card no-body class="mb-1" v-if="isValid('CanViewPIExpenses')">
                                <b-card-header header-tag="header" class="p-1" role="tab">
                                    <b-button block v-b-toggle.accordion-4 variant="primary">Expense</b-button>
                                </b-card-header>
                                <b-collapse id="accordion-4" accordion="my-accordion" role="tabpanel">
                                    <b-card-body>
                                        <purchaseorder-expense :show="expense" v-if="expense" @close="expenseSave" :newisPurchase="false" :isPurchasePostExpense="true" :purchaseOrderId="purchase.id" :formName="'AdvanceExpense'" />
                                        <div>
                                            <div class="row" v-if="purchase.isPurchasePost && purchase.id != '00000000-0000-0000-0000-000000000000'">
                                                <div class="col-md-12 text-right">
                                                    <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="expense=true" v-if="isValid('CanAddOrderExpense')"> Add Expense</a>
                                                </div>
                                            </div>
                                            <div class=" table-responsive">
                                                <table class="table ">
                                                    <thead class="thead-light table-hover">
                                                        <tr>
                                                            <th>#</th>
                                                            <th style="width:20%;">{{ $t('TheAddPurchaseCosting.Date') }} </th>
                                                            <th style="width:20%;">{{ $t('TheAddPurchaseCosting.VoucherNumber') }} </th>
                                                            <th class="text-center">{{ $t('TheAddPurchaseCosting.Amount') }} </th>
                                                            <th class="text-center">{{ $t('TheAddPurchaseCosting.PaymentMode') }} </th>
                                                            <th>{{ $t('TheAddPurchaseCosting.Description') }} </th>
                                                            <th> </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr v-for="(payment,index) in purchase.purchasePostExpense" v-bind:key="index">
                                                            <td>
                                                                {{index+1}}
                                                            </td>
                                                            <th>{{getDate(payment.date)}}</th>
                                                            <th>{{payment.voucherNumber}}</th>
                                                            <th class="text-center">
                                                                {{currency}} {{parseFloat(payment.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                            </th>
                                                            <th class="text-center"><span v-if="payment.paymentMode==0">Cash</span><span v-if="payment.paymentMode==1">Bank</span></th>
                                                            <th>{{payment.narration}}</th>
                                                            <th>
                                                                <!--<a href="javascript:void(0)" title="Remove" class="btn  btn-icon btn-danger btn-sm" v-on:click="removeExpense(payment.id)"><i class="fa fa-times"></i></a>-->
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

                        <div class="card-footer col-md-3 text-left" v-if="loading">
                            <loading v-model:active="loading"
                                     :can-cancel="true"
                                     :is-full-page="true"></loading>
                        </div>

                    </div>

                </div>
                <div v-if="!loading" class=" col-lg-12 invoice-btn-fixed-bottom">
                    <div class="row">
                        <div class=" col-md-12 button-group">
                            <button class="btn btn-outline-primary  me-2"
                                    v-on:click="savePurchasePost(true)"
                                    v-if="isValid('CanPurchaseInvoiceCosting')"
                                    >
                                <i class="far fa-save"></i> {{ $t('TheAddPurchaseCosting.SaveAndPost') }}
                            </button>
                            <button class="btn btn-danger"
                                    v-on:click="goToPurchase">
                                {{ $t('TheAddPurchaseCosting.Cancel') }}
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <purchaseInvoice :printDetails="printDetails" v-if="printDetails.length != 0" v-bind:key="printRender"></purchaseInvoice>
    </div>

    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    import moment from "moment";

    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        name: "AddPurchase",
        components: {
            Loading,
            Multiselect
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                branchId: '',
                expense: false,
                purchaseOrder: false,
                internationalPurchase: '',
                currency: '',
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    purchaseOrderId: "",
                    registrationNo: "",
                    supplierId: "",
                    invoiceNo: "",
                    isPurchaseReturn: false,
                    invoiceDate: "",
                    purchaseOrder: "",
                    wareHouseId: "",
                    purchaseItems: [],
                    isRaw: false,
                    isPurchasePost: false,
                    taxMethod: '',
                    taxRateId: "00000000-0000-0000-0000-000000000000",
                    actionProcess: [],
                    purchaseAttachments: [],
                    paymentVoucher: [],
                    purchasePostExpense: [],
                    partiallyPurchase: false,
                    autoPurchaseVoucher: false,
                    expenseToGst: false,
                    dueAmount: 0,
                },

                printId: '00000000-0000-0000-0000-000000000000',
                printDetails: [],
                options: [],
                loading: false,
                rander: 0,
                raw: '',
                printRender: 0,
                randerLineItem: 0,
                autoNumber: '',
                language: 'Nothing',
                supplierRender: 0,
                wareRander: 0,
                show: false,
                attachment: false,
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
                    wareHouseId: {},
                    purchaseItems: {  },
                },
            }
        },
        methods: {
            expenseSave: function () {
                this.expense = false;
                this.GetExpenseVoucher();
                /*this.GetProcessType();*/
            },
            GetExpenseVoucher: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('PurchasePost/PurchasePostExpensePaymentList?id=' + this.purchase.id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.purchase.purchasePostExpense = response.data;

                            var totalExpense = root.purchase.purchasePostExpense.reduce(
                                (total, expense) => total + expense.amount, 0);
                            var totalQuantity = root.purchase.purchaseItems.reduce(
                                (qty, prod) => qty + parseInt(prod.quantity), 0);

                            var unitExpense = totalExpense / totalQuantity;

                            root.purchase.purchaseItems.forEach(function (po) {
                                root.$refs.childComponentRef.updateExpense(unitExpense, po.rowId);

                            });
                        }
                    });
            },
            GetProcessType: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('PurchasePost/PurchasePostActionList?id=' + this.purchase.id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.purchase.actionProcess = response.data;
                        }
                    });
            },
            removeExpense: function (id) {
                this.purchase.purchasePostExpense = this.purchase.purchasePostExpense.filter((prod) => {
                    return prod.id != id;
                });
            },
            getDate: function (date) {
                if (date == null || date == undefined) {
                    return "";
                }
                else {
                    return moment(date).format('LLL');
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

            ChangeSupplier: function () {
                this.supplierRender++;
                this.randerLineItem++;
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
                            title: this.$t('TheAddPurchaseCosting.Error'),
                            text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },

            GetPoData: function (id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Purchase/PurchaseOrderDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            root.purchase.purchaseItems = [];
                            root.purchase.purchaseOrderId = response.data.id;
                            root.purchase.supplierId = response.data.supplierId;
                            root.purchase.taxMethod = response.data.taxMethod;
                            root.purchase.taxRateId = response.data.taxRateId;

                            if (root.internationalPurchase == 'true') {
                                root.purchase.actionProcess = response.data.actionProcess;
                                root.purchase.purchaseAttachments = response.data.purchaseAttachments;
                                root.purchase.paymentVoucher = response.data.paymentVoucher;
                                response.data.purchaseOrderExpenses.forEach(function (item) {
                                    if (item.amount - item.usedAmount > 0) {
                                        root.purchase.purchasePostExpense.push({
                                            id: item.id,
                                            date: item.date,
                                            bankCashAccountId: item.bankCashAccountId,
                                            contactAccountId: item.contactAccountId,
                                            vatAccountId: item.vatAccountId,
                                            taxRateId: item.taxRateId,
                                            taxMethod: item.taxMethod,
                                            voucherNumber: item.voucherNumber,
                                            narration: item.narration,
                                            chequeNumber: item.chequeNumber,
                                            amount: item.amount - item.usedAmount,
                                            paymentMode: item.paymentMode,
                                            paymentMethod: item.paymentMethod,
                                        });

                                    }
                                });

                            }


                            response.data.purchaseOrderItems.forEach(function (item) {
                                if (item.remainingQuantity > 0) {
                                    root.purchase.purchaseItems.push({
                                        rowId: item.id,
                                        id: item.id,
                                        batchNo: item.batchNo,
                                        discount: item.discount,
                                        expiryDate: item.expiryDate,
                                        isExpire: item.isExpire,
                                        fixDiscount: item.fixDiscount,
                                        product: item.product,
                                        basicUnit: item.unit == null ? '' : item.unit.name,
                                        productId: item.productId,
                                        purchaseId: item.purchaseId,
                                        quantity: item.remainingQuantity,
                                        highQty: item.highQty,
                                        unitPerPack: item.unitPerPack,
                                        taxMethod: item.taxMethod,
                                        taxRateId: item.taxRateId,
                                        serial: item.product.serial,
                                        serialNo: item.serialNo,
                                        guarantee: item.product.guarantee,
                                        guaranteeDate: item.guaranteeDate,
                                        unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                    });

                                }
                            });
                            root.rander++;
                            root.randerLineItem++;
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            },
            AutoIncrementCode: function () {
                //eslint-disable-line
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                root.$https
                    .get("/Purchase/PurchaseAutoGenerateNo", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            if (root.purchase.id == '00000000-0000-0000-0000-000000000000') {
                                root.purchase.registrationNo = response.data.post;
                                root.autoNumber = response.data.draft;
                            }
                            else {
                                root.autoNumber = response.data.post;
                            }
                            //    root.purchase.registrationNo = response.data;
                        }
                    });
            },
            SavePurchaseItems: function (purchaseItems) {

                this.purchase.purchaseItems = purchaseItems;
                this.getTotalAmount();
            },
            getTotalAmount: function () {
                this.purchase.dueAmount = this.$refs.childComponentRef.getTotalAmount();
            },
            savePurchasePost: function (invoiceType) {

                this.loading = true;
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                if (this.purchase.invoiceDate == "Invalid date") {
                    this.purchase.invoiceDate = "";
                }

                var purchasePost = {
                    id: this.purchase.id,
                    date: this.purchase.date,
                    registrationNo: this.purchase.registrationNo,
                    supplierId: this.purchase.supplierId,
                    invoiceNo: this.purchase.invoiceNo,
                    invoiceDate: this.purchase.invoiceDate,
                    purchaseOrder: this.purchase.purchaseOrder,
                    wareHouseId: this.purchase.wareHouseId,
                    purchaseOrderId: this.purchase.purchaseOrderId,
                    taxRateId: this.purchase.taxRateId,
                    taxMethod: this.purchase.taxMethod,
                    isRaw: this.purchase.isRaw,
                    purchasePostItems: this.purchase.purchaseItems,
                    actionProcess: this.purchase.actionProcess,
                    purchaseAttachments: this.purchase.purchaseAttachments,
                    paymentVoucher: this.purchase.paymentVoucher,
                    purchasePostExpense: this.purchase.purchasePostExpense,
                    isPurchasePost: invoiceType,
                    partiallyPurchase: this.purchase.partiallyPurchase,
                    autoPurchaseVoucher: this.purchase.autoPurchaseVoucher,
                    expenseToGst: this.purchase.expenseToGst,
                    dueAmount: this.purchase.dueAmount,
                    branchId: this.branchId,
                };

                this.$https
                    .post("/PurchasePost/SavePurchasePostCosting", purchasePost, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then((response) => {

                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                            root.loading = false
                            root.info = response.data.bpi

                            root.$swal({
                                title: this.$t('TheAddPurchaseCosting.Saved'),
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            });

                            root.$router.push('/purchase')
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

                            });
                            root.$router.push('/purchase')
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
                    .catch((error) => {
                        console.log(error);
                        root.$swal.fire({
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            text: error.response.data,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                        root.loading = false;
                    })
                    .finally(() => (root.loading = false));
            },
            savePurchasePostPrint: function (isPurchasePost) {
                this.loading = true;
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                var purchasePost = {
                    id: this.purchase.id,
                    date: this.purchase.date,
                    registrationNo: this.purchase.registrationNo,
                    supplierId: this.purchase.supplierId,
                    invoiceNo: this.purchase.invoiceNo,
                    invoiceDate: this.purchase.invoiceDate,
                    purchaseOrder: this.purchase.purchaseOrder,
                    wareHouseId: this.purchase.wareHouseId,
                    purchasePostItems: this.purchase.purchaseItems,
                    isPurchasePost: isPurchasePost,
                };
                this.$https
                    .post("/PurchasePost/SavePurchasePostInformation", purchasePost, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then((response) => {
                        root.loading = false;
                        root.$swal.fire({
                            icon: "success",
                            title: "Saved Successfully",
                            showConfirmButton: false,

                            timer: 800,
                            timerProgressBar: true,
                        });
                        root.printId = response.data.id;
                    }).then(function () {
                        root.$https
                            .get("/PurchasePost/PurchasePostDetail?Id=" + root.printId, {
                                headers: { Authorization: `Bearer ${token}` },
                            })
                            .then(function (response) {
                                if (response.data != null) {

                                    root.printDetails = response.data;
                                    root.printRender++;
                                }
                            });
                    })
                    .catch((error) => {
                        console.log(error);
                        root.$swal.fire({
                            icon: "error",
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            text: error,
                        });

                        root.loading = false;
                    })
                    .finally(() => (root.loading = false));
            },

            goToPurchase: function () {
                this.$router.push('/purchase');
            },
        },
        created: function () {
            debugger; //eslint-disable-line
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            if (this.$route.query.data != undefined) {
                var detail = this.$route.query.data;
                this.purchase.date = detail.date;
                this.purchase.discountAmount = detail.discountAmount;
                this.purchase.id = detail.id;
                this.purchase.invoiceDate = detail.invoiceDate;
                this.purchase.invoiceFixDiscount = detail.invoiceFixDiscount;
                this.purchase.invoiceNo = detail.invoiceNo;
                this.purchase.isPurchasePost = detail.isPurchasePost;
                this.purchase.isPurchaseReturn = detail.isPurchaseReturn;
                this.purchase.isRaw = detail.isRaw;
                this.purchase.netAmount = detail.netAmount;
                this.purchase.purchaseInvoiceId = detail.purchaseInvoiceId;
                this.purchase.purchaseOrderId = detail.purchaseOrderId;
                this.purchase.purchaseOrderNo = detail.purchaseOrderNo;
                this.purchase.registrationNo = detail.registrationNo;
                this.purchase.supplierId = detail.supplierId;
                this.purchase.taxMethod = detail.taxMethod;
                this.purchase.taxRateId = detail.taxRateId;
                this.purchase.wareHouseId = detail.wareHouseId;
                this.purchase.purchaseInvoiceActions = detail.purchaseInvoiceActions;
                this.purchase.purchaseInvoiceAttachments = detail.purchaseInvoiceAttachments;
                this.purchase.purchasePostExpense = detail.purchasePostExpenses;

                var totalExpense = detail.purchasePostExpenses.reduce(
                    (total, expense) => total + expense.amount, 0);
                var totalQuantity = detail.purchasePostItems.reduce(
                    (qty, prod) => qty + parseInt(prod.quantity), 0);

                var unitExpense = totalExpense / totalQuantity;

                detail.purchasePostItems.forEach(function (item) {
                    root.purchase.purchaseItems.push({
                        rowId: item.id,
                        id: item.id,
                        categoryName: item.categoryName,
                        batchNo: item.batchNo,
                        discount: item.discount,
                        expiryDate: item.expiryDate,
                        isExpire: item.isExpire,
                        fixDiscount: item.fixDiscount,
                        product: item.product,
                        basicUnit: item.unit == null ? '' : item.unit.name,
                        productId: item.productId,
                        purchaseId: item.purchaseId,
                        quantity: item.quantity,
                        highQty: item.highQty,
                        receiveQuantity: item.receiveQuantity,
                        unitPerPack: item.unitPerPack,
                        unitExpense: item.unitPrice + unitExpense,
                        taxMethod: item.taxMethod,
                        taxRateId: item.taxRateId,
                        serial: item.product.serial,
                        serialNo: item.serialNo,
                        guarantee: item.product.guarantee,
                        guaranteeDate: item.guaranteeDate,
                        unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                    });
                });

                this.rander++;
            }
            else {
                this.purchase.wareHouseId = localStorage.getItem('WareHouseId');
            }
        },
        mounted: function () {
            this.branchId = localStorage.getItem('BranchId');
            this.language = this.$i18n.locale;
            this.currency = localStorage.getItem('Currency');
            this.internationalPurchase = localStorage.getItem('InternationalPurchase');
            this.purchase.partiallyPurchase = localStorage.getItem('PartiallyPurchase') == 'true' ? true : false;
            this.purchase.autoPurchaseVoucher = localStorage.getItem('AutoPurchaseVoucher') == 'true' ? true : false;
            this.purchase.expenseToGst = localStorage.getItem('ExpenseToGst') == 'true' ? true : false;
            this.purchaseOrder = localStorage.getItem('PurchaseOrder') == 'true' ? true : false;

            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.options = ['Inclusive', 'Exclusive'];
            }
            else {
                this.options = ['شامل', 'غير شامل'];
            }
            this.raw = localStorage.getItem('IsProduction');

            this.AutoIncrementCode();
            this.purchase.date = moment().format("LLL");
        },
    };
</script>
