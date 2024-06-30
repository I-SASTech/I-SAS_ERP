<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :disabled="disabled" :multiple="false" v-bind:placeholder="$t('ExpensetypeDropdown.SelectColor')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" >
            <template v-slot:noResult>
                <p class="text-danger">{{ $t('ColorDropdown.NoColorFound') }} </p>
            </template>
        </multiselect>

    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    export default {
        name: 'colordropdown',
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
                value: '',
                show: false,
                type: '',
                color: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    name: '',
                    nameArabic: '',
                    description: '',
                    isActive: true
                },
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
                this.$https.get('/Product/WarrantyTypeList?isDropdown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        response.data.results.forEach(function (cat) {
                            root.options.push({
                                id: cat.id,
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != '' && cat.name != null) ? cat.name : cat.nameArabic : (cat.nameArabic != '' && cat.nameArabic != null) ? cat.nameArabic : cat.name
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
                    if (value != undefined) {
                        this.$emit('update:modelValue', value.id);
                    }
                    else {
                        this.$emit('update:modelValue', null);
                    }
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