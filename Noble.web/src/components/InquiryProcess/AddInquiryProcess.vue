<template >
    <modal :show="show" v-if=" isValid('CanAddInquiryProcess') || isValid('CanEditInquiryProcess') ">

        <div style="margin-bottom:0px" class="card">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'"> {{ $t('addInquiryProcess.UpdateInquiryProcess') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('addInquiryProcess.CreateInquiryProcess') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                       
                        <div class="">
                            <div class="card-body">
                                <div class="row ">
                                    <div :key="render" class="form-group has-label col-sm-12 ">
                                        <label class="text  font-weight-bolder"> {{ $t('addInquiryProcess.Code') }}:<span class="text-danger"> *</span></label>
                                        <input disabled class="form-control" v-model="process.code" type="text" />

                                    </div>
                                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.process.name.$error}">
                                        <label class="text  font-weight-bolder"> {{ $t('addInquiryProcess.ProcessName') }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control" v-model="v$.process.name.$model" type="text" />
                                        <span v-if="v$.process.name.$error" class="error">
                                            <span v-if="!v$.process.name.required">{{ $t('addInquiryProcess.NameRequired') }}</span>
                                        </span>
                                    </div>
                                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.process.label.$error}">
                                        <label class="text  font-weight-bolder"> {{ $t('addInquiryProcess.Label') }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control" v-model="v$.process.label.$model" type="text" />
                                        <span v-if="v$.process.label.$error" class="error">
                                            <span v-if="!v$.process.label.required">{{ $t('addInquiryProcess.LabelRequired') }}</span>
                                        </span>
                                    </div>


                                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.process.description.$error}">
                                        <label class="text  font-weight-bolder"> {{ $t('addInquiryProcess.Description') }}: </label>
                                        <textarea class="form-control" v-model="v$.process.description.$model" type="text" />
                                        <span v-if="v$.process.description.$error" class="error">{{ $t('addInquiryProcess.descriptionLength') }}</span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <div class="checkbox form-check-inline mx-2">
                                            <input type="checkbox" id="inlineCheckbox1" v-model="process.isActive">
                                            <label for="inlineCheckbox1">{{ $t('addInquiryProcess.Active') }}</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div v-if="!loading">
                            <div class="modal-footer justify-content-right" v-if="type=='Edit' && isValid('CanEditInquiryProcess')">
                                <button type="button" class="btn btn-soft-primary btn-sm " v-on:click="SaveProcess" v-bind:disabled="v$.process.$invalid"> {{ $t('addInquiryProcess.btnUpdate') }}</button>
                                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('addInquiryProcess.btnClear') }}</button>
                            </div>
                            <div class="modal-footer justify-content-right" v-if="type!='Edit' && isValid('CanAddInquiryProcess')">
                                <button type="button" class="btn btn-soft-primary btn-sm " v-on:click="SaveProcess" v-bind:disabled="v$.process.$invalid"> {{ $t('addInquiryProcess.btnSave') }}</button>
                                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('addInquiryProcess.btnClear') }}</button>
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
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import { maxLength, required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
        mixins: [clickMixin],
        props: ['show', 'newprocess', 'type'],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                process: this.newprocess,
                arabic: '',
                english: '',
                render: 0,
                loading: false,
            }
        },
        validations() {
            return {
                process: {
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
            close: function () {
                this.$emit('close');
            },
            GetAutoCodeGenerator: function () {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Project/InquiryProcessCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data != null) {
                        root.process.code = response.data;
                        root.render++;
                    }
                });
            },
            SaveProcess: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Project/SaveInquiryProcess', this.process, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                    if (response.data.isSuccess == true) {
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
                            text: "Your Process Name  Already Exist!",
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
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.process.id == '00000000-0000-0000-0000-000000000000' || this.process.id == undefined || this.process.id == '')
                this.GetAutoCodeGenerator();

        }
    }
</script>
