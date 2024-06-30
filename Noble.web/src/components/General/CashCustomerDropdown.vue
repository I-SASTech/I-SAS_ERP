<template>
    <div>
        <multiselect v-model="selectedValue"
                     @update:modelValue="$emit('update:modelValue', selectedValue == null? null: selectedValue.id)"
                     @search-change="$emit('search-change',$event)"
                     @close="$emit('close')"
                     :options="options"                    
                     :multiple="false"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"

                     :preselect-first="true">
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from "vue-multiselect";
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        mixins: [clickMixin],

        name: "CashCustomerDropdown",
        props: ["modelValue"],

        components: {
            Multiselect,
        },
        data: function () {
            return {
                options: [],
                selectedValue: [],
            };
        },
        methods: {
            getData: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                this.$https
                    .get("/Sale/CashCustomerList", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            response.data.forEach(function (opt) {

                                if (root.modelValue == opt.name && root.modelValue != undefined && root.modelValue != "") {
                                    root.selectedValue.push({
                                        id: opt.id,
                                        name: opt.code + "-" + opt.name,
                                    });
                                }
                                root.options.push({
                                    id: opt.id,
                                    name: opt.code + "-" + opt.name,
                                });
                            });
                        }

                        root.options.push({
                            id: '000000000-0000-0000-000000000',
                            name: 'test',
                        });
                    });
            },
        },
        mounted: function () {
            this.getData();
        }
    };
</script>