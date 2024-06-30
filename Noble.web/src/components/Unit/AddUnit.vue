<template>
    <modal show="show" v-if=" isValid('CanAddUnit') || isValid('CanEditUnit') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'"> {{ $t('AddUnit.UpdateProductUnit') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddUnit.AddProductUnit') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div v-bind:key="render" class="form-group col-sm-12">
                        <label>{{ $t('AddUnit.Code') }} :<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.unit.code.$error}">
                            <input readonly class="form-control" v-model="v$.unit.code.$model" />
                            <span v-if="v$.unit.code.$error" class="error text-danger">
                            </span>
                        </div>
                    </div>
                    <div v-if="english=='true'" class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddUnit.UnitName'))  }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control" v-model="v$.unit.name.$model" />
                        <span v-if="v$.unit.name.$error" class="error">
                            <span v-if="!v$.unit.name.required"> {{ $t('AddUnit.NameRequired') }}</span>
                            <span v-if="!v$.unit.name.maxLength">{{ $t('AddUnit.NameRequired') }}</span>
                        </span>
                    </div>

                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.unit.nameArabic.$error}">
                        <label class="text  font-weight-bolder"> {{$arabicLanguage($t('AddUnit.UnitNameAr'))  }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control " v-model="v$.unit.nameArabic.$model" type="text" />
                        <span v-if="v$.unit.nameArabic.$error" class="error">
                            <span v-if="!v$.unit.nameArabic.required"> {{ $t('AddUnit.NameRequired') }}</span>
                            <span v-if="!v$.unit.nameArabic.maxLength">{{ $t('AddUnit.NameLength') }}</span>
                        </span>
                    </div>

                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.unit.description.$error}">
                        <label class="text  font-weight-bolder"> {{ $t('AddUnit.Description') }}: </label>
                        <textarea rows="3" class="form-control" v-model="v$.unit.description.$model" type="text" />
                        <span v-if="v$.unit.description.$error" class="error">{{ $t('AddUnit.descriptionLength') }}</span>
                    </div>


                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="unit.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddUnit.Active') }} </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveUnit" v-bind:disabled="v$.unit.$invalid" v-if="type!='Edit' && isValid('CanAddUnit')">{{ $t('AddUnit.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveUnit" v-bind:disabled="v$.unit.$invalid" v-if="type=='Edit' && isValid('CanEditUnit')">{{ $t('AddUnit.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddUnit.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    
    import {maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'

    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        props: ['show', 'newunit', 'type'],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                render: 0,
                loading: false,
                unit: {},
            }
        },
        validations() {
            return {
                unit: {
                    name: {
                        maxLength: maxLength(250)
                    },
                    code: {
                        maxLength: maxLength(30)
                    },
                    nameArabic: {
                        // required: requiredIf((x) => {
                        //     if (x.name == '' || x.name == null)
                        //         return true;
                        //     return false;
                        // }),
                        required: requiredIf(function () {
                            return !this.unit.name;
                        }),
                        maxLength: maxLength(250)
                    },
                    description: {
                        maxLength: maxLength(500)
                    }
                }
                }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
            GetAutoCodeGenerator: function () {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Product/UnitCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data != null) {
                        root.unit.code = response.data;
                        root.render++;
                    }
                });
            },
            SaveUnit: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Product/SaveUnit', this.unit, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                    
                    // eslint-disable-line
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {
                           
                            root.$swal({
                                title: "Saved!",
                                icon:'success',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                type: 'success',
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
                            title: "Error!",
                            text: "Your Unit Name  Already Exist!",
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
                                title: 'Something Went Wrong!',
                                text: error.response.data,
                                type: 'error',
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            }
        },
        created: function () {
            this.unit= this.newunit;
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.unit.id == '00000000-0000-0000-0000-000000000000' || this.unit.id == undefined || this.unit.id == '')
                this.GetAutoCodeGenerator();

        }
    }
</script>
