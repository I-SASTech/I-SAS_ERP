<template>

    <div style="width:100%">
        <multiselect v-model="selectedValue"
                     @update:modelValue="UpdateData"
                     :options="options"
                     :multiple="false"
                     v-bind:disabled="disable"
                     track-by="name"
                     :clear-on-select="false"
                     :preserve-search="false"
                     :show-labels="false"
                     label="name"
                     :placeholder="$t('ProductDropdown.PleaseSelectProduct')">
            <template v-slot:noResult>
                <a class="btn btn-primary " v-on:click="addNewProduct" v-if="(invoiceWoInventory == 'true' || isservice)&&(isValid('CanAddServiceQuickItem') || isValid('CanAddQuickItem'))"> {{ $t('ProductDropdown.AddProduct') }}</a>
                <a v-else> </a>
            </template>
        </multiselect>

        <quickProductItem :newproduct="newProduct"
                          :show="showProduct"
                          :addInvoice="addInvoice"
                          v-if="showProduct"
                          @close="showProduct = false"
                          @closeOnSave="onAddNewItem"
                          :type="type" />

        <select-batch :selectedproduct="selectedProduct"
                      v-if="showBatches"
                      :show="showBatches"
                      @close="showBatches = false"
                      @update:modelValue="UpdateProduct">

        </select-batch>
    </div>
</template>
<script>
    import Multiselect from "vue-multiselect";
    import { requiredIf, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: "Product-Dropdown",
        props: ["modelValue", "wareHouseId", "dropdownpo", "fromBundles", "status", 'productList', "modelValues", "width", 'raw', 'emptyselect', 'serachValue', 'isservice', 'fromSale', 'fromSOrder', 'fromStockOut', "disable", "IsReport", "isForRaw"],

        components: {
            Multiselect,
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                options: [],
                selectedValue: [],
                product: {},
                isRaw: false,
                addInvoice: true,
                render: 0,
                arabic: '',
                english: '',
                loading: false,
                showBatches: false,
                invoiceWoInventory: '',
                newProduct: {
                    id: '00000000-0000-0000-0000-000000000000',
                    englishName: '',
                    arabicName: '',
                    salePrice: 0,
                    isActive: true,
                    code: '',
                    taxMethod: '',
                    taxRateId: '',
                    serviceItem: false,

                },
                showProduct: false,
                isFifo: false,
                openBatch: 1,
                selectedProduct: '',
                type: '',
                colorVariants: false,
            };
        },
        validations() {
            return {
                product:
                {
                    englishName: {
                        maxLength: maxLength(50)
                    },
                    arabicName: {
                        required: requiredIf(function () {
                            return !this.product.englishName;
                        }),
                        maxLength: maxLength(50)
                    }
                }
            }
        },

        methods: {
            addNewProduct: function () {
                this.newProduct = {
                    id: '00000000-0000-0000-0000-000000000000',
                    englishName: '',
                    arabicName: '',
                    salePrice: 0,
                    isActive: true,
                    code: '',
                    taxMethod: '',
                    unitId: '',
                    levelOneUnit: '',
                    taxRateId: '',
                    barcode: '',
                    categoryId: '',
                    serviceItem: false,
                }
                this.showProduct = !this.showProduct;
                this.type = "Add";
            },

            CallSameFunction: function (productId) {
                this.productListValueCompare(productId);
            },

            productListValueCompare: function (productId) {
                return this.options.find(x => x.id == productId)
            },

            productListCheck: function () {

                if (this.options.length == 0) {
                    return false;
                }
                else {
                    return true;

                }
            },

            UpdateData: function () {
                
                var selectedvalue = this.selectedValue.id;
                if (!this.emptyselect) {
                    this.selectedValue = [];
                }
                if (this.isFifo && (this.fromSale || this.fromSOrder || this.fromStockOut)) {

                    this.selectedProduct = this.options.find(x => x.id == selectedvalue)
                    this.showBatches = true;
                }
                if (this.IsReport) {

                    var prod = this.options.find(x => x.id == selectedvalue);
                    this.selectedValue.push({
                        id: prod.id,
                        name: (prod.displayName == null || prod.displayName == '') ? prod.code + " " + prod.englishName : prod.displayName,
                    });
                    this.$emit("update:modelValues", selectedvalue, this.options.find(x => x.id == selectedvalue));
                }
                if (this.fromBundles) {
                    this.$emit("update:modelValue", selectedvalue, this.options.find(x => x.id == selectedvalue));
                }
                else {
                    this.$emit("update:modelValues", selectedvalue, this.options.find(x => x.id == selectedvalue));
                }
            },

            UpdateProduct: function (product, batch) {

                this.showBatches = !this.showBatches
                if (this.fromSOrder) {
                    this.$emit("update:modelValues", product.id, product, null, null, batch);
                }
                else if (this.fromStockOut) {
                    this.$emit("update:modelValues", product.id, product, batch);
                }
                else {
                    this.$emit("update:modelValues", product.id, product, null, null, null, null, null, batch);
                }
            },

            onAddNewItem: function () {

                this.showProduct = false;
                this.asyncFind();
            },

            asyncFind: function (search) {
                
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                this.isRaw = this.raw == undefined ? false : this.raw;
                search = search == undefined ? '' : search;
                var service = this.isservice == true ? true : false;

                var isForRaw = false;
                if(this.isForRaw)
                {
                    isForRaw = true;
                }


                var url = "/Product/GetProductListForDropdown?searchTerm=" + search + "&isDropdown=true" + '&isRaw=' + root.isRaw + '&isService=' + service + '&branchId=' + localStorage.getItem('BranchId') + '&isForRaw=' + isForRaw;
                    
                if (search != "" || search != undefined) {
                    this.$https.get(url, { headers: { Authorization: `Bearer ${token}` }, }).then(function (response) {
                            if (response.data != null) {
                                root.options = [];
                                if (root.productList != undefined && root.productList.length > 0) {
                                    root.productList.forEach(function (prod) {
                                        if (root.modelValue == prod.id && root.modelValue != undefined) {
                                            root.selectedValue.push({
                                                id: prod.id,
                                                name: (prod.displayName == null || prod.displayName == '') ? prod.code + " " + prod.englishName : prod.displayName,
                                            });
                                            root.product = prod;
                                        }

                                    });
                                }

                                if (response.data.results.products != undefined) {
                                    response.data.results.products.forEach(function (prod) {
                                        if (root.modelValue != undefined && root.modelValue == prod.id) {
                                            root.selectedValue.push({
                                                id: prod.id,
                                                name: (prod.displayName == null || prod.displayName == '') ? prod.code + " " + prod.englishName : prod.displayName,
                                            });
                                            root.product = prod;
                                        }

                                        root.options.push({
                                            id: prod.id,
                                            taxRateId: prod.taxRateId,
                                            taxMethod: prod.taxMethod,
                                            displayName: prod.displayName,
                                            salePrice: prod.salePrice,
                                            name: (prod.displayName == null || prod.displayName == '') ? prod.code + " " + prod.englishName : prod.displayName,

                                        });
                                    });
                                }
                            }
                        });
                }
            },
            getData: function () {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.isRaw = this.raw == undefined ? false : this.raw;
                this.$https.get('/Product/GetProductInformation?isActive=true' + "&isDropdown=true" + "&isFifo=" + root.isFifo + "&openBatch=" + root.openBatch, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        response.data.results.productData.forEach(function (prod) {
                            root.selectedValue.push({
                                id: prod.id,
                                name: (prod.displayName == null || prod.displayName == '') ? prod.code + " " + prod.englishName : prod.displayName,

                            });
                        })
                    }
                }).then(function () {
                    root.modelValue = root.options.find(function (x) {
                        return x.id == root.modelValues;
                    })
                });
            },

        },
        computed: {

        },
        mounted: function () {

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            
            this.isFifo = localStorage.getItem('fIFO') == 'true' ? true : false
            
            
            this.asyncFind(this.serachValue);
        },
    };
</script>
