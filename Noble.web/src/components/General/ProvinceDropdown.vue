<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" :placeholder="$t('ProvinceDropdown.SelectState')" track-by="name"                    
                     :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
           
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'

    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: 'Provincedropdown',
        props: ["modelValue","country"],

        components: {
            Multiselect,
            
        },
        data: function () {
            return {
                options: [],
               value: '',
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
                var url = 'https://geodata.solutions/api/api.php';
               
                
                this.$https.get(url + '?type=getStates&countryId=' + this.country  ).then(function (response) {
                    
                    if (response.data != null) {
                        for (const [key, value] of Object.entries(response.data.result)) {
                            root.options.push({
                                id: key,
                                name: value
                            });
                        }

                       
                    }
                }).then(function () {
                    
                    root.value = root.options.find(function (x) {
                        return x.name == root.modelValue;
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
                    this.value = value;
                    this.$emit('update:modelValue', value.name);
                }
            }
        },
        mounted: function () {
            if (this.country != "") {
                this.getData();
            }
        },
    }
</script>