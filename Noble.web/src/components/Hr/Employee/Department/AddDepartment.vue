<template >
    <modal :show="show" >
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'">Update Department</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>Create Department</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row ">
                                    <div :key="render" class="form-group has-label col-sm-12 "
                                        v-bind:class="{ 'has-danger': v$.department.code.$error }">
                                        <label class="text  font-weight-bolder"> Code:
                                            <span class="text-danger"> *</span></label>
                                        <input disabled class="form-control"
                                            v-model="v$.department.code.$model" type="text" />
                                        <span v-if="v$.department.code.$error" class="error">
                                            <span v-if="!v$.department.code.maxLength">{{ $t('CodeLength') }}</span>
                                        </span>
                                    </div>
                                    <div v-if="english == 'true'" class="form-group has-label col-sm-12 "
                                        v-bind:class="{ 'has-danger': v$.department.name.$error }">
                                        <label class="text  font-weight-bolder"> Name: <span class="text-danger"> *</span></label>
                                        <input class="form-control"
                                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'"
                                            v-model="v$.department.name.$model" type="text" />
                                        <span v-if="v$.department.name.$error" class="error">
                                            <span v-if="!v$.department.name.required"> {{ $t('NameRequired') }}</span>
                                            <span v-if="!v$.department.name.maxLength">{{ $t('NameLength') }}</span>
                                        </span>
                                    </div>
                                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.department.nameArabic.$error}">
                                    <label class="text  font-weight-bolder"> {{$arabicLanguage($t('AddSize.SizeNameAr'))  }}:<span class="text-danger"> *</span> </label>
                                    <input class="form-control " v-model="v$.department.nameArabic.$model" type="text" />
                                    <span v-if="v$.department.nameArabic.$error" class="error">
                                        <span v-if="!v$.department.nameArabic.required"> {{ $t('AddDepartment.NameRequired') }}</span>
                                        <span v-if="!v$.department.nameArabic.maxLength">{{ $t('AddSize.NameLength') }}</span>
                                    </span>
                                </div>
                                    
                                    <div class="form-group text-start has-label col-sm-12 ">
                                        <label class="text  font-weight-bolder">Bank: <span class="text-danger">
                                                *</span></label>
                                        <bankdropdown v-model="department.bankId" @update:modelValue="GetBank1Account"
                                            :modelValue="department.bankId"></bankdropdown>
                                    </div>

                                    <div class="form-group has-label col-sm-12 "
                                        v-bind:class="{ 'has-danger': v$.department.description.$error }">
                                        <label class="text  font-weight-bolder"> Description:
                                        </label>
                                        <textarea class="form-control"
                                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'"
                                            v-model="v$.department.description.$model" type="text" />
                                        <span v-if="v$.department.description.$error" class="error">{{
                                            $t('descriptionLength') }}</span>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label style="margin: 7px;">Active</label> <br />
                                        <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate"
                                            v-bind:class="{ 'bootstrap-switch-on': department.isActive, 'bootstrap-switch-off': !department.isActive }"
                                            v-on:click="department.isActive = !department.isActive" style="width: 72px;">
                                            <div class="bootstrap-switch-container" style="width: 122px; margin-left: 0px;">
                                                <span class="bootstrap-switch-handle-on bootstrap-switch-success"
                                                    style="width: 50px;">
                                                    <i class="nc-icon nc-check-2"></i>
                                                </span>
                                                <span class="bootstrap-switch-label" style="width: 30px;">&nbsp;</span>
                                                <span class="bootstrap-switch-handle-off bootstrap-switch-success"
                                                    style="width: 50px;">
                                                    <i class="nc-icon nc-simple-remove"></i>
                                                </span>
                                                <input class="bootstrap-switch" type="checkbox" data-toggle="switch"
                                                    checked="" data-on-label="<i class='nc-icon nc-check-2'></i>"
                                                    data-off-label="<i class='nc-icon nc-simple-remove'></i>"
                                                    data-on-color="success" data-off-color="success">
                                            </div>
                                        </div>
                                    </div>
                                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveDepartment" v-bind:disabled="v$.department.$invalid" v-if="type!='Edit'">{{ $t('AddSize.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveDepartment" v-bind:disabled="v$.department.$invalid" v-if="type=='Edit'">{{ $t('AddSize.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddSize.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>

        
    </modal>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'

import { maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';

export default {
    mixins: [clickMixin],
    props: ['show', 'newDepartment', 'type'],
    components: {
        Loading
    },
    setup() {
        return { v$: useVuelidate() }
    },

    data: function () {
        return {
            department: this.newDepartment,
            arabic: '',
            english: '',
            render: 0,
            loading: false,
        }
    },
    validations() {
        return {
            department: {
                name: {
                    maxLength: maxLength(50)
                },
                code: {

                    maxLength: maxLength(30)
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
        close: function () {
            this.$emit('close');
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
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved' : 'تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });

                            root.close();
                        }
                        else {

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
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
                            text: "Your Department Name  Already Exist!",
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
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        if (this.department.id == '00000000-0000-0000-0000-000000000000' || this.department.id == undefined || this.department.id == '')
            this.GetAutoCodeGenerator();

    }
}
</script>
