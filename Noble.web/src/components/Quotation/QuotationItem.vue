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
                            {{ $t('QuotationItem.Product') }}
                        </th>
                        <th style="width: 110px;" class="text-center">
                            {{ $t('QuotationItem.UnitPrice') }}
                        </th>
                        <th class="text-center" style="width: 70px;" v-if="isValid('CanViewUnitPerPack')">
                            {{ $t('QuotationItem.UnitPerPack') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('QuotationItem.HighQty') }}
                        </th>
                        <th style="width: 110px;" class="text-center">
                            {{ $t('QuotationItem.Qty') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('QuotationItem.TOTALQTY') }}
                        </th>

                        <th class="text-center" style="width: 100px;" v-if="!isDiscountOnTransaction">
                            {{ $t('SaleItem.DISC%') }}
                        </th>

                        <th v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'" style="width: 135px;">
                            {{ $t('AddPurchase.VAT%') }}
                        </th>

                        <th style="width: 100px;" class="text-end">
                            {{ $t('QuotationItem.LineTotal') }}
                        </th>
                        <th style="width: 40px"></th>
                    </tr>
                </thead>
                <tbody>
                        <tr v-for="(prod , index) in purchaseProducts" :key="prod.productId + index">
                            <td class="border-top-0">
                                {{index+1}} 
                            </td>
                            <td class="border-top-0">
                                {{($i18n.locale == 'en' ||isLeftToRight())? products.find(x => x.id == prod.productId).englishName!=''? products.find(x => x.id == prod.productId).englishName : products.find(x => x.id == prod.productId).arabicName :    products.find(x => x.id == prod.productId).arabicName!=''? products.find(x => x.id == prod.productId).arabicName : products.find(x => x.id == prod.productId).englishName}}
                            </td>

                            <td class="border-top-0">
                                <decimal-to-fixed v-model="prod.unitPrice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.unitPrice, 'unitPrice', prod)" />
                            </td>
                            <td class="border-top-0 text-center" v-if="isValid('CanViewUnitPerPack')">
                                {{prod.unitPerPack}}
                            </td>
                            <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                                <decimal-to-fixed  v-model="prod.highQty" 
                                    v-bind:salePriceCheck="false"
                                    :isQunatity="true"
                                    @update:modelValue="updateLineTotal(prod.highQty, 'highQty', prod)" 
                                />
                                <small style="font-weight: 500;font-size:70%;">
                                    {{prod.levelOneUnit}}
                                </small>
                            </td>
                            <td class="border-top-0 text-center">
                                <decimal-to-fixed  v-model="prod.quantity" 
                                    v-bind:salePriceCheck="false"
                                    :isQunatity="true"
                                    @update:modelValue="updateLineTotal(prod.quantity, 'quantity', prod)" 
                                />
                                <small style="font-weight: 500;font-size:70%;" v-if="isMultiUnit=='true'">
                                    {{prod.basicUnit}}
                                </small>
                            </td>
                            <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                                {{parseInt(parseFloat(prod.highQty*prod.unitPerPack) + parseFloat(prod.quantity))}}
                            </td>
                            <td v-if="!isDiscountOnTransaction" class="text-center">
                                <div class="input-group" v-if="prod.discountSign == '%'">
                                    <decimal-to-fixed v-model="prod.discount" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.discount, 'discount', prod)" />
                                    <button v-on:click="OnChangeDiscountType(prod)" class="btn btn-primary" type="button" id="button-addon2">{{prod.discountSign}}</button>
                                </div>

                                <div class="input-group" v-else-if="prod.discountSign == 'F'">
                                    <decimal-to-fixed v-model="prod.fixDiscount" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.fixDiscount, 'fixDiscount', prod)" />
                                    <button v-on:click="OnChangeDiscountType(prod)" class="btn btn-primary" type="button" id="button-addon2">{{prod.discountSign}}</button>
                                </div>
                            </td>
                            

                            <td class="border-top-0" v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'">
                                <taxratedropdown v-model="prod.taxRateId" @update:modelValue="getVatValue(prod.taxRateId, prod)" />
                            </td>

                            <td class="border-top-0 text-end">
                                {{currency}}  {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0,-1))  }}
                            </td>
                            <td class="border-top-0 text-end">
                                <a href="javascript:void(0);" @click="removeProduct(prod.rowId)"><i class="las la-trash-alt text-secondary font-16"></i></a>                                
                            </td>
                        </tr>
                        
                </tbody>
            </table>
        </div>


        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="mt-4" >
                    <product-dropdown v-bind:key="rendered"
                              ref="productDropdownRef"
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
                                    <span class="fw-bold">{{ $t('QuotationItem.SubTotal') }}</span> 
                                    <span>(Tax {{taxMethod}})</span>
                                </td>
                                <td class="text-end" style="width:35%;">{{summary.withDisc}}</td>
                            </tr>
                            <tr v-if="isDiscountOnTransaction && isBeforeTax">
                                <td style="width:40%;">
                                    <span style="height:33px !important; ">{{ $t('QuotationItem.Disc') }}</span>
                                    <br />
                                    <span v-if="summary.item > 0">
                                        <a href="javascript:void(0)" v-on:click="UpdateDiscountField('beforeTax')">
                                            <small class="fw-bold text-primary">{{ $t('QuotationItem.ApplyAfterTax') }}</small>
                                        </a>
                                    </span>
                                </td>
                                <td style="width:25%;">
                                    <div class="input-group">
                                        <decimal-to-fixed v-model="transactionLevelDiscount" @update:modelValue="calcuateSummary" />
                                        <button v-if="taxMethod == ('Inclusive' || 'شامل')" disabled class="btn btn-primary" type="button" id="button-addon2">%</button>
                                        <button v-else class="btn btn-primary" v-on:click="UpdateDiscountField('fixed')" type="button" id="button-addon2">{{isFixed?'F':'%'}}</button>
                                    </div>                                    
                                </td>
                                <td class="text-end" style="width:35%;">{{transactionLevelTotalDiscount}}</td>
                            </tr>
                            <tr v-for="(vat,index) in paidVatList" :key="index">
                                <td class="fw-bold" colspan="2" style="width:65%;">{{vat.name}}</td>
                                <td class="text-end" style="width:35%;">{{vat.amount}}</td>
                            </tr>
                            <tr v-if="isDiscountOnTransaction && !isBeforeTax">
                                <td style="width:40%;">
                                    <span style="height:33px !important; ">{{ $t('QuotationItem.Disc') }}</span>
                                    <br />
                                    <span v-if="summary.item > 0">
                                        <a href="javascript:void(0)" v-on:click="UpdateDiscountField('beforeTax')">
                                            <small class="fw-bold text-primary">{{ $t('QuotationItem.ApplyBeforeTax') }}</small>
                                        </a>
                                    </span>
                                </td>
                                <td style="width:25%;">
                                    <div class="row">
                                        <div class="input-group">
                                            <decimal-to-fixed v-model="transactionLevelDiscount" @update:modelValue="calcuateSummary" />
                                            <button v-if="taxMethod == ('Inclusive' || 'شامل')" disabled class="btn btn-primary" type="button" id="button-addon2">%</button>
                                            <button v-else class="btn btn-primary" v-on:click="UpdateDiscountField('fixed')" type="button" id="button-addon2">{{isFixed?'F':'%'}}</button>
                                        </div>
                                    </div>
                                </td>
                                <td class="text-end" style="width:35%;">{{transactionLevelTotalDiscount}}</td>
                            </tr>
                            <tr>
                                <td style="width:40%;">
                                    <input class="form-control" type="text" :value="$t('QuotationItem.Adjustment')" style="border: 1px dashed #1761fd;" />
                                </td>
                                <td style="width:25%;">
                                    <div class="input-group">
                                        <decimal-to-fixed v-model="adjustment" @update:modelValue="calcuateSummary" />
                                        <button v-on:click="OnChangeOveallDiscount" class="btn btn-primary" type="button" id="button-addon2">{{adjustmentSign}}</button>
                                    </div>
                                </td>
                                <td class="text-end" style="width:35%;">{{adjustmentSign == '+'?adjustment:(-1)*adjustment}}</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:65%;">
                                    <span style="font-weight:bolder; font-size:16px">{{ $t('QuotationItem.TotalDue') }}({{currency}})</span>
                                </td>
                                <td class="text-end" style="width: 35%; font-weight: bolder; font-size: 16px">{{summary.withVat}}</td>
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
    //import Multiselect from 'vue-multiselect'

    export default {
        name: "PurchaseItem",
        props: ['purchase', 'purchaseItems', 'taxMethod', 'taxRateId', 'adjustmentProp', 'adjustmentSignProp', 'isDiscountOnTransaction', 'transactionLevelDiscountProp', 'newisFixed', 'newisBeforeTax'],
        
        mixins: [clickMixin],
        //components: {
        //    Multiselect
        //},
        data: function () {
            return {
                isFixed: this.newisFixed,
                isBeforeTax: this.newisBeforeTax,
                transactionLevelDiscount: 0,
                adjustment: 0,
                adjustmentSign: '+',
                isDiscountBeforeVat: false,
                transactionLevelTotalDiscount: 0,
                paidVatList:[],

                rendered: 0,
                product: {
                    id: "",
                },
                decimalQuantity: false,
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
                saleDefaultVat: '',
                productList: [],
                options: [],
            };
        },
        validations() {},
        filters: {

        },
        methods: {
            UpdateDiscountField: function (prop) {
                if (prop === 'fixed')
                    this.isFixed = this.isFixed ? false : true
                if (prop === 'beforeTax')
                    this.isBeforeTax = this.isBeforeTax ? false : true
                this.$emit("discountChanging", this.isFixed, this.isBeforeTax);
                this.calcuateSummary();
            },
            OnChangeOveallDiscount: function () {
                this.adjustmentSign = this.adjustmentSign == '+' ? '-' : '+'
                this.calcuateSummary()
            },
            NewItemChangeDiscount: function (prod) {
                if (prod.discountSign === '%') {
                    prod.discountSign = 'F';
                    prod.fixDiscount = 0
                    prod.discount = 0
                }
                else {
                    prod.discountSign = '%';
                    prod.discount = 0
                    prod.fixDiscount = 0
                }
            },
            OnChangeDiscountType: function (prod) {
                if (prod.discountSign === '%') {
                    prod.discountSign = 'F';
                    prod.fixDiscount = 0
                    prod.discount = 0
                    this.updateLineTotal(prod.fixDiscount, 'fixDiscount', prod)
                }
                else {
                    prod.discountSign = '%';
                    prod.discount = 0
                    prod.fixDiscount = 0
                    this.updateLineTotal(prod.discount, 'discount', prod)
                }
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
                    .get("/Product/GetProductBarcode", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.productList = response.data.results.products;

                        }
                    });


            },
            onBarcodeScanned(barcode) {

                if (localStorage.getItem("BarcodeScan") != 'Quotation')
                    return
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
                }
                else {
                    this.summary.qty = this.purchaseProducts.reduce((qty, prod) => qty + parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                }

                this.summary.total = this.purchaseProducts.reduce((total, prod) =>
                    total + (prod.totalPiece) * prod.unitPrice, 0).toFixed(3).slice(0, -1);

                if (!this.isDiscountOnTransaction) {
                    this.transactionLevelDiscount = 0;
                }
                var vatRate = 0;
                var discountOnly = 0;
                var discountForInclusiveVat = 0;
                var root = this;
                const taxIdList = [...new Set(this.purchaseProducts.map(item => item.taxRateId))];
                root.paidVatList = []
                //'isDiscountOnTransaction', 'transactionLevelDiscount'
                taxIdList.forEach(function (taxId) {
                    vatRate = root.vats.find((value) => value.id == taxId);
                    var filteredRecord = root.purchaseProducts
                        .filter((x) => x.taxRateId === taxId);
                    var totalQtyWithotFree = root.purchaseProducts.reduce((qty, prod) => qty + parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);

                    discountOnly += filteredRecord
                        .filter((x) => x.discount != 0 || x.discount != "" || x.offerQuantity != 0)
                        .reduce((discount, prod) =>
                            discount + (prod.totalPiece ? (prod.offerQuantity ? 0 : (((prod.totalPiece * prod.unitPrice) * prod.discount) / 100)) : 0), 0);

                    discountOnly += filteredRecord
                        .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "" || x.offerQuantity != 0)
                        .reduce((discount, prod) =>
                            discount + (prod.totalPiece ? (prod.offerQuantity ? 0 : (root.taxMethod == ("Inclusive" || "شامل") ? prod.fixDiscount + (prod.fixDiscount * vatRate.rate / 100) : prod.fixDiscount)) : 0), 0);

                    var paidVat = filteredRecord
                        .reduce((vat, prod) => (vat + ((prod.taxMethod == ("Inclusive" || "شامل")) ? ((parseFloat(prod.lineTotal) - (root.isBeforeTax ? (((prod.totalPiece * prod.unitPrice) * root.transactionLevelDiscount) / 100) : 0)) * vatRate.rate) / (100 + vatRate.rate) : ((parseFloat(prod.lineTotal) - (root.isBeforeTax && !root.isFixed && root.isDiscountOnTransaction ? (((prod.totalPiece * prod.unitPrice) * root.transactionLevelDiscount) / 100) : (root.isBeforeTax && root.isFixed && root.isDiscountOnTransaction ? (root.transactionLevelDiscount / parseFloat(totalQtyWithotFree) * prod.totalPiece) : 0))) * vatRate.rate) / 100)), 0).toFixed(3).slice(0, -1)
                    discountForInclusiveVat += parseFloat(filteredRecord
                        .reduce((vat, prod) => (vat + ((prod.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(prod.lineTotal) * vatRate.rate) / (100 + vatRate.rate) : 0)), 0).toFixed(3).slice(0, -1))

                    root.paidVatList.push({
                        name: vatRate.name,
                        amount: paidVat
                    })

                });
                //root.transactionLevelDiscount = root.transactionLevelDiscount;
                this.summary.discount = discountOnly
                this.summary.withDisc = (this.summary.total - this.summary.discount).toFixed(3).slice(0, -1);

                this.summary.vat = this.paidVatList.reduce((vat, paidVat) => (vat + parseFloat(paidVat.amount)), 0).toFixed(3).slice(0, -1);

                var exclusiveVat = this.taxMethod == ("Inclusive" || "شامل") ? 0 : parseFloat(this.summary.vat);
                this.transactionLevelTotalDiscount = ((this.isBeforeTax && this.isDiscountOnTransaction) ? (this.taxMethod == ("Inclusive" || "شامل") ? (parseFloat(this.transactionLevelDiscount) * (this.summary.withDisc - discountForInclusiveVat) / 100) : (this.isFixed ? parseFloat(this.transactionLevelDiscount) : parseFloat(this.transactionLevelDiscount) * this.summary.withDisc / 100)) : (this.isFixed ? parseFloat(this.transactionLevelDiscount) : (parseFloat(this.summary.withDisc) + parseFloat(exclusiveVat)) * parseFloat(this.transactionLevelDiscount) / 100)).toFixed(3).slice(0, -1)

                var totalIncDisc = (this.isBeforeTax && this.isDiscountOnTransaction && this.taxMethod == ("Inclusive" || "شامل")) ? (parseFloat(this.transactionLevelDiscount) * (this.summary.withDisc) / 100) : parseFloat(this.transactionLevelTotalDiscount)
                this.adjustment = (this.adjustment == '' || this.adjustment == null) ? 0 : parseFloat(this.adjustment)

                this.summary.withVat = (parseFloat(this.summary.withDisc) + parseFloat(exclusiveVat) + (this.adjustmentSign == '+' ? this.adjustment : (-1) * this.adjustment)).toFixed(3).slice(0, -1);

                this.summary.withVat = (parseFloat(this.summary.withVat) - totalIncDisc).toFixed(3).slice(0, -1);



                //calculate bundle Amount
                if (this.purchaseProducts.filter(x => x.isBundleOffer).length > 0) {

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

                    var bundleProducts = this.purchaseProducts.filter(x => x.isBundleOffer != undefined && x.offerQuantity > 0);

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

                    this.summary.bundleAmount = (parseFloat(bundle.withDisc) + parseFloat(exclusiveVat)).toFixed(3).slice(0, -1);
                    this.summary.withVat = (this.summary.withVat - bundle.withDisc);
                } else {
                    this.summary.bundleAmount = 0;
                }
                this.$emit("update:modelValue", this.purchaseProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));

                this.$emit("summary", this.summary);
            },

            updateLineTotal: function (e, prop, product) {
                var root = this;
                if (e != undefined) {
                    var discount = product.discount == 0 || product.discount == "" ? product.fixDiscount == 0 || product.fixDiscount == ""
                        ? 0
                        : product.fixDiscount
                        : product.discount;

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

                    if (product.productId != null) {
                        var prod = root.products.find((x) => x.id == product.productId);

                    }


                    if (!this.invoiceWoInventory && product.productId != null && this.formName != 'Quotation') {
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
                                    product['remainingStockLimit'] = (prod.promotionOffer.stockLimit - prod.promotionOffer.quantityOut);

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
                        } else {
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
                        this.purchaseProducts['product'] = product;
                        this.calcuateSummary();
                        this.$emit("update:modelValue", this.purchaseProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));

                    }
                    else {
                        //isDiscountBeforeVat
                        vat = this.vats.find((value) => value.id == product.taxRateId);


                        discount = product.discount == 0 ? (this.taxMethod == ("Inclusive" || "شامل") ? product.fixDiscount + (product.fixDiscount * vat.rate / 100) : product.fixDiscount) : (product.totalPiece * product.unitPrice * product.discount) / 100;
                        product.lineTotal = product.totalPiece * product.unitPrice - discount;

                        this.purchaseProducts['product'] = product;
                        this.calcuateSummary();
                        this.$emit("update:modelValue", this.purchaseProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));
                    }
                }
            },
            ClearList: function () {
                
                this.purchaseProducts = [];
                this.products = [];
            },
            CheckRecordInProduct: function () {
                
               return  this.$refs.productDropdownRef.productListCheck();
            },
            addProduct: function (productId, newProduct, quantity, unitPrice, isTemplate) {

                if (isTemplate) {
                    newProduct = this.$refs.productDropdownRef.productListValueCompare(productId);
                    

                }


                var prd = this.purchaseProducts.find(x => x.productId == productId);
                if (prd != undefined) {
                    prd.quantity++;
                    this.updateLineTotal(prd.quantity, "quantity", prd);
                }
                else {
                    this.products.push(newProduct);
                    var prod = this.products.find((x) => x.id == productId);

                    var rate = 0;
                    var taxRateId = '';
                    var taxMethod = '';
                    if (this.saleDefaultVat == 'DefaultVat' || this.saleDefaultVat == 'DefaultVatItem') {
                        if (prod.taxRateId != "00000000-0000-0000-0000-000000000000" && prod.taxRateId != undefined) {
                            rate = this.getVatValue(prod.taxRateId, prod);
                        }
                        taxRateId = prod.taxRateId;
                        taxMethod = prod.taxMethod;
                    }
                    if (this.saleDefaultVat == 'DefaultVatHead' || this.saleDefaultVat == 'DefaultVatHeadItem') {
                        if (this.taxRateId != "00000000-0000-0000-0000-000000000000" && this.taxRateId != undefined) {
                            rate = this.getVatValue(this.taxRateId, prod);
                        }
                        taxRateId = this.taxRateId;
                        taxMethod = this.taxMethod;
                    }

                    if (isTemplate) {
                        this.purchaseProducts.push({
                            rowId: this.createUUID(),
                            productId: prod.id,
                            unitPrice: prod.salePrice,
                            quantity: quantity,
                            highQty: 0,
                            discount: 0,
                            fixDiscount: 0,
                            taxRateId: taxRateId,
                            rate: rate,
                            taxMethod: taxMethod,
                            lineTotal: 0,
                            unitPerPack: newProduct.unitPerPack,
                            levelOneUnit: prod.levelOneUnit,
                            basicUnit: prod.basicUnit,

                            highUnitPrice: newProduct.highUnitPrice,
                        });
                    }
                    else {
                        this.purchaseProducts.push({
                            rowId: this.createUUID(),
                            productId: prod.id,
                            unitPrice: prod.salePrice,
                            quantity: 1,
                            highQty: 0,
                            discount: 0,
                            fixDiscount: 0,
                            taxRateId: taxRateId,
                            rate: rate,
                            taxMethod: taxMethod,
                            lineTotal: 0,
                            unitPerPack: newProduct.unitPerPack,
                            levelOneUnit: prod.levelOneUnit,
                            basicUnit: prod.basicUnit,

                            highUnitPrice: newProduct.highUnitPrice,
                            discountSign: '%',
                        });

                        var product = this.purchaseProducts.find((x) => {
                            return x.productId == productId;
                        });

                        if (isTemplate) {
                            this.updateLineTotal(unitPrice, "unitPrice", product);

                        }

                        this.getVatValue(product.taxRateId, product);

                        this.product.id = "";
                    }
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

            getTaxMethod: function (method, prod) {
                prod.taxMethod = method;
                this.updateLineTotal(prod.unitPrice, "unitPrice", prod);
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

                        if (root.$route.query.data != undefined) {
                            if (root.$route.query.data.saleOrderItems != undefined) {
                                //Sale Order Edit

                                root.$route.query.data.saleOrderItems.forEach(function (item) {
                                    root.purchaseProducts.push({
                                        rowId: item.id,
                                        id: item.id,
                                        discount: item.discount,
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
                                        highUnitPrice: item.product.highUnitPrice,
                                        discountSign: item.discount == 0 ? item.fixDiscount == 0 ? '%' : 'F' : '%',
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
                                root.adjustment = (root.adjustmentProp == null || root.adjustmentProp == undefined || root.adjustmentProp == '') ? 0 : (root.adjustmentSignProp == '+' ? root.adjustmentProp : (-1) * root.adjustmentProp)
                                root.adjustmentSign = root.adjustmentSignProp;
                                root.calcuateSummary()
                            }

                        }
                    });
            },
        },
        created: function () {
            this.transactionLevelDiscount = this.transactionLevelDiscountProp;
            if (this.$i18n.locale == 'en') {
                this.options = ['Inclusive', 'Exclusive'];
            }
            else {
                this.options = ['شامل', 'غير شامل'];
            }

            this.decimalQuantity = localStorage.getItem('decimalQuantity') == 'true' ? true : false;
            var root = this;
            var barcode = '';
            var interval;
            this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');


            document.addEventListener('keydown', function (evt) {
                if (interval)
                    clearInterval(interval);
                if (evt.code === 'Enter') {
                    if (barcode) {
                        root.onBarcodeScanned(barcode);
                    }
                    barcode = '';
                    return;

                }
                if (evt.key !== 'Shift')
                    barcode += evt.key;
            });
            localStorage.setItem("BarcodeScan", 'Quotation')
            //End
            this.getData();
        },
        mounted: function () {
            this.GetProductList();
            this.currency = localStorage.getItem('currency');
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
        },
    };
</script>

