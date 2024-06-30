<template>
    <div v-if="isClass" v-bind:class="dropdownAccount">
        <multiselect v-model="DisplayValue" :options="options" v-bind:placeholder="$t('PurchaseInvoiceDropDown.SelectOption')" :multiple="false" track-by="registrationNumber" :clear-on-select="false" :show-labels="false" label="registrationNumber" :preselect-first="true" >

        </multiselect>
    </div>
    <div v-else-if="isDisabled">
        <multiselect v-model="DisplayValue" :options="options" disabled v-bind:placeholder="$t('PurchaseInvoiceDropDown.SelectOption')" :multiple="false" track-by="registrationNumber" :clear-on-select="false" :show-labels="false" label="registrationNumber" :preselect-first="true" >

        </multiselect>
    </div>
    <div v-else>
        <multiselect v-model="DisplayValue" :options="options" v-bind:placeholder="$t('PurchaseInvoiceDropDown.SelectOption')" :multiple="false" track-by="registrationNumber" :clear-on-select="false" :show-labels="false" label="registrationNumber" :preselect-first="true" >

        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ["modelValue", 'supplierid', 'dropdownaccount', 'isClass', 'selectedIdPrucahse', 'isExpense', 'isDisabled','supplierAccountId'],

        components: {
            Multiselect,
        },
        data: function () {
            return {
                dropdownAccount: "",
                options: [],
                value: '',
                isDropDown: true,
                i: 0
            }
        },
        methods: {
            getData: function () {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var isExpense = false;
                if (this.isExpense) {
                    isExpense  = true;
                }
                var branchId = localStorage.getItem('BranchId');
                
                var suplierId = (this.supplierid == undefined || this.supplierid == null || this.supplierid == '') ? '00000000-0000-0000-0000-000000000000' : this.supplierid;
                var supplierAccountId = (this.supplierAccountId == undefined || this.supplierAccountId == null || this.supplierAccountId == '') ? '00000000-0000-0000-0000-000000000000' : this.supplierAccountId;
                root.options = [];
                this.$https.get('/PurchasePost/PurchasePostList?isDropDown=' + root.isDropDown + '&supplierid=' + suplierId + '&isExpense=' + isExpense + '&supplierAccountId=' + supplierAccountId + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        
                        root.product = response.data
                        response.data.results.forEach(function (results) {
                            root.options.push({
                                id: results.id,
                                registrationNumber: results.registrationNumber + '   ' + results.invoiceNo + ' - ' +  results.netAmount + ' ( ' + results.date + ')',
                                netAmount: results.netAmount
                            });
                        })
                        if (root.options.length > 0) {
                            
                            if (root.selectedIdPrucahse != undefined && root.selectedIdPrucahse.length > 0 && root.modelValue == undefined) {
                                
                                root.selectedIdPrucahse.forEach(function (x) {
                                    root.options.splice(root.options.findIndex(function (y) {
                                        return y.id === x.purchaseInvoice;
                                    }), 1);
                                });
                            }
                        }
                    }
                }).then(function () {
                    
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                });
            },

            GetAmountOfSelected: function () {
                
                if (this.value.length > 0)
                    return this.value[0].amount;
                else
                    return this.value.netAmount;
            }
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
            this.dropdownAccount = this.dropdownaccount;
            this.getData();
        },
    }
</script>