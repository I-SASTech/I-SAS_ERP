<template>
    <div class="row" v-if="isValid('CanViewPayRollSchedule')">

<div class="col-lg-12">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">{{ $t('PayrollSchedule.PayrollSchedule') }}</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PayrollSchedule.Home') }}</a></li>
                            <li class="breadcrumb-item active">{{ $t('PayrollSchedule.PayrollSchedule') }}</li>
                        </ol>
                    </div>
                    <div class="col-auto align-self-center">
                        <a v-if="isValid('CanAddPayRollSchedule')" v-on:click="openmodel" href="javascript:void(0);"
                            class="btn btn-sm btn-outline-primary mx-1">
                            <i class="align-self-center icon-xs ti-plus"></i>
                            {{ $t('PayrollSchedule.AddPayrollSchedule') }}
                        </a>
                        <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                            class="btn btn-sm btn-outline-danger">
                            {{ $t('PayrollSchedule.Close') }}
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-header">
            <div class="input-group">
                <button class="btn btn-secondary" type="button" id="button-addon1"><i
                        class="fas fa-search"></i></button>
                <input v-model="search" type="text" class="form-control" :placeholder="$t('PayrollSchedule.Search')"
                    aria-label="Example text with button addon" aria-describedby="button-addon1">
            </div>
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <table class="table mb-0">
                    <thead class="thead-light table-hover">
                        <tr>
                            <th>#</th>
                                        <th>
                                            {{ $t('PayrollSchedule.Name') }}
                                        </th>
                                        <th>
                                            {{ $t('PayrollSchedule.PayPeriod') }}
                                        </th>
                                        <th>
                                            {{ $t('PayrollSchedule.PeriodFirstDate') }}
                                        </th>
                                        <th>
                                            {{ $t('PayrollSchedule.PeriodLastDate') }}
                                        </th>
                                        <th>
                                            {{ $t('PayrollSchedule.PayDate') }}
                                        </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(schedule ,index) in payrollscheduleList" v-bind:key="schedule.id">
                                        <td v-if="currentPage === 1">
                                            {{index+1}}
                                        </td>
                                        <td v-else>
                                            {{((currentPage*10)-10) +(index+1)}}
                                        </td>

                                        <td v-if="isValid('CanEditPayRollSchedule')">
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="EditDeduction(schedule.id)">{{schedule.name}}</a>
                                            </strong>
                                        </td>
                                        <td v-else>
                                            <strong>
                                                {{schedule.name}}
                                            </strong>
                                        </td>
                                        <td>
                                            {{schedule.payPeriod}}
                                        </td>
                                        <td>
                                            {{getDate(schedule.payPeriodStartDate) }}
                                        </td>
                                        <td>
                                            {{getDate(schedule.payPeriodEndDate) }}
                                        </td>
                                        <td>
                                            {{getDate(schedule.payDate) }}
                                        </td>
                                    </tr>
                    </tbody>
                </table>
            </div>
            <hr />
            <div class="row">
                <div class="col-lg-6">
                    <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                    <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }}
                        {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                        {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                    <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing') }}
                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                        {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                    <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                            $t('Pagination.to')
                    }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries')
}}</span>
                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{ $t('Pagination.Showing') }}
                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                $t('Pagination.of')
                        }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                    <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }}
                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                        {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                </div>
                <div class=" col-lg-6">
                    <div class="float-end" v-on:click="GetBrandData()">
                        <b-pagination pills size="sm" v-model="currentPage"
                                              :total-rows="rowCount"
                                              :per-page="10"
                                              :first-text="$t('Table.First')"
                                              :prev-text="$t('Table.Previous')"
                                              :next-text="$t('Table.Next')"
                                              :last-text="$t('Table.Last')" ></b-pagination>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <add-payroll-schedule :newPayrollSchedule="newPayrollSchedule"
                       :show="show"
                       v-if="show"
                       @close="IsSave"
                       :type="type" />
</div>

</div>
<div v-else>
<acessdenied></acessdenied>
</div>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
                currency: '',
                searchQuery: '',
                show: false,
                payrollscheduleList: [],
                newPayrollSchedule: {
                    id: '',
                    payPeriod: '',
                    payPeriodStartDate: '',
                    name: '',
                    payPeriodEndDate: '',
                    ifPayDayFallOnHoliday: '',
                    payDate: '',
                    default: false,
                    isHourlyPay: false
                },
                type: '',
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                arabic: '',
                english: '',
            }
        },
        watch: {
            search: function () {
                this.GetDeductionData();
            }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({path: link});
            },
            getDate: function (date) {
                return moment(date).format("l");
            },
            IsSave: function () {
                this.show = false;
                this.GetDeductionData();
            },

            getPage: function () {
                this.GetDeductionData();
            },

            openmodel: function () {
                this.newPayrollSchedule = {
                    id: '00000000-0000-0000-0000-000000000000',
                    payPeriod: '',
                    payPeriodStartDate: '',
                    name: '',
                    payPeriodEndDate: '',
                    ifPayDayFallOnHoliday: '',
                    payDate: '',
                    default: false,
                    isHourlyPay: false
                }
                this.show = !this.show;
                this.type = "Add";
            },

            GetDeductionData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                root.$https.get('Payroll/PayrollScheduleList?searchTerm=' + this.search + '&pageNumber=' + this.currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.payrollscheduleList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            EditDeduction: function (Id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Payroll/PayrollScheduleDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.newPayrollSchedule.id = response.data.id;
                            root.newPayrollSchedule.payPeriod = response.data.payPeriod;
                            root.newPayrollSchedule.payPeriodStartDate = response.data.payPeriodStartDate;
                            root.newPayrollSchedule.name = response.data.name;
                            root.newPayrollSchedule.payPeriodEndDate = response.data.payPeriodEndDate;
                            root.newPayrollSchedule.ifPayDayFallOnHoliday = response.data.ifPayDayFallOnHoliday;
                            root.newPayrollSchedule.payDate = response.data.payDate;
                            root.newPayrollSchedule.default = response.data.default;
                            root.newPayrollSchedule.isHourlyPay = response.data.isHourlyPay;                            
                            
                            root.show = !root.show;
                            root.type = "Edit"
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });

            }
        },

        created: function () {
            this.$emit('update:modelValue', this.$route.name);
        },

        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.currency = localStorage.getItem('currency');
            this.GetDeductionData();

        }
    }
</script>