<template>
    <div class="row" v-if="isValid('CanViewSubCategory')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('SubCategory.ProductSubCategoryList') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('SubCategory.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('SubCategory.ProductSubCategoryList') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddSubCategory')" v-on:click="AddSubCategory"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('SubCategory.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('SubCategory.Close') }}
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
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('SubCategory.Search')"
                               aria-label="Example text with button addon" aria-describedby="button-addon1">
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
                                        {{ $t('SubCategory.Code') }}
                                    </th>
                                    <th width="16%" v-if="english == 'true'">
                                        {{$englishLanguage($t('SubCategory.SubCategoryName'))   }}
                                    </th>
                                    <th width="17%" v-if="isOtherLang()">
                                        {{$arabicLanguage($t('SubCategory.SubCategoryNameAr'))   }}
                                    </th>
                                    <th width="17%">
                                        {{ $t('Categories.CategoryName') }}
                                    </th>
                                    <th width="30%">
                                        {{ $t('SubCategory.Description') }}
                                    </th>

                                    <th width="10%">
                                        {{ $t('SubCategory.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(details, index) in subCategorylist " v-bind:key="details.id">
                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>
                                    <td v-if="isValid('CanEditSubCategory')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditSubCategoryInfo(details.id)">
                                                {{details.code}}
                                            </a>
                                        </strong>
                                    </td>
                                    <td v-else> {{ details.code }}</td>
                                    <td v-if="english == 'true'">{{ details.name }}</td>
                                    <td v-if="isOtherLang()">{{ details.nameArabic }}</td>
                                    <td>{{ details.categoryName }}</td>
                                    <td>{{ details.description }}</td>
                                    <td>
                                        <span v-if="details.isActive"
                                              class="badge badge-boxed  badge-outline-success">
                                            {{ $t('SubCategory.Active')}}
                                        </span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">
                                            {{$t('SubCategory.De-Active')}}
                                        </span>
                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />
                    <div class="float-start">
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
                            }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                            {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                    $t('Pagination.of')
                            }} {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === pageCount">
                            {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
 $t('Pagination.of')
                            }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                    </div>
                    <div class="float-end">
                        <div class="" v-on:click="GetSubCategoryData()">
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

            <subcategorymodal :show="show" :newsubcategory="newSubCategory" v-if="show" @close="IsSave" :type="type" />
        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: 'subCategory',
        data: function () {
            return {
                searchQuery: '',
                show: false,
                type: '',
                subCategorylist: [],
                arabic: '',
                english: '',
                newSubCategory: {
                    id: '',
                    code: '',
                    name: '',
                    nameArabic: '',
                    description: '',
                    categoryId: '',
                    isActive: true
                },
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
            }
        },
        watch: {
            // search: function (val) {
            //     this.GetSubCategoryData(val, 1);
            // }
        },

        methods: {
            search22: function () {
            this.GetSubCategoryData(this.search, this.currentPage, this.active);
        },

        clearData: function () {
            this.search = "";
            this.GetSubCategoryData(this.search, this.currentPage, this.active);

        },



            GotoPage: function (link) {
                this.$router.push({ path: link });
            },

            IsSave: function () {

                this.show = false;

                this.GetSubCategoryData(this.search, this.currentPage);
            },
            getPage: function () {
                this.GetSubCategoryData(this.search, this.currentPage);
            },
            AddSubCategory: function () {
                this.newSubCategory = {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    name: '',
                    nameArabic: '',
                    description: '',
                    categoryId: '',
                    isActive: true
                }

                this.show = !this.show;
                this.type = "Add";
            },
            GetSubCategoryData: function () {
                var root = this;
                var url = '/Product/GetSubCategoryInformation?isActive=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.subCategorylist = response.data.results.subCategories;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            EditSubCategoryInfo: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Product/SubCategoryDetailsViaId?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.newSubCategory = {
                            id: id,
                            code: response.data.code,
                            name: response.data.name,
                            nameArabic: response.data.nameArabic,
                            description: response.data.description,
                            categoryId: response.data.categoryId,
                            isActive: response.data.isActive
                        }

                        root.show = !root.show;
                        root.type = "Edit";
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
            this.GetSubCategoryData(this.search, 1);

        }
    }
</script>