<template>
    <div v-if="isMultiple" v-bind:class="dropdownAccount">
        <multiselect v-model="selectedValue"
                     @update:modelValue="$emit('update:modelValue', selectedValue)"
                     :options="options"
                     :multiple="true"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     :preselect-first="true"
                     v-bind:placeholder="$t('SaleInvoiceDropdown.Selectinvoice')">
        </multiselect>
    </div>
    <div v-else-if="isClass" v-bind:class="dropdownAccount">
        <multiselect v-model="selectedValue"
                     @update:modelValue="$emit('update:modelValue', selectedValue.id)"
                     :options="options"
                     :multiple="false"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     :preselect-first="true"
                     v-bind:placeholder="$t('Seaech By Invoice Date, Invoice Number, Due Days')">
        </multiselect>
    </div>
    <div v-else-if="isDisabled">
        <multiselect v-model="selectedValue"
                     @update:modelValue="$emit('update:modelValue', selectedValue.id)"
                     :options="options"
                     :multiple="false"
                     track-by="name"
                     disabled
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     :preselect-first="true"
                     v-bind:placeholder="$t('Seaech By Invoice Date, Invoice Number, Due Days')">
        </multiselect>
    </div>
    <div v-else-if="isMultiSelect">
        <multiselect v-model="selectedValue"
                     @update:modelValue="$emit('update:modelValue', selectedValue)"
                     :options="options"
                     :multiple="true"
                     track-by="name"
                     disabled
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     :preselect-first="true"
                     v-bind:placeholder="$t('SaleInvoiceDropdown.Selectinvoice')">
        </multiselect>
    </div>
    <div v-else>
        <multiselect v-model="selectedValue"
                     @update:modelValue="$emit('update:modelValue', selectedValue.id)"
                     :options="options"
                     :multiple="false"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     :preselect-first="true"
                     v-bind:placeholder="$t('Seaech By Invoice Date, Invoice Number, Due Days')">
        </multiselect>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    import Multiselect from "vue-multiselect";
    export default {
        mixins: [clickMixin],

        name: "SaleInvoiceDropdown",
        props: ["modelValue", 'dropdownaccount', 'isClass', 'isCredit', 'selectedIdInvoice', 'isExpense', 'contactId', 'isDisabled', 'isService', 'customerId', 'isReturn','isMultiple','isPartially'],

        components: {
            Multiselect,
        },
        data: function () {
            return {
                dropdownAccount: "",
                options: [],
                selectedValue: [],
                isDropdown: true,
                forCustomerId: '',
                forContactId: '',
            };
        },
        methods: {
            getData: function () {
                 //eslint-disable-line
                var root = this;
                var token = '';
               
                token = localStorage.getItem('token');
                
                var status = 'Paid';
                if (this.isCredit) {
                    status = 'Credit';
                }
                if (this.isCredit=='ProformaInvoice') {
                    status = 'ProformaInvoice';
                }
                var isRecipt = false;
                if (this.isExpense) {
                    isRecipt = true;
                }
                if (root.contactId == undefined) {
                    root.forContactId = '';
                }
                else
                {
                    root.forContactId = root.contactId
                }
                if (root.customerId == undefined) {
                    root.forCustomerId = '';
                }
                else
                {
                    root.forCustomerId = root.customerId
                }
                
                var service = false;
                if (root.isService == true) {
                    service = true;
                }
                var isPartially=false;
                if(this.isPartially)
                {
                    isPartially=true;
                }

                var url = '';
                var branchId = localStorage.getItem('BranchId');
                {
                    url = '/Sale/SaleInvoiceList?status=' + status + '&isDropdown=' + root.isDropdown + '&isService=' + service + '&branchId=' + branchId + '&customerId=' + root.forCustomerId + '&isPartially=' + isPartially + '&isExpense=' + isRecipt + '&contactId=' + root.forContactId;
                }
                
                this.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.options = [];
                        response.data.results.sales.forEach(function (result) {
                            if (root.modelValue == result.id && root.modelValue != undefined) {
                                root.selectedValue.push({
                                    id: result.id,
                                    name: result.registrationNumber +  ' - ' + localStorage.getItem('currency') + result.netAmount + " - " + root.getDate(result.date),
                                    amount: result.netAmount
                                });
                            }

                            root.options.push({
                                id: result.id,
                                name: result.registrationNumber +  ' - ' + localStorage.getItem('currency') + result.netAmount + " - " + root.getDate(result.date),
                                amount: result.netAmount
                            })
                        })

                        if (root.options.length > 0) {
                            if (root.selectedIdInvoice != undefined && root.selectedIdInvoice.length > 0 && root.values == undefined) {
                                root.selectedIdInvoice.forEach(function (x) {
                                    root.options.splice(root.options.findIndex(function (y) {
                                        return y.id === x.saleInvoice;
                                    }), 1);
                                });

                            }
                        }
                    }
                });
            },
            getDate: function (x) {
                return moment(x).format('DD MMM YYYY');
            },
            GetAmountOfSelected: function () {
                if (this.selectedValue.length > 0)
                    return this.selectedValue[0].amount;
                else
                    return this.selectedValue.amount;
            }
        },
        computed: {

        },
        mounted: function () {
            this.dropdownAccount = this.dropdownaccount;

            this.getData();
        }
    }
</script>
