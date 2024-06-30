<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">

        <div class="table-responsive">
            <table class="table add_table_list_bg mt-2 " v-if="saleProducts.length > 0" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">

                <thead class="m-0">
                    <tr>
                        <th style="width: 20px;">
                            #
                        </th>
                        <th style="width: 200px;">
                            {{ $t('InvoiceViewItem.Product') }}
                        </th>
                        <th class="text-right" style="width: 100px;">
                            {{ $t('InvoiceViewItem.UnitPrice') }}
                        </th>
                        <th class="text-center" style="width: 80px;" v-if="isFifo">
                            {{ $t('SaleItem.BatchNo') }}
                        </th>
                        <th class="text-center" style="width: 80px;" v-if="isFifo">
                            {{ $t('SaleItem.ExpiryDate') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('InvoiceViewItem.HighQty') }}
                        </th>
                        <th class="text-center" style="width: 100px;">
                            {{ $t('InvoiceViewItem.Quantity') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('InvoiceViewItem.TOTALQTY') }}
                        </th>
                        <th class="text-center" style="width: 100px;" hidden>
                            {{ $t('InvoiceViewItem.ReturnDays') }}
                        </th>
                        <th style="width: 100px;" v-if="saleProducts.filter(x=> x.isBundleOffer).length > 0" hidden>
                            {{ $t('InvoiceViewItem.Bundle') }}
                        </th>

                        <th class="text-center" style="width: 100px;">
                            {{ $t('InvoiceViewItem.DISC%') }}
                        </th>
                        <th class="text-center" style="width: 100px;">
                            {{ $t('InvoiceViewItem.FixDisc') }}
                        </th>
                        <th style="width: 100px;" hidden>
                            {{ $t('InvoiceViewItem.VAT%') }}
                        </th>
                        <th class="text-right" style="width: 100px;">
                            {{ $t('InvoiceViewItem.LineTotal') }}
                        </th>
                    </tr>
                </thead>
                <tbody id="sale-item">
                   
                        <tr v-for="(prod, index) in saleProducts" :key="rendered + index" v-bind:class="{'alert-danger':prod.outOfStock}">
                            <td>{{index+1}}</td>
                            <td>
                                <span>
                                    {{($i18n.locale == 'en' ||isLeftToRight())? products.find(x => x.id == prod.productId).englishName!=''? products.find(x => x.id == prod.productId).englishName : products.find(x => x.id == prod.productId).arabicName :    products.find(x => x.id == prod.productId).arabicName!=''? products.find(x => x.id == prod.productId).arabicName : products.find(x => x.id == prod.productId).englishName}}
                                </span>

                                <span v-if="products.find(x => x.id == prod.productId).promotionOffer!=undefined && products.find(x => x.id == prod.productId).promotionOffer.fixedDiscount > 0" class="badge badge-pill badge-success">
                                    {{(products.find(x => x.id == prod.productId).promotionOffer.fixedDiscount).toFixed(3).slice(0,-1)}}, ({{products.find(x => x.id == prod.productId).promotionOffer.stockLimit - products.find(x => x.id == prod.productId).promotionOffer.quantityOut}})
                                </span>
                                <span v-if="products.find(x => x.id == prod.productId).promotionOffer!=undefined && products.find(x => x.id == prod.productId).promotionOffer.discountPercentage > 0" class="badge badge-pill badge-success">
                                    {{(products.find(x => x.id == prod.productId).promotionOffer.discountPercentage).toFixed(3).slice(0,-1)}}%, ({{products.find(x => x.id == prod.productId).promotionOffer.stockLimit - products.find(x => x.id == prod.productId).promotionOffer.quantityOut}})
                                </span>
                                <span v-if="products.find(x => x.id == prod.productId).bundleCategory != undefined" class="badge badge-pill badge-success">
                                    {{products.find(x => x.id == prod.productId).bundleCategory.buy}} + {{products.find(x => x.id == prod.productId).bundleCategory.get}}, ({{products.find(x => x.id == prod.productId).bundleCategory.stockLimit - products.find(x => x.id == prod.productId).bundleCategory.quantityOut}})
                                </span>
                            </td>

                            <td class="text-right">
                                {{prod.unitPrice}}
                            </td>
                            <td class="text-center" v-if="isFifo">
                                {{prod.batchNo}}
                            </td>
                            <td class="text-center" v-if="isFifo">
                                {{getDate(prod.batchExpiry)}}
                            </td>
                            <td class="text-center" v-if="isMultiUnit=='true'">
                                {{prod.highQty}}<br />
                                <small style="font-weight: 500;font-size:70%;">
                                    {{prod.levelOneUnit}}
                                </small>
                            </td>
                            <td class="text-center">
                                {{prod.quantity}}<br />
                                <small style="font-weight: 500;font-size:70%;" v-if="isMultiUnit=='true'">
                                    {{prod.basicUnit}}
                                </small>
                            </td>
                            <td class="text-center" v-if="isMultiUnit=='true'">
                                {{prod.totalPiece}}
                            </td>
                            <td v-if="prod.saleReturnDays > 0" hidden>
                                {{prod.saleReturnDays}}
                            </td>
                            <td class="text-center" v-else hidden>
                                <span>--</span>
                            </td>
                            <td class="text-center" v-if="saleProducts.filter(x=> x.isBundleOffer).length > 0" hidden>
                                <span class="badge badge-pill badge-info">{{prod.bundleOffer}}</span>
                            </td>

                            <td class="text-center">
                                {{prod.discount}}
                            </td>

                            <td class="text-center">
                                {{prod.fixDiscount}}
                            </td>
                            <td hidden>
                                <taxratedropdown v-model="prod.taxRateId"
                                                 @update:modelValue="getVatValue($event, prod)" :dropdownpo="'dropdownpo'"></taxratedropdown>
                            </td>
                            <td class="text-right">
                                {{currency}} {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0,-1))  }}
                            </td>
                        </tr>
                    
                </tbody>
            </table>
        </div>

        <div class=" table-responsive"
             v-bind:key="rendered + 'g'"
             v-if="saleProducts.length > 0">
            <table class="table table-striped table-hover add_table_list_bg">
                <thead class="m-0">
                    <tr class="text-right">
                        <th class="text-center" style="width:100px;">
                            {{ $t('InvoiceViewItem.NoItem') }}
                        </th>
                        <th class="text-center" style="width:100px;" v-if="isMultiUnit=='true'">
                            {{ $t('InvoiceViewItem.TotalCarton') }}
                        </th>
                        <th class="text-center" style="width:100px;" v-if="isMultiUnit=='true'">
                            {{ $t('InvoiceViewItem.TotalPieces') }}
                        </th>
                        <th class="text-center" style="width:100px;">
                            {{ $t('InvoiceViewItem.TotalQty') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('InvoiceViewItem.Total') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('InvoiceViewItem.Disc') }}
                        </th>
                        <th style="width:160px;">
                            {{ $t('InvoiceViewItem.TotalAfterDisc') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('InvoiceViewItem.TotalVat') }}
                        </th>
                        <th v-if="summary.bundleAmount > 0" style="width:100px;">
                            {{ $t('InvoiceViewItem.BundleAmount') }}
                        </th>
                        <th style="width:155px;">
                            {{ $t('InvoiceViewItem.TotalWithVat') }}
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right">
                        <td class="text-center">
                            {{ summary.item}}
                        </td>
                        <td class="text-center" v-if="isMultiUnit=='true'">
                            {{ summary.totalCarton}}
                        </td>
                        <td class="text-center" v-if="isMultiUnit=='true'">
                            {{ summary.totalPieces}}
                        </td>
                        <td class="text-center">
                            {{$formatAmount(summary.qty)   }}
                        </td>

                        <td>
                            {{$formatAmount(summary.total)   }}
                        </td>
                        <td>
                            {{$formatAmount(summary.discount)   }}
                        </td>
                        <td>
                            {{currency}}  {{$formatAmount(summary.withDisc)   }}
                        </td>
                        <td>
                            {{currency}}  {{$formatAmount((parseFloat(summary.vat)).toFixed(3).slice(0,-1))   }}
                        </td>
                        <td v-if="summary.bundleAmount > 0">
                            {{currency}} {{$formatAmount(summary.bundleAmount) }}
                        </td>
                        <td>
                            {{currency}} {{$formatAmount(summary.withVat)  }}
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <authorize-user-model :authorize="authorize"
                              :show="show"
                              v-if="show"
                              @result="result"
                              @close="show = false" />
    </div>
</template>

<script>
    //
    
    import moment from "moment";

    export default {
        name: "SaleItem",
        props: ['saleItems', 'wareHouseId'],
        data: function () {
            return {
                dayStart: '',
                isFifo: false,
                decimalQuantity: false,
                invoiceWoInventory: '',
                fixDiscount: '',
                discount: '',
                bundle: '',
                counter: 0,
                isAuthour: {
                    changePriceDuringSale: false,
                    giveDiscountDuringSale: false,
                    column: '',
                },
                changePriceDuringSale: false,
                giveDiscountDuringSale: false,
                show: false,
                authorize: {
                    column: '',
                    userName: '',
                    password: '',
                },
                rendered: 0,
                product: {
                    id: "",
                },
                products: [],
                saleProducts: [],
                loading: false,
                vats: [],
                summary: {
                    item: 0,
                    qty: 0,
                    total: 0,
                    discount: 0,
                    withDisc: 0,
                    vat: 0,
                    withVat: 0,
                    bundleAmount: 0,
                    totalCarton: 0,
                    totalPieces: 0
                },
                currency: '',
                isMultiUnit: '',
            };
        },
        validations() {},
        filter: {},
        methods: {

            getDate: function (x) {
                return moment(x).format("l");
            },
            result: function (x) {
                this.isAuthour = x;
            },
            openmodel: function (column) {

                this.authorize = {
                    userName: '',
                    password: '',
                    column: column
                }
                this.show = !this.show;
            },
            openmodel1: function (column) {

                this.authorize = {
                    userName: '',
                    password: '',
                    column: column
                }
                this.show = !this.show;
            },
            openmodel2: function (column) {

                this.authorize = {
                    userName: '',
                    password: '',
                    column: column
                }
                this.show = !this.show;
            },

            changeProduct: function (NewProdId, rowId) {

                this.saleProducts = this.saleProducts.filter(x => x.rowId != rowId);
                this.addProduct(NewProdId);

            },

            calcuateSummary: function () {

                this.summary.item = this.saleProducts.length;
                if (this.decimalQuantity) {
                    this.summary.totalPieces = this.saleProducts.reduce((totalQty, prod) => totalQty + parseFloat(prod.quantity), 0);
                }
                else {
                    this.summary.totalPieces = this.saleProducts.reduce((totalQty, prod) => totalQty + parseInt(prod.quantity), 0);
                }

                if (this.decimalQuantity) {
                    this.summary.totalCarton = this.saleProducts.reduce((totalCarton, prod) => totalCarton + parseFloat(prod.highQty), 0);
                }
                else {
                    this.summary.totalCarton = this.saleProducts.reduce((totalCarton, prod) => totalCarton + parseInt(prod.highQty), 0);
                }

                if (this.decimalQuantity) {
                    this.summary.qty = this.saleProducts.reduce((qty, prod) => qty + parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                    this.summary.qty = parseFloat(this.summary.qty).toFixed(2);
                }
                else {
                    this.summary.qty = this.saleProducts.reduce((qty, prod) => qty + parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                }

                this.summary.total = this.saleProducts.reduce((total, prod) =>
                    total + (prod.totalPiece) * prod.unitPrice, 0).toFixed(3).slice(0, -1);

                /*just calculate discount*/
                var discount = this.saleProducts.filter((x) => (x.discount != 0 || x.discount != "") && x.offerQuantity != 0)
                    .reduce((discount, prod) => discount +
                        prod.diff ?
                        ((prod.diff * prod.unitPrice * prod.discount) / 100) :
                        ((prod.totalPiece < (prod.offerQuantity > prod.remainingStockLimit ? prod.remainingStockLimit : prod.offerQuantity)) ? ((prod.totalPiece * prod.unitPrice * prod.discount) / 100) :
                            ((prod.offerQuantity > prod.remainingStockLimit ? prod.remainingStockLimit : prod.offerQuantity) * prod.unitPrice * prod.discount) / 100), 0);

                /*just calculate fix discount*/
                var fixDiscount = this.saleProducts
                    .filter((x) => (x.fixDiscount != 0 || x.fixDiscount != "") && x.offerQuantity != 0)
                    .reduce((discount, prod) =>
                        discount + prod.offerQuantity ? ((prod.totalPiece < (prod.offerQuantity > prod.remainingStockLimit ? prod.remainingStockLimit : prod.offerQuantity)) ? prod.fixDiscount * prod.totalPiece : prod.fixDiscount * (prod.offerQuantity > prod.remainingStockLimit ? prod.remainingStockLimit : prod.offerQuantity)) : 0, 0);


                /*just calculate discount without promotion*/
                var discountOnly = this.saleProducts
                    .filter((x) => x.discount != 0 || x.discount != "" || x.offerQuantity != 0)
                    .reduce((discount, prod) =>
                        discount + (prod.totalPiece ? (prod.offerQuantity ? 0 : ((prod.totalPiece * prod.discount * prod.unitPrice) / 100)) : 0), 0);

                /*just calculate fix discount without promotion*/
                var fixDiscountOnly = this.saleProducts
                    .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "" || x.offerQuantity != 0)
                    .reduce((discount, prod) =>
                        discount + (prod.totalPiece ? (prod.offerQuantity ? 0 : (prod.totalPiece * prod.fixDiscount)) : 0), 0);


                this.summary.discount = (parseFloat(discount) + parseFloat(fixDiscount) + fixDiscountOnly + discountOnly).toFixed(3).slice(0, -1);

                this.summary.withDisc = (this.summary.total - this.summary.discount).toFixed(3).slice(0, -1);

                this.summary.vat = this.saleProducts
                    .reduce((vat, prod) => vat + prod.lineItemVAt, 0).toFixed(3).slice(0, -1);

                var taxmethod = this.saleProducts.find(function (x) {
                    return x.taxMethod == ("Inclusive" || "شامل")
                })

                this.summary.withVat = (parseFloat(this.summary.withDisc) + ((taxmethod == undefined) ? parseFloat(this.summary.vat) : 0)).toFixed(3).slice(0, -1);

                //calculate bundle Amount
                if (this.saleProducts.filter(x => x.isBundleOffer).length > 0) {

                    //get bundle get quantity
                    var bundle = {
                        item: 0,
                        qty: 0,
                        total: 0,
                        discount: 0,
                        withDisc: 0,
                        vat: 0,
                        withVat: 0,
                        quantityLimit: 0
                    };

                    var bundleProducts = this.saleProducts.filter(x => x.isBundleOffer != undefined && x.offerQuantity > 0);

                    bundle.total = bundleProducts.reduce((total, prod) =>
                        total + prod.offerQuantity * prod.unitPrice, 0).toFixed(3).slice(0, -1);

                    //var bundleExclusiveTax = bundleProducts.reduce((total, prod) =>
                    //    total + (prod.taxMethod == "Exclusive" ? (bundle.total * prod.rate/100) : 0), 0);

                    var discountBundle = bundleProducts.filter((x) => x.discount != 0 || x.discount != "")
                        .reduce((discount, prod) =>
                            discount + (prod.offerQuantity * prod.unitPrice * prod.discount) / 100, 0);

                    var fixDiscountBundle = bundleProducts
                        .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "")
                        .reduce((discount, prod) => discount + prod.fixDiscount, 0);

                    bundle.discount = (parseFloat(discountBundle) + parseFloat(fixDiscountBundle)).toFixed(3).slice(0, -1);

                    bundle.withDisc = (bundle.total - bundle.discount).toFixed(3).slice(0, -1);

                    bundle.vat = bundleProducts
                        .reduce((vat, prod) => vat + (((prod.unitPrice * prod.offerQuantity) -
                            ((prod.unitPrice * prod.offerQuantity * prod.discount) / 100)) *
                            parseFloat(prod.rate)) / ((prod.taxMethod == "Exclusive" || prod.taxMethod == "غير شامل") ? 100 : prod.rate + 100), 0).toFixed(3).slice(0, -1);

                    this.summary.bundleAmount = (parseFloat(bundle.withDisc) + ((taxmethod == undefined) ? parseFloat(bundle.vat) : 0)).toFixed(3).slice(0, -1);
                    this.summary.withVat = (this.summary.withVat - bundle.withDisc);
                } else {
                    this.summary.bundleAmount = 0;
                }

                this.$emit("update:modelValue", this.saleProducts);

                this.$emit("summary", this.summary);
            },

            updateLineTotal: function (e, prop, product) {
                var root = this;
                
                if (e != undefined) {
                    var discount = product.discount == 0 || product.discount == "" ? product.fixDiscount == 0 || product.fixDiscount == ""
                        ? 0
                        : product.fixDiscount
                        : product.discount;

                    var prod = root.products.find((x) => x.id == product.productId && x.batchNo == product.batchNo);
                    if (prop == "unitPrice") {
                        product.unitPrice = e;
                    }

                    if (prop == "quantity") {
                        if (e <= 0 || e == '') {
                            e = 0;
                        }
                        if (String(e).split('.').length > 1 && String(e).split('.')[1].length > 2)
                            e = parseFloat(String(e).slice(0, -1))
                        product.quantity = this.decimalQuantity ? e : Math.round(e);
                    }
                    if (prop == "highQty") {
                        if (e < 0 || e == '' || e == undefined) {
                            e = 0;
                        }
                        product.highQty = Math.round(e);
                    }
                    product.totalPiece = (parseFloat(product.highQty == undefined ? 0 : product.highQty) * parseFloat(product.unitPerPack == null ? 0 : product.unitPerPack)) + parseFloat(product.quantity == '' ? 0 : product.quantity);
                    if (prod.promotionOffer != null) {
                        if (product.totalPiece > 0 && moment().format("DD MMM YYYY") >= moment(prod.promotionOffer.fromDate).format("DD MMM YYYY") &&
                            moment().format("DD MMM YYYY") <= moment(prod.promotionOffer.toDate).format("DD MMM YYYY")) {
                            product.fixDiscount = prod.promotionOffer.fixedDiscount;
                            product.discount = prod.promotionOffer.discountPercentage;
                            product.offerQuantity = prod.promotionOffer.totalPiece;
                            product['isOfferQty'] = true;
                        } else {
                            if (product.isOfferQty) {
                                product.fixDiscount = 0;
                                product.discount = 0;
                                product.offerQuantity = 0;
                            }
                            product['isOfferQty'] = false;
                        }
                    }

                    if (prod.bundleCategory != null) {
                        if (product.totalPiece >= prod.bundleCategory.buy && moment().format("DD MMM YYYY") >= moment(prod.bundleCategory.fromDate).format("DD MMM YYYY") &&
                            moment().format("DD MMM YYYY") <= moment(prod.bundleCategory.toDate).format("DD MMM YYYY")) {
                            product['bundleOffer'] = prod.bundleCategory.buy.toString() + " + " + prod.bundleCategory.get.toString();
                            product['get'] = prod.bundleCategory.get;
                            product['buy'] =  prod.bundleCategory.buy;
                            product['quantityLimit'] = prod.bundleCategory.quantityLimit;
                            product['isBundleOffer'] = true;
                           
                        } else {
                            product['bundleOffer'] ="";
                            product['get'] = 0;
                            product['buy'] =  0;
                            product['quantityLimit'] = 0;
                            product['isBundleOffer'] = false;

                        }
                        //bundle category calculation
                        if (product.quantityLimit != undefined && parseFloat(product.totalPiece) >= (product.get + product.buy)) {
                            if ((product.get + product.buy) > 0) {
                                product.offerQuantity = Math.floor(parseFloat(product.totalPiece) / (product.get + product.buy));
                                if ((prod.bundleCategory.quantityOut + product.offerQuantity) <= prod.bundleCategory.stockLimit) {
                                    if (product.offerQuantity <= product.quantityLimit) {
                                        product.offerQuantity = product.offerQuantity * product.get;
                                    }
                                    else {
                                        product.offerQuantity = product.quantityLimit * product.get;
                                    }
                                }
                                else {
                                    var diffBundle = prod.bundleCategory.stockLimit - prod.bundleCategory.quantityOut;
                                    if (diffBundle > product.quantityLimit) {
                                        product.offerQuantity = product.quantityLimit * product.get;
                                    }
                                    else {
                                        product.offerQuantity = diffBundle * product.get;
                                    }
                                }
                            }
                            else {
                                product.offerQuantity = 0;
                            }
                        }
                        else {
                            if ((product.get + product.buy) > 0) {
                                product.offerQuantity = Math.floor(parseFloat(product.totalPiece) / (product.get + product.buy));
                            }
                            else {
                                product.offerQuantity = 0;
                            }
                        }
                        //bundle category calculation end
                    }

                    if (this.invoiceWoInventory == 'false') {
                        
                        var bundleQuantity = product.bundleOfferQuantity == undefined ? 0 : product.bundleOfferQuantity;
                        if (prod.inventory != null) {
                            if (parseFloat(product.totalPiece) + bundleQuantity > (prod.inventory.currentQuantity + ((this.saleOrderId != null && this.saleOrderId != '' && this.soInventoryReserve) ? parseFloat(product.soQty) : 0))) {
                                product['outOfStock'] = true;
                            } else {
                                product['outOfStock'] = false;
                            }
                        }
                        else {
                            product['outOfStock'] = true;
                        }
                    }


                    //End Calculate offer
                    if (prop == "discount") {
                        if (e == "" || e < 0) {
                            e = 0;
                        }
                        else if (e > 100) {
                            e = 100;
                        }
                        product.discount = e;
                    }

                    if (prop == "fixDiscount") {
                        if (e == "" || e < 0) {
                            e = 0;
                        }
                        else if (e > product.unitPrice) {
                            e = product.unitPrice;
                        }
                        product.fixDiscount = e;
                    }

                    var vat = 0;
                    var total = 0;
                    var calculateVAt = 0;
                    //here we will select quantity after deduct bundle quantity
                    if (product.offerQuantity > 0) {

                        if (product.isOfferQty) {
                            if (product.totalPiece > 0) {
                                if ((product.totalPiece) <= (prod.promotionOffer.stockLimit - prod.promotionOffer.quantityOut)) {
                                    product['remainingStockLimit'] =  (prod.promotionOffer.stockLimit - prod.promotionOffer.quantityOut);
                                    if (product.totalPiece <= product.offerQuantity) {
                                        discount = product.discount == 0 ? (product.fixDiscount * product.totalPiece) : (product.totalPiece * product.unitPrice * product.discount) / 100;

                                    }
                                    else {
                                        discount = product.discount == 0 ? (product.fixDiscount * product.offerQuantity) : (product.offerQuantity * product.unitPrice * product.discount) / 100;
                                    }
                                }
                                else {
                                    discount = product.discount == 0 ? (product.fixDiscount * (prod.promotionOffer.stockLimit - prod.promotionOffer.quantityOut)) : ((prod.promotionOffer.stockLimit - prod.promotionOffer.quantityOut) * product.unitPrice * product.discount) / 100;
                                }

                                vat = this.vats.find((value) => value.id == product.taxRateId);
                                total = product.totalPiece * product.unitPrice - discount;
                                if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                                    calculateVAt = (total * vat.rate) / (100 + vat.rate);
                                    product.lineItemVAt = calculateVAt;
                                    //product.lineTotal = total - calculateVAt;
                                    product.lineTotal = total;
                                }
                                else {
                                    calculateVAt = (total * vat.rate) / 100;
                                    product.lineItemVAt = calculateVAt;
                                    product.lineTotal = total + calculateVAt;
                                }

                            }
                            else {
                                total = product.offerQuantity * product.unitPrice - discount;
                                if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                                    calculateVAt = (total * vat.rate) / (100 + vat.rate);
                                    product.lineItemVAt = calculateVAt;
                                    //product.lineTotal = total - calculateVAt;
                                    product.lineTotal = total;
                                }
                                else {
                                    calculateVAt = (total * vat.rate) / 100;
                                    product.lineItemVAt = calculateVAt;
                                    product.lineTotal = total + calculateVAt;
                                }
                            }
                        }
                        else {
                            discount = product.discount == 0 ? product.fixDiscount : (product.offerQuantity * product.unitPrice * product.discount) / 100;
                            vat = this.vats.find((value) => value.id == product.taxRateId);

                            total = (product.totalPiece - product.offerQuantity) * product.unitPrice - discount;
                            if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                                calculateVAt = (total * vat.rate) / (100 + vat.rate);
                                product.lineItemVAt = calculateVAt;
                                //product.lineTotal = total - calculateVAt;
                                product.lineTotal = total;
                            }
                            else {
                                calculateVAt = (total * vat.rate) / 100;
                                product.lineItemVAt = calculateVAt;
                                product.lineTotal = total + calculateVAt;
                            }
                        }
                        this.saleProducts['product'] = product;
                        this.calcuateSummary();
                        this.$emit("update:modelValue", this.saleProducts);

                    } else {

                        discount = product.discount == 0 ? (product.fixDiscount * product.totalPiece) : (product.totalPiece * product.unitPrice * product.discount) / 100;
                        vat = this.vats.find((value) => value.id == product.taxRateId);

                        total = product.totalPiece * product.unitPrice - discount;
                        if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                            calculateVAt = (total * vat.rate) / (100 + vat.rate);
                            //product.lineTotal = total - calculateVAt;
                            product.lineItemVAt = calculateVAt;
                            product.lineTotal = total;
                        }
                        else {
                            calculateVAt = (total * vat.rate) / 100;
                            product.lineTotal = total + calculateVAt;
                            product.lineItemVAt = calculateVAt;
                        }
                        this.saleProducts['product'] = product;
                        this.calcuateSummary();
                        this.$emit("update:modelValue", this.saleProducts);
                    }
                }
            },

            addProduct: function (productId, newProduct, qty) {


                if (this.saleProducts.some(x => x.productId == productId)) {
                    var prd = this.saleProducts.find(x => x.productId == productId);
                    prd.quantity++;
                    this.updateLineTotal(prd.quantity, "quantity", prd);
                } else {

                    if (this.products.find(x => x.id == newProduct.id) == undefined || this.products.length <= 0) {
                        this.products.push(newProduct);
                    }
                    var prod = this.products.find((x) => x.id == productId);

                    var rate = 0;
                    if (prod.taxRateId != "00000000-0000-0000-0000-000000000000" && prod.taxRateId != undefined) {
                        rate = this.getVatValue(prod.taxRateId, prod);
                    }
                    if (qty != null && qty != undefined && qty != 0) {
                        this.saleProducts.push({
                            rowId: this.createUUID(),
                            productId: prod.id,
                            unitPrice: 0,
                            quantity: qty,
                            discount: 0,
                            fixDiscount: 0,
                            lineItemVAt: 0,
                            promotionId: prod.promotionOffer == null ? null : prod.promotionOffer.id,
                            bundleId: prod.bundleCategory == null ? null : prod.bundleCategory.id,
                            taxRateId: prod.taxRateId,
                            saleReturnDays: prod.saleReturnDays,
                            taxMethod: prod.taxMethod,
                            rate: rate,
                            lineTotal: prod.salePrice * 1,
                            buy: prod.bundleCategory != null ? prod.bundleCategory.buy : 0,
                            get: prod.bundleCategory != null ? prod.bundleCategory.get : 0,
                            quantityLimit: prod.bundleCategory != null ? prod.bundleCategory.quantityLimit : 0,
                            offerQuantity: 0,
                        });
                    }
                    else {
                        this.saleProducts.push({
                            rowId: this.createUUID(),
                            productId: prod.id,
                            unitPrice: prod.salePrice,
                            quantity: 1,
                            discount: 0,
                            fixDiscount: 0,
                            lineItemVAt: 0,
                            promotionId: prod.promotionOffer == null ? null : prod.promotionOffer.id,
                            bundleId: prod.bundleCategory == null ? null : prod.bundleCategory.id,
                            taxRateId: prod.taxRateId,
                            saleReturnDays: prod.saleReturnDays,
                            taxMethod: prod.taxMethod,
                            rate: rate,
                            lineTotal: prod.salePrice * 1,
                            buy: prod.bundleCategory != null ? prod.bundleCategory.buy : 0,
                            get: prod.bundleCategory != null ? prod.bundleCategory.get : 0,
                            quantityLimit: prod.bundleCategory != null ? prod.bundleCategory.quantityLimit : 0,
                            offerQuantity: 0,
                        });
                    }

                }
                var product = this.saleProducts.find((x) => {
                    return x.productId == productId;
                });

                this.getVatValue(product.taxRateId, product);
                this.updateLineTotal(product.quantity, "quantity", product);

                this.product.id = "";
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
            getVatValue: function (id, prod) {

                var vat = this.vats.find((value) => value.id == id);
                prod.taxRateId = id;
                prod.rate = vat.rate;
                this.updateLineTotal(prod.unitPrice, "unitPrice", prod);
                return vat.rate;
            },
            removeProduct: function (id) {

                this.saleProducts = this.saleProducts.filter((prod) => {
                    return prod.rowId != id;
                });

                this.calcuateSummary();
            },


            getData: function () {
                var root = this;
                this.loading = true;
                var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibm9ibGVAZ21haWwuY29tIiwic3ViIjoibm9ibGVAZ21haWwuY29tIiwianRpIjoiOGU2MTI1NzYtMDNhNy00MDk0LTg2ZWEtNjAwODViY2E5OTk5IiwiUm9sZSI6Ik5vYmxlIEFkbWluIiwiQ29tcGFueUlkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiVXNlcklkIjoiNWY4ZDU2MTQtMmM3ZS00ZWMwLTg2OGMtZDI1NGU2NTE2YjRkIiwiRW1haWwiOiJub2JsZUBnbWFpbC5jb20iLCJOb2JsZUNvbXBhbnlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIkJ1c2luZXNzSWQiOiIiLCJDbGllbnRQYXJlbnRJZCI6IiIsIkVtcGxveWVlSWQiOiIiLCJDb3VudGVySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJEYXlJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIklzUHJvY2VlZCI6ZmFsc2UsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik5vYmxlIEFkbWluIiwiZXhwIjoxNjU3MDk3NTE2LCJpc3MiOiJodHRwOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwOi8veW91cmRvbWFpbi5jb20ifQ.IjtMbckhrVhVabG1D-DhjTtidoDRPXxw-qCtL0yKhLY'

                root.$https
                    .get("/Sale/TaxRateForEmail?companyId=" + this.$route.query.companyId, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.vats = response.data.taxRates;
                        }
                    }).then(function () {

                        
                        if (root.saleItems != undefined) {

                            root.saleItems.forEach(function (item) {
                                
                                if (root.isFifo) {



                                    root.products.push({
                                        rowId: item.id,
                                        arabicName: item.arabicName,
                                        assortment: item.assortment,
                                        barCode: item.barCode,
                                        basicUnit: item.product.basicUnit,
                                        batchExpiry: item.expiryDate,
                                        batchNo: item.batchNo,
                                        brandId: item.brandId,
                                        bundleCategory: item.bundleCategory,
                                        category: item.category,
                                        categoryId: item.categoryId,
                                        code: item.code,
                                        colorId: item.colorId,
                                        colorName: item.colorName,

                                        colorNameArabic: item.colorNameArabic,
                                        currentQuantity: item.currentQuantity,
                                        description: item.description,
                                        englishName: item.productName,
                                        guarantee: item.product.guarantee,
                                        id: item.productId,
                                        image: item.image,
                                        inventory: item.product.inventory,
                                        inventoryBatch: item.product.inventoryBatch,
                                        isActive: item.isActive,
                                        isExpire: item.isExpire,
                                        isRaw: item.isRaw,

                                        length: item.length,
                                        levelOneUnit: item.product.levelOneUnit,
                                        originId: item.originId,
                                        promotionOffer: item.promotionOffer,
                                        purchasePrice: item.purchasePrice,
                                        salePrice: item.salePrice,
                                        salePriceUnit: item.salePriceUnit,
                                        saleReturnDays: item.saleReturnDays,
                                        serial: item.product.serial,
                                        serviceItem: item.serviceItem,

                                        shelf: item.shelf,
                                        sizeId: item.sizeId,
                                        sizeName: item.sizeName,
                                        sizeNameArabic: item.sizeNameArabic,
                                        stockLevel: item.stockLevel,
                                        styleNumber: item.styleNumber,
                                        subCategoryId: item.subCategoryId,
                                        taxMethod: item.taxMethod,
                                        taxRate: item.taxRate,
                                        taxRateId: item.taxRateId,
                                        taxRateValue: item.taxRateValue,
                                        unit: item.unit,
                                        unitId: item.unitId,

                                        unitPerPack: item.unitPerPack,
                                        width: item.width,
                                    });


                                }
                                else {
                                    root.products.push(item.product);
                                }


                                var vat = root.vats.find((value) => value.id == item.taxRateId);

                                root.saleProducts.push({
                                    rowId: item.id,
                                    productId: item.productId,
                                    unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                    quantity: item.quantity,
                                    highQty: item.highQty,
                                    discount: item.discount,
                                    offerQuantity: item.offerQuantity == undefined ? 0 : item.offerQuantity,
                                    fixDiscount: item.fixDiscount,
                                    taxRateId: item.taxRateId,
                                    taxMethod: item.taxMethod,
                                    rate: vat.rate,
                                    soQty: item.soQty,
                                    batchExpiry: item.batchExpiry,
                                    batchNo: item.batchNo,
                                    inventoryList: item.product.inventoryBatch == null ? null : item.product.inventoryBatch,
                                    currentQuantity: item.product.inventory == null ? 0 : item.product.inventory.currentQuantity,
                                    saleReturnDays: item.saleReturnDays,
                                    lineTotal: item.unitPrice * item.quantity,
                                    unitPerPack: item.unitPerPack,
                                    levelOneUnit: item.product.levelOneUnit,
                                    basicUnit: item.product.basicUnit,
                                    serial: item.serial,
                                    guaranteeDate: item.guaranteeDate,
                                    isSerial: item.product.serial,
                                    guarantee: item.product.guarantee,
                                });

                                var product = root.saleProducts.find((x) => {
                                    return x.productId == item.productId && x.rowId == item.id;
                                });
                                
                                root.getVatValue(product.taxRateId, product);
                                root.updateLineTotal(item.quantity, "quantity", product);
                                root.updateLineTotal(item.highQty, "highQty", product);
                                root.product.id = "";
                                root.rendered++;
                            });
                            root.loading = false
                            root.$emit("details", root.saleProducts);
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
        },
        created: function () {

            this.getData();
            var root = this;

            if (this.$route.query.mobiledata != undefined) {
                //root.purchaseProducts = root.$route.query.data.mobileOrderItemLookupModels;
                for (var j = 0; j < this.$route.query.mobiledata.mobileOrderItemLookupModels.length; j++) {

                    /*  this.saleProducts.rowId[j] = this.$route.query.mobiledata.mobileOrderItemLookupModels[j].id[j];*/
                    this.saleProducts.quantity[j] = this.$route.query.mobiledata.mobileOrderItemLookupModels[j].quantity[j];
                    //root.updateLineTotal(root.purchaseProducts[j].quantity, "quantity", root.purchaseProducts[j]);
                    //root.updateLineTotal(root.purchaseProducts[j].unitPrice, "unitPrice", root.purchaseProducts[j]);

                }
                root.calcuateSummary();
                this.saleProducts.rowId = this.$route.query.mobiledata.mobileOrderItemLookupModels.rowId;
                this.saleProducts.quantity = this.$route.query.mobiledata.mobileOrderItemLookupModels.quantity;
            }
        },
        mounted: function () {
            var root = this
            this.isFifo = root.$route.query.fifo == 'true' ? true : false;
            this.decimalQuantity = root.$route.query.decimal == 'true' ? true : false;
            this.isMultiUnit = root.$route.query.multiUnit;
            this.currency = root.$route.query.currency;
            //this.dayStart = localStorage.getItem('DayStart');
            this.invoiceWoInventory = root.$route.query.invoiceWoInventory;

            //this.changePriceDuringSale = localStorage.getItem('changePriceDuringSale');
            //this.changePriceDuringSale == 'true' ? (this.changePriceDuringSale = true) : (this.changePriceDuringSale = false);
            //this.giveDiscountDuringSale = localStorage.getItem('giveDicountDuringSale');
            //this.giveDiscountDuringSale == 'true' ? (this.giveDiscountDuringSale = true) : (this.giveDiscountDuringSale = false);

        },
    };
</script>
<style scoped>

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

    .tableHoverOn {
        background-color: #F4F6FC !important;
    }

    .multiselect__input, .multiselect__single {
        background-color: transparent !important;
    }
</style>
