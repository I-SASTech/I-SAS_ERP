<template>
    <div class="row">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('BOM') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('brand.Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('BOM') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="addNew" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
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
                                <input v-model="search" type="text" class="form-control" :placeholder="$t('brand.Search')"
                                    aria-label="Example text with button addon" aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1">

                            <button v-on:click="search22(true)" :disabled="!search" type="button"
                                class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled="!search" type="button"
                                class="btn btn-outline-primary mx-2 mt-3">
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
                                    <li class="nav-item"><a class="nav-link" v-bind:class="{ active: active == 'Draft' }"
                                            v-on:click="makeActive('Draft')" id="v-pills-home-tab" data-toggle="pill"
                                            href="#v-pills-home" role="tab" aria-controls="v-pills-home"
                                            aria-selected="true">{{ $t('PurchaseOrder.Draft') }}</a></li>
                                    <li class="nav-item"><a class="nav-link" v-bind:class="{ active: active == 'Post' }"
                                            v-on:click="makeActive('Post')" id="v-pills-profile-tab" data-toggle="pill"
                                            href="#v-pills-profile" role="tab" aria-controls="v-pills-profile"
                                            aria-selected="false">{{ $t('PurchaseOrder.Post') }}</a></li>

                                </ul>
                            </div>
                            <div class="tab-content mt-3" id="nav-tabContent">
                                <div v-if="active == 'Draft'">
                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>#</th>
                                                    <th>
                                                        {{ $t('brand.Code') }}
                                                    </th>
                                                    <th>
                                                        Date
                                                    </th>
                                                    <th>
                                                        Sale Order
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(brand, index) in bomList" v-bind:key="brand.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>

                                                    <td>
                                                        <strong>
                                                            <a href="javascript:void(0)"
                                                                v-on:click="EditBom(brand.id, 'Edit')"> {{ brand.code }}</a>
                                                        </strong>
                                                    </td>
                                                    <td>
                                                        {{ getDate(brand.date) }}
                                                    </td>
                                                    <td>
                                                        {{ brand.saleOrder }}
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <hr />
                                    <div class="float-start">
                                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries')
                                        }}</span>
                                        <span v-else-if="currentPage === 1 && rowCount < 10">
                                            {{ $t('Pagination.Showing') }}
                                            {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                                $t('Pagination.of') }}
                                            {{ rowCount }} {{ $t('Pagination.entries') }}
                                        </span>
                                        <span v-else-if="currentPage === 1 && rowCount >= 11">
                                            {{ $t('Pagination.Showing') }}
                                            {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                                $t('Pagination.of') }}
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
                                            {{ (currentPage * 10) - 9 }}
                                            {{ $t('Pagination.to') }}
                                            {{ rowCount }}
                                            {{ $t('Pagination.of') }}
                                            {{ rowCount }} {{ $t('Pagination.entries') }}
                                        </span>
                                    </div>
                                    <div class="float-end">
                                        <div class="" v-on:click="GetBrandData('Draft')">
                                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                :per-page="10" :first-text="$t('Table.First')"
                                                :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                :last-text="$t('Table.Last')"></b-pagination>
                                        </div>
                                    </div>


                                </div>
                                <div v-if="active == 'Post'">
                                    <div class="table-responsive">
                                        <table class="table mb-0">
                                            <thead class="thead-light table-hover">
                                                <tr>
                                                    <th>#</th>
                                                    <th>
                                                        {{ $t('brand.Code') }}
                                                    </th>
                                                    <th>
                                                        Date
                                                    </th>
                                                    <th>
                                                        Sale Order
                                                    </th>
                                                    <th>

                                                    </th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(brand, index) in bomList" v-bind:key="brand.id">
                                                    <td v-if="currentPage === 1">
                                                        {{ index + 1 }}
                                                    </td>
                                                    <td v-else>
                                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                                    </td>

                                                    <td>
                                                        {{ brand.code }}
                                                    </td>
                                                    <td>
                                                        {{ getDate(brand.date) }}
                                                    </td>
                                                    <td>
                                                        {{ brand.saleOrder }}
                                                    </td>
                                                    <td class="text-end">
                                                        <button type="button" class="btn btn-light dropdown-toggle"
                                                            data-bs-toggle="dropdown" aria-expanded="false">{{
                                                                $t('SaleOrder.Action') }} <i
                                                                class="mdi mdi-chevron-down"></i></button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item" href="javascript:void(0)"
                                                                v-on:click="EditBom(brand.id, 'Details')">Details</a>
                                                            <a class="dropdown-item" href="javascript:void(0)" v-if="brand.status != 'Converted'"
                                                                v-on:click="ConvertToGoodsReceive(brand.id, brand.saleOrderId)">Convert
                                                                To Goods Receive</a>
                                                                <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(brand.id, false)">{{
                                                            $t('SaleOrder.A4Print') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)"
                                                        v-on:click="PrintRdlc(brand.id, true)">{{
                                                            $t('SaleOrder.PdfDownload') }}</a>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <hr />
                                    <div class="float-start">
                                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries')
                                        }}</span>
                                        <span v-else-if="currentPage === 1 && rowCount < 10">
                                            {{ $t('Pagination.Showing') }}
                                            {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                                $t('Pagination.of') }}
                                            {{ rowCount }} {{ $t('Pagination.entries') }}
                                        </span>
                                        <span v-else-if="currentPage === 1 && rowCount >= 11">
                                            {{ $t('Pagination.Showing') }}
                                            {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                                $t('Pagination.of') }}
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
                                            {{ (currentPage * 10) - 9 }}
                                            {{ $t('Pagination.to') }}
                                            {{ rowCount }}
                                            {{ $t('Pagination.of') }}
                                            {{ rowCount }} {{ $t('Pagination.entries') }}
                                        </span>
                                    </div>
                                    <div class="float-end">
                                        <div class="" v-on:click="GetBrandData('Post')">
                                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount"
                                                :per-page="10" :first-text="$t('Table.First')"
                                                :prev-text="$t('Table.Previous')" :next-text="$t('Table.Next')"
                                                :last-text="$t('Table.Last')"></b-pagination>
                                        </div>
                                    </div>
                                </div>

                            </div>


                        </div>
                    </div>

                </div>
            </div>
            <invoicedetailsprint :show="show" v-if="show" :reportsrc="reportsrc" :changereport="changereport"
                @close="show = false" @IsSave="IsSave" />

        </div>

    </div>
</template>


<script>
import moment from 'moment'
import clickMixin from '@/Mixins/clickMixin'
export default {
    mixins: [clickMixin],
    data: function () {
        return {
            bomList: [],

            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            arabic: '',
            english: '',
            active:'Draft',
            show: false,
            changereport: 0,
        }
    },
    methods: {
        PrintRdlc: function (val, isDownload) {
            var root = this;
            var companyId = '';
            if (this.$store.isAuthenticated) {
                companyId = localStorage.getItem('CompanyID');
            }
            if (isDownload) {
                this.$https.get(this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?id=' + val + '&CompanyId=' + companyId + '&formName=Bom' + '&isDownload=' + isDownload
                    , { responseType: 'blob' }).then(function (response) {
                        root.loading = false;
                        
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        var date = moment().format('DD MMM YYYY');
                        link.setAttribute('download', root.formName + date + '.pdf');
                        document.body.appendChild(link);
                        link.click();

                    })
            }
            else {
                this.reportsrc = this.$ReportServer + '/Invoice/A4_DefaultTempletForm.aspx?id=' + val + '&CompanyId=' + companyId + '&formName=Bom' + '&isDownload=' + isDownload
              
                this.changereport++;
                this.show = !this.show;
            }
        },
        ConvertToGoodsReceive: function (id, saleOrderId) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/GetSaleOrderItemsForGoodsReceived?Id=' + id + '&saleOrderId=' + saleOrderId, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
                .then(function (response) {
                    if (response.data != null) {

                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddGoodReceive',
                            query: {
                                Add: false,
                                clone: 'SaleOrder',
                                isConversion: true,
                                bomId: id,
                                saleOrderId: saleOrderId
                            }
                        })
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });
        },
        getDate: function (date) {
            return moment(date).format('LLL');
        },
        addNew: function () {
            var root = this;
            root.$router.push({
                path: '/AddBom',
                query: { Add: true }
            })
        },
        search22: function () {
            this.GetBrandData(this.search, this.currentPage, this.active);
        },

        clearData: function () {
            this.search = "";
            this.GetBrandData(this.search, this.currentPage, this.active);

        },
        getPage: function () {
            this.GetBrandData(this.search, this.currentPage);
        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        makeActive: function (item) {
            this.active = item;
            this.GetBrandData(this.active);
        },
        GetBrandData: function (status) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('Product/GetBomList?isDropdown=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search + '&approvalStatus=' + status, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.bomList = response.data.results.bomsList;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        EditBom: function (id, status) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/GetBomDetails?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({
                            path: '/AddBom',
                            query:
                            {
                                data: '',
                                Add: false,
                                isBomItems: true,
                                status: status
                            }
                        })
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
        this.makeActive('Draft');
        this.$emit('update:modelValue', this.$route.name);
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.GetBrandData(this.search, 1);

    }
}
</script>