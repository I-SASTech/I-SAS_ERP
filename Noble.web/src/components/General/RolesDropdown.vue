<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false"  :placeholder="$t('RolesDropdown.SelectUser')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <template v-slot:noResult>
            <p  class="text-danger">  {{ $t('RolesDropdown.NoUserFound') }}</p>
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
        props: ["modelValue"],
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
                
                root.$https.get('/Company/NobleRolesList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        
                        response.data.nobleRoleModel.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                name: result.name == 'Sales Man' ? 'Salesman' : result.name
                            })
                       })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                    // if (root.modelValue != undefined) {
                    //     root.$parent.GetTerminalData()
                    // }
                });
            },
            GetNameOfSelected: function () {
                
                if (this.value.length > 0)
                {
                    return this.value[0].name;
                }
                else
                {
                    return this.value.name;
                }
                    
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
                    this.value = value;
                    this.$emit('update:modelValue', value);
                }
            }
        },
        mounted: function () {
            this.getData();
        }
    }
</script>