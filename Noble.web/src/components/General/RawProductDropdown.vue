<template>
    <div v-bind:class="dropdownpo">
        <multiselect v-model="selectedValue"
                     @update:modelValue="$emit('update:modelValue', selectedValue.id, products.find(x => x.id == selectedValue.id))"
                     :options="options"
                     :multiple="false"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     :preselect-first="true"
                     :placeholder="$t('RawProductDropdown.PleaseSelectProduct')"                
                     @search-change="asyncFind">
                     <template v-slot:noResult>
            <p  class="text-danger">  {{ $t('RawProductDropdown.NoProductFound') }}</p>
        </template>
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from "vue-multiselect";
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: "ProductDropdown",
        props: ["modelValue", "wareHouseId", "dropdownpo", "status", 'productList', 'modelValues'],

        components: {
            Multiselect,
        },
        data: function () {
            return {
                options: [],
                selectedValue: [],
                products: [],
                product: {}
            };
        },
        methods: {
            asyncFind: function (search) {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                search = search == undefined ? '' : search;
                var url = this.wareHouseId != undefined ? "/Product/GetProductInformation?searchTerm=" + search + '&wareHouseId=' + this.wareHouseId + "&isRaw=true" + "&isDropdown=true" : "/Product/GetProductInformation?searchTerm=" + search + '&status=' + root.status + "&isRaw=true" + "&isDropdown=true";


                this.$https
                    .get(url, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.options = [];
                            root.products = response.data.results.products;

                            if (root.productList != undefined && root.productList.length > 0) {
                                root.productList.forEach(function (prod) {
                                    if (root.modelValue == prod.id && root.modelValue != undefined) {
                                        root.selectedValue.push({
                                            id: prod.id,
                                            name: prod.code + ' ' + prod.englishName,
                                        });
                                        root.product = prod;
                                    }

                                });
                            }



                            if (root.products != undefined) {
                                root.products.forEach(function (prod) {
                                    if (root.modelValue == prod.id && root.modelValue != undefined) {
                                        root.selectedValue.push({
                                            id: prod.id,
                                            name: prod.englishName + "-" + prod.code,
                                        });
                                        root.product = prod;
                                    }

                                    root.options.push({
                                        id: prod.id,
                                        name: prod.englishName + "-" + prod.code,
                                    });
                                });
                            }

                        }
                    });
            },
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Product/GetProductInformation?isActive=true' + "&isRaw=true" + "&isDropdown=true", { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        response.data.results.products.forEach(function (prod) {
                            root.selectedValue.push({
                                id: prod.id,
                                name: prod.englishName + "-" + prod.code,
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

            if (this.modelValues == undefined || this.modelValues == "") {
                this.asyncFind(this.modelValue);
            }
            else {
                this.getData(this.modelValues);

            }
        },
    };
</script>
