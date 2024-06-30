<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" v-bind:placeholder="$t('AddAllowance.AllowanceType')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <!--<p slot="noResult" class="text-danger"> Oops! No AllowanceType found.</p>-->
            <template v-slot:no-result>
                <span  class="btn btn-primary " v-on:click="AddAllowanceType('Add')">{{ $t('AllowanceType.AddNew') }}</span>
                <br />
            </template>

        </multiselect>
         <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    import { requiredIf, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        name: 'allowanceTypedropdown',
        props: ["modelValue"],

        components: {
            Multiselect,
            Loading
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
                allowanceType: {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    nameArabic: '',
                    description: '',
                    isActive: true
                },
                render: 0,
                loading: false,
            }
        },
        validations() {
          return{
              allowanceType: {
                name: {
                    maxLength: maxLength(50)
                },
                nameArabic: {
                    required: requiredIf(function () {
                            return !this.allowanceType.name;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.name == '' || x.name == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(50)
                },
              
                description: {
                    maxLength: maxLength(200)
                }
            }
          }
        },
        methods: {
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.options = [];
                this.$https.get('/Payroll/AllowanceTypeList?isDropdown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        
                        response.data.allowanceTypes.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (result.name != '' ?  result.name :  result.nameArabic) : (result.nameArabic != '' ?  result.nameArabic : result.name),
                            })
                        })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                });
            },
            AddAllowanceType: function (type) {
                this.v$.$reset();
                this.allowanceType = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    nameArabic: '',
                    description: '',
                    isActive: true
                }

                this.show = !this.show;
                this.type = type;
            },
            close: function () {
                this.show = false;
            },
            SaveAllowanceType: function () {
                 this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Payroll/SaveAllowanceType', this.allowanceType, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data.isSuccess == true) {
                            root.$swal({
                                icon: 'success',
                                title: 'Saved Successfully!',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.show = false;
                            root.getData();
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your AllowanceType Name  Already Exist!",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
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