<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-lg">
            <div style="margin-bottom:0px" class="card">
                <div class="card-header">
                    <h5>{{ $t('Company.CompanyLicenseHistory') }}</h5>
                </div>
                <div class="card-body ">
                    <div class=" table-responsive">
                        <table class="table ">
                            <thead class="m-0">
                                <tr>
                                    <th>#</th>
                                    <th>{{ $t('Company.CompanyName') }} </th>
                                    <th>{{ $t('Company.LicenseType') }}</th>
                                    <th>{{ $t('Company.FromDate') }}</th>
                                    <th>{{ $t('Company.ToDate') }}</th>
                                    <th>{{ $t('Company.UserAllow') }}</th>
                                    <th>{{ $t('Company.TransactionAllow') }}</th>
                                    <th>{{ $t('Company.IsActive') }}</th>
                                    <th>{{ $t('Company.IsBlock') }}</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(company,index) in companyLicenceList" v-bind:key="index">
                                    <td>
                                        {{index+1}}
                                    </td>
                                    <td>
                                        {{companyName}}
                                    </td>
                                    <td>{{types[company.companyType - 1]}}</td>
                                    <td>{{getDate(company.fromDate)}}</td>
                                    <td>{{getDate(company.toDate)}}</td>
                                    <td>{{company.numberOfUsers}}</td>
                                    <td>{{company.numberOfTransactions}}</td>
                                    <td>{{company.isActive}}</td>
                                    <td>{{company.isBlock}}</td>
                                </tr>
                            </tbody>
                        </table>
                        <div class="modal-footer justify-content-right">
                            <button type="button" class="btn btn-secondary  mr-3 " v-on:click="$emit('close')">{{ $t('Company.Close') }}</button>

                        </div>
                    </div>
                </div>
            </div>
        </div>


    </modal>
</template>
<script>
    import moment from "moment";

    export default {
        props: ['show', 'companyLicenceList', 'companyName'],
        data: function () {
            return {
                render: 0,
                types: ['Trail', 'Base', 'Standard', 'Advanced']
            }
        },

        methods: {
            getDate: function (date) {
                return moment(date).format('DD/MM/YYYY');
            },
            close: function () {
                this.$emit('close');
            },
        },

        mounted: function () {
            this.render++;

        }
    }
</script>
