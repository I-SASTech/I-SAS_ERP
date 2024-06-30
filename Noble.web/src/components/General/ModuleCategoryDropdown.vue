<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" :placeholder="$t('ModuleCategoryDropdown.SelectPermissions')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <template v-slot:noResult>
            <p  class="text-danger">{{ $t('ModuleCategoryDropdown.NoPermission') }} </p>
        </template>
            <!-- <span slot="noResult" class="btn btn-primary " v-on:click="AddRole('Add')">Create new Role</span><br /> -->
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        mixins: [clickMixin],
        name: 'userRolesdropdown',
        props: ["modelValue", "id"],
        components: {
            Multiselect
        },
        data: function () {
            return {
                options: [],
                value: '',
                show: false,
                type: '',
                render: 0
            }
        },
        methods: {
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                root.$https.get('/Company/GetRightsByModuleName?moduleId=' + this.id + '&isNobel=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.length > 0) {
                        response.data.forEach(function (result) {
                            root.options.push({
                                name: result.permissionName
                            })
                        })
                    }
                });
            }
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
                    if(value != null)
                    {
                        this.value = value;
                        this.$emit("update:modelValue", value.name);
                    }
                    else{
                        this.value = value;
                        this.$emit("update:modelValue", null);
                    }
                }
            }
        },
        mounted: function () {
            this.getData();
        }
    }
</script>