<template>
    <div class="row">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto"
            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Process.Process') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('ProductionBatch.Home')
                                    }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Process.Process') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="AddProcess" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('ProductionBatch.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('ProductionBatch.Close') }}
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
                                <input v-model="search" type="text" class="form-control" :placeholder="$t('Process.Search')"
                                    aria-label="Example text with button addon" aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1">

                            <button v-on:click="search22(true)" :disabled="!search" type="button"
                                class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled="!search" type="button"
                                class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">

                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                            <table class="table table-striped table-hover table_list_bg">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>
                                        <th class="text-center">
                                            {{ $t('Process.Code') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('Process.Date') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('Process.Name') }}
                                        </th>

                                        <th class="text-center">
                                            {{ $t('Process.Description') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">
                                    <tr v-for="(process, index) in processlist" v-bind:key="process.id">
                                        <td v-if="currentPage === 1">
                                            {{ index + 1 }}
                                        </td>
                                        <td v-else>
                                            {{ ((currentPage * 10) - 10) + (index + 1) }}
                                        </td>

                                        <td class="text-center">
                                            <strong>
                                                <a href="javascript:void(0)"
                                                    v-on:click="EditProcess(process.id)">{{ process.code }}</a>
                                            </strong>
                                        </td>
                                        <td class="text-center">
                                            {{ process.date }}
                                        </td>
                                        <td class="text-center">
                                            {{ process.englishName }}
                                        </td>
                                        <td class="text-center">
                                            {{ process.description }}
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <span v-if="currentPage === 1 && rowCount === 0">
                                {{ $t('Pagination.ShowingEntries') }}
                            </span>
                            <span v-else-if="currentPage === 1 && rowCount < 10">
                                {{ $t('Pagination.Showing') }}
                                {{ currentPage }} {{ $t('Pagination.to') }}
                                {{ rowCount }}
                                {{ $t('Pagination.of') }}
                                {{ rowCount }}
                                {{ $t('Pagination.entries') }}
                            </span>
                            <span v-else-if="currentPage === 1 && rowCount >= 11">
                                {{ $t('Pagination.Showing') }}
                                {{ currentPage }}
                                {{ $t('Pagination.to') }}
                                {{ currentPage * 10 }}
                                {{ $t('Pagination.of') }}
                                {{ rowCount }}
                                {{ $t('Pagination.entries') }}
                            </span>
                            <span v-else-if="currentPage === 1">
                                {{ $t('Pagination.Showing') }}
                                {{ currentPage }}
                                {{ $t('Pagination.to') }}
                                {{ currentPage * 10 }} of {{ rowCount }}
                                {{ $t('Pagination.entries') }}
                            </span>
                            <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                {{ $t('Pagination.Showing') }}
                                {{ (currentPage * 10) - 9 }}
                                {{ $t('Pagination.to') }}
                                {{ currentPage * 10 }}
                                {{ $t('Pagination.of') }}
                                {{ rowCount }}
                                {{ $t('Pagination.entries') }}
                            </span>
                            <span v-else-if="currentPage === pageCount">
                                {{ $t('Pagination.Showing') }}
                                {{ (currentPage * 10) - 9 }}
                                {{ $t('Pagination.to') }}
                                {{ rowCount }}
                                {{ $t('Pagination.of') }}
                                {{ rowCount }}
                                {{ $t('Pagination.entries') }}
                            </span>
                        </div>
                        <div class=" col-lg-6">
                            <div class=" float-end" v-on:click="GetProcessData()">
                                <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                    :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                    :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                                </b-pagination>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <addProcess :process="newProcess" :show="show" v-if="show" @close="IsSave" :type="type" />
    </div>
</template>


<script>
import clickMixin from '@/Mixins/clickMixin'
export default {
    mixins: [clickMixin],
    data: function () {
        return {
            searchQuery: '',
            currency: '',
            show: false,
            processlist: [],

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
        search: function (val) {
            this.GetProcessData(val, 1);
        }
    },

    methods: {
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        IsSave: function () {

            this.show = false;

            this.GetProcessData(this.search, this.currentPage);
        },
        getPage: function () {
            this.GetProcessData(this.search, this.currentPage);
        },
        AddProcess: function () {
            this.$router.push({
                path: '/AddProcess',
                query: {
                    Add: true,
                }
            })
        },
        GetProcessData: function () {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('Batch/ProcessList?isDropdown=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.processlist = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        search22: function () {
            this.GetProcessData();
        },

        clearData: function () {
            this.search = "";
            this.GetProcessData();
        },
        EditProcess: function (Id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Batch/ProcessDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddProcess',
                            query: {
                                data: '',
                                Add: false,
                            }
                        })
                    }
                    else {
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
        this.currency = localStorage.getItem('currency');

        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.GetProcessData(this.search, 1);

    }
}
</script>