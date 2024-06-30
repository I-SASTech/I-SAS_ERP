<template>
    <modal :show="show" :modalLarge="true" :key="randerList">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel">{{ $t('LoanDetail.LoanDetail') }}-{{loanDetail.description}}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="col-sm-12" v-if="loanDetail.remainingLoan!=0">
                        <div class="page-title-box">
                            <div class="row">
                                <div class="col">
                                    <h4 class="page-title">{{loanDetail.employeeName }}</h4>
                                </div>
                                <div class="col-auto align-self-center">
                                    <a v-on:click="openmodel" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('LoanDetail.AddPayment') }}
                                </a>
                               
                                </div>
                            </div>
                        </div>
                    </div>
               
                <div class="table-responsive">
                    <table class="table mb-0">
                        <thead class="thead-light table-hover">
                            <tr>
                                <th>
                                    {{ $t('LoanDetail.Transaction') }}
                                </th>
                                <th>

                                    {{ $t('LoanDetail.LoanDate') }}
                                </th>
                                <th>
                                    {{ $t('LoanDetail.LoanAmount') }}
                                </th>
                                <th>
                                    {{ $t('LoanDetail.RecoveryLoanAmount') }}
                                </th>
                                <th>
                                    {{ $t('LoanDetail.Payment') }}
                                </th>
                                <th>
                                    {{ $t('LoanDetail.RecoveryBalance') }}
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    {{ $t('LoanDetail.Loan') }}
                                </td>
                                <td>
                                    {{ loanDetail.loanDate }}
                                </td>
                                <td>
                                    {{ currency }}
                                    {{
                                            parseFloat(loanDetail.loanAmount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                "$1,")
                                    }}
                                </td>
                                <td>
                                    {{ currency }}
                                    {{
                                            parseFloat(loanDetail.recoveryLoanAmount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                "$1,")
                                    }}

                                </td>
                                <td>
                                </td>

                                <td>
                                    {{ currency }}
                                    {{
                                            parseFloat(loanDetail.recoveryLoanAmount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                                "$1,")
                                    }}

                                </td>

                                <td>
                                </td>
                            </tr>
                            <tr v-for="loanPayment in loanDetail.loanPays" v-bind:key="loanPayment.id">

                                <td>
                                    {{ $t('LoanDetail.LoanPaymentCash') }}
                                </td>
                                <td>
                                    {{ loanPayment.recoveryDate }}
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    {{ currency }}{{ parseFloat(loanPayment.amount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,"$1,")}}
                                </td>

                                <td>
                                    {{ currency }}
                                    {{ parseFloat(loanPayment.remainingLoan).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,"$1,") }}
                                </td>
                            </tr>
                            <tr >
                                <td>
                                </td>
                                <td style="font-weight: bold;">
                                    {{ $t('LoanDetail.ToDateTotal') }}
                                </td>
                                <td style="font-weight: bold;">
                                    {{ currency }}
                                    {{parseFloat(loanDetail.loanAmount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,"$1,")}}
                                </td>
                                <td style="font-weight: bold;">
                                    {{ currency }}
                                    {{
                                            parseFloat(loanDetail.recoveryLoanAmount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,"$1,") }}
                                </td>
                                <td style="font-weight: bold;">
                                    {{ currency }}
                                    {{ parseFloat(TotalPayment).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,"$1,")}}
                                </td>

                                <td style="font-weight: bold;">
                                    {{ currency }}
                                    {{parseFloat(loanDetail.remainingLoan).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                </td>

                            </tr>
                        </tbody>
                    </table>
                </div>
                <loanRecovery :newLoanRecovery="newLoanRecovery" :newloanDetail="loanDetail" :show="loanRecoveryShow"
                    v-if="loanRecoveryShow" @close="RefreshList" />
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{
                        $t('LoanDetail.Cancel')
                }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>

</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
export default {
    mixins: [clickMixin],
    props: ['show', 'loanDetail'],
 components: {
            Loading
        },
    data: function () {
        return {
            currency: '',
            arabic: '',
            english: '',
            render: 0,
            randerList: 0,
            loading: false,
            loanRecoveryShow: false,
            newLoanRecovery: {
                id: '00000000-0000-0000-0000-000000000000',
                loanPaymentId: this.loanDetail.id,
                amount: 0,
                paymentDate: '',
                comments: '',
            },
        }
    },
    computed: {
        TotalPayment: function () {

            return this.loanDetail.loanPays.reduce(function (a, c) { return a + Number((c.amount) || 0) }, 0)
        },






    },

    methods: {
        openmodel: function () {
            this.newLoanRecovery = {
                id: '00000000-0000-0000-0000-000000000000',
                loanPaymentId: this.loanDetail.id,
                amount: 0,
                paymentDate: '',
                comments: '',

            }
            this.loanRecoveryShow = !this.loanRecoveryShow;
        },
        RefreshList: function (x) {
            if (x == true) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Payroll/LoanPaymentDetail?Id=' + this.loanDetail.id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {

                            root.loanDetail = response.data;
                            root.loanRecoveryShow = false;
                            root.randerList++;


                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            }
            else {
                this.loanRecoveryShow = false;

            }
        },
        RemovePayment: function (id) {


            var root = this;
            // working with IE and Chrome both
            this.$swal({
                title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟', 
                text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You will not be able to recover this!' : 'لن تتمكن من استرداد هذا!', 
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!', 
                closeOnConfirm: false,
                closeOnCancel: false
            }).then(function (result) {
                if (result) {

                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    root.$https.get('/Payroll/DeleteLoanRecovery?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data != null) {


                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                    text: 'Record Deleted Sucessfully!',
                                    type: 'success',
                                    confirmButtonClass: "btn btn-success",
                                    buttonsStyling: false
                                });
                                root.RefreshList(true);
                            }
                        },
                            function () {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                    type: 'error',
                                    confirmButtonClass: "btn btn-danger",
                                    buttonsStyling: false
                                });
                            });
                }
                else {
                    this.$swal((this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!', (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Your file is still intact!' : 'ملفك لا يزال سليما!', (this.$i18n.locale == 'en' || root.isLeftToRight()) ? 'info' : 'معلومات');
                }
            });
        },



        close: function () {
            this.$emit('close');
        },

    },
    mounted: function () {

        this.currency = localStorage.getItem('currency');

    }
}
</script>

