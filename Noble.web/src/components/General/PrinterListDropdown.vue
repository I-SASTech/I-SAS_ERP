<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" v-bind:placeholder="$t('PrinterListDropdown.SelectPrinter')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="false">
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
  
   
    export default {
        name: 'sizedropdown',
        props: ["modelValue"],
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

                this.$https
                    .get("/Product/GetPrinterList", {
                        headers: { Authorization: `Bearer ${token}` },
                    }).then(function (response) {


                    if (response.data != null) {
                        response.data.forEach(function (x) {
                            root.options.push({
                                name: x
                            })
                       })
                    }
                    }).then(function () {
                        if (root.modelValue != null) {
                            root.value = root.options.find(function (x) {
                                return x.name == root.modelValue;
                            })
                        }
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
                    this.$emit('update:modelValue', value.name);
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