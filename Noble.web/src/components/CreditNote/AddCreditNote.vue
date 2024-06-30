<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="formName == 'CreditNote'" class="page-title">
                                    {{ $t('AddCreditNote.AddCreditNote') }} - {{purchase.code}}
                                </h4>
                                <h4 v-else class="page-title">
                                    {{ $t('AddCreditNote.AddDebitNote') }} - {{purchase.code}}
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
            <hr class="hr-dashed hr-menu mt-0" />

            <div class="row">
                <div class="col-lg-6">
                    <div v-if="formName == 'CreditNote'" class="row form-group" v-bind:key="randerCustomer">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddSaleOrder.Customer') }} :
                                <span class="text-danger">*</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <customerdropdown v-model="purchase.contactId"
                                              :paymentTerm="purchase.paymentMethod"  @update:modelValue="getAddress" ref="CustomerDropdown"
                                              v-bind:modelValue="purchase.contactId" :key="customerRender" />
                        </div>
                    </div>

                    <div v-if="formName == 'DebitNote'" class="row form-group" v-bind:key="randerCustomer">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddPurchaseOrder.Supplier')}} :
                                <span class="text-danger">*</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <supplierdropdown v-model="purchase.contactId"
                                              v-bind:modelValue="purchase.contactId" :key="customerRender" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline ">
                                {{$t('AddSaleOrder.Description')}} # <span class="text-danger">*</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <textarea class="form-control " rows="3" v-model="purchase.narration" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">

                        </label>
                        <div class="inline-fields col-lg-8">
                            <div class="checkbox form-check-inline">
                                <input type="checkbox" id="inlineCheckbox10" v-model="purchase.isItemDescription">
                                <label for="inlineCheckbox10"> Item Description </label>
                            </div>
                            <div class="checkbox form-check-inline">
                                <input type="checkbox" id="inlineCheckbox11" v-model="purchase.isInventoryTransaction">
                                <label for="inlineCheckbox11"> Inventory Transaction </label>
                            </div>
                        </div>
                    </div>

                    <div class="row form-group" v-if="!purchase.isItemDescription">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline ">
                                {{$t('SaleOrderItem.Total')}} 
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" v-model="purchase.grossAmount" @update:modelValue="getAmount()" />
                        </div>
                    </div>
                    <div class="row form-group" v-if="!purchase.isItemDescription">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline ">
                                {{$t('SaleOrderItem.TotalVAT')}} 
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" disabled v-model="purchase.vatAmount" />
                        </div>
                    </div>
                    <div class="row form-group" v-if="!purchase.isItemDescription">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline ">
                                {{ $t('SaleOrderItem.TotalDue') }}({{currency}})
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" disabled v-model="purchase.amount" />
                        </div>
                    </div>
                </div>

                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSaleOrder.Date') }}</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="purchase.date" />
                        </div>
                    </div>

                    <div class="row form-group"
                         v-if="saleDefaultVat == 'DefaultVatHead' || saleDefaultVat == 'DefaultVatHeadItem'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddPurchase.TaxMethod') }}
                                :<span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect v-if="($i18n.locale == 'en' || isLeftToRight())" @update:modelValue="getAmount()"
                                         :options="['Inclusive', 'Exclusive']" v-bind:disabled="purchase.saleOrderItems.length > 0"
                                         @click="purchase.isFixed = false" v-model="purchase.taxMethod" :show-labels="false"
                                         v-bind:placeholder="$t('AddStockValue.SelectMethod')"
                                         v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                            <multiselect v-else :options="['شامل', 'غير شامل']" @update:modelValue="getAmount()"
                                         v-bind:disabled="purchase.saleOrderItems.length > 0" v-model="purchase.taxMethod"
                                         @select="purchase.isFixed = false" :show-labels="false"
                                         v-bind:placeholder="$t('AddStockValue.SelectMethod')"
                                         v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            </multiselect>
                        </div>
                    </div>

                    <div class="row form-group"
                         v-if="saleDefaultVat == 'DefaultVatHead' || saleDefaultVat == 'DefaultVatHeadItem'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddPurchase.VAT%') }} :<span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <taxratedropdown v-model="purchase.taxRateId" :modelValue="purchase.taxRateId" @update:modelValue="getAmount()"
                                             :isDisable="purchase.saleOrderItems.length > 0 ? true : false" :key="customerRender" />
                        </div>
                    </div>
                    <div class="row form-group" v-if="purchase.isItemDescription">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddSaleOrder.WareHouse') }}
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <warehouse-dropdown v-model="purchase.wareHouseId" />
                        </div>
                    </div>

                    <div class="row form-group" v-if="purchase.isItemDescription && formName == 'CreditNote'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('Sale.SaleInvoice') }}
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <sale-invoice-dropdown v-model="purchase.saleId" :isService="false" @update:modelValue="GetSaleDetail" />
                        </div>
                    </div>
                    <div class="row form-group" v-if="purchase.isItemDescription && formName == 'DebitNote'">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('Purchase.PurchaseInvoice') }}
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <purchaseinvoicedropdown v-model="purchase.purchaseInvoiceId" @update:modelValue="GetPurchaseDetail" />
                        </div>
                    </div>

                </div>

                <creditnoteitem @update:modelValue="SavePurchaseItems" @summary="SaveSummary" ref="childComponentRef" :key="rander"
                                :wareHouseId="purchase.wareHouseId" :taxMethod="purchase.taxMethod" :taxRateId="purchase.taxRateId"
                                @discountChanging="updateiscountChanging" :adjustmentProp="purchase.discount"
                                :adjustmentSignProp="adjustmentSignProp" :isDiscountOnTransaction="purchase.isDiscountOnTransaction"
                                :transactionLevelDiscountProp="purchase.transactionLevelDiscount" :isfixed="purchase.isFixed"
                                :isbeforetax="purchase.isBeforeTax" v-if="purchase.isItemDescription" />

                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div class="button-items" v-if="purchase.id === '00000000-0000-0000-0000-000000000000'">
                        <div class="btn-group" >
                            <button v-on:click="savePurchase('Approved')"
                                    v-bind:disabled="v$.purchase.$invalid"
                                    type="button" class="btn btn-outline-primary me-0">
                                {{ $t('AddSaleOrder.SaveAsPost') }}
                            </button>
                            <button v-bind:disabled="v$.purchase.$invalid"
                                    type="button" class="btn btn-outline-primary dropdown-toggle dropdown-toggle-split"
                                    data-bs-toggle="dropdown" aria-expanded="false">
                                <span class="sr-only">Dropdown</span> <i class="mdi mdi-chevron-down"></i>
                            </button>
                            <div class="dropdown-menu">
                                <a v-on:click="savePurchase('Approved', true)"
                                   class="dropdown-item" href="javascript:void(0);">
                                    {{$t('AddSaleOrder.SaveasPostandPrint')}}
                                </a>
                            </div>
                        </div>

                        <button class="btn btn-danger  mr-2" v-on:click="goToPurchase">
                            {{ $t('AddSaleOrder.Cancel') }}
                        </button>
                    </div>
                    <div class="button-items" v-else>

                        <div class="btn-group">
                            <button v-on:click="savePurchase('Approved')"
                                    v-bind:disabled="v$.purchase.$invalid"
                                    type="button" class="btn btn-outline-primary me-0">
                                {{ $t('AddSaleOrder.UpdateAsPost') }}
                            </button>
                            <button v-bind:disabled="v$.purchase.$invalid"
                                    type="button" class="btn btn-outline-primary dropdown-toggle dropdown-toggle-split"
                                    data-bs-toggle="dropdown" aria-expanded="false">
                                <span class="sr-only">Dropdown</span> <i class="mdi mdi-chevron-down"></i>
                            </button>
                            <div class="dropdown-menu">
                                <a v-on:click="savePurchase('Approved', true)"
                                   class="dropdown-item" href="javascript:void(0);">
                                    {{$t('AddSaleOrder.UpdateAsPostandPrint')}}
                                </a>
                            </div>
                        </div>

                        <button class="btn btn-danger  mr-2" v-on:click="goToPurchase">
                            {{ $t('AddSaleOrder.Cancel') }}
                        </button>
                    </div>
                </div>



                <div class="col-lg-12 mt-4 mb-5">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group ps-3">
                                        <div class="font-xs mb-1">{{ $t('AddSaleOrder.Attachment') }}</div>

                                        <button v-on:click="Attachment()" type="button"
                                                class="btn btn-light btn-square btn-outline-dashed mb-1">
                                            <i class="fas fa-cloud-upload-alt"></i> {{ $t('AddSaleOrder.Attachment') }}
                                        </button>

                                        <div>
                                            <small class="text-muted">
                                                {{ $t('AddSaleOrder.FileSize') }}
                                            </small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <bulk-attachment :attachmentList="purchase.attachmentList" :show="show" v-if="show" @close="attachmentSave" />

        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
   
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import { required } from '@vuelidate/validators'; 
    import useVuelidate from '@vuelidate/core';
    import Multiselect from 'vue-multiselect';


    //import Vue3Barcode from 'vue3-barcode'
    export default {
        mixins: [clickMixin],
        props: ['formName'],
        components: {
            Multiselect,
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                discountTypeOption: 'At Line Item Level',
                adjustmentSignProp: '+',
                currency: '',

                custemerOldId: '',
                customerAccountId: '',
                isRaw: '',
                SaleOrder: 'SaleOrder',
                payment: false,
                IsService: false,
                show: false,
                saleOrder: false,
                rander: 0,
                render: 0,
                customerRender: 0,
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    customerAddress: "",
                    mobile: "",
                    code: "",
                    contactId: "",
                    narration: '',
                    isCreditNote: false,
                    isItemDescription: true,
                    isInventoryTransaction: false,
                    terminalId: '',
                    invoicePrefix: '',
                    saleOrderItems: [],
                    attachmentList: [],
                    path: '',
                    taxMethod: '',
                    taxRateId: '',
                    vatAmount: 0,
                    amount: 0,
                    grossAmount: 0,
                    wareHouseId: '',
                    saleId: '',
                    purchaseInvoiceId: '',
                    branchId: '',

                    isBeforeTax: true,
                },
                loading: false,
                randerCustomer: 0,

                printDetails: [],

                printRender: 0,
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                isFifo: false,
                openBatch: 0,
                saleDefaultVat: ''

            };
        },




        validations() {
          return{
              purchase: {
                date: { required },
                code: { required },
                contactId: { required },
                wareHouseId: { required },
            },
          }
        },
        watch: {
            formName: function () {
                this.AutoIncrementCode();
            }
        },
        methods: {
            getAmount: function () {
                
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                var vat = 0;
                root.$https.get("/Product/TaxRateList", { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            vat = response.data.taxRates.find((value) => value.id == root.purchase.taxRateId)?.rate;

                            root.purchase.vatAmount = parseFloat((root.purchase.taxMethod == 'Inclusive' || root.purchase.taxMethod == 'شامل') ? root.purchase.grossAmount * vat / (vat + 100) : root.purchase.grossAmount * vat / 100).toFixed(2);
                            root.purchase.amount = (root.purchase.taxMethod == 'Inclusive' || root.purchase.taxMethod == 'شامل') ? root.purchase.grossAmount : (parseFloat(root.purchase.grossAmount) + parseFloat(root.purchase.vatAmount)).toFixed(2);
                        }
                    });

                
            },

            SaveSummary: function (summary) {
                this.purchase.grossAmount = summary.withDisc;
                this.purchase.vatAmount = summary.vat;
                this.purchase.amount = summary.withVat;
            },

            GotoPage: function (link) {
                this.$router.push({ path: link });
            },


            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function (attachment) {
                this.purchase.attachmentList = attachment;
                this.show = false;
            },

            getAddress: function () {
                if (this.custemerOldId != this.purchase.contactId) {
                    this.purchase.customerAddress = this.$refs.CustomerDropdown.GetCustomerAddress().address;
                    this.purchase.mobile = this.$refs.CustomerDropdown.GetCustomerAddress().mobile;
                }
                this.custemerOldId = this.purchase.contactId;

            },

            RanderCustomer: function () {
                this.randerCustomer++;
            },

            getDate: function (date) {
                if (date == null || date == undefined) {
                    return "";
                }
                else {
                    return moment(date).format('LLL');
                }
            },

            GetSaleDetail: function (id) {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Sale/SaleDetail?id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.loading = false;
                            root.purchase.date = moment(response.data.date).format('llll');
                            root.purchase.contactId = response.data.customerId;
                            root.purchase.taxMethod = response.data.taxMethod;
                            root.purchase.taxRateId = response.data.taxRateId;
                            root.purchase.wareHouseId = response.data.wareHouseId;
                            root.purchase.narration = response.data.note;
                            
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                response.data.saleItems.forEach(function (x) {
                                    x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                    x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                response.data.saleItems.forEach(function (x) {
                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }

                            root.$refs.childComponentRef.EmtypurchaseProductsList();
                            response.data.saleItems.forEach(function (so) {
                                root.$refs.childComponentRef.addProduct(so.productId, so.product, so, true);
                            });
                            root.customerRender++;
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            },

            GetPurchaseDetail: function (id) {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;

                root.$https.get('/PurchasePost/PurchasePostDetail?id=' + id + '&isMultiUnit=' + isMultiUnit, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.loading = false;
                            root.purchase.date = moment(response.data.date).format('llll');
                            root.purchase.contactId = response.data.supplierId;
                            root.purchase.taxMethod = response.data.taxMethod;
                            root.purchase.taxRateId = response.data.taxRateId;
                            root.purchase.wareHouseId = response.data.wareHouseId;

                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                                response.data.purchasePostItems.forEach(function (x) {
                                    x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                    x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }
                            if (localStorage.getItem('IsMultiUnit') == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                                response.data.purchasePostItems.forEach(function (x) {
                                    x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                    x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                                    x.unitPerPack = x.product.unitPerPack;
                                });
                            }

                            root.$refs.childComponentRef.EmtypurchaseProductsList();
                            response.data.purchasePostItems.forEach(function (so) {
                                root.$refs.childComponentRef.addProduct(so.productId, so.product, so, true);
                            });
                            root.customerRender++;
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            },


            AutoIncrementCode: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                
                var isCreditNote = false;
                if (this.formName == 'CreditNote') {
                    isCreditNote = true;
                }
                else {
                    isCreditNote = false;
                }

                root.$https
                    .get("/PaymentVoucher/CreditNoteAutoGenerateNo?terminalId=" + localStorage.getItem('TerminalId') + '&invoicePrefix=' + localStorage.getItem('InvoicePrefix') + '&isCreditNote=' + isCreditNote + '&branchId=' + localStorage.getItem('BranchId'), {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.purchase.code = response.data;
                        }
                    });
            },

            SavePurchaseItems: function (saleOrderItems, discount, adjustmentSignProp, transactionLevelDiscount) {

                this.purchase.saleOrderItems = saleOrderItems;
                this.purchase.discount = (discount == '' || discount == null) ? 0 : (adjustmentSignProp == '+' ? parseFloat(discount) : (-1) * parseFloat(discount))

                this.purchase.transactionLevelDiscount = (transactionLevelDiscount == '' || transactionLevelDiscount == null) ? 0 : parseFloat(transactionLevelDiscount)
            },

            updateiscountChanging: function (isFixed, isBeforeTax) {
                this.purchase.isFixed = isFixed
                this.purchase.isBeforeTax = isBeforeTax
            },

            savePurchase: function (status, print) {
                this.purchase.approvalStatus = status
                localStorage.setItem('active', status);

                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.purchase.saleOrderItems.forEach(function (x) {
                    x.highUnitPrice ? x.quantity = (x.highQty * x.unitPerPack) + x.quantity : x.quantity = x.totalPiece;
                });

                if (this.formName == 'CreditNote') {
                    this.purchase.isCreditNote = true;
                }
                else {
                    this.purchase.isCreditNote = false;
                }

                this.purchase.branchId = localStorage.getItem('BranchId');

                this.purchase.terminalId = localStorage.getItem('TerminalId');
                this.purchase.invoicePrefix = localStorage.getItem('InvoicePrefix');
                this.$https
                    .post('/PaymentVoucher/CreditNoteSave', root.purchase, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        root.loading = false
                        
                        if (print) {
                            root.PrintInvoice(response.data.id);

                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Data Saved Successfully!' : '!حفظ بنجاح',
                                type: 'success',
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,
                            }).then(function (response) {
                                if (response != undefined) {
                                    if (root.purchase.id == "00000000-0000-0000-0000-000000000000" && !root.purchase.isDuplicate) {
                                        root.$router.go('AddSaleOrder');

                                    } else {
                                        root.$router.push({
                                            path: '/SaleOrder',
                                            query: {
                                                data: 'AddSaleOrders' + this.formName,
                                                formName: this.formName,
                                            }
                                        })
                                    }
                                }
                            });
                        }


                    })
                    .catch(error => {
                        console.log(error)
                        if (localStorage.getItem('IsMultiUnit') == 'true') {
                            root.purchase.saleOrderItems.forEach(function (x) {

                                x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.unitPerPack));
                                x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.unitPerPack));

                            });
                        }
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
                    path: '/CreditNote',
                    query:{
                        formName: this.formName,
                    }
                })

            },
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + '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';

                if (path != null && path != '' && path != undefined) {
                    root.$https.get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        }
                    });
                }

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
                            root.GetInvoicePrintSetting();
                            root.getBase64Image(root.headerFooter.company.logoPath);
                        }
                    });
            },

            GetInvoicePrintSetting: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var branchId = '';
                branchId = localStorage.getItem('BranchId');
                if(branchId == null)
                {
                    branchId = '';
                }

                root.$https.get('/Sale/printSettingDetail?branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.headerFooter.footerEn = response.data.termsInEng;
                            root.headerFooter.footerAr = response.data.termsInAr;
                            root.headerFooter.isHeaderFooter = response.data.isHeaderFooter;
                            root.headerFooter.englishName = response.data.englishName;
                            root.headerFooter.arabicName = response.data.arabicName;
                            root.headerFooter.customerAddress = response.data.customerAddress;
                            root.headerFooter.customerVat = response.data.customerVat;
                            root.headerFooter.customerNumber = response.data.customerNumber;
                            root.headerFooter.cargoName = response.data.cargoName;
                            root.headerFooter.customerTelephone = response.data.customerTelephone;
                            root.headerFooter.itemPieceSize = response.data.itemPieceSize;
                            root.headerFooter.styleNo = response.data.styleNo;
                            root.headerFooter.blindPrint = response.data.blindPrint;
                            root.headerFooter.showBankAccount = response.data.showBankAccount;
                            root.headerFooter.bankAccount1 = response.data.bankAccount1;
                            root.headerFooter.bankAccount2 = response.data.bankAccount2;
                            root.headerFooter.bankIcon1 = response.data.bankIcon1;
                            root.headerFooter.bankIcon2 = response.data.bankIcon2;
                            root.headerFooter.customerNameEn = response.data.customerNameEn;
                            root.headerFooter.customerNameAr = response.data.customerNameAr;

                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            },

            PrintInvoice: function (value) {
                this.GetHeaderDetail();
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Purchase/SaleOrderDetail?id=" + value, {
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
                            root.printRender++;
                            // root.$router.go('AddSaleOrder');
                        }
                    });
            },

            GetUserDefineFlow: function () {
                var root = this;
                if (localStorage.getItem('quotationToSaleOrder') == undefined) {
                    root.$https.get('/Company/UserDefineFlowEdit?companyId=' + localStorage.getItem('CompanyID'), { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
                        .then(function (response) {
                            if (response.data != null) {
                                localStorage.setItem('quotationToSaleOrder', response.data.quotationToSaleOrder);
                                localStorage.setItem('quotationToSaleInvoice', response.data.quotationToSaleInvoice);
                                localStorage.setItem('partiallyQuotation', response.data.partiallyQuotation);
                                localStorage.setItem('partiallySaleOrder', response.data.partiallySaleOrder);

                                root.saleOrder = localStorage.getItem('quotationToSaleOrder') == 'true' ? true : false;
                            }
                            else {
                                console.log("error: something wrong from db.");
                            }
                        },
                            function (error) {
                                console.log(error);
                            });
                }
                else {
                    this.saleOrder = localStorage.getItem('quotationToSaleOrder') == 'true' ? true : false;

                }


            }

        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
            root.$route.query.data = this.$store.isGetEdit;
            }
            this.IsService = localStorage.getItem('IsService') == 'true' ? true : false;
            this.$emit('update:modelValue', this.$route.name);
            this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');
            this.currency = localStorage.getItem('currency');

            if (this.$route.query.data != undefined) {

                if (this.$route.query.data.isDuplicate) {
                    this.$route.query.data.id = '00000000-0000-0000-0000-000000000000';
                    this.purchase = this.$route.query.data;
                    this.purchase.isClose = false;
                    this.AutoIncrementCode();
                    this.purchase.date = moment().format('llll');
                }
                else {
                    this.purchase = this.$route.query.data;
                    this.purchase.date = moment(this.purchase.date).format('llll');
                }
                this.customerAccountId = this.$route.query.data.customerAccountId;
                this.custemerOldId = this.$route.query.data.contactId;
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
                this.attachment = true;
                this.rander++;
                this.render++;
            }
            else {
                this.purchase.wareHouseId = localStorage.getItem('WareHouseId');
                this.purchase.taxRateId = localStorage.getItem('TaxRateId');
                this.purchase.taxMethod = localStorage.getItem('taxMethod');

                this.AutoIncrementCode();
                this.purchase.date = moment().format('llll');
            }
            this.discountTypeOption = this.purchase.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'
            //this.discountTypeOption = this.purchase.isDiscountOnTransaction ? 'At Transaction Level' : 'At Line Item Level'

            this.adjustmentSignProp = this.purchase.discount >= 0 ? '+' : '-'

            this.GetUserDefineFlow();
        },
        mounted: function () {


            this.isRaw = localStorage.getItem('IsProduction');
            this.IsService = localStorage.getItem('IsService') == 'true' ? true : false;


            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            var batch = localStorage.getItem('openBatch')
            if (batch != undefined && batch != null && batch != "") {
                this.openBatch = batch
            }
            else {
                this.openBatch = 1
            }
            this.purchase.barCode = Math.floor(Math.random() * 100000000000);
        },
    };
</script>
