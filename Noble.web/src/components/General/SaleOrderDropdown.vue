<template>
    <div v-if="isMultiple">
        <multiselect v-model="DisplayValue"
                     :options="options"
                     :multiple="true"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     v-bind:placeholder="$t('SaleOrderDropdown.SelectOption')"                   
                     :preselect-first="true">
        </multiselect>
    </div>
    <div v-else-if="isDisabled==true">
        <multiselect v-model="DisplayValue"
                     disabled
                     :options="options"
                     :multiple="false"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     v-bind:placeholder="$t('SaleOrderDropdown.SelectOption')"                   
                     :preselect-first="true">
        </multiselect>
    </div>
    <div v-else>
        <multiselect v-model="DisplayValue"
                     
                     :options="options"
                     :multiple="false"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     v-bind:placeholder="$t('SaleOrderDropdown.SelectOption')"                   
                     :preselect-first="true">
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from "vue-multiselect";
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";


    export default {
        mixins: [clickMixin],

        //name: "SupplierDropdown",
        props: ["modelValue", 'isservice', 'isDisabled', 'contactAccountId', 'isMultiple', 'isPartially','isQuotation','isForBom'],

        components: {
            Multiselect,
        },
        data: function () {
            return {
                options: [],
                value: "",
            };
        },
        methods: {
            GetAmountOfSelected: function () {
                if (this.DisplayValue.length > 0)
                    return this.options[0].netAmount;
                else
                    return this.DisplayValue.netAmount;
            },
            getDate: function (x) {
                return moment(x).format('DD MMM YYYY');
            },
            getData: function () {
                
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                var branchId = localStorage.getItem('BranchId');
                var isPartially=false;
                if(this.isPartially)
                {
                    isPartially=true;
                }
                var isForBom = false;
                if(this.isForBom)
                {
                    isForBom=true;
                }

                var url = '';
                var isQuotationProp = this.isQuotation == true ? true : false;
                if (this.isservice) {
                    url = '/Purchase/SaleServiceOrderList?isDropdown=true' + '&branchId=' + branchId + '&isService=' + this.isservice + '&isPartially=' + isPartially + '&isQuotation=' + isQuotationProp + '&isForBom=' + isForBom;
                }
                else {
                    url = '/Purchase/SaleOrderList?isDropdown=true' + '&branchId=' + branchId + '&isPartially=' + isPartially + '&isQuotation=' + isQuotationProp + '&isForBom=' + isForBom;
                }
                
                var contactAccountId='';
                if(this.contactAccountId!=null && this.contactAccountId!=undefined && this.contactAccountId!='' )
                {
                    contactAccountId=this.contactAccountId;

                } 


                this.$https.get(url+ '&contactAccountId=' + contactAccountId, {headers: { Authorization: `Bearer ${token}` },})
                    .then(function (response) {

                        if (response.data != null) {
                            response.data.results.forEach(function (sup) {
                                root.options.push({
                                    id: sup.id,
                                    netAmount: sup.netAmount,
                                    name: sup.registrationNumber +  ' - ' + localStorage.getItem('currency') + sup.netAmount + " - " + root.getDate(sup.date),
                                });
                            });
                        }
                    })
                    .then(function () {
                        root.value = root.options.find(function (x) {
                            return x.id == root.modelValue;
                        });
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

                    if (this.isMultiple) {
                        this.value = value;
                        this.$emit("update:modelValue", value);
                    }
                    if (value != null) {
                        this.value = value;
                        this.$emit("update:modelValue", value.id);
                    }
                    else {
                        this.value = value;
                        this.$emit("update:modelValue", null);
                    }
                },
            },
        },
        mounted: function () {
            this.getData();
        },
    };
</script>