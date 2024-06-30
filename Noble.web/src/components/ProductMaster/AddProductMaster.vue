<template >
    <modal :show="show" v-if="isValid('CanAddProduct') || isValid('CanEditProduct')">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'">{{ $t('AddProductMaster.UpdateProductMaster') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddProductMaster.AddProductMaster') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div :key="render" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.productMaster.code.$error}">
                        <label class="text  font-weight-bolder"> {{ $t('AddProductMaster.Code') }}:<span class="text-danger"> *</span></label>
                        <input disabled class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.productMaster.code.$model" type="text" />
                        <span v-if="v$.productMaster.code.$error" class="error">
                            <span v-if="!v$.productMaster.code.maxLength">{{ $t('AddProductMaster.CodeLength') }}</span>
                        </span>
                    </div>
                    <div v-if="english=='true'" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.productMaster.name.$error}">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddProductMaster.ProductMasterName'))  }}: <span class="text-danger"> *</span></label>
                        <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.productMaster.name.$model" type="text" />
                        <span v-if="v$.productMaster.name.$error" class="error">
                            <span v-if="!v$.productMaster.name.required"> {{ $t('AddProductMaster.NameRequired') }}</span>
                            <span v-if="!v$.productMaster.name.maxLength">{{ $t('AddProductMaster.NameLength') }}</span>
                        </span>
                    </div>
                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.productMaster.nameArabic.$error}">
                        <label class="text  font-weight-bolder">{{$arabicLanguage($t('AddProductMaster.ProductMasterName'))  }}: <span class="text-danger"> *</span></label>
                        <input class="form-control " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.productMaster.nameArabic.$model" type="text" />
                        <span v-if="v$.productMaster.nameArabic.$error" class="error">
                            <span v-if="!v$.productMaster.nameArabic.required"> {{ $t('AddProductMaster.NameRequired') }}</span>
                            <span v-if="!v$.productMaster.nameArabic.maxLength">{{ $t('AddProductMaster.NameLength') }}</span>
                        </span>
                    </div>

                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.productMaster.description.$error}">
                        <label class="text  font-weight-bolder"> {{ $t('AddProductMaster.Description') }}: </label>
                        <textarea rows="3" class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.productMaster.description.$model" type="text" />
                        <span v-if="v$.productMaster.description.$error" class="error">{{ $t('AddProductMaster.descriptionLength') }}</span>
                    </div>
                    <div class="form-group col-md-12">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox2" v-model="productMaster.isActive">
                            <label for="inlineCheckbox2"> {{ $t('AddProductMaster.Active') }} </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveProductMaster" v-bind:disabled="v$.productMaster.$invalid" v-if="type!='Edit' && isValid('CanAddProduct') ">{{ $t('AddProductMaster.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveProductMaster" v-bind:disabled="v$.productMaster.$invalid" v-if="type=='Edit' && isValid('CanEditProduct') ">{{ $t('AddProductMaster.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddProductMaster.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>


    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
        mixins: [clickMixin],
        props: ['show', 'newProductMaster', 'type'],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                productMaster: this.newProductMaster,
                arabic: '',
                english: '',
                render: 0,
                loading: false,
                isMasterProduct: false,
            }
        },
        validations() {
            return {
                productMaster: {
                    name: {
                        maxLength: maxLength(250)
                    },
                    code: {
                        maxLength: maxLength(30)
                    },
                    
                    nameArabic: {
                        required: requiredIf(function () {
                            return !this.productMaster.name;
                        }),
                        //required: requiredIf((x) => {
                        //    if (x.name == '' || x.name == null)
                        //        return true;
                        //    return false;
                        //}),
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
                this.$https.get('/Product/ProductMasterCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.productMaster.code = response.data;
                        root.render++;
                    }
                });
            },
            SaveProductMaster: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Product/SaveProductMaster', this.productMaster, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {
                            if (root.type != "Edit") {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
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
                                text: "Your ProductMaster Name  Already Exist!",
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
            this.isMasterProduct = localStorage.getItem('IsMasterProductPermission') == 'true' ? true : false;

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.productMaster.id == '00000000-0000-0000-0000-000000000000' || this.productMaster.id == undefined || this.productMaster.id == '')
                this.GetAutoCodeGenerator();

        }
    }
</script>
