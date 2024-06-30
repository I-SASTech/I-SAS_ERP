<template>
    <div class="row" v-if="isValid('CanPushRecord')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{($t('PushRecords.PushDetails'))}}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{($t('PushRecords.Home'))}}</a></li>
                                    <li class="breadcrumb-item active">{{($t('PushRecords.PushDetails'))}}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <button class="btn btn-sm btn-outline-primary  " :disabled="isSync" @click="SyncData">{{($t('PushRecords.PushRecords'))}}</button>
                                <button class="btn btn-sm btn-outline-primary mx-1 " v-on:click="saveAutosync()"> {{isAutoSync ? $t('PushRecords.DisabledAutoPush') : $t('PushRecords.EnableAutoPush') }}</button>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('PushRecords.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="progress mb-2" v-if="!loading">
                        <div class="progress-bar bg-success" role="progressbar" :style="'width:' + syncedRecords + '%'" aria-valuenow="15" aria-valuemin="0" aria-valuemax="100">{{Math.floor(syncedRecords)}}%</div>
                        <div class="progress-bar bg-warning" role="progressbar" :style="'width:' + pendingRecords + '%'" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100">{{Math.ceil(pendingRecords)}}%</div>
                    </div>
                    <div class=" mb-2">
                        <div class="">
                            <ul class="nav nav-tabs" data-tabs="tabs">
                                <li class="nav-item"><a class="nav-link" v-bind:class="{active:status == 'All'}" v-on:click="changeStatus('All')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true"> {{($t('PushRecords.AllRecords'))}}</a></li>
                                <li class="nav-item"><a class="nav-link" v-bind:class="{active:status == 'Sync'}" v-on:click="changeStatus('Sync')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{($t('PushRecords.Synced'))}}</a></li>
                                <li class="nav-item"><a class="nav-link" v-bind:class="{active:status == 'NotSync'}" v-on:click="changeStatus('NotSync')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile1" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{($t('PushRecords.Pending'))}}</a></li>

                            </ul>
                        </div>
                    </div>
                    <div class="card-body" v-if="loading">
                        <div class="text-center" id="preloader">
                            <div id="loader"></div>
                        </div>
                    </div>
                    <div class="tab-content " id="nav-tabContent" v-else>
                        <div v-if="status == 'All'">
                            <div class="mt-4">
                                <div class="table-responsive">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>
                                                    {{($t('PushRecords.Table'))}}
                                                </th>
                                                <th class="text-center">
                                                    {{($t('PushRecords.PushRecords'))}}
                                                </th>
                                                <th class="text-center">
                                                    {{($t('PushRecords.PushDate'))}} 
                                                </th>
                                                <th class="text-center">
                                                     {{($t('PushRecords.CreatedDate'))}} 
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="details in syncRecordList" v-bind:key="details.id">
                                                <td>
                                                    {{details.table}}
                                                </td>
                                                <td class="text-center">
                                                    <span :class="details.push == true ? 'badge badge-success' : 'badge badge-warning'">
                                                        {{details.push == true ? 'Synced' : 'Pending'}}
                                                    </span>
                                                </td>
                                                <td class="text-center">
                                                    {{$options.filters.filterDate(details.pushDate)  }}
                                                </td>
                                                <td class="text-center"> {{details.changeDate}}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage === 1 && rowCount === 0">
                                        {{$t('Pagination.ShowingEntries')}}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount < 10">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                        {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount >= 11">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                            $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1">
                                        {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                        $t('Pagination.to')
                                        }} {{ currentPage * 10 }} of {{ rowCount }} {{$t('Pagination.entries')}}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{
                                        $t('Pagination.Showing')
                                        }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                            $t('Pagination.entries')
                                        }}
                                    </span>
                                    <span v-else-if="currentPage === pageCount">
                                        {{ $t('Pagination.Showing') }}
                                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                        $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class="float-end" v-on:click="getPage()">
                                        <b-pagination pills size="sm" v-model="currentPage"
                                                    :total-rows="rowCount"
                                                    :per-page="10"
                                                    :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')"
                                                    :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')" >
                                                </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div v-if="status == 'Sync'">
                            <div class="mt-4">
                                <div class="table-responsive">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>
                                                    {{($t('PushRecords.Table'))}}
                                                </th>
                                                <th class="text-center">
                                                    {{($t('PushRecords.PushRecords'))}}
                                                </th>
                                                <th class="text-center">
                                                    {{($t('PushRecords.PushDate'))}} 
                                                </th>
                                                <th class="text-center">
                                                     {{($t('PushRecords.CreatedDate'))}} 
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="details in syncRecordList" v-bind:key="details.id">
                                                <td>
                                                    {{details.table}}
                                                </td>
                                                <td class="text-center">
                                                    <span :class="details.push == true ? 'badge badge-success' : 'badge badge-warning'">
                                                        {{details.push == true ? 'Synced' : 'Pending'}}
                                                    </span>
                                                </td>
                                                <td class="text-center">
                                                    {{$options.filters.filterDate(details.pushDate)  }}
                                                </td>
                                                <td class="text-center"> {{details.changeDate}}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage === 1 && rowCount === 0">
                                        {{$t('Pagination.ShowingEntries')}}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount < 10">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                        {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount >= 11">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                            $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1">
                                        {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                        $t('Pagination.to')
                                        }} {{ currentPage * 10 }} of {{ rowCount }} {{$t('Pagination.entries')}}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{
                                        $t('Pagination.Showing')
                                        }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                            $t('Pagination.entries')
                                        }}
                                    </span>
                                    <span v-else-if="currentPage === pageCount">
                                        {{ $t('Pagination.Showing') }}
                                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                        $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class="float-end" v-on:click="getPage()">
                                       <b-pagination pills size="sm" v-model="currentPage"
                                                    :total-rows="rowCount"
                                                    :per-page="10"
                                                    :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')"
                                                    :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')" >
                                                </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div v-if="status == 'NotSync'">
                            <div class="mt-4">
                                <div class="table-responsive">
                                    <table class="table mb-0">
                                        <thead class="thead-light table-hover">
                                            <tr>
                                                <th>
                                                    {{($t('PushRecords.Table'))}}
                                                </th>
                                                <th class="text-center">
                                                    {{($t('PushRecords.PushRecords'))}}
                                                </th>
                                                <th class="text-center">
                                                    {{($t('PushRecords.PushDate'))}} 
                                                </th>
                                                <th class="text-center">
                                                     {{($t('PushRecords.CreatedDate'))}} 
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="details in syncRecordList" v-bind:key="details.id">
                                                <td>
                                                    {{details.table}}
                                                </td>
                                                <td class="text-center">
                                                    <span :class="details.push == true ? 'badge badge-success' : 'badge badge-warning'">
                                                        {{details.push == true ? 'Synced' : 'Pending'}}
                                                    </span>
                                                </td>
                                                <td class="text-center">
                                                    {{$options.filters.filterDate(details.pushDate)   }}
                                                </td>
                                                <td class="text-center"> {{details.changeDate}}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage === 1 && rowCount === 0">
                                        {{$t('Pagination.ShowingEntries')}}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount < 10">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                        {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1 && rowCount >= 11">
                                        {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                            $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                    <span v-else-if="currentPage === 1">
                                        {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                        $t('Pagination.to')
                                        }} {{ currentPage * 10 }} of {{ rowCount }} {{$t('Pagination.entries')}}
                                    </span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                        {{
                                        $t('Pagination.Showing')
                                        }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                        {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                            $t('Pagination.entries')
                                        }}
                                    </span>
                                    <span v-else-if="currentPage === pageCount">
                                        {{ $t('Pagination.Showing') }}
                                        {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                        $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                    </span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class="float-end" v-on:click="getPage()">
                                       <b-pagination pills size="sm" v-model="currentPage"
                                                    :total-rows="rowCount"
                                                    :per-page="10"
                                                    :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')"
                                                    :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')" >
                                                </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
        name: 'product',
        data: function () {
            return {
                arabic: '',
                english: '',
                show: false,
                type: '',
                syncRecordList: [
                ],
                status: 'All',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                searchQuery: '',
                loading: true,
                isSync: false,
                syncedRecords: 0,
                pendingRecords: 0,
                isAutoSync: false

            }
        },
        filters: {
            filterDate: function (val) {
                if (val == null || val == '')
                    return "--";

                return moment(val).format('DD/MM/YYYY HH:mm');
                

            }
        },
        methods: {
            changeStatus: function (status) {
                this.status = status;
                this.GetSyncRecords();
            },
            ImportDataFromCsv: function () {
                var root = this;
                root.$router.push({
                    path: '/ImportProduct'
                })
            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            getPage: function () {

                this.GetSyncRecords(this.search, this.currentPage);
            },
            GetSyncRecords: function () {
                var root = this;


                var url = '/System/GetPushRecordsInformation?status=' + this.status + '&pageNumber=' + this.currentPage;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loading = true;

                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.syncRecordList = response.data.results.syncRecords;

                        if (root.syncRecordList.length > 0) {
                            var total = root.syncRecordList[0].synced + root.syncRecordList[0].pending;

                            if (total > 0) {
                                root.syncedRecords = (root.syncRecordList[0].synced / total) * 100;
                                root.pendingRecords = (root.syncRecordList[0].pending / total) * 100;
                            }
                        }
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            SyncData: function () {
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.isSync = true;
                this.$https
                    .get('/System/PushDataRecord', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(() => {
                        root.isSync = false;
                    },
                        () => {
                            root.isSync = false;
                        });
            },
            recordUpdated() {
                var root = this;
                setTimeout(() => {
                    root.GetSyncRecords();

                    if (!root.isSync)
                        return;
                    root.recordUpdated();
                }, 5000);
            },

            saveAutosync: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.loading = true;
                this.isAutoSync = !this.isAutoSync;
                root.$https
                    .get('/System/SaveAutoSync?isSync=' + this.isAutoSync, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.status == 200) {
                            root.$swal({
                                title: "Success!",
                                text: "Auto sync save successfully enable",
                                type: 'success',
                                confirmButtonClass: "btn btn-Success",
                                buttonStyling: false,
                                icon: 'success'

                            });
                        } else {
                            root.isAutoSync = !root.isAutoSync;

                            
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: "error",
                                type: 'error',
                                confirmButtonClass: "btn btn-Success",
                                buttonStyling: false,
                                icon: 'error'

                            });

                        }

                        root.loading = false;
                    }, (error) => {
                        root.loading = false;

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: error,
                            type: 'error',
                            confirmButtonClass: "btn btn-Success",
                            buttonStyling: false,
                            icon: 'error'

                        });
                    });
            },

            GetAutosync: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.loading = true;
                root.$https
                    .get('/System/GetAutoSync', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        root.isAutoSync = response.data;
                        root.loading = false;
                    }, (error) => {
                        root.loading = false;

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: error,
                            type: 'error',
                            confirmButtonClass: "btn btn-Success",
                            buttonStyling: false,
                            icon: 'error'

                        });
                    });
            },
        },
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.GetAutosync();
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.$route.query.data != undefined) {
                this.currentPage = this.$route.query.data;
                this.GetSyncRecords();
            }
            else {
                this.GetSyncRecords(this.search, 1);
            }
        },
        watch: {
            isSync: {
                handler: function (val) {
                    if (val) {
                        this.recordUpdated();
                    }
                },
                deep: true
            }
        },
    }
</script>