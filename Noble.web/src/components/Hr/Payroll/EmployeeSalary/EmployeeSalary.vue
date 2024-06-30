<template>
     <div class="row" v-if="isValid('CanViewEmployeeSalary')">

<div class="col-lg-12">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">{{ $t('EmployeeSalary.EmployeeSalary') }}</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('EmployeeSalary.Home') }}</a></li>
                            <li class="breadcrumb-item active">{{ $t('EmployeeSalary.EmployeeSalary') }}</li>
                        </ol>
                    </div>
                    <div class="col-auto align-self-center">
                        <a v-if="isValid('CanAddEmployeeSalary')" v-on:click="AddSalaryTemplate" href="javascript:void(0);"
                            class="btn btn-sm btn-outline-primary mx-1">
                            <i class="align-self-center icon-xs ti-plus"></i>
                            {{ $t('EmployeeSalary.AddNew') }}
                        </a>
                        <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                            class="btn btn-sm btn-outline-danger">
                            {{ $t('EmployeeSalary.Close') }}
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
                <input v-model="search" type="text" class="form-control" :placeholder="$t('EmployeeSalary.Search')"
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
                                            {{ $t('EmployeeSalary.EmployeeEnglish') }}
                                        </th>
                                        <th>
                                            {{ $t('EmployeeSalary.EmployeeArabic') }}
                                        </th>
                                        <th>
                                            {{ $t('EmployeeSalary.SalaryType') }}
                                        </th>
                                        <th>
                                            {{ $t('EmployeeSalary.BaseSalary') }}
                                        </th>
                                        <th>
                                            {{ $t('EmployeeSalary.Allowance') }}
                                        </th>
                                        <th>
                                            {{ $t('EmployeeSalary.GrossSalary') }}
                                        </th>
                                        <th>
                                            {{ $t('EmployeeSalary.Deduction') }}
                                        </th>
                                        <th>
                                            {{ $t('EmployeeSalary.CONTRIBUTION') }}
                                        </th>
                                        <th>
                                            {{ $t('EmployeeSalary.IncomeTax') }}
                                        </th>
                                        <th>
                                            {{ $t('EmployeeSalary.NetSalary') }}
                                        </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(salaryTemplate ,index) in salaryTemplateList" v-bind:key="salaryTemplate.id">
                                        <td v-if="currentPage === 1">
                                            {{index+1}}
                                        </td>
                                        <td v-else>
                                            {{((currentPage*10)-10) +(index+1)}}
                                        </td>
                                        <td v-if="isValid('CanEditEmployeeSalary')">
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="EditSalaryTemplate(salaryTemplate.id)">{{salaryTemplate.employeeEnglishName}}</a>
                                            </strong>
                                        </td>
                                        <td v-else>
                                            <strong>
                                                {{salaryTemplate.employeeEnglishName}}
                                            </strong>
                                        </td>
                                        <td>
                                            {{salaryTemplate.employeeArabicName}}
                                        </td>
                                        <td>
                                            {{salaryTemplate.salaryType}}
                                        </td>
                                        <td>
                                            {{currency}} {{ parseFloat(salaryTemplate.baseSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            {{currency}} {{ parseFloat(salaryTemplate.allowanceAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            {{currency}} {{ parseFloat(salaryTemplate.grossSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            {{currency}} {{ parseFloat(salaryTemplate.deductionAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            {{currency}} {{ parseFloat(salaryTemplate.contributionAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            {{currency}} {{ parseFloat(salaryTemplate.incomeTaxAmount).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td>
                                            {{currency}} {{ parseFloat(salaryTemplate.netSalary).toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
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
            }
        },
        watch: {
            search: function () {
                this.GetSalaryTemplateData();
            }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({path: link});
            },
            AddSalaryTemplate: function () {
                this.$router.push('/AddEmployeeSalary');

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
                
                root.$https.get('Payroll/EmployeeSalaryList?searchTerm=' + this.search + '&pageNumber=' + this.currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
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
                root.$https.get('/Payroll/EmployeeSalaryDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.$router.push({
                                path: '/AddEmployeeSalary',                                
                                query: { data: response.data }
                            })
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
            this.GetSalaryTemplateData();

        }
    }
</script>