<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :loading="isLoading" :multiple="false" track-by="name" :clear-on-select="false" :show-labels="false" label="name" v-bind:placeholder="$t('SelectOption')" :preselect-first="true">

        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        mixins: [clickMixin],

        name: 'CountryDropdown',
        props: ["modelValue", "country"],

        components: {
            Multiselect,

        },
        data: function () {
            return {
                options: [],
                value: '',
                id: '',
                show: false,
                isLoading: false,
                type: '',

                render: 0
            }
        },
        validations() {

        },
        methods: {
            getData: function () {
                var root = this;
                this.isLoading = true;
                var url = 'https://countriesnow.space/api/v0.1/countries';

                this.$https.get(url).then(function (response) {

                    if (response.data != null) {
                        
                        response.data.data.forEach(function (cat) {
                            if (cat.country == root.country) {
                                cat.cities.forEach(function (city) {
                                    root.options.push({
                                        id: city,
                                        name: city
                                    })
                                })
                            }
                        })

                        root.isLoading = false;

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