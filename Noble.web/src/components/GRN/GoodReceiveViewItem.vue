<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" :style="[itemDisable==true ? {'pointer-events': 'none'} : {'pointer-events': 'auto'}] ">

        <div class=" table-responsive">

            <table class="table add_table_list_bg mt-2" v-if="purchaseProducts.length > 0" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                <thead>
                    <tr>
                        <th style="width: 40px;" class="text-center">
                            #
                        </th>
                        <th style="width: 200px;" class="text-center">
                            {{ $t('PurchaseViewItem.Product') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="!hide">
                            {{ $t('PurchaseViewItem.UnitPrice') }}
                        </th>
                        <th class="text-center" style="width: 70px;" v-if="isValid('CanViewUnitPerPack')">
                            {{ $t('PurchaseViewItem.UnitPerPack') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('PurchaseViewItem.HighQty') }}
                        </th>

                        <!--<th style="width: 50px;" v-if="isMultiUnit=='true'">

                        </th>-->
                        <th style="width: 110px;" class="text-center">
                            {{ $t('PurchaseViewItem.Qty') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('PurchaseViewItem.TOTALQTY') }}
                        </th>
                        <th style="width:110px;" class="text-center" v-if="(purchaseProducts.filter(x => x.isExpire).length > 0 && isFifo) && !po">
                            {{ $t('PurchaseViewItem.ExpDate') }}
                        </th>
                        <th style="width:110px;" class="text-center" v-if="isFifo && !po">
                            {{ $t('PurchaseViewItem.BatchNo') }}
                        </th>
                        <th style="width: 100px;" v-if="purchaseProducts.filter(x => x.guarantee).length > 0  && isSerial && !po">
                            {{ $t('PurchaseViewItem.Guarantee') }}
                        </th>
                        <th style="width: 100px;text-align:center;" v-if="purchaseProducts.filter(x => x.serial).length > 0  && isSerial && !po">
                            {{ $t('PurchaseViewItem.Serial') }}
                        </th>
                        <th style="width: 100px;" class="text-center" v-if="!hide">
                            {{ $t('PurchaseViewItem.Disc%') }}
                        </th>
                        <th style="width: 100px;" class="text-center" v-if="!hide">
                            {{ $t('PurchaseViewItem.FixDisc') }}
                        </th>
                        <!--<th style="width: 100px;">
                            {{ $t('PurchaseOrder.VAT%') }}
                        </th>-->
                        <th style="width: 100px;" class="text-right" v-if="!hide">
                            {{ $t('PurchaseViewItem.LineTotal') }}
                        </th>
                    </tr>
                </thead>
                <tbody id="purchase-item">
                    

                        <tr v-for="(prod , index) in purchaseProducts" :key="prod.productId + index" style="border-bottom: 1px solid #d6d6d6; ">
                            <td class="border-top-0">
                                {{index+1}}
                            </td>
                            <td class="border-top-0 text-center">
                                {{($i18n.locale == 'en' ||isLeftToRight())? products.find(x => x.id == prod.productId).englishName!=''? products.find(x => x.id == prod.productId).englishName : products.find(x => x.id == prod.productId).arabicName :    products.find(x => x.id == prod.productId).arabicName!=''? products.find(x => x.id == prod.productId).arabicName : products.find(x => x.id == prod.productId).englishName}}

                            </td>
                            <td class="border-top-0 text-center" v-if="!hide">
                                {{ parseFloat(prod.unitPrice).toFixed(3).slice(0,-1)}}
                            </td>
                            <td class="text-center" v-if="isValid('CanViewUnitPerPack')">
                                {{prod.unitPerPack}}
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
                            <td class="border-top-0 text-center" v-if="(purchaseProducts.filter(x => x.isExpire).length > 0 && isFifo) && !po">

                                <span v-if="prod.isExpire || isFifo">{{getDate(prod.expiryDate)}}</span>
                                <span v-else>
                                    -
                                </span>
                            </td>
                            <td class="border-top-0 text-center" v-if="isFifo && !po">
                                <span>{{prod.batchNo}}</span>

                            </td>
                            <td class="border-top-0  text-center" v-if="purchaseProducts.filter(x => x.guarantee).length > 0 && isSerial && !po">
                                <span v-if="prod.guarantee">
                                    {{prod.guaranteeDate}}
                                </span>
                                <span v-else>
                                    -
                                </span>
                            </td>
                            <td class="border-top-0 text-center" v-if="purchaseProducts.filter(x => x.serial).length > 0  && isSerial && !po">
                                <span v-if="prod.serial">
                                    {{prod.serialNo}}
                                </span>
                                <span v-else>
                                    -
                                </span>
                            </td>
                            <td class="border-top-0 text-center" v-if="!hide">
                                {{ parseFloat(prod.discount).toFixed(3).slice(0,-1)}}%
                            </td>
                            <td class="border-top-0 text-center" v-if="!hide">
                                {{ parseFloat(prod.fixDiscount).toFixed(3).slice(0,-1)}}
                            </td>
                            <td class="border-top-0 text-right" v-if="!hide">
                                {{currency}}  {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0,-1)) }}
                            </td>
                        </tr>
                    
                </tbody>
            </table>
        </div>

        <hr style="margin-bottom:0;" />
        <div v-if="hide"></div>
        <div v-else>
            <div class=" table-responsive"
                 v-bind:key="rendered + 'g'"
                 v-if="purchaseProducts.length > 0">
                <table class="table table-striped table-hover add_table_list_bg" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                    <thead class="m-0">
                        <tr class="text-right">
                            <th class="text-center" style="width:85px;">
                                {{ $t('PurchaseViewItem.NoItem') }}
                            </th>
                            <th class="text-center" style="width:100px;" v-if="isMultiUnit=='true'">
                                {{ $t('PurchaseViewItem.TotalCarton') }}
                            </th>
                            <th class="text-center" style="width:100px;" v-if="isMultiUnit=='true'">
                                {{ $t('PurchaseViewItem.TotalPieces') }}
                            </th>
                            <th class="text-center" style="width:100px;">
                                {{ $t('PurchaseViewItem.TotalQty') }}
                            </th>
                            <th style="width:100px;">
                                {{ $t('PurchaseViewItem.GrandTotal') }}
                            </th>
                            <th style="width:100px;">
                                {{ $t('PurchaseViewItem.Disc') }}
                            </th>
                            <th style="width:155px;">
                                {{ $t('PurchaseViewItem.TotalAfterDisc') }}
                            </th>
                            <th style="width:100px;">
                                {{ $t('PurchaseViewItem.TotalVAT') }}
                            </th>
                            <th style="width:140px;">
                                {{ $t('PurchaseViewItem.NetTotalWithVat') }}
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
                                {{currency}}   {{parseFloat(summary.total).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                            </td>
                            <td>
                                {{currency}} {{parseFloat(summary.discount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                            </td>
                            <td>
                                {{currency}}   {{parseFloat(summary.withDisc).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                            </td>
                            <td>
                                {{currency}}  {{ (parseFloat(summary.vat)+summary.inclusiveVat).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                            </td>
                            <td>
                                <strong> {{currency}} {{parseFloat(summary.withVAt).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</strong>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
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

    .tableHoverOn {
        background-color: #F4F6FC !important;
    }

    .multiselect__input, .multiselect__single {
        background-color: transparent !important;
    }

    #purchase-item tr td {
        vertical-align: baseline;
    }
</style>

<script>
    import moment from "moment";
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        name: "PurchaseItem",
        props: ['purchase', 'goodReceiveNoteItems', 'raw', 'taxMethod', 'taxRateId', 'hide', 'po'],
        mixins: [clickMixin],
        data: function () {
            return {
                isSerial: false,
                isFifo: false,
                decimalQuantity: false,
                rendered: 0,
                product: {
                    id: "",
                },
                products: [],
                purchaseProducts: [],
                loading: false,
                vats: [],
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
                isMultiUnit: ''
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
            changeProduct: function (NewProdId, rowId) {
                this.purchaseProducts = this.purchaseProducts.filter(x => x.rowId != rowId);
                this.addProduct(NewProdId);

            },
            calcuateSummary: function () {

                var root = this;
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
                    this.summary.qty = this.purchaseProducts.reduce((qty, prod) => qty + parseFloat(prod.totalPiece), 0);
                    this.summary.qty = parseFloat(this.summary.qty).toFixed(2);
                }
                else {
                    this.summary.qty = this.purchaseProducts.reduce((qty, prod) => qty + parseInt(prod.totalPiece), 0);
                }
                this.summary.total = this.purchaseProducts
                    .reduce((total, prod) => total + parseFloat(prod.totalPiece) * prod.unitPrice, 0)
                    .toFixed(3).slice(0, -1);

                var discount = this.purchaseProducts
                    .filter((x) => (x.discount != 0 || x.discount != "") && x.fixDiscount == 0)
                    .reduce(
                        (discount, prod) =>
                            discount + (prod.totalPiece * prod.unitPrice * prod.discount) / 100,
                        0
                    );
                var fixDiscount = this.purchaseProducts
                    .filter((x) => (x.fixDiscount != 0 || x.fixDiscount != "") && x.discount == 0)
                    .reduce((discount, prod) => discount + (prod.totalPiece * prod.fixDiscount), 0);

                this.summary.discount = (parseFloat(discount) + parseFloat(fixDiscount)).toFixed(3).slice(0, -1);
                this.summary.withDisc = (this.summary.total - this.summary.discount).toFixed(
                    2
                );
                this.summary.vat = this.purchaseProducts
                    .reduce((vat, prod) => parseFloat(vat) + ((prod.taxMethod == "Exclusive" || prod.taxMethod == 'غير شامل') ? ((((parseFloat(prod.unitPrice) * parseFloat(prod.totalPiece)) -

                        ((prod.discount == 0 ?
                            (prod.totalPiece * prod.fixDiscount) :
                            (prod.totalPiece * prod.unitPrice * prod.discount) / 100)))

                        * parseFloat(root.getVatValueForSummary(prod.taxRateId, prod))) / 100) : 0),
                        0
                    )
                    .toFixed(3).slice(0, -1);

                this.summary.inclusiveVat = this.purchaseProducts
                    .reduce((vat, prod) => parseFloat(vat) + ((prod.taxMethod == "Inclusive" || prod.taxMethod == 'شامل') ? ((((parseFloat(prod.unitPrice) * parseFloat(prod.totalPiece)) -

                        ((prod.discount == 0 ?
                            (prod.totalPiece * prod.fixDiscount) :
                            (prod.totalPiece * prod.unitPrice * prod.discount) / 100)))

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
                    if (e < 0) {
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
                    product.lowQty = Math.round(e);
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

                product.totalPiece = (parseFloat(product.highQty) * ((product.unitPerPack == null || product.unitPerPack == 0) ? 1 : product.unitPerPack)) + parseFloat(product.quantity);
                discount = product.discount == 0 ? product.totalPiece * product.fixDiscount : (product.totalPiece * product.unitPrice * product.discount) / 100;
                var vat = this.vats.find((value) => value.id == product.taxRateId);

                var total = product.totalPiece * product.unitPrice - discount;
                var calculateVAt = 0;
                if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                    calculateVAt = (total * vat.rate) / (100 + vat.rate);
                    //product.lineTotal = total - calculateVAt;
                    product.lineTotal = total;
                }
                else {
                    calculateVAt = (total * vat.rate) / 100;
                    product.lineTotal = total + calculateVAt;
                }

                this.purchaseProducts['product'] = Product


                this.calcuateSummary();

                this.$emit("update:modelValue", this.purchaseProducts);

            },

            addProduct: function (productId, newProduct) {

                if (this.products.find(x => x.id == newProduct.id) == undefined || this.products.length <= 0) {
                    this.products.push(newProduct);
                }

                var prod = this.products.find((x) => x.id == productId);

                var rate = 0;
                if (this.taxRateId != "00000000-0000-0000-0000-000000000000" && this.taxRateId != undefined) {
                    rate = this.getVatValue(this.taxRateId, prod);
                }

                this.purchaseProducts.push({
                    rowId: this.createUUID(),
                    productId: prod.id,
                    unitPrice: 0,
                    quantity: 0,
                    highQty: 0,
                    totalPiece: 0,
                    levelOneUnit: prod.levelOneUnit,
                    basicUnit: prod.unit == null ? '' : prod.unit.name,
                    unitPerPack: prod.unitPerPack,
                    discount: 0,
                    fixDiscount: 0,
                    taxRateId: this.taxRateId,
                    rate: rate,
                    taxMethod: this.taxMethod,
                    expiryDate: "",
                    isExpire: prod.isExpire,
                    batchNo: "",
                    lineTotal: 0,
                });

                var product = this.purchaseProducts.find((x) => {
                    return x.productId == productId;
                });

                this.getVatValue(product.taxRateId, product);

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


                        if (root.goodReceiveNoteItems != undefined && root.goodReceiveNoteItems.length > 0) {
                            //root.purchaseProducts = root.goodReceiveNoteItems;

                            root.goodReceiveNoteItems.forEach(function (item) {
                                root.purchaseProducts.push({
                                    rowId: item.id,
                                    id: item.id,
                                    batchNo: item.batchNo,
                                    discount: item.discount,
                                    expiryDate: item.expiryDate,
                                    isExpire: item.isExpire,
                                    fixDiscount: item.fixDiscount,
                                    product: item.product,
                                    basicUnit: item.unit == null ? '' : item.unit.name,
                                    productId: item.productId,
                                    purchaseId: item.purchaseId,
                                    quantity: item.quantity,
                                    highQty: item.highQty,
                                    unitPerPack: item.unitPerPack,
                                    taxMethod: item.taxMethod,
                                    taxRateId: item.taxRateId,
                                    unitPrice: item.unitPrice,
                                    serial: item.product.serial,
                                    serialNo: item.serialNo,
                                    guarantee: item.product.guarantee,
                                    levelOneUnit: item.levelOneUnit,
                                    guaranteeDate: (item.guaranteeDate != null && item.guaranteeDate != undefined && item.guaranteeDate != '') ? moment(item.guaranteeDate).format("L") : '',
                                });
                            });

                            for (var l = 0; l < root.purchaseProducts.length; l++) {
                                root.products.push(root.purchaseProducts[l].product);
                                root.updateLineTotal(root.purchaseProducts[l].quantity, "quantity", root.purchaseProducts[l]);
                                if (root.isMultiUnit) {
                                    root.updateLineTotal(root.purchaseProducts[l].highQty, "highQty", root.purchaseProducts[l]);
                                }

                                root.updateLineTotal(root.purchaseProducts[l].unitPerPack, "unitPerPack", root.purchaseProducts[l]);
                                root.updateLineTotal(root.purchaseProducts[l].unitPrice, "unitPrice", root.purchaseProducts[l]);
                                root.updateLineTotal(root.purchaseProducts[l].discount, "discount", root.purchaseProducts[l]);
                                root.updateLineTotal(root.purchaseProducts[l].fixDiscount, "fixDiscount", root.purchaseProducts[l]);
                                //    root.updateLineTotal(root.purchaseProducts[l].expiryDate, "expiryDate", root.purchaseProducts[l]);
                            }
                            root.calcuateSummary()
                        }
                        if (root.purchase != undefined) {



                            if (root.purchase.goodReceiveNoteItems != undefined) {
                                //Purchase Return Edit
                                root.purchase.goodReceiveNoteItems.forEach(function (item) {
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
                                        unitPerPack: item.unitPerPack,
                                        taxMethod: item.taxMethod,
                                        taxRateId: item.taxRateId,
                                        unitPrice: item.unitPrice,
                                        serial: item.product.serial,
                                        serialNo: item.serialNo,
                                        guarantee: item.product.guarantee,
                                        levelOneUnit: item.levelOneUnit,
                                        guaranteeDate: (item.guaranteeDate != null && item.guaranteeDate != undefined && item.guaranteeDate != '') ? moment(item.guaranteeDate).format("L") : '',
                                        basicUnit: item.unit == null ? '' : item.unit.name
                                    });
                                });

                                for (var j = 0; j < root.purchaseProducts.length; j++) {
                                    root.products.push(root.purchaseProducts[j].product);
                                    root.updateLineTotal(root.purchaseProducts[j].quantity, "quantity", root.purchaseProducts[j]);
                                    if (root.isMultiUnit) {
                                        root.updateLineTotal(root.purchaseProducts[j].highQty, "highQty", root.purchaseProducts[j]);
                                    }

                                    root.updateLineTotal(root.purchaseProducts[j].unitPerPack, "unitPerPack", root.purchaseProducts[j]);
                                    root.updateLineTotal(root.purchaseProducts[j].unitPrice, "unitPrice", root.purchaseProducts[j]);
                                    root.updateLineTotal(root.purchaseProducts[j].discount, "discount", root.purchaseProducts[j]);
                                    root.updateLineTotal(root.purchaseProducts[j].fixDiscount, "fixDiscount", root.purchaseProducts[j]);
                                }
                                root.calcuateSummary()
                            }
                        }

                        else if (root.$route.query.data != undefined) {
                            //Purchase Invoice Edit

                            if (root.$route.query.data.goodReceiveNoteItems != undefined) {

                                root.purchaseProducts = root.$route.query.data.goodReceiveNoteItems;
                                for (var i = 0; i < root.purchaseProducts.length; i++) {
                                    root.products.push(root.purchaseProducts[i].product);
                                    root.updateLineTotal(root.purchaseProducts[i].quantity, "quantity", root.purchaseProducts[i]);
                                    root.updateLineTotal(root.purchaseProducts[i].unitPrice, "unitPrice", root.purchaseProducts[i]);
                                    root.updateLineTotal(root.purchaseProducts[i].discount, "discount", root.purchaseProducts[i]);
                                    root.updateLineTotal(root.purchaseProducts[i].fixDiscount, "fixDiscount", root.purchaseProducts[i]);
                                }
                                root.calcuateSummary()
                            }
                            else if (root.$route.query.data.purchaseOrderItems != undefined) {
                                //Purchase Order Edit

                                root.$route.query.data.purchaseOrderItems.forEach(function (item) {
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
                                        unitPerPack: item.unitPerPack,
                                        taxMethod: item.taxMethod,
                                        taxRateId: item.taxRateId,
                                        unitPrice: item.unitPrice,
                                        serial: item.product.serial,
                                        serialNo: item.serialNo,
                                        guarantee: item.product.guarantee,
                                        basicUnit: item.unit == null ? '' : item.unit.name,
                                        guaranteeDate: (item.guaranteeDate != null && item.guaranteeDate != undefined && item.guaranteeDate != '') ? moment(item.guaranteeDate).format("L") : '',
                                    });
                                });

                                for (var k = 0; k < root.purchaseProducts.length; k++) {
                                    root.products.push(root.purchaseProducts[k].product);

                                    root.updateLineTotal(root.purchaseProducts[k].quantity, "quantity", root.purchaseProducts[k]);
                                    if (root.isMultiUnit) {
                                        root.updateLineTotal(root.purchaseProducts[k].highQty, "highQty", root.purchaseProducts[k]);
                                    }
                                    root.updateLineTotal(root.purchaseProducts[k].highQty, "highQty", root.purchaseProducts[k]);
                                    root.updateLineTotal(root.purchaseProducts[k].unitPerPack, "unitPerPack", root.purchaseProducts[k]);
                                    root.updateLineTotal(root.purchaseProducts[k].unitPrice, "unitPrice", root.purchaseProducts[k]);
                                    root.updateLineTotal(root.purchaseProducts[k].discount, "discount", root.purchaseProducts[k]);
                                    root.updateLineTotal(root.purchaseProducts[k].fixDiscount, "fixDiscount", root.purchaseProducts[k]);
                                }
                                root.calcuateSummary()
                            }
                        }
                    });
            },
            getTotalAmount: function () {
                return this.summary.withVAt;
            },
        },
        created: function () {
            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            this.getData();
        },
        mounted: function () {
            this.decimalQuantity = localStorage.getItem('decimalQuantity') == 'true' ? true : false;
            this.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;

            if (token == '') {
                this.currency = localStorage.getItem('currency');
                this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            }
        },
    };
</script>
