<template>
    <div>
        <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-6 col-lg-6">
                            <div class="form-group">
                                <h5 class="card-title DayHeading">{{ $t('ProductFilterDashBoard.ProductInventoryInformation') }}</h5>
                            </div>
                        </div>
                        <div class="col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                            <div class="form-group">
                                <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="PrintDetails">{{ $t('ProductFilterDashBoard.Print') }}</a>
                                <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="Dashboard">{{ $t('ProductFilterDashBoard.Cancel') }}</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" v-bind:key="render">
                    <div class="col-md-3 col-lg-3">
                        <div class="form-group ml-3">
                            <label>{{ $t('ProductFilterDashBoard.FromDate') }}</label>
                            <datepicker v-model="fromDate" :key="render" />
                        </div>
                    </div>
                    <div class="col-md-3 col-lg-3">
                        <div class="form-group">
                            <label>{{ $t('ProductFilterDashBoard.ToDate') }}</label>
                            <datepicker v-model="toDate" :key="render" />
                        </div>
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <div class="form-group" v-on:click="advanceFilters = !advanceFilters">
                            <div class="form-group ml-3">
                                <a href="javascript:void(0)" class="btn btn-outline-primary "><i class="fa fa-filter"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" v-if="advanceFilters" id="hide">
                    <div class="col-md-3 col-lg-3">
                        <div class="form-group ml-3">
                            <label>{{ $t('ProductFilterDashBoard.ProductName') }}</label>
                            <product-single-dropdown v-model="productId" :modelValue="productId" @update:modelValue="productValue(productId)" :key="render" />
                        </div>
                    </div>
                    <div class="col-md-3 col-lg-3">
                        <div class="form-group">
                            <label>{{ $t('ProductFilterDashBoard.WareHouseName') }}</label>
                            <warehouse-dropdown v-model="warehouseId" :modelValue="warehouseId" @update:modelValue="warehouseValue(warehouseId)" :key="render" />
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div>
                        <div class="table-responsive">
                            <table class="table table-striped table-hover table_list_bg">
                                <thead class="">
                                    <tr>
                                        <th>#</th>
                                        <th v-if="!advanceFilters">
                                            {{ $t('ProductFilterDashBoard.Product') }}
                                        </th>
                                        <th v-if="!advanceFilters">
                                            {{ $t('ProductFilterDashBoard.WareHouse') }}
                                        </th>
                                        <th>
                                            {{ $t('ProductFilterDashBoard.QuantityIn') }}
                                        </th>
                                        <th>
                                            {{ $t('ProductFilterDashBoard.QuantityOut') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(product,index) in productList" v-bind:key="product.id">
                                        <td v-if="currentPage === 1">
                                            {{index+1}}<br />   ({{getDate(product.date)}})
                                        </td>
                                        <td v-else>
                                            {{((currentPage*10)-10) +(index+1)}} {{getDate(product.date)}}
                                        </td>
                                        <td v-if="!advanceFilters">
                                            {{product.productName}}
                                        </td>
                                        <td v-if="!advanceFilters">
                                            {{product.wareHouseName}}<br /><span class="btn-primary rounded border-secondary ">{{product.transactionType=='StockOut'?'Stock Out':product.transactionType=='StockIn'?'Stock In':product.transactionType=='SaleInvoice'?'Sale Invoice':product.transactionType=='PurchaseReturn'?'Purchase Return':product.transactionType=='PurchasePost'?'Purchase':product.transactionType=='CashReceipt'?'Cash':product.transactionType=='CashReceipt'?'Cash':''}}</span>
                                        </td>
                                        <td>
                                            {{product.transactionType=='StockOut'?'--':product.transactionType=='StockIn'?product.quantityIn:product.transactionType=='SaleInvoice'?'--':product.transactionType=='PurchaseReturn'?product.quantityOut:product.transactionType=='PurchasePost'?product.quantityIn:product.transactionType=='CashReceipt'?'--':product.transactionType=='CashReceipt'?'--':''}}
                                        </td>
                                        <td>
                                            {{product.transactionType=='StockOut'?product.quantityOut:product.transactionType=='StockIn'?'--':product.transactionType=='SaleInvoice'?product.quantityOut:product.transactionType=='PurchaseReturn'?'--':product.transactionType=='PurchasePost'?'--':product.transactionType=='CashReceipt'?'--':product.transactionType=='CashReceipt'?'--':''}}
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="float-left">
                        <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount < 10">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount >= 11  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                    </div>
                    <div class="float-right">
                        <div class="overflow-auto" v-on:click="getPage()">
                            <b-pagination pills size="lg" v-model="currentPage"
                                          :total-rows="rowCount"
                                          :per-page="10"
                                          first-text="First"
                                          prev-text="Previous"
                                          next-text="Next"
                                          last-text="Last"></b-pagination>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <productInventoryRegisterPrintReport :printDetails="productList" :dates="combineDate" :isPrint="Shown" :isShown="advanceFilters" v-if="productList.length != 0" v-bind:key="printRender"></productInventoryRegisterPrintReport>
    </div>
</template>
<script>
    import moment from "moment";
    export default {
        data: function () {
            return {
                render: 0,
                productId: '00000000-0000-0000-0000-000000000000',
                warehouseId: '00000000-0000-0000-0000-000000000000',
                fromDate: '',
                toDate: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                productList: [],
                Shown: false,
                printRender: 0,
                advanceFilters: false,
                combineDate: ''
            }
        },
        watch: {
            toDate: function (todate) {
                var fromdate = this.fromDate;
                var warehouseid = this.warehouseId;
                var productid = this.productId;
                if (!this.advanceFilters) {
                    warehouseid = '00000000-0000-0000-0000-000000000000';
                    productid = '00000000-0000-0000-0000-000000000000';
                }
                this.GetInventoryList(fromdate, todate, 1, productid, warehouseid);
            },
            fromDate: function (fromdate) {
                var todate = this.toDate;
                var warehouseid = this.warehouseId;
                var productid = this.productId;
                if (!this.advanceFilters) {
                    warehouseid = '00000000-0000-0000-0000-000000000000';
                    productid = '00000000-0000-0000-0000-000000000000';
                }
                this.GetInventoryList(fromdate, todate, 1, productid, warehouseid);
            }
        },
        methods: {
            Dashboard: function () {
                this.$router.push({
                    path: '/InventoryFilterReport',
                })
            },
            productValue: function (x) {
                var warehouseid = this.warehouseId;
                var productid = x;
                if (!this.advanceFilters) {
                    warehouseid = '00000000-0000-0000-0000-000000000000';
                    productid = '00000000-0000-0000-0000-000000000000';
                }
                this.GetInventoryList(this.fromDate, this.toDate, this.currentPage, productid, warehouseid);
            },
            warehouseValue: function (x) {
                var warehouseid = x;
                var productid = x;
                if (!this.advanceFilters) {
                    warehouseid = '00000000-0000-0000-0000-000000000000';
                    productid = '00000000-0000-0000-0000-000000000000';
                }
                this.GetInventoryList(this.fromDate, this.toDate, this.currentPage, productid, warehouseid);
            },
            getDate: function (date) {
                return moment(date).format('l');
            },
            getPage: function () {
                this.GetInventoryList(this.fromDate, this.toDate, this.currentPage, this.productId, this.warehouseId);
            },
            GetInventoryList: function (fromdate, todate, currentPage, productId, warehouseId) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/GetInventoryList?fromDate=' + fromdate + '&toDate=' + todate + '&pageNumber=' + currentPage + '&isProductFilter=true' + '&productId=' + productId + '&warehouseId=' + warehouseId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.productList = response.data.results;
                            root.pageCount = response.data.pageCount;
                            root.rowCount = response.data.rowCount;
                        }
                    });
            },
            PrintDetails: function () {
                var root = this;
                root.Shown = true;
                root.combineDate = 'From Date: ' + this.fromDate + ' To Date: ' + this.toDate;
                root.printRender++;
            }
        },
        mounted: function () {
            if (this.$route.query != undefined) {
                this.warehouseId = this.$route.query.warehouseId;
                this.productId = this.$route.query.productId;
                this.fromDate = this.$route.query.fromDate;
                this.toDate = this.$route.query.toDate;
                this.render++;
            }
            this.GetInventoryList(this.fromDate, this.toDate, 1, this.productId, this.warehouseId);
            this.render++;
        }
    }
</script>
