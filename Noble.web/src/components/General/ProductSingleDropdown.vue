<template>
    <div  v-bind:class="dropdownpo">
        <multiselect v-model="selectedValue"
                     @update:modelValue="$emit('update:modelValue', selectedValue.id, products.find(x => x.id == selectedValue.id))"
                     :options="options"
                     :multiple="false"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     :preselect-first="true"                    
                     @search-change="asyncFind">
                     <template v-slot:noResult>
            <p  class="text-danger"> {{ $t('ProductSingleDropdown.NoProductFound') }}</p>
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
        props: ["modelValue", "wareHouseId", "dropdownpo"],

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
                
                var url = "/Product/GetProductInformation?searchTerm=" + search;
                this.$https
                    .get(url, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        

                        if (response.data != null) {
                            root.options = [];
                            root.products = response.data.results.products;

                            if (root.products != undefined) {
                                root.products.forEach(function (prod) {
                                    if (root.modelValue == prod.id && root.modelValue != undefined) {
                                        root.selectedValue.push({
                                            id: prod.id,
                                            name: ( prod.displayName != null || prod.displayName != '' ) ? prod.displayName : prod.code + ' ' + prod.englishName,
                                        });
                                        root.product = prod;
                                    }

                                    root.options.push({
                                        id: prod.id,
                                        name: ( prod.displayName != null || prod.displayName != '' ) ? prod.displayName : prod.code + ' ' + prod.englishName,
                                    });
                                });
                            }
                        }
                    });
            },
        },
        computed: {

        },
        created: function () {
            var val = this.modelValue;
            if(val == '00000000-0000-0000-0000-000000000000'){
                val = '';
                this.asyncFind(val);
            }
            else{
                this.asyncFind(this.modelValue);
            }
        },
    };
</script>
