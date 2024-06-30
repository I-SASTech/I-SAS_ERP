<template>
    <div class="row" v-if="isValid('CanViewInquiryType')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('InquiryType.InquiryTypeList') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('InquiryList.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('InquiryType.InquiryTypeList') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddColor')" v-on:click="openmodel()" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
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
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('InquiryType.SearchbyType')" aria-label="Example text with button addon" aria-describedby="button-addon1">
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
                                        {{ $t('InquiryType.Code') }}
                                    </th>
                                    <th v-if="english=='true'">
                                        {{ $t('InquiryType.Name') }}
                                    </th>
                                    <th v-if="isOtherLang()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                        {{ $t('InquiryType.Label') }}
                                    </th>
                                    <th class="text-center" width="40%">
                                        {{ $t('InquiryType.Description') }}
                                    </th>
                                    <th>
                                        {{ $t('InquiryType.Status') }}
                                    </th>


                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(type,index) in typelist" v-bind:key="type.id">
                                    <td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*10)-10) +(index+1)}}
                                    </td>

                                    <td v-if="isValid('CanEditInquiryType')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="Edittype(type.id)">{{type.code}}</a>
                                        </strong>
                                    </td>
                                    <td v-else>

                                        {{type.code}}

                                    </td>
                                    <td>
                                        {{type.name}}
                                    </td>
                                    <td>
                                        {{type.label}}
                                    </td>


                                    <td class="text-center">
                                        {{type.description}}
                                    </td>
                                    <td>
                                        <span v-if="type.isActive" class="badge badge-boxed  badge-outline-success">{{$t('InquiryType.Active')}}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('InquiryType.De-Active')}}</span>
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
                        <div class="" v-on:click="GettypeData()">
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

            <add-inquiry-type-model :newinquiryType="newtype"
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
                typelist: [],
                newtype: {
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
            //     this.GettypeData(val, 1);
            // }
        },
        methods: {

            search22: function () {
                this.GettypeData(this.search, this.currentPage);
            },

            clearData: function () {
                this.search="";
                this.GettypeData(this.search, this.currentPage);

            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            IsSave: function () {

                this.show = !this.show;

                this.GettypeData(this.search, this.currentPage);
            },
            getPage: function () {
                this.GettypeData(this.search, this.currentPage);
            },
            openmodel: function () {
                this.newtype = {
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
            GettypeData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Project/InquiryTypeList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.typelist = response.data.results.inquiryTypeLookUp;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            Edittype: function (Id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Project/InquiryTypeDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.newtype.id = response.data.id;
                            root.newtype.name = response.data.name;
                            root.newtype.label = response.data.label;
                            root.newtype.description = response.data.description;
                            root.newtype.code = response.data.code;
                            root.newtype.isActive = response.data.isActive;
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
            this.GettypeData(this.search, 1);



            //Decode JWT Token 
           
            this.GenerateToken('Inquiry Management');
        }
    }
</script>