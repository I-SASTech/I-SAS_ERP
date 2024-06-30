<template>
    <div class="col-lg-12 " v-if="isValid('CanAddDispatchNote')">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 v-if="purchase.id === '00000000-0000-0000-0000-000000000000'" class="page-title">
                                {{
                                    $t('AddDispatchNote.DispatchNote')
                                }}
                            </h4>
                            <h4 v-else class="page-title">{{ $t('AddDispatchNote.UpdateDispatchNote') }} </h4>
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
                <div class="row form-group">
                    <label class="col-form-label col-lg-4">
                        <span class="tooltip-container text-dashed-underline ">{{ $t('AddDispatchNote.Invoice') }} #</span>
                    </label>
                    <div class="inline-fields col-lg-8">
                        <input v-model="purchase.registrationNo" class="form-control" type="text" disabled>
                    </div>
                </div>

                <div class="row form-group">
                    <label class="col-form-label col-lg-4">
                        <span class="tooltip-container text-dashed-underline ">{{ $t('AddDispatchNote.Date') }} :</span>
                    </label>
                    <div class="inline-fields col-lg-8">
                        <input v-model="purchase.date" class="form-control" type="text" disabled>
                    </div>
                </div>
                <div class="row form-group" v-bind:key="randerCustomer">
                    <label class="col-form-label col-lg-4">
                        <span class="tooltip-container text-dashed-underline ">
                            {{ $t('AddDispatchNote.Customer') }} :
                            <span class="text-danger">*</span>
                        </span>
                    </label>
                    <div class="inline-fields col-lg-8">
                        <customerdropdown v-model="v$.purchase.customerId.$model" :modelValue="purchase.customerId"
                                          :key="saleOrderRender" />
                        <a href="javascript:void(0);" data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight"
                           aria-controls="offcanvasRight" class="text-primary">{{ $t('AddDispatchNote.ViewCustomerDetails') }}</a>
                        <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight"
                             aria-labelledby="offcanvasRightLabel">
                            <div class="offcanvas-header">
                                <h5 id="offcanvasRightLabel" class="m-0">{{ $t('AddDispatchNote.CustomerDetails') }}</h5>
                                <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas"
                                        aria-label="Close"></button>
                            </div>
                            <div class="offcanvas-body">
                                <div class="row">
                                    <div class="col-lg-12 form-group">
                                        <label>{{ $t('AddSaleOrder.Mobile') }} :</label>
                                        <input type="text" class="form-control" v-model="purchase.mobile" />
                                    </div>
                                    <div class="col-lg-12 form-group">
                                        <label>{{ $t('AddDispatchNote.CustomerAddress') }} :</label>
                                        <textarea rows="3" v-model="purchase.customerAddress"
                                                  class="form-control"> </textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



            </div>
            <div class="col-lg-6">
                <div class="row form-group">
                    <label class="col-form-label col-lg-4">
                        <span class="tooltip-container text-dashed-underline ">
                            {{ $t('AddDispatchNote.Refrence') }}
                            :
                        </span>
                    </label>
                    <div class="inline-fields col-lg-8">
                        <input class="form-control " v-model="v$.purchase.refrence.$model" />
                    </div>
                </div>
                <div class="row form-group">
                    <label class="col-form-label col-lg-4">
                        <span class="tooltip-container text-dashed-underline ">
                            {{ $t('AddDispatchNote.SaleOrder') }}
                            :
                        </span>
                    </label>
                    <div class="inline-fields col-lg-8">
                        <saleorderdropdown @update:modelValue="GetSaleOrderData" :modelValue="purchase.saleOrderId" />
                    </div>
                </div>
            </div>

            <dispatch-item @update:modelValue="SavePurchaseItems" :purchaseItems="purchase.dispatchNoteItems"
                           :key="saleOrderRender" />
            <div class="col-lg-12 mt-4 mb-5">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                <div class="form-group pe-3">
                                    <label>{{ $t('AddDispatchNote.TermandCondition') }} :</label>
                                    <textarea class="form-control " rows="3" v-model="purchase.note" />
                                </div>
                            </div>
                            <div class="col-lg-4" hidden>
                                <div class="form-group ps-3">
                                    <div class="font-xs mb-1">Attach File(s) to Sale Invoice</div>

                                    <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> {{ $t('AddSale.Attachment') }} </button>

                                    <div>
                                        <small class="text-muted">
                                            You can upload a maximum of 10 files, 5MB each
                                        </small>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12 invoice-btn-fixed-bottom">
                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div v-if="purchase.id === '00000000-0000-0000-0000-000000000000'">
                        <button class="btn btn-outline-primary  me-2" v-if="isValid('CanAddDispatchNote')"
                                v-on:click="savePurchase('Draft')"
                                :disabled="v$.$invalid || purchase.dispatchNoteItems.filter(x => x.quantity == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddDispatchNote.SaveAsDraft') }}
                        </button>
                        <button class="btn btn-outline-primary  me-2" v-if="isValid('CanAddDispatchNote')"
                                v-on:click="savePurchase('Approved')"
                                :disabled="v$.$invalid || purchase.dispatchNoteItems.filter(x => x.quantity == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddDispatchNote.SaveAsPost') }}
                        </button>
                        <button class="btn btn-danger  me-2" v-on:click="goToPurchase">
                            {{ $t('AddDispatchNote.Cancel') }}
                        </button>
                    </div>
                    <div v-else>
                        <button class="btn btn-outline-primary  me-2" v-if="isValid('CanAddDispatchNote')"
                                v-on:click="savePurchase('Draft')"
                                :disabled="v$.$invalid || purchase.dispatchNoteItems.filter(x => x.quantity == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddDispatchNote.UpdateAsDraft') }}
                        </button>

                        <button class="btn btn-outline-primary  me-2" v-if="isValid('CanAddDispatchNote')"
                                v-on:click="savePurchase('Approved')"
                                :disabled="v$.$invalid || purchase.dispatchNoteItems.filter(x => x.quantity == '').length > 0">
                            <i class="far fa-save"></i> {{ $t('AddDispatchNote.SaveAsPost') }}
                        </button>
                        <button class="btn btn-danger  me-2" v-on:click="goToPurchase">
                            {{ $t('AddDispatchNote.Cancel') }}
                        </button>
                    </div>
                </div>
            </div>

        </div>


        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>

    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<style scoped>
    input {
        height: 40px !important;
    }
</style>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    //
    import moment from "moment";

    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    //import Multiselect from 'vue-multiselect';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    //import Vue3Barcode from 'vue3-barcode'
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
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    registrationNo: "",
                    customerId: "",
                    saleOrderId: null,
                    refrence: "",
                    purchaseOrder: "",
                    note: '',
                    dispatchNoteItems: [],
                },
                loading: false,
                saleOrderRender: 0,
                randerCustomer: 0,
            };
        },
        validations() {
            return {
                purchase: {
                    date: { required },
                    registrationNo: { required },
                    customerId: { required },
                    refrence: {},
                    /* dispatchNoteItems: { required },*/
                },
            }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },

            AutoIncrementCode: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/Sale/DispatchNoteAutoGenerateNo?branchId=" + localStorage.getItem('BranchId'), {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.purchase.registrationNo = response.data;
                        }
                    });
            },
            SavePurchaseItems: function (dispatchNoteItems) {

                this.purchase.dispatchNoteItems = dispatchNoteItems;
            },
            savePurchase: function (status) {
                this.purchase.approvalStatus = status
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.purchase.branchId = localStorage.getItem('BranchId');

                this.$https
                    .post('/Sale/SaveDispatchNote', root.purchase, { headers: { "Authorization": `Bearer ${token}` } })
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
                        })
                            .then(function (response) {
                                if (response != undefined) {
                                    if (root.purchase.id == "00000000-0000-0000-0000-000000000000") {
                                        root.$router.go('AddDispatchNote');

                                    } else {
                                        root.$router.push("DispatchNotes");
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

            GetSaleOrderData: function (id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (id != undefined && id != null) {
                    root.$https.get('/Purchase/SaleOrderDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data != null) {
                                root.purchase.date = moment(response.data.date).format('llll');
                                root.purchase.saleOrderId = response.data.id;
                                root.purchase.refrence = response.data.refrence;
                                root.purchase.customerId = response.data.customerId;
                                root.purchase.note = response.data.note;
                                root.purchase.dispatchNoteItems = response.data.saleOrderItems;
                                root.saleOrderRender++;

                            }
                        });
                }
            },
            goToPurchase: function () {
                this.$router.push('/DispatchNotes');
            },
        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            
            this.$emit('update:modelValue', this.$route.name);
            if (this.$route.query.data != undefined) {
                this.purchase = this.$route.query.data;
                this.purchase.date = moment(this.purchase.date).format('LLL');
                this.attachment = true;
            }
        },
        mounted: function () {

            if (this.$route.query.data == undefined) {
                this.AutoIncrementCode();
                this.purchase.date = moment().format('LLL');
            }





        },
    };
</script>
