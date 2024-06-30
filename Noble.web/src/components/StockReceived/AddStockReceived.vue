<template>
    <div class="row">
        <div class="col-md-12 ml-auto mr-auto">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="wareHouseTransfer.id === '00000000-0000-0000-0000-000000000000'"
                                    class="page-title">
                                    {{ $t('Add Stock Received') }}
                                </h4>
                                <h4 v-else class="page-title">{{ $t('Update Stock Received') }}</h4>
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
                            <span id="ember695" class="tooltip-container text-dashed-underline "> {{ $t('Stock Request No')
                            }}<span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-bind:key="randerInput" v-model="wareHouseTransfer.code" disabled class="form-control"
                                type="text">
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span id="ember695" class="tooltip-container text-dashed-underline "> {{ $t('Stock Request')
                            }}<span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <stockrequestdropdown v-model="wareHouseTransfer.stockRequestId"
                                :modelValue="wareHouseTransfer.stockRequestId" :isStockTransfer="isStockTransfer" :isStockReceived="isStockReceived" :key="render"
                                @update:modelValue="EditWareHouseTransfer(wareHouseTransfer.stockRequestId)" :isDisabled="true" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddWareHouseTransfer.Date') }}
                                <span class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="wareHouseTransfer.date" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('WareHouse') }}:<span
                                    class="text-danger">*</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <warehouse-dropdown v-model="wareHouseTransfer.wareHouseId" :value="wareHouseTransfer.wareHouseId"
                                @default="setDefaultWareHouseFrom" :key="render" />
                        </div>
                    </div>
                </div>
               

            </div>

            <stockreceiveditem :wareHouseTransferItem="wareHouseTransfer.wareHouseTransferItems" 
                               ref="wareHouseItemsRenove"
                               :wareHouse="wareHouseTransfer.wareHouseId" 
                               @update:modelValue="SaveWareHouseTransferItems"
                               @summary="updateSummary"
                               :totalAmount="wareHouseTransfer.totalAmount"
                               v-bind:key="randerWareHouse" />


            <div class="col-lg-12 invoice-btn-fixed-bottom">
                <div v-if="!loading && wareHouseTransfer.id == '00000000-0000-0000-0000-000000000000'"
                    class="col-md-12 arabicLanguage">

                    <a class="btn btn-outline-primary me-2 disabled" v-if="wareHouseTransfer.wareHouseId == '' || wareHouseTransfer.wareHouseId == null" v-on:click="saveWareHouseTransfer('Draft')">
                        <i class="far fa-save"></i> {{ $t('AddWareHouseTransfer.SaveAsDraft') }}
                    </a>
                    <a class="btn btn-outline-primary me-2" v-else v-on:click="saveWareHouseTransfer('Draft')">
                        <i class="far fa-save"></i> {{ $t('AddWareHouseTransfer.SaveAsDraft') }}
                    </a>
                    <a class="btn btn-outline-primary me-2 disabled" v-if="wareHouseTransfer.wareHouseId == '' || wareHouseTransfer.wareHouseId == null" v-on:click="saveWareHouseTransfer('Approved')">
                        <i class="far fa-save"></i> {{ $t('AddWareHouseTransfer.SaveAsPost') }}
                    </a>
                    <a class="btn btn-outline-primary me-2" v-else v-on:click="saveWareHouseTransfer('Approved')">
                        <i class="far fa-save"></i> {{ $t('AddWareHouseTransfer.SaveAsPost') }}
                    </a>


                    <button class="btn btn-danger me-2" v-on:click="goToWareHouseTransfer">
                        {{ $t('AddPurchase.Cancel') }}
                    </button>
                </div>
                <div v-if="!loading && wareHouseTransfer.id != '00000000-0000-0000-0000-000000000000'"
                    class="col-md-12 arabicLanguage">

                    <button class="btn btn-outline-primary me-2" v-on:click="saveWareHouseTransfer('Draft')"
                        v-if="wareHouseTransfer.stockStatus == 'Default'">
                        <i class="far fa-save"></i> {{ $t('AddWareHouseTransfer.UpdateAsDraft') }}
                    </button>
                    <button class="btn btn-outline-primary me-2" v-on:click="saveWareHouseTransfer('Approved')">
                        <i class="far fa-save"></i> {{ $t('AddWareHouseTransfer.UpdateAsPost') }}
                    </button>

                    <button class="btn btn-danger me-2" v-on:click="goToWareHouseTransfer">
                        {{ $t('AddPurchase.Cancel') }}
                    </button>
                </div>
            </div>


            <div class="col-lg-12 mt-4 mb-5">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-8">

                            </div>
                            <div class="col-lg-4">
                                <div class="form-group ps-3" v-if="!loading">
                                    <div class="font-xs mb-1">{{ $t('AddWareHouseTransfer.AttachFile') }}</div>

                                    <button v-on:click="Attachment()" type="button"
                                        class="btn btn-light btn-square btn-outline-dashed mb-1">
                                        <i class="fas fa-cloud-upload-alt"></i> {{ $t('AddPurchase.Attachment') }}
                                    </button>

                                    <div>
                                        <small class="text-muted">
                                            {{ $t('AddWareHouseTransfer.FileSize') }}
                                        </small>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <bulk-attachment :attachmentList="wareHouseTransfer.attachmentList" :show="show" v-if="show"
            @close="attachmentSave" />

        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>

    <!-- <div v-else> <acessdenied></acessdenied></div> -->
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

import moment from "moment";

export default {
    mixins: [clickMixin],
    name: "AddStockReceived",
    components: {
        Loading
    },
    data: function () {
        return {
            wareHouseTransfer: {
                id: "00000000-0000-0000-0000-000000000000",
                date: "",
                code: "",
                wareHouseId: "",
                wareHouseTransferItems: [],
                attachmentList: [],
                branchId: '',
                branchType: '',
                fromBranchId: '',
                stockRequestId: '',
                stockTransferId: '',
                approvalStatus: '',
                totalAmount:0,
            },
            isStockTransfer: false,
            isStockReceived: false,
            loading: false,
            show: false,
            rendered: 0,
            render: 0,
            func: 0,
            randerInput: 0,
            language: 'Nothing',
            randerWareHouse: 0
        };
    },
    validations() {

    },
    methods: {
        updateSummary: function (summary) {
                this.wareHouseTransfer.totalAmount = summary.totalAmount;
            },
        RederFucn: function () {
            this.render++;
        },
        ClearRecord: function () {
            this.wareHouseTransfer.wareHouseTransferItems = [];
            this.wareHouseTransfer.attachmentList = [];
            this.wareHouseTransfer.stockRequestId = '';
            this.wareHouseTransfer.wareHouseId = '';
            this.wareHouseTransfer.vehicalNo = '';
            this.wareHouseTransfer.driverName = '';
            this.wareHouseTransfer.driverNumber = '';
            this.wareHouseTransfer.driverNationalId = '';
            this.AutoIncrementCode();
            this.$refs.wareHouseItemsRenove.wareHouseItemsClear();
            this.RederFucn();

        },
        Attachment: function () {
            this.show = true;
        },

        attachmentSave: function (attachment) {

            this.show = false;
            this.wareHouseTransfer.attachmentList = attachment;
        },

        setDefaultWareHouseFrom: function (id) {

            this.wareHouseTransfer.fromWareHouseId = id;
        },

        languageChange: function (lan) {

            if (this.language == lan) {
                if (this.wareHouseTransfer.id == '00000000-0000-0000-0000-000000000000') {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/addSale');
                }
                else {
                    this.$swal({
                        title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
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

        AutoIncrementCode: function () {
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }
            root.$https
                .get("/Purchase/GetStockReceivedAutoCode?branchId=" + localStorage.getItem('BranchId'), {
                    headers: { Authorization: `Bearer ${token}` },
                })
                .then(function (response) {
                    if (response.data != null) {
                        root.wareHouseTransfer.code = response.data;
                        root.randerInput++;
                    }
                });
        },

        SaveWareHouseTransferItems: function (wareHouseTransferItems) {
            this.wareHouseTransfer.wareHouseTransferItems = wareHouseTransferItems;
        },

        saveWareHouseTransfer: function (status) {
            this.wareHouseTransfer.approvalStatus = status
            localStorage.setItem('active', status);

            this.loading = true;
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }

            var isMultiUnit = localStorage.getItem('IsMultiUnit');
            if (isMultiUnit == 'true') {
                root.wareHouseTransfer.wareHouseTransferItems.forEach(function (x) {
                    x.quantity = parseInt(parseInt(x.quantity) + (parseInt(x.highQty) * parseInt(x.product.unitPerPack)));
                });
            }
            root.wareHouseTransfer.branchId = localStorage.getItem('BranchId');
            root.wareHouseTransfer.branchType = localStorage.getItem('BranchType');

            this.$https
                .post("/Purchase/SaveStockReceivedInformation", root.wareHouseTransfer, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                .then((response) => {
                    if (response.data.isSuccess) {
                        root.$swal.fire({
                            icon: "success",
                            title: response.data.isAddUpdate,
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                        });

                        root.ClearRecord();
                    }
                    else {
                        root.$swal.fire({
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Something went wrong.",
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                    }
                    root.loading = false;
                })
        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        goToWareHouseTransfer: function () {
            var root = this;
            if (root.isValid('CanViewStockTransfer') || root.isValid('CanDraftStockTransfer')) {
                root.$router.push({
                    path: '/stockreceived',
                    query: {
                        data: 'stockreceived'
                    }
                })
            }
            else {
                root.$router.go();
            }
        },
        EditWareHouseTransfer: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Purchase/GetStockRequestDetails?Id=' + id + '&branchId=' + localStorage.getItem('WareHouseId'), { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {

                        var isMultiUnit = localStorage.getItem('IsMultiUnit');
                        if (isMultiUnit == 'true' && localStorage.getItem('decimalQuantity') == 'true') {
                            response.data.wareHouseTransferItems.forEach(function (x) {
                                x.highQty = parseInt(parseFloat(x.quantity) / parseFloat(x.product.unitPerPack));
                                x.quantity = parseFloat(parseFloat(x.quantity) % parseFloat(x.product.unitPerPack)).toFixed(3).slice(0, -1);
                            });
                        }
                        if (isMultiUnit == 'true' && localStorage.getItem('decimalQuantity') != 'true') {
                            response.data.wareHouseTransferItems.forEach(function (x) {
                                x.highQty = parseInt(parseInt(x.quantity) / parseInt(x.product.unitPerPack));
                                x.quantity = parseInt(parseInt(x.quantity) % parseInt(x.product.unitPerPack));
                            });
                        }
                        root.wareHouseTransfer.wareHouseTransferItems = response.data.wareHouseTransferItems;
                        root.randerWareHouse++;
                    }
                },
                    function (error) {
                        console.log(error);
                    });
        },


    },

    created: function () {
        var root =  this;

        if(root.$route.query.Add == 'false')
        {
            root.$route.query.data = this.$store.isGetEdit;
        }
        if (this.$route.query.data != undefined) {
            if(this.$route.query.data.isStockReceived)
            {
                this.wareHouseTransfer.id = this.$route.query.data.id;
                this.wareHouseTransfer.wareHouseTransferItems = this.$route.query.data.wareHouseTransferItems;
                this.wareHouseTransfer.date = this.$route.query.data.date;
                this.wareHouseTransfer.code = this.$route.query.data.code;
                this.wareHouseTransfer.wareHouseId = this.$route.query.data.warehouseId;
                this.wareHouseTransfer.branchId = this.$route.query.data.branchId;
                this.wareHouseTransfer.stockRequestId = this.$route.query.data.stockRequestId;
                this.wareHouseTransfer.stockTransferId = this.$route.query.data.stockTransferId;
                this.isStockReceived = this.$route.query.data.isStockReceived;
                this.wareHouseTransfer.totalAmount = this.$route.query.data.totalAmount;
                this.randerWareHouse++;
                this.render++;
                this.RederFucn();
            }
            else
            {
                this.AutoIncrementCode();
                this.wareHouseTransfer.date = moment().format("LLL");
                this.rander++;
                this.wareHouseTransfer.wareHouseTransferItems = this.$route.query.data.wareHouseTransferItems;
                this.wareHouseTransfer.stockRequestId = this.$route.query.data.stockRequestId;
                this.wareHouseTransfer.stockTransferId = this.$route.query.data.id;
                this.wareHouseTransfer.fromBranchId = this.$route.query.data.branchId;
                this.randerWareHouse++;
            }
        }
        this.$emit('update:modelValue', this.$route.name);
    },
    mounted: function () {
        this.language = this.$i18n.locale;
    }
};
</script>
