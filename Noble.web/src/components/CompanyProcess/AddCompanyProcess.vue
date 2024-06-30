<template>
    <modal :show="show">
        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header" v-if="type=='Edit'">
                            <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('AddCompanyProcess.UpdateProcess') }} </h5>

                        </div>
                        <div class="modal-header" v-else>
                            <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('AddCompanyProcess.AddProcess') }} </h5>
                        </div>
                        <div>
                            <div class="card-body ">
                                <div class="row ">
                                    <div v-if="english=='true'" class="form-group has-label col-sm-12 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddCompanyProcess.ProcessName'))  }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="newProcess.processName" type="text" />
                                    </div>
                                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.newProcess.processNameArabic.$error}">
                                        <label class="text  font-weight-bolder"> {{$arabicLanguage($t('AddCompanyProcess.ProcessName'))  }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.newProcess.processNameArabic.$model" type="text" />
                                        <span v-if="v$.newProcess.processNameArabic.$error" class="error">
                                            <span v-if="!v$.newProcess.processNameArabic.required"> {{ $t('AddCompanyProcess.NameRequired') }}</span>
                                        </span>
                                    </div>
                                    <div class="form-group has-label col-sm-12 " v-bind:class=" ($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                        <label class="text  font-weight-bolder"> {{ $t('AddCompanyProcess.Type') }}:</label>
                                        <multiselect :options="processList" v-model="newProcess.type" :show-labels="false" placeholder="Select Type">
                                        </multiselect>
                                    </div>
                                    <div class="form-group col-md-12" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        <label style="margin: 7px;">{{ $t('AddCompanyProcess.Active') }}</label> <br />
                                        <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': newProcess.status, 'bootstrap-switch-off': !newProcess.status}" v-on:click="newProcess.status = !newProcess.status" style="width: 72px;">
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
                            <div class="modal-footer justify-content-right" v-if="type=='Edit' && isValid('Can Edit Color')">
                                <button type="button" class="btn btn-primary  " v-on:click="SaveProcess" v-bind:disabled="v$.newProcess.$invalid"> {{ $t('AddCompanyProcess.btnUpdate') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('AddCompanyProcess.btnClear') }}</button>
                            </div>
                            <div class="modal-footer justify-content-right" v-if="type!='Edit' && isValid('Can Save Color')">
                                <button type="button" class="btn btn-primary  " v-on:click="SaveProcess" v-bind:disabled="v$.newProcess.$invalid"> {{ $t('AddCompanyProcess.btnSave') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('AddCompanyProcess.btnClear') }}</button>
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
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    import Multiselect from "vue-multiselect";
    import { required, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        props: ['show', 'process', 'type'],
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
                render: 0,
                loading: false,
                processList: [],
                newProcess: this.process
            }
        },
        validations() {
            return {
                newProcess: {
                    processNameArabic: {
                        required: requiredIf(function () {
                            return !this.newProcess.processName;
                        }),
                        // required: requiredIf((x) => {
                        //     if (x.processName == '' || x.processName == null)
                        //         return true;
                        //     return false;
                        // }),
                    },
                    type: {
                        required
                    }
                }
            }
        },
        methods: {
            GetProcessType: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Company/ProcessTypeList', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.processList = response.data;
                        }
                    });
            },
            close: function () {
                this.$emit('close');
            },
            SaveProcess: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Company/SaveProcess', this.newProcess, { headers: { "Authorization": `Bearer ${token}` } })
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
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
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
        mounted: function () {

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.GetProcessType();
        }
    }
</script>
