<template>
    <div class="row" v-if="isValid('CanViewDayWiseReport')">
        <div class="col-lg-12 ">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('DayStartWiseReport.DayWiseReport') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{
                                        $t('DayStartWiseReport.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('DayStartWiseReport.DayWiseReport') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">

                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row align-items-center">

                <div class=" col-lg-4   form-group">
                    <label>{{ $t('DayStartWiseReport.FromDate') }}</label>
                    <day-dropdown v-model="fromDay" />
                </div>

                <div class=" col-lg-4   form-group">
                    <label>{{ $t('DayStartWiseReport.ToDate') }}</label>
                    <day-dropdown v-model="toDay" />
                </div>

                <div class=" col-lg-3 form-group" v-if="allowBranches">
                    <label>{{ $t('Branches.SelectBranch') }}</label>
                    <branch-dropdown v-model="branchIds" :ismultiple="true" :islayout="false" />
                </div>

            </div>

            <div class="card">
                <div class="col-12 col-sm-12 col-md-12 mt-2" style="padding-left:0px !important" v-bind:key="randerList">


                    <div class=" table-responsive ">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th>
                                        {{ $t('DayStartWiseReport.FromTime') }}
                                    </th>
                                    <th>
                                        {{ $t('DayStartWiseReport.CounterN') }}
                                    </th>


                                    <th class="text-center">
                                        {{ $t('DayStartWiseReport.OpeningCash') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('DayStartWiseReport.CashInHand') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('DayStartWiseReport.Expense') }}
                                    </th>

                                    <th class="text-center">
                                        {{ $t('DayStartWiseReport.CarryCash') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('DayStartWiseReport.EndTime') }}
                                    </th>

                                    <th class="text-center">
                                        {{ $t('DayStartWiseReport.ReceivedAmount') }}
                                    </th>
                                    <th class="text-center" style="width:12%">
                                        {{ $t('DayStartWiseReport.Action') }}
                                    </th>


                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(po, index) in inActiveTerminals" v-bind:key="index">
                                    <td>
                                        {{ index + 1 }}
                                    </td>
                                    <td>
                                        {{ po.fromTime }}
                                    </td>
                                    <td>
                                        <p style="margin:0;padding:0;">{{ po.endTerminalFor }}</p>
                                        <p style="margin:0;padding:0;">{{ po.counterName }}</p>
                                        <p style="margin:0;padding:0;">{{ po.superVisorName }}</p>
                                    </td>
                                    <td class="text-center">
                                        {{ parseFloat(po.openingCash).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                            "$1,") }}
                                    </td>
                                    <td class="text-center">
                                        {{ parseFloat((po.cashInHand + po.openingCash) - po.draftExpense).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                            "$1,") }}
                                    </td>
                                    <td class="text-center">
                                        {{ parseFloat(po.expenseCash).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                            "$1,") }}
                                    </td>
                                    <td class="text-center">
                                        {{ parseFloat(po.carryCash).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g,
                                            "$1,") }}
                                    </td>
                                    <td class="text-center">
                                        <p style="margin:0;padding:0;">{{ po.toTime }}</p>
                                        <p style="margin:0;padding:0;">{{ po.endTerminalBy }}</p>
                                    </td>

                                    <td class="text-center">
                                        {{ po.receivingAmount }}
                                        <p v-if="!po.isReceived">(Not Received)</p>
                                    </td>
                                    <td class="text-center">
                                        <a href="javascript:void(0)" title="View" class="btn  btn-icon btn-danger btn-sm"
                                            v-on:click="DayEnd(po.id, po.counterId)"><i class="fas fa-eye"></i></a>
                                        <a v-if="!po.isReceived" title="CashReceiving" href="javascript:void(0)"
                                            class="btn  btn-icon btn-danger btn-sm"
                                            v-on:click="ReceivingCash(po.carryCash, po.id)"><i
                                                class="fas fa-money-bill-wave-alt"></i></a>
                                        <a href="javascript:void(0)" title="Print" class="btn  btn-icon btn-danger btn-sm "
                                            v-on:click="getOpeningBalanceForDayEnd(po.id, po.counterId)"><i
                                                class=" fa fa-print"></i></a>

                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </div>

                </div>
                <cash-receiving-model :paidAmountProp="paidAmount" :inActiveDayId="inActiveDayId" v-if="receivingCash"
                    :show="receivingCash" @close="receivingCash = false" :type="type" />

                <cashcounterdetail @close="show1 = false" :counterCode="counterCode" :dayStartId="dayStartId"
                    :counterId="counterId" :show="show1" :roport=true v-if="show1" :type="type" />
            </div>

            <dayEndReportPrint :headerFooter="headerFooter" :printDetail="printDetail" v-if="printReport"
                v-bind:key="printRender"></dayEndReportPrint>
            <dayEndA4ReportPrint :headerFooter="headerFooter" :printDetail="printDetail" v-if="printA4Report"
                v-bind:key="printRender"></dayEndA4ReportPrint>
        </div>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
import moment from "moment";

export default {
        mixins: [clickMixin],
        setup() {
            return { v$: useVuelidate() }
        },
    data: function () {
        return {
            allowBranches: false,
            branchIds: [],
                branchId: '',
            fromDay: '',
            toDay: '',
            isBankDetailShow: false,
            dayStartId: '00000000-0000-0000-0000-000000000000',
            counterId: '00000000-0000-0000-0000-000000000000',
            bankDetailList: [],
            bankDetailListTotal: 0,
            dayWiseList: [],
            bank: '',
            inActiveTerminals: [],

            randerList: 0,
            show1: false,
            render: 0,
            designRander: 0,
            time: 0,
            dateRander: 0,
            UserName: '',
            isOpenDay: '',
            cashInHand: 0,
            openingCash: 0,

            //Day End
            printReport: false,
            printA4Report: false,
            printRender: 0,
            date: '',
            printDetail: [],
            startTime: '',
            endTime: '',
            total: 0,
            receivingCash: false,

            bankDetails: [],
            // Day End Variable End
            printDetailForRePrint: [],

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
                bankExpense: 0,
                bankAmount: 0,
                endTerminalBy: '',
                supervisorName: '',
                supervisorPassword: '',
                nextDayOpeningCash: 0,
                isSupervisor: false,
                postExpense: 0,
                draftExpense: 0,
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

                },
                nextDayOpeningCash: {

                },
                user: {
                    required
                },
                password: {
                    required
                },
                creditReason: {

                },

            },
            }



    },
    watch: {

        fromDay: function () {

            if (this.toDay != "") {
                this.getData()
            }
        },
        toDay: function () {

            if (this.fromDay != "") {
                this.getData()
            }
        },
        branchIds: function () {
                var root = this;
                this.branchId = '';
                this.branchIds.forEach(function (result) {
                    root.branchId = root.branchId == '' ? result.id : root.branchId + ',' + result.id;
                })
                this.getData();
            }

    },
    methods: {
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        DayEnd: function (id, counterId) {
            this.dayStartId = id
            this.counterId = counterId
            this.show1 = true

        },
        ReceivingCash: function (amount, id) {
            this.paidAmount = amount;
            this.inActiveDayId = id;
            this.receivingCash = !this.receivingCash
        },
        Cancel: function () {

            this.dayDesign = false;
            this.designRander++;

        },



        //Day End Section
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

                    }
                });
        },
        PrintDayEnd: function () {


            this.printDetail = this.dayEnd;
            this.printDetail.bankDetailListTotal = this.bankDetailListTotal;
            this.printDetail.bankDetailList = this.bankDetailList;
            this.printDetail.grandTotal = this.grandTotal;
            this.printDetail.total = this.total;
            this.printDetail.startTime = this.startTime;
            this.printDetail.toTime = moment(this.endTime).format('HH:mm');
            this.printDetail.startDate = this.startTime;
            this.printDetail.date = this.endTime;
            this.printDetail.totalCash = this.TotalCash();
            this.printDetail.verifyCash = this.dayEnd.totalCashForVerify;
            this.printDetail.getTotalSale = this.getTotalSale();
            this.printDetail.bankDetails = this.bankDetails;
            this.printDetail.transactions = this.transactions;
            this.printDetail.outStandingBalance = this.outStandingBalance;
            this.printDetail.carryCash = this.dayEnd.carryCash;
            this.printDetail.dayWiseList = this.dayEnd.dayWiseList;
            this.printDetail.user = this.dayEnd.user;
            //this.printDetail.verifyCash = this.dayEnd.totalCash;

            this.printRender++;
            var InvoiceSetting = localStorage.getItem('PrintSizeA4');
            if (InvoiceSetting == 'A4') {
                this.printA4Report = true;
            }
            else {
                this.printReport = true;

            }

        },
        getTimeOnly: function (x) {
            return moment(x).format('llll');
        },
        TotalCash: function () {

            return parseFloat(parseFloat(this.dayEnd.cashInHand) - parseFloat(this.dayEnd.openingCash) + parseFloat(this.dayEnd.expenseCash)).toFixed(3).slice(0, -1);
        },
        getTotalSale: function () {
            return parseFloat(parseFloat(this.TotalCash()) + parseFloat(this.dayEnd.bankAmount)).toFixed(3).slice(0, -1);
        },
        calculateGrandTotal: function () {


            //this.grandTotal = parseFloat(parseFloat(this.dayEnd.verifyCash) - parseFloat(this.dayEnd.carryCash)).toFixed(3).slice(0, -1);

        },
        getOpeningBalanceForDayEnd: function (id, counterId) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            //this.dayStart.counterId = id;
            this.$https.get('/Product/OpeningBalance?counterId=' + counterId + '&dayId=' + id + '&isReport=' + true + '&isOpeningCash=' + false, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {

                    root.dayEnd.openingCash = parseFloat(response.data.openingBalance) < 0 ? parseFloat(parseFloat(response.data.openingBalance) * (-1)).toFixed(3).slice(0, -1) : parseFloat(response.data.openingBalance).toFixed(3).slice(0, -1)
                    root.UserName = response.data.startFor;
                    //var cashInHand = parseFloat(parseFloat(response.data.cashInHand)).toFixed(3).slice(0, -1)
                    root.dayEnd.cashInHand = parseFloat(response.data.cashInHand).toFixed(3).slice(0, -1)
                    root.dayEnd.bankAmount = parseFloat(response.data.bank).toFixed(3).slice(0, -1)
                    root.dayEnd.expenseCash = parseFloat(response.data.postExpense).toFixed(3).slice(0, -1)
                    root.dayEnd.counterCode = response.data.terminalCode;
                    var totalVat = response.data.totalVat;
                    root.dayEnd.totalVat = totalVat < 0 ? totalVat.toFixed(3).slice(0, -1) * (-1) : parseFloat(totalVat).toFixed(3).slice(0, -1)
                    root.total = parseFloat(parseFloat(root.dayEnd.cashInHand)).toFixed(3).slice(0, -1)
                    root.transactions = response.data.noOfTransaction
                    root.startTime = response.data.startTime;
                    root.endTime = response.data.endTime;
                    root.date = response.data.date;

                    root.bankDetailListTotal = response.data.bankDetailListTotal;
                    root.bankDetailList = response.data.bankDetailList;
                    root.bankDetails = response.data.bankDetails;
                    root.dayEnd.bankExpense = parseFloat(response.data.bankExpense).toFixed(3).slice(0, -1)
                    root.dayEnd.carryCash = parseFloat(response.data.carryCash).toFixed(3).slice(0, -1)
                    root.dayEnd.dayWiseList = response.data.dayWiseList
                    /*  root.total = total < 0 ? total * (-1) : parseFloat(total).toFixed(3).slice(0, -1) ;*/
                    root.grandTotal = parseFloat(root.total).toFixed(3).slice(0, -1);
                    root.dayEnd.postExpense = parseFloat(response.data.postExpense).toFixed(3).slice(0, -1);
                    //root.dayEnd.draftExpense = parseFloat(response.data.draftExpense).toFixed(3).slice(0, -1);
                    //root.dayEnd.verifyCash = parseFloat(response.data.verifyCash).toFixed(3).slice(0, -1);
                    root.dayEnd.totalCashForVerify = parseFloat(response.data.totalCash);
                    root.dayEnd.user = response.data.startFor;
                    //root.total = 1;
                    root.PrintDayEnd();
                }
            });
        },

        getData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            //this.dayStart.counterId = id;
            this.$https.get('/Product/InActiveTerminalReportList?fromDay=' + root.fromDay + '&toDay=' + root.toDay, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    root.inActiveTerminals = response.data;
                    root.randerList++
                }
            });
        }
    },
    created: function () {
        this.$emit('update:modelValue', this.$route.name);
        this.isBankDetailShow = localStorage.getItem('BankDetail') == 'true' ? true : false;

        this.getData();
        this.allowBranches = localStorage.getItem('AllowBranches') == 'true' ? true : false;
    },
    mounted: function () {
        this.GetHeaderDetail();
    }
}
</script>
