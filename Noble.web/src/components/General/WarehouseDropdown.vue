<template>
    <div>
        <multiselect v-if="disable" v-model="selectedValue"
                     @update:modelValue="$emit('update:modelValue', selectedValue.id,selectedValue.name)" disabled
                     :options="options" :placeholder="$t('WarehouseDropdown.SelectWarehouse')" :multiple="false" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
        </multiselect>
        <multiselect v-else v-model="selectedValue"
                     @update:modelValue="$emit('update:modelValue', selectedValue.id,selectedValue.name)"
                     :options="options" :placeholder="$t('WarehouseDropdown.SelectWarehouse')" :multiple="false" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        props: ["modelValue", "disable","fromWareHouse"],
        mixins: [clickMixin],

        components: {
            Multiselect,

        },
        data: function () {
            return {
                options: [],
                selectedValue: []
            }
        },
        methods: {
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.get('/Company/GetWarehouseInformation?isDropdown=true', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        
                        if (response.data != null) {
                            
                            response.data.warehousesListModels.forEach(function (wareHouse) {
                                
                                if (root.modelValue == wareHouse.id && root.modelValue != undefined) {
                                        root.selectedValue.push({

                                            id: wareHouse.id,
                                            name: (localStorage.getItem('locales') == 'en' ) ? wareHouse.storeID + ' ' + wareHouse.name : wareHouse.storeID + ' ' + wareHouse.nameArabic

                                        });
                                }
                           
                                else {
                                    
                                    if (wareHouse.storeID == 'S001') {
                                        root.$emit('default', wareHouse.id,wareHouse.name);
                                        localStorage.setItem('WareHouseId', wareHouse.id);
                                    }
                                }

                                if (root.fromWareHouse != undefined && root.fromWareHouse != "") {

                                    if (root.fromWareHouse != wareHouse.id) {
                                        root.options.push({
                                            id: wareHouse.id,
                                            name: (localStorage.getItem('locales') == 'en' ) ? (wareHouse.name != '' && wareHouse.name != null) ? wareHouse.storeID + ' ' + wareHouse.name : wareHouse.storeID + ' ' + wareHouse.nameArabic : (wareHouse.nameArabic != '' && wareHouse.nameArabic != null) ? wareHouse.storeID + ' ' + wareHouse.nameArabic : wareHouse.storeID + ' ' +wareHouse.name
                                        })
                                    }
                                }
                                else {
                                    root.options.push({
                                        id: wareHouse.id,
                                        name: (localStorage.getItem('locales') == 'en' ) ? (wareHouse.name != '' && wareHouse.name != null) ? wareHouse.storeID + ' ' + wareHouse.name : wareHouse.storeID + ' ' + wareHouse.nameArabic : (wareHouse.nameArabic != '' && wareHouse.nameArabic != null) ? wareHouse.storeID + ' ' + wareHouse.nameArabic : wareHouse.storeID + ' ' + wareHouse.name
                                    })
                                }
                            })
                        }
                    });
            },
            EmtyWareHouseList: function () {
                
                this.selectedValue='';

            
            },
            GetWareHouseName: function () {
                
                if (this.selectedValue.name != undefined)
                    return this.selectedValue.name;
                else
                    return this.selectedValue[0].name;
            }
        },
        
        mounted: function () {
            this.getData();
        },
    }
</script>