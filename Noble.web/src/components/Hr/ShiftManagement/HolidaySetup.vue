<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Holiday.Holiday') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Holiday.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Holiday.Holiday') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="openmodel()" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Holiday.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('Holiday.Close') }}
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
                        <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('Holiday.Search')" aria-label="Example text with button addon" aria-describedby="button-addon1">
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
                                    <th width="5%">#</th>
                                    <th width="15%">
                                        {{ $t('Holiday.HolidayType') }}
                                    </th>
                                    <th width="20%">
                                        {{ $t('Holiday.PaidStatus') }}
                                    </th>

                                    <th width="20%">
                                        {{ $t('Holiday.Close') }} Date
                                    </th>
                                    <th width="30%">
                                        {{ $t('Holiday.Description') }}
                                    </th>
                                    <th width="10%">
                                        {{ $t('Holiday.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(color,index) in holydaylist" v-bind:key="color.id">
                                    <td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*10)-10) +(index+1)}}
                                    </td>
                                    <td>
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditColor(color.id)">
                                                <span v-if="color.holidayType==1" class="badge badge-boxed  badge-outline-primary">{{$t('Holiday.National')}}</span>
                                                <span v-if="color.holidayType==2" class="badge badge-boxed  badge-outline-primary">{{$t('Holiday.Guested')}}</span>
                                            </a>
                                        </strong>
                                    </td>
                                    <td>
                                        <span v-if="color.paidStatus" class="badge badge-boxed  badge-outline-primary">{{$t('Holiday.Paid')}}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('Holiday.UnPaid')}}</span>
                                    </td>

                                    <td>{{getDate(color.date)}}</td>

                                    <td>{{color.description}}</td>

                                    <td>
                                        <span v-if="color.isActive" class="badge badge-boxed  badge-outline-success">{{$t('Holiday.Active')}}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('Holiday.De-Active')}}</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />

                    <div class="float-start">
                        <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount < 10">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount >= 11  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                    </div>
                    <div class="float-end">
                        <div class="" v-on:click="GetData()">
                            <b-pagination pills size="sm" v-model="currentPage"
                                          :total-rows="rowCount"
                                          :per-page="10"
                                          :first-text="$t('Table.First')"
                                          :prev-text="$t('Table.Previous')"
                                          :next-text="$t('Table.Next')"
                                          :last-text="$t('Table.Last')"></b-pagination>
                        </div>
                    </div>
                </div>
            </div>

            <addholidaysetup :newHoliday="newHoliday"
                             :show="show"
                             v-if="show"
                             @close="IsSave"
                             :type="type" />
        </div>
    </div>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
                arabic: '',
                english: '',
                searchQuery: '',
                show: false,
                holydaylist: [],
                newHoliday: {
                    id: '',
                    holidayType: '',
                    date: '',
                    year: '',
                    description: '',
                    paidStatus: false,
                    isActive: true
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
            //     this.GetData(val, 1);
            // }
        },

        methods: {
            search22: function () {
            this.GetData(this.search, this.currentPage);
        },

        clearData: function () {
            this.search = "";
            this.GetData(this.search, this.currentPage);

        },
            getDate: function (date) {
                return moment(date).format('LL');
            },

            GotoPage: function (link) {
                this.$router.push({ path: link });
            },

            IsSave: function () {
                this.show = false;
                this.GetData(this.search, this.currentPage);
            },

            getPage: function () {
                this.GetData(this.search, this.currentPage);
            },

            openmodel: function () {
                this.newHoliday = {
                    id: '00000000-0000-0000-0000-000000000000',
                    holidayType: '',
                    date: '',
                    year: '',
                    description: '',
                    paidStatus: false,
                    isActive: true
                }
                this.show = !this.show;
                this.type = "Add";
            },

            GetData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Hr/GetHolidaysList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.holydaylist = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            EditColor: function (Id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Hr/GetHolidaysDetails?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {

                            root.newHoliday.id = response.data.id;
                            root.newHoliday.holidayType = response.data.holidayType == 1 ? 'National' :'Guested';
                            root.newHoliday.date = response.data.date;
                            root.newHoliday.year = response.data.year;
                            root.newHoliday.description = response.data.description;
                            root.newHoliday.paidStatus = response.data.paidStatus;
                            root.newHoliday.isActive = response.data.isActive;
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
            this.GetData(this.search, 1);
        }
    }
</script>