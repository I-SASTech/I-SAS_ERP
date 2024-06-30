<template>
    <div>
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

    export default {
        //name: "SupplierDropdown",
        props: ["modelValue", 'isservice'],
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
            getData: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                var url = '/EmployeeRegistration/TemporaryCashRequestList?isDropdown=true';
                

                this.$https.get(url, { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {

                        if (response.data != null) {
                            response.data.results.forEach(function (sup) {
                                root.options.push({
                                    id: sup.id,
                                    name: sup.registrationNo,
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