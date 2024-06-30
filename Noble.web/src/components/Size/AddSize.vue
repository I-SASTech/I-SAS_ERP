<template>
    <modal show="show" v-if=" isValid('CanAddSize') || isValid('CanEditSize') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'"> {{ $t('AddSize.UpdateProductSize') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddSize.AddProductSize') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div v-bind:key="render" class="form-group col-sm-12">
                        <label>{{ $t('AddSize.Code') }} :<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.size.code.$error}">
                            <input readonly class="form-control" v-model="v$.size.code.$model" />
                            <span v-if="v$.size.code.$error" class="error text-danger">
                            </span>
                        </div>
                    </div>
                    <div v-if="english=='true'" class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddSize.SizeName'))  }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control" v-model="v$.size.name.$model" />
                        <span v-if="v$.size.name.$error" class="error">
                            <span v-if="!v$.size.name.required"> {{ $t('AddSize.NameRequired') }}</span>
                            <span v-if="!v$.size.name.maxLength">{{ $t('AddSize.NameRequired') }}</span>
                        </span>
                    </div>

                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.size.nameArabic.$error}">
                        <label class="text  font-weight-bolder"> {{$arabicLanguage($t('AddSize.SizeNameAr'))  }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control " v-model="v$.size.nameArabic.$model" type="text" />
                        <span v-if="v$.size.nameArabic.$error" class="error">
                            <span v-if="!v$.size.nameArabic.required"> {{ $t('AddSize.NameRequired') }}</span>
                            <span v-if="!v$.size.nameArabic.maxLength">{{ $t('AddSize.NameLength') }}</span>
                        </span>
                    </div>

                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.size.description.$error}">
                        <label class="text  font-weight-bolder"> {{ $t('AddSize.Description') }}: </label>
                        <textarea rows="3" class="form-control" v-model="v$.size.description.$model" type="text" />
                        <span v-if="v$.size.description.$error" class="error">{{ $t('AddSize.descriptionLength') }}</span>
                    </div>


                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="size.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddSize.Active') }} </label>
                        </div>
                    </div>



                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveSize" v-bind:disabled="v$.size.$invalid" v-if="type!='Edit' && isValid('CanAddSize')">{{ $t('AddSize.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveSize" v-bind:disabled="v$.size.$invalid" v-if="type=='Edit' && isValid('CanEditSize')">{{ $t('AddSize.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddSize.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { requiredIf, required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        props: ['show', 'newsize', 'type'],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                render: 0,
                arabic: '',
                english: '',
                loading: false,
                size: {},
            }
        },
        validations() {
            return {
                size: {
                    name: {
                        maxLength: maxLength(250)
                    },
                    nameArabic: {
                        // required: requiredIf((x) => {
                        //     if (x.name == '' || x.name == null)
                        //         return true;
                        //     return false;
                        // }),
                        required: requiredIf(function () {
                            return !this.size.name;
                        }),
                        maxLength: maxLength(250)
                    },
                    code: {
                        required,
                        maxLength: maxLength(30)
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
                this.$https.get('/Product/SizeCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.size.code = response.data;
                        root.render++;
                    }
                });
            },
            SaveSize: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Product/SaveSize', this.size, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data.isSuccess == true) {
                            if (root.type != "Edit") {

                                root.$swal({
                                    icon: 'success',
                                    title: 'Saved Successfully',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.close();
                            }
                            else {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                    icon: 'success',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                    type: 'success',
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
                                icon: 'error',
                                text: "Your Size Name  Already Exist!",
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
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
        created: function () {
            this.size = this.newsize;
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.size.id == '00000000-0000-0000-0000-000000000000' || this.size.id == undefined || this.size.id == '')
                this.GetAutoCodeGenerator();
        }
    }
</script>
