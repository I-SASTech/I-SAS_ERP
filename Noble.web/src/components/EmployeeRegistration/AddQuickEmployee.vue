<template>
    <modal :show="show" >
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'"> {{ $t('AddQuickEmployee.UpdateEmployee') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddQuickEmployee.AddEmployee') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row ">
                    <div :key="render" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.employee.code.$error}">
                        <label class="text  font-weight-bolder"> {{ $t('AddQuickEmployee.EmployeeCode') }}: <span class="text-danger"> *</span></label>
                        <input disabled class="form-control" v-model="v$.employee.code.$model" type="text" />
                        <span v-if="v$.employee.code.$error" class="error">

                        </span>
                    </div>


                    <div v-if="english=='true'" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.employee.englishName.$error}">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddQuickEmployee.EmployeeName(English)'))  }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control" v-model="v$.employee.englishName.$model" type="text" />
                        <span v-if="v$.employee.englishName.$error" class="error text-danger">
                            <span v-if="!v$.employee.englishName.required">{{ $t('AddQuickEmployee.EngValidation') }}</span>
                            <span v-if="!v$.employee.englishName.maxLength">{{ $t('AddQuickEmployee.EngMax') }}</span>

                        </span>
                    </div>

                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.employee.arabicName.$error}">
                        <label class="text  font-weight-bolder"> {{$arabicLanguage($t('AddQuickEmployee.EmployeeName(Arabic)'))  }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.arabicName.$model" type="text" />
                        <span v-if="v$.employee.arabicName.$error" class="error text-danger arabicLanguage">
                            <span class="arabicLanguage" v-if="!v$.employee.arabicName.required">{{ $t('AddQuickEmployee.ArValidation') }}</span>
                            <span class="arabicLanguage" v-if="!v$.employee.arabicName.maxLength">{{ $t('AddQuickEmployee.ArMax') }}</span>
                        </span>
                    </div>
                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.employee.email.$error}">
                        <label class="text  font-weight-bolder"> {{ $t('AddQuickEmployee.Email') }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control" @blur="EmailDuplicate(employee.email)" v-model="v$.employee.email.$model" type="text" />
                        <span v-if="v$.employee.email.$error" class="error text-danger arabicLanguage">
                            <span class="arabicLanguage" v-if="!v$.employee.email.required">{{ $t('AddQuickEmployee.EmailRequired') }}</span>
                            <span class="arabicLanguage" v-if="!v$.employee.email.email">{{ $t('AddQuickEmployee.EmailInvalid') }}</span>

                        </span>
                    </div>


                    <div class="form-group col-sm-6">
                        <label>{{ $t('AddQuickEmployee.EmGender') }} :<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.employee.gender.$error}">
                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight())" v-model="v$.employee.gender.$model" :options="['Male', 'Fe-Male', 'Other']" :show-labels="false" :placeholder="$t('AddQuickEmployee.SelectGender')">
                            </multiselect>
                            <multiselect v-else v-model="v$.employee.gender.$model" :options="['ذكر', 'أنثى', 'آخر']" :show-labels="false" :placeholder="$t('AddQuickEmployee.SelectGender')">
                            </multiselect>
                            <span v-if="v$.employee.gender.$error" class="error text-danger">
                                <span v-if="!v$.employee.gender.required">{{ $t('AddQuickEmployee.GValidation') }}</span>
                            </span>
                        </div>
                    </div>
                    <div class="form-group col-sm-6">
                        <label>{{ $t('AddQuickEmployee.RegistrationDate') }} :<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.employee.registrationDate.$error}">
                            <datepicker v-model="v$.employee.registrationDate.$model" :key="daterander"></datepicker>
                            <span v-if="v$.employee.registrationDate.$error" class="error text-danger">
                                <span v-if="!v$.employee.englishName.required">{{ $t('AddQuickEmployee.RegistrationValidation') }}</span>
                            </span>
                        </div>
                    </div>
                    <div class="form-group col-sm-6">
                        <label>{{ $t('AddQuickEmployee.IDNumber') }}:<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.employee.idNumber.$error}">
                            <input class="form-control " type="text" v-model="v$.employee.idNumber.$model" />
                            <span v-if="v$.employee.idNumber.$error" class="error text-danger">
                                <span v-if="!v$.employee.idNumber.required">{{ $t('AddQuickEmployee.IDRequired') }}</span>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div v-if="type=='Edit'">
                    <button type="button" class="btn btn-soft-primary btn-sm mx-2" v-on:click="SaveEmployee" v-bind:disabled="v$.employee.$invalid"> {{ $t('AddQuickEmployee.btnUpdate') }}</button>
                    <button type="button" class="btn btn-soft-secondary btn-sm " v-on:click="close()">{{ $t('AddQuickEmployee.btnClear') }}</button>
                </div>
                <div v-else>
                    <button type="button" class="btn btn-soft-primary btn-sm   mx-2" v-on:click="SaveEmployee" v-bind:disabled="v$.employee.$invalid"> {{ $t('AddQuickEmployee.btnSave') }}</button>
                    <button type="button" class="btn btn-soft-secondary btn-sm " v-on:click="close()">{{ $t('AddQuickEmployee.btnClear') }}</button>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </modal>
</template>
<script>

    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    import Multiselect from 'vue-multiselect';
    import moment from 'moment';
    import clickMixin from '@/Mixins/clickMixin'

    import { required, maxLength, email, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        props: ['show', 'newEmployee', 'type'],
         setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                   loading: false,
                employee: this.newEmployee,
                arabic: '',
                english: '',
                render: 0,
                daterander: 0,
                emailExist: false
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
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Email Already Exist!' : 'البريد الإلكتروني موجود بالفعل!',
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1700,
                                timerProgressBar: true,
                            });
                        }
                        else {
                            root.emailExist = false;
                        }


                    })
            },
            close: function () {
                this.$emit('close');
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
                if (this.emailExist) {
                    this.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Email Already Exist!' : 'البريد الإلكتروني موجود بالفعل!',
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 1700,
                        timerProgressBar: true,
                    });
                }
                else {


                    this.$https.post('/EmployeeRegistration/SaveEmployee', this.employee, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data.isSuccess == true) {

                            if (root.type != "Edit") {
                                
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: "Your Employee " + response.data.employee.englishName + " has been created!",
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.close();
                            }

                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Your Employee Name  Already Exist!' : 'اسم الموظف الخاص بك موجود بالفعل!',
                                type: 'error',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                        root.loading = false
                    });
                }
            }
        },
        mounted: function () {
            
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.employee.id == '00000000-0000-0000-0000-000000000000' || this.employee.id == undefined || this.employee.id == '')
                this.GetAutoCodeGenerator();
            this.employee.registrationDate = moment().format('llll');
            this.daterander++;

        }
    }
</script>
