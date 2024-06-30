<template>
    <div class="col-lg-12">
        <div class=" table-responsive mt-3">
            <table class="table mb-0" style="table-layout:fixed;">
                <thead class="thead-light">
                    <tr>
                        <th style="width: 30px;">
                            #
                        </th>
                        <th style="width: 200px;">
                            {{ $t('PurchaseItem.Product') }}
                        </th>
                        <!--<th style="width: 200px;">
                            {{ $t('PurchaseOrder.WareHouse') }}
                        </th>-->
                        <th style="width: 110px;" class="text-center">
                            {{ $t('PurchaseItem.UnitPrice') }}
                        </th>
                        <th class="text-center" style="width: 70px;" v-if="isValid('CanViewUnitPerPack')">
                            {{ $t('PurchaseItem.UnitPerPack') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('PurchaseItem.HighQty') }}
                        </th>
                        <!--<th style="width: 50px;" v-if="isMultiUnit=='true'">

                        </th>-->
                        <th style="width: 90px;" class="text-center">
                            PO Quantity
                        </th>
                        <th style="width: 90px;" class="text-center">
                            {{ $t('PurchaseItem.Qty') }}
                        </th>
                        <!--<th style="width: 90px;" class="text-center" v-if="po && purchaseid!='00000000-0000-0000-0000-000000000000'">
                            R.Qty
                        </th>-->
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('PurchaseItem.TOTALQTY') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="purchase != undefined">
                            {{ $t('PurchaseItem.CurrentQty') }}
                        </th>
                        <th style="width:110px;" class="text-center" v-if="(purchaseProducts.filter(x => x.isExpire).length > 0 && isFifo) && !po">
                            {{ $t('PurchaseItem.ExpDate') }}
                        </th>
                        <th style="width:110px;" class="text-center" v-if="isFifo && !po">
                            {{ $t('PurchaseItem.BatchNo') }}
                        </th>
                     
                        <th style="width:150px;" class="text-center" v-if="purchaseProducts.filter(x => x.guarantee).length > 0  && isSerial && !po">
                            {{ $t('PurchaseItem.WarrantyType') }}
                        </th>
                        <th style="width: 100px;" v-if="purchaseProducts.filter(x => x.guarantee).length > 0  && isSerial && !po">
                            {{ $t('PurchaseItem.Guarantee') }}
                        </th>
                        <th style="width: 100px;" class="text-center" v-if="purchaseProducts.filter(x => x.serial).length > 0  && isSerial && !po">
                            {{ $t('PurchaseItem.Serial') }}
                        </th>
                        <th style="width: 80px;" class="text-center">
                            {{ $t('PurchaseItem.Disc%') }}
                        </th>
                        <th style="width: 80px;" class="text-center">
                            {{ $t('PurchaseItem.FixDisc') }}
                        </th>
                        <th style="width: 100px;" class="text-end">
                            {{ $t('PurchaseItem.LineTotal') }}
                        </th>
                        <th style="width: 40px" class="text-end"></th>
                    </tr>
                </thead>
                <tbody>
                    
                        <tr v-for="(prod , index) in purchaseProducts" :key="prod.productId + index" v-bind:class="{'alert-danger':prod.outOfStock}" class="tble_border_remove">
                            <td class="border-top-0">
                                {{index+1}}
                            </td>
                            <td class="border-top-0">
                                {{($i18n.locale == 'en' ||isLeftToRight())? products.find(x => x.id == prod.productId).englishName!=''? products.find(x => x.id == prod.productId).englishName : products.find(x => x.id == prod.productId).arabicName :    products.find(x => x.id == prod.productId).arabicName!=''? products.find(x => x.id == prod.productId).arabicName : products.find(x => x.id == prod.productId).englishName}}
                            </td>
                            <td class="border-top-0">
                                <decimal-to-fixed v-model="prod.unitPrice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.unitPrice, 'unitPrice', prod)" />
                            </td>
                            <td class="text-center" v-if="isValid('CanViewUnitPerPack')">
                                {{prod.unitPerPack}}
                            </td>
                            <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                                <input type="number" v-model="prod.highQty"
                                       style=""
                                       @focus="$event.target.select()"
                                       class="form-control "
                                       @keyup="updateLineTotal($event.target.value, 'highQty', prod)" />
                                <small style="font-weight: 500;font-size:70%;">
                                    {{prod.levelOneUnit}}
                                </small>
                            </td>

                            <td class="border-top-0 text-center">
                                {{prod.poQuantity}}
                            </td>
                            <td class="border-top-0 text-center">
                                <input type="number" v-model="prod.quantity"
                                       style=""
                                       @focus="$event.target.select()"
                                       class="form-control "
                                       @keyup="updateLineTotal($event.target.value, 'quantity', prod)" />
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
                            <td class="border-top-0 text-center" v-if="(purchaseProducts.filter(x => x.isExpire).length > 0 && isFifo)  && !po">
                                <datepicker v-if="prod.isExpire || isFifo" v-model="prod.expiryDate" />
                                <span v-else>
                                    -
                                </span>
                            </td>
                            <td class="border-top-0 text-center" v-if="isFifo && !po">
                                <input type="text" v-model="prod.batchNo"
                                       @focus="$event.target.select()"
                                       class="form-control " />
                            </td>
                          

                            <td class="border-top-0 text-center" v-if="purchaseProducts.filter(x => x.guarantee).length > 0 && isSerial && !po">
                                <warranty-type-dropdown v-if="prod.guarantee" v-model="prod.warrantyTypeId" :modelValue="prod.warrantyTypeId" />
                                <span v-else>
                                    -
                                </span>
                            </td>

                            <td class="border-top-0  text-center" v-if="purchaseProducts.filter(x => x.guarantee).length > 0 && isSerial && !po">
                                <datepicker v-if="prod.guarantee" v-model="prod.guaranteeDate" />
                                <span v-else>
                                    -
                                </span>
                            </td>
                            <td class="border-top-0 text-center" v-if="purchaseProducts.filter(x => x.serial).length > 0  && isSerial && !po">
                                <input type="text"
                                       v-model="prod.serialNo"
                                       v-if="prod.serial"
                                       @focus="$event.target.select()"
                                       class="form-control " />
                                <span v-else>
                                    -
                                </span>
                            </td>
                            <td class="border-top-0">
                                <decimal-to-fixed v-model="prod.discount" v-bind:disable="prod.fixDiscount != 0?true:false" v-bind:salePriceCheck="false" @update::="updateLineTotal(prod.discount, 'discount', prod)" />
                            </td>
                            <td class="border-top-0">
                                <decimal-to-fixed v-model="prod.fixDiscount" :disable="prod.discount != 0?true:false" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.fixDiscount, 'fixDiscount', prod)" />
                            </td>
                            <!--<td class="border-top-0">
                                <taxratedropdown v-model="prod.taxRateId"
                                                 @update:modelValue="getVatValue($event, prod)" :dropdownpo="'dropdownpo'"></taxratedropdown>
                            </td>-->
                            <td class="border-top-0 text-end">
                                {{currency}}  {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0,-1))  }}
                            </td>
                            <td class="border-top-0 pt-0 text-end">
                                <button @click="removeProduct(prod.rowId)"
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
                <div class="mt-4" v-if="purchase==undefined && (purchaseOrderId==''  || purchaseOrderId==null)">
                    <product-dropdown v-bind:key="rendered"
                                      @update:modelValues="addProduct"
                                      width="100%" />
                </div>
                <div class="mt-4" v-else>
                    <product-dropdown :wareHouseId="wareHouseId"
                                      :raw="false"
                                      @update:modelValues="addProduct"
                                      width="100%" />
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 ">
                <div class="mt-4" v-bind:key="rendered + 'g'">
                    <table class="table" style="background-color: #f1f5fa;">
                        <tbody>
                            <tr>
                                <td colspan="2" style="width:65%;">
                                    <span class="fw-bold">{{ $t('PurchaseItem.SubTotal') }}  </span>
                                    <span>(Tax {{taxMethod}})</span>
                                </td>
                                <td class="text-end" style="width:35%;">{{summary.withDisc}}</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:65%;" class="fw-bold">
                                    <span style="height:33px !important; ">{{ $t('PurchaseItem.Discount') }}</span>

                                </td>
                                <td style="width:35%;">
                                    <div class="text-end">
                                        {{summary.discount}}
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:65%;" class="fw-bold">
                                    <span style="height:33px !important; ">{{ $t('PurchaseItem.TotalVAT') }}</span>

                                </td>
                                <td style="width:35%;">
                                    <div class="text-end">
                                        {{currency}}  {{ (parseFloat(summary.vat)+summary.inclusiveVat).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2" style="width:65%;">
                                    <span style="font-weight:bolder; font-size:16px">{{ $t('PurchaseItem.TotalDue') }}({{currency}})</span>
                                </td>
                                <td class="text-end" style="width: 35%; font-weight: bolder; font-size: 16px">{{summary.withVAt}}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

        </div>


    </div>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        name: "PurchaseItem",
        props: ['purchase', 'purchaseItems', 'raw', 'taxMethod', 'taxRateId', 'po', 'purchaseid', 'purchaseOrderId'],
        mixins: [clickMixin],
        data: function () {
            return {
                isSerial: false,
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
                    inclusiveVat: 0,
                    totalCarton: 0,
                    totalPieces: 0
                },
                currency: '',
                searchTerm: '',
                isMultiUnit: '',
                wareRendered: 0,
                isRaw: false,
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

                if (localStorage.getItem("BarcodeScan") != 'Purchase')
                    return
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

                var discount = product.discount == 0 || product.discount == "" ? product.fixDiscount == 0 || product.fixDiscount == "" ? 0 : product.fixDiscount : product.discount;

                if (prop == "unitPrice") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = 0;
                    }
                    product.unitPrice = e;

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
                    if (this.purchase != undefined) {
                        if (product.totalPiece > product.remainingQuantity || product.totalPiece > product.inventory.currentQuantity) {
                            product['outOfStock'] = true
                            
                        } else {
                            product['outOfStock'] = false

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
                    product.lineTotal = total;
                }

                product.discountAmount = discount;
                product.vatAmount = calculateVAt;
                product.totalAmount = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? product.lineTotal : product.lineTotal + product.vatAmount;
                product.grossAmount = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? total * 100 / (100 + vat.rate) : total;

                this.purchaseProducts['product'] = product;
                
                this.calcuateSummary();
                this.$emit("update:modelValue", this.purchaseProducts);

            },

            addProduct: function (productId, newProduct, tempItem, isTemp, poVatId, poTaxMethod) {
                var prd = this.purchaseProducts.find(x => x.productId == productId);
                if (prd != undefined) {
                    prd.quantity++;
                    this.updateLineTotal(prd.quantity, "quantity", prd);
                }
                else {

                    this.products.push(newProduct);
                    var prod = this.products.find((x) => x.id == productId);
                    var rate = 0;

                    if (isTemp) {
                        if (poVatId != "00000000-0000-0000-0000-000000000000" && poVatId != undefined) {
                            rate = this.getVatValue(poVatId, prod);
                        }
                        this.purchaseProducts.push({
                            rowId: this.createUUID(),
                            productId: prod.id,
                            unitPrice: tempItem.unitPrice,
                            quantity: tempItem.quantity,
                            poQuantity: tempItem.poQuantity,
                            receiveQuantity: 0,
                            highQty: tempItem.highQty,
                            totalPiece: 0,
                            remainingQuantity: 0,
                            levelOneUnit: prod.levelOneUnit,
                            basicUnit: prod.unit == null ? '' : prod.unit.name,
                            unitPerPack: tempItem.unitPerPack,
                            discount: tempItem.discount,
                            fixDiscount: tempItem.fixDiscount,
                            taxRateId: poVatId,
                            rate: rate,
                            taxMethod: poTaxMethod,
                            expiryDate: "",
                            isExpire: prod.isExpire,
                            batchNo: "",
                            lineTotal: 0,
                            guarantee: prod.guarantee,
                            serial: prod.serial,
                            serialNo: '',
                            guaranteeDate: '',
                            inventory: prod.inventory != null ? prod.inventory.currentQuantity : 0
                        });
                    }
                    else {
                        if (this.taxRateId != "00000000-0000-0000-0000-000000000000" && this.taxRateId != undefined) {
                            rate = this.getVatValue(this.taxRateId, prod);
                        }
                        this.purchaseProducts.push({
                            rowId: this.createUUID(),
                            productId: prod.id,
                            unitPrice: '0',
                            quantity: 0,
                            poQuantity: 0,

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
                            warrantyTypeId: '',
                            inventory: prod.inventory != null ? prod.inventory.currentQuantity : 0,
                        });
                    }


                    var product = this.purchaseProducts.find((x) => {
                        return x.productId == productId;
                    });

                    this.getVatValue(product.taxRateId, product);

                    this.product.id = "";
                    this.rendered++;
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
                                    levelOneUnit: item.product.levelOneUnit,
                                    product: item.product,
                                    productId: item.productId,
                                    purchaseId: item.purchaseId,
                                    quantity: item.quantity,
                                    poQuantity: item.poQuantity,
                                    highQty: item.highQty,
                                    unitPerPack: item.unitPerPack,
                                    taxMethod: item.taxMethod,
                                    taxRateId: item.taxRateId,
                                    serial: item.product.serial,
                                    serialNo: item.serialNo,
                                    guarantee: item.product.guarantee,
                                    guaranteeDate: item.guaranteeDate,
                                    basicUnit: item.product.basicUnit,
                                    unitPrice: item.unitPrice,
                                    warrantyTypeId: item.warrantyTypeId,
                                    totalPiece: 0
                                });
                            });

                            for (var l = 0; l < root.purchaseProducts.length; l++) {
                                root.products.push(root.purchaseProducts[l].product);
                                root.updateLineTotal(root.purchaseProducts[l].quantity, "quantity", root.purchaseProducts[l]);
                                if (root.isMultiUnit) {
                                    root.updateLineTotal(root.purchaseProducts[l].highQty, "highQty", root.purchaseProducts[l]);
                                }
                                root.updateLineTotal(root.purchaseProducts[l].highQty, "highQty", root.purchaseProducts[l]);
                                root.updateLineTotal(root.purchaseProducts[l].unitPerPack, "unitPerPack", root.purchaseProducts[l]);
                                root.updateLineTotal(root.purchaseProducts[l].unitPrice, "unitPrice", root.purchaseProducts[l]);
                                root.updateLineTotal(root.purchaseProducts[l].discount, "discount", root.purchaseProducts[l]);
                                root.updateLineTotal(root.purchaseProducts[l].fixDiscount, "fixDiscount", root.purchaseProducts[l]);
                                //    root.updateLineTotal(root.purchaseProducts[l].expiryDate, "expiryDate", root.purchaseProducts[l]);
                            }
                            root.calcuateSummary()
                        }
                        if (root.purchase != undefined) {
                            if (root.purchase.purchasePostItems != undefined) {
                                //Purchase Return Edit
                                root.purchase.purchasePostItems.forEach(function (item) {
                                    if (item.remainingQuantity > 0) {
                                        root.purchaseProducts.push({
                                            rowId: item.id,
                                            id: item.id,
                                            batchNo: item.batchNo,
                                            discount: item.discount,
                                            expiryDate: item.expiryDate,
                                            isExpire: item.product.isExpire,
                                            fixDiscount: item.fixDiscount,
                                            product: item.product,
                                            productId: item.productId,
                                            purchaseId: item.purchaseId,
                                            quantity: item.quantity,
                                            poQuantity: item.poQuantity,
                                            highQty: item.highQty,
                                            remainingQuantity: item.remainingQuantity,
                                            inventory: item.product.inventory,
                                            unitPerPack: item.unitPerPack,
                                            taxMethod: item.taxMethod,
                                            taxRateId: item.taxRateId,
                                            unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                            levelOneUnit: item.product.levelOneUnit,
                                            basicUnit: item.product.basicUnit,
                                            serial: item.product.serial,
                                            serialNo: item.serialNo,
                                            guarantee: item.product.guarantee,
                                            guaranteeDate: item.guaranteeDate,
                                            warrantyTypeId: item.warrantyTypeId,
                                            totalPiece: 0
                                        });
                                    }
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
                            else if (root.$route.query.data.purchaseOrderItems != undefined) {
                                //Purchase Order Edit

                                root.$route.query.data.purchaseOrderItems.forEach(function (item) {
                                    root.purchaseProducts.push({
                                        rowId: item.id,
                                        id: item.id,
                                        batchNo: item.batchNo,
                                        discount: item.discount,
                                        expiryDate: item.expiryDate,
                                        isExpire: item.product.isExpire,
                                        fixDiscount: item.fixDiscount,
                                        product: item.product,
                                        productId: item.productId,
                                        purchaseId: item.purchaseId,
                                        levelOneUnit: item.product.levelOneUnit,
                                        quantity: item.quantity,
                                        receiveQuantity: item.receiveQuantity,
                                        highQty: item.highQty,
                                        unitPerPack: item.unitPerPack,
                                        taxMethod: item.taxMethod,
                                        taxRateId: item.taxRateId,
                                        serial: item.product.serial,
                                        serialNo: item.serialNo,
                                        guarantee: item.product.guarantee,
                                        guaranteeDate: item.guaranteeDate,
                                        unitPrice: item.unitPrice == 0 ? '0' : item.unitPrice,
                                        basicUnit: item.product.basicUnit,
                                        warrantyTypeId: item.warrantyTypeId,
                                        totalPiece: 0
                                    });
                                });

                                for (var k = 0; k < root.purchaseProducts.length; k++) {
                                    root.products.push(root.purchaseProducts[k].product);

                                    root.updateLineTotal(root.purchaseProducts[k].quantity, "quantity", root.purchaseProducts[k]);
                                    if (root.isMultiUnit) {
                                        root.updateLineTotal(root.purchaseProducts[k].highQty, "highQty", root.purchaseProducts[k]);
                                    }
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
            clearList: function () {
                this.purchaseProducts = [];
            },
        },

        created: function () {

            //this.$barcodeScanner.init(this.onBarcodeScanned);
            //For Scanner Code
            //var root = this;
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

            //End
            localStorage.setItem("BarcodeScan", 'Purchase')

            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            //this.$barcodeScanner.init(this.onBarcodeScanned);
            this.getData();


        },

        mounted: function () {
            this.decimalQuantity = localStorage.getItem('decimalQuantity') == 'true' ? true : false;
            this.internationalPurchase = localStorage.getItem('InternationalPurchase');
            this.isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
            this.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;

            this.GetProductList();
                this.currency = localStorage.getItem('currency');
                this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            
        },
    };
</script>
<style scoped>
</style>
