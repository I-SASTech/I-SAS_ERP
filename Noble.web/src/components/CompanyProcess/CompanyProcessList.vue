<template>
    <div class="row" v-if="isValid('Can View Color')">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
            <div class="card ">
                <div class="card-header">
                    <h4 class="card-title DayHeading" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"> {{ $t('CompanyProcessList.CompanyProcessList') }}</h4>
                    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                        <div class="col-md-5 col-lg-5 pb-3">
                            <div class="form-group">
                                <label></label>
                                <input type="text" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" class="form-control" v-model="search" name="search" id="search" :placeholder="$t('SearchbyColor')" />
                            </div>
                        </div>
                        <div class="col-md-7 col-lg-7">
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div v-if="isValid('Can Save Color')">
                                <a href="javascript:void(0)" class="btn btn-outline-primary  " v-on:click="openmodel"><i class="fa fa-plus"></i>  {{ $t('CompanyProcessList.AddNew') }}</a>

                            </div>
                            <div>
                                <router-link :to="'/ProductManagement'">
                                    <a href="javascript:void(0)" class="btn btn-outline-primary  "><i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                                </router-link>
                            </div>
                        </div>
                        <div class="mt-2">
                            <table class="table table-shopping" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <thead class="">
                                    <tr>
                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">#</th>
                                        <th v-if="english=='true'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{$englishLanguage($t('CompanyProcessList.ProcessName'))  }}
                                        </th>
                                        <th v-if="isOtherLang()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{$arabicLanguage($t('CompanyProcessList.ProcessName'))  }}
                                        </th>
                                        <th class="text-center" width="40%">
                                            {{ $t('CompanyProcessList.Type') }}
                                        </th>
                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('CompanyProcessList.Status') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(process,index) in processList" v-bind:key="process.id">
                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{index+1}}
                                        </td>
                                        <td v-if="english=='true'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            <a class="font-weight-bold" href="javascript:void(0)" v-on:click="EditProcess(process.id)">{{process.processName}}</a> 
                                        </td>
                                        <td v-if="isOtherLang()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            <a class="font-weight-bold" href="javascript:void(0)" v-on:click="EditProcess(process.id)">{{process.processNameArabic}}</a>
                                        </td>
                                        <td class="text-center">
                                            {{process.type}}
                                        </td>
                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{process.status==true?$t('CompanyProcessList.Active'):$t('CompanyProcessList.De-Active')}}</td>

                                    </tr>
                                </tbody>
                            </table>

                        </div>
                    </div>
                    <div class="float-left">
                        <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount < 10">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount >= 11  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                    </div>
                    <div class="float-right">
                        <div class="" v-on:click="GetColorData()">
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
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
        <add-company-process :process="newProcess"
                    :show="show"
                    v-if="show"
                    @close="IsSave"
                    :type="type" />

    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>

<script>
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
         components: {
            Loading
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                searchQuery: '',
                show: false,
                processList: [],
                newProcess: {
                    id: '',
                    processName: '',
                    processNameArabic: '',
                    type: '',
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
            search: function (val) {
                this.GetColorData(val, 1);
            }
        },
        methods: {
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
                    processName: '',
                    processNameArabic: '',
                    type: '',
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
                
                root.$https.get('Company/ProcessList?isActive=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        
                        root.$store.GetProcessList(response.data.results);
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
                root.$https.get('/Company/ProcessDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.newProcess.id = response.data.id;
                            root.newProcess.processName = response.data.processName;
                            root.newProcess.processNameArabic = response.data.processNameArabic;
                            root.newProcess.type = response.data.type;
                            root.newProcess.status = response.data.status;
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
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.GetColorData(this.search, 1);
        }
    }
</script>