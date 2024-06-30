<template>
    <div v-if="isCurrency">
        <multiselect v-model="selectedValue" :placeholder="$t('CurrencyDropdown.selectCard')" label="name" track-by="name"
                     @update:modelValue="$emit('update:modelValue', selectedValue == null? null: selectedValue.sign)"
                   
                     :options="options" :option-height="104" :show-labels="false">

            <template v-slot:singleLabel="props">
                <img class="option__image" v-if="props.option.img==''" src="images/Product.png" width="20" height="20">
                <img v-else class="option__image" :src="'data:image/png;base64,' + props.option.img" width="20" height="20">
                <span class="option__desc"><span class="option__title">{{ props.option.name }}</span></span>
            </template>

            <template v-slot:option="props">
                <img v-if="props.option.img==''" class="option__image float-left" :src="'data:image/png;base64,' + props.option.img" width="20" height="20">
                <img v-else class="option__image float-left" src="images/Product.png" width="20" height="20">
                <div class="option__desc"><span class="option__title">{{ props.option.name }}</span></div>
            </template>
        </multiselect>
    </div>
    <div v-else>
        <multiselect  v-model="selectedValue" :placeholder="$t('CurrencyDropdown.selectCard')" label="name" track-by="name"
                     @update:modelValue="$emit('update:modelValue', selectedValue == null? null: selectedValue.id)"
                    
                     :options="options" :option-height="104" :show-labels="false">

            <template v-slot:singleLabel="props">
                <img v-if="props.option.img==''" class="option__image" src="images/Product.png" width="20" height="20">
                <img v-else class="option__image" :src="'data:image/png;base64,' + props.option.img" width="20" height="20">
                <span class="option__desc"><span class="option__title">{{ props.option.name }}</span></span>
            </template>

            <template v-slot:option="props">
                <img v-if="props.option.img==''" class="option__image float-left" src="images/Product.png" width="20" height="20">
                <img v-else class="option__image float-left" :src="'data:image/png;base64,' + props.option.img" width="20" height="20">
                <div class="option__desc"><span class="option__title">{{ props.option.name }}</span></div>
            </template>
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from "vue-multiselect";
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        mixins: [clickMixin],

        name: "CurrencyDropdown",
        props: ["modelValue","isCurrency"],

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
                    .get("/Product/CurrencyList", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            response.data.currencies.forEach(function (opt) {
                                
                                if (opt.isActive) {

                                    if (root.isCurrency) {
                                        if (root.modelValue == opt.sign && root.modelValue != undefined && root.modelValue != "") {
                                            root.selectedValue.push({
                                                id: opt.id,
                                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (opt.name != '' && opt.name != null) ? opt.name : opt.nameArabic : (opt.nameArabic != '' && opt.nameArabic != null) ? opt.nameArabic : opt.name,
                                                sign: opt.sign,
                                                img: opt.image
                                            });
                                        }
                                    }
                                    else {
                                        if (root.modelValue == opt.id && root.modelValue != undefined && root.modelValue != "") {
                                            root.selectedValue.push({
                                                id: opt.id,
                                                sign: opt.sign,
                                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (opt.name != '' && opt.name != null) ? opt.name : opt.nameArabic : (opt.nameArabic != '' && opt.nameArabic != null) ? opt.nameArabic : opt.name,
                                                img: opt.image
                                            });
                                        }
                                    }
                                    root.options.push({
                                        id: opt.id,
                                        name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (opt.name != '' && opt.name != null) ? opt.name : opt.nameArabic : (opt.nameArabic != '' && opt.nameArabic != null) ? opt.nameArabic : opt.name,
                                        sign: opt.sign,
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