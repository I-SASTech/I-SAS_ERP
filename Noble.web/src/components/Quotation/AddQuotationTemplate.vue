<template>
    <div v-if="isValid('CanEditQuotation') || isValid('CanDraftQuotation') || isValid('CanAddQuotation')" >
        <div class="row">
            <div class="col-lg-12">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="row">
                                <div class="col">
                                    <h4 v-if="purchase.id === '00000000-0000-0000-0000-000000000000'" class="page-title">Add Template <span style="font-weight:bold">  - {{purchase.registrationNo}}</span></h4>
                                    <h4 v-else class="page-title">Update Template <span style="font-weight:bold">  - {{purchase.registrationNo}}</span></h4>

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
                    <div class="col-md-12">
                        <QuotationItemTemplate @update:modelValue="SavePurchaseItems" :key="rander" :taxMethod="purchase.taxMethod" :taxRateId="purchase.taxRateId" :isTemplate="true" />
                    </div>
                    <div class="col-lg-12 mt-4 mb-5">
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-5">

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-8" style="border-right: 1px solid rgb(238, 238, 238);">
                                        <div class="form-group pe-3">
                                            <label>Template Description :<span class="text-danger"> *</span></label>
                                            <textarea class="form-control" 
                                                      v-model="purchase.description" rows="3"/>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group ps-3">
                                            <div class="font-xs mb-1"> {{ $t('AddQuotation.Attachment') }}</div>
                                            <button type="button" class="btn btn-light btn-square btn-outline-dashed mb-1" v-on:click="Attachment()">
                                                <i class="fas fa-cloud-upload-alt"></i> Attachment
                                            </button>
                                            <div>
                                                <small class="text-muted"> You can upload a maximum of 10 files, 5MB each </small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <loading v-model:active="loading" :can-cancel="false" :is-full-page="false"></loading>
                    <div class="col-lg-12 ">
                    </div>

                </div>
                <bulk-attachment :attachmentList="purchase.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
            </div>
            <div class="col-lg-12 invoice-btn-fixed-bottom">
                <div v-if="!loading && purchase.id === '00000000-0000-0000-0000-000000000000'">
                    <div class="button-items">
                        <button class="btn btn-primary  mr-2"
                                v-on:click="savePurchase('Approved')"
                                v-if="isValid('CanAddQuotation')"
                                :disabled="v$.$invalid">

                            <i class="far fa-save"></i>  Save
                        </button>
                        <button class="btn btn-danger  mr-2"
                                v-on:click="goToPurchase">
                            {{ $t('AddQuotation.Cancel') }}
                        </button>
                    </div>
                </div>
                <div v-if="!loading && purchase.id!='00000000-0000-0000-0000-000000000000'">
                    <div class="button-items">
                        <button class="btn btn-primary  mr-2"
                                v-on:click="savePurchase('Approved')"
                                v-if="isValid('CanEditQuotation')">
                            <i class="far fa-save"></i> Update
                        </button>
                        <button class="btn btn-danger  mr-2"
                                v-on:click="goToPurchase">
                            {{ $t('AddQuotation.Cancel') }}
                        </button>
                    </div>
                </div>

            </div>
            <!--<div class="card">
    <div class="card-body">
        <div class="row">
            <div class="col-lg-12">
                <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 poHeading" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                        <span v-if="purchase.id === '00000000-0000-0000-0000-000000000000'">Add Template <span style="font-weight:bold">  - {{purchase.registrationNo}}</span></span>
                        <span v-else>Update Template <span style="font-weight:bold">  - {{purchase.registrationNo}}</span></span>

                    </div>

                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 dateHeading" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                        {{purchase.date}}
                    </div>
                </div>
                <div class="row bottomBorder"></div>
                <div class="row mt-3">
                    <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" >
                        <label>Template Description :<span class="text-danger"> *</span></label>
                        <textarea class="form-control" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'"
                               v-model="purchase.description" />
                    </div>

                </div>
                <br />
                <QuotationItemTemplate @update:modelValue="SavePurchaseItems" :key="rander" :taxMethod="purchase.taxMethod" :taxRateId="purchase.taxRateId" :isTemplate="true" />
              <div class="row">
                    <div v-if="!loading && purchase.id === '00000000-0000-0000-0000-000000000000'" class="col-md-12  " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                        <button class="btn btn-primary mr-2 float-left" v-on:click="Attachment()">
                            {{ $t('AddQuotation.Attachment') }}
                        </button>
                        <button class="btn btn-primary  mr-2"
                                v-on:click="savePurchase('Approved')"
                                v-if="isValid('CanAddQuotation')"
                                  :disabled="v$.$invalid">

                            <i class="far fa-save"></i>  Save
                        </button>
                        <button class="btn btn-danger  mr-2"
                                v-on:click="goToPurchase">
                            {{ $t('AddQuotation.Cancel') }}
                        </button>

                    </div>
                    <div v-if="!loading && purchase.id!='00000000-0000-0000-0000-000000000000'" class="col-md-12 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">-->
            <!--<button class="btn btn-primary mr-2 float-left" v-on:click="Attachment()">
        {{ $t('AddQuotation.Attachment') }}
    </button>-->
            <!--<button class="btn btn-primary  mr-2"
                                    v-on:click="savePurchase('Approved')"
                                    v-if="isValid('CanEditQuotation')"
                                    >
                                <i class="far fa-save"></i> Update
                            </button>
                            <button class="btn btn-danger  mr-2"
                                    v-on:click="goToPurchase">
                                {{ $t('AddQuotation.Cancel') }}
                            </button>
                        </div>
                        <div class="card-footer col-md-3" v-else>
                            <loading v-model:active="loading"
                                     :can-cancel="true"
                                     :is-full-page="true"></loading>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>-->
            <!--<bulk-attachment :attachmentList="purchase.attachmentList" :show="show" v-if="show" @close="attachmentSave" />-->
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
        </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import moment from "moment";
    
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';

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
                randerCustomer: 0,
                isService: false,
                daterander: 0,
                rander: 0,
                render: 0,
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    registrationNo: "",
                    customerId: "",
                    refrence: "",
                    days: '',
                    purpose: "Quotation",
                    for: "",
                    purchaseOrder: "",
                    paymentMethod: "",
                    sheduleDelivery: "",
                    note: '',
                    isFreight: false,
                    isLabour: false,
                    isQuotation: true,
                    quotationTemplateItems: [],
                    attachmentList: [],
                    path: '',
                    clientPurchaseNo: '',

                    importExportItems: [],
                    orderTypeId: '',
                    incotermsId: '',
                    commodities: '',
                    natureOfCargo: '',
                    attn: '',
                    quotationValidDate: '',
                    freeTimePOL: '',
                    freeTimePOD: '',
                    taxMethod: '',
                    taxRateId: '',
                },
                loading: false,
                show: false,
                importExportSale: false,

                itemRender: 0,
                serviceId: '',
                stuffingLocationId: '',
                portOfLoadingId: '',
                portOfDestinationId: '',
                carrierId: '',
                ft: '',
                hc: '',
                tt: '',
                etd: '',
                saleDefaultVat: '',
            };
        },

        computed: {
            isAddProductValid: function () {

                if (this.serviceId == '' || this.serviceId == null || this.serviceId == undefined || this.serviceId == '00000000-0000-0000-0000-000000000000') {
                    return true
                }

                return false;
            },

        },
        validations() {
            return {
                purchase: {
                    date: { required },
                    description: { required },
                    registrationNo: { required },
                    refrence: {},


                    quotationTemplateItems: { required },
                },
                }
        },
        methods: {

            createUUID: function () {
                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
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


            RanderCustomer: function () {
                this.randerCustomer++;
            },

            AutoIncrementCode: function () {
                
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                var service=false
                if (this.isService) {
                    service = true;
                }
                root.$https
                    .get('/Purchase/QuotationTemplateAutoGenerateNo?IsService=' + service, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.purchase.registrationNo = response.data;
                        }
                    });
            },
            SavePurchaseItems: function (quotationTemplateItems) {

                this.purchase.quotationTemplateItems = quotationTemplateItems;
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
                    .post('/Purchase/SaveQuotationTemplateInformation', root.purchase, { headers: { "Authorization": `Bearer ${token}` } })
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
                                    if (root.isService) {
                                        root.$router.push('/ServiceQuotation');


                                    }
                                    else {
                                        root.$router.push('/Quotation');

                                    }

                                } else {
                                    if (root.isService) {
                                        root.$router.push('/ServiceQuotation');


                                    }
                                    else {
                                        root.$router.push('/Quotation');

                                    }
                                }
                            }
                        });

                    })
                    .catch(error => {
                        console.log(error)
                        if (localStorage.getItem('IsMultiUnit') == 'true') {
                            root.purchase.quotationTemplateItems.forEach(function (x) {

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

            goToPurchase: function () {

                if (this.isService) {
                    this.$router.push('/ServiceQuotation');

                    
                }
                else {
                    this.$router.push('/Quotation');

                }
               

            },
        },
        created: function () {
            if(this.$route.query.Add == 'false')
            {
                this.$route.query.data = this.$store.isGetEdit;
            }

            this.$emit('update:modelValue', this.$route.name);

            if (this.$route.query.data != undefined) {
                
                

                this.purchase = this.$route.query.data;
                this.isService = this.$route.query.data.isService;
                if (this.isService) {
                    this.purchase.isService = true;
                }
                else {
                    this.purchase.isService = false;
                }
                if (this.$route.query.Add=='false') {
                    this.purchase.id = "00000000-0000-0000-0000-000000000000";
                    this.AutoIncrementCode();
                }


                this.purchase.date = moment(this.purchase.date).format('llll');
                
                this.attachment = true;
                this.rander++;
                this.render++;
                this.rendered++;
            }
            
        },
        mounted: function () {

            this.purchase.date = moment().format('llll');
            this.daterander++;
        },
    };
</script>
