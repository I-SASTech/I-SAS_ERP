<template>
    <div  v-if="isValid('CanViewStockAverageValue')" v-bind:style="$i18n.locale == 'ar' ? languageChange('en') : languageChange('ar')">
        <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" >
            <div >
                <div class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-6 col-lg-6">
                                <div class="form-group ">
                                    <h5 class="card-title DayHeading ">{{ $t('ProductStockAvgValue.ProductStockAverageValue') }}</h5>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="form-group " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    <router-link :to="'/AllReports'">
                                        <a href="javascript:void(0)" class="btn btn-outline-primary "><i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                                    </router-link>
                                    <a href="javascript:void(0)" class="btn btn-outline-primary " v-if="isValid('CanPrintStockAverageValue')" v-on:click="PrintDetails">{{ $t('ProductStockAvgValue.Print') }}</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" v-bind:key="render" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'pr-3' " >
                        <div class="col-md-3 col-lg-3">
                            <div class="form-group ml-3">
                                <label>{{ $t('ProductStockAvgValue.FromDate') }}</label>
                                <datepicker v-model="fromDate" :key="render" />
                            </div>
                        </div>
                        <div class="col-md-3 col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('ProductStockAvgValue.ToDate') }}</label>
                                <datepicker v-model="toDate" :key="render" />
                            </div>
                        </div>
                        <div class="col-md-3 col-lg-3">
                            <div class="form-group" v-on:click="advanceFilters = !advanceFilters">
                                <div class="form-group mt-3">
                                    <a href="javascript:void(0)" class="btn btn-outline-primary "><i class="fa fa-filter"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" v-if="advanceFilters" id="hide">
                        <div class="col-md-3 col-lg-3">
                            <div class="form-group ml-3">
                                <label>{{ $t('ProductStockAvgValue.ProductName') }}</label>
                                <product-single-dropdown v-model="productId" :modelValue="productId" @update:modelValue="productValue(productId)" :key="render" />
                            </div>
                        </div>
                        <div class="col-md-3 col-lg-3">
                            <div class="form-group">
                                <label>{{ $t('ProductStockAvgValue.WareHouseName') }}</label>
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
                                                {{ $t('ProductStockAvgValue.Product') }}
                                            </th>
                                            <th v-if="!advanceFilters">
                                                {{ $t('ProductStockAvgValue.WareHouse') }}
                                            </th>
                                            <th>
                                                {{ $t('ProductStockAvgValue.QuantityIn') }}
                                            </th>
                                            <th>
                                                {{ $t('ProductStockAvgValue.QuantityOut') }}
                                            </th>
                                            <th>
                                                {{ $t('ProductStockAvgValue.AvgPurchasePrice') }}
                                            </th>
                                            <th>
                                                {{ $t('ProductStockAvgValue.AvgSalePrice') }}
                                            </th>
                                            <th>
                                                {{ $t('ProductStockAvgValue.LineTotal') }}
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
                                                 <span v-if="language=='en'">{{product.productName}}</span> 
                                                <span v-else>{{product.productNameArabic}}</span> 
                                            </td>
                                            <td v-if="!advanceFilters">
                                                <span v-if=" $i18n.locale=='en'">{{product.wareHouseName}}</span>
                                                <span v-else>{{product.wareHouseNameArabic}}</span><br />
                                                <span class="btn-primary rounded border-secondary ">
                                                    {{getTransactionType(product.transactionType)}}
                                                    </span>                                            </td>
                                            <td v-if="isMultiUnit == 'true'" class="text-center">
                                                {{product.transactionType=='StockOut'?0:product.transactionType=='StockIn'?product.highQtyIn:product.transactionType=='SaleInvoice'?0:product.transactionType=='PurchaseReturn'?product.highQtyOut:product.transactionType=='PurchasePost'?product.highQtyIn:product.transactionType=='CashReceipt'?0:product.transactionType=='CashReceipt'?0:''}}
                                            </td>
                                            <td v-else class="text-center">
                                                {{product.transactionType=='StockOut'?0:product.transactionType=='StockIn'?product.quantityIn:product.transactionType=='SaleInvoice'?0:product.transactionType=='PurchaseReturn'?product.quantityOut:product.transactionType=='PurchasePost'?product.quantityIn:product.transactionType=='CashReceipt'?0:product.transactionType=='CashReceipt'?0:''}}
                                            </td>
                                            <td v-if="isMultiUnit == 'true'" class="text-center">
                                                {{product.transactionType=='StockOut'?product.highQtyOut:product.transactionType=='StockIn'?0:product.transactionType=='SaleInvoice'?product.highQtyOut:product.transactionType=='PurchaseReturn'?0:product.transactionType=='PurchasePost'?0:product.transactionType=='CashReceipt'?0:product.transactionType=='CashReceipt'?0:''}}
                                            </td>
                                            <td v-else class="text-center">
                                                {{product.transactionType=='StockOut'?product.quantityOut:product.transactionType=='StockIn'?0:product.transactionType=='SaleInvoice'?product.quantityOut:product.transactionType=='PurchaseReturn'?0:product.transactionType=='PurchasePost'?0:product.transactionType=='CashReceipt'?0:product.transactionType=='CashReceipt'?0:''}}
                                            </td>
                                            <td class="text-center">
                                                {{product.transactionType=='StockOut'?0:product.transactionType=='StockIn'?product.avgPurchasePrice:product.transactionType=='SaleInvoice'?0:product.transactionType=='PurchaseReturn'?product.avgPurchasePrice:product.transactionType=='PurchasePost'?product.avgPurchasePrice:product.transactionType=='CashReceipt'?0:product.transactionType=='CashReceipt'?0:''}}
                                            </td>
                                            <td class="text-center">
                                                {{product.transactionType=='StockOut'?product.avgSalePrice:product.transactionType=='StockIn'?0:product.transactionType=='SaleInvoice'?product.avgSalePrice:product.transactionType=='PurchaseReturn'?0:product.transactionType=='PurchasePost'?0:product.transactionType=='CashReceipt'?0:product.transactionType=='CashReceipt'?0:''}}
                                            </td>
                                            <td class="text-center">
                                                {{parseFloat(product.quantityOut * product.avgSalePrice).toFixed(3).slice(0,-1)}}
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
        <productStockAvgValuePrintReport :printDetails="productList" :headerFooter="headerFooter" :dates="combineDate" :formName="formName" :isPrint="isShown" :isShown="advanceFilters" v-if="productList.length != 0" v-bind:key="printRender"></productStockAvgValuePrintReport>
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
                warehouseId: '00000000-0000-0000-0000-000000000000',
                fromDate: '',
                toDate: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                productList: [],
                isShown: false,
                formName: 'Product Stock Average Value',
                printRender: 0,
                advanceFilters: false,
                combineDate: '',
                language: 'Nothing',
                isMultiUnit: '',
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
                var productid = this.productId;
                if(!this.advanceFilters){
                    warehouseid = '00000000-0000-0000-0000-000000000000';
                    productid = '00000000-0000-0000-0000-000000000000';
                }
                this.GetInventoryList(fromdate, todate, 1, productid, warehouseid);
            },
            fromDate: function (fromdate) {
                var todate = this.toDate;
                var warehouseid = this.warehouseId;
                var productid = this.productId;
                if(!this.advanceFilters){
                    warehouseid = '00000000-0000-0000-0000-000000000000';
                    productid = '00000000-0000-0000-0000-000000000000';
                }
                this.GetInventoryList(fromdate, todate, 1, productid, warehouseid);
            }
        },
        methods: {
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';

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
            languageChange: function (lan) {
                if (this.language == lan) {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/ProductStockAvgValue');

                }


            },
            productValue: function (x) {
                var warehouseid = this.warehouseId;
                var productid = x;
                if(!this.advanceFilters){
                    warehouseid = '00000000-0000-0000-0000-000000000000';
                    productid = '00000000-0000-0000-0000-000000000000';
                }
                this.GetInventoryList(this.fromDate, this.toDate, 1, productid, warehouseid);
            },
            warehouseValue: function (x) {
                var warehouseid = x;
                var productid = this.productId;
                if(!this.advanceFilters){
                    warehouseid = '00000000-0000-0000-0000-000000000000';
                    productid = '00000000-0000-0000-0000-000000000000';
                }
                this.GetInventoryList(this.fromDate, this.toDate, 1, productid, warehouseid);
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
                this.$https.get('/Company/GetInventoryList?fromDate=' + fromdate + '&toDate='+ todate + '&pageNumber=' + currentPage + '&isPASV=true' + '&productId=' + productId + '&warehouseId=' + warehouseId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.productList = response.data.results;
                        root.productList.forEach(function (x) {
                            
                            x.highQtyIn = parseInt(parseFloat(x.quantityIn == undefined ? 0 : x.quantityIn) / parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack)) + '-' + parseInt(parseFloat(x.quantityIn == undefined ? 0 : x.quantityIn) % parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack))
                            x.highQtyOut = parseInt(parseFloat(x.quantityOut == undefined ? 0 : x.quantityOut) / parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack)) + '-' + parseInt(parseFloat(x.quantityOut == undefined ? 0 : x.quantityOut) % parseFloat(x.unitPerPack == null ? 0 : x.unitPerPack))
                        });
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
            },

             getTransactionType(transactionType){
                 if (transactionType == 'StockOut') return this.$t('ProductStockAvgValue.StockOut')
                 if (transactionType == 'StockIn') return this.$t('ProductStockAvgValue.StockIn')
                 if (transactionType == 'SaleInvoice') return this.$t('ProductStockAvgValue.SaleInvoice')
                 if (transactionType == 'PurchaseReturn') return this.$t('ProductStockAvgValue.PurchaseReturn')
                 if (transactionType == 'PurchasePost') return this.$t('ProductStockAvgValue.Purchase')
                 if (transactionType == 'CashReceipt') return this.$t('ProductStockAvgValue.Cash')
            }
        },
        mounted: function () {
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            this.GetHeaderDetail();
            this.language = this.$i18n.locale;
            this.fromDate = moment().subtract(15, 'days').format("DD MMM YYYY");
            this.toDate = moment().format("DD MMM YYYY");
            this.render++;
        }
    }
</script>
