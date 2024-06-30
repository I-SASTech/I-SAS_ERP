<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <template v-slot:noResult>
            <p  class="text-danger"> Oops! No Group found.</p>
            </template>
        </multiselect>
       
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import axios from "axios"
    export default {
        name: 'branddropdown',
        props: ["modelValue"],
        mixins: [clickMixin],

        components: {
            Multiselect,
            //
        },
         setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                options: [],
                value: '',
                show: false,
                type: '',
                group: {
                    id: '00000000-0000-0000-0000-000000000000',
                    groupName: '',
                    groupType: '',
                },
                groupTypeList: ['Basic', 'Advance', 'Premium', 'Customize'],
                render: 0,
                loading: false,
            }
        },
        validations() {
           return{
             group: {
                groupName: {
                    required
                },
                groupType: {
                    required
                },
            }
           }
        },
        methods: {
            getData: function () {
                var root = this;
                
                axios.get(root.$PermissionIp +'/NoblePermission/GetNobleGroup').then(function (response) {
                    if (response.data != null) {
                        response.data.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                name: result.groupName + '-' + result.groupType,
                            })
                        })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                    root.$emit('update:modelValue', root.value.id);
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
                    if (this.value == null) {
                        this.$emit('update:modelValue', '');
                    }
                    else {
                        this.$emit('update:modelValue', value.id);
                    }

                }
            }
        },
        mounted: function () {

            this.getData();
        },
    }
</script>