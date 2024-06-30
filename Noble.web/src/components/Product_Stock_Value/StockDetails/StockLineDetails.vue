<template>
    <div class="col-lg-12">
        <div class=" table-responsive mt-3">
            <table class="table mb-0" style="table-layout:fixed;">
                <thead class="thead-light">
                    <tr>
                        <th style="width: 30px;">
                            #
                        </th>
                        <th style="width: 150px;">
                            {{ $t('StockLineDetails.ProductName') }}
                        </th>
                        <th style="width: 100px;" class="text-center">
                            {{ $t('StockLineDetails.UnitPrice') }}
                        </th>
                        <th style="width: 70px;" class="text-center">
                            {{ $t('StockLineDetails.salePrice') }}
                        </th>

                        <th class="text-center" style="width: 90px;" v-if="isValid('CanViewCurrentQuantity') && formName=='StockOut'">
                            {{ $t('SaleItem.CurrentQuantity') }}
                        </th>

                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('StockLineDetails.HighQty') }}
                        </th>
                        <th style="width: 100px;" class="text-center">
                            {{ $t('StockLineDetails.Quantity') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('StockLineDetails.TOTALQTY') }}
                        </th>

                        <th class="text-center" style="width: 100px;" v-if="isSerial && formName=='StockOut'">
                            {{ $t('StockLineDetails.Serial') }}
                        </th>
                        <th style="width: 100px;" v-if="isSerial && formName=='StockOut'">
                            {{ $t('StockLineDetails.Guarantee') }}
                        </th>

                        <th style="width:150px;" class="text-center" v-if="purchaseProducts.filter(x => x.guarantee).length > 0  && isSerial && formName=='StockIn'">
                            {{ $t('StockLineDetails.WarrantyType') }}
                        </th>
                        <th style="width: 100px;" v-if="purchaseProducts.filter(x => x.guarantee).length > 0  && isSerial && formName=='StockIn'">
                            {{ $t('StockLineDetails.Guarantee') }}
                        </th>
                        <th style="width: 100px;" class="text-center" v-if="purchaseProducts.filter(x => x.isSerial).length > 0  && isSerial && formName=='StockIn'">
                            {{ $t('StockLineDetails.Serial') }}
                        </th>
                        <th style="width:110px;" class="text-center" v-if="isFifo">
                            {{ $t('StockLineDetails.BatchNo') }}
                        </th>
                        <th style="width:110px;" class="text-center" v-if="isFifo">
                            {{ $t('StockLineDetails.ExpDate') }}
                        </th>
                        <th style="width: 100px;" class="arabicLanguage">
                            {{ $t('StockLineDetails.Total') }}
                        </th>
                        <th style="width: 40px" class="text-end"></th>
                    </tr>
                </thead>
                <tbody>

                    <tr v-for="(prod , index) in purchaseProducts" :key="prod.productId + index" v-bind:class="{'alert-danger':prod.outOfStock}">
                        <td class="border-top-0">
                            {{index+1}}
                        </td>
                        <td class="border-top-0">
                            {{ products.find(x => x.id == prod.productId).displayName }}
                        </td>

                        <td class="border-top-0">
                            <!--<input type="number" v-model="prod.price"
                            @focus="$event.target.select()"
                            class="form-control input-border text-center"
                            @blur="CheckSalePrice(prod.price,prod.productId)"
                            @keyup="updateLineTotal($event.target.value, 'price', prod)" />-->
                            <decimal-to-fixed v-model="prod.price" v-bind:salePriceCheck="true" @update:modelValue="updateLineTotal(prod.price, 'price', prod)" />
                        </td>
                        <td class="border-top-0 text-center">
                            {{currency}} {{parseFloat(prod.salePrice).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                        </td>
                        <td class="border-top-0 text-center" v-if="isValid('CanViewCurrentQuantity') && formName=='StockOut'">
                            {{prod.currentQuantity}}
                        </td>

                        <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                            <decimal-to-fixed v-bind:salePriceCheck="false" v-model="prod.highQty"
                                              :isQunatity="true"
                                              @update:modelValue="updateLineTotal(prod.highQty, 'highQty', prod)" />
                            <small style="font-weight: 500;font-size:70%;">
                                {{prod.levelOneUnit}}
                            </small>
                        </td>
                        <td class="border-top-0 text-center">
                            <decimal-to-fixed v-bind:salePriceCheck="false" v-model="prod.quantity"
                                              :isQunatity="true"
                                              @update:modelValue="updateLineTotal(prod.quantity, 'quantity', prod)" />
                            <small v-if="isMultiUnit=='true'" style="font-weight: 500;font-size:70%;">
                                {{prod.basicUnit}}
                            </small>
                        </td>
                        <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                            {{prod.totalPiece}}
                        </td>

                        <td class="text-center border-top-0" v-if="isSerial && formName=='StockOut'">
                            <button @click="AddSerial(prod)" v-if="prod.isSerial" title="Add Serial" class="btn btn-primary btn-sm"> Add Serial </button>
                            <span v-else>-</span>
                        </td>
                        <td class="border-top-0  text-center" v-if="isSerial && formName=='StockOut'">
                            <datepicker v-model="prod.guaranteeDate" v-if="prod.guarantee" />
                            <span v-else>-</span>
                        </td>

                        <td class="border-top-0 text-center" v-if="purchaseProducts.filter(x => x.guarantee).length > 0 && isSerial && formName=='StockIn'">
                            <warranty-type-dropdown v-if="prod.guarantee" v-model="prod.warrantyTypeId" :modelValue="prod.warrantyTypeId" />
                            <span v-else>
                                -
                            </span>
                        </td>

                        <td class="border-top-0  text-center" v-if="purchaseProducts.filter(x => x.guarantee).length > 0 && isSerial && formName=='StockIn'">
                            <datepicker v-if="prod.guarantee" v-model="prod.guaranteeDate" />
                            <span v-else>
                                -
                            </span>
                        </td>
                        <td class="border-top-0 text-center" v-if="purchaseProducts.filter(x => x.isSerial).length > 0  && isSerial && formName=='StockIn'">
                            <input type="text"
                                   v-model="prod.serialNo"
                                   v-if="prod.isSerial"
                                   @focus="$event.target.select()"
                                   class="form-control input-border text-center " />
                            <span v-else>
                                -
                            </span>
                        </td>



                        <td class="border-top-0 text-center" v-if="formName!='StockIn' && isFifo">
                            {{prod.batchNo}}

                        </td>

                        <td class="border-top-0 text-center" v-if="formName=='StockIn' && isFifo">
                            <input type="text" v-model="prod.batchNo"
                                   @focus="$event.target.select()"
                                   class="form-control input-border text-center " />

                        </td>
                        <td class="border-top-0 text-center" v-if="formName=='StockIn' && isFifo">
                            <datepicker v-model="prod.batchExpiry" />

                        </td>
                        <td class="border-top-0 text-center" v-if="formName!='StockIn' && isFifo">
                            {{getDate(prod.batchExpiry)}}

                        </td>

                        <td class="border-top-0 arabicLanguage">
                            {{currency}}  {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0,-1))  }}
                        </td>
                        <td class="border-top-0 pt-0 text-end">
                            <button @click="removeProduct(prod.rowId)"
                                    title="Remove Item"
                                    class="btn btn-sm">
                                <i class="las la-trash-alt text-secondary font-16"></i>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>


        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="mt-4">
                    <product-dropdown @update:modelValues="addProduct" width="100%" :wareHouseId="formName=='StockOut'? wareHouseId : undefined" :fromStockOut="true" />
                </div>

            </div>
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 ">
                <div class="mt-4" v-bind:key="rendered + 'g'">
                    <table class="table" style="background-color: #f1f5fa;">
                        <tbody>
                            <tr>
                                <td colspan="2" style="width:65%;" class="fw-bold">
                                    <span>   {{ $t('StockLineDetails.NoItem') }} </span>
                                </td>
                                <td class="text-end" style="width:35%;"> {{ summary.item }}</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:65%;" class="fw-bold">
                                    <span>    {{ $t('StockLineDetails.TotalQty') }}</span>
                                </td>
                                <td class="text-end" style="width:35%;"> {{ summary.qty }}</td>
                            </tr>

                            <tr>
                                <td colspan="2" style="width:65%;" class="fw-bold">
                                    <span style="height:33px !important; ">{{ $t('StockLineDetails.TotalVAT') }}</span>

                                </td>
                                <td style="width:35%;" class="fw-bold">
                                    <div class="text-end">
                                        {{currency}}  {{ (parseFloat(summary.vat)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2" style="width:65%;">
                                    <span style="font-weight:bolder; font-size:16px">{{ $t('StockLineDetails.TotalDue') }}({{currency}})</span>
                                </td>
                                <td class="text-end" style="width: 35%; font-weight: bolder; font-size: 16px">{{summary.total}}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

        </div>

        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>

</template>


<script>
    import moment from "moment";
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        name: "StockLineItem",
        mixins: [clickMixin],
        props: ['purchase', 'purchaseItems', 'taxMethod', 'taxRateId', 'formName', 'wareHouseId'],
        components: {
            Loading
        },
        data: function () {
            return {
                isSerial: false,
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
                productList: [],
                serialItem: '',
                showSerial: false
            };
        },
        validations() { },
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
                return moment(x).format("l");
            },
            AddSerial: function (item) {

                this.serialItem = item;
                this.showSerial = true;
            },

            updateSerial: function (serial, item) {

                var prod = this.purchaseProducts.find(x => x.rowId == item.rowId);
                if (prod != undefined) {
                    prod.serial = serial;
                }
                this.showSerial = false;
            },

            //GetProductList: function () {

            //    var root = this;
            //    var token = "";
            //    if (token == '') {
            //        token = localStorage.getItem("token");
            //    }


            //    // var url = this.wareHouseId != undefined ? "/Product/GetProductInformation?searchTerm=" + search + '&wareHouseId=' + this.wareHouseId + "&isDropdown=true" + '&isRaw=' + root.isRaw : "/Product/GetProductInformation?searchTerm=" + search + '&status=' + root.status + "&isDropdown=true" + '&isRaw=' + root.isRaw;
            //    var url = ''
            //    if (this.formName == 'StockOut') {
            //        url = "/Product/GetProductBarcode?wareHouseId=" + this.wareHouseId + '&isRaw=' + false
            //    }
            //    else {
            //        url = "/Product/GetProductBarcode?isRaw=" + false
            //    }
            //    this.$https
            //        .get(url, {
            //            headers: { Authorization: `Bearer ${token}` },
            //        })
            //        .then(function (response) {
            //            if (response.data != null) {
            //                root.productList = response.data.results.products;

            //            }
            //        });


            //},
            //onBarcodeScanned(barcode) {
            //    if (localStorage.getItem("BarcodeScan") != 'StockLine')
            //        return
            //    var root = this;

            //    if (root.productList.length > 0) {
            //        var product = this.productList.find(x => x.barCode == barcode)
            //        if (product != null) {
            //            root.addProduct(product.id, product)
            //        }
            //    }

            //},
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
                    this.summary.qty = this.purchaseProducts.reduce((qty, prod) => qty + parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                    this.summary.qty = parseFloat(this.summary.qty).toFixed(2);
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

            updateBatch: function (productId, batch) {

                var prd = this.purchaseProducts.find(x => x.productId == productId);
                if (prd != undefined) {
                    prd.batchNo = batch.batchNumber;
                    prd.batchExpiry = batch.expiryDate;
                }
                this.updateLineTotal(prd.quantity, "quantity", prd);
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
                var prod = '';
                var uid = this.createUUID();
                if (this.formName != 'StockIn' && this.isFifo) {
                    var inventoryData = {
                        autoNumbering: newProduct.inventory.autoNumbering,
                        averagePrice: newProduct.inventory.averagePrice,
                        batchNumber: newProduct.inventory.batchNumber,
                        bundleId: newProduct.inventory.bundleId,
                        buy: newProduct.inventory.buy,
                        currentQuantity: newProduct.inventory.currentQuantity,
                        currentStockValue: newProduct.inventory.currentStockValue,
                        date: newProduct.inventory.date,
                        documentId: newProduct.inventory.documentId,
                        documentNumber: newProduct.inventory.documentNumber,
                        expiryDate: newProduct.inventory.expiryDate,
                        get: newProduct.inventory.get,
                        id: newProduct.inventory.id,
                        isActive: newProduct.inventory.isActive,
                        isOpen: newProduct.inventory.isOpen,
                        price: newProduct.inventory.price,
                        product: newProduct.inventory.product,

                        productId: newProduct.inventory.productId,
                        promotionId: newProduct.inventory.promotionId,
                        quantity: newProduct.inventory.quantity,
                        remainingQuantity: newProduct.inventory.remainingQuantity,
                        salePrice: newProduct.inventory.salePrice,
                        serial: newProduct.inventory.serial,
                        stock: newProduct.inventory.stock,
                        stockId: newProduct.inventory.stockId,
                        transactionType: newProduct.inventory.transactionType,
                        wareHouseId: newProduct.inventory.wareHouseId,
                        warrantyDate: newProduct.inventory.warrantyDate,

                    };

                    this.products.push({
                        rowId: uid,
                        arabicName: newProduct.arabicName,
                        assortment: newProduct.assortment,
                        barCode: newProduct.barCode,
                        basicUnit: newProduct.basicUnit,
                        batchExpiry: newProduct.batchExpiry,
                        batchNo: newProduct.batchNo,
                        brandId: newProduct.brandId,
                        bundleCategory: newProduct.bundleCategory,
                        category: newProduct.category,
                        categoryId: newProduct.categoryId,
                        code: newProduct.code,
                        colorId: newProduct.colorId,
                        colorName: newProduct.colorName,

                        colorNameArabic: newProduct.colorNameArabic,
                        currentQuantity: newProduct.currentQuantity,
                        description: newProduct.description,
                        englishName: newProduct.englishName,
                        guarantee: newProduct.guarantee,
                        id: newProduct.id,
                        image: newProduct.image,
                        inventory: inventoryData,
                        inventoryBatch: newProduct.inventoryBatch,
                        isActive: newProduct.isActive,
                        isExpire: newProduct.isExpire,
                        isRaw: newProduct.isRaw,

                        length: newProduct.length,
                        levelOneUnit: newProduct.levelOneUnit,
                        originId: newProduct.originId,
                        promotionOffer: newProduct.promotionOffer,
                        purchasePrice: newProduct.purchasePrice,
                        salePrice: newProduct.salePrice,
                        salePriceUnit: newProduct.salePriceUnit,
                        saleReturnDays: newProduct.saleReturnDays,
                        serial: newProduct.serial,
                        serviceItem: newProduct.serviceItem,

                        shelf: newProduct.shelf,
                        sizeId: newProduct.sizeId,
                        sizeName: newProduct.sizeName,
                        sizeNameArabic: newProduct.sizeNameArabic,
                        stockLevel: newProduct.stockLevel,
                        styleNumber: newProduct.styleNumber,
                        subCategoryId: newProduct.subCategoryId,
                        taxMethod: newProduct.taxMethod,
                        taxRate: newProduct.taxRate,
                        taxRateId: newProduct.taxRateId,
                        taxRateValue: newProduct.taxRateValue,
                        unit: newProduct.unit,
                        unitId: newProduct.unitId,

                        unitPerPack: newProduct.unitPerPack,
                        width: newProduct.width,
                    });

                    prod = this.products.find((x) => x.rowId == uid);


                }
                else {
                    if (this.products.find(x => x.id == newProduct.id) == undefined || this.products.length <= 0) {
                        this.products.push(newProduct);
                    }
                    prod = this.products.find((x) => x.id == productId);
                }

                //var prod = this.products.find((x) => x.rowId == uid);
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
                if (this.formName == 'StockIn') {
                    this.purchaseProducts.push({
                        rowId: this.createUUID(),
                        productId: prod.id,
                        price: 0,
                        salePrice: 0,
                        quantity: 0,
                        highQty: 0,
                        totalPiece: 1,
                        currentQuantity: 0,
                        lineTotal: 0,
                        taxMethod: this.taxMethod,
                        taxRateId: rateId,
                        rate: rate,
                        isExpire: false,
                        batchNo: "",
                        batchExpiry: "",
                        //inventoryList: prod.inventoryBatch == null ? null : prod.inventoryBatch,
                        unitPerPack: 0,
                        levelOneUnit: '',
                        basicUnit: '',
                        serial: '',
                        guaranteeDate: '',

                        guarantee: false,
                        isSerial: false,
                        serialNo: '',
                        warrantyTypeId: '',
                    });
                }
                else {
                    this.purchaseProducts.push({
                        rowId: uid,
                        productId: prod.id,
                        price:  0,
                        salePrice: 0,
                        quantity: 0,
                        highQty: 0,
                        totalPiece: 1,
                        currentQuantity: 0,
                        lineTotal: 0,
                        taxMethod: this.taxMethod,
                        taxRateId: rateId,
                        rate: rate,
                        isExpire: false,
                        //batchNo: prod.batchNo,
                        //batchExpiry: prod.batchExpiry,
                        //inventoryList: prod.inventoryBatch == null ? null : prod.inventoryBatch,
                        unitPerPack: 0,
                        levelOneUnit: '',
                        basicUnit: '',
                        serial: '',
                        guaranteeDate: '',

                        guarantee: false,
                        isSerial: false,
                        serialNo: '',
                        warrantyTypeId: '',
                    });
                }

                this.GetProductInfo(productId);

                this.product.id = "";
                var product = this.purchaseProducts.find((x) => {
                    return x.productId == productId && x.rowId == uid;
                });
                if (product != undefined) {
                    this.updateLineTotal(product.price, "unitPrice", product);
                }

                this.rendered++;
            },

            GetProductInfo: function (id) {
                var root = this;
                var token = localStorage.getItem('token');

                var openBatch;
                var batch = localStorage.getItem('openBatch')
                if (batch != undefined && batch != null && batch != "") {
                    openBatch = batch
                }
                else {
                    openBatch = 1
                }

                root.$https.get('/Product/GetProductDetailForInvoiceQuery?id=' + id + "&isFifo=" + root.isFifo + "&openBatch=" + openBatch + '&branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        var newProduct = response.data;
                        var prod = root.purchaseProducts.find((x) => x.productId == id);

                        if (prod != undefined) {


                            prod.price = prod.inventory != null ? prod.inventory.price : 0;
                            prod.salePrice = newProduct.salePrice;
                            prod.inventory = newProduct.currentQuantity;
                            prod.isExpire = newProduct.isExpire;
                            prod.unitPerPack = newProduct.unitPerPack;
                            prod.levelOneUnit = newProduct.levelOneUnit;
                            prod.basicUnit = newProduct.basicUnit;
                            prod.guarantee = newProduct.guarantee;
                            prod.serial = newProduct.serial;


                            root.updateLineTotal(prod.quantity, "quantity", prod);
                        }
                    }
                });
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

            //getDate: function (x) {

            //    if (x != null && x != undefined) {
            //        return moment(x).format("l");
            //    }
            //},

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
                                            inventoryList: item.product.inventoryBatch == null ? null : item.product.inventoryBatch,
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
                                            serial: item.serial,
                                            guaranteeDate: item.guaranteeDate,
                                            warrantyTypeId: item.warrantyTypeId,
                                            serialNo: item.serialNo,
                                            guarantee: item.product.guarantee,
                                            isSerial: item.product.serial,
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
                                            inventoryList: item.product.inventoryBatch == null ? null : item.product.inventoryBatch,
                                            currentQuantity: item.product.inventory != null ? item.product.inventory.currentQuantity : 0,
                                            productId: item.productId,
                                            stockAdjustmentId: item.stockAdjustmentId,
                                            quantity: item.quantity,
                                            highQty: item.highQty,
                                            totalPiece: item.totalPiece,
                                            salePrice: item.salePrice,
                                            price: item.price,
                                            serial: item.serial,
                                            guaranteeDate: item.guaranteeDate,
                                            warrantyTypeId: item.warrantyTypeId,
                                            serialNo: item.serialNo,
                                            rate: 0,
                                            taxMethod: root.taxMethod,
                                            taxRateId: root.taxRateId,
                                            unitPerPack: item.product.unitPerPack,
                                            levelOneUnit: item.product.levelOneUnit,
                                            basicUnit: item.product.basicUnit,
                                            guarantee: item.product.guarantee,
                                            isSerial: item.product.serial,
                                            isExpire: item.product.isExpire,
                                            batchNo: item.batchNo,
                                            batchExpiry: item.batchExpiry
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
            //this.$barcodeScanner.init(this.onBarcodeScanned);
            //For Scanner Code
            // var root = this;
            //var barcode = '';
            //var interval;
            //document.addEventListener('keydown', function (evt) {
            //    if (interval)
            //        clearInterval(interval);
            //    if (evt.code === 'Enter') {
            //        if (barcode) {
            //            root.onBarcodeScanned(barcode);
            //        }
            //        barcode = '';
            //        return;

            //    }
            //    if (evt.key !== 'Shift')
            //        barcode += evt.key;
            //});
            localStorage.setItem("BarcodeScan", 'StockLine')
            //End
            //this.$barcodeScanner.init(this.onBarcodeScanned);
            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            this.getData();
        },
        mounted: function () {
            //this.GetProductList();
            this.decimalQuantity = localStorage.getItem('decimalQuantity') == 'true' ? true : false;
            this.currency = localStorage.getItem('currency');
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            this.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;
        },
    };
</script>


