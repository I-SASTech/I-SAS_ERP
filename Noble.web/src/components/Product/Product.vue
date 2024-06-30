<template>

    <div v-if="isValid('CanViewItem')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Product.ListOfITems') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Product.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Product.ListOfITems') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">

                                <a v-if="isValid('CanAddItem')" v-on:click="AddProduct"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Product.AddNew') }}
                                </a>

                                <a v-if="isValid('CanImportProduct')" v-on:click="ImportDataFromCsv"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{$t('Product.ImportItems')}}
                                </a>

                                <a class="btn btn-sm btn-outline-primary ms-1" v-on:click="OpenProductModel"
                                   href="javascript:void(0);">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Product.Qty') }}
                                </a>

                                <a class="btn btn-sm btn-outline-primary mx-1" v-on:click="GetFilterPdfRecord(true)">
                                    PDF
                                    <i class="fas fa-file-pdf float-right"></i>
                                </a>
                                <a class="btn btn-sm btn-outline-primary mx-1" v-on:click="DowmloadCSV()">
                                    Export Excel
                                    <i class="fas fa-file-pdf float-right"></i>
                                </a>

                                <a class="btn btn-sm btn-outline-primary mx-1" v-on:click="SelectBranchesModel()" v-if="allowBranches">
                                    Sync Branches
                                </a>

                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Origin.Close') }}
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
                                <button class="btn btn-soft-primary" type="button" id="button-addon1" v-on:click="SearchFilter()">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" class="form-control" :placeholder="$t('Product.SearchbyProductNameorCode')"
                                       aria-label="Example text with button addon" aria-describedby="button-addon1, button-addon2">
                                <a class="btn btn-outline-primary" v-on:click="FilterWareHouse" id="button-addon2">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

                            <button v-on:click="search22(true)" :disabled="!search" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled="!search" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>
                    <br id="hide" v-if="advanceFilters" />

                    <div class="row">
                        <!--<div class="col-md-3 col-lg-3  col-12 form-group" id="hide" v-if="advanceFilters">
                            <label>{{ $t('Product.SearchbyWareHosue') }}</label>
                            <warehouse-dropdown v-model="warehouseId" :key="render" />
                        </div>-->

                        <div class="col-md-3 col-lg-3  col-12 form-group" id="hide" v-if="advanceFilters && isValid('CanViewProduct')">
                            <label>{{ $t('Product.SearchbyProduct') }}</label>
                            <productMasterdropdown v-model="produtMaster" :isMultiple="true" :key="render"></productMasterdropdown>
                        </div>
                        <div class="col-md-3 col-lg-3  col-12 form-group" id="hide" v-if="advanceFilters">
                            <label>{{ $t('Product.SearchbyCategory') }}</label>
                            <categorydropdown v-model="category" :isMultiple="true" :key="render"></categorydropdown>
                        </div>
                        <div class="col-md-3 col-lg-3  col-12 form-group" id="hide" v-if="advanceFilters">
                            <label>{{ $t('Product.SearchbySubCategory') }}</label>
                            <multiselect v-model="subCategory" :multiple="true" :options="subCategoryOptions" :key="render" :show-labels="false" track-by="name" label="name" v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'arabicLanguage '" v-bind:placeholder="$t('AddProduct.PleaseSelectSubCategory')">
                            </multiselect>
                        </div>
                        <div class="col-md-3 col-lg-3  col-12 form-group" id="hide" v-if="advanceFilters">
                            <label>{{ $t('Product.SearchbyColor') }}</label>
                            <colordropdown v-model="color" :isMultiple="true" :key="render" />
                        </div>
                        <div class="col-md-3 col-lg-3  col-12 form-group" id="hide" v-if="advanceFilters">
                            <label>{{ $t('Product.SearchbySize') }}</label>
                            <sizedropdown v-model="size" :isMultiple="true" :key="render" />
                        </div>
                        <div class="col-md-3 col-lg-3  col-12 form-group" id="hide" v-if="advanceFilters">
                            <label>{{ $t('Product.SearchbyOrigin') }}</label>
                            <origindropdown v-model="origin" :isMultiple="true" @update:modelValue="GetFilter('Origin')" />
                        </div>
                        <div class="col-md-3 col-lg-3  col-12 form-group" id="hide" v-if="advanceFilters && allowBranches">
                            <label>Branches</label>
                            <branch-dropdown v-model="brancheslist" @update:modelValue="GetProductData()" />
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3" id="hide" v-if="advanceFilters">

                            <button v-on:click="SearchFilter()" :disabled="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="ClearFilter()" :disabled="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mx-2 mt-3">
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
                                    <th v-if="isValid('AllowBranches')">
                                        <span class="me-1" v-if="isSelectAll">
                                            <input type="checkbox" v-model="branches.isSelectAll" id="ProductSelectAll" v-on:change="SelectAllProducts(false)" />
                                        </span>
                                        <span class="me-1" v-if="!isSelectAll">
                                            <input type="checkbox" v-model="branches.isSelectAll" id="ProductSelectAlla" v-on:change="SelectAllProducts(true)" />
                                        </span>
                                        <span>All</span>
                                    </th>
                                    <th v-for="name in itemListHeader" :key="name">{{ name }}</th>

                                    <th class="text-center" v-if="isValid('CanViewDetailItem')">
                                        {{ $t('Product.ViewDetail') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(details,index) in productList" v-bind:key="details.id">
                                    <td v-if="isSelectAll && isValid('AllowBranches')" :key="selectAllRender">
                                        <input type="checkbox" :id="1.01 + index" :checked="isSelectAll" :key="selectAllRender" />
                                    </td>
                                    <td v-if="!isSelectAll && isValid('AllowBranches')" :key="selectAllRender1">
                                        <input type="checkbox" :id="1.02 + index" v-on:change="SelectProduct(details.id)" />
                                    </td>
                                    <template v-for="item in itemListViewSetup">
                                        <td :key="item.name" v-if="(details.image==null || details.image=='' || details.image==undefined) && item.name =='Image'"><img src="images/Product.png" style="width: 25px;" /></td>
                                        <td :key="item.name" v-else-if="(details.image!=null || details.image!='' || details.image!=undefined) && item.name =='Image'"><productimage v-bind:path="details.image" /></td>
                                        <td v-if="item.name == 'Item Name'" :key="item.name" v-on:click="EditProductInfo(details.id)" id="forCursor">
                                            <span v-if="details.isRaw">
                                                <span><b>{{details.displayName}}</b></span>
                                                <label style="color:red">{{ $t('Product.RawMaterial') }}</label>
                                            </span>
                                            <span v-else>
                                                <b>{{details.displayName}}</b>
                                            </span>
                                        </td>
                                        <td :key="item.name" v-if="isValid('CanEditItem') && item.name == 'Item Code'">
                                            <strong v-if="details.isRaw">
                                                <a href="javascript:void(0)" style="color:red" v-on:click="EditProductInfo(details.id)">{{details.code}}</a>
                                            </strong>
                                            <strong v-else>
                                                <a href="javascript:void(0)" v-on:click="EditProductInfo(details.id)">{{details.code}}</a>
                                            </strong>
                                        </td>
                                        <td :key="item.name" v-if="!isValid('CanEditItem') && item.name == 'Item Code'" v-bind:style="details.isRaw == true ? 'color:!important red' : ''"> {{details.code}}</td>
                                        <td :key="item.name" v-if="item.name == 'Sale Price'" class="text-center">{{details.salePrice}}</td>
                                        <td :key="item.name" v-if="item.name == 'Purchase Price'" class="text-center">{{details.purchasePrices}}</td>
                                        <td :key="item.name" v-if="item.name == 'BarCode Type'">{{ details.barCodeType }}</td>
                                        <td :key="item.name" class="text-center" v-if="isValid('CanViewCurrentStock') && item.name == 'Current Quantity'">
                                            <div v-if="details.inventory != null && details.currentQuantity > 0">
                                                {{details.currentQuantity}}
                                            </div>

                                            <div v-else class="badge bg-danger">{{ details.inventory == null ? 0 : details.currentQuantity}} {{ $t('Product.LowStock') }}</div>
                                        </td>

                                        <td :key="item.name" v-if="item.name == 'SKU'">{{ details.sku }}</td>
                                        <td :key="item.name" v-if="item.name == 'Item Style/Model'">{{ details.styleNumber }}</td>
                                        <td :key="item.name" v-if="item.name == 'Part Number'">{{ details.partNumber }}</td>
                                        <td :key="item.name" v-if="item.name == 'Item Category'">{{ details.categoryNameEn }}</td>
                                        <td :key="item.name" v-if="item.name == 'Item Brand'">{{ details.brandEnglish }}</td>
                                        <td :key="item.name" v-if="item.name == 'Item Origin'">{{ details.originNameEn }}</td>
                                        <td :key="item.name" v-if="item.name == 'Item Size'">{{ details.sizeName }}</td>
                                        <td :key="item.name" v-if="item.name == 'Item Color'">{{ details.colorName }}</td>
                                        <td :key="item.name" v-if="item.name == 'Item Unit'">{{ details.unitNameEn }}</td>

                                    </template>

                                    <td class="text-center" v-if="isValid('CanViewDetailItem')">
                                        <button value="View" class="btn btn-icon btn-sm  btn-outline-primary" v-on:click="ViewProductInfo(details.id)"><i class="fas fa-eye"></i></button>
                                    </td>

                                </tr>


                            </tbody>
                        </table>
                    </div>
                    <hr />
                    <div class="float-start">
                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount < 40">
                            {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === 1 && rowCount >= 41">
                            {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 40 }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === 1">
                            {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                $t('Pagination.to')
                            }} {{ currentPage * 40 }} of {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                            {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 40) - 39 }} {{ $t('Pagination.to') }} {{ currentPage * 40 }} {{
                                    $t('Pagination.of')
                            }} {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === pageCount">
                            {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 40) - 39 }} {{ $t('Pagination.to') }} {{ rowCount }} {{$t('Pagination.of')}}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                    </div>
                    <div class="float-end">
                        <div class="" v-on:click="GetProductData()">
                            <b-pagination pills size="sm" v-model="currentPage"
                                          :total-rows="rowCount"
                                          :per-page="40"
                                          :first-text="$t('Table.First')"
                                          :prev-text="$t('Table.Previous')"
                                          :next-text="$t('Table.Next')"
                                          :last-text="$t('Table.Last')">
                            </b-pagination>
                        </div>
                    </div>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>
            <productfiltermodel :show="show" v-if="show" @close="show=false" />
        </div>

        <ProductFilterDownloadReport :headerFooter="headerFooter" :printDetails="filterProduct" v-if="isDownload" v-bind:key="printRenderPdf"></ProductFilterDownloadReport>
        <productPrintReport v-if="showPrint" :printDetails="productData" :newheaderFooter="headerFooter" v-bind:key="printRender"></productPrintReport>
        <select-branches-model :show="showSelectBranches"
                               v-if="showSelectBranches"
                               @save="SaveBranchesModel"
                               @close="CloseModel()" />
    </div>
    <div v-else>  <acessdenied></acessdenied></div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'

    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    export default {
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading

        },
        name: 'product',
        data: function () {
            return {
                allowBranches: false,
                brancheslist: '',
                isSelectAll: false,
                selectAllRender: 0,
                selectAllRender1: 0,
                showSelectBranches: false,
                branchIds: [],
                branches: {
                    isSelectAll: false,
                    branchIds: [],
                    productIds: [],
                },

                arabic: '',
                produtMaster: '',
                size: '',
                origin: '',
                english: '',
                category: '',
                subCategory: '',
                productMasterId: '',
                categoryId: '',
                subCategoryId: '',
                sizeId: '',
                color: '',
                colorId: '',
                originId: '',
                show: false,
                download: false,
                isDownload: false,
                type: '',
                showPrint: false,
                productData: '',
                productFilter: {
                    productMasterId: '',
                    categoryId: '',
                    subCategoryId: '',
                    sizeId: '',
                    colorId: '',
                    originId: '',
                    warehouseId: '',
                    searchTerm: '',
                    pageNumber: 1,
                    isDropdown: false,
                    branchId: '',
                    pageSize: 40,
                },
                filterProduct: [],
                subCategoryOptions: [],
                productList: [

                ],
                warehouseId: '',
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                
                searchQuery: '',
                image: '',
                render: 0,
                loading: true,
                renderedImage: 0,
                printRenderPdf: 0,
                advanceFilters: false,
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                printRender: 0,
                itemViewSetupList: [],
                itemViewSetupListForPrint: [],
                itemListViewSetup: [],

                itemListHeader: [],
            }
        },
        computed: {
            isFilterButtonDisabled() {
                return (
                    this.warehouseId ||
                    this.produtMaster ||
                    this.category ||
                    this.subCategory ||
                    this.color ||
                    this.size ||
                    this.origin ||
                    this.brancheslist
                );
            },
        },

        methods: {
            getItemHeaderList: function () {
                this.itemListHeader = [];
                this.itemListViewSetup.forEach(value => {
                    if (value.name == "Item Code") {
                        this.itemListHeader.push(this.$t('AddProduct.ProductCode'));
                    }
                    else if (value.name == "Item Name") {
                        this.itemListHeader.push(this.$t('AddProduct.ItemName'));
                    }
                    else if (value.name == "Sale Price") {
                        this.itemListHeader.push(this.$t('AddProduct.salePrice'));
                    }
                    else if (value.name == "Purchase Price") {
                        this.itemListHeader.push(this.$t('AddProduct.PurchasePrice'));
                    }
                    else if (value.name == "BarCode Type") {
                        this.itemListHeader.push('BarCode Type');
                    }
                    else if (value.name == "Current Quantity") {
                        this.itemListHeader.push(this.$t('Current Quantity'));
                    }
                    else if (value.name == "Image") {
                        this.itemListHeader.push(this.$t('Product.Image'));
                    }
                    else if (value.name == "Item Style/Model") {
                        this.itemListHeader.push(this.$t('Item Style/Model'));
                    }
                    else if (value.name == "SKU") {
                        this.itemListHeader.push(this.$t('SKU'));
                    }
                    else if (value.name == "Part Number") {
                        this.itemListHeader.push(this.$t('Part Number'));
                    }
                    else if (value.name == "Item Category") {
                        this.itemListHeader.push(this.$t('Category'));
                    }
                    else if (value.name == "Item SubCategory") {
                        this.itemListHeader.push(this.$t('SubCategory'));
                    }
                    else if (value.name == "Item Brand") {
                        this.itemListHeader.push(this.$t('Brand'));
                    }
                    else if (value.name == "Item Origin") {
                        this.itemListHeader.push(this.$t('Origins'));
                    }
                    else if (value.name == "Item Size") {
                        this.itemListHeader.push(this.$t('Sizes'));
                    }
                    else if (value.name == "Item Color") {
                        this.itemListHeader.push(this.$t('Color'));
                    }
                    else if (value.name == "Item Unit") {
                        this.itemListHeader.push(this.$t('Units'));
                    }
                });

            },
            search22: function () {
                this.GetProductData(this.search, this.currentPage, this.warehouseId);
            },
            SelectProduct: function (val) {
                this.branches.productIds.push({ id: val, name: '' });
            },
            SaveBranchesModel: function (val) {
                this.branches.branchIds = val;
                this.showSelectBranches = !this.showSelectBranches;
                this.SaveProductsAganistBranches();
            },
            CloseModel: function (x) {
                this.aa = x;
                this.showSelectBranches = !this.showSelectBranches;
            },
            SelectBranchesModel: function () {
                this.showSelectBranches = !this.showSelectBranches;
            },
            SelectAllProducts: function (val) {
                this.isSelectAll = val;
                this.selectAllRender++;

            },
            SaveProductsAganistBranches: function () {
                var root = this;
                var url = '/Branches/SaveBranchesProducts';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.loading = true;

                root.$https.post(url, this.branches, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        if (response.data.isSuccess) {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: response.data.isAddUpdate,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                            root.branches.isSelectAll = false;
                            root.isSelectAll = false;
                            root.selectAllRender1++;
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: "Your Branch has been updated!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                        }
                    }
                    root.loading = false;
                });
            },
            clearData: function () {
                this.search = "";
                this.GetProductData(this.search, this.currentPage, this.warehouseId);

            },

            ClearFilter() {
                // Reset the filter conditions here
                this.warehouseId = '';
                this.productMasterId = '';
                this.categoryId = '';
                this.colorId = '';
                this.sizeId = '';
                this.subCategoryId = '';
                this.originId = '';
                this.currentPage = 1;
                this.GetProductData(this.search, 1, this.warehouseId);
                this.getSubcategory();
                this.render++;
                // Trigger the search or data refresh

                this.GetProductData(this.search, 1);
            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            GetHeaderDetail: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;
                            root.getBase64Image(root.headerFooter.company.logoPath);
                        }
                    });
            },
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        }
                    });
            },
            OpenProductModel: function () {
                this.show = !this.show;
            },
            getSubcategory: function () {
                //this.catId = event;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }


                this.subCategoryOptions = [];
                this.$https.get('/Product/GetSubCategoryInformation?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        response.data.results.subCategories.forEach(function (rout) {

                            root.subCategoryOptions.push({
                                id: rout.id,
                                name: root.$i18n.locale == 'en' ? (rout.name != "" ? rout.code + ' ' + rout.name : rout.code + ' ' + rout.nameArabic) : (rout.nameArabic != '' ? rout.code + ' ' + rout.nameArabic : rout.code + ' ' + rout.name)
                            })
                        })
                    }
                })
            },
            GetFilter: function (filterName) {

                if (filterName == 'productMaster') {
                    if (this.produtMaster == null || this.produtMaster == undefined) {
                        this.productMasterId = '';
                    }
                    else {
                        this.productMasterId = this.getOriginId(this.produtMaster);

                    }
                }
                else if (filterName == 'Category') {

                    if (this.categoryId == null || this.categoryId == undefined) {
                        this.categoryId = '';
                    }
                    else {
                        this.categoryId = this.getOriginId(this.category);

                    }
                    //else {
                    //    this.getSubcategory(this.categoryId)
                    //}



                }
                else if (filterName == 'Color') {

                    if (this.colorId == null || this.colorId == undefined) {
                        this.colorId = '';
                    }
                    else {
                        this.colorId = this.getOriginId(this.color);

                    }



                }
                else if (filterName == 'Size') {

                    if (this.sizeId == null || this.sizeId == undefined) {
                        this.sizeId = '';
                    }
                    else {
                        this.sizeId = this.getOriginId(this.size);

                    }



                }
                else if (filterName == 'SubCategory') {

                    if (this.subCategoryId == null || this.subCategoryId == undefined) {
                        this.subCategoryId = '';
                    }
                    else {
                        this.subCategoryId = this.getOriginId(this.subCategory);
                    }



                }
                else if (filterName == 'Origin') {


                    if (this.originId == null || this.originId == undefined) {
                        this.originId = '';
                    }
                    else {
                        this.originId = this.getOriginId(this.origin);

                    }



                }


                this.GetProductData();

            },

            SearchFilter: function () {
                this.warehouseId;
                this.originId = this.getOriginId(this.origin);
                this.subCategoryId = this.getOriginId(this.subCategory);
                this.sizeId = this.getOriginId(this.size);
                this.colorId = this.getOriginId(this.color);
                this.categoryId = this.getOriginId(this.category);
                this.productMasterId = this.getOriginId(this.produtMaster);

                this.GetProductData(this.search, 1);
            },

            getOriginId: function (value) {
                var originId = [];
                for (var i = 0; i < value.length; i++) {
                    originId[i] = value[i].id
                }
                return originId;
            },
            FilterWareHouse: function () {

                this.advanceFilters = !this.advanceFilters;
                if (this.advanceFilters == false) {
                    this.warehouseId = '';
                    this.productMasterId = '';
                    this.categoryId = '';
                    this.colorId = '';
                    this.sizeId = '';
                    this.subCategoryId = '';
                    this.originId = '';
                    this.currentPage = 1;
                    this.brancheslist = '';
                    this.productFilter.branchId = '';
                    this.GetProductData(this.search, 1, this.warehouseId);
                    this.getSubcategory();
                }


            },
            ImportDataFromCsv: function () {
                var root = this;
                root.$router.push({
                    path: '/ImportExportRecords',
                    query: { data: 'Item' }
                })
            },

            getPage: function () {
                this.GetProductData(this.search, this.currentPage, this.warehouseId);
            },
            GetFilterPdfRecord: function () {


                var root = this;

                this.filterProduct = [];
                this.loading = true;
                this.productFilter.searchTerm = this.search;
                this.productFilter.pageNumber = this.currentPage;
                this.productFilter.warehouseId = this.warehouseId == '' ? null : this.warehouseId;
                this.productFilter.productMasterId = this.productMasterId == '' ? null : this.productMasterId;
                this.productFilter.categoryId = this.categoryId == '' ? null : this.categoryId;
                this.productFilter.subCategoryId = this.subCategoryId == '' ? null : this.subCategoryId;
                this.productFilter.colorId = this.colorId == '' ? null : this.colorId;
                this.productFilter.sizeId = this.sizeId == '' ? null : this.sizeId;
                this.productFilter.originId = this.originId == '' ? null : this.originId;
                this.productFilter.isDropdown = true;
                this.productFilter.pageSize = 40;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var url = '/Product/GetProductFilterInformation';

                root.$https.post(url, this.productFilter, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.filterProduct = response.data.results.products;
                        root.isDownload = true;
                        root.loading = false;

                        root.printRenderPdf++;

                    }
                });
            },
            DowmloadCSV: function () {
                var root = this;
                var token = localStorage.getItem('token');
                root.loading = true;

                this.filterProduct = [];
                this.loading = true;
                this.productFilter.searchTerm = this.search;
                this.productFilter.pageNumber = this.currentPage;
                this.productFilter.warehouseId = this.warehouseId == '' ? null : this.warehouseId;
                this.productFilter.productMasterId = this.productMasterId == '' ? null : this.productMasterId;
                this.productFilter.categoryId = this.categoryId == '' ? null : this.categoryId;
                this.productFilter.subCategoryId = this.subCategoryId == '' ? null : this.subCategoryId;
                this.productFilter.colorId = this.colorId == '' ? null : this.colorId;
                this.productFilter.sizeId = this.sizeId == '' ? null : this.sizeId;
                this.productFilter.originId = this.originId == '' ? null : this.originId;
                this.productFilter.isDropdown = true;

                root.$https.post('/Product/GetProductListExcel', this.productFilter, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'Product Report.xlsx');
                        document.body.appendChild(link);
                        link.click();
                        root.loading = false;

                    }).catch(error => {
                        console.log(error);
                        root.loading = false;
                        root.$swal.fire({
                            icon: 'error',
                            type: 'error',
                            title: root.$t('Error'),
                            text: root.$t('Something went Wrong'),
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                    });
            },
            GetItemViewSetup:  function () {
                

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Product/GetItemsViewSetup', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.itemViewSetupList = [];
                        root.itemViewSetupListForPrint = [];

                        root.itemViewSetupList = response.data.itemViewSetupList;
                        root.itemViewSetupListForPrint = response.data.itemViewSetupListForPrint;
                        root.itemListViewSetup = response.data.itemListViewSetup;

                        root.getItemHeaderList();
                        root.GetProductData()
                    }
                });
            },
            GetProductData: function () {


                if (localStorage.getItem("BarcodeScan") != 'Product')
                    return
                var root = this;

                this.productFilter.searchTerm = this.search;
                this.productFilter.pageNumber = this.currentPage;
                this.productFilter.branchId = this.brancheslist;
                this.productFilter.isDropdown = false;

                this.productFilter.warehouseId = this.warehouseId == '' ? null : this.warehouseId;
                this.productFilter.productMasterId = this.productMasterId == '' ? null : this.productMasterId;
                this.productFilter.categoryId = this.categoryId == '' ? null : this.categoryId;
                this.productFilter.subCategoryId = this.subCategoryId == '' ? null : this.subCategoryId;
                this.productFilter.colorId = this.colorId == '' ? null : this.colorId;
                this.productFilter.sizeId = this.sizeId == '' ? null : this.sizeId;
                this.productFilter.originId = this.originId == '' ? null : this.originId;
                this.productFilter.pageSize = 40;


                var url = '/Product/GetProductFilterInformation';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.post(url, this.productFilter, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.productList = response.data.results.products;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                    
                    if (root.itemViewSetupList.length == 0 || root.itemViewSetupList.length == 0) {
                        root.$swal({
                            title: 'Product Display Name',
                            text: "Please setup product display name first by click on button below ↓",
                            icon: 'warning',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Product Display Name Setup'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                root.$router.push({
                                    path: '/additemviewsetup',
                                })
                            }
                        });
                    }
                });
            },
            
            AddProduct: function () {
                var root = this;
                if (root.itemViewSetupList.length == 0 || root.itemViewSetupListForPrint.length == 0) {
                    root.$swal({
                        title: 'Product Display Name',
                        text: "Please setup product display name first by click on button below ↓",
                        icon: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Product Display Name Setup'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            root.$router.push({
                                path: '/additemviewsetup',
                            })
                        }
                    });
                }
                else {
                    root.$router.push({
                        path: '/addproduct',
                        query: {
                            Add: true,
                            prop: Math.ceil(root.rowCount / 40)
                        }
                    })
                }
            },
            EditProductInfo: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Product/ProductDetailsViaId?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/addproduct',
                            query: {
                                data: '',
                                Add: false,
                                prop: root.currentPage
                            }
                        })
                    }
                });
            },
            PrintProductInfo: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Product/ProductViewDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.productData = response.data;
                        root.showPrint = true;
                        root.printRender++

                    }
                });
            },
            ViewProductInfo: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Product/ProductViewDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.$router.push({
                            path: '/ProductView',
                            query: {
                                data: response.data,
                                prop: root.currentPage,

                            }
                        })
                    }
                });
            },
        },

        created: function () {
            this.GetItemViewSetup();


            this.$emit('update:modelValue', this.$route.name);
            localStorage.setItem("BarcodeScan", 'Product');
            


            this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
        },
        mounted: function () {

            this.GetHeaderDetail();
            this.showPrint = false;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            // if (this.$route.query.data != undefined) {
            //     this.currentPage = this.$route.query.data;
            //     this.GetProductData();
            // }
            // else {
            //     this.GetProductData(this.search, 1);
            // }
        }
    }
</script>
<style scoped>
#forCursor:hover{
    cursor: pointer;
}
</style>