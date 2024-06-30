<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" placeholder="Select Year" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        name: 'Yeardropdown',
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
                show: false,
                type: '',
                render: 0,
                loading: false,
            }
        },
        methods: {
            EmptyRecord: function () {
                
                this.DisplayValue='';

            
            },
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.get('/Company/GetYear', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    
                    if (response.data != null) {
                    response.data.forEach(function (cat) {
                        var splitmodelValue = cat.name.split('-');
                        var result = splitmodelValue[0];
                        root.options.push({
                            id: cat.id,
                            name: result
                        })
                    })
                }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                });
            },
            close: function () {
                this.show = false;
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