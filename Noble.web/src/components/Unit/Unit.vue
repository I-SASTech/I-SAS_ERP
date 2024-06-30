<template>
    <div class="row" v-if="isValid('CanViewUnit')">        

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Unit.ProductUnitList') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Unit.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Unit.ProductUnitList') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddUnit')" v-on:click="openmodel()" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Unit.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('Unit.Close') }}
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
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('Unit.Search')" aria-label="Example text with button addon" aria-describedby="button-addon1">
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
                                        {{ $t('Unit.Code') }}
                                    </th>
                                    <th v-if="english=='true'" width="20%">
                                        {{$englishLanguage($t('Unit.UnitName'))   }}
                                    </th>
                                    <th v-if="isOtherLang()" width="20%">
                                        {{$arabicLanguage($t('Unit.UnitNameAr')) }}
                                    </th>

                                    <th width="40%">
                                        {{ $t('Unit.Description') }}
                                    </th>
                                    <th width="10%">
                                        {{ $t('Unit.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(unit,index) in unitlist" v-bind:key="unit.id">
                                    <td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*10)-10) +(index+1)}}
                                    </td>
                                    <td v-if="isValid('CanEditUnit')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditUnit(unit.id)">{{unit.code}}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        <strong>
                                            {{unit.code}}
                                        </strong>
                                    </td>

                                    <td v-if="english=='true'">{{unit.name}}</td>
                                    <td v-if="isOtherLang()">{{unit.nameArabic}}</td>
                                    <td>{{unit.description}}</td>

                                    <td>
                                        <span v-if="unit.isActive" class="badge badge-boxed  badge-outline-success">{{$t('Unit.Active')}}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('Unit.De-Active')}}</span>
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
                        <div class="" v-on:click="GetUnitData()">
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

            <unitmodel :newunit="newUnit"
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
                unitlist: [],
                newUnit: {
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

            //     this.GetUnitData(val, 1);
            // }
        },
        methods: {
            search22: function () {
            this.GetUnitData(this.search, this.currentPage, this.active);
        },

        clearData: function () {
            this.search = "";
            this.GetUnitData(this.search, this.currentPage, this.active);

        },
            IsSave: function () {

                this.show = false;
                this.GetUnitData(this.search, this.currentPage);
            },
            getPage: function () {
                this.GetUnitData(this.search, this.currentPage);
            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            openmodel: function () {
                this.newUnit = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    nameArabic: '',
                    description: '',
                    isActive: true

                }
                this.show = !this.show;
                this.type = "Add";
            },
            GetUnitData: function () {
                var root = this;
                var token = '';

                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/UnitList?isActive=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.unitlist = response.data.results.units;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            EditUnit: function (Id) {


                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Product/UnitDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {

                            root.newUnit.id = response.data.id;
                            root.newUnit.name = response.data.name;
                            root.newUnit.nameArabic = response.data.nameArabic;
                            root.newUnit.description = response.data.description;
                            root.newUnit.code = response.data.code;
                            root.newUnit.isActive = response.data.isActive;
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
            this.GetUnitData(this.search, 1);
        }
    }
</script>