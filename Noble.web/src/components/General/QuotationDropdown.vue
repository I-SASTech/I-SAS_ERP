<template>
    <div>
        <multiselect v-model="DisplayValue"
                     :options="options"
                     :multiple="false"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     v-bind:placeholder="$t('QuotationDropdown.SelectOption')"                   
                     :preselect-first="true">
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from "vue-multiselect";
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        //name: "SupplierDropdown",
        props: ["modelValue","isservice","contactAccountId",'isPartially'],

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

            Clear: function () {
              this.value='';
               
            },
            getData: function () {
                var root = this;
                 
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                var branchId = localStorage.getItem('BranchId');


                var url = '';
                if (this.isservice) {
                    url = '/Purchase/SaleServiceOrderList?isDropdown=true'
                }
                else {
                    url = '/Purchase/SaleOrderList?isDropdown=true'
                }
               

                var isPartially=false;
                if(this.isPartially)
                {
                    isPartially=true;
                }

                this.$https.get(url + '&isQuotation=true' + '&status=3' + '&isService=' + this.isservice + '&branchId=' + branchId+ '&contactAccountId=' + this.contactAccountId+ '&isPartially=' +isPartially, {headers: { Authorization: `Bearer ${token}` },})
                    .then(function (response) {

                        if (response.data != null) {
                            response.data.results.forEach(function (sup) {
                                root.options.push({
                                    id: sup.id,
                                    netAmount: sup.netAmount,
                                    name: sup.registrationNumber+  ' - ' + localStorage.getItem('currency') +  ' - ' + sup.netAmount,
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