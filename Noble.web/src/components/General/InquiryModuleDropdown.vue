<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :disabled="disabled" :multiple="false" v-bind:placeholder="$t('InquirySetupDropdown.SelectInquirySetup')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <!--<p slot="noResult" class="text-danger"> Oops! No Color found.</p>-->
            <template v-slot:noResult>
            <span  class="btn btn-primary " v-on:click="AddInquiryModule('Add')">{{ $t('InquirySetupDropdown.CreateInquirySetup') }}</span><br />
</template>
        </multiselect>

        <modal :show="show" v-if="show">

            <div style="margin-bottom:0px" class="card">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="tab-content" id="nav-tabContent">
                            <div class="modal-header" v-if="type=='Edit'">

                                <h5 class="modal-title DayHeading" id="myModalLabel"> {{ $t('addInquirySetup.UpdateInquirySetup') }}</h5>

                            </div>
                            <div class="modal-header" v-else>

                                <h5 class="modal-title DayHeading" id="myModalLabel"> {{ $t('addInquirySetup.CreateInquirySetup') }}</h5>

                            </div>
                            <div class="">
                                <div class="card-body">
                                    <div class="row ">
                                        <div :key="render" class="form-group has-label col-sm-12 ">
                                            <label class="text  font-weight-bolder"> {{ $t('addInquirySetup.Code') }}:<span class="text-danger"> *</span></label>
                                            <input disabled class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="module.code" type="text" />

                                        </div>
                                        <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.module.name.$error}">
                                            <label class="text  font-weight-bolder">{{ $t('addInquirySetup.Name') }}: <span class="text-danger"> *</span></label>
                                            <input class="form-control" v-model="v$.module.name.$model" type="text" />
                                            <span v-if="v$.module.name.$error" class="error">
                                                <span v-if="!v$.module.name.required"> {{ $t('addInquirySetup.NameRequired') }}</span>
                                            </span>
                                        </div>
                                        <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.module.label.$error}">
                                            <label class="text  font-weight-bolder">{{ $t('addInquirySetup.Label') }}: <span class="text-danger"> *</span></label>
                                            <input class="form-control"  v-model="v$.module.label.$model" type="text" />
                                            <span v-if="v$.module.label.$error" class="error">
                                                <span v-if="!v$.module.label.required"> {{ $t('addInquirySetup.LabelRequired') }}</span>
                                            </span>
                                        </div>


                                        <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.module.description.$error}">
                                            <label class="text  font-weight-bolder"> {{ $t('addInquirySetup.Description') }}: </label>
                                            <textarea class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.module.description.$model" type="text" />
                                            <span v-if="v$.module.description.$error" class="error">{{ $t('addInquirySetup.descriptionLength') }}</span>
                                        </div>
                                        <div class="form-group col-md-12">
                                            <label style="margin: 7px;">{{ $t('addInquirySetup.Active') }}</label> <br />
                                            <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': module.isActive, 'bootstrap-switch-off': !module.isActive}" v-on:click="module.isActive = !module.isActive" style="width: 72px;">
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
                                    <button type="button" class="btn btn-primary  " v-on:click="SaveModule" v-bind:disabled="v$.module.$invalid"> {{ $t('addInquirySetup.btnUpdate') }}</button>
                                    <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('addInquirySetup.btnClear') }}</button>
                                </div>
                                <div class="modal-footer justify-content-right" v-if="type!='Edit' && isValid('CanAddBrand')">
                                    <button type="button" class="btn btn-primary  " v-on:click="SaveModule" v-bind:disabled="v$.module.$invalid"> {{ $t('addInquirySetup.btnSave') }}</button>
                                    <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('addInquirySetup.btnClear') }}</button>
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
                module: {
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
                module: {
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
            GetmoduleData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Project/InquiryModuleList?isActive=' + true, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        response.data.results.inquiryModuleLookUp.forEach(function (module) {
                            root.options.push({
                                id: module.id,
                                name: module.name
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
            AddInquiryModule: function (type) {
                this.v$.$reset();
                this.GetAutoCodeGenerator();
                this.newmodule = {
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
                this.$https.get('/Project/InquiryModuleCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.module.code = response.data;
                        root.render++;
                    }
                });
            },
            SaveModule: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Project/SaveInquiryModule', this.module, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {
                            root.options.push({
                                id: root.module.id,
                                name: root.module.name
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
                                text: "Your Module Name  Already Exist!",
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
                    this.$emit('update:modelValue', value.id);
                }
            }
        },
        mounted: function () {
            //this.english = localStorage.getItem('English');
            //this.arabic = localStorage.getItem('Arabic');
            this.GetmoduleData();
        },
    }
</script>