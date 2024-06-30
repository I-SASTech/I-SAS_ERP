<template>
    <div>
        <multiselect v-if="disabled" disabled="disabled == 'true'" v-model="DisplayValue" :options="options" :multiple="false"  :placeholder="$t('AddPriceLabeling.SelectPriceLabeling')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" >
        </multiselect>
        <multiselect v-else v-model="DisplayValue" :options="options" :multiple="false"  :placeholder="$t('AddPriceLabeling.SelectPriceLabeling')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" >
        </multiselect>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    export default {
        name: 'priceLabelingdropdown',
        props: ["modelValue", "disabled","isUpdate","isSingle"],
        mixins: [clickMixin],

        components: {
            Multiselect,
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                options: [],
                value: '',
                show: false,
                type: '',
               
                render: 0,
                loading: false,
            }
        },
        methods: {
            GetAmountOfSelected: function () {

                if (this.options.length > 0)
                    return this.options[0].price;
                else
                    return this.options.price;
            },
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Product/PriceLabelingList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        
                        response.data.results.priceLabelings.forEach(function (cat) {
                            root.options.push({
                                id: cat.id,
                                name: cat.name,
                                price: cat.price,
                            })
                        })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                });
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
                    
                    if(this.isSingle==true)
                    {
                        this.value = value;
                        this.$emit('update:modelValue', value.id);

                    }
                    else
                    {
                        this.value = value;
                  
                    this.$emit('update:modelValue', value);
                    }

                    



                }
            }
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.getData();
        },
    }
</script>