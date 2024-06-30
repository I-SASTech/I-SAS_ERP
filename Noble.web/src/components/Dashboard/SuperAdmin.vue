<template>
    <div>
        <div class="row">
            <div class="col-lg-3 col-md-6 col-sm-6">
                <div class="card card-stats">
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-5 col-md-4">
                                <div class="icon-big text-center icon-warning">
                                    <i class="fas fa-cash-register text-primary"></i>
                                </div>
                            </div>
                            <div class="col-7 col-md-8">
                                <div class="numbers">
                                    <p class="card-category">{{ $t('SuperAdmin.OpeningCash') }}</p>
                                    <p class="card-title">{{currency}} {{counterDetails.openingCash.toFixed(3).slice(0,-1)}}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer ">
                        <hr>
                        <div class="stats">
                            <i class="fa fa-refresh"></i>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-6">
                <div class="card card-stats">
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-5 col-md-4">
                                <div class="icon-big text-center icon-warning">
                                    <!--<i class="wallet "></i>-->
                                    <i class="fas fa-wallet text-primary"></i>
                                </div>
                            </div>
                            <div class="col-7 col-md-8">
                                <div class="numbers">
                                    <p class="card-category">{{ $t('SuperAdmin.CashInHand') }}</p>
                                    <p class="card-title">{{currency}} {{counterDetails.cashInHand.toFixed(3).slice(0,-1)}}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer ">
                        <hr>
                        <div class="stats">
                            <i class="fa fa-refresh"></i>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-6">
                <div class="card card-stats">
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-5 col-md-4">
                                <div class="icon-big text-center icon-warning">
                                    <i class="fas fa-university text-primary"></i>
                                </div>
                            </div>
                            <div class="col-7 col-md-8">
                                <div class="numbers">
                                    <p class="card-category">{{ $t('SuperAdmin.Bank') }}</p>
                                    <p class="card-title">{{currency}} {{counterDetails.bank.toFixed(3).slice(0,-1)}}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer ">
                        <hr>
                        <div class="stats">
                            <i class="fa fa-refresh"></i>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-3 col-md-6 col-sm-6">
                <div class="card card-stats">
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-5 col-md-4">
                                <div class="icon-big text-center icon-warning">
                                    <i class="far fa-money-bill-alt text-primary"></i>
                                </div>
                            </div>
                            <div class="col-7 col-md-8">
                                <div class="numbers">
                                    <p class="card-category">{{ $t('SuperAdmin.Expense') }}</p>
                                    <p class="card-title">{{currency}} {{counterDetails.expense.toFixed(3).slice(0,-1)}}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer ">
                        <hr>
                        <div class="stats">
                            <i class="fa fa-refresh"></i>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <!--<div class="row">
            <div class="col-lg-3 col-md-6 col-sm-6">
                <div class="card card-stats">
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-5 col-md-4">
                                <div class="icon-big text-center icon-warning">
                                    <i class="fas fa-money-bill text-primary"></i>
                                </div>
                            </div>
                            <div class="col-7 col-md-8">
                                <div class="numbers">
                                    <p class="card-category">Total</p>
                                    <p class="card-title">{{currency}} {{((counterDetails.openingCash + counterDetails.cashInHand)  - counterDetails.expense).toFixed(3).slice(0,-1)}}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer ">
                        <hr>
                        <div class="stats">
                            <i class="fa fa-refresh"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>-->
        <div v-if="isValid('Can View Report')">
            <div class="row">
                <div class="col-md-12 ml-auto mr-auto">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-md-6 col-lg-6">
                                    <h6 class="ml-3">{{ $t('SuperAdmin.InventoryFilterReport') }}</h6>
                                </div>
                                <div class="col-md-6 col-lg-6">
                                    <a href="javascript:void(0)" class="btn btn-outline-primary  float-right" v-on:click="PrintDetails">{{ $t('SuperAdmin.Print') }}</a>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-lg-3">
                                    <div class="form-group">
                                        <label>{{ $t('SuperAdmin.FromDate') }}</label>
                                        <datepicker v-model="fromDate" :key="rander" />
                                    </div>
                                </div>
                                <div class="col-md-3 col-lg-3">
                                    <div class="form-group">
                                        <label>{{ $t('SuperAdmin.ToDate') }}</label>
                                        <datepicker v-model="toDate" :key="rander" />
                                    </div>
                                </div>
                                <div class="col-md-2 col-lg-2 mt-2">
                                    <div class="form-group" v-on:click="advanceFilters = !advanceFilters">
                                        <div class="form-group ml-3">
                                            <a href="javascript:void(0)" class="btn btn-outline-primary "><i class="fa fa-filter"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="col-lg-12">                                
                                <div class="row" v-if="advanceFilters" id="hide">
                                    <div class="col-md-3 col-lg-3">
                                        <div class="form-group ml-3">
                                            <span>{{ $t('SuperAdmin.WareHouseName') }}</span>
                                            <warehouse-dropdown v-model="warehouseId" @update:modelValue="getByWarehouse(warehouseId)" />
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-striped table-hover table_list_bg">
                                        <thead class="">
                                            <tr>
                                                <th>#</th>
                                                <th>
                                                    {{ $t('SuperAdmin.Product') }}
                                                </th>
                                                <th v-if="!advanceFilters">
                                                    {{ $t('SuperAdmin.WareHouse') }}
                                                </th>
                                                <th>
                                                    {{ $t('SuperAdmin.QuantityIn') }}
                                                </th>
                                                <th>
                                                    {{ $t('SuperAdmin.QuantityOut') }}
                                                </th>
                                                <th class="text-center">
                                                    {{ $t('SuperAdmin.Details') }}
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(inventory,index) in inventoryList" v-bind:key="inventory.id">
                                                <td v-if="currentPage === 1">
                                                    {{index+1}}<br />  <small>({{convertDate(inventory.date)}})</small> 
                                                </td>
                                                <td v-else>
                                                    {{((currentPage*10)-10) +(index+1)}} {{convertDate(inventory.date)}}
                                                </td>
                                                <td>
                                                    {{inventory.productName}}
                                                </td>
                                                <td v-if="!advanceFilters">
                                                    {{inventory.wareHouseName}}<br /><span class="btn-primary rounded border-secondary"><small> {{inventory.transactionType=='StockOut'?'Stock Out':inventory.transactionType=='StockIn'?'Stock In':inventory.transactionType=='SaleInvoice'?'Sale Invoice':inventory.transactionType=='PurchaseReturn'?'Purchase Return':inventory.transactionType=='PurchasePost'?'Purchase Invoice':inventory.transactionType=='CashReceipt'?'Cash':inventory.transactionType=='CashReceipt'?'Cash':''}}</small></span>
                                                </td>
                                                <td>
                                                    {{inventory.transactionType=='StockOut'?'--':inventory.transactionType=='StockIn'?inventory.quantityIn:inventory.transactionType=='SaleInvoice'?'--':inventory.transactionType=='PurchaseReturn'?inventory.quantityOut:inventory.transactionType=='PurchasePost'?inventory.quantityIn:inventory.transactionType=='CashReceipt'?'--':inventory.transactionType=='CashReceipt'?'--':''}}
                                                </td>
                                                <td>
                                                    {{inventory.transactionType=='StockOut'?inventory.quantityOut:inventory.transactionType=='StockIn'?'--':inventory.transactionType=='SaleInvoice'?inventory.quantityOut:inventory.transactionType=='PurchaseReturn'?'--':inventory.transactionType=='PurchasePost'?'--':inventory.transactionType=='CashReceipt'?'--':inventory.transactionType=='CashReceipt'?'--':''}}
                                                </td>
                                                <td class="text-center">
                                                    <a href="javascript:void(0)" class="btn  btn-icon btn-primary btn-sm" v-on:click="DetailOfProduct(inventory.productId, inventory.warehouseId)"><i class=" fa fa-eye"></i></a>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
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
                </div>
                <inventoryFilterReportPrint :printDetails="inventoryList" :dates="combineDate" :isPrint="isShown" :isShown="advanceFilters" v-if="inventoryList.length != 0" v-bind:key="printRender"></inventoryFilterReportPrint>
                <div v-else> <acessdenied></acessdenied></div>
            </div>
        </div>
    </div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";

    export default {
        mixins: [clickMixin],
        data: function () {
            return {
                fromDate: '',
                toDate: '',
                warehouseId: '00000000-0000-0000-0000-000000000000',
                rander: 0,
                printRender: 0,
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                isShown: false,
                inventoryList: [],
                advanceFilters: false,
                combineDate: '',

                searchQuery: '',
                show: false,
                show1: false,
                show2: false,
                show3: false,
                counterCode: null,
                counterId: '00000000-0000-0000-0000-000000000000',
                openingCash: '',
                type: '',
                type1: '',
                CompanyID: '',
                UserID: '',
                employeeId: '',
                expense: '',
                cashInHand: '',
                bank: '',
                dayStartId: '00000000-0000-0000-0000-000000000000',
                isDayAlreadyStart: false,
                loading: false,

                time: 0,
                dateRander: 0,
                UserName: '',
                total: 0,
                grandTotal: 0,
                counterDetails: {
                    bank: 0,
                    cashInHand: 0,
                    counterCode: 0,
                    counterId: 0,
                    expense: 0,
                    openingCash: 0
                }
            }
        },
        watch: {
            toDate: function (todate) {
                var fromdate = this.fromDate;
                var warehouseid = this.warehouseId;
                if (!this.advanceFilters) {
                    warehouseid = '00000000-0000-0000-0000-000000000000';
                }
                this.GetInventoryList(fromdate, todate, 1, warehouseid);
            },
            fromDate: function (fromdate) {
                var todate = this.toDate;
                var warehouseid = this.warehouseId;
                if (!this.advanceFilters) {
                    warehouseid = '00000000-0000-0000-0000-000000000000';
                }
                this.GetInventoryList(fromdate, todate, 1, warehouseid);
            }
        },

        methods: {
            DayRegister: function () {
                var root = this;
               
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Product/IsDayStart?userId=' + this.UserID + '&employeeId' + this.employeeId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isDayStart) {

                            root.$https.get('/Product/GetCounterInformation?userId=' + root.UserID + '&employeeId' + root.employeeId + '&isDayEnd=' + true,
                                { headers: { "Authorization": `Bearer ${token}` } })
                                .then(function (response) {
                                    root.counterCode = response.data.counterCode;
                                    root.counterId = response.data.counterId;
                                    root.getTerminalIds();
                                },
                                    function (error) {
                                        root.loading = false;
                                        console.log(error);
                                    }).then(function () {
                                        if (root.counterCode != null && root.counterCode != '' && root.counterCode != undefined) {
                                            root.show2 = !root.show2;
                                            root.type = "Add";
                                        }

                                    });
                        } else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: 'Day not start yet.',
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                //buttonStyling: false,
                                buttonsStyling: false,
                                icon: 'error'
                            });
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            },

            getTerminalIds: function () {
                var root = this;
               
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var ids = [{ id: root.counterId, name: root.counterCode }];

                root.$https.post('/Product/DayCounterInformation', ids, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        root.counterDetails = {
                            bank: 0,
                            cashInHand: 0,
                            counterCode: 0,
                            counterId: 0,
                            expense: 0,
                            openingCash: 0
                        }
                        response.data.forEach(function (x) {
                            root.counterDetails.bank += x.bank;
                            root.counterDetails.cashInHand += x.cashInHand;
                            root.counterDetails.counterCode = x.counterCode;
                            root.counterDetails.expense += x.expense;
                            root.counterDetails.openingCash += x.openingCash;
                        })
                    });
            },

            convertDate: function (date) {
                return moment(date).format('l');
            },
            getPage: function () {
                this.GetInventoryList(this.fromDate, this.toDate, this.currentPage, this.warehouseId);
            },
            getByWarehouse: function (warehouseid) {
                this.GetInventoryList(this.fromDate, this.toDate, this.currentPage, warehouseid);
            },
            GetInventoryList: function (fromdate, todate, currentPage, warehouseId) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/GetInventoryList?fromDate=' + fromdate + '&toDate=' + todate + '&pageNumber=' + currentPage + '&isInventory=true' + '&warehouseId=' + warehouseId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.inventoryList = response.data.results;
                            root.pageCount = response.data.pageCount;
                            root.rowCount = response.data.rowCount;
                        }
                    });
            },
            DetailOfProduct: function (x, y) {
                this.$router.push({
                    path: '/ProductFilterDashBoard',
                    query: { fromDate: this.fromDate, toDate: this.toDate, productId: x, warehouseId: y }
                })
            },
            PrintDetails: function () {
                var root = this;
                root.isShown = true;
                root.combineDate = 'From Date: ' + this.fromDate + ' To Date: ' + this.toDate;
                root.printRender++;
            }
        },
        mounted: function () {
           
            this.currency = localStorage.getItem('currency');
               
                this.CompanyID = localStorage.getItem('CompanyID');
                this.UserID = localStorage.getItem('UserID');
                this.employeeId = localStorage.getItem('EmployeeId');                
                this.DayRegister();
            
            this.fromDate = moment().format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.rander++;
        }
    }
</script>
<style scoped>
    .card-title {
        font-size: 22px;
    }
</style>