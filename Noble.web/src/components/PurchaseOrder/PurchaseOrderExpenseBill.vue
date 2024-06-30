<template>
    <modal :show="show" :modalLarge="true">
        <div class="row" v-if=" isValid('CanAddOrderExpense')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="col-md-12  ml-auto mr-auto" v-bind:style="$i18n.locale == 'ar' ? languageChange('en') : languageChange('ar')">
                <div class="card">
                    <div class="card-body">
                        <div class="overlay">
                            <div class="row align-items-center h-100 justify-content-sm-center">
                                <div class="loadingio-spinner-dual-ball-44dlc48bacw">
                                    <div class="ldio-m86dw9oanea">
                                        <div> </div> <div> </div> <div> </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-header">
                            <h5 class="modal-title">{{ $t('PurchaseOrderExpenseBill.AddAdvanceExpenseBill') }}</h5>
                        </div>
                        <div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-xm-12 col-sm-12 col-md-12 col-lg-12">
                                        <label>  {{ $t('PurchaseOrderExpenseBill.Bills') }} : <span class="text-danger"> *</span></label>
                                        <BillsDropdown v-model="billsId" :modelValue="billsId" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer justify-content-right">
                            <button type="button" class="btn btn-primary  ml-2" v-if=" isValid('CanAddOrderExpense')" v-on:click="SaveVoucher()"><i class="far fa-save"></i> {{ $t('PurchaseOrderExpenseBill.SaveAndPost') }}</button>
                            <button class="btn btn-danger " v-on:click="onCancel">  {{ $t('PurchaseOrderExpenseBill.Cancel') }}</button>
                        </div>
                    </div>
                </div>
                <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            </div>
        </div>
        <div v-else> <acessdenied></acessdenied></div>
    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        name: "PurchaseOrderBillExpense",
        mixins: [clickMixin],
        props: ['show', 'purchaseId', 'isInvoice'],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                billsId: '',
                loading: false,
            }
        },

        validations() {
            return {
                paymentVoucher: {
                    billsId: {
                        required
                    },
                }
                }
        },
        methods: {

            languageChange: function (lan) {
                if (this.language == lan) {

                    if (this.paymentVoucher.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addPaymentVoucherformName?formName=' + this.formName);
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

            SaveVoucher: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.get('/PaymentVoucher/AdvanceBillAttachment?billsId=' + this.billsId + '&purchaseId=' + this.purchaseId + '&isInvoice=' + this.isInvoice, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.id != '00000000-0000-0000-0000-000000000000') {

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: response.data.isAddUpdate,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 2000,
                                timerProgressBar: true,
                                confirmButtonClass: "btn btn-success",
                                buttonsStyling: false
                            });
                            root.onCancel();
                        }

                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                type: 'error',
                                icon: 'error',
                                title: root.$t('PurchaseOrderExpenseBill.Error'),
                                text: "Error",
                                confirmButtonClass: "btn btn-danger",
                                showConfirmButton: true,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
            onCancel: function () {
                this.$emit('close');
            },
        },
        created: function () {
            this.language = this.$i18n.locale;
        }
    }
</script>