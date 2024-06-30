<template>
    <div v-if="isValid('CanAddTCIssue') || isValid('CanDraftTCIssue') || isValid('CanEditTCIssue')">
        <div class="row">
            <div class="col-md-12 ">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">
                                <span v-if="temporaryCash.id === '00000000-0000-0000-0000-000000000000'">{{ $t('AddTemporaryCashIssue.AddTemporaryCashIssue') }} <span style="font-weight:bold">  - {{temporaryCash.registrationNo}}</span></span>
                                <span v-else>{{ $t('AddTemporaryCashIssue.UpdateTemporaryCashIssue') }}  <span style="font-weight:bold">  - {{temporaryCash.registrationNo}}</span></span>

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
                                        <span class="tooltip-container text-dashed-underline ">{{ $t('AddTemporaryCashIssue.Employee') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <employeeDropdown v-model="temporaryCash.userId" @update:modelValue="GetEmployeeLimit(temporaryCash.userId)" :modelValue="temporaryCash.userId" :key="customerRender" :temporaryCashReceiver="true" />
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> {{ $t('AddTemporaryCashIssue.NewEmployee') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <input class="form-control" v-bind:disabled="temporaryCash.userId!='00000000-0000-0000-0000-000000000000' && temporaryCash.userId!=null" v-model="temporaryCash.newUser" />
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline ">{{ $t('AddTemporaryCashIssue.CashIssuer') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <employeeDropdown v-model="temporaryCash.cashIssuerId" :modelValue="temporaryCash.cashIssuerId" :temporaryCashAllocation="true" />
                                    </div>
                                </div>
                                <div class="row form-group" v-if="isEmployee">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline ">{{ $t('AddTemporaryCashIssue.TemporaryCashBalance') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <input class="form-control" disabled v-model="limit" />
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
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline ">  {{ $t('AddTemporaryCashIssue.TemporaryCashRequest') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <temporary-cash-request-dropdown @update:modelValue="GetTemporaryCashRequestDetail(temporaryCash.temporaryCashRequestId)" v-model="temporaryCash.temporaryCashRequestId" :modelValue="temporaryCash.temporaryCashRequestId" />
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> {{ $t('AddTemporaryCashIssue.TemporaryCashBalance') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <input class="form-control" disabled v-model="temporaryCashOpeningBalance" />
                                    </div>
                                </div>
                                <div class="row form-group" v-if="isEmployee? (limit < amount):false">
                                    <label class="col-form-label col-lg-4">
                                        <span class="tooltip-container text-dashed-underline "> {{ $t('AddTemporaryCashIssue.Reason') }} : <span class="text-danger">*</span></span>
                                    </label>
                                    <div class="inline-fields col-lg-8">
                                        <textarea class="form-control " rows="3" v-model="temporaryCash.reason" />

                                    </div>
                                </div>


                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <temporary-cash-issue-item @update:modelValue="SavePurchaseItems" ref="childComponentRef" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12 mt-4 mb-5">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-8" style="border-right: 1px solid #eee;">
                                                <div class="form-group pe-3">
                                                    <label>{{ $t('AddTemporaryCashIssue.TermandCondition') }}:</label>
                                                    <div>
                                                        <textarea class="form-control " rows="3" autofocus="autofocus" v-model="temporaryCash.note" />
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-lg-4" v-if="temporaryCash.id === '00000000-0000-0000-0000-000000000000'">
                                                <div class="form-group ps-3">
                                                    <div class="font-xs mb-1">{{ $t('AddTemporaryCashIssue.AttachFile') }} </div>

                                                    <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i>   {{ $t('AddTemporaryCashIssue.Attachment') }} </button>

                                                    <div>
                                                        <small class="text-muted">
                                                            {{ $t('AddTemporaryCashIssue.FileSize') }}
                                                        </small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4" v-else>
                                                <div class="form-group ps-3">
                                                    <div class="font-xs mb-1">{{ $t('AddTemporaryCashIssue.AttachFile') }} </div>

                                                    <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> { {{ $t('AddTemporaryCashIssue.Attachment') }} </button>

                                                    <div>
                                                        <small class="text-muted">
                                                            {{ $t('AddTemporaryCashIssue.FileSize') }}
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



                                    <button class="btn btn-outline-primary  mr-2 " v-on:click="savePurchase('Draft')" v-if="isValid('CanDraftTCIssue')" v-bind:disabled="v$.$invalid || (temporaryCashBalance < amount) || (isLimitExceed && temporaryCash.reason==='') || temporaryCash.temporaryCashIssueItems.length==0"><i class="far fa-save"></i>   {{ $t('AddTemporaryCashIssue.SaveAsDraft') }}</button>

                                    <button class="btn btn-outline-primary  mr-2 " v-on:click="savePurchase('Approved')" v-if="isValid('CanAddTCIssue')" v-bind:disabled="v$.$invalid || (temporaryCashBalance < amount) || (isLimitExceed && temporaryCash.reason==='') || temporaryCash.temporaryCashIssueItems.length==0"> <i class="far fa-save"></i>   {{ $t('AddTemporaryCashIssue.SaveAsPost') }}</button>

                                    <button class="btn btn-danger " v-on:click="goToPurchase"> {{ $t('AddTemporaryCashIssue.Cancel') }}</button>

                                </div>
                                <div v-else>




                                    <button class="btn btn-outline-primary  mr-2 " v-on:click="savePurchase('Draft')" v-if="isValid('CanDraftTCIssue') && isValid('CanEditTCIssue')" v-bind:disabled="v$.$invalid || (temporaryCashBalance < amount) || (isLimitExceed && temporaryCash.reason==='') || temporaryCash.temporaryCashIssueItems.length==0"> <i class="far fa-save"></i>{{ $t('AddTemporaryCashIssue.UpdateAsDraft') }}</button>
                                    <button class="btn btn-outline-primary  mr-2 " v-on:click="savePurchase('Approved')" v-if="isValid('CanAddTCIssue') && isValid('CanEditTCIssue')" v-bind:disabled="v$.$invalid || (temporaryCashBalance < amount) || (isLimitExceed && temporaryCash.reason==='') || temporaryCash.temporaryCashIssueItems.length==0"><i class="far fa-save"></i>  {{ $t('AddTemporaryCashIssue.UpdateAsPost') }}</button>
                                    <button class="btn btn-danger " v-on:click="goToPurchase">{{ $t('AddTemporaryCashIssue.Cancel') }}</button>

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
                    temporaryCashRequestId: "",
                    cashIssuerId: "",
                    reason: "",
                    note: "",
                    isCashRequesterUser: false,
                    temporaryCashIssueItems: [],
                    attachmentList: [],
                    branchId:'',
                },
                loading: false,
                isLimitExceed: false,
                isEmployee: false,
                amount: 0,
                limit: 0,
                days: 0,
                temporaryCashOpeningBalance: 0,
                temporaryCashBalance: 0,
                customerRender: 0,
            };
        },
        validations() {
            return {
                temporaryCash: {
                    date: { required },
                    cashIssuerId: { required },
                    newUser:
                    {
                        required: requiredIf((x) => {
                            if (x.userId == '00000000-0000-0000-0000-000000000000' || x.userId == null || x.userId == undefined)
                                return true;
                            return false;
                        }),
                    }

                }
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

            GetTemporaryCashBalance: function () {

                var root = this

                this.$https.get('/EmployeeRegistration/TemporaryCashBalance', { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            root.temporaryCashBalance = response.data;
                            root.temporaryCashOpeningBalance = parseFloat(response.data) >= 0 ? 'Dr ' + parseFloat(response.data) * +1 : 'Cr ' + parseFloat(response.data) * (-1);
                        }
                    });

            },

            GetEmployeeLimit: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                if (id != undefined && id != null) {
                    root.$https.get('/EmployeeRegistration/EmployeeDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data != null) {
                                root.days = response.data.days;
                                root.limit = response.data.limit;
                                root.isEmployee = true;

                                if (root.isEmployee && root.amount > root.limit) {
                                    root.isLimitExceed = true;
                                }
                                else {
                                    root.isLimitExceed = false;
                                    root.temporaryCash.reason = '';
                                }
                            }
                        },
                            function () {
                                root.loading = false;
                                root.isEmployee = false;
                                root.temporaryCash.isCashRequesterUser = true;
                            });
                }
                root.isEmployee = false;
            },

            GetTemporaryCashRequestDetail: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (id != undefined) {
                    root.$https.get('/EmployeeRegistration/TemporaryCashRequestDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data != null) {
                                root.temporaryCash.userId = response.data.userId;
                                root.temporaryCash.newUser = response.data.newUser;
                                root.temporaryCash.note = response.data.note;
                                root.temporaryCash.isCashRequesterUser = response.data.isCashRequesterUser;

                                root.$refs.childComponentRef.GetTemporaryCashRequestDetail(response.data.temporaryCashItems);
                                root.GetEmployeeLimit(response.data.userId);

                                root.customerRender++;
                            }
                        },
                            function (error) {
                                root.loading = false;
                                console.log(error);
                            });
                }
            },


            AutoIncrementCode: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/EmployeeRegistration/AutoTemporaryCashIssueCode", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.temporaryCash.registrationNo = response.data;
                        }
                    });
            },

            SavePurchaseItems: function (saleOrderItems, amount) {
                var root = this;
                this.temporaryCash.temporaryCashIssueItems = saleOrderItems;
                this.amount = amount;

                if (this.isEmployee && this.amount > this.limit) {
                    root.isLimitExceed = true;
                }
                else {
                    root.isLimitExceed = false;
                    root.temporaryCash.reason = '';
                }
            },

            savePurchase: function (status) {
                this.temporaryCash.approvalStatus = status;

                this.loading = true;
                var root = this;

                this.$https
                    .post('/EmployeeRegistration/AddTemporaryCashIssue', root.temporaryCash, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
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
                                        root.$router.go('AddTemporaryCashIssue');

                                    } else {
                                        root.$router.push({
                                            path: '/TemporaryCashIssue'                                            
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
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)

            },

            goToPurchase: function () {
                if (this.isValid('CanViewTCIssue') || this.isValid('CanDraftTCIssue')) {
                    this.$router.push({
                        path: '/TemporaryCashIssue',
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
                this.temporaryCash.reason = data.reason;
                this.temporaryCash.isCashRequesterUser = data.isCashRequesterUser;
                this.temporaryCash.temporaryCashRequestId = data.temporaryCashRequestId;
                this.temporaryCash.cashIssuerId = data.cashIssuerId;
                this.temporaryCash.temporaryCashIssueItems = data.temporaryCashIssueItems;
                this.temporaryCash.attachmentList = data.attachmentList;
            }
            else {
                this.AutoIncrementCode();
                this.temporaryCash.date = moment().format('llll');
            }
        },
        mounted: function () {
            this.temporaryCash.branchId = localStorage.getItem('BranchId');
            if (this.$route.query.data != undefined) {
                this.GetEmployeeLimit(this.$route.query.data.userId);
            }
            this.GetTemporaryCashBalance();
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
        color: #35353D;
    }

    .bottomBorder {
        padding-top: 24px !important;
        border-bottom: 1px solid #EFF4F7;
    }
</style>
