<template>
    <div>
        <multiselect v-if="PeriodName == 'Monthly'" v-model="DisplayValue" :options="options" :searchable="false" :disabled="disabled" :multiple="false" :placeholder="$t('Compare With')" track-by="name" :clear-on-select="false" :show-labels="false" label="name">

        </multiselect>
        <multiselect v-if="PeriodName == 'Quarterly'" v-model="DisplayValue" :options="options2" :searchable="false" :disabled="disabled" :multiple="false" :placeholder="$t('Compare With')" track-by="name" :clear-on-select="false" :show-labels="false" label="name">

        </multiselect>
        <multiselect v-if="PeriodName == 'Yearly'" v-model="DisplayValue" :options="options1" :searchable="false" :disabled="disabled" :multiple="false" :placeholder="$t('Compare With')" track-by="name" :clear-on-select="false" :show-labels="false" label="name">

        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: 'MonthlyDropdown',
        props: ["modelValue", 'disabled','PeriodName'],

        components: {
            Multiselect
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                options: [{ name: '1 Month',id:1},{ name: '2 Month',id:2}, { name: '3 Month',id:3},{ name: '4 Month',id:4},{ name: '5 Month',id:5},{ name: '6 Month',id:6},{ name: '7 Month',id:7},{ name: '8 Month',id:8},{ name: '9 Month',id:9},{ name: '10 Month',id:10},{ name: '11 Month',id:11},{ name: '12 Month',id:12}],
                options1: [{ name: '1 Year',id:1},{ name: '2 Year',id:2}, { name: '3 Year',id:3},{ name: '4 Year',id:4} ,{ name: '5 Year',id:5}],
                options2: [{ name: '1 Quartar',id:1},{ name: '2 Quartar',id:2}, { name: '3 Quartar',id:3},{ name: '4 Quartar',id:4}],
                value: '',
                loading: false,
            }
        },
        methods: {
            getData: function () {
                var root = this;
                 
                if(root.PeriodName == 'Monthly')
                {
                    root.value = root.options.find(function (x) {
                        return x.name == root.modelValue;
                    })
                }
                else if(root.PeriodName == 'Quarterly'){
                    root.value = root.options2.find(function (x) {
                        return x.name == root.modelValue;
                    })
                }
                else if(root.PeriodName == 'Yearly')
                {
                    root.value = root.options1.find(function (x) {
                        return x.name == root.modelValue;
                    })
                }
                
               
            },
        },
        computed: {
            DisplayValue: {
                get: function () {
                    if (this.value != "" || this.value != undefined) {
                        return  this.value;
                    }
                    return this.modelValue;
                },
                set: function (value) {
                    // value.name= 'Compare With'+ ' ' + value.name;

                    this.value = value;
                    this.$emit('update:modelValue', value.id);
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