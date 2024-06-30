<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" v-bind:placeholder="$t('SaleInvoiceDropdown.Selectinvoice')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <template v-slot:noResult>
            <p  class="text-danger">{{ $t('SaleInvoiceDropdown.NoInvoiceFound') }}</p>
            </template>
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        name: 'ProformaInvoiceDropdown',
        props: ["modelValue", 'isService'],
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
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                var url = '';
                if (this.isService) {
                    url = '/Sale/SaleList?status=' + 'ProformaInvoice' + '&isDropdown=true' + '&branchId=' + localStorage.getItem('BranchId') 
                }
                else {
                    url = '/Sale/SaleList?status=' + 'ProformaInvoice' + '&isDropdown=true' + '&branchId=' + localStorage.getItem('BranchId') 
                }

                this.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        response.data.results.sales.forEach(function (cat) {                            
                            root.options.push({
                                id: cat.id,
                                name: cat.registrationNumber
                            })
                       })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                });
            },
            close: function () {
                this.show = false;
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
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.getData();
        },
    }
</script>