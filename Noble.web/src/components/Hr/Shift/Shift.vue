<template>

    <div class="row">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Shift.Shift') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Shift.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Shift.Shift') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="openmodel" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Shift.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Shift.Close') }}
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
                        <button class="btn btn-secondary" type="button" id="button-addon1">
                            <i class="fas fa-search"></i>
                        </button>
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('Shift.Search')"
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
                                        {{ $t('Shift.ShiftName') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Shift.StartTime') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Shift.EndTime') }}
                                    </th>
                                    <th>
                                        {{ $t('Shift.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(shift ,index) in shiftList" v-bind:key="shift.id">
                                    <td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*10)-10) +(index+1)}}
                                    </td>

                                    <td>
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditShift(shift.id)">{{shift.shiftName}}</a>
                                        </strong>
                                    </td>

                                    <td class="text-center">
                                        {{getDate(shift.startTime)}}
                                    </td>
                                    <td class="text-center">
                                        {{getDate(shift.endTime)}}

                                    </td>
                                    <td>
                                        <span v-if="shift.status" class="badge badge-boxed  badge-outline-success">{{ $t('Shift.Active') }}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{ $t('Shift.De-Active') }}</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-lg-6">
                            <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                            <span v-else-if="currentPage === 1 && rowCount < 10">
                                {{ $t('Pagination.Showing') }}
                                {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                {{ rowCount }} {{ $t('Pagination.entries') }}
                            </span>
                            <span v-else-if="currentPage === 1 && rowCount >= 11">
                                {{ $t('Pagination.Showing') }}
                                {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                                {{ rowCount }} {{ $t('Pagination.entries') }}
                            </span>
                            <span v-else-if="currentPage === 1">
                                {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                            $t('Pagination.to')
                                }} {{ currentPage * 10 }} of {{ rowCount }} {{
 $t('Pagination.entries')
                                }}
                            </span>
                            <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                {{ $t('Pagination.Showing') }}
                                {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                $t('Pagination.of')
                                }} {{ rowCount }} {{ $t('Pagination.entries') }}
                            </span>
                            <span v-else-if="currentPage === pageCount">
                                {{ $t('Pagination.Showing') }}
                                {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                {{ rowCount }} {{ $t('Pagination.entries') }}
                            </span>
                        </div>
                        <div class=" col-lg-6">
                            <div class="float-end" v-on:click="GetBrandData()">
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
            </div>

            <add-shift :newshift="newShift"
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
                currency: '',
                searchQuery: '',
                show: false,
                shiftList: [],
                newShift: {
                    id: '',
                    code: '',
                    startTime: '',
                    endTime: '',
                    description: '',
                    status: true,
                    shiftName: '',
                    nameArabic: '',
                    sunday: false,
                    monday: false,
                    tuesday: false,
                    wednesday: false,
                    thursday: false,
                    friday: false,
                    saturday: false,
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
            // search: function () {
            //     this.GetShiftData();
            // }
        },
        methods: {
            search22: function () {
            this.GetShiftData(this.search, this.currentPage);
        },

        clearData: function () {
            this.search = "";
            this.GetShiftData(this.search, this.currentPage);

        },
            getDate: function (date) {
                return moment(date).format('LL');
            },

            GotoPage: function (link) {
                this.$router.push({ path: link });
            },

            IsSave: function () {
                this.show = false;
                this.GetShiftData();
            },

            getPage: function () {
                this.GetShiftData();
            },

            openmodel: function () {
                this.newShift = {
                    id: '00000000-0000-0000-0000-000000000000',
                    startTime: '',
                    endTime: '',
                    description: '',
                    status: true,
                    shiftName: '',
                    nameArabic: '',
                    code: '',
                    sunday: false,
                    monday: false,
                    tuesday: false,
                    wednesday: false,
                    thursday: false,
                    friday: false,
                    saturday: false,
                }
                this.show = !this.show;
                this.type = "Add";
            },

            GetShiftData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Hr/ShiftList?searchTerm=' + this.search + '&pageNumber=' + this.currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.shiftList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            EditShift: function (Id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Hr/ShiftDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.newShift.id = response.data.id;
                            root.newShift.startTime = response.data.startTime;
                            root.newShift.endTime = response.data.endTime;
                            root.newShift.description = response.data.description;
                            root.newShift.shiftName = response.data.shiftName;
                            root.newShift.nameArabic = response.data.nameArabic;
                            root.newShift.code = response.data.code;
                            root.newShift.sunday = response.data.sunday;
                            root.newShift.monday = response.data.monday;
                            root.newShift.tuesday = response.data.tuesday;
                            root.newShift.wednesday = response.data.wednesday;
                            root.newShift.thursday = response.data.thursday;
                            root.newShift.friday = response.data.friday;
                            root.newShift.saturday = response.data.saturday;
                            root.newShift.status = response.data.status;

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
            this.GetShiftData();

        }
    }
</script>