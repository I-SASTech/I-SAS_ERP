<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :disabled="disabled" :multiple="true" v-bind:placeholder="$t('ColorDropdown.SelectColor')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">


        </multiselect>

    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
   
    export default {
        name: 'Weekdropdown',
        props: ["modelValue",],
        mixins: [clickMixin],

        components: {
            Multiselect,
            
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                options: [],
                value: '',
                show: false,
                type: '',
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
                this.$https.get('/Payroll/WeekDaysList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        response.data.results.forEach(function (cat) {
                          
                          
                                root.options.push({
                                    id: cat.id,
                                    name: cat.name,
                                    nameArabic: cat.nameArabic,
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