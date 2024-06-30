<template>
    <div class="row">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('UserDefineFlow.UserDefineFlow') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('UserDefineFlow.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('UserDefineFlow.UserDefineFlow') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Terminal.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6" v-if="(isValid('CanDraftServiceQuotation') || isValid('CanViewServiceQuotation')) || (isValid('CanDraftQuotation') || isValid('CanViewQuotation'))">
                                    <div class="form-group">
                                        <div class="checkbox form-check-inline mx-2">
                                            <input type="checkbox" id="inlineCheckbox1" v-model="userDefineFlow.quotationToSaleOrder" :key="render">
                                            <label for="inlineCheckbox1">{{ $t('UserDefineFlow.QuotationToSaleOrder') }}</label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6" v-if="(isValid('CanDraftServiceQuotation') || isValid('CanViewServiceQuotation')) || (isValid('CanDraftQuotation') || isValid('CanViewQuotation'))">
                                    <div class="form-group">
                                        <div class="checkbox form-check-inline mx-2">
                                            <input type="checkbox" id="inlineCheckbox2" v-model="userDefineFlow.quotationToSaleInvoice" :key="render">
                                            <label for="inlineCheckbox2">{{ $t('UserDefineFlow.QuotationToSaleInvoice') }}</label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6" v-if="(isValid('CanDraftServiceQuotation') || isValid('CanViewServiceQuotation')) || (isValid('CanDraftQuotation') || isValid('CanViewQuotation'))">
                                    <div class="form-group">
                                        <div class="checkbox form-check-inline mx-2">
                                            <input type="checkbox" id="inlineCheckbox3" v-model="userDefineFlow.partiallyQuotation" :key="render">
                                            <label for="inlineCheckbox3">{{ $t('UserDefineFlow.PartiallyQuotation') }}</label>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6" v-if="(isValid('CanViewSaleOrder') || isValid('CanDraftSaleOrder')) || (isValid('CanViewServiceSaleOrder') || isValid('CanDraftServiceSaleOrder'))">
                                    <div class="form-group">
                                        <div class="checkbox form-check-inline mx-2">
                                            <input type="checkbox" id="inlineCheckbox4" v-model="userDefineFlow.partiallySaleOrder" :key="render">
                                            <label for="inlineCheckbox4">{{ $t('UserDefineFlow.PartiallySaleOrder') }}</label>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div v-if="!loading" class="card-footer">
                            <div class="row">
                                <div class="button-items">
                                    <button type="button" class="btn btn-primary btn-sm" v-on:click="SaveUserDefineFlow"> {{ $t('UserDefineFlow.Save') }}</button>
                                    <button type="button" class="btn btn-danger btn-sm " v-on:click="GotoPage('/StartScreen')">{{ $t('UserDefineFlow.Cancel') }}</button>
                                </div>
                            </div>
                        </div>
                        <div v-else>
                            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                        </div>
                    </div>
                </div>
            </div>
               
            </div>

        </div>

</template>


<script>
    import clickMixin from '@/Mixins/clickMixin';
    
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                userDefineFlow: {
                    companyId: '',
                    quotationToSaleOrder: false,
                    quotationToSaleInvoice: false,
                    partiallyQuotation: false,
                    partiallySaleOrder: false,
                },
                loading: false,
                render: false,
            }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            SaveUserDefineFlow: function () {
                this.loading = true;
                var root = this;
                this.userDefineFlow.companyId = localStorage.getItem('CompanyID');

                this.$https.post('/Company/UserDefineFlowSave', this.userDefineFlow, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
                    .then(function (response) {

                        if (response.data != null) {

                            root.$swal({
                               title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.EditUserDefineFlow();
                            root.loading = false;
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },


            EditUserDefineFlow: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/UserDefineFlowEdit?companyId=' + localStorage.getItem('CompanyID'), { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            
                            root.userDefineFlow.quotationToSaleOrder = response.data.quotationToSaleOrder;
                            root.userDefineFlow.quotationToSaleInvoice = response.data.quotationToSaleInvoice;
                            root.userDefineFlow.partiallyQuotation = response.data.partiallyQuotation;
                            root.userDefineFlow.partiallySaleOrder = response.data.partiallySaleOrder;

                            localStorage.setItem('quotationToSaleOrder', response.data.quotationToSaleOrder);
                            localStorage.setItem('quotationToSaleInvoice', response.data.quotationToSaleInvoice);
                            localStorage.setItem('partiallyQuotation', response.data.partiallyQuotation);
                            localStorage.setItem('partiallySaleOrder', response.data.partiallySaleOrder);

                            root.render++;
                            root.loading = false;
                        }
                        else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });

            }
        },
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
            this.EditUserDefineFlow();
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            
        }
    }
</script>