<template>
    <div class="row"
         v-if="isValid('CanAddInquiry') || isValid('CanEditInquiry')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">
                                    {{ $t('AddInquiry.AddInquiry') }}
                                </h4>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Sale.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <hr class="hr-dashed hr-menu mt-0" />
            <div class="row">
                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddInquiry.Inquiry') }} #
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input disabled v-model="inquiry.inquiryNumber" class="form-control" type="text">
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddInquiry.SalePerson') }}:  <span class="text-danger">*</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <employeeDropdown v-model='inquiry.referedBy' :modelValue="inquiry.referedBy" :isMultiple="false" />

                        </div>
                    </div>



                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddInquiry.SalePerson') }}:<span class="text-danger"> *</span></span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <employeeDropdown v-model='inquiry.referedBy' :modelValue="inquiry.referedBy" :isMultiple="false" />
                        </div>
                    </div>



                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddInquiry.Customer') }}: <span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <customerdropdown v-model="inquiry.customerId" @update:modelValue="CheckCustomerAlreadyInquiry" :modelValue="inquiry.customerId" />
                        </div>
                    </div>

                    <div class="row form-group"
                         v-if="isValid('CreditPurchase') && (isValid('CanViewPostOrder') || isValid('CanAddPurchaseOrder'))">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddInquiry.CustomerStatus') }}: <span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect :options="customerOptions" disabled v-model="customerValue" :show-labels="false" :placeholder="$t('AddInquiry.SelectType')">

                            </multiselect>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddInquiry.GeneralDescription') }}:<span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <textarea class="form-control" v-model="inquiry.description" />

                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddInquiry.Reference') }}:</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input class="form-control" v-model="inquiry.reference" />
                        </div>
                    </div>



                </div>
                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">{{ $t('AddInquiry.Date') }} :</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <input v-model="inquiry.dateTime" class="form-control" type="text" disabled>
                        </div>
                    </div>

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddInquiry.MediaType') }}: <span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect :options="options" v-model="DisplayValue" :show-labels="false" track-by="name" :clear-on-select="false" label="name" :placeholder="$t('AddInquiry.SelectType') " @search-change="addMediaType">
                                <template v-slot:noResult>
                                    <span class="btn btn-primary " v-on:click="AddNewMediaType">{{ $t('AddInquiry.CreateNew') }}</span><br />
                                </template>
                            </multiselect>
                        </div>
                    </div>


                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddInquiry.RequestReceiver') }}: <span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <usersDropdown @update:modelValue="AssignToReceiver" :isloginhistory="true" :modelValue="inquiry.receiverId"></usersDropdown>

                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddInquiry.InquiryType') }}: <span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <inquiry-type-dropdown v-model="inquiry.inquiryTypeId" :modelValue="inquiry.inquiryTypeId"></inquiry-type-dropdown>

                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddInquiry.Priority') }}: <span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <multiselect :options="priorityOptions" v-model="DisplayPriorityValue" :show-labels="false" track-by="name" :clear-on-select="false" label="name" :placeholder="$t('AddInquiry.SelectType') " @search-change="addPriority">
                                <template v-slot:noResult>
                                    <span  class="btn btn-primary " v-on:click="AddNewPriority">Create New</span><br />
                                </template>
                            </multiselect>
                        </div>
                    </div>
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline ">
                                {{ $t('AddInquiry.ServiceType') }}: <span class="text-danger"> *</span>
                            </span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <inquiry-process-dropdown v-model="inquiry.inquiryProcessId" :modelValue="inquiry.inquiryProcessId" />

                        </div>
                    </div>
                   


                </div>

                <!--v-for="loockup in modulelist[0]" :key="loockup"-->
                <!--v-if="loockup.moduleQuestionLookUps.length > 0"-->
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-2" :key="moduleRander">
                    <div>
                        <!--inquiryModuleLookUp-->
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item" v-for="(module,index) in inquiry.inquiryModuleLookUp" :key="module.id">
                                <a class="nav-link " style="pointer-events: none;" v-bind:class="{active:activeInquiry == module.label}"  data-bs-toggle="tab" href="#Hold" role="tab" aria-selected="true"><span class="badge  me-2" v-bind:class="(module.attachmentCompulsory && module.attachments.length<=0)?(module.isEnable ? 'badge badge-boxed badge-outline-danger' : 'badge badge-boxed badge-outline-warning'): (module.isEnable ? 'badge badge-boxed badge-outline-success' : 'badge badge-boxed badge-outline-warning')">{{index+1}}</span>{{module.label}}</a>
                            </li>

                        </ul>


                    </div>

                    <div class="card" style="border: 1px #dddddd solid;border-radius:0px !important" v-if="selectedModule.moduleQuestionLookUps.length > 0">
                        <div class="card-body">
                            <div class="row ">
                                <div class="col-lg-6 " v-for="(ques,index) in selectedModule.moduleQuestionLookUps" :key="ques">

                                    <p style="margin:0px !important;padding:0px !important">
                                        Q{{index+1}}:{{ques.question}}

                                    </p>
                                    <div v-for="(ans) in ques.answerLookUps" :key="ans">
                                        <input  type="checkbox" @update:modelValue="IsEnableNextButton(ques.id, ans.id)" v-model="ans.selected" />
                                        <span>{{ans.answer}}</span>
                                        <input  v-if="ans.id=== 10 && ans.selected" class="form-control" v-model="ans.customAnswer" />
                                    </div>
                                </div>

                            </div>
                            <div class="row mt-2">
                                <div class="col-6 " >
                                    <button class="btn  btn-sm me-2" v-bind:class="(selectedModule.attachmentCompulsory && selectedModule.attachments.length<=0)?'btn-danger': 'btn-primary'"
                                            v-on:click="AttachmentModule">
                                        <span>
                                            {{ $t('AddInquiry.Attachment') }}
                                        </span>

                                    </button>
                                </div>
                                <div class="col-6 " >
                                    <button class="btn  btn-sm me-2"
                                            v-on:click="PreviousModule">
                                        <span>
                                            {{ $t('AddInquiry.Previous') }}
                                        </span>

                                    </button>
                                    <!--:disabled="purchase.saleOrderItems.filter(x => x.answer=='').length > 0"-->
                                    <button class="btn btn-primary btn-sm me-2 " v-if="selectedModule.compulsory"
                                            :disabled="isDisable"
                                            v-on:click="NextModule">
                                        <span>
                                            {{ $t('AddInquiry.Next') }}
                                        </span>

                                    </button>
                                    <button class="btn btn-primary btn-sm me-2 " v-else
                                            v-on:click="NextModule">
                                        <span>
                                            {{ $t('AddInquiry.Next') }}
                                        </span>

                                    </button>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="card" style="border: 1px #dddddd solid;border-radius:0px !important" v-else>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12 ">

                                    <label>{{ $t('AddInquiry.Description') }}:</label>
                                    <!--<textarea class="form-control" />-->
                                    <textarea class="form-control" v-model="selectedModule.description" />
                                </div>

                            </div>
                            <div class="row mt-2">
                                <div class="col-6 " >
                                    <button class="btn  btn-sm me-2" v-bind:class="(selectedModule.attachmentCompulsory && selectedModule.attachments.length<=0)?'btn-danger': 'btn-primary'"
                                            v-on:click="AttachmentModule">
                                        <span>
                                            {{ $t('AddInquiry.Attachment') }}
                                        </span>

                                    </button>
                                </div>
                                <div class="col-6 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    <button class="btn btn-primary btn-sm me-2"
                                            v-on:click="PreviousModule">
                                        <span>
                                            {{ $t('AddInquiry.Previous') }}
                                        </span>

                                    </button>

                                    <!--:disabled="purchase.saleOrderItems.filter(x => x.quantity=='').length > 0"-->
                                    <button class="btn btn-primary btn-sm me-2 " v-if="selectedModule.compulsory"
                                            :disabled="selectedModule.description == null || selectedModule.description == '' || selectedModule.description == undefined "
                                            v-on:click="NextModule">
                                        <span>
                                            {{ $t('AddInquiry.Next') }}
                                        </span>

                                    </button>
                                    <button class="btn btn-primary btn-sm me-2 " v-else
                                            v-on:click="NextModule">
                                        <span>
                                            {{ $t('AddInquiry.Next') }}
                                        </span>

                                    </button>
                                </div>

                            </div>
                        </div>
                    </div>


                </div>

                <!--v-for="(ques,index) in loockup" :key="ques"-->




                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                  
                    <div class="inline-fields col-xs-12 col-sm-6 col-md-6 col-lg-6">
                        <div class="checkbox form-check-inline mx-2">
                            <input  type="checkbox" id="inlineCheckbox1"  v-model="inquiry.isTerm" />
                            <label for="inlineCheckbox1"> {{ $t('AddInquiry.AddTerms') }} </label>
                        </div>

                    </div>
                    <!--<label>{{ $t('AddInquiry.AddTerms') }} :</label> <toggle-button class="ml-2" color="#3178F6" v-model="inquiry.isTerm" />-->
                </div>
                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-if="inquiry.isTerm">
                    <label>{{ $t('AddInquiry.TermAndCondition') }}:</label>
                    <textarea class="form-control" v-model="inquiry.termAndCondition" />
                </div>
                <!--<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
        <label>Line Item:</label>
        <productMultiSelectDropdown @update:modelValue="pusProductRecord" :isRequest="false" :modelValue="itemList"></productMultiSelectDropdown>
    </div>-->
               

                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div v-if="!loading "
                         class="col-md-12 arabicLanguage">

                        <button v-if="inquiry.id == '00000000-0000-0000-0000-000000000000' && isValid('CanAddInquiry')" :disabled="v$.inquiry.$invalid || inquiry.inquiryModuleLookUp.filter(x => x.isEnable==false).length > 0" class="btn btn-outline-primary  me-2"
                                v-on:click="SaveData">
                            <i class="far fa-save"></i>
                            {{ $t('AddInquiry.Save') }}

                        </button>
                        <button v-else-if="isValid('CanEditInquiry')" :disabled="v$.inquiry.$invalid" class="btn btn-outline-primary  me-2"
                                v-on:click="SaveData">
                            <i class="far fa-save"></i>{{ $t('AddInquiry.Update') }}

                        </button>
                        <button class="btn btn-danger me-2" v-on:click="CloseInquiry">
                            {{ $t('AddPurchase.Cancel') }}
                        </button>
                    </div>
                </div>

                <div class="col-lg-12 mt-4 mb-5">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                    <label>{{ $t('AddInquiry.UserMessage') }}:</label>
                                    <textarea class="form-control" rows="3" v-model="inquiry.userMessage" />
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group ps-3" v-if="!loading">
                                        <div class="font-xs mb-1">{{ $t('AddInquiry.Attachment') }}</div>

                                        <button v-on:click="Attachment()" type="button"
                                                class="btn btn-light btn-square btn-outline-dashed mb-1">
                                            <i class="fas fa-cloud-upload-alt"></i>     {{ $t('AddInquiry.Attachment') }}
                                        </button>

                                        <div>
                                            <small class="text-muted">
                                                {{ $t('AddInquiry.FileSize') }}
                                            </small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <bulk-attachment :attachmentList="inquiry.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
        <bulk-attachment :attachmentList="selectedModule.attachments" :show="showModuleAttachment" v-if="showModuleAttachment" @close="moduleAttachmentSave" />
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
   /* import { VueEditor } from "vue2-editor";*/
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    //import VueCtkDateTimePicker from 'vue-ctk-date-time-picker';
    //import 'vue-ctk-date-time-picker/dist/vue-ctk-date-time-picker.css';
    //import Vue from 'vue'
    //Vue.component('VueCtkDateTimePicker', VueCtkDateTimePicker);
    import Multiselect from 'vue-multiselect'
    export default {
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
            //
            /*VueEditor*/
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                loading:false,
                inquiry: {
                    id: '00000000-0000-0000-0000-000000000000',
                    inquiryNumber: '',
                    dateTime: '',
                    dueDateTime: '',
                    reference: '',
                    description: '',
                    termAndCondition: '',
                    userMessage: '',
                    isTerm: false,
                    inquiryStatus: 'Pending',
                    customerId: '',
                    receiverId: '',
                    handlerId: '',
                    referedBy: '',
                    mediaTypeId: '00000000-0000-0000-0000-000000000000',
                    mediaType: '',
                    inquiryModuleId: '00000000-0000-0000-0000-000000000000',
                    inquiryProcessId: '',
                    inquiryTypeId: '00000000-0000-0000-0000-000000000000',
                    inquiryPriorityId: '00000000-0000-0000-0000-000000000000',
                    inquiryItems: [],
                    attachmentList: [],
                    inquiryModuleLookUp: [],
                    branchId: '',
                },
                show: false,
                isDisable: true,
                isUpdate: false,
                showModuleAttachment: false,
                dueDate: '',
                options: [],
                value: '',
                newCC: '',
                itemList: [],
                modulelist: [],
                priorityOptions: [],
                priorityValue: [],
                customerOptions: ['New', 'Old'],
                customerValue:'',
                selectedModule: {
                    attachments: [],
                    code: '',
                    description: '',
                    id: '',
                    isActive: '',
                    compulsory: '',
                    label: '',
                    name: '',
                    rowNumber: '',
                    moduleQuestionLookUps: [],
                    isEnable : false
                },
                selectModuleIndex: 0,
                moduleRander: 0,
                activeInquiry: '',
            };
        },
        validations() {
            return {
                inquiry: {
                    dueDateTime: { required },

                    description: { required },
                    mediaTypeId: { required },
                    customerId: { required },
                    /*handlerId: { required },*/
                    receiverId: { required },
                    inquiryTypeId: { required },
                    inquiryPriorityId: { required },
                    referedBy: { required },
                    inquiryProcessId: { required },
                }
                }
        },

        computed: {
            IsEnable() {
                this.inquiry.inquiryModuleLookUp.find(x=>x.compulsory && (x.description == null || x.description == '') && x.moduleQuestionLookUps.length == 0)
                return true;
            },
            DisplayValue: {
                get: function () {
                    if (this.value != "" || this.value != undefined) {
                        return this.value;
                    }
                    return this.modelValue;
                },
                set: function (value) {
                    this.value = value;
                    this.inquiry.mediaTypeId = this.value.id
                }
            },
            DisplayPriorityValue: {
                get: function () {
                    if (this.priorityValue != "" || this.priorityValue != undefined) {
                        return this.priorityValue;
                    }
                    return this.modelValue;
                },
                set: function (priorityValue) {
                    this.priorityValue = priorityValue;
                    this.inquiry.inquiryPriorityId = this.priorityValue.id
                }
            }
        },

        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            IsEnableNextButton: function (quesId, ansId) {
                var selected = true;
                this.isDisable = true;
                var root = this
                var count = 1;
                var length = this.selectedModule.moduleQuestionLookUps.length;
                this.selectedModule.moduleQuestionLookUps.forEach(function (x) {
                    var ansIndex = x.answerLookUps.findIndex(y => y.selected)
                    
                    if (x.id != quesId) {
                        selected = ansIndex >= 0 ? false : true
                    }
                    if (count == length) {
                        root.isDisable = selected
                        root.selectedModule.isEnable = !root.isDisable
                    }
                    count++

                })
                var question  = this.selectedModule.moduleQuestionLookUps.filter((question) => {
                    return question.id == quesId
                });

                var totalCount = 0
                question[0].answerLookUps.forEach(function (x) {
                    if (x.selected && x.id != ansId)
                        totalCount += 1
                })
                if (totalCount > 0) {
                    root.isDisable = false
                    root.selectedModule.isEnable = !root.isDisable
                }
                var isBreakLoop = false
                question[0].answerLookUps.forEach(function (x) {
                    if (x.selected == false && x.id == ansId && selected == false) {
                        root.isDisable = false
                        isBreakLoop = true
                        root.selectedModule.isEnable = !root.isDisable
                    }
                    else if (length == 1)
                    {
                        root.isDisable = x.selected == false ? false : true
                        root.selectedModule.isEnable = !root.isDisable
                    }
                    else if (isBreakLoop == false) {
                        root.isDisable = true
                        
                    }
                   
                })
                
            },

            EnableNextButton: function () {
                var selected = true;
                this.isDisable = true;
                var root = this
                var count = 1;
                var length = this.selectedModule.moduleQuestionLookUps.length;
                this.selectedModule.moduleQuestionLookUps.forEach(function (x) {
                    var ansIndex = x.answerLookUps.findIndex(y => y.selected)
                    selected = ansIndex >= 0 ? false : true
                    if (count == length) {
                        root.isDisable = selected
                        root.selectedModule.isEnable = !root.isDisable
                    }
                    count++

                })
            },
            AttachmentModule: function () {
                this.showModuleAttachment = true;
            },
            moduleAttachmentSave: function (attachment) {
                this.selectedModule.attachments = attachment;
                this.showModuleAttachment = false;
            },
            NextModule: function () {
                if (this.selectModuleIndex < this.inquiry.inquiryModuleLookUp.length - 1) {

                    if (this.selectedModule.moduleQuestionLookUps.length == 0 && (this.selectedModule.description != null || this.selectedModule.description != '')) {
                        this.selectedModule.isEnable = true
                    }
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].attachments = this.selectedModule.attachments
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].code = this.selectedModule.code
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].description = this.selectedModule.description
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].id = this.selectedModule.id
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].isActive = this.selectedModule.isActive
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].compulsory = this.selectedModule.compulsory
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].attachmentCompulsory = this.selectedModule.attachmentCompulsory
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].label = this.selectedModule.label
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].name = this.selectedModule.name
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].rowNumber = this.selectedModule.rowNumber
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].moduleQuestionLookUps = this.selectedModule.moduleQuestionLookUps
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].isEnable = this.selectedModule.isEnable
                    this.selectModuleIndex++;
                    this.selectedModule.attachments = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].attachments
                    this.selectedModule.code = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].code
                    this.selectedModule.description = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].description
                    this.selectedModule.id = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].id
                    this.selectedModule.isActive = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].isActive
                    this.selectedModule.compulsory = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].compulsory
                    this.selectedModule.attachmentCompulsory = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].attachmentCompulsory
                    this.selectedModule.label = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].label
                    this.selectedModule.name = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].name
                    this.selectedModule.rowNumber = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].rowNumber
                    this.selectedModule.isEnable = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].isEnable
                    this.selectedModule.moduleQuestionLookUps = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].moduleQuestionLookUps

                    this.activeInquiry = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].label
                    this.moduleRander++
                    this.EnableNextButton()
                }
                else if (this.selectModuleIndex == this.inquiry.inquiryModuleLookUp.length - 1) {
                    
                    if (this.selectedModule.moduleQuestionLookUps.length == 0 && (this.selectedModule.description != null || this.selectedModule.description != '')) {
                        this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].attachments = this.selectedModule.attachments
                        this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].code = this.selectedModule.code
                        this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].description = this.selectedModule.description
                        this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].id = this.selectedModule.id
                        this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].isActive = this.selectedModule.isActive
                        this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].compulsory = this.selectedModule.compulsory
                        this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].attachmentCompulsory = this.selectedModule.attachmentCompulsory
                        this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].label = this.selectedModule.label
                        this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].name = this.selectedModule.name
                        this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].rowNumber = this.selectedModule.rowNumber
                        this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].moduleQuestionLookUps = this.selectedModule.moduleQuestionLookUps
                        this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].isEnable = this.selectedModule.isEnable
                       
                        this.selectedModule.isEnable = true
                    }
                }
            },
            PreviousModule: function () {
                if (this.selectModuleIndex > 0) {
                    if (this.selectedModule.moduleQuestionLookUps.length == 0 && (this.selectedModule.description != null || this.selectedModule.description != '')) {
                        this.selectedModule.isEnable = true
                    }
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].attachments = this.selectedModule.attachments
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].code = this.selectedModule.code
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].description = this.selectedModule.description
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].id = this.selectedModule.id
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].isActive = this.selectedModule.isActive
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].compulsory = this.selectedModule.compulsory
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].attachmentCompulsory = this.selectedModule.attachmentCompulsory
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].label = this.selectedModule.label
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].name = this.selectedModule.name
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].rowNumber = this.selectedModule.rowNumber
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].moduleQuestionLookUps = this.selectedModule.moduleQuestionLookUps
                    this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].isEnable = this.selectedModule.isEnable
                    this.selectModuleIndex--;
                    this.selectedModule.attachments = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].attachments
                    this.selectedModule.code = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].code
                    this.selectedModule.description = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].description
                    this.selectedModule.id = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].id
                    this.selectedModule.isActive = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].isActive
                    this.selectedModule.compulsory = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].compulsory
                    this.selectedModule.attachmentCompulsory = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].attachmentCompulsory
                    this.selectedModule.label = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].label
                    this.selectedModule.name = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].name
                    this.selectedModule.rowNumber = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].rowNumber
                    this.selectedModule.moduleQuestionLookUps = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].moduleQuestionLookUps
                    this.selectedModule.isEnable = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].isEnable


                    this.activeInquiry = this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].label
                    this.moduleRander++
                    this.EnableNextButton()
                }
            },
            CloseInquiry: function () {
                if (this.isValid('CanViewInquiry'))
                    this.$router.push('Inquiry')
                else {
                    this.$router.go('/AddInquiry')
                }
            },
            addMediaType: function (data) {
                if (data != '' && data != null && data != undefined) {
                    this.newMediaType = data
                }
            },
            AddNewMediaType: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                root.$https.get("/Project/SaveMediaType?name=" + this.newMediaType, { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.options.push({
                                id: response.data.id,
                                name: response.data.name
                            })
                            root.value = response.data
                            //root.inquiry.mediaType = response.data.name
                            root.inquiry.mediaTypeId = response.data.id
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cannot Generate Auto Inoice Number!' : 'استوردلا يمكن إنشاء رقم فاتورة تلقائي!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                    });


                //this.inquiry.mediaType = this.newMediaType
            },
            GetMediaType: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                root.$https.get("/Project/GetMediaType", { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            response.data.forEach(function (x) {
                                root.options.push({
                                    id: x.id,
                                    name: x.name
                                })
                            })
                        }
                    })
                    .then(function () {
                        if (root.inquiry.mediaTypeId != '00000000-0000-0000-0000-000000000000' && root.inquiry.mediaTypeId != null) {
                            root.value = root.options.find(function (x) {
                                return x.id == root.inquiry.mediaTypeId;
                            })
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cannot Generate Auto Inoice Number!' : 'استوردلا يمكن إنشاء رقم فاتورة تلقائي!', 
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                    });
            },

            addPriority: function (data) {
                if (data != '' && data != null && data != undefined) {
                    this.newPriority = data
                }
            },
            AddNewPriority: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                root.$https.get("/Project/SavePriority?name=" + this.newPriority, { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.priorityOptions.push({
                                id: response.data.id,
                                name: response.data.name
                            })
                            root.priorityValue = response.data
                            //root.inquiry.mediaType = response.data.name
                            root.inquiry.inquiryPriorityId = response.data.id
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cannot Generate Auto Inoice Number!' : 'استوردلا يمكن إنشاء رقم فاتورة تلقائي!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                    });


                //this.inquiry.mediaType = this.newMediaType
            },

            GetPriority: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                root.$https.get("/Project/GetPriority", { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            response.data.forEach(function (x) {
                                root.priorityOptions.push({
                                    id: x.id,
                                    name: x.name
                                })
                            })
                        }
                    })
                    .then(function () {
                        if (root.inquiry.inquiryPriorityId != '00000000-0000-0000-0000-000000000000' && root.inquiry.inquiryPriorityId != null) {
                            root.priorityValue = root.priorityOptions.find(function (x) {
                                return x.id == root.inquiry.inquiryPriorityId;
                            })
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cannot Generate Auto Inoice Number!' : 'استوردلا يمكن إنشاء رقم فاتورة تلقائي!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                    });
            },
            Attachment: function () {
                this.show = true;
            },
            attachmentSave: function (attachment) {
                this.inquiry.attachmentList = attachment;
                this.show = false;
            },
            AssignToHandler: function (x) {
                this.inquiry.handlerId = x.id
            },
            AssignToReceiver: function (x) {
                this.inquiry.receiverId = x.id
            },
            pusProductRecord: function (x) {
                console.log(x)
                var root = this
                root.inquiry.inquiryItems = []
                x.forEach(function (y) {
                    root.inquiry.inquiryItems.push({
                        inquiryId: '00000000-0000-0000-0000-000000000000',
                        itemId: y.id
                    })
                })

            },
            GetInquiryAutoCode: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                root.$https.get("/Project/InquiryAutoGenerateNo?branchId=" + localStorage.getItem('BranchId'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            if (root.isUpdate == false)
                                root.inquiry.inquiryNumber = response.data.autoNumber
                            

                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cannot Generate Auto Inoice Number!' : 'استوردلا يمكن إنشاء رقم فاتورة تلقائي!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                    });
            },

            SaveData: function () {
                this.loading = true;
                this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].attachments = this.selectedModule.attachments
                this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].code = this.selectedModule.code
                this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].description = this.selectedModule.description
                this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].id = this.selectedModule.id
                this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].isActive = this.selectedModule.isActive
                this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].compulsory = this.selectedModule.compulsory
                this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].attachmentCompulsory = this.selectedModule.attachmentCompulsory
                this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].label = this.selectedModule.label
                this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].name = this.selectedModule.name
                this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].rowNumber = this.selectedModule.rowNumber
                this.inquiry.inquiryModuleLookUp[this.selectModuleIndex].moduleQuestionLookUps = this.selectedModule.moduleQuestionLookUps
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                this.inquiry.branchId = localStorage.getItem('BranchId');

                console.log(this.inquiry)
                root.$https.post("/Project/SaveInquiry", this.inquiry, { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$swal({
                                title: root.$t('SavedSuccessfully'),
                                text: response.data.message,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            }).then(function () {
                                if (response.data.isAdd) {
                                    root.$router.go({
                                        path: '/AddInquiry'
                                    });

                                }
                                else {
                                    root.$router.push('/Inquiry');
                                }
                            });

                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cannot Generate Auto Inoice Number!' : 'استوردلا يمكن إنشاء رقم فاتورة تلقائي!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                            root.loading = false
                    })
                    .finally(() => root.loading = false);

            },
            GetmoduleData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Project/InquiryModuleList?isActive=' + true, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        if (root.$route.query.data != undefined) {
                            root.inquiry.inquiryModuleLookUp = response.data.results.inquiryModuleLookUp;
                            console.log(root.$route.query.data.inquiryModuleLookUp)
                            console.log(root.inquiry.inquiryModuleLookUp)
                            root.selectedModule.attachments = response.data.results.inquiryModuleLookUp[0].attachments
                            root.selectedModule.code = response.data.results.inquiryModuleLookUp[0].code
                            root.selectedModule.description = response.data.results.inquiryModuleLookUp[0].description
                            root.selectedModule.id = response.data.results.inquiryModuleLookUp[0].id
                            root.selectedModule.isActive = response.data.results.inquiryModuleLookUp[0].isActive
                            root.selectedModule.compulsory = response.data.results.inquiryModuleLookUp[0].compulsory
                            root.selectedModule.attachmentCompulsory = response.data.results.inquiryModuleLookUp[0].attachmentCompulsory
                            root.selectedModule.label = response.data.results.inquiryModuleLookUp[0].label
                            root.selectedModule.name = response.data.results.inquiryModuleLookUp[0].name
                            root.selectedModule.rowNumber = response.data.results.inquiryModuleLookUp[0].rowNumber
                            root.selectedModule.moduleQuestionLookUps = response.data.results.inquiryModuleLookUp[0].moduleQuestionLookUps
                            


                            root.activeInquiry = response.data.results.inquiryModuleLookUp[0].label
                        }
                        else {
                            root.inquiry.inquiryModuleLookUp = response.data.results.inquiryModuleLookUp;
                            root.selectedModule.attachments = response.data.results.inquiryModuleLookUp[0].attachments
                            root.selectedModule.code = response.data.results.inquiryModuleLookUp[0].code
                            root.selectedModule.description = response.data.results.inquiryModuleLookUp[0].description
                            root.selectedModule.id = response.data.results.inquiryModuleLookUp[0].id
                            root.selectedModule.isActive = response.data.results.inquiryModuleLookUp[0].isActive
                            root.selectedModule.compulsory = response.data.results.inquiryModuleLookUp[0].compulsory
                            root.selectedModule.attachmentCompulsory = response.data.results.inquiryModuleLookUp[0].attachmentCompulsory
                            root.selectedModule.label = response.data.results.inquiryModuleLookUp[0].label
                            root.selectedModule.name = response.data.results.inquiryModuleLookUp[0].name
                            root.selectedModule.rowNumber = response.data.results.inquiryModuleLookUp[0].rowNumber
                            root.selectedModule.moduleQuestionLookUps = response.data.results.inquiryModuleLookUp[0].moduleQuestionLookUps



                            root.activeInquiry = response.data.results.inquiryModuleLookUp[0].label
                        }
                        
                    }
                    root.loading = false;
                });
            },
            CheckCustomerAlreadyInquiry: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Project/CheckCustomerAlreadyInquiry?id=' + this.inquiry.customerId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.customerValue = "Old"
                        console.log(root.selectedModule)
                    }
                    else {
                        root.customerValue = "New"
                    }
                    root.loading = false;
                });
            },
        },
        created: function () {
             var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            if (this.$route.query.data != undefined) {
                this.inquiry = this.$route.query.data
                this.itemList = this.$route.query.data.inquiryItems
                this.inquiry.dueDateTime = moment(this.inquiry.dueDateTime)
                this.inquiry.dateTime = moment(this.inquiry.dateTime).format('lll')
                
                //this.selectedModule.attachments = this.$route.query.data.inquiryModuleLookUp[0].attachments
                //this.selectedModule.code = this.$route.query.data.inquiryModuleLookUp[0].code
                //this.selectedModule.description = this.$route.query.data.inquiryModuleLookUp[0].description
                //this.selectedModule.id = this.$route.query.data.inquiryModuleLookUp[0].id
                //this.selectedModule.isActive = this.$route.query.data.inquiryModuleLookUp[0].isActive
                //this.selectedModule.compulsory = this.$route.query.data.inquiryModuleLookUp[0].compulsory
                //this.selectedModule.attachmentCompulsory = this.$route.query.data.inquiryModuleLookUp[0].attachmentCompulsory
                //this.selectedModule.label = this.$route.query.data.inquiryModuleLookUp[0].label
                //this.selectedModule.name = this.$route.query.data.inquiryModuleLookUp[0].name
                //this.selectedModule.rowNumber = this.$route.query.data.inquiryModuleLookUp[0].rowNumber
                //this.selectedModule.moduleQuestionLookUps = this.$route.query.data.inquiryModuleLookUp[0].moduleQuestionLookUps

                //this.activeInquiry = this.$route.query.data.inquiryModuleLookUp[0].label
                this.isUpdate = true
                this.customerValue = "Old"
                this.GetMediaType();
                this.GetPriority();
                this.GetmoduleData();
            }
            else {
                this.GetInquiryAutoCode();
                this.GetMediaType();
                this.inquiry.dueDateTime = moment()
                this.inquiry.dateTime = moment().format('lll')
                this.GetmoduleData();
                this.GetPriority();
            }

        },
        mounted: function () {
            //this.GenerateToken('Inquiry Management');
        },
    };
</script>
<style scoped>
    .poHeading {
        font-size: 32px;
        font-style: normal;
        line-height: 37px;
        font-weight: 500;
        font-size: 24px;
        line-height: 26px;
        color: #3178F6;
        text-align: center
    }

    .dateHeading {
        font-size: 16px;
        font-style: normal;
        font-weight: 400;
        line-height: 18px;
        letter-spacing: 0.01em;
        text-align: left;
        color: #35353D;
    }

    .bottomBorder {
        padding-top: 24px !important;
        border-bottom: 1px solid #EFF4F7;
    }
</style>
