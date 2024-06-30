<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
        <div class=" table-responsive">
            <table class="table " v-if="purchaseProducts.length > 0" style="table-layout:fixed;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                <thead>
                    <tr>
                        <th style="width:5%;">
                            #
                        </th>
                        <th style="width:30%;">
                            {{ $t('ViewRecipeItem.RawProduct') }}
                        </th>
                        <!--<th style="width: 20%;" class="text-center">
                            {{ $t('SaleReturn.WareHouse') }}
                        </th>-->

                    <th style="width: 15%;" class="text-center" v-if="isMultiUnit=='true'">
                        {{ $t('ViewRecipeItem.HighQty') }}
                    </th>
                    <th style="width: 15%;" class="text-center">
                        {{ $t('ViewRecipeItem.Qty') }}
                    </th>
                    <th style="width: 20%;" class="text-center" hidden>
                        {{ $t('ViewRecipeItem.TOTALWITHOUTWASTE') }}
                    </th>
                    <th style="width: 15%;" class="text-center">
                        {{ $t('ViewRecipeItem.WASTE') }}%
                    </th>

                    <th style="width: 20%;" class="text-right">
                        {{ $t('ViewRecipeItem.TotalQuantity') }}
                    </th>
                    </tr>
                </thead>
                <tbody id="purchase-item">

                        <tr v-for="(prod , index) in purchaseProducts.slice().reverse()" :key="prod.productId + index" style="border-bottom: 1px solid #d6d6d6; ">
                            <td class="border-top-0">
                                {{index+1}}
                            </td>
                            <td class="border-top-0">
                                <!--<product-dropdown v-model="prod.productId"
                                @update:modelValues="changeProduct($event, prod.rowId, prod.productId)" />-->
                                <span v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                    {{products.find(x => x.id == prod.productId).englishName}}
                                </span>
                                <span v-else>
                                    {{products.find(x => x.id == prod.productId).arabicName}}
                                </span>
                            </td>
                            <!--<td class="border-top-0">
                                <warehouse-dropdown  v-model="prod.wareHouseId" :dropdownpo="'dropdownpo'" />
                            </td>-->

                            <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                                {{prod.highQuantity}}
                                <div class="text-center">{{prod.levelOneUnit}}</div>

                            </td>
                            <td class="border-top-0 text-center">
                                {{prod.quantity}}
                                <div class="text-center" v-if="isMultiUnit=='true'">{{prod.basicUnit}}</div>

                            </td>
                            <td class="border-top-0 text-center" hidden>
                                {{$formatAmount(parseFloat(prod.withoutWastageTotal).toFixed(3).slice(0,-1))  }}
                            </td>

                            <td class="border-top-0  text-center">
                                {{prod.waste}}
                                <!--<input type="number" v-model="prod.waste"
                                       @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                       class="form-control input-border text-center tableHoverOn"
                                       @keyup="updateLineTotal($event.target.value, 'waste', prod)" />-->
                            </td>
                            <td class="border-top-0 text-right">
                                {{prod.lineTotal}}
                            </td>
                        </tr>
                    
                </tbody>
            </table>
        </div>

        <hr hidden />
        <div class=" table-responsive" v-bind:key="rendered + 'g'" v-if="purchaseProducts.length > 0" hidden>
            <table class="table " style="table-layout:fixed">
                <thead class="m-0">
                    <tr class="text-right">
                        <th class="text-center" style="width:85px;">
                            {{ $t('ViewRecipeItem.NoItem') }}
                        </th>
                        <th class="text-center" style="width:100px;">
                            {{ $t('ViewRecipeItem.TotalQty') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('ViewRecipeItem.TOTALWITHOUTWASTE') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('ViewRecipeItem.TOTALWITHWASTE') }}
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
                            {{$formatAmount(summary.total)  }}
                        </td>
                        <td>
                            {{$formatAmount(summary.totalWithWaste)  }}
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
    //
    
    //import moment from "moment";
    //import VueBarcode from 'vue-barcode';
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
                    .reduce((total, prod) => total + prod.highQuantity * prod.unitPerPack + parseFloat(prod.quantity), 0)
                    .toFixed(3).slice(0,-1);
                this.summary.totalWithWaste = this.purchaseProducts
                    .reduce((total, prod) => total + ((prod.highQuantity * prod.unitPerPack) + parseFloat(prod.quantity)) - parseFloat((((prod.highQuantity * prod.unitPerPack) + parseFloat(prod.quantity)) * prod.waste) / 100), 0)
                    .toFixed(3).slice(0,-1);









                this.$emit("update:modelValue", this.purchaseProducts);
            },

            updateLineTotal: function (e, prop, product) {




                if (prop == "highQuantity") {
                    if (e < 0) {
                        e = 0;
                    }
                    product.highQuantity = e;
                }
                if (prop == "quantity") {
                    if (e < 0 || e == '') {
                        e = 0;
                    }
                    product.quantity = e;
                }

                if (prop == "waste") {
                    if (e < 0) {
                        e = 0;
                    }
                    product.waste = e;
                }

                product.withoutWastageTotal = product.highQuantity * product.unitPerPack + parseFloat(product.quantity);


                if (product.waste != 0) {
                    product.lineTotal = ((product.highQuantity * product.unitPerPack) + parseFloat(product.quantity)) - parseFloat((((product.highQuantity * product.unitPerPack) + parseFloat(product.quantity)) * product.waste) / 100);
                }
                else {
                    product.lineTotal = product.highQuantity * product.unitPerPack + parseFloat(product.quantity);
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



                this.purchaseProducts.push({
                    rowId: this.createUUID(),
                    productId: prod.id,
                    unitPrice: 0,
                    levelOneUnit: prod.levelOneUnit,
                    highQuantity: 0,
                    unitPerPack: prod.unitPerPack == null ? 0 : prod.unitPerPack,
                    basicUnit: prod.unit == null ? '' : prod.unit.name,
                    quantity: 0,
                    waste: 0,
                    lineTotal: 0,
                    withoutWastageTotal: 0,
                    wareHouseId: '',
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
                if (root.$route.query.data != undefined) {
                    //Recipt No Edit
                    if (root.$route.query.data.recipeNoItems != undefined) {
                        root.$route.query.data.recipeNoItems.forEach(function (item) {
                            root.purchaseProducts.push({
                                rowId: item.id,
                                id: item.id,
                                product: item.product,
                                productId: item.productId,
                                wareHouseId: item.wareHouseId,
                                quantity: item.quantity,
                                unitPerPack: item.unitPerPack,
                                basicUnit: item.unit == null ? '' : item.unit.name,
                                levelOneUnit: item.levelOneUnit,
                                highQuantity: item.highQuantity,
                                waste: item.waste,
                            });
                        });

                        for (var k = 0; k < root.purchaseProducts.length; k++) {
                            root.products.push(root.purchaseProducts[k].product);

                            root.updateLineTotal(root.purchaseProducts[k].quantity, "quantity", root.purchaseProducts[k]);
                            root.updateLineTotal(root.purchaseProducts[k].unitPerPack, "unitPerPack", root.purchaseProducts[k]);
                            root.updateLineTotal(root.purchaseProducts[k].basicUnit, "basicUnit", root.purchaseProducts[k]);
                            root.updateLineTotal(root.purchaseProducts[k].levelOneUnit, "levelOneUnit", root.purchaseProducts[k]);
                            root.updateLineTotal(root.purchaseProducts[k].highQuantity, "highQuantity", root.purchaseProducts[k]);
                            root.updateLineTotal(root.purchaseProducts[k].waste, "waste", root.purchaseProducts[k]);
                        }
                        root.calcuateSummary()
                    }

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
