<template>
<div>
    <multiselect v-if="isMultiple == true" v-model="DisplayValue" :options="options" :multiple="true" v-bind:placeholder="$t('ProductMasterDropdown.SelectProduct')" track-by="dropDownName" :clear-on-select="false" :show-labels="false" label="dropDownName" :preselect-first="true" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left ' : 'arabicLanguage '">
      <template v-slot:noResult>
        <a  class="btn btn-primary " v-on:click="AddBrand('Add')" v-if="isValid('CanAddProduct')">{{ $t('ProductMasterDropdown.AddProductMaster1') }}</a><br />
    </template>
    </multiselect>
    <multiselect v-else v-model="DisplayValue" :options="options" :multiple="false" v-bind:placeholder="$t('ProductMasterDropdown.SelectProduct')" track-by="dropDownName" :clear-on-select="false" :show-labels="false" label="dropDownName" :preselect-first="true" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left ' : 'arabicLanguage '">
        <template v-slot:noResult>
        <a  class="btn btn-primary " v-on:click="AddBrand('Add')" v-if="isValid('CanAddProduct')">{{ $t('ProductMasterDropdown.AddProductMaster1') }}</a><br />
    </template>
    </multiselect>

    <modal :show="show" v-if="show">
        <div class="modal-content">
            <div class="modal-header" v-if="type == 'Edit'">
                <h5 class="modal-title" id="myModalLabel"> {{ $t('ProductMasterDropdown.UpdateBrand') }} </h5>
            </div>
            <div class="modal-header" v-else>
                <h5 class="modal-title" id="myModalLabel"> {{ $t('ProductMasterDropdown.AddProductMaster') }}</h5>
            </div>
            <div class="modal-body">
                <div class="row ">
                    <div :key="render" class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.productMaster.code.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('ProductMasterDropdown.Code') }}:<span class="text-danger"> *</span></label>
                        <input disabled class="form-control" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'" v-model="v$.productMaster.code.$model" type="text" />
                        <span v-if="v$.productMaster.code.$error" class="error">
                            <span v-if="!v$.productMaster.code.maxLength">{{ $t('ProductMasterDropdown.CodeLength')
                                }}</span>
                        </span>
                    </div>
                    <div v-if="english == 'true'" class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.productMaster.name.$error }">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('ProductMasterDropdown.ProductNameEn')) 
                                    
                            }}: <span class="text-danger"> *</span></label>
                        <input class="form-control" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'" v-model="v$.productMaster.name.$model" type="text" />
                        <span v-if="v$.productMaster.name.$error" class="error">
                            <span v-if="!v$.productMaster.name.required"> {{ $t('ProductMasterDropdown.NameRequired') }}</span>
                            <span v-if="!v$.productMaster.name.maxLength">{{ $t('ProductMasterDropdown.NameLength')
                                }}</span>
                        </span>
                    </div>
                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.productMaster.nameArabic.$error }">
                        <label class="text  font-weight-bolder">{{$arabicLanguage($t('ProductMasterDropdown.ProductNameEn')) 
                                    
                            }}: <span class="text-danger"> *</span></label>
                        <input class="form-control  " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.productMaster.nameArabic.$model" type="text" />
                        <span v-if="v$.productMaster.nameArabic.$error" class="error">
                            <span v-if="!v$.productMaster.nameArabic.required"> {{ $t('ProductMasterDropdown.NameRequired')}}</span>
                            <span v-if="!v$.productMaster.nameArabic.maxLength">{{$t('ProductMasterDropdown.NameLength')}}</span>
                        </span>
                    </div>

                    <div class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.productMaster.description.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('ProductMasterDropdown.Description') }}:
                        </label>
                        <textarea rows="3" class="form-control" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'" v-model="v$.productMaster.description.$model" type="text" />
                        <span v-if="v$.productMaster.description.$error" class="error">{{ $t('ProductMasterDropdown.descriptionLength') }}</span>
                        </div>
                        <div class="form-group col-md-4">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox1" v-model="productMaster.isActive">
                                <label for="inlineCheckbox1"> {{ $t('ProductMasterDropdown.Active') }} </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div v-if="!loading">
                    <div class="modal-footer">
                        <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBrand"
                            v-bind:disabled="v$.productMaster.$invalid" v-if="type != 'Edit'">{{ $t('ProductMasterDropdown.btnSave')}}</button>
                        <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBrand"
                            v-bind:disabled="v$.productMaster.$invalid" v-if="type == 'Edit'">{{ Update }}</button>
                        <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{$t('ProductMasterDropdown.btnClear') }}</button>
                    </div>
                </div>
                <div v-else>
                    <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                </div>
            </div>
        </modal>
    </div>
</template>

<script>
import Multiselect from 'vue-multiselect'
import clickMixin from '@/Mixins/clickMixin'

    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

import {
    requiredIf,
    maxLength
} from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
export default {
    mixins: [clickMixin],
    name: 'branddropdown',
    props: ["modelValue", "isMultiple"],

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
            productMaster: {
                id: '00000000-0000-0000-0000-000000000000',
                code: '',
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
           productMaster: {
            name: {
                maxLength: maxLength(50)
            },
            nameArabic: {
                required: requiredIf(function () {
                            return !this.productMaster.name;
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
        EmptyRecord: function () {

            this.DisplayValue = '';

        },
        getData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.options = [];
            this.$https.get('/Product/ProductMasterList?isActive=true', {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {

                if (response.data != null) {
                    response.data.results.productMasters.forEach(function (result) {
                        root.options.push({
                            id: result.id,
                            dropDownName: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (result.name != '' ? result.code + ' ' + result.name : result.code + ' ' + result.nameArabic) : (result.nameArabic != '' ? result.code + ' ' + result.nameArabic : result.code + ' ' + result.name),
                            name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (result.name != '' ? result.name : result.nameArabic) : (result.nameArabic != '' ? result.nameArabic : result.name),
                            nameArabic: result.nameArabic
                        })
                    })
                }
            }).then(function () {
                root.value = root.options.find(function (x) {
                    return x.id == root.modelValue;
                })
            });
        },
        AddBrand: function (type) {
            this.v$.$reset();
            this.GetAutoCodeGenerator();
            this.productMaster = {
                id: '00000000-0000-0000-0000-000000000000',
                code: '',
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
        GetAutoCodeGenerator: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Product/ProductMasterCode', {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {

                if (response.data != null) {
                    root.productMaster.code = response.data;
                    root.render++;
                }
            });
        },
        SaveBrand: function () {
            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.post('/Product/SaveProductMaster', this.productMaster, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                .then(function (response) {

                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {
                            root.getData();
                           
                            root.$swal({
                                icon: 'success',
                                title: 'Saved Successfully!',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.show = false;
                        } else {
                            root.getData();

                           

                            root.show = false;
                        }
                    } else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your Product Master Name  Already Exist!",
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
                    root.$swal.fire({
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
                if (value == null || value == undefined) {
                    this.$emit('update:modelValue', value);

                } else {
                    if (this.isMultiple == true) {
                        this.$emit('update:modelValue', value);
                    } else {
                        this.$emit('update:modelValue', value.id);
                    }

                }
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
