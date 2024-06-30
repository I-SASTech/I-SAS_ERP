<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" v-bind:placeholder="$t('AddContribution.SelectOption')" track-by="area" :clear-on-select="false" :show-labels="false" label="area" :preselect-first="true" >

        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'


    export default {
        mixins: [clickMixin],

        name: 'ContributionDropdown',
        props: ["modelValue"],

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
                this.$https.get('/Payroll/ContributionList?isDropdown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        response.data.results.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                nameInPayslip: result.nameInPayslip,
                                nameInPayslipArabic: result.nameInPayslipArabic,
                                amountType: result.amountType,
                                amount: result.amount,
                                area: result.code + ' ' + result.nameInPayslip
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