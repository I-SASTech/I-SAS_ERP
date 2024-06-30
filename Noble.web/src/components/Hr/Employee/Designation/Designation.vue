<template>
    <div class="row" >
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
            
            <div class="card">
                <div class="BorderBottom ml-2 mr-2 mt-3 mb-3">
                    <span class=" DayHeading">{{ $t('Designation.DesignationList') }}</span>
                </div>
                <div class="card-body">
                    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                            <div class="form-group">
                                <label>{{ $t('Designation.SearchbyDesignation') }}</label>
                                <div>
                                    <input type="text" class="form-control search_input" v-model="search" name="search" id="search" :placeholder="$t('Search')" />
                                    <span class="fas fa-search search_icon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                            <a v-if="isValid('Can Save Brand')" href="javascript:void(0)" class="btn btn-primary " style="margin-top:27px;" v-on:click="openmodel"><i class="fa fa-plus"></i> {{ $t('AddNew') }} </a>
                            <router-link :to="'/ProductManagement'">
                                <a href="javascript:void(0)" class="btn btn-outline-danger " style="margin-top:27px;">  <i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                            </router-link>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                            <table class="table table-striped table-hover table_list_bg" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <thead v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            {{ $t('Designation.Code') }}
                                        </th>
                                        <th v-if="english=='true'">
                                            {{$englishLanguage($t('Designation.Name'))  }}
                                        </th>
                                        <th v-if="isOtherLang()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{$arabicLanguage($t('Designation.Name'))  }}
                                        </th>
                                        <th class="text-center" width="40%">
                                            {{ $t('Designation.Description') }}
                                        </th>
                                        <th>
                                            {{ $t('Designation.Status') }}
                                        </th>


                                    </tr>
                                </thead>
                                <tbody v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                    <tr v-for="(designation,index) in designationlist" v-bind:key="designation.id">
                                        <td v-if="currentPage === 1">
                                            {{index+1}}
                                        </td>
                                        <td v-else>
                                            {{((currentPage*10)-10) +(index+1)}}
                                        </td>

                                        <td v-if="isValid('Can Edit Brand')">
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="EditDesignation(designation.id)">{{designation.code}}</a>
                                            </strong>
                                        </td>
                                        <td v-else>

                                            {{designation.code}}

                                        </td>
                                        <td v-if="english=='true'">
                                            {{designation.name}}
                                        </td>
                                        <td v-if="isOtherLang()">
                                            {{designation.nameArabic}}
                                        </td>


                                        <td class="text-center">
                                            {{designation.description}}
                                        </td>
                                        <td>{{designation.isActive==true?$t('Active'):$t('De-Active')}}</td>


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
                        <div class="" v-on:click="GetDesignationData()">
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
        <designationModel :newDesignation="newDesignation"
                    :show="show"
                    v-if="show"
                    @close="IsSave"
                    :type="type" />

    </div>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
                searchQuery: '',
                show: false,
                designationlist: [],
                newDesignation: {
                    id: '',
                    name: '',
                    nameArabic: '',
                    description: '',
                    code: '',
                    isActive: true
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
            search: function (val) {
                this.GetDesignationData(val, 1);
            }
        },
        methods: {
            IsSave: function () {
                
                this.show = false;

                this.GetDesignationData(this.search, this.currentPage);
            },
            getPage: function () {
                this.GetDesignationData(this.search, this.currentPage);
            },
            openmodel: function () {
                this.newDesignation = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    nameArabic: '',
                    description: '',
                    isActive: true

                }
                this.show = !this.show;
                this.type = "Add";
            },
            GetDesignationData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('EmployeeRegistration/DesignationList?isActive=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.designationlist = response.data.results.designations;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            EditDesignation: function (Id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/EmployeeRegistration/DesignationDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.newDesignation.id = response.data.id;
                            root.newDesignation.name = response.data.name;
                            root.newDesignation.nameArabic = response.data.nameArabic;
                            root.newDesignation.description = response.data.description;
                            root.newDesignation.code = response.data.code;
                            root.newDesignation.isActive = response.data.isActive;
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
            this.GetDesignationData(this.search, 1);

        }
    }
</script>