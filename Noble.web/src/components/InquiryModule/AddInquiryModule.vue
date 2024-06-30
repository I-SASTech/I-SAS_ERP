<template>
    <modal :show="show" :modalLarge="true" v-if=" isValid('CanAddInquirySetup') || isValid('CanEditInquirySetup') ">

        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'">{{ $t('addInquirySetup.UpdateInquirySetup') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('addInquirySetup.CreateInquirySetup') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                       
                        <div class="">
                            <div class="card-body">
                                <div class="row ">
                                    <div :key="render" class="form-group has-label col-sm-4 ">
                                        <label class="text  font-weight-bolder"> {{ $t('addInquirySetup.Code') }}:<span class="text-danger"> *</span></label>
                                        <input disabled class="form-control" v-model="module.code" type="text" />

                                    </div>
                                    <div class="form-group has-label col-sm-4 " v-bind:class="{'has-danger' : v$.module.name.$error}">
                                        <label class="text  font-weight-bolder">{{ $t('addInquirySetup.Name') }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control" v-model="v$.module.name.$model" type="text" />
                                        <span v-if="v$.module.name.$error" class="error">
                                            <span v-if="!v$.module.name.required"> {{ $t('addInquirySetup.NameRequired') }}</span>
                                        </span>
                                    </div>
                                    <div class="form-group has-label col-sm-4 " v-bind:class="{'has-danger' : v$.module.label.$error}">
                                        <label class="text  font-weight-bolder">{{ $t('addInquirySetup.Label') }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control" v-model="v$.module.label.$model" type="text" />
                                        <span v-if="v$.module.label.$error" class="error">
                                            <span v-if="!v$.module.label.required"> {{ $t('addInquirySetup.LabelRequired') }}</span>
                                        </span>
                                    </div>
                                    <!--<div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.module.description.$error}">
                                <label class="text  font-weight-bolder"> {{ $t('addInquirySetup.Description') }}: </label>
                                <textarea class="form-control"  v-model="v$.module.description.$model" type="text" />
                                <span v-if="v$.module.description.$error" class="error">{{ $t('addInquirySetup.descriptionLength') }}</span>
                            </div>-->
                                    <div class="form-group has-label col-sm-11">
                                        <label class="text  font-weight-bolder">{{ $t('addInquirySetup.Question') }} : </label>
                                        <input class="form-control" v-model="question" type="text" />

                                    </div>
                                    <div class="form-group has-label col-sm-1 mt-3">
                                        <button title="Answer" @click="add(k)"
                                                class="btn btn-secondary  btn-sm btn-round btn-icon ">
                                            <i data-v-59af708c="" class="dripicons-checkmark tick-mark"></i>
                                        </button>

                                    </div>

                                    <div class="form-group col-6" v-for="(input,k) in answers" :key="k">
                                        <label class="text  font-weight-bolder">{{ $t('addInquirySetup.Ans') }} {{k+1}}: </label>
                                        <div class="input-group">

                                            <input type="text" class="form-control" v-model="input.answer">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" v-bind:style="$i18n.locale == 'en' ? 'padding:5px 5px 5px 0px;' : 'padding: 5px 0px 5px 5px;' "> <i class="fas fa-minus-circle" @click="remove(k)" v-show="k || ( !k && answers.length > 0)"></i></span>
                                                <!--<span class="input-group-text" v-bind:style="$i18n.locale == 'en' ? 'padding: 5px 5px 5px 0px;' : 'padding: 5px 0px 5px 5px;' "> <i class="fas fa-plus-circle" @click="add(k)" v-show="k == answers.length-1"></i></span>-->
                                            </div>
                                        </div>

                                    </div>


                                    <div class=" col-sm-12 ">
                                        <button type="button" class="btn btn-primary btn-sm "  v-on:click="AddQuestionToList" v-bind:disabled="question == ''">{{$t('addInquirySetup.AddQuestionAnswer')}}</button>

                                    </div>
                                    <div class=" col-sm-6 " v-for="(ques,index) in module.moduleQuestionLookUps" :key="ques">
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <p style="margin:0px !important;padding:0px !important">
                                                    Q{{index+1}}:{{ques.question}}

                                                </p>

                                            </div>
                                            <div class="col-lg-2 " style="margin:0px !important;padding:0px !important">
                                                <span style="margin:0px !important;padding:0px !important">
                                                    <button @click="RemoveQuestionFromList(ques.id)"
                                                            title="Remove Item"
                                                            style="margin:0px !important;padding:0px !important"
                                                            class="btn   btn-sm   "
                                                            v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'float-right' : 'float-left'">
                                                        <i data-v-59af708c="" class="las la-trash-alt text-danger font-16"></i>
                                                    </button>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="row" v-for="(ans,i) in ques.answerLookUps" :key="ans">
                                            <div class="col-lg-10">
                                                <p style="margin:0px !important;" v-bind:style="$i18n.locale == 'en' ? 'padding:0px 0px 0px 8px;' : 'padding: 0px 8px 0px 0px;' ">
                                                    A{{i+1}}):{{ans.answer}}

                                                </p>

                                            </div>
                                            <div class="col-lg-2 " style="margin:0px !important;padding:0px !important">
                                                <span style="margin:0px !important;padding:0px !important">
                                                    <button @click="RemoveAnswerFromList(ques.id, ans.id)"
                                                            title="Remove Item"
                                                            class="btn   btn-sm   "
                                                            style="margin:0px !important;padding:0px !important">
                                                        <i data-v-59af708c="" class="las la-trash-alt text-danger font-16"></i>
                                                    </button>
                                                </span>
                                            </div>
                                        </div>

                                        <hr />
                                    </div>
                                    <div class=" col-sm-12 ">
                                        <div class="row">
                                            <div class="form-group col-md-4">
                                                <label style="margin: 7px;">{{ $t('addInquirySetup.Compulsory') }}</label> <br />
                                                <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': module.compulsory, 'bootstrap-switch-off': !module.compulsory}" v-on:click="module.compulsory = !module.compulsory" style="width: 72px;">
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

                                            <div class="form-group col-md-4">
                                                <label style="margin: 7px;">{{ $t('addInquirySetup.AttachmentCompulsory') }}</label> <br />
                                                <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': module.attachmentCompulsory, 'bootstrap-switch-off': !module.attachmentCompulsory}" v-on:click="module.attachmentCompulsory = !module.attachmentCompulsory" style="width: 72px;">
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
                                            <div class="form-group col-md-4">
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
                            </div>
                        </div>
                        <div v-if="!loading">
                            <div class="modal-footer justify-content-right" v-if="type=='Edit' && isValid('CanEditInquirySetup')">
                                <button type="button" class="btn btn-primary  " v-on:click="SaveModule" v-bind:disabled="v$.module.$invalid"> {{ $t('addInquirySetup.btnUpdate') }}</button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('addInquirySetup.btnClear') }}</button>
                            </div>
                            <div class="modal-footer justify-content-right" v-if="type!='Edit' && isValid('CanAddInquirySetup')">
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
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import { maxLength, required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
        mixins: [clickMixin],
        props: ['show', 'newmodule', 'type'],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                module: this.newmodule,
                arabic: '',
                english: '',
                render: 0,
                loading: false,
                question: '',
                answers: []

            }
        },
        validations() {
            return {
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
            add() {
                if (this.answers.length < 10) {
                    this.answers.push({
                        id : this.answers.length,
                        answer: ''
                    })
                }
            },

            remove(index) {
                this.answers.splice(index, 1)
            },
            AddQuestionToList: function () {
                if (this.question != '') {
                    var uid = this.createUUID()
                    this.module.moduleQuestionLookUps.push({
                        id: uid,
                        question: this.question,
                        inquiryModuleId: '00000000-0000-0000-0000-000000000000',
                        answerLookUps: this.answers
                    })
                }

                this.question = ''
                this.answers = []

            },
            RemoveQuestionFromList: function (id) {

                this.module.moduleQuestionLookUps = this.module.moduleQuestionLookUps.filter((x) => {
                    return x.id != id;
                });

            },
            RemoveAnswerFromList: function (id, answerIndex) {
                this.module.moduleQuestionLookUps.forEach(function (x) {
                    if (x.id == id) {
                        x.answerLookUps = x.answerLookUps.filter((x) => {
                            return x.id != answerIndex;
                        });
                    }
                })
                //var question =this.module.moduleQuestionLookUps.filter((x) => {
                //    return x.id == id;
                //});
                //question = question.answer.filter((x) => {
                //    return x.id != answerIndex;
                //});

            },
            createUUID: function () {

                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },
            close: function () {
                this.$emit('close');
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
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.module.id == '00000000-0000-0000-0000-000000000000' || this.module.id == undefined || this.module.id == '')
                this.GetAutoCodeGenerator();

        }
    }
</script>
<style scoped>
    .input-group-append .input-group-text, .input-group-prepend .input-group-text {
        background-color: #e3ebf1;
        border: 1px solid #e3ebf1;
        color: #000000;
    }

    .input-group .form-control {
        border-left: 1px solid #e3ebf1;
    }

        .input-group .form-control:focus {
            border-left: 1px solid #3178F6;
        }

    .input-group-text {
        border-radius: 0;
    }
</style>
