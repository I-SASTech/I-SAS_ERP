<template>
    <div>
        <multiselect v-model="DisplayValue"
                     :options="options"
                     :multiple="false"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     v-bind:placeholder="$t('DayDropdown.SelectOption')"                  
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
        props: ["modelValue"],

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
                
                this.$https
                    .get('/Product/DayStartReportList', {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {

                        if (response.data != null) {
                            response.data.forEach(function (sup) {
                                root.options.push({
                                    id: sup.id,
                                    name: sup.fromTime + ' - ' + sup.toTime,
                                    counterId: sup.counterId,
                                    toTime: sup.toTime,
                                    fromTime: sup.fromTime
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
                        this.$emit("update:modelValue", value.fromTime);
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