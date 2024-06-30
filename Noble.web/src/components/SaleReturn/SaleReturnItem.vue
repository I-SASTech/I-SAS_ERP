<template>
    <div>
        <div class="table-responsive" v-bind:key="rendered">
            <table class="table mb-0" v-if="saleProducts.length > 0" style="table-layout:fixed;" >
                <thead class="thead-light table-hover" >
                    <tr>
                        <th style="width: 20px;">
                            #
                        </th>
                        <th style="width:200px;">
                            {{ $t('SaleReturnItem.Product') }}
                        </th>
                        <th style="width:100px;" class="text-center">
                            {{ $t('SaleReturnItem.UnitPrice') }}
                        </th>
                        <th class="text-center" style="width: 70px;" v-if="isValid('CanViewUnitPerPack')">
                            {{ $t('SaleReturnItem.UnitPerPack') }}
                        </th>
                        <th style="width: 70px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('SaleReturnItem.HighQty') }}
                        </th>
                        <th style="width:70px;" class="text-center">
                            {{ $t('SaleReturnItem.Qty') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('SaleReturnItem.TOTALQTY') }}
                        </th>
                        <th v-if="saleProducts.filter(x=> x.isBundleOffer).length > 0" style="width:100px;">
                            {{ $t('SaleReturnItem.bundle') }}
                        </th>

                        <th class="text-center" style="width: 100px;" v-if="isSerial">
                            {{ $t('SaleReturnItem.Serial') }}
                        </th>
                        <th style="width: 100px;" v-if="isSerial">
                            {{ $t('SaleReturnItem.Guarantee') }}
                        </th>

                        <th style="width:70px;" class="text-center">
                            {{ $t('SaleReturnItem.Disc%') }}
                        </th>
                        <th style="width:100px;" class="text-center">
                            {{ $t('SaleReturnItem.FixDisc') }}
                        </th>

                        <th v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'" style="width: 105px;">
                            {{ $t('AddPurchase.TaxMethod') }}
                        </th>
                        <th v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'" style="width: 130px;">
                            {{ $t('AddPurchase.VAT%') }}
                        </th>

                        <th class="text-right" style="width:120px;">
                            {{ $t('SaleReturnItem.LineTotal') }}
                        </th>
                        <th style="width:40px;"></th>
                    </tr>
                </thead>
                <tbody :key="rendered">
                        <tr v-for="(prod, index) in saleProducts" :key="index" v-bind:class="{'alert-danger':prod.outOfStock}" >
                            <td>{{index+1}}</td>
                            <td>
                                {{($i18n.locale == 'en' ||isLeftToRight())? products.find(x => x.id == prod.productId).englishName!=''? products.find(x => x.id == prod.productId).englishName : products.find(x => x.id == prod.productId).arabicName :    products.find(x => x.id == prod.productId).arabicName!=''? products.find(x => x.id == prod.productId).arabicName : products.find(x => x.id == prod.productId).englishName}}
                            </td>

                            <td>
                                <!--<input v-model="prod.unitPrice"
                                            @focus="$event.target.select()"
                                            class="form-control  input-border"
                                            @keyup="updateLineTotal($event.target.value, 'unitPrice', prod)" />-->

                                <decimal-to-fixed v-model="prod.unitPrice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.unitPrice, 'unitPrice', prod)" />
                            </td>
                            <td class="border-top-0 text-center" v-if="isValid('CanViewUnitPerPack')">
                                {{prod.unitPerPack}}
                            </td>
                            <td class="text-center" v-if="isMultiUnit=='true'">
                                <input type="number" v-model="prod.highQty"
                                       style=""
                                       @focus="$event.target.select()"
                                       class="form-control input-border text-center "
                                       @keyup="updateLineTotal($event.target.value, 'highQty', prod)" />
                                <small style="font-weight: 500;font-size:70%;">
                                    {{prod.levelOneUnit}}
                                </small>
                            </td>
                            <td class="text-center">
                                <input v-model="prod.quantity"
                                       type="number"
                                       @focus="$event.target.select()"
                                       class="form-control text-center  input-border"
                                       @keyup="updateLineTotal($event.target.value, 'quantity', prod)" />
                                <small style="font-weight: 500;font-size:70%;" v-if="isMultiUnit=='true'">
                                    {{prod.basicUnit}}
                                </small>
                            </td>
                            <td class=" text-center" v-if="isMultiUnit=='true'">
                                {{parseInt(parseFloat(prod.highQty*prod.unitPerPack) + parseFloat(prod.quantity))}}
                            </td>
                            <td class="text-center" v-if="saleProducts.filter(x=> x.isBundleOffer).length > 0">
                                <span class="badge badge-pill badge-info">{{prod.bundleOffer}}</span>
                            </td>
                            <!--<td class="text-center" v-else-if="saleProducts.filter(x=> x.isBundleOffer).length > 0">
                                <span>-</span>
                            </td>-->

                            <td class="text-center" v-if="isSerial">
                                <button @click="AddSerial(prod)" title="Add Serial" class="btn btn-primary btn-sm"> Add Serial </button>
                            </td>
                            <td class=" text-center" v-if="isSerial">
                                <datepicker v-model="prod.guaranteeDate" />
                            </td>

                            <td>
                                <decimal-to-fixed v-model="prod.discount" v-bind:disable="prod.fixDiscount != 0?true:false" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.discount, 'discount', prod)" />
                            </td>
                            <td>
                                <decimal-to-fixed v-model="prod.fixDiscount" :disable="prod.discount != 0?true:false" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.fixDiscount, 'fixDiscount', prod)" />
                            </td>

                            <td v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'">
                                <multiselect :options="options" v-model="prod.taxMethod" disabled :show-labels="false" v-bind:placeholder="$t('PurchaseItem.TaxMethod')" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                </multiselect>
                            </td>

                            <td v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'">
                                <taxratedropdown v-model="prod.taxRateId" :isDisable="true" />
                            </td>

                            <td class="text-right">
                                {{currency}} {{parseFloat(prod.lineTotal).toFixed(3).slice(0,-1) }}
                            </td>
                            <td class="border-top-0 text-end">
                                <a href="javascript:void(0);" @click="removeProduct(prod.rowId)"><i class="las la-trash-alt text-secondary font-16"></i></a>                                
                            </td>
                        </tr>
                </tbody>
            </table>
        </div>

        <div class=" table-responsive mt-3"
             v-bind:key="rendered + 'g'"
             v-if="saleProducts.length > 0">
            <table class="table mb-0" style="table-layout:fixed;">
                <thead class="thead-light table-hover" >
                    <tr class="text-right">
                        <th style="width:100px;">
                            {{ $t('SaleReturnItem.NoItem') }}
                        </th>
                        <th class="text-center" style="width:100px;" v-if="isMultiUnit=='true'">
                            {{ $t('SaleReturnItem.TotalCarton') }}
                        </th>
                        <th class="text-center" style="width:100px;" v-if="isMultiUnit=='true'">
                            {{ $t('SaleReturnItem.TotalPieces') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('SaleReturnItem.TotalQty') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('SaleReturnItem.Total') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('SaleReturnItem.Disc') }}
                        </th>
                        <th style="width:160px;">
                            {{ $t('SaleReturnItem.TotalAfterDisc') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('SaleReturnItem.TotalVAT') }}
                        </th>
                        <th style="width:100px;" v-if="summary.bundleAmount > 0">
                            {{ $t('SaleReturnItem.BundleAmount') }}
                        </th>
                        <th style="width:150px;">
                            {{ $t('SaleReturnItem.TotalwithVAT') }}
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right " >
                        <td>
                            {{ summary.item}}
                        </td>
                        <td class="text-center" v-if="isMultiUnit=='true'">
                            {{ summary.totalCarton}}
                        </td>
                        <td class="text-center" v-if="isMultiUnit=='true'">
                            {{ summary.totalPieces}}
                        </td>
                        <td>
                            {{ summary.qty }}
                        </td>

                        <td>
                            {{currency}} {{$formatAmount(summary.total)   }}
                        </td>
                        <td>
                            {{currency}} {{$formatAmount(summary.discount)   }}
                        </td>
                        <td>
                            {{currency}} {{$formatAmount(summary.withDisc)   }}
                        </td>
                        <td>
                            {{currency}} {{$formatAmount((parseFloat(summary.vat)+summary.inclusiveVat).toFixed(3).slice(0,-1))   }}
                        </td>
                        <td v-if="summary.bundleAmount > 0">
                            {{currency}} {{$formatAmount(summary.bundleAmount)  }}
                        </td>
                        <td>
                            {{currency}} {{$formatAmount(summary.withVat)   }}
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <add-serial-model :item="serialItem"
                          :show="showSerial"
                          v-if="showSerial"
                          @update:modelValue="updateSerial"
                          @close="showSerial = false" />
    </div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'

    export default {
        name: "SaleItem",
        props: ['saleItems'],
        mixins: [clickMixin],
        components: {
            Multiselect
        },
        data: function () {
            return {
                isSerial: false,
                decimalQuantity: false,
                rendered: 0,
                product: {
                    id: "",
                },
                products: [],
                currency: '',
                saleProducts: [],
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
                    withVat: 0,
                    bundleAmount: 0,
                    inclusiveVat: 0,
                    totalCarton: 0,
                    totalPieces: 0
                },
                serialItem: '',
                showSerial: false,
                saleDefaultVat: ''
            };
        },
        validations() {},
        filter: {},
        methods: {
            AddSerial: function (item) {
                
                this.serialItem = item;
                this.showSerial = true;
            },

            updateSerial: function (serial, item) {
                
                var prod = this.saleProducts.find(x => x.rowId == item.rowId);
                if (prod != undefined) {
                    prod.serial = serial;
                }
                this.showSerial = false;
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
                }
                else {
                    this.summary.qty = this.saleProducts.reduce((qty, prod) => qty + parseInt(prod.highQty == '' ? 0 : prod.highQty * (prod.unitPerPack == null ? 0 : prod.unitPerPack)) + parseInt(prod.quantity == '' ? 0 : prod.quantity), 0);
                }

                this.summary.total = this.saleProducts.reduce((total, prod) =>
                    total + prod.totalPiece * prod.unitPrice, 0).toFixed(3).slice(0,-1);

                var discount = this.saleProducts.filter((x) => x.discount != 0 || x.discount != "")
                    .reduce((discount, prod) =>
                        discount + (prod.totalPiece * prod.unitPrice * prod.discount) / 100, 0);

                var fixDiscount = this.saleProducts
                    .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "")
                    .reduce((discount, prod) => discount + (prod.fixDiscount * prod.totalPiece), 0);

                this.summary.discount = (parseFloat(discount) + parseFloat(fixDiscount)).toFixed(3).slice(0,-1);

                this.summary.withDisc = (this.summary.total - this.summary.discount).toFixed(3).slice(0, -1);
                
                this.summary.vat = this.saleProducts
                    .reduce((vat, prod) => vat + ((prod.taxMethod == "Exclusive" || prod.taxMethod == "غير شامل") ? ((((prod.unitPrice * prod.totalPiece) -
                        ((prod.unitPrice * prod.totalPiece * prod.discount) / 100) - (prod.totalPiece * prod.fixDiscount)) *
                        parseFloat(prod.rate)) / 100) : 0), 0).toFixed(3).slice(0,-1);

                this.summary.inclusiveVat = this.saleProducts
                    .reduce((vat, prod) => vat + ((prod.taxMethod == "Inclusive" || prod.taxMethod == "شامل") ? ((((prod.unitPrice * prod.totalPiece) -
                        ((prod.unitPrice * prod.totalPiece * prod.discount) / 100) - (prod.totalPiece * prod.fixDiscount)) *
                        parseFloat(prod.rate)) / (100 + parseFloat(prod.rate))) : 0), 0);

                this.summary.withVat = (parseFloat(this.summary.withDisc) + parseFloat(this.summary.vat)).toFixed(3).slice(0,-1);

                //calculate bundle Amount
                //if (this.saleProducts.filter(x => x.isBundleOffer).length > 0) {

                //    //get bundle get qunetity
                //    var bundle = {
                //        item: 0,
                //        qty: 0,
                //        total: 0,
                //        discount: 0,
                //        withDisc: 0,
                //        vat: 0,
                //        withVat: 0,
                //    };

                //    var bundleProducts = this.saleProducts.filter(x => x.isBundleOffer != undefined);

                //    bundle.total = bundleProducts.reduce((total, prod) =>
                //        total + prod.bundleOfferQuantity * prod.unitPrice, 0).toFixed(3).slice(0,-1);

                //    var discountBundle = bundleProducts.filter((x) => x.discount != 0 || x.discount != "")
                //        .reduce((discount, prod) =>
                //            discount + (prod.bundleOfferQuantity * prod.unitPrice * prod.discount) / 100, 0);

                //    var fixDiscountBundle = bundleProducts
                //        .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "")
                //        .reduce((discount, prod) => discount + prod.fixDiscount, 0);

                //    bundle.discount = (parseFloat(discountBundle) + parseFloat(fixDiscountBundle)).toFixed(3).slice(0,-1);

                //    bundle.withDisc = (bundle.total - bundle.discount).toFixed(3).slice(0,-1);

                //    bundle.vat = bundleProducts
                //        .reduce((vat, prod) => vat + (((prod.unitPrice * prod.bundleOfferQuantity) -
                //            ((prod.unitPrice * prod.bundleOfferQuantity * prod.discount) / 100)) *
                //            parseFloat(prod.rate)) / 100, 0).toFixed(3).slice(0,-1);

                //    this.summary.bundleAmount = (parseFloat(bundle.withDisc) + parseFloat(bundle.vat)).toFixed(3).slice(0,-1);
                //} else {
                //    this.summary.bundleAmount = 0;
                //}


                this.$emit("update:modelValue", this.saleProducts);

                this.$emit("summary", this.summary);
            },

            updateLineTotal: function (e, prop, product) {
                
                if (e != undefined) {

                    var discount =
                        product.discount == 0 || product.discount == ""
                            ? product.fixDiscount == 0 || product.fixDiscount == ""
                                ? 0
                                : product.fixDiscount
                            : product.discount;

                    if (prop == "unitPrice") {

                        product.unitPrice = e;
                    }
                    if (prop == "quantity") {
                        if (e <= 0 || e == "") {
                            e = '';
                        }
                        if (String(e).split('.').length > 1 && String(e).split('.')[1].length > 2)
                            e = parseFloat(String(e).slice(0, -1))
                        product.quantity = this.decimalQuantity ? e : Math.round(e);
                    }

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

                    if (prop == "highQty") {
                        if (e < 0 || e == '' || e == undefined) {
                            e = '';
                        }
                        product.highQty = Math.round(e);
                    }
                    if (product.highUnitPrice) {

                        product.totalPiece = (parseFloat(product.highQty == undefined ? 0 : product.highQty) + (parseFloat(product.quantity == '' ? 0 : product.quantity) / parseFloat(product.unitPerPack == null ? 0 : product.unitPerPack)));
                    }
                    else {
                        product.totalPiece = (parseFloat(product.highQty == undefined ? 0 : product.highQty) * parseFloat(product.unitPerPack == null ? 0 : product.unitPerPack)) + parseFloat(product.quantity == '' ? 0 : product.quantity);
                    }
                    //product.totalPiece = (parseFloat(product.highQty == undefined ? 0 : product.highQty) * parseFloat(product.unitPerPack == null ? 0 : product.unitPerPack)) + parseFloat(product.quantity == '' ? 0 : product.quantity);
                    if (product.totalPiece > product.remainingQuantity) {
                        product['outOfStock'] = true
                    } else {
                        product['outOfStock'] = false
                    }

                    discount = product.discount == 0 ? (product.fixDiscount * product.totalPiece)  : (product.totalPiece * product.unitPrice * product.discount) / 100;
                    var vat = this.vats.find((value) => value.id == product.taxRateId);

                    var total = product.totalPiece * product.unitPrice - discount;
                    var calculateVAt = 0;
                    if (product.taxMethod == "Inclusive" || product.taxMethod == "شامل") {
                        calculateVAt = (total * vat.rate) / (100 + vat.rate);
                        //product.lineTotal = total - calculateVAt;
                        product.lineTotal = total;
                    }
                    else {
                        calculateVAt = (total * vat.rate) / 100;
                        product.lineTotal = total + calculateVAt;
                    }


                    product.discountAmount = discount;
                    product.vatAmount = calculateVAt;
                    product.totalAmount = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? product.lineTotal : product.lineTotal + product.vatAmount;
                    product.grossAmount = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? total * 100 / (100 + vat.rate) : total;

                    this.saleProducts['product'] = product

                    this.calcuateSummary();

                    this.$emit("update:modelValue", this.saleProducts);

                }
            },

            addProduct: function (productId) {

                if (this.saleProducts.some(x => x.productId == productId)) {
                    var prd = this.saleProducts.find(x => x.productId == productId);
                    prd.quantity++;
                    this.updateLineTotal(prd.quantity, "quantity", prd);
                } else {
                    var prod = this.products.find((x) => x.id == productId);

                    var rate = this.getVatValue(prod.taxRateId, prod);

                    this.saleProducts.push({
                        rowId: this.createUUID(),
                        productId: prod.id,
                        unitPrice: prod.salePrice,
                        quantity: 1,
                        highQty: 0,
                        discount: 0,
                        fixDiscount: 0,
                        taxMethod: prod.taxMethod,
                        taxRateId: prod.taxRateId,
                        rate: rate,
                        lineTotal: prod.salePrice * 1,
                        levelOneUnit: prod.levelOneUnit,
                        basicUnit: prod.basicUnit,
                        serial: '',
                        guaranteeDate: '',
                    });
                }

                var product = this.saleProducts.find((x) => {
                    return x.productId == productId;
                });

                this.getVatValue(product.taxRateId, product);
                this.updateLineTotal(product.quantity, "quantity", product);
                this.updateLineTotal(product.highQty, "highQty", product);
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
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                
                root.$https
                    .get("/Product/TaxRateList", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.vats = response.data.taxRates;
                        }
                    }).then(function () {

                        
                        if (root.saleItems != undefined) {
                            root.saleItems.forEach(function (item) {
                                if (item.remainingQuantity>0) {
                                    root.products.push(item.product);
                                    var rate = root.getVatValue(item.taxRateId, item);
                                    root.saleProducts.push({
                                        rowId: item.id,
                                        productId: item.productId,
                                        batchNo: item.batchNo,
                                        batchExpiry: item.batchExpiry,
                                        unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                        quantity: item.quantity,
                                        highQty: item.highQty,
                                        discount: item.discount,
                                        fixDiscount: item.fixDiscount,
                                        taxRateId: item.taxRateId,
                                        rate: rate,
                                        taxMethod: item.taxMethod,
                                        remainingQuantity: item.remainingQuantity,
                                        product: item.product,
                                        lineTotal: item.unitPrice * item.quantity,
                                        unitPerPack: item.unitPerPack,
                                        levelOneUnit: item.product.levelOneUnit,
                                        basicUnit: item.product.basicUnit,
                                        serial: item.serial,
                                        guaranteeDate: item.guaranteeDate,
                                        highUnitPrice: item.product.highUnitPrice,
                                    });
                                    var product = root.saleProducts.find((x) => {
                                        return x.productId == item.productId && x.rowId == item.id;
                                    });
                                    root.getVatValue(product.taxRateId, product);
                                    root.updateLineTotal(item.quantity, "quantity", product);
                                    root.updateLineTotal(item.highQty, "highQty", product);
                                    root.product.id = "";
                                    root.rendered++;
                                }
                                
                            });
                            root.$emit("details", root.saleProducts);
                        }
                    });
            },
        },
        created: function () {
            if (this.$i18n.locale == 'en') {
                this.options = ['Inclusive', 'Exclusive'];
            }
            else {
                this.options = ['شامل', 'غير شامل'];
            }
            this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');

            this.getData();
        },
        mounted: function () {
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            this.currency = localStorage.getItem('currency');
            this.decimalQuantity = localStorage.getItem('decimalQuantity') == 'true' ? true : false;
            this.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;
        },
    };
</script>
