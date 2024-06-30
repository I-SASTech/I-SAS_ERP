<template>
    <div v-if="isValid('Can View Report')" v-bind:style="$i18n.locale == 'ar' ? languageChange('en') : languageChange('ar')">
        <div class="col-md-12 ml-auto mr-auto" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-6 col-lg-6">
                            <h5 class="ml-3">{{ $t('RetailReport.RetailReport') }}</h5>
                        </div>
                        <div class="col-md-6 col-lg-6">
                            <a href="javascript:void(0)" class="btn btn-outline-primary btn-round float-right" v-on:click="PrintDetails">{{ $t('RetailReport.Print') }}</a>
                            <router-link :to="'/AllReports'">
                                <a href="javascript:void(0)" class="btn btn-outline-primary btn-round float-right"><i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                            </router-link>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label>{{ $t('RetailReport.FromDate') }}</label>
                                    <datepicker v-model="fromDate" :key="rander" />
                                </div>
                            </div>
                            <div class="col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label>{{ $t('RetailReport.ToDate') }}</label>
                                    <datepicker v-model="toDate" :key="rander" />
                                </div>
                            </div>
                            <!--<div class="col-md-3 col-lg-3" v-if="advanceFilters" id="hide">
                                <div class="form-group">
                                    <span>{{ $t('AllReports.WareHouseName') }}</span>
                                    <warehouse-dropdown v-model="warehouseId" @update:modelValue="getByWarehouse(warehouseId)" />
                                </div>
                            </div>
                            <div class="col-md-2 col-lg-2 mt-2">
                                <div class="form-group" v-on:click="advanceFilters = !advanceFilters">
                                    <div class="form-group ml-3">
                                        <a href="javascript:void(0)" class="btn btn-outline-primary btn-round"><i class="fa fa-filter"></i></a>
                                    </div>
                                </div>
                            </div>-->
                        </div>
                        <div class="table-responsive">
                            <table class="table">
                                <thead class="">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            {{ $t('RetailReport.Date') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('RetailReport.TotalCash') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('RetailReport.TotalBank') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('RetailReport.TotalCredit') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('RetailReport.TotalExpense') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('RetailReport.TotalVAT') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(inventory,index) in inventoryList" v-bind:key="inventory.id">
                                        <td v-if="currentPage === 1">
                                            {{index+1}}
                                        </td>
                                        <td v-else>
                                            {{((currentPage*10)-10) +(index+1)}}
                                        </td>
                                        <td>
                                            <span>{{getDate(inventory.date)}}</span>
                                        </td>
                                        <td class="text-center">
                                            {{inventory.totalCash}}
                                        </td>
                                        <td class="text-center">
                                            {{inventory.totalBank}}
                                        </td>
                                        <td class="text-center">
                                            {{inventory.totalCredit}}
                                        </td>
                                        <td class="text-center">
                                            {{inventory.totalExpence}}
                                        </td>
                                        <td class="text-center">
                                            {{inventory.totalVat *(-1)}}
                                        </td>
                                        
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
        <retail-report-print :printDetails="inventoryList" :headerFooter="headerFooter" :fromDate="fromDate" :toDate="toDate" v-if="print" v-bind:key="printRender" />
    </div>
    <div v-else> <acessdenied></acessdenied></div>
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
                stockDetail: '',
                print: false,
                inventoryList: [],
                advanceFilters: false,
                combineDate: '',
                highQtyIn: 0,
                highQtyOut: 0,
                highQtyBalance: 0,
                isMultiUnit: '',
                language: 'Nothing',
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
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
            languageChange: function (lan) {
                if (this.language == lan) {
                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;
                    this.$router.go('/InventoryFilterReport');
                }
            },
            getDate: function (date) {

                return moment(date).format(' YYYY-MM-DD');
            },
            convertDate: function (date) {
                return moment(date).format('l');
            },
            getPage: function () {
                this.GetInventoryList(this.fromDate, this.toDate, this.currentPage, this.warehouseId);
            },
            getByWarehouse: function (warehouseid) {
                this.currentPage = 1;
                this.GetInventoryList(this.fromDate, this.toDate, this.currentPage, warehouseid);
            },
            GetInventoryList: function (fromdate, todate, currentPage) {
                /*fromdate, todate, currentPage*/
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/GetRetailList?fromDate=' + fromdate + '&toDate=' + todate + '&pageNumber=' + currentPage , { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                    
                        if (response.data != null) {
                            
                            root.inventoryList = response.data;

                            root.pageCount = response.data.pageCount;
                            root.rowCount = response.data.rowCount;
                        }
                    });
            },
            PrintDetails: function () {
                var root = this;
                root.print = true;
                root.printRender++;
            },
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + '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';

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        }
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
                            root.getBase64Image(root.headerFooter.company.logoPath);
                        }
                    });
            },
        },
        mounted: function () {
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            this.GetHeaderDetail()
            this.language = this.$i18n.locale;
            this.fromDate = moment().add(-7, 'days').format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.rander++;
        }
    }
</script>