<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
        <div class=" table-responsive">
            <table class="table table-striped table-hover add_table_list_bg" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                <thead style="background-color: #F4F6FC;">
                    <tr>
                        <th style="width: 30px;">
                            #
                        </th>
                        <th style="width: 200px;">
                            {{ $t('QuotationViewItem.Product') }}
                        </th>
                        <th style="width: 110px;" class="text-center">
                            {{ $t('QuotationViewItem.UnitPrice') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('QuotationViewItem.HighQty') }}
                        </th>
                        <th style="width: 110px;" class="text-center">
                            {{ $t('QuotationViewItem.Qty') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('QuotationViewItem.TOTALQTY') }}
                        </th>
                        <!--<th style="width:110px;" class="text-center" v-if="purchaseProducts.filter(x => x.isExpire).length > 0">
                            {{ $t('QuotationViewItem.ExpDate') }}
                        </th>
                        <th style="width:110px;" class="text-center" v-if="purchaseProducts.filter(x => x.isExpire).length > 0">
                            {{ $t('QuotationViewItem.BatchNo') }}
                        </th>-->
                        <th style="width: 100px;" class="text-center">
                            {{ $t('QuotationViewItem.Disc%') }}
                        </th>
                        <th style="width: 100px;" class="text-center">
                            {{ $t('QuotationViewItem.FixDisc') }}
                        </th>
                        <th style="width: 100px;" class="text-center">
                            {{ $t('QuotationViewItem.VAT%') }}
                        </th>
                        <th style="width: 100px;" class="text-right">
                            {{ $t('QuotationViewItem.LineTotal') }}
                        </th>
                    </tr>
                </thead>
                <tbody id="purchase-item">

                        <tr v-for="(prod , index) in purchaseProducts" :key="prod.productId + index" style="border-bottom: 1px solid #d6d6d6; ">
                            <td class="border-top-0">
                                {{index+1}}
                            </td>
                            <td class="border-top-0">

                                {{($i18n.locale == 'en' ||isLeftToRight())? products.find(x => x.id == prod.productId).englishName!=''? products.find(x => x.id == prod.productId).englishName : products.find(x => x.id == prod.productId).arabicName :    products.find(x => x.id == prod.productId).arabicName!=''? products.find(x => x.id == prod.productId).arabicName : products.find(x => x.id == prod.productId).englishName}}
                            </td>

                            <td class="border-top-0 text-center">
                                {{currency}}  {{ parseFloat(prod.unitPrice).toFixed(3).slice(0,-1)}}
                            </td>
                            <th class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                                {{prod.highQty }}
                            </th>
                            <td class="border-top-0 text-center">
                                {{prod.quantity}}
                            </td>
                            <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                                {{prod.totalPiece}}
                            </td>
                            <!--<td class="border-top-0 text-center" v-if="purchaseProducts.filter(x => x.isExpire).length > 0">
                                {{prod.expiryDate}}
                            </td>
                            <td class="border-top-0 text-center" v-if="purchaseProducts.filter(x => x.isExpire).length > 0">
                                {{prod.batchNo}}
                            </td>-->
                            <td class="border-top-0 text-center">
                                {{ parseFloat(prod.discount).toFixed(3).slice(0,-1)}}%
                            </td>
                            <td class="border-top-0 text-center">
                                {{currency}}  {{ parseFloat(prod.fixDiscount).toFixed(3).slice(0,-1)}}
                            </td>
                            <td class="border-top-0 text-center">
                                {{prod.taxRate}}%
                            </td>
                            <td class="border-top-0 text-right">
                                {{currency}}  {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0,-1))  }}
                            </td>
                        </tr>
                </tbody>
            </table>
        </div>

        <div class=" table-responsive mt-3"
             v-bind:key="rendered + 'g'" style=" background-color: #f2f2f2;">
            <table class="table table-striped table-hover add_table_list_bg">
                <thead class="m-0">
                    <tr class="text-right">
                        <th class="text-center" style="width:85px;">
                            {{ $t('QuotationViewItem.NoItem') }}
                        </th>
                        <th class="text-center" style="width:100px;" v-if="isMultiUnit=='true'">
                            {{ $t('QuotationViewItem.TotalCarton') }}
                        </th>
                        <th class="text-center" style="width:100px;" v-if="isMultiUnit=='true'">
                            {{ $t('QuotationViewItem.TotalPieces') }}
                        </th>
                        <th class="text-center" style="width:100px;">
                            {{ $t('QuotationViewItem.TotalQty') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('QuotationViewItem.Total') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('QuotationViewItem.Disc') }}
                        </th>
                        <th style="width:155px;">
                            {{ $t('QuotationViewItem.TotalAfterDisc') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('QuotationViewItem.TotalVAT') }}
                        </th>
                        <th style="width:140px;">
                            {{ $t('QuotationViewItem.TotalwithVAT') }}
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right">
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

                        <td>
                            {{currency}}   {{ parseFloat(summary.total).toFixed(3).slice(0,-1)}}
                        </td>
                        <td>
                            {{currency}} {{  parseFloat(summary.discount).toFixed(3).slice(0,-1)}}
                        </td>
                        <td>
                            {{currency}}   {{  parseFloat(summary.withDisc).toFixed(3).slice(0,-1)}}
                        </td>
                        <td>
                            {{currency}}  {{ (parseFloat(summary.vat)+summary.inclusiveVat).toFixed(3).slice(0,-1) }}
                        </td>
                        <td>
                            {{currency}}   {{  parseFloat(summary.withVAt).toFixed(3).slice(0,-1)}}
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<style scoped>
    #sale-item td {
        padding-bottom: 0px;
        padding-top: 0px;
    }

    .input-border {
        border: transparent;
        background-color: transparent !important;
    }

        .input-border:focus {
            outline: none !important;
            border: none !important;
        }

    .multiselect__tags {
        background-color: transparent !important;
    }

    .multiselect__input, .multiselect__single {
        background-color: transparent !important;
    }

    .tableHoverOn {
        background-color: #F4F6FC !important;
    }
</style>

<script>
    //
    //
    //import moment from "moment";
    //import Vue3Barcode from 'vue3-barcode'
    export default {
        name: "PurchaseItem",
        props: ['purchase', 'purchaseItems'],

        data: function () {
            return {
                rendered: 0,
                product: {
                    id: "",
                },
                products: [],
                purchaseProducts: [],
                loading: false,
                vats: [],
                isMultiUnit: '',
                summary: {
                    item: 0,
                    qty: 0,
                    total: 0,
                    discount: 0,
                    withDisc: 0,
                    vat: 0,
                    withVAt: 0,
                    inclusiveVat: 0,
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
        methods: {
            GetProductList: function () {

                var root = this;
                var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiOGU2MTI1NzYtMDNhNy00MDk0LTg2ZWEtNjAwODViY2E5OTk5IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjU3MDk3NTE2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.IjtMbckhrVhVabG1D-DhjTtidoDRPXxw-qCtL0yKhLY'

                this.isRaw = this.raw == undefined ? false : this.raw;
                //search = search == undefined ? '' : search;
                // var url = this.wareHouseId != undefined ? "/Product/GetProductInformation?searchTerm=" + search + '&wareHouseId=' + this.wareHouseId + "&isDropdown=true" + '&isRaw=' + root.isRaw : "/Product/GetProductInformation?searchTerm=" + search + '&status=' + root.status + "&isDropdown=true" + '&isRaw=' + root.isRaw;

                this.$https
                    .get("/Product/GetProductBarcode?isEmail=" + true, {
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

            changeProduct: function (NewProdId, rowId) {
                this.purchaseProducts = this.purchaseProducts.filter(x => x.rowId != rowId);
                this.addProduct(NewProdId);

            },
            calcuateSummary: function () {

                var root = this;
                this.summary.item = this.purchaseProducts.length;
                this.summary.totalPieces = this.purchaseProducts.reduce((totalQty, prod) => totalQty + prod.quantity, 0);
                this.summary.totalCarton = this.purchaseProducts.reduce((totalCarton, prod) => totalCarton + prod.highQty, 0);
                this.summary.qty = this.purchaseProducts.reduce(
                    (qty, prod) => qty + parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                this.summary.total = this.purchaseProducts
                    .reduce((total, prod) => total + parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece) * prod.unitPrice, 0)
                    .toFixed(3).slice(0, -1);
                var discount = this.purchaseProducts
                    .filter((x) => x.discount != 0 || x.discount != "")
                    .reduce(
                        (discount, prod) =>
                            discount + ((prod.totalPiece == '' ? 0 : prod.totalPiece) * prod.unitPrice * prod.discount) / 100,
                        0
                    );
                var fixDiscount = this.purchaseProducts
                    .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "")
                    .reduce((discount, prod) => discount + prod.fixDiscount, 0);
                this.summary.discount = (parseFloat(discount) + parseFloat(fixDiscount)).toFixed(3).slice(0, -1);
                this.summary.withDisc = (this.summary.total - this.summary.discount).toFixed(
                    2
                );
                this.summary.vat = this.purchaseProducts
                    .reduce((vat, prod) => parseFloat(vat) + ((prod.taxMethod == "Exclusive" || prod.taxMethod == "غير شامل") ? ((((parseFloat(prod.unitPrice) * parseFloat(prod.quantity == '' ? 0 : prod.quantity)) -

                        ((prod.fixDiscount != 0 || prod.fixDiscount != "" ?
                            prod.fixDiscount :
                            ((prod.totalPiece == '' ? 0 : prod.totalPiece) * prod.unitPrice * prod.discount) / 100)))

                        * parseFloat(root.getVatValueForSummary(prod.taxRateId, prod))) / 100) : 0),
                        0
                    )
                    .toFixed(3).slice(0, -1);

                this.summary.inclusiveVat = this.purchaseProducts
                    .reduce((vat, prod) => parseFloat(vat) + ((prod.taxMethod == "Inclusive" || prod.taxMethod == "شامل") ? ((((parseFloat(prod.unitPrice) * parseFloat(prod.quantity == '' ? 0 : prod.quantity)) -

                        ((prod.fixDiscount != 0 || prod.fixDiscount != "" ?
                            prod.fixDiscount :
                            ((prod.totalPiece == '' ? 0 : prod.totalPiece) * prod.unitPrice * prod.discount) / 100)))

                        * parseFloat(root.getVatValueForSummary(prod.taxRateId, prod))) / (100 + prod.rate)) : 0),
                        0
                    );
                //this.summary.withVAt = (
                //    parseFloat(parseFloat(this.summary.withDisc)) + parseFloat(parseFloat(this.summary.vat))
                //).toFixed(3).slice(0,-1);

                this.summary.withVAt = (parseFloat(this.summary.withDisc) + parseFloat(this.summary.vat)).toFixed(3).slice(0, -1);

                this.$emit("update:modelValue", this.purchaseProducts);
            },

            updateLineTotal: function (e, prop, product) {


                var discount = product.discount == 0 || product.discount == "" ? product.fixDiscount == 0 || product.fixDiscount == "" ? 0 : product.fixDiscount : product.discount;

                if (prop == "unitPrice") {
                    if (e < 0) {
                        e = 0;
                    }
                    product.unitPrice = e;
                }

                if (prop == "quantity") {
                    if (e <= 0 || e == '') {
                        e = '';
                    }
                    product.quantity = e;
                }

                if (prop == "discount") {
                    if (e == "") {
                        e = 0;
                    }
                    product.discount = e;

                }

                if (prop == "fixDiscount") {
                    if (e == "") {
                        e = 0;
                    }
                    product.fixDiscount = e;
                }
                if (prop == "highQty") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = 0;
                    }
                    product.highQty = e;
                }

                product.totalPiece = (parseFloat(product.highQty == undefined ? 0 : product.highQty) * parseFloat(product.unitPerPack == null ? 0 : product.unitPerPack)) + parseFloat(product.quantity == '' ? 0 : product.quantity);

                discount = product.discount == 0 ? (product.fixDiscount * product.totalPiece) : (product.totalPiece * product.unitPrice * product.discount) / 100;
                var vat = this.vats.find((value) => value.id == product.taxRateId);

                var total = (product.totalPiece == '' ? 0 : product.totalPiece) * product.unitPrice - discount;
                var calculateVAt = 0;
                if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                    //calculateVAt = (total * vat.rate) / (100 + vat.rate);
                    //product.lineTotal = total - calculateVAt;
                    product.lineTotal = total;
                }
                else {
                    calculateVAt = (total * vat.rate) / 100;
                    product.lineTotal = total + calculateVAt;
                }
                this.purchaseProducts['product'] = product;

                this.calcuateSummary();

                this.$emit("update:modelValue", this.purchaseProducts);

            },

            addProduct: function (productId, newProduct) {

                var prd = this.purchaseProducts.find(x => x.productId == productId);
                if (prd != undefined) {
                    prd.quantity++;
                    this.updateLineTotal(prd.quantity, "quantity", prd);
                }
                else {
                    this.products.push(newProduct);
                    var prod = this.products.find((x) => x.id == productId);

                    var rate = 0;
                    if (prod.taxRateId != "00000000-0000-0000-0000-000000000000" && prod.taxRateId != undefined) {
                        rate = this.getVatValue(prod.taxRateId, prod);
                    }

                    this.purchaseProducts.push({
                        rowId: this.createUUID(),
                        productId: prod.id,
                        unitPrice: prod.salePrice,
                        quantity: 1,
                        highQty: 0,
                        discount: 0,
                        fixDiscount: 0,
                        taxRateId: prod.taxRateId,
                        rate: rate,
                        taxMethod: prod.taxMethod,
                        expiryDate: "",
                        isExpire: prod.isExpire,
                        batchNo: "",
                        lineTotal: 0,
                        unitPerPack: newProduct.unitPerPack,
                        levelOneUnit: prod.levelOneUnit,
                        basicUnit: prod.basicUnit,
                    });

                    var product = this.purchaseProducts.find((x) => {
                        return x.productId == productId;
                    });

                    this.getVatValue(product.taxRateId, product);

                    this.product.id = "";
                    /*this.rendered++;*/
                }
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
            getVatValue: function (id, prod) {

                var vat = this.vats.find((value) => value.id == id);
                prod.taxRateId = id;
                prod.rate = vat.rate;
                this.updateLineTotal(prod.unitPrice, "unitPrice", prod);
                return vat.rate;
            },
            getVatValueForSummary: function (id, prod) {

                var vat = this.vats.find((value) => value.id == id);
                prod.taxRateId = id;
                prod.rate = vat.rate;
                return vat.rate;
            },
            removeProduct: function (id) {

                this.purchaseProducts = this.purchaseProducts.filter((prod) => {
                    return prod.rowId != id;
                });

                this.calcuateSummary();
            },

            getData: function () {
                var root = this;
                var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiOGU2MTI1NzYtMDNhNy00MDk0LTg2ZWEtNjAwODViY2E5OTk5IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjU3MDk3NTE2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.IjtMbckhrVhVabG1D-DhjTtidoDRPXxw-qCtL0yKhLY'

                root.$https
                    .get("/Sale/TaxRateForEmail?companyId=" + this.$route.query.cId, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.vats = response.data.taxRates;
                        }
                    }).then(function () {

                        if (root.purchase != undefined) {
                            if (root.purchase.saleOrderItems != undefined) {
                                //Sale Order Edit

                                root.purchase.saleOrderItems.forEach(function (item) {
                                    root.purchaseProducts.push({
                                        rowId: item.id,
                                        id: item.id,
                                        batchNo: item.batchNo,
                                        discount: item.discount,
                                        expiryDate: item.expiryDate,
                                        isExpire: item.isExpire,
                                        fixDiscount: item.fixDiscount,
                                        product: item.product,
                                        productId: item.productId,
                                        purchaseId: item.purchaseId,
                                        quantity: item.quantity,
                                        highQty: item.highQty,
                                        taxMethod: item.taxMethod,
                                        taxRateId: item.taxRateId,
                                        unitPrice: item.unitPrice,
                                        unitPerPack: item.unitPerPack,
                                        levelOneUnit: item.product.levelOneUnit,
                                        basicUnit: item.product.basicUnit,
                                    });
                                });

                                for (var k = 0; k < root.purchaseProducts.length; k++) {
                                    root.products.push(root.purchaseProducts[k].product);

                                    root.updateLineTotal(root.purchaseProducts[k].quantity, "quantity", root.purchaseProducts[k]);
                                    root.updateLineTotal(root.purchaseProducts[k].highQty, "highQty", root.purchaseProducts[k]);
                                    root.updateLineTotal(root.purchaseProducts[k].unitPrice, "unitPrice", root.purchaseProducts[k]);
                                    root.updateLineTotal(root.purchaseProducts[k].discount, "discount", root.purchaseProducts[k]);
                                    root.updateLineTotal(root.purchaseProducts[k].fixDiscount, "fixDiscount", root.purchaseProducts[k]);
                                }
                                root.calcuateSummary()
                            }

                        }
                    });
            },
        },
        created: function () {
            //this.$barcodeScanner.init(this.onBarcodeScanned);
            this.getData();
        },
        mounted: function () {
            this.GetProductList();
            this.currency = this.$route.query.cur;
            this.isMultiUnit = this.$route.query.unit;
        },
        //destroyed() {
        //    // Remove listener when component is destroyed
        //    this.$barcodeScanner.destroy()
        //},
    };
</script>
