<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="true" v-bind:placeholder="$t('CategoryMultiDropdown.PleaseSelectProductCategory')" track-by="dropDownName" :clear-on-select="false" :show-labels="false" label="dropDownName" :preselect-first="true">
            <template v-slot:noResult>
            <p  class="text-danger"> {{ $t('CategoryMultiDropdown.NoCategoryFound') }}</p>
</template>
            <!--<span slot="noResult" class="btn btn-primary " v-on:click="AddCategory('Add')">{{ $t('CategoryMultiDropdown.AddCategory') }}</span><br />-->

        </multiselect>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect';
    export default {
        props: ["modelValue"],
        mixins: [clickMixin],

        components: {
            Multiselect
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                options: [],
                value: '',
                show: false,
                loading: false,
                type: '',
                rendered: 0
            }
        },
        methods: {
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                root.options = [];
                this.$https.get('/Product/GetCategoryInformation?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {

                        response.data.results.categories.forEach(function (cat) {
                            root.options.push({
                                id: cat.id,
                                dropDownName: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != "" ? cat.code + ' ' + cat.name : cat.code + ' ' + cat.nameArabic) : (cat.nameArabic != "" ? cat.code + ' ' + cat.nameArabic : cat.code + ' ' + cat.name),
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != "" ? cat.name : cat.nameArabic) : (cat.nameArabic != "" ? cat.nameArabic : cat.name)

                            })
                        })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {

                        return x.id == root.modelValue;
                    })
                });
            },
            close: function () {
                this.show = false;

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

                    this.value = value;
                    this.$emit('update:modelValue', value.id);
                }
            }
        },

        mounted: function () {
            
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.getData();
        },
    }
</script>