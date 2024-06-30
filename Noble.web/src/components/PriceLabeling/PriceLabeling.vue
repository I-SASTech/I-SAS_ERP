<template>
    <div class="row">

        <div class="col-12">
            <ul class="nav nav-tabs" data-tabs="tabs">
                <li class="nav-item">
                    <a class="nav-link" v-bind:class="{ active: active == 'PriceType' }"
                        v-on:click="makeActive('PriceType')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home"
                        role="tab" aria-controls="v-pills-home" aria-selected="true">
                        {{ $t('Product.PriceType') }}
                    </a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" v-bind:class="{ active: active == 'PriceRecord' }"
                        v-on:click="makeActive('PriceRecord')" id="v-pills-profile-tab" data-toggle="pill"
                        href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">
                        {{ $t('Product.AddPriceRecord') }}
                    </a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" v-bind:class="{ active: active == 'PriceRecordChange' }"
                        v-on:click="makeActive('PriceRecordChange')" id="v-pills-profile-tab" data-toggle="pill"
                        href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">
                        {{ $t('Product.EditPriceRecord') }}
                    </a>
                </li>

            </ul>
        </div>

        <div class="col-lg-12" v-if="active == 'PriceType'">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('PriceLabeling.PriceLabeling') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PriceLabeling.Home')
                                    }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('PriceLabeling.PriceLabeling') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="openmodel" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('PriceLabeling.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('PriceLabeling.Close') }}
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
                                    :placeholder="$t('PriceLabeling.Search')" aria-label="Example text with button addon"
                                    aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

<button v-on:click="search22(true)" :disabled="!search" type="button" class="btn btn-outline-primary mt-3">
    {{ $t('Sale.ApplyFilter') }}
</button>
<button v-on:click="clearData(false)" :disabled="!search"  type="button" class="btn btn-outline-primary mx-2 mt-3">
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
                                    <th width="20%">
                                        {{ $t('PriceLabeling.Code') }}
                                    </th>
                                    <th v-if="english == 'true'" width="30%">
                                        {{ $t('PriceLabeling.PriceLabelingNameEnglish') }}
                                    </th>
                                    <th v-if="isOtherLang()" width="30%">
                                        {{ $t('PriceLabeling.PriceLabelingNameArabic') }}
                                    </th>

                                    <!-- <th width="40%">
                                        {{ $t('PriceLabeling.Price') }}
                                    </th> -->
                                    <th width="20%">
                                        {{ $t('PriceLabeling.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(priceLabeling, index) in priceLabelinglist" v-bind:key="priceLabeling.id">
                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>
                                    <td>
                                        <strong>
                                            <a href="javascript:void(0)"
                                                v-on:click="EditPriceLabeling(priceLabeling.id)">{{ priceLabeling.code }}</a>
                                        </strong>
                                    </td>
                                    <td v-if="english == 'true'">
                                        {{ priceLabeling.name }}
                                    </td>
                                    <td v-if="isOtherLang()">
                                        {{ priceLabeling.nameArabic }}
                                    </td>

                                    <!-- <td>
                                        {{priceLabeling.price}}
                                    </td> -->
                                    <td>
                                        <span v-if="priceLabeling.isActive"
                                            class="badge badge-boxed  badge-outline-success">{{ $t('PriceLabeling.Active') }}</span>
                                        <span v-else
                                            class="badge badge-boxed  badge-outline-danger">{{ $t('PriceLabeling.De-Active') }}</span>
                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />

                    <div class="float-start">
                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }} {{ currentPage }}
                            {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                            $t('Pagination.to') }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{ (currentPage * 10) - 9 }}
                            {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                $t('Pagination.entries') }}</span>
                    </div>
                    <div class="float-end">
                        <div class="" v-on:click="GetPriceLabelingData()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                            </b-pagination>
                        </div>
                    </div>
                </div>
            </div>
            <priceLabelingmodel :newPriceLabel="newPriceLabeling" :show="show" v-if="show" @close="IsSavePriceLabeling"
                :type="type" />
        </div>
        <div class="col-lg-12" v-else-if="active == 'PriceRecord'">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('PriceRecord.PriceRecord') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('PriceRecord.Home')
                                    }}</a></li>
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
                            <label>{{ $t('Product.SearchByNameorCode') }}</label>
                            <input v-model="search" type="text" class="form-control"
                                :placeholder="$t('Product.SearchByNameorCode')" aria-label="Example text with button addon"
                                aria-describedby="button-addon1">
                        </div>

                        <div class="col-md-3 col-lg-3  col-12 form-group" v-if="isValid('CanViewProduct')">
                            <label>{{ $t('Product.SearchbyProduct') }}</label>
                            <productMasterdropdown ref="productMaster" v-model="produtMasterId"></productMasterdropdown>
                        </div>
                        <div class="col-md-3 col-lg-3  col-12 form-group">
                            <label>{{ $t('Product.SearchbyCategory') }}</label>
                            <categorydropdown ref="categoryDropdown" @update:modelValue="getSubcategory(categoryId)"
                                v-model="categoryId"></categorydropdown>
                        </div>
                        <div class="col-md-3 col-lg-3  col-12 form-group">
                            <label>{{ $t('Product.SearchbySubCategory') }}</label>
                            <multiselect v-model="subCategory" @update:modelValue="getSubCategoryValue()"
                                :options="subCategoryOptions" :show-labels="false" track-by="name" label="name"
                                v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'arabicLanguage '"
                                v-bind:placeholder="$t('AddProduct.PleaseSelectSubCategory')">
                            </multiselect>
                        </div>
                        <div class="col-md-3 col-lg-3  col-12 form-group">
                            <label>{{ $t('Product.SearchbyColor') }}</label>
                            <colordropdown ref="colorDropdown" v-model="colorId" />
                        </div>
                        <div class="col-md-3 col-lg-3  col-12 form-group">
                            <label>{{ $t('Product.SearchbySize') }}</label>
                            <sizedropdown ref="sizeDropdown" v-model="sizeId" />
                        </div>
                        <div class="col-md-3 col-lg-3  col-12 form-group">
                            <label>{{ $t('Product.Active/In-Active') }}</label>
                            <multiselect v-model="isActiveValue" :options="['Active', 'In-Active']" :show-labels="false"
                                placeholder="Select ">
                            </multiselect>
                        </div>
                        <div class="col-md-3 col-lg-3  col-12 form-group mt-3">
                            <div class="button-items">
                                <button class="btn btn-outline-primary" v-on:click="FilterRecord"><i
                                        class="far fa-save "></i>{{ $t('Product.Filter') }}</button>
                                <button class="btn btn-danger" v-on:click="ClearFilterRecord">{{ $t('Product.ClearFilter')
                                }}</button>
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
                                        {{ $t('PriceRecord.PriceType') }}
                                    </th>
                                    <th width="10%" class="text-center">
                                        {{ $t('PriceRecord.Price') }}
                                    </th>

                                    <th width="15%">
                                        {{ $t('PriceRecord.Status') }}
                                    </th>

                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="priceRecord in productList " v-bind:key="priceRecord.id">
                                    <td class="p-3">

                                        {{ (priceRecord.displayName == '' || priceRecord.displayName == null) ?
                                            priceRecord.englishName : priceRecord.displayName }}

                                    </td>

                                    <td class="text-center">
                                        {{ priceRecord.purchasePrice }}

                                    </td>
                                    <td class="text-center">
                                        {{ priceRecord.costPrice }}

                                    </td>
                                    <td class="text-center">
                                        {{ priceRecord.salePrice }}

                                    </td>
                                    <td class="text-center">
                                        {{ priceRecord.priceLabelName }}<br>

                                    </td>
                                    <td class="text-center">
                                        <decimal-to-fixed v-model="priceRecord.newPrice" />

                                    </td>

                                    <td>
                                        <input type="checkbox" id="inlineCheckbox1" v-model="priceRecord.isActive" />

                                    </td>

                                </tr>

                            </tbody>

                            <tfoot>
                                <tr>
                                    <td class="text-end" colspan="7">
                                        <button type="button" class="btn btn-outline-primary btn-round "
                                            v-on:click="SavePriceRecordChange(true)">{{ $t('AddPriceRecord.Update')
                                            }}</button>
                                    </td>

                                </tr>

                            </tfoot>
                        </table>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-lg-6">
                            <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                            <span v-else-if="currentPage === 1 && rowCount < 20"> {{ $t('Pagination.Showing') }}
                                {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                            <span v-else-if="currentPage === 1 && rowCount >= 21"> {{ $t('Pagination.Showing') }}
                                {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 20 }} {{ $t('Pagination.of') }}
                                {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                            <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                $t('Pagination.to') }} {{ currentPage * 20 }} of {{ rowCount }} {{ $t('Pagination.entries')
    }}</span>
                            <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{ $t('Pagination.Showing') }}
                                {{ (currentPage * 20) - 19 }} {{ $t('Pagination.to') }} {{ currentPage * 20 }} {{ $t('Pagination.of')
                                }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                            <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }}
                                {{ (currentPage * 20) - 19 }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        </div>
                        <div class=" col-lg-6">
                            <div class=" float-end" v-on:click="GetProductData()">
                                <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                    :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                    :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                                </b-pagination>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
        <div class="col-lg-12" v-else-if="active == 'PriceRecordChange'">
            <div class="row">

                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="row">
                                    <div class="col">
                                        <h4 class="page-title">{{ $t('PriceRecordChange.PriceRecordChange') }}</h4>
                                        <ol class="breadcrumb">
                                            <li class="breadcrumb-item"><a href="javascript:void(0);">{{
                                                $t('PriceRecord.Home') }}</a></li>
                                            <li class="breadcrumb-item active">{{ $t('PriceRecordChange.PriceRecordChange')
                                            }}</li>
                                        </ol>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <div class="col-md-4">
                                <priceLabelingDropdown v-model="priceLebelId"
                                    @update:modelValue="GetProductDataPriceLabeling(priceLebelId)" />
                            </div>
                        </div>
                        <div class="card-body px-4" v-if="show">
                            <div class="table-responsive pb-5">
                                <table class="table mb-3">
                                    <thead class="thead-light table-hover ">
                                        <tr>

                                            <th class="p-3">
                                                {{ $t('Code') }}
                                            </th>
                                            <th class="p-3">
                                                {{ $t('PriceRecord.ProductName') }}
                                            </th>
                                            <th>
                                                {{ $t('Sale Price') }}
                                            </th>
                                            <th>
                                                {{ $t('Purchase Price') }}
                                            </th>
                                            <th>
                                                {{ $t('Cost Price') }}
                                            </th>
                                            <th>
                                                {{ $t('Old Label Price') }}
                                            </th>
                                            <th>
                                                {{ $t('New Label Price') }}
                                            </th>

                                            <th>
                                                {{ $t('PriceRecord.Status') }}
                                            </th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="priceRecord in productList " v-bind:key="priceRecord.id">
                                            <td>{{ priceRecord.code }}</td>
                                            <td class="p-3">
                                                <strong>
                                                    <a href="javascript:void(0)"> {{ (priceRecord.displayName == '' ||
                                                        priceRecord.displayName == null) ? priceRecord.englishName :
                                                        priceRecord.displayName }}</a>
                                                </strong>
                                            </td>
                                            <td>
                                                {{ priceRecord.salePrice }}
                                            </td>
                                            <td>
                                                {{ priceRecord.purchasePrice }}
                                            </td>
                                            <td>
                                                {{ priceRecord.costPrice }}

                                            </td>
                                            <td>
                                                {{ priceRecord.price }}
                                            </td>
                                            <td>
                                                <decimal-to-fixed :textAlignLeft="true" v-model="priceRecord.newPrice" />
                                            </td>
                                            <td>
                                                <input type="checkbox" id="inlineCheckbox1"
                                                    v-model="priceRecord.isActive" />
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td class="text-end" colspan="7">
                                                <button type="button" class="btn btn-outline-primary btn-round "
                                                    v-on:click="SavePriceRecordChange(false)">{{ $t('AddPriceRecord.Update')
                                                    }}</button>
                                            </td>

                                        </tr>

                                    </tfoot>

                                </table>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-6">
                                    <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries')
                                    }}</span>
                                    <span v-else-if="currentPage === 1 && rowCount < 20"> {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                        {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage === 1 && rowCount >= 21"> {{ $t('Pagination.Showing') }}
                                        {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 20 }} {{ $t('Pagination.of')
                                        }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                        $t('Pagination.to') }} {{ currentPage * 20 }} of {{ rowCount }} {{
        $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{
                                        $t('Pagination.Showing') }} {{ (currentPage * 20) - 19 }} {{ $t('Pagination.to') }}
                                        {{ currentPage * 20 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                            $t('Pagination.entries') }}</span>
                                    <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }}
                                        {{ (currentPage * 20) - 19 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                            $t('Pagination.of') }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                                </div>
                                <div class=" col-lg-6">
                                    <div class=" float-end" v-on:click="GetProductData()">
                                        <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                            :per-page="10" :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                            :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                                        </b-pagination>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from 'vue-multiselect'
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';
export default {
    components: {
        Multiselect,
        Loading
    },
    mixins: [clickMixin],
    data: function () {
        return {
            active: 'PriceType',
            priceLabelinglist: [],
            newPriceLabeling: {
                id: '',
                name: '',
                nameArabic: '',
                description: '',
                code: '',
                price: 0,
                isActive: true
            },
            pageCount: '',
            produtMasterId: '',
            priceLebelId: '',
            colorId: '',
            sizeId: '',
            originId: '',
            categoryId: '',
            subCategoryId: '',
            subCategory: '',

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
    watch: {
        // search: function (val) {
        //     this.GetPriceLabelingData(val, 1);
        // }
    },
    methods: {

        search22: function () {
            this.GetPriceLabelingData(this.search, this.currentPage, this.active);
        },

        clearData: function () {
            this.search = "";
            this.GetPriceLabelingData(this.search, this.currentPage, this.active);

        },
        GetProductDataPriceLabeling: function (priceLebelId) {
            var root = this;
            var url = '/Product/GetPriceRecordChange?priceLebelId=' + priceLebelId.id;
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

                    root.productList = response.data;
                    root.productList.forEach((product) => {
                        product.price = product.newPrice == 0 ? product.price : product.newPrice;
                        product.newPrice = 0.00;
                    });
                    root.loading = false;
                    root.show = true
                }
                root.loading = false;
            });
        },
        SavePriceRecordChange: function (value) {
            console.log(value);

            var root = this;
            var token = '';
            this.loading = true;
            if (token == '') {
                token = localStorage.getItem('token');
            }

            var product = this.productList.filter(x => x.isActive);

            this.$https.post('/Product/SavePriceRecordChange', product, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data.IsAddUpdate != '') {
                        root.$swal({
                            title: "Add",
                            text: response.data.IsAddUpdate,
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        root.close();
                    } else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: response.data.IsAddUpdate,
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                }).catch(error => {
                    console.log(error)
                    root.$swal.fire({
                        icon: 'error',
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                        text: error.response.data,

                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,
                    });

                    root.loading = false
                })
                .finally(() => root.loading = false)
        },
        GotoPage: function (link) {
            this.$router.push({
                path: link
            });
        },
        makeActive: function (link) {
            if (link == 'PriceRecord') {
                this.productList = [];
                this.GetProductData(this.search, 1);
            } else if (link == 'PriceType') {
                this.GetPriceLabelingData(this.search, 1);
            } else if (link == 'PriceRecordChange') {
                this.productList = [];
                this.GetPriceLabelingData(this.search, 1);
            }
            this.active = link;
        },

        IsSavePriceLabeling: function () {

            this.show = false;

            this.GetPriceLabelingData(this.search, this.currentPage);
        },
        // getPage: function () {
        //     this.GetPriceLabelingData(this.search, this.currentPage);
        // },
        openmodel: function () {
            this.newPriceLabeling = {
                id: '00000000-0000-0000-0000-000000000000',
                name: '',
                price: 0,
                nameArabic: '',
                description: '',
                isActive: true

            }
            this.show = !this.show;
            this.type = "Add";
        },
        GetPriceLabelingData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('Product/PriceLabelingList?isActive=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {
                if (response.data != null) {
                    root.priceLabelinglist = response.data.results.priceLabelings;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        EditPriceLabeling: function (id) {
            
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/PriceLabelingDetail?Id=' + id, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data) {
                        root.newPriceLabeling.id = response.data.id;
                        root.newPriceLabeling.name = response.data.name;
                        root.newPriceLabeling.price = response.data.price;
                        root.newPriceLabeling.nameArabic = response.data.nameArabic;
                        root.newPriceLabeling.description = response.data.description;
                        root.newPriceLabeling.code = response.data.code;
                        root.newPriceLabeling.isActive = response.data.isActive;
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
        getSubcategory: function (categoryId) {
            //this.catId = event;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            this.subCategoryOptions = [];
            this.$https.get('/Product/GetSubCategoryInformation?isActive=true' + '&categoryId=' + categoryId, {
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
        getSubCategoryValue: function () {
            this.subCategoryId = this.subCategory.id;

        },

        IsSave: function () {

            this.show = false;

            this.GetProductData(this.search, this.currentPage, this.warehouseId);
        },
        FilterRecord: function () {

            this.GetProductData(this.search, this.currentPage, this.warehouseId);
        },
        ClearFilterRecord: function () {
            var root = this;

            if (root.$refs.colorDropdown != undefined) {
                this.$refs.colorDropdown.EmptyRecord();

            }
            if (root.$refs.productMaster != undefined) {
                this.$refs.productMaster.EmptyRecord();

            }
            if (root.$refs.categoryDropdown != undefined) {
                this.$refs.categoryDropdown.EmptyRecord();

            }
            if (root.$refs.sizeDropdown != undefined) {
                this.$refs.sizeDropdown.EmptyRecord();

            }
            this.produtMasterId = '';
            this.sizeId = '';
            this.colorId = '';
            this.originId = '';
            this.categoryId = '';
            this.subCategory = '';
            this.subCategoryId = '';
            this.isActiveValue = '';
            this.searchTerm = '';


            this.GetProductData(this.search, this.currentPage, this.warehouseId);
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

        // getPage: function () {

        //     this.GetProductData(this.search, this.currentPage, this.warehouseId);
        // },
        GetProductData: function () {

            var root = this;

            var url = '/Product/PriceRecordList?searchTerm=' + this.search + '&sizeId=' + this.sizeId + '&isActiveValue=' + this.isActiveValue + '&subCategoryId=' + this.subCategoryId + '&subCategoryId=' + this.subCategoryId + '&pageNumber=' + this.currentPage + '&colorId=' + this.colorId + '&originId=' + this.originId + '&categoryId=' + this.categoryId + '&produtMasterId=' + this.produtMasterId + '&pageSize=20';
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
        this.$emit('update:modelValue', this.$route.name);
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.GetPriceLabelingData(this.search, 1);

    }
}
</script>
