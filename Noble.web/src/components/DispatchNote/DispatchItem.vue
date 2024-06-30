<template>
    <div class="col-lg-12">
        <div class=" table-responsive">

            <table class="table add_table_list_bg">
                <thead class="thead-light table-hover">
                    <tr>
                        <th style="width: 10%;">
                            #
                        </th>
                        <th style="width: 40%;">
                            {{ $t('DispatchItem.Product') }}
                        </th>
                        <th style="width: 20%;" class="text-center">
                            {{ $t('DispatchItem.Qty') }}
                        </th>
                        <th style="width: 20%;">

                        </th>
                        <th style="width: 10%;">

                        </th>
                    </tr>
                </thead>
                <tbody id="purchase-item">

                        <tr v-for="(prod, index) in purchaseProducts" :key="prod.productId + index">
                            <td> {{ index + 1 }}</td>
                            <td class="">
                                <span v-if="($i18n.locale == 'en' || isLeftToRight())">
                                    {{ products.find(x => x.id == prod.productId).displayName }}
                                </span>
                                <span v-else>
                                    {{ products.find(x => x.id == prod.productId).displayName }}
                                </span>
                            </td>
                            <td>
                                <input type="number" v-model="prod.quantity" @focus="$event.target.select()"
                                    class="form-control input-border text-center tableHoverOn"
                                    @keyup="updateLineTotal($event.target.value, 'quantity', prod)" />
                            </td>
                            <td class="text-center">
                                <span class="badge badge-danger" v-if="prod.notify">Remaining Qty:
                                    {{ prod.remaining }}</span>
                            </td>
                            <td class="text-end">
                                <a href="javascript:void(0);" @click="removeProduct(prod.rowId)"><i
                                        class="las la-trash-alt text-secondary font-16"></i></a>
                            </td>
                        </tr>
                </tbody>
            </table>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="mt-4">
                    <product-dropdown :raw="false" @update:modelValues="addProduct" width="100%" />
                </div>
            </div>


            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 ">
                <div class=" table-responsive mt-3" v-bind:key="rendered + 'g'">
                    <table class="table " style="background-color:#EAF1FE;">
                        <tbody>
                            <tr>
                                <td colspan="2" style="width:65%;">
                                    <span class="fw-bold">  {{ $t('DispatchItem.NoItem') }} </span>
                                </td>
                                <td class="text-end" style="width:35%;">{{ summary.item }}</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width:65%;">
                                    <span class="fw-bold"> {{ $t('DispatchItem.TotalQty') }} </span>
                                </td>
                                <td class="text-end" style="width:35%;"> {{ summary.qty }}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
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
            vats: [],
            summary: {
                item: 0,
                qty: 0
            },
            currency: '',
            searchTerm: '',
            productList: []
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

            if (localStorage.getItem("BarcodeScan") != 'Dispatch')
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


            this.$emit("update:modelValue", this.purchaseProducts);
        },

        updateLineTotal: function (e, prop, product) {


            if (prop == "quantity") {
                if (e <= 0 || e == '') {
                    e = '';
                }
                product.quantity = Math.round(e);
            }
            if (product.saleOrderId != '00000000-0000-0000-0000-000000000000' && product.quantity > product.remaining) {
                product.notify = true;
            }
            else {
                product.notify = false;
            }

            this.purchaseProducts['product'] = product

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
                quantity: 1,
                taxRateId: prod.taxRateId,
                remaining: 0,
                notify: false,
                saleOrderId: '00000000-0000-0000-0000-000000000000'
            });

            this.updateLineTotal(prod.unitPrice, "unitPrice", prod);
            //var product = this.purchaseProducts.find((x) => {
            //    return x.productId == productId;
            //});

            /*this.getVatValue(product.taxRateId, product);*/

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
        //getVatValue: function (id, prod) {

        //    var vat = this.vats.find((value) => value.id == id);
        //    prod.taxRateId = id;
        //    prod.rate = vat.rate;
        //    this.updateLineTotal(prod.unitPrice, "unitPrice", prod);
        //    return vat.rate;
        //},
        //getVatValueForSummary: function (id, prod) {

        //    var vat = this.vats.find((value) => value.id == id);
        //    prod.taxRateId = id;
        //    prod.rate = vat.rate;
        //    return vat.rate;
        //},
        removeProduct: function (id) {

            this.purchaseProducts = this.purchaseProducts.filter((prod) => {
                return prod.rowId != id;
            });

            this.calcuateSummary();
        },

        getData: function () {
            var root = this;

            if (root.purchaseItems != undefined && root.purchaseItems.length > 0) {
                //Dispatch Note on Sale Order
                root.purchaseItems.forEach(function (item) {
                    root.purchaseProducts.push({
                        rowId: item.id,
                        id: item.id,
                        product: item.product,
                        productId: item.productId,
                        quantity: item.quantity,
                        remaining: item.quantityOut,
                        saleOrderId: item.saleOrderId,
                    });
                });

                for (var s = 0; s < root.purchaseProducts.length; s++) {
                    root.products.push(root.purchaseProducts[s].product);
                    root.updateLineTotal(root.purchaseProducts[s].quantity, "quantity", root.purchaseProducts[s]);
                }
                root.calcuateSummary()
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

        //End
        localStorage.setItem("BarcodeScan", 'Dispatch')
        this.getData();
    },
    mounted: function () {
        this.GetProductList();
        this.currency = localStorage.getItem('currency');
    },
    //destroyed() {
    //    // Remove listener when component is destroyed
    //    this.$barcodeScanner.destroy()
    //},
};
</script>
