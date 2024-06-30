<template>
<div class="row">

    <div class="col-lg-12">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">{{ $t('PriceRecord.PriceRecord') }}</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PriceRecord.Home') }}</a></li>
                                <li class="breadcrumb-item active">{{ $t('PriceRecord.PriceRecord') }}</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="card">
            <div class="card-header">
                <div class="row">
                    <div class="col-md-3 col-lg-3  col-12 form-group">
                        <label>Search By Name or Code:</label>
                        <input v-model="search" type="text" class="form-control" placeholder="Search By Name or Code" aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>

                    <div class="col-md-3 col-lg-3  col-12 form-group" v-if=" isValid('CanViewProduct')">
                        <label>{{ $t('Product.SearchbyProduct') }}</label>
                        <productMasterdropdown v-model="produtMasterId"></productMasterdropdown>
                    </div>
                    <div class="col-md-3 col-lg-3  col-12 form-group">
                        <label>{{ $t('Product.SearchbyCategory') }}</label>
                        <categorydropdown v-model="categoryId"></categorydropdown>
                    </div>
                    <div class="col-md-3 col-lg-3  col-12 form-group">
                        <label>{{ $t('Product.SearchbySubCategory') }}</label>
                        <multiselect v-model="subCategoryId" :options="subCategoryOptions" :show-labels="false" track-by="name" label="name" v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'arabicLanguage '" v-bind:placeholder="$t('AddProduct.PleaseSelectSubCategory')">
                        </multiselect>
                    </div>
                    <div class="col-md-3 col-lg-3  col-12 form-group">
                        <label>{{ $t('Product.SearchbyColor') }}</label>
                        <colordropdown v-model="colorId" />
                    </div>
                    <div class="col-md-3 col-lg-3  col-12 form-group">
                        <label>{{ $t('Product.SearchbySize') }}</label>
                        <sizedropdown v-model="sizeId" />
                    </div>
                    <div class="col-md-3 col-lg-3  col-12 form-group">
                        <label>Active/In-Active:</label>
                        <multiselect v-model="isActiveValue" :options="[ 'Active', 'In-Active']" :show-labels="false" placeholder="Select ">
                        </multiselect>
                    </div>
                    <div class="col-md-3 col-lg-3  col-12 form-group mt-3">
                        <div class="button-items" >
                        <button class="btn btn-outline-primary"  v-on:click="FilterRecord"><i class="far fa-save "></i>Filter</button>
                        <button class="btn btn-danger" v-on:click="ClearFilterRecord">Clear Filter</button>
                    </div>
                    </div>
                </div>
            </div>

            <div class="card-body px-4">
                <div class="table-responsive pb-5">
                    <table class="table mb-3">
                        <thead class="thead-light table-hover ">
                            <tr>

                                <th width="45%" class="p-3">
                                    {{ $t('PriceRecord.ProductName') }}
                                </th>
                                <th width="10%" class="text-center">
                                    {{ $t('PriceRecord.PurchasePrice') }}
                                </th>
                                <th width="10%" class="text-center">
                                    {{ $t('PriceRecord.CostPrice') }}
                                </th>
                                <th width="10%" class="text-center">
                                    {{ $t('PriceRecord.SalePrice') }}
                                </th>
                                <th width="10%" class="text-center">
                                   Price Type
                                </th>
                                <th width="10%" class="text-center">
                                    Price
                                </th>

                                <th width="15%">
                                    {{ $t('PriceRecord.Status') }}
                                </th>

                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="priceRecord in productList " v-bind:key="priceRecord.id">
                                <td class="p-3">
                                    {{(priceRecord.displayName == '' || priceRecord.displayName == null) ? priceRecord.englishName : priceRecord.displayName}}

                                </td>

                                <td class="text-center">
                                    {{priceRecord.purchasePrice}}

                                </td>
                                <td class="text-center">
                                    {{priceRecord.costPrice}}

                                </td>
                                <td class="text-center">
                                    {{priceRecord.salePrice}}

                                </td>
                                <td class="text-center">
                                    {{priceRecord.priceLabelName}}<br>

                                </td>
                                <td class="text-center">
                                    <decimal-to-fixed  v-model="priceRecord.price" />

                                </td>

                                <td>
                                    <input type="checkbox" id="inlineCheckbox1" v-model="priceRecord.isActive" />
                                   
                                </td>
                                

                            </tr>
                           
                        </tbody>
                     
                        <tfoot>
                            <tr>
                                <td class="text-end" colspan="7">
                                    <button type="button" class="btn btn-outline-primary btn-round " v-on:click="SavePriceRecordChange()">{{ $t('AddPriceRecord.Update') }}</button>                                </td>

                            </tr>
                            <!-- <tr>
                                <td colspan="4" class="text-center">
                                    <button title="Add New Account" class="btn btn-outline-primary btn-round mt-3 " v-on:click="openmodel(details)">
                                        <i class="mdi mdi-plus"></i>
                                    </button>
                                </td>
                            </tr> -->
                        </tfoot>
                    </table>
                </div>
                <hr />
                <div class="row">
                    <div class="col-lg-6">
                        <span v-if="currentPage===1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount < 100"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount >= 101  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*100}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*100}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*100)-99}} {{ $t('Pagination.to') }} {{currentPage*100}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*100)-99}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                    </div>
                    <div class=" col-lg-6">
                        <div class=" float-end" v-on:click="GetProductData()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                            </b-pagination>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <AddPriceRecord :newPriceRecord="newPriceRecord" :show="show" v-if="show" @close="IsSave" :type="type">

        </AddPriceRecord>
    </div>

</div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from 'vue-multiselect'

export default {
    components: {
        Multiselect,
    },
    mixins: [clickMixin],
    name: 'PriceRecord',
    data: function () {
        return {
            produtMasterId: '',
            colorId: '',
            sizeId: '',
            originId: '',
            categoryId: '',
            subCategoryId: '',

            isActiveValue: '',
            subCategoryOptions: [],

            arabic: '',
            english: '',
            show: false,
            type: '',
            showPrint: false,
            productData: '',
            productList: [

            ],
            warehouseId: '00000000-0000-0000-0000-000000000000',
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            searchQuery: '',
            image: '',
            loading: true,
            renderedImage: 0,
            advanceFilters: false,

            printRender: 0,
            colorlist: [],
            newPriceRecord: {
                id: '00000000-0000-0000-0000-000000000000',
                priceLabelingId: '00000000-0000-0000-0000-000000000000',
                product: [],
                price: 0,
                purchasePrice: 0,
                salePrice: 0,
                costPrice: 0,
                isActive: true
            },
        }
    },
    methods: {
        getSubcategory: function () {
            //this.catId = event;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            this.subCategoryOptions = [];
            this.$https.get('/Product/GetSubCategoryInformation?isActive=true', {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
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

        IsSave: function () {

            this.show = false;

            this.GetProductData(this.search, this.currentPage, this.warehouseId);
        },
        FilterRecord: function () {
            this.produtMasterId= '';
            this.sizeId= '';
            this.colorId= '';
            this.originId= '';
            this.categoryId= '';
            this.subCategoryId= '';
            this.isActiveValue= '';
            this.searchTerm= '';


            this.GetProductData(this.search, this.currentPage, this.warehouseId);
        },
        ClearFilterRecord: function () {


            this.GetProductData(this.search, this.currentPage, this.warehouseId);
        },

        openmodel: function (product) {

            this.newPriceRecord = {
                id: '00000000-0000-0000-0000-000000000000',
                priceLabelingId: '00000000-0000-0000-0000-000000000000',
                product: product,
                price: 0,
                purchasePrice: product.purchasePrice,
                salePrice: product.salePrice,
                costPrice: product.costPrice,
                isActive: true

            }
            this.show = !this.show;
            this.type = "Add";
        },
        EditPriceRecord: function (Id, details) {

            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/PriceRecordDetail?Id=' + Id, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                .then(function (response) {
                        if (response.data) {
                            root.newPriceRecord.id = response.data.id;
                            root.newPriceRecord.priceLabelingId = response.data.priceLabelingId;
                            root.newPriceRecord.price = response.data.price;
                            root.newPriceRecord.product = details;
                            root.newPriceRecord.purchasePrice = details.purchasePrice;
                            root.newPriceRecord.salePrice = details.salePrice;
                            root.newPriceRecord.costPrice = details.costPrice;
                            root.newPriceRecord.isActive = response.data.isActive;
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

        },

        getPage: function () {

            this.GetProductData(this.search, this.currentPage, this.warehouseId);
        },
        GetProductData: function () {

            var root = this;

            var url = '/Product/PriceRecordList?searchTerm=' + this.search+ '&sizeId=' + this.sizeId + '&isActiveValue=' + this.isActiveValue+ '&subCategoryId=' + this.subCategoryId + '&subCategoryId=' + this.subCategoryId + '&pageNumber=' + this.currentPage + '&colorId=' + this.colorId + '&originId=' + this.originId + '&categoryId=' + this.categoryId + '&produtMasterId=' + this.produtMasterId + '&pageSize=100';
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get(url, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
                if (response.data != null) {

                    root.productList = response.data.results;
                    root.$store.GetPriceLabelingRecord(root.productList);
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },

    },
    created: function () {

    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        if (this.$route.query.data != undefined) {
            this.currentPage = this.$route.query.data;
            this.GetProductData();
        } else {
            this.GetProductData(this.search, 1);
        }
    }
}
</script>

<style scoped>
.higlight {
    background-color: red;
}
</style>
