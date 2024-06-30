<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel">{{
                        $t('LoanRecoveryPayment.LoanRecoveryPayment')
                }}-{{ loanDetail.description }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="row">
                                <div class="col">
                                    <h4 class="page-title">{{ loanDetail.employeeName }}</h4>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="input-group">
                                        <button class="btn btn-secondary" type="button" id="button-addon1">
                                            <i>{{ currency }}</i></button>
                                        <input v-model="loanDetail.remainingLoan" type="text" class="form-control"
                                            disabled aria-label="Example text with button addon"
                                            style="border: 1px dashed #1761fd;" aria-describedby="button-addon1">
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-6" :key="dateRender">
                        <label>{{ $t('LoanRecoveryPayment.PaymentDate') }} :<span class="text-danger"> *</span></label>
                        <datepicker v-model="loanRecovery.paymentDate" />
                        <span style="color:#777">{{ $t('LoanRecoveryPayment.LoanDate') }} -
                            {{ loanDetail.loanDate }}</span>
                    </div>
                    <div class="form-group has-label col-sm-6">

                        <label class="text  font-weight-bolder">{{ $t('LoanRecoveryPayment.Amount') }} : <span
                                class="text-danger"> *</span></label>
                        <div class="input-group">
                            <button class="btn btn-secondary" type="button" id="button-addon1">
                                <i>{{ currency }}</i></button>
                            <input v-model="loanRecovery.amount" type="number" class="form-control"
                                @focus="$event.target.select()" aria-label="Example text with button addon"
                                aria-describedby="button-addon1">
                        </div>
                    </div>

                    <div class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': v$.loanRecovery.comments.$error }">
                        <label class="text  font-weight-bolder">{{ $t('LoanRecoveryPayment.Comments') }} :<span
                                class="text-danger"> *</span> </label>
                        <textarea class="form-control" v-model="v$.loanRecovery.comments.$model" type="text" />
                    </div>

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveLoanRecovery"
                    v-bind:disabled="v$.loanRecovery.$invalid">{{ $t('LoanRecoveryPayment.Save') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close(false)">{{
                        $t('LoanRecoveryPayment.Cancel')
                }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import { required, minValue } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

import moment from 'moment';

export default {
    mixins: [clickMixin],
    props: ['show', 'newLoanRecovery', 'newloanDetail'],
    components: {
        Loading
    },
     setup() {
            return { v$: useVuelidate() }
        },
    data: function () {
        return {
            loanRecovery: this.newLoanRecovery,
            loanDetail: this.newloanDetail,
            currency: '',
            arabic: '',
            english: '',
            render: 0,
            dateRender: 0,
            loading: false,
        }
    },
    validations() {
      return{
          loanRecovery: {
            paymentDate: {
                required
            },

            amount: {
                required,
                minValue: minValue(1),

            },


            comments: {
                required

            }
        }
      }
    },
    methods: {
        close: function (x) {

            if (x == true)
                this.$emit('close', x);
            else {
                this.$emit('close', false);
            }

        },
        SaveLoanRecovery: function () {
            var root = this;
            this.loading = true;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            if (this.loanRecovery.amount > this.loanDetail.remainingLoan) {
                root.$swal({
                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                    text: "Amount Increase Loan Amount",
                    type: 'error',
                    icon: 'error',
                    showConfirmButton: false,
                    timer: 1500,
                    timerProgressBar: true,
                });
                root.loading = false;
                return;
            }
            this.loanRecovery.remainingLoan = parseFloat(this.loanDetail.remainingLoan) - parseFloat(this.loanRecovery.amount);
            this.$https.post('/Payroll/SaveLoanRecovery', this.loanRecovery, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });

                            root.close(true);
                        }
                        else {

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close(true);

                        }
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your LoanRecovery Name  Already Exist!",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                })
                .catch(error => {
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
                .finally(() => root.loading = false);
        }
    },
    mounted: function () {
        if (this.loanDetail.loanType == 1) {
            this.loanRecovery.amount = this.loanDetail.installmentBaseSalary;
        }
        this.loanRecovery.paymentDate = moment().format('llll');
        this.dateRender++;


        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.currency = localStorage.getItem('currency');


    }
}
</script>



