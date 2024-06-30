<template>
    <div class="col-lg-12">
        <div class=" table-responsive mt-3">
            <table class="table mb-0" style="table-layout:fixed;">
                <thead class="thead-light">
                    <tr>
                        <th>
                            {{ $t('WareHouseTransferItem.Product') }}
                        </th>
                        <!-- <th class="text-center" >
                            {{ $t('WareHouseTransferItem.CurrentQuantity') }}
                        </th> -->
                        <th class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('WareHouseTransferItem.HighQty') }}
                        </th>
                        <th class="text-center">
                            {{ $t('Requested Qty') }}
                        </th>
                        <th class="text-center">
                            {{ $t('Transfer Qty') }}
                        </th>
                        <th class="text-center">
                            {{ $t('Received Qty') }}
                        </th>
                        <th class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('WareHouseTransferItem.TOTALQTY') }}
                        </th>
                        <th class="text-center" v-if="branchType!='Simple Branches'">
                            {{ $t('Price') }}
                        </th>
                        <th class="text-center" v-if="branchType!='Simple Branches'">
                            {{ $t('Transfer Price') }}
                        </th>
                        <th class="text-end" v-if="branchType!='Simple Branches'">
                            {{ $t('PurchaseItem.LineTotal') }}
                        </th>
                        <th class="text-end"></th>
                    </tr>
                </thead>
                <tbody >
                    <tr v-for="(prod , index) in wareHouseTransferProducts.slice().reverse()" :key="index">
                        <td class="border-top-0">
                            {{($i18n.locale == 'en' ||isLeftToRight())? products.find(x => x.id == prod.productId).englishName!=''? products.find(x => x.id == prod.productId).englishName : products.find(x => x.id == prod.productId).arabicName :    products.find(x => x.id == prod.productId).arabicName!=''? products.find(x => x.id == prod.productId).arabicName : products.find(x => x.id == prod.productId).englishName}}

                        </td>
                        <!-- <td class="border-top-0 text-center">
                            {{prod.currentQuantity}}
                        </td> -->
                        <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                            <input type="number" v-model="prod.highQty"
                                   style=""
                                   @focus="$event.target.select()"
                                   class="form-control text-center "
                                   @keyup="updateLineTotal($event.target.value, 'highQty', prod)" />
                            <small style="font-weight: 500;font-size:70%;">
                                {{prod.levelOneUnit}}
                            </small>
                        </td>
                        <td class="border-top-0 text-center">
                            <input type="number" v-model="prod.quantity"
                                   @focus="$event.target.select()"
                                   disabled
                                   class="form-control  text-center "
                                   @keyup="updateLineTotal($event.target.value, 'quantity', prod)" />
                            <small v-if="isMultiUnit=='true'" style="font-weight: 500;font-size:70%;">
                                {{prod.basicUnit}}
                            </small>
                        </td>
                        <td class="border-top-0 text-center" v-if="isMultiUnit=='true'">
                            {{prod.totalPiece}}
                        </td>

                        <td class="border-top-0 text-center">
                            <input type="number" 
                                   @focus="$event.target.select()" 
                                   disabled
                                   class="form-control  text-center " 
                                   v-model="prod.transferQuantity"  
                                   @keyup="updateLineTotal($event.target.value, ' transferQuantity', prod)" />
                        </td>
                        <td class="border-top-0 text-center">
                            <input type="number" 
                                   @focus="$event.target.select()" 
                                   class="form-control  text-center " 
                                   v-model="prod.receivedQuantity"  
                                   @keyup="updateLineTotal($event.target.value, 'receivedQuantity', prod)" />
                        </td>
                        <td class="border-top-0 text-center" v-if="branchType!='Simple Branches'">
                            <input type="number" class="form-control  text-center " v-model="prod.averagePrice" />
                        </td>
                        
                        <td class="border-top-0 text-center" v-if="branchType!='Simple Branches'">
                            <input type="number" 
                                   @focus="$event.target.select()" 
                                   class="form-control  text-center " 
                                   v-model="prod.transferAmount" 
                                   disabled 
                                   @keyup="updateLineTotal($event.target.value, 'transferAmount', prod)" />
                        </td>
                        <td class="border-top-0 text-end" v-if="branchType!='Simple Branches'">
                            {{ currency }} {{$formatAmount(parseFloat(prod.lineTotal).toFixed(3).slice(0, -1))   }}
                        </td>
                        <td class="border-top-0 pt-0  text-end">
                            <button @click="removeProduct(prod.id)"
                                    title="Remove Item"
                                    class="btn btn-sm ">
                                <i class="las la-trash-alt text-secondary font-16"></i>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>


        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="mt-4" >
                    <product-dropdown v-bind:key="rendered+wareHouseRander"
                                      @update:modelValues="addProduct"
                                      :wareHouseId="wareHouse"
                                      width="100%"
                                      :disable="true" />
                </div>
               
            </div>
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 " >
                <div class="mt-4 mb-5" v-bind:key="rendered + 'g'">
                    <table class="table" style="background-color:#EAF1FE;">
                        <tbody>
                            <tr>
                                <td colspan="2" style="width:65%;">
                                    <span style="font-weight:bolder; font-size:16px">{{ $t('PurchaseItem.TotalDue') }}({{ currency }})</span>
                                </td>
                                <td class="text-end" style="width: 35%; font-weight: bolder; font-size: 16px">
                                    {{ summary.totalAmount }}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>


    </div>
</template>


<script>
    
    //import moment from "moment";
    import clickMixin from '@/Mixins/clickMixin'


    //import VueBarcode from 'vue-barcode';
    export default {
        name: "StockReceivedItem",
        props: ['wareHouseTransferItem', 'wareHouse','isStockRequest', 'isStockTransfer','totalAmount'],
        mixins: [clickMixin],
        data: function () {
           
            return {
                branchType:'',
                currency:'',
                summary:{
                    totalAmount:0
                },

                decimalQuantity: false,
                isMultiUnit: '',
                rendered: 0,
                product: {
                    id: "",
                },
                products: [],
                wareHouseTransferProducts: [],
                loading: false,
                wareHouseRander: 0,
                searchTerm: '',
                productList: []
            };
        },
        validations() {},
        filters: {

        },
        methods: {
            wareHouseItemsClear: function(){
                this.wareHouseTransferProducts = [];
                this.productList = [];
                this.products = [];
            },
            changeProduct: function (NewProdId, prod, rowId) {
                if (this.wareHouse != undefined) {
                    this.wareHouseTransferProducts = this.wareHouseTransferProducts.filter(x => x.rowId != rowId);
                    this.addProduct(NewProdId);
                }

            },

            updateLineTotal: function (e, prop, product) {
                

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
                        e = '';
                    }
                    product.highQty = Math.round(e);
                }
                if (prop == "transferAmount") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = '';
                    }
                    product.transferAmount = Math.round(e);
                }
                if (prop == "receivedQuantity") {
                    
                    if (e < 0 || e == '' || e == undefined) {
                        e = '';
                    }
                    product.receivedQuantity = Math.round(e);
                }
                if (prop == "transferQuantity") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = '';
                    }
                    product.transferQuantity= Math.round(e);
                }
                console.log(product);
                product.lineTotal = product.receivedQuantity * product.transferAmount;


                //this.totalAmount = product.lineTotal;

                product.totalPiece = (parseFloat(product.highQty == undefined ? 0 : product.highQty) * parseFloat(product.unitPerPack == null ? 0 : product.unitPerPack)) + parseFloat(product.quantity == '' ? 0 : product.quantity);
                if (product.currentQuantity != null) {
                    if (parseFloat(product.totalPiece) > product.currentQuantity) {
                        product['outOfStock'] = true
                    } else {
                        product['outOfStock'] = false
                    }
                } else {
                    product['outOfStock'] = true
                }

                this.calcuateSummary();
                
                this.wareHouseTransferProducts['product'] = product



                this.$emit("update:modelValue", this.wareHouseTransferProducts);
            },
            calcuateSummary: function(){
                this.summary.totalAmount = this.wareHouseTransferProducts.reduce((total, prod) =>
                    total + (prod.receivedQuantity) * prod.transferAmount, 0).toFixed(3).slice(0, -1);  
               
                this.summary.totalAmount = this.stringToMath(this.summary.totalAmount);
                this.$emit("summary", this.summary);
            },
            stringToMath:function(e)
            {
                if (e < 0 || e == '' || e == undefined) {
                        e = '';
                    }
                var aa = Math.round(e);

                return aa
            },
            addProduct: function (productId, newProduct) {
                
                if (this.products.find(x => x.id == newProduct.id) == undefined || this.products.length <= 0) {
                    this.products.push(newProduct);
                }

                var prod = this.products.find((x) => x.id == productId);


                if (this.wareHouseTransferProducts.find(x => x.productId == productId) == undefined) {

                    this.wareHouseTransferProducts.push({
                        rowId: this.createUUID(),
                        productId: prod.id,
                        highQty: 0,
                        quantity: 0,
                        currentQuantity: prod.inventory.currentQuantity,
                        product: prod,
                        averagePrice :  prod.inventory.averagePrice,
                        amount:prod.inventory.averagePrice,
                        transferAmount: 0,
                        receivedQuantity : 0,
                        transferQuantity: 0,
                    });
                    this.updateLineTotal(1, "quantity", prod);
                    this.updateLineTotal(0, "highQty", prod);

                } else {
                    var prd = this.wareHouseTransferProducts.find(x => x.productId == productId);
                    prd.quantity++;

                    this.updateLineTotal(prd.quantity, "quantity", prd);
                    this.updateLineTotal(prd.highQty, "highQty", prd);

                }

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
                this.wareHouseTransferProducts = this.wareHouseTransferProducts.filter((prod) => {
                    return prod.rowId != id;
                });

            },

        },
        created: function () {
            this.currency = localStorage.getItem('currency');
            this.branchType = localStorage.getItem('BranchType');
        },
        mounted: function () {
            
            this.decimalQuantity = localStorage.getItem('decimalQuantity') == 'true' ? true : false;
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
            var root = this;
            if (this.wareHouse != undefined) {
                this.wareHouseRander++;
            }

            if (this.wareHouseTransferItem.length > 0 && this.wareHouseTransferItem != undefined) {
                
                this.wareHouseTransferItem.forEach(x => {
                    root.wareHouseTransferProducts.push({
                        id: x.id,
                        productId: x.productId,
                        product: x.product,
                        highQty: x.highQty,
                        quantity: x.quantity,
                        currentQuantity: x.product.inventory.currentQuantity,
                        averagePrice :  x.product.inventory.averagePrice,
                        amount:x.amount,
                        transferAmount: x.transferAmount,
                        transferQuantity: x.transferQuantity,
                        receivedQuantity : x.receivedQuantity,
                        lineTotal:x.lineTotal,
                        totalPiece:x.totalPiece,
                    });
                    

                })
                
                root.summary.totalAmount = root.totalAmount;

                this.wareHouseTransferProducts.forEach(x => {
                    root.products.push(x.product);


                })
            }

        },
    };
</script>
