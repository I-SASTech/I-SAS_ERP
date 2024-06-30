<template>
    <div class="row" v-if="isValid('CanViewCategory')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Categories.ProductCategoryList') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Categories.Home')
                                    }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Categories.ProductCategoryList') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddCategory')" v-on:click="AddCategory" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Categories.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
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
                                <button class="btn btn-secondary" type="button" id="button-addon1"><i
                                        class="fas fa-search"></i></button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('Categories.Search')" aria-label="Example text with button addon"
                                    aria-describedby="button-addon1">
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
                                        {{ $t('Categories.Code') }}
                                    </th>
                                    <th v-if="english == 'true'" width="20%">
                                        {{$englishLanguage($t('Categories.CategoryName'))   }}
                                    </th>
                                    <th v-if="isOtherLang()" width="20%">
                                        {{$arabicLanguage($t('Categories.CategoryNameAr'))   }}
                                    </th>

                                    <th width="35%">
                                        {{ $t('Categories.Description') }}
                                    </th>
                                    <th width="10%" class="text-center">
                                        {{ $t('Categories.RDays') }}
                                    </th>
                                    <th width="5%">
                                        {{ $t('Categories.Service') }}
                                    </th>
                                    <th width="5%">
                                        {{ $t('Categories.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(details, index) in categorylist" v-bind:key="details.id">
                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>
                                    <td v-if="isValid('CanEditCategory')">
                                        <strong>
                                            <a href="javascript:void(0)"
                                                v-on:click="EditCategoryInfo(details.id)">{{ details.code }}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        <strong>
                                            {{ details.code }}
                                        </strong>
                                    </td>

                                    <td v-if="english == 'true'">{{ details.name }}</td>
                                    <td v-if="isOtherLang()">{{ details.nameArabic }}</td>
                                    <td>{{ details.description }}</td>
                                    <td class="text-center">{{ details.returnDays }}</td>
                                    <td>
                                        <span v-if="details.isService"
                                            class="badge badge-boxed  badge-outline-success">{{ $t('Categories.Yes') }}</span>
                                        <span v-else
                                            class="badge badge-boxed  badge-outline-danger">{{ $t('Categories.No') }}</span>
                                    </td>
                                    <td>
                                        <span v-if="details.isActive"
                                            class="badge badge-boxed  badge-outline-success">{{ $t('Categories.Active') }}</span>
                                        <span v-else
                                            class="badge badge-boxed  badge-outline-danger">{{ $t('Categories.De-Active') }}</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-lg-6">
                            <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                            <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }}
                                {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                            <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing') }}
                                {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                                {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                            <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                $t('Pagination.to') }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries')
    }}</span>
                            <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{ $t('Pagination.Showing') }}
                                {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of')
                                }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                            <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }}
                                {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        </div>
                        <div class=" col-lg-6">
                            <div class="float-end" v-on:click="GetCategoryData()">
                                <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                    :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                    :next-text="$t('Table.Next')" :last-text="$t('Table.Last')"></b-pagination>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <categorymodal :show="show" :newcategory="newCategory" v-if="show" @close="show = false" @IsSave="IsSave"
                :type="type" />
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
    name: 'category',
    data: function () {
        return {
            arabic: '',
            english: '',
            searchQuery: '',
            show: false,
            type: '',
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            categorylist: [

            ],
            newCategory: {
                id: '',
                code: '',
                name: '',
                nameArabic: '',
                description: '',
                purchaseAccount: '',
                cOSQAccount: '',
                inventoryAccount: '',
                incomeAccount: '',
                saleAccount: '',
                isActive: true,
                returnDays: 0,
                isReturn: false,
                isService: false,
            }

        }
    },
    watch: {
        // search: function (val) {
        //     this.GetCategoryData(val, 1);
        // }
    },

    methods: {
        search22: function () {
            this.GetCategoryData(this.search, this.currentPage, this.active);
        },

        clearData: function () {
            this.search = "";
            this.GetCategoryData(this.search, this.currentPage, this.active);

        },


        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        ExportData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var path = '/Template/Category.csv'
            var ext = path.split('.')[1];
            root.$https.get('/Product/ExportData?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    const url = window.URL.createObjectURL(new Blob([response.data]));
                    const link = document.createElement('a');
                    link.href = url;
                    link.setAttribute('download', 'file.' + ext);
                    document.body.appendChild(link);
                    link.click();
                }

            });
        },
        IsSave: function () {

            this.GetCategoryData(this.search, this.currentPage);
        },
        getPage: function () {
            this.GetCategoryData(this.search, this.currentPage);
        },
        ImportdCsv: function () {
            this.$router.push('/ImportCategory')
        },
        AddCategory: function () {
            this.newCategory = {
                id: '00000000-0000-0000-0000-000000000000',
                code: '',
                name: '',
                nameArabic: '',
                description: '',
                purchaseAccount: '',
                cOSQAccount: '',
                inventoryAccount: '',
                incomeAccount: '',
                saleAccount: '',
                isActive: true,
                isReturn: false,
                isService: false,
                returnDays: 0,

            }

            this.show = !this.show;
            this.type = "Add";
        },
        GetCategoryData: function () {
            var root = this;
            var url = '/Product/GetCategoryInformation?isActive=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.categorylist = response.data.results.categories;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });

        },
        EditCategoryInfo: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/CategoryDetailsViaId?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.newCategory = {
                        id: id,
                        code: response.data.code,
                        name: response.data.name,
                        nameArabic: response.data.nameArabic,
                        description: response.data.description,
                        purchaseAccountId: response.data.purchaseAccountId,
                        cOGSAccountId: response.data.cogsAccountId,
                        inventoryAccountId: response.data.inventoryAccountId,
                        incomeAccountId: response.data.incomeAccountId,
                        saleAccountId: response.data.saleAccountId,
                        isActive: response.data.isActive,
                        returnDays: response.data.returnDays,
                        isReturn: response.data.isReturn,
                        isService: response.data.isService,
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
        this.GetCategoryData(this.search, 1);
    }
}
</script>