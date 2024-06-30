<template>
<div>
    <multiselect v-model="DisplayValue" :options="options" :disabled="disabled" :multiple="false" v-bind:placeholder="$t('BrandDropdown.SelectBrand')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
        <!--<p slot="noResult" class="text-danger"> Oops! No Brand found.</p>-->
        <template v-slot:noResult>
        <a  class="btn btn-primary " v-on:click="AddBrand('Add')" v-if="isValid('CanAddBrand')">{{$t('BrandDropdown.CreateProductBrand')}}</a><br />
</template>
    </multiselect>
    <modal :show="show" v-if="show">

        <div class="modal-content">

            <div class="modal-header" v-if="type == 'Edit'">

                <h5 class="modal-title" id="myModalLabel"> {{ $t('BrandDropdown.UpdateBrand') }}</h5>

            </div>
            <div class="modal-header" v-else>

                <h5 class="modal-title" id="myModalLabel"> {{ $t('BrandDropdown.CreateProductBrand') }}</h5>

            </div>

            <div class="modal-body">
                <div class="row ">
                    <div :key="render" class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.brand.code.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('BrandDropdown.Code') }}:<span class="text-danger"> *</span></label>
                        <input disabled class="form-control" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'" v-model="v$.brand.code.$model" type="text" />
                        <span v-if="v$.brand.code.$error" class="error">
                            <span v-if="!v$.brand.code.maxLength">{{ $t('BrandDropdown.CodeLength')
                                }}</span>
                        </span>
                    </div>
                    <div v-if="english == 'true'" class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.brand.name.$error }">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('BrandDropdown.BrandName')) 
                                    
                            }}: <span class="text-danger"> *</span></label>
                        <input class="form-control" v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.brand.name.$model" type="text" />
                        <span v-if="v$.brand.name.$error" class="error">
                            <span v-if="!v$.brand.name.required"> {{ $t('BrandDropdown.NameRequired')
                                }}</span>
                            <span v-if="!v$.brand.name.maxLength">{{ $t('BrandDropdown.NameLength')
                                }}</span>
                        </span>
                    </div>
                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.brand.nameArabic.$error }">
                        <label class="text  font-weight-bolder">{{$arabicLanguage($t('BrandDropdown.BrandNameArabic')) 
                                    
                            }}: <span class="text-danger"> *</span></label>
                        <input class="form-control text-right " v-model="v$.brand.nameArabic.$model" type="text" />
                        <span v-if="v$.brand.nameArabic.$error" class="error">
                            <span v-if="!v$.brand.nameArabic.required"> {{ $t('BrandDropdown.NameRequired')
                                }}</span>
                            <span v-if="!v$.brand.nameArabic.maxLength">{{ $t('BrandDropdown.NameLength')
                                }}</span>
                        </span>
                    </div>

                    <div class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.brand.description.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('BrandDropdown.Description') }}:
                        </label>
                        <textarea rows="3" class="form-control" v-model="v$.brand.description.$model" type="text" />
                        <span v-if="v$.brand.description.$error" class="error">
                                {{$t('BrandDropdown.descriptionLength')}}
                            </span>
                        </div>
                        <div class="form-group col-md-4">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox1" v-model="brand.isActive">
                                <label for="inlineCheckbox1"> {{ $t('BrandDropdown.Active') }} </label>
                            </div>
                        </div>

                    </div>
                </div>

                <div v-if="!loading">
                    <div class="modal-footer justify-content-right" v-if="type == 'Edit'">
                        <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBrand"
                                v-bind:disabled="v$.brand.$invalid">
                            {{ $t('BrandDropdown.Update') }}
                        </button>
                        <button type="button" class="btn btn-secondary" v-on:click="close()">Cancel</button>
                    </div>
                    <div class="modal-footer justify-content-right" v-else>
                        <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveBrand"
                            v-bind:disabled="v$.brand.$invalid"> {{ $t('BrandDropdown.btnSave') }}</button>
                        <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">
                            {{$t('BrandDropdown.btnClear')}}
                        </button>
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
    props: ["modelValue", 'disabled'],

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
            brand: {
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
         brand: {
            name: {
                maxLength: maxLength(50)
            },
            nameArabic: {
                required: requiredIf(function () {
                            return !this.brand.name;
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
            this.$https.get('/Product/BrandList?isActive=true', {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {

                if (response.data != null) {
                    response.data.results.brands.forEach(function (result) {
                        root.options.push({
                            id: result.id,
                            name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (result.name != '' ? result.code + ' ' + result.name : result.code + ' ' + result.nameArabic) : (result.nameArabic != '' ? result.code + ' ' + result.nameArabic : result.code + ' ' + result.name),
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
            this.brand = {
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
            this.$https.get('/Product/BrandCode', {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {

                if (response.data != null) {
                    root.brand.code = response.data;
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
            this.$https.post('/Product/SaveBrand', this.brand, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
                .then(function (response) {

                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {

                            root.$store.GetBrandList(response.data);
                            if (response.data.brand.isActive == true) {
                                root.options.push({
                                    id: response.data.brand.id,
                                    name: response.data.brand.name,
                                    nameArabic: response.data.brand.nameArabic,
                                    description: response.data.brand.description,
                                    code: response.data.brand.code,
                                })
                            }
                            root.$swal({
                                icon: 'success',
                                title: 'Saved Successfully!',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.show = false;
                        } else {
                            var data = root.$store.isbrandList.find(function (x) {
                                return x.id == response.data.brand.id;
                            });
                            data.id = response.data.brand.id;
                            data.name = (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (response.data.brand.name != '' ? response.data.brand.code + ' ' + response.data.brand.name : response.data.brand.code + ' ' + response.data.brand.nameArabic) : (response.data.brand.nameArabic != '' ? response.data.brand.code + ' ' + response.data.brand.nameArabic : response.data.brand.code + ' ' + response.data.brand.name),
                                data.nameArabic = response.data.brand.nameArabic;
                            data.description = response.data.brand.description;
                            data.code = response.data.brand.code;

                            root.show = false;
                        }
                    } else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your Brand Name  Already Exist!",
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
