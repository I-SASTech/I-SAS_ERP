<template>
    <div v-if="isDisabled">
        <!--Disable Date With 00 00 Date Fomrat-->
        <el-date-picker v-model="inputValue" type="date" v-bind:placeholder="$t('DatePicker.PickDate')"
                        style="width: 100%; " :picker-options="pickerOptions" disabled>
        </el-date-picker>
    </div>
    <div v-else-if="isDisable">
        <!--Disable Date-->
        <el-date-picker v-model="inputValue" type="date" disabled v-bind:placeholder="$t('DatePicker.PickDate')"
                        style="width: 100%; " :picker-options="pickerOptions">
        </el-date-picker>
    </div>
    <div v-else-if="period">
        <!--Disable Date-->
        <el-date-picker v-model="inputValue" type="date" v-bind:placeholder="$t('DatePicker.PickDate')"
                        style="width: 100%;  " :picker-options="pickerOptions1">
        </el-date-picker>
    </div>
    <div v-else>
        <el-date-picker v-model="inputValue" type="date" v-bind:placeholder="$t('DatePicker.PickDate')"
                        style="width: 100%; " :picker-options="pickerOptions">
        </el-date-picker>
    </div>
</template>
<script>
    import moment from "moment";
    //import "element-ui/lib/theme-chalk/index.css";
    export default {
        props: ["modelValue", "dropdowndatecss", "isDisabled", "isDisable", 'period'],
        name: 'DatePicker',
        data: function () {
            return {
                dropdownDatecss: "",
                inputValue: "",
                editField: "",
                pickerOptions: {
                    shortcuts: [
                        {
                            text: this.$t("DateFilter.Today"),
                            onClick(picker) {
                                picker.$emit("pick", new Date());
                            },
                        },
                        {
                            text: this.$t("DateFilter.Yesterday"),
                            onClick(picker) {
                                const date = new Date();
                                date.setTime(date.getTime() - 3600 * 1000 * 24);
                                picker.$emit("pick", date);
                            },
                        },
                        {
                            text: this.$t("DateFilter.Aweekago"),
                            onClick(picker) {
                                const date = new Date();
                                date.setTime(date.getTime() - 3600 * 1000 * 24 * 7);
                                picker.$emit("pick", date);
                            },
                        },
                        {
                            text: this.$t("DateFilter.AMonthago"),
                            onClick(picker) {
                                const date = new Date();

                                var d = date.getDate();
                                date.setFullYear(date.getFullYear() + -1);
                                if (date.getDate() != d) {
                                    date.setDate(0);
                                }
                                picker.$emit("pick", date);
                            },
                        },
                        {
                            text: this.$t("DateFilter.Quarterago"),

                            onClick(picker) {
                                const date = new Date();
                                var d = date.getDate();
                                date.setMonth(date.getMonth() + -4);
                                if (date.getDate() != d) {
                                    date.setDate(0);
                                }
                                picker.$emit("pick", date);
                            },
                        },
                        {
                            text: this.$t("DateFilter.HalfYear"),
                            onClick(picker) {
                                const date = new Date();
                                var d = date.getDate();
                                date.setMonth(date.getMonth() + -6);
                                if (date.getDate() != d) {
                                    date.setDate(0);
                                }
                                picker.$emit("pick", date);
                            },
                        },
                        {
                            text: this.$t("DateFilter.AYearago"),
                            onClick(picker) {
                                const date = new Date();
                                var d = date.getDate();
                                date.setFullYear(date.getFullYear() + -1);
                                if (date.getDate() != d) {
                                    date.setDate(0);
                                }
                                picker.$emit("pick", date);
                            },
                        },
                        {
                            text: this.$t("End of month"),
                            onClick(picker) {
                                const date = new Date();
                                date.setMonth(date.getMonth() + 1);
                                date.setDate(0);

                                picker.$emit("pick", date);
                            },
                        },
                        {
                            text: this.$t("End of last month"),
                            onClick(picker) {
                                const date = new Date();
                                date.setDate(0);

                                picker.$emit('pick', date);
                            }
                        },
                    ],
                },
                pickerOptions1: {
                    shortcuts: [
                        {
                            text: this.$t("DateFilter.Today"),
                            onClick(picker) {
                                picker.$emit("pick", new Date());
                            },
                        },
                        {
                            text: this.$t("End of month"),
                            onClick(picker) {
                                const date = new Date();
                                date.setMonth(date.getMonth() + 1);
                                date.setDate(0);

                                picker.$emit("pick", date);
                            },
                        },
                        {
                            text: this.$t("End of last month"),
                            onClick(picker) {
                                const date = new Date();
                                date.setDate(0);

                                picker.$emit('pick', date);
                            }
                        },
                        {
                            text: this.$t("End of last Quarter"),
                            onClick(picker) {
                                const date = new Date();
                                const currentMonth = date.getMonth();
                                const quarterMonth = Math.floor(currentMonth / 3) * 3;
                                date.setMonth(quarterMonth);
                                date.setDate(0);

                                picker.$emit('pick', date);
                            }
                        },
                        {
                            text: this.$t("End of last Financial year"),
                            onClick(picker) {
                                const date = new Date();
                                date.setFullYear(date.getFullYear() - 1);
                                date.setMonth(11); // December (month index starts from 0)
                                date.setDate(31);

                                picker.$emit('pick', date);
                            }
                        },
                    ],
                },
            };
        },
        methods: {},
        mounted: function () {
            this.dropdownDatecss = this.dropdowndatecss;
            this.inputValue = this.modelValue;
        },
        updated: function () {
            var input = "";
            if (this.inputValue != "" && this.inputValue != null && this.inputValue != undefined)
            {
                input = moment(String(this.inputValue)).format("DD MMM YYYY");
            }
            this.$emit("update:modelValue", input);
        },
    };
</script>
