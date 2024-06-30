<template>
    <div v-bind:class="{'has-danger' : v$.discounts.$error}">
        <input type="text" v-model="v$.discounts.$model" v-if="disable != undefined" readonly v-on:focusout="onFocusOut" v-on:focusin="onFocusIn" @blur="isInputActive = false" @focus="isInputActive = true" @click="$event.target.select()" class="form-control form-control-lg" />
        <input type="text" v-model="v$.discounts.$model" v-else v-on:focusout="onFocusOut" v-on:focusin="onFocusIn" @blur="isInputActive = false" @focus="isInputActive = true" @click="$event.target.select()" class="form-control form-control-lg" />
        <span v-if="v$.discounts.$error" class="error">
            {{$t('InputDropdown.NumberAllow')}}
        </span>
    </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        props: ["modelValue", "disable"],
        mixins: [clickMixin],

        data: function () {
            return {
                isInputActive: false,
                discounts: 0.0
            }
        },
        validations() {
            return{
                discounts: {
                percentageValue(discounts) {
                    return /^(0*100{1,1}\.?((?<=\.)0*)?%?$)|(^0*\d{0,2}\.?((?<=\.)\d*)?%?)$/.test(discounts);
                }
            },
            }
        },
        methods: {
            onFocusOut: function () {

                var val = 0.0;
                if (this.discounts != null && this.discounts != "") {
                    val = this.discounts;
                }
                this.discounts = this.discounts + '%';

                this.$emit('update:modelValue', val);
            },
            onFocusIn: function () {
                if (this.discounts == '0') {
                    this.discounts = ''
                }
                else {
                    var len = this.discounts.length;
                    this.discounts = this.discounts.slice(0, len - 1);
                }
            },

        },
        mounted: function () {
            this.discounts = this.modelValue;
        }
    }
</script>
