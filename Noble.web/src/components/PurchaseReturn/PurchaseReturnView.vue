<template>
    <div class="row" v-if="isValid('CanViewDetailPurchaseReturn')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col">
                    <h5 class="page_title">{{ $t('PurchaseReturnView.PurchaseReturn') }}</h5>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PurchaseReturnView.Home') }}</a></li>
                        <li class="breadcrumb-item active">
                            {{ $t('PurchaseReturnView.PurchaseReturn') }}
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
                    <div class="row" >                                          
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" >
                            <h5>{{ $t('PurchaseView.From') }}</h5>
                            <div class="card" style="border: 1px #dddddd solid;">
                                <div class="card-body">
                                    <label>{{($i18n.locale == 'en' ||isLeftToRight())?headerFooter.company.nameEnglish:headerFooter.company.companyNameArabic}}</label>
                                    <br />
                                    <label>{{($i18n.locale == 'en' ||isLeftToRight())?headerFooter.company.addressEnglish:headerFooter.company.addressArabic}}</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" >
                            <h5>{{ $t('PurchaseView.InvoiceTo') }}</h5>
                            <div class="card" style="border: 1px #dddddd solid;">
                                <div class="card-body">
                                    <label>{{purchase.supplierName}}</label>
                                    <br />
                                    <!--<label v-if="!sale.isCredit">{{sale.customerAddress}}</label>-->
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- <div class="row">


                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 mb-2">
                            <label class="font-weight-bold"> {{ $t('PurchaseReturnView.TaxMethod') }} :</label>
                            <div>{{purchase.taxMethod}}</div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 mb-2">
                            <label class="font-weight-bold">{{ $t('PurchaseReturnView.VAT%') }} :</label>
                            <div>{{purchase.taxRatesName}}</div>
                        </div>

                    </div> -->
                    <div class="col-lg-12">
                        <!-- <purchase-view-item :key="rander1" :purchase="purchase" :raw="purchase.isRaw" :taxMethod="purchase.taxMethod" :taxRateId="purchase.taxRateId" @update:modelValue="SavePurchaseItems" /> -->
                        <purchase-view-item :purchase="purchase" :taxMethod="purchase.taxMethod" :taxRateId="purchase.taxRateId" :raw="purchase.isRaw"  :key="rander1"   :adjustmentProp="purchase.discount" :adjustmentSignProp="adjustmentSignProp" :isDiscountOnTransaction="purchase.isDiscountOnTransaction" :transactionLevelDiscountProp="purchase.transactionLevelDiscount" :isFixed="purchase.isFixed" :isBeforeTax="purchase.isBeforeTax" />

                        <div class="row">
                            <div class="col-md-6 ">
                                <button class="btn btn-sm btn-primary mx-1" v-on:click="Attachment()">
                                    {{ $t('PurchaseView.Attachment') }}
                                </button>
                                <button class="btn btn-sm btn-outline-danger" v-on:click="goToPurchase">
                                    {{ $t('PurchaseReturnView.Cancel') }}
                                </button>
                            </div>

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
                            <label class="invoice_lbl">{{ $t('PurchaseReturnView.PurchaseReturn') }}</label>
                            <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                            <label>{{purchase.registrationNo}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('PurchaseView.Date') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label> {{purchase.date}}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('PurchaseReturnView.Supplier') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label> {{ purchase.supplierName }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl"> {{ $t('PurchaseReturnView.SupplierInvoiceNumber') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{ purchase.invoiceNo }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">   {{ $t('PurchaseReturnView.PurchaseOrderDate') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{ purchase.invoiceDate }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">   {{ $t('PurchaseReturnView.WareHouse') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label> {{ purchase.wareHouseName }}</label>
                            <hr style="margin-top: 0.1rem;" />
                        </div>





                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2">
                            <label class="invoice_lbl">{{ $t('PurchaseReturnView.SendCopyTo') }}</label>
                            <hr style="margin-bottom: 0.1rem; margin-top: 0.3rem;" />
                            <label>{{ $t('PurchaseReturnView.Email') }}</label>
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
        <bulk-attachment :documentid="purchase.id" :show="isAttachshow" v-if="isAttachshow" @close="attachmentSaved" />
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    //
  import moment from "moment";
    
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    export default {
        mixins: [clickMixin],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                discountTypeOption: 'At Line Item Level',
                adjustmentSignProp: '+',

                isAttachshow: false,
                rendered: 0,
                registrationNo: "",
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    registrationNo: "",
                    supplierId: "",
                    invoiceNo: "",
                    invoiceDate: "",
                    purchaseOrder: "",
                    wareHouseId: "",
                    isRaw: false,
                    purchaseOrderItems: [],

                    discount: 0,
                    isDiscountOnTransaction: false,
                    isFixed: false,
                    isBeforeTax: true,
                    transactionLevelDiscount: 0
                },
                raw: '',
                rander: 0,
                rander1: 0,
                purchaseinvoicedropdown: "",
                loading: false,
                language: 'Nothing',
                supplierRender: 0,
                options: [],
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
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
                    purchaseOrderItems: { required },
                },
                }
        },
        methods: {
            Attachment: function () {
                this.isAttachshow = true;
            },

            attachmentSaved: function () {
                this.isAttachshow = false;
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

            SavePurchaseItems: function (purchaseOrderItems) {
                this.purchase.purchaseOrderItems = purchaseOrderItems;
            },

            goToPurchase: function () {
                
                this.$router.push({
                    path: '/PurchaseReturn',
                    query: {
                        data: 'PurchaseReturns'
                    }
                })
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
                          
                        }
                    });
            },
        },
        created: function () {

var root =  this;

if(root.$route.query.Add == 'false')
{
    root.$route.query.data = this.$store.isGetEdit;
}
},
        mounted: function () {
            this.GetHeaderDetail()
            this.language = this.$i18n.locale;
                this.raw = localStorage.getItem('IsProduction');
            

            if (this.$route.query.data != undefined) {
                this.purchase = this.$route.query.data;
                this.purchase.date = moment(this.purchase.date).format("LLL");
                this.purchase.purchaseOrderItems = this.$route.query.data.purchasePostItems;
            }

        },
    };
</script>
