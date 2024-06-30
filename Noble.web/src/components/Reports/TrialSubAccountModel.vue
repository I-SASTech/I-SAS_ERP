<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel">Account of Cost-Center</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="table-responsive" data-pattern="priority-columns">
                        <table class="table mb-0 table-striped">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>


                                    <th>
                                        {{ $t('TrialBalanceReport.Name') }}
                                    </th>
                                    <th>
                                        {{ $t('TrialBalanceReport.Debit') }}
                                    </th>
                                    <th>
                                        {{ $t('TrialBalanceReport.Credit') }}
                                    </th>
                                    <th>
                                        Total
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(account, index) in subAccount" v-bind:key="account.Code">
                                    <td>
                                        {{ index + 1 }}
                                    </td>



                                    <td>
                                        {{ account.accountName }}

                                    </td>
                                    <td>{{ Number(parseFloat(account.debit).toFixed(2)).toLocaleString() }}</td>
                                    <td>{{ Number(Math.abs(parseFloat(account.credit).toFixed(2))).toLocaleString() }}</td>
                                    <td>{{ Number(Math.abs(parseFloat(account.debit - account.credit).toFixed(2))).toLocaleString() }}
                                    </td>

                                </tr>
                                <tr>
                                    <td style="background-color:white"></td>
                                </tr>

                                <tr style="margin-top:20px">
                                    <td></td>
                                    <td><b> {{ $t('TrialBalanceReport.Total') }} </b></td>
                                    <td>
                                        <b>{{ Number(parseFloat(totalDebit).toFixed(2)).toLocaleString() }}</b>
                                    </td>
                                    <td>
                                        <b>{{ Number(parseFloat(totalCredit).toFixed(2)).toLocaleString() }}</b>
                                    </td>
                                    <td>
                                        <b>{{ Number(parseFloat(totalCredit + totalDebit).toFixed(2)).toLocaleString() }}</b>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddCity.btnClear')
                }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'



export default {
    props: ['show', 'subAccount', 'type'],
    mixins: [clickMixin],
    components: {
        
    },
    data: function () {
        return {
            render: 0,
            totalDebit: 0,
            totalCredit: 0,
            loading: false
        }
    },
    methods: {
        close: function () {
            this.$emit('close');
        },
    },
    mounted: function () {
        var root = this;
        root.totalDebit = root.subAccount.reduce(function (prev, item) {
            return prev + Number(item.debit);

        }, 0);
        root.totalCredit = Math.abs(root.subAccount.reduce(function (prev, item) {
            return Math.abs(prev + Number(item.credit));

        }, 0));
    }
}
</script>
