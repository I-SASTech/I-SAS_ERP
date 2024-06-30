<template>
    <el-date-picker v-model="inputValue"
                    type="month"
                    style="width: 100%; "
                    v-bind:placeholder="$t('Sale.PickMonth')" />
</template>
<script>
    //import moment from "moment";
    export default {
        props: ["modelValue", "isDisabled"],
        name: 'DatePicker',
        data: function () {
            return {
                inputValue: "",
            };
        },
        methods: {},
        mounted: function () {
            this.inputValue = this.modelValue;
        },
        updated: function () {
            this.$emit("update:modelValue", this.inputValue);
        },
    };
</script>
