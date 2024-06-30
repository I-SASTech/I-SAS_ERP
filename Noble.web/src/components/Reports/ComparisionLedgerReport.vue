<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title"> {{ $t('ProductLedgerReport.ProductComparisionReport') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);"> {{ $t('ProductLedgerReport.Home') }}</a></li>
                                    <li class="breadcrumb-item active"> {{ $t('ProductLedgerReport.ProductComparisionReport') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="PrintDetails(false)" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="fas fa-print font-14"></i>
                                    {{ $t('ProductLedgerReport.Print') }}
                                </a>
                                <a v-on:click="PrintDetails(true)" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="far fa-file-pdf font-14"></i>
                                    {{ $t('ProductLedgerReport.ExportPDF') }}
                                </a>
                                <!-- <a v-on:click="PrintCsv"
                                    href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="far fa-file-excel font-14"></i>
                                    Export CSV
                                </a> -->
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('ProductLedgerReport.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="row align-items-center">


                        <div class="col-md-4 form-group">
                            <label> {{ $t('ProductLedgerReport.FromDate') }}</label>
                            <datepicker v-model="fromDate" :key="render" />
                        </div>
                        <div class="col-md-4 form-group">
                            <label> {{ $t('ProductLedgerReport.ToDate') }}</label>
                            <datepicker v-model="toDate" :key="render" />
                        </div>
                        <div class=" col-lg-4 form-group" v-if="allowBranches">
                            <label>{{ $t('Branches.SelectBranch') }}</label>
                            <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                        </div>

                        <div class=" col-lg-4 mt-1">

                            <button v-on:click="search22(true)" :disabled ="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled ="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div v-for="product in supplierPurchaselist" v-bind:key="product.name">
                    <h5>
                        {{product.name}}
                    </h5>
                    <table class="table mb-0">
                        <thead class="thead-light">
                            <tr>
                                <th colspan="2" class="text-center">
                                    Inventory
                                </th>

                                <th colspan="2" class="text-center">
                                    COGS

                                </th>
                                <th colspan="2" class="text-center">
                                    Sale

                                </th>


                            </tr>
                        </thead>
                        <thead class="thead-light">
                            <tr>
                                <th class="text-center">
                                    Quantity
                                </th>

                                <th class="text-center">
                                    Balance

                                </th>
                                <th class="text-center">
                                    Quantity
                                </th>

                                <th class="text-center">
                                    Balance

                                </th>
                                <th class="text-center">
                                    Quantity
                                </th>

                                <th class="text-center">
                                    Balance

                                </th>


                            </tr>
                        </thead>
                        <tbody>


                            <tr>
                                <th class="text-center">
                                    {{product.quantity}}
                                </th>

                                <th class="text-center">
                                    {{ product.inventoryBalance > 0 ? 'Dr' : 'Cr' }}  {{nonNegative(product.inventoryBalance)}}

                                </th>
                                <th class="text-center">
                                    {{product.quantity}}
                                </th>

                                <th class="text-center">
                                    {{ product.inventoryBalance > 0 ? 'Dr' : 'Cr' }} {{nonNegative(product.cogsBalance)}}

                                </th>
                                <th class="text-center">
                                    {{product.quantity}}
                                </th>

                                <th class="text-center">
                                    {{ product.inventoryBalance > 0 ? 'Dr' : 'Cr' }} {{nonNegative(product.saleBalance)}}

                                </th>


                            </tr>
                        </tbody>
                    </table>

                </div>
            </div>



            <ComparisionLedgerPrintDownload :fromDate="fromDate" :toDate="toDate" :printDetails="supplierPurchaselist" :headerFooter="headerFooter"
                                            v-if="printDownload" @close="printDownload = false" />
            <ComparisionLedgerPrintReport :fromDate="fromDate" :toDate="toDate" :printDetails="supplierPurchaselist" v-if="print"
                                          :headerFooter="headerFooter" v-bind:key="printRender" />

        </div>
    </div>
</template>


<script>
    //import moment from "moment";

    export default {
        name: 'SupplierPurchaseReport',
        data: function () {
            return {
                allowBranches: false,
                branchIds: [],
                branchId: '',
                productId: '',
                supplierPurchaselist: '',
                isMultiUnit: false,
                toDate: '',
                fromDate: '',
                counter: 0,
                printDownload: false,
                printRender: 0,
                render: 0,
                print: false,
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
            }
        },
        computed: {
            isFilterButtonDisabled() {
      return (
        this.fromDate ||
        this.toDate ||
        this.branchIds
      );
    },
  },
        watch: {
            // fromDate: function () {

            //     this.counter++;
            //     if (this.counter != 1) {
            //         this.GetSupplierPurchaseData();

            //     }

            // },
            // toDate: function () {

            //     this.counter++;
            //     if (this.counter != 2) {

            //         this.GetSupplierPurchaseData();

            //     }
            branchIds: function () {
                var root = this;
                this.branchId = '';
                this.branchIds.forEach(function (result) {
                    root.branchId = root.branchId == '' ? result.id : root.branchId + ',' + result.id;
                })
                this.GetSupplierPurchaseData();
            }
        },


        methods: {
            search22: function () {
                this.GetSupplierPurchaseData(this.fromDate, this.toDate);
            },

            clearData: function () {
                this.search = "";
                this.GetSupplierPurchaseData(this.fromDate, this.toDate);

            },
            nonNegative: function (value) {



                return parseFloat(Math.abs(value)).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            GetSupplierPurchaseData: function () {


                var root = this;
                var url = '/Report/ProductComparisionReport?fromDate=' + this.fromDate + '&toDate=' + this.toDate + '&branchId=' + this.branchId;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.supplierPurchaselist = response.data;
                        root.loading = false;
                    }
                    root.loading = false;
                });

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

            PrintDetails: function (download) {
                var root = this;
                if (download) {
                    root.printDownload = true;
                }
                else {
                    root.print = true;
                    root.printRender++;
                }

            },

        },
        mounted: function () {
            // this.fromDate = moment().add(-7, 'days').format("DD MMM YYYY");
            // this.toDate = moment().format("DD MMM YYYY");
            this.render++;
            this.isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
            this.GetHeaderDetail();
            this.GetSupplierPurchaseData();
            this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
        }
    }
</script>