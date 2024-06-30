<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false"
            v-bind:placeholder="$t('AddDesignation.SelectDesignation')" track-by="name" :clear-on-select="false"
            :show-labels="false" label="name" :preselect-first="true">
            <!--<p slot="noResult" class="text-danger"> Oops! No Designation found.</p>-->
            <template v-slot:noResult>
            <span  class="btn btn-primary " v-on:click="AddDesignation('Add')">{{ $t('AddDesignation.AddDesignation')}}</span><br />
</template>
        </multiselect>
        <modal :show="show" v-if="show">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title m-0" id="exampleModalDefaultLabel">{{ $t('AddDesignation.AddDesignation') }}
                    </h6>
                    <button type="button" class="btn-close" v-on:click="close()"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div :key="render" class="form-group has-label col-sm-12 "
                            v-bind:class="{ 'has-danger': v$.designation.code.$error }">
                            <label class="text  font-weight-bolder"> {{ $t('AddDesignation.Code') }}:<span
                                    class="text-danger"> *</span></label>
                            <input disabled class="form-control"
                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'"
                                v-model="v$.designation.code.$model" type="text" />

                        </div>
                        <div v-if="english == 'true'" class="form-group has-label col-sm-12 "
                            v-bind:class="{ 'has-danger': v$.designation.name.$error }">
                            <label class="text  font-weight-bolder"> {{ $t('AddDesignation.NameEnglish') }}: <span
                                    class="text-danger"> *</span></label>
                            <input class="form-control"
                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'"
                                v-model="v$.designation.name.$model" type="text" />

                        </div>
                        <div v-if="isOtherLang()" class="form-group has-label col-sm-12 "
                            v-bind:class="{ 'has-danger': v$.designation.nameArabic.$error }">
                            <label class="text  font-weight-bolder">{{ $t('AddDesignation.NameArabic') }}: <span
                                    class="text-danger"> *</span></label>
                            <input class="form-control " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"
                                v-model="v$.designation.nameArabic.$model" type="text" />

                        </div>

                        <div class="form-group has-label col-sm-12 "
                             v-bind:class="{ 'has-danger': v$.designation.description.$error }">
                            <label class="text  font-weight-bolder"> {{ $t('AddDesignation.Description') }}: </label>
                            <input class="form-control " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"
                                   v-model="v$.designation.description.$model" type="text" />
                        </div>


                        <div class="form-group col-md-4">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox1" v-model="designation.isActive">
                                <label for="inlineCheckbox1"> {{ $t('AddDesignation.Active') }} </label>
                            </div>
                        </div>


                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveDesignation"
                        v-bind:disabled="v$.designation.$invalid" v-if="type != 'Edit'">{{ $t('AddDesignation.Save')
                        }}</button>
                    <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveDesignation"
                        v-bind:disabled="v$.designation.$invalid" v-if="type == 'Edit'">{{ $t('AddDesignation.Update')
                        }}</button>
                    <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{  $t('AddDesignation.Cancel')}}</button>
                </div>
                <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
            </div>

        </modal>
    </div>
</template>
<script>
import { requiredIf, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from 'vue-multiselect'
   import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

export default {
    name: 'designationdropdown',
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
            designation: {
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
         designation: {
            name: {
                maxLength: maxLength(50)
            },
            nameArabic: {
                required: requiredIf(function () {
                            return !this.designation.name;
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
        getData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/EmployeeRegistration/DesignationList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    response.data.results.designations.forEach(function (cat) {
                        root.options.push({
                            id: cat.id,
                            name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != '' && cat.name != null) ? cat.code + ' ' + cat.name : cat.code + ' ' + cat.nameArabic : (cat.nameArabic != '' && cat.nameArabic != null) ? cat.code + ' ' + cat.nameArabic : cat.code + ' ' + cat.name
                        })
                    })
                }
            }).then(function () {
                root.value = root.options.find(function (x) {
                    return x.id == root.modelValue;
                })
            });
        },
        AddDesignation: function (type) {
            this.v$.$reset();
            this.GetAutoCodeGenerator();
            this.designation = {
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
            this.$https.get('/EmployeeRegistration/DesignationCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    root.designation.code = response.data;
                    root.render++;
                }
            });
        },
        SaveDesignation: function () {
            var root = this;
            this.loading = true;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.post('/EmployeeRegistration/SaveDesignation', this.designation, { headers: { "Authorization": `Bearer ${token}` } })
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
                            root.options = [];
                            root.getData();
                            root.show = false;
                        }
                        else {

                            root.$swal({
                               title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: "Your Designation " + response.data.designation.name + " has been updated!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                            root.show = false;
                            root.options = [];
                            root.getData();
                        }
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your Designation Name  Already Exist!",
                            type: 'error',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
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
                if (value == null) {
                    this.$emit('update:modelValue', '');
                }
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