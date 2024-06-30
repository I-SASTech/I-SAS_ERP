<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" v-bind:placeholder="$t('AddDepartment.SelectDepartment')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" >
            <!--<p slot="noResult" class="text-danger"> Oops! No Department found.</p>-->
            <template v-slot:noResult>
            <span  class="btn btn-primary " v-on:click="AddDepartment('Add')">{{ $t('AddDepartment.AddDepartment') }}</span><br />
</template>
        </multiselect>

        <modal :show="show" v-if="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" >{{ $t('AddDepartment.AddDepartment') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div :key="render" class="form-group text-start has-label col-sm-12 " v-bind:class="{'has-danger' : v$.department.code.$error}">
                            <label class=" font-weight-bolder"> {{ $t('AddDepartment.Code') }}:<span class="text-danger"> *</span></label>
                            <input disabled class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.department.code.$model" type="text" />
                            
                        </div>
                        <div v-if="english=='true'" class="form-group text-start has-label col-sm-12 " v-bind:class="{'has-danger' : v$.department.name.$error}">
                            <label class="text  font-weight-bolder"> {{ $t('AddDepartment.NameEnglish')}}: <span class="text-danger"> *</span></label>
                            <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.department.name.$model" type="text" />
                        </div>
                        <div v-if="isOtherLang()" class="form-group text-start has-label col-sm-12 " v-bind:class="{'has-danger' : v$.department.nameArabic.$error}">
                            <label class="text  font-weight-bolder">{{ $t('AddDepartment.NameArabic')}}: <span class="text-danger"> *</span></label>
                            <input class="form-control "  v-bind:class="$i18n.locale == 'en' ||isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.department.nameArabic.$model" type="text" />
                        </div>
                        <div  class="form-group text-start has-label col-sm-12 " >
                            <label class="text  font-weight-bolder">Bank: <span class="text-danger"> *</span></label>
                            <bankdropdown v-model="department.bankId" @update:modelValue="GetBank1Account"  :modelValue="department.bankId"></bankdropdown>
                        </div>

                        <div class="form-group text-start has-label col-sm-12 " v-bind:class="{'has-danger' : v$.department.description.$error}">
                            <label class="text  font-weight-bolder"> {{ $t('AddDepartment.Description') }}: </label>
                            <textarea class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.department.description.$model" type="text" />
                            <span v-if="v$.department.description.$error" class="error">{{ $t('descriptionLength') }}</span>
                        </div>
                    
                    
                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="department.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddDepartment.Active') }} </label>
                        </div>
                    </div>
                   

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveDepartment" v-bind:disabled="v$.department.$invalid" v-if="type!='Edit'">{{ $t('AddDepartment.Save') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveDepartment" v-bind:disabled="v$.department.$invalid" v-if="type=='Edit'">{{ $t('AddDepartment.Update') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddDepartment.Cancel') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>

    </modal>
 </div>
</template>
<script>
    import { requiredIf, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
       import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
        name: 'departmentdropdown',
        props: ["modelValue"],
        mixins: [clickMixin],

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
                department: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    name: '',
                    bankId: '',
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
             department: {
                name: {
                    maxLength: maxLength(50)
                },
                nameArabic: {
                    required: requiredIf(function () {
                            return !this.department.name;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.name == '' || x.name == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(50)
                },
                code: {
                    maxLength: maxLength(30)
                },
                description: {
                    maxLength: maxLength(200)
                }
            }
           }
        },
        methods: {
            GetBank1Account: function (account) {
                debugger; //eslint-disable-line

                this.department.bankId = account.id;
                },
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/EmployeeRegistration/DepartmentList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        response.data.results.departments.forEach(function (cat) {
                            root.options.push({
                                id: cat.id,
                                bankId: cat.bankId,
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != '' && cat.name != null) ? cat.code + ' ' + cat.name : cat.code + ' ' + cat.nameArabic : (cat.nameArabic != '' && cat.nameArabic != null) ? cat.code + ' ' + cat.nameArabic : cat.code + ' ' + cat.name
                            })
                        })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                });
            },
            AddDepartment: function (type) {
                this.v$.$reset();
                this.GetAutoCodeGenerator();
                this.department = {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    name: '',
                    nameArabic: '',
                    bankId: '',
                    description: '',
                    isActive: true
                }

                this.show = !this.show;
                this.type = type;
            },
            close: function () {
                this.show = false;
            },
            GetAutoCodeGenerator: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/EmployeeRegistration/DepartmentCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.department.code = response.data;
                        root.render++;
                    }
                });
            },
            SaveDepartment: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/EmployeeRegistration/SaveDepartment', this.department, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {
                         
                            root.$swal({
                                icon: 'success',
                                title: 'Saved Successfully!',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.options = [];
                            root.getData();
                            root.show = false;
                        }
                        else {
                        
                            root.$swal({
                               title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: "Your Department " + response.data.department.name + " has been updated!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                            root.options = [];
                            root.getData();
                            root.show = false;
                        }
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your Department Name  Already Exist!",
                            type: 'error',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                    }).catch(error => {
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
                    .finally(() => root.loading = false)
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