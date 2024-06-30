<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('FinancialYear.FinancialYearClosing') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                    <li class="breadcrumb-item active">{{ $t('FinancialYear.FinancialYearClosing') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/FinancialYearClosing')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('FinancialYear.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row mb-2">
                        <div class="col-sm-2" v-for="(period, index) in closingTypePeriodList" :key="index+'month'">
                            <div class="d-grid gap-2 form-group" v-bind:class="period.isSelect? 'bg-warning':'' ">
                                <button v-on:click="GetAccountBalnce(period)" type="button" class="btn" v-bind:class="period.isClose? 'btn-outline-danger':'btn-outline-primary' ">{{period.name}} <br /> <small>{{getDate(period.fromDate)}} - {{getDate(period.toDate)}}</small> </button>
                            </div>
                        </div>
                    </div>
                    <table class="table mb-0">
                        <thead class="thead-light table-hover">
                            <tr>
                                <th style="width:5%;">
                                    #
                                </th>
                                <th style="width:35%;">
                                    {{ $t('FinancialYear.PeriodName') }}
                                </th>
                                <th class="text-end" style="width: 20%;">
                                    Debit
                                </th>
                                <th class="text-end" style="width: 20%;">
                                    Credit
                                </th>
                                <th class="text-end" style="width: 20%;">
                                    Balance
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <template v-for="(costcenter, index) in closingPeriod.costCenterList" :key="index+'costcenter'">
                                <tr>
                                    <td colspan="6"><h3>{{ costcenter.name }}</h3> </td>
                                </tr>
                                <tr v-for="(period, yearInd) in costcenter.accountList" :key="yearInd">
                                    <td>{{ yearInd + 1 }}</td>
                                    <td>{{ period.name }}</td>
                                    <td class="text-end">
                                        {{$roundOffFilter(period.debit)}}
                                    </td>
                                    <td class="text-end">
                                        {{$roundOffFilter(period.credit)}}
                                    </td>
                                    <td class="text-end">
                                        {{$roundOffFilter(period.balance) }}
                                    </td>
                                </tr>
                            </template>
                        </tbody>
                    </table>
                </div>
                <!--end card-body-->
            </div>

            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import moment from "moment";

    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
        data: function () {

            return {
                loading: false,
                rander: 0,
                lang: '',
                closingTypePeriodList: [],

                closingPeriod: {
                    id: '00000000-0000-0000-0000-000000000000',
                    closingType: '',
                    periodName: '',
                    fromDate: '',
                    toDate: '',
                    description: '',
                    isClose: false,
                    costCenterList: [],
                },
            }
        },
        methods: {
            getDate: function (date) {
                return moment(date).format('l');
            },

            GotoPage: function (link) {
                this.$router.push({ path: link });
            },

            GetSetting: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/FinancialYear/GetFinancialYearDetail', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.closingPeriod.closingType = response.data.closingType;
                            root.closingTypePeriodList = response.data.closingTypePeriodList;
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
                    .finally(() => root.loading = false);
            },


            GetAccountBalnce: function (item) {
                var root = this;
                var token = localStorage.getItem('token');
                debugger; //eslint-disable-line
                if (item.isSelect) {
                    item.isSelect = false;
                }
                else {
                    item.isSelect = true;
                }
                console.log(root.closingTypePeriodList);
                this.loading = true;
                this.$https.post('/FinancialYear/MultiPeriodAccountBalanceList', root.closingTypePeriodList, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.closingPeriod.costCenterList = response.data;
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

                        root.loading = false;
                    }).finally(() => root.loading = false);


            },
        },
        created: function () {
            this.lang = localStorage.getItem('');
            this.GetSetting();
        },
    }
</script>