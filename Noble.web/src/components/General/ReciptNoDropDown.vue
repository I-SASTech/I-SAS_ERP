<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" track-by="registrationNumber" :clear-on-select="false" :show-labels="false" label="registrationNumber" :preselect-first="true" v-bind:placeholder="$t('ReciptNoDropDown.SelectOption')"  >

        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],

        props: ["modelValue"],

        components: {
            Multiselect,
          
            
        },
        data: function () {
            return {
                options: [],
                value: '',
                isDropDown: true
            }
        },
        methods: {
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                 
                this.$https.get('/Batch/RecipeNoList?isDropDown=' + root.isDropDown, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                     
                    if (response.data != null) {

                        response.data.results.forEach(function (results) {
                             
                            root.options.push({
                                id: results.id,
                                registrationNumber: results.registrationNumber + " --" + results.recipeName + " --" + results.productName + " --" + results.quantity,
                               
                               
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