<template>
    <div v-if="isDisable">
        <multiselect v-model="DisplayValue" disabled :options="options" :multiple="false" track-by="name" :clear-on-select="false" :show-labels="false" v-bind:placeholder="$t('TerminalDropDown.SelectOption')" label="name" :preselect-first="true">
            <template v-slot:noResult>
                <p  class="text-danger">{{$t('TerminalDropDown.NoTerminalFound')}}</p>
            </template>
        </multiselect>

    </div>
    <div v-else>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" track-by="name" :clear-on-select="false" :show-labels="false" v-bind:placeholder="$t('TerminalDropDown.SelectOption')" label="name" :preselect-first="true" >
            <template v-slot:noResult>
                <p  class="text-danger">{{$t('TerminalDropDown.NoTerminalFound')}}</p>
            </template>
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        name: 'Terminaldropdown',
        props: ["modelValue", 'isAllSelected', 'isDayStart', 'isDisable', 'isSelect', 'terminalType','allShow', 'terminalUserType'],
        mixins: [clickMixin],

        components: {
            Multiselect,

        },
        data: function () {
            return {
                options: [],
                value: [],
                show: false,
                type: '',
                render: 0,
                isDayStarts: false
            }
        },
        methods: {
            Remove: function () {
            this.value='';

        },

            getData: function () {
                var root = this;
                 if(root.terminalType == null || root.terminalType == '')
                 {
                    return
                 }
                 //eslint-disable-line
                
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.isDayStarts == undefined)
                    this.isDayStarts = false

                var IsSelected = false;
                if (this.isSelect) {
                    IsSelected = true;
                }
                var allShown = false;
                if (this.allShow) {
                    allShown = true;
                }
                                
                var url = ""
                if (root.terminalType == 'All') {
                     url = '/Company/TerminalList?isActive=false' + '&IsSelected=true' + '&allShown=' + allShown
                }
                else {
                    url = '/Company/TerminalList?isActive=true' + '&IsSelected=' + IsSelected + '&terminalType=' + root.terminalType + '&allShown=' + allShown
                }                

                this.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        response.data.terminals.forEach(function (cat) {                            
                            if(root.terminalUserType != 'Both')
                            {
                                if (root.terminalUserType == cat.terminalUserType) {
                                    root.options.push({
                                        id: cat.id,
                                        name: cat.code
                                    });
                                }                                
                            }
                            else{
                                root.options.push({
                                    id: cat.id,
                                    name: cat.code
                                });
                            }
                            
                        })
                    }


                }).then(function () {
                    
                    if (root.modelValue != undefined && root.modelValue != '') {
                        root.options.forEach(function (x) {
                            if (x.id === root.modelValue) {
                                

                                root.value.push({
                                    id: x.id,
                                    name: x.name
                                });
                                root.$emit('update:modelValue', x.id);
                            }
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
                    if (value == null || value == '' || value == undefined) {
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
            this.isDayStarts = this.isDayStart
        },
    }
</script>