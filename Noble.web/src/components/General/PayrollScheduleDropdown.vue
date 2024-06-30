<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" v-bind:placeholder="$t('AddPayrollSchedule.SelectPayrollSchedule')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <template v-slot:noResult>
            <p  class="text-danger">{{ $t('AddPayrollSchedule.NoAllowanceFound') }}</p>
        </template>
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";

    export default {
        mixins: [clickMixin],
        name: 'payPeriod',
        props: ["modelValue", "salaryType","isEmployee"],

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
                
                var url = '';
                if (this.salaryType == undefined) {
                    url = '/Payroll/PayrollScheduleList?isDropdown=true' + '&salaryType=' + '';
                }
                else {
                    url = '/Payroll/PayrollScheduleList?isDropdown=true' + '&salaryType=' + this.salaryType;
                }

                this.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        
                        root.options = [];
                        response.data.results.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                isHourlyPay: result.isHourlyPay,
                                name: root.isEmployee ? result.name : result.name + ' - (' + root.getDate(result.payPeriodStartDate) + ' - ' + root.getDate(result.payPeriodEndDate) +')',
                            })
                        })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                });
            },
            getDate: function (x) {
                return moment(x).format("MM-DD-YYYY");
            }
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