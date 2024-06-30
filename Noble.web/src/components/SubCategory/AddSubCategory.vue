<template>
    <modal show="show" v-if="isValid('CanAddSubCategory') || isValid('CanEditSubCategory')">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                    {{
                        $t('AddSubCategory.UpdateSubCategory')
                    }}
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    {{
 $t('AddSubCategory.AddSubCategory')
                    }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-12">
                        <label>{{ $t('AddSubCategory.Code') }} :<span class="text-danger"> *</span></label>
                        <div>
                            <input readonly class="form-control" :key="rendered" v-model="subCategory.code" />

                        </div>
                    </div>
                    <div class="form-group col-sm-12">
                        <label>{{ $t('AddSubCategory.SelectCategory') }} :<span class="text-danger"> *</span></label>
                        <div v-bind:class="{ 'has-danger': v$.subCategory.categoryId.$error }">
                            <categorydropdown v-model="v$.subCategory.categoryId.$model"
                                              :modelValue="subCategory.categoryId"></categorydropdown>
                            <span v-if="v$.subCategory.categoryId.$error" class="error text-danger">
                                <span v-if="!v$.subCategory.categoryId.required">Category is required</span>
                            </span>
                        </div>
                    </div>

                    <div v-if="english == 'true'" class="form-group has-label col-sm-12">
                        <label>
                            {{$englishLanguage($t('AddSubCategory.SubCategoryName'))   }} :<span class="text-danger">
                                *
                            </span>
                        </label>
                        <div v-bind:class="{ 'has-danger': v$.subCategory.name.$error }">
                            <input class="form-control " v-model="v$.subCategory.name.$model" />
                            <span v-if="v$.subCategory.name.$error" class="error text-danger">
                                <span v-if="!v$.subCategory.name.required">
                                    {{
 $t('AddSubCategory.NameRequired')
                                    }}
                                </span>
                                <span v-if="!v$.subCategory.name.maxLength">{{ $t('AddSubCategory.NameLength') }}</span>
                            </span>
                        </div>
                    </div>
                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12"
                         v-bind:class="{ 'has-danger': v$.subCategory.nameArabic.$error }">
                        <label class="text  font-weight-bolder">
                            {{
$arabicLanguage($t('AddSubCategory.SubCategoryNameAr'))
                            }}: <span class="text-danger"> *</span>
                        </label>
                        <input class="form-control  " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"
                               v-model="v$.subCategory.nameArabic.$model" type="text" />
                        <span v-if="v$.subCategory.nameArabic.$error" class="error">
                            <span v-if="!v$.subCategory.nameArabic.required">
                                {{
 $t('AddSubCategory.NameRequired')
                                }}
                            </span>
                            <span v-if="!v$.subCategory.nameArabic.maxLength">
                                {{
 $t('AddSubCategory.NameLength')
                                }}
                            </span>
                        </span>
                    </div>


                    <div class="form-group has-label col-sm-12 "
                         v-bind:class="{ 'has-danger': v$.subCategory.description.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('AddSubCategory.Description') }}: </label>
                        <textarea rows="3" class="form-control" v-model="v$.subCategory.description.$model" type="text" />
                        <span v-if="v$.subCategory.description.$error" class="error">
                            {{
                                $t('AddSubCategory.descriptionLength150')
                            }}
                        </span>
                    </div>

                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="subCategory.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddSubCategory.Active') }} </label>
                        </div>
                    </div>



                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveSubCategory"
                        v-bind:disabled="v$.subCategory.$invalid" v-if="type != 'Edit' && isValid('CanAddSubCategory')">
                    {{
                            $t('AddSubCategory.btnSave')
                    }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveSubCategory"
                        v-bind:disabled="v$.subCategory.$invalid" v-if="type == 'Edit' && isValid('CanEditSubCategory')">
                    {{
                            $t('AddSubCategory.btnUpdate')
                    }}
                </button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">
                    {{
                        $t('AddSubCategory.btnClear')
                    }}
                </button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </modal>
    <acessdenied v-else :model=true></acessdenied>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    export default ({
        mixins: [clickMixin],
        name: 'AddSubCategory',
        props: ['show', 'newsubcategory', 'type', 'categoryid'],
        components: {
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
                rendered: 0,
                subCategory: {}
            }
        },
        validations() {
            return {
                subCategory:
                {
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
                            return !this.subCategory.name;
                        }),
                        maxLength: maxLength(250)
                    },
                    categoryId: {
                        required
                    },
                    description: {
                        maxLength: maxLength(500)
                    },
                }
            }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },

            SaveSubCategory: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Product/SaveSubCategoryInformation', this.subCategory, { headers: { "Authorization": `Bearer ${token}` } })
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
                                text: "Your Sub Category Already Exist!",
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
            },
            AutoIncrementCode: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/Product/SubCategoryAutoGenerateCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.subCategory.code = response.data;
                            root.rendered++
                        }
                    });
            }
        },

        created: function () {
            this.subCategory = this.newsubcategory;
        },
        mounted: function () {

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.subCategory.id == "00000000-0000-0000-0000-000000000000") {
                this.AutoIncrementCode();
            }
            if (this.categoryid != undefined) {
                this.subCategory.categoryId = this.categoryid;
            }
        }
    })

</script>