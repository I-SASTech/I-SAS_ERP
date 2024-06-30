<template>
    <div class="row">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AboutUs.AboutUs') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item">
                                        <a href="javascript:void(0);">
                                            {{
                                        $t('AboutUs.Home')
                                            }}
                                        </a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('AboutUs.AboutUs') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('AboutUs.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />
            <div class="row">
                <div class="col-md-12">
                    <div class="card border-0">
                        <div class="card-body border-0">
                            <div class="row">
                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                    <div class="form-group">
                                        <h4 style="padding:0; margin:0;">{{ $t('AboutUs.LicenseGroup') }}: </h4>
                                        <ul class="list-group list-group-horizontal-md" >
                                            <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>{{ aboutUsDetail.groupName }}</li>
                                        </ul>

                                    </div>
                                </div>

                            </div>
                             <hr class="hr-dashed hr-menu mt-0" />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <h4>{{ $t('AboutUs.ModuleList') }}: </h4>
                                    </div>
                                </div>
                                <div class="col-md-4" v-for="(module) in aboutUsDetail.moduleList" :key="module">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>{{module}}</li>
                                    </ul>
                                </div>

                            </div>
                             <hr class="hr-dashed hr-menu mt-0" />
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group">
                                        <h4>
                                            {{ $t('AboutUs.LicenseType') }}:
                                        </h4>
                                        <ul class="list-group list-group-horizontal-md" >
                                            <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>{{ aboutUsDetail.licenseType }}</li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-4"
                                     v-if="!aboutUsDetail.isTechnicalSupport && aboutUsDetail.licenseType != 'Unlimited'">
                                    <div class="form-group">
                                        <p style="padding: 0; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.FromDate') }}:
                                        </p>
                                        <ul class="list-group list-group-horizontal-md" >
                                            <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>{{ aboutUsDetail.fromDate }}</li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-4"
                                     v-else-if="!aboutUsDetail.isTechnicalSupport && aboutUsDetail.licenseType === 'Unlimited' && !aboutUsDetail.isActive">
                                    <div class="form-group">
                                        <p style="padding: 0; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.FromDate') }}: 
                                        </p>
                                        <ul class="list-group list-group-horizontal-md" >
                                            <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>{{ aboutUsDetail.fromDate }}</li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-4"
                                     v-if="!aboutUsDetail.isTechnicalSupport && aboutUsDetail.licenseType != 'Unlimited'">
                                    <div class="form-group">
                                        <p style="padding: 0; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.ToDate') }}: 
                                        </p>
                                        <ul class="list-group list-group-horizontal-md" >
                                            <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>{{ aboutUsDetail.toDate }}</li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-4"
                                     v-if="!aboutUsDetail.isTechnicalSupport && aboutUsDetail.licenseType === 'Unlimited' && !aboutUsDetail.isActive">
                                    <div class="form-group">
                                        <p style="padding: 0; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.ToDate') }}:
                                        </p>
                                        <ul class="list-group list-group-horizontal-md" >
                                            <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>{{  aboutUsDetail.toDate }}</li>
                                        </ul>
                                    </div>
                                </div>

                                <div class="col-4">
                                    <div class="form-group"
                                         v-if="aboutUsDetail.licenseType == 'Subscription' && aboutUsDetail.isActive">
                                        <p style="padding: 0; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.PaymentFrequency') }}:
                                        </p>
                                        <ul class="list-group list-group-horizontal-md" >
                                            <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>{{ aboutUsDetail.paymentFrequency }}</li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="row" v-if="aboutUsDetail.isTechnicalSupport">
                                <div class="col-12">
                                    <div class="form-group">
                                        <h4 style="padding:0 ; margin:0;">
                                            Technical Support: 
                                        </h4>
                                        <ul class="list-group list-group-horizontal-md" >
                                            <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>{{ aboutUsDetail.supportType }}</li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-4" v-if="aboutUsDetail.supportType != 'UnLimited'">
                                    <div class="form-group">
                                        <p style="padding: 0 ; margin: 0; font-size: 16px !important; font-weight: 500;">
                                            {{ $t('AboutUs.FromDate') }}: 
                                        </p>
                                        <ul class="list-group list-group-horizontal-md" >
                                            <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>{{ aboutUsDetail.fromDate }}</li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-4" v-if="aboutUsDetail.supportType != 'UnLimited'">
                                    <div class="form-group">
                                        <p style="padding: 0 ; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.ToDate') }}:
                                        </p>
                                        <ul class="list-group list-group-horizontal-md" >
                                            <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>{{  aboutUsDetail.toDate }}</li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-4" v-if="aboutUsDetail.supportType != 'UnLimited'">
                                    <div class="form-group">
                                        <p style="padding: 0; margin: 0; font-size: 16px !important; font-weight:500;">
                                            {{ $t('AboutUs.PaymentFrequency') }}: 
                                        </p>
                                        <ul class="list-group list-group-horizontal-md" >
                                            <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>{{ aboutUsDetail.paymentFrequency }}</li>
                                        </ul>
                                    </div>
                                </div>

                            </div>
                            <hr class="hr-dashed hr-menu mt-0" />
                            <div class="row">
                                <div class="col-4">
                                    <div class="form-group" >
                                        <p style="padding: 0; margin: 0; font-size: 16px !important; font-weight:500;">
                                            Developers Setting:
                                        </p>
                                        <ul class="list-group list-group-horizontal-md" >
                                            <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i><a href="javascript:void(0)" class="btn text-primary" v-on:click="ShowDeveloperSettingLoginModel">Developer Setting</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="row" v-if="showDeveloperSetting">
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i> 
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="UpdateQuantity()"> {{ $t('AboutUs.UpdateQuantity') }}</a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="UpdatePurchase()" >{{ $t('AboutUs.UpdatePurchase') }}</a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="UpdateSale()">
                                                {{ $t('AboutUs.UpdateSale') }}
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="OneTimeStockQuantity()">
                                                {{ $t('AboutUs.StockEntry') }}
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="CreateProductAccount()">
                                                {{ $t('AboutUs.CreateProductAccount') }}
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="CreataLedgerTransaction()">
                                                {{ $t('AboutUs.CreataLedgerTransaction') }}
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="CreataLedgerTransaction('PurchasePost')">
                                                Ledger Transaction For Purchase
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <!-- <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="SumOfTransaction('Sum')">
                                                Sum Of Transaction
                                            </a>
                                        </li>
                                    </ul>
                                </div> -->
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="ContactDisplayName(true,false,false)">
                                                {{ $t('AboutUs.ChangeDisplayName') }}
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="ContactDisplayName(false, true,false)">
                                                {{ $t('AboutUs.CashCustomerToContacts') }}
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="ContactDisplayName(false,false,true)">
                                                {{ $t('AboutUs.ChangeInvoiceIdtoContactId') }}
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="ContactDisplayName(false,false,true,'Invoice')">
                                                {{ $t('AboutUs.DocumentTypeInvoice') }}
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="UpdateInvoiceTotal()">
                                                {{ $t('AboutUs.UpdateInvoiceTotal') }}
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="UpdateSaleOrderTotal()">
                                                {{ $t('AboutUs.UpdateSaleOrderTotal') }}
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="UpdatePurchaseInvoiceTotal()">
                                                {{ $t('AboutUs.UpdatePurchaseInvoiceTotal') }}
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="UpdateDailyExpense()">
                                                Upate Daily Expense
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="SaleInvoiceAccount()">
                                                {{ $t('AboutUs.SaleInvoiceAccount') }}
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-horizontal-md" >
                                        <li class="list-group-item border-0"><i class="la la-angle-double-right text-info me-2"></i>
                                            <a href="javascript:void(0)" class="text-primary" v-on:click="GetChangeBarCodeType()">
                                                {{ $t('Change Items Barcode Type') }}
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <!-- <brandmodel :newbrand="newBrand" :show="show" v-if="show" @close="IsSave" :type="type" /> -->
                <developersettingloginmodel @close="onCloseEvent" @login="LoginForDeveloper" :show="show" v-if="show" />
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </div>


</template>



<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
        data: function () {
            return {
                aboutUsDetail: '',
                productAccount: false,
                loading: false,
                show: false,
                showDeveloperSetting: false
            }
        },

        methods: {
            LoginForDeveloper: function(x)
            {
                this.show = false;
                this.showDeveloperSetting = x;
            },
            onCloseEvent: function () {
                this.show = false
            },
            ShowDeveloperSettingLoginModel: function () {
                this.show = true;
            },
            close: function () {
                this.$emit('close');
            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },

            UpdateInvoiceTotal: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loading = true;
                root.$https.get('Sale/CalculateSaleInvoice', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            UpdateSaleOrderTotal: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loading = true;
                root.$https.get('Sale/CalculateSaleOrder', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            UpdateDailyExpense: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loading = true;
                root.$https.get('Sale/CalculateDailyExpense', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            UpdatePurchaseInvoiceTotal: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loading = true;
                root.$https.get('Sale/CalculatePurchaseInvoice', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            UpdateQuantity: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Sale/UpdateQuantity', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            OneTimeStockQuantity: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Sale/OneTimeStockQuantity', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            UpdatePurchase: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Sale/RemoveDuplicateTransactions?purchaseNumber=' + localStorage.getItem('purchaseNumber'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            SaleInvoiceAccount: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Sale/TrialBalanceSettingFORSaleInvoice', { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            GetChangeBarCodeType: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/GetChangeBarCodeType', { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                        if (response.data.isSuccess) {
                            root.$swal({
                                title: "Saved!",
                                text: response.data.isAddUpdate,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                        else {
                            root.$swal({
                                title: "Error!",
                                text: "Something Went Wrong!",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
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
                    .finally(() => root.loading = false);
            },
            UpdateSale: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Sale/TrialBalanceSetting?saleNumber=' + localStorage.getItem('saleNumber'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            GetAboutUsDetail: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Company/AboutUs?isAboutUs=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess) {
                        root.aboutUsDetail = response.data.message
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            CreateProductAccount: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/CreateProductAccount?productAccount=' + true, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isAddUpdate != null) {
                            root.$swal({
                                title: "Saved!",
                                text: "Data has been added successfully",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                        else {
                            root.$swal({
                                title: "Error!",
                                text: "Something Went Wrong!",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
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
                    .finally(() => root.loading = false);
            },
            CreataLedgerTransaction: function (val) {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/CreateProductAccount?productAccount=' + this.productAccount + '&sum=' + val, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess) {
                            root.$swal({
                                title: "Saved!",
                                text: response.data.isAddUpdate,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                        else {
                            root.$swal({
                                title: "Error!",
                                text: response.data.isAddUpdate,
                                type: 'error',
                                icon: 'error',
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
                    .finally(() => root.loading = false);
            },
            SumOfTransaction: function (val) {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/CreateProductAccount?sum=' + val, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isAddUpdate != null) {
                            root.$swal({
                                title: "Saved!",
                                text: "Data has been added successfully",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                        else {
                            root.$swal({
                                title: "Error!",
                                text: "Your Color Name  Already Exist!",
                                type: 'error',
                                icon: 'error',
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
                    .finally(() => root.loading = false);
            },
            ContactDisplayName: function (val, val2, val3,documentType) {
                 
                var root = this;
                this.loading = true;
                var token = '';
                 
                if (token == '') {
                     
                    token = localStorage.getItem('token');
                }
                root.$https.get('Contact/ChangeCashCustomerToContact?changeDisplayName=' + val + '&cashCustomerToContact=' + val2 + '&invoiceChange=' + val3+ '&documentType=' + documentType, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isAddUpdate != null) {
                            root.$swal({
                                title: "Saved!",
                                text: "Data has been added successfully",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                        else {
                            root.$swal({
                                title: "Error!",
                                text: "Something Went Wrong!",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
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
                    .finally(() => root.loading = false);
            },
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.GetAboutUsDetail();

        }
    }
</script>