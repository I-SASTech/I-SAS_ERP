<template>
    <div class="row" v-if="isValid('CanViewExpenseType')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('ExpenseTypes.ExpenseType') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                    <li class="breadcrumb-item active">{{ $t('ExpenseTypes.ExpenseType') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddExpenseType')" v-on:click="openmodel()" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('color.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('color.Close') }}
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
                                <input v-model="search" type="text" class="form-control" :placeholder="$t('color.Search')"
                                    aria-label="Example text with button addon" aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

                            <button v-on:click="search22(true)" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" type="button" class="btn btn-outline-primary mx-2 mt-3">
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
                                    <th v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'">#</th>
                                    <th v-if="english == 'true'"
                                        v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'">
                                        {{$englishLanguage($t('ExpenseTypes.ProcessName'))   }}
                                    </th>
                                    <th v-if="arabic == 'true'"
                                        v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'">
                                        {{$arabicLanguage($t('ExpenseTypes.ProcessName'))   }}
                                    </th>
                                    <th v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'">
                                        {{ $t('ExpenseTypes.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(process, index) in processList" v-bind:key="process.id">
                                    <td v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-if="english == 'true'"
                                        v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'">
                                        <strong>
                                            <a class="font-weight-bold" v-if="isValid('CanEditExpenseType')" href="#"
                                                v-on:click="EditProcess(process.id)">{{ process.expenseTypeName }}</a>
                                            <span v-else>{{ process.expenseTypeName }}</span>
                                        </strong>
                                    </td>
                                    <td v-if="arabic == 'true'"
                                        v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'">
                                        <strong>
                                            <a class="font-weight-bold" href="#" v-if="isValid('CanEditExpenseType')"
                                                v-on:click="EditProcess(process.id)">{{ process.expenseTypeArabic }}</a>
                                            <span v-else>{{ process.expenseTypeArabic }}</span>
                                        </strong>
                                    </td>
                                    <td>
                                        <span v-if="process.status" class="badge badge-boxed  badge-outline-success">{{
                                            $t('color.Active') }}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{
                                            $t('color.De-Active') }}</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />

                    <div class="float-start">
                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }} {{ currentPage
                        }}
                            {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                            $t('Pagination.to') }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries')
    }}</span>
                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{ (currentPage * 10) -
                            9 }}
                            {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                $t('Pagination.entries') }}</span>
                    </div>
                    <div class="float-end">
                        <div class="overflow-auto" v-on:click="GetColorData()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                first-text="First" prev-text="Previous" next-text="Next" last-text="Last"></b-pagination>
                        </div>
                    </div>
                </div>
            </div>

            <addExpense-type :newProcess="newProcess" :show="show" v-if="show" @close="IsSave" :type="type" />
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
            arabic: '',
            english: '',
            searchQuery: '',
            show: false,
            processList: [],
            newProcess: {
                id: '',
                expenseTypeName: '',
                expenseTypeArabic: '',
                description: '',
                accountId: '',
                status: true
            },
            type: '',
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
        }
    },
    watch: {
        // search: function (val) {
        //     this.GetColorData(val, 1);
        // }
    },
    methods: {


        search22: function () {
    this.GetColorData(this.search, this.currentPage);
            },

            clearData: function () {
                this.search="";
                this.GetColorData(this.search, this.currentPage);

            },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        IsSave: function () {
            this.show = false;

            this.GetColorData(this.search, this.currentPage);
        },
        getPage: function () {
            this.GetColorData(this.search, this.currentPage);
        },
        openmodel: function () {
            this.newProcess = {
                id: '00000000-0000-0000-0000-000000000000',
                expenseTypeName: '',
                expenseTypeArabic: '',
                accountId: '',
                description: '',
                status: true
            }
            this.show = !this.show;
            this.type = "Add";
        },
        GetColorData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('Accounting/ExpenseTypeList?isActive=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.processList = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        EditProcess: function (Id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Accounting/ExpenseTypeDetail?id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.newProcess.id = response.data.id;
                        root.newProcess.expenseTypeName = response.data.expenseTypeName;
                        root.newProcess.expenseTypeArabic = response.data.expenseTypeArabic;
                        root.newProcess.status = response.data.status;
                        root.newProcess.accountId = response.data.accountId;
                        root.newProcess.description = response.data.description;
                        root.show = !root.show;
                        root.type = "Edit"
                    } else {
                        console.log("error: something wrong from db.");
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
        this.GetColorData(this.search, 1);
    }
}
</script>
