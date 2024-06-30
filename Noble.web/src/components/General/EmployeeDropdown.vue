<template>
    <div>
        <multiselect v-model="DisplayValue" v-if="disable"  :disabled="disable" :options="options" :multiple="multipleOption" :close-on-select="!multipleOption" :hide-selected="multipleOption" track-by="name" :clear-on-select="false" :show-labels="false" label="name" v-bind:placeholder="$t('EmployeeDropdown.SelectEmployee')" :preselect-first="true">
            <template v-slot:noResult>
            <p  class="text-danger"> {{ $t('EmployeeDropdown.OopsNoEmployeefound') }}</p>
            <span  class="btn btn-primary " v-on:click="AddEmployee('Add')">{{ $t('EmployeeDropdown.CreateQuickEmployee') }}</span><br />
       </template>
        </multiselect>
        <multiselect v-else v-model="DisplayValue" :options="options" :multiple="multipleOption" :close-on-select="!multipleOption" :hide-selected="multipleOption" track-by="name" :clear-on-select="false" :show-labels="false" label="name" v-bind:placeholder="$t('EmployeeDropdown.SelectEmployee')" :preselect-first="true">
            <template v-slot:noResult>
            <p  class="text-danger"> {{ $t('EmployeeDropdown.OopsNoEmployeefound') }}</p>
            <span  class="btn btn-primary " v-if="isValid('CanAddEmployeeReg') || isValid('CanEditEmployeeReg') " v-on:click="AddEmployee('Add')">{{ $t('EmployeeDropdown.CreateQuickEmployee') }}</span><br />
        </template>
        </multiselect>
        <modal :show="show" :modalLarge="true" v-if="show">

            <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="tab-content" id="nav-tabContent">
                            <div class="modal-header" v-if="type=='Edit'">

                                <h5 class="modal-title" id="myModalLabel"> {{ $t('EmployeeDropdown.UpdateEmployee') }}</h5>

                            </div>
                            <div class="modal-header" v-else>

                                <h5 class="modal-title" id="myModalLabel"> {{ $t('EmployeeDropdown.AddEmployee') }}</h5>

                            </div>
                            <div class="">
                                <div class="card-body">
                                    <div class="row ">
                                        <div :key="render" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.employee.code.$error}">
                                            <label class="text  font-weight-bolder"> {{ $t('EmployeeDropdown.EmployeeCode') }}: <span class="text-danger"> *</span></label>
                                            <input disabled class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.code.$model" type="text" />
                                            <span v-if="v$.employee.code.$error" class="error">

                                            </span>
                                        </div>


                                        <div v-if="english=='true'" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.employee.englishName.$error}">
                                            <label class="text  font-weight-bolder"> {{$englishLanguage($t('EmployeeDropdown.EmployeeName(English)'))  }}: <span class="text-danger"> *</span> </label>
                                            <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.englishName.$model" type="text" />
                                            <span v-if="v$.employee.englishName.$error" class="error text-danger">
                                                <span v-if="!v$.employee.englishName.required">{{ $t('EmployeeDropdown.EngValidation') }}</span>
                                                <span v-if="!v$.employee.englishName.maxLength">{{ $t('EmployeeDropdown.EngMax') }}</span>

                                            </span>
                                        </div>

                                        <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.employee.arabicName.$error}">
                                            <label class="text  font-weight-bolder"> {{$arabicLanguage($t('EmployeeDropdown.EmployeeName(Arabic)'))  }}: <span class="text-danger"> *</span> </label>
                                            <input class="form-control "  v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"  v-model="v$.employee.arabicName.$model" type="text" />
                                            <span v-if="v$.employee.arabicName.$error" class="error text-danger arabicLanguage">
                                                <span class="arabicLanguage" v-if="!v$.employee.arabicName.required">{{ $t('EmployeeDropdown.ArValidation') }}</span>
                                                <span class="arabicLanguage" v-if="!v$.employee.arabicName.maxLength">{{ $t('EmployeeDropdown.ArMax') }}</span>
                                            </span>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('EmployeeDropdown.RegistrationDate') }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.employee.registrationDate.$error}">
                                                <datepicker v-model="v$.employee.registrationDate.$model" :key="daterander"></datepicker>
                                                <span v-if="v$.employee.registrationDate.$error" class="error text-danger">
                                                    <span v-if="!v$.employee.englishName.required">{{ $t('EmployeeDropdown.RegistrationValidation') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('EmployeeDropdown.EmGender') }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.employee.gender.$error}">
                                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight())" v-model="v$.employee.gender.$model" :options="['Male', 'Fe-Male', 'Other']" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :show-labels="false" :placeholder="$t('EmployeeDropdown.SelectGender')">
                                                </multiselect>
                                                <multiselect v-else v-model="v$.employee.gender.$model" :options="['ذكر', 'أنثى', 'آخر']" :show-labels="false" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :placeholder="$t('EmployeeDropdown.SelectGender')">
                                                </multiselect>
                                                <span v-if="v$.employee.gender.$error" class="error text-danger">
                                                    <span v-if="!v$.employee.gender.required">{{ $t('EmployeeDropdown.GValidation') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('EmployeeDropdown.IDNumber') }}:<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.employee.idNumber.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" type="number" v-model="v$.employee.idNumber.$model" />
                                                <span v-if="v$.employee.idNumber.$error" class="error text-danger">
                                                    <span v-if="!v$.employee.idNumber.required">{{ $t('EmployeeDropdown.IDRequired') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.EmployeeType') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.employeeType.$error}">
                                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight())" v-model="v$.employee.employeeType.$model" :options="['Manager', 'Contractor', 'Supervisor','Admin','Labour']" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :show-labels="false" :placeholder="$t('AddEmployeeRegistration.SelectEmployeeType')">
                                                </multiselect>
                                                <multiselect v-else v-model="v$.employee.employeeType.$model" :options="['مدير', 'مقاول', 'مشرف','مشرف','تعب']" :show-labels="false" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :placeholder="$t('AddEmployeeRegistration.SelectEmployeeType')">
                                                </multiselect>
                                                <span v-if="v$.employee.employeeType.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>

                                        <div class="col-sm-6" v-bind:class="{'has-danger' : v$.employee.email.$error}">
                                            <label class="text  font-weight-bolder"> {{ $t('EmployeeDropdown.Email') }}:<span class="text-danger"> *</span> </label>
                                            <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" @blur="EmailDuplicate(employee.email)" v-model="v$.employee.email.$model" type="text" />
                                            <span v-if="v$.employee.email.$error" class="error text-danger arabicLanguage">
                                                <span class="arabicLanguage" v-if="!v$.employee.email.required">{{ $t('EmployeeDropdown.EmailRequired') }}</span>
                                                <span class="arabicLanguage" v-if="!v$.employee.email.email">{{ $t('EmployeeDropdown.EmailInvalid') }}</span>
                                            </span>
                                            <span class="arabicLanguage text-danger" v-if="emailExist">{{ $t('EmployeeDropdown.EmailExist') }}</span>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer justify-content-right" v-if="type=='Edit'">

                                <button type="button" class="btn btn-primary  " v-on:click="SaveEmployee" v-bind:disabled="v$.employee.$invalid"> {{ $t('EmployeeDropdown.btnUpdate') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('EmployeeDropdown.btnClear') }}</button>

                            </div>
                            <div class="modal-footer justify-content-right" v-else>

                                <button type="button" class="btn btn-primary  " v-on:click="SaveEmployee" v-bind:disabled="v$.employee.$invalid"> {{ $t('EmployeeDropdown.btnSave') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('EmployeeDropdown.btnClear') }}</button>

                            </div>
                        </div>
                    </div>
                </div>
                  <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            </div>

        </modal>

    </div>
</template>
<script>
  import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    
    import Multiselect from 'vue-multiselect';
    import moment from 'moment';
    import { required, maxLength, email, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        name: 'EmployeeDropdown',
        props: ["modelValue", "isSignup", 'temporaryCashRequest', 'temporaryCashAllocation','temporaryCashReceiver','isMultiple',"employeeType",'disable', 'showValues','employeeList'],

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
                 loading: false,
                arabic: '',
                english: '',
                options: [],
                emailExist: false,
                multipleOption: false,
                value: '',
                daterander: 0,
                render: 0,
                show: false,
                IsDropDown: false,
                IsSignup: false,
                employee: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    registrationDate: '',
                    englishName: '',
                    arabicName: '',
                    employeeType: '',
                    gender: '',
                    idNumber: '',
                    email: ''
                },
                type: '',
            }
        },
        validations() {
            return{
                employee: {
                code: { required },
                email: {
                    required,
                    email,

                },
                registrationDate: { required },
                employeeType: {  },
                englishName: {
                    maxLength: maxLength(30)
                },
                arabicName: {
                    required: requiredIf(function () {
                            return !this.employee.englishName;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.englishName == '' || x.englishName == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(40)
                },
                gender: { required },
                idNumber: { required },
            }
            }
        },
        methods: {
            EmailDuplicate: function (x) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/account/DuplicateEmail?email=' + x, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data == true) {
                            root.emailExist = true;
                        }
                        else {
                            root.emailExist = false;
                        }


                    })
            },
            AddEmployee: function (type) {
                this.v$.$reset();
                this.employee = {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    registrationDate: '',
                    englishName: '',
                    arabicName: '',
                    gender: '',
                    idNumber: '',
                    email: '',
                }
                this.GetAutoCodeGenerator();

                this.employee.registrationDate = moment().format('llll');
                this.daterander++;
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
                root.$https
                    .get('/EmployeeRegistration/EmployeeCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.employee.code = response.data;
                        }
                    });
            },
            SaveEmployee: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.post('/EmployeeRegistration/SaveEmployee', this.employee, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess == true) {

                        if (root.type != "Edit") {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: "Your Employee has been created!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                            root.close();

                            root.options = [];
                            root.getData();
                        }

                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your Employee Name  Already Exist!",
                            type: 'error',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 800,
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
                });
            },
            EmptyRecord: function () {
                
                this.DisplayValue='';

            
            },
            GetSalaryOfSelected: function () {
                

                if (this.value.length > 0)
                    return this.value[0].employeeSalary;
                else
                    return this.value.employeeSalary;
            },
            GetName: function () {
                

                if (this.value.length > 0)
                    return this.value[0];
                else
                    return this.value;
            },

            getData: function () {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                var employeeType = '';
                if (this.employeeType == undefined) {
                    employeeType = 'Default'

                }
                else {
                    employeeType = this.employeeType
                }

                var url = '';
                if (this.isSignup) {
                    url = '/EmployeeRegistration/EmployeeList?isSignup=' + this.isSignup;
                }
                else {
                    url = '/EmployeeRegistration/EmployeeList?IsDropDown=true' + '&employeeType=' + employeeType;
                }
                this.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        if (root.temporaryCashAllocation) {
                            response.data.results.forEach(function (result) {
                                if (result.temporaryCashIssuer) {
                                    root.options.push({
                                        id: result.id,
                                        employeeSalary: result.employeeSalaries == null || result.employeeSalaries == undefined || result.employeeSalaries.length == 0 ? null : result.employeeSalaries[0].baseSalary,
                                        name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (result.englishName != '' && result.englishName != null) ? result.code + ' ' + result.englishName : result.code + ' ' + result.arabicName : (result.arabicName != '' && result.arabicName != null) ? result.code + ' ' + result.arabicName : result.code + ' ' + result.englishName
                                    });
                                }
                                
                            })
                            
                        }
                        else if (root.temporaryCashRequest) {
                            response.data.results.forEach(function (result) {
                                if (result.temporaryCashRequester) {
                                    root.options.push({
                                        id: result.id,
                                        employeeSalary: result.employeeSalaries == null || result.employeeSalaries == undefined || result.employeeSalaries.length == 0 ? null : result.employeeSalaries[0].baseSalary,
                                        name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (result.englishName != '' && result.englishName != null) ? result.code + ' ' + result.englishName : result.code + ' ' + result.arabicName : (result.arabicName != '' && result.arabicName != null) ? result.code + ' ' + result.arabicName : result.code + ' ' + result.englishName
                                    })
                                }

                            });
                            root.getCashRequestUserData();
                        }
                        else if (root.temporaryCashReceiver) {
                            response.data.results.forEach(function (result) {
                                if (result.temporaryCashReceiver) {
                                    root.options.push({
                                        id: result.id,
                                        employeeSalary: result.employeeSalaries == null || result.employeeSalaries == undefined || result.employeeSalaries.length == 0 ? null : result.employeeSalaries[0].baseSalary,
                                        name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (result.englishName != '' && result.englishName != null) ? result.code + ' ' + result.englishName : result.code + ' ' + result.arabicName : (result.arabicName != '' && result.arabicName != null) ? result.code + ' ' + result.arabicName : result.code + ' ' + result.englishName
                                    })
                                }

                            });
                            root.getCashRequestUserData();
                        }
                        else {
                            response.data.results.forEach(function (result) {
                                root.options.push({
                                    id: result.id,
                                    employeeSalary: result.employeeSalaries == null || result.employeeSalaries == undefined || result.employeeSalaries.length == 0 ? null : result.employeeSalaries[0].baseSalary,
                                    name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (result.englishName != '' && result.englishName != null) ? result.code + ' ' + result.englishName : result.code + ' ' + result.arabicName : (result.arabicName != '' && result.arabicName != null) ? result.code + ' ' + result.arabicName : result.code + ' ' + result.englishName
                                })
                            });
                            root.getCashRequestUserData();
                        }
                        
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                });
            },

            getCashRequestUserData: function () {
                var root = this;


                var url = url = '/EmployeeRegistration/TemporaryCashUserList';

                this.$https.get(url, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } }).then(function (response) {

                    if (response.data != null) {
                        response.data.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                employeeSalary: 0,
                                name: result.name
                            })
                        })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
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
                    if (this.isMultiple) {
                        this.$emit('update:modelValue', value == null ? null : value);
                    }
                    else {
                        this.$emit('update:modelValue', value == null ? null : value.id,value);
                    }
                    
                }
            }
        },
        mounted: function () {
            if (this.isMultiple)
                this.multipleOption = this.isMultiple
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.getData();
        },
    }
</script>