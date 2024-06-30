<template>
    <modal :show="show" v-bind:modalLarge="true" v-if=" isValid('CanViewReturnHistory')">

        <div style="margin-bottom:0px" class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="text-right">
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()"> {{ $t('ReturnHistory.btnClear') }}</button>
                        </div>
                        <div class="col-lg-12" v-for="purchase in purchaseReturn" v-bind:key="purchase.id">
                            <div class="tab-content" id="nav-tabContent">
                                <div class="card-header">
                                    <div class="row">
                                        <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                            <h5>{{ $t('ReturnHistory.PurchaseReturn') }} - {{purchase.registrationNo}}</h5>
                                        </div>
                                        <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'arabicLanguage' : 'text-left'">
                                            <span>
                                                {{purchase.date}}
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            {{ $t('ReturnHistory.Supplier') }}
                                        </div>
                                        <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{ purchase.supplierName }}
                                        </div>

                                        <div class="col-lg-6">
                                            {{ $t('ReturnHistory.SupplierInvoiceNumber') }}
                                        </div>
                                        <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{ purchase.invoiceNo }}
                                        </div>

                                        <div class="col-lg-6">
                                            {{ $t('ReturnHistory.PurchaseOrderDate') }}
                                        </div>
                                        <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{ purchase.invoiceDate }}
                                        </div>

                                        <div class="col-lg-6">
                                            {{ $t('ReturnHistory.WareHouse') }}
                                        </div>
                                        <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{ purchase.wareHouseName }}
                                        </div>

                                        <div class="col-lg-6">
                                            {{ $t('ReturnHistory.TaxMethod') }}
                                        </div>
                                        <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{purchase.taxMethod}}
                                        </div>

                                        <div class="col-lg-6">
                                            {{ $t('ReturnHistory.VAT%') }}
                                        </div>
                                        <div class="col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                            {{purchase.taxRatesName}}
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <br />
                            <purchase-view-item :hide="true" :key="rander1" :purchase="purchase" :raw="purchase.isRaw" :taxMethod="purchase.taxMethod" :taxRateId="purchase.taxRateId" @update:modelValue="SavePurchaseItems" />

                        </div>


                        <div class="card-body">
                            <div class="row ">
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        props: ['show', 'newpurchaseReturn', 'type'],
        mixins: [clickMixin],
        data: function () {
            return {
                purchaseReturn: this.newpurchaseReturn,
                rendered: 0,
                registrationNo: "",
               
                raw: '',
                rander: 0,
                rander1: 0,
                purchaseinvoicedropdown: "",
                loading: false,
                language: 'Nothing',
                supplierRender: 0,
                options: [],
            }
        },
        methods: {
            GetData: function (id) {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                var isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
                root.$https.get('/PurchasePost/PurchasePostDetail?id=' + id + '&isReturnView=' + true + '&isMultiUnit=' + isMultiUnit, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.purchase = response.data;
                        root.purchase.purchaseOrderItems = response.data.purchasePostItems;
                        root.rander++;
                        root.rander1++;
                    }
                });
            },
            close: function () {
                this.$emit('close');
            },
            SavePurchaseItems: function (purchaseOrderItems) {
                this.purchaseReturn.purchaseOrderItems = purchaseOrderItems;
            },
        },
        mounted: function () {
            this.language = this.$i18n.locale;
                this.raw = localStorage.getItem('IsProduction');
            

        }
    }
</script>
