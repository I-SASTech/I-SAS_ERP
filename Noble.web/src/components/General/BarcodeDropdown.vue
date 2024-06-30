<template>
    <div>
        <multiselect v-if="!disable" v-model="DisplayValue" :options="options" :multiple="false" placeholder="Select Barcode Type" track-by="name" :clear-on-select="false" :show-labels="false" label="name">
            
        </multiselect>
        <multiselect v-else disabled v-model="DisplayValue" :options="options" :multiple="false" placeholder="Select Barcode Type" track-by="name" :clear-on-select="false" :show-labels="false" label="name">
            
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    export default {
        name: 'barcodeDropdown',
        props: ['disable', 'modelValue'],
        components: {
            Multiselect,
        },
        data: function () {
            return {
               value: [],
                options: [
                    { name: 'EAN-2', id: 'EAN2', limit:'2' },
                    { name: 'EAN-5', id: 'EAN5', limit:'5' },
                    { name: 'EAN-8', id: 'EAN8', limit:'8' },
                    { name: 'EAN-13', id: 'EAN13', limit:'13' },
                    { name: 'UPC', id: 'UPC', limit:'12' },
                    { name: 'ITF-14', id: 'ITF14', limit:'14' },
                    { name: 'CODE 39', id: 'code39', limit:'12' },
                    { name: 'CODE 128', id: 'code128', limit:'128' },
                    { name: 'CODABAR', id: 'codabar', limit:'10' },
                    { name: 'Pharmacode', id: 'pharmacode', limit:'5' },
                    { name: 'MSI', id: 'msi', limit:'4' }
                ],
                render: 0
            }
        },
        computed: {
            DisplayValue: {
                get: function () {
                    if (this.value != "" || this.value != undefined) {
                        var root = this;
                        var newValue;
                        newValue = root.options.find(function (x) {
                        return x.id == root.modelValue;
                        })
                        return newValue;
                    }
                  return this.modelValue;
                },
                set: function (value) {
                    this.value = value;
                    this.$emit('update:modelValue', value.id);
                }
            }
        },
    }
</script>