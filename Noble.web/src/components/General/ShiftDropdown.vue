<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" v-bind:placeholder="$t('AddDepartment.SelectDepartment')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" >
<template v-slot:noResult>
    <p class="text-danger"> Oops! No Shift found.</p>
    
</template>
        </multiselect>

 </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    export default {
        name: 'shiftdropdown',
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
            }
        },
        methods: {
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Hr/ShiftList?isDropdown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data != null) {
                        response.data.results.forEach(function (cat) {
                            root.options.push({
                                id: cat.id,
                                name: cat.shiftName
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
                    if (value == null) {
                        this.$emit('update:modelValue', '');
                    }
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