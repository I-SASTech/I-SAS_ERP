<template>
    <div class="row pl-2 pe-2 ">
        <div class="col-lg-12  ">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('TheDayStart.StartYourDay') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('TheDayStart.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('TheDayStart.DayStart') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="!isSupervisor" :disabled="loginDisabled" v-on:click="show4 = !show4" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('TheDayStart.SupervisorLogin') }}
                                </a>
                                <a v-if="!isSupervisor" :disabled="!loginDisabled" v-on:click="SupervisorLogout()" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary ">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    Sign Out
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger mx-2">
                                    {{ $t('Bank.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="row " v-if="dayDesign==false" :key="designRander">
                <div class="col-md-6 col-lg-3" v-if="nobleRole=='Sales Man' || (nobleRole=='Admin' && superAdminDayStart)">
                    <div class="card report-card " data-bs-toggle="offcanvas" data-bs-target="#ForDayStart" aria-controls="offcanvasRight" :disabled="isDayAlreadyStart" v-bind:class="isDayAlreadyStart ? 'opacity50' : 'cardHover'" v-if="isValid('StartDay') && iSupervisorLogin " v-on:click="DayStartCardClick">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <h5 class="mt-3">{{ $t('TheDayStart.DayStart') }}</h5>
                                    <!--<p class="m-0"> {{ $t('TheDayStart.DayStart') }}</p>-->
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <i data-feather="tag" class="align-self-center text-muted icon-sm"></i>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                    <div class="card report-card " :disabled="isDayAlreadyStart " data-bs-toggle="offcanvas" data-bs-target="#ForDayStart" aria-controls="offcanvasRight" v-bind:class="isDayAlreadyStart ? 'opacity50' : 'cardHover'" v-else-if="isValid('StartDay') && IsPermissionToStartDay " v-on:click="DayStartCardClick">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <!--<p class="text-dark mb-0 fw-semibold">{{ $t('TheDayStart.DayStart') }}</p>-->
                                    <h5 class="mt-3">{{ $t('TheDayStart.DayStart') }}</h5>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <!--<img src="person.png"  class="align-self-center text-muted icon-sm" />-->
                                        <i data-feather="tag" class="align-self-center text-muted icon-sm"></i>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                    <div class="card report-card " disabled v-bind:class="(isDayAlreadyStart || !IsPermissionToStartDay) ? 'opacity50' : 'cardHover'" v-else-if="isValid('StartDay') && !IsPermissionToStartDay " v-on:click="DayStartCardClick">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <!--<p class="text-dark mb-0 fw-semibold">{{ $t('TheDayStart.DayStart') }}</p>-->
                                    <h5 class="mt-3">{{ $t('TheDayStart.DayStart') }}</h5>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <!--<img src="person.png"  class="align-self-center text-muted icon-sm" />-->
                                        <i data-feather="tag" class="align-self-center text-muted icon-sm"></i>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3" v-if="nobleRole=='Sales Man' || (nobleRole=='Admin' && superAdminDayStart)">
                    <div class="card report-card" data-bs-toggle="offcanvas" data-bs-target="#ForTransfer" aria-controls="offcanvasRight" :disabled="!isDayAlreadyStart" v-bind:class="!isDayAlreadyStart ? 'opacity50' : 'cardHover'" v-on:click="TransferCounterCard()" v-if="IsTransferAllow">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <h5 class="mt-3">{{ $t('TheDayStart.TransferCashCounter1') }}</h5>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <i data-feather="lock" class="align-self-center text-muted icon-sm"></i>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3" v-if="nobleRole=='Sales Man' || (nobleRole=='Admin' && superAdminDayStart)">
                    <div class="card report-card" data-bs-toggle="offcanvas" data-bs-target="#ForEnd" aria-controls="offcanvasRight" :disabled="!isDayAlreadyStart" v-bind:class="!isDayAlreadyStart ? 'opacity50' : 'cardHover'" v-if="isValid('CloseDay') && iSupervisorLogin" v-on:click="EndOperationCard(UserID)">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <h5 class="mt-3">{{ $t('TheDayStart.DayEnd') }}</h5>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <i data-feather="calendar" class="align-self-center text-muted icon-sm"></i>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                    <div class="card report-card" data-bs-toggle="offcanvas" data-bs-target="#ForEnd" aria-controls="offcanvasRight" :disabled="!isDayAlreadyStart" v-bind:class="!isDayAlreadyStart ? 'opacity50' : 'cardHover'" v-else-if="isValid('CloseDay') && IsPermissionToCloseDay" v-on:click="EndOperationCard(UserID)">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <h5 class="mt-3">{{ $t('TheDayStart.DayEnd') }}</h5>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <i data-feather="calendar" class="align-self-center text-muted icon-sm"></i>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                    <div class="card report-card" data-bs-toggle="offcanvas" data-bs-target="#ForEnd" aria-controls="offcanvasRight" :disabled="!isDayAlreadyStart" v-bind:class="(!isDayAlreadyStart || !IsPermissionToCloseDay) ? 'opacity50' : 'cardHover'" v-else-if="isValid('CloseDay') && !IsPermissionToCloseDay" v-on:click="EndOperationCard(UserID)">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <h5 class="mt-3">{{ $t('TheDayStart.DayEnd') }}</h5>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <i data-feather="calendar" class="align-self-center text-muted icon-sm"></i>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-lg-3" v-if="nobleRole=='Sales Man' || (nobleRole=='Admin' && superAdminDayStart)">
                    <div class="card report-card" v-on:click="RePrint()" v-if="!isDayAlreadyStart && printDetailForRePrint!=null && printDetailForRePrint!=undefined">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <h5 class="mt-3">{{ $t('TheDayStart.Re-Print') }}</h5>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <i data-feather="activity" class="align-self-center text-muted icon-sm"></i>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-12 col-sm-12 col-md-12 mt-2">
                        <h4 class="page-title">{{$t('TheDayStart.CashCounterDetails')}}</h4>
                        <div class=" table-responsive ">
                            <table class="table add_table_list_bg">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            {{$t('TheDayStart.Date')}}
                                        </th>

                                        <th>
                                            {{$t('TheDayStart.CashCounter#')}}
                                        </th>

                                        <th>
                                            {{$t('TheDayStart.User1')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.OpeningCash')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.TotalSale')}}
                                        </th>


                                        <th class="text-center">
                                            {{$t('TheDayStart.Expense')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.CashInHand')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.Bank')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.Action')}}
                                        </th>




                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(po,index) in dayStarts" v-bind:key="po.id">
                                        <td>
                                            {{index+1}}
                                        </td>
                                        <td>
                                            {{po.date}}
                                        </td>

                                        <td>
                                            {{po.counterName}}
                                        </td>

                                        <td>
                                            {{po.startTerminalFor}}
                                        </td>
                                        <td class="text-center">
                                            {{parseFloat(po.openingCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td class="text-center">
                                            {{parseFloat(parseFloat(po.cashInHand) + parseFloat(po.bankAmount)+parseFloat(po.expenseCash)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>


                                        <td class="text-center">
                                            {{parseFloat(po.expenseCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td class="text-center">
                                            {{parseFloat((parseFloat(po.cashInHand)+parseFloat(po.openingCash))-parseFloat(po.draftExpense)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td class="text-center">
                                            {{parseFloat(po.bankAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td class="text-center">
                                            <button class="btn  btn-outline-primary btn-sm me-1" data-bs-toggle="offcanvas" data-bs-target="#ForEnd" aria-controls="offcanvasRight" v-on:click="ViewCard(po)"><i class="fas fa-eye"></i></button>
                                        </td>



                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <hr />

                    </div>
                    <div class="col-12 col-sm-12 col-md-12 mt-2" v-if="isSupervisor || iSupervisorLogin" v-bind:key="randerList">
                        <h4 class="page-title">{{$t('TheDayStart.InActiveTerminals')}}</h4>

                        <div class=" table-responsive ">
                            <table class="table mb-0">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            {{$t('TheDayStart.Date')}}
                                        </th>
                                        <th>
                                            {{$t('TheDayStart.FromTime')}}
                                        </th>
                                        <th>
                                            {{$t('TheDayStart.CounterN')}}
                                        </th>
                                        <th>
                                            {{$t('TheDayStart.User1')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.Supervisor')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.CarryCash')}}
                                        </th>


                                        <th class="text-center">
                                            {{$t('TheDayStart.OpeningCash')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.CashInHand')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.Expense')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.EndTime')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.EndUser')}}
                                        </th>

                                        <th class="text-center">
                                            {{$t('TheDayStart.ReceivingAmount')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.Action')}}
                                        </th>


                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(po,index) in inactivedayStarts" v-bind:key="po.id">
                                        <td>
                                            {{index+1}}
                                        </td>
                                        <td>
                                            {{po.date}}
                                        </td>
                                        <td>
                                            {{po.fromTime}}
                                        </td>
                                        <td>
                                            {{po.counterName}}
                                        </td>
                                        <td>
                                            {{po.endTerminalFor}}
                                        </td>

                                        <td class="text-center">
                                            {{po.superVisorName}}
                                        </td>
                                        <td class="text-center">
                                            {{parseFloat(po.carryCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td class="text-center">
                                            {{parseFloat(po.openingCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td class="text-center">
                                            {{parseFloat((po.cashInHand+po.openingCash)-po.draftExpense).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td class="text-center">
                                            {{parseFloat(po.expenseCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td class="text-center">
                                            {{po.toTime}}
                                        </td>
                                        <td class="text-center">
                                            {{po.endTerminalBy}}
                                        </td>

                                        <td class="text-center">
                                            {{po.receivingAmount}}
                                        </td>
                                        <td class="tableActionWidth">
                                            <a v-if="!po.isReceived" href="javascript:void(0)" title="Cash Receiving" class="btn   btn-primary btn-sm mx-1" v-on:click="ReceivingCash(po.carryCash,po.id)"> {{$t('TheDayStart.Receiving')}}</a>

                                            <button class="btn btn-icon  btn-sm btn-primary " data-bs-toggle="offcanvas" data-bs-target="#ForEnd" aria-controls="offcanvasRight" v-on:click="ViewCard(po)"><i class="fas fa-eye"></i></button>

                                        </td>

                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <hr />

                    </div>

                    <div class="col-12 col-sm-12 col-md-12 mt-2" v-if="isSupervisor || iSupervisorLogin">
                        <div v-for="(day) in dayWiseList" :key="day.id">
                            <h4 v-bind:key="randerList" class="page-title">  {{$t('TheDayStart.InActiveTerminals')}} ({{day[0].date}} {{day[0].fromTime}} - {{day[0].date}} {{day[0].toTime}} )</h4>


                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <th>#</th>
                                            <th>
                                                {{$t('TheDayStart.Date')}}
                                            </th>
                                            <th>
                                                {{$t('TheDayStart.FromTime')}}
                                            </th>
                                            <th>
                                                {{$t('TheDayStart.CounterN')}}
                                            </th>
                                            <th>
                                                {{$t('TheDayStart.User1')}}
                                            </th>
                                            <th class="text-center">
                                                {{$t('TheDayStart.Supervisor')}}
                                            </th>
                                            <th class="text-center">
                                                {{$t('TheDayStart.CarryCash')}}
                                            </th>


                                            <th class="text-center">
                                                {{$t('TheDayStart.OpeningCash')}}
                                            </th>
                                            <th class="text-center">
                                                {{$t('TheDayStart.CashInHand')}}
                                            </th>
                                            <th class="text-center">
                                                {{$t('TheDayStart.Expense')}}
                                            </th>
                                            <th class="text-center">
                                                {{$t('TheDayStart.EndTime')}}
                                            </th>
                                            <th class="text-center">
                                                {{$t('TheDayStart.EndUser')}}
                                            </th>

                                            <th class="text-center">
                                                {{$t('TheDayStart.ReceivingAmount')}}
                                            </th>
                                            <th class="text-center">
                                                {{$t('TheDayStart.Action')}}
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(po,index) in day" v-bind:key="po.id">
                                            <td>
                                                {{index+1}}
                                            </td>
                                            <td>
                                                {{po.date}}
                                            </td>
                                            <td>
                                                {{po.fromTime}}
                                            </td>
                                            <td>
                                                {{po.counterName}}
                                            </td>
                                            <td>
                                                {{po.endTerminalBy}}
                                            </td>

                                            <td class="text-center">
                                                {{po.superVisorName}}
                                            </td>
                                            <td class="text-center">
                                                {{parseFloat(po.carryCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{parseFloat(po.openingCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{parseFloat((po.cashInHand+po.openingCash)-po.draftExpense).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{parseFloat(po.expenseCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                            </td>
                                            <td class="text-center">
                                                {{po.toTime}}
                                            </td>
                                            <td class="text-center">
                                                {{po.endTerminalFor}}
                                            </td>

                                            <td class="text-center">
                                                {{po.receivingAmount}}
                                            </td>

                                            <td class="tableActionWidth">
                                                <a v-if="!po.isReceived" href="javascript:void(0)" title="Cash Receiving" class="btn   btn-primary btn-sm" v-on:click="ReceivingCash(po.carryCash,po.id)"> {{$t('TheDayStart.Receiving')}}</a>

                                                <button class="btn btn-icon  btn-sm btn-primary " data-bs-toggle="offcanvas" data-bs-target="#ForEnd" aria-controls="offcanvasRight" v-on:click="ViewCard(po)"><i class="fas fa-eye"></i></button>

                                            </td>

                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <hr />

                        </div>
                    </div>


                    <div class="col-12 col-sm-12 col-md-12 mt-2" v-if="isSupervisor || iSupervisorLogin" v-bind:key="randerList">

                        <h4 class="page-title">{{$t('TheDayStart.TransferHistory')}}</h4>


                        <div class="table-responsive">
                            <table class="table mb-0">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            {{$t('TheDayStart.Date')}}
                                        </th>
                                        <th>
                                            {{$t('TheDayStart.TransferTime')}}
                                        </th>
                                        <th>
                                            {{$t('TheDayStart.CounterN')}}
                                        </th>
                                        <th>
                                            {{$t('TheDayStart.TransferBy')}}
                                        </th>
                                        <th>
                                            {{$t('TheDayStart.TransferTo')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.OpeningCash')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.CashInHand')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.Expense')}}
                                        </th>
                                        <th class="text-center">
                                            {{$t('TheDayStart.Bank')}}
                                        </th>


                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(po,index) in transferHistories" v-bind:key="po.id">
                                        <td>
                                            {{index+1}}
                                        </td>
                                        <td>
                                            {{po.date}}
                                        </td>
                                        <td>
                                            {{po.fromTime}}
                                        </td>
                                        <td>
                                            {{po.counterName}}
                                        </td>
                                        <td>
                                            {{po.startTerminalBy}}
                                        </td>
                                        <td>
                                            {{po.startTerminalFor}}
                                        </td>
                                        <td class="text-center">
                                            {{parseFloat(po.openingCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td class="text-center">
                                            {{parseFloat((po.cashInHand)-po.expenseCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td class="text-center">
                                            {{parseFloat(po.expenseCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                        <td class="text-center">
                                            {{parseFloat(po.bankAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <hr />

                    </div>
                    <div class="col-12 col-sm-12 col-md-12 mt-2" v-if="isSupervisor || iSupervisorLogin " v-bind:key="randerList">


                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6"></div>
                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                <h4 class="page-title">{{$t('TheDayStart.TotalOfDay')}}</h4>

                                <div class="mt-1">
                                    <table class="table" style="background-color: #f1f5fa;">
                                        <tbody>
                                            <tr>
                                                <td colspan="2" style="width:65%;">
                                                    <span class="fw-bold"> {{$t('TheDayStart.OpeningCash')}}</span>
                                                </td>
                                                <td class="text-end" style="width:35%;"> {{parseFloat(openingCash).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="width:65%;">
                                                    <span class="fw-bold">  {{$t('TheDayStart.CashInHand')}}</span>
                                                </td>
                                                <td class="text-end" style="width:35%;">     {{parseFloat(parseFloat(cashInHand)+parseFloat(openingCash)-parseFloat(draftExpense)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="width:65%;">
                                                    <span class="fw-bold">    {{$t('TheDayStart.Expense')}}</span>
                                                </td>
                                                <td class="text-end" style="width:35%;">       {{parseFloat(expense).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="width:65%;">
                                                    <span class="fw-bold"> {{$t('TheDayStart.Bank')}}</span>
                                                </td>
                                                <td class="text-end" style="width:35%;">
                                                    {{parseFloat(bank).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                </td>
                                            </tr>

                                        </tbody>
                                    </table>
                                </div>


                            </div>

                        </div>



                    </div>

                </div>



            </div>
            <div>
                <div class="offcanvas offcanvas-end" tabindex="-1" id="ForDayStart" aria-labelledby="offcanvasRightLabel" style="width:600px !important">
                    <div class="offcanvas-header">
                        <h5 id="offcanvasTopLabel" class="m-0">{{ $t('TheDayStart.DayStart') }}</h5>
                       <button v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>



                    </div>
                    <div class="offcanvas-body">


                        <div class="row ">

                            <div class="form-group has-label col-sm-6 ">
                                <label class="tooltip-container text-dashed-underline "> {{$t('TheDayStart.CashCounterUser')}}:<span class="text-danger"> *</span></label>
                                <input class="form-control" v-model="UserName" type="text" disabled />
                            </div>
                            <div class="form-group has-label col-sm-6 ">
                                <label class="tooltip-container text-dashed-underline "> {{$t('TheDayStart.Date&Time')}}<span class="text-danger"> *</span></label>
                                <input class="form-control" v-model="dayStart.date" type="text" disabled />
                            </div>
                            <div class="form-group has-label col-sm-6 ">
                                <label class="tooltip-container text-dashed-underline "> {{ $t('TheDayStart.Counter') }} #:<span class="text-danger"> *</span></label>
                                <terminal-dropdown v-if="isOpenDay" v-model="v$.dayStart.counterId.$model" :terminalType="terminalType" :terminalUserType="terminalUserType" :key="render" @update:modelValue="getOpeningBalance" :modelValue="dayStart.counterId" :isDayStart=true />
                                <terminal-dropdown v-else-if="nobleRole=='Admin'" :isDisable="false" v-model="dayStart.counterId" :terminalType="terminalType" :terminalUserType="terminalUserType" :key="render" @update:modelValue="getOpeningBalance" :modelValue="dayStart.counterId" :isDayStart=true />
                                <terminal-dropdown v-else :isDisable="true" v-model="dayStart.counterId" :terminalType="terminalType" :terminalUserType="terminalUserType" :key="render" @update:modelValue="getOpeningBalance" :modelValue="dayStart.counterId" :isDayStart=true />

                            </div>
                            <div class="form-group has-label col-sm-6 ">
                            </div>

                            <div class="form-group has-label col-sm-6 ">
                                <label class="tooltip-container text-dashed-underline "> {{ $t('TheDayStart.OpeningCash') }}:<span class="text-danger"> *</span> </label>
                                <decimal-to-fixed v-bind:salePriceCheck="false" v-model="openingCash" :disabled="true" />
                            </div>
                            <div class="form-group has-label col-sm-6 " v-bind:class="{ 'has-danger': v$.dayStart.verifyCash.$error}">
                                <label class="tooltip-container text-dashed-underline ">  {{ $t('TheDayStart.VerifyCash') }}:<span class="text-danger"> *</span> </label>
                                <input class="form-control" @focus="CheckReasonFalse" @blur="CheckReason(dayStart.verifyCash,true)" v-model="v$.dayStart.verifyCash.$model" type="number" @click="$event.target.select()" />
                                <span v-if="v$.dayStart.verifyCash.$error" class="error text-danger">
                                    <span v-if="!v$.dayStart.verifyCash.required">{{ $t('TheDayStart.VerifyCashRequired') }}</span>
                                </span>
                            </div>
                            <div class="form-group has-label col-sm-6 " v-if="!iSupervisorLogin">
                                <label class="tooltip-container text-dashed-underline "> {{ $t('TheDayStart.User') }}:<span class="text-danger"> *</span> </label>
                                <input class="form-control" v-model="v$.dayStart.user.$model" type="text" />
                            </div>
                            <div class="form-group has-label col-sm-6 " v-if="!iSupervisorLogin">
                                <label class="tooltip-container text-dashed-underline "> {{ $t('TheDayStart.Password') }}:<span class="text-danger"> *</span> </label>
                                <input class="form-control" v-model="v$.dayStart.password.$model" type="password" />
                            </div>
                            <div class="form-group has-label col-sm-12 " v-if="reason">
                                <label class="tooltip-container text-dashed-underline "> {{ $t('TheDayStart.Reason') }}:<span class="text-danger"> *</span> </label>
                                <textarea class="form-control" v-model="v$.dayStart.reason.$model" type="text" @click="$event.target.select()" />
                            </div>

                        </div>
                        <div class="text-end">
                            <button type="button" class="btn btn-primary mx-2" :key="onesRander" v-on:click.once="SaveDayStart" :disabled="v$.dayStart.$invalid">Start</button>
                            <button type="button" class="btn btn-danger" style="margin-right:8px" data-bs-dismiss="offcanvas" v-on:click="Cancel">{{ $t('TheDayStart.Cancel') }}</button>

                        </div>
                    </div>
                </div>

                <div class="offcanvas offcanvas-end" tabindex="-1" id="ForTransfer" aria-labelledby="offcanvasRightLabel" style="width:600px !important">
                    <div class="offcanvas-header">
                        <h5 id="offcanvasTopLabel" class="m-0">{{$t('TheDayStart.TransferCashCounter1')}}</h5>
                       <button v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                    </div>
                    <div class="offcanvas-body">
                        <div class="col-lg-12">

                            <div>

                                <div class="row ">
                                    <div class=" col-12">


                                        <h5 class="page_title">{{$t('TheDayStart.UserandCashCounterInfo')}}</h5>
                                        <table class="table border  table-bordered ">

                                            <tr style="font-size:12px;padding-bottom:4px; ">
                                                <td style="text-align:left;font-weight:bold;color:black !important;width:30%"> <span> {{ $t('TheDayStart.CashCounterUser') }}</span></td>
                                                <td style="text-align:left;color:black !important;width:20%"> <span>{{UserName}}</span></td>
                                                <td style="text-align:left;font-weight:bold;color:black !important;width:20%"> <span>{{ $t('TheDayStart.Date&Time') }}</span></td>
                                                <td style="text-align:left;color:black !important;width:35%"> <span>{{transferDayCounter.date}}</span></td>

                                            </tr>
                                            <tr style="font-size:12px;padding-bottom:4px; ">
                                                <td style="text-align:left;font-weight:bold;color:black !important;width:30%"> <span>{{ $t('TheDayStart.Counter') }}</span></td>
                                                <td style="text-align:left;color:black !important" colspan="3"> <span>{{transferDayCounter.counterCode}}</span></td>

                                            </tr>


                                        </table>


                                    </div>




                                    <div class=" col-12">
                                        <h5 class="page_title">{{$t('TheDayStart.SalesInfo')}}</h5>
                                        <table class="table border  table-bordered ">

                                            <tr style="font-size:12px;padding-bottom:4px; ">
                                                <td style="text-align:left;font-weight:bold;color:black !important;width:30%"> <span>{{ $t('TheDayStart.OpeningCash') }}</span></td>
                                                <td style="text-align:left;color:black !important;width:20%"> <span>{{transferDayCounter.openingCash}}</span></td>
                                                <td style="text-align:left;font-weight:bold;color:black !important;width:20%"> <span>{{$t('TheDayStart.TotalCashSale')}}</span></td>
                                                <td style="text-align:left;color:black !important;width:35%">
                                                    <span>{{parseFloat(parseFloat(transferDayCounter.cashInHand) - parseFloat(transferDayCounter.openingCash) + parseFloat(transferDayCounter.expenseCash)).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span>
                                                </td>
                                            </tr>
                                            <tr style="font-size:12px;padding-bottom:4px; ">
                                                <td style="text-align:left;font-weight:bold;color:black !important;width:30%"> <span>{{ $t('TheDayStart.Bank') }}  ( {{$t('TheDayStart.Transactions')}} : {{transactions}} )</span></td>
                                                <td style="text-align:left;color:black !important;width:20%"> <span> {{transferDayCounter.bankAmount}}</span></td>
                                                <td style="text-align:left;font-weight:bold;color:black !important;width:20%" v-if="!isBankDetailShow"> <span>{{ $t('TheDayStart.Expense') }}</span></td>
                                                <td style="text-align:left;font-weight:bold;color:black !important;width:20%" v-else> <span>{{ $t('TheDayStart.CashExpense') }}</span></td>
                                                <td style="text-align:left;color:black !important;width:35%"> <span> {{transferDayCounter.expenseCash}}</span></td>

                                            </tr>
                                            <tr style="font-size:12px;padding-bottom:4px; " v-if="isBankDetailShow">
                                                <td style="text-align:left;font-weight:bold;color:black !important" colspan="2"> <span>{{ $t('TheDayStart.BankExpense') }}</span></td>
                                                <td style="text-align:left;font-weight:bold;color:black !important" colspan="2">  <span>{{parseFloat(transferDayCounter.bankExpense).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>

                                            </tr>
                                            <tr style="font-size:12px;padding-bottom:4px; ">
                                                <td style="text-align:left;font-weight:bold;color:black !important" colspan="2"> <span>{{$t('TheDayStart.TotalInovices')}} ({{fromInvoice}} - {{toInvoice}})</span></td>
                                                <td style="text-align:left;color:black !important" colspan="2"> <span> {{totalInvoices}}</span></td>
                                            </tr>
                                            <tr style="font-size:12px;padding-bottom:4px; ">
                                                <td style="text-align:left;font-weight:bold;color:black !important" colspan="2"> <span> {{$t('TheDayStart.CashInHand')}}</span></td>
                                                <td style="text-align:left;color:black !important" colspan="2"> <span> {{total}}</span></td>
                                            </tr>


                                        </table>



                                    </div>




                                </div>
                                <div class="card-body">

                                    <div class="row ">
                                        <div class="col-12">
                                            <h5 class="page_title">{{$t('TheDayStart.ReceiverInfo')}}</h5>

                                        </div>


                                        <div class="form-group has-label col-sm-6 " v-bind:class="{ 'has-danger': v$.transferDayCounter.verifyCash.$error}">
                                            <label class="text  font-weight-bolder">  {{ $t('TheDayStart.VerifyCash') }}:<span class="text-danger"> *</span> </label>
                                            <decimal-to-fixed v-bind:salePriceCheck="false" v-model="v$.transferDayCounter.verifyCash.$model" />
                                            <span v-if="v$.transferDayCounter.verifyCash.$error" class="error text-danger">
                                                <span v-if="!v$.transferDayCounter.verifyCash.required">{{ $t('TheDayStart.VerifyCashRequired') }}</span>
                                            </span>
                                        </div>





                                        <div class="form-group has-label col-sm-6 ">


                                        </div> <div class="form-group has-label col-sm-6 ">

                                            <label class="text  font-weight-bolder">{{$t('TheDayStart.User1')}} :<span class="text-danger"> *</span> </label>
                                            <usersDropdown :isTransfer="true" v-model="v$.transferDayCounter.toUser.$model"></usersDropdown>
                                        </div>
                                        <div class="form-group has-label col-sm-6 ">
                                            <label class="text  font-weight-bolder"> {{$t('TheDayStart.Password')}} :<span class="text-danger"> *</span> </label>
                                            <input class="form-control" v-model="v$.transferDayCounter.toPassword.$model" type="password" />
                                        </div>

                                    </div>
                                </div>

                                <div class="modal-footer justify-content-right" v-if="type=='Edit'&& isValid('StartDay')">

                                    <button type="button" class="btn btn-primary  " :key="onesRander" v-on:click.once="SaveDayTransfer"><i class="far fa-save"></i> {{ $t('TheDayStart.DayClose') }}</button>
                                    <button type="button" class="btn btn-danger mr-3 " v-on:click="close()" data-bs-dismiss="offcanvas" aria-label="Close">{{ $t('TheDayStart.Cancel') }}</button>

                                </div>
                                <div class="modal-footer justify-content-right" v-else>

                                    <button type="button" class="btn btn-primary" :key="onesRander" v-on:click.once="SaveDayTransfer" :disabled="v$.transferDayCounter.$invalid "><i class="far fa-save"></i> {{ $t('TheDayStart.Start') }}</button>
                                    <button type="button" class="btn btn-danger mr-3 " v-on:click="close()" data-bs-dismiss="offcanvas" aria-label="Close"> {{ $t('TheDayStart.Cancel') }}</button>

                                </div>
                            </div>
                        </div>
                    </div>
                    <dayEndReportPrint :headerFooter="headerFooter" :printDetail="printDetail" :isTransfer="isTransferPrint" v-if="printReport" v-bind:key="printRender"></dayEndReportPrint>
                    <dayEndA4ReportPrint :headerFooter="headerFooter" :isTransfer="isTransferPrint" :printDetail="printDetail" v-if="printA4Report" v-bind:key="printRender"></dayEndA4ReportPrint>
                </div>
                <div class="offcanvas offcanvas-end col-12" tabindex="-1" data-bs-scroll="true" id="ForEnd" aria-labelledby="offcanvasRightLabel" style="width:600px !important">
                    <div class="offcanvas-header" v-if="isView">
                        <h5 id="offcanvasTopLabel" class="m-0">{{ $t('TheDayStart.CashCounterDetails') }}</h5>
                       <button v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                    </div>
                    <div v-if="isView" class="offcanvas-body" v-bind:key="counterRander">
                        <div class="col-lg-12">
                            <div>
                                <div>
                                    <div class="row">
                                        <div class="col-12 ">
                                            <h5 class=" page_title" id="myModalLabel" style=" padding-bottom:3px;"> {{ $t('TheDayStart.UserandCashCounterInfo') }}</h5>
                                            <table class="table border  table-bordered ">

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="text-align:left;font-weight:bold;color:black !important;width:30%"> <span>{{ $t('TheDayStart.CashCounterUser') }}</span></td>
                                                    <td style="text-align:left;color:black !important;width:20%"> <span>{{UserName}}</span></td>
                                                    <td style="text-align:left;font-weight:bold;color:black !important;width:20%"> <span>{{ $t('TheDayStart.Date&Time') }}</span></td>
                                                    <td style="text-align:left;color:black !important;width:35%"> <span>{{date}} {{startTime}}</span></td>

                                                </tr>
                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="text-align:left;font-weight:bold;color:black !important;width:30%"> <span>{{ $t('TheDayStart.Counter') }}</span></td>
                                                    <td style="text-align:left;color:black !important" colspan="3"> <span>{{dayEnd.counterCode}}</span></td>

                                                </tr>


                                            </table>

                                        </div>


                                    </div>
                                    <!-- Row 2 -->
                                    <div class="row">
                                        <div class="col-12 ">
                                            <h5 class=" page_title" id="" style="padding-top:3px; padding-bottom:3px;">{{ $t('TheDayStart.SalesInfo') }}</h5>
                                            <table class="table border  table-bordered ">

                                                <tr style="font-size:12px;padding-bottom:4px; ">
                                                    <td style="text-align:left;font-weight:bold;color:black !important"> <span>{{ $t('TheDayStart.OpeningCash') }}</span></td>
                                                    <td style="text-align:left;color:black !important"> <span>{{dayEnd.openingCash}}</span></td>

                                                    <td style="text-align:left;font-weight:bold;color:black !important"> <span>{{ $t('TheDayStart.CashSale') }}</span></td>
                                                    <td style="text-align:left;color:black !important"> <span>{{TotalCashForView()}}</span></td>
                                                </tr>
                                                <tr style="font-size:12px;padding-bottom:4px; " v-if="!isBankDetailShow">
                                                    <td style="text-align:left;font-weight:bold;color:black !important"> <span>{{ $t('TheDayStart.Expense') }}</span></td>
                                                    <td style="text-align:left;color:black !important"> <span> {{dayEnd.expenseCash}}</span></td>
                                                    <td style="text-align:left;font-weight:bold;color:black !important;"> <span>{{ $t('TheDayStart.CashInHand') }}</span><span style="color:black;"> ({{ $t('TheDayStart.OpeningCashCashSaleExpense') }}) </span></td>
                                                    <td style="text-align:left;color:black !important;"> <span> {{total}}</span></td>

                                                </tr>

                                                <tr style="font-size:12px;padding-bottom:4px; " v-if="isBankDetailShow">
                                                    <td style="text-align:left;font-weight:bold;color:black !important;"> <span>{{ $t('TheDayStart.CashExpense') }}</span></td>
                                                    <td style="text-align:left;color:black !important;"> <span> {{dayEnd.expenseCash}}</span></td>
                                                    <td style="text-align:left;font-weight:bold;color:black !important;"> <span>{{ $t('TheDayStart.BankExpense') }}</span></td>
                                                    <td style="text-align:left;color:black !important;"> <span> {{dayEnd.bankExpense}}</span></td>

                                                </tr>
                                                <tr style="font-size:12px;padding-bottom:4px; " v-if="isBankDetailShow">
                                                    <td style="text-align:left;font-weight:bold;color:black !important" colspan="3"> <span>{{ $t('TheDayStart.CashInHand') }}</span><span style="color:black;"> ({{ $t('TheDayStart.OpeningCashCashSaleExpense') }}) </span></td>
                                                    <td style="text-align:left;color:black !important"> <span> {{total}}</span></td>
                                                </tr>



                                            </table>

                                        </div>
                                    </div>

                                    <!-- Row 4 -->
                                    <div class="row mt-2">
                                        <div class="col-sm-12">
                                            <h5 class=" page_title" id="" style="padding-top:3px; padding-bottom:3px;padding-left:3px">{{ $t('TheDayStart.BankInfo') }}</h5>
                                            <div class="row">
                                                <div class="col-sm-12 mt-2">
                                                    <table class="table" v-if="!isBankDetailShow">
                                                        <tr style="font-size:12px;padding-bottom:4px;border-color:black !important" v-for="(bank) in bankDetails" v-bind:key="bank.id">
                                                            <td v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left;border-top:1px solid;color: black !important;font-weight:bold' : 'text-align:right;border-top:1px solid;color: black !important;font-weight:bold'"> <span>{{bank.bankName}}</span>:</td>
                                                            <td v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:right;border-top:1px solid;color: black !important;font-weight:bold' : 'text-align:left;border-top:1px solid;color: black !important;font-weight:bold'">{{parseFloat(bank.totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="color: black;font-size:13px;padding-top:0px !important; padding-bottom:0px !important; font-weight:bold" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left' : 'text-align:right'"> <span>{{ $t('TheDayStart.BankTotal') }}</span>:</td>
                                                            <td style="color: black;font-size:13px;padding-top:0px !important; padding-bottom:0px !important;font-weight:bold" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:right' : 'text-align:left'">{{parseFloat(dayEnd.bankAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                        </tr>
                                                    </table>
                                                    <table class="table" v-else>

                                                        <tr style="font-size:12px;padding-bottom:4px;border-color:black !important" v-for="(bank) in bankDetailList" v-bind:key="bank.id">
                                                            <td v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left;border-top:1px solid;color: black !important;font-weight:bold' : 'text-align:right;border-top:1px solid;color: black !important;font-weight:bold'"> <span>{{bank.bankName}}</span>:</td>
                                                            <td v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:right;border-top:1px solid;color: black !important;font-weight:bold' : 'text-align:left;border-top:1px solid;color: black !important;font-weight:bold'">{{parseFloat(bank.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="color: black;font-size:13px;padding-top:0px !important; padding-bottom:0px !important; font-weight:bold" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left' : 'text-align:right'"> <span>{{ $t('TheDayStart.BankTotal') }}</span>:</td>
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
                                            <div class="row border border-dark ms-1 me-1" style="border:1px solid !important">

                                                <div class="col-sm-12 mt-2 mb-2 text-center" style="font-size :14px; font-weight:bold;color:black;">
                                                    <span>{{ $t('TheDayStart.TotalSale') }} <span style="color:black;font-size:13px">({{ $t('TheDayStart.Sales+Bank=') }})  </span> </span><span>{{getTotalSaleForView()}}</span>
                                                </div>

                                            </div>

                                        </div>
                                    </div>


                                </div>

                                <div class="modal-footer justify-content-right" v-if="type=='Edit'">

                                    <button type="button" class="btn btn-danger mr-3 " v-on:click="close()" data-bs-dismiss="offcanvas" aria-label="Close">{{ $t('TheDayStart.Close') }}</button>

                                </div>
                                <div class="modal-footer justify-content-right" v-else>

                                    <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()" data-bs-dismiss="offcanvas" aria-label="Close">{{ $t('TheDayStart.Close') }}</button>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="offcanvas-header" v-if="!isView">
                        <h5 id="offcanvasTopLabel" class="m-0">{{ $t('TheDayStart.DayEnd') }}</h5>
                       <button v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'margin-left:0px !important'" type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                    </div>
                    <div v-if="!isView" class="offcanvas-body" v-bind:key="counterRander">
                        <div class="col-lg-12">
                            <div class="row">
                                <div class="col-12">

                                    <h5 class="page_title"> {{ $t('TheDayStart.UserandCashCounterInfo') }}</h5>
                                    <table class="table border  table-bordered">
                                        <tr style="font-size:12px;padding-bottom:4px; ">
                                            <td style="text-align:left;font-weight:bold;color:black !important;width:30%"> <span> {{ $t('TheDayStart.CashCounterUser') }}</span></td>
                                            <td style="text-align:left;color:black !important;width:20%"> <span>{{UserName}}</span></td>
                                            <td style="text-align:left;font-weight:bold;color:black !important;width:20%"> <span>{{ $t('TheDayStart.Date&Time') }}</span></td>
                                            <td style="text-align:left;color:black !important;width:35%"> <span>{{startTime}}</span></td>
                                        </tr>
                                        <tr style="font-size:12px;padding-bottom:4px; ">
                                            <td style="text-align:left;font-weight:bold;color:black !important;width:30%"> <span>{{ $t('TheDayStart.Counter') }}</span></td>
                                            <td style="text-align:left;color:black !important" colspan="3"> <span>{{dayEnd.counterCode}}</span></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-12">
                                    <h5 class=" page_title">{{ $t('TheDayStart.SalesInfo') }}</h5>
                                    <table class="table border  table-bordered ">

                                        <tr style="font-size:12px;padding-bottom:4px; ">
                                            <td style="text-align:left;font-weight:bold;color:black !important;width:30%"> <span>{{ $t('TheDayStart.OpeningCash') }}</span></td>
                                            <td style="text-align:left;color:black !important;width:20%"> <span>{{dayEnd.openingCash}}</span></td>

                                            <td style="text-align:left;font-weight:bold;color:black !important;width:20%"> <span>{{ $t('TheDayStart.CashSale') }}</span></td>
                                            <td style="text-align:left;color:black !important;width:35%"> <span>{{TotalCash()}}</span></td>
                                        </tr>
                                        <tr style="font-size:12px;padding-bottom:4px; ">
                                            <td style="text-align:left;font-weight:bold;color:black !important;width:30%" v-if="!isBankDetailShow"> <span>{{ $t('TheDayStart.Expense') }}</span></td>
                                            <td style="text-align:left;font-weight:bold;color:black !important;width:30%" v-else> <span>{{ $t('TheDayStart.CashExpense') }}</span></td>
                                            <td style="text-align:left;color:black !important;width:20%"> <span> {{dayEnd.expenseCash}}</span></td>
                                            <td style="text-align:left;font-weight:bold;color:black !important;width:20%"> <span>{{ $t('TheDayStart.TotalVAT') }}</span></td>
                                            <td style="text-align:left;color:black !important;width:35%"> <span> {{dayEnd.totalVat}}</span></td>

                                        </tr>
                                        <tr style="font-size:12px;padding-bottom:4px; " v-if="isBankDetailShow">
                                            <td style="text-align:left;font-weight:bold;color:black !important" colspan="2"> <span>{{ $t('TheDayStart.BankExpense') }}</span></td>
                                            <td style="text-align:left;font-weight:bold;color:black !important" colspan="2">  <span>{{parseFloat(dayEnd.bankExpense).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</span></td>

                                        </tr>
                                        <tr style="font-size:12px;padding-bottom:4px; ">
                                            <td style="text-align:left;font-weight:bold;color:black !important" colspan="3"> <span>{{ $t('TheDayStart.CashInHand') }}</span><span style="color:black;">  ( {{ $t('TheDayStart.OpeningCashCashSaleExpense') }} )  </span></td>
                                            <td style="text-align:left;color:black !important"> <span> {{total}}</span></td>



                                        </tr>


                                    </table>
                                </div>
                                <div>


                                    <!-- Row 4 -->
                                    <div class="row mt-2">
                                        <div class="col-12">
                                            <h5 class=" page_title" id="myModalLabel" style=" padding-bottom:3px;padding-left:3px">{{ $t('TheDayStart.BankInfo') }}</h5>
                                            <div class="row">
                                                <div class="col-sm-12 mt-2">
                                                    <table class="table" v-if="!isBankDetailShow">
                                                        <tr style="font-size:12px;padding-bottom:4px;border-color:black !important" v-for="(bank) in bankDetails" v-bind:key="bank.id">
                                                            <td v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left;border-top:1px solid;color: black !important;font-weight:bold' : 'text-align:right;border-top:1px solid;color: black !important;font-weight:bold'"> <span>{{bank.bankName}}</span>:</td>
                                                            <td v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:right;border-top:1px solid;color: black !important;font-weight:bold' : 'text-align:left;border-top:1px solid;color: black !important;font-weight:bold'">{{parseFloat(bank.totalAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="color: black;font-size:13px;padding-top:0px !important; padding-bottom:0px !important; font-weight:bold" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left' : 'text-align:right'"> <span>{{ $t('TheDayStart.BankTotal') }}</span>:</td>
                                                            <td style="color: black;font-size:13px;padding-top:0px !important; padding-bottom:0px !important;font-weight:bold" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:right' : 'text-align:left'">{{parseFloat(dayEnd.bankAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                        </tr>
                                                    </table>
                                                    <table class="table" v-else>

                                                        <tr style="font-size:12px;padding-bottom:4px;border-color:black !important" v-for="(bank) in bankDetailList" v-bind:key="bank.id">
                                                            <td v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left;border-top:1px solid;color: black !important;font-weight:bold' : 'text-align:right;border-top:1px solid;color: black !important;font-weight:bold'"> <span>{{bank.bankName}}</span>:</td>
                                                            <td v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:right;border-top:1px solid;color: black !important;font-weight:bold' : 'text-align:left;border-top:1px solid;color: black !important;font-weight:bold'">{{parseFloat(bank.amount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="color: black;font-size:13px;padding-top:0px !important; padding-bottom:0px !important; font-weight:bold" v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-align:left' : 'text-align:right'"> <span>{{ $t('TheDayStart.BankTotal') }}</span>:</td>
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
                                            <div class="row border border-dark ms-1 me-1" style="border:1px solid !important">

                                                <div class="col-sm-12 mt-2 mb-2 text-center" style="font-size :14px; font-weight:bold;color:black;">
                                                    <span>{{ $t('TheDayStart.TotalSale') }} <span style="color:black;font-size:13px">({{ $t('TheDayStart.Sales+Bank=') }})  </span> </span><span>{{getTotalSale()}}</span>
                                                </div>

                                            </div>

                                        </div>
                                    </div>


                                    <div class="row mt-2">
                                        <div class=" col-sm-12 ">
                                            <h5 class=" page_title"> {{ $t('TheDayStart.ReceiverInfo') }}</h5>

                                        </div>
                                        <div class="form-group has-label col-sm-6 ">
                                            <label class="text  font-weight-bolder"> {{ $t('TheDayStart.CashInHand') }} </label>
                                            <input class="form-control" v-model="total" type="number" disabled @click="$event.target.select()" />
                                        </div>

                                        <div class="form-group has-label col-sm-6 ">
                                            <label class="text  font-weight-bolder"> {{ $t('TheDayStart.VerifyCash') }}:<span class="text-danger"> *</span> </label>
                                            <input class="form-control" v-model="v$.dayEnd.verifyCash.$model" @focus="CheckReasonFalseDayEnd" @blur="CheckReasonForDayEnd(dayEnd.verifyCash)" type="number" />
                                        </div>
                                        <div class="form-group has-label col-sm-6 ">
                                            <label class="text  font-weight-bolder"> {{ $t('TheDayStart.NextDayOpeningCash') }}:<span class="text-danger"> *</span> </label>
                                            <input class="form-control" v-model="v$.dayEnd.nextDayOpeningCash.$model" type="number" @focus="$event.target.select()" :disabled="disableOpeningCash" @blur="CaluclateTrasnferCash(false)" />
                                        </div>
                                        <div class="form-group has-label col-sm-6 ">
                                            <label class="text  font-weight-bolder"> {{ $t('TheDayStart.CashTransfer') }}:<span class="text-danger"> *</span> </label>
                                            <input class="form-control" v-bind:disabled="transferCashInput" @focus="$event.target.select()" @blur="CaluclateTrasnferCash(true)" v-model="v$.dayEnd.carryCash.$model" type="number" />
                                        </div>


                                        <div class="form-group has-label col-sm-12 " v-if="reasonForEnd">
                                            <label class="text  font-weight-bolder"> {{ $t('TheDayStart.Reason') }}:<span class="text-danger"> *</span> </label>
                                            <input class="form-control" v-model="v$.dayEnd.creditReason.$model" type="text" @update:modelValue="ReasonIsNotEmpty(dayEnd.creditReason)" />
                                        </div>
                                        <div class="form-group has-label col-sm-6 " v-if="dayEnd.carryCash>0 && dayEnd.isSupervisor == false">
                                            <label class="text  font-weight-bolder"> {{$t('TheDayStart.CashReceivedBy')}}: </label>
                                            <usersDropdown :isSupervisor="true" v-model="dayEnd.supervisorName"></usersDropdown>
                                        </div>
                                        <div class="form-group has-label col-sm-6 " v-if="dayEnd.carryCash>0 && dayEnd.isSupervisor == false">
                                            <label class="text  font-weight-bolder"> {{$t('TheDayStart.Password')}}: </label>
                                            <input class="form-control" v-model="dayEnd.supervisorPassword" type="password" />
                                        </div>
                                        <div class="form-group has-label col-sm-6 " v-if="!iSupervisorLogin">
                                            <label class="text  font-weight-bolder"> {{$t('TheDayStart.UserName')}}:<span class="text-danger"> *</span> </label>
                                            <input class="form-control" v-model="v$.dayEnd.user.$model" type="text" />
                                        </div>
                                        <div class="form-group has-label col-sm-6 " v-if="!iSupervisorLogin">
                                            <label class="text  font-weight-bolder">{{$t('TheDayStart.Password')}}:<span class="text-danger"> *</span> </label>
                                            <input class="form-control" v-model="v$.dayEnd.password.$model" type="password" />
                                        </div>
                                    </div>
                                </div>

                                <div class="modal-footer justify-content-right" v-if="type=='Edit'">

                                    <button type="button" class="btn btn-primary  " :key="onesRander" v-on:click.once="SaveDayEnd" v-bind:disabled="v$.dayEnd.$invalid  "> {{ $t('TheDayStart.btnUpdate') }}</button>
                                    <button type="button" class="btn btn-danger mr-3 " v-on:click="close()" data-bs-dismiss="offcanvas" aria-label="Close">{{ $t('TheDayStart.Close') }}</button>

                                </div>
                                <div class="modal-footer justify-content-right" v-else>

                                    <button type="button" class="btn btn-primary" v-if="((dayEnd.carryCash>0 || dayEnd.nextDayOpeningCash>0) || total== 0)  && !(dayEnd.carryCash<0 || dayEnd.nextDayOpeningCash<0)" :key="onesRander" v-on:click.once="SaveDayEnd" v-bind:disabled="v$.dayEnd.$invalid   ">{{ $t('TheDayStart.DayEnd') }}</button>
                                    <button type="button" class="btn btn-primary " :key="onesRander" v-else v-on:click.once="SaveDayEnd" disabled>{{ $t('TheDayStart.DayEnd') }}</button>
                                    <button type="button" class="btn btn-danger mr-3 " v-on:click="close()" data-bs-dismiss="offcanvas" aria-label="Close">{{ $t('TheDayStart.Close') }}</button>

                                </div>
                            </div>
                        </div>
                    </div>
                    <dayEndReportPrint :headerFooter="headerFooter" :printDetail="printDetail" v-if="printReport" v-bind:key="printRender"></dayEndReportPrint>
                    <dayEndA4ReportPrint :headerFooter="headerFooter" :printDetail="printDetail" v-if="printA4Report" v-bind:key="printRender"></dayEndA4ReportPrint>
                </div>



            </div>
            <supervisor-login-model @close="onCloseEvent"
                                    :isFlushData=" false"
                                    :show="show4"
                                    :isReset="false"
                                    v-if="show4" />

            <cash-receiving-model :paidAmountProp="paidAmount"
                                  :inActiveDayId="inActiveDayId"
                                  v-if="receivingCash"
                                  :show="receivingCash"
                                  @close="receivingCash = false"
                                  :type="type" />




        </div>
        <invoicedetailsprint :show="showReport" v-if="showReport" :reportsrc="reportsrc"  :changereport="changereport" @close="IsClose" @IsSave="IsSave" />

        <dayEndReportPrint :headerFooter="headerFooter" :printDetail="printDetailForRePrint" v-if="reprint" v-bind:key="reprintRender"></dayEndReportPrint>
        <dayEndA4ReportPrint :headerFooter="headerFooter" :printDetail="printDetailForRePrint" v-if="reprintA4" v-bind:key="reprintRender"></dayEndA4ReportPrint>
        <loading v-model:active="loading"
                 :can-cancel="true"
                 :is-full-page="true"></loading>

    </div>
    
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, requiredIf, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import moment from "moment";
      import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
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
                reportsrc:'',
                changereport:0,
                showReport:false,

                isBankDetailShow: false,
                superAdminDayStart: false,
                nobleRole: '',
                terminalType: 'CashCounter',
                terminalUserType: localStorage.getItem('TerminalUserType'),
                searchQuery: '',
                activeTab: '',
                show: false,
                dayDesign: false,
                transferCounter: false,
                show1: false,
                reprint: false,
                reprintA4: false,
                show2: false,
                show3: false,
                show4: false,
                show5: false,
                onesRander: 0,
                receivingCash: false,
                paidAmount: 0,
                reprintRender: 0,
                inActiveDayId: '00000000-0000-0000-0000-000000000000',
                reason: false,
                reasonForEnd: false,
                reasonIsRequired: false,
                IsTransferAllow: false,
                DayStartDisable: false,
                counterCode: null,
                loginDisabled: false,
                counterId: '00000000-0000-0000-0000-000000000000',
                openingCash: '',
                type: '',
                outStandingBalance: 0,
                type1: '',
                CompanyID: '',
                UserID: '',
                employeeId: '',
                expense: '',
                cashInHand: '',
                bankDetailList: [],
                bankDetailListTotal: 0,
                dayWiseList: [],
                bank: '',
                dayStartId: '00000000-0000-0000-0000-000000000000',
                isDayAlreadyStart: false,
                loading: false,
                dayCounter: '0',
                counterList: [],
                dayStarts: [],
                inactivedayStarts: [],
                transferHistories: [],
                permissionToStartExpenseDay: false,
                isExpenseDay: false,
                isFirstUser: false,
                isSupervisor: false,
                IsPermissionToStartDay: false,
                IsPermissionToCloseDay: false,
                iSupervisorLogin: false,
                iSupervisorStartDay: false,
                randerList: 0,
                totalCash: 0,
                terminalRander: 0,
                isView: false,

                options: {
                    xaxis: {
                        categories: ['OPENING CASH', 'EXPENSE', 'BANK', 'CASH IN HAND', 'Total Cash', 'TOTAL']
                    }
                },
                series: [{
                    name: '',
                    data: []
                }],

                chartOptions: {
                    labels: ['OPENING CASH', 'EXPENSE', 'BANK', 'CASH IN HAND', 'TOTAL Cash', 'TOTAL']
                },
                series9: [],
                render: 0,
                designRander: 0,
                time: 0,
                dateRander: 0,
                UserName: '',
                isOpenDay: '',
                dayStart: {
                    id: '00000000-0000-0000-0000-000000000000',
                    date: '',
                    fromTime: '',
                    toTime: '',
                    isActive: true,
                    counterCode: '',
                    locationId: '',
                    openingCash: 0,
                    verifyCash: null,
                    user: '',
                    password: '',
                    reason: '',
                    counterId: null,
                    isExpenseDay: false,
                    isFirstUser: false,
                },
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
                transferDayCounter: {
                    id: '00000000-0000-0000-0000-000000000000',
                    date: '',
                    fromTime: '',
                    toTime: '',
                    isActive: true,
                    counterCode: '',
                    locationId: '',
                    openingCash: 0,
                    verifyCash: null,
                    user: '',
                    password: '',
                    toUser: '',
                    toPassword: '',
                    counterId: '00000000-0000-0000-0000-000000000000',
                    isExpenseDay: false,
                    isFirstUser: false,
                    isTransfer: true,
                    expenseCash: 0,
                    bankExpense: 0,
                    bankAmount: 0,
                    cashInHand: 0,
                    creditReason: '',
                    postExpense: 0,
                    draftExpense: 0,
                },

                //Day End
                printReport: false,
                printA4Report: false,
                printRender: 0,
                date: '',
                draftExpense: '',
                InvoiceSetting: '',
                startTime: '',
                total: 0,
                grandTotal: 0,
                transactions: 0,
                counterRander: 0,
                DisableRander: 0,
                transferCashInput: false,
                disableOpeningCash: false,
                bankDetails: [],
                // Day End Variable End
                printDetailForRePrint: [],

                // Transfer Counter
                isTransferPrint: true,
                totalInvoices: 0,
                fromInvoice: '',
                nextDayOpeningCash: 0,
                carryCashOnTrasnfer: 0,
                toInvoice: '',
                totalReturnCount: 0,
                totalReturnValue: '',
                //Transfer Counter Varaible End





                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                filePath: null,
            }
        },
        validations() {
          return{
              dayStart: {
                date: {
                    required
                },
                fromTime: {
                    required,
                    isValid: function (value) {
                        if (value.includes('mm') || value.includes('HH')) {
                            return false
                        }
                        return true;
                    }
                },
                verifyCash: {
                    required
                },
                user: {
                    required
                },

                counterId: {
                    required
                },
                password: {
                    required
                },
                reason: {
                    required: requiredIf(function() {
                  return parseFloat(this.dayStart.openingCash).toFixed(0) > parseFloat(this.dayStart.verifyCash).toFixed(0) || 
                     parseFloat(this.dayStart.openingCash).toFixed(0) < parseFloat(this.dayStart.verifyCash).toFixed(0) ? true : false;
                    }),

                    // required: requiredIf((x) => {
                    //     if (parseFloat(x.openingCash).toFixed(0) > parseFloat(x.verifyCash).toFixed(0) || parseFloat(x.openingCash).toFixed(0) < parseFloat(x.verifyCash).toFixed(0))
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(200)
                },


            },
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

            transferDayCounter: {
                date: {
                    required
                },
                verifyCash: {
                    required
                },
                toUser: {
                    required
                },

                toPassword: {
                    required
                }
            }

          }

        },

        methods: {
            IsClose:function () {
                this.showReport = false;
                this.$router.go();
            },
            IsSave:function () {
                this.showReport = !this.showReport;
            },
            RePrint: function () {




                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;
                            root.reprintRender++;
                            var InvoiceSetting = localStorage.getItem('PrintSizeA4');
                            if (InvoiceSetting == 'A4') {
                                root.reprintA4 = true;
                            }
                            else {
                                root.reprint = true;

                            }

                        }
                    });





            },
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            PrintDayTransfer: function () {

                this.GetHeaderDetail();
                this.printRender++;
                if (this.InvoiceSetting == 'A4') {
                    this.printA4Report = true;
                    //this.logout();
                }
                else {
                    this.printReport = true;

                }

            },

            ReceivingCash: function (amount, id) {
                this.paidAmount = amount;
                this.inActiveDayId = id;
                this.receivingCash = !this.receivingCash
            },


            //Main Page Section Button ,List,Cards
            StartOperationReset: function () {
                this.$router.go();

            },
            onCloseEvent: function (data) {

                this.loginDisabled = data;

                if (data) {

                    var root = this;
                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    localStorage.setItem('IsSupervisor', true);
                    localStorage.setItem('iSupervisorLogin', true);
                    this.dayStart.user = localStorage.getItem('SupervisorUserName')
                    this.dayStart.password = localStorage.getItem('SupervisorPassword')
                    this.dayEnd.user = localStorage.getItem('SupervisorUserName')
                    this.dayEnd.password = localStorage.getItem('SupervisorPassword')
                    root.iSupervisorLogin = true;
                    root.$https.get('/Product/IsDayStart?userId=' + this.UserID + '&employeeId=' + this.employeeId + '&isSupervisor=' + true, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data.isDayStart) {
                                root.isFirstUser = response.data.isFirstUser;
                                root.dayStarts = response.data.dayStarts;
                                root.transferHistories = response.data.transferHistories;
                                root.bank = response.data.bank;
                                root.cashInHand = response.data.cashInHand;
                                root.expense = response.data.expense;
                                root.openingCash = response.data.openingCash;
                                root.openingCash = response.data.openingCash;
                                root.totalCash = response.data.totalCash;

                                root.series[0].data = [];
                                root.series9 = [];
                                root.series[0].data.push(root.openingCash);
                                root.series[0].data.push(root.expense);
                                root.series[0].data.push(root.bank);
                                root.series[0].data.push(root.cashInHand);
                                root.series[0].data.push(root.totalCash);
                                root.series9 = root.series[0].data;
                            }
                            root.inactivedayStarts = response.data.inacticeDayStarts;
                            root.dayWiseList = response.data.dayWiseList;

                            root.randerList++;
                            root.loading = false;

                        },
                            function (error) {
                                root.loading = false;
                                console.log(error);
                            });
                }
                this.show4 = false
            },
            SupervisorLogout: function () {
                localStorage.setItem('iSupervisorLogin', false);
                localStorage.setItem('IsSupervisor', false);

                this.$router.go();
                //localStorage.setItem('SupervisorId', null);
                //localStorage.setItem('SupervisorUserName', null);
                //localStorage.setItem('SupervisorPassword', null);
                this.loginDisabled = false;

                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Success!' : 'النجاح',
                    text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Logout Successfully' : 'تم تسجيل الخروج بنجاح', 
                    type: 'success',
                    icon: 'success',
                    showConfirmButton: false,
                    timer: 1500,
                    timerProgressBar: true,
                });
            },
            GetDetailOfInvoices: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Product/GetDetailOfInvoices', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            console.log("error: something wrong from db.");
                        }

                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
            GetReport: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Product/GetDayStartReport', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            console.log("error: something wrong from db.");
                        }

                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
            Cancel: function () {

                this.dayDesign = false;
                this.designRander++;

            },
            CheckReason: function (x) {


                if (parseFloat(this.dayStart.openingCash) > x || parseFloat(this.dayStart.openingCash) < x) {
                    this.reason = true;
                }
                else {
                    this.reason = false;
                }



            },
            CheckReasonForDayEnd: function (x) {
                if (x) {
                    if (parseFloat(this.total).toFixed(0) != parseFloat(this.dayEnd.verifyCash).toFixed(0)) {
                        this.reasonForEnd = true;
                        this.reasonIsRequired = true;
                    }
                    else {
                        this.reasonForEnd = false;
                    }
                }
            },
            ReasonIsNotEmpty: function (x) {

                if (x != '') {
                    this.reasonIsRequired = false;
                }
                else {
                    this.reasonIsRequired = true;
                }
            },
            CheckReasonFalse: function () {

                this.reason = false;
            },
            CheckReasonFalseDayEnd: function () {
                this.reasonForEnd = false;
            },
            loadChartsDetails() {

                var root = this;
                root.series = [{
                    name: '',
                    data: []
                }];
                if (root.isSupervisor) {
                    root.series[0].data.push(root.openingCash);
                    root.series[0].data.push(root.expense);
                    root.series[0].data.push(root.bank);
                    root.series[0].data.push(root.cashInHand);
                    root.series[0].data.push(root.cashInHand + root.openingCash);
                    root.series[0].data.push(root.totalCash);
                    root.series9 = root.series[0].data;
                }
                else {

                    root.series[0].data.push(root.dayStarts[0].openingCash);
                    root.series[0].data.push(root.dayStarts[0].expenseCash);
                    root.series[0].data.push(root.dayStarts[0].bankAmount);
                    root.series[0].data.push(root.dayStarts[0].cashInHand);
                    root.series[0].data.push(root.dayStarts[0].cashInHand + root.dayStarts[0].openingCash);
                    root.series[0].data.push(root.dayStarts[0].totalCash);
                    root.series9 = root.series[0].data;
                }

                root.loadChart = true;
            },
            getOpeningBalance: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.dayStart.counterId = id;
                this.$https.get('/Product/OpeningBalance?counterId=' + id + '&isOpeningCash=' + true, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {


                        root.openingCash = parseFloat(response.data.openingBalance) < 0 ? parseFloat(parseFloat(response.data.openingBalance) * (-1)).toFixed(3).slice(0, -1) : parseFloat(response.data.openingBalance).toFixed(3).slice(0, -1)
                        root.dayStart.openingCash = root.openingCash

                    }

                });
            },
            getOpeningBalanceForTrasnfer: function () {


                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                //this.dayStart.counterId = id;
                this.$https.get('/Product/OpeningBalance?counterId=' + localStorage.getItem('CounterId') + '&isOpeningCash=' + false, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {

                        root.transferDayCounter.openingCash = parseFloat(response.data.openingBalance) < 0 ? parseFloat(parseFloat(response.data.openingBalance) * (-1)).toFixed(3).slice(0, -1) : parseFloat(response.data.openingBalance).toFixed(3).slice(0, -1)

                        //var cashInHand = parseFloat(parseFloat(response.data.cashInHand) - parseFloat(response.data.postExpense)).toFixed(3).slice(0, -1)
                        root.transferDayCounter.cashInHand = parseFloat(response.data.cashInHand).toFixed(3).slice(0, -1)
                        root.transferDayCounter.bankAmount = parseFloat(response.data.bank).toFixed(3).slice(0, -1)
                        root.transferDayCounter.expenseCash = parseFloat(response.data.postExpense).toFixed(3).slice(0, -1)
                        root.transferDayCounter.bankExpense = parseFloat(response.data.bankExpense).toFixed(3).slice(0, -1)
                        root.transferDayCounter.counterCode = response.data.terminalCode;
                        var totalVat = response.data.totalVat;
                        root.transferDayCounter.totalVat = totalVat < 0 ? totalVat.toFixed(3).slice(0, -1) * (-1) : parseFloat(totalVat).toFixed(3).slice(0, -1)
                        root.total = parseFloat(parseFloat(root.transferDayCounter.cashInHand)).toFixed(3).slice(0, -1);
                        root.transactions = response.data.noOfTransaction
                        root.startTime = response.data.startTime;
                        root.date = response.data.date;
                        root.bankDetails = response.data.bankDetails;
                        root.transferDayCounter.postExpense = parseFloat(response.data.postExpense).toFixed(3).slice(0, -1);
                        //root.transferDayCounter.draftExpense = parseFloat(response.data.draftExpense).toFixed(3).slice(0, -1);
                        /*  root.total = total < 0 ? total * (-1) : parseFloat(total).toFixed(3).slice(0, -1) ;*/
                        root.grandTotal = parseFloat(root.total).toFixed(3).slice(0, -1);
                        //root.total = 1;
                        root.counterRander++
                    }
                });



            },
            getTerminalByUserId: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/Product/GetTerminalByUserId', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data) {

                        root.dayStart.counterId = response.data;
                        root.terminalRander++;
                    }

                });
            },
            isDayStart: function () {
                
                var root = this;
                var url = '';
                if (this.iSupervisorLogin == true) {
                    url = '/Product/IsDayStart?userId=' + this.UserID + '&employeeId=' + this.employeeId + '&isSupervisor=' + root.iSupervisorLogin
                }
                else {
                    url = '/Product/IsDayStart?userId=' + this.UserID + '&employeeId=' + this.employeeId + '&isSupervisor=' + root.isSupervisor
                }
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loading = true;

                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isDayStart) {

                            root.isDayAlreadyStart = true;
                            //root.dayCounter = response.data.dayCount.length;
                            //root.counterList = response.data.dayCount;
                            root.isFirstUser = response.data.isFirstUser;
                            root.dayStarts = response.data.dayStarts;
                            root.transferHistories = response.data.transferHistories;
                            root.bank = response.data.bank;
                            root.cashInHand = parseFloat(response.data.cashInHand).toFixed(3).slice(0, -1);
                            root.expense = parseFloat(response.data.expense).toFixed(3).slice(0, -1);
                            root.draftExpense = parseFloat(response.data.draftExpense).toFixed(3).slice(0, -1);
                            root.postExpense = parseFloat(response.data.postExpense).toFixed(3).slice(0, -1);
                            root.bankExpense = parseFloat(response.data.bankExpense).toFixed(3).slice(0, -1);
                            root.openingCash = parseFloat(response.data.openingCash).toFixed(3).slice(0, -1);
                            root.openingCash = parseFloat(response.data.openingCash).toFixed(3).slice(0, -1);
                            root.totalCash = parseFloat(response.data.totalCash).toFixed(3).slice(0, -1);
                            root.inactivedayStarts = response.data.inacticeDayStarts;
                            root.outStandingBalance = response.data.outStandingBalance;
                            root.dayWiseList = response.data.dayWiseList;
                            root.totalInvoices = response.data.totalInvoices;
                            root.fromInvoice = response.data.fromInvoice;
                            root.toInvoice = response.data.toInvoice;
                            root.totalReturnCount = response.data.totalReturnCount;
                            root.totalReturnValue = response.data.totalReturnValue;
                            //localStorage.setItem('token', response.data.token);
                            localStorage.setItem('IsExpenseDay', response.data.isExpenseDay);

                            root.loadChartsDetails();

                        }
                        else {
                            root.dayStarts = response.data.dayStarts;
                            root.transferHistories = response.data.transferHistories;
                            root.isDayAlreadyStart = false;
                            root.inactivedayStarts = response.data.inacticeDayStarts;
                            root.outStandingBalance = response.data.outStandingBalance;
                            root.permissionToStartExpenseDay = response.data.isExpenseDay;
                            root.bank = response.data.bank;
                            root.cashInHand = parseFloat(response.data.cashInHand).toFixed(3).slice(0, -1);
                            root.expense = parseFloat(response.data.expense).toFixed(3).slice(0, -1);
                            root.draftExpense = parseFloat(response.data.draftExpense).toFixed(3).slice(0, -1);
                            root.postExpense = parseFloat(response.data.postExpense).toFixed(3).slice(0, -1);
                            root.bankExpense = parseFloat(response.data.bankExpense).toFixed(3).slice(0, -1);
                            root.openingCash = parseFloat(response.data.openingCash).toFixed(3).slice(0, -1);
                            root.openingCash = parseFloat(response.data.openingCash).toFixed(3).slice(0, -1);
                            root.totalCash = parseFloat(response.data.totalCash).toFixed(3).slice(0, -1);
                            root.dayWiseList = response.data.dayWiseList;
                            localStorage.setItem('IsExpenseDay', response.data.isExpenseDay);
                        }
                        root.loading = false;

                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });
            },
            close: function () {

                this.dayDesign = false;
                this.dayStart = {
                    id: '00000000-0000-0000-0000-000000000000',
                    date: '',
                    fromTime: '',
                    toTime: '',
                    isActive: true,
                    counterCode: '',
                    locationId: '',
                    openingCash: 0,
                    verifyCash: null,
                    user: '',
                    password: '',
                    reason: '',
                    counterId: '',
                    isExpenseDay: false,
                    isFirstUser: false,
                };
                this.dayEnd = {
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
                    nextDayOpeningCash: 0,
                    isSupervisor: false,
                };
                this.transferDayCounter = {
                    id: '00000000-0000-0000-0000-000000000000',
                    date: '',
                    fromTime: '',
                    toTime: '',
                    isActive: true,
                    counterCode: '',
                    locationId: '',
                    openingCash: 0,
                    verifyCash: null,
                    user: '',
                    password: '',
                    toUser: '',
                    toPassword: '',
                    counterId: '00000000-0000-0000-0000-000000000000',
                    isExpenseDay: false,
                    isFirstUser: false,
                    isTransfer: true,
                    expenseCash: 0,
                    bankAmount: 0,
                    cashInHand: 0,
                };
                this.show4 = false;

                if (localStorage.getItem('iSupervisorLogin') == 'true') {
                    this.loginDisabled = true;
                    this.isSupervisor = false;
                    this.iSupervisorLogin = true;
                    this.dayStart.user = localStorage.getItem('SupervisorUserName')
                    this.dayStart.password = localStorage.getItem('SupervisorPassword')
                    this.dayEnd.user = localStorage.getItem('SupervisorUserName')
                    this.dayEnd.password = localStorage.getItem('SupervisorPassword')
                    this.dayEnd.isSupervisor = true
                }

            },
            //Main Page Section End

            //Transfer Counter Section

            TransferCounterCard: function () {
                if (!this.isDayAlreadyStart)
                    return;
                //this.dayDesign = true;
                this.activeTab = 'Transfer';
                if (this.transferDayCounter.id == '00000000-0000-0000-0000-000000000000' || this.transferDayCounter.id == undefined || this.transferDayCounter.id == '') {
                    this.transferDayCounter.fromTime = moment().format('HH:mm');
                    this.transferDayCounter.date = moment().format('llll');
                    this.dateRander++;
                    this.time++;
                        this.UserName = localStorage.getItem('LoginUserName');
                        this.CompanyID = localStorage.getItem('CompanyID');
                        this.UserID = localStorage.getItem('UserID');
                        this.isOpenDay = localStorage.getItem('IsOpenDay') == 'true' ? true : false;
                        this.InvoiceSetting = localStorage.getItem('PrintSizeA4');
                    
                    this.transferDayCounter.isOpenDay = this.isOpenDay;
                    this.transferDayCounter.counterCode = this.counterCode;
                    this.transferDayCounter.openingCash = parseFloat(this.openingCash).toFixed(3).slice(0, -1);
                    this.transferDayCounter.counterId = this.counterId;
                    this.transferDayCounter.isExpenseDay = this.isExpenseDay;
                    this.getOpeningBalanceForTrasnfer();

                }

            },
            //Transfer Counter Section End
            GetModuleWiseClaims: function () {
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('SimpleToken');
                }

                var root = this;
                this.$https.get('/Company/GetModuleWiseClaims', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        response.data.forEach(function (x) {

                            localStorage.setItem(x.tokenName + '_token', x.token)
                        })
                        root.$router.go();
                    }
                    
                }).catch(error => {

                    console.log(error)
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: error.response.data,

                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                    root.loading = false
                    root.onesRander++
                })
            },
            //Day Start Section
            SaveDayStart: function () {
                

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                localStorage.setItem('CounterId', this.dayStart.counterId);
                this.dayStart.isSupervisorLogin = localStorage.getItem('iSupervisorLogin') == 'true' ? true : false;
                localStorage.setItem('DayStartTime', this.dayStart.fromTime);
                this.dayStart.isFirstUser = this.isFirstUser;
                if (this.dayStart.id == '00000000-0000-0000-0000-000000000000' || this.dayStart.id == undefined || this.dayStart.id == '') {
                    this.dayStart.saleId = this.UserID;
                    this.dayStart.locationId = this.CompanyID;
                }
                
                this.$https.post('/Product/SaveDayStart?isDayStart=' + true, this.dayStart, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data.dayId != '00000000-0000-0000-0000-000000000000') {

                        {

                            root.$swal({
                                title: root.$t('TheDayStart.SavedSuccessfully'),
                                text: root.$t('TheDayStart.Saved'),
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });

                            var time = moment().format('h:mm a');
                            localStorage.setItem('CounterCode', root.dayStart.counterCode);
                            localStorage.setItem('DayStartTime', time);
                            localStorage.setItem('IsDayStart', true);


                            localStorage.setItem('token', response.data.result.token);
                            localStorage.setItem('IsExpenseDay', response.data.result.isExpenseDay);
                            localStorage.setItem('PrinterName', response.data.result.printerName);
                            root.GetModuleWiseClaims();
                            var salesMan = localStorage.getItem('SalesMan');
                            if (salesMan == 'Sales Man') {
                                if (localStorage.getItem('isTouchInvoice') == 'true') {
                                    root.$router.push('/TouchScreen');
                                }
                                else {
                                    root.$router.push('/TouchScreen');
                                }
                            }

                            

                        }
                    }
                    else {
                        root.$swal({
                            title: root.$t('TheDayStart.Error'),
                            text: root.$t('TheDayStart.DayStartAlreadyExist'),
                            type: 'error',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                        });
                        root.onesRander++
                    }
                }).catch(error => {

                    console.log(error)
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: error.response.data,

                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                    root.loading = false
                    root.onesRander++
                })
            },
            CaluclateTrasnferCash: function (isTrasnfer) {

                if (this.dayEnd.nextDayOpeningCash > 0 && isTrasnfer == false) {
                    this.transferCashInput = true;
                    this.dayEnd.carryCash = parseFloat(parseFloat(this.dayEnd.verifyCash) - parseFloat(this.dayEnd.nextDayOpeningCash)).toFixed(3).slice(0, -1);
                }
                else if (this.dayEnd.nextDayOpeningCash == 0 && isTrasnfer == false) {
                    this.disableOpeningCash = false;
                    this.transferCashInput = false;
                    this.dayEnd.nextDayOpeningCash = 0;
                    this.dayEnd.carryCash = 0;
                }
                else if (this.dayEnd.carryCash > 0 && isTrasnfer == true) {
                    this.disableOpeningCash = true;
                    this.dayEnd.nextDayOpeningCash = parseFloat(parseFloat(this.dayEnd.verifyCash) - parseFloat(this.dayEnd.carryCash)).toFixed(3).slice(0, -1);
                }
                else if (this.dayEnd.carryCash == 0 && isTrasnfer == true) {
                    this.disableOpeningCash = false;
                    this.transferCashInput = false;
                    this.dayEnd.nextDayOpeningCash = 0;
                    this.dayEnd.carryCash = 0;
                }

            },
            DayStartCardClick: function () {
                if (this.isDayAlreadyStart || (!this.IsPermissionToStartDay && !this.iSupervisorLogin)) {
                    return;
                }

                //this.dayDesign = true;
                //this.activeTab = 'Start';
                var root = this;
                var token = '';

                if (this.dayStart.id == '00000000-0000-0000-0000-000000000000' || this.dayStart.id == undefined || this.dayStart.id == '') {
                    this.dayStart.fromTime = moment().format('HH:mm');
                    this.dayStart.date = moment().format('llll');
                    this.dateRander++;
                    this.time++;
                    this.UserName = localStorage.getItem('LoginUserName');
                    this.CompanyID = localStorage.getItem('CompanyID');
                    this.UserID = localStorage.getItem('UserID');
                    this.isOpenDay = localStorage.getItem('IsOpenDay') == 'true' ? true : false;
                    this.SupervisorLogin = localStorage.getItem('IsSupervisor') == 'true' ? true : false;
                    this.dayStart.isOpenDay = this.isOpenDay;
                    this.dayStart.counterCode = this.counterCode;
                    this.dayStart.openingCash = parseFloat(this.openingCash).toFixed(3).slice(0, -1);
                    this.dayStart.counterId = this.counterId;
                    this.dayStart.isExpenseDay = this.isExpenseDay;
                    this.getTerminalByUserId();

                }
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.permissionToStartExpenseDay) {
                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Confirm!' : 'يتأكد!',
                        text: "Are You Sure? Want To Start Day Expense!",
                        type: 'warning',
                        icon: 'warning',
                        showConfirmButton: true,
                        confirmButtonText: 'Yes, I am sure!',
                        cancelButtonText: "No, Start Normal!",
                        showCancelButton: true,
                        confirmButtonColor: '#014c8c',
                        cancelButtonColor: '#ef8157'
                    }).then(function (response) {
                        if (response != undefined) {
                            if (response.isConfirmed) {

                                root.$https.get('/Product/IsDayStart?userId=' + root.UserID + '&employeeId=' + root.employeeId + '&isSupervisor=' + root.isSupervisor, { headers: { "Authorization": `Bearer ${token}` } })
                                    .then(function (response) {
                                        if (response.data.isDayStart) {
                                            root.isFirstUser = response.data.isFirstUser;
                                            console.log(response.date);
                                        }

                                    },
                                        function (error) {
                                            root.loading = false;
                                            console.log(error);
                                        }).catch(error => {

                                            console.log(error)
                                            root.$swal.fire(
                                                {
                                                    icon: 'error',
                                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هناك خطأ ما!', 
                                                    text: error.response.data,

                                                    showConfirmButton: false,
                                                    timer: 5000,
                                                    timerProgressBar: true,
                                                });

                                            root.loading = false
                                        })
                            }
                            else {
                                root.$https.get('/Product/IsDayStart?userId=' + root.UserID + '&employeeId=' + root.employeeId + '&isSupervisor=' + root.isSupervisor, { headers: { "Authorization": `Bearer ${token}` } })
                                    .then(function (response) {
                                        if (response.data.isDayStart) {
                                            root.isFirstUser = response.data.isFirstUser;
                                            console.log(response.date);
                                        }
                                        else {
                                            root.dayStartId = '00000000-0000-0000-0000-000000000000';

                                            root.$https.get('/Product/GetCounterInformation?userId=' + root.UserID + '&employeeId' + root.employeeId + '&isDayStart=' + true, { headers: { "Authorization": `Bearer ${token}` } })
                                                .then(function (response) {
                                                    if (response.data.counterCode != null) {
                                                        root.counterCode = response.data.counterCode;
                                                        root.openingCash = response.data.openingCash;
                                                        //root.counterId = response.data.counterId;

                                                        root.isExpenseDay = false;
                                                    } else {
                                                        console.log(response.date);
                                                    }
                                                },
                                                    function (error) {
                                                        root.loading = false;
                                                        console.log(error);
                                                    }).then(function () {
                                                        if (root.counterCode != null && root.counterCode != '' && root.counterCode != undefined) {
                                                            root.dayDesign = !root.dayDesign;
                                                            root.type = "Add";
                                                        }

                                                    });
                                        }
                                    },
                                        function (error) {
                                            root.loading = false;
                                            console.log(error);
                                        }).catch(error => {

                                            console.log(error)
                                            root.$swal.fire(
                                                {
                                                    icon: 'error',
                                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هناك خطأ ما!',
                                                    text: error.response.data,

                                                    showConfirmButton: false,
                                                    timer: 5000,
                                                    timerProgressBar: true,
                                                });

                                            root.loading = false
                                        })
                            }

                        }
                    });
                }
                else {


                    root.$https.get('/Product/IsDayStart?userId=' + root.UserID + '&employeeId=' + root.employeeId + '&isSupervisor=' + root.isSupervisor, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {

                            if (response.data.isDayStart) {
                                root.isFirstUser = response.data.isFirstUser;
                                console.log(response.date);
                            }
                            else {
                                root.isFirstUser = response.data.isFirstUser;
                                root.dayStarts = response.data.dayStarts;
                                root.dayStartId = '00000000-0000-0000-0000-000000000000';


                            }
                        },
                            function (error) {
                                root.loading = false;
                                console.log(error);
                            });
                }

            },
            //Day Start Section End





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

                var companyId = '';
                    companyId = localStorage.getItem('CompanyID');
                
                 
                var IsDayStart=localStorage.getItem('IsDayStart');
                var CounterId=localStorage.getItem('CounterId');
                this.reportsrc=   this.$ReportServer+'/Invoice/A4_DefaultTempletForm.aspx?CompanyId=' + companyId+'&formName=DayEnd'+'&IsDayStart='+IsDayStart+'&CounterId='+CounterId 
                this.changereport++;
                this.showReport = !this.showReport;

            },
            getTimeOnly: function (x) {
                return moment(x).format('lll');
            },
            TotalCash: function () {

                return parseFloat(parseFloat(this.dayEnd.cashInHand) - parseFloat(this.dayEnd.openingCash) + parseFloat(this.dayEnd.expenseCash)).toFixed(3).slice(0, -1);
            },
            TotalCashForView: function () {

                return parseFloat(parseFloat(this.dayEnd.cashInHand) + parseFloat(this.dayEnd.expenseCash)).toFixed(3).slice(0, -1);
            },
            getTotalSale: function () {
                return parseFloat(parseFloat(this.TotalCash()) + parseFloat(this.dayEnd.bankAmount)).toFixed(3).slice(0, -1);
            },
            getTotalSaleForView: function () {
                return parseFloat(parseFloat(this.TotalCashForView()) + parseFloat(this.dayEnd.bankAmount)).toFixed(3).slice(0, -1);
            },
            calculateGrandTotal: function () {


                //this.grandTotal = parseFloat(parseFloat(this.dayEnd.verifyCash) - parseFloat(this.dayEnd.carryCash)).toFixed(3).slice(0, -1);

            },
            DisableTranferCounter: function () {

                this.carryCashOnTrasnfer = this.dayEnd.carryCash;


            },
            EndOperationCard: function (data) {

                if (!this.isDayAlreadyStart || (!this.IsPermissionToCloseDay && !this.iSupervisorLogin))
                    return;
                var root = this;

                //this.dayDesign = true;
                this.activeTab = 'End';
                this.isView = data == true ? true : false
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Product/IsDayStart?userId=' + this.UserID + '&employeeId' + this.employeeId + '&isSupervisor=' + root.isSupervisor, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isDayStart) {
                            root.dayStartId = response.data.dayStartId;
                            root.isFirstUser = response.data.isFirstUser;
                            root.cashInHand = response.data.cashInHand;
                            //root.counterId = response.data.counterId;
                            root.bank = response.data.bank;
                            root.openingCash = parseFloat(response.data.openingCash).toFixed(3).slice(0, -1);
                            root.show1 = !root.show1;
                            root.type = "Add";

                        } else {
                            console.log(response.date);
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });



                this.getOpeningBalanceForDayEnd();
                if (this.dayEnd.id == '00000000-0000-0000-0000-000000000000' || this.dayEnd.id == undefined || this.dayEnd.id == '') {
                    this.dayEnd.toTime = moment().format('HH:mm');
                    this.dayEnd.date = moment().format('llll');
                    this.dateRander++;
                    this.time++;
                    if (token == '') {
                        this.UserName = localStorage.getItem('LoginUserName');
                        if (localStorage.getItem('iSupervisorLogin') == 'true') {
                            this.dayEnd.user = localStorage.getItem('SupervisorUserName')
                            this.dayEnd.endTerminalBy = localStorage.getItem('SupervisorUserName');
                            this.dayEnd.endTerminalFor = localStorage.getItem('LoginUserName');
                        }
                        else {
                            this.dayEnd.user = localStorage.getItem('LoginUserName');
                            this.dayEnd.endTerminalBy = localStorage.getItem('LoginUserName');
                            this.dayEnd.endTerminalFor = localStorage.getItem('LoginUserName');
                        }

                        this.InvoiceSetting = localStorage.getItem('PrintSizeA4');

                        var IsSupervisor = localStorage.getItem('IsSupervisor');
                        if (IsSupervisor == 'true') {
                            this.isSupervisor = true
                        }
                        else {
                            this.isSupervisor = false
                        }
                        //this.dayEnd.endTerminalBy = localStorage.getItem('LoginUserName');

                        this.role = localStorage.getItem('RoleName');
                    }
                    //this.dayEnd.counterCode = localStorage.getItem('CounterCode');
                    this.dayEnd.counterId = this.counterId;
                    this.dayEnd.openingCash = parseFloat(this.openingCash).toFixed(3).slice(0, -1);
                    this.dayEnd.cashInHand = parseFloat(this.cashInHand).toFixed(3).slice(0, -1);
                    this.total = parseFloat(parseFloat(this.cashInHand) + parseFloat(this.openingCash)).toFixed(3).slice(0, -1) - parseFloat(this.expense).toFixed(3).slice(0, -1);
                    this.dayEnd.expense = parseFloat(this.expense).toFixed(3).slice(0, -1);
                }
                this.SupervisorLogin = localStorage.getItem('IsSupervisor') == 'true' ? true : false;

                var isOpenDay = localStorage.getItem('IsOpenDay') == 'true' ? true : false;
                if (localStorage.getItem('iSupervisorLogin') == 'true') {
                    this.dayEnd.isSupervisor = true
                }
                else {
                    this.dayEnd.isSupervisor = this.isSupervisor;
                }
                this.dayEnd.isOpenDay = isOpenDay;
                this.dayEnd.id = this.dayStartId;



            },
            DisableInput: function (x) {

                if (x > 0) {
                    this.disableOpeningCash = false;
                    this.transferCashInput = true;
                }
                else {
                    this.disableOpeningCash = true;
                    this.transferCashInput = false;
                }
            },
            getOpeningBalanceForDayEnd: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                //this.dayStart.counterId = id;
                this.$https.get('/Product/OpeningBalance?counterId=' + localStorage.getItem('CounterId') + '&isOpeningCash=' + false, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

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
                        root.outStandingBalance = response.data.outStandingBalance;
                        //root.total = 1;
                        root.counterRander++
                    }
                });
            },
            SaveDayEnd: function () {
                
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.dayEnd.carryCash == null && this.dayEnd.carryCash == '') {
                    this.dayEnd.carryCash = 0;
                    if (this.dayEnd.carryCash == 0) {
                        this.dayEnd.supervisorName = '';
                        this.dayEnd.supervisorPassword = '';
                    }
                }
                //if (this.dayEnd.nxtDayOpeningCash > 0) {
                //    this.dayEnd.carryCash = this.dayEnd.nxtDayOpeningCash;
                //}
                this.isTransferPrint = false;
                this.dayEnd.id = this.dayStartId;
                this.dayEnd.isSupervisorLogin = this.SupervisorLogin;


                this.dayEnd.saleId = localStorage.getItem('UserID');

                this.dayEnd.locationId = localStorage.getItem('CompanyID');
                this.printDetail = this.dayEnd;
                this.printDetail.bankDetailListTotal = this.bankDetailListTotal;
                this.printDetail.bankDetailList = this.bankDetailList;
                this.printDetail.grandTotal = this.grandTotal;
                this.printDetail.total = this.total;
                this.printDetail.startTime = this.startTime;
                this.printDetail.startDate = this.startTime;
                this.printDetail.date = moment().format('lll');
                this.printDetail.totalCash = this.TotalCash();
                this.printDetail.getTotalSale = this.getTotalSale();
                this.printDetail.bankDetails = this.bankDetails;
                this.printDetail.transactions = this.transactions;
                //this.dayEnd.carryCash = this.dayEnd.nextDayOpeningCash;
                this.printDetail.dayWiseList = this.dayWiseList;
                this.printDetail.cashInHand = this.total;
                this.printDetail.outStandingBalance = this.outStandingBalance;
                localStorage.setItem("printDetailForRePrint", JSON.stringify(this.printDetail));
                root.dayEnd.remainingAmount = parseFloat(root.total - root.dayEnd.verifyCash)

                this.$https.post('/Product/SaveDayStart?isDayStart=' + false, this.dayEnd, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess) {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved' : 'تم الحفظ',
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                        });

                        localStorage.setItem('IsDayStart', false);
                        localStorage.setItem('isExpenseDay', false);
                        root.PrintDayEnd();


                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Your day Start End  Already Exist!' : 'يومك يبدأ ينتهي بالفعل',
                            type: 'error',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                        });
                        root.onesRander++
                    }
                }).catch(error => {

                    console.log(error)
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: error.response.data,

                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                    root.loading = false
                    root.onesRander++
                })
            },

            //Day End Section End
            SupervisorLogin: function () {

                this.show4 = !this.show4;
            },

            ViewCard: function (data) {

                var root = this;

                //this.dayDesign = true;
                this.activeTab = 'End';
                this.isView = true
                //var token = '';
                //if (token == '') {
                //    token = localStorage.getItem('token');
                //}

                //root.UserName = data.dayEndUser;

                root.dayEnd.counterCode = data.counterName;
                root.date = data.date;
                root.startTime = data.fromTime;
                root.dayEnd.openingCash = data.openingCash;
                root.dayEnd.cashInHand = parseFloat(data.cashInHand);
                root.dayEnd.expenseCash = parseFloat(data.postExpense).toFixed(3).slice(0, -1)
                root.dayEnd.bankExpense = parseFloat(data.bankExpense).toFixed(3).slice(0, -1)
                root.dayEnd.bankAmount = parseFloat(data.bankAmount).toFixed(3).slice(0, -1)
                root.bankDetails = data.bankDetails;

                /*  root.total = total < 0 ? total * (-1) : parseFloat(total).toFixed(3).slice(0, -1) ;*/
                root.total = parseFloat(parseFloat(data.cashInHand) + parseFloat(data.openingCash)).toFixed(3).slice(0, -1)

                root.grandTotal = parseFloat(root.total).toFixed(3).slice(0, -1);


            },

            SaveDayTransfer: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.transferDayCounter.isFirstUser = this.isFirstUser;
                this.transferDayCounter.user = this.userName;

                if (this.transferDayCounter.id == '00000000-0000-0000-0000-000000000000' || this.transferDayCounter.id == undefined || this.transferDayCounter.id == '') {
                    this.transferDayCounter.saleId = this.UserID;
                    this.transferDayCounter.locationId = this.CompanyID;
                }

                this.printDetail = this.transferDayCounter;
                this.printDetail.grandTotal = parseFloat(this.transferDayCounter.cashInHand).toFixed(3).slice(0, -1);
                this.printDetail.total = parseFloat(this.total).toFixed(3).slice(0, -1);
                this.printDetail.startTime = this.transferDayCounter.fromTime;
                this.printDetail.toTime = moment().format("HH:mm ");
                this.printDetail.startDate = this.transferDayCounter.date;
                this.printDetail.totalCash = parseFloat(parseFloat(this.transferDayCounter.cashInHand) - parseFloat(this.transferDayCounter.openingCash) + parseFloat(this.transferDayCounter.expenseCash)).toFixed(3).slice(0, -1);
                this.printDetail.getTotalSale = parseFloat(parseFloat(this.transferDayCounter.cashInHand) - parseFloat(this.transferDayCounter.openingCash) + parseFloat(this.transferDayCounter.expenseCash) + parseFloat(this.transferDayCounter.bankAmount)).toFixed(3).slice(0, -1);
                this.printDetail.bankDetails = this.bankDetails;
                this.printDetail.transactions = this.transactions;
                this.printDetail.counterCode = this.transferDayCounter.counterCode;

                this.printDetail.user = localStorage.getItem('LoginUserName');
                this.$https.post('/Product/SaveDayStart?isDayStart=' + true, this.transferDayCounter, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {

                        root.$swal({
                            title: root.$t('TheDayStart.SavedSuccessfully'),
                            text: root.$t('TheDayStart.Saved'),
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        //localStorage.setItem('token', response.data.result.token);
                        //var time = moment().format('h:mm a');
                        //localStorage.setItem('CounterCode', root.dayStart.counterCode);
                        //localStorage.setItem('DayStartTime', time);
                        //localStorage.setItem('IsDayStart', true);
                        //root.close();
                        //localStorage.setItem('IsExpenseDay', response.data.result.isExpenseDay);
                        root.PrintDayTransfer();



                        /*  root.$router.go('/daystart')*/

                    }
                    else {

                        root.$swal({
                            title: root.$t('TheDayStart.Error'),
                            text: root.$t('TheDayStart.DayStartAlreadyExist'),
                            type: 'error',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                        });
                        root.onesRander++
                    }
                }).catch(error => {

                    console.log(error)
                    root.$swal.fire(
                        {

                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: error.response.data,

                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                    root.loading = false
                    root.onesRander++
                })
            }
        },
        watch: {
            nextDayOpeningCash: function () {

                if (this.nextDayOpeningCash > 0) {

                    this.disableOpeningCash = false;
                    this.transferCashInput = true;
                    this.DisableRander++;
                }
                else {

                    this.disableOpeningCash = false;
                    this.transferCashInput = false;


                }



            }, carryCashOnTrasnfer: function () {

                if (this.carryCashOnTrasnfer > 0) {
                    this.disableOpeningCash = true;
                    this.transferCashInput = false;
                    this.DisableRander++;
                }
                else {
                    this.disableOpeningCash = false;
                    this.transferCashInput = false;
                }

                this.nextDayOpeningCash = parseFloat(parseFloat(this.nextDayOpeningCash) - parseFloat(this.carryCashOnTrasnfer)).toFixed(3).slice(0, -1);



            }
        },
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
            this.isBankDetailShow = localStorage.getItem('BankDetail') == 'true' ? true : false;
        },
        mounted: function () {

            

                this.dayDesign = false;
                this.UserName = localStorage.getItem('LoginUserName');
                this.superAdminDayStart = localStorage.getItem('SuperAdminDayStart') == 'true' ? true : false;
                var supervisorLogin = localStorage.getItem('iSupervisorLogin');
                if (supervisorLogin == "true") {
                    localStorage.setItem("iSupervisorLogin", true)
                    localStorage.setItem("IsSupervisor", false)
                    this.loginDisabled = true
                    this.iSupervisorLogin = true
                    this.dayStart.user = localStorage.getItem('SupervisorUserName')
                    this.dayStart.password = localStorage.getItem('SupervisorPassword')
                    this.dayEnd.user = localStorage.getItem('SupervisorUserName')
                    this.dayEnd.password = localStorage.getItem('SupervisorPassword')
                }
                this.nobleRole = localStorage.getItem('NobleRole');
                var IsSupervisor = localStorage.getItem('IsSupervisor');

                if (IsSupervisor == 'true') {
                    this.isSupervisor = true
                }
                else {
                    this.isSupervisor = false
                }
                var list = localStorage.getItem('printDetailForRePrint');
                this.printDetailForRePrint = JSON.parse(list);
                this.CompanyID = localStorage.getItem('CompanyID');
                this.UserID = localStorage.getItem('UserID');
                this.dayStart.counterId = localStorage.getItem('TerminalId');
                this.counterId = localStorage.getItem('CounterId');
                this.employeeId = localStorage.getItem('EmployeeId') == null || localStorage.getItem('EmployeeId') == undefined || localStorage.getItem('EmployeeId') == '' ? '' : localStorage.getItem('EmployeeId');
                if (this.employeeId == null) {
                    this.DayStartDisable = true;
                }
                this.isDayStart();

                this.IsTransferAllow = localStorage.getItem('IsTransferAllow') == 'true' ? true : false;
                this.IsPermissionToStartDay = localStorage.getItem('IsPermissionToStartDay') == 'true' ? true : false;
                this.IsPermissionToCloseDay = localStorage.getItem('IsPermissionToCloseDay') == 'true' ? true : false;

                //var supervisorId = localStorage.getItem('SupervisorId');
                //if (supervisorId != null) {
                //    this.loginDisabled = true;
                //    this.DayStartDisable = false
                //}
            
        }
    }
</script>
<style scoped>
   










    .opacity50 {
        opacity: 0.7 !important;
    }










    .cardHover:hover {

/*        box-shadow: rgba(50, 50, 93, 0.25) 0px 30px 60px -12px inset, rgba(0, 0, 0, 0.3) 0px 18px 36px -18px inset;
*/        box-shadow: #3178F6 0px 0px 0px 1px;
       /* -webkit-box-shadow: 1px 1px 1px 1px #3178F6;
        -moz-box-shadow: 1px 1px 1px 1px #3178F6;
        box-shadow: 1px 1px 1px 1px #3178F6;*/
        /*        border-bottom-color: #35353D !important;
    */
    }







   
 

</style>