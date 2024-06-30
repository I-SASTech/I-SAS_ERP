<template>
    <div class="row" v-if="isValid('CanViewRunPayroll')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('RunPayroll.RunPayroll') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('RunPayroll.Home')
                                    }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('RunPayroll.RunPayroll') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddRunPayroll') || isValid('CanDraftRunPayroll')"
                                    v-on:click="AddSalaryTemplate" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('RunPayroll.RunNewPayroll') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('RunPayroll.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-8" style="padding-top:20px">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1"><i
                                class="fas fa-search"></i></button>
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('RunPayroll.Search')"
                            aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                    <div class=" col-lg-4 mt-1">

                        <button v-on:click="search22(true)" :disabled ="!search" type="button" class="btn btn-outline-primary mt-3">
                            {{ $t('Sale.ApplyFilter') }}
                        </button>
                        <button v-on:click="clearData(false)" :disabled ="!search" type="button" class="btn btn-outline-primary mx-2 mt-3">
                            {{ $t('Sale.ClearFilter') }}
                        </button>

                    </div>
                </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th>
                                        {{ $t('RunPayroll.PayPlan') }}
                                    </th>

                                    <th>
                                        {{ $t('RunPayroll.Status') }}
                                    </th>
                                    <th>
                                        {{ $t('RunPayroll.PayPeriod') }}
                                    </th>
                                    <th>
                                        {{ $t('RunPayroll.PayDate') }}
                                    </th>
                                    <th>
                                        {{ $t('RunPayroll.TotalEmployees') }}
                                    </th>
                                    <th>
                                        {{ $t('RunPayroll.TotalTax') }}
                                    </th>
                                    <th>
                                        {{ $t('RunPayroll.NetSalary') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('RunPayroll.Print') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(salaryTemplate, index) in salaryTemplateList" v-bind:key="salaryTemplate.id">
                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>
                                    <td>
                                        <strong v-if="!salaryTemplate.status && isValid('CanEditOpenRunPayroll')">
                                            <a href="javascript:void(0)"
                                                v-on:click="EditSalaryTemplate(salaryTemplate.id)">{{ salaryTemplate.payPlan }}</a>
                                        </strong>
                                        <strong v-else>
                                            {{ salaryTemplate.payPlan }}
                                        </strong>
                                    </td>
                                    <td>
                                        <span class="badge badge-danger" v-if="salaryTemplate.status"> Close</span>
                                        <span class="badge badge-success" v-else>Open</span>
                                    </td>
                                    <td>
                                        {{ salaryTemplate.payPeriod }}
                                    </td>
                                    <td>
                                        {{ salaryTemplate.payDate }}
                                    </td>
                                    <td>
                                        {{ salaryTemplate.totalEmployees }}
                                    </td>
                                    <td>
                                        {{ currency }} {{ salaryTemplate.taxAmount }}
                                    </td>
                                    <td>
                                        {{ currency }} {{ salaryTemplate.netSalary }}
                                    </td>
                                    <td class="text-center">
                                        <a href="javascript:void(0);" v-on:click="GetCashSalary(salaryTemplate.id, 'Bank')"
                                            class="me-2" v-if="isValid('CanViewBankRunPayroll')"><i
                                                class="fas fa-university text-secondary font-16"></i></a>
                                        <a href="javascript:void(0);" v-on:click="GetCashSalary(salaryTemplate.id, 'Cash')"
                                            v-if="isValid('CanViewCashRunPayroll')"><i
                                                class="fas fa-money-bill-alt text-secondary font-16"></i></a>
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
                                {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of')
                                }}
                                {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        </div>
                        <div class=" col-lg-6">
                            <div class="float-end" v-on:click="getPage()">
                                <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                    :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                    :next-text="$t('Table.Next')" :last-text="$t('Table.Last')"></b-pagination>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <run-payroll-model :show="show" v-if="show" :runPayroll="runPayroll" @close="show = false" />
        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>


<script>
import clickMixin from '@/Mixins/clickMixin'
export default {
    mixins: [clickMixin],
    data: function () {
        return {
            currency: '',
            searchQuery: '',
            salaryTemplateList: [],
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            arabic: '',
            english: '',
            show: false,
            runPayroll: ''
        }
    },
    watch: {
        // search: function () {
        //     this.GetSalaryTemplateData();
        // }
    },
    methods: {
        search22: function () {
            this.GetSalaryTemplateData(this.search, this.currentPage);
        },

        clearData: function () {
            this.search = "";
            this.GetSalaryTemplateData(this.search, this.currentPage);

        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        AddSalaryTemplate: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Payroll/ChecKPaySchedule', { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data > 0) {
                        root.$router.push('/AddRunPayroll');
                    }
                    else {
                        root.$swal({
                            title: 'Warning!',
                            text: 'Please publish the opened payrolls before running the next payroll.',
                            type: 'warning',
                            confirmButtonClass: "btn btn-warning",
                            icon: 'warning',
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });


        },

        getPage: function () {
            this.GetSalaryTemplateData();
        },

        GetSalaryTemplateData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('Payroll/RunPayrollList?searchTerm=' + this.search + '&pageNumber=' + this.currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.salaryTemplateList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },

        EditSalaryTemplate: function (Id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Payroll/RunPayrollDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddRunPayroll',
                            query: { data: '',
                                Add: false, }
                        })
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });

        },

        GetCashSalary: function (id, prop) {
            var root = this;

            root.$https.get('/Payroll/RunPayrollDetailPrint?id=' + id + '&prop=' + prop, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
                .then(function (response) {
                    if (response.data) {
                        root.runPayroll = response.data;
                        root.show = true;
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
    },

    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.currency = localStorage.getItem('currency');
        this.GetSalaryTemplateData();

    }
}
</script>