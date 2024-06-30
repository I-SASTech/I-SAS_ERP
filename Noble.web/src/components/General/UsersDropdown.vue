<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false"  :placeholder="$t('UsersDropdown.SelectUser')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" >
            <template v-slot:noResult>
                <p class="text-danger">{{ $t('UsersDropdown.NoUserFound') }}</p>
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
        props: ["modelValue", "isloginhistory", "isTransfer", "isSupervisor", 'alluser', 'cashallocation'],
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
            EmptyRecord: function () {
                
                this.DisplayValue='';

            
            },
            getData: function () {
                var root = this;
                var token = '';
                var alluser = this.alluser == undefined ? false : this.alluser;
                var isHistory = this.isloginhistory == undefined ? false : this.isloginhistory;
                var istransferTerminal = this.isTransfer == undefined ? false : this.isTransfer;
                var isSupervisorTerminal = this.isSupervisor == undefined ? false : this.isSupervisor;
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/account/ForRoleUsersList?isHistory=' + isHistory + '&istransferTerminal=' + istransferTerminal + '&isSupervisorTerminal=' + isSupervisorTerminal + '&alluser=' + alluser, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        if (root.cashallocation) {
                            response.data.forEach(function (result) {
                                if (result.temporaryCashReceiver) {
                                    root.options.push({
                                        id: result.id,
                                        name: result.fullName
                                    })
                                }
                                
                            })
                        }
                        else {
                            response.data.forEach(function (result) {
                                root.options.push({
                                    id: result.id,
                                    name: result.fullName
                                })
                            })
                        }
                        
                    }
                }).then(function () {
                    if (root.modelValue != undefined && root.modelValue != '' && root.modelValue != null) {
                        root.value = root.options.find(function (x) {
                            return x.id == root.modelValue;
                        })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
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
                        if (this.isloginhistory) {
                            this.value = value;
                            this.$emit("update:modelValue", value);
                        }
                        else {

                            if (this.isTransfer) {
                                this.value = value;
                                this.$emit("update:modelValue", value.name);
                            }
                            else if (this.isSupervisor) {
                                this.value = value;
                                this.$emit("update:modelValue", value.name);
                            }
                            else {
                                this.value = value;
                                this.$emit("update:modelValue", value.id);
                            }

                        }
                        
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