<template>
    <div>
        <link rel="stylesheet" href="https://unpkg.com/vue-multiselect@2.1.0/dist/vue-multiselect.min.css">    
        <multiselect v-model="value" :options="options" :show-labels="false" placeholder="Select Role">
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'

    export default {
        components: {
            Multiselect
        },
        data() {
            return {
                value: '',
                options: ['Admin','User']
            }
        }
    }
</script>
<style scoped>
    .multiselect__option--group {
        font-weight: bold !important;
        text-align: left !important;
        background: none !important;
        margin-left: 5px !important;
    }

    .multiselect__option--highlight {
        background: lightgray !important;
        outline: none !important;
        color: #000 !important;
    }

    .accountDropdown .multiselect__option {
        margin-left: 30px;
    }

    .multiselect__option--selected.multiselect__option--highlight {
        background: lightgray !important;
        color: #000 !important;
    }

    .multiselect, .multiselect__input, .multiselect__single {
        font-size: 15px !important;
        font-family: "Montserrat", "Helvetica Neue", Arial, sans-serif !important;
    }
</style>


