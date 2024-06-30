<template>
    <div class="dropdown-menu dropdown-menu-end" v-if="islayout">
        <a v-for="(item,index) in options" v-bind:key="index" @click="selectedValue(item)" class="dropdown-item" href="javascript:void(0)">
            {{item.name}}
        </a>
    </div>

    <div v-else>
        <multiselect v-model="DisplayValue" :options="options" :disabled="disabled" :multiple="ismultiple" v-bind:placeholder="$t('Branches.SelectBranch')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <template v-slot:noResult>
            <p  class="text-danger"> Oops! No Branch found.</p>
      </template>
        </multiselect>

        <addbranches :newbranch="newBranch"
                     :show="show"
                     v-if="show"
                     @close="close()"
                     :type="type" />
    </div>
</template>

<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    
    export default {
        mixins: [clickMixin],
        name: 'branchdropdown',
        props: ["modelValue", 'disabled', 'islayout' , 'ismultiple', 'fromBranch','selectAll'],

        components: {
            Multiselect
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                options: [],
                value: '',
                show: false,
                type: '',
                newBranch: {},
                render: 0,
                loading: false,
            }
        },
        methods: {
            Remove: function () {
            this.value='';

        },
            openmodel: function () {
                this.newBranch = {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    branchName: '',
                    contactNo: '',
                    address: '',
                    city: '',
                    state: '',
                    postalCode: '',
                    country: '',
                }

                this.show = !this.show;
                this.type = "Add";
            },
            getData: function () {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var isLayout = this.islayout == true ? true : false;
                this.$https.get('/Branches/BranchList?isDropdown=true' + '&isLayout=' + isLayout + '&locationId=' + localStorage.getItem('CompanyID'), { headers: {"Authorization": `Bearer ${token}`}
                }).then(function (response) {
                    if (response.data != null) {
                        response.data.results.forEach(function (result) {
                            if (root.fromBranch != undefined && root.fromBranch != "") {
                                if (root.fromBranch != result.id) {
                                    root.options.push({
                                        id: result.id,
                                        mainBranch: result.mainBranch,
                                        name: result.code + ' ' + result.branchName,
                                    })
                                }
                            }
                            else
                            {
                                root.options.push({
                                    id: result.id,
                                    mainBranch: result.mainBranch,
                                    name: result.code + ' ' + result.branchName,
                                })
                            }
                        })
                    }
                }).then(function () {
                    if (root.ismultiple == true) {
                        if (root.modelValue != undefined && root.modelValue != '' && root.modelValue != null) {
                            root.value = root.modelValue;
                        }
                    }
                    else {
                        root.value = root.options.find(function (x) {
                            return x.id == root.modelValue;
                        })
                    }
                    
                });
            },

            close: function () {
                this.show = false;
                this.getData();
            },

            selectedValue: function (value) {
                this.$emit('update:modelValue', value);
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
                    if (this.ismultiple) {
                        this.$emit('update:modelValue', value);
                    }
                    else {
                        this.$emit('update:modelValue', value.id);
                    }                    
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
