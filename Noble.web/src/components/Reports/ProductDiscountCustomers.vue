<template>
    <div v-if="isValid('CanViewProductDiscountCustomer')" v-bind:style="$i18n.locale == 'ar' ? languageChange('en') : languageChange('ar')">
        <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" >
            <div >
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <h5 class="card-title DayHeading">{{ $t('ProductDiscountCustomers.ProductDiscountCustomers') }}</h5>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                <div class="form-group">
                                    <router-link :to="'/AllReports'">
                                        <a href="javascript:void(0)" class="btn btn-outline-primary "><i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                                    </router-link>
                                    <a href="javascript:void(0)" v-if="isValid('CanPrintProductDiscountCustomer')" class="btn btn-outline-primary " v-on:click="PrintDetails">{{ $t('ProductDiscountCustomers.Print') }}</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" v-bind:key="render" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'pr-3'">
                        <div class="col-md-3 col-lg-3">
                            <div class="form-group ml-3">
                                <label>{{ $t('ProductDiscountCustomers.FromDate') }}</label>
                                <datepicker v-model="fromDate" :key="render" />
                            </div>
                        </div>
                        <div class="col-md-3 col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('ProductDiscountCustomers.ToDate') }}</label>
                                <datepicker v-model="toDate" :key="render" />
                            </div>
                        </div>
                        <div class="col-md-2 col-lg-2">
                            <div class="form-group">
                                <label>{{ $t('ProductDiscountCustomers.ProductName') }}</label>
                                <product-single-dropdown v-model="productId" :modelValue="productId" :key="render" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="btn-group">
                                <button type="button" class="btn btn-outline-primary " v-on:click="valueChange(true)">{{ $t('ProductDiscountCustomers.FixedDiscount') }}</button>
                                <button type="button" class="btn btn-outline-primary " v-on:click="valueChange(false)">{{ $t('ProductDiscountCustomers.%Discount') }}</button>
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
                                            <th>
                                                {{ $t('ProductDiscountCustomers.Customer') }}
                                            </th>
                                            <th>
                                                {{ $t('ProductDiscountCustomers.QuantityOut') }}
                                            </th>
                                            <th>
                                                {{ $t('ProductDiscountCustomers.SalePrice') }}
                                            </th>
                                            <th v-if="advanceFilters">
                                                {{ $t('ProductDiscountCustomers.FixedDiscount') }}
                                            </th>
                                            <th v-if="!advanceFilters">
                                                {{ $t('ProductDiscountCustomers.PercentageDiscount') }}
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
                                                {{product.customerName}}
                                            </td>
                                            <td>
                                                {{product.quantityOut}}
                                            </td>
                                            <td>
                                                {{product.salePrice}}
                                            </td>
                                            <td v-if="advanceFilters">
                                                {{product.discountFixed}}
                                            </td>
                                            <td v-if="!advanceFilters">
                                                {{product.discountPercentage}}%
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
                            <div class="overflow-auto" v-on:click="getPage()" v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                <b-pagination pills size="lg" v-model="currentPage"
                                              :total-rows="rowCount"
                                              :per-page="10"
                                              first-text="First"
                                              prev-text="Previous"
                                              next-text="Next"
                                              last-text="Last"></b-pagination>
                            </div>
                            <div class="overflow-auto" v-on:click="getPage()" v-else>
                                <b-pagination pills size="lg" v-model="currentPage"
                                              :total-rows="rowCount"
                                              :per-page="10"
                                              first-text="الأولى"
                                              prev-text="السابقة"
                                              next-text="التالية"
                                              last-text="الأخيرة"></b-pagination>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <productDiscountCustomersPrintReport :printDetails="productList" :dates="combineDate" :formName="formName" :isPrint="isShown" :isShown="advanceFilters" v-if="productList.length != 0" v-bind:key="printRender"></productDiscountCustomersPrintReport>
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
                render: 0,
                productId: '00000000-0000-0000-0000-000000000000',
                fromDate: '',
                toDate: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                productList: [],
                isShown: false,
                formName: 'Product Discount Customers',
                printRender: 0,
                advanceFilters: true,
                combineDate: '',
                language: 'Nothing',
            }
        },
        methods: {
            languageChange: function (lan) {
                if (this.language == lan) {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/ProductDiscountCustomers');

                }


            },
            valueChange: function(x){
                if(x){
                    this.advanceFilters = x;
                    this.GetInventoryList(this.fromDate, this.toDate, 1, this.productId, x);
                }
                else{
                    this.advanceFilters = x;
                    this.GetInventoryList(this.fromDate, this.toDate, 1, this.productId, x);
                }
            },
            getDate: function (date) {
                return moment(date).format('l');
            },
            getPage: function () {
                this.GetInventoryList(this.fromDate, this.toDate, this.currentPage, this.productId, this.advanceFilters);
            },
            GetInventoryList: function (fromdate, todate, currentPage, productId, advanceFilters) {  
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/GetInventoryList?fromDate=' + fromdate + '&toDate='+ todate + '&pageNumber=' + currentPage + '&isProductCustomersDiscount=true' + '&isDiscount=' + advanceFilters + '&productId=' + productId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.productList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                    }
                });
            },
            PrintDetails: function (){
                var root = this;
                root.isShown = true;
                root.combineDate = 'From Date: ' + this.fromDate + ' To Date: ' + this.toDate;
                root.printRender++;
            }
        },
        mounted: function () { 
            this.language = this.$i18n.locale;
            this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.render++;
        }
    }
</script>
