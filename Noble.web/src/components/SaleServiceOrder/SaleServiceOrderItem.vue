<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
        <label>Add Items</label>

        <div v-if="invoiceWoInventory || (!invoiceWoInventory && !soInventoryReserve)">
            <product-dropdown v-bind:key="rendered"
                              @update:modelValues="addProduct"
                              :isservice="true"
                              width="100%" />
        </div>
        <div v-else>
            <product-dropdown :wareHouseId="wareHouseId"
                              :raw="false"
                              @update:modelValues="addProduct"
                              :isservice="true"
                              width="100%" />
        </div>

        <div class=" table-responsive mt-3">
            <table class="table add-table_list_bg" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                <thead style="background-color: #3178f6;color:#ffffff;">
                    <tr>
                        <th style="width: 30px;">
                            #
                        </th>
                        <th style="width: 150px;">
                            {{ $t('SaleOrderItem.Product') }}
                        </th>
                        <th class="text-center" style="width: 200px;">
                            {{ $t('SaleOrderItem.Description') }}
                        </th>
                        <th style="width: 110px;" class="text-center">
                            {{ $t('SaleOrderItem.UnitPrice') }}
                        </th>
                        <th class="text-center" style="width: 100px;" v-if="!invoiceWoInventory">
                            {{ $t('SaleOrderItem.CurrentQuantity') }}
                        </th>

                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">

                            {{ $t('SaleOrderItem.HighQty') }}
                        </th>
                        <th style="width: 110px;" class="text-center">
                            {{ $t('SaleOrderItem.Qty') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('SaleOrderItem.TOTALQTY') }}
                        </th>

                        <th class="text-center" style="width: 100px;" v-if="isSerial  && !(invoiceWoInventory || (!invoiceWoInventory && !soInventoryReserve))">
                            {{ $t('SaleOrderItem.Serial') }}
                        </th>
                        <th style="width: 100px;" v-if="isSerial  && !(invoiceWoInventory || (!invoiceWoInventory && !soInventoryReserve))">
                            {{ $t('SaleOrderItem.Guarantee') }}
                        </th>

                        <th style="width: 100px;" class="text-center">
                            {{ $t('SaleOrderItem.Disc%') }}
                        </th>
                        <th style="width: 100px;" class="text-center">
                            {{ $t('SaleOrderItem.FixDisc') }}
                        </th>

                        <th v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'" style="width: 110px;">
                            {{ $t('AddPurchase.TaxMethod') }}
                        </th>
                        <th v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'" style="width: 135px;">
                            {{ $t('AddPurchase.VAT%') }}
                        </th>

                        <th class="text-center" style="width: 60px;">
                            {{ $t('SaleItem.Free') }}
                        </th>
                        <th style="width: 100px;" class="text-right">
                            {{ $t('SaleOrderItem.LineTotal') }}
                        </th>
                        <th style="width: 40px"></th>
                    </tr>
                </thead>
                <tbody id="purchase-item">
                    
                        <tr v-for="(prod , index) in purchaseProducts" :key="prod.productId + index" style="background:#EAF1FE;" v-bind:class="{'alert-danger':prod.outOfStock}">
                            <td>
                                {{index+1}}
                            </td>
                            <td v-if="prod.productId!=null">
                                <span>{{products.find(x => x.id == prod.productId).code}}</span> <br />
                                <span style="font-size:10px !important"> 
                                {{($i18n.locale == 'en' ||isLeftToRight())? products.find(x => x.id == prod.productId).englishName!=''? products.find(x => x.id == prod.productId).englishName : products.find(x => x.id == prod.productId).arabicName :    products.find(x => x.id == prod.productId).arabicName!=''? products.find(x => x.id == prod.productId).arabicName : products.find(x => x.id == prod.productId).englishName}}
                                </span>
                            </td>

                            <td v-bind:colspan="prod.productId==null?2:1">
                                <textarea data-gramm="false" class="form-control input-border" style="background-color: #ffffff !important;padding: 0 5px 0 5px;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'"
                                          v-model="prod.description" />
                            </td>

                            <td>
                                <decimal-to-fixed v-model="prod.unitPrice" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.unitPrice, 'unitPrice', prod)" />
                            </td>
                            <!--<td class=" text-center">
        {{currency}} {{parseFloat(prod.salePrice).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
    </td>-->
                            <td class=" text-center" v-if="!invoiceWoInventory">
                                {{prod.currentQuantity}}
                            </td>
                            <td class=" text-center" v-if="isMultiUnit=='true'">
                                <input type="number" v-model="prod.highQty"
                                       @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                       class="form-control input-border text-center tableHoverOn"
                                       @keyup="updateLineTotal($event.target.value, 'highQty', prod)" />
                                <small style="font-weight: 500;font-size:70%;">
                                    {{prod.levelOneUnit}}
                                </small>
                            </td>
                            <td class=" text-center">
                                <input type="number" v-model="prod.quantity"
                                       @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                       class="form-control input-border text-center tableHoverOn"
                                       @keyup="updateLineTotal($event.target.value, 'quantity', prod)" />
                                <small style="font-weight: 500;font-size:70%;" v-if="isMultiUnit=='true'">
                                    {{prod.basicUnit}}
                                </small>
                            </td>
                            <td class=" text-center" v-if="isMultiUnit=='true'">
                                {{parseInt(parseFloat(prod.highQty*prod.unitPerPack) + parseFloat(prod.quantity))}}
                            </td>

                            <td class="text-center " v-if="isSerial  && !(invoiceWoInventory || (!invoiceWoInventory && !soInventoryReserve))">
                                <button @click="AddSerial(prod)" v-if="prod.isSerial" title="Add Serial" class="btn btn-primary btn-sm"> Add Serial </button>
                                <span v-else>-</span>
                            </td>
                            <td class="  text-center" v-if="isSerial  && !(invoiceWoInventory || (!invoiceWoInventory && !soInventoryReserve))">
                                <datepicker v-model="prod.guaranteeDate" v-if="prod.guarantee" />
                                <span v-else>-</span>
                            </td>

                            <td>
                                <decimal-to-fixed v-model="prod.discount" v-bind:disable="prod.fixDiscount != 0?true:false" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.discount, 'discount', prod)" />
                            </td>
                            <td>
                                <decimal-to-fixed v-model="prod.fixDiscount" :disable="prod.discount != 0?true:false" v-bind:salePriceCheck="false" @update:modelValue="updateLineTotal(prod.fixDiscount, 'fixDiscount', prod)" />
                            </td>

                            <td class="border-top-0" v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'">
                                <multiselect :options="options" v-model="prod.taxMethod" @update:modelValue="getTaxMethod(prod.taxMethod, prod)" :show-labels="false" v-bind:placeholder="$t('PurchaseItem.TaxMethod')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                </multiselect>
                            </td>

                            <td class="border-top-0" v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'">
                                <taxratedropdown v-model="prod.taxRateId" @update:modelValue="getVatValue(prod.taxRateId, prod)" />
                            </td>

                            <td class="text-center ">
                                <input type="checkbox" class="checkBoxHeight" v-model="prod.isFree"
                                       v-on:change="updateLineTotal(prod.isFree, 'isFree', prod)">
                            </td>

                            <td class=" text-right">
                                {{currency}}  {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0,-1))  }}
                            </td>
                            <td class=" pt-0">
                                <button @click="removeProduct(prod.rowId)"
                                        title="Remove Item"
                                        class="btn btn-secondary btn-neutral btn-round btn-sm  btn-icon">
                                    <i class="nc-icon nc-simple-remove"></i>
                                </button>
                            </td>
                        </tr>
                    

                    <tr style="background:#EAF1FE;">
                        <td></td>

                        <td colspan="2">
                            <textarea data-gramm="false" class="form-control input-border" style="background-color: #ffffff !important;padding: 0 5px 0 5px;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'"
                                      v-model="newItem.description" />
                        </td>

                        <td>
                            <decimal-to-fixed v-model="newItem.unitPrice" />
                        </td>

                        <td class=" text-center" v-if="!invoiceWoInventory">
                        </td>
                        <td class=" text-center" v-if="isMultiUnit=='true'">
                            <input type="number" v-model="newItem.highQty"
                                   @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                   class="form-control input-border text-center tableHoverOn" />
                        </td>

                        <td class=" text-center">
                            <input type="number" v-model="newItem.quantity"
                                   @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                   class="form-control input-border text-center tableHoverOn" />
                        </td>
                        <td class=" text-center" v-if="isMultiUnit=='true'">
                        </td>

                        <td class="text-center " v-if="isSerial  && !(invoiceWoInventory || (!invoiceWoInventory && !soInventoryReserve))">
                        </td>

                        <td class="  text-center" v-if="isSerial  && !(invoiceWoInventory || (!invoiceWoInventory && !soInventoryReserve))">
                        </td>

                        <td>
                            <decimal-to-fixed v-model="newItem.discount" v-bind:disable="newItem.fixDiscount != 0?true:false" />
                        </td>
                        <td>
                            <decimal-to-fixed v-model="newItem.fixDiscount" v-bind:disable="newItem.discount != 0?true:false" />
                        </td>

                        <td v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'" class="text-center "></td>
                        <td v-if="saleDefaultVat == 'DefaultVatItem' || saleDefaultVat =='DefaultVatHeadItem'" class=" text-right"></td>
                        
                        <td class="text-center "></td>
                        <td class=" text-right"></td>

                        <td class=" pt-0">
                            <button @click="newItemProduct()"
                                    title="Add Item" v-bind:disabled="newItem.description==''"
                                    class="btn btn-primary btn-round btn-sm  btn-icon">
                                <i class="fa fa-check"></i>
                            </button>
                        </td>
                    </tr>

                </tbody>
            </table>
        </div>

        <div class=" table-responsive mt-3"
             v-bind:key="rendered + 'g'">
            <table class="table " style="table-layout:fixed">
                <thead class="m-0" style=" background-color: #3178F6; color:#ffffff;">
                    <tr class="text-right">
                        <th class="text-center" style="width:85px;">
                            {{ $t('SaleOrderItem.NoItem') }}
                        </th>
                        <th class="text-center" style="width:100px;" v-if="isMultiUnit=='true'">
                            {{ $t('SaleOrderItem.TotalCarton') }}
                        </th>
                        <th class="text-center" style="width:100px;" v-if="isMultiUnit=='true'">
                            {{ $t('SaleOrderItem.TotalPieces') }}
                        </th>
                        <th class="text-center" style="width:100px;">
                            {{ $t('SaleOrderItem.TotalQty') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('SaleOrderItem.Total') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('SaleOrderItem.Disc') }}
                        </th>
                        <th style="width:155px;">
                            {{ $t('SaleOrderItem.TotalAfterDisc') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('SaleOrderItem.TotalVAT') }}
                        </th>
                        <th style="width:140px;">
                            {{ $t('SaleOrderItem.TotalwithVAT') }}
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-right" style="background-color:#EAF1FE;">
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
        mixins: [clickMixin],
        name: "PurchaseItem",
        components: {
            Multiselect
        },
        props: ['purchase', 'purchaseItems', 'wareHouseId', 'taxMethod', 'taxRateId'],

        data: function () {
            return {
                newItem: {
                    description: '',
                    unitPrice: 0,
                    highQty: 0,
                    quantity: 0,
                    discount: 0,
                    fixDiscount: 0
                },

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
                showSerial: false,
                saleDefaultVat: ''
            };
        },
        validations() {},
        filters: {

        },
        methods: {
            CheckRecordInProduct: function () {
                
                return this.$refs.productDropdownRef.productListCheck();
            },
            newItemProduct: function () {
                var taxRateId = '';
                var taxMethod = '';
                if (this.saleDefaultVat == 'DefaultVatHead' || this.saleDefaultVat == 'DefaultVatHeadItem') {                    
                    taxRateId = this.taxRateId;
                    taxMethod = this.taxMethod;
                }
                else {
                    taxRateId = localStorage.getItem('TaxRateId');
                    taxMethod = localStorage.getItem('taxMethod');
                }

                var vat = this.vats.find((value) => value.id == taxRateId);

                var rowId = this.createUUID();
                this.purchaseProducts.push({
                    rowId: rowId,
                    productId: null,
                    unitPrice: this.newItem.unitPrice,
                    quantity: this.newItem.quantity,
                    highQty: this.newItem.highQty,
                    discount: this.newItem.discount,
                    description: this.newItem.description,
                    serviceItem: true,
                    salePrice: 0,
                    currentQuantity: 0,
                    fixDiscount: this.newItem.fixDiscount,
                    taxRateId: taxRateId,
                    rate: vat.rate,
                    totalPiece: 0,

                    taxMethod: taxMethod,
                    lineTotal: 0,
                    serial: '',
                    guaranteeDate: '',
                    isSerial: false,
                    guarantee: false,
                    unitPerPack: 0,
                    levelOneUnit: '',
                    basicUnit: '',
                    inventory: null,
                    isFree: false,
                });

                this.newItem.description = '';
                this.newItem.unitPrice = 0;
                this.newItem.highQty = 0;
                this.newItem.quantity = 0;
                this.newItem.discount = 0;
                this.newItem.fixDiscount = 0;

                var product = this.purchaseProducts.find((x) => {
                    return x.rowId == rowId;
                });

                this.updateLineTotal(product.quantity, "quantity", product);
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
                    this.summary.qty = this.purchaseProducts.reduce(
                        (qty, prod) => qty + parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                }
                else {
                    this.summary.qty = this.purchaseProducts.reduce(
                        (qty, prod) => qty + parseInt(prod.totalPiece == '' ? 0 : prod.totalPiece), 0);
                }

                this.summary.total = this.purchaseProducts
                    .reduce((total, prod) => total + (prod.isFree ? 0 : (parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece) * prod.unitPrice)), 0)
                    .toFixed(3).slice(0, -1);

                var discount = this.purchaseProducts
                    .filter((x) => x.discount != 0 || x.discount != "")
                    .reduce(
                        (discount, prod) =>
                            discount + (prod.isFree ? 0 : (((prod.totalPiece == '' ? 0 : prod.totalPiece) * prod.unitPrice * prod.discount) / 100)), 0);

                var fixDiscount = this.purchaseProducts
                    .filter((x) => x.fixDiscount != 0 || x.fixDiscount != "")
                    .reduce((discount, prod) => discount + (prod.isFree ? 0 : (prod.totalPiece * prod.fixDiscount)), 0);

                this.summary.discount = (parseFloat(discount) + parseFloat(fixDiscount)).toFixed(3).slice(0, -1);

                this.summary.withDisc = (this.summary.total - this.summary.discount).toFixed(2);

                this.summary.vat = this.purchaseProducts
                    .reduce((vat, prod) => parseFloat(vat) + (prod.isFree ? 0 : ((prod.taxMethod == "Exclusive" || prod.taxMethod == "غير شامل") ? ((((parseFloat(prod.unitPrice) * parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece)) -

                        ((prod.fixDiscount != 0 || prod.fixDiscount != "" ?
                            (prod.fixDiscount * prod.totalPiece) :
                            ((prod.totalPiece == '' ? 0 : prod.totalPiece) * prod.unitPrice * prod.discount) / 100)))

                        * parseFloat(root.getVatValueForSummary(prod.taxRateId, prod))) / 100) : 0)), 0)
                    .toFixed(3).slice(0, -1);

                this.summary.inclusiveVat = this.purchaseProducts
                    .reduce((vat, prod) => parseFloat(vat) + (prod.isFree ? 0 : ((prod.taxMethod == "Inclusive" || prod.taxMethod == "شامل") ? ((((parseFloat(prod.unitPrice) * parseFloat(prod.totalPiece == '' ? 0 : prod.totalPiece)) -

                        ((prod.fixDiscount != 0 || prod.fixDiscount != "" ?
                            (prod.fixDiscount * prod.totalPiece) :
                            ((prod.totalPiece == '' ? 0 : prod.totalPiece) * prod.unitPrice * prod.discount) / 100)))

                        * parseFloat(root.getVatValueForSummary(prod.taxRateId, prod))) / (100 + prod.rate)) : 0)), 0);

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
                    if (String(e).split('.').length > 1 && String(e).split('.')[1].length > 2)
                        e = parseFloat(String(e).slice(0, -1))
                    product.quantity = this.decimalQuantity ? e : Math.round(e);
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
                    product.highQty = Math.round(e);
                }
                product.totalPiece = (parseFloat(product.highQty == undefined ? 0 : product.highQty) * parseFloat(product.unitPerPack == null ? 0 : product.unitPerPack)) + parseFloat(product.quantity == '' ? 0 : product.quantity);



                if (this.soInventoryReserve && !this.invoiceWoInventory && product.productId != null) {
                    if (!product.serviceItem) {
                        if (product.inventory != null) {
                            if (parseFloat(product.totalPiece) > product.inventory.currentQuantity) {
                                product['outOfStock'] = true
                            }
                            else {
                                product['outOfStock'] = false
                            }
                        }
                        else {
                            product['outOfStock'] = true
                            
                        }
                    }
                }

                discount = product.discount == 0 ? (product.fixDiscount * product.totalPiece) : (product.totalPiece * product.unitPrice * product.discount) / 100;
                var vat = this.vats.find((value) => value.id == product.taxRateId);

                var total = (product.totalPiece == '' ? 0 : product.totalPiece) * product.unitPrice - discount;
                var calculateVAt = 0;
                if (product.taxMethod == 'Exclusive' || product.taxMethod == 'غير شامل') {
                    calculateVAt = (total * vat.rate) / 100;
                    product.lineTotal = total + calculateVAt;
                }
                else {
                    product.lineTotal = total;
                }

                this.purchaseProducts['product'] = product


                this.calcuateSummary();

                this.$emit("update:modelValue", this.purchaseProducts);

            },

            addProduct: function (productId, newProduct, soItem, quotation, desc) {
                
                if (productId == null) {
                    var rowId = this.createUUID();
                    var vat = this.vats.find((value) => value.id == soItem.taxRateId);

                    this.purchaseProducts.push({
                        rowId: rowId,
                        productId: null,
                        unitPrice: soItem.unitPrice,
                        quantity: soItem.quantity,
                        highQty: soItem.highQty,
                        discount: soItem.discount,
                        description: soItem.description,
                        serviceItem: soItem.serviceItem,
                        salePrice: 0,
                        currentQuantity: 0,
                        fixDiscount: soItem.fixDiscount,
                        taxRateId: soItem.taxRateId,
                        rate: vat.rate,
                        totalPiece: 0,
                        taxMethod: soItem.taxMethod,
                        lineTotal: 0,
                        serial: '',
                        guaranteeDate: '',
                        isSerial: false,
                        guarantee: false,
                        unitPerPack: 0,
                        levelOneUnit: '',
                        basicUnit: '',
                        inventory: null,
                        isFree: soItem.isFree,
                    });

                    var product1 = this.purchaseProducts.find((x) => {
                        return x.rowId == rowId;
                    });

                    this.getVatValue(product1.taxRateId, product1);
                }
                else {
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
                        

                        this.purchaseProducts.push({
                            rowId: this.createUUID(),
                            productId: prod.id,
                            unitPrice: quotation ? soItem.unitPrice : prod.salePrice,
                            quantity: quotation ? soItem.quantity : 1,
                            highQty: quotation ? soItem.highQty : 0,
                            discount: 0,
                            description: quotation ? desc : newProduct.description,
                            serviceItem: prod.serviceItem,
                            salePrice: prod.salePrice,
                            currentQuantity: prod.inventory == null ? 0 : prod.inventory.currentQuantity,
                            fixDiscount: 0,
                            taxRateId: quotation ? soItem.taxRateId : taxRateId,
                            rate: rate,
                            totalPiece: 0,
                            taxMethod: quotation ? soItem.taxMethod : taxMethod,
                            lineTotal: 0,
                            serial: '',
                            guaranteeDate: '',
                            isSerial: newProduct.serial,
                            guarantee: newProduct.guarantee,
                            unitPerPack: newProduct.unitPerPack,
                            levelOneUnit: prod.levelOneUnit,
                            basicUnit: prod.basicUnit,
                            inventory: prod.inventory,
                            isFree: quotation ? soItem.isFree : false,
                        });

                        var product = this.purchaseProducts.find((x) => {
                            return x.productId == productId;
                        });

                        this.getVatValue(product.taxRateId, product);

                        this.product.id = "";
                    }
                }


            },
            EmtypurchaseProductsList: function () {
                this.purchaseProducts = [];
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
                                    if (item.productId != null) {
                                        root.purchaseProducts.push({
                                            rowId: item.id,
                                            id: item.id,
                                            discount: item.discount,
                                            fixDiscount: item.fixDiscount,
                                            product: item.product,
                                            currentQuantity: item.product.inventory == null ? 0 : item.product.inventory.currentQuantity,
                                            salePrice: item.product.salePrice == null ? 0 : item.product.salePrice,
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
                                            description: item.description,
                                            serviceItem: item.serviceItem,
                                            serial: item.serial,
                                            guaranteeDate: item.guaranteeDate,
                                            isSerial: item.product.serial,
                                            guarantee: item.product.guarantee,
                                            isFree: item.isFree,
                                        });
                                    }
                                    else {
                                        root.purchaseProducts.push({
                                            rowId: item.id,
                                            id: item.id,
                                            discount: item.discount,
                                            fixDiscount: item.fixDiscount,
                                            product: item.product,
                                            currentQuantity: 0,
                                            salePrice: 0,
                                            productId: item.productId,
                                            purchaseId: item.purchaseId,
                                            quantity: item.quantity,
                                            highQty: item.highQty,
                                            taxMethod: item.taxMethod,
                                            taxRateId: item.taxRateId,
                                            unitPrice: item.unitPrice,
                                            unitPerPack: item.unitPerPack,
                                            levelOneUnit: '',
                                            basicUnit: '',
                                            inventory: null,
                                            description: item.description,
                                            serviceItem: item.serviceItem,
                                            serial: item.serial,
                                            guaranteeDate: item.guaranteeDate,
                                            isSerial: false,
                                            guarantee: false,
                                            isFree: item.isFree,
                                        });
                                    }


                                });

                                for (var k = 0; k < root.purchaseProducts.length; k++) {
                                    if (root.purchaseProducts[k].productId != null) {
                                        root.products.push(root.purchaseProducts[k].product);
                                        root.updateLineTotal(root.purchaseProducts[k].highQty, "highQty", root.purchaseProducts[k]);
                                    }
                                    root.updateLineTotal(root.purchaseProducts[k].quantity, "quantity", root.purchaseProducts[k]);
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
            this.isMultiUnit = 'false';
            this.isSerial = localStorage.getItem('IsSerial') == 'true' ? true : false;
        }
    };
</script>


<style scoped>
    .checkBoxHeight {
        width: 20px;
        height: 30px;
    }

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
        background-color: #ffffff !important;
        height: 32px !important;
        max-height: 32px !important;
    }
</style>
