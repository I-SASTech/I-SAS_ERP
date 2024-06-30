<template>
    <div v-if="isValid('CanAddTCRequest') || isValid('CanEditTCRequest') || isValid('CanDraftTCRequest') ">
        <div class="row">
            <div class="col-md-12 ">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">
                                <span v-if="temporaryCash.id === '00000000-0000-0000-0000-000000000000'">{{ $t('AddTemporaryCashRequest.AddTemporaryCashRequest') }} <span style="font-weight:bold">  - {{temporaryCash.registrationNo}}</span></span>
                                <span v-else>{{ $t('AddTemporaryCashRequest.UpdateTemporaryCashRequest') }} <span style="font-weight:bold">  - {{temporaryCash.registrationNo}}</span></span>

                            </h4>
                        </div>
                    </div>
                </div>
                <hr class="hr-dashed hr-menu mt-0" />
                <div>
                    <div>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> {{ $t('AddTemporaryCashRequest.Employee') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <employeeDropdown v-model="temporaryCash.userId" :modelValue="temporaryCash.userId" :temporaryCashRequest="true" />
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> {{ $t('AddTemporaryCashRequest.NewEmployee') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <input class="form-control" v-bind:disabled="temporaryCash.userId!='00000000-0000-0000-0000-000000000000' && temporaryCash.userId!=null" v-model="temporaryCash.newUser" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> Date : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">

                                        <datepicker v-model="temporaryCash.date"></datepicker>
                                    </div>
                                </div>
                                

                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <temporary-cash-request-item @update:modelValue="SavePurchaseItems" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12 mt-4 mb-5">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                                <div class="form-group pe-3">
                                                    <label>{{ $t('AddTemporaryCashRequest.TermandCondition') }}:</label>
                                                    <div>
                                                        <textarea class="form-control " rows="3" autofocus="autofocus" v-model="temporaryCash.note" />
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-lg-4" v-if="temporaryCash.id === '00000000-0000-0000-0000-000000000000'">
                                                <div class="form-group ps-3">
                                                    <div class="font-xs mb-1">{{ $t('AddTemporaryCashRequest.AttachFile') }} </div>

                                                    <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i>  {{ $t('AddTemporaryCashRequest.Attachment') }} </button>

                                                    <div>
                                                        <small class="text-muted">
                                                            {{ $t('AddTemporaryCashRequest.FileSize') }}
                                                        </small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4" v-else>
                                                <div class="form-group ps-3">
                                                    <div class="font-xs mb-1">{{ $t('AddTemporaryCashRequest.AttachFile') }} </div>

                                                    <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> {{ $t('AddTemporaryCashRequest.Attachment') }} </button>

                                                    <div>
                                                        <small class="text-muted">
                                                            {{ $t('AddTemporaryCashRequest.FileSize') }}
                                                        </small>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 invoice-btn-fixed-bottom">
                            <div class="button-items" v-if="!loading">
                                <div v-if="temporaryCash.id === '00000000-0000-0000-0000-000000000000'">
                                    <button class="btn btn-outline-primary  mr-2 " v-on:click="savePurchase('Draft')" v-if="isValid('CanDraftTCRequest')" :disabled="v$.$invalid"><i class="far fa-save"></i>   {{ $t('AddTemporaryCashRequest.SaveAsDraft') }}</button>

                                    <button class="btn btn-outline-primary  mr-2 " v-on:click="savePurchase('Approved')" v-if="isValid('CanAddTCRequest')" :disabled="v$.$invalid"> <i class="far fa-save"></i>  {{ $t('AddTemporaryCashRequest.SaveAsPost') }}</button>

                                    <button class="btn btn-danger " v-on:click="goToPurchase">  {{ $t('AddTemporaryCashRequest.Cancel') }}</button>

                                </div>
                                <div v-else>
                                    <button class="btn btn-outline-primary  mr-2 " v-on:click="savePurchase('Draft')" v-if="isValid('CanDraftTCRequest') && isValid('CanEditTCRequest')" :disabled="v$.$invalid"> <i class="far fa-save"></i>  {{ $t('AddTemporaryCashRequest.UpdateAsDraft') }}</button>
                                    <button class="btn btn-outline-primary  mr-2 " v-on:click="savePurchase('Approved')" v-if="isValid('CanAddTCRequest') && isValid('CanEditTCRequest')" :disabled="v$.$invalid "><i class="far fa-save"></i>  {{ $t('AddTemporaryCashRequest.UpdateAsPost') }}</button>
                                    <button class="btn btn-danger " v-on:click="goToPurchase">{{ $t('AddTemporaryCashRequest.Cancel') }}</button>

                                </div>
                            </div>
                            <div class="card-footer col-md-3" v-else>
                                <loading v-model:active="loading"
                                         :can-cancel="true"
                                         :is-full-page="true"></loading>
                            </div>

                        </div>
                    </div>
                  
                </div>
            </div>
            <bulk-attachment :attachmentList="temporaryCash.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
            </div>
        </div>
        <div v-else> <acessdenied></acessdenied></div>

</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import moment from "moment";
    
    import { required, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    //import Vue3Barcode from 'vue3-barcode'
    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                show: false,
                temporaryCash: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    userId: "00000000-0000-0000-0000-000000000000",
                    newUser: "",
                    registrationNo: "",
                    approvalStatus: "",
                    note: "",
                    isCashRequesterUser: false,
                    temporaryCashItems: [],
                    attachmentList: [],
                    branchId:'',
                },
                loading: false
            };
        },
        validations() {
            return {
                temporaryCash: {
                    date: { required },
                    newUser:
                    {
                        required: requiredIf((x) => {
                            if (x.userId == '00000000-0000-0000-0000-000000000000' || x.userId == null || x.userId == undefined)
                                return true;
                            return false;
                        }),
                    }

                },
                }
        },
        methods: {
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function (attachment) {
                this.temporaryCash.attachmentList = attachment;
                this.show = false;
            },

            

            AutoIncrementCode: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/EmployeeRegistration/AutoTemporaryCashRequestCode", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.temporaryCash.registrationNo = response.data;
                        }
                    });
            },

            SavePurchaseItems: function (saleOrderItems) {
                this.temporaryCash.temporaryCashItems = saleOrderItems;
            },

            savePurchase: function (status) {
                this.temporaryCash.approvalStatus = status;

                this.loading = true;
                var root = this;

                this.$https
                    .post('/EmployeeRegistration/AddTemporaryCashRequest', root.temporaryCash, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
                    .then(response => {
                        if (response.data != null) {
                            root.loading = false
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Data Saved Successfully!' : '!حفظ بنجاح',
                                type: 'success',
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,
                            }).then(function (response) {
                                if (response != undefined) {
                                    if (root.temporaryCash.id == "00000000-0000-0000-0000-000000000000") {
                                        root.$router.go('AddTemporaryCashRequest');

                                    } else {
                                        root.$router.push({
                                            path: '/TemporaryCashRequest'                                            
                                        })
                                    }
                                }
                            });

                        }
                        
                    })
                    .catch(error => {
                        console.log(error)                        
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)

            },

            goToPurchase: function () {
                if (this.isValid('CanViewTCRequest') || this.isValid('CanDraftTCRequest')) {
                    this.$router.push({
                        path: '/TemporaryCashRequest',
                    })
                }
                else {
                    this.$router.go()
                }
            }
        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            
           this.$emit('update:modelValue', this.$route.name);

            
            if (this.$route.query.data != undefined) {   
                var data = this.$route.query.data;
                this.temporaryCash.id = data.id;
                this.temporaryCash.date = moment(data.date).format('llll');
                this.temporaryCash.userId = data.userId;
                this.temporaryCash.newUser = data.newUser;
                this.temporaryCash.registrationNo = data.registrationNo;
                this.temporaryCash.note = data.note;
                this.temporaryCash.isCashRequesterUser = data.isCashRequesterUser;
                this.temporaryCash.temporaryCashItems = data.temporaryCashItems;
                this.temporaryCash.attachmentList = data.attachmentList;
            }
            else {
                this.AutoIncrementCode();
                this.temporaryCash.date = moment().format('llll');
            }
        },
        mounted: function () {
            this.temporaryCash.branchId = localStorage.getItem('BranchId');
        },
    };
</script>


