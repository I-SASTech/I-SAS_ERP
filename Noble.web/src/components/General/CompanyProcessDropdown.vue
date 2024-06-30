<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" v-bind:placeholder="$t('CompanyProcessDropdown.SelectAction')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <!--<p slot="noResult" class="text-danger"> Oops! No Color found.</p>-->
            <template v-slot:noResult>
            <span  class="btn btn-primary " v-on:click="openmodel()">{{ $t('CompanyProcessDropdown.AddProcess') }}</span><br />
       </template>
        </multiselect>
        <add-company-process :process="newProcess"
                             :show="show"
                             v-if="show"
                             @close="IsSave"
                             :type="type" />
    </div>
</template>
<script>
    import { required, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect';
    export default {
        name: 'colordropdown',
        props: ["modelValue",'documenttype'],
        mixins: [clickMixin],
        components: {
            Multiselect
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
                processList: [],
                newProcess: {
                    id: '',
                    processName: '',
                    processNameArabic: '',
                    type: '',
                    status: true
                },
                render: 0,
                loading: false,
                docType:''
            }
        },
        validations() {
          return{
              newProcess: {
                processNameArabic: {
                    required: requiredIf(function () {
                            return !this.newProcess.processName;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.processName == '' || x.processName == null)
                    //         return true;
                    //     return false;
                    // }),
                },
                type: {
                    required
                }
            }
          }
        },
        methods: {
            IsSave: function () {
                this.show = false;
                this.getData(this.docType);
            },
            //GetProcessType: function () {
            //    var root = this;
            //    var token = '';
            //    if (token == '') {
            //        token = localStorage.getItem('token');
            //    }
            //    
            //    root.$https.get('Company/ProcessTypeList', { headers: { "Authorization": `Bearer ${token}` } })
            //        .then(function (response) {
            //            if (response.data != null && response.data != '') {
            //                root.processList = response.data;
            //            }
            //        });
            //},
            getData: function (value) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.options = [];
                this.$https.get('/Company/ProcessList?isDropdown=true' + '&document=' + value, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        response.data.results.forEach(function (cat) {
                            if (cat.processName != 'Add' && cat.processName != 'يضيف' && cat.processName != 'Update' && cat.processName != 'تحديث' && cat.processName != 'Attachment' && cat.processName != 'مرفق' && cat.processName != 'Add Payment' && cat.processName != 'إضافة الدفع') {
                                root.options.push({
                                    id: cat.id,
                                    name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.processName != '' && cat.processName != null) ? cat.processName : cat.processNameArabic : (cat.processNameArabic != '' && cat.processNameArabic != null) ? cat.processNameArabic : cat.processName
                                })
                            }                            
                        })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                });
            },
            openmodel: function () {
                this.newProcess = {
                    id: '00000000-0000-0000-0000-000000000000',
                    processName: '',
                    processNameArabic: '',
                    type: '',
                    status: true
                }
                this.show = !this.show;
                this.type = "Add";
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
            
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.docType = this.documenttype;
            this.getData(this.documenttype);            
        },
    }
</script>