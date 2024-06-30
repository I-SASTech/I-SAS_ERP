<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">

        <!--<table class="table table-striped table-hover table_list_bg">
            <tr>
                <td  class="border-top-0" >
                    <rawProductDropdown v-bind:key="rendered"
                                      @update:modelValues="addProduct"
                                      width="100%" />
                </td>
            </tr>
        </table>-->
        <div class=" table-responsive" v-if="readOnly">

            <table class="table add-table_list_bg" v-if="purchaseProducts.length > 0" style="table-layout:fixed;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                <thead style="background-color: #3178f6;color:#ffffff;">
                    <tr>
                        <th style="width: 20%;">
                            {{ $t('AddProductionBatch.RawProduct') }}
                        </th>

                        <th style="width: 10%;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('AddProductionBatch.HighQty') }}
                        </th>
                        <th hidden style="width: 15%;" class="text-center">
                            {{ $t('AddProductionBatch.CurrentQuantity') }}
                        </th>
                        <th style="width: 10%;" class="text-center">
                            {{ $t('AddProductionBatch.Qty') }}
                        </th>

                        <!--<th style="width: 15%;" class="text-center">
                            {{ $t('PurchaseOrder.TOTALWITHOUTWASTE') }}
                        </th>-->
                        <!--<th style="width: 10%;" class="text-center">
                            {{ $t('PurchaseOrder.WASTE') }}%
                        </th>-->
                        <!--<th style="width: 10%;" class="text-center">
                            {{ $t('PurchaseOrder.LineTotal') }}
                        </th>-->
                        <th style="width: 5%"></th>
                    </tr>
                </thead>
                <tbody id="contractor-item">

                    <tr v-for="(prod , index) in purchaseProducts.slice().reverse()" :key="prod.productId + index" style="background:#EAF1FE;">
                        <td>
                            <!--<product-dropdown v-model="prod.productId"
                            @update:modelValues="changeProduct($event, prod.rowId, prod.productId)" />-->
                            <span v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                {{products.find(x => x.id == prod.productId).englishName}}
                            </span>
                            <span v-else>
                                {{products.find(x => x.id == prod.productId).arabicName}}
                            </span>
                        </td>


                        <td v-if="isMultiUnit=='true'">
                            <input type="number" readonly v-model="prod.highQuantity"
                                   @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                   class="form-control input-border text-center tableHoverOn"
                                   @keyup="updateLineTotal($event.target.value, 'highQuantity', prod)" />
                            <div class="text-center">{{prod.levelOneUnit}}</div>
                        </td>
                        <td hidden class="border-top-0 text-center">
                            {{prod.currentQuantity}}
                        </td>
                        <td>
                            <input type="number" readonly v-model="prod.quantity"
                                   @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                   class="form-control input-border text-center tableHoverOn"
                                   @keyup="updateLineTotal($event.target.value, 'quantity', prod)" />
                            <div class="text-center" v-if="isMultiUnit=='true'">{{prod.basicUnit}}</div>
                        </td>

                        <!--<td class="border-top-0 text-center">
                            {{parseFloat(prod.withoutWastageTotal).toFixed(3).slice(0,-1) | formatAmount}}
                        </td>-->
                        <!--<td >
                            <input type="number" v-model="prod.waste"
                                   @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                   class="form-control input-border text-center"
                                   @keyup="updateLineTotal($event.target.value, 'waste', prod)" />
                        </td>-->
                        <!--<td class="border-top-0 text-center">
                            {{prod.lineTotal}}
                        </td>-->
                        <!--<td class="border-top-0 pt-0">
                            <button @click="removeProduct(prod.rowId)"
                                    title="Remove Item"
                                    class="btn btn-secondary btn-neutral btn-round btn-sm  btn-icon">
                                <i class="nc-icon nc-simple-remove"></i>
                            </button>
                        </td>-->
                    </tr>
                </tbody>
            </table>
        </div>

        <div class=" table-responsive" v-else>

            <table class="table add-table_list_bg" v-if="purchaseProducts.length > 0" style="table-layout:fixed;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                <thead style="background-color: #3178f6;color:#ffffff;">
                    <tr>
                        <th style="width: 20%;">
                            {{ $t('AddProductionBatch.RawProduct') }}
                        </th>

                        <th style="width: 10%;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('AddProductionBatch.HighQty') }}
                        </th>
                        <th hidden style="width: 15%;" class="text-center">
                            {{ $t('AddProductionBatch.CurrentQuantity') }}
                        </th>
                        <th style="width: 20%;">
                            {{ $t('AddProductionBatch.WareHouse') }}
                        </th>
                        <th style="width: 10%;" class="text-center">
                            {{ $t('AddProductionBatch.Qty') }}
                        </th>

                        <!--<th style="width: 15%;" class="text-center">
                            {{ $t('PurchaseOrder.TOTALWITHOUTWASTE') }}
                        </th>-->
                        <!--<th style="width: 10%;" class="text-center">
                            {{ $t('PurchaseOrder.WASTE') }}%
                        </th>-->
                        <!--<th style="width: 10%;" class="text-center">
                            {{ $t('PurchaseOrder.LineTotal') }}
                        </th>-->
                        <th style="width: 5%"></th>
                    </tr>
                </thead>
                <tbody id="contractor-item">

                    <tr v-for="(prod , index) in purchaseProducts.slice().reverse()" :key="prod.productId + index" style="background:#EAF1FE;" v-bind:class="{'alert-danger':prod.outOfStock}">
                        <td>
                            <!--<product-dropdown v-model="prod.productId"
                            @update:modelValues="changeProduct($event, prod.rowId, prod.productId)" />-->
                            <span v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                {{products.find(x => x.id == prod.productId).englishName}}
                            </span>
                            <span v-else>
                                {{products.find(x => x.id == prod.productId).arabicName}}
                            </span>
                        </td>


                        <td v-if="isMultiUnit=='true'">
                            <input type="number" v-model="prod.highQuantity"
                                   @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                   class="form-control input-border text-center tableHoverOn"
                                   @keyup="updateLineTotal($event.target.value, 'highQuantity', prod)" />
                            <div class="text-center">{{prod.levelOneUnit}}</div>
                        </td>
                        <td hidden class="border-top-0 text-center">
                            {{prod.currentQuantity}}
                        </td>
                        <td>

                            <warehouse-dropdown v-model="prod.wareHouseId" @update:modelValue="updateLineTotal($event.target.value, 'wareHouse', prod)" :dropdownpo="'dropdownpo'" />
                        </td>
                        <td>
                            <input type="number" v-model="prod.quantity"
                                   @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                   class="form-control input-border text-center tableHoverOn"
                                   @keyup="updateLineTotal($event.target.value, 'quantity', prod)" />
                            <div class="text-center" v-if="isMultiUnit=='true'">{{prod.basicUnit}}</div>
                        </td>

                        <!--<td class="border-top-0 text-center">
                            {{parseFloat(prod.withoutWastageTotal).toFixed(3).slice(0,-1) | formatAmount}}
                        </td>-->
                        <!--<td >
                            <input type="number" v-model="prod.waste"
                                   @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                   class="form-control input-border text-center"
                                   @keyup="updateLineTotal($event.target.value, 'waste', prod)" />
                        </td>-->
                        <!--<td class="border-top-0 text-center">
                            {{prod.lineTotal}}
                        </td>-->
                        <td class="border-top-0 pt-0">
                            <button @click="removeProduct(prod.rowId)"
                                    title="Remove Item"
                                    class="btn btn-secondary btn-neutral btn-round btn-sm  btn-icon">
                                <i class="nc-icon nc-simple-remove"></i>
                            </button>
                        </td>
                    </tr>

                </tbody>
            </table>
        </div>

        <!--<hr />-->
        <!--<div class=" table-responsive"
             v-bind:key="rendered + 'g'"
             v-if="purchaseProducts.length > 0">
            <table class="table " style="table-layout:fixed">
                <thead class="m-0">
                    <tr class="text-right">
                        <th class="text-center" style="width:85px;">
                            {{ $t('PurchaseOrder.NoItem') }}
                        </th>
                        <th class="text-center" style="width:100px;">
                            {{ $t('PurchaseOrder.TotalQty') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('PurchaseOrder.TOTALWITHOUTWASTE') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('PurchaseOrder.TOTALWITHWASTE') }}
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
                             {{ summary.total | formatAmount}}
                        </td>
                        <td>
                               {{ summary.totalWithWaste | formatAmount}}
                        </td>

                    </tr>
                </tbody>
            </table>
        </div>-->
    </div>
</template>


<script>
    //

    //import moment from "moment";
    //import Vue3Barcode from 'vue3-barcode'
    export default {
        name: "ProductionBatchItem",
        props: ['contractor', 'readOnly'],

        data: function () {
            return {
                rendered: 0,
                product: {
                    id: "",
                },
                products: [],
                purchaseProducts: [],
                loading: false,

                summary: {
                    item: 0,
                    qty: 0,
                    total: 0,
                    totalWithWaste: 0,
                },
                searchTerm: '',
                isMultiUnit: ''
            };
        },
        validations() {},
        filters: {

        },
        //watch: {
        //    purchaseProducts: function () {

        //        console.log(this.purchaseProducts);
        //
        //        var root = this;
        //        for (var y in this.purchaseProducts) {
        //            if (this.purchaseProducts[y].wareHouseId == undefined && this.purchaseProducts[y].wareHouseId == null) {
        //                root.disable = true;
        //            }
        //            else {
        //                root.disable = false;

        //            }

        //        }
        //        this.$emit("isdisable", this.disable);
        //    }
        //},
        methods: {

            changeProduct: function (NewProdId, rowId) {
                this.purchaseProducts = this.purchaseProducts.filter(x => x.rowId != rowId);
                this.addProduct(NewProdId);

            },
            calcuateSummary: function () {

                this.summary.item = this.purchaseProducts.length;
                this.summary.qty = this.purchaseProducts.reduce(
                    (qty, prod) => qty + parseInt(prod.quantity), 0);
                this.summary.total = this.purchaseProducts
                    .reduce((total, prod) => total + (prod.highQuantity * prod.unitPerPack + parseFloat(prod.quantity) * this.contractor.noOfBatches), 0)
                    .toFixed(3).slice(0, -1);
                this.summary.totalWithWaste = this.purchaseProducts
                    .reduce((total, prod) => total + (((prod.highQuantity * prod.unitPerPack) + parseFloat(prod.quantity)) * this.contractor.noOfBatches) - parseFloat(((((prod.highQuantity * prod.unitPerPack) + parseFloat(prod.quantity)) * prod.waste) * this.contractor.noOfBatches) / 100), 0)
                    .toFixed(3).slice(0, -1);
                this.$emit("update:modelValue", this.purchaseProducts);
            },

            updateLineTotal: function (e, prop, product) {

                //var prod = this.products.find((x) => x.id == product.productId);
                if (prop == "highQuantity") {
                    if (e < 0) {
                        e = 0;
                    }
                    product.highQuantity = Math.round(e);
                }
                if (prop == "quantity") {
                    if (e < 0) {
                        e = 0;
                    }
                    product.quantity = Math.round(e);
                }
                if (prop == "wareHouse") {
                    if (e < 0) {
                        e = 0;
                    }
                    product.wareHouseId = e;
                }

                if (prop == "waste") {
                    if (e < 0) {
                        e = 0;
                    }
                    product.waste = e;
                }
                if (prop == "currentQuantity") {
                    if (e < 0) {
                        e = 0;
                    }
                    product.currentQuantity = e;
                }
                if (product.quantity > product.remainingQuantity) {
                    product['outOfStock'] = true;
                } else {
                    product['outOfStock'] = false;
                }
                
                product.lineTotal = ((product.highQuantity * product.unitPerPack) + parseFloat(product.quantity));
                this.purchaseProducts['product'] = product;

                this.calcuateSummary();

                this.$emit("update:modelValue", this.purchaseProducts);

            },

            addProduct: function (productId, newProduct) {

                if (this.products.find(x => x.id == newProduct.id) == undefined || this.products.length <= 0) {
                    this.products.push(newProduct);
                }

                var prod = this.products.find((x) => x.id == productId);

                this.purchaseProducts.push({
                    rowId: this.createUUID(),
                    productId: prod.id,
                    unitPrice: 0,
                    levelOneUnit: prod.levelOneUnit,
                    highQuantity: 0,
                    unitPerPack: prod.unitPerPack,
                    basicUnit: prod.unit == null ? '' : prod.unit.name,
                    quantity: 0,
                    waste: 0,
                    lineTotal: 0,
                    withoutWastageTotal: 0,
                    wareHouseId: '',
                    currentQuantity: 0,
                    outOfStock: false,
                });
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


            removeProduct: function (id) {

                this.purchaseProducts = this.purchaseProducts.filter((prod) => {
                    return prod.rowId != id;
                });

                this.calcuateSummary();
            },

            getData: function () {
                var root = this;
                if (root.contractor != undefined) {
                    root.contractor.contractorItems.forEach(function (item) {
                        root.purchaseProducts.push({
                            rowId: item.id,
                            id: item.id,
                            product: item.product,
                            productId: item.productId,
                            quantity: item.remainingQuantity,
                            unitPerPack: item.unitPerPack,
                            basicUnit: item.unit == null ? '' : item.unit.name,
                            levelOneUnit: item.levelOneUnit,
                            highQuantity: item.highQuantity,
                            waste: item.waste,
                            wareHouseId: item.wareHouseId,
                            currentQuantity: item.currentQuantity,
                            remainingQuantity: item.remainingQuantity,
                        });
                    });

                    for (var k = 0; k < root.purchaseProducts.length; k++) {

                        root.products.push(root.purchaseProducts[k].product);

                        root.updateLineTotal(root.purchaseProducts[k].quantity, "quantity", root.purchaseProducts[k]);
                        root.updateLineTotal(root.purchaseProducts[k].unitPerPack, "unitPerPack", root.purchaseProducts[k]);
                        root.updateLineTotal(root.purchaseProducts[k].highQuantity, "highQuantity", root.purchaseProducts[k]);
                        root.updateLineTotal(root.purchaseProducts[k].waste, "waste", root.purchaseProducts[k]);
                    }
                    root.calcuateSummary()
                }

            },
        },
        created: function () {

            this.getData();
        },
        mounted: function () {
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
        },
    };
</script>
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

    #purchase-item tr td {
        vertical-align: baseline;
    }

    .tableHoverOn {
        background-color: #ffffff !important;
        height: 32px !important;
        max-height: 32px !important;
    }
</style>

