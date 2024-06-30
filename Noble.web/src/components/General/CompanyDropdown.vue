<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">

        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        name: 'companydropdown',
        props: ["modelValue"],
        mixins: [clickMixin],

        components: {
            Multiselect,

        },
        data: function () {
            return {
                options: [],
                value: ''
            }
        },
        methods: {
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/Company/List', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {


                    if (response.data != null) {
                        response.data.forEach(function (cat) {

                            root.options.push({
                                id: cat.id,
                                name: (cat.clientNo == null ? "" : cat.clientNo)+" :"+cat.nameEnglish + " :" + cat.vatRegistrationNo
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
            this.getData();
        },
    }
</script>