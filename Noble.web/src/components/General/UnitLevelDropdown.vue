<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false"
            v-bind:placeholder="$t('UnitLevelDropdown.SelectUnit')" track-by="name" :clear-on-select="false"
            :show-labels="false" label="name" :preselect-first="true"
            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left ' : 'arabicLanguage '">
            <!--<p slot="noResult" class="text-danger"> Oops! No Unit found.</p>-->
            <template v-slot:noResult>
                <a  class="btn btn-primary " v-on:click="AddUnit('Add')">{{ $t('UnitLevelDropdown.AddProductUnit') }}</a><br />
            </template>
        </multiselect>
        <modal :show="show" v-if="show">

            <div class="modal-content">
                <div class="modal-header" v-if="type == 'Edit'">
                    <h5 class="modal-title" id="myModalLabel"> {{ $t('UnitLevelDropdown.UpdateUnit') }}</h5>
                </div>
                <div class="modal-header" v-else>
                    <h5 class="modal-title" id="myModalLabel"> {{ $t('UnitLevelDropdown.AddProductUnit') }}</h5>
                </div>

                <div class="modal-body">
                    <div class="row ">
                        <div :key="render" class="form-group has-label col-sm-12 "
                            v-bind:class="{ 'has-danger': v$.unit.code.$error }">
                            <label class="text  font-weight-bolder"> {{ $t('UnitLevelDropdown.Code') }}:<span
                                    class="text-danger"> *</span></label>
                            <input disabled class="form-control" v-model="v$.unit.code.$model"
                               
                                type="text" />
                            <span v-if="v$.unit.code.$error" class="error">
                                <span v-if="!v$.unit.code.maxLength">{{ $t('UnitLevelDropdown.CodeLength') }}</span>
                            </span>
                        </div>
                        <div v-if="english == 'true'" class="form-group has-label col-sm-12 "
                            v-bind:class="{ 'has-danger': v$.unit.name.$error }">
                            <label class="text  font-weight-bolder"> {{$englishLanguage($t('UnitLevelDropdown.UnitName')) 
                                
                            }}: <span class="text-danger"> *</span></label>
                            <input class="form-control" v-model="v$.unit.name.$model"
                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'"
                                type="text" />
                            <span v-if="v$.unit.name.$error" class="error">
                                <span v-if="!v$.unit.name.required">{{ $t('UnitLevelDropdown.NameRequired') }}</span>
                                <span v-if="!v$.unit.name.maxLength">{{ $t('UnitLevelDropdown.NameLength') }}</span>
                            </span>
                        </div>
                        <div v-if="isOtherLang()" class="form-group has-label col-sm-12 "
                            v-bind:class="{ 'has-danger': v$.unit.nameArabic.$error }">
                            <label class="text  font-weight-bolder"> {{$arabicLanguage($t('UnitLevelDropdown.UnitNameAr')) 
                                    
                            }}: <span class="text-danger"> *</span></label>
                            <input class="form-control " v-model="v$.unit.nameArabic.$model"
                                v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" type="text" />
                            <span v-if="v$.unit.nameArabic.$error" class="error">
                                <span v-if="!v$.unit.nameArabic.required">{{ $t('UnitLevelDropdown.NameRequired')
                                }}</span>
                                <span v-if="!v$.unit.nameArabic.maxLength">{{ $t('UnitLevelDropdown.NameLength')
                                }}</span>
                            </span>
                        </div>

                        <div class="form-group has-label col-sm-12 "
                            v-bind:class="{ 'has-danger': v$.unit.description.$error }">
                            <label class="text  font-weight-bolder"> {{ $t('UnitLevelDropdown.Description') }}: </label>
                            <textarea rows="3" class="form-control" v-model="v$.unit.description.$model"
                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'"
                                type="text" />
                            <span v-if="v$.unit.description.$error" class="error">{{
                                    $t('UnitLevelDropdown.descriptionLength')
                            }}</span>
                        </div>

                        <div class="form-group col-md-4">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox1" v-model="unit.isActive">
                                <label for="inlineCheckbox1"> {{ $t('UnitLevelDropdown.Active')  }} </label>
                            </div>
                        </div>
                    </div>

                </div>
                <div v-if="!loading">
                    <div class="modal-footer">
                        <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveUnit"
                            v-bind:disabled="v$.unit.$invalid" v-if="type != 'Edit'">{{
                                    $t('UnitLevelDropdown.btnSave')
                            }}</button>
                        <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveUnit"
                            v-bind:disabled="v$.unit.$invalid" v-if="type == 'Edit'">{{
                                   Update
                            }}</button>
                        <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{
                                $t('UnitLevelDropdown.btnClear')
                        }}</button>
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
import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
import Multiselect from 'vue-multiselect'
import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

export default {
    name: 'unitdropdown',
    props: ["modelValue"],
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
            unit: {
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
         unit: {
            name: {
                maxLength: maxLength(20)
            },
            nameArabic: {
                required: requiredIf(function () {
                            return !this.unit.name;
                        }),

                // required: requiredIf((x) => {
                //     if (x.name == '' || x.name == null)
                //         return true;
                //     return false;
                // }),
                maxLength: maxLength(20)
            },
            code: { required },
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

            this.$https.get('/Product/UnitList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {


                if (response.data != null) {

                    response.data.results.units.forEach(function (cat) {

                        root.options.push({
                            id: cat.id,
                            name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != '' && cat.name != null) ? cat.code + ' ' + cat.name : cat.code + ' ' + cat.nameArabic : (cat.nameArabic != '' && cat.nameArabic != null) ? cat.code + ' ' + cat.nameArabic : cat.code + ' ' + cat.name
                        })
                    })
                }
            }).then(function () {
                root.value = root.options.find(function (x) {
                    return x.name == root.modelValue;
                })
            });
        },
        AddUnit: function (type) {
            this.v$.$reset();
            this.GetAutoCodeGenerator();
            this.unit = {
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
            this.$https.get('/Product/UnitCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    // eslint-disable-line
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
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {
                            
                            root.loading = false;
                           
                            root.$swal({
                                icon: 'success',
                                title: 'Saved Successfully!',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.show = false;
                            root.getData();
                        }
                        else {
                           
                            root.$swal({
                               title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: "Your Unit " + response.data.unit.name + " has been updated!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,

                            });
                            root.close();
                            root.getData();
                        }
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your Unit Name  Already Exist!",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,

                        });
                        root.getData();
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
                this.$emit('update:modelValue', value.name);
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