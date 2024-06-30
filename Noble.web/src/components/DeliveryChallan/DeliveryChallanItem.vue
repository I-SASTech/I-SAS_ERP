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
                        <th class="text-center" style="width: 100px;" v-if="IsService">
                            {{ $t('SaleItem.Unit') }}
                        </th>
                        <th class="text-center" style="width: 100px;" v-if="!invoiceWoInventory">
                            {{ $t('SaleItem.CurrentQuantity') }}
                        </th>
                        <th style="width: 110px;" class="text-center">
                            {{ $t('QuotationItem.Qty') }}
                        </th>
                        <th style="width: 110px;" class="text-center" v-if="isReservedChallan == true">
                            Active
                        </th>
                        <th style="width: 40px" v-else></th>
                    </tr>
                </thead>
                <tbody id="purchase-item">

                    <tr v-for="(prod, index) in purchaseProducts" :key="prod.productId + index"
                        v-bind:class="{ 'alert-danger': prod.outOfStock }">
                        <td class="border-top-0">
                            {{ index + 1 }}
                        </td>
                        <td class="border-top-0">
                            <textarea data-gramm="false" rows="2" v-model="prod.description" class="form-control"
                                @update:modelValue="updateLineTotal(prod.description, 'description', prod)" />
                        </td>
                        <td v-if="IsService">
                            <input type="text" v-model="prod.unitName" @focus="$event.target.select()"
                                class="form-control text-center" />
                        </td>

                        <td class="text-center" v-if="!invoiceWoInventory">
                            {{ prod.currentQuantity }}
                        </td>
                        <td class="border-top-0 text-center">
                            <input type="number" v-model="prod.quantity" @focus="$event.target.select()"
                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'"
                                class="form-control input-border text-center tableHoverOn"
                                @keyup="updateLineTotal($event.target.value, 'quantity', prod)" />

                        </td>

                        <td class="border-top-0 text-center" v-if="isReservedChallan == true">
                            <input type="checkbox" id="prod.id"
                                @keyup="updateLineTotal($event.target.value, 'active', prod)" v-model="prod.isActive">


                        </td>

                        <td class="border-top-0 pt-0 text-center" v-else >
                            <button @click="removeProduct(prod.rowId)" title="Remove Item" class="btn">
                                <i class="las la-trash-alt text-secondary font-16"></i>
                            </button>
                        </td>
                    </tr>
                    <tr v-if="IsService">
                        <td class="text-center">
                        </td>

                        <td>
                            <textarea data-gramm="false" rows="2" v-model="newItem.description" class="form-control" />
                        </td>
                        <td>
                            <input type="text" v-model="newItem.unitName" @focus="$event.target.select()"
                                class="form-control text-center" />
                        </td>


                        <td class="text-center" v-if="!invoiceWoInventory">
                        </td>
                        <td class="text-center">
                            <decimal-to-fixed v-model="newItem.quantity" />
                        </td>

                        <td class="text-end">
                            <button @click="newItemProduct()" title="Add Item" v-bind:disabled="newItem.description == ''"
                                class="btn btn-primary btn-sm btn-round btn-icon float-right">
                                <i class="fa fa-check"></i>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>

        <div v-if="isValid('CanAddItem') && isValid('CanViewItem')">
            <label>Add Items</label>
            <product-dropdown v-bind:key="rendered" @update:modelValues="addProduct" width="100%" />
        </div>


    </div>
</template>


<script>
import clickMixin from '@/Mixins/clickMixin'

export default {
    name: "DeliveryChallanItems",
    props: ['purchase', 'deliveryChallanItems', 'taxMethod', 'taxRateId', 'isTemplate', 'isReservedChallan', 'isService', 'isView', 'isDeliveryChallan'],

    mixins: [clickMixin],

    data: function () {
        return {
            invoiceWoInventory: false,
            rendered: 0,
            product: {
                id: "",
            },
            decimalQuantity: false,
            IsService: false,
            products: [],
            purchaseProducts: [],
            loading: false,
            vats: [],
            isMultiUnit: '',
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
            currency: '',
            searchTerm: '',
            saleDefaultVat: '',
            productList: [],
            options: [],
        };
    },
    validations() { },
    filters: {

    },
    methods: {
        newItemProduct: function () {
            var rowId = this.createUUID();
            this.purchaseProducts.push({
                rowId: rowId,
                productId: null,
                serviceProductId: rowId,
                unitPrice: this.newItem.unitPrice,
                quantity: this.newItem.quantity,
                description: this.newItem.description,
                unitName: this.newItem.unitName,
                serviceItem: true,
                guarantee: false,
                lineTotal: 0,
                buy: 0,
                get: 0,
                currentQuantity: 0,
                quantityLimit: 0,
                offerQuantity: 0,
                unitPerPack: 0,
                levelOneUnit: '',
                isFree: false,
            });

            this.newItem.description = '';
            this.newItem.unitName = '';
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
        GetProductList: function () {

            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }

            this.isRaw = this.raw == undefined ? false : this.raw;

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
            if (prop == "active") {
                if (e < 0) {
                    e = 0;
                }
                product.isActive = e;
            }
            if (prop == "description") {
                if (e < 0) {
                    e = 0;
                }
                product.description = e;
            }

            if (prop == "quantity") {
                if (e <= 0 || e == '') {
                    e = '';
                }
                if (String(e).split('.').length > 1 && String(e).split('.')[1].length > 2)
                    e = parseFloat(String(e).slice(0, -1))
                product.quantity = this.decimalQuantity ? e : Math.round(e);
            }
            if (this.IsService) {
                product['outOfStock'] = false

            }

            else if (this.isReservedChallan == true || this.isView == true) {
                product['outOfStock'] = false
            }
            else {
                if (product.soQty < product.quantity) {
                    product['outOfStock'] = true
                }
                else {
                    product['outOfStock'] = false

                }
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

            this.purchaseProducts['product'] = product

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
                    unitPrice: 0,
                    SoQty: prod.SoQty,
                    quantity: 0,
                    currentQuantity: 0,
                    highQty: 0,
                    discount: 0,
                    fixDiscount: 0,
                    unitName: '',
                    description: prod.name,
                    taxRateId: '',
                    rate: 0,
                    taxMethod: '',
                    lineTotal: 0,
                    unitPerPack: 0,
                    isActive: true,
                    highUnitPrice: 0,
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
                        if (root.$route.query.data.deliveryChallanItems != undefined) {
                            //Sale Order Edit

                            root.$route.query.data.deliveryChallanItems.forEach(function (item) {
                                root.purchaseProducts.push({
                                    rowId: item.id,
                                    serviceProductId: item.serviceProductId == null ? item.id : item.serviceProductId,
                                    id: item.id,
                                    description: item.description,
                                    unitName: item.unitName==null || item.unitName==undefined?'':item.unitName ,
                                    discount: item.discount,
                                    fixDiscount: item.fixDiscount,
                                    product: item.product,
                                    productId: item.productId==null || item.productId==undefined?'':item.productId ,
                                    purchaseId: item.purchaseId==undefined?'':item.purchaseId,
                                    quantity: item.quantity,
                                    highQty: item.highQty,
                                    taxMethod: item.taxMethod,
                                    taxRateId: item.taxRateId,
                                    unitPrice: item.unitPrice,
                                    unitPerPack: item.unitPerPack==null || item.unitPerPack==undefined?0:item.unitPerPack ,
                                    soQty: item.soQty,
                                    isActive: true,
                                });
                            });

                            for (var l = 0; l < root.purchaseProducts.length; l++) {
                                if (root.purchaseProducts[l].product != undefined && root.purchaseProducts[l].product != null) {
                                    root.products.push(root.purchaseProducts[l].product);

                                }

                                root.updateLineTotal(root.purchaseProducts[l].quantity, "quantity", root.purchaseProducts[l]);
                                root.updateLineTotal(root.purchaseProducts[l].unitPrice, "unitPrice", root.purchaseProducts[l]);
                            }

                        }
                    }
                    else {
                        if (root.deliveryChallanItems != undefined) {
                            root.deliveryChallanItems.forEach(function (item) {
                                root.purchaseProducts.push({
                                    rowId: item.id,
                                    serviceProductId: item.serviceProductId == null ? item.id : item.serviceProductId,
                                    id: item.id,
                                    description: item.description,
                                    unitName: item.unitName==null || item.unitName==undefined?'':item.unitName ,
                                    discount: item.discount,
                                    fixDiscount: item.fixDiscount,
                                    productId: item.productId==null || item.productId==undefined?'':item.productId ,
                                    purchaseId: item.purchaseId==undefined?'':item.purchaseId,
                                    quantity: item.quantity,
                                    highQty: item.highQty,
                                    taxMethod: item.taxMethod,
                                    taxRateId: item.taxRateId,
                                    unitPrice: item.unitPrice,
                                    unitPerPack: item.unitPerPack==null || item.unitPerPack==undefined?0:item.unitPerPack ,
                                    soQty: item.soQty,
                                    isActive: true,

                                });
                            });

                            for (var k = 0; k < root.purchaseProducts.length; k++) {
                                if (root.purchaseProducts[k].product != undefined && root.purchaseProducts[k].product != null) {
                                    root.products.push(root.purchaseProducts[k].product);

                                }

                                root.updateLineTotal(root.purchaseProducts[k].quantity, "quantity", root.purchaseProducts[k]);
                                root.updateLineTotal(root.purchaseProducts[k].unitPrice, "unitPrice", root.purchaseProducts[k]);
                            }
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
        this.IsService = localStorage.getItem('IsSimpleInvoice') == 'true' ? false : true;
        this.invoiceWoInventory = localStorage.getItem('InvoiceWoInventory') == 'true' ? true : false;
    },
};
</script>
