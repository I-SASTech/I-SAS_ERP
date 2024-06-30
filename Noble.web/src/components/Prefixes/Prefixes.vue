<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Prefixies.Prefixies') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Prefixies.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Prefixies.Prefixies') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card col-md-12">

                <div class="card-body">
                    <div class="row">
                        
                        <div class="col-lg-6" v-if="isValid('CanViewHoldInvoices') || isValid('CanViewPaidInvoices') || isValid('CanViewCreditInvoices') ">
                            <div class="row form-group">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">{{ $t('Prefixies.SaleInvoicePrefix') }}:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.sInvoice.$model">
                                    <span v-if="v$.prefix.sInvoice.$error" class="error">
                                        <span v-if="!v$.prefix.sInvoice.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CanViewSaleReturn')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">{{ $t('Prefixies.SaleReturnPrefix') }}:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.sReturn.$model">
                                    <span v-if="v$.prefix.sReturn.$error" class="error">
                                        <span v-if="!v$.prefix.sReturn.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CanViewSaleOrder')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">{{ $t('Prefixies.SaleOrderPrefix') }}:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.sOrder.$model">
                                    <span v-if="v$.prefix.sOrder.$error" class="error">
                                        <span v-if="!v$.prefix.sOrder.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CanViewQuotation')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">{{ $t('Prefixies.SaleQuatationPrefix') }}:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.sQutation.$model">
                                    <span v-if="v$.prefix.sQutation.$error" class="error">
                                        <span v-if="!v$.prefix.sQutation.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CanViewEmployeeRegistration')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">Employee :</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.employee.$model">
                                    <span v-if="v$.prefix.employee.$error" class="error">
                                        <span v-if="!v$.prefix.employee.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CanViewProforma')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">Perfoma Invoice :</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.proformaSaleInvoice.$model">
                                    <span v-if="v$.prefix.proformaSaleInvoice.$error" class="error">
                                        <span v-if="!v$.prefix.proformaSaleInvoice.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CreditNote')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">Credit Note :</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.creditNote.$model">
                                    <span v-if="v$.prefix.creditNote.$error" class="error">
                                        <span v-if="!v$.prefix.creditNote.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CanViewCPR')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">Customer Pay Receipt:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.customerPayReceipt.$model">
                                    <span v-if="v$.prefix.customerPayReceipt.$error" class="error">
                                        <span v-if="!v$.prefix.customerPayReceipt.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CanViewAR')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">Advance Sale Receipt:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.advanceSaleReceipt.$model">
                                    <span v-if="v$.prefix.advanceSaleReceipt.$error" class="error">
                                        <span v-if="!v$.prefix.advanceSaleReceipt.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="row form-group" v-if="isValid('CanViewPurchaseDraft') || isValid('CanViewPurchasePost')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">{{ $t('Prefixies.PurchaseInvoicePrefix') }}:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.pInvoice.$model">
                                    <span v-if="v$.prefix.pInvoice.$error" class="error">
                                        <span v-if="!v$.prefix.pInvoice.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CanViewPurchaseReturn')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">{{ $t('Prefixies.PurchaseReturnPrefix') }}:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.pReturn.$model">
                                    <span v-if="v$.prefix.pReturn.$error" class="error">
                                        <span v-if="!v$.prefix.pReturn.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CanViewDraftOrder') || isValid('CanViewPostOrder') || isValid('CanViewInProcessOrder')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">{{ $t('Prefixies.PurchaseOrderPrefix') }}:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.pOrder.$model">
                                    <span v-if="v$.prefix.pOrder.$error" class="error">
                                        <span v-if="!v$.prefix.pOrder.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CanViewSaleOrderTracking')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">{{ $t('Prefixies.SaleOrderTrackingPrefix') }}:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.sOrdeTracking.$model">
                                    <span v-if="v$.prefix.pOrder.$error" class="error">
                                        <span v-if="!v$.prefix.pOrder.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CreateDocument')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">{{ $t('Prefixies.ReceiptInvoice') }}:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.receiptInvoice.$model">
                                    <span v-if="v$.prefix.receiptInvoice.$error" class="error">
                                        <span v-if="!v$.prefix.receiptInvoice.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>                           
                            <div class="row form-group" v-if="isValid('CreateDocument')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">{{ $t('Prefixies.GlobalInvoice') }}:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.globalInvoice.$model">
                                    <span v-if="v$.prefix.globalInvoice.$error" class="error">
                                        <span v-if="!v$.prefix.GlobalInvoice.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('DebitNote')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">Debit Note:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.debitNote.$model">
                                    <span v-if="v$.prefix.debitNote.$error" class="error">
                                        <span v-if="!v$.prefix.debitNote.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CanViewSPR')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">Supplier Pay Receipt:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.supplierPayReceipt.$model">
                                    <span v-if="v$.prefix.supplierPayReceipt.$error" class="error">
                                        <span v-if="!v$.prefix.supplierPayReceipt.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group" v-if="isValid('CanViewAR')">
                                <label class="col-form-label col-lg-4">
                                    <span class="tooltip-container text-dashed-underline ">Advance Purchase Receipt:</span>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <input class="form-control" type="text" v-model="v$.prefix.advancePurchaseReceipt.$model">
                                    <span v-if="v$.prefix.advancePurchaseReceipt.$error" class="error">
                                        <span v-if="!v$.prefix.advancePurchaseReceipt.maxLength">{{ $t('Prefixies.maximumLengthmustbe03') }}</span>
                                    </span>
                                </div>
                            </div>
                           
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <button type="button" class="btn btn-outline-primary ms-5 me-2" :disabled="v$.prefix.$invalid" v-on:click="SaveSetting"><i class="far fa-save" ></i>{{ $t('Prefixies.Save') }} </button>
                    <button type="button" class="btn btn-outline-danger" v-on:click="GotoPage('/StartScreen')">{{ $t('Prefixies.Cancel') }}</button>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </div>
</template>
 <script>
    
    import { required,maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default ({
        name: "Prefixies",
         mixins: [clickMixin],
         components: {
            Loading
        },
         setup() {
             return { v$: useVuelidate() }
         },
        data: function () {
            return {
                loading: false,
                prefix: {
                    sInvoice: '',
                    debitNote: '',
                    creditNote: '',
                    proformaSaleInvoice: '',
                    sReturn: '',
                    sOrder: '',
                    sQutation: '',
                    pInvoice: '',
                    pReturn: '',
                    pOrder: '',
                    employee: '',
                    sOrdeTracking:'',
                    receiptInvoice:'',
                    globalInvoice:'',
                    customerPayReceipt:'',
                    advanceSaleReceipt:'',
                    supplierPayReceipt:'',
                    advancePurchaseReceipt:'',
                }
            }
        },
         validations() {
             return {
                 prefix: {
                    advancePurchaseReceipt:{
                    required,
                    maxLength:maxLength(3)
                },
                supplierPayReceipt:{
                    required,
                    maxLength:maxLength(3)
                },
                customerPayReceipt:{
                    required,
                    maxLength:maxLength(3)
                },
                advanceSaleReceipt:{
                    required,
                    maxLength:maxLength(3)
                },
                     sInvoice: {
                         required,
                         maxLength: maxLength(3)
                     },
                     sReturn: {
                         required,
                         maxLength: maxLength(3)
                     },
                     sOrder: {
                         required,
                         maxLength: maxLength(3)
                     },
                     sQutation: {
                         required,
                         maxLength: maxLength(3)
                     },
                     pInvoice: {
                         required,
                         maxLength: maxLength(3)
                     },
                     pReturn: {
                         required,
                         maxLength: maxLength(3)
                     },
                     pOrder: {
                         required,
                         maxLength: maxLength(3)
                     },
                     sOrdeTracking: {
                         required,
                         maxLength: maxLength(3)
                     },
                     employee: {
                         maxLength: maxLength(3)
                     },
                     receiptInvoice: {
                         maxLength: maxLength(2)
                     },
                     globalInvoice: {
                         maxLength: maxLength(2)
                     },
                     proformaSaleInvoice: {
                         maxLength: maxLength(3)
                     },
                     creditNote: {
                         maxLength: maxLength(2)
                     },
                     debitNote: {
                         maxLength: maxLength(2)
                     },
                 }
                 }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            SaveSetting: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.post('/Company/SavePrefixies', this.prefix, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$swal({
                                title: "Saved!",
                                text: "Saved Successfully!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Something Went Wrong!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },

            GetData: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/PrefixDetails', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.prefix = response.data;
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Something Went Wrong!',
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
            this.GetData()

        },
        mounted: function () {

        }
    })
</script> 