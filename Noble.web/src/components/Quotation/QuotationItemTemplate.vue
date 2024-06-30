<template>
    <div>

        <div class=" table-responsive mt-3">
            <table class="table add_table_list_bg">
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

                        <th style="width: 110px;" class="text-center">
                            {{ $t('QuotationItem.Qty') }}
                        </th>







                        <th style="width: 40px"></th>
                    </tr>
                </thead>
                <tbody id="purchase-item">

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


                            <td class="border-top-0 text-center">
                                <input type="number" v-model="prod.quantity"
                                       @focus="$event.target.select()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"
                                       class="form-control input-border text-center tableHoverOn"
                                       @keyup="updateLineTotal($event.target.value, 'quantity', prod)" />

                            </td>




                            <td class="border-top-0 pt-0">
                                <button @click="removeProduct(prod.rowId)"
                                        title="Remove Item"
                                        class="btn">
                                    <i class="las la-trash-alt text-secondary font-16"></i>
                                </button>
                            </td>
                        </tr>
                </tbody>
            </table>

        </div>

        <div>
            <label>Add Items</label>
            <product-dropdown v-bind:key="rendered"
                              @update:modelValues="addProduct"
                              width="100%" />
        </div>
    </div>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        name: "PurchaseItem",
        props: ['purchase', 'purchaseItems', 'taxMethod', 'taxRateId', 'isTemplate'],

        mixins: [clickMixin],
      
        data: function () {
            return {
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

            updateLineTotal: function (e, prop, product) {



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
              
                if (prop == "highQty") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = 0;
                    }
                    product.highQty = e;
                }
                if (product.highUnitPrice) {

                    product.totalPiece = (parseFloat(product.highQty == undefined ? 0 : product.highQty) + (parseFloat(product.quantity == '' ? 0 : product.quantity) / parseFloat(product.unitPerPack == null ? 0 : product.unitPerPack)));
                }
                else {
                    product.totalPiece = (parseFloat(product.highQty == undefined ? 0 : product.highQty) * parseFloat(product.unitPerPack == null ? 0 : product.unitPerPack)) + parseFloat(product.quantity == '' ? 0 : product.quantity);
                }
                //product.totalPiece = (parseFloat(product.highQty == undefined ? 0 : product.highQty) * parseFloat(product.unitPerPack == null ? 0 : product.unitPerPack)) + parseFloat(product.quantity == '' ? 0 : product.quantity);


              
              
                this.purchaseProducts['product'] = product;


                this.$emit("update:modelValue", this.purchaseProducts);

            },

            addProduct: function (productId, newProduct) {

                var prd = this.purchaseProducts.find(x => x.productId == productId);
                if (prd != undefined) {
                    prd.quantity++;
                    this.updateLineTotal(prd.quantity, "quantity", prd);
                }
                else {
                    this.products.push(newProduct);
                    var prod = this.products.find((x) => x.id == productId);

                 


                    this.purchaseProducts.push({
                        rowId: this.createUUID(),
                        productId: prod.id,
                        unitPrice: prod.salePrice,
                        quantity: 1,
                        highQty: 0,
                        discount: 0,
                        fixDiscount: 0,
                        taxRateId: '',
                        rate: 0,
                        taxMethod: '',
                        lineTotal: 0,
                        unitPerPack: newProduct.unitPerPack,
                        levelOneUnit: prod.levelOneUnit,
                        basicUnit: prod.basicUnit,

                        highUnitPrice: newProduct.highUnitPrice,
                    });

                  

                    this.product.id = "";
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
           

          
          
            removeProduct: function (id) {
                
                this.purchaseProducts = this.purchaseProducts.filter((prod) => {
                    return prod.rowId != id;
                });
                this.$emit("update:modelValue", this.purchaseProducts);

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
                            if (root.$route.query.data.quotationTemplateItems != undefined) {
                                //Sale Order Edit
                                
                                root.$route.query.data.quotationTemplateItems.forEach(function (item) {
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
                                    });
                                });

                                for (var l = 0; l < root.purchaseProducts.length; l++) {
                                    root.products.push(root.purchaseProducts[l].product);

                                    root.updateLineTotal(root.purchaseProducts[l].quantity, "quantity", root.purchaseProducts[l]);
                                    root.updateLineTotal(root.purchaseProducts[l].unitPrice, "unitPrice", root.purchaseProducts[l]);
                                }
                                root.calcuateSummary()
                            }

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
