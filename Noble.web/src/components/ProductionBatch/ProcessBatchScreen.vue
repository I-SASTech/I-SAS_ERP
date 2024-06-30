<template>
    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="col-lg-12">


            <div class=" mb-2 row">
                <div class="col-lg-9  ">
                    <span v-if="purchase.id === '00000000-0000-0000-0000-000000000000'"> <span class="MainLightHeading">{{ $t('AddProductionBatch.AddProductionBatch') }} - </span><span class="DayHeading">{{purchase.registrationNo}}</span></span>
                    <span v-else><span class="MainLightHeading">{{ $t('AddProductionBatch.UpdateProductionBatch') }} - </span><span class="DayHeading">{{purchase.registrationNo}}</span></span>

                </div>
                <div class="col-lg-3 text-right">
                    <span>
                        {{purchase.date}}
                    </span>
                </div>
            </div>

            <div class="row">

                <div v-bind:class="contractorDiv ? 'col-sm-7 col-12' : ' col-sm-12'">
                    <div class="card">
                        <div class="card-body ">
                            <ul class="nav nav-tabs" data-tabs="tabs">
                                <li class="nav-item" v-for="process in purchase.processList" v-bind:key="process.id">
                                    <a class="nav-link " v-bind:style="process.approvalStatus == 0 || process.approvalStatus == 4 ? 'pointer-events : none;' : '' " v-bind:class="{active:active == process.isActive}" v-on:click="makeActiveProcess(process)" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true"> {{process.englishName}} <span class="badge badge-primary" v-if="process.approvalStatus==6"> C</span> <span class="badge badge-primary" v-if="process.approvalStatus==5"> P</span></a>
                                </li>

                            </ul>

                        </div>


                    </div>
                    <div class="card">
                        <div class="card-body ">
                            <h6 style="color:#3178F6">{{selectedProcess.description}}</h6>

                        </div>
                    </div>
                    <div class="row" v-bind:key="randerProcess">
                        <div class="col-sm-4 ">
                            <div class="card">
                                <div class="card-body" style="height:600px">
                                    <span class="dot"></span> <span style="font-weight: 700; font-size: 14.9314px; line-height: 18px;  color: #0D062D;">{{ $t('AddProductionBatch.ToDo') }}</span>
                                    <hr style="border: 1px solid #F2C94C;" />
                                    <div v-for="contractor in selectedProcess.processContractors" v-bind:key="contractor.contractorId" v-on:click="ContractorDetail(contractor)">
                                        <div v-if="contractor.approvalStatus==4 || contractor.approvalStatus==0 " class="ContractorCard mb-2" v-bind:class="contractor.isSelect ? 'ActiveColor' : ''">
                                            <span style="font-weight: 600; font-size: 16.7978px;  color: #0D062D;">
                                                {{contractor.contractorNameEn}}
                                            </span>
                                            <p style="font-weight: 400; font-size: 12px; line-height: 15px; color: #787486;">{{ $t('AddProductionBatch.ToDoDescription') }} </p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-sm-4 ">
                            <div class="card">
                                <div class="card-body" style="height:600px">
                                    <span class="dot1"></span> <span style="font-weight: 700; font-size: 14.9314px; line-height: 18px;  color: #0D062D;">{{ $t('AddProductionBatch.InProcess') }}</span>
                                    <hr style="border: 1px solid #2F80ED;" />
                                    <div v-for="contractor in selectedProcess.processContractors" v-bind:key="contractor.contractorId" v-on:click="ContractorDetail(contractor)">
                                        <div v-if="contractor.approvalStatus==5 " class="ContractorCard mb-2" v-bind:class="contractor.isSelect ? 'ActiveColor' : ''">
                                            <span style="font-weight: 600; font-size: 16.7978px;  color: #0D062D;">
                                                {{contractor.contractorNameEn}} <span> <a href="javascript:void(0)" title="A4" class="btn btn-primary btn-sm btn-icon ml-1 mr-1" v-on:click="PrintReport(contractor.id)"><i class=" fa fa-print"></i></a></span>
                                            </span>
                                            <p style="font-weight: 400; font-size: 12px; line-height: 15px; color: #787486;">{{ $t('AddProductionBatch.InProcessDescription') }} /</p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-sm-4 ">
                            <div class="card">
                                <div class="card-body" style="height:600px">
                                    <span class="dot1"></span> <span style="font-weight: 700; font-size: 14.9314px; line-height: 18px;  color: #0D062D;">{{ $t('AddProductionBatch.Completed') }}</span>
                                    <hr style=" border: 1px solid #68B266;" />
                                    <div v-for="contractor in selectedProcess.processContractors" v-bind:key="contractor.contractorId" v-on:click="ContractorDetail(contractor)">
                                        <div v-if="contractor.approvalStatus==6 " class="ContractorCard mb-2" v-bind:class="contractor.isSelect ? 'ActiveColor' : ''">
                                            <span style="font-weight: 600; font-size: 16.7978px;  color: #0D062D;">
                                                {{contractor.contractorNameEn}} <span> <a href="javascript:void(0)" title="A4" class="btn btn-primary btn-sm btn-icon ml-1 mr-1" v-on:click="PrintReport(contractor.id)"><i class=" fa fa-print"></i></a></span>
                                            </span>
                                            <p style="font-weight: 400; font-size: 12px; line-height: 15px; color: #787486;">{{ $t('AddProductionBatch.CompletedDescription') }} </p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <!--<div class="col-sm-4 card"></div>
                        <div class="col-sm-4 card"></div>-->

                    </div>
                </div>
                <div class="col-sm-5 col-12" v-if="contractorDiv">
                    <div class="card">
                        <div class="card-body row" v-if="contractor.approvalStatus==0 || contractor.approvalStatus==4">
                            <div class="col-6">
                                <h6 style="color:#3178F6">{{contractor.contractorNameEn}}</h6>
                            </div>
                            <div class="col-6 text-right">
                                <h6 style="color: #787486;font-weight: 400;font-size: 16px;">{{contractor.date}}</h6>
                            </div>
                            <div class="col-6 ">
                                <label>{{ $t('AddProductionBatch.FromDate') }}:</label>
                                <datepicker v-model="contractor.fromDate"></datepicker>

                            </div>
                            <div class="col-6 ">
                                <label>{{ $t('AddProductionBatch.ToDate') }}:</label>
                                <datepicker v-model="contractor.toDate"></datepicker>

                            </div>
                            <div class="col-6 ">
                                <label>{{ $t('AddProductionBatch.ContractorType') }}:</label>
                                <multiselect v-model="contractor.contractorType" :options="['In Door', 'Out Door']" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :show-labels="false" :placeholder="$t('AddProductionBatch.SelectType')">
                                </multiselect>
                            </div>
                            <div class="col-lg-12 mt-3">
                                <contractor-item @update:modelValue="SaveContractorItems" v-bind:readOnly="false" v-bind:contractor="contractor" :key="randerContractorList" />
                            </div>
                            <div class="col-lg-12">
                                <div class="accordion" role="tablist">

                                    <b-card no-body class="mb-1">
                                        <b-card-header header-tag="header" class="p-1" role="tab">
                                            <b-button block v-b-toggle.accordion-3 variant="primary">{{ $t('AddProductionBatch.AdvancePayment') }}</b-button>
                                        </b-card-header>
                                        <b-collapse id="accordion-3" accordion="my-accordion" role="tabpanel">
                                            <b-card-body>
                                                <purchaseorder-payment :totalAmount="0" :customerAccountId="contractor.contractorAccountId" :newbatchProcessContractorId="contractor.batchProcessContractorId" :purchaseOrderId="purchaseOrderId" :show="payment" v-if="payment" @close="PaymentList(contractor.batchProcessContractorId)" :isSaleOrder="'false'" :isPurchase="'false'" :newisContractor="'true'" :formName="'ContractorAdvancePay'" />
                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-12 text-right">
                                                            <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="payment=true">{{ $t('AddProductionBatch.AddPayment') }} </a>
                                                        </div>
                                                    </div>
                                                    <div class=" table-responsive" v-bind:key="randerPaymentList">
                                                        <table class="table ">
                                                            <thead class="m-0">
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th class="text-center">{{ $t('AddProductionBatch.Date') }} </th>
                                                                    <th class="text-center">{{ $t('AddProductionBatch.VoucherNumber') }} </th>
                                                                    <th class="text-center">{{ $t('AddProductionBatch.Amount') }} </th>
                                                                    <th class="text-center">{{ $t('AddProductionBatch.PaymentMode') }} </th>
                                                                    <th class="text-left">{{ $t('AddProductionBatch.Description') }} </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr v-for="(payment,index) in paymentVoucherList" v-bind:key="index">
                                                                    <td>
                                                                        {{index+1}}
                                                                    </td>
                                                                    <th class="text-center">{{getDate(payment.date)}}</th>
                                                                    <th class="text-center">{{payment.voucherNumber}}</th>
                                                                    <th class="text-center">{{currency}} {{parseFloat(payment.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</th>
                                                                    <th class="text-center"><span v-if="payment.paymentMode==0">Cash</span><span v-if="payment.paymentMode==1">Bank</span></th>
                                                                    <th class="text-left">{{payment.narration}}</th>

                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </b-card-body>
                                        </b-collapse>
                                    </b-card>


                                </div>

                            </div>
                            <div class="col-lg-12">
                                <label>Terms And Conditions:</label>
                                <textarea class="form-control" v-model="contractor.comments"></textarea>
                            </div>
                            <div class="text-right col-lg-12">
                                <button type="button" class="btn btn-primary" v-on:click="SaveContractor('InProcess')">InProcess</button>
                                <button type="button" class="btn btn-danger" style="margin-right:8px" v-on:click="CloseContractorModel">{{ $t('Cancel') }}</button>

                            </div>

                        </div>


                        <div class="card-body row" v-else>
                            <div class="col-6">
                                <h6 style="color:#3178F6">{{contractor.contractorNameEn}}</h6>
                            </div>
                            <div class="col-6 text-right">
                                <h6 style="color: #787486;font-weight: 400;font-size: 16px;">{{contractor.date}}</h6>
                            </div>
                            <div class="col-6">
                                <span><span style="color:#787486;font-size:16px">From Date:</span>{{contractor.fromDates}}</span>
                            </div>
                            <div class="col-6 text-right">
                                <span> <span style="color:#787486;font-size:16px">To Date:</span> :{{contractor.toDates}}</span>
                            </div>
                            <div class="col-6 ">
                                <span> <span style="color:#787486;font-size:16px">Contractor Type :</span> {{contractor.contractorType}}</span>

                            </div>
                            <div class="col-6 ">

                            </div>
                            <div class="col-6 " v-if="contractor.approvalStatus==6">
                                <span> <span style="color:#787486;font-size:16px">Completion Date:</span> :{{contractor.completionDate}}</span>


                            </div>
                            <div class="col-6 " v-else>
                                <label>Completion Date:</label>
                                <datepicker v-model="contractor.completionDate"></datepicker>

                            </div>
                            <div class="col-lg-12 mt-3">

                                <contractor-item @update:modelValue="SaveContractorItems" v-bind:readOnly="true" v-bind:contractor="contractor" :key="randerContractorList" />
                            </div>
                            <div class="text-right col-lg-12">
                                <button type="button" class="btn btn-primary" v-on:click="SaveContractor('Complete')" v-if="contractor.approvalStatus==5">Complete</button>
                                <button type="button" class="btn btn-danger" style="margin-right:8px" v-on:click="CloseContractorModel">{{ $t('Cancel') }}</button>

                            </div>
                        </div>
                        <div>

                        </div>
                    </div>
                </div>
            </div>


            <br />

            <div v-if="!loading" class="col-md-12 text-right">
                <div v-if="purchase.id === '00000000-0000-0000-0000-000000000000'">



                </div>
                <div v-else>

                    <button class="btn btn-danger  mr-2"
                            v-on:click="goToPurchase">
                        {{ $t('AddProductionBatch.Cancel') }}
                    </button>
                </div>




            </div>
            <div class="card-footer col-md-3" v-else>
                <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            </div>
            <!--<remainingStockmodel :purchase="purchase"
                             :show="show"
                             v-if="show"
                             @close="show=false" />-->
        </div>

        <gate-pass-report :printDetails="printDetails" :headerFooter="headerFooter" v-if="ShowPrint" v-bind:key="printRender"></gate-pass-report>
    </div>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    import Multiselect from 'vue-multiselect';
    import moment from "moment";
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    //import Vue3Barcode from 'vue3-barcode'
    export default {
        mixins: [clickMixin],
        components: {
            Loading,
            Multiselect
        },

        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                randerProcess: 0,
                purchaseItemRander: 0,
                randerPaymentList: 0,
                currentPage: 1,
                search: '',
                purchase: {
                    id: "00000000-0000-0000-0000-000000000000",
                    date: "",
                    registrationNo: "",
                    employeeRegistrationId: "",
                    expireDate: "",
                    recipeNoId: "",
                    saleOrderId: "",
                    noOfBatches: 1,
                    productionBatchItems: [],
                    processlist: [],
                    startTime: "",
                    endTime: "",

                },
                ProcessActive: true,
                disable: false,
                loading: false,
                processlist: [],
                paymentVoucherList: [],
                selectedProcess: [],
                contractorlist: [],
                productList: [],
                contractor: {
                    date: '',
                    fromDate: '',
                    completionDate: '',
                    toDate: '',
                    contractorType: '',
                    comments: '',
                    contractorNameEn: '',
                    contractorAccountId: '',
                    processId: '',
                    batchProcessId: '',
                    contractorId: '',
                    productionBatchId: '',
                    contractorItems: [],
                    productList: [],
                },
                payment: false,
                purchaseOrderId: '',
                active: true,
                contractorDiv: false,
                currency: '',
                randerContractorList: 0,
                printRender: 0,

                printDetails: '',
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },


                show: false,
                ShowPrint: false,
            };
        },
        validations() {
            return {
                purchase: {
                    date: { required },
                    expireDate: {},
                    registrationNo: { required },
                    recipeNoId: {

                    },

                    productionBatchItems: {
                    },
                },
            }
        },
        methods: {

            PrintReport: function (value) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.printDetails = [];
                root.$https.get("/Batch/GatePassDetail?id=" + value, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {

                            root.printDetails = response.data;
                            root.ShowPrint = true;
                            root.printRender++;
                        }
                    });
            },
            getDate: function (date) {
                if (date == null || date == undefined) {
                    return "";
                }
                else {
                    return moment(date).format('LLL');
                }
            },
            CloseContractorModel: function () {
                this.contractorDiv = false;
                this.selectedProcess.processContractors.find(function (x) {
                    if (x.isSelect == true) {
                        x.isSelect = false;

                    }

                });

            },

            ProcessListRecord: function () {


                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Batch/ProcessContractorList?id=' + root.contractor.productionBatchId, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {


                            root.purchase.processList = response.data;
                            root.purchase.processList.forEach(function (result) {

                                if (result.isActive) {

                                    root.selectedProcess = result;

                                }
                                else {
                                    result.isActive = false;
                                }


                            });
                            root.CloseContractorModel();



                        }
                    });
            },
            SaveContractor: function (x) {

                var root = this;
                var token = '';
                this.contractor.approvalStatus = x;
                this.loading = true;
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Batch/AddProcessContractorItems', this.contractor, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {

                            //root.PrintReport(response.data);
                            root.ProcessListRecord(root.contractor.batchProcessId);
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });


                            //root.selectedProcess.processContractors.find(function (y) {
                            //    if (y.contractorId == x.contractorId) {
                            //        if (x.approvalStatus == 'InProcess') {
                            //            y.approvalStatus = 5

                            //        }
                            //        else if (x.approvalStatus == 'Complete') {
                            //            y.approvalStatus = 6
                            //        }

                            //    }

                            //});
                            //root.randerProcess++;


                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                type: 'error',
                                icon: 'error',
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
            },

            EffectOnItems: function () {
                this.purchaseItemRander++;
            },
            GetPaymentVoucher: function (id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Batch/ProcessContractorPaymentList?id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.paymentVoucherList = response.data;
                            root.randerPaymentList++;
                        }
                    });
            },
            PaymentList: function (id) {
                this.payment = false;
                this.GetPaymentVoucher(id);
            },
            SaveContractorItems: function (contractorItems) {

                this.contractor.contractorItems = contractorItems;
            },
            ContractorDetail: function (contractor) {

                var root = this;
                this.contractorDiv = true;
                this.contractor = {
                    id: '',
                    approvalStatus: '',
                    date: '',
                    fromDate: '',
                    contractorId: '',
                    toDate: '',
                    completionDate: '',
                    contractorNameEn: '',
                    contractorType: '',
                    contractorAccountId: '',
                    processId: '',
                    batchProcessId: '',
                    batchProcessContractorId: '',
                    contractorItems: [],
                };
                if (contractor.approvalStatus == 5) {
                    root.contractor.contractorItems = contractor.contractorItems;
                    root.contractor.completionDate = moment().format('LLL');
                }
                else if (contractor.approvalStatus == 6) {
                    root.contractor.contractorItems = contractor.contractorItems;

                }
                else {
                    if (root.selectedProcess.processItems.length > 0) {
                        root.selectedProcess.processItems.forEach(function (processItem) {
                            root.contractor.processId = processItem.id;
                            root.purchase.productionBatchItems.forEach(function (batchItem) {
                                if (processItem.productId == batchItem.productId) {
                                    root.contractor.contractorItems.push(batchItem);
                                }

                            });

                        });
                        root.selectedProcess.processContractors.forEach(function (x) {
                            if (contractor.contractorId == x.contractorId) {
                                x.isSelect = true;
                            }
                            else {
                                x.isSelect = false;
                            }

                        });
                    }

                }
                root.randerContractorList++;

                this.contractor.batchProcessId = contractor.batchProcessId;
                this.contractor.approvalStatus = contractor.approvalStatus;
                this.contractor.contractorType = contractor.contractorType;
                this.contractor.fromDate = contractor.fromDate;
                if (this.contractor.fromDate == null) {
                    this.contractor.fromDate = moment().format('LLL');

                }
                this.contractor.toDate = contractor.toDate;
                if (this.contractor.toDate == null) {
                    //this.contractor.fromDate = moment().format('LLL');
                    this.contractor.toDate = moment().format('LLL');

                }

                this.contractor.fromDates = contractor.fromDates;
                this.contractor.toDates = contractor.toDates;
                this.contractor.contractorAccountId = contractor.contractorAccountId;
                this.contractor.contractorNameEn = contractor.contractorNameEn;
                this.contractor.id = contractor.id;
                this.contractor.contractorId = contractor.contractorId;
                this.contractor.batchProcessContractorId = contractor.id;
                this.contractor.productionBatchId = contractor.productionBatchId;
                this.contractor.date = moment().format('LLL');
                this.GetPaymentVoucher(this.contractor.batchProcessContractorId);
            },

            makeActiveProcess: function (process) {

                var isTab = false;
                if (this.selectedProcess.id == process.id) {
                    return;
                }
                this.purchase.processList.forEach(function (x) {

                    {
                        if (x.approvalStatus == 0 || x.approvalStatus == 4) {
                            return;
                        }
                        else {
                            if (x.id == process.id) {
                                isTab = true;
                                x.isActive = true;
                            }
                            else {
                                x.isActive = false;
                            }
                        }
                    }



                });
                if (isTab) {
                    this.selectedProcess = [];
                    this.selectedProcess = process;
                    this.contractorDiv = false;
                    this.selectedProcess.processContractors.find(function (x) {
                        if (x.isSelect == true) {
                            x.isSelect = false;

                        }

                    });
                }

            },
            SelectContractor: function (contractorId, processId) {

                var root = this;
                this.contractorlist.find(function (x) {
                    if (x.contractorId == contractorId) {
                        if (x.isActive == false) {
                            x.isActive = true;
                        }
                        else {
                            x.isActive = false;
                        }

                    }

                });
                root.processlist.forEach(function (x) {
                    if (x.id == processId) {

                        x.processContractors.forEach(function (x) {
                            console.log(x);
                            if (x.contractorId == contractorId) {
                                x = root.contractorlist;
                            }

                        });
                    }
                });



            },
            GetProcessData: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Batch/ProcessList?isDropdown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {


                        response.data.results.forEach(function (result) {
                            root.processlist.push({
                                id: result.id,
                                code: result.code,
                                color: result.color,
                                description: result.description,
                                englishName: result.englishName,
                                processContractors: result.processContractors,
                                processItems: result.processItems,
                                isActive: false,
                            })
                        })

                        root.$store.GetProcessList(root.processlist);
                    }
                    root.loading = false;
                });
            },

            GetFinishProduct: function (id) {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                root.$https.get('/Batch/RecipeNoDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {


                            //root.purchase.recipeQuantity = response.data.quantity;
                            //response.data.recipeNoItems.forEach(function (recipe) {
                            //    alert(root.processItems);
                            //    root.processItems.forEach(function (x) {
                            //        if (x.isActive == true) {
                            //            x.forEach(function (result) {
                            //                if (result.productId == recipe.productId) {
                            //                    root.purchase.productionBatchItems.push(recipe);
                            //                }
                            //            })
                            //        }


                            //    });


                            //});

                            root.purchase.recipeQuantity = response.data.quantity;
                            root.purchase.productionBatchItems = response.data.recipeNoItems;
                            //root.productList.forEach(function (result) {
                            //    response.data.recipeNoItems.forEach(function (recipe) {
                            //        if (result.productId == recipe.productId) {
                            //            root.purchase.productionBatchItems.push(recipe);
                            //        }

                            //    });

                            //});
                            root.purchaseItemRander++;


                        }
                    });

            },

            AutoIncrementCode: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                root.$https
                    .get("/Batch/ProductionBatchAutoGenerateNo", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            root.purchase.registrationNo = response.data;
                        }
                    });
            },

            SavePurchaseItems: function (productionBatchItems) {

                var root = this;
                for (var y in productionBatchItems) {
                    if (productionBatchItems[y].wareHouseId == null) {
                        root.disable = true;
                        break;
                    }
                    else {
                        root.disable = false;

                    }

                }
                this.purchase.productionBatchItems = productionBatchItems;
            },
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {

                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;

                        }
                        else {


                            root.filePath = '';
                            root.headerFooter.company.logoPath = '';
                        }
                    })
            },

            GetInvoicePrintSetting: function () {
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Sale/printSettingDetail', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {

                            root.headerFooter.footerEn = response.data.termsInEng;
                            root.headerFooter.footerAr = response.data.termsInAr;
                            root.headerFooter.isHeaderFooter = response.data.isHeaderFooter;
                            root.headerFooter.englishName = response.data.englishName;
                            root.headerFooter.arabicName = response.data.arabicName;
                            root.headerFooter.customerAddress = response.data.customerAddress;
                            root.headerFooter.customerVat = response.data.customerVat;
                            root.headerFooter.customerNumber = response.data.customerNumber;
                            root.headerFooter.cargoName = response.data.cargoName;
                            root.headerFooter.customerTelephone = response.data.customerTelephone;
                            root.headerFooter.itemPieceSize = response.data.itemPieceSize;
                            root.headerFooter.styleNo = response.data.styleNo;
                            root.headerFooter.blindPrint = response.data.blindPrint;
                            root.headerFooter.showBankAccount = response.data.showBankAccount;
                            root.headerFooter.bankAccount1 = response.data.bankAccount1;
                            root.headerFooter.bankAccount2 = response.data.bankAccount2;
                            root.headerFooter.bankIcon1 = response.data.bankIcon1;
                            root.headerFooter.bankIcon2 = response.data.bankIcon2;
                            root.headerFooter.customerNameEn = response.data.customerNameEn;
                            root.headerFooter.customerNameAr = response.data.customerNameAr;

                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            },

            GetHeaderDetail: function () {


                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;
                            root.GetInvoicePrintSetting();
                            root.getBase64Image(root.headerFooter.company.logoPath);
                        }
                    });
            },

            goToPurchase: function () {
                this.$router.push('/ProductionBatch');
            },
        },
        created: function () {
            this.currency = localStorage.getItem('currency');

            var root =  this;

if(root.$route.query.Add == 'false')
{
    root.$route.query.data = this.$store.isGetEdit;
}
            this.$emit('update:modelValue', this.$route.name);
            if (this.$route.query.data != undefined) {

                this.purchase = this.$route.query.data;
                this.purchase.processList.forEach(function (result, index) {

                    if (index == 0) {

                        result.isActive = true;
                        root.selectedProcess = result;

                    }
                    else {
                        result.isActive = false;
                    }
                    index++;

                });
                this.purchase.date = moment(this.purchase.date).format('LLL');
            }
        },
        mounted: function () {
            this.ShowPrint = false;
            this.GetHeaderDetail();
            if (this.$route.query.data == undefined) {
                this.AutoIncrementCode();
                this.GetProcessData();

                this.purchase.date = moment().format('LLL');
            }
        },
    };
</script>
<style scoped>
    .cardStyle {
        height: 80px !important;
        width: 302px;
        left: 0px;
        top: 0px;
        background-color: #FFFFFF;
        border-style: solid;
        border: 1px solid rgba(0,0,0,.2);
        height: 50px;
        border-radius: 10px
    }

    .CardHeading {
        font-style: normal;
        font-weight: 600;
        font-size: 16px;
        line-height: 25px;
        /* identical to box height, or 156% */
        /* Text/Heading/Dark */

        color: #27272E;
    }

    .description {
        font-style: normal;
        font-weight: 400;
        font-size: 14px;
        line-height: 23px;
        /* identical to box height, or 164% */

        font-feature-settings: 'salt' on;
        /* Text/Body/Light */

        color: #425466;
    }

    .drop-zone {
        background-color: #eee;
        margin-bottom: 10px;
        padding: 10px;
    }

    .drag-el {
        background-color: #fff;
        margin-bottom: 10px;
        padding: 5px;
    }


    .circletag {
        display: block;
        width: 36px;
        height: 36px;
        text-align: center;
        align-content: center;
        align-items: center;
        border-radius: 50%;
        -moz-border-radius: 50%;
        -webkit-border-radius: 50%;
    }

    .iconSize {
        width: 16px;
        height: 16px;
    }

    .ProcessHeading {
        font-style: normal;
        font-weight: 600;
        font-size: 24px;
        line-height: 23px;
        /* identical to box height */

        letter-spacing: 0.01em;
        color: #3178F6;
    }

    .opacity50 {
        background-color: #F4F4F4;
    }

    .cardStyle:hover {
        border-color: #3178F6;
    }

    .ActiveColor {
        background-color: #f0f5ff !important;
    }

    .dot {
        height: 7.47px;
        width: 7.47px;
        background-color: #F2C94C;
        border-radius: 50%;
        display: inline-block;
    }

    .dot1 {
        height: 7.47px;
        width: 7.47px;
        background-color: #2F80ED;
        border-radius: 50%;
        display: inline-block;
    }

    .dot2 {
        height: 7.47px;
        width: 7.47px;
        background-color: #68B266;
        border-radius: 50%;
        display: inline-block;
    }

    .ContractorCard {
        padding: 10px;
        width: 100%;
        background: #F9F9F9;
        border-radius: 4px;
    }

    .disabledTab {
        pointer-events: none;
    }
</style>
