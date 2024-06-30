<template>

    <input type="text" v-bind:disabled="disable" v-shortkey.avoid class="form-control RemovePadding" v-bind:class="textAlignLeft? 'text-start ' : 'text-center '" v-model="displayValue" @blur="isInputActive = false" @focus="isInputActive=true" @click="$event.target.select()" />

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        name: 'DecimalDropdown',
        props: ["modelValue", "salePriceCheck", "disable", "isQunatity", "textAlignLeft"],
        mixins: [clickMixin],

        data: function () {
            return {
                isInputActive: false,
                currency: '',
            }
        },

        computed: {
            displayValue: {
                get: function () {

                    if (this.isInputActive) {

                        if (this.modelValue == undefined) {
                            return 0;
                        }
                        return this.modelValue.toString();
                    } else {

                        if (this.salePriceCheck) {

                            this.$emit('CheckSalePrice', this.modelValue);

                        }
                        if (this.isQunatity) {
                            return parseFloat(this.modelValue);

                        }
                        else {
                            return parseFloat(this.modelValue).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");

                        }
                    }
                },
                set: function (modifiedValue) {
                    
                    if (/^[0-9\u0660-\u0669]+$/.test(modifiedValue) == true) {
                        modifiedValue = modifiedValue.replace(/[٠-٩]/g, d => "٠١٢٣٤٥٦٧٨٩".indexOf(d)).replace(/[۰-۹]/g, d => "۰۱۲۳۴۵۶۷۸۹".indexOf(d));

                    }
                    let newValue = parseFloat(modifiedValue.replace(/[^\d.]/g, ""));

                    if (isNaN(newValue)) {
                        newValue = 0
                    }
                   
                    this.$emit('update:modelValue', newValue)
                }
            }
        },
        mounted: function () {
            this.currency = localStorage.getItem('currency');
        }
    }
</script>
<style scoped>
    @media (max-width: 1180px) {


        .RemovePadding {
            padding: 0px !important;
        }
    }
</style>
