<template>
    <div class="row" v-if="isValid('CanViewSalaryTaxSlab')">

<div class="col-lg-12">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">{{ $t('SalaryTaxSlab.SalaryTaxSlab') }}</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('SalaryTaxSlab.Home') }}</a></li>
                            <li class="breadcrumb-item active">{{ $t('SalaryTaxSlab.SalaryTaxSlab') }}</li>
                        </ol>
                    </div>
                    <div class="col-auto align-self-center">
                        <a v-if="isValid('CanAddSalaryTaxSlab')" v-on:click="AddSalaryTemplate" href="javascript:void(0);"
                            class="btn btn-sm btn-outline-primary mx-1">
                            <i class="align-self-center icon-xs ti-plus"></i>
                            {{ $t('SalaryTaxSlab.AddNew') }}
                        </a>
                        <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                            class="btn btn-sm btn-outline-danger">
                            {{ $t('SalaryTaxSlab.Close') }}
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
                <input v-model="search" type="text" class="form-control" :placeholder="$t('SalaryTaxSlab.Search')"
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
                                            {{ $t('SalaryTaxSlab.FromDate') }}
                                        </th>
                                        <th>
                                            {{ $t('SalaryTaxSlab.ToDate') }}
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
                                        <td v-if="isValid('CanEditSalaryTaxSlab')">
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="EditSalaryTemplate(salaryTemplate.id)">{{getDate(salaryTemplate.fromDate)}}</a>
                                            </strong>
                                        </td>
                                        <td v-else>
                                            <strong>
                                                {{getDate(salaryTemplate.fromDate)}}
                                            </strong>
                                        </td>
                                        <td>
                                            {{getDate(salaryTemplate.toDate)}}
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
                    <div class=" float-end" v-on:click="GetBrandData()">
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
    import moment from 'moment'
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
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
            getDate: function (x) {
                return moment(x).format('l');
            },
            AddSalaryTemplate: function () {
              //  this.$router.push('/AddSalaryTaxSlab');
                this.$router.push({
                                path: '/AddSalaryTaxSlab',
                                query: {
                                Add: true, }
                            })

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
                
                root.$https.get('Payroll/SalaryTaxSlabList?searchTerm=' + this.search + '&pageNumber=' + this.currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
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
                root.$https.get('/Payroll/SalaryTaxSlabDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/AddSalaryTaxSlab',
                                query: { data: '',
                                Add: false, }
                            })



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
            this.GetSalaryTemplateData();

        }
    }
</script>