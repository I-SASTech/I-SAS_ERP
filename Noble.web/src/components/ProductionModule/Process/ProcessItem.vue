<template>
    <div>

        <rawProductDropdown @update:modelValue="addProduct"
                            width="100%" />
        <div class=" table-responsive mt-3">

            <table class="table mb-0" style="table-layout:fixed;">
                <thead class="thead-light">
                    <tr>
                        <th width="10%">
                            #
                        </th>
                        <th width="80%">
                            {{ $t('AddProcess.RawProduct') }}
                        </th>


                        <th width="10%"></th>
                    </tr>
                </thead>
                <tbody id="purchase-item">
                    

                        <tr v-for="(prod , index) in purchaseProducts.slice().reverse()" :key="prod.productId + index" style="background:#EAF1FE;">
                            <td>
                                {{index+1}}
                            </td>
                            <td>
                                <span v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                    {{products.find(x => x.id == prod.productId).englishName}}
                                </span>
                                <span v-else>
                                    {{products.find(x => x.id == prod.productId).arabicName}}
                                </span>
                            </td>

                            <td class="pt-0">
                                <a href="javascript:void(0);"  @click="removeProduct(prod.rowId)"><i class="las la-trash-alt text-secondary font-16"></i></a>
                                
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

                this.$emit("update:modelValue", this.purchaseProducts);
            },

            updateLineTotal: function (product) {

                





                this.purchaseProducts['product'] = product;


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
                });





                this.product.id = "";
                this.rendered++;
                this.updateLineTotal(this.purchaseProducts);
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

            },

            getData: function () {

                var root = this;
                
                if (root.$route.query.data != undefined) {
                    //Recipt No Edit
                    if (root.$route.query.data.processItems != undefined) {
                        root.$route.query.data.processItems.forEach(function (item) {
                            root.purchaseProducts.push({
                                rowId: item.id,
                                id: item.id,
                                product: item.product,
                                productId: item.productId,
                            });
                        });

                        for (var k = 0; k < root.purchaseProducts.length; k++) {
                            root.products.push(root.purchaseProducts[k].product);

                        }
                    }

                }

            },
        },
        created: function () {
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


