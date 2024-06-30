<template>
    <div>
        <!--<multiselect v-model="selectedValue"
                     @update:modelValue="$emit('update:modelValue', selectedValue == null? null: selectedValue.id)"
                     :options="options"
                     :multiple="false"
                     track-by="name"
                     :clear-on-select="false"
                     :show-labels="false"
                     label="name"
                     :preselect-first="true">
        </multiselect>-->

        <multiselect v-model="selectedValue" placeholder="select card" label="name" track-by="name"
                     @update:modelValue="$emit('update:modelValue', selectedValue == null? null: selectedValue.id)"
                     :options="options" :option-height="104" :show-labels="false">

            <template v-slot:singleLabel="props">
                <img class="option__image" :src="'data:image/png;base64,' + props.option.img" width="20" height="20">
                <span class="option__desc"><span class="option__title">{{ props.option.name }}</span></span>
            </template>

            <template v-slot:option="props">
                <img class="option__image float-left" :src="'data:image/png;base64,' + props.option.img" width="20" height="20">
                <div class="option__desc"><span class="option__title">{{ props.option.name }}</span></div>
            </template>
        </multiselect>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from "vue-multiselect";
    export default {
        name: "PaymentOptionDropdown",
        props: ["modelValue"],
        mixins: [clickMixin],

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
                    .get("/Product/PaymentOptionsList?isActive=true", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            response.data.paymentOptions.forEach(function (opt) {
                                if (opt.isActive) {

                                    if (root.modelValue == opt.id && root.modelValue != undefined && root.modelValue != "") {
                                        root.selectedValue.push({
                                            id: opt.id,
                                            name: opt.name,
                                            img: opt.image
                                        });
                                    }
                                    root.options.push({
                                        id: opt.id,
                                        name: opt.name,
                                        img: opt.image
                                    });
                                }
                            });

                        }
                    });
            },
        },
        mounted: function () {
            this.getData();
        }
    };
</script>