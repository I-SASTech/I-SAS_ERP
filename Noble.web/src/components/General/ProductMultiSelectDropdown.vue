<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" 
                     :multiple="true" track-by="name" :clear-on-select="false" 
                     :show-labels="false" label="name" :preselect-first="true" :placeholder="$t('ProductMultiSelectDropdown.PleaseSelectProduct')" 
                      @search-change="getData">
                      <template v-slot:noResult>
            <p  class="text-danger"> {{$t('ProductMultiSelectDropdown.NoProductFound')}}</p>
        </template>
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: 'productMultSelectDropdown',
        props: ['status', 'productIds', 'type','categoryId','isRequest','modelValue'],
        components: {
            Multiselect,            
        },
        data: function () {
            return {
                options: [],
                value: []
            }
        },
        methods: {
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var category = root.categoryId == undefined ? '00000000-0000-0000-0000-000000000000' : root.categoryId;
                
                if (this.isRequest) {
                    this.$https.get('/Product/GetProductByCategoryId?categoryId=' + category, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.options = [];

                            response.data.results.products.forEach(function (product) {
                                if (product.taxMethod == 'Inclusive' || product.taxMethod == 'شامل') {
                                    product.price = parseFloat(product.salePrice)
                                }
                                else if (product.taxMethod == 'Exclusive' || product.taxMethod == 'غير شامل') {
                                    var vatPrice = ((product.salePrice * product.taxRateValue) / (100)).toFixed(3).slice(0, -1);
                                    product.price = parseFloat(product.salePrice) + parseFloat(vatPrice)

                                }
                                else if (product.taxMethod == 'Exempted' || product.taxMethod == 'معفى') {
                                    product.price = parseFloat(product.salePrice)
                                }
                                root.options.push({
                                    id: product.id,
                                    name: ( product.displayName != null && product.displayName != '' ) ? product.displayName : product.code + ' ' + product.englishName,
                                    englishName: product.englishName,
                                    arabicName: product.arabicName,
                                    salePrice: product.price,
                                    barCode: product.barCode
                                })

                                root.value.push({
                                    id: product.id,
                                    name: ( product.displayName != null && product.displayName != '' ) ? product.displayName : product.code + ' ' + product.englishName,
                                    englishName: product.englishName,
                                    arabicName: product.arabicName,
                                    salePrice: product.price,
                                    barCode: product.barCode
                                })
                            })
                            root.$emit('update:modelValue', root.value);
                        }
                    });
                }
                else {
                    this.$https.get('/Product/GetProductByCategoryId?categoryId=' + category, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.options = [];

                            response.data.results.products.forEach(function (product) {
                                
                                root.options.push({
                                    id: product.id,
                                    name: ( product.displayName != null && product.displayName != '' ) ? product.displayName : product.code + ' ' + product.englishName,
                                    englishName: product.englishName,
                                    arabicName: product.arabicName,
                                })

                                
                            })
                            root.$emit('update:modelValue', root.value);
                        }
                    }).then(function () {
                        if (root.modelValue != undefined) {
                            root.modelValue.forEach(function (x) {

                                root.value.push(root.options.find(function (y) {
                                    return y.id == x.itemId;
                                }));
                            })
                        }
                        
                    });

                }
            },
        },
        computed: {
            DisplayValue: {
                get: function () {
                    if (this.value != "" || this.value != undefined) {
                        return this.value;
                    }
                    return this.modelValue;
                },
                set: function (value) {
                    this.value = value;
                    this.$emit('update:modelValue', value);
                }
            }
        },
        mounted: function () {
            this.getData();
        },
    }
</script>