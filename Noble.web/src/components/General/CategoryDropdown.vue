<template>
<div>
    <multiselect v-if="isMultiple" v-model="DisplayValue" :options="options" :multiple="true" v-bind:placeholder="$t('CategoryDropdown.PleaseSelectProductCategory')" track-by="dropDownName" :clear-on-select="false" :show-labels="false" label="dropDownName" :preselect-first="true">
        <!--<p slot="noResult" class="text-danger"> Oops! No Category found.</p>-->
        <template v-slot:noResult>
        <a  class="btn btn-primary " v-on:click="AddCategory('Add')" v-if="isValid('CanAddCategory')">{{ $t('CategoryDropdown.AddCategory') }}</a><br />
</template>
    </multiselect>
    <multiselect v-else v-model="DisplayValue" :options="options" :multiple="false" v-bind:placeholder="$t('CategoryDropdown.PleaseSelectProductCategory')" track-by="dropDownName" :clear-on-select="false" :show-labels="false" label="dropDownName" :preselect-first="true" >
        <!--<p slot="noResult" class="text-danger"> Oops! No Category found.</p>-->
        <template v-slot:noResult>
        <a  class="btn btn-primary " v-on:click="AddCategory('Add')" v-if="isValid('CanAddCategory')">{{ $t('CategoryDropdown.AddCategory') }}</a><br />

        </template>
    </multiselect>
    <modal :show="show" v-if="show">
        <div class="modal-content">
            <div class="modal-header" v-if="type == 'Edit'">
                <h5 class="modal-title" id="myModalLabel"> {{ $t('CategoryDropdown.UpdateCategory') }}</h5>
            </div>
            <div class="modal-header" v-else>
                <h5 class="modal-title" id="myModalLabel"> {{ $t('CategoryDropdown.AddCategory') }}</h5>
            </div>
            <div class="modal-body">
                <div class="row ">
                    <div v-bind:key="rendered" class="form-group col-sm-12">
                        <label>{{ $t('CategoryDropdown.Code') }} :<span class="text-danger">
                                *</span></label>
                        <div v-bind:class="{ 'has-danger': v$.category.code.$error }">
                            <input readonly class="form-control" v-model="v$.category.code.$model" />
                            <span v-if="v$.category.code.$error" class="error text-danger">
                            </span>
                        </div>
                    </div>
                    <div v-if="english == 'true'" class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.category.name.$error }">
                        <label class="text  font-weight-bolder">{{$englishLanguage($t('CategoryDropdown.CategoryName')) 
                                    
                            }}: <span class="text-danger"> *</span></label>
                        <input class="form-control" v-model="v$.category.name.$model" type="text" />
                        <span v-if="v$.category.name.$error" class="error">
                            <span v-if="!v$.category.name.required"> {{ $t('CategoryDropdown.NameRequired')
                                }}</span>
                            <span v-if="!v$.category.name.maxLength">{{ $t('CategoryDropdown.NameLength')
                                }}</span>
                        </span>
                    </div>

                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.category.nameArabic.$error }">
                        <label class="text  font-weight-bolder"> {{$arabicLanguage($t('CategoryDropdown.CategoryNameAr')) 
                                    
                            }}: <span class="text-danger"> *</span></label>
                        <input class="form-control  " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.category.nameArabic.$model" type="text" />
                        <span v-if="v$.category.nameArabic.$error" class="error">
                            <span v-if="!v$.category.nameArabic.required"> {{$t('CategoryDropdown.NameRequired')}}</span>
                            <span v-if="!v$.category.nameArabic.maxLength">{{$t('CategoryDropdown.NameLength')}}</span>
                        </span>
                    </div>
                    <div class="form-group col-sm-12">
                        <label>{{ $t('CategoryDropdown.Description') }} :</label>
                        <div v-bind:class="{ 'has-danger': v$.category.description.$error }">
                            <textarea rows="3" class="form-control " v-model="v$.category.description.$model" />
                            <span v-if="v$.category.description.$error" class="error text-danger">

                                </span>
                            </div>
                        </div>
                        <div class="form-group col-md-4">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox1" v-model="category.isActive">
                                <label for="inlineCheckbox1"> {{ $t('CategoryDropdown.Active') }} </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveCategory"
                        v-bind:disabled="v$.category.$invalid" v-if="type != 'Edit'">{{ $t('CategoryDropdown.btnSave')}}</button>
                    <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveCategory"
                        v-bind:disabled="v$.category.$invalid" v-if="type == 'Edit'">{{$t('CategoryDropdown.btnUpdate')}}</button>
                    <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{$t('CategoryDropdown.btnClear')}}</button>
                </div>
                <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
            </div>
        </modal>
        <div class="card-footer col-md-3" v-if="loading">
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </div>
</template>

<script>
import clickMixin from '@/Mixins/clickMixin'
import {
    required,
    maxLength,
    requiredIf
} from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';

import Multiselect from 'vue-multiselect';
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
export default {
    props: ["modelValue", "isTemporary", 'isMultiple'],
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
            loading: false,
            type: '',
            category: {
                id: '00000000-0000-0000-0000-000000000000',
                code: '',
                name: '',
                nameArabic: '',
                description: '',
                purchaseAccount: '',
                cOSQAccount: '',
                inventoryAccount: '',
                incomeAccount: '',
                saleAccount: '',
                isActive: true
            },
            rendered: 0
        }
    },
    validations() {
     return{
           category: {
            code: {
                required
            },
            name: {
                maxLength: maxLength(50)
            },
            nameArabic: {
                required: requiredIf(function () {
                            return !this.category.name;
                        }),
                // required: requiredIf((x) => {
                //     if (x.name == '' || x.name == null)
                //         return true;
                //     return false;
                // }),
                maxLength: maxLength(50)
            },
            description: {},
            purchaseAccountId: {},
            cOGSAccountId: {},
            inventoryAccountId: {},
            incomeAccountId: {},
            saleAccountId: {}
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
            this.$https.get('/Product/GetCategoryInformation?isActive=true', {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {

                if (response.data != null) {

                    response.data.results.categories.forEach(function (cat) {
                        root.options.push({
                            id: cat.id,
                            dropDownName:  cat.name != "" ? cat.code + ' ' + cat.name : cat.code + ' ' + cat.nameArabic ,
                            name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != "" ? cat.name : cat.nameArabic) : (cat.nameArabic != "" ? cat.nameArabic : cat.name)

                        })
                        if (root.isTemporary == true) {
                            if (cat.name == 'Temporary Category') {

                                root.modelValue = cat.id;

                            }
                        }

                    })

                }
            }).then(function () {

                root.value = root.options.find(function (x) {

                    return x.id == root.modelValue;
                })

            });
        },
        AddCategory: function (type) {

            this.v$.$reset();
            this.AutoIncrementCode();
            this.category = {
                id: '00000000-0000-0000-0000-000000000000',
                code: '',
                name: '',
                nameArabic: '',
                description: '',
                purchaseAccount: '',
                cOSQAccount: '',
                inventoryAccount: '',
                incomeAccount: '',
                saleAccount: '',
                isActive: true
            }

            this.show = !this.show;
            this.type = type;
        },
        close: function () {
            this.show = false;

        },
        SaveCategory: function () {

            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https
                .post('/Product/SaveCategoryInformation', this.category, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                .then(response => {
                    if (response.data.isSuccess == true) {
                        root.loading = false

                        root.show = false;
                        root.$swal({
                            icon: 'success',
                            title: 'Saved Successfully!',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        root.show = false;
                        root.getData();
                    } else {
                        root.loading = false

                        root.$swal({
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
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
                .finally(() => root.loading = false)
        },
        AutoIncrementCode: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https
                .get('/Product/CategoryAutoGenerateCode', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {
                    if (response.data != null) {
                        root.category.code = response.data;
                        root.rendered++
                    }
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
