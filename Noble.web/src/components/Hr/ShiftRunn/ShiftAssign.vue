<template>

    <div class="row">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('ShiftAssign.ShiftAssign') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('ShiftAssign.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('ShiftAssign.ShiftAssign') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/AddShiftAssign')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('ShiftAssign.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('ShiftAssign.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1">
                            <i class="fas fa-search"></i>
                        </button>
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('ShiftAssign.Search')"
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
                                        {{ $t('ShiftAssign.Code') }}
                                    </th>

                                    <th>
                                        {{ $t('ShiftAssign.ShiftName') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('ShiftAssign.StartTime') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('ShiftAssign.EndTime') }}
                                    </th>
                                    <th>
                                        {{ $t('ShiftAssign.ClosingReason') }}
                                    </th>
                                    <th>
                                        {{ $t('ShiftAssign.Status') }}
                                    </th>
                                    <th>
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
                                            <a href="javascript:void(0)" v-on:click="EditShift(shift.id)">{{shift.code}}</a>
                                        </strong>
                                    </td>
                                    <td>
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditShift(shift.id)">{{shift.shiftName}}</a>
                                        </strong>
                                    </td>

                                    <td class="text-center">
                                        {{getDate(shift.fromDate)}}
                                    </td>
                                    <td class="text-center">
                                        {{getDate(shift.toDate)}}

                                    </td>
                                    <td>
                                        {{shift.reasonOfClosingShift}}
                                    </td>
                                    <td>
                                        <span v-if="shift.isActive" class="badge badge-boxed  badge-outline-success">{{ $t('Shift.Active') }}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{ $t('Shift.De-Active') }}</span>
                                    </td>
                                    <td class="text-end">
                                        <button type="button" class="btn btn-light dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">{{ $t('Sale.Action') }} <i class="mdi mdi-chevron-down"></i></button>
                                        <div class="dropdown-menu">
                                            <a class="dropdown-item" href="javascript:void(0)" v-on:click="DuplicateShift(shift.id)">{{ $t('Sale.DuplicateInvoice') }}</a>
                                            
                                        </div>
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
                                {{ $t('Pagination.Showing') }} {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries')}}
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
                shiftList: [],
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
                this.GetShiftData();
            }
        },
        methods: {
            getDate: function (date) {
                return moment(date).format('LL');
            },

            GotoPage: function (link) {
                this.$router.push({ path: link });
            },


            getPage: function () {
                this.GetShiftData();
            },


            GetShiftData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Hr/ShiftAssignList?searchTerm=' + this.search + '&pageNumber=' + this.currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.shiftList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            EditShift: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Hr/ShiftAssignDetail?id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddShiftAssign',
                            type: 'Edit',
                            query: {
                                data: '',
                                Add: false,
                            }
                        })
                    }
                    
                });
            },

            DuplicateShift: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Hr/ShiftAssignDetail?id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddShiftAssign',
                            type: 'Duplicate',
                            query: {
                                data: '',
                                Add: false,
                            }
                        })
                    }
                    
                });
            },
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