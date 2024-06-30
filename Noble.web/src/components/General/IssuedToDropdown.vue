<template>
    <div>
        <multiselect v-bind:disabled="disabled" v-model="DisplayValue" :options="options" :multiple="false" v-bind:placeholder="$t('AddAllowance.SelectOption')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">

        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        name: 'IssuedToDropdown',
        props: ["modelValue","disabled"],
        mixins: [clickMixin],

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
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Payroll/IssuedToListQuery', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        response.data.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                name: result.name,
                                accountId: result.accountId,
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
                    this.$emit('update:modelValue', value);
                }
            }
        },

        mounted: function () {
            this.getData();
        },
    }
</script>