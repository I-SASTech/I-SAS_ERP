<template>
    <div class="container-fluid ">
        <div class="row m-4">

            <div class="col-lg-12 col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title DayHeading">{{ $t('ProductDetailDashBoard.Product') }}</h4>
                    </div>
                    <div class="row">
                        <div class="col-md-5 col-lg-5">
                            <div class="form-group ml-3">
                                <datepicker v-model="search" :key="rander" />
                            </div>
                        </div>
                        <div class="col-md-5 col-lg-5">
                            <div class="form-group ml-3">
                                <product-single-dropdown v-model="productId" :modelValue="productId" @update:modelValue="productValue(productId)" :key="rander" />
                            </div>
                        </div>

                        <div class="col-md-4 col-lg-3">

                            <a href="javascript:void(0)" class="btn btn-outline-primary  " v-on:click="Dashboard"><i class="fa fa-plus"></i> Dashboard</a>
                        </div>

                    </div>
                    <div class="card-body">
                        <div>
                            <div class="table-responsive">
                                <table class="table table-striped table-hover table_list_bg">
                                    <thead class="">
                                        <tr>

                                            <th>#</th>
                                            <th>
                                                {{ $t('ProductDetailDashBoard.Product') }}
                                            </th>
                                            <th>
                                                {{ $t('ProductDetailDashBoard.WareHouse') }}
                                            </th>
                                            <th>
                                                {{ $t('ProductDetailDashBoard.QuantityIn') }}
                                            </th>
                                            <th>
                                                {{ $t('ProductDetailDashBoard.QuantityOut') }}
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
                                            <td>
                                                {{product.productName}}
                                            </td>
                                            <td>
                                                {{product.wareHouseName}}<br /><span class="btn-primary rounded border-secondary ">{{product.transactionType=='StockOut'?'Stock Out':product.transactionType=='StockIn'?'Stock In':product.transactionType=='SaleInvoice'?'Sale Invoice':product.transactionType=='PurchaseReturn'?'Purchase Return':product.transactionType=='PurchasePost'?'Purchase':product.transactionType=='CashReceipt'?'Cash':product.transactionType=='CashReceipt'?'Cash':''}}</span>
                                            </td>
                                            <td>
                                                {{inventory.transactionType=='StockOut'?'--':inventory.transactionType=='StockIn'?inventory.quantityIn:inventory.transactionType=='SaleInvoice'?'--':inventory.transactionType=='PurchaseReturn'?inventory.quantityOut:inventory.transactionType=='PurchasePost'?inventory.quantityIn:inventory.transactionType=='CashReceipt'?'--':inventory.transactionType=='CashReceipt'?'--':''}}
                                            </td>
                                            <td>
                                                {{inventory.transactionType=='StockOut'?inventory.quantityOut:inventory.transactionType=='StockIn'?'--':inventory.transactionType=='SaleInvoice'?inventory.quantityOut:inventory.transactionType=='PurchaseReturn'?'--':inventory.transactionType=='PurchasePost'?'--':inventory.transactionType=='CashReceipt'?'--':inventory.transactionType=='CashReceipt'?'--':''}}
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>

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
                    <div class="" v-on:click="getPage()">
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
</template>
<script>



    import moment from "moment";

    export default {

        data: function () {

            return {
                render: 0,
                rander: 0,
                productId: '00000000-0000-0000-0000-000000000000',

                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                productList: []



            }
        },
        watch: {
            search: function (val) {
               

                this.GetInventory(val, this.productId, 1);
            }
        },
        methods: {
            Dashboard: function () {
                this.$router.push({
                    path: '/Dashboard1',
                  
                })
            },
            productValue: function (x) {
               
                this.GetInventory(this.search,x, 1);

            },
            DetailOfProduct: function (x) {
                this.$router.push({
                    path: '/detailofproduct',
                    query: { data: x }
                })
            },
            getDate: function (date) {
                return moment(date).format('l');
            },
            getPage: function () {


                this.GetInventory(this.search, this.currentPage);
            },

            GetInventory: function (search, productId, currentPage) {
               
                search = moment(search).format("DD MMM YYYY");
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/GetInventory?searchTerm=' + search + '&productId=' + productId + '&pageNumber=' + currentPage, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                           
                            root.productList = response.data.results;
                            root.pageCount = response.data.pageCount;
                            root.rowCount = response.data.rowCount;
                        }
                    });
            },

        },
        mounted: function () {
            
            if (this.$route.query.date != undefined) {

                
                this.productId = this.$route.query.productId;
                this.search = this.$route.query.date;
               
            }
            this.GetInventory(this.search, this.productId, 1);
            this.rander++;
          /*  this.search = moment().format("DD MMM YYYY");*/

        },
    }
</script>
