<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :searchable="false" :disabled="disabled" :multiple="false" :placeholder="$t('SalutationDropdown.Salutation')" track-by="name" :clear-on-select="false" :show-labels="false" label="name">

        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: 'SalutationDropdown',
        props: ["modelValue", 'disabled'],

        components: {
            Multiselect
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                options: [{ name: 'Mr.'},{ name: 'Mrs.'}, { name: 'Ms.'}, { name: 'Miss.'}, { name: 'Dr.'}, { name: 'Dr.'}],
                value: '',
                loading: false,
            }
        },
        methods: {
            getData: function () {
                var root = this;
                root.value = root.options.find(function (x) {
                    return x.name == root.modelValue;
                })
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
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.getData();
        },
    }
</script>