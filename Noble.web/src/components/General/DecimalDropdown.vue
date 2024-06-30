<template>
    <div >
        <input type="text" v-bind:disabled="disable? true: false" class="form-control" v-model="displayValue" @blur="isInputActive = false" @focus="isInputActive=true"  @click="$event.target.select()" />
    </div>
   
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        mixins: [clickMixin],

        name: 'DecimalDropdown',
        props: ["modelValue", 'isVAT', 'disable'],
        data: function () {
            return {
                isInputActive: false,
                currency: '',
            }
        },     

        computed: {
            displayValue: {
                get: function () {
                    
                    if (this.isInputActive) {

                        // Cursor is inside the input field. unformat display value for user
                        return this.modelValue.toString()
                    } else {
                        
                        // User is not modifying now. Format display value for user interface
                        if (this.isVAT) {

                            return (this.modelValue == undefined ? '0' : this.modelValue) + "%"
                        }
                        else {
                            return this.currency + " " + parseFloat(this.modelValue).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
                        }
                    }
                },
                set: function (modifiedValue) {
                    if(/^[0-9\u0660-\u0669]+$/.test(modifiedValue)==true)
                {
                    modifiedValue = modifiedValue.replace(/[٠-٩]/g, d => "٠١٢٣٤٥٦٧٨٩".indexOf(d)).replace(/[۰-۹]/g, d => "۰۱۲۳۴۵۶۷۸۹".indexOf(d));
                 
                }
                    // Recalculate value after ignoring "$" and "," in user input
                    let newValue = parseFloat(modifiedValue.replace(/[^\d\.]/g, "")); // eslint-disable-line
                    // Ensure that it is not NaN
                    if (isNaN(newValue)) {
                        newValue = 0
                    }
                    // Note: we cannot set this.value as it is a "prop". It needs to be passed to parent component
                    // $emit the event so that parent component gets it
                    this.$emit('update:modelValue', newValue)
                }
            }
        },
        mounted: function () {           
            this.currency = localStorage.getItem('currency');
        }
    }
</script>