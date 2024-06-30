<template>
    <div class="row" v-if="isValid('CanViewPromotionOffer')">
        <div class="col-lg-12 col-sm-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Promotion.Promotions') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Promotion.Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('Promotion.Promotions') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddPromotionOffer')" v-bind:disabled="activePromotion >= 3"
                                    v-on:click="AddPromotion" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Promotion.AddNew') }}
                                </a>
                                <a class="btn btn-sm btn-outline-primary mx-1" v-on:click="SelectBranchesModel()"
                                    v-if="allowBranches">
                                    Sync Branches
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
                        <div class="col-md-4" v-if="allowBranches">
                            <branch-dropdown v-model="brancheslist" :key="render" />
                        </div>
                        <div class="col-md-4" v-if="allowBranches">
                            <button type="button" class="btn btn-outline-primary me-2 "
                                v-on:click="getData(search, 1, active)"> Save</button>
                            <button type="button" class="btn btn-danger" v-on:click="ClearFilter()">{{
                                $t('AddBranches.Cancel') }}</button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-8" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-secondary" type="button" id="button-addon1"><i
                                        class="fas fa-search"></i></button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('Promotion.Search')" aria-label="Example text with button addon"
                                    aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

                            <button v-on:click="search22(true)" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div>
                                <ul class="nav nav-tabs" data-tabs="tabs">
                                    <li class="nav-item"><a class="nav-link" v-bind:class="{ active: active == 'Active' }"
                                            v-on:click="makeActive('Active')" id="v-pills-home-tab" data-toggle="pill"
                                            href="#v-pills-home" role="tab" aria-controls="v-pills-home"
                                            aria-selected="true"> {{ $t('Promotion.Active') }}</a></li>
                                    <li class="nav-item"><a class="nav-link" v-bind:class="{ active: active == 'History' }"
                                            v-on:click="makeActive('History')" id="v-pills-profile-tab" data-toggle="pill"
                                            href="#v-pills-profile" role="tab" aria-controls="v-pills-profile"
                                            aria-selected="false">{{ $t('Promotion.History') }}</a></li>
                                </ul>
                            </div>
                            <div class="tab-content mt-3" id="nav-tabContent">
                                <div v-if="active == 'Active'">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table-responsive">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                        <tr>
                                                            <th>
                                                                <span class="me-1" v-if="isSelectAll">
                                                                    <input type="checkbox" v-model="branches.isSelectAll"
                                                                        id="ProductSelectAll"
                                                                        v-on:change="SelectAllProducts(false)" />
                                                                </span>
                                                                <span class="me-1" v-if="!isSelectAll">
                                                                    <input type="checkbox" v-model="branches.isSelectAll"
                                                                        id="ProductSelectAlla"
                                                                        v-on:change="SelectAllProducts(true)" />
                                                                </span>
                                                                All
                                                            </th>
                                                            <th>
                                                                #
                                                            </th>
                                                            <th>
                                                                {{ $t('Promotion.OFFER_NAME') }}
                                                            </th>
                                                            <th>
                                                                {{ $t('Promotion.ProductName') }}
                                                            </th>
                                                            <th>
                                                                Discount
                                                            </th>
                                                            <th>
                                                                Discount Type
                                                            </th>
                                                            <th>
                                                                {{ $t('Promotion.Quantity') }}
                                                            </th>
                                                            <!--<th>
                                                                Quantity Out
                                                            </th>-->

                                                            <th>
                                                                {{ $t('Promotion.FromDate') }}
                                                            </th>
                                                            <th>
                                                                {{ $t('Promotion.ToDate') }}
                                                            </th>
                                                            <th>
                                                                {{ $t('Promotion.Status') }}
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr v-for="(details, index) in promotionList"
                                                            v-bind:key="details.id">
                                                            <td v-if="isSelectAll" :key="selectAllRender">
                                                                <input type="checkbox" :id="1.01 + index"
                                                                    :checked="isSelectAll" :key="selectAllRender" />
                                                            </td>
                                                            <td v-else :key="selectAllRender1">
                                                                <input type="checkbox" :id="1.02 + index"
                                                                    v-on:change="SelectProduct(details.id)" />
                                                            </td>
                                                            <td>
                                                                {{ index + 1 }}
                                                            </td>
                                                            <td v-if="isValid('CanEditPromotionOffer')">
                                                                <strong>
                                                                    <a href="javascript:void(0)"
                                                                        v-on:click="EditPromotionInfo(details.id)">{{ details.offer }}</a>
                                                                </strong>
                                                            </td>
                                                            <td v-else>
                                                                {{ details.offer }}
                                                            </td>
                                                            <td>{{ details.productName }}</td>
                                                            <td>{{ details.discount }}</td>
                                                            <td>{{ details.discountType }}</td>
                                                            <td>{{ details.stockLimit }}</td>
                                                            <!--<td>{{details.quantityOut}}</td>-->

                                                            <td>{{ details.fromDate }}</td>
                                                            <td>{{ details.toDate }}</td>
                                                            <td>
                                                                <span v-if="details.isActive"
                                                                    class="badge badge-boxed  badge-outline-success">{{ $t('color.Active') }}</span>
                                                                <span v-else
                                                                    class="badge badge-boxed  badge-outline-danger">{{ $t('color.De-Active') }}</span>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <span v-if="currentPage === 1 && rowCount === 0">
                                                {{ $t('Pagination.ShowingEntries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1 && rowCount < 10">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }} {{ $t('Pagination.to') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1 && rowCount >= 11">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }} of {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                                {{ $t('Pagination.Showing') }}
                                                {{ (currentPage * 10) - 9 }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === pageCount">
                                                {{ $t('Pagination.Showing') }}
                                                {{ (currentPage * 10) - 9 }}
                                                {{ $t('Pagination.to') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                        </div>
                                        <div class=" col-lg-6">
                                            <div class="float-end" v-on:click="getPage()">
                                                <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                    :per-page="10" :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')">
                                                </b-pagination>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="active == 'History'">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table-responsive">
                                                <table class="table mb-0">
                                                    <thead class="thead-light table-hover">
                                                        <tr>
                                                            <th>
                                                                {{ $t('Promotion.ID') }}
                                                            </th>
                                                            <th>
                                                                {{ $t('Promotion.OFFER_NAME') }}
                                                            </th>
                                                            <th>
                                                                {{ $t('Promotion.ProductName') }}
                                                            </th>
                                                            <th>
                                                                Discount
                                                            </th>
                                                            <th>
                                                                Discount Type
                                                            </th>
                                                            <th>
                                                                {{ $t('Promotion.Quantity') }}
                                                            </th>
                                                            <!--<th>
                                                                Quantity Out
                                                            </th>-->

                                                            <th>
                                                                {{ $t('Promotion.FromDate') }}
                                                            </th>
                                                            <th>
                                                                {{ $t('Promotion.ToDate') }}
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr v-for="(details, index) in promotionList"
                                                            v-bind:key="details.id">
                                                            <td>
                                                                {{ index + 1 }}
                                                            </td>
                                                            <td>
                                                                {{ details.offer }}
                                                            </td>
                                                            <td>{{ details.productName }}</td>
                                                            <td>{{ details.discount }}</td>
                                                            <td>{{ details.discountType }}</td>
                                                            <td>{{ details.stockLimit }}</td>
                                                            <!--<td>{{details.quantityOut}}</td>-->
                                                            <td>{{ details.fromDate }}</td>
                                                            <td>{{ details.toDate }}</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <span v-if="currentPage === 1 && rowCount === 0">
                                                {{ $t('Pagination.ShowingEntries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1 && rowCount < 10">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }} {{ $t('Pagination.to') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1 && rowCount >= 11">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === 1">
                                                {{ $t('Pagination.Showing') }}
                                                {{ currentPage }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }} of {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                                {{ $t('Pagination.Showing') }}
                                                {{ (currentPage * 10) - 9 }}
                                                {{ $t('Pagination.to') }}
                                                {{ currentPage * 10 }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                            <span v-else-if="currentPage === pageCount">
                                                {{ $t('Pagination.Showing') }}
                                                {{ (currentPage * 10) - 9 }}
                                                {{ $t('Pagination.to') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.of') }}
                                                {{ rowCount }}
                                                {{ $t('Pagination.entries') }}
                                            </span>
                                        </div>
                                        <div class=" col-lg-6">
                                            <div class="float-end" v-on:click="getPage()">
                                                <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                    :per-page="10" :first-text="$t('Table.First')"
                                                    :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                    :last-text="$t('Table.Last')">
                                                </b-pagination>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <select-branches-model 
                           :show="showSelectBranches"
                           v-if="showSelectBranches"
                           @save="SaveBranchesModel"
                           @close="CloseModel()"/>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>
        </div>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin';
    
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        name: 'Promotion',
        components: {
            Loading
        },
        mixins: [clickMixin],
        data: function () {
            return {
            allowBranches: false,
            brancheslist: '',
            render: 0,
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
            loading: false,
            show: false,
            promotionList: [],
            active: 'Active',
            search: '',
            searchQuery: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            activePromotion: 0,
            branchId: '',
        }
    },
    watch: {
        // search: function (val) {
        //     this.getData(val, 1, this.active);
        // }
    },
    methods: {
        ClearFilter: function () {
            this.search = '';
            this.brancheslist = '';
            this.render++;
            this.getData(this.search, this.currentPage, this.active);
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
        search22: function () {
            this.getData(this.search, this.currentPage, this.active);
        },

        clearData: function () {
            this.search = "";
            this.getData(this.search, this.currentPage, this.active);

            },
            SaveProductsAganistBranches: function()
            {
                
                var root = this;
                var url = '/Branches/SavePromotionBranches' ;
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
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

            AddPromotion: function () {
             //   this.$router.push('/AddPromotion')
             this.$router.push({
                            path: '/AddPromotion',
                            query: { 
                                Add: true, }
                        })

            },
            getPage: function () {
                this.getData(this.search, this.currentPage, this.active);
            },
            makeActive: function (item) {
                this.active = item;
                this.getData(this.search, 1, item);
            },
            getData: function (search, currentPage, status) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                var branch = localStorage.getItem('BranchId');
                

            this.$https.get('/Product/GetPromotion?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&branchId=' + branch + '&bundleBranches=' + this.brancheslist, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    if (response.data != null) {
                        root.promotionList = response.data.results;
                        root.activePromotion = response.data.activeBundle;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                    }
                });
        },

            EditPromotionInfo: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Product/PromotionDetailsViaId?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddPromotion',
                            query: { data: '',
                                Add: false, }
                        })
                    }
                });

        }
    },
    created: function () {

            this.$emit('update:modelValue', this.$route.name);
            this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;

    },
    mounted: function () {
        this.makeActive("Active");
    }
}
</script>