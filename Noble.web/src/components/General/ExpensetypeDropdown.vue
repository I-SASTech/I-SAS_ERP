<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" v-bind:placeholder="$t('ExpensetypeDropdown.SelectColor')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <!--<span slot="noResult" class="btn btn-primary " v-on:click="AddColor('Add')">{{ $t('ExpensetypeDropdown.AddProductColor') }}</span><br />-->
        </multiselect>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    import Multiselect from 'vue-multiselect'
    export default {
        name: 'colordropdown',
        props: ["modelValue"],
        components: {
            Multiselect
        },
        mixins: [clickMixin],

        data: function () {
            return {
                arabic: '',
                english: '',
                options: [],
                value: '',            
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
                this.$https.get('/Accounting/ExpenseTypeList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        
                        response.data.results.expenseTypeLook.forEach(function (cat) {
                            root.options.push({
                                id: cat.id,
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.expenseTypeName != '' && cat.expenseTypeName != null) ? cat.expenseTypeName : cat.expenseTypeArabic : (cat.expenseTypeArabic != '' && cat.expenseTypeArabic != null) ? cat.expenseTypeArabic : cat.expenseTypeName
                            })
                        })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
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