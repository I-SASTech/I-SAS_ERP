<template>
    <div  :style="[itemDisable==true ? {'pointer-events': 'none'} : {'pointer-events': 'auto'}] ">
        <div class=" table-responsive mt-3">
            <table class="table">
                <thead class="thead-light table-hover">
                    <tr>
                        <th style="width: 30px;">
                            #
                        </th>
                        <th style="width: 200px;">
                            {{ $t('PurchaseCostingItem.Product') }}
                        </th>
                        <th style="width: 110px;" class="text-center">
                            {{ $t('PurchaseCostingItem.UnitPrice') }}
                        </th>
                        <th style="width: 110px;" class="text-center">
                            Costing
                        </th>
                        <th style="width: 110px;" class="text-center">
                            Sale Price
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('PurchaseCostingItem.HighQty') }}
                        </th>
                        <th style="width: 90px;" class="text-center">
                            {{ $t('PurchaseCostingItem.Qty') }}
                        </th>
                        <th style="width: 90px;" class="text-center" v-if="po && purchaseid!='00000000-0000-0000-0000-000000000000' && internationalPurchase=='true'">
                            R.Qty
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('PurchaseCostingItem.TOTALQTY') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="purchase != undefined">
                            {{ $t('PurchaseCostingItem.CurrentQty') }}
                        </th>
                        
                        <th style="width:110px;" class="text-center" v-if="purchaseProducts.filter(x => x.isExpire).length > 0 && isFifo">
                            {{ $t('PurchaseCostingItem.ExpDate') }}
                        </th>
                        <th style="width:110px;" class="text-center" v-if="isFifo">
                            {{ $t('PurchaseCostingItem.BatchNo') }}
                        </th>
                        <th style="width: 80px;" class="text-center">
                            {{ $t('PurchaseCostingItem.Disc%') }}
                        </th>
                        <th style="width: 80px;" class="text-center">
                            {{ $t('PurchaseCostingItem.FixDisc') }}
                        </th>
                        <th style="width: 100px;" class="text-right">
                            {{ $t('PurchaseCostingItem.LineTotal') }}
                        </th>
                        <th style="width: 40px"></th>
                    </tr>
                </thead>
                <tbody id="purchase-item">
                    
                        <tr v-for="(prod , index) in purchaseProducts" :key="prod.productId + index" v-bind:class="{'alert-danger':prod.outOfStock}">
                            <td class="border-top-0">
                                {{index+1}}
                            </td>
                            <td class="border-top-0">
                                {{($i18n.locale == 'en' ||isLeftToRight())? products.find(x => x.id == prod.productId).englishName!=''? products.find(x => x.id == prod.productId).englishName : products.find(x => x.id == prod.productId).arabicName :    products.find(x => x.id == prod.productId).arabicName!=''? products.find(x => x.id == prod.productId).arabicName : products.find(x => x.id == prod.productId).englishName}}
                            </td>
                            <td class="border-top-0 text-center" v-if="!hide">
                                {{ parseFloat(prod.unitPrice).toFixed(3).slice(0,-1)}}
                            </td>
                            <td class="border-top-0 text-center" v-if="!hide">
                                {{ parseFloat(prod.unitExpense).toFixed(3).slice(0,-1)}}
                            </td>
                            <td class="border-top-0">
                                <decimal-to-fixed v-model="prod.salePrice"  />
                            </td>
                            <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                                {{prod.highQty}}
                                <small style="font-weight: 500;font-size:70%;">
                                    {{prod.levelOneUnit}}
                                </small>
                            </td>

                            <td class="border-top-0 text-center">
                                {{prod.quantity}}
                                <small v-if="isMultiUnit=='true'" style="font-weight: 500;font-size:70%;">
                                    {{prod.basicUnit}}
                                </small>
                            </td>
                            <td class="border-top-0 text-center" v-if="po && purchaseid!='00000000-0000-0000-0000-000000000000' && internationalPurchase=='true'">
                                <input type="number" v-model="prod.receiveQuantity"
                                       @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                       class="form-control input-border text-center tableHoverOn"
                                       @keyup="updateLineTotal($event.target.value, 'receiveQuantity', prod)" />
                                <small v-if="isMultiUnit=='true'" style="font-weight: 500;font-size:70%;">
                                    {{prod.basicUnit}}
                                </small>
                            </td>
                            <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                                {{prod.totalPiece}}
                            </td>
                            <td class="border-top-0 text-center" v-if="purchase != undefined">
                                {{prod.inventory.currentQuantity}}
                            </td>
                            <td class="border-top-0 text-center" v-if="purchaseProducts.filter(x => x.isExpire).length > 0 && isFifo">
                                
                                <span v-if="prod.isExpire || isFifo">{{getDate(prod.expiryDate)}}</span>
                                <span v-else>
                                    -
                                </span>
                            </td>
                            <td class="border-top-0 text-center" v-if="isFifo">
                                {{prod.batchNo}}
                            </td>
                            <td class="border-top-0 text-center" v-if="!hide">
                                {{ parseFloat(prod.discount).toFixed(3).slice(0,-1)}}%
                            </td>
                            <td class="border-top-0 text-center" v-if="!hide">
                                {{ parseFloat(prod.fixDiscount).toFixed(3).slice(0,-1)}}
                            </td>
                            <td class="border-top-0 text-right" v-if="!hide">
                                {{currency}}  {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0,-1))  }}
                            </td>
                            <td class="border-top-0 pt-0 text-end">
                                    <i class="las la-trash-alt text-secondary font-16"  @click="removeProduct(prod.rowId)"
                                        title="Remove Item"></i>
                            </td>
                        </tr>
                    
                </tbody>
            </table>
        </div>

        <!--<product-dropdown @update:modelValues="addProduct" width="100%" :raw="raw" v-if="purchase==undefined && (purchaseOrderId==''  || purchaseOrderId==null)" />-->
        
        <div class=" table-responsive mt-3"
             v-bind:key="rendered + 'g'">
            <table class="table">
                <thead class="thead-light table-hover">
                    <tr class="text-right">
                        <th class="text-center" style="width:85px;">
                            {{ $t('PurchaseCostingItem.NoItem') }}
                        </th>
                        <th class="text-center" style="width:100px;">
                            {{ $t('PurchaseCostingItem.TotalQty') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('PurchaseCostingItem.GrandTotal') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('PurchaseCostingItem.Disc') }}
                        </th>
                        <th style="width:155px;">
                            {{ $t('PurchaseCostingItem.TotalAfterDisc') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('PurchaseCostingItem.TotalVAT') }}
                        </th>
                        <th style="width:140px;">
                            {{ $t('PurchaseCostingItem.NetTotalWithVat') }}
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right">
                        <td class="text-center">
                            {{ summary.item }}
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
</template>


<script>
import * as moment from "moment";

    //import moment from "moment";
    //import Vue3Barcode from 'vue3-barcode'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: "PurchaseItem",
        props: ['purchase', 'purchaseItems', 'raw', 'taxMethod', 'taxRateId', 'isSerial', 'po', 'purchaseid', 'purchaseOrderId', 'hide'],

        data: function () {
            return {
                isFifo: false,
                decimalQuantity: false,
                internationalPurchase: '',
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
                    inclusiveVat: 0
                },
                currency: '',
                searchTerm: '',
                isMultiUnit: '',
                wareRendered: 0,
                isRaw: false,
                productList: [],
                unitExpenseRender: 0
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

        created() {
            //this.$barcodeScanner.init(this.onBarcodeScanned);
            //For Scanner Code 
           
            this.getData();


        },
        methods: {
            getDate: function (x) {
                return moment(x).format('DD/MM/yyyy')
            },
            GetProductList: function () {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                this.isRaw = this.raw == undefined ? false : this.raw;
                //search = search == undefined ? '' : search;
                // var url = this.wareHouseId != undefined ? "/Product/GetProductInformation?searchTerm=" + search + '&wareHouseId=' + this.wareHouseId + "&isDropdown=true" + '&isRaw=' + root.isRaw : "/Product/GetProductInformation?searchTerm=" + search + '&status=' + root.status + "&isDropdown=true" + '&isRaw=' + root.isRaw;

                this.$https
                    .get("/Product/GetProductBarcode?isRaw=" + root.isRaw, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.productList = response.data.results.products;

                        }
                    });


            },
            onBarcodeScanned(barcode) {


                if (!this.itemDisable) {
                    var root = this;
                    if (root.productList.length > 0) {
                        var product = this.productList.find(x => x.barCode == barcode)
                        if (product != null) {
                            root.addProduct(product.id, product)
                        }
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
                if (this.decimalQuantity) {
                    this.summary.qty = this.purchaseProducts.reduce(
                        (qty, prod) => qty + parseFloat(prod.totalPiece), 0);
                }
                else {
                    this.summary.qty = this.purchaseProducts.reduce(
                        (qty, prod) => qty + parseInt(prod.totalPiece), 0);
                }
                

                this.summary.total = this.purchaseProducts
                    .reduce((total, prod) => total + parseInt(prod.totalPiece) * prod.unitPrice, 0)
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
                    .reduce((vat, prod) => parseFloat(vat) + ((prod.taxMethod == "Exclusive" || prod.taxMethod == 'غير شامل') ? ((((parseFloat(prod.unitPrice == '' ? 0 : prod.unitPrice) * parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece)) -

                        ((prod.discount == 0 ?
                            (prod.totalPiece * prod.fixDiscount) :
                            ((prod.totalPiece == '' ? 0 : prod.totalPiece) * (prod.unitPrice == '' ? 0 : prod.unitPrice) * prod.discount) / 100)))

                        * parseFloat(root.getVatValueForSummary(prod.taxRateId, prod))) / 100) : 0),
                        0
                    )
                    .toFixed(3).slice(0, -1);

                this.summary.inclusiveVat = this.purchaseProducts
                    .reduce((vat, prod) => parseFloat(vat) + ((prod.taxMethod == "Inclusive" || prod.taxMethod == 'شامل') ? ((((parseFloat(prod.unitPrice == '' ? 0 : prod.unitPrice) * parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece)) -

                        ((prod.discount == 0 ?
                            (prod.totalPiece * prod.fixDiscount) :
                            (prod.totalPiece * (prod.unitPrice == '' ? 0 : prod.unitPrice) * prod.discount) / 100)))

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
                debugger; //eslint-disable-line

                var discount = product.discount == 0 || product.discount == "" ? product.fixDiscount == 0 || product.fixDiscount == "" ? 0 : product.fixDiscount : product.discount;

                if (prop == "unitPrice") {
                    product.unitPrice = e;

                }
                
                if (prop == "unitExpense") {
                    product.unitExpense = e;

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
                    product.highQty = e;
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
                
                product.totalPiece = (parseFloat(product.highQty == '' ? 0 : product.highQty) * (product.unitPerPack == null ? 0 : product.unitPerPack)) + parseFloat(product.quantity == '' ? 0 : product.quantity);
                discount = product.discount == 0 ? product.totalPiece * product.fixDiscount : (product.totalPiece * product.unitPrice * product.discount) / 100;

                if (this.purchase != undefined) {
                    if (this.purchase.isPurchaseReturn) {
                        if (product.totalPiece > product.remainingQuantity || product.totalPiece > product.inventory.currentQuantity) {
                            product['outOfStock'] = true;
                        } else {
                            product['outOfStock'] = false;
                        }
                    }
                }

                var vat = this.vats.find((value) => value.id == product.taxRateId);
                var total = product.totalPiece * product.unitPrice - discount;
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
                    unitPrice: '0',
                    quantity: 1,
                    receiveQuantity: 0,
                    highQty: 0,
                    totalPiece: 0,
                    remainingQuantity: 0,
                    levelOneUnit: prod.levelOneUnit,
                    basicUnit: prod.unit == null ? '' : prod.unit.name,
                    unitPerPack: prod.unitPerPack == null ? 0 : prod.unitPerPack,
                    discount: 0,
                    fixDiscount: 0,
                    taxRateId: this.taxRateId,
                    rate: rate,
                    taxMethod: this.taxMethod,
                    expiryDate: "",
                    isExpire: prod.isExpire,
                    batchNo: "",
                    lineTotal: 0,
                    guarantee: prod.guarantee,
                    serial: prod.serial,
                    serialNo: '',
                    guaranteeDate: '',
                    inventory: prod.inventory != null ? prod.inventory.currentQuantity : 0,
                    //    wareHouseId: this.wareHouseId,
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
            updateExpense: function (e, productId) {
                
                var product = this.purchaseProducts.find(x => x.rowId == productId);

                product.unitExpense = product.unitPrice + e;
                this.$emit("update:modelValue", this.purchaseProducts);
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
                        
                        if (root.purchaseItems != undefined && root.purchaseItems.length > 0) {
                            
                            root.purchaseItems.forEach(function (item) {
                                root.purchaseProducts.push({
                                    rowId: item.id,
                                    id: item.id,
                                    batchNo: item.batchNo,
                                    discount: item.discount,
                                    expiryDate: item.expiryDate,
                                    isExpire: item.product.isExpire,
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
                                    serial: item.product.serial,
                                    serialNo: item.serialNo,
                                    guarantee: item.product.guarantee,
                                    guaranteeDate: item.guaranteeDate,
                                    unitExpense: item.unitExpense,
                                    salePrice: item.product.salePrice,
                                    unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                });
                            });

                            for (var l = 0; l < root.purchaseProducts.length; l++) {
                                root.products.push(root.purchaseProducts[l].product);
                                root.updateLineTotal(root.purchaseProducts[l].quantity, "quantity", root.purchaseProducts[l]);
                                root.updateLineTotal(root.purchaseProducts[l].highQty, "highQty", root.purchaseProducts[l]);
                                root.updateLineTotal(root.purchaseProducts[l].unitPerPack, "unitPerPack", root.purchaseProducts[l]);
                                root.updateLineTotal(root.purchaseProducts[l].unitPrice, "unitPrice", root.purchaseProducts[l]);
                                root.updateLineTotal(root.purchaseProducts[l].discount, "discount", root.purchaseProducts[l]);
                                root.updateLineTotal(root.purchaseProducts[l].fixDiscount, "fixDiscount", root.purchaseProducts[l]);
                                //    root.updateLineTotal(root.purchaseProducts[l].expiryDate, "expiryDate", root.purchaseProducts[l]);
                            }
                            root.calcuateSummary()
                        }
                        else if (root.$route.query.data != undefined) {
                            //Purchase Invoice Edit
                            
                            if (root.$route.query.data.purchaseItems != undefined) {

                                root.purchaseProducts = root.$route.query.data.purchaseItems;
                                for (var i = 0; i < root.purchaseProducts.length; i++) {
                                    root.products.push(root.purchaseProducts[i].product);
                                    root.updateLineTotal(root.purchaseProducts[i].quantity, "quantity", root.purchaseProducts[i]);
                                    root.updateLineTotal(root.purchaseProducts[i].unitPrice, "unitPrice", root.purchaseProducts[i]);
                                    root.updateLineTotal(root.purchaseProducts[i].discount, "discount", root.purchaseProducts[i]);
                                    root.updateLineTotal(root.purchaseProducts[i].fixDiscount, "fixDiscount", root.purchaseProducts[i]);
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

        mounted: function () {
            this.decimalQuantity = localStorage.getItem('decimalQuantity') == 'true' ? true : false;
            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            this.internationalPurchase = localStorage.getItem('InternationalPurchase');
            this.GetProductList();
                this.currency = localStorage.getItem('currency');
                this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            
        },
    };
</script>

