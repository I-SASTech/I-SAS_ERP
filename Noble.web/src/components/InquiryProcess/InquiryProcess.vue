<template>
    <div class="row" v-if="isValid('CanViewInquiryProcess')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('InquiryProcess.InquiryProcessList') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('InquiryList.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('InquiryProcess.InquiryProcessList') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddInquiryProcess')" v-on:click="openmodel()" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('color.AddNew') }}
                                </a>
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
                        <div class="col-lg-8" style="padding-top:20px">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('InquiryProcess.SearchbyModule')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

<button v-on:click="search22(true)" type="button" class="btn btn-outline-primary mt-3">
    {{ $t('Sale.ApplyFilter') }}
</button>
<button v-on:click="clearData(false)" type="button"
class="btn btn-outline-primary mx-2 mt-3">
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
                                        {{ $t('InquiryProcess.Code') }}
                                    </th>
                                    <th v-if="english=='true'">
                                        {{ $t('InquiryProcess.ProcessName') }}
                                    </th>
                                    <th v-if="isOtherLang()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                        {{ $t('InquiryProcess.Label') }}
                                    </th>
                                    <th class="text-center" width="40%">
                                        {{ $t('InquiryProcess.Description') }}
                                    </th>
                                    <th>
                                        {{ $t('InquiryProcess.Status') }}
                                    </th>


                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(process,index) in processlist" v-bind:key="process.id">
                                    <td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*10)-10) +(index+1)}}
                                    </td>

                                    <td v-if="isValid('CanEditInquiryProcess')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="Editprocess(process.id)">{{process.code}}</a>
                                        </strong>
                                    </td>
                                    <td v-else>

                                        {{process.code}}

                                    </td>
                                    <td>
                                        {{process.name}}
                                    </td>
                                    <td>
                                        {{process.label}}
                                    </td>


                                    <td class="text-center">
                                        {{process.description}}
                                    </td>
                                    <td>
                                        <span v-if="process.isActive" class="badge badge-boxed  badge-outline-success">{{$t('InquiryProcess.Active')}}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('InquiryProcess.De-Active')}}</span>
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
                        <div class="" v-on:click="GetprocessData()">
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

            <add-inquiry-process-model :newprocess="newprocess"
                                       :show="show"
                                       v-if="show"
                                       @close="IsSave"
                                       :type="type" />
        </div>

    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
                searchQuery: '',
                show: false,
                processlist: [],
                newprocess: {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    label: '',
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
            // search: function (val) {
            //     this.GetprocessData(val, 1);
            // }
        },
        methods: {

            search22: function () {
    this.GetprocessData(this.search, this.currentPage);
            },

            clearData: function () {
                this.search="";
                this.GetprocessData(this.search, this.currentPage);

            },
            IsSave: function () {

                this.show = !this.show;

                this.GetprocessData(this.search, this.currentPage);
            },
            getPage: function () {
                this.GetprocessData(this.search, this.currentPage);
            },
            openmodel: function () {
                this.newprocess = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    label: '',
                    code: '',
                    description: '',
                    isActive: true

                }
                this.show = !this.show;
                this.type = "Add";
            },
            GetprocessData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Project/InquiryProcessList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.processlist = response.data.results.inquiryProcessLookUp;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            Editprocess: function (Id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Project/InquiryProcessDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.newprocess.id = response.data.id;
                            root.newprocess.name = response.data.name;
                            root.newprocess.label = response.data.label;
                            root.newprocess.description = response.data.description;
                            root.newprocess.code = response.data.code;
                            root.newprocess.isActive = response.data.isActive;
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
            this.GetprocessData(this.search, 1);

        }
    }
</script>