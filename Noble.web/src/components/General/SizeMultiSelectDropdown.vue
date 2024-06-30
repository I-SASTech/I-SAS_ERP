<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="true" :disabled="disabled" v-bind:placeholder="$t('SizeDropdown.SelectSize')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" >
            <!--<p slot="noResult" class="text-danger"> Oops! No Size found.</p>-->
            
            <template v-slot:noResult>
                <span ></span>
            </template>
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        name: 'sizeMultidropdown',
        props: ["modelValue", 'disabled'],
        mixins: [clickMixin],
        components: {
            Multiselect
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                options: [],
                value: [],
                show: false,
                render: 0,
                loading: false,
            }
        },
        methods: {
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/Product/SizeList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {


                    if (response.data != null) {
                        response.data.results.sizes.forEach(function (cat) {

                            root.options.push({
                                id: cat.id,
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != '' && cat.name != null) ?  cat.name :  cat.nameArabic : (cat.nameArabic != '' && cat.nameArabic != null) ?  cat.nameArabic : cat.name
                            })
                        })
                    }
                }).then(function () {
                    if (root.modelValue != null && root.modelValue != undefined && root.modelValue != '') {
                        root.modelValue.forEach(function (id) {
                            var size = root.options.find(function (x) { return x.id == id; });
                            root.value.push(size);
                        });
                        root.$emit('update:modelValue', root.value);
                    }
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
                    this.value = value;
                    this.$emit('update:modelValue', value);
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