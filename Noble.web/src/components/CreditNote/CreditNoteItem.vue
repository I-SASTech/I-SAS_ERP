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
                            {{ $t('SaleOrderItem.Product') }}
                        </th>
                        <th v-if="colorVariants" class="text-center" style="width: 200px;">
                            {{ $t('SaleOrderItem.Description') }}
                        </th>
                        <th style="width: 80px;" class="text-center">
                            {{ $t('SaleOrderItem.UnitPrice') }}
                        </th>
                        <th class="text-center" style="width: 70px;" v-if="isValid('CanViewUnitPerPack')">
                            {{ $t('SaleOrderItem.UnitPerPack') }}
                        </th>

                        <th style="width: 70px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('SaleOrderItem.HighQty') }}
                        </th>
                        <th style="width: 70px;" class="text-center">
                            {{ $t('SaleOrderItem.Qty') }}
                        </th>
                        <th style="width: 70px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('SaleOrderItem.TOTALQTY') }}
                        </th>

                        <!--<th v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'" style="width: 110px;">
                            {{ $t('AddPurchase.TaxMethod') }}
                        </th>-->
                        <th v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'" style="width: 100px;">
                            {{ $t('AddPurchase.VAT%') }}
                        </th>

                        <th style="width: 80px;" class="text-end">
                            {{ $t('SaleOrderItem.LineTotal') }}
                        </th>
                        <th style="width: 30px"></th>
                    </tr>
                </thead>
                <tbody>
                    

                        <tr v-for="(prod , index) in purchaseProducts" :key="prod.productId + index" v-bind:class="{'alert-danger':prod.outOfStock}">
                            <td>
                                {{index+1}}
                            </td>
                            <td>
                                {{($i18n.locale == 'en' ||isLeftToRight())? products.find(x => x.id == prod.productId).englishName!=''? products.find(x => x.id == prod.productId).displayName : products.find(x => x.id == prod.productId).displayName :    products.find(x => x.id == prod.productId).arabicName!=''? products.find(x => x.id == prod.productId).displayName : products.find(x => x.id == prod.productId).displayName}}
                            </td>
                            <td v-if="colorVariants">
                                <textarea class="form-control input-border" v-model="prod.description" />
                            </td>
                            <td>
                                <decimal-to-fixed v-model="prod.unitPrice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.unitPrice, 'unitPrice', prod)" />
                            </td>
                            <td class=" text-center" v-if="isValid('CanViewUnitPerPack')">
                                {{prod.unitPerPack}}
                            </td>

                            <td class="text-center" v-if="isMultiUnit=='true'" :title="prod.levelOneUnit" data-tippy-arrow="true" data-tippy-animation="fade">
                                <decimal-to-fixed v-model="prod.highQty"
                                                  v-bind:salePriceCheck="false"
                                                  :isQunatity="true"
                                                  @update:modelValue="updateLineTotal(prod.highQty, 'highQty', prod)" />
                                <!--<small style="font-weight: 500;font-size:70%;">
                                    {{prod.levelOneUnit}}
                                </small>-->
                            </td>
                            <td class="text-center" :title="prod.basicUnit" data-tippy-arrow="true" data-tippy-animation="fade">
                                <decimal-to-fixed v-model="prod.quantity"
                                                  v-bind:salePriceCheck="false"
                                                  :isQunatity="true"
                                                  @update:modelValue="updateLineTotal(prod.quantity, 'quantity', prod)" />
                                <!--<small style="font-weight: 500;font-size:70%;" v-if="isMultiUnit=='true'">
                                    {{prod.basicUnit}}
                                </small>-->
                            </td>
                            <td class="text-center" v-if="isMultiUnit=='true'">
                                {{parseInt(parseFloat(prod.highQty*prod.unitPerPack) + parseFloat(prod.quantity))}}
                            </td>

                            <!--<td  v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'">
                                <multiselect :options="options" v-model="prod.taxMethod" @update:modelValue="getTaxMethod(prod.taxMethod, prod)" :show-labels="false" v-bind:placeholder="$t('PurchaseItem.TaxMethod')" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                </multiselect>
                            </td>-->

                            <td v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'">
                                <taxratedropdown v-model="prod.taxRateId" @update:modelValue="getVatValue(prod.taxRateId, prod)" />
                            </td>

                            <td class="text-end">
                                {{currency}}  {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0,-1))  }}
                            </td>
                            <td class="text-end">
                                <a href="javascript:void(0);" @click="removeProduct(prod.rowId)"><i class="las la-trash-alt text-secondary font-16"></i></a>
                            </td>
                        </tr>
                    
                    <!-- <tr v-if="IsService">
                        <td colspan="2">
                            <textarea data-gramm="false" rows="2" v-model="newItem.description" class="form-control" />
                        </td>
                       
                        <td>
                            <decimal-to-fixed v-model="newItem.unitPrice" />
                        </td>

                      
                        <td class="text-center">
                            <decimal-to-fixed  v-model="newItem.quantity"
                                   />
                        </td>
                      
                        <td class="text-end">
                            <button @click="newItemProduct()"
                                    title="Add Item" v-bind:disabled="newItem.description==''"
                                    class="btn btn-primary btn-sm btn-round btn-icon float-right">
                                <i class="fa fa-check"></i>
                            </button>
                        </td>
                    </tr> -->
                </tbody>
            </table>
        </div>


        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="mt-4" v-if="invoiceWoInventory || (!invoiceWoInventory && !soInventoryReserve)">
                    <product-dropdown v-bind:key="rendered"
                                      @update:modelValues="addProduct"
                                      ref="productDropdownRef"
                                      :fromSOrder="true"
                                      width="100%" />
                </div>
                <div class="mt-4" v-else>
                    <product-dropdown :wareHouseId="wareHouseId"
                                      :raw="false"
                                      ref="productDropdownRef"
                                      @update:modelValues="addProduct"
                                      :fromSOrder="true"
                                      width="100%" />
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 ">
                <div class="mt-4" v-bind:key="rendered + 'g'">
                    <table class="table" style="background-color: #f1f5fa;">
                        <tbody>
                            <tr>
                                <td colspan="2" style="width:65%;">
                                    <span class="fw-bold">{{ $t('SaleOrderItem.Subtotal') }}</span>
                                    <span>(Tax {{taxMethod}})</span>
                                </td>
                                <td class="text-end" style="width:35%;">{{summary.withDisc}}</td>
                            </tr>
                            <tr v-if="isDiscountOnTransaction && isBeforeTax">
                                <td style="width:40%;">
                                    <span style="height:33px !important; "> {{ $t('SaleOrderItem.Disc') }}</span>
                                    <br />
                                    <span v-if="summary.item > 0">
                                        <a href="javascript:void(0)" v-on:click="UpdateDiscountField('beforeTax')">
                                            <small class="fw-bold text-primary">{{ $t('SaleOrderItem.ApplyAfterTax') }}</small>
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
                            <!--<tr v-if="isDiscountOnTransaction && !isBeforeTax">
                                <td style="width:40%;">
                                    <span style="height:33px !important; ">{{ $t('SaleOrderItem.Disc') }}</span>
                                    <br />
                                    <span v-if="summary.item > 0">
                                        <a href="javascript:void(0)" v-on:click="UpdateDiscountField('beforeTax')">
                                            <small class="fw-bold text-primary">{{ $t('SaleOrderItem.ApplyBeforeTax') }}</small>
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
                            </tr>-->
                            <!--<tr>
                                <td style="width:40%;">
                                    <input class="form-control" type="text" :value="$t('SaleOrderItem.Adjustment')" style="border: 1px dashed #1761fd;" />
                                </td>
                                <td style="width:25%;">
                                    <div class="input-group">
                                        <decimal-to-fixed v-model="adjustment" @update:modelValue="calcuateSummary" />
                                        <button v-on:click="OnChangeOveallDiscount" class="btn btn-primary" type="button" id="button-addon2">{{adjustmentSign}}</button>
                                    </div>
                                </td>
                                <td class="text-end" style="width:35%;">{{adjustmentSign == '+'?adjustment:(-1)*adjustment}}</td>
                            </tr>-->
                            <tr>
                                <td colspan="2" style="width:65%;">
                                    <span style="font-weight:bolder; font-size:16px">{{ $t('SaleOrderItem.TotalDue') }}({{currency}})</span>
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
    import moment from "moment";
    import clickMixin from '@/Mixins/clickMixin'
    /* import Multiselect from 'vue-multiselect'*/

    export default {
        name: "CreditNoteItem",
        props: ['purchase', 'purchaseItems', 'wareHouseId', 'taxMethod', 'taxRateId', 'adjustmentProp', 'adjustmentSignProp', 'isDiscountOnTransaction', 'transactionLevelDiscountProp', 'isfixed', 'isbeforetax'],
        mixins: [clickMixin],
        //components: {
        //    Multiselect
        //},
        data: function () {
            return {
                isBeforeTax: this.isbeforetax,
                isFixed: this.isfixed,
                transactionLevelDiscount: 0,
                adjustment: 0,
                adjustmentSign: '+',
                isDiscountBeforeVat: false,
                IsService: false,
                transactionLevelTotalDiscount: 0,

                options: [],
                colorVariants: false,
                isSerial: false,
                isFifo: false,
                decimalQuantity: false,
                rendered: 0,
                product: {
                    id: "",
                },
                isMultiUnit: '',
                products: [],
                purchaseProducts: [],
                loading: false,
                vats: [],
                paidVatList: [],
                newItem: {
                    description: '',
                    unitPrice: 0,
                    quantity: 0,
                },
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
                soInventoryReserve: false,
                invoiceWoInventory: false,
                currency: '',
                searchTerm: '',
                productList: [],
                serialItem: '',
                saleDefaultVat: '',
                showSerial: false
            };
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
            CheckRecordInProduct: function () {

                return this.$refs.productDropdownRef.productListCheck();
            },
            ClearList: function () {

                this.purchaseProducts = [];
                this.products = [];

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

            //    this.isRaw = this.raw == undefined ? false : this.raw;
            //    //search = search == undefined ? '' : search;
            //    // var url = this.wareHouseId != undefined ? "/Product/GetProductInformation?searchTerm=" + search + '&wareHouseId=' + this.wareHouseId + "&isDropdown=true" + '&isRaw=' + root.isRaw : "/Product/GetProductInformation?searchTerm=" + search + '&status=' + root.status + "&isDropdown=true" + '&isRaw=' + root.isRaw;

            //    this.$https
            //        .get("/Product/GetProductBarcode", {
            //            headers: { Authorization: `Bearer ${token}` },
            //        })
            //        .then(function (response) {
            //            if (response.data != null) {
            //                root.productList = response.data.results.products;

            //            }
            //        });


            //},

            onBarcodeScanned(barcode) {

                if (localStorage.getItem("BarcodeScan") != 'SaleOrder')
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


                this.$emit("update:modelValues",  this.purchaseProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));
                //this.$emit("update:modelValue", this.purchaseProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));

                this.$emit("summary", this.summary);
            },

            updateLineTotal: function (e, prop, product) {
                
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


                    
                    if (product.wholesaleQuantity != null && product.wholesaleQuantity != undefined) {
                        if (product.wholesaleQuantity > 0 && product.wholesaleQuantity <= product.quantity) {
                            product.unitPrice = product.wholesalePrice;
                        }
                        else {
                            product.unitPrice = product.salePrice;
                        }
                    }

                    
                    if (parseInt(product.schemeQuantity) > 0 && parseInt(product.scheme) > 0 && parseInt(product.schemeQuantity) <= product.totalPiece) {
                        product.discountSign = 'F';

                        product.fixDiscount = ((product.totalPiece * product.unitPrice)
                            / ((parseInt(product.schemeQuantity) > 0 ? parseInt(product.schemeQuantity) : 0) + (parseInt(product.scheme) > 0 ? parseInt(product.scheme) : 0))) * (parseInt(product.scheme) > 0 ? parseInt(product.scheme) : 0);
                        
                    }
                    else {
                        if (parseInt(product.schemeQuantity) > 0 && parseInt(product.schemeQuantity) > product.totalPiece) {
                            product.fixDiscount = 0;
                        }
                        if (parseInt(product.schemeQuantity) == 0 || parseInt(product.scheme) == 0) {
                            product.fixDiscount = 0;
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
                        else if (e > product.unitPrice && (product.scheme == '' || product.scheme == 0 || product.scheme == null || product.scheme == undefined)) {
                            e = product.unitPrice;
                        }
                        product.fixDiscount = e;
                    }

                    var vat = 0;
                    var calculateVAt = 0;

                    //isDiscountBeforeVat
                    vat = this.vats.find((value) => value.id == product.taxRateId);
                    
                    var total = product.totalPiece * product.unitPrice;
                    discount = product.discount == 0 ? (this.taxMethod == ("Inclusive" || "شامل") ? product.fixDiscount + (product.fixDiscount * vat.rate / 100) : product.fixDiscount) : (product.totalPiece * product.unitPrice * product.discount) / 100;
                    product.lineTotal = product.totalPiece * product.unitPrice - discount;

                    if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                        calculateVAt = (product.lineTotal * vat.rate) / (100 + vat.rate);
                        product.lineItemVAt = calculateVAt;
                    }
                    else {
                        calculateVAt = (product.lineTotal * vat.rate) / 100;
                        product.lineItemVAt = calculateVAt;
                    }

                    product.discountAmount = discount;
                    product.vatAmount = calculateVAt;
                    product.totalAmount = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? product.lineTotal : product.lineTotal + product.vatAmount;
                    product.grossAmount = (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') ? total * 100 / (100 + vat.rate) : total;

                    this.purchaseProducts['product'] = product
                    this.calcuateSummary();
                    this.$emit("update:modelValue", this.purchaseProducts, this.adjustment, this.adjustmentSign, parseFloat(this.transactionLevelDiscount));
                }
            },

            addProduct: function (productId, newProduct, soItem, quotation, batch, quantity, unitPrice, isTemplate) {

                var uid = this.createUUID();

                if (isTemplate) {
                    newProduct = this.$refs.productDropdownRef.productListValueCompare(productId);


                }

                if (this.purchaseProducts.some(x => x.productId == productId) && !this.isFifo) {
                    var prd = this.purchaseProducts.find(x => x.productId == productId);
                    prd.quantity++;
                    this.updateLineTotal(prd.quantity, "quantity", prd);
                }
                else if (this.purchaseProducts.some(x => x.productId == productId && x.batchNo == batch.batchNumber) && this.isFifo) {
                    var prd1 = this.purchaseProducts.find(x => x.productId == productId && x.batchNo == newProduct.batchNo);
                    prd1.quantity++;
                    this.updateLineTotal(prd1.quantity, "quantity", prd1);
                }
                else {

                    var prod = '';
                    if (this.isFifo && (batch != undefined || batch != null)) {

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
                            highUnitPrice: newProduct.highUnitPrice,
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
                            discountSign: '%',
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

                            highUnitPrice: newProduct.highUnitPrice,
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
                            discountSign: '%',
                        });

                        prod = this.products.find((x) => x.rowId == uid);

                    }
                    else {
                        if (this.products.find(x => x.id == productId) == undefined || this.products.length <= 0) {
                            this.products.push(newProduct);
                        }
                        prod = this.products.find((x) => x.id == productId);
                    }
                    //this.products.push(newProduct);
                    //var prod = this.products.find((x) => x.id == productId);

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
                            rowId: uid,
                            productId: prod.id,
                            unitPrice: unitPrice,
                            quantity: quantity,
                            highQty: 0,
                            discount: 0,
                            description: '',
                            salePrice: prod.salePrice,
                            currentQuantity: prod.inventory == null ? 0 : prod.inventory.currentQuantity,
                            fixDiscount: 0,
                            taxRateId: taxRateId,
                            taxMethod: quotation ? soItem.taxMethod : taxMethod,

                            rate: rate,
                            totalPiece: 0,
                            isExpire: prod.isExpire,

                            expiryDate: prod.batchExpiry,
                            batchNo: prod.batchNo,
                            highUnitPrice: newProduct.highUnitPrice,
                            lineTotal: 0,
                            serial: '',
                            guaranteeDate: '',
                            isSerial: newProduct.serial,
                            guarantee: newProduct.guarantee,
                            unitPerPack: newProduct.unitPerPack,
                            levelOneUnit: prod.levelOneUnit,
                            basicUnit: prod.basicUnit,
                            inventory: prod.inventory,
                            inventoryList: prod.inventoryBatch == null ? null : prod.inventoryBatch,
                        });
                    }
                    else {
                        this.purchaseProducts.push({
                            rowId: uid,
                            productId: prod.id,
                            unitPrice: quotation ? soItem.unitPrice : prod.salePrice,
                            quantity: quotation ? soItem.quantity : 1,
                            highQty: quotation ? soItem.highQty : 0,
                            discount: 0,
                            description: '',
                            currentQuantity: prod.inventory == null ? 0 : prod.inventory.currentQuantity,
                            fixDiscount: 0,
                            taxRateId: taxRateId,
                            taxMethod: quotation ? soItem.taxMethod : taxMethod,

                            scheme: prod.scheme,
                            schemeQuantity: prod.schemeQuantity,
                            wholesalePrice: prod.wholesalePrice,
                            salePrice: prod.salePrice,
                            wholesaleQuantity: prod.wholesaleQuantity,
                            schemePhysicalQuantity: 0,

                            rate: rate,
                            totalPiece: 0,
                            isExpire: prod.isExpire,

                            expiryDate: prod.batchExpiry,
                            batchNo: prod.batchNo,
                            highUnitPrice: newProduct.highUnitPrice,
                            lineTotal: 0,
                            serial: '',
                            guaranteeDate: '',
                            isSerial: newProduct.serial,
                            guarantee: newProduct.guarantee,
                            unitPerPack: newProduct.unitPerPack,
                            levelOneUnit: prod.levelOneUnit,
                            basicUnit: prod.basicUnit,
                            inventory: prod.inventory,
                            inventoryList: prod.inventoryBatch == null ? null : prod.inventoryBatch,
                            discountSign: '%',
                        });

                        var product = this.purchaseProducts.find((x) => {
                            return x.productId == productId && x.rowId == uid;
                        });
                        if (isTemplate) {
                            this.updateLineTotal(unitPrice, "unitPrice", product);

                        }
                        this.getVatValue(product.taxRateId, product);

                        this.product.id = "";
                    }
                }
            },

            updateBatch: function (productId, batch) {

                var prd = this.purchaseProducts.find(x => x.productId == productId);
                if (prd != undefined) {
                    prd.batchNo = batch.batchNumber;
                    prd.batchExpiry = batch.expiryDate;
                }
                this.updateLineTotal(prd.quantity, "quantity", prd);
            },

            getDate: function (x) {
                return moment(x).format("l");
            },

            EmtypurchaseProductsList: function () {
                this.purchaseProducts = [];
                this.products = [];
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

                root.$https.get("/Product/TaxRateList", {headers: { Authorization: `Bearer ${token}` },})
                    .then(function (response) {
                        if (response.data != null) {
                            root.vats = response.data.taxRates;
                        }
                    }).then(function () {

                        if (root.$route.query.data != undefined) {
                            if (root.$route.query.data.saleOrderItems != undefined) {
                                
                                root.$route.query.data.saleOrderItems.forEach(function (item) {
                                    root.purchaseProducts.push({
                                        rowId: item.id,
                                        id: item.id,
                                        batchNo: item.batchNo,
                                        discount: item.discount,
                                        expiryDate: item.expiryDate,
                                        isExpire: item.isExpire,
                                        fixDiscount: item.fixDiscount,
                                        product: item.product,
                                        description: item.description,
                                        inventoryList: item.product.inventoryBatch == null ? null : item.product.inventoryBatch,
                                        currentQuantity: item.product.inventory == null ? 0 : item.product.inventory.currentQuantity,
                                        salePrice: item.product.salePrice == null ? 0 : item.product.salePrice,
                                        wholesaleQuantity: item.product.wholesaleQuantity,
                                        wholesalePrice: item.product.wholesalePrice,
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
                                        inventory: item.product.inventory,
                                        serial: item.serial,
                                        scheme: item.scheme,
                                        schemeQuantity: item.schemeQuantity,
                                        schemePhysicalQuantity: item.schemePhysicalQuantity,
                                        guaranteeDate: item.guaranteeDate,
                                        isSerial: item.product.serial,
                                        highUnitPrice: item.product.highUnitPrice,
                                        guarantee: item.product.guarantee,
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

            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false;
            this.decimalQuantity = localStorage.getItem('decimalQuantity') == 'true' ? true : false;
            this.soInventoryReserve = localStorage.getItem('SoInventoryReserve') == 'true' ? true : false;
            this.invoiceWoInventory = localStorage.getItem('InvoiceWoInventory') == 'true' ? true : false;
            this.IsService = localStorage.getItem('IsService') == 'true' ? true : false;


            this.saleDefaultVat = localStorage.getItem('SaleDefaultVat');


            //this.$barcodeScanner.init(this.onBarcodeScanned);
            //For Scanner Code
            var root = this;
            var barcode = '';
            var interval;
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
            localStorage.setItem("BarcodeScan", 'SaleOrder')
            //End
            this.getData();
        },
        mounted: function () {
            //this.GetProductList();
            this.currency = localStorage.getItem('currency');
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            this.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;
            this.colorVariants = localStorage.getItem('ColorVariants') == 'true' ? true : false;
            this.IsService = localStorage.getItem('IsService') == 'true' ? true : false;

        },
    };
</script>

<style scoped>
    /* Chrome, Safari, Edge, Opera */
    input::-webkit-outer-spin-button,
    input::-webkit-inner-spin-button {
        -webkit-appearance: none;
        margin: 0;
    }

    /* Firefox */
    input[type=number] {
        -moz-appearance: textfield;
    }
</style>

