<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" v-bind:placeholder="$t('AddSalaryTemplate.SelectTemplate')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <template v-slot:noResult>
                <p  class="text-danger"> {{ $t('AddSalaryTemplate.NoAllowanceType') }} </p>

            </template>

        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: 'AddEmployeeSalary',
        props: ["modelValue"],

        components: {
            Multiselect
        },
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
                
                this.$https.get('/Payroll/SalaryTemplateList?isDropdown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        
                        root.options = [];
                        response.data.results.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                name: result.structureName,
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