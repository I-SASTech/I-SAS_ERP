<template>
    <div>
        <div class=" table-responsive">
            <table class="table" v-if="purchaseProducts.length > 0">
                <thead class="thead-light table-hover">
                    <tr>
                        <th style="width: 20px;">
                            #
                        </th>
                        <th style="width: 150px;">
                            {{ $t('ViewStockItem.ProductName') }}
                        </th>
                        <th style="width: 100px;" class="text-center">
                            {{ $t('ViewStockItem.UnitPrice') }}
                        </th>
                        <th style="width: 70px;" class="text-center">
                            {{ $t('ViewStockItem.salePrice') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('ViewStockItem.HighQty') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('ViewStockItem.TOTALQTY') }}
                        </th>

                        <th style="width: 100px;" class="text-center">
                            {{ $t('ViewStockItem.Quantity') }}
                        </th>


                        <th style="width: 110px;" class="text-center" v-if="isFifo">
                            {{ $t('ViewStockItem.BatchNo') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isFifo">
                            {{ $t('ViewStockItem.ExpDate') }}
                        </th>
                        <th style="width: 100px;" class="arabicLanguage">
                            {{ $t('ViewStockItem.Total') }}
                        </th>
                    </tr>
                </thead>
                <tbody id="purchase-item">                   

                        <tr v-for="(prod , index) in purchaseProducts" :key="prod.productId + index">
                            <td class="border-top-0">
                                {{index+1}}
                            </td>
                            <td class="border-top-0">
                                {{($i18n.locale == 'en' ||isLeftToRight())? products.find(x => x.id == prod.productId).englishName!=''? products.find(x => x.id == prod.productId).englishName : products.find(x => x.id == prod.productId).arabicName :    products.find(x => x.id == prod.productId).arabicName!=''? products.find(x => x.id == prod.productId).arabicName : products.find(x => x.id == prod.productId).englishName}}
                            </td>

                            <td class="border-top-0 text-center">
                                {{currency}} {{parseFloat(prod.price).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                            </td>
                            <td class="border-top-0 text-center">
                                {{currency}} {{parseFloat(prod.salePrice).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                            </td>
                            <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                                {{prod.highQty}}<br />
                                <small style="font-weight: 500;font-size:70%;">
                                    {{prod.levelOneUnit}}
                                </small>
                            </td>
                            <td class="border-top-0 text-center">
                                {{prod.quantity}}<br />
                                <small v-if="isMultiUnit=='true'" style="font-weight: 500;font-size:70%;">
                                    {{prod.basicUnit}}
                                </small>
                            </td>

                            <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                                {{prod.totalPiece}}
                            </td>
                            <td class="border-top-0 text-center" v-if="isFifo">
                                <span> {{prod.batchNo}}</span>
                            </td>

                            <td class="border-top-0 text-center" v-if=" isFifo">
                                <span>{{getDate(prod.batchExpiry)}}</span>

                            </td>
                            <td class="border-top-0 arabicLanguage">
                                {{currency}}  {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0,-1))  }}
                            </td>
                        </tr>
                    
                </tbody>
            </table>
        </div>
        <hr />
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 "></div>
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 ">
                <div class="mt-4" v-bind:key="rendered + 'g'" v-if="purchaseProducts.length > 0">
                    <table class="table" style="background-color: #F1F5FA;">
                        <tbody>
                            <tr>
                                <td colspan="2" style="width:65%;">
                                    <span class="fw-bold">  {{ $t('ViewStockItem.NoItem') }} </span>
                                </td>
                                <td class="text-end" style="width:35%;">{{ summary.item }}</td>
                            </tr>
                            <tr v-if="isMultiUnit=='true'">
                                <td colspan="2" style="width:65%;">
                                    <span class="fw-bold">   {{ $t('ViewStockItem.TotalCarton') }} </span>
                                </td>
                                <td class="text-end" style="width:35%;">{{ summary.totalCarton}}</td>
                            </tr>
                            <tr v-if="isMultiUnit=='true'">
                                <td colspan="2" style="width:65%;">
                                    <span class="fw-bold">    {{ $t('ViewStockItem.TotalPieces') }} </span>
                                </td>
                                <td class="text-end" style="width:35%;">{{ summary.totalPieces}}</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:65%;">
                                    <span class="fw-bold">  {{ $t('ViewStockItem.TotalQty') }} </span>
                                </td>
                                <td class="text-end" style="width:35%;">{{ summary.qty }}</td>
                            </tr>
                            <tr v-if="formName=='StockOut'">
                                <td colspan="2" style="width:65%;">
                                    <span class="fw-bold">   {{ $t('ViewStockItem.VAT') }} </span>
                                </td>
                                <td class="text-end" style="width:35%;">{{currency}} {{ summary.vat}}</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:65%;">
                                    <h5 class="fw-bold">    {{ $t('ViewStockItem.Total') }} </h5>
                                </td>
                                <td class="text-end" style="width:35%;">{{currency}}   {{ summary.total}}</td>
                            </tr>


                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        
            <!--<div class=" table-responsive"
                 v-bind:key="rendered + 'g'"
                 v-if="purchaseProducts.length > 0">
                <table class="table">
                    <thead class="thead-light table-hover">
                        <tr class="arabicLanguage">
                            <th class="text-center" style="width:85px;">
                                {{ $t('ViewStockItem.NoItem') }}
                            </th>
                            <th class="text-center" style="width:100px;" v-if="isMultiUnit=='true'">
                                {{ $t('ViewStockItem.TotalCarton') }}
                            </th>
                            <th class="text-center" style="width:100px;" v-if="isMultiUnit=='true'">
                                {{ $t('ViewStockItem.TotalPieces') }}
                            </th>
                            <th class="text-center" style="width:100px;">
                                {{ $t('ViewStockItem.TotalQty') }}
                            </th>
                            <th class="text-center" style="width:100px;" v-if="formName=='StockOut'">
                                {{ $t('ViewStockItem.VAT') }}
                            </th>
                            <th style="width:100px;">
                                {{ $t('ViewStockItem.Total') }}
                            </th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr class="arabicLanguage">
                            <td class="text-center">
                                {{ summary.item }}
                            </td>
                            <td class="text-center" v-if="isMultiUnit=='true'">
                                {{ summary.totalCarton}}
                            </td>
                            <td class="text-center" v-if="isMultiUnit=='true'">
                                {{ summary.totalPieces}}
                            </td>
                            <td class="text-center">
                                {{ summary.qty }}
                            </td>
                            <td class="text-center" v-if="formName=='StockOut'">
                                {{currency}} {{ summary.vat}}
                            </td>
                            <td>
                                {{currency}}   {{ summary.total}}
                            </td>

                        </tr>
                    </tbody>
                </table>
            </div>-->
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
</template>



<script>
    //
    //
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import moment from "moment";
    //import VueBarcode from 'vue-barcode';
    export default {
        name: "StockLineItem",
        props: ['purchase', 'purchaseItems', 'taxMethod', 'taxRateId', 'formName', 'wareHouseId'],
        components: {
            Loading
        },
        data: function () {
            return {
                isFifo: false,
                decimalQuantity: false,
                rendered: 0,
                product: {
                    id: "",
                },
                isMultiUnit: '',
                vats: [],
                products: [],
                purchaseProducts: [],
                loading: false,
                summary: {
                    item: 0,
                    qty: 0,
                    total: 0,
                    vat: 0,
                    totalCarton: 0,
                    totalPieces: 0
                },
                currency: '',
                searchTerm: '',
                productList: []
            };
        },
        validations() {},
        filters: {

        },
        computed: {
            itemDisable() {
                if (this.taxMethod != '' && this.taxMethod != null && this.taxRateId != '00000000-0000-0000-0000-000000000000' && this.taxRateId != undefined)
                    return false;
                return true;
            }
        },
        methods: {
            getDate: function (x) {
                if (x == null) {
                    return '-'
                }
                else {
                    return moment(x).format("DD/MM/yyyy");
                }

            },
            GetProductList: function () {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }


                // var url = this.wareHouseId != undefined ? "/Product/GetProductInformation?searchTerm=" + search + '&wareHouseId=' + this.wareHouseId + "&isDropdown=true" + '&isRaw=' + root.isRaw : "/Product/GetProductInformation?searchTerm=" + search + '&status=' + root.status + "&isDropdown=true" + '&isRaw=' + root.isRaw;
                var url = ''
                if (this.formName == 'StockOut') {
                    url = "/Product/GetProductBarcode?wareHouseId=" + this.wareHouseId + '&isRaw=' + false
                }
                else {
                    url = "/Product/GetProductBarcode?isRaw=" + false
                }
                this.$https
                    .get(url, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.productList = response.data.results.products;

                        }
                    });


            },
            onBarcodeScanned(barcode) {
                var root = this;
                if (root.productList.length > 0) {
                    var product = this.productList.find(x => x.barCode == barcode)
                    if (product != null) {
                        root.addProduct(product.id, product)
                    }
                }

            },
            CheckSalePrice: function (price, y) {

                var prod = this.purchaseProducts.find((x) => x.productId == y);
                if (parseFloat(price) > prod.salePrice) {
                    this.$swal({
                        title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: "Unit Price Geater than Sale Price!",
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                    });

                }
            },
            changeProduct: function (NewProdId, rowId) {
                this.purchaseProducts = this.purchaseProducts.filter(x => x.rowId != rowId);
                this.addProduct(NewProdId);

            },
            calcuateSummary: function () {
                
                this.summary.item = this.purchaseProducts.length;
                if (this.decimalQuantity) {
                    this.summary.totalPieces = this.purchaseProducts.reduce((totalQty, prod) => totalQty + parseFloat(prod.quantity), 0);
                }
                else {
                    this.summary.totalPieces = this.purchaseProducts.reduce((totalQty, prod) => totalQty + parseInt(prod.quantity), 0);
                }

                if (this.decimalQuantity) {
                    this.summary.totalCarton = this.purchaseProducts.reduce((totalCarton, prod) => totalCarton + parseFloat(prod.highQty), 0);
                }
                else {
                    this.summary.totalCarton = this.purchaseProducts.reduce((totalCarton, prod) => totalCarton + parseInt(prod.highQty), 0);
                }

                if (this.decimalQuantity) {
                    this.summary.qty = this.purchaseProducts.reduce(
                        (qty, prod) => qty + parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                }
                else {
                    this.summary.qty = this.purchaseProducts.reduce(
                        (qty, prod) => qty + parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                }
                this.summary.total = this.purchaseProducts
                    .reduce((total, prod) => total + parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece) * prod.price, 0)
                    .toFixed(3).slice(0, -1);

                if (this.taxMethod == 'Inclusive' || this.taxMethod == 'شامل') {
                    this.summary.vat = this.purchaseProducts
                        .reduce((vat, prod) => vat + (parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece) * prod.price) * prod.rate / (100 + prod.rate), 0)
                        .toFixed(3).slice(0, -1);
                }
                if (this.taxMethod == 'Exclusive' || this.taxMethod == 'غير شامل') {
                    this.summary.vat = this.purchaseProducts
                        .reduce((vat, prod) => vat + (parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece) * prod.price) * prod.rate / 100, 0)
                        .toFixed(3).slice(0, -1);
                    this.summary.total = (parseFloat(this.summary.total) + parseFloat(this.summary.vat)).toFixed(3).slice(0, -1);
                }

                this.$emit("update:modelValue", this.purchaseProducts);
            },

            updateLineTotal: function (e, prop, product) {

                if (prop == "price") {
                    if (e < 0) {
                        e = 0;
                    }
                    product.price = e;
                }

                if (prop == "quantity") {
                    if (e <= 0 || e == '') {
                        e = '';
                    }
                    if (String(e).split('.').length > 1 && String(e).split('.')[1].length > 2)
                        e = parseFloat(String(e).slice(0, -1))
                    product.quantity = this.decimalQuantity ? e : Math.round(e);
                }
                if (prop == "highQty") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = '';
                    }
                    product.highQty = Math.round(e);
                }
                //if (product.price == "") {
                //    product.price = 0;
                //}
                //if (product.quantity == "") {
                //    product.quantity = 0;
                //}
                product.totalPiece = (parseFloat(product.highQty == undefined ? 0 : product.highQty) * parseFloat((product.unitPerPack == null || product.unitPerPack == 0 || product.unitPerPack == undefined) ? 1 : product.unitPerPack)) + parseFloat(product.quantity == '' ? 0 : product.quantity);
                if (product.currentQuantity >= product.totalPiece || product.currentQuantity == undefined) {
                    product['outOfStock'] = false
                }
                else {
                    if (this.formName == 'StockIn') {
                        product['outOfStock'] = false
                    }
                    else {
                        product['outOfStock'] = true

                    }
                }



                product.lineTotal = (product.totalPiece == '' ? 0 : product.totalPiece) * product.price;

                var calculateVAt = 0;
                if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                    calculateVAt = (product.lineTotal * product.rate) / (100 + product.rate);
                    product.lineTotal = product.lineTotal - calculateVAt;
                }
                if (product.taxMethod == 'Exclusive' || product.taxMethod == 'غير شامل') {
                    calculateVAt = (product.lineTotal * product.rate) / 100;
                    product.lineTotal = product.lineTotal + calculateVAt;
                }
                this.purchaseProducts['product'] = product

                this.calcuateSummary();

                this.$emit("update:modelValue", this.purchaseProducts);
            },

            addProduct: function (productId, newProduct) {


                if (this.products.find(x => x.id == newProduct.id) == undefined || this.products.length <= 0) {
                    this.products.push(newProduct);
                }

                var prod = this.products.find((x) => x.id == productId);
                var rate = 0;
                var rateId = '00000000-0000-0000-0000-000000000000';
                if (this.taxRateId != "00000000-0000-0000-0000-000000000000" && this.taxRateId != undefined && this.taxRateId != "") {
                    rate = this.vats.find((value) => value.id == this.taxRateId).rate;
                    rateId = this.taxRateId;
                }
                else {
                    rate = 0;
                    rateId = '00000000-0000-0000-0000-000000000000';
                }

                this.purchaseProducts.push({
                    rowId: this.createUUID(),
                    productId: prod.id,
                    price: prod.inventory != null ? prod.inventory.price : 0,
                    salePrice: prod.salePrice,
                    quantity: 1,
                    highQty: 0,
                    totalPiece: 1,
                    currentQuantity: prod.inventory != null ? prod.inventory.currentQuantity : 0,
                    lineTotal: 0,
                    taxMethod: this.taxMethod,
                    taxRateId: rateId,
                    rate: rate,
                    unitPerPack: prod.unitPerPack,
                    levelOneUnit: prod.levelOneUnit,
                    basicUnit: prod.basicUnit,
                });

                this.product.id = "";
                var product = this.purchaseProducts.find((x) => {
                    return x.productId == productId;
                });
                this.updateLineTotal(product.price, "unitPrice", product);
                this.rendered++;
            },
            createUUID: function () {
                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },


            removeProduct: function (id) {

                this.purchaseProducts = this.purchaseProducts.filter((prod) => {
                    return prod.rowId != id;
                });

                this.calcuateSummary();
            },

            getData: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                if (root.formName == 'StockIn' && root.$route.query.data != undefined)
                    root.loading = true;
                root.$https
                    .get("/Product/TaxRateList", {
                        headers: { Authorization: `Bearer ${token}` },
                    }).then(function (response) {
                        if (response.data != null) {
                            root.vats = response.data.taxRates;
                        }
                    }).then(function () {

                        if (root.$route.query.data != undefined) {
                            if (root.taxRateId != '' && root.taxRateId != null) {
                                if (root.$route.query.data != undefined) {
                                    root.$route.query.data.stockAdjustmentDetails.forEach(function (item) {
                                        root.purchaseProducts.push({
                                            rowId: item.id,
                                            id: item.id,
                                            product: item.product,
                                            batchNo: item.batchNo,
                                            batchExpiry: item.batchExpiry,
                                            currentQuantity: item.product.inventory != null ? item.product.inventory.currentQuantity : 0,
                                            productId: item.productId,
                                            stockAdjustmentId: item.stockAdjustmentId,
                                            quantity: item.quantity,
                                            highQty: item.highQty,
                                            totalPiece: item.totalPiece,
                                            salePrice: item.salePrice,
                                            price: item.price,
                                            rate: root.vats.find((value) => value.id == root.taxRateId).rate,
                                            taxMethod: root.taxMethod,
                                            taxRateId: root.taxRateId,
                                            unitPerPack: item.product.unitPerPack,
                                            levelOneUnit: item.product.levelOneUnit,
                                            basicUnit: item.product.basicUnit,
                                        });
                                    });
                                }
                            }
                            else {
                                if (root.$route.query.data != undefined) {
                                    root.$route.query.data.stockAdjustmentDetails.forEach(function (item) {
                                        root.purchaseProducts.push({
                                            rowId: item.id,
                                            id: item.id,
                                            product: item.product,
                                            batchNo: item.batchNo,
                                            batchExpiry: item.batchExpiry,
                                            currentQuantity: item.product.inventory != null ? item.product.inventory.currentQuantity : 0,
                                            productId: item.productId,
                                            stockAdjustmentId: item.stockAdjustmentId,
                                            quantity: item.quantity,
                                            highQty: item.highQty,
                                            totalPiece: item.totalPiece,
                                            salePrice: item.salePrice,
                                            price: item.price,
                                            rate: 0,
                                            taxMethod: root.taxMethod,
                                            taxRateId: root.taxRateId,
                                            unitPerPack: item.product.unitPerPack,
                                            levelOneUnit: item.product.levelOneUnit,
                                            basicUnit: item.product.basicUnit,
                                        });
                                    });

                                }
                            }
                            
                            for (var s = 0; s < root.purchaseProducts.length; s++) {
                                root.products.push(root.purchaseProducts[s].product);
                                root.updateLineTotal(root.purchaseProducts[s].quantity, "quantity", root.purchaseProducts[s]);
                                if (root.isMultiUnit == 'true') {
                                    root.updateLineTotal(root.purchaseProducts[s].highQty, "highQty", root.purchaseProducts[s]);
                                }

                                root.updateLineTotal(root.purchaseProducts[s].price, "price", root.purchaseProducts[s]);
                            }
                            root.calcuateSummary()
                            root.loading = false
                        }
                    });
            },
        },
        created: function () {
            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            this.$barcodeScanner.init(this.onBarcodeScanned);
            this.getData();
        },
        mounted: function () {
            this.GetProductList();
            this.decimalQuantity = localStorage.getItem('decimalQuantity') == 'true' ? true : false;
            this.currency = localStorage.getItem('currency');
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
        },
    };
</script>
