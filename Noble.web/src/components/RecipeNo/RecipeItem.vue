<template>
    <div >

        <label><b>{{ $t('RecipeItem.RawProduct') }}</b></label>
        <rawProductDropdown @update:modelValue="addProduct"
                            width="100%" />
        <div class=" table-responsive mt-3">

            <table class="table mb-0" style="table-layout:fixed;" >
                <thead class="thead-light table-hover">
                    <tr>
                        <th style="width:5%;">
                            #
                        </th>
                        <th style="width:30%;">
                            {{ $t('RecipeItem.RawProduct') }}
                        </th>
                        <th v-if="fromBom" style="width: 15%;"  class="text-center">
                            {{ $t('Unit PerPack') }}
                        </th>
                        <th v-if="fromBom" style="width: 15%;"  class="text-center">
                            {{ $t('Unit Name') }}
                        </th>
                        <th v-if="fromBom" style="width: 15%;"  class="text-center">
                            {{ $t('Current Quantity') }}
                        </th>
                        <th style="width: 15%;" class="text-center" v-if="isMultiUnit=='true'">
                            {{ $t('RecipeItem.HighQty') }}
                        </th>
                        <th style="width: 15%;" class="text-center">
                            {{ $t('RecipeItem.Qty') }}
                        </th>
                        <th style="width: 20%;" class="text-center" hidden>
                            {{ $t('RecipeItem.TOTALWITHOUTWASTE') }}
                        </th>
                        <th v-if="!fromBom" style="width: 15%;" class="text-center">
                            {{ $t('RecipeItem.WASTE') }}%
                        </th>
                        
                        <th v-if="fromBom" style="width: 15%;"  class="text-center">
                            {{ $t('Price') }}
                        </th>

                        <th style="width: 20%;" class="text-right" v-if="isMultiUnit=='true'">
                            {{ $t('RecipeItem.TotalQuantity') }}
                        </th>
                        <th style="width: 5%"></th>
                    </tr>
                </thead>
                <tbody id="purchase-item">

                        <tr  v-for="(prod , index) in purchaseProducts.slice().reverse()" :key="prod.productId + index" v-bind:class=" fromBom ? (prod.productId == prod.productId && prod.quantity > prod.currentQuantity  )? 'bgColor' : '' : '' ">
                            <td class="border-top-0">
                                {{index+1}}
                            </td>
                            <td class="border-top-0">
                
                                <span v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                    {{products.find(x => x.id == prod.productId).englishName}}
                                </span>
                                <span v-else>
                                    {{products.find(x => x.id == prod.productId).arabicName}}
                                </span>
                            </td>
                            <td v-if="fromBom" style="width: 15%;"  class="text-center">
                                 {{ prod.unitPerPack }}
                            </td>
                            <td v-if="fromBom" style="width: 15%;"  class="text-center">
                                 {{ prod.unitNameEn }}
                            </td>
                            <th v-if="fromBom" style="width: 15%;"  class="text-center">
                                 {{ prod.currentQuantity }}
                            </th>

                            <td class="border-top-0" v-if="isMultiUnit=='true'">
                                <input type="number" v-model="prod.highQuantity"
                                       @focus="$event.target.select()" 
                                       class="form-control input-border text-center "
                                       @keyup="updateLineTotal($event.target.value, 'highQuantity', prod)" />
                                <div class="text-center">{{prod.levelOneUnit}}</div>

                            </td>
                            <td class="border-top-0">
                                <input type="number" v-model="prod.quantity"
                                       @focus="$event.target.select()" 
                                       class="form-control input-border text-center "
                                       @keyup="updateLineTotal($event.target.value, 'quantity', prod)" />
                                <div class="text-center" v-if="isMultiUnit=='true'">{{prod.basicUnit}}</div>

                            </td>
                            <td class="border-top-0 text-center" hidden>
                                {{$formatAmount(parseFloat(prod.withoutWastageTotal).toFixed(3).slice(0,-1))  }}
                            </td>
                            
                            <td class="border-top-0" v-if="!fromBom">
                                <input type="number" v-model="prod.waste"
                                       @focus="$event.target.select()" 
                                       class="form-control input-border text-center "
                                       @keyup="updateLineTotal($event.target.value, 'waste', prod)" />
                            </td>
                            <td class="border-top-0" v-if="fromBom">
                                <input type="number" v-model="prod.salePrice"
                                       @focus="$event.target.select()" 
                                       class="form-control input-border text-center "
                                       @keyup="updateLineTotal($event.target.value, 'price', prod)" />
                            </td>
                            <td class="border-top-0 text-right" v-if="isMultiUnit=='true'">
                                {{prod.lineTotal}}
                            </td>
                            <td class="text-end">
                                <a href="javascript:void(0);" @click="removeProduct(prod.rowId)"><i
                                                    class="las la-trash-alt text-secondary font-16"></i></a>
                                
                            </td>
                        </tr>
                </tbody>
            </table>
        </div>

        <hr hidden />
        <div class=" table-responsive" v-bind:key="rendered + 'g'" v-if="purchaseProducts.length > 0" hidden>
            <table class="table " style="table-layout:fixed">
                <thead class="thead-light table-hover">
                    <tr class="text-right">
                        <th class="text-center" style="width:85px;">
                            {{ $t('RecipeItem.NoItem') }}
                        </th>
                        <th class="text-center" style="width:100px;">
                            {{ $t('RecipeItem.TotalQty') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('RecipeItem.TOTALWITHOUTWASTE') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('RecipeItem.TOTALWITHWASTE') }}
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


<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: "PurchaseItem",
        props: ['purchase', 'purchaseItems','fromBom','rawProducts','wareHouseId'],

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
                productList: [],
                searchTerm: '',
                isMultiUnit: '',
               
                productQuantity:{
                    productId:'',
                    quantity:'',
                },

                storeProductQuantity : []
            };
        },
        validations() {},
        filters: {

        },
        methods: {
            GetProductList: function () {
               
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                this.$https
                    .get("/Product/GetProductBarcode?isRaw=" + true, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.productList = response.data.results.products;

                        }
                    });


            },
            onBarcodeScanned(barcode) {

                if (localStorage.getItem("BarcodeScan") != 'Recipe')
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
                this.summary.qty = this.purchaseProducts.reduce(
                    (qty, prod) => qty + parseInt(prod.quantity == '' ? 0 : prod.quantity), 0);
                this.summary.total = this.purchaseProducts
                    .reduce((total, prod) => total + (prod.highQuantity == '' ? 0 : prod.highQuantity) * prod.unitPerPack + parseFloat(prod.quantity == '' ? 0 : prod.quantity), 0)
                    .toFixed(3).slice(0, -1);
                this.summary.totalWithWaste = this.purchaseProducts
                    .reduce((total, prod) => total + (((prod.highQuantity == '' ? 0 : prod.highQuantity) * prod.unitPerPack) + parseFloat(prod.quantity == '' ? 0 : prod.quantity)) - parseFloat(((((prod.highQuantity == '' ? 0 : prod.highQuantity) * prod.unitPerPack) + parseFloat(prod.quantity == '' ? 0 : prod.quantity)) * prod.waste) / 100), 0)
                    .toFixed(3).slice(0, -1);

                
                if(this.fromBom)
                {
                    this.$emit('change');
                }
                this.$emit("update:modelValue", this.purchaseProducts);
           
            },

            updateLineTotal: function (e, prop, product) {
               
                if (prop == "highQuantity") {
                    if (e <= 0 || e == '') {
                        e = '';
                    }
                    product.highQuantity = Math.round(e);
                }
                if (prop == "quantity") {
                    if (e <= 0 || e == '') {
                        e = '';
                    }
                    product.quantity = Math.round(e);

                }
                if (prop == "price") {
                    if (e <= 0 || e == '') {
                        e = '';
                    }
                    product.salePrice = Math.round(e);
                }

                if (prop == "waste") {
                    if (e < 0) {
                        e = 0;
                    }
                    if (e > 100) {
                        e = 100;
                    }
                    product.waste = e;
                }

                product.withoutWastageTotal = (product.highQuantity == '' ? 0 : product.highQuantity) * product.unitPerPack + parseFloat(product.quantity == '' ? 0 : product.quantity);


                if (product.waste != 0) {
                    product.lineTotal = (((product.highQuantity == '' ? 0 : product.highQuantity) * product.unitPerPack) + parseFloat(product.quantity == '' ? 0 : product.quantity)) - parseFloat(((((product.highQuantity == '' ? 0 : product.highQuantity) * product.unitPerPack) + parseFloat(product.quantity == '' ? 0 : product.quantity)) * product.waste) / 100);
                }
                else {
                    product.lineTotal = (product.highQuantity == '' ? 0 : product.highQuantity) * product.unitPerPack + parseFloat(product.quantity == '' ? 0 : product.quantity);
                }
                this.purchaseProducts['product'] = product;

                this.calcuateSummary();

                if(this.fromBom)
                {
                    this.$emit('change');
                    this.storeProductQuantity = this.$store.isProductQuantityList;
                }

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
                    unitNameEn: prod.unitNameEn,
                    quantity: 0,
                    currentQuantity: 0,
                    waste: 0,
                    lineTotal: 0,
                    withoutWastageTotal: 0,
                    wareHouseId: prod.wareHouseId,
                    salePrice:prod.purchasePrices
                });




                this.product.id = "";
                this.rendered++;

                if(this.fromBom)
                {
                    this.$emit('change');
                    this.$emit("update:modelValue", this.purchaseProducts);
                    this.GetProductInfo(productId);
                }
            },
            GetProductInfo: function (id) {
                var root = this;
                var token = localStorage.getItem('token');

                var openBatch;
                var batch = localStorage.getItem('openBatch')
                if (batch != undefined && batch != null && batch != "") {
                    openBatch = batch
                }
                else {
                    openBatch = 1
                }

                root.$https.get('/Product/GetProductDetailForInvoiceQuery?id=' + id + '&wareHouseId=' + this.wareHouseId + "&isFifo=" + false + "&openBatch=" + openBatch + "&colorVariants=" + false + '&branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        var newProduct = response.data;
                        var prod = root.purchaseProducts.find((x) => x.productId == id);

                        if (prod != undefined) {
                           console.log(newProduct.unitNameEn)
                           console.log(prod.unitNameEn)
                            prod.unitPrice = newProduct.sa == 0 ? '0' : newProduct.salePrice;
                            prod.levelOneUnit = newProduct.levelOneUnit;
                            prod.highQuantity = 0;
                            prod.unitPerPack = newProduct.unitPerPack == null ? 0 : prod.unitPerPack;
                            prod.basicUnit = newProduct.basicUnit == null ? '' : prod.unit.name;
                            prod.unitNameEn = newProduct.unitNameEn,
                            prod.currentQuantity = newProduct.inventory == null ? 0 : newProduct.inventory.currentQuantity;
                            prod.lineTotal = 0,
                            prod.withoutWastageTotal = 0,
                            prod.salePrice = newProduct.purchasePrices;

                           

                            root.updateLineTotal(prod.quantity, "quantity", prod);
                           
                        }
                    }
                });
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
                //Edit When Sample Request Select
                if (root.purchase != undefined) {
                    root.purchase.recipeNoItems.forEach(function (item) {
                        root.purchaseProducts.push({
                            rowId: item.id,
                            id: item.id,
                            product: item.product,
                            productId: item.productId,
                            wareHouseId: item.wareHouseId,
                            quantity: item.quantity,
                            unitPerPack: item.unitPerPack,
                            basicUnit: item.unit == null ? '' : item.unit.name,
                            unitNameEn: item.unitNameEn,
                            levelOneUnit: item.levelOneUnit,
                            highQuantity: item.highQuantity,
                            waste: item.waste,
                            salePrice:item.purchasePrices
                        });
                    });

                    for (var k = 0; k < root.purchaseProducts.length; k++) {

                        root.products.push(root.purchaseProducts[k].product);

                        root.updateLineTotal(root.purchaseProducts[k].quantity, "quantity", root.purchaseProducts[k]);
                        root.updateLineTotal(root.purchaseProducts[k].unitPerPack, "unitPerPack", root.purchaseProducts[k]);
                        root.updateLineTotal(root.purchaseProducts[k].highQuantity, "highQuantity", root.purchaseProducts[k]);
                        root.updateLineTotal(root.purchaseProducts[k].waste, "waste", root.purchaseProducts[k]);
                        root.updateLineTotal(root.purchaseProducts[k].salePrice, "price", root.purchaseProducts[k]);
                    }
                    root.calcuateSummary()
                }
                else if(root.$route.query.isBomItems)
                {
                    
                    if (root.$route.query.data.bomSaleOrderItem != undefined) {
                        root.purchaseProducts = [];
                        root.rawProducts.forEach(function (item)  {
                                  root.purchaseProducts.push({
                                        rowId: item.id,
                                        id: item.id,
                                        product: item.product,
                                        productId: item.productId,
                                        wareHouseId: item.wareHouseId,
                                        quantity: item.quantity,
                                        unitPerPack: item.unitPerPack,
                                        basicUnit: item.unit == null ? '' : item.unit.name,
                                        unitNameEn: item.unitName,
                                        levelOneUnit: item.levelOneUnit,
                                        highQuantity: item.highQuantity,
                                        waste: item.waste,
                                        salePrice:item.salePrice,
                                        currentQuantity: item.currentQuantity,
                                    });
                                });
                        for (var a = 0; a < root.purchaseProducts.length; a++) {
                            root.products.push(root.purchaseProducts[a].product);

                            root.updateLineTotal(root.purchaseProducts[a].quantity, "quantity", root.purchaseProducts[a]);
                            root.updateLineTotal(root.purchaseProducts[a].unitPerPack, "unitPerPack", root.purchaseProducts[a]);
                            root.updateLineTotal(root.purchaseProducts[a].basicUnit, "basicUnit", root.purchaseProducts[a]);
                            root.updateLineTotal(root.purchaseProducts[a].levelOneUnit, "levelOneUnit", root.purchaseProducts[a]);
                            root.updateLineTotal(root.purchaseProducts[a].highQuantity, "highQuantity", root.purchaseProducts[a]);
                            root.updateLineTotal(root.purchaseProducts[a].waste, "waste", root.purchaseProducts[a]);
                            root.updateLineTotal(root.purchaseProducts[a].salePrice, "price", root.purchaseProducts[a]);
                        }
                        root.calcuateSummary()
                    }
                }
                else if (root.$route.query.data != undefined) {
              
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
                                unitNameEn: item.unitNameEn,
                                levelOneUnit: item.levelOneUnit,
                                highQuantity: item.highQuantity,
                                waste: item.waste,
                                salePrice:item.purchasePrices
                            });
                        });

                        for (var i = 0; i < root.purchaseProducts.length; i++) {
                            root.products.push(root.purchaseProducts[i].product);

                            root.updateLineTotal(root.purchaseProducts[i].quantity, "quantity", root.purchaseProducts[i]);
                            root.updateLineTotal(root.purchaseProducts[i].unitPerPack, "unitPerPack", root.purchaseProducts[i]);
                            root.updateLineTotal(root.purchaseProducts[i].basicUnit, "basicUnit", root.purchaseProducts[i]);
                            root.updateLineTotal(root.purchaseProducts[i].levelOneUnit, "levelOneUnit", root.purchaseProducts[i]);
                            root.updateLineTotal(root.purchaseProducts[i].highQuantity, "highQuantity", root.purchaseProducts[i]);
                            root.updateLineTotal(root.purchaseProducts[i].waste, "waste", root.purchaseProducts[i]);
                            root.updateLineTotal(root.purchaseProducts[i].salePrice, "price", root.purchaseProducts[i]);
                        }
                        root.calcuateSummary()
                    }

                }

            },
        },
        created: function () {
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
            localStorage.setItem("BarcodeScan", 'Recipe')
            //End
            this.getData();
        },
        mounted: function () {
            this.GetProductList();
            this.isMultiUnit = localStorage.getItem('IsMultiUnit');
        },
    };
</script>
<style scoped>
.bgColor{
    background-color: #f1aeb5;
}
</style>