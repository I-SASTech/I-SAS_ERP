<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel"> {{ $t('CashCounterDetail.CashCounterDetails') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="col-12 mb-2" style="border-bottom: 1px solid #EFF4F7; " v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'padding-left:0px' : 'padding-right:0px;'">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group" v-on:click="AdvanceFilter" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:right' : 'text-align:left'" v-if="showFilter">
                                <div class="form-group">
                                    <a href="javascript:void(0)" class="btn btn-outline-primary "><i class="fa fa-filter"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div>
                    <div class="row" v-if="showFilter">
                        <div class="col-lg-4 col-md-4 col-sm-4 col-4" v-if="advanceFilters">
                            <div class="form-group" style="width:100%">
                                <label>From Time</label><br />
                                <vue-timepicker v-model="fromTime" format="hh:mm A" input-width="100%" />
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-4" v-if="advanceFilters">
                            <div class="form-group" style="width:100%">
                                <label>To Time</label><br />
                                <vue-timepicker v-model="toTime" format="hh:mm A" input-width="100%" />
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-4 mt-3 " v-if="advanceFilters">
                            <button type="button" class="btn btn-primary  " v-if="fromTime=='' " disabled v-on:click="SearchTimeWise"> {{ $t('CashCounterDetail.Search') }}</button>
                            <button type="button" class="btn btn-primary  " v-else-if=" toTime==''" disabled v-on:click="SearchTimeWise"> {{ $t('CashCounterDetail.Search') }}</button>
                            <button type="button" class="btn btn-primary  " v-else v-on:click="SearchTimeWise"> {{ $t('CashCounterDetail.Search') }}</button>

                        </div>

                        <div class="col-sm-12 ">
                            <h6 class="modal-title labelHeading" id="myModalLabel" style=" padding-bottom:3px;padding-left:3px;"> {{ $t('CashCounterDetail.UserandCashCounterInfo') }}</h6>
                        </div>
                        <table class="table border ml-3 mr-3 table-bordered ">

                            <tr style="font-size:12px;padding-bottom:4px; ">
                                <td style="text-align:center;font-weight:bold;color:black !important"> <span>{{ $t('CashCounterDetail.CashCounterUser') }}</span></td>
                                <td style="text-align:center;color:black !important"> <span>{{UserName}}</span></td>
                                <td style="text-align:center;font-weight:bold;color:black !important"> <span>{{ $t('CashCounterDetail.Date&Time') }}</span></td>
                                <td style="text-align:center;color:black !important"> <span>{{startTime}}</span></td>

                            </tr>
                            <tr style="font-size:12px;padding-bottom:4px; ">
                                <td style="text-align:center;font-weight:bold;color:black !important"> <span>{{ $t('CashCounterDetail.Counter') }}</span></td>
                                <td style="text-align:center;color:black !important" colspan="3"> <span>{{dayEnd.counterCode}}</span></td>

                            </tr>


                        </table>


                    </div>
                    <!-- Row 2 -->
                    <div class="row">
                        <div class="col-sm-12 mb-2">
                            <h6 class="modal-title labelHeading" id="myModalLabel" style="padding-top:3px; padding-bottom:3px;padding-left:3px;">{{ $t('CashCounterDetail.SalesInfo') }}</h6>

                        </div>
                        <table class="table border ml-3 mr-3 table-bordered ">

                            <tr style="font-size:12px;padding-bottom:4px; ">
                                <td style="text-align:center;font-weight:bold;color:black !important"> <span>{{ $t('CashCounterDetail.OpeningCash') }}</span></td>
                                <td style="text-align:center;color:black !important"> <span>{{dayEnd.openingCash}}</span></td>

                                <td style="text-align:center;font-weight:bold;color:black !important"> <span>{{ $t('CashCounterDetail.CashSale') }}</span></td>
                                <td style="text-align:center;color:black !important"> <span>{{TotalCash()}}</span></td>
                            </tr>
                            <tr style="font-size:12px;padding-bottom:4px; " v-if="!isBankDetailShow">
                                <td style="text-align:center;font-weight:bold;color:black !important"> <span>{{ $t('CashCounterDetail.Expense') }}</span></td>
                                <td style="text-align:center;color:black !important"> <span> {{dayEnd.expenseCash}}</span></td>
                                <td style="text-align:center;font-weight:bold;color:black !important"> <span>{{ $t('CashCounterDetail.CashInHand') }}</span><span style="color:black;"> ({{ $t('CashCounterDetail.OpeningCashCashSaleExpense') }}) </span></td>
                                <td style="text-align:center;color:black !important"> <span> {{total}}</span></td>

                            </tr>
                            <tr style="font-size:12px;padding-bottom:4px; " v-if="isBankDetailShow">
                                <td style="text-align:center;font-weight:bold;color:black !important"> <span>{{ $t('CashCounterDetail.CashExpense') }}</span></td>
                                <td style="text-align:center;color:black !important"> <span> {{dayEnd.expenseCash}}</span></td>
                                <td style="text-align:center;font-weight:bold;color:black !important"> <span>{{ $t('CashCounterDetail.BankExpense') }}</span></td>
                                <td style="text-align:center;color:black !important"> <span> {{dayEnd.bankExpense}}</span></td>

                            </tr>
                            <tr style="font-size:12px;padding-bottom:4px; " v-if="isBankDetailShow">
                                <td style="text-align:center;font-weight:bold;color:black !important" colspan="3"> <span>{{ $t('CashCounterDetail.CashInHand') }}</span><span style="color:black;"> ({{ $t('CashCounterDetail.OpeningCashCashSaleExpense') }}) </span></td>
                                <td style="text-align:center;color:black !important"> <span> {{total}}</span></td>
                            </tr>



                        </table>
                    </div>

                    <!-- Row 4 -->
                    <div class="row mt-2">
                        <div class="col-sm-12">
                            <h6 class="modal-title labelHeading" id="myModalLabel" style="padding-top:3px; padding-bottom:3px;padding-left:3px">{{ $t('CashCounterDetail.BankInfo') }}</h6>
                            <div class="row">
                                <div class="col-sm-12 mt-2">
                                    <table class="table " v-if="!isBankDetailShow">

                                        <tr style="font-size:12px;padding-bottom:4px;border-color:black !important" v-for="bank in bankDetails" v-bind:key="bank.bankName">
                                            <td v-bind:style="        ($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left;border-top:1px solid;color: black !important;font-weight:bold' : 'text-align:right;border-top:1px solid;color: black !important;font-weight:bold'"> <span>{{bank.bankName}}</span>:</td>
                                            <td v-bind:style="        ($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:right;border-top:1px solid;color: black !important;font-weight:bold' : 'text-align:left;border-top:1px solid;color: black !important;font-weight:bold'">{{parseFloat(bank.totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        </tr>
                                        <tr>
                                            <td style="color: black;font-size:13px;padding-top:0px !important; padding-bottom:0px !important; font-weight:bold" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left' : 'text-align:right'"> <span>{{ $t('CashCounterDetail.BankTotal') }}</span>:</td>
                                            <td style="color: black;font-size:13px;padding-top:0px !important; padding-bottom:0px !important;font-weight:bold" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:right' : 'text-align:left'">{{parseFloat(dayEnd.bankAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        </tr>

                                    </table>
                                    <table class="table " v-else>

                                        <tr style="font-size:12px;padding-bottom:4px;border-color:black !important" v-for="(bank, index) in bankDetailList" v-bind:key="index">
                                            <td v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left;border-top:1px solid;color: black !important;font-weight:bold' : 'text-align:right;border-top:1px solid;color: black !important;font-weight:bold'"> <span>{{bank.bankName}}</span>:</td>
                                            <td v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:right;border-top:1px solid;color: black !important;font-weight:bold' : 'text-align:left;border-top:1px solid;color: black !important;font-weight:bold'">{{parseFloat(bank.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        </tr>
                                        <tr>
                                            <td style="color: black;font-size:13px;padding-top:0px !important; padding-bottom:0px !important; font-weight:bold" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left' : 'text-align:right'"> <span>{{ $t('CashCounterDetail.BankTotal') }}</span>:</td>
                                            <td style="color: black;font-size:13px;padding-top:0px !important; padding-bottom:0px !important;font-weight:bold" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:right' : 'text-align:left'">{{parseFloat(bankDetailListTotal).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                        </tr>

                                    </table>
                                </div>
                            </div>

                        </div>
                    </div>
                    <!-- Row 5 -->
                    <div class="row mt-2">
                        <div class="col-sm-12 ">
                            <div class="row border border-dark ml-1 mr-1" style="border:1px solid !important">

                                <div class="col-sm-12 mt-2 mb-2 text-center" style="font-size :14px; font-weight:bold;color:black;">
                                    <span>{{ $t('CashCounterDetail.TotalSale') }} <span style="color:black;font-size:13px">({{ $t('CashCounterDetail.Sales+Bank=') }})  </span> </span><span>{{getTotalSale()}}</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('CashCounterDetail.Close') }}</button>
            </div>
        </div>


    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    

    import moment from "moment";
    import VueTimepicker from 'vue3-timepicker/src/VueTimepicker.vue'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        mixins: [clickMixin],
        components: {

            VueTimepicker
        },
        props: ['show', 'cashInHand', 'openingCash', 'bank', 'expense', 'type', 'counterCode', 'counterId', 'dayStartId', 'outStandingBalance', 'dayWiseList', 'roport'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                bankDetailListTotal: 0,
                bankDetailList: [],
                isBankDetailShow: false,
                printReport: false,
                printA4Report: false,
                printRender: 0,
                render: 0,
                time: 0,
                dateRander: 0,
                UserName: '',
                fromTime: '',
                toTime: '',
                advanceFilters: false,
                isView: true,
                date: '',
                InvoiceSetting: '',
                startTime: '',
                total: 0,
                grandTotal: 0,
                transactions: 0,
                isSupervisor: false,
                counterRander: 0,
                bankDetails: [],
                showFilter: true,
                dayEnd: {
                    id: '',
                    date: '',
                    toTime: '',
                    isActive: true,
                    locationId: '',
                    openingCash: '',
                    cashInHand: '',
                    verifyCash: '',
                    user: '',
                    password: '',
                    creditReason: '',
                    carryCash: 0,
                    counterId: '00000000-0000-0000-0000-000000000000',
                    expenseCash: 0,
                    bankAmount: 0,
                    endTerminalBy: '',
                    supervisorName: '',
                    supervisorPassword: '',
                    isSupervisor: false,
                },
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                filePath: null,
            }
        },
        validations() {
            return {
                dayEnd: {
                    date: {
                    },
                    toTime: {

                    },
                    verifyCash: {
                        required
                    },
                    carryCash: {
                        required
                    },
                    user: {
                        required
                    },
                    password: {
                        required
                    },
                    creditReason: {
                        //required: requiredIf((x) => {

                        //    if (x.openingCash - x.cashInHand - x.bankAmount - x.expense - x.expenseCash > parseFloat(x.verifyCash))
                        //        return true;

                        //    return false;
                        //})
                    }

                }
                }
        },
        //watch: {

        //    fromTime: function () {
        //
        //        if (this.advanceFilters) {
        //            if (this.fromTime.split(':')[1] != 'mm') {
        //                this.getOpeningBalance( this.fromTime, this.toTime);
        //            }
        //        }
        //    },
        //    toTime: function () {
        //        if (this.advanceFilters) {
        //            if (this.toTime.split(':')[1] != 'mm') {
        //                this.getOpeningBalance(this.fromTime, this.toTime);
        //            }
        //        }
        //    }

        //},

        methods: {
            AdvanceFilter: function () {

                this.advanceFilters = !this.advanceFilters;
                if (this.advanceFilters == false) {
                    this.toTime = '';
                    this.getOpeningBalance();
                }


            },
            SearchTimeWise: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                //this.dayStart.counterId = id;
                this.$https.get('/Product/OpeningBalance?counterId=' + localStorage.getItem('CounterId') + '&isOpeningCash=' + false + '&fromTime=' + this.fromTime + '&toTime=' + this.toTime, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {

                        root.dayEnd.openingCash = parseFloat(response.data.openingBalance) < 0 ? parseFloat(parseFloat(response.data.openingBalance) * (-1)).toFixed(3).slice(0, -1) : parseFloat(response.data.openingBalance).toFixed(3).slice(0, -1)

                        root.dayEnd.cashInHand = parseFloat(response.data.cashInHand).toFixed(3).slice(0, -1)
                        root.dayEnd.bankAmount = parseFloat(response.data.bank).toFixed(3).slice(0, -1)
                        root.dayEnd.expenseCash = parseFloat(response.data.postExpense).toFixed(3).slice(0, -1)
                        root.dayEnd.bankExpense = parseFloat(response.data.bankExpense).toFixed(3).slice(0, -1)
                        root.dayEnd.counterCode = response.data.terminalCode;
                        root.dayEnd.totalVat = (parseFloat(response.data.totalVat) * (-1)).toFixed(3).slice(0, -1)
                        root.total = parseFloat(parseFloat(root.dayEnd.cashInHand)).toFixed(3).slice(0, -1)
                        root.transactions = response.data.noOfTransaction
                        root.startTime = response.data.startTime;
                        root.date = response.data.date;
                        root.bankDetailListTotal = response.data.bankDetailListTotal;
                        root.bankDetailList = response.data.bankDetailList;
                        root.bankDetails = response.data.bankDetails;
                        root.dayEnd.postExpense = parseFloat(response.data.postExpense).toFixed(3).slice(0, -1);
                        root.grandTotal = parseFloat(root.total).toFixed(3).slice(0, -1);
                        //root.total = 1;
                        root.counterRander++
                    }
                });
            }, getTimeOnly: function (x) {
                return moment(x).format('HH:mm ');
            },
            TotalCash: function () {
                return parseFloat(parseFloat(this.dayEnd.cashInHand) - parseFloat(this.dayEnd.openingCash) + parseFloat(this.dayEnd.expenseCash)).toFixed(3).slice(0, -1);
            },
            getTotalSale: function () {
                return parseFloat(parseFloat(this.TotalCash()) + parseFloat(this.dayEnd.bankAmount)).toFixed(3).slice(0, -1);
            },

            calculateGrandTotal: function () {
                this.grandTotal = parseFloat(parseFloat(this.dayEnd.verifyCash) - parseFloat(this.dayEnd.carryCash)).toFixed(3).slice(0, -1);
            },
            getOpeningBalance: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                //this.dayStart.counterId = id;
                this.$https.get('/Product/OpeningBalance?counterId=' + localStorage.getItem('CounterId') + '&isOpeningCash=' + false + '&fromTime=' + this.fromTime + '&toTime=' + this.toTime, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {


                        root.dayEnd.openingCash = parseFloat(response.data.openingBalance) < 0 ? parseFloat(parseFloat(response.data.openingBalance) * (-1)).toFixed(3).slice(0, -1) : parseFloat(response.data.openingBalance).toFixed(3).slice(0, -1)

                        root.dayEnd.cashInHand = parseFloat(response.data.cashInHand).toFixed(3).slice(0, -1)
                        root.dayEnd.bankAmount = parseFloat(response.data.bank).toFixed(3).slice(0, -1)
                        root.dayEnd.expenseCash = parseFloat(response.data.postExpense).toFixed(3).slice(0, -1)
                        root.dayEnd.bankExpense = parseFloat(response.data.bankExpense).toFixed(3).slice(0, -1)
                        root.dayEnd.counterCode = response.data.terminalCode;
                        root.dayEnd.totalVat = (parseFloat(response.data.totalVat) * (-1)).toFixed(3).slice(0, -1)
                        root.total = parseFloat(parseFloat(root.dayEnd.cashInHand)).toFixed(3).slice(0, -1)
                        root.transactions = response.data.noOfTransaction
                        root.startTime = response.data.startTime;
                        root.date = response.data.date;
                        root.bankDetailListTotal = response.data.bankDetailListTotal;
                        root.bankDetailList = response.data.bankDetailList;
                        root.bankDetails = response.data.bankDetails;
                        root.dayEnd.postExpense = parseFloat(response.data.postExpense).toFixed(3).slice(0, -1);
                        root.grandTotal = parseFloat(root.total).toFixed(3).slice(0, -1);
                        //root.total = 1;
                        root.counterRander++
                    }
                });
            },


            getOpeningBalanceForReport: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                //this.dayStart.counterId = id;
                this.$https.get('/Product/OpeningBalance?counterId=' + this.counterId + '&dayId=' + this.dayStartId + '&isReport=' + true + '&isOpeningCash=' + false, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {


                        root.dayEnd.openingCash = parseFloat(response.data.openingBalance) < 0 ? parseFloat(parseFloat(response.data.openingBalance) * (-1)).toFixed(3).slice(0, -1) : parseFloat(response.data.openingBalance).toFixed(3).slice(0, -1)

                        root.dayEnd.cashInHand = parseFloat(response.data.cashInHand).toFixed(3).slice(0, -1)
                        root.dayEnd.bankAmount = parseFloat(response.data.bank).toFixed(3).slice(0, -1)
                        root.dayEnd.expenseCash = parseFloat(response.data.postExpense).toFixed(3).slice(0, -1)
                        root.dayEnd.bankExpense = parseFloat(response.data.bankExpense).toFixed(3).slice(0, -1)
                        root.dayEnd.counterCode = response.data.terminalCode;
                        root.dayEnd.totalVat = (parseFloat(response.data.totalVat) * (-1)).toFixed(3).slice(0, -1)
                        root.total = parseFloat(parseFloat(root.dayEnd.cashInHand)).toFixed(3).slice(0, -1)
                        root.transactions = response.data.noOfTransaction
                        root.startTime = response.data.startTime;
                        root.date = response.data.date;
                        root.bankDetailListTotal = response.data.bankDetailListTotal;
                        root.bankDetailList = response.data.bankDetailList;
                        root.bankDetails = response.data.bankDetails;
                        root.dayEnd.postExpense = parseFloat(response.data.postExpense).toFixed(3).slice(0, -1);
                        root.grandTotal = parseFloat(root.total).toFixed(3).slice(0, -1);
                        //root.total = 1;
                        root.counterRander++
                    }
                });
            },
            close: function () {
                this.$emit('close');
            },

        },
        mounted: function () {
            this.isBankDetailShow = localStorage.getItem('BankDetail') == 'true' ? true : false;
            this.UserName = localStorage.getItem('LoginUserName');
            if (this.roport == true) {
                this.getOpeningBalanceForReport()
                this.showFilter = false;
            }
            else {
                this.getOpeningBalance();
            }



        }
    }
</script>

<style scoped>
    .labelHeading {
        font-style: normal;
        font-style: normal;
        font-weight: 600;
        line-height: 37px;
        letter-spacing: 0.01em;
        color: black;
    }

    .DayHeading1 {
        font-style: normal;
        font-size: 17px;
        font-style: normal;
        font-weight: 700;
        line-height: 37px;
        letter-spacing: 0.01em;
        color: #3178F6;
    }

    .vue__time-picker input.display-time {
        height: 3.2em !important;
    }
</style>
