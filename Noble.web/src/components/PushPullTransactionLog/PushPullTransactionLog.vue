<template>
    <div class="row">      

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Sync Log</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('color.Home') }}</a></li>
                                    <li class="breadcrumb-item active">Sync Log</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                               
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
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
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12" >
                            <div class="form-group">
                                <label>{{ $t('Sale.FromDate') }}</label>
                                <datepicker v-model="fromDate" :key="render"/>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12" >
                            <div class="form-group">
                                <label>{{ $t('Sale.ToDate') }}</label>
                                <datepicker v-model="toDate" :key="render" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12" >
                            <div class="form-group">
                                <label> {{ $t('DailyExpense.PaymentMode') }}</label>
                                <multiselect  v-model="logType" :options="['Fresh', 'History']" :show-labels="false" placeholder="Select Type">
                                </multiselect>
                               
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12" >
                            <div class=" col-lg-4 mt-1" >

                        <button v-on:click="ApplyFilter()" type="button" class="btn btn-outline-primary mt-3">
                            {{ $t('Sale.ApplyFilter') }}
                        </button>
                        </div>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th class="text-center">
                                        Table Name
                                    </th>

                                    <th class="text-center">
                                         Date
                                    </th>
                                    <th class="text-center">
                                         Action
                                    </th>
                                    <th class="text-center">
                                        Push Date
                                    </th>
                                    <th class="text-center">
                                        Pull Date
                                    </th>
                                   
                                    <th class="text-center">
                                       Document Number
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(color,index) in colorlist" v-bind:key="color.changeDate">
                                    <td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*10)-10) +(index+1)}}
                                    </td>
                                   
                                    <td class="text-center" >
                                        <strong >
                                            <a href="javascript:void(0)" v-on:click="openmodel(color.code)">  {{formatTableName(color.table)}}</a>

                                          
                                        </strong>
                                    </td>
                                    <td class="text-center">{{GetDate(color.changeDate)}}</td>
                                    <td class="text-center">{{color.action}}</td>
                                    <td class="text-center">{{GetDate(color.pushDate)}}</td>
                                    <td class="text-center">{{GetDate(color.pullDate)}}</td>
                                    <td class="text-center">{{color.documentNo}}</td>

                                  
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
            <pushpulllogmodel 
                        :show="show"
                        :documentNo="documentNo"
                        :logType="logType"
                        v-if="show"
                        @close="IsSave"
                         />
            
        </div>

    </div>

</template>


<script>
    import moment from 'moment'
    import Multiselect from 'vue-multiselect'


    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        components: {
            Multiselect,

        },
        data: function () {
            return {
                arabic: '',
                fromDate: '',
                logType: '',
                toDate: '',
                english: '',
                searchQuery: '',
                show: false,
                colorlist: [],
                newColor: {
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
            }
        },
        watch: {
            // search: function (val) {
            //     this.GetColorData(val, 1);
            // }
        },
        methods: {
            openmodel: function (documentNo) {
              
                this.show = !this.show;
                this.documentNo = documentNo;
            },
            ApplyFilter() {

                this.GetColorData(this.search, this.currentPage, this.active);

                     
                    },

            formatTableName(tableName) {
                        // Split the tableName by capital letters
                        const words = tableName.split(/(?=[A-Z])/);
                        
                        // Capitalize the first letter of each word and join with a space
                        return words.map(word => word.charAt(0).toUpperCase() + word.slice(1)).join(' ');
                    },


            GetDate: function (date) {
                if(date==null || date=='' )
                {
                    return '';
                }
                return   moment(date).format('lll');

        },
            search22: function () {
            this.GetColorData();
        },

        clearData: function () {
            this.search = "";
            this.GetColorData(this.search, this.currentPage, this.active);

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
          
            GetColorData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Branches/PushPullRecordList?isActive=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search+ '&logType=' + this.logType+ '&toDate=' + this.toDate+ '&fromDate=' + this.fromDate, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.colorlist = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
           
        },
        created: function () {
           
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.fromDate = moment().add(-30, 'days').format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.logType = 'Fresh';
            this.render++;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.GetColorData(this.search, 1);
        }
    }
</script>