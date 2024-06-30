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
                                <a v-if="closingPeriod.periodName!='' && !closingPeriod.isClose" v-on:click="SaveClosingPeriod()" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    Save
                                </a>
                                
                                <a v-on:click="GotoPage('/FinancialYearClosingView')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    View
                                </a>

                                <a v-on:click="GotoPage('/FinancialYear')" href="javascript:void(0);"
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
                            <div class="d-grid gap-2 form-group" v-bind:class="period.name==closingPeriod.periodName && period.fromDate==closingPeriod.fromDate? 'bg-warning':'' ">
                                <button v-on:click="GetAccountBalnce(period.fromDate, period.toDate, period.name, period.isClose)" type="button" class="btn" v-bind:class="period.isClose? 'btn-outline-danger':'btn-outline-primary' ">{{period.name}} <br /> <small>{{getDate(period.fromDate)}} - {{getDate(period.toDate)}}</small> </button>
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
                                <th class="text-end" style="width: 15%;">
                                    Previous Balance
                                </th>
                                <th class="text-end" style="width: 15%;">
                                    Debit
                                </th>
                                <th class="text-end" style="width: 15%;">
                                    Credit
                                </th>
                                <th class="text-end" style="width: 15%;">
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
                                        {{$roundOffFilter(period.previousBalance)}}
                                    </td>
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

            SaveClosingPeriod: function () {
                var root = this;
                debugger; //eslint-disable-line

                const fromDate = new Date(this.closingPeriod.fromDate);
                const toDate = new Date(this.closingPeriod.toDate);
                const currentDate = new Date();
                if (fromDate <= currentDate && toDate >= currentDate) {
                    root.$swal.fire({
                        title: "Type reason early closing financial year",
                        input: "text",
                        inputAttributes: {
                            autocapitalize: "off"
                        },
                        showCancelButton: true,
                        confirmButtonText: "Save",
                        showLoaderOnConfirm: true,
                        preConfirm: async (login) => {
                            try {
                                root.loading = true;
                                var token = localStorage.getItem('token');
                                root.closingPeriod.description = login;
                                root.$https.post('/FinancialYear/SaveFinancialYearClosingBalance', root.closingPeriod, { headers: { "Authorization": `Bearer ${token}` } })
                                    .then(function (response) {
                                        if (response.data.id != "00000000-0000-0000-0000-000000000000") {
                                            root.$swal({
                                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                                type: 'success',
                                                icon: 'success',
                                                showConfirmButton: false,
                                                timer: 1500,
                                                timerProgressBar: true,
                                            });
                                            root.$router.go();
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
                                    }).finally(() => root.loading = false);
                            } catch (error) {
                                root.$swal.showValidationMessage(` Request failed: ${error} `);
                            }
                        },
                        allowOutsideClick: () => !root.$swal.isLoading()
                    });
                }
                else {
                    root.loading = true;
                    var token = localStorage.getItem('token');
                    root.$https.post('/FinancialYear/SaveFinancialYearClosingBalance', root.closingPeriod, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data.id != "00000000-0000-0000-0000-000000000000") {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.$router.go();
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
                        }).finally(() => root.loading = false);
                }
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

            ReOpenPeriod: function (data) {
                var root = this;
                
                this.$swal({
                    icon: 'warning',
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟',
                    text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure You want to re-open it!' : 'هل أنت متأكد أنك تريد إعادة فتحه!',
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, open it!' : 'نعم، افتحه!',
                    closeOnConfirm: false,
                    closeOnCancel: false
                }).then(function (result) {
                    if (result.isConfirmed) {

                        root.loading = true;
                        var token = localStorage.getItem('token');

                        root.$https
                            .post('/FinancialYear/ReOpenFinancialYear', data, { headers: { "Authorization": `Bearer ${token}` } })
                            .then(function (response) {
                                root.loading = false;
                                if (response.data.id != '00000000-0000-0000-0000-000000000000') {                                    
                                    root.$swal({
                                        icon: 'success',
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                        text: response.data.isAddUpdate,
                                        type: 'success',
                                        confirmButtonClass: "btn btn-success",
                                        buttonsStyling: false
                                    });
                                    root.$router.go();
                                }
                                else {
                                    root.$swal({
                                        icon: 'error',
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                        text: response.data.isAddUpdate,
                                        type: 'error',
                                        confirmButtonClass: "btn btn-danger",
                                        buttonsStyling: false
                                    });
                                }
                            },
                                function () {
                                    root.loading = false;
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                        type: 'error',
                                        confirmButtonClass: "btn btn-danger",
                                        buttonsStyling: false
                                    });
                                });
                    }
                });
            },

            GetAccountBalnce: function (fromDate, toDate, name, isClose) {
                var root = this;
                var token = localStorage.getItem('token');

                this.closingPeriod.isClose = isClose;
                this.closingPeriod.periodName = name;
                this.closingPeriod.fromDate = fromDate;
                this.closingPeriod.toDate = toDate;

                if (isClose) {
                    this.ReOpenPeriod(this.closingPeriod);
                }
                else {
                    this.loading = true;
                    this.$https.get('/FinancialYear/GetAccountBalanceList?fromDate=' + fromDate + '&toDate=' + toDate, { headers: { "Authorization": `Bearer ${token}` } })
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
                }

            },
        },
        created: function () {
            this.lang = localStorage.getItem('');
            this.GetSetting();
        },
    }
</script>