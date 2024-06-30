<template>
    <div v-if="isValid('Can View Report')" v-bind:style="$i18n.locale == 'ar' ? languageChange('en') : languageChange('ar')">
        <div class="col-md-12 ml-auto mr-auto" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-6 col-lg-6">
                            <h5 class="ml-3">Medical Report</h5>
                        </div>
                        <div class="col-md-6 col-lg-6">
                            <a href="javascript:void(0)" class="btn btn-outline-primary btn-round float-right" v-on:click="PrintDetails">{{ $t('MedicalReport.Print') }}</a>
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
                                    <label>{{ $t('MedicalReport.FromDate') }}</label>
                                    <datepicker v-model="fromDate" :key="rander" />
                                </div>
                            </div>
                            <div class="col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label>{{ $t('MedicalReport.ToDate') }}</label>
                                    <datepicker v-model="toDate" :key="rander" />
                                </div>
                            </div>
                        </div>
                        <div class="row" >
                            <div class="col-md-3 col-lg-3">
                                <div class="form-group ml-3">
                                    <span>Select Category</span>
                                    <categorydropdown @update:modelValue="getByWarehouse(categoryId)" v-model="categoryId"></categorydropdown>

                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <table class="table table-striped table-hover table_list_bg">
                                <thead class="">
                                    <tr>
                                        
                                        <th>
                                            {{ $t('MedicalReport.Product') }}
                                        </th>
                                        <th>
                                            Invoice Number
                                        </th>
                                        <th class="text-center">
                                            Doctor Name
                                        </th>
                                        <th class="text-center">
                                            Hospital Name
                                        </th>
                                        <th class="text-center">
                                            Value
                                        </th>
                                        <th class="text-center">
                                            Remaing Value
                                        </th> 
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(inventory) in inventoryList" v-bind:key="inventory.id">

                                        <td>
                                            <span v-if="language=='en'">{{inventory.productName}}</span>
                                            <span v-else>{{inventory.productNameArabic}}</span>
                                        </td>
                                        <td>
                                            <span v-if=" $i18n.locale=='en'">{{inventory.invoiceNumber}}</span>
                                            <span v-else>{{inventory.invoiceNumber}}</span><br />

                                        </td>
                                        <td class="text-center">
                                            {{inventory.doctorName}}
                                        </td>
                                        <td class="text-center">
                                            {{inventory.hospitalName}}
                                        </td>
                                        <td class="text-center">
                                            {{inventory.stockValue}}
                                        </td>
                                        <td class="text-center">
                                            {{inventory.currentStock}}
                                        </td>
                                        <td class="text-center">
                                            <a href="javascript:void(0)" class="btn btn-round btn-icon btn-primary btn-sm" v-on:click="DetailOfProduct(inventory.productId, inventory.warehouseId)"><i class=" fa fa-eye"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <inventoryFilterReportPrint :printDetails="inventoryList" :dates="combineDate" :isPrint="isShown" :isShown="advanceFilters" v-if="inventoryList.length != 0" v-bind:key="printRender"></inventoryFilterReportPrint>
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
                isShown: false,
                inventoryList: [],
                categoryId: '00000000-0000-0000-0000-000000000000',
                advanceFilters: false,
                combineDate: '',
                language: 'Nothing',
            }
        },
        watch: {
            toDate: function (todate) {
                var fromdate = this.fromDate;
                var warehouseid = this.categoryId;
                
                this.GetInventoryList(fromdate, todate,  warehouseid);
            },
            fromDate: function (fromdate) {
                var todate = this.toDate;
                var warehouseid = this.categoryId;
               
                this.GetInventoryList(fromdate, todate, warehouseid);
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
            convertDate: function (date) {
                return moment(date).format('l');
            },
            getPage: function () {
                this.GetInventoryList(this.fromDate, this.toDate,  this.warehouseId);
            },
            getByWarehouse: function (warehouseid) {
                this.GetInventoryList(this.fromDate, this.toDate,  warehouseid);
            },
            GetInventoryList: function (fromdate, todate, warehouseId) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Report/MedicalReport?fromDate=' + fromdate + '&toDate=' + todate  + '&categoryId=' + warehouseId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.inventoryList = response.data;
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
            },

            getTransactionType(transactionType){
                if (transactionType == 'StockOut') return this.$t('MedicalReport.StockOut')
                if (transactionType == 'StockIn') return this.$t('MedicalReport.StockIn')
                if (transactionType == 'SaleInvoice') return this.$t('MedicalReport.SaleInvoice')
                if (transactionType == 'PurchaseReturn') return this.$t('MedicalReport.PurchaseReturn')
                if (transactionType == 'PurchasePost') return this.$t('MedicalReport.Purchase')
                if (transactionType == 'CashReceipt') return this.$t('MedicalReport.Cash')
            }
        },
        mounted: function () {
            this.language = this.$i18n.locale;
            this.fromDate = moment().format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.rander++;
        }
    }
</script>