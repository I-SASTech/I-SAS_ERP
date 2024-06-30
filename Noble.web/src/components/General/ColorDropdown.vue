<template>
<div>
    <multiselect v-if="isMultiple" v-model="DisplayValue" :options="options" :disabled="disabled" :multiple="true" v-bind:placeholder="$t('ColorDropdown.SelectColor')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
        <!--<p slot="noResult" class="text-danger"> Oops! No Color found.</p>-->
        <template v-slot:noResult>
        <a  class="btn btn-primary " v-on:click="AddColor('Add')" v-if="isValid('CanAddColor')">{{
                    $t('ColorDropdown.AddProductColor')
            }}</a><br />
</template>
    </multiselect>
    <multiselect v-else v-model="DisplayValue" :options="options" :disabled="disabled" :multiple="false" v-bind:placeholder="$t('ColorDropdown.SelectColor')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'arabicLanguage '">
        <!--<p slot="noResult" class="text-danger"> Oops! No Color found.</p>-->
        <template v-slot:noResult>
        <a  class="btn btn-primary " v-on:click="AddColor('Add')" v-if="isValid('CanAddColor')">{{$t('ColorDropdown.AddProductColor')}}</a><br />
</template>
    </multiselect>

    <modal :show="show" v-if="show">
        <div class="modal-content">
            <div class="modal-header" v-if="type == 'Edit'">
                <h5 class="modal-title" id="myModalLabel"> {{ $t('ColorDropdown.UpdateProductColor') }}</h5>
            </div>
            <div class="modal-header" v-else>
                <h5 class="modal-title" id="myModalLabel"> {{ $t('ColorDropdown.AddProductColor') }} </h5>
            </div>
            <div class="modal-body">
                <div class="row ">
                    <div :key="render" class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.color.code.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('ColorDropdown.Code') }}:<span class="text-danger"> *</span></label>
                        <input disabled class="form-control" v-model="v$.color.code.$model" type="text" />
                        <span v-if="v$.color.code.$error" class="error">
                            <span v-if="!v$.color.code.maxLength">{{ $t('ColorDropdown.CodeLength') }}</span>
                        </span>
                    </div>
                    <div v-if="english == 'true'" class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.color.name.$error }">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('ColorDropdown.ColorName')) 
                                    
                            }}: <span class="text-danger"> *</span></label>
                        <input class="form-control" v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.color.name.$model" type="text" />
                        <span v-if="v$.color.name.$error" class="error">
                            <span v-if="!v$.color.name.required">{{ $t('ColorDropdown.NameRequired') }}</span>
                            <span v-if="!v$.color.name.maxLength">{{ $t('ColorDropdown.NameLength') }}</span>
                        </span>
                    </div>

                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.color.nameArabic.$error }">
                        <span v-if="!v$.color.name.maxLength">{{ $t('ColorDropdown.NameLength') }}</span>
                        <label class="text  font-weight-bolder">{{$arabicLanguage($t('ColorDropdown.ColorNameAr')) 
                                    
                            }}: <span class="text-danger"> *</span></label>
                        <input class="form-control text-right " v-model="v$.color.nameArabic.$model" type="text" />
                        <span v-if="v$.color.nameArabic.$error" class="error">
                            <span v-if="!v$.color.nameArabic.required"> {{ $t('ColorDropdown.NameRequired')
                                }}</span>
                            <span v-if="!v$.color.nameArabic.maxLength">{{ $t('ColorDropdown.NameLength') }}</span>
                        </span>
                    </div>

                    <div class="form-group has-label col-sm-12 " v-bind:class="{ 'has-danger': v$.color.description.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('ColorDropdown.Description') }}: </label>
                        <textarea rows="3" class="form-control" v-model="v$.color.description.$model" type="text" />
                        <span v-if="v$.color.description.$error" class="error">{{$t('ColorDropdown.descriptionLength')}}</span>
                        </div>

                        <div class="form-group col-md-4">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox1" v-model="color.isActive">
                                <label for="inlineCheckbox1"> {{ $t('ColorDropdown.Active') }} </label>
                            </div>
                        </div>
                    </div>
                </div>

                <div v-if="!loading">
                    <div class="modal-footer">
                        <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveColor"
                            v-bind:disabled="v$.color.$invalid" v-if="type != 'Edit'">{{$t('ColorDropdown.btnSave')}}</button>
                        <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveColor"
                            v-bind:disabled="v$.color.$invalid" v-if="type == 'Edit'">{{Update}}</button>
                        <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{  $t('ColorDropdown.btnClear')}}</button>
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
import {
    requiredIf,
    maxLength
} from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from 'vue-multiselect'
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';


export default {
    name: 'colordropdown',
    props: ["modelValue", 'disabled', 'isSaleItem', 'isMultiple'],
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
            type: '',
            color: {
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
         color: {
            name: {
                maxLength: maxLength(50)
            },
            nameArabic: {
                required: requiredIf(function () {
                            return !this.color.name;
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
            this.$https.get('/Product/ColorList?isActive=true', {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {

                if (response.data != null) {
                    response.data.results.colors.forEach(function (cat) {
                        if (root.isSaleItem) {
                            root.options.push({
                                id: cat.id,
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != '' && cat.name != null) ? cat.name : cat.nameArabic : (cat.nameArabic != '' && cat.nameArabic != null) ? cat.nameArabic : cat.name
                            })
                        } else {
                            root.options.push({
                                id: cat.id,
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != '' && cat.name != null) ? cat.code + ' ' + cat.name : cat.code + ' ' + cat.nameArabic : (cat.nameArabic != '' && cat.nameArabic != null) ? cat.code + ' ' + cat.nameArabic : cat.code + ' ' + cat.name
                            })
                        }

                    })
                }
            }).then(function () {
                root.value = root.options.find(function (x) {
                    return x.id == root.modelValue;
                })
            });
        },
        AddColor: function (type) {
            this.v$.$reset();
            this.GetAutoCodeGenerator();
            this.color = {
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
            this.$https.get('/Product/ColorCode', {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            }).then(function (response) {

                if (response.data != null) {
                    root.color.code = response.data;
                    root.render++;
                }
            });
        },
        SaveColor: function () {
            var root = this;
            this.loading = true;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.post('/Product/SaveColor', this.color, {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                })
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
                            root.show = false;
                            root.getData();
                        } else {
                            root.getData();
                            root.show = false;
                        }
                    } else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your Color Name  Already Exist!",
                            type: 'error',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        root.getData();
                    }
                }).catch(error => {
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
