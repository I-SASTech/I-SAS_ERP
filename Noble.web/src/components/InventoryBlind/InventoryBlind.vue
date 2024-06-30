<template>
    <div class="row" v-if="isValid('CanViewInventoryCount') || isValid('CanEditInventoryCount')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('InventoryBlind.InventoryCountList') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('InventoryBlind.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('InventoryBlind.InventoryCountList') }}
                                    </li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddInventoryCount')" v-on:click="AddInventoryBlind"
                                    href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('InventoryBlind.AddNew') }}
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
                            :placeholder="$t('InventoryBlind.SearchByCodeorWarehouseName')" aria-label="Example text with button addon"
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
                    <div>
                        <ul class="nav nav-tabs" data-tabs="tabs">
                            <li class="nav-item"><a class="nav-link" v-bind:class="{ active: active == 'UnCounted' }"
                                    v-if="isValid('CanViewInventoryCount')" v-on:click="makeActive('UnCounted')"
                                    id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab"
                                    aria-controls="v-pills-home" aria-selected="true">{{
                                            $t('InventoryBlind.CountingList')
                                    }}</a></li>
                            <li class="nav-item"><a class="nav-link" v-bind:class="{ active: active == 'Counted' }"
                                    v-if="isValid('CanEditInventoryCount')" v-on:click="makeActive('Counted')"
                                    id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab"
                                    aria-controls="v-pills-profile" aria-selected="false">{{
                                            $t('InventoryBlind.UpdatedCount')
                                    }}</a></li>
                        </ul>
                    </div>
                    <div class="tab-content mt-3" id="nav-tabContent">
                        <div v-if="active == 'UnCounted'">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>#</th>
                                                    <th style="width:10%" width="10%">
                                                        {{ $t('InventoryBlind.Code') }}
                                                    </th>
                                                    <th style="width:25%" v-if="english === 'true'" width="25%">
                                                        {{$englishLanguage($t('InventoryBlind.WareHouse'))   }}
                                                    </th>
                                                    <th style="width:25%" v-if="arabic === 'true'" width="25%">
                                                        {{$arabicLanguage($t('InventoryBlind.WareHouse'))   }}
                                                    </th>

                                                    <th style="width:20%" width="40%">
                                                        {{ $t('InventoryBlind.Date') }}
                                                    </th>
                                                    <th style="width:1%"></th>
                                                    <th style="width:1%"></th>
                                                    <th style="width:1%"></th>
                                                    <th style="width:1%"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(details, index) in inventoryBlindlist"
                                                    v-bind:key="details.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>
                                                    <td v-if="isValid('CanEditInventoryCount')">
                                                        <strong>
                                                            <a href="javascript:void(0)"
                                                                v-on:click="EditInventoryBlindInfo(details.id)">
                                                                {{ details.documentId }}
                                                            </a>
                                                        </strong>
                                                    </td>
                                                    <td v-else>
                                                        <strong>
                                                            {{ details.documentId }}
                                                        </strong>
                                                    </td>
                                                    <td v-if="english == 'true'">
                                                        {{ details.warehouseName }}</td>
                                                    <td >
                                                        {{ details.warehouseNameArabic }}</td>
                                                    <td>
                                                        {{ details.dateTime }}</td>
                                                    <td>
                                                        <button title="View" class="btn btn-icon btn-sm  btn-outline-primary"
                                                            v-on:click="ViewInventoryBlindInfo(details.id)"
                                                            v-if="isValid('CanViewDetailInventoryCount')">
                                                            <i class="fas fa-eye"></i>
                                                        </button>
                                                    </td>
                                                    <td>
                                                        <a href="javascript:void(0)" title="Blind"
                                                            class="btn btn-icon btn-outline-primary btn-sm mr-1 ml-1"
                                                            v-on:click="PrintBlindInventory(details.id, false, false)"
                                                            v-if="isValid('CanPrintInventoryCount')">
                                                            <i class=" fa fa-print"></i>
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <a href="javascript:void(0)" title="Counting"
                                                            class="btn btn-icon btn-outline-primary btn-sm mr-1 "
                                                            v-on:click="PrintBlindInventory(details.id, true, false)"
                                                            v-if="isValid('CanPrintInventoryCount')">
                                                            <i class=" fa fa-print"></i>
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <a href="javascript:void(0)" title="Counting Xlxs"
                                                            class="btn btn-icon btn-outline-primary btn-sm"
                                                            v-on:click="DownloadXlxsFile(details.warehouseId)"
                                                            v-if="isValid('CanPrintInventoryCount')">
                                                            <i class="fa fa-table"></i>
                                                        </a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
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
                            </div>
                        </div>
                        <div v-if="active == 'Counted'">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>#</th>
                                                    <th style="width:10%" width="10%">
                                                        {{ $t('InventoryBlind.Code') }}
                                                    </th>
                                                    <th style="width:30%" v-if="english === 'true'" width="20%">
                                                        {{$englishLanguage($t('InventoryBlind.WareHouse'))   }}
                                                    </th>
                                                    <th style="width:30%" v-if="arabic === 'true'" width="20%">
                                                        {{$arabicLanguage($t('InventoryBlind.WareHouse'))   }}
                                                    </th>

                                                    <th style="width:20%" width="40%">
                                                        {{ $t('InventoryBlind.Date') }}
                                                    </th>
                                                    <th style="width:1%"></th>
                                                    <th style="width:1%"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(details, index) in inventoryBlindlist"
                                                    v-bind:key="details.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>
                                                    <td style="width:10%">
                                                        <strong>
                                                            {{ details.documentId }}
                                                        </strong>
                                                    </td>

                                                    <td style="width:30%" v-if="english == 'true'">
                                                        {{ details.warehouseName }}</td>
                                                    <td style="width:30%" >
                                                        {{ details.warehouseNameArabic }}</td>
                                                    <td style="width:20%">
                                                        {{ details.dateTime }}</td>

                                                    <td >
                                                        <button class="btn btn-icon btn-sm  btn-outline-primary mr-1 ml-1"
                                                            v-on:click="ViewInventoryBlindInfo(details.id)"
                                                            v-if="isValid('CanViewDetailInventoryCount')">
                                                            <i class="fas fa-eye"></i>
                                                        </button>
                                                    </td>
                                                    <td>
                                                        <a href="javascript:void(0)" title="Updated"
                                                            class="btn  btn-icon btn-outline-primary btn-sm"
                                                            v-on:click="PrintBlindInventory(details.id, true, true)"
                                                            v-if="isValid('CanPrintInventoryCount')">
                                                            <i class=" fa fa-print"></i>
                                                        </a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <blindPrint :printDetails="productList" :headerFooter="headerFooter" v-bind:key="printRender"></blindPrint>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from 'moment'
export default {
    mixins: [clickMixin],
    name: 'InventoryBlind',
    data: function () {
        return {
            active: 'UnCounted',
            arabic: '',
            english: '',
            searchQuery: '',
            show: false,
            type: '',
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            inventoryBlindlist: [

            ],
            isCounted: false,
            productList: [],
            headerFooter: {
                company: '',
                date: '',
                documentId: '',
                wareHouseName: '',
                isQuantity: false,
                isQuantityRemarks: false

            },
            printRender: 0

        }
    },
    computed: {
        resultQuery: function () {
            var root = this;
            if (this.searchQuery) {
                return this.productList.filter((c) => {
                    return root.searchQuery.toLowerCase().split(' ').every(v => c.name.toLowerCase().includes(v))
                })
            } else {
                return root.productList;
            }
        },
    },
    watch: {
        // search: function (val) {

        //     this.GetInventoryBlindData(val, 1);
        // }
    },

    methods: {
        search22: function () {
            this.GetInventoryBlindData(this.search, this.currentPage);
            },

            clearData: function () {
                this.search="";
                this.GetInventoryBlindData(this.search, this.currentPage);

            },




        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        DownloadXlxsFile: function (warehouseId) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.loading = true;
            var path = '/Template/Inventory Count Template.xlsx'
            var ext = path.split('.')[1];
            root.$https.get('/Product/DownloadInventoryCountAsync?filePath=' + path + '&warehouseId=' + warehouseId, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                .then(function (response) {

                    const url = window.URL.createObjectURL(new Blob([response.data]));
                    const link = document.createElement('a');
                    link.href = url;
                    link.setAttribute('download', 'Inventory Count Template.' + ext);
                    document.body.appendChild(link);
                    link.click();
                    root.loading = false;
                });
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
                    }
                });
        },
        PrintBlindInventory: function (id, isQuantity, isQuantityRemarks) {

            var root = this;
            var token = '';
            this.inventoryBlindlist.forEach(function (x) {
                if (x.id == id) {

                    root.headerFooter.documentId = x.documentId;
                    root.headerFooter.wareHouseName = x.warehouseName
                }
            });
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/GetBlindInventoryDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.headerFooter.date = moment(response.data.dateTime).format('DD/MM/YYYY');
                    root.headerFooter.isQuantity = isQuantity;
                    root.headerFooter.isQuantityRemarks = isQuantityRemarks;
                    root.productList = response.data.inventoryBlindDetailModels;
                    root.printRender++;
                }
            });
        },
        IsSave: function () {

            this.GetInventoryBlindData(this.search, this.currentPage);
        },
        makeActive: function (activeTab) {
            if (activeTab == "UnCounted")
                this.isCounted = false
            else
                this.isCounted = true
            this.active = activeTab
            this.GetInventoryBlindData()
        },
        getPage: function () {
            this.GetInventoryBlindData(this.search, this.currentPage);
        },
        AddInventoryBlind: function () {
            // this.$router.push('/InventoryBlind');
             this.$router.push({
                path: '/InventoryBlind',
                query: {
                    Add: true,
                    isDeliveryChallan: true,

                }
            })
        },

        GetInventoryBlindData: function () {

            var root = this;
            var url = '/Product/GetBlindInventoryList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search + '&isCounted=' + this.isCounted;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.inventoryBlindlist = response.data.results.inventoryBlindList;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                    console.log(root.inventoryBlindlist)
                }
                root.loading = false;
            });

        },
        EditInventoryBlindInfo: function (id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/GetBlindInventoryDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.$store.GetEditObj(response.data);
                    root.$router.push({
                        path: '/InventoryBlind',
                        query: {
                              data: '',
                                Add: false,
                            isEdit: true,

                        }

                    });

                }
            });
        },
        ViewInventoryBlindInfo: function (id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/GetBlindInventoryDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.$router.push({
                        path: '/InventoryBlind',
                        query: {
                            data: response.data,
                            isEdit: true,
                            isDisabled: true
                        }
                    })
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
        this.GetInventoryBlindData(this.search, 1);
        this.GetHeaderDetail();
    }
}
</script>