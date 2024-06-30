<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" track-by="name" :clear-on-select="false" :show-labels="false" label="name" v-bind:placeholder="$t('CountryDropdown.SelectOption')" :preselect-first="true">
           
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        mixins: [clickMixin],

        name: 'CountryDropdown',
        props: ["modelValue"],

        components: {
            Multiselect,
            
        },
        data: function () {
            return {
                options: [],
                value: '',
               id:'',
                show: false,
                type: '',
              
                render: 0
            }
        },
        validations() {
          
        },
        methods: {
            getData: function () {
                var root = this;
                
                var url = 'https://restcountries.com/v3.1/all';
               
                
                this.$https.get(url ).then(function (response) {
                    
                    if (response.data != null) {
                        response.data.forEach(function (result) {
                            root.options.push({
                                id: result.name.common,
                                name: result.name.common
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
                    return this.value;
                },
                set: function (value) {
                    
                    this.value = value;
                    this.$emit('update:modelValue', value.id);
                }
            }
        },
        mounted: function () {
            this.getData();
        },
    }
</script>