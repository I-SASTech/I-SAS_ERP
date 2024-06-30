<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :disabled="disabled" :multiple="false" v-bind:placeholder="$t('InquiryTypeDropdown.SelectType')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <!--<p slot="noResult" class="text-danger"> Oops! No Color found.</p>-->
            <template v-slot:noResult>
            <span  class="btn btn-primary " v-on:click="AddInquiryType('Add')">{{ $t('InquiryTypeDropdown.CreateInquiryType') }}</span><br />
</template>
        </multiselect>

        <modal :show="show" v-if="show">

            <div style="margin-bottom:0px" class="card">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="tab-content" id="nav-tabContent">
                            <div class="modal-header" v-if="type=='Edit'">

                                <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('addInquiryType.UpdateInquiryType') }}</h5>

                            </div>
                            <div class="modal-header" v-else>

                                <h5 class="modal-title DayHeading" id="myModalLabel"> {{ $t('addInquiryType.CreateInquiryType') }}</h5>

                            </div>
                            <div class="">
                                <div class="card-body">
                                    <div class="row ">
                                        <div :key="render" class="form-group has-label col-sm-12 ">
                                            <label class="text  font-weight-bolder"> {{ $t('addInquiryType.Code') }}:<span class="text-danger"> *</span></label>
                                            <input disabled class="form-control"  v-model="inquiryType.code" type="text" />

                                        </div>
                                        <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.inquiryType.name.$error}">
                                            <label class="text  font-weight-bolder"> {{ $t('addInquiryType.TypeName') }}: <span class="text-danger"> *</span></label>
                                            <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.inquiryType.name.$model" type="text" />
                                            <span v-if="v$.inquiryType.name.$error" class="error">
                                                <span v-if="!v$.inquiryType.name.required">{{ $t('addInquiryType.NameRequired') }}</span>
                                            </span>
                                        </div>
                                        <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.inquiryType.label.$error}">
                                            <label class="text  font-weight-bolder"> {{ $t('addInquiryType.Label') }}: <span class="text-danger"> *</span></label>
                                            <input class="form-control"  v-model="v$.inquiryType.label.$model" type="text" />
                                            <span v-if="v$.inquiryType.label.$error" class="error">
                                                <span v-if="!v$.inquiryType.label.required">{{ $t('addInquiryType.LabelRequired') }}</span>
                                            </span>
                                        </div>


                                        <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.inquiryType.description.$error}">
                                            <label class="text  font-weight-bolder"> {{ $t('addInquiryType.Description') }}: </label>
                                            <textarea class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.inquiryType.description.$model" type="text" />
                                            <span v-if="v$.inquiryType.description.$error" class="error">{{ $t('addInquiryType.descriptionLength') }}</span>
                                        </div>
                                        <div class="form-group col-md-12">
                                            <label style="margin: 7px;">{{ $t('addInquiryType.Active') }}</label> <br />
                                            <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': inquiryType.isActive, 'bootstrap-switch-off': !inquiryType.isActive}" v-on:click="inquiryType.isActive = !inquiryType.isActive" style="width: 72px;">
                                                <div class="bootstrap-switch-container" style="width: 122px; margin-left: 0px;">
                                                    <span class="bootstrap-switch-handle-on bootstrap-switch-success" style="width: 50px;">
                                                        <i class="nc-icon nc-check-2"></i>
                                                    </span>
                                                    <span class="bootstrap-switch-label" style="width: 30px;">&nbsp;</span>
                                                    <span class="bootstrap-switch-handle-off bootstrap-switch-success" style="width: 50px;">
                                                        <i class="nc-icon nc-simple-remove"></i>
                                                    </span>
                                                    <input class="bootstrap-switch" type="checkbox" data-toggle="switch" checked="" data-on-label="<i class='nc-icon nc-check-2'></i>" data-off-label="<i class='nc-icon nc-simple-remove'></i>" data-on-color="success" data-off-color="success">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div v-if="!loading">
                                <div class="modal-footer justify-content-right" v-if="type=='Edit' && isValid('CanEditBrand')">
                                    <button type="button" class="btn btn-primary  " v-on:click="SaveType" v-bind:disabled="v$.inquiryType.$invalid"> {{ $t('addInquiryType.btnUpdate') }}</button>
                                    <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('addInquiryType.btnClear') }}</button>
                                </div>
                                <div class="modal-footer justify-content-right" v-if="type!='Edit' && isValid('CanAddBrand')">
                                    <button type="button" class="btn btn-primary  " v-on:click="SaveType" v-bind:disabled="v$.inquiryType.$invalid"> {{ $t('addInquiryType.btnSave') }}</button>
                                    <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('addInquiryType.btnClear') }}</button>
                                </div>
                            </div>
                            <div v-else>
                                <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </modal>
    </div>
</template>
<script>
    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
        name: 'colordropdown',
        props: ["modelValue", 'disabled'],
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
                inquiryType: {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    label: '',
                    description: '',
                    code: '',
                    isActive: true
                },
                render: 0,
                loading: false,
            }
        },
        validations() {
           return{
             inquiryType: {
                name: {
                    required
                },
                label: {
                    required
                },
                description: {
                    maxLength: maxLength(200)
                }
            }
           }
        },
        methods: {
            GetinquiryTypeData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Project/InquiryTypeList?isActive=' + true, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        response.data.results.inquiryTypeLookUp.forEach(function (inquiryType) {
                            root.options.push({
                                id: inquiryType.id,
                                name: inquiryType.name
                            });
                        })
                    }
                    root.loading = false;
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                });
            },
            AddInquiryType: function (type) {
                this.v$.$reset();
                this.GetAutoCodeGenerator();
                this.newinquiryType = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    label: '',
                    code: '',
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
                this.$https.get('/Project/InquiryTypeCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.inquiryType.code = response.data;
                        root.render++;
                    }
                });
            },
            SaveType: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Project/SaveInquiryType', this.inquiryType, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {
                            root.options.push({
                                id: root.inquiryType.id,
                                name: root.inquiryType.name
                            });
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
                                text: "Your Type Name  Already Exist!",
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
                    if (value === undefined || value === null) {
                        this.$emit('update:modelValue', "00000000-0000-0000-0000-000000000000");
                    }
                    else {
                        this.$emit('update:modelValue', value.id);
                    }
                    
                }
            }
        },
        mounted: function () {
            //this.english = localStorage.getItem('English');
            //this.arabic = localStorage.getItem('Arabic');
            this.GetinquiryTypeData();
        },
    }
</script>