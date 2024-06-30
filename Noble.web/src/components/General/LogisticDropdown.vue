<template>
    <div>
        <multiselect v-model="DisplayValue"
                     :options="options"
                     :multiple="false"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     v-bind:placeholder="$t('LogisticDropdown.SelectOption')"
                   
                     :preselect-first="true">
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from "vue-multiselect";
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        //name: "SupplierDropdown",
        props: ["modelValue"],
        mixins: [clickMixin],
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
            EmptyRecord: function () {
                
                this.DisplayValue='';

            
            },
            getData: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                var branchId = localStorage.getItem('BranchId');

                this.$https
                    .get('/Region/LogisticList?logisticsForm=Transporter' + '&isActive=true' + '&branchId=' + branchId, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {

                        if (response.data != null) {
                            
                            response.data.results.logistics.forEach(function (sup) {
                                root.options.push({
                                    id: sup.id,
                                    name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (sup.englishName != '' && sup.englishName != null) ? sup.code + ' ' + sup.englishName : sup.code + ' ' + sup.arabicName : (sup.arabicName != '' && sup.arabicName != null) ? sup.code + ' ' + sup.arabicName : sup.code + ' ' + sup.name
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
                        this.$emit("update:modelValue", value.id,value);
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