<template>
    <div class="row" v-if="isValid('CanViewColor')">      

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('color.ProductColorList') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('color.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('color.ProductColorList') }}</li>
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
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('color.Search')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                        <div class=" col-lg-4 mt-1">

                            <button v-on:click="search22(true)" :disabled="!search" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled="!search" type="button" class="btn btn-outline-primary mx-2 mt-3">
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
                                    <th width="10%">
                                        {{ $t('color.Code') }}
                                    </th>
                                    <th v-if="english=='true'" width="20%">
                                        {{$englishLanguage($t('color.ColorName'))   }}
                                    </th>
                                    <th v-if="isOtherLang()" width="20%">
                                        {{$arabicLanguage($t('color.ColorName'))  }}
                                    </th>

                                    <th width="40%">
                                        {{ $t('color.Description') }}
                                    </th>
                                    <th width="10%">
                                        {{ $t('color.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(color,index) in colorlist" v-bind:key="color.id">
                                    <td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*10)-10) +(index+1)}}
                                    </td>
                                    <td v-if="isValid('CanEditColor')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditColor(color.id)">{{color.code}}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        <strong>
                                            {{color.code}}
                                        </strong>
                                    </td>

                                    <td v-if="english=='true'">{{color.name}}</td>
                                    <td v-if="isOtherLang()">{{color.nameArabic}}</td>
                                    <td>{{color.description}}</td>

                                    <td>
                                        <span v-if="color.isActive" class="badge badge-boxed  badge-outline-success">{{$t('color.Active')}}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('color.De-Active')}}</span>
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

            <colormodel :newcolor="newColor"
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
                arabic: '',
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
            search22: function () {
            this.GetColorData(this.search, this.currentPage, this.active);
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
            openmodel: function () {
                this.newColor = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    nameArabic: '',
                    description: '',
                    isActive: true

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
                root.$https.get('Product/ColorList?isActive=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.colorlist = response.data.results.colors;
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
                root.$https.get('/Product/ColorDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {

                            root.newColor.id = response.data.id;
                            root.newColor.name = response.data.name;
                            root.newColor.nameArabic = response.data.nameArabic;
                            root.newColor.description = response.data.description;
                            root.newColor.code = response.data.code;
                            root.newColor.isActive = response.data.isActive;
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
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.GetColorData(this.search, 1);
        }
    }
</script>