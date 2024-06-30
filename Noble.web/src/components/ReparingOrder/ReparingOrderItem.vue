<template>
    <div class="col-lg-6"> 
        <product-dropdown @update:modelValues="addProduct" width="100%" />

        <div class=" table-responsive mt-3">
            <table class="table add_table_list_bg" style="table-layout:fixed;">
                <thead style="background-color: #F4F6FC;">
                    <tr>
                        <th style="width:5%;">
                            #
                        </th>
                        <th style="width:60%;">
                            Part Name
                        </th>
                        <th style="width: 25%;" class="text-center">
                            Quantity
                        </th>
                        <th style="width: 10%"></th>
                    </tr>
                </thead>
                <tbody id="purchase-item">
                        <tr v-for="(prod , index) in purchaseProducts" :key="prod.productId + index">
                            <td>
                                {{index+1}}
                            </td>
                            <td>
                                {{$i18n.locale == 'en'? products.find(x => x.id == prod.productId).englishName!=''? products.find(x => x.id == prod.productId).englishName : products.find(x => x.id == prod.productId).arabicName :    products.find(x => x.id == prod.productId).arabicName!=''? products.find(x => x.id == prod.productId).arabicName : products.find(x => x.id == prod.productId).englishName}}
                            </td>
                            <td class="text-center">
                                <input type="number" v-model="prod.quantity"
                                       @focus="$event.target.select()"
                                       class="form-control text-center"
                                       @keyup="updateLineTotal($event.target.value, 'quantity', prod)" />

                            </td>
                            <td class="text-end">
                                <a href="javascript:void(0);" @click="removeProduct(prod.rowId)"><i class="las la-trash-alt text-secondary font-16"></i></a>

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
                            {{ $t('AddSampleRequest.NoItem') }}
                        </th>
                        <th class="text-center" style="width:100px;">
                            {{ $t('AddSampleRequest.TotalQty') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('AddSampleRequest.TOTALWITHOUTWASTE') }}
                        </th>
                        <th style="width:100px;">
                            {{ $t('AddSampleRequest.TOTALWITHWASTE') }}
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
                productList: [],
                searchTerm: '',
                isMultiUnit: ''
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
                    .get("/Product/GetProductBarcode?isRaw=" + false, {
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
                    highQuantity: 0,
                    unitPerPack: prod.unitPerPack == null ? 0 : prod.unitPerPack,
                    basicUnit: prod.unit == null ? '' : prod.unit.name,
                    quantity: 1,
                    waste: 0,
                    lineTotal: 0,
                    withoutWastageTotal: 0,
                    wareHouseId: '',
                });

                var prodDetail = this.purchaseProducts.find((x) => x.productId == productId);

                this.updateLineTotal(prodDetail.quantity, 'quantity', prodDetail);


                this.product.id = "";
                //    this.rendered++;
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
                    if (root.$route.query.data.reparingItems != undefined) {
                        root.$route.query.data.reparingItems.forEach(function (item) {
                            root.purchaseProducts.push({
                                rowId: item.id,
                                id: item.id,
                                product: item.product,
                                productId: item.productId,
                                quantity: item.quantity,
                                unitPerPack: item.unitPerPack,
                            });
                        });

                        for (var k = 0; k < root.purchaseProducts.length; k++) {
                            root.products.push(root.purchaseProducts[k].product);

                            root.updateLineTotal(root.purchaseProducts[k].quantity, "quantity", root.purchaseProducts[k]);
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

