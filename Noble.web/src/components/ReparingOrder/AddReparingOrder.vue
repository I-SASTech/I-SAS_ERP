<template>
    <div class="row" v-if="isValid('CanViewPurchaseDraft')" v-bind:key="randerAll">
        <div class="col-lg-12 mb-5">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="reparingOrder.type == 'Add'" class="page-title">
                                    Add Reparing Order
                                </h4>
                                <h4 v-else-if="view" class="page-title">
                                    Reparing Order
                                </h4>
                                <h4 v-else class="page-title">
                                    Update Reparing Order
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
                <div class="col-lg-3">
                    <label>Reparing Order Number</label>
                    <input type="text" class="form-control" v-model="reparingOrder.registrationNo" disabled />
                </div>
                <div class="col-lg-3">
                    <label>Customer :<span class="text-danger"> *</span></label>
                    <customerdropdown1 v-model="reparingOrder.customerId" :reparingOrder="true" :disable="view" :modelValue="reparingOrder.customerId" :key="rander+randerCustomer" />
                </div>
                <div class="col-lg-3">
                    <label>Employee :<span class="text-danger"> *</span></label>
                    <employeeDropdown v-model="reparingOrder.employeeId" :modelValue="reparingOrder.employeeId" :disable="view" />
                </div>
                <div class="col-lg-3">
                    <button class="btn btn-primary btn-sm mt-4" v-on:click="OpenModel">Customer</button>
                </div>
            </div>

            <div class="row mt-3">
                <div class="col-lg-12">

                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link" v-bind:class="{active:active == 'JobDetails'}"
                               v-on:click="makeActive('JobDetails')" data-bs-toggle="tab" href="#JobDetails" role="tab" aria-selected="true">
                                Job Details
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" v-bind:class="{active:active == 'PartDetails'}"
                               v-on:click="makeActive('PartDetails')" data-bs-toggle="tab" href="#PartDetails" role="tab" aria-selected="true">
                                Part Details
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" v-bind:class="{active:active == 'Info'}"
                               v-on:click="makeActive('Info')" data-bs-toggle="tab" href="#Info" role="tab" aria-selected="true">
                                Info
                            </a>
                        </li>
                    </ul>


                    <div class="tab-content">
                        <div class="tab-pane pt-3" id="JobDetails" role="tabpanel" v-bind:class="{ active: active == 'JobDetails' }">
                            <div class="row ">
                                <div class="col-lg-4 border">
                                    <div class="row pt-3">
                                        <div class="col-lg-12 form-group">
                                            <label>Warranty Category :</label>
                                            <reparingordertypeDropdown :disable="view" v-model="reparingOrder.warrantyCategoryId" :isWarranty="true" :update="update" :modelValue="reparingOrder.warrantyCategoryId" :formName="'WarrantyCategory'"></reparingordertypeDropdown>
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>UPS Description :<span class="text-danger"> *</span></label>
                                            <reparingordertypeDropdown :disable="view" v-model="reparingOrder.upsDescriptionId" :modelValue="reparingOrder.upsDescriptionId" :formName="'UpsDescription'"></reparingordertypeDropdown>
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>Serial No :<span class="text-danger"> *</span></label>
                                            <input class="form-control" v-bind:disabled="view" v-model="reparingOrder.serialNo" />
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('ReparingOrder.Problem') }}:<span class="text-danger"> *</span></label>
                                            <reparingordertypeDropdown :disable="view" v-model="reparingOrder.problemId" :modelValue="reparingOrder.problemId" :formName="'Problem'"></reparingordertypeDropdown>
                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>{{ $t('ReparingOrder.AcessoryIncluded') }} :</label>
                                            <reparingordertypeDropdown :disable="view" v-model="reparingOrder.acessoryIncludedId" :modelValue="reparingOrder.acessoryIncludedId" :formName="'AcessoryIncluded'"></reparingordertypeDropdown>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 border">
                                    <div class="row pt-3">
                                        <h6 class="text-center">Received Payment Again Job</h6>
                                        <div class="col-lg-12 form-group">
                                            <label>Charges :</label>
                                            <input v-bind:disabled="!reparingOrder.isComplete" class="form-control" @click="$event.target.select()" v-model="reparingOrder.advanceAmount" type="number" />

                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>Payment :</label>
                                            <input v-bind:disabled="!reparingOrder.isComplete" class="form-control" @update:modelValue="RemainingBalance(reparingOrder.cashAmount)" @click="$event.target.select()" v-model="reparingOrder.cashAmount" type="number" />

                                        </div>
                                        <hr />
                                        <div class="col-lg-12 form-group">
                                            <label>Balance :</label>
                                            <input v-bind:disabled="!reparingOrder.isComplete" class="form-control" @click="$event.target.select()" v-model="balance" type="number" />

                                        </div>
                                        <div class="col-lg-12 form-group" v-if="reparingOrder.type != 'Add'">
                                            <button v-bind:disabled="!reparingOrder.isComplete" class="btn btn-primary   mr-2" v-on:click="savePurchasePost(false,true)"><i class="fa fa-print"></i>  Print</button>

                                        </div>

                                        <div class="col-lg-6">
                                            <div class="inline-fields mx-1">
                                                <div class="checkbox form-check-inline">
                                                    <input type="checkbox" id="inlineCheckbox1" v-model="reparingOrder.isRepared" v-on:change="ChangeValue()" v-bind:key="randerToggle">
                                                    <label for="inlineCheckbox1">Is Repared </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="inline-fields mx-1">
                                                <div class="checkbox form-check-inline">
                                                    <input type="checkbox" id="inlineCheckbox2" v-model="reparingOrder.isReturn" v-on:change="ChangeValue()" v-bind:key="randerToggle">
                                                    <label for="inlineCheckbox2">Is Returned </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="inline-fields mx-1">
                                                <div class="checkbox form-check-inline">
                                                    <input type="checkbox" id="inlineCheckbox3" v-model="reparingOrder.isComplete" v-bind:key="randerToggle">
                                                    <label for="inlineCheckbox3">Job Complete </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-lg-4 border">
                                    <div class="row pt-3">
                                        <div class="col-lg-12 form-group">
                                            <label>Date</label>
                                            <input type="text" class="form-control" v-model="reparingOrder.date" disabled />
                                        </div>

                                        <div class="col-lg-12 form-group">
                                            <label>Expected Date:</label>
                                            <datepicker v-model="reparingOrder.expectedDate" :isDisable="view" v-bind:key="rander" />
                                        </div>
                                        <div class="col-lg-12  form-group">
                                            <label>Complete Date:</label>
                                            <datepicker v-model="reparingOrder.completeDate" :isDisable="view" />
                                        </div>
                                        <div class="col-lg-6 form-group">
                                            <label>Status :</label>
                                            <multiselect :options="['In Progress','Complete','New','Printed','Repaired','Cash Received']" v-bind:disabled="view" v-model="reparingOrder.status" :show-labels="false" v-bind:placeholder="$t('SelectMethod')" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                            </multiselect>

                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>Job Assigned To:</label>
                                            <usersDropdown v-model="reparingOrder.jobAssignId" :modelValue="reparingOrder.jobAssignId" :alluser="'true'"></usersDropdown>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-12 mt-3">
                                    <div class="row">
                                        <div class="col-xs-4 col-sm-4 col-md-3 col-lg-3 " v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                            <label>Payment Type :</label>
                                            <multiselect v-model="reparingOrder.paymentType" :options="['Cash','Bank']" v-bind:disabled="view" :show-labels="false" v-bind:placeholder="$t('SelectMethod')" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                            </multiselect>
                                        </div>
                                        <div class="col-xs-4 col-sm-4 col-md-3 col-lg-3 " v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                            <label>Estimate Amount :</label>
                                            <input v-bind:disabled="view" class="form-control" @click="$event.target.select()" v-model="reparingOrder.estimateAmount" type="number" />

                                        </div>
                                        <div class="col-xs-4 col-sm-4 col-md-3 col-lg-3 " v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">

                                            <label>Registered By:</label>
                                            <usersDropdown v-model="reparingOrder.registeredById" :alluser="'true'" :modelValue="reparingOrder.registeredById"></usersDropdown>

                                        </div>
                                        <!--<div class="col-xs-4 col-sm-4 col-md-3 col-lg-3 " v-if="reparingOrder.type == 'Add'" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                        <label>Advance Payment :</label>
                        <input v-bind:disabled="view" class="form-control" @click="$event.target.select()" v-model="reparingOrder.advanceAmount" type="number" />

                    </div>-->
                                        <div class="col-xs-4 col-sm-4 col-md-3 col-lg-3 " v-if="reparingOrder.type != 'Add'" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                            <!--<label>Cash Received:<span style="font-size:12px !important">(Advance Payment :{{reparingOrder.advanceAmount}})</span></label>
                        <input v-bind:disabled="view" class="form-control" @click="$event.target.select()" v-model="reparingOrder.cashAmount" type="number" />-->
                                            <div class="col-xs-4 col-sm-4 col-md-3 col-lg-3" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                                <label>Remarks:</label>
                                                <input v-bind:disabled="view" v-model="reparingOrder.remarks" class="form-control" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" />


                                            </div>
                                        </div>


                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="tab-pane pt-3" id="PartDetails" role="tabpanel" v-bind:class="{ active: active == 'PartDetails' }">
                            <div class="row mt-3">
                                <ReparingOrderItem @update:modelValue="SavePurchaseItems"></ReparingOrderItem>
                            </div>
                        </div>
                        <div class="tab-pane pt-3" id="Info" role="tabpanel" v-bind:class="{ active: active == 'Info' }">
                            <div class="row mt-3">
                                <div class="col-lg-4">
                                    <div class="row">
                                        
                                        <div class="col-lg-12 form-group">
                                            <label>Card No :</label>
                                            <input v-bind:disabled="view" class="form-control" v-model="reparingOrder.cradNumber" type="text" />

                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>Purchase Date:</label>
                                            <datepicker v-model="reparingOrder.purchaseDate" :isDisable="view" />

                                        </div>
                                        <div class="col-lg-12 form-group">
                                            <label>Dealer Ref:</label>
                                            <input v-bind:disabled="view" class="form-control" v-model="reparingOrder.dealerRef" type="text" />

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12 invoice-btn-fixed-bottom">
            <div class="button-items" v-if="reparingOrder.type == 'Add'">
                <button class="btn btn-outline-primary  mr-2" v-bind:disabled="v$.reparingOrder.$invalid" v-shortkey="['shift', 'enter']" @shortkey="savePurchasePost(true)" v-on:click="savePurchasePost(true)">
                    <i class="far fa-save"></i> Save as Print
                </button>

                <button class="btn btn-danger  mr-2" v-on:click="goToPurchase" v-shortkey="['del']" @shortkey="goToPurchase">
                    {{ $t('AddQuotation.Cancel') }}
                </button>
            </div>
            <div class="button-items" v-else>
                <button class="btn btn-outline-primary  mr-2" v-bind:disabled="v$.reparingOrder.$invalid" v-shortkey="['shift', 'enter']" @shortkey="savePurchasePost(true)" v-on:click="savePurchasePost(true)">
                    <i class="far fa-save"></i> Update as Print
                </button>

                <button class="btn btn-danger  mr-2" v-on:click="goToPurchase" v-shortkey="['del']" @shortkey="goToPurchase">
                    {{ $t('AddQuotation.Cancel') }}
                </button>
            </div>
        </div>

        <loading v-model:active="loading"
                 :can-cancel="true"
                 :is-full-page="true"></loading>
        <AddCustomerModel :newshow="show" v-if="show" @CustomerId="CustomerId" :mobileNo="mobileNo" @close="IsSave" />
        <ReparingOrderThermalPrint :newprintDetails="printDetails" :headerFooter="headerFooter" :AddReparingOrder="true" v-if="isPrint" v-bind:key="printRander" />
        <ReparingOrderPaymentPrint :printDetails="printDetails" :headerFooter="headerFooter" :AddReparingOrder="true" v-if="isPrint2" v-bind:key="printRander2" />

    </div>



    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import moment from "moment";
    
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    //import Vue3Barcode from 'vue3-barcode'
    export default {
        mixins: [clickMixin],
        name: "ReparingOrder",
        components: {
            
            Multiselect,
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                printRander: 0,
                accountRender: 0,
                randerAll: 0,
                randerToggle: 0,
                view: false,
                show: false,
                isPrint: false,
                payment: false,
                update: false,
                isFifo: false,
                active: 'JobDetails',
                purchaseOrderId: "",
                purchaseOrder: false,

                internationalPurchase: '',
                currency: '',
                mobileNo: '',
                reparingOrder: {
                    id: "00000000-0000-0000-0000-000000000000",
                    registrationNo: "",
                    type: "",
                    registeredById: "",
                    accountId: "",
                    date: "",
                    employeeId: "",
                    warrantyCategoryId: "",
                    upsDescriptionId: "",
                    serialNo: "",
                    problemId: "",
                    paymentType: "Cash",
                    acessoryIncludedId: "",
                    receivedDate: "",
                    expectedDate: "",
                    status: "In Progress",
                    jobAssignId: "",
                    completeDate: "",
                    remarks: "",
                    remaningPrice: 0,
                    discount: 0,
                    isComplete: false,
                    isCollected: false,
                    isReturn: false,
                    isRepared: false,
                    IsCashed: false,
                    estimateAmount: 0,
                    advancePayment: 0,
                    advanceAmount: 0,
                    cashAmount: 0,
                    paymentVouchers: [],
                    reparingItems: [],
                    branchId: '',

                },

                printDetails: [],
                options: [],
                loading: false,
                isPrint2: false,
                rander: 0,
                printRander2: 0,
                balance: 0,
                raw: '',
                randerCustomer: 0,
                printRender: 0,
                randerForClose: 0,
                autoNumber: '',
                language: 'Nothing',
                supplierRender: 0,
                wareRander: 0,
                display: false,
                isAttachshow: false,
                attachment: false,
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
            };
        },
        validations() {
            return {
                reparingOrder: {
                    date: {},
                    registrationNo: {},
                    customerId: {},
                    serialNo: {},
                    employeeId: { required },

                },
                }
        },

        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },

            ChangeValue: function () {
                
                if (this.reparingOrder.isRepared) {
                    this.reparingOrder.isReturn = false;
                }
                else {
                    if (this.reparingOrder.isReturn == false) {
                        this.reparingOrder.isReturn = false;

                    }
                    else {
                        this.reparingOrder.isReturn = true;

                    }
                }
                this.randerToggle++;
            },
            CustomerId: function (id) {
                this.show = false;
                this.reparingOrder.customerId = id;

                this.randerCustomer++;
            },
            OpenModel: function () {
                this.mobileNo = '';
                this.show = true;
            },
            IsSave: function () {
                this.show = false;
            },
            PrintInvoiceForPayment: function (value) {

                var id = value;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get("/ReparingOrder/ReparingOrderDetail?Id=" + id, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {


                            root.printDetails = response.data;

                            root.isPrint2 = true;

                            root.printRander2++;



                        }
                    });
            },
            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.headerFooter.company.logoPath = 'data:image/png;base64,' + 'iVBORw0KGgoAAAANSUhEUgAAATYAAACjCAMAAAA3vsLfAAAAhFBMVEX///8AcuEAZ98Ab+AAa+AAbeAAad9Tk+cAbuC+1fUFeOLS4/no8/0AZd8AaODv9v1to+vk7/z1+v7d6vqBru3F2vemxfKHsu5Zl+iYvfDU5PnM3/g9iea20PWNte5Rk+gwhOVIjuepx/NjnOl2p+wlfuS30fUAX96cvvAde+M1huWoxfIDNGHbAAAHKElEQVR4nO2d65aiOhBGJQlpVAKId6VVtJ3ROe//fgfvqSQocS3EjrV/NsEJ38qlqlKVabUQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEGQ30E6nG6ybcfrbPNkN2+6N68kDWVimzdHOYlYEFDP8yj1GWGT79q6+W7kQoZWfi/8IfwgmEzA/UVaY1/fiCX49k7V1wbC90wwMq2zt2/D1zOyDSkzinaAb/v19vgteEa2hSgV7TBVxQcscU/IlpB7qhUI9yeqvWxJ9EC1QrdR7f1uGGvZpndn6EW3Yf09bxRb2WZVVCsWOMcNEVvZtqqxZsafvKDvDWIp245XUq2Ypm77WpayaWONRYIxwTXjl2av6H1j2Mm2VgYb88bzwo8Nhz9MFU50X9L/hrCTbQVHmxhcn8QbxS7xk3o73ixWsvXgNgrdgYViBbM6u900VrLBORrt4NMf6KgSlzcFK9k2gdRWX/SV0eayi2Ul215uTLQ1/xsMxmBTV5/fACvZ5KWNGtoC2eiqlg6/BzayxbJs/o/eIAc/Vj1U/PuwkQ1spGynNxgA443X1OV3wEa2viwbNwQjFwxl0wGjja/1BlA2lw23p9c2k33xAz2smrr8DljtpLL/ZLIvgO8VuOzMW8kGZNE3yhi4V047pVaygUnI/6iPp3BHcPlAwUo24AZo9m4Ko0rC5fNSK9nAnuAxZRZmAZDN5R3BMt4G3QAykJ9NYACEjWvsdeMosg3bJZxO8Nowpsaz60Scb5X8BtFr7JteAJTN42X8PeVwdWBzKrLdcDZrL1ZCOWVwOv6hyVYKP8n2Rz0mpYyTiDPtV9webLaytfLgcdtDc6dXNnvZwvIULQm6bfarasdWtla7SjYDc3uKPiFba/dYN8eP5FvPyNYaPdCNEtfzjZ6SrTWM7u0LzHPZqzrzjGytOFfNtNtQEy4HPq48JVvh1Hci04tUrJxf1o48KVurtV4JxcgNmMjbjXzE61lWlC3SC2Rm41UUMRYU+IWvwLKp62bHjaUgMtERyRdlB/wCY1ZpPByNk8lkkizWXcfTTiHxrKB/otfrnUuvIGlB0/1EnKQ3/16P1t/DiutUf3hsPv+cZU2nu8gOYaADEWHL0QMt5uMVuTbn+XT2ml6+F+HCIwwksBUGRbmbFI6L5uAAkJHO7tPWwDiJDCEhKnKzpxQmxNScs/FHCTfVU+TPNiwxZBaVN/cY/YD6vjNxdqf8jGinAnebe8LtU4QbM/9upJsvYfN+cD8wzrY2Jfe/lpnRK5d1+LJq7gXeB+hW4XQgWsjNH3uxgetHCa1qNXtiZtXcY86vb4MqNXvByqq5WiDjHn218D1gJGJcMeLo37MM/WpVuMUbbttvE7grUpKNZnEaz8e+L/1xewk/fmmbKPUZ8/WJyxZ3/9lfjjJ62PYa004nZ+OMRvvrjNNKvhnPB9Ppzz/dxWAuD7cBzO0Gpdrj4/yNOtIylUDngPKLExqO1UMZp5MpgQx+Dh8mzOMeyKOHY8pfSfZZ31OSkVYv6H5DdOGGoD72oGitYXRHmFiZpSKstetNApKUIy2HWY1+jGFOs+ILdOHCZyqTcQRQIHrNQO13z8xmXYkZzEHV6zngyucP1OfOIJv8t8/cwNOsM6IDV0KubZXQBqS5+twZ5EnHr0fDiTGYRv/BetKl/nMgRZXuX/klrwQkzJPrSlYi2xLWXBns2QQYw86WqoWyDtF15zPLFmxCuwo/ZwsjgQ7kgWz+IHxUTzr9DNlSINs1OGSWje3AJOX/6b8HDRT/lZ/yUkDp2XX4mGWLhmBLYAb7Al544W6sUq50vBUylsjWAxd9mJwn0D5w9y4ysPVdJ5VZNk9JhiPamf0c+F4OF12N5Fgtv5j9RtkOgwcs+fqNFrCQjbubIgjDbZe9dPP3dOczkWU4GBzQ81evGlvDuIBwOOAG7frO6UvjS5ZbJj09mnVwAHIQ45hDT97kRTgDtLRoRxYizXxVBWhhUCZl1ihjzRBPcYgYpiVQPr1MrXQHzupPJRqhog2ZnILoaVvLb6h8PfmvRLl0zeP+ZtQeDkdJYLznKVGaB1GwWi5XvnZSHxl8L4eItbOTgPGIc+WE6nL/sN7cowXaH50/lx89uiH8wO0aXXUJK0E4n1mZl6WqSbBb/PurSkEpcfqU9EjqPczqAIWO28f1y9xdv+pGaFiboGpgeU/3j4anmhDnKOH+7sQTyqaYZvfTZz6jxu/ApjwjJmB6tvigtC7ykOzr8nG8wndgHnBU5KbMyPm2JKWyaP9RhR3pmGv/AZNHyb7MRVp3iL41+GTlbtijhHS3F1I5R+BzsrwnQvuLcKk9ZRFPnL5+vZT+buKJw9myECwbtB9FftL2IGPn9nS5cP9igTukvUOJZPVY2bF96HBsDUEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQBEEQmf8BckJbXFsgizoAAAAASUVORK5CYII=';

                root.$https
                    .get('/Contact/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            root.filePath = response.data;
                            root.headerFooter.company.logoPath = 'data:image/png;base64,' + root.filePath;
                        }
                    });
            },
            SavePurchaseItems: function (purchaseItems) {

                this.reparingOrder.reparingItems = purchaseItems;
            },
            GetCustomerAccount: function () {

                var customer = this.$refs.customerDropdown.GetCustomerAddress();
                this.reparingOrder.accountId = customer.accountId;

            },
            RemainingBalance: function () {
                this.balance = this.reparingOrder.advanceAmount - this.reparingOrder.cashAmount;

            },
            paymentButton: function () {

                this.payment = true;


            },
            makeActive: function (item) {
                this.active = item;

            },

            getDate: function (date) {
                if (date == null || date == undefined) {
                    return "";
                }
                else {
                    return moment(date).format('LLL');
                }
            },

            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.reparingOrder.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addproduct');
                    }
                    else {
                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },
            GetPaymentVoucher: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('ReparingOrder/AdvancePaymentListQuery?id=' + this.reparingOrder.id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null && response.data != '') {
                            root.reparingOrder.paymentVouchers = response.data;
                        }
                    });
            },
            paymentSave: function (paymentVoucher) {

                if (paymentVoucher == false) {
                    this.payment = false;
                }
                else {
                    this.payment = false;
                    this.remaining = true;
                    if (paymentVoucher.amount == this.reparingOrder.estimateAmount) {
                        paymentVoucher.remaningPrice = 0
                    }
                    else {
                        this.reparingOrder.remaningPrice = this.reparingOrder.remaningPrice - paymentVoucher.amount;
                    }
                    this.reparingOrder.paymentVouchers.push(paymentVoucher);

                }


            },
            AutoIncrementCode: function () {

                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                root.$https
                    .get("/ReparingOrder/ReparingOrderAutoGenerateNo", {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then(function (response) {
                        if (response.data != null) {
                            {
                                root.reparingOrder.registrationNo = response.data;
                                root.autoNumber = response.data;
                            }

                        }
                    });
            },
            savePurchasePost: function (x, payment) {
                if (payment == null || payment == undefined)
                    payment = false;
                else
                    this.reparingOrder.isPrint = payment;


                this.loading = true;
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }
                if (this.reparingOrder.customerId == null || this.reparingOrder.customerId == '' || this.reparingOrder.customerId == undefined) {
                    root.$swal({
                        title: "Error!",
                        text: 'Customer is not empty',
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,

                    });
                    this.loading = false;
                    return;
                }
                if (this.reparingOrder.serialNo == null || this.reparingOrder.serialNo == '' || this.reparingOrder.serialNo == undefined) {
                    root.$swal({
                        title: "Error!",
                        text: 'Serial No is not emtpy',
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,

                    });
                    this.loading = false;
                    return;
                }
                if (this.reparingOrder.upsDescriptionId == null || this.reparingOrder.upsDescriptionId == '' || this.reparingOrder.upsDescriptionId == undefined) {
                    root.$swal({
                        title: "Error!",
                        text: 'UPS Description is not emtpy',
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,

                    });
                    this.loading = false;
                    return;
                }
                if (this.reparingOrder.problemId == null || this.reparingOrder.problemId == '' || this.reparingOrder.problemId == undefined) {
                    root.$swal({
                        title: "Error!",
                        text: 'Problem Issue is not emtpy',
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,

                    });
                    this.loading = false;
                    return;
                }

                if (this.reparingOrder.advanceAmount == null || this.reparingOrder.advanceAmount == '' || this.reparingOrder.advanceAmount == undefined)
                    this.reparingOrder.advanceAmount = 0;
                if (this.reparingOrder.advancePayment == null || this.reparingOrder.advancePayment == '' || this.reparingOrder.advancePayment == undefined)
                    this.reparingOrder.advancePayment = 0;
                if (this.reparingOrder.cashAmount == null || this.reparingOrder.cashAmount == '' || this.reparingOrder.cashAmount == undefined)
                    this.reparingOrder.cashAmount = 0;
                if (x == true)
                    this.reparingOrder.status = 'Printed';

                this.reparingOrder.branchId = localStorage.getItem('BranchId');

                this.$https
                    .post("/ReparingOrder/SaveReparingOrderInformation", this.reparingOrder, {
                        headers: { Authorization: `Bearer ${token}` },
                    })
                    .then((response) => {

                        if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                            root.loading = false
                            root.info = response.data.bpi


                            if (payment == true) {
                                root.PrintInvoiceForPayment(response.data.message.id);
                            }
                            else {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: response.data.message.isAddUpdate,
                                    type: 'success',
                                    confirmButtonClass: "btn btn-success",
                                    buttonStyling: false,
                                    icon: 'success',
                                    timer: 1500,
                                    timerProgressBar: true,

                                })
                                if (x == true) {
                                    root.PrintInvoice(response.data.message.id);

                                }
                                else {
                                    root.$router.push({
                                        path: '/ReparingOrder',

                                    });

                                }
                            }






                        }
                        else if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Update") {
                            root.loading = false
                            root.info = response.data.bpi
                            if (payment == true) {
                                root.PrintInvoiceForPayment(response.data.message.id);
                            }
                            else if (x == true) {
                                root.PrintInvoice(response.data.message.id);

                            }
                            else {
                                root.$router.push({
                                    path: '/ReparingOrder',

                                });

                            }

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: response.data.message.isAddUpdate,
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true,

                            });


                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: response.data.message.isAddUpdate,
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                icon: 'error',
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }

                    })
                    .catch((error) => {
                        console.log(error);
                        root.$swal.fire({
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            text: error.response.data,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                        root.loading = false;
                    })
                    .finally(() => (root.loading = false));
            },
            PrintInvoice: function (value) {

                var id = value;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get("/ReparingOrder/ReparingOrderDetail?Id=" + id, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                    .then(function (response) {
                        if (response.data != null) {

                            root.printDetails = response.data;

                            root.isPrint = true;

                            root.printRander++;






                        }
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

                            root.getBase64Image(root.headerFooter.company.logoPath);

                        }
                    });
            },

            goToPurchase: function () {
                this.$router.push({
                    path: '/ReparingOrder',

                })

            },
        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
              root.$route.query.data = this.$store.isGetEdit;
            }


            this.mobileNo = this.$route.query.mobileNo;
            
            var loginUserName = localStorage.getItem('LoginUserName');

            if (this.$route.query.data != undefined) {
                this.show = false;
                if (loginUserName == 'Ashfaq') {
                    this.view = false;

                }
                else {
                    this.view = true;

                }
                var detail = this.$route.query.data;

                this.reparingOrder = detail;
                this.update = true;
                this.reparingOrder.date = detail.date;
                this.reparingOrder.type = 'Update';
                this.balance = this.reparingOrder.payment;
                this.rander++;
                this.randerToggle++;


            }
            else {
                this.show = false;
                this.AutoIncrementCode();
                this.reparingOrder.type = 'Add';
                this.reparingOrder.employeeId = localStorage.getItem('EmployeeId');
                this.reparingOrder.jobAssignId = localStorage.getItem('UserID');
                this.reparingOrder.registeredById = localStorage.getItem('UserID');
                this.reparingOrder.date = moment().format("LLL");
                this.reparingOrder.expectedDate = moment().add(3, 'days').format("DD MMM YYYY");

            }

        },
        mounted: function () {
            this.GetHeaderDetail();
            this.language = this.$i18n.locale;

            this.currency = localStorage.getItem('Currency');




        },
    };
</script>

