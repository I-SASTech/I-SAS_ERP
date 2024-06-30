<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false"
            v-bind:placeholder="$t('SubCategoryDropdown.PleaseSelectSubCategory')" track-by="dropDownName"
            :clear-on-select="false" :show-labels="false" label="dropDownName" :preselect-first="true"
            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left ' : 'arabicLanguage '">
            <!--<p slot="noResult" class="text-danger"> Oops! No SubCategory found.</p>-->
         <template v-slot:noResult>
            <a  class="btn btn-primary " v-on:click="AddSubCategory('Add')"
                v-if="isValid('CanAddSubCategory')">{{ $t('SubCategoryDropdown.AddSubCategory') }}</a><br />
            </template>
        </multiselect>
        <modal :show="show" v-if="show">

            <div class="modal-content">
                <div class="modal-header">
                    <h5>{{ $t('SubCategoryDropdown.AddSubCategory') }}</h5>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <label>{{ $t('SubCategoryDropdown.Code') }} :<span class="text-danger"> *</span></label>
                            <div v-bind:class="{ 'has-danger': v$.subCategory.code.$error }">
                                <input readonly class="form-control" v-model="v$.subCategory.code.$model" />
                                <span v-if="v$.subCategory.code.$error" class="error text-danger">
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-12 form-group">
                            <label>{{ $t('SubCategoryDropdown.SelectCategory') }} :<span class="text-danger">
                                    *</span></label>
                            <div>
                                <categorydropdown v-model="v$.subCategory.categoryId.$model"
                                    v-bind:modelValue="subCategory.categoryId" :key="rendered"></categorydropdown>
                            </div>
                        </div>

                        <div v-if="english == 'true'" class="col-sm-12 form-group">
                            <label>{{$englishLanguage($t('SubCategoryDropdown.SubCategoryName'))   }} :<span
                                    class="text-danger"> *</span></label>
                            <div v-bind:class="{ 'has-danger': v$.subCategory.name.$error }">
                                <input class="form-control "
                                    
                                    v-model="v$.subCategory.name.$model" />
                                <span v-if="v$.subCategory.name.$error" class="error text-danger">
                                    <span v-if="!v$.subCategory.name.required"> {{$t('SubCategoryDropdown.NameRequired')}}</span>
                                    <span v-if="!v$.subCategory.name.maxLength">{{$t('SubCategoryDropdown.NameLength')}}</span>
                                </span>
                            </div>
                        </div>
                        <div v-if="isOtherLang()" class="has-label col-sm-12 form-group "
                            v-bind:class="{ 'has-danger': v$.subCategory.nameArabic.$error }">
                            <label class="text  font-weight-bolder"> {{$arabicLanguage($t('SubCategoryDropdown.SubCategoryNameAr')) 
                                    
                            }}: <span class="text-danger"> *</span></label>
                            <input class="form-control " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"
                                v-model="v$.subCategory.nameArabic.$model" type="text" />
                            <span v-if="v$.subCategory.nameArabic.$error" class="error">
                                <span v-if="!v$.subCategory.nameArabic.required"> {{
                                        $t('SubCategoryDropdown.NameRequired')
                                }}</span>
                                <span v-if="!v$.subCategory.nameArabic.maxLength">{{
                                        $t('SubCategoryDropdown.NameLength')
                                }}</span>
                            </span>
                        </div>
                        <div class="col-sm-12 form-group">
                            <label>{{ $t('SubCategoryDropdown.Description') }} :</label>
                            <div v-bind:class="{ 'has-danger': v$.subCategory.description.$error }">
                                <textarea rows="3" class="form-control" v-model="v$.subCategory.description.$model" />
                                <span v-if="v$.subCategory.description.$error" class="error text-danger">

                                </span>
                            </div>
                        </div>
                    </div>
                </div>

                <div v-if="!loading" class=" col-md-12">
                    <button class="btn btn-soft-primary btn-sm" v-bind:disabled="v$.subCategory.$invalid"
                        v-on:click="SaveSubCategory">{{ $t('SubCategoryDropdown.btnSave') }}</button>
                    <button class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('SubCategoryDropdown.btnClear')
                    }}</button>
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
import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
import clickMixin from '@/Mixins/clickMixin'
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';

export default {
    mixins: [clickMixin],
    name: 'subcategorydropdown',
    props: ["modelValue", "categoryId"],

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
            rendered: 0,
            subCategory: {
                id: '00000000-0000-0000-0000-000000000000',
                code: '',
                name: '',
                nameArabic: '',
                description: '',
                categoryId: this.categoryId,
                isActive: true
            },
            loading: false,
            render: 0
        }
    },
    validations() {
       return{
         subCategory:
        {
            code: {
                required
            },
            name: {
                maxLength: maxLength(50)
            },
            nameArabic: {
                required: requiredIf(function () {
                            return !this.subCategory.name;
                        }),
                // required: requiredIf((x) => {
                //     if (x.name == '' || x.name == null)
                //         return true;
                //     return false;
                // }),
                maxLength: maxLength(50)
            },
            categoryId: {
                required
            },
            description: {}
        }
       }
    },
    methods: {
        getData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            this.$https.get('/Product/GetSubCategoryInformation?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {


                if (response.data != null) {
                    response.data.subCategories.forEach(function (cat) {

                        root.options.push({
                            id: cat.id,
                            dropDownName: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != '' ? cat.code + ' ' + cat.name : cat.code + ' ' + cat.nameArabic) : (cat.nameArabic != '' ? cat.code + ' ' + cat.nameArabic : cat.code + ' ' + cat.name),
                            name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != '' ? cat.name : cat.nameArabic) : (cat.nameArabic != '' ? cat.nameArabic : cat.name)

                        })
                    })
                }
            }).then(function () {
                root.value = root.options.find(function (x) {

                    return x.id == root.modelValue;
                })
            });
        },
        getCategoryBaseData: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            // eslint-disable-line
            this.$https.get('/Product/GetSubCategoryInformation?categoryId=' + id + '&isActive=' + true, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                // eslint-disable-line
                if (response.data != null) {

                    response.data.results.subCategories.forEach(function (scat) {

                        root.options.push({
                            id: scat.id,
                            dropDownName: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (scat.name != '' ? scat.code + ' ' + scat.name : scat.code + ' ' + scat.nameArabic) : (scat.nameArabic != '' ? scat.code + ' ' + scat.nameArabic : scat.code + ' ' + scat.name),
                            name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (scat.name != '' ? scat.name : scat.nameArabic) : (scat.nameArabic != '' ? scat.nameArabic : scat.name)
                        })
                    })
                }
                else {
                    root.options.push({
                        id: '',
                        name: ''
                    })
                }
            }).then(function () {
                root.value = root.options.find(function (x) {

                    return x.id == root.modelValue;
                })
            });
        },
        AddSubCategory: function (type) {
            this.v$.$reset();
            this.AutoIncrementCode();
            this.subCategory = {
                id: '00000000-0000-0000-0000-000000000000',
                code: '',
                name: '',
                nameArabic: '',
                description: '',
                categoryId: this.categoryId,
                isActive: true
            }

            this.show = !this.show;
            this.type = type;
            this.rendered++;
        },
        close: function () {
            this.show = false;
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
                            
                            root.show = false;
                            root.getData();
                            root.$swal({
                                icon: 'success',
                                title: 'Saved Successfully!',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });

                        }
                        else {
                            
                            root.$swal({
                               title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                icon: 'success',
                                text: "Your Size " + response.data.subCategory.name + " has been updated!",
                                type: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                            
                            root.close();
                            root.getData();
                        }
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            icon: 'error',
                            text: "Your Subcategory Already Exist!",
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        root.getData();
                    }

                }).catch(error => {
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
                .finally(() => root.loading = false)
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
                        root.render++
                    }
                });
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
                this.$emit('update:modelValue', value.id);
            }
        }
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        if (this.categoryId != null) {
            // eslint-disable-line
            this.getCategoryBaseData(this.categoryId);
        }
    },
}
</script>